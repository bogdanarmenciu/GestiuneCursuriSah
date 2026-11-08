using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using GestiunePrezente.Common;
using GestiunePrezente.MeniuPrincipal;
using GridPersonalizat;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace GestiunePrezente.MeniuAdministrare
{
    public partial class FrmCautareRegistruNumereInterne : Form
    {
        CManagerDeProiect ManagerDeProiect;
        FrmMeniuPrincipal FrmParent;
        string? m_sSql;
        int m_lADPG = 0;

        public FrmCautareRegistruNumereInterne(FrmMeniuPrincipal FormParent, CManagerDeProiect MM)
        {
            FrmParent = FormParent;
            ManagerDeProiect = MM;
            InitializeComponent();

            this.WindowState = FormWindowState.Maximized;
            CustomGrid.SetConnection(ManagerDeProiect.GetConnection());
            m_gRegistrulNumereInterne.ReadOnly = true;
            m_tsbStergere.Enabled = false;

            if (ManagerDeProiect.GetRolUser_DirectorGeneral() == 1)
                m_gRegistrulNumereInterne.SelectionChanged += OnGridSelectionChanged;
        }

        private void FrmCautareRegistruNumereInterne_Load(object sender, EventArgs e)
        {
            // diviziune
            int rolGlobal = ManagerDeProiect.GetRolUser_DirectorGeneral();
            string sqlDiv = $@"SELECT MBDV_Id, MBDV_Diviziune
                                 FROM MB_Diviziune
                                WHERE MBDV_Id = CASE WHEN {rolGlobal}>0 THEN MBDV_Id ELSE {ManagerDeProiect.GetDiviziune()} END";
            CustomCombo.SetConnection(ManagerDeProiect.GetConnection());
            m_cbDiviziune.SetSql(sqlDiv);
            m_cbDiviziune.SetComboById("MBDV_Id", ManagerDeProiect.GetDiviziune());

            // combo tipuri document
            IncarcaTipuriDocument();
        }

        private void IncarcaTipuriDocument()
        {
            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            var dt = q.LoadTableP(@"SELECT ADTD_Id, ADTD_Denumire
                                      FROM AD_TipuriDocumenteNumarIntern
                                     WHERE ADTD_Activ = 1
                                  ORDER BY ADTD_OrdineAfisare, ADTD_Denumire");
            // adăugăm rând „(toate)"
            var rowAll = dt.NewRow();
            rowAll["ADTD_Id"] = DBNull.Value;
            rowAll["ADTD_Denumire"] = "(toate)";
            dt.Rows.InsertAt(rowAll, 0);

            m_cbTipDocument.DisplayMember = "ADTD_Denumire";
            m_cbTipDocument.ValueMember = "ADTD_Id";
            m_cbTipDocument.DataSource = dt;
            m_cbTipDocument.SelectedIndex = 0;
        }

        // =========================================================
        //                       CAUTARE
        // =========================================================
        private void m_tsbCautare_Click(object sender, EventArgs e) => Cauta();

        private void Cauta()
        {
            var f = new System.Text.StringBuilder();
            var pars = new List<SqlParameter>();

            // Diviziune
            var v = m_cbDiviziune.GetColumnData("MBDV_Id");
            if (v != null && v != DBNull.Value)
            {
                f.Append(" AND ADRN_MBDV_Id = @div ");
                pars.Add(new SqlParameter("@div", Convert.ToInt32(v)));
            }

            // Data
            if (m_ckActivareFiltruData.Checked)
            {
                f.Append(" AND ADRN_Data >= @d1 AND ADRN_Data < DATEADD(DAY,1,@d2) ");
                pars.Add(new SqlParameter("@d1", m_dtpDeLaData.Value.Date));
                pars.Add(new SqlParameter("@d2", m_dtpPanaLaData.Value.Date));
            }

            // Descriere / Observatii
            if (!string.IsNullOrWhiteSpace(m_ebDescriereInregistrare.Text))
            {
                f.Append(" AND ADRN_Descriere LIKE @desc ");
                pars.Add(new SqlParameter("@desc", m_ebDescriereInregistrare.Text + "%"));
            }
            if (!string.IsNullOrWhiteSpace(m_ebObservatii.Text))
            {
                f.Append(" AND ADRN_Observatii LIKE @obs ");
                pars.Add(new SqlParameter("@obs", m_ebObservatii.Text + "%"));
            }

            // Intrare/Iesire
            if (m_ckIntrare.Checked != m_ckIesire.Checked)
            {
                f.Append(" AND ADRN_Iesire = @ies ");
                pars.Add(new SqlParameter("@ies", m_ckIesire.Checked));
            }

            // === FILTRU TIP DOCUMENT (ÎNLOCUIEȘTE 17 CHECKBOX-URI) ===
            var tip = m_cbTipDocument.SelectedValue;
            if (tip != null && tip != DBNull.Value)
            {
                f.Append(" AND ADRN_ADTD_Id = @tip ");
                pars.Add(new SqlParameter("@tip", Convert.ToInt32(tip)));
            }

            // Arhiva
            bool da = m_ckArhivaDA.Checked, nu = m_ckArhivaNU.Checked;
            if (da ^ nu)
            {
                f.Append(da
                    ? " AND ADRN_Id IN (SELECT ADAH_ADRN_Id FROM AD_Arhiva WHERE ADAH_ADRN_Id IS NOT NULL) "
                    : " AND ADRN_Id NOT IN (SELECT ADAH_ADRN_Id FROM AD_Arhiva WHERE ADAH_ADRN_Id IS NOT NULL) ");
            }

            // Filtru roluri
            int rolGlobal = ManagerDeProiect.GetRolUser_DirectorGeneral();
            pars.Add(new SqlParameter("@rolGlobal", rolGlobal));
            pars.Add(new SqlParameter("@divUser", ManagerDeProiect.GetDiviziune()));

            m_sSql = $@"
SELECT ROW_NUMBER() OVER (ORDER BY ADRN_Numar DESC) AS Row,
       ADRN_Id,
       ADRN_Numar,
       ADRN_Data,
       CASE WHEN ADRN_Iesire=0 THEN 'Intrare' ELSE 'Iesire' END AS TipInreg,
       ADTD_Denumire,
       ADRN_Descriere,
       ADRN_Observatii,
       MBUT_Login,
       MBDV_Diviziune
  FROM AD_RegistruNumereInterne
  INNER JOIN AD_TipuriDocumenteNumarIntern ON ADRN_ADTD_Id = ADTD_Id
   LEFT JOIN MB_Utilizatori                ON ADRN_MBUT_Id = MBUT_Id
   LEFT JOIN MB_Diviziune                  ON ADRN_MBDV_Id = MBDV_Id
 WHERE ADRN_MBDV_Id = CASE WHEN @rolGlobal>0 THEN ADRN_MBDV_Id ELSE @divUser END
       {f}
 ORDER BY ADRN_Numar DESC;";

            m_gRegistrulNumereInterne.SetSql(m_sSql, pars.ToArray());

            // Header-e
            var g = m_gRegistrulNumereInterne;
            g.Columns[0].HeaderText = "Nr. crt"; g.Columns[0].Width = 50;
            g.Columns[1].HeaderText = "ADRN_Id"; g.Columns[1].Visible = false;
            g.Columns[2].HeaderText = "Nr. inreg."; g.Columns[2].Width = 70;
            g.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            g.Columns[3].HeaderText = "Data"; g.Columns[3].Width = 80;
            g.Columns[3].DefaultCellStyle.Format = "dd.MM.yyyy";
            g.Columns[4].HeaderText = "Intrare/Iesire"; g.Columns[4].Width = 80;
            g.Columns[5].HeaderText = "Tip document"; g.Columns[5].Width = 200;
            g.Columns[6].HeaderText = "Descriere"; g.Columns[6].Width = 300;
            g.Columns[7].HeaderText = "Observatii"; g.Columns[7].Width = 300;
            g.Columns[8].HeaderText = "Utilizator"; g.Columns[8].Width = 100;
            g.Columns[9].HeaderText = "Diviziune"; g.Columns[9].Width = 100;

            g.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn c in g.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        // =========================================================
        //                  ACTIUNI: Nou / Modifica / Sterge
        // =========================================================
        private void m_tsbNou_Click(object sender, EventArgs e)
        {
            if (!FrmGestiuneRegistruNumereInterne.PoateDeschide(ManagerDeProiect)) return;
            var frm = new FrmGestiuneRegistruNumereInterne(FrmParent, ManagerDeProiect, 0)
            { MdiParent = FrmParent };
            frm.Show();
        }

        private void m_gRegistrulNumereInterne_DoubleClick(object sender, EventArgs e) => Modifica();

        private void Modifica()
        {
            var v = m_gRegistrulNumereInterne.GetColumnData("ADRN_Id");
            if (v == null || v == DBNull.Value) return;
            int id = Convert.ToInt32(v);
            if (!FrmGestiuneRegistruNumereInterne.PoateDeschide(ManagerDeProiect)) return;
            var frm = new FrmGestiuneRegistruNumereInterne(FrmParent, ManagerDeProiect, id)
            { MdiParent = FrmParent };
            frm.Show();
        }

        private void OnGridSelectionChanged(object? sender, EventArgs e)
        {
            m_tsbStergere.Enabled = false;
            var v = m_gRegistrulNumereInterne.GetColumnData("ADRN_Numar");
            if (v == null || v == DBNull.Value) return;
            int numarSelectat = Convert.ToInt32(v);

            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            int ultimulNr = q.ExecuteToIntP("SELECT ADSN_UltimulNr FROM AD_SecventaNumarIntern WHERE ADSN_Id = 1");
            q.Close();

            m_tsbStergere.Enabled = (numarSelectat == ultimulNr);
        }

        private void m_tsbStergere_Click(object sender, EventArgs e)
        {
            var vId = m_gRegistrulNumereInterne.GetColumnData("ADRN_Id");
            var vNr = m_gRegistrulNumereInterne.GetColumnData("ADRN_Numar");
            if (vId == null || vId == DBNull.Value) return;
            int id = Convert.ToInt32(vId);
            int numar = Convert.ToInt32(vNr);

            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            int ultimulNr = q.ExecuteToIntP("SELECT ADSN_UltimulNr FROM AD_SecventaNumarIntern WHERE ADSN_Id = 1");

            if (numar != ultimulNr)
            {
                MessageBox.Show(
                    $"Se poate șterge doar ultima înregistrare din registru (nr. {ultimulNr}).",
                    "Operație nepermisă", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                q.Close();
                return;
            }

            if (MessageBox.Show("Confirmați ștergerea înregistrării?", "Atenție",
                                MessageBoxButtons.YesNo) == DialogResult.No)
            {
                q.Close();
                return;
            }

            // Dacă e act adițional (ADTD_Id=6) legat de un contract, ștergem și abonamentul
            int adtdId = 0, cucoId = 0, cuacId = 0;
            q.OpenP(@"SELECT ADRN_ADTD_Id, ISNULL(ADRN_CUCO_Id, 0)
                        FROM AD_RegistruNumereInterne WHERE ADRN_Id = @id",
                CInterogareSQL.P("@id", id));
            if (q.NextRow())
            {
                adtdId = q.GetIntValue(0);
                cucoId = q.GetIntValue(1);
            }

            if (adtdId == 6 && cucoId > 0)
            {
                // Găsește ultimul abonament al contractului
                q.OpenP(@"SELECT TOP 1 CUAC_Id, CUAC_DataInceput, CUAC_DataSfarsit
                            FROM CU_AbonamenteCursant
                           WHERE CUAC_CUCO_Id = @cuco
                           ORDER BY CUAC_DataInceput DESC, CUAC_Id DESC",
                    CInterogareSQL.P("@cuco", cucoId));
                DateTime dtStart = DateTime.MinValue, dtEnd = DateTime.MaxValue;
                if (q.NextRow())
                {
                    cuacId  = q.GetIntValue(0);
                    dtStart = q.GetDateValue(1) ?? DateTime.MinValue;
                    dtEnd   = q.GetDateValue(2) ?? DateTime.MaxValue;
                }

                if (cuacId > 0)
                {
                    int nrPrezente = q.ExecuteToIntP(
                        @"SELECT COUNT(*) FROM CU_PrezenteDetaliu
                          WHERE CUPD_CUCO_Id = @cuco AND CUPD_Prezent = 1
                            AND CUPD_DataIntrare >= @d1 AND CUPD_DataIntrare <= @d2",
                        CInterogareSQL.P("@cuco", cucoId),
                        CInterogareSQL.P("@d1", dtStart),
                        CInterogareSQL.P("@d2", dtEnd));

                    if (nrPrezente > 0)
                    {
                        MessageBox.Show(
                            $"Abonamentul asociat are {nrPrezente} prezență/prezențe înregistrate și nu poate fi șters.",
                            "Atenție", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        q.Close();
                        return;
                    }
                }
            }

            q.BeginTransaction(System.Data.IsolationLevel.Serializable);
            try
            {
                if (adtdId == 6 && cuacId > 0)
                    q.ExecuteNonQueryP("DELETE FROM CU_AbonamenteCursant WHERE CUAC_Id = @id",
                        CInterogareSQL.P("@id", cuacId));

                q.ExecuteNonQueryP("DELETE FROM AD_RegistruNumereInterne WHERE ADRN_Id = @id",
                    CInterogareSQL.P("@id", id));
                q.ExecuteNonQueryP(
                    "UPDATE AD_SecventaNumarIntern SET ADSN_UltimulNr = ADSN_UltimulNr - 1 WHERE ADSN_Id = 1");
                q.CommitTransaction();
            }
            catch (Exception ex)
            {
                q.RollbackTransaction();
                MessageBox.Show("Eroare la ștergere: " + ex.Message);
                return;
            }
            finally { q.Close(); }

            Cauta();
        }

        // =========================================================
        //                       PRINT WORD
        // =========================================================
        private void m_tsbPrint_Click(object sender, EventArgs e) => PrintRegistru();

        private void PrintRegistru()
        {
            if (m_gRegistrulNumereInterne.Rows.Count == 0)
            { MessageBox.Show("Nu există înregistrări de tipărit."); return; }

            const string template = @"d:\WORK\ONIXTEIA\ACTE\SABLOANE\SablonContract.dotx";
            if (!System.IO.File.Exists(template))
            { MessageBox.Show("Nu am găsit șablonul:\n" + template); return; }

            string outFile = System.IO.Path.Combine(
                System.IO.Path.GetTempPath(),
                $"RegistruNumereInterne_{DateTime.Now:yyyyMMdd_HHmmss}.docx");

            // Coloane vizibile: 0=Nr.crt, skip 1(ADRN_Id hidden), 2=Nr.înreg., 3=Data,
            //                   4=Intrare/Ieșire, 5=Tip document, 6=Descriere, 7=Observații
            int[]    colIdx  = { 0,        2,          3,       4,                5,              6,           7 };
            string[] headers = { "Nr. crt","Nr. înreg.","Data","Intrare/Ieșire","Tip document","Descriere","Semnătură" };
            // Lățimi DXA — total 10466 (A4 portret 11906 - margini 720×2)
            int[]    widths  = { 500, 700, 900, 1000, 1800, 4366, 1200 };

            try
            {
                System.IO.File.Copy(template, outFile, true);
                using var wordDoc = WordprocessingDocument.Open(outFile, true);
                wordDoc.ChangeDocumentType(WordprocessingDocumentType.Document);
                var mainPart = wordDoc.MainDocumentPart!;
                var body     = mainPart.Document.Body!;

                // Clonăm SectPr care conține referințele la headerul din șablon
                var sectPr = body.Elements<SectionProperties>().LastOrDefault()?.CloneNode(true) as SectionProperties
                             ?? new SectionProperties();

                // Portret A4
                var pgSz = sectPr.GetFirstChild<PageSize>() ?? new PageSize();
                sectPr.RemoveAllChildren<PageSize>();
                pgSz.Width  = 11906; pgSz.Height = 16838;
                pgSz.Orient = PageOrientationValues.Portrait;
                sectPr.InsertAt(pgSz, 0);

                var pgMar = sectPr.GetFirstChild<PageMargin>() ?? new PageMargin();
                sectPr.RemoveAllChildren<PageMargin>();
                pgMar.Top = 720; pgMar.Bottom = 720; pgMar.Left = 720; pgMar.Right = 720;
                sectPr.AppendChild(pgMar);

                body.RemoveAllChildren();

                // Titlu
                body.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification { Val = JustificationValues.Center },
                        new SpacingBetweenLines { Before = "0", After = "60" }),
                    new Run(new RunProperties(new Bold(), new FontSize { Val = "28" },
                                new RunFonts { Ascii = "Arial", HighAnsi = "Arial" }),
                        new Text("REGISTRU NUMERE INTERNE"))));

                body.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification { Val = JustificationValues.Center },
                        new SpacingBetweenLines { Before = "0", After = "160" }),
                    new Run(new RunProperties(new FontSize { Val = "20" },
                                new RunFonts { Ascii = "Arial", HighAnsi = "Arial" }),
                        new Text($"Generat: {DateTime.Today:dd.MM.yyyy}"))));

                // Tabel
                var tbl = new Table(new TableProperties(
                    new TableWidth { Width = "10466", Type = TableWidthUnitValues.Dxa },
                    new TableBorders(
                        new TopBorder    { Val = BorderValues.Single, Size = 6, Color = "000000" },
                        new BottomBorder { Val = BorderValues.Single, Size = 6, Color = "000000" },
                        new LeftBorder   { Val = BorderValues.Single, Size = 6, Color = "000000" },
                        new RightBorder  { Val = BorderValues.Single, Size = 6, Color = "000000" },
                        new InsideHorizontalBorder { Val = BorderValues.Single, Size = 4, Color = "000000" },
                        new InsideVerticalBorder   { Val = BorderValues.Single, Size = 4, Color = "000000" }
                    )
                ));

                // Rând header tabel
                var hRow = new TableRow();
                for (int i = 0; i < headers.Length; i++)
                {
                    hRow.AppendChild(new TableCell(
                        new TableCellProperties(
                            new TableCellWidth { Width = widths[i].ToString(), Type = TableWidthUnitValues.Dxa },
                            new Shading { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "D9D9D9" }
                        ),
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Center },
                                new SpacingBetweenLines { Before = "0", After = "0" }),
                            new Run(new RunProperties(new Bold(), new FontSize { Val = "18" },
                                        new RunFonts { Ascii = "Arial", HighAnsi = "Arial" }),
                                new Text(headers[i]))
                        )
                    ));
                }
                tbl.AppendChild(hRow);

                // Rânduri date
                foreach (DataGridViewRow row in m_gRegistrulNumereInterne.Rows)
                {
                    if (row.IsNewRow) continue;
                    var dRow = new TableRow();
                    for (int i = 0; i < colIdx.Length; i++)
                    {
                        // Ultima coloană = Semnătură — se lasă goală pentru semnat manual
                        string txt = "";
                        if (i < colIdx.Length - 1)
                        {
                            object? val = row.Cells[colIdx[i]].Value;
                            txt = val switch
                            {
                                DateTime dt => dt.ToString("dd.MM.yyyy"),
                                null        => "",
                                _           => val.ToString()!
                            };
                        }
                        dRow.AppendChild(new TableCell(
                            new TableCellProperties(
                                new TableCellWidth { Width = widths[i].ToString(), Type = TableWidthUnitValues.Dxa }
                            ),
                            new Paragraph(
                                new ParagraphProperties(new SpacingBetweenLines { Before = "0", After = "0" }),
                                new Run(new RunProperties(new FontSize { Val = "18" },
                                            new RunFonts { Ascii = "Arial", HighAnsi = "Arial" }),
                                    new Text(txt) { Space = SpaceProcessingModeValues.Preserve })
                            )
                        ));
                    }
                    tbl.AppendChild(dRow);
                }

                body.AppendChild(tbl);
                body.AppendChild(sectPr);
                mainPart.Document.Save();

                System.Diagnostics.Process.Start(
                    new System.Diagnostics.ProcessStartInfo(outFile) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la generare: " + ex.Message);
                try { if (System.IO.File.Exists(outFile)) System.IO.File.Delete(outFile); } catch { }
            }
        }

        // =========================================================
        //              AUTOCOMPLETE PARTENER (cu parametri)
        // =========================================================
        private void m_ebPartener_TextChanged(object sender, EventArgs e)
        {
            //m_lADPG = 0;
            //m_AM.MaximumSize = new System.Drawing.Size(500, 500);
            //m_AM.RemoveAll();
            //var columnWidth = new int[] { 50, 300, 200 };

            //var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            //q.OpenP(@"SELECT ADPG_Id, ADPG_Denumire + ' - ' + ISNULL(ADPG_Judet,''), ADPG_CUI
            //            FROM AD_ParteneriGenerali
            //           WHERE ADPG_Denumire LIKE @t OR ADPG_CUI LIKE @t
            //        ORDER BY ADPG_Denumire",
            //        CInterogareSQL.P("@t", m_ebPartener.Text + "%"));
            //while (q.NextRow())
            //{
            //    int id = q.GetIntValue(0);
            //    string den = q.GetStringValue(1) ?? "";
            //    string cui = q.GetStringValue(2) ?? "";
            //    m_AM.AddItem(new MulticolumnAutocompleteItem(
            //        new[] { id.ToString(), den, cui }, den + " - " + cui)
            //    { ColumnWidth = columnWidth, ImageIndex = 0 });
            //}
            //q.Close();
        }

        private void m_ebPartener_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter && m_AM.SelectedItemIndex >= 0)
            //{
            //    var item = m_AM.GetItemMulticolumnAutocompleteItem(m_AM.SelectedItemIndex);
            //    m_lADPG = item?.GetID() ?? 0;
            //}
            //if (e.KeyCode == Keys.Back) { m_ebPartener.Text = ""; m_lADPG = 0; }
        }

        // =========================================================
        //                  ARHIVA — selectie din grid
        // =========================================================
        private void m_tsbDeschideArhiva_Click(object sender, EventArgs e)
        {
            // permitem doar rolurilor relevante
            if (ManagerDeProiect.GetRolUser_Administrator()
              + ManagerDeProiect.GetRolUser_DirectorGeneral() == 0) return;

            var v = m_gRegistrulNumereInterne.GetColumnData("ADRN_Id");
            if (v == null || v == DBNull.Value) return;
            int id = Convert.ToInt32(v);

            Cursor.Current = Cursors.WaitCursor;
            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            q.OpenP("SELECT ADAH_Cale FROM AD_Arhiva WHERE ADAH_ADRN_Id = @id ORDER BY ADAH_Id DESC",
                CInterogareSQL.P("@id", id));
            var fisiere = new System.Collections.Generic.List<string>();
            while (q.NextRow())
            {
                string? s = q.GetStringValue(0);
                if (!string.IsNullOrEmpty(s)) fisiere.Add(s!);
            }
            q.Close();

            if (fisiere.Count == 0)
                MessageBox.Show("Nu există fișier arhivat pentru această înregistrare.");
            else
                foreach (string f in fisiere)
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(f) { UseShellExecute = true });
            Cursor.Current = Cursors.Default;
        }
    }
}
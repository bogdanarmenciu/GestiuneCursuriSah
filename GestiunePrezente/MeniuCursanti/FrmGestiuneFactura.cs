using GestiunePrezente.Common;
using System.Globalization;

namespace GestiunePrezente.MeniuCursanti
{
    public partial class FrmGestiuneFactura : Form
    {
        readonly CManagerDeProiect ManagerDeProiect;
        int m_lCUCO;
        readonly int m_lFVFA;  // 0 = înregistrare nouă

        // panou selecție contract (vizibil doar când cucoId iniţial = 0)
        GridPersonalizat.CustomGrid? m_gridSelectare;
        TextBox? m_ebFiltruSelectare;
        Label? m_lblContractSelectat;
        TextBox? m_tbBeneficiariSelectati;

        private sealed class AdrnItem
        {
            public int Id { get; init; }
            private string Display { get; init; } = "";
            public AdrnItem(int id, string display) { Id = id; Display = display; }
            public override string ToString() => Display;
        }

        public const string MBME_NUME = "Gestiune Factura";
        public static bool PoateDeschide(CManagerDeProiect mm)
            => DrepturiFormular.PoateDeschide(mm, MBME_NUME);

        public FrmGestiuneFactura(CManagerDeProiect mm, int cucoId, int fvfaId = 0)
        {
            ManagerDeProiect = mm;
            m_lCUCO = cucoId;
            m_lFVFA = fvfaId;
            InitializeComponent();

            if (cucoId == 0)
                AdaugaPanouSelectareContract();

            CreeazaControlBeneficiari();
            IncarcaDate();
        }

        private void IncarcaSponsorizari()
        {
            m_cbSponsorizareADRN.Items.Clear();
            if (m_lCUCO <= 0) return;
            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            q.OpenP(@"SELECT ADRN_Id,
                             CAST(ADRN_Numar AS varchar(20)) + ' / ' +
                             CONVERT(varchar(10), ADRN_Data, 104) +
                             CASE WHEN ISNULL(ADRN_Descriere,'') <> ''
                                  THEN ' - ' + ADRN_Descriere ELSE '' END
                        FROM AD_RegistruNumereInterne
                       WHERE ADRN_CUCO_Id = @cuco
                         AND ADRN_ValoareSponsorizare IS NOT NULL
                       ORDER BY ADRN_Data DESC, ADRN_Numar DESC",
                CInterogareSQL.P("@cuco", m_lCUCO));
            while (q.NextRow())
                m_cbSponsorizareADRN.Items.Add(new AdrnItem(q.GetIntValue(0), q.GetStringValue(1) ?? ""));
            q.Close();
        }

        private void IncarcaDate()
        {
            m_ebSumaIncasare.Text = "";
            m_cbTipIncasare.SelectedIndex = 0;
            IncarcaSponsorizari();
            Text = m_lFVFA == 0 ? "Adaugă factură" : "Modifică factură";

            if (m_lFVFA > 0)
            {
                var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
                q.OpenP(@"SELECT FVFA_NrFactura, FVFA_DataEmitere, FVFA_PerioadaInceput,
                                 FVFA_PerioadaSfarsit, FVFA_Suma,
                                 CAST(ISNULL(FVFA_TipIncasare, 0) AS int) AS FVFA_TipIncasare,
                                 FVFA_NrDocument, FVFA_DataIncasare, FVFA_ADRN_Id
                            FROM FV_Facturi WHERE FVFA_Id = @id",
                    CInterogareSQL.P("@id", m_lFVFA));
                if (q.NextRow())
                {
                    m_ebNrFactura.Text = q.GetStringValue(0) ?? "";

                    DateTime? de = q.GetDateValue(1);
                    if (de.HasValue) m_dtDataEmitere.Value = de.Value;

                    DateTime? pi = q.GetDateValue(2);
                    if (pi.HasValue) m_dtPerioadaInc.Value = pi.Value;

                    DateTime? ps = q.GetDateValue(3);
                    if (ps.HasValue) m_dtPerioadaSf.Value = ps.Value;

                    decimal? suma = q.GetDecimalValue(4);
                    if (suma.HasValue) m_ebSuma.Text = suma.Value.ToString("F2");

                    int tip = q.GetIntValue(5);
                    // tip 3 în DB = contract sponsorizare; dacă ADRN_Id e setat, forțăm tip 3
                    int adrnId = q.GetIntValue(8);
                    if (adrnId > 0) tip = 3;
                    if (tip < m_cbTipIncasare.Items.Count)
                        m_cbTipIncasare.SelectedIndex = tip; // declanșează ActualizeazaVizibilitateIncasare

                    m_ebNrDocument.Text = q.GetStringValue(6) ?? "";

                    DateTime? di = q.GetDateValue(7);
                    if (di.HasValue) m_dtDataIncasare.Value = di.Value;

                    if (tip == 3 && adrnId > 0)
                    {
                        foreach (AdrnItem item in m_cbSponsorizareADRN.Items)
                        {
                            if (item.Id == adrnId)
                            {
                                m_cbSponsorizareADRN.SelectedItem = item;
                                break;
                            }
                        }
                        ActualizeazaBalantaSponsorizare();
                    }
                }
                q.Close();
            }
            else
            {
                var azi = DateTime.Today;
                m_dtPerioadaInc.Value = new DateTime(azi.Year, azi.Month, 1);
                m_dtPerioadaSf.Value  = new DateTime(azi.Year, azi.Month, DateTime.DaysInMonth(azi.Year, azi.Month));

                var qMax = new CInterogareSQL(ManagerDeProiect.GetConnection());
                int nextNr = qMax.ExecuteToInt(
                    "SELECT ISNULL(MAX(TRY_CAST(FVFA_NrFactura AS int)), 0) + 1 FROM FV_Facturi WHERE FVFA_NrFactura IS NOT NULL");
                qMax.Close();
                if (nextNr > 0)
                    m_ebNrFactura.Text = nextNr.ToString();
            }

            ActualizeazaBeneficiari();
        }

        private void m_cbTipIncasare_SelectedIndexChanged(object sender, EventArgs e)
            => ActualizeazaVizibilitateIncasare();

        private void ActualizeazaVizibilitateIncasare()
        {
            int idx = m_cbTipIncasare.SelectedIndex;
            bool withDoc  = idx == 1 || idx == 2;
            bool isSp     = idx == 3;
            bool withPlata = withDoc || isSp;

            m_lblSumaIncasare.Visible       = withPlata;
            m_ebSumaIncasare.Visible        = withPlata;
            m_labelNrDocument.Visible       = withDoc;
            m_ebNrDocument.Visible          = withDoc;
            m_labelDataIncasare.Visible     = withDoc;
            m_dtDataIncasare.Visible        = withDoc;

            m_labelSponsorizareADRN.Visible = isSp;
            m_cbSponsorizareADRN.Visible    = isSp;
            if (!isSp) m_lblBalantaSponsorizare.Visible = false;

            if (withPlata) ActualizeazaInfoIncasare();
            else           m_lblInfoIncasare.Visible = false;

            if (isSp) IncarcaSponsorizari();
        }

        private void ActualizeazaInfoIncasare()
        {
            int idx   = m_cbTipIncasare.SelectedIndex;
            bool isSp = idx == 3;

            decimal.TryParse(m_ebSuma.Text.Replace(',', '.'),
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out decimal sumaFactura);

            if (isSp)
            {
                // La sponsorizare: pre-completăm cu min(FVFA_Suma, disponibil_contract)
                // Balanta se actualizează din ActualizeazaBalantaSponsorizare — doar ștergem câmpul dacă e gol
                if (string.IsNullOrWhiteSpace(m_ebSumaIncasare.Text) && sumaFactura > 0)
                    m_ebSumaIncasare.Text = sumaFactura.ToString("F2");
                m_lblInfoIncasare.Visible = false;
                return;
            }

            // Cash / OP: afișăm total incasat + rest din factura curentă
            if (m_lFVFA > 0)
            {
                var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
                q.OpenP("SELECT ISNULL(SUM(CUIN_Valoare), 0) FROM CU_Incasari WHERE CUIN_FVFA_Id = @id",
                    CInterogareSQL.P("@id", m_lFVFA));
                decimal totalIncasat = q.NextRow() ? (q.GetDecimalValue(0) ?? 0) : 0;
                q.Close();

                decimal rest = sumaFactura - totalIncasat;
                m_lblInfoIncasare.Text = $"Total încasat: {totalIncasat:F2} lei  |  Rest de plată: {rest:F2} lei";
                m_lblInfoIncasare.ForeColor = rest <= 0 ? Color.DarkGreen : Color.Navy;
                m_lblInfoIncasare.Visible = true;

                if (string.IsNullOrWhiteSpace(m_ebSumaIncasare.Text))
                    m_ebSumaIncasare.Text = (rest > 0 ? rest : 0).ToString("F2");
            }
            else
            {
                m_lblInfoIncasare.Visible = false;
                if (string.IsNullOrWhiteSpace(m_ebSumaIncasare.Text))
                    m_ebSumaIncasare.Text = m_ebSuma.Text;
            }
        }

        private void m_cbSponsorizareADRN_SelectedIndexChanged(object sender, EventArgs e)
            => ActualizeazaBalantaSponsorizare();

        private void ActualizeazaBalantaSponsorizare()
        {
            if (m_cbSponsorizareADRN.SelectedItem is not AdrnItem sel)
            {
                m_lblBalantaSponsorizare.Visible = false;
                return;
            }
            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            q.OpenP(@"SELECT ISNULL(A.ADRN_ValoareSponsorizare, 0),
                             ISNULL((SELECT SUM(F2.FVFA_Suma)
                                       FROM FV_Facturi F2
                                      WHERE F2.FVFA_ADRN_Id = @id
                                        AND F2.FVFA_Id <> @fvfa), 0)
                        FROM AD_RegistruNumereInterne A
                       WHERE A.ADRN_Id = @id",
                CInterogareSQL.P("@id",   sel.Id),
                CInterogareSQL.P("@fvfa", m_lFVFA));
            if (q.NextRow())
            {
                decimal valoare    = q.GetDecimalValue(0) ?? 0;
                decimal consumat   = q.GetDecimalValue(1) ?? 0;
                decimal disponibil = valoare - consumat;
                if (valoare > 0)
                {
                    decimal.TryParse(m_ebSuma.Text.Replace(',', '.'),
                        System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture, out decimal sumaFactura);

                    string text = $"Valoare: {valoare:F2} lei  |  Consumat: {consumat:F2} lei  |  Disponibil: {disponibil:F2} lei";
                    decimal acoperiSp = Math.Min(sumaFactura > 0 ? sumaFactura : 0, Math.Max(disponibil, 0));
                    if (sumaFactura > 0 && disponibil < sumaFactura)
                    {
                        decimal restParinte = sumaFactura - acoperiSp;
                        text += $"\n⚠ Acoperit din sponsorizare: {acoperiSp:F2} lei  |  Rest de achitat de părinte: {restParinte:F2} lei";
                    }
                    m_lblBalantaSponsorizare.Text = text;
                    m_lblBalantaSponsorizare.ForeColor = disponibil >= sumaFactura ? Color.Navy : Color.DarkRed;
                    m_lblBalantaSponsorizare.Visible = true;

                    // Pre-completăm suma de încasat cu ce acoperă contractul
                    if (m_ebSumaIncasare.Visible)
                        m_ebSumaIncasare.Text = acoperiSp.ToString("F2");
                }
                else
                    m_lblBalantaSponsorizare.Visible = false;
            }
            q.Close();
        }

        private void CreeazaControlBeneficiari()
        {
            // Textbox verde cu beneficiarii — apare la dreapta câmpului NrFactura
            m_tbBeneficiariSelectati = new TextBox
            {
                Location    = new Point(m_ebNrFactura.Right + 10, m_ebNrFactura.Top),
                Size        = new Size(ClientSize.Width - m_ebNrFactura.Right - 20, 27),
                Anchor      = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
                ReadOnly    = true,
                BackColor   = Color.FromArgb(220, 245, 220),
                ForeColor   = Color.DarkGreen,
                Font        = new Font(Font.FontFamily, Font.Size, FontStyle.Bold),
                TabStop     = false,
                Text        = m_lCUCO == 0 ? "— selectați un contract din lista de mai sus —" : "",
                BorderStyle = BorderStyle.FixedSingle
            };
            Controls.Add(m_tbBeneficiariSelectati);
        }

        private void ActualizeazaBeneficiari()
        {
            if (m_lCUCO <= 0) return;
            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            q.OpenP(@"
SELECT C.CUCO_NrContract,
       ISNULL(STUFF((
           SELECT ', ' + CU2.CUCU_Prenume + ' ' + CU2.CUCU_Nume
             FROM CU_ContracteCursanti CC2
            INNER JOIN CU_Cursanti CU2 ON CC2.CUCC_CUCU_Id = CU2.CUCU_Id
            WHERE CC2.CUCC_CUCO_Id = @cuco
              AND ISNULL(CC2.CUCC_Retras, 0) = 0
              FOR XML PATH(''), TYPE
       ).value('.', 'NVARCHAR(MAX)'), 1, 2, ''), '') AS Beneficiari,
       ISNULL(
           (SELECT TOP 1 CUAC_Pret FROM CU_AbonamenteCursant
             WHERE CUAC_CUCO_Id = @cuco
             ORDER BY CASE WHEN CUAC_DataInceput <= GETDATE() AND CUAC_DataSfarsit >= GETDATE() THEN 0 ELSE 1 END,
                      CUAC_DataInceput DESC),
           C.CUCO_Pret
       ) AS Pret,
       ISNULL((SELECT COUNT(*) FROM CU_ContracteCursanti
                WHERE CUCC_CUCO_Id = @cuco AND ISNULL(CUCC_Retras, 0) = 0), 1) AS NrPersoane
  FROM CU_Contracte C WHERE C.CUCO_Id = @cuco",
                CInterogareSQL.P("@cuco", m_lCUCO));
            if (q.NextRow())
            {
                if (m_tbBeneficiariSelectati != null)
                    m_tbBeneficiariSelectati.Text = $"Contract {q.GetIntValue(0)}  —  {q.GetStringValue(1)}";

                if (m_lFVFA == 0)
                {
                    decimal? pret = q.GetDecimalValue(2);
                    int nrPers    = q.GetIntValue(3);
                    if (nrPers < 1) nrPers = 1;
                    if (pret.HasValue && pret.Value > 0)
                    {
                        // 1 persoana = 100%, 2+ = 50% fiecare suplimentar
                        decimal total = pret.Value * (nrPers + 1) / 2m;
                        m_ebSuma.Text = total.ToString("F2");
                    }
                }
            }
            q.Close();
        }

        private void AdaugaPanouSelectareContract()
        {
            const int panelH = 230;

            foreach (Control c in Controls)
                c.Top += panelH;
            ClientSize = new Size(1200, ClientSize.Height + panelH);
            FormBorderStyle = FormBorderStyle.Sizable;
            MinimumSize = new Size(800, ClientSize.Height);

            GridPersonalizat.CustomGrid.SetConnection(ManagerDeProiect.GetConnection());

            var panel = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(ClientSize.Width, panelH),
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top
            };

            var lblFiltru = new Label { Text = "Caută contract:", Location = new Point(8, 8), AutoSize = true };
            m_ebFiltruSelectare = new TextBox { Location = new Point(120, 5), Size = new Size(240, 27) };
            m_ebFiltruSelectare.TextChanged += (s, e) => CautaContracteSelectare();

            m_lblContractSelectat = new Label
            {
                Text = "Selectați un contract din listă.",
                Location = new Point(8, 42), AutoSize = true,
                ForeColor = Color.Gray
            };

            m_gridSelectare = new GridPersonalizat.CustomGrid
            {
                Location = new Point(0, 68),
                Size = new Size(panel.Width, panelH - 72),
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Font = new Font("Arial", 8F),
                RowHeadersWidth = 25
            };
            m_gridSelectare.SelectionChanged += (s, e) =>
            {
                object? val = m_gridSelectare.GetColumnData("CUCO_Id");
                if (val == null || val == DBNull.Value) return;
                m_lCUCO = Convert.ToInt32(val);
                IncarcaSponsorizari();
                object? nr  = m_gridSelectare.GetColumnData("Nr. Contract");
                object? ben = m_gridSelectare.GetColumnData("Beneficiari");
                m_lblContractSelectat!.Text = $"Contract selectat: {nr} — {ben}";
                m_lblContractSelectat.ForeColor = Color.DarkGreen;
                ActualizeazaBeneficiari();
            };

            var sep = new Label
            {
                Location = new Point(0, panelH - 4), Size = new Size(2000, 2),
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
                BorderStyle = BorderStyle.Fixed3D
            };

            panel.Controls.Add(lblFiltru);
            panel.Controls.Add(m_ebFiltruSelectare);
            panel.Controls.Add(m_lblContractSelectat);
            panel.Controls.Add(m_gridSelectare);
            panel.Controls.Add(sep);
            Controls.Add(panel);
            CautaContracteSelectare();
        }

        private void CautaContracteSelectare()
        {
            if (m_gridSelectare == null) return;

            string filtru = "";
            string text = m_ebFiltruSelectare?.Text.Trim() ?? "";
            if (text != "")
            {
                string safe = CFormatareDate.StringToSqlString(text);
                filtru = $@" AND (
                    CAST(C.CUCO_NrContract AS varchar(20)) LIKE N'{safe}%'
                    OR EXISTS (
                        SELECT 1 FROM CU_ContracteCursanti CC2
                        INNER JOIN CU_Cursanti CU2 ON CC2.CUCC_CUCU_Id = CU2.CUCU_Id
                        WHERE CC2.CUCC_CUCO_Id = C.CUCO_Id
                          AND ISNULL(CC2.CUCC_Retras, 0) = 0
                          AND (CU2.CUCU_Nume LIKE N'{safe}%' OR CU2.CUCU_Prenume LIKE N'{safe}%')
                    ))";
            }

            string sql = $@"
SELECT ROW_NUMBER() OVER (ORDER BY C.CUCO_NrContract) AS [Nr. crt],
       C.CUCO_Id,
       C.CUCO_NrContract                                              AS [Nr. Contract],
       ISNULL(Beneficiari.NumeComplet, '')                            AS [Beneficiari],
       C.CUCO_DataInceputContract                                     AS [Data început contract],
       AbCurent.CUAC_DataInceput                                      AS [Data început abonament],
       AbCurent.CUAC_DataSfarsit                                      AS [Data sfârșit abonament],
       MBUT.MBUT_Login                                                AS [User],
       ISNULL(AbCurent.CUAB_Descriere, '')                           AS [Abonament],
       ISNULL(
           (SELECT TOP 1 CUAC_Pret FROM CU_AbonamenteCursant
             WHERE CUAC_CUCO_Id = C.CUCO_Id
             ORDER BY CASE WHEN CUAC_DataInceput <= GETDATE() AND CUAC_DataSfarsit >= GETDATE() THEN 0 ELSE 1 END,
                      CUAC_DataInceput DESC),
           C.CUCO_Pret
       ) * (ISNULL((SELECT COUNT(*) FROM CU_ContracteCursanti
                     WHERE CUCC_CUCO_Id = C.CUCO_Id
                       AND ISNULL(CUCC_Retras, 0) = 0), 1) + 1) / 2.0 AS [Pret],
       C.CUCO_Listat                                                  AS [Contract Listat],
       C.CUCO_Retras                                                  AS [Contract Reziliat],
       CASE WHEN Restante.NrRestante = 0 THEN 'La zi'
            ELSE 'Restant: ' + CAST(Restante.NrRestante AS varchar(5))
       END                                                             AS [Situatie]
  FROM CU_Contracte C
 INNER JOIN MB_Utilizatori MBUT ON C.CUCO_MBUT_Id = MBUT.MBUT_Id
 OUTER APPLY (
     SELECT STUFF((
         SELECT ', ' + CU2.CUCU_Nume + ' ' + CU2.CUCU_Prenume
           FROM CU_ContracteCursanti CC2
          INNER JOIN CU_Cursanti CU2 ON CC2.CUCC_CUCU_Id = CU2.CUCU_Id
          WHERE CC2.CUCC_CUCO_Id = C.CUCO_Id
            AND ISNULL(CC2.CUCC_Retras, 0) = 0
          ORDER BY CU2.CUCU_Nume, CU2.CUCU_Prenume
            FOR XML PATH(''), TYPE
     ).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS NumeComplet
 ) AS Beneficiari
 OUTER APPLY (
     SELECT TOP 1 CUAB.CUAB_Descriere, CUAC.CUAC_Pret,
                  CUAC.CUAC_DataInceput, CUAC.CUAC_DataSfarsit
       FROM CU_AbonamenteCursant CUAC
      INNER JOIN CU_Abonamente CUAB ON CUAC.CUAC_CUAB_Id = CUAB.CUAB_Id
      WHERE CUAC.CUAC_CUCO_Id = C.CUCO_Id
      ORDER BY CASE WHEN CUAC.CUAC_DataInceput <= GETDATE() AND CUAC.CUAC_DataSfarsit >= GETDATE() THEN 0 ELSE 1 END,
               CUAC.CUAC_DataInceput DESC
 ) AS AbCurent
 OUTER APPLY (
     SELECT COUNT(*) AS NrRestante
       FROM FV_Facturi F2
      WHERE F2.FVFA_CUCO_Id = C.CUCO_Id
        AND F2.FVFA_ADRN_Id IS NULL
        AND ISNULL(F2.FVFA_Suma, 0) > 0
        AND ISNULL((SELECT SUM(I.CUIN_Valoare) FROM CU_Incasari I
                     WHERE I.CUIN_FVFA_Id = F2.FVFA_Id), 0) < ISNULL(F2.FVFA_Suma, 0)
 ) AS Restante
WHERE C.CUCO_Retras = 0
{filtru}
ORDER BY C.CUCO_NrContract";

            m_gridSelectare.SuspendLayout();
            m_gridSelectare.Visible = false;
            m_gridSelectare.SetSql(sql);
            m_gridSelectare.FormatColumn("Nr. crt",                50,  true, DataGridViewContentAlignment.MiddleRight);
            m_gridSelectare.FormatColumn("CUCO_Id",                0,   false);
            m_gridSelectare.FormatColumn("Nr. Contract",           50,  true, DataGridViewContentAlignment.MiddleRight);
            m_gridSelectare.FormatColumn("Beneficiari",            300);
            m_gridSelectare.FormatColumn("Data început contract",  110, true, null, "dd.MM.yyyy");
            m_gridSelectare.FormatColumn("Data început abonament", 110, true, null, "dd.MM.yyyy");
            m_gridSelectare.FormatColumn("Data sfârșit abonament", 110, true, null, "dd.MM.yyyy");
            m_gridSelectare.FormatColumn("User",                   100);
            m_gridSelectare.FormatColumn("Abonament",              150);
            m_gridSelectare.FormatColumn("Pret",                   70,  true, DataGridViewContentAlignment.MiddleRight);
            m_gridSelectare.FormatColumn("Contract Listat",        70);
            m_gridSelectare.FormatColumn("Contract Reziliat",      70);
            m_gridSelectare.FormatColumn("Situatie",               110, true, DataGridViewContentAlignment.MiddleCenter);
            m_gridSelectare.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            m_gridSelectare.CellFormatting -= GridSelectare_CellFormatting;
            m_gridSelectare.CellFormatting += GridSelectare_CellFormatting;
            foreach (DataGridViewColumn c in m_gridSelectare.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
            m_gridSelectare.ResumeLayout();
            m_gridSelectare.Visible = true;
        }

        private void GridSelectare_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0 || m_gridSelectare == null) return;
            if (m_gridSelectare.Columns[e.ColumnIndex].HeaderText != "Situatie") return;
            string val = e.Value as string ?? "";
            (e.CellStyle.BackColor, e.CellStyle.ForeColor) = val.StartsWith("Restant")
                ? (Color.FromArgb(255, 180, 180), Color.DarkRed)
                : (Color.FromArgb(200, 240, 200), Color.DarkGreen);
        }

        private void m_btnOK_Click(object sender, EventArgs e)
        {
            if (m_gridSelectare != null && m_lCUCO == 0)
            {
                MessageBox.Show("Selectați un contract din lista de sus.", "Atenție",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool isSp = m_cbTipIncasare.SelectedIndex == 3;

            decimal? suma = null;
            if (!string.IsNullOrWhiteSpace(m_ebSuma.Text))
            {
                if (!decimal.TryParse(m_ebSuma.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal parsedSuma))
                {
                    MessageBox.Show("Suma introdusă nu este validă.", "Atenție", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                suma = parsedSuma;
            }

            int tipIncasare = m_cbTipIncasare.SelectedIndex;
            string? nrDocument = !isSp && m_cbTipIncasare.SelectedIndex > 0 && !string.IsNullOrWhiteSpace(m_ebNrDocument.Text)
                ? m_ebNrDocument.Text.Trim() : null;
            DateTime? dataIncasare = !isSp && m_cbTipIncasare.SelectedIndex > 0
                ? m_dtDataIncasare.Value.Date : (DateTime?)null;
            int adrnId = isSp && m_cbSponsorizareADRN.SelectedItem is AdrnItem sel ? sel.Id : 0;

            if (isSp && adrnId == 0)
            {
                MessageBox.Show("Selectați contractul de sponsorizare.", "Atenție",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Warning non-blocant dacă suma depășește disponibilul
            if (isSp && suma.HasValue && adrnId > 0)
            {
                var qW = new CInterogareSQL(ManagerDeProiect.GetConnection());
                qW.OpenP(@"SELECT ISNULL(A.ADRN_ValoareSponsorizare, 0),
                                  ISNULL((SELECT SUM(F2.FVFA_Suma) FROM FV_Facturi F2
                                           WHERE F2.FVFA_ADRN_Id = @id AND F2.FVFA_Id <> @fvfa), 0)
                             FROM AD_RegistruNumereInterne A WHERE A.ADRN_Id = @id",
                    CInterogareSQL.P("@id",   adrnId),
                    CInterogareSQL.P("@fvfa", m_lFVFA));
                if (qW.NextRow())
                {
                    decimal val  = qW.GetDecimalValue(0) ?? 0;
                    decimal con  = qW.GetDecimalValue(1) ?? 0;
                    decimal disp = val - con;
                    if (val > 0 && suma.Value > disp)
                    {
                        if (MessageBox.Show(
                            $"Suma {suma.Value:F2} lei depășește disponibilul din contractul de sponsorizare ({disp:F2} lei).\nContinuați?",
                            "Atenție", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                        {
                            qW.Close();
                            return;
                        }
                    }
                }
                qW.Close();
            }

            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            int fvfaIdSalvat;
            if (m_lFVFA == 0)
            {
                fvfaIdSalvat = q.ExecuteToIntPThrow(@"
INSERT INTO FV_Facturi
    (FVFA_CUCO_Id, FVFA_NrFactura, FVFA_DataEmitere,
     FVFA_PerioadaInceput, FVFA_PerioadaSfarsit, FVFA_Suma,
     FVFA_TipIncasare, FVFA_NrDocument, FVFA_DataIncasare,
     FVFA_ADRN_Id, FVFA_MBUT_Id, FVFA_DataSalvarii)
OUTPUT INSERTED.FVFA_Id
VALUES (@cuco, @nrf, @de, @pi, @ps, @suma, @tip, @nrd, @di, @adrn, @u, GETDATE())",
                    CInterogareSQL.P("@cuco", m_lCUCO),
                    CInterogareSQL.P("@nrf",  !string.IsNullOrWhiteSpace(m_ebNrFactura.Text) ? (object)m_ebNrFactura.Text.Trim() : DBNull.Value),
                    CInterogareSQL.P("@de",   m_dtDataEmitere.Visible ? (object)m_dtDataEmitere.Value.Date : DBNull.Value),
                    CInterogareSQL.P("@pi",   m_dtPerioadaInc.Value.Date),
                    CInterogareSQL.P("@ps",   m_dtPerioadaSf.Value.Date),
                    CInterogareSQL.P("@suma", suma.HasValue  ? (object)suma.Value  : DBNull.Value),
                    CInterogareSQL.P("@tip",  (object)(byte)tipIncasare),
                    CInterogareSQL.P("@nrd",  nrDocument != null ? (object)nrDocument : DBNull.Value),
                    CInterogareSQL.P("@di",   dataIncasare.HasValue ? (object)dataIncasare.Value : DBNull.Value),
                    CInterogareSQL.P("@adrn", adrnId > 0 ? (object)adrnId : DBNull.Value),
                    CInterogareSQL.P("@u",    ManagerDeProiect.GetUserId()));
            }
            else
            {
                q.ExecuteNonQueryP(@"
UPDATE FV_Facturi SET
    FVFA_NrFactura = @nrf, FVFA_DataEmitere = @de,
    FVFA_PerioadaInceput = @pi, FVFA_PerioadaSfarsit = @ps,
    FVFA_Suma = @suma, FVFA_TipIncasare = @tip,
    FVFA_NrDocument = @nrd, FVFA_DataIncasare = @di,
    FVFA_ADRN_Id = @adrn, FVFA_MBUT_Id = @u, FVFA_DataSalvarii = GETDATE()
WHERE FVFA_Id = @id",
                    CInterogareSQL.P("@nrf",  !string.IsNullOrWhiteSpace(m_ebNrFactura.Text) ? (object)m_ebNrFactura.Text.Trim() : DBNull.Value),
                    CInterogareSQL.P("@de",   m_dtDataEmitere.Visible ? (object)m_dtDataEmitere.Value.Date : DBNull.Value),
                    CInterogareSQL.P("@pi",   m_dtPerioadaInc.Value.Date),
                    CInterogareSQL.P("@ps",   m_dtPerioadaSf.Value.Date),
                    CInterogareSQL.P("@suma", suma.HasValue  ? (object)suma.Value  : DBNull.Value),
                    CInterogareSQL.P("@tip",  (object)(byte)tipIncasare),
                    CInterogareSQL.P("@nrd",  nrDocument != null ? (object)nrDocument : DBNull.Value),
                    CInterogareSQL.P("@di",   dataIncasare.HasValue ? (object)dataIncasare.Value : DBNull.Value),
                    CInterogareSQL.P("@adrn", adrnId > 0 ? (object)adrnId : DBNull.Value),
                    CInterogareSQL.P("@u",    ManagerDeProiect.GetUserId()),
                    CInterogareSQL.P("@id",   m_lFVFA));
                fvfaIdSalvat = m_lFVFA;
            }
            q.Close();

            // La OP/Cash: inserăm în CU_Incasari suma introdusă de utilizator (poate fi plată parțială)
            if ((tipIncasare == 1 || tipIncasare == 2) && fvfaIdSalvat > 0)
            {
                decimal sumaIncasare = 0;
                if (!string.IsNullOrWhiteSpace(m_ebSumaIncasare.Text))
                    decimal.TryParse(m_ebSumaIncasare.Text.Replace(',', '.'),
                        System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture, out sumaIncasare);

                if (sumaIncasare > 0)
                {
                    var qNr = new CInterogareSQL(ManagerDeProiect.GetConnection());
                    int nextNr = qNr.ExecuteToInt(
                        $"SELECT ISNULL(MAX(CUIN_Numar), 0) + 1 FROM CU_Incasari WHERE CUIN_MBDV_Id = {ManagerDeProiect.GetDiviziune()}");
                    qNr.Close();
                    string nrFact = m_ebNrFactura.Text.Trim();
                    var qI = new CInterogareSQL(ManagerDeProiect.GetConnection());
                    qI.ExecuteNonQueryP(@"
INSERT INTO CU_Incasari
    (CUIN_MBSO_Id, CUIN_MBDV_Id, CUIN_Numar, CUIN_Data, CUIN_TipPlata,
     CUIN_NrDocument, CUIN_Valoare, CUIN_Observatii,
     CUIN_DataSalvarii, CUIN_MBUT_Id, CUIN_FVFA_Id)
VALUES (@so, @dv, @nr, @data, @tip, @nrd, @suma, @rep, GETDATE(), @u, @fvfa)",
                        CInterogareSQL.P("@so",   ManagerDeProiect.GetSocietate()),
                        CInterogareSQL.P("@dv",   ManagerDeProiect.GetDiviziune()),
                        CInterogareSQL.P("@nr",   nextNr),
                        CInterogareSQL.P("@data", dataIncasare ?? DateTime.Today),
                        CInterogareSQL.P("@tip",  (byte)tipIncasare),
                        CInterogareSQL.P("@nrd",  nrDocument != null ? (object)nrDocument : DBNull.Value),
                        CInterogareSQL.P("@suma", sumaIncasare),
                        CInterogareSQL.P("@rep",  $"Contravaloare factură nr. {nrFact}"),
                        CInterogareSQL.P("@u",    ManagerDeProiect.GetUserId()),
                        CInterogareSQL.P("@fvfa", fvfaIdSalvat));
                    qI.Close();
                }
            }

            // La Sponsorizare: inserăm o singură dată în CU_Incasari (CUIN_TipPlata=3)
            // Suma = ce a introdus utilizatorul în m_ebSumaIncasare (propus = min(FVFA_Suma, disponibil))
            if (tipIncasare == 3 && adrnId > 0 && fvfaIdSalvat > 0)
            {
                decimal sumaSpons = 0;
                if (!string.IsNullOrWhiteSpace(m_ebSumaIncasare.Text))
                    decimal.TryParse(m_ebSumaIncasare.Text.Replace(',', '.'),
                        System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture, out sumaSpons);

                var qCk = new CInterogareSQL(ManagerDeProiect.GetConnection());
                int existaCuin = qCk.ExecuteToInt(
                    $"SELECT COUNT(*) FROM CU_Incasari WHERE CUIN_FVFA_Id = {fvfaIdSalvat} AND CUIN_TipPlata = 3");
                qCk.Close();

                if (existaCuin == 0 && sumaSpons > 0)
                {
                    var qNr = new CInterogareSQL(ManagerDeProiect.GetConnection());
                    int nextNr = qNr.ExecuteToInt(
                        $"SELECT ISNULL(MAX(CUIN_Numar), 0) + 1 FROM CU_Incasari WHERE CUIN_MBDV_Id = {ManagerDeProiect.GetDiviziune()}");
                    qNr.Close();
                    string nrFact = m_ebNrFactura.Text.Trim();
                    var qI = new CInterogareSQL(ManagerDeProiect.GetConnection());
                    qI.ExecuteNonQueryP(@"
INSERT INTO CU_Incasari
    (CUIN_MBSO_Id, CUIN_MBDV_Id, CUIN_Numar, CUIN_Data, CUIN_TipPlata,
     CUIN_Valoare, CUIN_Observatii,
     CUIN_DataSalvarii, CUIN_MBUT_Id, CUIN_FVFA_Id, CUIN_ADRN_Id)
VALUES (@so, @dv, @nr, GETDATE(), 3, @suma, @rep, GETDATE(), @u, @fvfa, @adrn)",
                        CInterogareSQL.P("@so",   ManagerDeProiect.GetSocietate()),
                        CInterogareSQL.P("@dv",   ManagerDeProiect.GetDiviziune()),
                        CInterogareSQL.P("@nr",   nextNr),
                        CInterogareSQL.P("@suma", sumaSpons),
                        CInterogareSQL.P("@rep",  $"Sponsorizare factură nr. {nrFact}"),
                        CInterogareSQL.P("@u",    ManagerDeProiect.GetUserId()),
                        CInterogareSQL.P("@fvfa", fvfaIdSalvat),
                        CInterogareSQL.P("@adrn", adrnId));
                    qI.Close();
                }
            }

            DialogResult = DialogResult.OK;
        }

        private void m_btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}

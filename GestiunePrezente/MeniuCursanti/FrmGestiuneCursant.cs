using GestiunePrezente.Common;
using GestiunePrezente.MeniuPrincipal;
using Microsoft.Data.SqlClient;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace GestiunePrezente.MeniuCursanti
{
    public partial class FrmGestiuneCursant : Form
    {
        readonly CManagerDeProiect ManagerDeProiect;
        readonly FrmMeniuPrincipal FrmParent;
        int m_lCUCU = 0;
        readonly int m_lMBME = 0;
        string? sSql, m_sSql;

        // ID cursant după salvare — folosit de FrmGestiuneSolicitare
        public int IdCursant => m_lCUCU;

        string? m_sPreNume, m_sPrePrenume;

        public const string MBME_NUME = "Gestiune Cursant";
        public static bool PoateDeschide(CManagerDeProiect mm)
            => DrepturiFormular.PoateDeschide(mm, MBME_NUME);

        public FrmGestiuneCursant(FrmMeniuPrincipal FormParent, CManagerDeProiect MM, int lCUCU, int lMBME = 3,
            string? prenume = null, string? nume = null)
        {
            FrmParent = FormParent;
            ManagerDeProiect = MM;
            m_lCUCU = lCUCU;
            m_lMBME = lMBME;
            m_sPrePrenume = prenume;
            m_sPreNume    = nume;
            InitializeComponent();

            m_dtDataNasterii.CustomFormat = "dd.MM.yyyy";
        }

        private void m_ebNume_TextChanged(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = true;
            m_tslNume.Text = $"{m_ebNume.Text} {m_ebPrenume.Text}".Trim();
        }

        private void m_ebPrenume_TextChanged(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = true;
            m_tslNume.Text = $"{m_ebNume.Text} {m_ebPrenume.Text}".Trim();
        }

        private void m_ebCNP_TextChanged(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = true;
        }

        private void m_cbCetAteanStrain_CheckedChanged(object sender, EventArgs e)
        {
            bool strain = m_cbCetAteanStrain.Checked;
            m_ebCNP.Enabled = !strain;
            if (strain) m_ebCNP.Text = "";
            m_lblNrDocument.Visible = strain;
            m_ebNrDocumentStrain.Visible = strain;
            m_stbSalveaza.Enabled = true;
        }

        private void m_ebNrDocumentStrain_TextChanged(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = true;
        }

        private void m_cbTipDocument_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = true;
        }

        private void m_dtDataNasterii_ValueChanged(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = true;
        }

        private void m_ebLocalitateaNasterii_TextChanged(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = true;
        }

        private void m_ebJudetulNasterii_TextChanged(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = true;
        }

        private void m_ebCISerie_TextChanged(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = true;
        }

        private void m_ebCINumar_TextChanged(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = true;
        }

        private void m_ebStrada_TextChanged(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = true;
        }

        private void m_ebNumarStrada_TextChanged(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = true;
        }

        private void m_ebBloc_TextChanged(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = true;
        }

        private void m_ebScara_TextChanged(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = true;
        }

        private void m_ebAp_TextChanged(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = true;
        }

        private void m_ebOras_TextChanged(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = true;
        }

        private void m_ebJudet_TextChanged(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = true;
        }

        private void m_ebTelMobil_TextChanged(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = true;
        }

        private void m_ebTelFix_TextChanged(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = true;
        }

        private void m_ebEmail_TextChanged(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = true;
        }

        private void m_ebNumeFB_TextChanged(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = true;
        }

        private void m_ebNumeSkype_TextChanged(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = true;
        }

        private void m_ebFideId_TextChanged(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = true;
        }

        private void m_ebNumeTutore_TextChanged(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = true;
        }

        private void m_ebPrenumeTutore_TextChanged(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = true;
        }

        private void m_ebTelefonTutore_TextChanged(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = true;
        }

        private void m_ebEmailTutore_TextChanged(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = true;
        }

        private void m_ebCISerieTutore_TextChanged(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = true;
        }

        private void m_ebCNPTutore_TextChanged(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = true;
        }

        private void m_ebCINumarTutore_TextChanged(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = true;
        }

        private void m_stbSalveaza_Click(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = !Salveaza();
        }

        private void m_tsbListeazaBarcode_Click(object sender, EventArgs e)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            string sBarcode = m_ebBarcode.Text;
            string sNumeComplet = $"{m_ebNume.Text} {m_ebPrenume.Text}";
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Eticheta_Cursant.pdf");

            try
            {
                QuestPDF.Fluent.Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(1, Unit.Centimetre);
                        page.DefaultTextStyle(x => x.FontSize(10).FontFamily(Fonts.TimesNewRoman));

                        page.Content().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });
                            table.Cell().Padding(10).Border(0.5f).Column(col =>
                            {
                                col.Spacing(5);
                                col.Item().AlignCenter().Text($"{sNumeComplet}").SemiBold();
                                col.Item().Height(60).AlignCenter().BarcodeCode128(sBarcode);
                                col.Item().AlignCenter().Text(sBarcode).FontSize(8);
                            });

                        });
                    });
                }).GeneratePdf(filePath);
                Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare: {ex.Message}");
            }
        }

        private void m_ebLocalitateaNasterii_DoubleClick(object sender, EventArgs e)
        {
            FrmAjutorJudetLocalitate frm = new(FrmParent, ManagerDeProiect);

            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                m_ebJudetulNasterii.Text = frm.GetJudet();
                m_ebLocalitateaNasterii.Text = frm.GetLocalitate();
            }
        }

        private void m_ebJudetulNasterii_DoubleClick(object sender, EventArgs e)
        {
            FrmAjutorJudetLocalitate frm = new(FrmParent, ManagerDeProiect);

            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                m_ebJudetulNasterii.Text = frm.GetJudet();
                m_ebLocalitateaNasterii.Text = frm.GetLocalitate();
            }
        }

        private void m_ebOras_DoubleClick(object sender, EventArgs e)
        {
            FrmAjutorJudetLocalitate frm = new(FrmParent, ManagerDeProiect);

            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                m_ebJudet.Text = frm.GetJudet();
                m_ebOras.Text = frm.GetLocalitate();
            }
        }

        private void m_ebJudet_DoubleClick(object sender, EventArgs e)
        {
            FrmAjutorJudetLocalitate frm = new(FrmParent, ManagerDeProiect);

            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                m_ebJudet.Text = frm.GetJudet();
                m_ebOras.Text = frm.GetLocalitate();
            }
        }

        private EloGridView m_dgvELO = null!;
        private GridPersonalizat.CustomGrid m_gTurnee       = null!;
        private GridPersonalizat.CustomGrid m_gRundeCursant = null!;
        private GridPersonalizat.CustomGrid m_gELOTurnee    = null!;
        private GridPersonalizat.CustomGrid m_gPartide      = null!;

        private class EloGridView : DataGridView
        {
            protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
            {
                if (keyData == Keys.Enter && IsCurrentCellInEditMode)
                {
                    int col = CurrentCell?.ColumnIndex ?? -1;
                    int row = CurrentCell?.RowIndex    ?? -1;
                    if (col < 0 || row < 0) return base.ProcessCmdKey(ref msg, keyData);

                    int nextCol = col + 1;
                    if (nextCol < ColumnCount)
                    {
                        CurrentCell = this[nextCol, row];
                        BeginEdit(true);
                    }
                    else
                    {
                        int nextRow = row + 1;
                        if (nextRow > NewRowIndex) nextRow = NewRowIndex;
                        CurrentCell = this[0, nextRow];
                        BeginEdit(true);
                    }
                    return true;
                }
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void FrmGestiuneCursant_Load(object sender, EventArgs e)
        {
            InitTabELO();
            InitTabTurnee();
            InitTabPartide();
            Modifica();
            // Pre-completare din solicitare (doar cursant nou)
            if (m_lCUCU == 0)
            {
                if (!string.IsNullOrEmpty(m_sPreNume))    m_ebNume.Text    = m_sPreNume;
                if (!string.IsNullOrEmpty(m_sPrePrenume)) m_ebPrenume.Text = m_sPrePrenume;
                m_stbSalveaza.Enabled = false;
            }
        }

        private void InitTabELO()
        {
            var tab = new TabPage("Istoric ELO");

            var split = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Horizontal,
                SplitterDistance = 220
            };

            // --- sus: auto din turnee importate ---
            var lblAuto = new Label
            {
                Text = "Din turnee importate (automat):",
                Dock = DockStyle.Top, Height = 20,
                Font = new Font(Font, FontStyle.Bold)
            };
            m_gELOTurnee = new GridPersonalizat.CustomGrid
            {
                Dock = DockStyle.Fill, ReadOnly = true,
                RowHeadersWidth = 25,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Font = new Font("Arial", 8F)
            };
            split.Panel1.Controls.Add(m_gELOTurnee);
            split.Panel1.Controls.Add(lblAuto);

            // --- jos: intrare manuala ---
            var lblManual = new Label
            {
                Text = "Intrare manuală (alte turnee neimportate):",
                Dock = DockStyle.Top, Height = 20,
                Font = new Font(Font, FontStyle.Bold)
            };
            m_dgvELO = new EloGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows    = true,
                AllowUserToDeleteRows = true,
                AutoSizeColumnsMode   = DataGridViewAutoSizeColumnsMode.None,
                SelectionMode         = DataGridViewSelectionMode.FullRowSelect,
                EditMode              = DataGridViewEditMode.EditOnEnter,
                RowHeadersWidth       = 25
            };
            var styleRight = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight };
            m_dgvELO.Columns.AddRange(
                new DataGridViewTextBoxColumn { Name = "colData",   HeaderText = "Data (zz.ll.aaaa)", Width = 130 },
                new DataGridViewTextBoxColumn { Name = "colClasic", HeaderText = "Clasic", Width = 90, DefaultCellStyle = styleRight },
                new DataGridViewTextBoxColumn { Name = "colRapid",  HeaderText = "Rapid",  Width = 90, DefaultCellStyle = styleRight },
                new DataGridViewTextBoxColumn { Name = "colBlitz",  HeaderText = "Blitz",  Width = 90, DefaultCellStyle = styleRight }
            );
            m_dgvELO.RowValidated += (s, e) => SalveazaELO();
            split.Panel2.Controls.Add(m_dgvELO);
            split.Panel2.Controls.Add(lblManual);

            tab.Controls.Add(split);
            m_tabControl.TabPages.Add(tab);
        }

        private void InitTabTurnee()
        {
            var tab = new TabPage("Turnee");

            m_gTurnee = new GridPersonalizat.CustomGrid
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                RowHeadersWidth = 25,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Font = new Font("Arial", 8F)
            };
            m_gTurnee.DoubleClick += (s, e) =>
            {
                object? val = m_gTurnee.GetColumnData("CUTE_Id");
                if (val == null || val == DBNull.Value) return;
                int cuteId = Convert.ToInt32(val);
                if (!FrmGestiuneTurnee.PoateDeschide(ManagerDeProiect)) return;
                var frm = new FrmGestiuneTurnee(FrmParent, ManagerDeProiect, cuteId) { MdiParent = FrmParent };
                frm.FormClosed += (_, _) => IncarcaTurnee();
                frm.Show();
            };
            m_gTurnee.SelectionChanged += m_gTurnee_SelectionChanged;
            m_gContracteCursant.DoubleClick += (s, e) =>
            {
                object? val = m_gContracteCursant.GetColumnData("CUCO_Id");
                if (val == null || val == DBNull.Value) return;
                int cucoId = Convert.ToInt32(val);
                if (!FrmGestiuneContract.PoateDeschide(ManagerDeProiect)) return;
                var frm = new FrmGestiuneContract(FrmParent, ManagerDeProiect, cucoId)
                { MdiParent = FrmParent };
                frm.Show();
            };

            m_gRundeCursant = new GridPersonalizat.CustomGrid
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                RowHeadersWidth = 25,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Font = new Font("Arial", 8F)
            };

            var split = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Horizontal,
                SplitterDistance = 220
            };
            split.Panel1.Controls.Add(m_gTurnee);
            split.Panel2.Controls.Add(m_gRundeCursant);

            tab.Controls.Add(split);
            m_tabControl.TabPages.Add(tab);
        }

        private void InitTabPartide()
        {
            var tab = new TabPage("Partide");

            m_gPartide = new GridPersonalizat.CustomGrid
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                RowHeadersWidth = 25,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Font = new Font("Arial", 8F)
            };

            tab.Controls.Add(m_gPartide);
            m_tabControl.TabPages.Add(tab);
        }

        private void IncarcaPartide()
        {
            if (m_lCUCU <= 0) return;
            const string sql = @"
                SELECT ROW_NUMBER() OVER (ORDER BY T.CUTE_Data DESC, J.CUTJ_Runda DESC)
                                                                               AS [Nr.],
                       J.CUTJ_Runda                                            AS [Rd.],
                       ISNULL(ADV.CUJA_Nume + N' ' + ADV.CUJA_Prenume, N'—') AS [Adversar],
                       ISNULL(CAST(J.CUTJ_RatingAdv AS NVARCHAR(6)), N'—')    AS [Rtg adv],
                       CASE J.CUTJ_Culoare
                           WHEN 1 THEN N'Alb' WHEN 2 THEN N'Negru' ELSE N'—'
                       END                                                     AS [Culoare],
                       CASE WHEN J.CUTJ_Rezultat = 1   THEN N'1'
                            WHEN J.CUTJ_Rezultat = 0.5 THEN N'½'
                            ELSE N'0' END                                      AS [Rez.],
                       ISNULL(CAST(J.CUTJ_WeExpected AS NVARCHAR(8)), N'—')   AS [We],
                       ISNULL(CAST(J.CUTJ_RtgDelta   AS NVARCHAR(8)), N'—')   AS [Rtg+/-],
                       T.CUTE_Denumire                                         AS [Turneu],
                       CASE T.CUTE_Tip
                           WHEN 1 THEN N'Clasic' WHEN 2 THEN N'Rapid'
                           WHEN 3 THEN N'Blitz'  WHEN 4 THEN N'Alte'
                           ELSE N'—' END                                       AS [Tip],
                       CONVERT(NVARCHAR(10), T.CUTE_Data, 104)                 AS [Data]
                  FROM CU_TurneeJocuri J
                  JOIN CU_TurneeDetaliu D   ON J.CUTJ_CUTD_Id     = D.CUTD_Id
                  JOIN CU_Turnee T          ON D.CUTD_CUTE_Id      = T.CUTE_Id
                  JOIN CU_Jucatori MINE     ON D.CUTD_CUJA_Id      = MINE.CUJA_Id
                 LEFT JOIN CU_Jucatori ADV  ON J.CUTJ_CUJA_Id_Adv  = ADV.CUJA_Id
                 WHERE MINE.CUJA_CUCU_Id = @cucu
                   AND J.CUTJ_NrStartAdv IS NOT NULL
                 ORDER BY T.CUTE_Data DESC, J.CUTJ_Runda DESC";

            m_gPartide.SetSql(sql, CInterogareSQL.P("@cucu", m_lCUCU));
            m_gPartide.FormatColumn("Nr.",      40,  true, DataGridViewContentAlignment.MiddleRight);
            m_gPartide.FormatColumn("Rd.",      38,  true, DataGridViewContentAlignment.MiddleCenter);
            m_gPartide.FormatColumn("Adversar", 210, true);
            m_gPartide.FormatColumn("Rtg adv",  65,  true, DataGridViewContentAlignment.MiddleRight);
            m_gPartide.FormatColumn("Culoare",  65,  true, DataGridViewContentAlignment.MiddleCenter);
            m_gPartide.FormatColumn("Rez.",     45,  true, DataGridViewContentAlignment.MiddleCenter);
            m_gPartide.FormatColumn("We",       55,  true, DataGridViewContentAlignment.MiddleRight);
            m_gPartide.FormatColumn("Rtg+/-",   65,  true, DataGridViewContentAlignment.MiddleRight);
            m_gPartide.FormatColumn("Turneu",   240, true);
            m_gPartide.FormatColumn("Tip",       60, true, DataGridViewContentAlignment.MiddleCenter);
            m_gPartide.FormatColumn("Data",      90, true, DataGridViewContentAlignment.MiddleCenter);
            foreach (DataGridViewColumn c in m_gPartide.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void m_gTurnee_SelectionChanged(object? sender, EventArgs e)
        {
            object? val = m_gTurnee.GetColumnData("CUTD_Id");
            if (val == null || val == DBNull.Value)
            {
                m_gRundeCursant.DataSource = null;
                return;
            }
            IncarcaRundePentruTurneu(Convert.ToInt32(val));
        }

        private void IncarcaRundePentruTurneu(int cutdId)
        {
            if (cutdId <= 0) { m_gRundeCursant.DataSource = null; return; }
            GridPersonalizat.CustomGrid.SetConnection(ManagerDeProiect.GetConnection());
            string sql = @"
                SELECT J.CUTJ_Runda                                           AS [Rd.],
                       ISNULL(ADV.CUJA_Nume + N' ' + ADV.CUJA_Prenume, N'—') AS [Adversar],
                       ISNULL(CAST(J.CUTJ_RatingAdv AS NVARCHAR(6)), N'—')   AS [Rtg adv],
                       CASE J.CUTJ_Culoare
                           WHEN 1 THEN N'Alb' WHEN 2 THEN N'Negru' ELSE N'—'
                       END                                                    AS [Culoare],
                       CASE WHEN J.CUTJ_Rezultat = 1   THEN N'1'
                            WHEN J.CUTJ_Rezultat = 0.5 THEN N'½'
                            ELSE N'0' END                                     AS [Rez.],
                       ISNULL(CAST(J.CUTJ_WeExpected AS NVARCHAR(8)), N'—')  AS [We],
                       ISNULL(CAST(J.CUTJ_RtgDelta   AS NVARCHAR(8)), N'—')  AS [Rtg+/-]
                  FROM CU_TurneeJocuri J
                  LEFT JOIN CU_Jucatori ADV ON J.CUTJ_CUJA_Id_Adv = ADV.CUJA_Id
                 WHERE J.CUTJ_CUTD_Id = @cutd
                 ORDER BY J.CUTJ_Runda";
            m_gRundeCursant.SetSql(sql, CInterogareSQL.P("@cutd", cutdId));
            m_gRundeCursant.FormatColumn("Rd.",      40,  true, DataGridViewContentAlignment.MiddleCenter);
            m_gRundeCursant.FormatColumn("Adversar", 220, true);
            m_gRundeCursant.FormatColumn("Rtg adv",  65,  true, DataGridViewContentAlignment.MiddleRight);
            m_gRundeCursant.FormatColumn("Culoare",  65,  true, DataGridViewContentAlignment.MiddleCenter);
            m_gRundeCursant.FormatColumn("Rez.",     45,  true, DataGridViewContentAlignment.MiddleCenter);
            m_gRundeCursant.FormatColumn("We",       55,  true, DataGridViewContentAlignment.MiddleRight);
            m_gRundeCursant.FormatColumn("Rtg+/-",   65,  true, DataGridViewContentAlignment.MiddleRight);
            foreach (DataGridViewColumn c in m_gRundeCursant.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void IncarcaTurnee()
        {
            if (m_lCUCU <= 0) return;
            GridPersonalizat.CustomGrid.SetConnection(ManagerDeProiect.GetConnection());
            string sql = @"
                SELECT T.CUTE_Id,
                       D.CUTD_Id,
                       T.CUTE_Data              AS [Data],
                       T.CUTE_Denumire          AS [Turneu],
                       T.CUTE_Localitate        AS [Localitate],
                       D.CUTD_NrStart           AS [Nr.start],
                       D.CUTD_RatingLaTurneu    AS [Rating],
                       D.CUTD_Puncte            AS [Puncte],
                       D.CUTD_Loc               AS [Loc],
                       D.CUTD_RatingPerformanta AS [Perf.]
                  FROM CU_TurneeDetaliu D
                  JOIN CU_Turnee T    ON D.CUTD_CUTE_Id  = T.CUTE_Id
                  JOIN CU_Jucatori J  ON D.CUTD_CUJA_Id  = J.CUJA_Id
                 WHERE J.CUJA_CUCU_Id = @cucu
                 ORDER BY T.CUTE_Data DESC";

            m_gTurnee.SetSql(sql, CInterogareSQL.P("@cucu", m_lCUCU));
            m_gTurnee.FormatColumn("CUTE_Id",    60,  false);
            m_gTurnee.FormatColumn("CUTD_Id",    60,  false);
            m_gTurnee.FormatColumn("Data",        90,  true, null, "dd.MM.yyyy");
            m_gTurnee.FormatColumn("Turneu",     250);
            m_gTurnee.FormatColumn("Localitate", 130);
            m_gTurnee.FormatColumn("Nr.start",    60,  true, DataGridViewContentAlignment.MiddleCenter);
            m_gTurnee.FormatColumn("Rating",      55,  true, DataGridViewContentAlignment.MiddleRight);
            m_gTurnee.FormatColumn("Puncte",      60,  true, DataGridViewContentAlignment.MiddleRight);
            m_gTurnee.FormatColumn("Loc",         45,  true, DataGridViewContentAlignment.MiddleRight);
            m_gTurnee.FormatColumn("Perf.",       55,  true, DataGridViewContentAlignment.MiddleRight);
            m_gTurnee.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn c in m_gTurnee.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void IncarcaELOAutomat()
        {
            if (m_lCUCU <= 0) return;
            GridPersonalizat.CustomGrid.SetConnection(ManagerDeProiect.GetConnection());
            string sql = @"
                SELECT T.CUTE_Data                               AS [Data],
                       T.CUTE_Denumire                          AS [Turneu],
                       D.CUTD_RatingLaTurneu                    AS [Rating],
                       (SELECT CAST(SUM(CUTJ_RtgDelta) AS DECIMAL(6,1))
                          FROM CU_TurneeJocuri
                         WHERE CUTJ_CUTD_Id = D.CUTD_Id
                           AND CUTJ_RtgDelta IS NOT NULL)       AS [Rtg+/- total]
                  FROM CU_TurneeDetaliu D
                  JOIN CU_Turnee T    ON D.CUTD_CUTE_Id = T.CUTE_Id
                  JOIN CU_Jucatori J  ON D.CUTD_CUJA_Id = J.CUJA_Id
                 WHERE J.CUJA_CUCU_Id = @cucu
                 ORDER BY T.CUTE_Data DESC";

            m_gELOTurnee.SetSql(sql, CInterogareSQL.P("@cucu", m_lCUCU));
            m_gELOTurnee.FormatColumn("Data",         90,  true, null, "dd.MM.yyyy");
            m_gELOTurnee.FormatColumn("Turneu",       260, true);
            m_gELOTurnee.FormatColumn("Rating",        65, true, DataGridViewContentAlignment.MiddleRight);
            m_gELOTurnee.FormatColumn("Rtg+/- total",  90, true, DataGridViewContentAlignment.MiddleRight);
            foreach (DataGridViewColumn c in m_gELOTurnee.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void IncarcaELO()
        {
            IncarcaELOAutomat();
            m_dgvELO.Rows.Clear();
            if (m_lCUCU <= 0) return;

            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            q.OpenP(@"SELECT CUELO_Data,
                             CAST(CUELO_Clasic AS DECIMAL(8,0)),
                             CAST(CUELO_Rapid  AS DECIMAL(8,0)),
                             CAST(CUELO_Blitz  AS DECIMAL(8,0))
                        FROM CU_IstoricELO
                       WHERE CUELO_CUCU_Id = @id
                       ORDER BY CUELO_Data DESC",
                CInterogareSQL.P("@id", m_lCUCU));
            while (q.NextRow())
            {
                var dt      = q.GetDateValue(0);
                decimal? clasic = q.GetDecimalValue(1);
                decimal? rapid  = q.GetDecimalValue(2);
                decimal? blitz  = q.GetDecimalValue(3);
                m_dgvELO.Rows.Add(
                    dt.HasValue     ? dt.Value.ToString("dd.MM.yyyy")       : "",
                    clasic.HasValue ? ((int)clasic.Value).ToString()         : "",
                    rapid.HasValue  ? ((int)rapid.Value).ToString()          : "",
                    blitz.HasValue  ? ((int)blitz.Value).ToString()          : "");
            }
            q.Close();
        }

        private void SalveazaELO()
        {
            if (m_lCUCU <= 0) return;

            var rows = new List<(DateTime data, int? clasic, int? rapid, int? blitz)>();
            foreach (DataGridViewRow row in m_dgvELO.Rows)
            {
                if (row.IsNewRow) continue;

                string sData   = (row.Cells["colData"].Value?.ToString()   ?? "").Trim();
                string sClasic = (row.Cells["colClasic"].Value?.ToString() ?? "").Trim();
                string sRapid  = (row.Cells["colRapid"].Value?.ToString()  ?? "").Trim();
                string sBlitz  = (row.Cells["colBlitz"].Value?.ToString()  ?? "").Trim();

                if (string.IsNullOrEmpty(sData)) continue;

                if (!DateTime.TryParseExact(sData,
                        new[] { "dd.MM.yyyy", "ddMMyyyy" },
                        System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.None, out DateTime dtData))
                    return;

                int? clasic = null, rapid = null, blitz = null;
                if (!string.IsNullOrEmpty(sClasic))
                {
                    if (!int.TryParse(sClasic, out int v) || v <= 0) return;
                    clasic = v;
                }
                if (!string.IsNullOrEmpty(sRapid))
                {
                    if (!int.TryParse(sRapid, out int v) || v <= 0) return;
                    rapid = v;
                }
                if (!string.IsNullOrEmpty(sBlitz))
                {
                    if (!int.TryParse(sBlitz, out int v) || v <= 0) return;
                    blitz = v;
                }

                rows.Add((dtData, clasic, rapid, blitz));
            }

            if (rows.Count == 0) return;

            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            q.BeginTransaction(System.Data.IsolationLevel.Serializable);
            try
            {
                q.ExecuteNonQueryP("DELETE FROM CU_IstoricELO WHERE CUELO_CUCU_Id = @id",
                    CInterogareSQL.P("@id", m_lCUCU));

                foreach (var (data, clasic, rapid, blitz) in rows)
                    q.ExecuteNonQueryP(
                        @"INSERT INTO CU_IstoricELO (CUELO_CUCU_Id, CUELO_Data, CUELO_Clasic, CUELO_Rapid, CUELO_Blitz)
                          VALUES (@id, @d, @c, @r, @b)",
                        CInterogareSQL.P("@id", m_lCUCU),
                        CInterogareSQL.P("@d",  data),
                        CInterogareSQL.P("@c",  (object?)clasic ?? DBNull.Value),
                        CInterogareSQL.P("@r",  (object?)rapid  ?? DBNull.Value),
                        CInterogareSQL.P("@b",  (object?)blitz  ?? DBNull.Value));

                q.CommitTransaction();
            }
            catch
            {
                q.RollbackTransaction();
            }
            finally
            {
                q.Close();
            }
        }

        private void Modifica()
        {
            if (m_lCUCU > 0)
            {
                sSql = string.Format(@"SELECT CUCU_NrMatricol,CUCU_CNP, CUCU_DataNasterii, CUCU_Nume, CUCU_Prenume,
                                              CUCU_CISerie, CUCU_CINumar, CUCU_Strada, CUCU_NumarStrada, CUCU_Bloc, CUCU_Scara,
                                              CUCU_Apartament, CUCU_Oras, CUCU_Judet,
                                              CUCU_LocalitateaNasterii,CUCU_JudetulNasterii,
                                              CUCU_TelefonMobil, CUCU_TelefonFix, CUCU_Email, CUCU_NumeFacebook, CUCU_NumeSkype,
                                              CUCU_NumeTutore,CUCU_PrenumeTutore,CUCU_TelefonTutore,CUCU_EmailTutore,
                                              CUCU_CISerieTutore, CUCU_CINumarTutore, CUCU_CNPTutore, CUCU_Barcode,
                                              ISNULL(CUCU_CetAteanStrain,0), ISNULL(CUCU_NrDocumentStrain,''),
                                              CAST(ISNULL(CUCU_TipDocument,0) AS INT),
                                              ISNULL(CUCU_FideId,0)
                                        FROM CU_Cursanti
                                        WHERE CUCU_Id = {0}", m_lCUCU);
                int i = -1;
                CInterogareSQL q = new(ManagerDeProiect.GetConnection());
                q.Open(sSql);
                if (q.NextRow())
                {
                    m_ebNrMatricol.Text = q.GetIntValue(++i).ToString();
                    string sCNP = q.GetStringValue(++i)!;
                    m_ebCNP.Text = sCNP;
                    if (q.GetDateValue(++i) != null)
                        m_dtDataNasterii.Value = (DateTime)q.GetDateValue(i)!;
                    m_ebNume.Text = q.GetStringValue(++i);
                    m_ebPrenume.Text = q.GetStringValue(++i);
                    m_tslNume.Text = $"{m_ebNume.Text} {m_ebPrenume.Text}".Trim();
                    m_ebCISerie.Text = q.GetStringValue(++i);
                    m_ebCINumar.Text = q.GetStringValue(++i);
                    m_ebStrada.Text = q.GetStringValue(++i);
                    m_ebNumarStrada.Text = q.GetStringValue(++i);
                    m_ebBloc.Text = q.GetStringValue(++i);
                    m_ebScara.Text = q.GetStringValue(++i);
                    m_ebAp.Text = q.GetStringValue(++i);
                    m_ebOras.Text = q.GetStringValue(++i);
                    m_ebJudet.Text = q.GetStringValue(++i);
                    m_ebLocalitateaNasterii.Text = q.GetStringValue(++i);
                    m_ebJudetulNasterii.Text = q.GetStringValue(++i);
                    m_ebTelMobil.Text = q.GetStringValue(++i);
                    m_ebTelFix.Text = q.GetStringValue(++i);
                    m_ebEmail.Text = q.GetStringValue(++i);
                    m_ebNumeFB.Text = q.GetStringValue(++i);
                    m_ebNumeSkype.Text = q.GetStringValue(++i);
                    m_ebNumeTutore.Text = q.GetStringValue(++i);
                    m_ebPrenumeTutore.Text = q.GetStringValue(++i);
                    m_ebTelefonTutore.Text = q.GetStringValue(++i);
                    m_ebEmailTutore.Text = q.GetStringValue(++i);
                    m_ebCISerieTutore.Text = q.GetStringValue(++i);
                    m_ebCINumarTutore.Text = q.GetStringValue(++i);
                    m_ebCNPTutore.Text = q.GetStringValue(++i);
                    m_ebBarcode.Text = q.GetStringValue(++i);
                    bool strain = q.GetBoolValue(++i);
                    m_cbCetAteanStrain.Checked = strain;
                    m_ebNrDocumentStrain.Text = q.GetStringValue(++i) ?? "";
                    m_ebCNP.Enabled = !strain;
                    m_lblNrDocument.Visible = strain;
                    m_ebNrDocumentStrain.Visible = strain;
                    int tipDoc = q.GetIntValue(++i);
                    m_cbTipDocument.SelectedIndex = (tipDoc >= 1 && tipDoc <= 3) ? tipDoc : 0;
                    int fideId = q.GetIntValue(++i);
                    m_ebFideId.Text = fideId > 0 ? fideId.ToString() : "";
                }
                q.Close();
            }

            m_stbSalveaza.Enabled = false;

            if (m_lCUCU > 0)
            {
                IncarcaContracteCursant();
                IncarcaArhivaCursant();
                IncarcaELO();
                IncarcaTurnee();
                IncarcaPartide();
                m_tsbDeschideArhiva.Enabled = true;
            }
        }

        private void IncarcaContracteCursant()
        {
            if (m_lCUCU <= 0) return;
            var sql = @"
SELECT CUCO_Id,
       CUCO_NrContract             AS [Nr. contract],
       CUCO_DataInceputContract    AS [Data început],
       CUCO_DataSfarsitContract    AS [Data sfârșit]
  FROM CU_Contracte
  INNER JOIN CU_ContracteCursanti ON CUCC_CUCO_Id = CUCO_Id
  WHERE CUCC_CUCU_Id = @cucu
  ORDER BY CUCO_DataInceputContract DESC;";
            m_gContracteCursant.SetSql(sql, CInterogareSQL.P("@cucu", m_lCUCU));
            m_gContracteCursant.FormatColumn("CUCO_Id", 0, false);
            m_gContracteCursant.FormatColumn("Nr. contract", 120, true, DataGridViewContentAlignment.MiddleRight);
            m_gContracteCursant.FormatColumn("Data început", 110, true, null, "dd.MM.yyyy");
            m_gContracteCursant.FormatColumn("Data sfârșit", 110, true, null, "dd.MM.yyyy");
            foreach (DataGridViewColumn c in m_gContracteCursant.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void m_gContracteCursant_SelectionChanged(object sender, EventArgs e)
        {
            object? val = m_gContracteCursant.GetColumnData("CUCO_Id");
            if (val == null || val == DBNull.Value) return;
            IncarcaAbonamenteCursant(Convert.ToInt32(val));
        }

        private void IncarcaAbonamenteCursant(int cucoId)
        {
            if (cucoId <= 0) { m_gAbonamenteCursant.SetSql("SELECT 1 WHERE 1=0"); return; }
            m_gAbonamenteCursant.SetSql(@"
SELECT ROW_NUMBER() OVER (ORDER BY CUAC_DataInceput DESC) AS [Nr. crt.],
       CUAC_Id,
       CUAB_Descriere   AS [Tip abonament],
       CUAC_DataInceput AS [Data început],
       CUAC_DataSfarsit AS [Data sfârșit],
       CUAC_Pret        AS [Preț (lei)]
  FROM CU_AbonamenteCursant
  INNER JOIN CU_Abonamente ON CUAC_CUAB_Id = CUAB_Id
 WHERE CUAC_CUCO_Id = @id
 ORDER BY CUAC_DataInceput DESC;",
                CInterogareSQL.P("@id", cucoId));
            var g = m_gAbonamenteCursant;
            if (g.Columns.Count < 6) return;
            g.Columns[0].Width = 60;  g.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            g.Columns[1].Visible = false;
            g.Columns[2].Width = 280;
            g.Columns[3].Width = 110; g.Columns[3].DefaultCellStyle.Format = "dd.MM.yyyy";
            g.Columns[4].Width = 110; g.Columns[4].DefaultCellStyle.Format = "dd.MM.yyyy";
            g.Columns[5].Width = 100; g.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            g.Columns[5].DefaultCellStyle.Format = "N2";
            foreach (DataGridViewColumn c in g.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void IncarcaArhivaCursant()
        {
            if (m_lCUCU <= 0) return;
            var sql = @"
SELECT ADAH_Id,
       ISNULL(ADAH_NumeFisier, RIGHT(ADAH_Cale, LEN(ADAH_Cale) - LEN(LEFT(ADAH_Cale, LEN(ADAH_Cale) - CHARINDEX('\', REVERSE(ADAH_Cale)))))) AS [Fișier],
       ADAH_DataAdaugare AS [Data],
       ADAH_Cale         AS [Cale]
  FROM AD_Arhiva
 WHERE ADAH_CUCU_Id = @cucu
    OR ADAH_CUCO_Id IN (SELECT CUCC_CUCO_Id FROM CU_ContracteCursanti WHERE CUCC_CUCU_Id = @cucu)
    OR ADAH_ADRN_Id IN (
           SELECT ADRN_Id FROM AD_RegistruNumereInterne
            WHERE ADRN_CUCO_Id IN (SELECT CUCC_CUCO_Id FROM CU_ContracteCursanti WHERE CUCC_CUCU_Id = @cucu))
 ORDER BY ADAH_DataAdaugare DESC, ADAH_Id DESC;";
            m_gArhivaCursant.SetSql(sql, CInterogareSQL.P("@cucu", m_lCUCU));
            m_gArhivaCursant.FormatColumn("ADAH_Id", 0, false);
            m_gArhivaCursant.FormatColumn("Fișier", 350);
            m_gArhivaCursant.FormatColumn("Data", 130, true, null, "dd.MM.yyyy HH:mm");
            m_gArhivaCursant.FormatColumn("Cale", 600);
            foreach (DataGridViewColumn c in m_gArhivaCursant.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void m_tsbDeschideArhiva_Click(object sender, EventArgs e)
        {
            m_tabControl.SelectedTab = m_tabPageArhivaCursant;
        }

        private void m_btnDeschideArhivaCursant_Click(object sender, EventArgs e)
        {
            object? val = m_gArhivaCursant.GetColumnData("Cale");
            if (val == null || val == DBNull.Value) return;
            string cale = val.ToString()!;
            if (!File.Exists(cale)) { MessageBox.Show("Fișierul nu mai există la calea: " + cale); return; }
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(cale) { UseShellExecute = true });
        }

        private void m_btnAdaugaArhivaCursant_Click(object sender, EventArgs e)
        {
            if (m_lCUCU <= 0) return;
            using var dlg = new OpenFileDialog { Title = "Selectați fișierul de arhivat", Filter = "Toate fișierele|*.*" };
            if (dlg.ShowDialog() != DialogResult.OK) return;

            string src = dlg.FileName;
            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            string? numeCursant = q.ExecuteToStringP(
                "SELECT RTRIM(CUCU_Nume) + '_' + RTRIM(CUCU_Prenume) FROM CU_Cursanti WHERE CUCU_Id = @id",
                CInterogareSQL.P("@id", m_lCUCU));
            q.Close();

            string folder = Path.Combine(@"d:\WORK\ONIXTEIA\ACTE\ARHIVA\CURSANTI", numeCursant ?? m_lCUCU.ToString());
            Directory.CreateDirectory(folder);
            string dest = Path.Combine(folder, Path.GetFileName(src));
            if (File.Exists(dest) &&
                MessageBox.Show("Fișierul există deja. Suprascrieți?", "Atenție", MessageBoxButtons.YesNo) == DialogResult.No)
                return;
            File.Copy(src, dest, true);

            var q2 = new CInterogareSQL(ManagerDeProiect.GetConnection());
            int exists = q2.ExecuteToIntP("SELECT COUNT(*) FROM AD_Arhiva WHERE ADAH_Cale = @cale",
                CInterogareSQL.P("@cale", dest));
            if (exists == 0)
                q2.ExecuteNonQueryP(
                    @"INSERT INTO AD_Arhiva (ADAH_Cale, ADAH_NumeFisier, ADAH_DataAdaugare, ADAH_CUCU_Id, ADAH_MBDV_Id)
                      VALUES (@cale, @nume, GETDATE(), @cucu, @div)",
                    CInterogareSQL.P("@cale", dest),
                    CInterogareSQL.P("@nume", Path.GetFileName(dest)),
                    CInterogareSQL.P("@cucu", m_lCUCU),
                    CInterogareSQL.P("@div",  ManagerDeProiect.GetDiviziune()));
            q2.Close();
            IncarcaArhivaCursant();
        }

        private void m_btnStergeArhivaCursant_Click(object sender, EventArgs e)
        {
            object? val = m_gArhivaCursant.GetColumnData("ADAH_Id");
            if (val == null || val == DBNull.Value) return;
            int id = Convert.ToInt32(val);
            if (MessageBox.Show("Ștergeți înregistrarea din arhivă?\n(fișierul de pe disc nu va fi șters)", "Atenție",
                    MessageBoxButtons.YesNo) == DialogResult.No) return;
            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            q.ExecuteNonQueryP("DELETE FROM AD_Arhiva WHERE ADAH_Id = @id", CInterogareSQL.P("@id", id));
            q.Close();
            IncarcaArhivaCursant();
        }

        private void FrmGestiuneCursant_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_stbSalveaza.Enabled == true)
            {
                if (MessageBox.Show(CFormatareDate.GetString("IDS_SAVE_ASK"), CFormatareDate.GetString("IDS_ATENTIE"), MessageBoxButtons.YesNo) == DialogResult.No)
                    e.Cancel = false;
                else
                    if (!Salveaza())
                        e.Cancel = true;
            }
        }

        private bool Salveaza()
        {
            object? value;
            string sData, sSql, sUpdate = "", sTmp,
                sCNP, sDataNasterii, sNume, sPrenume,
                sCISerie, sCINumar, sStrada, sNumarStrada, sBloc, sScara,
                sAp, sPrenumeTutore, sTelMobil, sTelFix,
                sEmail, sOras, sJudet, sLocalitateaNasterii, sJudetulNasterii, sNumeFB, sNumeSkype, sNumeTutore, sEmailTutore, sTelefonTutore, sCINumarTutore, sCISerieTutore, sCNPTutore;

            //NUME
            sData = m_ebNume.Text.Trim().ToUpper();
            if (sData == "")
            {
                CFormatareDate.ShowWarning("IDS_ERR_NUMECURSANT");
                m_ebNume.Focus();
                return false;
            }
            else
            {
                sNume = string.Format(" N'{0}' ", CFormatareDate.StringToSqlString(sData));
                sTmp = string.Format(" CUCU_Nume = N'{0}'", CFormatareDate.StringToSqlString(sData));
                sUpdate += sTmp;
            }
            //PRENUME
            sData = m_ebPrenume.Text.Trim().ToUpper();
            if (sData == "")
            {
                CFormatareDate.ShowWarning("IDS_ERR_PRENUMECURSANT");
                m_ebPrenume.Focus();
                return false;
            }
            else
            {
                sPrenume = string.Format(" N'{0}' ", CFormatareDate.StringToSqlString(sData));
                sTmp = string.Format(" ,CUCU_Prenume = N'{0}'", CFormatareDate.StringToSqlString(sData));
                sUpdate += sTmp;
            }

            //CNP
            sData = m_ebCNP.Text;
            if (sData == "" && !m_cbCetAteanStrain.Checked)
            {
                CFormatareDate.ShowWarning("IDS_ERR_CNPCURSANT");
                m_ebCNP.Focus();
                return false;
            }
            else
            {
                sCNP = string.Format(" '{0}' ", CFormatareDate.StringToSqlString(sData));
                sTmp = string.Format(" ,CUCU_CNP = '{0}'", CFormatareDate.StringToSqlString(sData));
                sUpdate += sTmp;
            }
            if (sData != "-" && !m_cbCetAteanStrain.Checked)
            {
                //TEST DACA CNP ESTE VALID
                if (!EsteCNPValid(m_ebCNP, m_dtDataNasterii))
                    return false;

                if (!ExistaCNP(m_ebCNP))
                    return false;
            }


            sData = m_dtDataNasterii.Text;
            if (sData == "")
            {
                //CFormatareDate.ShowWarning("IDS_ERR_DATANASTERIICURSANT");
                //m_dtDataNasterii.Focus();
                //return false;
                sDataNasterii = string.Format("NULL");
                sTmp = string.Format(" ,CUCU_DataNasterii = NULL ");
                sUpdate += sTmp;
            }
            else
            {
                sDataNasterii = string.Format(" {0} ", CFormatareDate.DateToSQLString(m_dtDataNasterii.Value));
                sTmp = string.Format(" ,CUCU_DataNasterii = {0} ", CFormatareDate.DateToSQLString(m_dtDataNasterii.Value));
                sUpdate += sTmp;
            }

            sData = m_ebLocalitateaNasterii.Text.Trim();
            if (sData == "")
            {
                sLocalitateaNasterii = string.Format("NULL");
                sTmp = string.Format(" ,CUCU_LocalitateaNasterii = NULL");
                sUpdate += sTmp;
            }
            else
            {
                sLocalitateaNasterii = string.Format(" N'{0}' ", CFormatareDate.StringToSqlString(sData));
                sTmp = string.Format(" ,CUCU_LocalitateaNasterii = N'{0}'", CFormatareDate.StringToSqlString(sData));
                sUpdate += sTmp;
            }
            sData = m_ebJudetulNasterii.Text.Trim();
            if (sData == "")
            {
                sJudetulNasterii = string.Format("NULL");
                sTmp = string.Format(" ,CUCU_JudetulNasterii = NULL");
                sUpdate += sTmp;
            }
            else
            {
                sJudetulNasterii = string.Format(" N'{0}' ", CFormatareDate.StringToSqlString(sData));
                sTmp = string.Format(" ,CUCU_JudetulNasterii = N'{0}'", CFormatareDate.StringToSqlString(sData));
                sUpdate += sTmp;
            }

            //CI Serie, 
            sData = m_ebCISerie.Text.Trim().ToUpper();
            if (sData == "")
            {
                //CFormatareDate.ShowWarning("IDS_ERR_CISERIECURSANT");
                //m_ebCISerie.Focus();
                //return false;

                sCISerie = string.Format("NULL");
                sTmp = string.Format(" ,CUCU_CISerie = NULL");
                sUpdate += sTmp;
            }
            else
            {
                sCISerie = string.Format(" '{0}' ", CFormatareDate.StringToSqlString(sData));
                sTmp = string.Format(" ,CUCU_CISerie = '{0}'", CFormatareDate.StringToSqlString(sData));
                sUpdate += sTmp;
            }
            //CI Numar
            sData = m_ebCINumar.Text;
            if (sData == "")
            {
                //CFormatareDate.ShowWarning("IDS_ERR_CINUMARCURSANT");
                //m_ebCINumar.Focus();
                //return false;

                sCINumar = string.Format("NULL");
                sTmp = string.Format(" ,CUCU_CINumar = NULL");
                sUpdate += sTmp;
            }
            else
            {
                sCINumar = string.Format(" '{0}' ", CFormatareDate.StringToSqlString(sData));
                sTmp = string.Format(" ,CUCU_CINumar = '{0}'", CFormatareDate.StringToSqlString(sData));
                sUpdate += sTmp;
            }
            //CUCU_Strada 
            sData = m_ebStrada.Text.Trim();
            if (sData == "")
            {
                //CFormatareDate.ShowWarning("IDS_ERR_STRADACURSANT");
                //m_ebStrada.Focus();
                //return false;
                sStrada = string.Format("NULL");
                sTmp = string.Format(" ,CUCU_Strada = NULL");
                sUpdate += sTmp;
            }
            else
            {
                sStrada = string.Format(" N'{0}' ", CFormatareDate.StringToSqlString(sData));
                sTmp = string.Format(" ,CUCU_Strada = N'{0}'", CFormatareDate.StringToSqlString(sData));
                sUpdate += sTmp;
            }
            //CUCU_NumarStrada
            sData = m_ebNumarStrada.Text;
            if (sData == "")
            {
                sNumarStrada = string.Format("NULL");
                sTmp = string.Format(" ,CUCU_NumarStrada = NULL");
                sUpdate += sTmp;
            }
            else
            {
                sNumarStrada = string.Format(" '{0}' ", CFormatareDate.StringToSqlString(sData));
                sTmp = string.Format(" ,CUCU_NumarStrada = '{0}'", CFormatareDate.StringToSqlString(sData));
                sUpdate += sTmp;
            }
            //Bloc
            sData = m_ebBloc.Text;
            if (sData == "")
            {
                sBloc = string.Format("NULL");
                sTmp = string.Format(" ,CUCU_Bloc = NULL");
                sUpdate += sTmp;
            }
            else
            {
                sBloc = string.Format(" '{0}' ", CFormatareDate.StringToSqlString(sData));
                sTmp = string.Format(" ,CUCU_Bloc = '{0}'", CFormatareDate.StringToSqlString(sData));
                sUpdate += sTmp;
            }
            //CUCU_Scara
            sData = m_ebScara.Text;
            if (sData == "")
            {
                sScara = string.Format("NULL");
                sTmp = string.Format(" ,CUCU_Scara = NULL");
                sUpdate += sTmp;
            }
            else
            {
                sScara = string.Format(" '{0}' ", CFormatareDate.StringToSqlString(sData));
                sTmp = string.Format(" ,CUCU_Scara = '{0}'", CFormatareDate.StringToSqlString(sData));
                sUpdate += sTmp;
            }
            //CUCU_Apartament
            sData = m_ebAp.Text;
            if (sData == "")
            {
                sAp = string.Format("NULL");
                sTmp = string.Format(" ,CUCU_Apartament = NULL");
                sUpdate += sTmp;
            }
            else
            {
                sAp = string.Format(" '{0}' ", CFormatareDate.StringToSqlString(sData));
                sTmp = string.Format(" ,CUCU_Apartament = '{0}'", CFormatareDate.StringToSqlString(sData));
                sUpdate += sTmp;
            }
            //  CUCU_Oras
            sData = m_ebOras.Text;
            if (sData == "")
            {
                sOras = string.Format("NULL");
                sTmp = string.Format(" ,CUCU_Oras = NULL");
                sUpdate += sTmp;
            }
            else
            {
                sOras = string.Format(" N'{0}' ", CFormatareDate.StringToSqlString(sData));
                sTmp = string.Format(" ,CUCU_Oras = N'{0}'", CFormatareDate.StringToSqlString(sData));
                sUpdate += sTmp;
            }
            //CUCU_Judet
            sData = m_ebJudet.Text;
            if (sData == "")
            {
                sJudet = string.Format("NULL");
                sTmp = string.Format(" ,CUCU_Judet = NULL");
                sUpdate += sTmp;
            }
            else
            {
                sJudet = string.Format(" N'{0}' ", CFormatareDate.StringToSqlString(sData));
                sTmp = string.Format(" ,CUCU_Judet = N'{0}'", CFormatareDate.StringToSqlString(sData));
                sUpdate += sTmp;
            }

            sData = m_ebTelefonTutore.Text;
            if (sData == "")
            {
                sTelefonTutore = string.Format("NULL");
                sTmp = string.Format(" ,CUCU_TelefonTutore = NULL");
                sUpdate += sTmp;
            }
            else
            {
                sTelefonTutore = string.Format(" '{0}' ", CFormatareDate.StringToSqlString(sData));
                sTmp = string.Format(" ,CUCU_TelefonTutore = '{0}'", CFormatareDate.StringToSqlString(sData));
                sUpdate += sTmp;
            }

            // CUCU_TelefonMobil
            sData = m_ebTelMobil.Text;
            if (sData == "")
            {
                // sTelMobil = string.Format("NULL");
                //CFormatareDate.ShowWarning("IDS_ERR_TELCURSANT");
                //m_ebTelMobil.Focus();
                //return false;
                sTelMobil = string.Format("NULL");
                sTmp = string.Format(" ,CUCU_TelefonMobil = NULL");
                sUpdate += sTmp;

            }
            else
            {
                sTelMobil = string.Format(" '{0}' ", CFormatareDate.StringToSqlString(sData));
                sTmp = string.Format(" ,CUCU_TelefonMobil ='{0}'", CFormatareDate.StringToSqlString(sData));
                sUpdate += sTmp;
            }
            // CUCU_TelefonFix
            sData = m_ebTelFix.Text;
            if (sData == "")
            {
                sTelFix = string.Format("NULL");
                sTmp = string.Format(" ,CUCU_TelefonFix = NULL");
                sUpdate += sTmp;
            }
            else
            {
                sTelFix = string.Format(" '{0}' ", CFormatareDate.StringToSqlString(sData));
                sTmp = string.Format(" ,CUCU_TelefonFix = '{0}'", CFormatareDate.StringToSqlString(sData));
                sUpdate += sTmp;
            }


            // CUCU_Email
            sData = m_ebEmail.Text;
            if (sData == "")
            {
                sEmail = string.Format("NULL");
                sTmp = string.Format(" ,CUCU_Email = NULL");
                sUpdate += sTmp;
            }
            else
            {
                sEmail = string.Format(" '{0}' ", CFormatareDate.StringToSqlString(sData));
                sTmp = string.Format(" ,CUCU_Email = '{0}'", CFormatareDate.StringToSqlString(sData));
                sUpdate += sTmp;
            }

            // FACEBOOK
            sData = m_ebNumeFB.Text;
            if (sData == "")
            {
                sNumeFB = string.Format("NULL");
                sTmp = string.Format(" ,CUCU_NumeFacebook = NULL");
                sUpdate += sTmp;
            }
            else
            {
                sNumeFB = string.Format(" '{0}' ", CFormatareDate.StringToSqlString(sData));
                sTmp = string.Format(" ,CUCU_NumeFacebook = '{0}'", CFormatareDate.StringToSqlString(sData));
                sUpdate += sTmp;
            }
            // SKYPE
            sData = m_ebNumeSkype.Text;
            if (sData == "")
            {
                sNumeSkype = string.Format("NULL");
                sTmp = string.Format(" ,CUCU_NumeSkype = NULL");
                sUpdate += sTmp;
            }
            else
            {
                sNumeSkype = string.Format(" '{0}' ", CFormatareDate.StringToSqlString(sData));
                sTmp = string.Format(" ,CUCU_NumeSkype = '{0}'", CFormatareDate.StringToSqlString(sData));
                sUpdate += sTmp;
            }


            sData = m_ebNumeTutore.Text.Trim().ToUpper();
            if (sData == "")
            {
                sNumeTutore = string.Format("NULL");
                sTmp = string.Format(" ,CUCU_NumeTutore = NULL");
                sUpdate += sTmp;
                //CFormatareDate.ShowWarning("IDS_ERR_NUMETATA");
                //m_ebNumeTata.Focus();
                //return false;
            }
            else
            {
                sNumeTutore = string.Format(" N'{0}' ", CFormatareDate.StringToSqlString(sData));
                sTmp = string.Format(" ,CUCU_NumeTutore = N'{0}'", CFormatareDate.StringToSqlString(sData));
                sUpdate += sTmp;
            }

            sData = m_ebPrenumeTutore.Text.Trim().ToUpper();
            if (sData == "")
            {
                sPrenumeTutore = string.Format("NULL");
                sTmp = string.Format(" ,CUCU_PrenumeTutore = NULL");
                sUpdate += sTmp;
                //CFormatareDate.ShowWarning("IDS_ERR_PRENUMETATA");
                //m_ebPrenumeTata.Focus();
                //return false;
            }
            else
            {
                sPrenumeTutore = string.Format(" N'{0}' ", CFormatareDate.StringToSqlString(sData));
                sTmp = string.Format(" ,CUCU_PrenumeTutore = N'{0}'", CFormatareDate.StringToSqlString(sData));
                sUpdate += sTmp;
            }


            sData = m_ebEmailTutore.Text;
            if (sData == "")
            {
                sEmailTutore = string.Format("NULL");
                sTmp = string.Format(" ,CUCU_EmailTutore = NULL");
                sUpdate += sTmp;
            }
            else
            {
                sEmailTutore = string.Format(" '{0}' ", CFormatareDate.StringToSqlString(sData));
                sTmp = string.Format(" ,CUCU_EmailTutore = '{0}'", CFormatareDate.StringToSqlString(sData));
                sUpdate += sTmp;
            }

            sData = m_ebCISerieTutore.Text.Trim().ToUpper();
            if (sData == "")
            {
                sCISerieTutore = string.Format("NULL");
                sTmp = string.Format(" ,CUCU_CISerieTutore = NULL");
                sUpdate += sTmp;
            }
            else
            {
                sCISerieTutore = string.Format(" '{0}' ", CFormatareDate.StringToSqlString(sData));
                sTmp = string.Format(" ,CUCU_CISerieTutore = '{0}'", CFormatareDate.StringToSqlString(sData));
                sUpdate += sTmp;
            }

            sData = m_ebCINumarTutore.Text;
            if (sData == "")
            {
                sCINumarTutore = string.Format("NULL");
                sTmp = string.Format(" ,CUCU_CINumarTutore = NULL");
                sUpdate += sTmp;
            }
            else
            {
                sCINumarTutore = string.Format(" '{0}' ", CFormatareDate.StringToSqlString(sData));
                sTmp = string.Format(" ,CUCU_CINumarTutore = '{0}'", CFormatareDate.StringToSqlString(sData));
                sUpdate += sTmp;
            }

            sData = m_ebCNPTutore.Text;
            if (sData == "")
            {
                sCNPTutore = string.Format("NULL");
                sTmp = string.Format(" ,CUCU_CNPTutore = NULL");
                sUpdate += sTmp;
            }
            else
            {
                sCNPTutore = string.Format(" '{0}' ", CFormatareDate.StringToSqlString(sData));
                sTmp = string.Format(" ,CUCU_CNPTutore = '{0}'", CFormatareDate.StringToSqlString(sData));
                sUpdate += sTmp;
            }

            if (!EsteCNPValid(m_ebCNPTutore))
                return false;

            if (!ExistaCNP(m_ebCNPTutore))
                return false;

            // Cetățean străin + nr. document
            bool bStrain = m_cbCetAteanStrain.Checked;
            string sNrDocumentStrain = CFormatareDate.StringToSqlString(m_ebNrDocumentStrain.Text.Trim());
            sUpdate += string.Format(" ,CUCU_CetAteanStrain = {0}", bStrain ? 1 : 0);
            sUpdate += string.Format(" ,CUCU_NrDocumentStrain = {0}",
                string.IsNullOrEmpty(sNrDocumentStrain) ? "NULL" : $"N'{sNrDocumentStrain}'");
            int iTipDoc = m_cbTipDocument.SelectedIndex;
            sUpdate += $" ,CUCU_TipDocument = {(iTipDoc > 0 ? iTipDoc.ToString() : "NULL")}";

            // FIDE ID
            if (int.TryParse(m_ebFideId.Text.Trim(), out int iFideId) && iFideId > 0)
                sUpdate += $" ,CUCU_FideId = {iFideId}";
            else
                sUpdate += " ,CUCU_FideId = NULL";

            if (m_lCUCU == 0)
            {
                CInterogareSQL q = new(ManagerDeProiect.GetConnection());
                sSql = string.Format(@" INSERT INTO CU_Cursanti
                                            (CUCU_MBSO_Id,CUCU_MBDV_Id, CUCU_CNP, CUCU_DataNasterii, CUCU_Nume, CUCU_Prenume,
                                            CUCU_CISerie, CUCU_CINumar, CUCU_Strada, CUCU_NumarStrada, CUCU_Bloc, CUCU_Scara,
                                            CUCU_Apartament,  CUCU_TelefonMobil, CUCU_TelefonFix, CUCU_Email, CUCU_Oras, CUCU_Judet, CUCU_NrMatricol,
                                            CUCU_LocalitateaNasterii,CUCU_JudetulNasterii, CUCU_NumeFacebook,CUCU_NumeSkype,
                                            CUCU_NumeTutore,CUCU_PrenumeTutore,CUCU_TelefonTutore,CUCU_EmailTutore,
                                            CUCU_CISerieTutore, CUCU_CINumarTutore, CUCU_CNPTutore,
                                            CUCU_CetAteanStrain, CUCU_NrDocumentStrain, CUCU_TipDocument,
                                            CUCU_MBUT_Id,CUCU_DataSalvarii)
                                            SELECT {0},{1},{2},{3},{4},{5},
                                            {6},{7},{8},{9},{10},{11},
                                            {12},{13},{14},{15},{16},{17},ISNULL((SELECT MAX(CUCU_NrMatricol)+1 FROM CU_Cursanti),1),
                                            {18},{19},{20},{21},
                                            {22},{23},{24},{25},
                                            {26},{27},{28},
                                            {29},{30},{32},
                                            {31},GETDATE()",
                                            ManagerDeProiect.GetSocietate(), ManagerDeProiect.GetDiviziune(), sCNP, sDataNasterii, sNume, sPrenume,
                                            sCISerie, sCINumar, sStrada, sNumarStrada, sBloc, sScara,
                                            sAp, sTelMobil, sTelFix, sEmail, sOras, sJudet,
                                            sLocalitateaNasterii, sJudetulNasterii, sNumeFB, sNumeSkype,
                                            sNumeTutore, sPrenumeTutore, sTelefonTutore, sEmailTutore,
                                            sCISerieTutore, sCINumarTutore, sCNPTutore,
                                            bStrain ? 1 : 0,
                                            string.IsNullOrEmpty(sNrDocumentStrain) ? "NULL" : $"N'{sNrDocumentStrain}'",
                                            ManagerDeProiect.GetUserId(),
                                            iTipDoc > 0 ? iTipDoc.ToString() : "NULL");


                q.ExecuteStatement(sSql);

                sSql = "SELECT SCOPE_IDENTITY() AS SID";
                m_lCUCU = q.ExecuteToInt(sSql);

                sData = m_ebBarcode.Text;
                if (sData == "")
                {

                    //DACA BARCODE E NULL, IL GENERAM DUPA FORMULA NRCRT_AN_ZI_LUNA_ORA_MIN_SEC
                    SqlCommand cmd;
                    cmd = new SqlCommand("GE_GenerareBarcodeAutomat", ManagerDeProiect.GetConnection())
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@lCUCU", m_lCUCU).Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters.Add("@sBarcode", System.Data.SqlDbType.VarChar, 30).Direction = System.Data.ParameterDirection.Output;
                    cmd.CommandTimeout = 0;
                    cmd.ExecuteNonQuery();

                    string sBarcode = (string)cmd.Parameters["@sBarcode"].Value;
                    if (sBarcode != "")
                    {
                        sTmp = string.Format(" CUCU_Barcode = '{0}' ", CFormatareDate.StringToSqlString(sBarcode));
                    }
                    else
                    {
                        sBarcode = string.Format("NULL");
                        sTmp = string.Format(" CUCU_Barcode = NULL ");
                    }

                    m_ebBarcode.Text = sBarcode;

                    //   sBarcode = string.Format(" '{0}' ", CFormatareDate.StringToSqlString(sBarcode));

                    sSql = string.Format(@"UPDATE CU_Cursanti SET {0} WHERE CUCU_Id = {1}", sTmp, m_lCUCU);

                    q.ExecuteStatement(sSql);
                }

                q.Close();

            }
            else
            {
                sTmp = string.Format(" ,CUCU_MBUT_Id = {0},CUCU_DataSalvarii=GETDATE()", ManagerDeProiect.GetUserId());
                sUpdate += sTmp;

                CInterogareSQL qUpdate = new(ManagerDeProiect.GetConnection());
                sSql = string.Format(@"UPDATE CU_Cursanti SET {0} WHERE CUCU_Id = {1}", sUpdate, m_lCUCU);

                qUpdate.ExecuteStatement(sSql);

                qUpdate.Close();
            }
            /*
                        //ISTORIC
                        sSql = string.Format(@" INSERT INTO CU_CursantiIstoric
                                               (CUCU_MBDV_Id, CUCU_CNP, CUCU_DataNasterii, CUCU_Nume, CUCU_Prenume, CUCU_CISerie,
                                                CUCU_CINumar, CUCU_Strada, CUCU_NumarStrada, CUCU_Bloc, CUCU_Scara, CUCU_Apartament, CUCU_Domiciliu, CUCU_PrenumeMama, CUCU_PrenumeTata,
                                                CUCU_TelefonMobil, CUCU_TelefonFix, CUCU_Email, CUCU_Email2, CUCU_Oras, CUCU_Judet, CUCU_NrMatricol, CUCU_InitialaMama, 
                                                CUCU_InitialaTata, CUCU_MBST_Id, CUCU_LocalitateaNasterii, CUCU_JudetulNasterii, CUCU_MBUT_Id,CUCU_DataModificarii,CUCU_CUCU_Modificat,
                                                CUCU_ActeDosar_CI,CUCU_ActeDosar_CertificatNastere,CUCU_ActeDosar_FemeieCasatorita,CUCU_ActeDosar_CertificatCasatorie,
                                                CUCU_ActeDosar_UltimaDiplomaStudii,CUCU_ActeDosar_AdeverintaMedicala,CUCU_CetateanStrain,
                                                CUCU_DispProgram_Oricand,CUCU_DispProgram_Dimineata,CUCU_DispProgram_Dupamasa,CUCU_DispProgram_Sambata,CUCU_DispProgram_OSaptDimOSaptDupamasa,CUCU_NumeFacebook,CUCU_NumeSkype,CUCU_TelefonMobilInternational,CUCU_DispProgram_Altele,CUCU_AltProgramCursant,CUCU_ParolaApp)
                                            SELECT CUCU_MBDV_Id, CUCU_CNP, CUCU_DataNasterii, CUCU_Nume, CUCU_Prenume, CUCU_CISerie, CUCU_CINumar, CUCU_Strada, 
                                                   CUCU_NumarStrada, CUCU_Bloc, CUCU_Scara, CUCU_Apartament, CUCU_Domiciliu, CUCU_PrenumeMama, CUCU_PrenumeTata, CUCU_TelefonMobil, 
                                                   CUCU_TelefonFix, CUCU_Email, CUCU_Email2, CUCU_Oras, CUCU_Judet, CUCU_NrMatricol, CUCU_InitialaMama, CUCU_InitialaTata, 
                                                   CUCU_MBST_Id, CUCU_LocalitateaNasterii, CUCU_JudetulNasterii, {0},GetDate(),{1},
                                                CUCU_ActeDosar_CI,CUCU_ActeDosar_CertificatNastere,CUCU_ActeDosar_FemeieCasatorita,CUCU_ActeDosar_CertificatCasatorie,
                                                CUCU_ActeDosar_UltimaDiplomaStudii,CUCU_ActeDosar_AdeverintaMedicala,CUCU_CetateanStrain,
                                                CUCU_DispProgram_Oricand,CUCU_DispProgram_Dimineata,CUCU_DispProgram_Dupamasa,CUCU_DispProgram_Sambata,CUCU_DispProgram_OSaptDimOSaptDupamasa,CUCU_NumeFacebook,CUCU_NumeSkype,CUCU_TelefonMobilInternational,CUCU_DispProgram_Altele,CUCU_AltProgramCursant,CUCU_ParolaApp
                                            FROM CU_Cursanti
                                            WHERE CUCU_Id={2}", ManagerDeProiect.GetUserId(), m_lCUCU, m_lCUCU);
                        CInterogareSQL qIstoric = new CInterogareSQL(ManagerDeProiect.GetConnection());
                        qIstoric.ExecuteStatement(sSql);
                        qIstoric.Close();

                        //SALVARE CA SI PARTENER
                        int lADPA = 0, lADPG = 0, lExistaCNPCaSiContClient = 0;
                        sSql = string.Format(@"SELECT ISNULL(ADPA_Id,0),ISNULL(ADPA_ADPG_Id,0)  FROM AD_Parteneri WHERE ADPA_CUCU_Id ={0}", m_lCUCU);
                        {
                            CInterogareSQL q = new CInterogareSQL(ManagerDeProiect.GetConnection());
                            q.Open(sSql);
                            if (q.NextRow())
                            {
                                lADPA = q.GetIntValue(0);
                                lADPG = q.GetIntValue(1);
                            }

                            //RECUPERAM PARTENERUL CLIENT DUPA CNP DACA NU EXISTA DUPA CURSANT(ADPA_CUCU_Id=NULL)!!!!!
                            if (lADPG == 0)
                            {
                                sSql = string.Format(@"SELECT ISNULL(ADPA_Id,0),ISNULL(ADPA_ADPG_Id,0),*  FROM AD_Parteneri WHERE ADPA_CUI = (SELECT CUCU_CNP FROM CU_Cursanti WHERE CUCU_Id={0})
                                                        AND ADPA_MBTC_Id=2", m_lCUCU);
                                {
                                    q.Open(sSql);
                                    if (q.NextRow())
                                    {
                                        lADPA = q.GetIntValue(0);
                                        lADPG = q.GetIntValue(1);

                                        if (lADPG > 0)
                                        {
                                            lExistaCNPCaSiContClient = 1;
                                        }
                                    }
                                }
                            }
                            q.Close();
                        }



                        if (lADPG > 0)
                        {
                            CInterogareSQL qUpdate = new CInterogareSQL(ManagerDeProiect.GetConnection());

                            if (lExistaCNPCaSiContClient > 0)
                            {
                                sSql = string.Format(@"UPDATE AD_ParteneriGenerali SET ADPG_CUI = (SELECT CUCU_CNP FROM CU_Cursanti WHERE CUCU_Id={1}),
                                                                                       ADPG_CUCU_Id = {1}
                                                   WHERE ADPG_Id = {0}", lADPG, m_lCUCU);
                                qUpdate.ExecuteStatement(sSql);

                                sSql = string.Format(@"UPDATE AD_Parteneri SET ADPA_CUI = (SELECT CUCU_CNP FROM CU_Cursanti WHERE CUCU_Id={1}),
                                                                               ADPA_CUCU_Id={1}
                                                   WHERE ADPA_Id = {0} AND ADPA_MBTC_Id=2", lADPA, m_lCUCU);
                                qUpdate.ExecuteStatement(sSql);
                            }

                            sSql = string.Format(@"UPDATE AD_ParteneriGenerali SET ADPG_Denumire= CUCU_Nume+' '+CUCU_Prenume,
                                                                                    ADPG_Strada=CUCU_Strada,
                                                                                    ADPG_Numar=CUCU_NumarStrada,
                                                                                    ADPG_Bloc=CUCU_Bloc,
                                                                                    ADPG_Scara=CUCU_Scara,
                                                                                    ADPG_Apartament=CUCU_Apartament,
                                                                                    ADPG_Localitate=CUCU_Oras,
                                                                                    ADPG_Judet=CUCU_Judet,
                                                                                    ADPG_CUI=CUCU_CNP,
                                                                                    ADPG_Telefon=CUCU_TelefonFix,
                                                                                    ADPG_TelefonMobil=CUCU_TelefonMobil,
                                                                                    ADPG_Email=CUCU_Email,
                                                                                    ADPG_Email2=CUCU_Email2,
                                                                                    ADPG_TelefonMobilInternational=CUCU_TelefonMobilInternational
                                                    FROM CU_Cursanti
                                                    WHERE ADPG_CUCU_Id=CUCU_Id AND ADPG_Id = {0}", lADPG);
                            qUpdate.ExecuteStatement(sSql);

                            sSql = string.Format(@"UPDATE AD_Parteneri SET ADPA_Denumire= CUCU_Nume+' '+CUCU_Prenume,
                                                                            ADPA_Strada=CUCU_Strada,
                                                                            ADPA_Numar=CUCU_NumarStrada,
                                                                            ADPA_Bloc=CUCU_Bloc,
                                                                            ADPA_Scara=CUCU_Scara,
                                                                            ADPA_Apartament=CUCU_Apartament,
                                                                            ADPA_Localitate=CUCU_Oras,
                                                                            ADPA_Judet=CUCU_Judet,
                                                                            ADPA_CUI=CUCU_CNP,
                                                                            ADPA_Telefon=CUCU_TelefonFix,
                                                                            ADPA_TelefonMobil=CUCU_TelefonMobil,
                                                                            ADPA_Email=CUCU_Email,
                                                                            ADPA_Email2=CUCU_Email2
                                                    FROM CU_Cursanti 
                                                    WHERE ADPA_CUCU_Id=CUCU_Id AND ADPA_ADPG_Id = {0}", lADPG);

                            qUpdate.ExecuteStatement(sSql);

                            qUpdate.Close();
                        }
                        else
                        {
                            lADPG = 0;
                            CInterogareSQL qIns = new CInterogareSQL(ManagerDeProiect.GetConnection());
                            sSql = string.Format(@"INSERT INTO AD_ParteneriGenerali (ADPG_MBDV_Id,ADPG_MBTA_Id,ADPG_MBTP_Id,ADPG_CUCU_Id,
                                                                             ADPG_Denumire,ADPG_Strada,ADPG_Numar,ADPG_Bloc,ADPG_Scara,ADPG_Apartament,ADPG_Localitate,ADPG_Judet,
                                                                             ADPG_CUI,ADPG_Telefon,ADPG_TelefonMobil,ADPG_Email,ADPG_Email2,ADPG_MBUT_Id,ADPG_DataSalvarii,ADPG_MBCP_Id,ADPG_TelefonMobilInternational)
                                                    SELECT CUCU_MBDV_Id,1,2,{0},
                                                    CUCU_Nume+' '+CUCU_Prenume,CUCU_Strada,CUCU_NumarStrada,CUCU_Bloc,CUCU_Scara,CUCU_Apartament,CUCU_Oras,CUCU_Judet,CUCU_CNP,CUCU_TelefonFix,
                                                    CUCU_TelefonMobil,CUCU_Email,CUCU_Email2,{1},GETDATE(),15,CUCU_TelefonMobilInternational
                                                    FROM CU_Cursanti 
                                                    WHERE CUCU_Id = {2}", m_lCUCU, ManagerDeProiect.GetUserId(), m_lCUCU);

                            qIns.ExecuteStatement(sSql);

                            sSql = "SELECT SCOPE_IDENTITY() AS SID";
                            lADPG = qIns.ExecuteToInt(sSql);

                            sSql = string.Format(@"INSERT INTO AD_Parteneri (ADPA_MBDV_Id,ADPA_MBTA_Id,ADPA_MBTC_Id,ADPA_MBTP_Id,ADPA_CUCU_Id,
                                                                             ADPA_Denumire,ADPA_Strada,ADPA_Numar,ADPA_Bloc,ADPA_Scara,ADPA_Apartament,ADPA_Localitate,ADPA_Judet,ADPA_CUI,
                                                                             ADPA_Telefon,ADPA_TelefonMobil,ADPA_Email,ADPA_Email2,ADPA_ADPG_Id,ADPA_MBUT_Id,ADPA_DataSalvarii,ADPA_MBPS_Id)
                                                    SELECT CUCU_MBDV_Id,1,2,2,{0},
                                                    CUCU_Nume+' '+CUCU_Prenume,CUCU_Strada,CUCU_NumarStrada,CUCU_Bloc,CUCU_Scara,CUCU_Apartament,CUCU_Oras,CUCU_Judet,CUCU_CNP,CUCU_TelefonFix,
                                                    CUCU_TelefonMobil,CUCU_Email,CUCU_Email2,{1},{2},GETDATE(),2
                                                    FROM CU_Cursanti 
                                                    WHERE CUCU_Id = {2}", m_lCUCU, lADPG, m_lCUCU, ManagerDeProiect.GetUserId());

                            qIns.ExecuteStatement(sSql);

                            qIns.Close();
                        }*/

            return true;
        }

        private bool EsteCNPValid(TextBox tboxCNP, DateTimePicker? dtpData = null)
        {

            //TEST SE IL CODICE NUMERICO PERSONALE E VALIDO

            /*IL ALGORITMO DI VALIDITA DI UN CODICE NUMERICO PERSONALE

            STEP 1:SI VERIFICA SE LA STRINGA DI CNP HA 13 CARRATERI 
                   SI VERIFICA SE PRIMA CIFRA E TRA 1-6 O E 9 PER GLI STRANIERI.
                   LE PROSSIME CIFRE DEVONO RIPRESENTARE UNA DATA CALENDARISTICA VALIDA IN FORMATO AAMMGG (ANNO, MESE, GIORNO)

            STEP 2: SI USA LA CHIAVE DI TEST:"279146358279". 
                    LE PRIME DODICI CIFRE DA CNP SI MOLTIPLICANO A VOLTA DA SINISTRA A DESTRA CON LA CIFRA CORISPONDENTE DALLA CHIAVE DI TEST

            STEP 3: SI ADUNANO TUTTI I 12 PRODOTTI E LA SOMMA OTENUTA SI DIVIDE CON 11

                *  SE IL RESTO DELLA DIVISIONE A 11 E MINORE DI 10 => QUESTO RESTO RIPRESENTA LA CIFRA DI CONTROLLO
                *  SE IL RESTO DELLA DIVISIONE A 11 E UGUALE CON 10=> CIFRA DI CONTROLLO E 1

            PER UN CNP VALIDO, LA CIFRA DI CONTROLLO DEVE COINCIDERE CON L'ULTIMA CIFRA DA CNP (LA TREDICESIMA CIFRA DA CNP)
            */

            string sCnp = tboxCNP.Text;
            string sAn, sLuna, sZi, sSex;

            int lCNP = sCnp.Length;

            if (lCNP == 0)
                return true;

            if (lCNP != 13)
            {
                CFormatareDate.ShowWarning("IDS_ERR_CNP_LUNGIME");
                tboxCNP.Focus();
                return false;
            }

            string sTest = "279146358279";//CUVANTUL DI TEST

            int lSuma = 0;

            for (int i = 0; i < sTest.Length; i++)
            {
                //   sCnp.
                //   lSuma += atol(string(sCnp.Substring(i,1))) * atol(string(sTest.Substring(i,1)));//SUMA
                lSuma += Convert.ToInt32(sCnp.Substring(i, 1)) * Convert.ToInt32(sTest.Substring(i, 1));//SUMA
            }

            int lRest = lSuma % 11;//RESTUL IMPARTIRII CU 11

            if (!((lRest < 10 && lRest == Convert.ToInt32(sCnp.Substring(12, 1)) || (lRest == 10 && Convert.ToInt32(sCnp.Substring(12, 1)) == 1))))//TEST RESTUL IMPARTIRII CU 11
            {
                CFormatareDate.ShowWarning("IDS_ERR_CNP_VALID");
                tboxCNP.Focus();
                return false;
            }

            //PROPONE AUTOMATICO LA DATA DI NASCITA

            //SESSO E RIPRESENTATO DALLA PRIMA CIFRA DA CNP (MASCHIO/FEMINE)

            // 1 / 2 - NATO TRA 1.01.1900 e 31.12.1999 
            // 3 / 4 - NATO TRA 1.01.1800 e 31.12.1899
            // 5 / 6 - NATO TRA 1.01.2000 e 31.12.2099
            // 7 / 8 - PER LE PERSONE STRANIERE CHE HANNO IL DOMICILIO IN ROMANIA
            // 9	 - PER LE PERSONE STRANIERE

            sSex = Convert.ToString(sCnp[0]);

            if (!(sSex == "1" || sSex == "2" || sSex == "3" || sSex == "4" || sSex == "5" || sSex == "6" || sSex == "7" || sSex == "8" || sSex == "9"))
            {
                CFormatareDate.ShowWarning("IDS_ERR_CNP_PRIMACIFRA");
                tboxCNP.Focus();
                return false;
            }

            //ANNO
            sAn = Convert.ToString(sCnp.Substring(1, 1)) + Convert.ToString(sCnp.Substring(2, 1));

            //MESE
            sLuna = Convert.ToString(sCnp.Substring(3, 1)) + Convert.ToString(sCnp.Substring(4, 1));
            if (Convert.ToInt32(sLuna) > 12)
            {
                CFormatareDate.ShowWarning("IDS_ERR_CNP_CIFRELUNA");
                tboxCNP.Focus();
                return false;
            }

            //GIORNO
            sZi = Convert.ToString(sCnp.Substring(5, 1)) + Convert.ToString(sCnp.Substring(6, 1));
            if (Convert.ToInt32(sZi) > 31)
            {
                CFormatareDate.ShowWarning("IDS_ERR_CNP_CIFREZI");
                tboxCNP.Focus();
                return false;
            }

            if (sSex == "1" || sSex == "2" || sSex == "7" || sSex == "8")
                sAn = "19" + sAn;
            else
                if (sSex == "5" || sSex == "6")
                    sAn = "20" + sAn;



            string sData = sLuna + "/" + sZi + "/" + sAn;
            // m_dtDataNasterii.Value = DateTime.ParseExact(sData, @"yyyy-MM-dd", System.Threading.Thread.CurrentThread.CurrentCulture);
            dtpData?.Value = DateTime.ParseExact(sData, "d", CultureInfo.InvariantCulture);
            return true;
        }

        private bool ExistaCNP(TextBox tBoxCNP)
        {
            string sCNP = tBoxCNP.Text;

            int lCNP = sCNP.Length;

            if (lCNP == 0)
                return true;

            //TEST DACA MAI EXISTA CNP IN BAZA DE DATE



            if (m_lCUCU > 0)//UPDATE
            {
                sSql = string.Format(@" SELECT CUCU_Id FROM CU_Cursanti WHERE CUCU_CNP='{0}' AND CUCU_Id<>{1}", sCNP, m_lCUCU);
            }
            else//INSERT
            {
                sSql = string.Format(@" SELECT CUCU_Id FROM CU_Cursanti WHERE CUCU_CNP='{0}'", sCNP);
            }
            CInterogareSQL qCNP = new(ManagerDeProiect.GetConnection());
            qCNP.Open(sSql);
            if (qCNP.NextRow())
            {
                CFormatareDate.ShowWarning("IDS_ERR_CNPEXISTA");
                tBoxCNP.Focus();
                return false;
            }
            qCNP.Close();

            return true;
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }
    }
}

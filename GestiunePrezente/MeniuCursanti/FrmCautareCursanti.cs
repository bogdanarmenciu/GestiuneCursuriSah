using GestiunePrezente.Common;
using GestiunePrezente.MeniuPrincipal;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Diagnostics;
using static QuestPDF.Helpers.Colors;

namespace GestiunePrezente.MeniuCursanti
{
    public partial class FrmCautareCursanti : Form
    {
        readonly CManagerDeProiect ManagerDeProiect;
        readonly FrmMeniuPrincipal FrmParent;
        string? m_sSql;
        readonly int m_lMBME = 2;
        public FrmCautareCursanti(FrmMeniuPrincipal FormParent, CManagerDeProiect MM)
        {
            FrmParent = FormParent;
            ManagerDeProiect = MM;
            InitializeComponent();
            GridPersonalizat.CustomGrid.SetConnection(ManagerDeProiect.GetConnection());
            m_ckActiv.Checked = true;

            //if (ManagerDeProiect.GetRolUser_Administrator() != 1)
            //    m_tsbSterge.Enabled = false;
        }

        private void m_ckActiv_CheckedChanged(object sender, EventArgs e)
        {
            Cauta();
        }

        private void m_ckFaraContract_CheckedChanged(object sender, EventArgs e)
        {
            Cauta();
        }

        private void m_ebNume_TextChanged(object sender, EventArgs e)
        {
            Cauta();
        }

        private void m_ebPrenume_TextChanged(object sender, EventArgs e)
        {
            Cauta();
        }

        private void m_ebCNP_TextChanged(object sender, EventArgs e)
        {
            Cauta();
        }

        private void m_ebTelMobil_TextChanged(object sender, EventArgs e)
        {
            Cauta();
        }

        private void m_ebEmail_TextChanged(object sender, EventArgs e)
        {
            Cauta();
        }

        private void m_tsbCautare_Click(object sender, EventArgs e)
        {
            Cauta();
        }

        private void m_gCursanti_DoubleClick(object sender, EventArgs e)
        {
            Modifica();
        }
        private void m_tsbNou_Click(object sender, EventArgs e)
        {
            if (!FrmGestiuneCursant.PoateDeschide(ManagerDeProiect)) return;
            FrmGestiuneCursant frm = new(FrmParent, ManagerDeProiect, 0, m_lMBME)
            {
                MdiParent = FrmParent
            };
            frm.Show();
        }

        private void m_tsbInscriereRapida_Click(object sender, EventArgs e)
        {
            var frm = new FrmInscriereCopil(FrmParent, ManagerDeProiect)
            { MdiParent = FrmParent };
            frm.Show();
        }
        private void m_tsbSterge_Click(object sender, EventArgs e)
        {
            int lCUCU;
            object? value = m_gCursanti.GetColumnData("CUCU_Id");
            if (value == null) return;

            lCUCU = Convert.ToInt32(value);
            if (MessageBox.Show(CFormatareDate.GetString("IDS_CURSANT_ASK_DELETE"), CFormatareDate.GetString("IDS_ATENTIE"), MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            CInterogareSQL q = new(ManagerDeProiect.GetConnection());
            int lFolosit;

            m_sSql = string.Format(@"SELECT COUNT(*)
                                    FROM CU_ContracteCursanti
                                    WHERE CUCC_CUCU_Id = {0}", lCUCU);
            lFolosit = q.ExecuteToInt(m_sSql);

            if (lFolosit > 0)
            {
                CFormatareDate.ShowWarning("IDS_CURSANT_FOLOSIT_IN_CONTRACTE");
                return;
            }

            m_sSql = string.Format(@"DELETE FROM CU_Cursanti WHERE CUCU_Id={0}", lCUCU);
            q.ExecuteStatement(m_sSql);
            q.Close();

            Cauta();

        }

        private void m_tsbListeazaBarcode_Click(object sender, EventArgs e)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var listaCursanti = new List<(string Nume, string Prenume, string Barcode)>();

            // Collect selected CUCU_Id values (integers — safe to use in IN clause directly)
            var selectedIds = new List<int>();
            foreach (DataGridViewRow row in m_gCursanti.SelectedRows)
            {
                if (row.IsNewRow) continue;
                object? val = row.Cells["CUCU_Id"].Value;
                if (val != null && val != DBNull.Value)
                    selectedIds.Add(Convert.ToInt32(val));
            }

            CInterogareSQL q = new(ManagerDeProiect.GetConnection());

            if (selectedIds.Count > 0)
                m_sSql = $@"SELECT CUCU_Nume, CUCU_Prenume, CUCU_Barcode
                            FROM CU_Cursanti
                            WHERE CUCU_Id IN ({string.Join(",", selectedIds)})
                            ORDER BY CUCU_Nume, CUCU_Prenume";
            else
                m_sSql = @"SELECT CUCU_Nume, CUCU_Prenume, CUCU_Barcode
                           FROM CU_Cursanti
                           ORDER BY CUCU_Nume, CUCU_Prenume";

            q.Open(m_sSql);
            while (q.NextRow())
            {
                listaCursanti.Add((
                    q.GetStringValue(0)!,
                    q.GetStringValue(1)!,
                    q.GetStringValue(2)!
                ));
            }
            q.Close();


            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Etichete_Cursanti.pdf");
            try
            {
                QuestPDF.Fluent.Document.Create(container =>
                {
                    for (int i = 0; i < listaCursanti.Count; i += 14)
                    {
                        var grupCurent = listaCursanti.Skip(i).Take(14).ToList();
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

                                foreach (var (Nume, Prenume, Barcode) in grupCurent)
                                {
                                    table.Cell().Padding(10).Border(0.5f).Column(col =>
                                    {
                                        col.Spacing(5);
                                        col.Item().AlignCenter().Text($"{Nume} {Prenume}").SemiBold();
                                        col.Item().Height(60).AlignCenter().BarcodeCode128(Barcode);
                                        col.Item().AlignCenter().Text(Barcode).FontSize(8);
                                    });
                                }
                            });
                        });
                    }
                }).GeneratePdf(filePath);
                Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la generare: {ex.Message}");
            }
        }

        private void Cauta()
        {
            object? value;
            string sData, sTmp, sFiltru = "";


            sData = m_ebNume.Text;
            if (sData != "")
            {
                sTmp = string.Format(" AND CUCU_Nume LIKE N'{0}%' ", CFormatareDate.StringToSqlString(sData));
                sFiltru += sTmp;
            }

            sData = m_ebPrenume.Text;
            if (sData != "")
            {
                sTmp = string.Format(" AND CUCU_Prenume LIKE N'{0}%' ", CFormatareDate.StringToSqlString(sData));
                sFiltru += sTmp;
            }

            sData = m_ebCNP.Text;
            if (sData != "")
            {
                sTmp = string.Format(" AND CUCU_CNP LIKE '%{0}%' ", CFormatareDate.StringToSqlString(sData));
                sFiltru += sTmp;
            }

            sData = m_ebTelMobil.Text;
            if (sData != "")
            {
                sTmp = string.Format(" AND CUCU_TelefonMobil LIKE '{0}%' ", CFormatareDate.StringToSqlString(sData));
                sFiltru += sTmp;
            }
            sData = m_ebEmail.Text;
            if (sData != "")
            {
                sTmp = string.Format(" AND CUCU_Email LIKE '{0}%' ", CFormatareDate.StringToSqlString(sData));
                sFiltru += sTmp;
            }

            if (m_ckActiv.Checked)
            {
                sFiltru += @" AND (
                    ISNULL(CUCU_FaraContract, 0) = 1
                    OR CUCU_Id IN (
                        SELECT CUCC_CUCU_Id
                          FROM CU_ContracteCursanti
                         INNER JOIN CU_Contracte ON CUCO_Id = CUCC_CUCO_Id
                         INNER JOIN CU_AbonamenteCursant ON CUAC_CUCO_Id = CUCC_CUCO_Id
                         WHERE ISNULL(CUCO_Retras, 0) = 0
                           AND ISNULL(CUCC_Retras, 0) = 0
                           AND CUAC_DataInceput <= CAST(GETDATE() AS DATE)
                           AND (CUAC_DataSfarsit IS NULL OR CUAC_DataSfarsit >= CAST(GETDATE() AS DATE))
                    )) ";
            }

            if (m_ckFaraContract.Checked)
                sFiltru += " AND ISNULL(CUCU_FaraContract, 0) = 1 ";

            m_sSql = string.Format(@"SELECT ROW_NUMBER() OVER (ORDER BY CUCU_Nume, CUCU_Prenume) AS Row,
                                            CASE WHEN EXISTS (
                                                SELECT 1 FROM AD_Arhiva
                                                WHERE ADAH_CUCU_Id = CUCU_Id
                                                   OR ADAH_CUCO_Id IN (SELECT CUCC_CUCO_Id FROM CU_ContracteCursanti WHERE CUCC_CUCU_Id = CUCU_Id)
                                                   OR ADAH_ADRN_Id IN (SELECT ADRN_Id FROM AD_RegistruNumereInterne
                                                                        WHERE ADRN_CUCO_Id IN (SELECT CUCC_CUCO_Id FROM CU_ContracteCursanti WHERE CUCC_CUCU_Id = CUCU_Id))
                                            ) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS [Documente arhiva],
                                            ISNULL(CUCU_FaraContract, 0) AS [Fara contract],
                                            CUCU_NrMatricol,CUCU_Id, CUCU_Nume, CUCU_Prenume, CUCU_CNP, CUCU_TelefonMobil,
                                            CUCU_TelefonTutore AS [Telefon mobil Reprezentant Legal],
                                            ISNULL(CUCU_NumeTutore,'') + CASE WHEN ISNULL(CUCU_PrenumeTutore,'') <> '' THEN ' ' + CUCU_PrenumeTutore ELSE '' END AS [Nume Prenume Reprezentant Legal],
                                            CUCU_Email,
                                            STUFF(
                                                CASE WHEN ISNULL(CUCU_CINumar,'')        = '' THEN ', Numar CI'               ELSE '' END +
                                                CASE WHEN ISNULL(CUCU_EmailTutore,'')    = '' THEN ', Email Reprez Legal'     ELSE '' END +
                                                CASE WHEN ISNULL(CUCU_CINumarTutore,'') = '' THEN ', Numar CI Reprez Legal'  ELSE '' END,
                                            1, 2, '') AS [Documente Lipsa],
                                            MBDV_Diviziune AS Oras,CUCU_NumeFacebook,CUCU_NumeSkype
                                            FROM CU_Cursanti C
                                    INNER JOIN MB_Diviziune ON CUCU_MBDV_Id=MBDV_Id
                                    LEFT JOIN CU_Formatori ON CUCU_CUFO_Id=CUFO_Id
                                    WHERE 1=1 {0}
                                    ORDER BY CUCU_Nume, CUCU_Prenume", sFiltru);

            m_gCursanti.SuspendLayout();
            m_gCursanti.Visible = false;

            m_gCursanti.SetSql(m_sSql);

            m_gCursanti.FormatColumn("Nr. crt", 50, null, DataGridViewContentAlignment.MiddleRight);
            m_gCursanti.FormatColumn("Documente arhiva", 65);
            m_gCursanti.FormatColumn("Fara contract", 80);
            m_gCursanti.FormatColumn("Nr.Matricol", 70);
            m_gCursanti.FormatColumn("CUCU_Id", 70, false);
            m_gCursanti.FormatColumn("Nume", 150);
            m_gCursanti.FormatColumn("Prenume", 150);
            m_gCursanti.FormatColumn("CNP", 100);
            m_gCursanti.FormatColumn("Telefon Mobil", 100);
            m_gCursanti.FormatColumn("Telefon mobil Reprezentant Legal", 180);
            m_gCursanti.FormatColumn("Nume Prenume Reprezentant Legal", 200);
            m_gCursanti.FormatColumn("Email", 150);
            m_gCursanti.FormatColumn("Documente Lipsa", 300);
            m_gCursanti.FormatColumn("Oras", 100);
            m_gCursanti.FormatColumn("Facebook", 150);
            m_gCursanti.FormatColumn("Skype", 150);
            m_gCursanti.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (DataGridViewColumn column in m_gCursanti.Columns)
            {
                m_gCursanti.Columns[column.Name]!.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            m_gCursanti.ResumeLayout();
            m_gCursanti.Visible = true;
        }

        private void Modifica()
        {
            int lCUCU;
            object? value = m_gCursanti.GetColumnData("CUCU_Id");
            if (value != null)
            {
                lCUCU = Convert.ToInt32(value);
                if (!FrmGestiuneCursant.PoateDeschide(ManagerDeProiect)) return;
                FrmGestiuneCursant frm = new(FrmParent, ManagerDeProiect, lCUCU, m_lMBME)
                {
                    MdiParent = FrmParent
                };
                frm.Show();
            }
        }
    }
}

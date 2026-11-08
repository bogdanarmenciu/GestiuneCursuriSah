using GestiunePrezente.Common;
using Microsoft.Data.SqlClient;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using PdfPageSizes = QuestPDF.Helpers.PageSizes;
using PdfUnit = QuestPDF.Infrastructure.Unit;
using PdfLicenseType = QuestPDF.Infrastructure.LicenseType;
using System.Data;
using System.Diagnostics;

namespace GestiunePrezente.MeniuCursanti
{
    public partial class FrmCursantRapidPrezenta : Form
    {
        readonly CManagerDeProiect ManagerDeProiect;
        readonly int m_lCUPR;

        public FrmCursantRapidPrezenta(CManagerDeProiect mm, int lCUPR)
        {
            ManagerDeProiect = mm;
            m_lCUPR          = lCUPR;
            InitializeComponent();
        }

        private void m_btnSalveaza_Click(object sender, EventArgs e) => Salveaza();

        private void m_btnAnuleaza_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Salveaza()
        {
            string sNume    = m_ebNume.Text.Trim().ToUpper();
            string sPrenume = m_ebPrenume.Text.Trim().ToUpper();

            if (string.IsNullOrWhiteSpace(sNume))
            {
                MessageBox.Show("Completați numele cursantului.", "Validare",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                m_ebNume.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(sPrenume))
            {
                MessageBox.Show("Completați prenumele cursantului.", "Validare",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                m_ebPrenume.Focus();
                return;
            }

            m_btnSalveaza.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;

            var q  = new CInterogareSQL(ManagerDeProiect.GetConnection());
            var tx = q.BeginTransaction(IsolationLevel.Serializable);
            int    lCUCU   = 0;
            string sBarcode = "";

            try
            {
                // ---- 1. Număr matricol următor ----
                int nrMatricol = q.ExecuteToIntP(
                    "SELECT ISNULL(MAX(CUCU_NrMatricol)+1, 1) FROM CU_Cursanti;");

                // ---- 2. INSERT cursant minimal ----
                lCUCU = q.ExecuteToIntP(@"
INSERT INTO CU_Cursanti
    (CUCU_MBSO_Id, CUCU_MBDV_Id, CUCU_Nume, CUCU_Prenume,
     CUCU_NrMatricol, CUCU_MBUT_Id, CUCU_DataSalvarii, CUCU_FaraContract,
     CUCU_DataNasterii)
OUTPUT INSERTED.CUCU_Id
VALUES (@so, @dv, @nume, @prenume, @nrm, @u, GETDATE(), 1, '1900-01-01')",
                    CInterogareSQL.P("@so",      ManagerDeProiect.GetSocietate()),
                    CInterogareSQL.P("@dv",      ManagerDeProiect.GetDiviziune()),
                    CInterogareSQL.P("@nume",    sNume),
                    CInterogareSQL.P("@prenume", sPrenume),
                    CInterogareSQL.P("@nrm",     nrMatricol),
                    CInterogareSQL.P("@u",       ManagerDeProiect.GetUserId()));

                if (lCUCU == 0)
                {
                    q.RollbackTransaction();
                    MessageBox.Show("Cursantul nu s-a putut salva. Verificați erorile.",
                        "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ---- 3. Generare barcode prin SP ----
                using (var cmdBarcode = new SqlCommand("GE_GenerareBarcodeAutomat",
                    ManagerDeProiect.GetConnection(), tx)
                {
                    CommandType    = CommandType.StoredProcedure,
                    CommandTimeout = 0
                })
                {
                    cmdBarcode.Parameters.AddWithValue("@lCUCU", lCUCU).Direction
                        = ParameterDirection.Input;
                    cmdBarcode.Parameters.Add("@sBarcode", SqlDbType.VarChar, 30).Direction
                        = ParameterDirection.Output;
                    cmdBarcode.ExecuteNonQuery();
                    sBarcode = cmdBarcode.Parameters["@sBarcode"].Value as string ?? "";
                }

                if (string.IsNullOrWhiteSpace(sBarcode))
                {
                    q.RollbackTransaction();
                    MessageBox.Show(
                        "Barcode-ul nu a putut fi generat (GE_GenerareBarcodeAutomat).\nCursantul NU a fost salvat.",
                        "Eroare barcode", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ---- 4. Persistă barcode-ul pe cursant ----
                q.ExecuteNonQueryP(
                    "UPDATE CU_Cursanti SET CUCU_Barcode = @b WHERE CUCU_Id = @c;",
                    CInterogareSQL.P("@b", sBarcode),
                    CInterogareSQL.P("@c", lCUCU));

                // ---- 5. Adaugă cursantul în ședința curentă (fără contract) ----
                q.ExecuteNonQueryP(@"
INSERT INTO CU_PrezenteDetaliu
    (CUPD_CUPR_Id, CUPD_CUCO_Id, CUPD_CUCU_Id, CUPD_Prezent, CUPD_DataIntrare)
VALUES (@pr, NULL, @cu, 1, GETDATE())",
                    CInterogareSQL.P("@pr", m_lCUPR),
                    CInterogareSQL.P("@cu", lCUCU));

                q.CommitTransaction();

                // ---- 6. Generează și deschide PDF barcode ----
                GenereazaBarcodePdf(sNume, sPrenume, sBarcode);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                try { q.RollbackTransaction(); } catch { }
                MessageBox.Show($"Eroare la salvare: {ex.Message}",
                    "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current        = Cursors.Default;
                m_btnSalveaza.Enabled = true;
            }
        }

        private static void GenereazaBarcodePdf(string sNume, string sPrenume, string sBarcode)
        {
            QuestPDF.Settings.License = PdfLicenseType.Community;

            string sNumeComplet = $"{sNume} {sPrenume}";
            string filePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "Eticheta_CursantRapid.pdf");

            try
            {
                Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PdfPageSizes.A4);
                        page.Margin(1, PdfUnit.Centimetre);
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
                                col.Item().AlignCenter().Text(sNumeComplet).SemiBold();
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
                MessageBox.Show($"Eroare la generare PDF barcode: {ex.Message}");
            }
        }
    }
}

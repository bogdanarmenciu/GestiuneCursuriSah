using GestiunePrezente.Common;
using GestiunePrezente.MeniuPrincipal;
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
    public partial class FrmInscriereCopil : Form
    {
        readonly CManagerDeProiect ManagerDeProiect;
        readonly FrmMeniuPrincipal FrmParent;

        public FrmInscriereCopil(FrmMeniuPrincipal formParent, CManagerDeProiect mm)
        {
            FrmParent = formParent;
            ManagerDeProiect = mm;
            InitializeComponent();
        }

        private void FrmInscriereCopil_Load(object sender, EventArgs e)
        {
            GridPersonalizat.CustomCombo.SetConnection(ManagerDeProiect.GetConnection());
            m_cbAbonament.SetSql("SELECT CUAB_Id, CUAB_Descriere FROM CU_Abonamente ORDER BY CUAB_Descriere");
            m_dtDataNasterii.Value = DateTime.Today.AddYears(-10);
            m_dtDataInceput.Value  = DateTime.Today;
            ActualizeazaVizibilitateTutore();
        }

        // =========================================================
        //   Minor detection
        // =========================================================
        private void m_dtDataNasterii_ValueChanged(object sender, EventArgs e) => ActualizeazaVizibilitateTutore();
        private bool EsteMinor() => m_dtDataNasterii.Value > DateTime.Today.AddYears(-18);

        private void ActualizeazaVizibilitateTutore()
        {
            bool minor = EsteMinor();
            panelTutore.Visible = minor;
            ClientSize = new Size(ClientSize.Width, minor ? 460 : 340);
        }

        // =========================================================
        //   Auto-fill preț + dată sfârșit
        // =========================================================
        private void m_dtDataInceput_ValueChanged(object sender, EventArgs e) => RecalculeazaContract();
        private void m_cbAbonament_SelectedIndexChanged(object sender, EventArgs e) => RecalculeazaContract();

        private void RecalculeazaContract()
        {
            object? idVal = m_cbAbonament.GetColumnData("CUAB_Id");
            if (idVal == null || idVal == DBNull.Value) return;

            string sData = string.Format(" {0} ", CFormatareDate.DateToSQLString(m_dtDataInceput.Value));
            string sql = string.Format(@"
SELECT ISNULL(
    (SELECT TOP 1 CUABP_Pret FROM CU_PreturiAbonamente
     WHERE CUABP_CUAB_Id = CUAB_Id
       AND CUABP_DataStart <= {0}
       AND (CUABP_DataSfarsit IS NULL OR CUABP_DataSfarsit >= {0})
     ORDER BY CUABP_DataStart DESC),
    0
) AS PretEfectiv,
       CASE CUAB_MBTD_Id
           WHEN 1 THEN DATEADD(day, -1, DATEADD(hour,  CUAB_Perioada, {0}))
           WHEN 2 THEN DATEADD(day, -1, DATEADD(day,   CUAB_Perioada, {0}))
           WHEN 3 THEN DATEADD(day, -1, DATEADD(week,  CUAB_Perioada, {0}))
           WHEN 4 THEN EOMONTH(DATEADD(month, CUAB_Perioada - 1, {0}))
           WHEN 5 THEN DATEADD(day, -1, DATEADD(year,  CUAB_Perioada, {0}))
           ELSE NULL
       END AS DataSfarsit
  FROM CU_Abonamente WHERE CUAB_Id = {1}", sData, Convert.ToInt32(idVal));

            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            q.Open(sql);
            if (q.NextRow())
            {
                m_ebPret.Text = CFormatareDate.DecimalToString(q.GetDecimalValue(0)!.Value);
                var sf = q.GetDateValue(1);
                if (sf.HasValue) m_dtDataSfarsit.Value = sf.Value;
            }
            q.Close();
        }

        // =========================================================
        //   Validare
        // =========================================================
        private bool Valideaza()
        {
            if (string.IsNullOrWhiteSpace(m_ebPrenume.Text))
            { MessageBox.Show("Completați prenumele cursantului."); m_ebPrenume.Focus(); return false; }

            if (string.IsNullOrWhiteSpace(m_ebNume.Text))
            { MessageBox.Show("Completați numele cursantului."); m_ebNume.Focus(); return false; }

            if (string.IsNullOrWhiteSpace(m_ebCNP.Text))
            { MessageBox.Show("Completați CNP-ul cursantului."); m_ebCNP.Focus(); return false; }

            if (m_ebCNP.Text.Trim().Length != 13 || !m_ebCNP.Text.Trim().All(char.IsDigit))
            { MessageBox.Show("CNP-ul cursantului trebuie să aibă exact 13 cifre."); m_ebCNP.Focus(); return false; }

            if (EsteMinor())
            {
                if (string.IsNullOrWhiteSpace(m_ebPrenumeTutore.Text) || string.IsNullOrWhiteSpace(m_ebNumeTutore.Text))
                { MessageBox.Show("Completați numele tutorelui legal (cursant minor)."); m_ebPrenumeTutore.Focus(); return false; }
                if (string.IsNullOrWhiteSpace(m_ebCNPTutore.Text))
                { MessageBox.Show("Completați CNP-ul tutorelui legal."); m_ebCNPTutore.Focus(); return false; }
                if (m_ebCNPTutore.Text.Trim().Length != 13 || !m_ebCNPTutore.Text.Trim().All(char.IsDigit))
                { MessageBox.Show("CNP-ul tutorelui trebuie să aibă exact 13 cifre."); m_ebCNPTutore.Focus(); return false; }
            }

            object? ab = m_cbAbonament.GetColumnData("CUAB_Id");
            if (ab == null || ab == DBNull.Value)
            { MessageBox.Show("Selectați tipul de abonament."); m_cbAbonament.Focus(); return false; }

            if (!decimal.TryParse(m_ebPret.Text.Replace(",", "."),
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture, out _))
            { MessageBox.Show("Prețul abonamentului nu a fost calculat. Selectați abonamentul și data de început."); m_cbAbonament.Focus(); return false; }

            return true;
        }

        // =========================================================
        //   Toolbar handlers
        // =========================================================
        private void m_tsbInscriere_Click(object sender, EventArgs e) => Inscrie();

        private void m_tsbListeazaBarcode_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(m_ebBarcode.Text))
            { MessageBox.Show("Nu există barcode generat. Inscrie cursantul mai întâi."); return; }

            QuestPDF.Settings.License = PdfLicenseType.Community;

            string sBarcode     = m_ebBarcode.Text;
            string sNumeComplet = $"{m_ebNume.Text} {m_ebPrenume.Text}";
            string filePath     = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Eticheta_Cursant.pdf");

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

        // =========================================================
        //   Înscrie — tranzacție unică pentru tot: cursant + barcode + contract
        //   Dacă orice pas eșuează → rollback total, nimic nu se salvează
        // =========================================================
        private void Inscrie()
        {
            if (!Valideaza()) return;

            object? abId = m_cbAbonament.GetColumnData("CUAB_Id");
            decimal.TryParse(m_ebPret.Text.Replace(",", "."),
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out decimal pret);

            m_tsbInscriere.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;

            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            var tx = q.BeginTransaction(System.Data.IsolationLevel.Serializable);
            int lCUCU = 0, lCUCO = 0, nrContract = 0;
            string sBarcode = "";

            try
            {
                // ---- 1. Verificare CNP duplicat ----
                int cnt = q.ExecuteToIntP(
                    "SELECT COUNT(*) FROM CU_Cursanti WHERE CUCU_CNP = @cnp;",
                    CInterogareSQL.P("@cnp", m_ebCNP.Text.Trim()));
                if (cnt > 0)
                {
                    q.RollbackTransaction();
                    MessageBox.Show("Există deja un cursant cu acest CNP în baza de date.");
                    return;
                }

                // ---- 2. INSERT cursant — OUTPUT INSERTED returnează ID-ul direct, fără SCOPE_IDENTITY cross-batch ----
                int nrMatricol = q.ExecuteToIntP(
                    "SELECT ISNULL(MAX(CUCU_NrMatricol)+1, 1) FROM CU_Cursanti;");

                lCUCU = q.ExecuteToIntP(@"
INSERT INTO CU_Cursanti
    (CUCU_MBSO_Id, CUCU_MBDV_Id, CUCU_CNP, CUCU_DataNasterii, CUCU_Nume, CUCU_Prenume,
     CUCU_TelefonMobil, CUCU_Email,
     CUCU_NumeTutore, CUCU_PrenumeTutore, CUCU_TelefonTutore,
     CUCU_CISerieTutore, CUCU_CINumarTutore, CUCU_CNPTutore,
     CUCU_NrMatricol, CUCU_MBUT_Id, CUCU_DataSalvarii)
OUTPUT INSERTED.CUCU_Id
VALUES (@so, @dv, @cnp, @dn, @nume, @prenume,
        @tel, @email,
        @nmTutore, @prTutore, @telTutore,
        @ciSerie, @ciNr, @cnpTutore,
        @nrm, @u, GETDATE());",
                    CInterogareSQL.P("@so",       ManagerDeProiect.GetSocietate()),
                    CInterogareSQL.P("@dv",       ManagerDeProiect.GetDiviziune()),
                    CInterogareSQL.P("@cnp",      m_ebCNP.Text.Trim()),
                    CInterogareSQL.P("@dn",       m_dtDataNasterii.Value),
                    CInterogareSQL.P("@nume",     m_ebNume.Text.Trim().ToUpper()),
                    CInterogareSQL.P("@prenume",  m_ebPrenume.Text.Trim().ToUpper()),
                    CInterogareSQL.P("@tel",      m_ebTelefon.Text.Trim()),
                    CInterogareSQL.P("@email",    m_ebEmail.Text.Trim()),
                    CInterogareSQL.P("@nmTutore", m_ebNumeTutore.Text.Trim().ToUpper()),
                    CInterogareSQL.P("@prTutore", m_ebPrenumeTutore.Text.Trim().ToUpper()),
                    CInterogareSQL.P("@telTutore",m_ebTelefonTutore.Text.Trim()),
                    CInterogareSQL.P("@ciSerie",  m_ebCISerieTutore.Text.Trim().ToUpper()),
                    CInterogareSQL.P("@ciNr",     m_ebCINumarTutore.Text.Trim()),
                    CInterogareSQL.P("@cnpTutore",m_ebCNPTutore.Text.Trim()),
                    CInterogareSQL.P("@nrm",      nrMatricol),
                    CInterogareSQL.P("@u",        ManagerDeProiect.GetUserId())
                );
                if (lCUCU == 0)
                {
                    q.RollbackTransaction();
                    MessageBox.Show("Cursantul nu s-a putut salva. Verificați erorile de mai sus.");
                    return;
                }

                // ---- 3. Generare barcode (în aceeași tranzacție) ----
                using (var cmdBarcode = new SqlCommand("GE_GenerareBarcodeAutomat",
                    ManagerDeProiect.GetConnection(), tx)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 0
                })
                {
                    cmdBarcode.Parameters.AddWithValue("@lCUCU", lCUCU).Direction = ParameterDirection.Input;
                    cmdBarcode.Parameters.Add("@sBarcode", SqlDbType.VarChar, 30).Direction = ParameterDirection.Output;
                    cmdBarcode.ExecuteNonQuery();
                    sBarcode = cmdBarcode.Parameters["@sBarcode"].Value as string ?? "";
                }
                if (string.IsNullOrWhiteSpace(sBarcode))
                {
                    q.RollbackTransaction();
                    MessageBox.Show(
                        "Barcode-ul nu a putut fi generat (GE_GenerareBarcodeAutomat a returnat gol).\nCursantul NU a fost salvat.",
                        "Eroare barcode", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Save barcode to cursant record (SP only generates the string, caller must persist)
                q.ExecuteNonQueryP(
                    "UPDATE CU_Cursanti SET CUCU_Barcode = @barcode WHERE CUCU_Id = @cucu;",
                    CInterogareSQL.P("@barcode", sBarcode),
                    CInterogareSQL.P("@cucu", lCUCU));

                // ---- 4. Generare număr contract ----
                nrContract = CGeneratorNumarContract.Next(q);
                if (nrContract == 0)
                {
                    q.RollbackTransaction();
                    MessageBox.Show("Nu s-a putut genera numărul de contract (AD_SecventaNumarIntern).\nCursantul NU a fost salvat.");
                    return;
                }

                // ---- 5. INSERT contract — batch unic: INSERT + SCOPE_IDENTITY() în același scope ----
                lCUCO = q.ExecuteToIntP(@"
INSERT INTO CU_Contracte
    (CUCO_MBUT_Id, CUCO_NrContract, CUCO_DataInceputContract,
     CUCO_DataSfarsitContract, CUCO_DataSalvareContract)
VALUES (@u, @nr, @d1, @d2, GETDATE());
SELECT SCOPE_IDENTITY();",
                    CInterogareSQL.P("@u",  ManagerDeProiect.GetUserId()),
                    CInterogareSQL.P("@nr", nrContract),
                    CInterogareSQL.P("@d1", m_dtDataInceput.Value),
                    CInterogareSQL.P("@d2", m_dtDataSfarsit.Value)
                );
                if (lCUCO == 0)
                {
                    q.RollbackTransaction();
                    MessageBox.Show("Contractul nu s-a putut salva. Verificați erorile de mai sus.");
                    return;
                }

                // ---- 6. Legătură contract ↔ cursant ----
                if (q.ExecuteNonQueryP(
                    "INSERT INTO CU_ContracteCursanti (CUCC_CUCU_Id, CUCC_CUCO_Id) VALUES (@cu, @co);",
                    CInterogareSQL.P("@cu", lCUCU),
                    CInterogareSQL.P("@co", lCUCO)) == 0)
                {
                    q.RollbackTransaction();
                    MessageBox.Show("Legătura contract-cursant nu s-a putut crea. Verificați erorile de mai sus.");
                    return;
                }

                // ---- 7. Prima perioadă de abonament ----
                q.ExecuteNonQueryP(@"
INSERT INTO CU_AbonamenteCursant
    (CUAC_CUCO_Id, CUAC_CUAB_Id, CUAC_DataInceput, CUAC_DataSfarsit, CUAC_Pret,
     CUAC_MBUT_Id, CUAC_DataSalvarii)
VALUES (@cuco, @ab, @d1, @d2, @pret, @u, GETDATE());",
                    CInterogareSQL.P("@cuco", lCUCO),
                    CInterogareSQL.P("@ab",   Convert.ToInt32(abId)),
                    CInterogareSQL.P("@d1",   m_dtDataInceput.Value),
                    CInterogareSQL.P("@d2",   m_dtDataSfarsit.Value),
                    CInterogareSQL.P("@pret", pret),
                    CInterogareSQL.P("@u",    ManagerDeProiect.GetUserId())
                );

                // ---- 8. Registru numere interne ----
                q.ExecuteNonQueryP(@"
INSERT INTO AD_RegistruNumereInterne
    (ADRN_Numar, ADRN_Data, ADRN_ADTD_Id, ADRN_Iesire, ADRN_MBDV_Id,
     ADRN_CUCO_Id, ADRN_Descriere, ADRN_MBUT_Id, ADRN_DataSalvarii)
VALUES (@nr, GETDATE(), 1, 1, @div, @cuco, @desc, @u, GETDATE());",
                    CInterogareSQL.P("@nr",   nrContract),
                    CInterogareSQL.P("@div",  ManagerDeProiect.GetDiviziune()),
                    CInterogareSQL.P("@cuco", lCUCO),
                    CInterogareSQL.P("@desc", $"Contract nr. {nrContract} – {m_ebPrenume.Text.Trim()} {m_ebNume.Text.Trim()} ({m_cbAbonament.Text})"),
                    CInterogareSQL.P("@u",    ManagerDeProiect.GetUserId())
                );

                // ---- COMMIT — tot sau nimic ----
                q.CommitTransaction();
                m_ebBarcode.Text = sBarcode;
            }
            catch (Exception ex)
            {
                q.RollbackTransaction();
                MessageBox.Show($"Eroare neașteptată la înscriere: {ex.Message}",
                    "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                q.Close();
                m_tsbInscriere.Enabled = true;
                Cursor.Current = Cursors.Default;
            }

            // Ajungem aici DOAR dacă CommitTransaction a reușit
            MessageBox.Show(
                $"Cursant înscris cu succes!\nNr. contract: {nrContract}.",
                "Înscriere finalizată", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (!FrmGestiuneContract.PoateDeschide(ManagerDeProiect)) { this.Close(); return; }
            var frmContract = new FrmGestiuneContract(FrmParent, ManagerDeProiect, lCUCO)
            { MdiParent = FrmParent };
            frmContract.Show();
            frmContract.GenereazaContract();
            this.Close();
        }
    }
}

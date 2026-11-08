using GestiunePrezente.Common;
using GestiunePrezente.MeniuPrincipal;

namespace GestiunePrezente.MeniuCursanti
{
    public partial class FrmArhiva : Form
    {
        readonly CManagerDeProiect ManagerDeProiect;
        readonly FrmMeniuPrincipal FrmParent;
        readonly int m_lCUCO;

        public FrmArhiva(FrmMeniuPrincipal formParent, CManagerDeProiect mm, int cucoId)
        {
            FrmParent = formParent;
            ManagerDeProiect = mm;
            m_lCUCO = cucoId;
            InitializeComponent();

            GridPersonalizat.CustomGrid.SetConnection(ManagerDeProiect.GetConnection());
        }

        private void FrmArhiva_Load(object sender, EventArgs e)
        {
            if (m_lCUCO > 0)
            {
                var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
                string? nrCo = q.ExecuteToStringP(
                    "SELECT CAST(CUCO_NrContract AS NVARCHAR(20)) FROM CU_Contracte WHERE CUCO_Id = @id",
                    CInterogareSQL.P("@id", m_lCUCO));
                q.Close();
                Text = $"Arhivă — Contract nr. {nrCo}";
            }
            IncarcaArhiva();
        }

        private void IncarcaArhiva()
        {
            if (m_lCUCO <= 0) return;
            var sql = @"
SELECT ADAH_Id,
       ISNULL(ADAH_NumeFisier, RIGHT(ADAH_Cale, LEN(ADAH_Cale) - LEN(LEFT(ADAH_Cale, LEN(ADAH_Cale) - CHARINDEX('\', REVERSE(ADAH_Cale)))))) AS [Fișier],
       ADAH_DataAdaugare AS [Data],
       ADAH_Cale         AS [Cale]
  FROM AD_Arhiva
 WHERE ADAH_CUCO_Id = @cuco
    OR ADAH_ADRN_Id IN (SELECT ADRN_Id FROM AD_RegistruNumereInterne WHERE ADRN_CUCO_Id = @cuco)
 ORDER BY ADAH_DataAdaugare DESC, ADAH_Id DESC;";

            m_gArhiva.SetSql(sql, CInterogareSQL.P("@cuco", m_lCUCO));
            m_gArhiva.FormatColumn("ADAH_Id", 0, false);
            m_gArhiva.FormatColumn("Fișier", 350);
            m_gArhiva.FormatColumn("Data", 130, true, null, "dd.MM.yyyy HH:mm");
            m_gArhiva.FormatColumn("Cale", 600);
            foreach (DataGridViewColumn c in m_gArhiva.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void m_tsbDeschide_Click(object sender, EventArgs e)
        {
            object? val = m_gArhiva.GetColumnData("Cale");
            if (val == null || val == DBNull.Value) return;
            string cale = val.ToString()!;
            if (!File.Exists(cale)) { MessageBox.Show("Fișierul nu mai există la calea: " + cale); return; }
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(cale) { UseShellExecute = true });
        }

        private void m_tsbAdauga_Click(object sender, EventArgs e)
        {
            if (m_lCUCO <= 0) return;
            using var dlg = new OpenFileDialog { Title = "Selectați fișierul de arhivat", Filter = "Toate fișierele|*.*" };
            if (dlg.ShowDialog() != DialogResult.OK) return;

            string src = dlg.FileName;
            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            string? nrCo = q.ExecuteToStringP(
                "SELECT CAST(CUCO_NrContract AS NVARCHAR(20)) FROM CU_Contracte WHERE CUCO_Id = @id",
                CInterogareSQL.P("@id", m_lCUCO));
            string? numeCuCo = q.ExecuteToStringP(
                @"SELECT TOP 1 CUCU_Nume + '_' + CUCU_Prenume FROM CU_ContracteCursanti
                  INNER JOIN CU_Cursanti ON CUCC_CUCU_Id = CUCU_Id
                  WHERE CUCC_CUCO_Id = @id ORDER BY CUCU_Nume, CUCU_Prenume",
                CInterogareSQL.P("@id", m_lCUCO));
            q.Close();

            string numeFolderCo = FaraDiacritice($"{nrCo ?? m_lCUCO.ToString()}_{numeCuCo ?? ""}").Trim('_');
            string folder = Path.Combine(@"d:\WORK\ONIXTEIA\ACTE\ARHIVA\CONTRACTE", numeFolderCo);
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
                    @"INSERT INTO AD_Arhiva (ADAH_Cale, ADAH_NumeFisier, ADAH_DataAdaugare, ADAH_CUCO_Id, ADAH_MBDV_Id)
                      VALUES (@cale, @nume, GETDATE(), @cuco, @div)",
                    CInterogareSQL.P("@cale", dest),
                    CInterogareSQL.P("@nume", Path.GetFileName(dest)),
                    CInterogareSQL.P("@cuco", m_lCUCO),
                    CInterogareSQL.P("@div",  ManagerDeProiect.GetDiviziune()));
            q2.Close();
            IncarcaArhiva();
        }

        private static string FaraDiacritice(string s) =>
            s.Replace('ă', 'a').Replace('Ă', 'A')
             .Replace('â', 'a').Replace('Â', 'A')
             .Replace('î', 'i').Replace('Î', 'I')
             .Replace('ș', 's').Replace('Ș', 'S')
             .Replace('ş', 's').Replace('Ş', 'S')
             .Replace('ț', 't').Replace('Ț', 'T')
             .Replace('ţ', 't').Replace('Ţ', 'T');

        private void m_tsbSterge_Click(object sender, EventArgs e)
        {
            object? val = m_gArhiva.GetColumnData("ADAH_Id");
            if (val == null || val == DBNull.Value) return;
            int id = Convert.ToInt32(val);
            if (MessageBox.Show("Ștergeți înregistrarea din arhivă?\n(fișierul de pe disc nu va fi șters)", "Atenție",
                    MessageBoxButtons.YesNo) == DialogResult.No) return;
            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            q.ExecuteNonQueryP("DELETE FROM AD_Arhiva WHERE ADAH_Id = @id", CInterogareSQL.P("@id", id));
            q.Close();
            IncarcaArhiva();
        }
    }
}

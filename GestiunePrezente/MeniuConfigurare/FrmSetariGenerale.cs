using GestiunePrezente.Common;
using GestiunePrezente.MeniuPrincipal;
using GestiunePrezente.SmartBill;
using Microsoft.Data.SqlClient;

namespace GestiunePrezente.MeniuConfigurare
{
    public partial class FrmSetariGenerale : Form
    {
        private readonly CManagerDeProiect m_manager;
        private int m_mbsoId;
        private bool m_modified;

        public const string MBME_NUME = "SmartBill";
        public static bool PoateDeschide(CManagerDeProiect mm)
            => DrepturiFormular.PoateDeschide(mm, MBME_NUME);

        public FrmSetariGenerale(FrmMeniuPrincipal formParent, CManagerDeProiect mm)
        {
            m_manager = mm;
            InitializeComponent();
            Load += (_, _) => IncarcaDate();
        }

        // ── Date ─────────────────────────────────────────────────────────
        private void IncarcaDate()
        {
            try
            {
                var conn = m_manager.GetConnection();
                using var cmd = new SqlCommand(
                    @"SELECT MBSO_Id,
                             ISNULL(MBSO_SmartBillEmail,  ''),
                             ISNULL(MBSO_SmartBillApiKey, ''),
                             ISNULL(MBSO_SmartBillSerie,  '')
                      FROM MB_Societate
                      WHERE MBSO_Id = @id", conn);
                cmd.Parameters.AddWithValue("@id", m_manager.GetSocietate());
                using var rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    m_mbsoId        = rd.GetInt32(0);
                    m_tbEmail.Text  = rd.GetString(1);
                    m_tbApiKey.Text = rd.GetString(2);
                    m_tbSerie.Text  = rd.GetString(3);
                }
            }
            catch { }

            m_modified = false;
            m_btnSalvare.Enabled = false;
        }

        private void Salveaza()
        {
            if (m_mbsoId == 0) return;
            try
            {
                new CInterogareSQL(m_manager.GetConnection()).ExecuteNonQueryP(
                    @"UPDATE MB_Societate SET
                        MBSO_SmartBillEmail  = @email,
                        MBSO_SmartBillApiKey = @key,
                        MBSO_SmartBillSerie  = @serie
                      WHERE MBSO_Id = @id",
                    CInterogareSQL.P("@email", m_tbEmail.Text.Trim()),
                    CInterogareSQL.P("@key",   m_tbApiKey.Text.Trim()),
                    CInterogareSQL.P("@serie", m_tbSerie.Text.Trim()),
                    CInterogareSQL.P("@id",    m_mbsoId));

                m_modified = false;
                m_btnSalvare.Enabled = false;
                MessageBox.Show("Setările au fost salvate.", "Succes",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la salvare: " + ex.Message, "Eroare",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task TesteazaConexiuneAsync()
        {
            m_btnTestSB.Enabled = false;
            m_btnTestSB.Text = "Se testează...";
            try
            {
                string email  = m_tbEmail.Text.Trim();
                string apiKey = m_tbApiKey.Text.Trim();
                string serie  = m_tbSerie.Text.Trim();

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(serie))
                {
                    MessageBox.Show("Completați Email, API key și Serie înainte de test.", "Date incomplete",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string cui = "";
                try
                {
                    using var cmd = new SqlCommand("SELECT ISNULL(MBSO_CUI,'') FROM MB_Societate WHERE MBSO_Id=@id",
                        m_manager.GetConnection());
                    cmd.Parameters.AddWithValue("@id", m_manager.GetSocietate());
                    cui = (string)(cmd.ExecuteScalar() ?? "");
                }
                catch { }

                var svc = new CSmartBillService(email, apiKey, serie, cui);
                string? err = await svc.TesteazaConexiuneAsync();

                if (err == null)
                    MessageBox.Show("Conexiune SmartBill reușită!", "Succes",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    CEroare.Arata("Eroare conexiune SmartBill", err);
            }
            finally
            {
                m_btnTestSB.Enabled = true;
                m_btnTestSB.Text = "Test conexiune";
            }
        }

        // ── Event handlers ────────────────────────────────────────────────
        private void m_tbEmail_TextChanged(object sender, EventArgs e)  => SetModified();
        private void m_tbApiKey_TextChanged(object sender, EventArgs e) => SetModified();
        private void m_tbSerie_TextChanged(object sender, EventArgs e)  => SetModified();

        private void m_btnSalvare_Click(object sender, EventArgs e) => Salveaza();

        private async void m_btnTestSB_Click(object sender, EventArgs e) => await TesteazaConexiuneAsync();

        private void SetModified()
        {
            m_modified = true;
            m_btnSalvare.Enabled = true;
        }
    }
}

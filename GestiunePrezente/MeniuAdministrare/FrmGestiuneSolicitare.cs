using System.Data;
using GestiunePrezente.Common;
using GestiunePrezente.MeniuCursanti;
using GestiunePrezente.MeniuPrincipal;

namespace GestiunePrezente.MeniuAdministrare
{
    public partial class FrmGestiuneSolicitare : Form
    {
        readonly FrmMeniuPrincipal FrmParent;
        readonly CManagerDeProiect ManagerDeProiect;
        int m_lADSO = 0;
        int m_lCUCU = 0; // cursant asociat (0 = niciunul)

        public const string MBME_NUME = "Gestiune Solicitare";
        public static bool PoateDeschide(CManagerDeProiect mm)
            => DrepturiFormular.PoateDeschide(mm, MBME_NUME);

        public FrmGestiuneSolicitare(FrmMeniuPrincipal formParent, CManagerDeProiect mm, int lADSO)
        {
            FrmParent = formParent;
            ManagerDeProiect = mm;
            m_lADSO = lADSO;
            InitializeComponent();
        }

        private void FrmGestiuneSolicitare_Load(object sender, EventArgs e)
        {
            IncarcaSurse();
            if (m_lADSO > 0)
                IncarcaDate();
            else
            {
                m_dtpData.Value = DateTime.Now;
                m_cbStatus.SelectedIndex = 0; // Nou
                m_cbSursa.SelectedIndex = 0;
                m_cbNivelSah.SelectedIndex = 0;
            }
            ActualizeazaBtnCursant();
            m_tsbSalveaza.Enabled = false;
        }

        private void IncarcaSurse()
        {
            m_cbSursa.Items.Clear();
            m_cbSursa.Items.Add("(nicio sursă)");
            using var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            q.OpenP("SELECT ADSR_Id, ADSR_Denumire FROM AD_SurseSolicitari ORDER BY ADSR_Id");
            while (q.NextRow())
                m_cbSursa.Items.Add($"{q.GetIntValue(0)}|{q.GetStringValue(1)}");
            q.Close();
            m_cbSursa.SelectedIndex = 0;
        }

        private void IncarcaDate()
        {
            using var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            q.OpenP(@"SELECT ADSO_ADSR_Id, ADSO_DataSolicitare, ADSO_NivelSah,
                             ADSO_NumeCopil, ADSO_PrenumeCopil, ADSO_VarstaCopil,
                             ADSO_NumeContact, ADSO_PrenumeContact,
                             ADSO_Email, ADSO_Telefon, ADSO_Mesaj, ADSO_InboxURL,
                             ADSO_Status, ADSO_DataApel, ISNULL(ADSO_ObservatiiApel,''),
                             ISNULL(ADSO_CUCU_Id, 0)
                        FROM AD_Solicitari
                       WHERE ADSO_Id = @id",
                CInterogareSQL.P("@id", m_lADSO));
            if (q.NextRow())
            {
                // Sursă
                int sursaId = q.GetIntValue(0);
                for (int i = 1; i < m_cbSursa.Items.Count; i++)
                {
                    string item = m_cbSursa.Items[i] as string ?? "";
                    if (item.StartsWith(sursaId + "|")) { m_cbSursa.SelectedIndex = i; break; }
                }

                m_dtpData.Value = q.GetDateValue(1) ?? DateTime.Now;

                string nivel = q.GetStringValue(2) ?? "";
                int nivelIdx = m_cbNivelSah.Items.IndexOf(nivel);
                m_cbNivelSah.SelectedIndex = nivelIdx >= 0 ? nivelIdx : 0;

                m_ebNumeCopil.Text      = q.GetStringValue(3) ?? "";
                m_ebPrenumeCopil.Text   = q.GetStringValue(4) ?? "";
                m_ebVarsta.Text         = q.GetIntValue(5) > 0 ? q.GetIntValue(5).ToString() : "";
                m_ebNumeContact.Text    = q.GetStringValue(6) ?? "";
                m_ebPrenumeContact.Text = q.GetStringValue(7) ?? "";
                m_ebEmail.Text          = q.GetStringValue(8) ?? "";
                m_ebTelefon.Text        = q.GetStringValue(9) ?? "";
                m_ebMesaj.Text          = q.GetStringValue(10) ?? "";
                m_ebInboxURL.Text       = q.GetStringValue(11) ?? "";

                int status = q.GetIntValue(12);
                m_cbStatus.SelectedIndex = status >= 1 && status <= 5 ? status - 1 : 0;

                DateTime? dataApel = q.GetDateValue(13);
                if (dataApel.HasValue)
                {
                    m_ckDataApel.Checked  = true;
                    m_dtpDataApel.Enabled = true;
                    m_dtpDataApel.Value   = dataApel.Value;
                }

                m_ebObservatii.Text = q.GetStringValue(14) ?? "";
                m_lCUCU = q.GetIntValue(15);
            }
            q.Close();
        }

        // =========================================================
        //                          SALVARE
        // =========================================================
        private void m_tsbSalveaza_Click(object sender, EventArgs e)
            => m_tsbSalveaza.Enabled = !Salveaza();

        private bool Salveaza()
        {
            int? sursaId = null;
            string sursaItem = m_cbSursa.SelectedItem as string ?? "";
            if (sursaItem.Contains('|'))
                sursaId = int.Parse(sursaItem.Split('|')[0]);

            int status = m_cbStatus.SelectedIndex + 1;
            int? varsta = int.TryParse(m_ebVarsta.Text.Trim(), out int v) ? v : null;
            DateTime? dataApel = m_ckDataApel.Checked ? m_dtpDataApel.Value.Date : null;

            string? nivel = m_cbNivelSah.SelectedItem as string;
            if (string.IsNullOrEmpty(nivel)) nivel = null;

            using var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            try
            {
                if (m_lADSO == 0)
                {
                    m_lADSO = q.ExecuteToIntPThrow(@"
INSERT INTO AD_Solicitari
 (ADSO_MBDV_Id, ADSO_ADSR_Id, ADSO_DataSolicitare, ADSO_NivelSah,
  ADSO_NumeCopil, ADSO_PrenumeCopil, ADSO_VarstaCopil,
  ADSO_NumeContact, ADSO_PrenumeContact,
  ADSO_Email, ADSO_Telefon, ADSO_Mesaj, ADSO_InboxURL,
  ADSO_Status, ADSO_DataApel, ADSO_ObservatiiApel,
  ADSO_CUCU_Id, ADSO_MBUT_Id, ADSO_DataSalvarii)
OUTPUT INSERTED.ADSO_Id
VALUES (@div, @sr, @data, @nivel,
        @numec, @prenumec, @varsta,
        @numeco, @prenumeco,
        @email, @tel, @mesaj, @url,
        @status, @apel, @obs,
        @cucu, @u, GETDATE());",
                        Params(sursaId, status, varsta, nivel, dataApel, q));
                }
                else
                {
                    q.ExecuteNonQueryPThrow(@"
UPDATE AD_Solicitari SET
  ADSO_ADSR_Id        = @sr,
  ADSO_DataSolicitare = @data,
  ADSO_NivelSah       = @nivel,
  ADSO_NumeCopil      = @numec,
  ADSO_PrenumeCopil   = @prenumec,
  ADSO_VarstaCopil    = @varsta,
  ADSO_NumeContact    = @numeco,
  ADSO_PrenumeContact = @prenumeco,
  ADSO_Email          = @email,
  ADSO_Telefon        = @tel,
  ADSO_Mesaj          = @mesaj,
  ADSO_InboxURL       = @url,
  ADSO_Status         = @status,
  ADSO_DataApel       = @apel,
  ADSO_ObservatiiApel = @obs,
  ADSO_CUCU_Id        = @cucu,
  ADSO_DataSalvarii   = GETDATE()
 WHERE ADSO_Id = @id;",
                        Params(sursaId, status, varsta, nivel, dataApel, q)
                        .Append(CInterogareSQL.P("@id", m_lADSO)).ToArray());
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la salvare: " + ex.Message);
                return false;
            }
        }

        private Microsoft.Data.SqlClient.SqlParameter[] Params(
            int? sursaId, int status, int? varsta, string? nivel, DateTime? dataApel,
            CInterogareSQL _)
        {
            return new[]
            {
                CInterogareSQL.P("@div",      ManagerDeProiect.GetDiviziune()),
                CInterogareSQL.P("@sr",       sursaId.HasValue ? (object)sursaId.Value : DBNull.Value),
                CInterogareSQL.P("@data",     m_dtpData.Value.Date),
                CInterogareSQL.P("@nivel",    nivel ?? (object)DBNull.Value),
                CInterogareSQL.P("@numec",    Nul(m_ebNumeCopil.Text)),
                CInterogareSQL.P("@prenumec", Nul(m_ebPrenumeCopil.Text)),
                CInterogareSQL.P("@varsta",   varsta.HasValue ? (object)varsta.Value : DBNull.Value),
                CInterogareSQL.P("@numeco",   Nul(m_ebNumeContact.Text)),
                CInterogareSQL.P("@prenumeco",Nul(m_ebPrenumeContact.Text)),
                CInterogareSQL.P("@email",    Nul(m_ebEmail.Text)),
                CInterogareSQL.P("@tel",      Nul(m_ebTelefon.Text)),
                CInterogareSQL.P("@mesaj",    Nul(m_ebMesaj.Text)),
                CInterogareSQL.P("@url",      Nul(m_ebInboxURL.Text)),
                CInterogareSQL.P("@status",   status),
                CInterogareSQL.P("@apel",     dataApel.HasValue ? (object)dataApel.Value : DBNull.Value),
                CInterogareSQL.P("@obs",      Nul(m_ebObservatii.Text)),
                CInterogareSQL.P("@cucu",     m_lCUCU > 0 ? (object)m_lCUCU : DBNull.Value),
                CInterogareSQL.P("@u",        ManagerDeProiect.GetUserId()),
            };
        }

        private static object Nul(string s)
            => string.IsNullOrWhiteSpace(s) ? DBNull.Value : s.Trim();

        // =========================================================
        //                     BUTON CURSANT
        // =========================================================
        private void m_btnCursant_Click(object sender, EventArgs e)
        {
            if (!FrmGestiuneCursant.PoateDeschide(ManagerDeProiect)) return;
            if (m_lCUCU > 0)
            {
                // Deschide cursantul existent
                var frm = new FrmGestiuneCursant(FrmParent, ManagerDeProiect, m_lCUCU)
                { MdiParent = FrmParent };
                frm.Show();
                return;
            }

            // Salvează solicitarea înainte de a crea cursantul
            if (m_lADSO == 0)
            {
                if (MessageBox.Show("Solicitarea va fi salvată înainte de a genera cursantul. Continuați?",
                    "Confirmare", MessageBoxButtons.YesNo) == DialogResult.No) return;
                if (!Salveaza()) return;
                m_tsbSalveaza.Enabled = false;
            }

            // Deschide FrmGestiuneCursant cu date pre-completate
            var frmC = new FrmGestiuneCursant(FrmParent, ManagerDeProiect, 0,
                prenume: m_ebPrenumeCopil.Text.Trim(),
                nume:    m_ebNumeCopil.Text.Trim())
            { MdiParent = FrmParent };

            frmC.FormClosed += (s, _) =>
            {
                int newCUCU = frmC.IdCursant;
                if (newCUCU > 0)
                {
                    m_lCUCU = newCUCU;
                    // Salvăm legătura în BD
                    using var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
                    q.ExecuteNonQueryP(
                        "UPDATE AD_Solicitari SET ADSO_CUCU_Id = @cucu WHERE ADSO_Id = @id",
                        CInterogareSQL.P("@cucu", m_lCUCU),
                        CInterogareSQL.P("@id",   m_lADSO));
                    q.Close();
                    ActualizeazaBtnCursant();
                }
            };
            frmC.Show();
        }

        private void ActualizeazaBtnCursant()
        {
            if (m_lCUCU > 0)
            {
                m_btnCursant.Text      = "Deschide cursant";
                m_btnCursant.BackColor = Color.FromArgb(200, 240, 200);
            }
            else
            {
                m_btnCursant.Text      = "Generează cursant";
                m_btnCursant.BackColor = SystemColors.Control;
            }
        }

        // =========================================================
        //                     EVENIMENTE
        // =========================================================
        private void m_eb_Changed(object sender, EventArgs e) => m_tsbSalveaza.Enabled = true;

        private void m_ckDataApel_CheckedChanged(object sender, EventArgs e)
        {
            m_dtpDataApel.Enabled = m_ckDataApel.Checked;
            if (m_ckDataApel.Checked && m_dtpDataApel.Value.Year < 2000)
                m_dtpDataApel.Value = DateTime.Today;
            m_tsbSalveaza.Enabled = true;
        }

        private void FrmGestiuneSolicitare_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_tsbSalveaza.Enabled)
            {
                if (MessageBox.Show("Salvați modificările?", "Atenție", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    if (!Salveaza()) e.Cancel = true;
            }
        }
    }
}

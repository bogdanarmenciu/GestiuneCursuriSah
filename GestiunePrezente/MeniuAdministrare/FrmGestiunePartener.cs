using GestiunePrezente.Common;
using GestiunePrezente.MeniuPrincipal;
using Microsoft.Data.SqlClient;

namespace GestiunePrezente.MeniuAdministrare
{
    public partial class FrmGestiunePartener : Form
    {
        readonly CManagerDeProiect ManagerDeProiect;
        readonly FrmMeniuPrincipal FrmParent;
        int m_lADPA;

        public const string MBME_NUME = "Gestiune Partener";
        public static bool PoateDeschide(CManagerDeProiect mm)
            => DrepturiFormular.PoateDeschide(mm, MBME_NUME);

        public FrmGestiunePartener(FrmMeniuPrincipal formParent, CManagerDeProiect mm, int adpaId)
        {
            FrmParent = formParent;
            ManagerDeProiect = mm;
            m_lADPA = adpaId;
            InitializeComponent();

            foreach (var tb in new[] {
                m_ebStrada, m_ebNumar, m_ebBloc, m_ebScara, m_ebEtaj, m_ebApartament,
                m_ebCodPostal, m_ebLocalitate, m_ebJudet, m_ebSector,
                m_ebTelefon, m_ebTelefonMobil, m_ebEmail, m_ebPersoanaContact })
            {
                tb.TextChanged += m_eb_Changed;
            }
        }

        private void FrmGestiunePartener_Load(object sender, EventArgs e)
        {
            Text = m_lADPA == 0 ? "Partener nou" : "Modifică partener";
            if (m_lADPA > 0) IncarcaDate();
            m_tsbSalveaza.Enabled = false;
        }

        // =========================================================
        //                       ÎNCĂRCARE
        // =========================================================
        private void IncarcaDate()
        {
            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            q.OpenP(@"SELECT ADPA_Denumire, ADPA_CUI,
                             ISNULL(ADPA_RegCom1,''), ISNULL(ADPA_RegCom2,''),
                             ISNULL(CAST(ADPA_RegCom3 AS varchar(4)),''),
                             ISNULL(ADPA_ContBancar,''),
                             ADPA_IsClient, ADPA_IsFurnizor, ADPA_IsSponsor,
                             ISNULL(ADPA_Strada,''), ISNULL(ADPA_Numar,''),
                             ISNULL(ADPA_Bloc,''), ISNULL(ADPA_Scara,''),
                             ISNULL(ADPA_Etaj,''), ISNULL(ADPA_Apartament,''),
                             ISNULL(ADPA_CodPostal,''), ISNULL(ADPA_Localitate,''),
                             ISNULL(ADPA_Judet,''), ISNULL(ADPA_Sector,''),
                             ISNULL(ADPA_Telefon,''), ISNULL(ADPA_TelefonMobil,''),
                             ISNULL(ADPA_Email,''), ISNULL(ADPA_PersoanaContact,'')
                        FROM AD_Parteneri WHERE ADPA_Id = @id",
                CInterogareSQL.P("@id", m_lADPA));

            if (q.NextRow())
            {
                m_ebDenumire.Text        = q.GetStringValue(0) ?? "";
                m_ebCUI.Text             = q.GetStringValue(1) ?? "";
                m_ebRegCom1.Text         = q.GetStringValue(2) ?? "";
                m_ebRegCom2.Text         = q.GetStringValue(3) ?? "";
                m_ebRegCom3.Text         = q.GetStringValue(4) ?? "";
                m_ebContBancar.Text      = q.GetStringValue(5) ?? "";
                m_ckIsClient.Checked     = q.GetIntValue(6) == 1;
                m_ckIsFurnizor.Checked   = q.GetIntValue(7) == 1;
                m_ckIsSponsor.Checked    = q.GetIntValue(8) == 1;
                m_ebStrada.Text          = q.GetStringValue(9) ?? "";
                m_ebNumar.Text           = q.GetStringValue(10) ?? "";
                m_ebBloc.Text            = q.GetStringValue(11) ?? "";
                m_ebScara.Text           = q.GetStringValue(12) ?? "";
                m_ebEtaj.Text            = q.GetStringValue(13) ?? "";
                m_ebApartament.Text      = q.GetStringValue(14) ?? "";
                m_ebCodPostal.Text       = q.GetStringValue(15) ?? "";
                m_ebLocalitate.Text      = q.GetStringValue(16) ?? "";
                m_ebJudet.Text           = q.GetStringValue(17) ?? "";
                m_ebSector.Text          = q.GetStringValue(18) ?? "";
                m_ebTelefon.Text         = q.GetStringValue(19) ?? "";
                m_ebTelefonMobil.Text    = q.GetStringValue(20) ?? "";
                m_ebEmail.Text           = q.GetStringValue(21) ?? "";
                m_ebPersoanaContact.Text = q.GetStringValue(22) ?? "";
            }
            q.Close();
        }

        // =========================================================
        //                       SALVARE
        // =========================================================
        private void m_tsbSalveaza_Click(object sender, EventArgs e)
        {
            if (Salveaza()) m_tsbSalveaza.Enabled = false;
        }

        private bool Salveaza()
        {
            if (string.IsNullOrWhiteSpace(m_ebDenumire.Text))
            {
                MessageBox.Show("Introduceți denumirea partenerului.", "Atenție",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                m_ebDenumire.Focus();
                return false;
            }

            int? regCom3 = null;
            if (!string.IsNullOrWhiteSpace(m_ebRegCom3.Text))
            {
                if (!int.TryParse(m_ebRegCom3.Text.Trim(), out int rc3))
                {
                    MessageBox.Show("Anul din Nr. Reg. Com. trebuie să fie un număr.", "Atenție",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                regCom3 = rc3;
            }

            var ps = new List<SqlParameter>
            {
                CInterogareSQL.P("@div",   ManagerDeProiect.GetDiviziune()),
                CInterogareSQL.P("@den",   m_ebDenumire.Text.Trim()),
                CInterogareSQL.P("@cui",   Nul(m_ebCUI.Text)),
                CInterogareSQL.P("@rc1",   Nul(m_ebRegCom1.Text)),
                CInterogareSQL.P("@rc2",   Nul(m_ebRegCom2.Text)),
                CInterogareSQL.P("@rc3",   regCom3.HasValue ? (object)regCom3.Value : DBNull.Value),
                CInterogareSQL.P("@cb",    Nul(m_ebContBancar.Text)),
                CInterogareSQL.P("@str",   Nul(m_ebStrada.Text)),
                CInterogareSQL.P("@nr",    Nul(m_ebNumar.Text)),
                CInterogareSQL.P("@bl",    Nul(m_ebBloc.Text)),
                CInterogareSQL.P("@sc",    Nul(m_ebScara.Text)),
                CInterogareSQL.P("@et",    Nul(m_ebEtaj.Text)),
                CInterogareSQL.P("@ap",    Nul(m_ebApartament.Text)),
                CInterogareSQL.P("@cp",    Nul(m_ebCodPostal.Text)),
                CInterogareSQL.P("@loc",   Nul(m_ebLocalitate.Text)),
                CInterogareSQL.P("@jud",   Nul(m_ebJudet.Text)),
                CInterogareSQL.P("@sec",   Nul(m_ebSector.Text)),
                CInterogareSQL.P("@tel",   Nul(m_ebTelefon.Text)),
                CInterogareSQL.P("@mob",   Nul(m_ebTelefonMobil.Text)),
                CInterogareSQL.P("@email", Nul(m_ebEmail.Text)),
                CInterogareSQL.P("@pc",    Nul(m_ebPersoanaContact.Text)),
                CInterogareSQL.P("@cli",   m_ckIsClient.Checked),
                CInterogareSQL.P("@fur",   m_ckIsFurnizor.Checked),
                CInterogareSQL.P("@spo",   m_ckIsSponsor.Checked),
                CInterogareSQL.P("@u",     ManagerDeProiect.GetUserId()),
            };

            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            try
            {
                if (m_lADPA == 0)
                {
                    m_lADPA = q.ExecuteToIntPThrow(@"
INSERT INTO AD_Parteneri
  (ADPA_MBDV_Id, ADPA_Denumire, ADPA_CUI,
   ADPA_RegCom1, ADPA_RegCom2, ADPA_RegCom3, ADPA_ContBancar,
   ADPA_Strada, ADPA_Numar, ADPA_Bloc, ADPA_Scara, ADPA_Etaj,
   ADPA_Apartament, ADPA_CodPostal, ADPA_Localitate, ADPA_Judet, ADPA_Sector,
   ADPA_Telefon, ADPA_TelefonMobil, ADPA_Email, ADPA_PersoanaContact,
   ADPA_IsClient, ADPA_IsFurnizor, ADPA_IsSponsor,
   ADPA_MBUT_Id, ADPA_DataSalvarii)
OUTPUT INSERTED.ADPA_Id
VALUES
  (@div, @den, @cui,
   @rc1, @rc2, @rc3, @cb,
   @str, @nr, @bl, @sc, @et,
   @ap, @cp, @loc, @jud, @sec,
   @tel, @mob, @email, @pc,
   @cli, @fur, @spo,
   @u, GETDATE())",
                        ps.ToArray());
                    Text = "Modifică partener";
                }
                else
                {
                    ps.Add(CInterogareSQL.P("@id", m_lADPA));
                    q.ExecuteNonQueryPThrow(@"
UPDATE AD_Parteneri SET
  ADPA_Denumire=@den, ADPA_CUI=@cui,
  ADPA_RegCom1=@rc1, ADPA_RegCom2=@rc2, ADPA_RegCom3=@rc3, ADPA_ContBancar=@cb,
  ADPA_Strada=@str, ADPA_Numar=@nr, ADPA_Bloc=@bl, ADPA_Scara=@sc, ADPA_Etaj=@et,
  ADPA_Apartament=@ap, ADPA_CodPostal=@cp, ADPA_Localitate=@loc,
  ADPA_Judet=@jud, ADPA_Sector=@sec,
  ADPA_Telefon=@tel, ADPA_TelefonMobil=@mob, ADPA_Email=@email, ADPA_PersoanaContact=@pc,
  ADPA_IsClient=@cli, ADPA_IsFurnizor=@fur, ADPA_IsSponsor=@spo,
  ADPA_MBUT_Id=@u, ADPA_DataSalvarii=GETDATE()
WHERE ADPA_Id=@id",
                        ps.ToArray());
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la salvare: " + ex.Message, "Eroare",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally { q.Close(); }
        }

        private static object Nul(string s)
            => string.IsNullOrWhiteSpace(s) ? DBNull.Value : (object)s.Trim();

        // =========================================================
        //                     DIRTY FLAG
        // =========================================================
        private void m_eb_Changed(object? sender, EventArgs e)
            => m_tsbSalveaza.Enabled = true;

        private void FrmGestiunePartener_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!m_tsbSalveaza.Enabled) return;
            if (MessageBox.Show("Salvați modificările?", "Atenție",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                if (!Salveaza()) e.Cancel = true;
        }
    }
}

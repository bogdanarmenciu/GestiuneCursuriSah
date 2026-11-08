using GestiunePrezente.Common;
using GestiunePrezente.MeniuPrincipal;

namespace GestiunePrezente.MeniuConfigurare
{
    public partial class FrmGestiuneSocietate : Form
    {
        readonly FrmMeniuPrincipal FrmParent;
        readonly CManagerDeProiect ManagerDeProiect;
        readonly int m_lMBSO = 0;
        string? m_sSql;

        public const string MBME_NUME = "Gestiune Societate";
        public static bool PoateDeschide(CManagerDeProiect mm)
            => DrepturiFormular.PoateDeschide(mm, MBME_NUME);

        public FrmGestiuneSocietate(FrmMeniuPrincipal FormParent, CManagerDeProiect MM, int lMBSO)
        {
            FrmParent = FormParent;
            ManagerDeProiect = MM;
            m_lMBSO = lMBSO;

            InitializeComponent();
        }

        private void FrmGestiuneSocietate_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_tsbSalvare.Enabled == true)
            {
                if (MessageBox.Show(CFormatareDate.GetString("IDS_SAVE_ASK"), CFormatareDate.GetString("IDS_ATENTIE"), MessageBoxButtons.YesNo) == DialogResult.No)
                    e.Cancel = false;
                else
                    if (!Salveaza())
                        e.Cancel = true;
            }
        }

        private void m_tsbSalvare_Click(object sender, EventArgs e)
        {
            Salveaza();
        }

        private bool Salveaza()
        {
            var fields = new (TextBox TextBox, string SqlField, string ErrorId)[]
            {
        (m_ebSocietate, "MBSO_Descriere", "IDS_ERR_SOCIETATE"),
        (m_ebCUI, "MBSO_CUI", "IDS_ERR_SOCIETATE_CUI"),
        (m_ebJ, "MBSO_J", "IDS_ERR_SOCIETATE_J"),
        (m_ebStrada, "MBSO_Strada", "IDS_ERR_SOCIETATE_STRADA"),
        (m_ebNrStrada, "MBSO_NumarStrada", "IDS_ERR_SOCIETATE_NRSTRADA"),
        (m_ebBloc, "MBSO_Bloc", "IDS_ERR_SOCIETATE_BLOC"),
        (m_ebScara, "MBSO_Scara", "IDS_ERR_SOCIETATE_SCARA"),
        (m_ebApartament, "MBSO_Apartament", "IDS_ERR_SOCIETATE_AP"),
        (m_ebCodPostal, "MBSO_CodPostal", "IDS_ERR_SOCIETATE_CODPOSTAL"),
        (m_ebOras, "MBSO_Oras", "IDS_ERR_SOCIETATE_ORAS"),
        (m_ebJudet, "MBSO_Judet", "IDS_ERR_SOCIETATE_JUDET"),
        (m_ebTara, "MBSO_Tara", "IDS_ERR_SOCIETATE_TARA"),
        (m_ebBanca, "MBSO_Banca", "IDS_ERR_SOCIETATE_BANCA"),
        (m_ebCont, "MBSO_Cont", "IDS_ERR_SOCIETATE_CONT"),
        (m_ebContTrez, "MBSO_ContTrez", "IDS_ERR_SOCIETATE_CONT"),
        (m_ebAdresaMailSocietate, "MBSO_EmailSocietate", "IDS_ERR_SOCIETATE_EMAL"),
        (m_ebTelefonSoc, "MBSO_TelefonSocietate", "IDS_ERR_SOCIETATE_TEL"),
        (m_ebReprezentant, "MBSO_Reprezentant", "IDS_ERR_SOCIETATE_REPREZENTANT"),
        (m_ebTelReprezentant, "MBSO_TelReprezentant", "IDS_ERR_SOCIETATE_TELREPREZENTANT"),
        (m_ebEmailReprezentant, "MBSO_EmailReprezentant", "IDS_ERR_SOCIETATE_EMAILREPREZENTANT"),
        (m_ebFunctieReprezentant, "MBSO_FunctieReprezentant", "IDS_ERR_SOCIETATE_FUNCTIEREPREZENTANT"),
        (m_ebCISerieReprezentant, "MBSO_CISerieReprezentant", "IDS_ERR_SOCIETATE_CISERIEREPREZENTANT"),
        (m_ebCINRReprezentant, "MBSO_CINumarReprezentant", "IDS_ERR_SOCIETATE_CINRREPREZENTANT"),
        (m_ebCNPReprezentant, "MBSO_CNPReprezentant", "IDS_ERR_SOCIETATE_CNPREPREZENTANT"),
        (m_ebAdresaReprezentant, "MBSO_AdresaReprezentant", "IDS_ERR_SOCIETATE_ADRESAREPREZENTANT"),
        (m_ebResponsabilDiplome, "MBSO_ResponsabilDiplome", "IDS_ERR_SOCIETATE_RESPONSABILDIPLOME"),
        (m_ebResponsabilChitante, "MBSO_ResponsabilChitante", "IDS_ERR_SOCIETATE_RESPONSABILCHITANTE"),
        (m_ebIPServer, "MBSO_ServerIP", "IDS_ERR_SOCIETATE_IPSERVER")
            };

            string sUpdate = "";

            foreach (var (textBox, sqlField, errorId) in fields)
            {
                string text = textBox.Text.Trim();

                if (string.IsNullOrEmpty(text))
                {
                    CFormatareDate.ShowWarning(errorId);
                    textBox.Focus();
                    return false;
                }

                if (sUpdate.Length > 0)
                    sUpdate += ",";

                if (sqlField == "MBSO_Descriere" || sqlField == "MBSO_AdresaReprezentant")
                {
                    sUpdate += string.Format("{0} = N'{1}'", sqlField, CFormatareDate.StringToSqlString(text));
                }
                else
                {
                    sUpdate += string.Format("{0} = '{1}'", sqlField, CFormatareDate.StringToSqlString(text));
                }
            }

            CInterogareSQL qUpdate = new(ManagerDeProiect.GetConnection());
            m_sSql = string.Format(@"UPDATE MB_Societate SET {0} WHERE MBSO_Id = {1}", sUpdate, m_lMBSO);
            qUpdate.ExecuteStatement(m_sSql);
            qUpdate.Close();

            m_tsbSalvare.Enabled = false;
            return true;
        }



        private void Modifica()
        {
            if (m_lMBSO > 0)
            {
                m_sSql = string.Format(@"SELECT MBSO_Descriere, MBSO_CUI, MBSO_J, MBSO_Strada, MBSO_NumarStrada, MBSO_Bloc, MBSO_Scara, MBSO_Apartament, MBSO_CodPostal, MBSO_Oras, MBSO_Judet, 
                                         MBSO_Tara, MBSO_Banca, MBSO_Cont, MBSO_Reprezentant, MBSO_TelReprezentant, MBSO_EmailReprezentant, MBSO_FunctieReprezentant, MBSO_CISerieReprezentant,
                                         MBSO_CINumarReprezentant, MBSO_CNPReprezentant, MBSO_AdresaReprezentant, MBSO_EmailSocietate, MBSO_TelefonSocietate,MBSO_ResponsabilDiplome,MBSO_ResponsabilChitante,MBSO_ContTrez,MBSO_ServerIP
                                         FROM MB_Societate                                       
                                         WHERE MBSO_Id = {0}", m_lMBSO);

                CInterogareSQL q = new(ManagerDeProiect.GetConnection());
                q.Open(m_sSql);
                if (q.NextRow())
                {
                    m_ebSocietate.Text = q.GetStringValue(0);
                    m_ebCUI.Text = q.GetStringValue(1);
                    m_ebJ.Text = q.GetStringValue(2);
                    m_ebStrada.Text = q.GetStringValue(3);
                    m_ebNrStrada.Text = q.GetStringValue(4);
                    m_ebBloc.Text = q.GetStringValue(5);
                    m_ebScara.Text = q.GetStringValue(6);
                    m_ebApartament.Text = q.GetStringValue(7);
                    m_ebCodPostal.Text = q.GetStringValue(8);
                    m_ebOras.Text = q.GetStringValue(9);
                    m_ebJudet.Text = q.GetStringValue(10);
                    m_ebTara.Text = q.GetStringValue(11);
                    m_ebBanca.Text = q.GetStringValue(12);
                    m_ebCont.Text = q.GetStringValue(13);
                    m_ebReprezentant.Text = q.GetStringValue(14);
                    m_ebTelReprezentant.Text = q.GetStringValue(15);
                    m_ebEmailReprezentant.Text = q.GetStringValue(16);
                    m_ebFunctieReprezentant.Text = q.GetStringValue(17);
                    m_ebCISerieReprezentant.Text = q.GetStringValue(18);
                    m_ebCINRReprezentant.Text = q.GetStringValue(19);
                    m_ebCNPReprezentant.Text = q.GetStringValue(20);
                    m_ebAdresaReprezentant.Text = q.GetStringValue(21);
                    m_ebAdresaMailSocietate.Text = q.GetStringValue(22);
                    m_ebTelefonSoc.Text = q.GetStringValue(23);
                    m_ebResponsabilDiplome.Text = q.GetStringValue(24);
                    m_ebResponsabilChitante.Text = q.GetStringValue(25);
                    m_ebContTrez.Text = q.GetStringValue(26);
                    m_ebIPServer.Text = q.GetStringValue(27);
                }
                q.Close();
            }

            m_tsbSalvare.Enabled = false;
        }

        private void FrmGestiuneSocietate_Load(object sender, EventArgs e)
        {
            Modifica();
        }

        private void m_ebSocietate_TextChanged(object sender, EventArgs e)
        {
            m_tsbSalvare.Enabled = true;
        }

        private void m_ebCUI_TextChanged(object sender, EventArgs e)
        {
            m_tsbSalvare.Enabled = true;
        }

        private void m_ebJ_TextChanged(object sender, EventArgs e)
        {
            m_tsbSalvare.Enabled = true;
        }

        private void m_ebStrada_TextChanged(object sender, EventArgs e)
        {
            m_tsbSalvare.Enabled = true;
        }

        private void m_ebNrStrada_TextChanged(object sender, EventArgs e)
        {
            m_tsbSalvare.Enabled = true;
        }

        private void m_ebBloc_TextChanged(object sender, EventArgs e)
        {
            m_tsbSalvare.Enabled = true;
        }

        private void m_ebScara_TextChanged(object sender, EventArgs e)
        {
            m_tsbSalvare.Enabled = true;
        }

        private void m_ebApartament_TextChanged(object sender, EventArgs e)
        {
            m_tsbSalvare.Enabled = true;
        }

        private void m_ebCodPostal_TextChanged(object sender, EventArgs e)
        {
            m_tsbSalvare.Enabled = true;
        }

        private void m_ebOras_TextChanged(object sender, EventArgs e)
        {
            m_tsbSalvare.Enabled = true;
        }

        private void m_ebJudet_TextChanged(object sender, EventArgs e)
        {
            m_tsbSalvare.Enabled = true;
        }

        private void m_ebTara_TextChanged(object sender, EventArgs e)
        {
            m_tsbSalvare.Enabled = true;
        }

        private void m_ebBanca_TextChanged(object sender, EventArgs e)
        {
            m_tsbSalvare.Enabled = true;
        }

        private void m_ebCont_TextChanged(object sender, EventArgs e)
        {
            m_tsbSalvare.Enabled = true;
        }

        private void m_ebContTrez_TextChanged(object sender, EventArgs e)
        {
            m_tsbSalvare.Enabled = true;
        }

        private void m_ebAdresaMailSocietate_TextChanged(object sender, EventArgs e)
        {
            m_tsbSalvare.Enabled = true;
        }

        private void m_ebTelefonSoc_TextChanged(object sender, EventArgs e)
        {
            m_tsbSalvare.Enabled = true;
        }

        private void m_ebReprezentant_TextChanged(object sender, EventArgs e)
        {
            m_tsbSalvare.Enabled = true;
        }

        private void m_ebTelReprezentant_TextChanged(object sender, EventArgs e)
        {
            m_tsbSalvare.Enabled = true;
        }

        private void m_ebEmailReprezentant_TextChanged(object sender, EventArgs e)
        {
            m_tsbSalvare.Enabled = true;
        }

        private void m_ebFunctieReprezentant_TextChanged(object sender, EventArgs e)
        {
            m_tsbSalvare.Enabled = true;
        }

        private void m_ebCISerieReprezentant_TextChanged(object sender, EventArgs e)
        {
            m_tsbSalvare.Enabled = true;
        }

        private void m_ebCINRReprezentant_TextChanged(object sender, EventArgs e)
        {
            m_tsbSalvare.Enabled = true;
        }

        private void m_ebCNPReprezentant_TextChanged(object sender, EventArgs e)
        {
            m_tsbSalvare.Enabled = true;
        }

        private void m_ebResponsabilDiplome_TextChanged(object sender, EventArgs e)
        {
            m_tsbSalvare.Enabled = true;
        }

        private void m_ebResponsabilChitante_TextChanged(object sender, EventArgs e)
        {
            m_tsbSalvare.Enabled = true;
        }

        private void m_ebIPServer_TextChanged(object sender, EventArgs e)
        {
            m_tsbSalvare.Enabled = true;
        }

        private void m_ebAdresaReprezentant_TextChanged(object sender, EventArgs e)
        {
            m_tsbSalvare.Enabled = true;
        }
    }
}

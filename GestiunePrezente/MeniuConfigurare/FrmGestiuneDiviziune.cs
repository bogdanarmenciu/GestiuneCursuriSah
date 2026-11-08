using GestiunePrezente.Common;
using GestiunePrezente.MeniuPrincipal;

namespace GestiunePrezente.MeniuConfigurare
{
    public partial class FrmGestiuneDiviziune : Form
    {
        readonly FrmMeniuPrincipal FrmParent;
        readonly CManagerDeProiect ManagerDeProiect;
        int m_lMBDV = 0;
        string? m_sSql;

        public const string MBME_NUME = "Gestiune Diviziune";
        public static bool PoateDeschide(CManagerDeProiect mm)
            => DrepturiFormular.PoateDeschide(mm, MBME_NUME);

        public FrmGestiuneDiviziune(FrmMeniuPrincipal FormParent, CManagerDeProiect MM, int lMBDV)
        {
            FrmParent = FormParent;
            ManagerDeProiect = MM;
            m_lMBDV = lMBDV;

            InitializeComponent();
        }

        private void m_tsbSalvare_Click(object sender, EventArgs e)
        {
            Salveaza();
        }
        private bool Salveaza()
        {
            string? sData, sSql, sUpdate = "", sTmp, sDiviziune, sTelefon, sAdresa, sMail;

            //UTILIZATOR
            sData = m_ebDiviziune.Text;
            if (sData == "")
            {
                CFormatareDate.ShowWarning("IDS_ERR_DIVIZIUNE");
                m_ebDiviziune.Focus();
                return false;
            }
            else
            {
                sDiviziune = string.Format(" '{0}' ", CFormatareDate.StringToSqlString(sData));
                sTmp = string.Format("MBDV_Diviziune = '{0}' ", CFormatareDate.StringToSqlString(sData));
                sUpdate += sTmp;
            }


            sData = m_ebTelefon.Text;
            if (sData == "")
            {
                CFormatareDate.ShowWarning("IDS_ERR_DIVIZIUNE_TELEFON");
                m_ebTelefon.Focus();
                return false;
            }
            else
            {
                sTelefon = string.Format(" '{0}' ", CFormatareDate.StringToSqlString(sData));
                sTmp = string.Format(",MBDV_TelefonMobil = '{0}' ", CFormatareDate.StringToSqlString(sData));
                sUpdate += sTmp;
            }

            sData = m_ebAdresa.Text;
            if (sData == "")
            {
                CFormatareDate.ShowWarning("IDS_ERR_DIVIZIUNE_ADRESA");
                m_ebAdresa.Focus();
                return false;
            }
            else
            {
                sAdresa = string.Format(" N'{0}' ", CFormatareDate.StringToSqlString(sData));
                sTmp = string.Format(",MBDV_Adresa = N'{0}' ", CFormatareDate.StringToSqlString(sData));
                sUpdate += sTmp;
            }

            sData = m_ebAdresaMail.Text;
            if (sData == "")
            {
                CFormatareDate.ShowWarning("IDS_ERR_DIVIZIUNE_MAIL");
                m_ebAdresaMail.Focus();
                return false;
            }
            else
            {
                sMail = string.Format(" '{0}' ", CFormatareDate.StringToSqlString(sData));
                sTmp = string.Format(",MBDV_Mail = '{0}' ", CFormatareDate.StringToSqlString(sData));
                sUpdate += sTmp;
            }

            //TEST DACA EXISTA DIVIZIUNEA
            if (!ExistaDiviziune())
                return false;


            if (m_lMBDV == 0)//insert
            {
                CInterogareSQL q = new(ManagerDeProiect.GetConnection());
                m_sSql = string.Format(@" INSERT INTO MB_Diviziune(MBDV_Diviziune,MBDV_TelefonMobil,MBDV_Adresa,MBDV_Mail,MBDV_MBSO_Id)
                                        SELECT {0},{1},{2},{3},{4}", sDiviziune, sTelefon, sAdresa, sMail, ManagerDeProiect.GetSocietate());

                q.ExecuteStatement(m_sSql);

                m_sSql = "SELECT SCOPE_IDENTITY() AS SID";
                m_lMBDV = q.ExecuteToInt(m_sSql);

                q.Close();
            }
            else
            {
                CInterogareSQL qUpdate = new(ManagerDeProiect.GetConnection());
                sSql = string.Format(@"UPDATE MB_Diviziune SET {0} WHERE MBDV_Id = {1}", sUpdate, m_lMBDV);

                qUpdate.ExecuteStatement(sSql);

                qUpdate.Close();
            }

            m_tsbSalvare.Enabled = false;
            return true;
        }

        private void Modifica()
        {
            if (m_lMBDV > 0)
            {
                m_sSql = string.Format(@"SELECT MBDV_Diviziune,MBDV_TelefonMobil,MBDV_Adresa,MBDV_Mail
                                        FROM MB_Diviziune                                       
                                        WHERE MBDV_Id = {0}", m_lMBDV);
                CInterogareSQL q = new(ManagerDeProiect.GetConnection());
                q.Open(m_sSql);
                if (q.NextRow())
                {
                    m_ebDiviziune.Text = q.GetStringValue(0);
                    m_ebTelefon.Text = q.GetStringValue(1);
                    m_ebAdresa.Text = q.GetStringValue(2);
                    m_ebAdresaMail.Text = q.GetStringValue(3);
                }
                q.Close();
            }

            m_tsbSalvare.Enabled = false;
        }

        private void FrmGestiuneDiviziune_FormClosing(object sender, FormClosingEventArgs e)
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

        private void FrmGestiuneDiviziune_Load(object sender, EventArgs e)
        {
            Modifica();
        }

        private bool ExistaDiviziune()
        {
            //TEST DACA MAI EXISTA DIVIZIUNEA
            string sDiviziune = m_ebDiviziune.Text;

            if (m_lMBDV > 0)//UPDATE
            {
                m_sSql = string.Format(@"SELECT MBDV_Id FROM MB_Diviziune WHERE MBDV_Diviziune='{0}' AND MBDV_Id<>{1}", sDiviziune, m_lMBDV);
            }
            else//INSERT
            {
                m_sSql = string.Format(@"SELECT MBDV_Id FROM MB_Diviziune WHERE MBDV_Diviziune='{0}'", sDiviziune);
            }
            CInterogareSQL q = new(ManagerDeProiect.GetConnection());
            q.Open(m_sSql);
            if (q.NextRow())
            {
                CFormatareDate.ShowWarning("IDS_ERR_EXISTADIVIZIUNE");
                m_ebDiviziune.Focus();
                return false;
            }
            q.Close();

            return true;
        }

        private void m_ebDiviziune_TextChanged(object sender, EventArgs e)
        {
            m_tsbSalvare.Enabled = true;
        }

        private void m_ebTelefon_TextChanged(object sender, EventArgs e)
        {
            m_tsbSalvare.Enabled = true;
        }

        private void m_ebAdresa_TextChanged(object sender, EventArgs e)
        {
            m_tsbSalvare.Enabled = true;
        }

        private void m_ebAdresaMail_TextChanged(object sender, EventArgs e)
        {
            m_tsbSalvare.Enabled = true;
        }
    }
}

using GestiunePrezente.Common;
using GestiunePrezente.MeniuPrincipal;

namespace GestiunePrezente.MeniuCursanti
{
    public partial class FrmGestiuneFormator : Form
    {
        readonly CManagerDeProiect ManagerDeProiect;
        readonly FrmMeniuPrincipal FrmParent;
        int m_lCUFO = 0;
        readonly int m_lMBME = 0;
        private string? sSql;
        private readonly string? m_sSql;

        public const string MBME_NUME = "Gestiune Formator";
        public static bool PoateDeschide(CManagerDeProiect mm)
            => DrepturiFormular.PoateDeschide(mm, MBME_NUME);

        public FrmGestiuneFormator(FrmMeniuPrincipal FormParent, CManagerDeProiect MM, int lCUFO, int lMBME = 13)
        {
            FrmParent = FormParent;
            ManagerDeProiect = MM;
            m_lCUFO = lCUFO;
            m_lMBME = lMBME;
            InitializeComponent();
        }

        private void m_ebNume_TextChanged(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = true;
        }

        private void m_ebPrenume_TextChanged(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = true;
        }

        private void m_ebTelMobil_TextChanged(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = true;
        }

        private void m_stbSalveaza_Click(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = !Salveaza();
        }

        private bool Salveaza()
        {
            string sData, sSql, sUpdate = "", sTmp, sNume, sPrenume, sTelMobil;

            //NUME
            sData = m_ebNume.Text;
            if (sData == "")
            {
                CFormatareDate.ShowWarning("IDS_ERR_NUMEFORMATOR");
                m_ebNume.Focus();
                return false;
            }
            else
            {
                sNume = string.Format(" '{0}' ", CFormatareDate.StringToSqlString(sData));
                sTmp = string.Format(" CUFO_Nume = '{0}'", CFormatareDate.StringToSqlString(sData));
                sUpdate += sTmp;
            }
            //PRENUME
            sData = m_ebPrenume.Text;
            if (sData == "")
            {
                CFormatareDate.ShowWarning("IDS_ERR_PRENUMEFORMATOR");
                m_ebPrenume.Focus();
                return false;
            }
            else
            {
                sPrenume = string.Format(" '{0}' ", CFormatareDate.StringToSqlString(sData));
                sTmp = string.Format(" ,CUFO_Prenume = '{0}'", CFormatareDate.StringToSqlString(sData));
                sUpdate += sTmp;
            }

            // CUCU_TelefonMobil
            sData = m_ebTelMobil.Text;
            if (sData == "")
            {
                sTelMobil = string.Format("NULL");
                sTmp = string.Format(" ,CUFO_TelefonMobil = NULL");
                sUpdate += sTmp;

            }
            else
            {
                sTelMobil = string.Format(" '{0}' ", CFormatareDate.StringToSqlString(sData));
                sTmp = string.Format(" ,CUFO_TelefonMobil ='{0}'", CFormatareDate.StringToSqlString(sData));
                sUpdate += sTmp;
            }

            if (m_lCUFO == 0)//insert
            {
                CInterogareSQL q = new(ManagerDeProiect.GetConnection());
                sSql = string.Format(@" INSERT INTO CU_Formatori 
                                            (CUFO_MBSO_Id,CUFO_MBDV_Id, CUFO_Nume, CUFO_Prenume, CUFO_TelefonMobil, CUFO_MBUT_Id,CUFO_DataSalvarii,CUFO_NrMatricol)
                                            SELECT {0},{1},{2},{3},{4},{5},GETDATE(),ISNULL((SELECT MAX(CUFO_NrMatricol)+1 FROM CU_Formatori),1)",
                                            ManagerDeProiect.GetSocietate(), ManagerDeProiect.GetDiviziune(), sNume, sPrenume, sTelMobil, ManagerDeProiect.GetUserId());


                q.ExecuteStatement(sSql);

                sSql = "SELECT SCOPE_IDENTITY() AS SID";
                m_lCUFO = q.ExecuteToInt(sSql);

                q.Close();

            }
            else
            {
                sTmp = string.Format(" ,CUFO_MBUT_Id = {0},CUFO_DataSalvarii=GETDATE()", ManagerDeProiect.GetUserId());
                sUpdate += sTmp;

                CInterogareSQL qUpdate = new(ManagerDeProiect.GetConnection());
                sSql = string.Format(@"UPDATE CU_Formatori SET {0} WHERE CUFO_Id = {1}", sUpdate, m_lCUFO);

                qUpdate.ExecuteStatement(sSql);

                qUpdate.Close();
            }

            return true;
        }

        private void FrmGestiuneFormator_Load(object sender, EventArgs e)
        {
            Modifica();
        }

        private void Modifica()
        {
            if (m_lCUFO > 0)
            {
                sSql = string.Format(@"SELECT CUFO_Nume, CUFO_Prenume, CUFO_TelefonMobil
                                       FROM CU_Formatori        
                                       WHERE CUFO_Id = {0}", m_lCUFO);
                int i = -1;
                CInterogareSQL q = new(ManagerDeProiect.GetConnection());
                q.Open(sSql);
                if (q.NextRow())
                {
                    m_ebNume.Text = q.GetStringValue(++i);
                    m_ebPrenume.Text = q.GetStringValue(++i);
                    m_ebTelMobil.Text = q.GetStringValue(++i);
                }
                q.Close();
            }

            m_stbSalveaza.Enabled = false;
        }
    }
}

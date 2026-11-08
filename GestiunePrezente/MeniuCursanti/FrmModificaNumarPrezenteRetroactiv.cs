using GestiunePrezente.Common;
using GestiunePrezente.MeniuPrincipal;

namespace GestiunePrezente.MeniuCursanti
{
    public partial class FrmModificaNumarPrezenteRetroactiv : Form
    {
        readonly CManagerDeProiect ManagerDeProiect;
        readonly FrmMeniuPrincipal FrmParent;
        string? m_sSql;
        private readonly int m_lCUCU = 0;
        private readonly int m_lCUAC = 0;
        int m_lFormaDeschisa = 0;
        public FrmModificaNumarPrezenteRetroactiv(FrmMeniuPrincipal FormParent, CManagerDeProiect MM, int lCUCU, int lCUAC)
        {
            FrmParent = FormParent;
            ManagerDeProiect = MM;
            m_lCUCU = lCUCU;
            m_lCUAC = lCUAC;
            InitializeComponent();
        }

        private void m_ebNrPrezente_TextChanged(object sender, EventArgs e)
        {
            m_tsbSalveaza.Enabled = true;
        }

        private void m_tsbSalveaza_Click(object sender, EventArgs e)
        {
            string sData;

            CInterogareSQL q = new(ManagerDeProiect.GetConnection());
            m_sSql = string.Format(@"UPDATE CU_AbonamentCursanti SET CUAC_DeLaData={0},CUAC_PanaLaData={1},CUAC_DataSalvarii=GetDate(),CUAC_MBUT_Id={2}  WHERE CUAC_Id = {3}",
                                        CFormatareDate.DateToSQLString(m_dtpDataInceputAbCurent.Value),
                                        CFormatareDate.DateToSQLString(m_dtpDataExpirareAbCurent.Value), ManagerDeProiect.GetUserId(), m_lCUAC);
            q.ExecuteStatement(m_sSql);

            sData = m_ebNrPrezente.Text;
            if (sData == "")
            {
                //CFormatareDate.ShowWarning("IDS_ERR_NRPREZENTERETROACTIV");
                //m_ebNrPrezente.Focus();
                //return;
            }
            else
            {
                int lNrSedinteRetro = Convert.ToInt32(sData);
                int lNrSedinteAbonament = 0, lNrSedinteEfectuate = 0;


                sData = string.Format(" {0} ", CFormatareDate.DateTimeToSQLString(m_dtpDataExpirare.Value));


                m_sSql = string.Format(@"SELECT COUNT(*) FROM CU_AbonamentCursanti 
                                            INNER JOIN CU_DetaliuAbonamentCursanti ON CUDA_CUAC_Id=CUAC_Id
                                            WHERE CUDA_CUCU_Id = {0} AND {1} BETWEEN CUAC_DeLaData AND CUAC_PanaLaData", m_lCUCU, sData);
                int lExistaAbonament = q.ExecuteToInt(m_sSql);

                if (lExistaAbonament > 0)
                {
                    CFormatareDate.ShowWarning("IDS_ERR_DATAEXPIRAREABONAMENT");
                    q.Close();
                    return;
                }


                m_sSql = string.Format(@"SELECT CUAB_NumarSedinte                                     
                                         FROM CU_Cursanti
                                         INNER JOIN CU_Abonamente ON CUCU_CUAB_Id=CUAB_Id
                                         WHERE CUCU_Id={0}", m_lCUCU);
                q.Open(m_sSql);
                if (q.NextRow())
                {
                    lNrSedinteAbonament = q.GetIntValue(0);
                }

                m_sSql = string.Format(@"SELECT ISNULL(Prezente.NrPrezenteEfectuate,0) NrPrezenteEfectuate
                                        FROM CU_Cursanti C
                                        INNER JOIN MB_Diviziune ON CUCU_MBDV_Id=MBDV_Id 
                                        INNER JOIN CU_Abonamente ON CUCU_CUAB_Id=CUAB_Id
                                        LEFT JOIN
                                        (
                                        SELECT COUNT(*) NrPrezenteEfectuate,CUPD_CUCU_Id
                                        FROM CU_PrezenteDetaliu 
                                        INNER JOIN CU_Prezente ON CUPD_CUPR_Id=CUPR_Id
                                        WHERE CUPR_DeLaData BETWEEN DATEADD(dd, -30, CUPR_DeLaData) AND CUPR_DeLaData
                                        GROUP BY CUPD_CUCU_Id
                                        )Prezente ON CUPD_CUCU_Id=CUCU_Id
                                        WHERE CUCU_Id={0}", m_lCUCU);
                q.Open(m_sSql);
                if (q.NextRow())
                {
                    lNrSedinteEfectuate = q.GetIntValue(0);
                }

                if (lNrSedinteRetro + lNrSedinteEfectuate > lNrSedinteAbonament)
                {
                    if (MessageBox.Show(CFormatareDate.GetString("IDS_ERR_DEPASIRE_NR_SEDINTE_PERMISE"), CFormatareDate.GetString("IDS_ATENTIE"), MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        q.Close();
                        return;
                    }


                }

                if (lNrSedinteRetro <= 0)
                {
                    m_sSql = string.Format(@"UPDATE CU_Cursanti SET CUCU_NrPrezenteRetro=NULL,CUCU_DataExpirarePrezenteRetro = NULL WHERE CUCU_Id = {0}", m_lCUCU);
                    q.ExecuteStatement(m_sSql);
                }
                else
                {
                    m_sSql = string.Format(@"UPDATE CU_Cursanti SET CUCU_NrPrezenteRetro={0},CUCU_DataExpirarePrezenteRetro = {1} WHERE CUCU_Id = {2}", lNrSedinteRetro, sData, m_lCUCU);
                    q.ExecuteStatement(m_sSql);
                }



                q.Close();



                DialogResult = DialogResult.OK;

                Close();
            }
        }

        private void m_dtpDataExpirare_ValueChanged(object sender, EventArgs e)
        {
            if (m_lFormaDeschisa <= 0)
                return;

            m_tsbSalveaza.Enabled = true;

            DateTime dtExp = m_dtpDataExpirare.Value;
            dtExp = dtExp.AddDays(1);
            m_dtpDataInceputAbCurent.Value = dtExp;

            //       int lDuratazile = 0, lCUAB = 0;


            //m_sSql = string.Format(@"SELECT CUAB_Perioada  FROM CU_Abonamente WHERE CUAB_Id=(SELECT CUAC_CUAB_Id FROM CU_AbonamentCursanti WHERE CUAC_Id{0})", m_lCUAC);
            //CInterogareSQL q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            //q.Open(m_sSql);
            //if (q.NextRow())
            //{
            //    lDuratazile = q.GetIntValue(0);
            //}
            //q.Close();

            //DateTime dt = m_dtpDataInceputAbCurent.Value;
            //dt = dt.AddDays(lDuratazile - 1);
            //m_dtpDataExpirareAbCurent.Value = dt;
        }

        private void m_dtpDataExpirareAbCurent_ValueChanged(object sender, EventArgs e)
        {

            if (m_lFormaDeschisa <= 0)
                return;
            m_tsbSalveaza.Enabled = true;
        }

        private void m_dtpDataInceputAbCurent_ValueChanged(object sender, EventArgs e)
        {

            if (m_lFormaDeschisa <= 0)
                return;

            m_tsbSalveaza.Enabled = true;

            int lDuratazile = 0;


            m_sSql = string.Format(@"SELECT CUAB_Perioada  FROM CU_Abonamente WHERE CUAB_Id=(SELECT CUAC_CUAB_Id FROM CU_AbonamentCursanti WHERE CUAC_Id={0})", m_lCUAC);
            CInterogareSQL q = new(ManagerDeProiect.GetConnection());
            q.Open(m_sSql);
            if (q.NextRow())
            {
                lDuratazile = q.GetIntValue(0);
            }
            q.Close();

            DateTime dt = m_dtpDataInceputAbCurent.Value;
            dt = dt.AddDays(lDuratazile - 1);
            m_dtpDataExpirareAbCurent.Value = dt;
        }

        private void FrmModificaNumarPrezenteRetroactiv_Load(object sender, EventArgs e)
        {
            CInterogareSQL q = new(ManagerDeProiect.GetConnection());
            DateTime dtExpirare = DateTime.MinValue;
            m_sSql = string.Format(@"SELECT CUCU_NrPrezenteRetro,CUCU_DataExpirarePrezenteRetro
                                     FROM CU_Cursanti
                                    WHERE CUCU_Id={0}", m_lCUCU);
            q.Open(m_sSql);
            if (q.NextRow())
            {
                m_ebNrPrezente.Text = q.GetIntValue(0).ToString();
                if (q.GetDateValue(1) != null)
                {
                    dtExpirare = (DateTime)q.GetDateValue(1)!;
                    m_dtpDataExpirare.Value = (DateTime)q.GetDateValue(1)!;
                }

            }

            m_sSql = string.Format(@"SELECT CUAC_DeLaData-1,CUAC_PanaLaData,CUAC_DeLaData
                                     FROM CU_AbonamentCursanti
                                     INNER JOIN CU_DetaliuAbonamentCursanti ON CUDA_CUAC_Id=CUAC_Id   
                                     WHERE CUDA_CUCU_Id={0} AND CUAC_Id={1}", m_lCUCU, m_lCUAC);

            q.Open(m_sSql);
            if (q.NextRow())
            {
                if (dtExpirare == DateTime.MinValue)
                {
                    if (q.GetDateValue(0) != null)
                        m_dtpDataExpirare.Value = (DateTime)q.GetDateValue(0)!;
                }
                if (q.GetDateValue(1) != null)
                    m_dtpDataExpirareAbCurent.Value = (DateTime)q.GetDateValue(1)!;

                if (q.GetDateValue(2) != null)
                    m_dtpDataInceputAbCurent.Value = (DateTime)q.GetDateValue(2)!;


            }


            q.Close();

            m_lFormaDeschisa = 1;
        }
    }
}

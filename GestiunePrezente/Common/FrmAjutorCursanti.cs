using System.Data;
using Microsoft.Data.SqlClient;
using GestiunePrezente.MeniuPrincipal;
using GestiunePrezente.MeniuCursanti;

namespace GestiunePrezente.Common
{
    public partial class FrmAjutorCursanti : Form
    {
        readonly CManagerDeProiect ManagerDeProiect;
        readonly FrmMeniuPrincipal FrmParent;
        string m_sSql = string.Empty;
        private readonly int m_lCUCO = 0;
        private int m_lCUCU = 0;
        DateTime m_dtDataPrezentei = DateTime.MinValue;
        public FrmAjutorCursanti(FrmMeniuPrincipal FormParent, CManagerDeProiect MM, int lCUCO)
        {
            FrmParent = FormParent;
            ManagerDeProiect = MM;
            m_lCUCO = lCUCO;
            InitializeComponent();
            GridPersonalizat.CustomGrid.SetConnection(ManagerDeProiect.GetConnection());
            Cauta();
        }

        private void m_ebNume_TextChanged(object sender, EventArgs e)
        {
            Cauta();
        }

        private void m_tsbCautare_Click(object sender, EventArgs e)
        {
            Cauta();
        }
        private void m_ebDeLa_TextChanged(object sender, EventArgs e)
        {
            Cauta();
        }

        private void m_ebPanaLa_TextChanged(object sender, EventArgs e)
        {
            Cauta();
        }

        private void m_tsbModificaNrPrezente_Click(object sender, EventArgs e)
        {
            int lCUCU;
            object? vCUCU = m_gCursanti.GetColumnData("CUCI_Id");

            if (vCUCU != null)
            {
                lCUCU = Convert.ToInt32(vCUCU);

                object? value = m_gCursanti.GetColumnData("CUAC_Id");
                int lCUAC = Convert.ToInt32(value);

                FrmModificaNumarPrezenteRetroactiv frm = new(FrmParent, ManagerDeProiect, lCUCU, lCUAC);

                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    SetCUCU();
                    DialogResult = DialogResult.OK;
                    Close();
                }

                Cauta();

            }
        }

        private void m_tsbSalveaza_Click(object sender, EventArgs e)
        {
            //NUME
            object? value;
            string sData, sNume, sPrenume, sTelMobil, sCUAB, sCUFO;
            sData = m_ebNume.Text;
            if (sData == "")
            {
                CFormatareDate.ShowWarning("IDS_ERR_NUMECURSANT");
                m_ebNume.Focus();
                return;
            }
            else
            {
                sNume = string.Format(" '{0}' ", CFormatareDate.StringToSqlString(sData));
            }
            //PRENUME
            sData = m_ebPrenume.Text;
            if (sData == "")
            {
                CFormatareDate.ShowWarning("IDS_ERR_PRENUMECURSANT");
                m_ebPrenume.Focus();
                return;
            }
            else
            {
                sPrenume = string.Format(" '{0}' ", CFormatareDate.StringToSqlString(sData));
            }

            //sData = m_ebTelMobil.Text;
            //if (sData == "")
            //{
            //    sTelMobil = string.Format("NULL");

            //}
            //else
            //{
            //    sTelMobil = string.Format(" '{0}' ", CFormatareDate.StringToSqlString(sData));
            //}

            //ABONAMENT
            //sData = m_cbAbonament.Text;
            //if (sData == "")
            //{
            //    CFormatareDate.ShowWarning("IDS_ERR_ABONAMENT");
            //    m_cbAbonament.Focus();
            //    return;
            //}
            //else
            //{
            //    value = m_cbAbonament.GetColumnData("CUAB_Id");
            //    sCUAB = string.Format(" {0} ", value);
            //}


            int lCUCU = 0;
            CInterogareSQL q = new(ManagerDeProiect.GetConnection());
            m_sSql = string.Format(@"SELECT COUNT(*) 
                                     FROM CU_Cursanti
                                     WHERE CUCU_Nume={0} AND CUCU_Prenume = {1}",
                                     sNume, sPrenume);

            q.Open(m_sSql);
            if (q.NextRow())
            {
                lCUCU = q.GetIntValue(0);
            }

            if (lCUCU > 0)
            {
                CFormatareDate.ShowWarning("IDS_ERR_CURSANT_EXISTA");

                return;

            }

            //           m_sSql = string.Format(@"SELECT CUAB_Perioada  FROM CU_Abonamente  WHERE CUAB_Id = {0}", Convert.ToInt32(sCUAB));
            int lDurataZileAb = 0;
            q.Open(m_sSql);
            if (q.NextRow())
            {
                lDurataZileAb = q.GetIntValue(0);
            }

            //m_sSql = string.Format(@" INSERT INTO CU_Cursanti 
            //                            (CUCU_MBSO_Id,CUCU_MBDV_Id,  CUCU_Nume, CUCU_Prenume, CUCU_TelefonMobil,
            //                                CUCU_NrMatricol,                                          
            //                                CUCU_MBUT_Id,CUCU_DataSalvarii,CUCU_CUAB_Id)
            //                            SELECT {0},{1},{2},{3},{4}, ISNULL((SELECT MAX(CUCU_NrMatricol)+1 FROM CU_Cursanti),1),
            //                            {5},GETDATE(),{6},{7}",
            //                        ManagerDeProiect.GetSocietate(), ManagerDeProiect.GetDiviziune(), sNume, sPrenume,
            //                        sTelMobil, ManagerDeProiect.GetUserId(), sCUAB);
            sCUAB = "1"; // abonament implicit

            m_sSql = string.Format(@" INSERT INTO CU_Cursanti 
                                        (CUCU_MBSO_Id,CUCU_MBDV_Id,  CUCU_Nume, CUCU_Prenume, CUCU_TelefonMobil,
                                            CUCU_NrMatricol,                                          
                                            CUCU_MBUT_Id,CUCU_DataSalvarii,CUCU_CUAB_Id)
                                        SELECT {0},{1},{2},{3},{4}, ISNULL((SELECT MAX(CUCU_NrMatricol)+1 FROM CU_Cursanti),1),
                                        {5},GETDATE(),{6},{7}",
                        ManagerDeProiect.GetSocietate(), ManagerDeProiect.GetDiviziune(), sNume, sPrenume, ManagerDeProiect.GetUserId(), sCUAB);


            q.ExecuteStatement(m_sSql);



            m_sSql = "SELECT SCOPE_IDENTITY() AS SID";
            // m_lCUCC = q.ExecuteToInt(m_sSql);

            SetCUCU(1);

            //SALVEAZA ABONAMENT


            SqlCommand cmd;
            cmd = new SqlCommand("CU_InsereazaAbonamentCursant", ManagerDeProiect.GetConnection())
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@lCUCU", m_lCUCO);
            //         cmd.Parameters.AddWithValue("@dtDeLaDataAb", dtInceputAb);
            //          cmd.Parameters.AddWithValue("@dtPanaLaDataAb", dtSfarsitAb);
            cmd.Parameters.AddWithValue("@lMBSO", ManagerDeProiect.GetSocietate());
            cmd.Parameters.AddWithValue("@lMBDV", ManagerDeProiect.GetDiviziune());
            cmd.Parameters.AddWithValue("@lCUAB", Convert.ToInt32(sCUAB));
            cmd.Parameters.AddWithValue("@lMBUT", ManagerDeProiect.GetUserId());
            cmd.CommandTimeout = 0;
            cmd.ExecuteNonQuery();

            Cauta(1);

            m_tsbSalveaza.Enabled = false;
            //  DialogResult = DialogResult.OK;

            //  Close();
        }

        private void m_gCursanti_DoubleClick(object sender, EventArgs e)
        {
            if (SetCUCU() > 0)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        public int SetCUCU(int bFromInsert = 0)
        {
            if (bFromInsert == 1)
                return m_lCUCO;

            m_lCUCU = 0;
            object? value = m_gCursanti.GetColumnData("CUCU_Id");
            if (value != null && value != DBNull.Value)
                m_lCUCU = Convert.ToInt32(value);

            return m_lCUCU;
        }

        public int GetCUCU()
        {
            return m_lCUCU;
        }

        private string ConstruireFiltru()
        {
            string sFiltru = "";

            void AdaugaFiltru(string text, string sqlTemplate)
            {
                if (!string.IsNullOrWhiteSpace(text))
                    sFiltru += string.Format(sqlTemplate, CFormatareDate.StringToSqlString(text));
            }

            AdaugaFiltru(m_ebNume.Text, " AND CUCU_Nume LIKE N'{0}%' ");
            AdaugaFiltru(m_ebPrenume.Text, " AND CUCU_Prenume LIKE N'{0}%' ");
            AdaugaFiltru(m_ebDeLa.Text, " AND CUCU_NrMatricol >= {0} ");
            AdaugaFiltru(m_ebPanaLa.Text, " AND CUCU_NrMatricol <= {0} ");

            return sFiltru;
        }
        private void Cauta(int lFromInsert = 0)
        {
            string sFiltru = ConstruireFiltru();

            // SQL neatins
            if (lFromInsert == 0)
                m_sSql = string.Format(@"SELECT ROW_NUMBER() OVER (ORDER BY CUCU_Nume, CUCU_Prenume),
                                                 CUCU_Id, CUCU_Nume, CUCU_Prenume, CUCU_NrMatricol, CUCU_CNP
                                          FROM CU_Cursanti
                                          WHERE CUCU_Id NOT IN (SELECT CUCC_CUCU_Id FROM CU_ContracteCursanti WHERE CUCC_CUCO_Id={1}) {0}
                                          ORDER BY CUCU_Nume,CUCU_Prenume", sFiltru, m_lCUCO);
            else
                // nu va intra pe acest else niciodata pe ce cod avem noi 
                m_sSql = string.Format(@"SELECT DISTINCT ROW_NUMBER() OVER (ORDER BY CUCU_Nume, CUCU_Prenume) AS Row,CUCU_Id,
                                           CUCU_NrMatricol, CUCU_Nume, CUCU_Prenume, CUCU_CNP, CUCU_TelefonMobil, CUCU_Email,
                                           MBDV_Diviziune AS Oras,TAbonamentActiv   .CUAC_DeLaData,TAbonamentActiv.CUAC_PanaLaData,
                                           ISNULL(Prezente.NrPrezenteEfectuate,0)+
                                           CASE 
                                                WHEN ISNULL(CUCU_DataExpirarePrezenteRetro,convert(date, '19000101', 102))=AB.CUAC_DeLaData-1
                                                THEN ISNULL(CUCU_NrPrezenteRetro,0) ELSE 0 END
                                           NrPrezenteEfectuate,AB.CUAC_Id, CUFO_Nume+' '+CUFO_Prenume AS Formator
                                         FROM CU_Cursanti C
                                         INNER JOIN MB_Diviziune ON CUCU_MBDV_Id=MBDV_Id 
                                         INNER JOIN CU_DetaliuAbonamentCursanti ON CUDA_CUCU_Id=CUCU_Id
                                         INNER JOIN CU_AbonamentCursanti AB ON CUDA_CUAC_Id=AB.CUAC_Id
                                         INNER JOIN CU_Formatori ON C.CUCU_CUFO_Id=CUFO_Id 
                                         INNER JOIN CU_Abonamente ON AB.CUAC_CUAB_Id=CUAB_Id
                                         LEFT JOIN (...) 
                                         WHERE CUCU_CUFO_Id = (SELECT CUPR_CUFO_Id FROM CU_Prezente WHERE CUPR_Id={1}) {0}
                                         ORDER BY CUCU_Nume, CUCU_Prenume", sFiltru, m_lCUCO);

            m_gCursanti.SetSql(m_sSql);

            m_gCursanti.FormatColumn("Nr. crt", 50, colAlign: DataGridViewContentAlignment.MiddleRight);
            m_gCursanti.FormatColumn("CUCU_Id", 70, bVisible: false);
            m_gCursanti.FormatColumn("Nume", 150);
            m_gCursanti.FormatColumn("Prenume", 150);
            m_gCursanti.FormatColumn("Nr.Matricol", 70, colAlign: DataGridViewContentAlignment.MiddleRight);
            m_gCursanti.FormatColumn("CNP", 100);

            m_gCursanti.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
    }
}

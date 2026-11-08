using System.Data;
using Microsoft.Data.SqlClient;
using GestiunePrezente.MeniuPrincipal;
using GestiunePrezente.MeniuCursanti;

namespace GestiunePrezente.Common
{
    public partial class FrmAjutorCursantiPrezenta : Form
    {
        readonly CManagerDeProiect ManagerDeProiect;
        readonly FrmMeniuPrincipal FrmParent;
        string m_sSql = string.Empty;
        private int m_lCUCU = 0, m_lCUCO = 0;
        private readonly int m_lCUPR = 0;
        DateTime m_dtDataPrezentei = DateTime.MinValue;
        private bool m_bSedintaInd = false;
        public FrmAjutorCursantiPrezenta(FrmMeniuPrincipal FormParent, CManagerDeProiect MM, int lCUPR)
        {
            FrmParent = FormParent;
            ManagerDeProiect = MM;
            m_lCUPR = lCUPR;
            InitializeComponent();
            GridPersonalizat.CustomGrid.SetConnection(ManagerDeProiect.GetConnection());
            Cauta();
        }

        private void FrmAjutorCursantiPrezenta_Load(object sender, EventArgs e)
        {
            m_sSql = string.Format(@"SELECT CUAB_Id,CUAB_Descriere
                                        FROM CU_Abonamente
                                        ORDER BY CUAB_Descriere");
            GridPersonalizat.CustomCombo.SetConnection(ManagerDeProiect.GetConnection());
            m_cbAbonament.SetSql(m_sSql);


            m_sSql = string.Format(@"SELECT CUPR_CUFO_Id, CUPR_DeLaData,
                                             ISNULL(TP.CUTP_SedintaIndividuala, 0)
                                        FROM CU_Prezente
                                        INNER JOIN CU_TipPrezenta TP ON CUPR_CUTP_Id = TP.CUTP_Id
                                       WHERE CUPR_Id={0}", m_lCUPR);
            CInterogareSQL q = new(ManagerDeProiect.GetConnection());
            q.Open(m_sSql);
            if (q.NextRow())
            {
                DateTime? dateTimeNullable = q.GetDateValue(1);
                if (dateTimeNullable.HasValue)
                {
                    DateTime dateTime = dateTimeNullable.Value;
                    m_dtDataPrezentei = dateTime;
                }
                else
                {
                    m_dtDataPrezentei = DateTime.MinValue;
                }
                m_bSedintaInd = q.GetIntValue(2) == 1;
            }
            q.Close();
        }
        private void m_ebNume_TextChanged(object sender, EventArgs e)
        {
            Cauta();
        }

        private void m_tsbCautare_Click(object sender, EventArgs e)
        {
            Cauta();
        }
        private void m_cbAbonament_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cauta();
        }
        private void m_gCursanti_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (SetCUCU() > 0 && SetCUCO() > 0)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }

        private void m_tsbModificaNrPrezente_Click(object sender, EventArgs e)
        {
            int lCUCU;
            //     int lRow = m_gCursanti.GetCurSelRow();
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

            sData = m_ebTelMobil.Text;
            if (sData == "")
            {
                sTelMobil = string.Format("NULL");

            }
            else
            {
                sTelMobil = string.Format(" '{0}' ", CFormatareDate.StringToSqlString(sData));
            }

            //ABONAMENT
            sData = m_cbAbonament.Text;
            if (sData == "")
            {
                CFormatareDate.ShowWarning("IDS_ERR_ABONAMENT");
                m_cbAbonament.Focus();
                return;
            }
            else
            {
                value = m_cbAbonament.GetColumnData("CUAB_Id");
                sCUAB = string.Format(" {0} ", value);
            }


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

            m_sSql = string.Format(@"SELECT CUAB_Perioada  FROM CU_Abonamente  WHERE CUAB_Id = {0}", Convert.ToInt32(sCUAB));
            int lDurataZileAb = 0;
            q.Open(m_sSql);
            if (q.NextRow())
            {
                lDurataZileAb = q.GetIntValue(0);
            }

            m_sSql = string.Format(@" INSERT INTO CU_Cursanti 
                                        (CUCU_MBSO_Id,CUCU_MBDV_Id,  CUCU_Nume, CUCU_Prenume, CUCU_TelefonMobil,
                                            CUCU_NrMatricol,                                          
                                            CUCU_MBUT_Id,CUCU_DataSalvarii,CUCU_CUAB_Id)
                                        SELECT {0},{1},{2},{3},{4}, ISNULL((SELECT MAX(CUCU_NrMatricol)+1 FROM CU_Cursanti),1),
                                        {5},GETDATE(),{6},{7}",
                                    ManagerDeProiect.GetSocietate(), ManagerDeProiect.GetDiviziune(), sNume, sPrenume,
                                    sTelMobil, ManagerDeProiect.GetUserId(), sCUAB);


            q.ExecuteStatement(m_sSql);



            m_sSql = "SELECT SCOPE_IDENTITY() AS SID";
            m_lCUCU = q.ExecuteToInt(m_sSql);

            SetCUCU(1);

            //SALVEAZA ABONAMENT


            SqlCommand cmd;
            cmd = new SqlCommand("CU_InsereazaAbonamentCursant", ManagerDeProiect.GetConnection())
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@lCUCU", m_lCUCU);
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
            if (SetCUCU() > 0 && SetCUCO() > 0)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private int Update(string columnName, ref int memberVar, int bFromInsert)
        {
            if (bFromInsert == 1)
                return memberVar;

            memberVar = 0;
            object? value = m_gCursanti.GetColumnData(columnName);

            if (value != null && value != DBNull.Value)
                memberVar = Convert.ToInt32(value);

            return memberVar;
        }

        public int SetCUCU(int bFromInsert = 0) => Update("CUCU_Id", ref m_lCUCU, bFromInsert);
        public int SetCUCO(int bFromInsert = 0) => Update("CUCO_Id", ref m_lCUCO, bFromInsert);
        public int GetCUCU()
        {
            return m_lCUCU;
        }

        public int GetCUCO()
        {
            return m_lCUCO;
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
            AdaugaFiltru(m_ebTelMobil.Text, " AND CUCU_TelefonMobil LIKE '{0}%' ");

            if (!string.IsNullOrWhiteSpace(m_cbAbonament.Text))
            {
                var cuabIdObj = m_cbAbonament.GetColumnData("CUAB_Id");
                if (cuabIdObj != null && cuabIdObj != DBNull.Value)
                    sFiltru += string.Format(" AND CUCO_CUAB_Id = {0} ", cuabIdObj);
            }

            return sFiltru;
        }
        private void Cauta(int lFromInsert = 0)
        {
            string sFiltru = ConstruireFiltru();

            // SQL filtrat dupa tipul sedintei (individual vs grup) via CU_AbonamenteCursant
            if (lFromInsert == 0)
            {
                int sedInd = m_bSedintaInd ? 1 : 0;
                m_sSql = string.Format(@"SELECT DISTINCT ROW_NUMBER() OVER (ORDER BY CUCU_Nume, CUCU_Prenume) AS Row,
                                          CUCU_Id, CUCO_Id, CUCU_Nume, CUCU_Prenume, CUCU_CNP, CUCU_TelefonMobil, CUCU_NrMatricol, CUAB_Descriere, CUCU_Email,
                                          MBDV_Diviziune AS Oras
                                     FROM CU_Cursanti
                                     INNER JOIN CU_ContracteCursanti ON CUCC_CUCU_Id=CUCU_Id AND ISNULL(CUCC_Retras,0)=0
                                     INNER JOIN CU_Contracte ON CUCC_CUCO_Id=CUCO_Id AND CUCO_Retras=0
                                     INNER JOIN MB_Diviziune ON CUCU_MBDV_Id=MBDV_Id
                                     INNER JOIN CU_AbonamenteCursant CUAC ON CUAC.CUAC_CUCO_Id=CUCO_Id
                                     INNER JOIN CU_Abonamente ON CUAC.CUAC_CUAB_Id=CUAB_Id AND CUAB_SedintaIndividuala={2}
                                     WHERE CUCU_Id NOT IN (SELECT CUPD_CUCU_Id FROM CU_PrezenteDetaliu WHERE CUPD_CUPR_Id={1})
                                       AND (SELECT CONVERT(DATE,CUPR_DeLaData) FROM CU_Prezente WHERE CUPR_Id={1})
                                           BETWEEN CUAC.CUAC_DataInceput AND CUAC.CUAC_DataSfarsit
                                       {0}
                                     ORDER BY CUCU_Nume, CUCU_Prenume", sFiltru, m_lCUPR, sedInd);
            }
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
                                         ORDER BY CUCU_Nume, CUCU_Prenume", sFiltru, m_lCUPR);

            m_gCursanti.SetSql(m_sSql);

            // Configurare coloane elegantă
            m_gCursanti.FormatColumn("Nr. crt", 50, colAlign: DataGridViewContentAlignment.MiddleRight);
            m_gCursanti.FormatColumn("CUCU_Id", 70, bVisible: false);
            m_gCursanti.FormatColumn("CUCO_Id", 70, bVisible: false);
            m_gCursanti.FormatColumn("Nume", 150);
            m_gCursanti.FormatColumn("Prenume", 150);
            m_gCursanti.FormatColumn("CNP", 100);
            m_gCursanti.FormatColumn("Telefon Mobil", 100);
            m_gCursanti.FormatColumn("Nr.Matricol", 70, colAlign: DataGridViewContentAlignment.MiddleRight);
            m_gCursanti.FormatColumn("Tip Abonament", 150);
            m_gCursanti.FormatColumn("Email", 150);
            m_gCursanti.FormatColumn("Oras", 100);

            m_gCursanti.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
    }
}

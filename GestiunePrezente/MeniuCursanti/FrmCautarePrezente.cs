using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using GestiunePrezente.Common;
using GestiunePrezente.MeniuPrincipal;
using System.Diagnostics.Contracts;

namespace GestiunePrezente.MeniuCursanti
{
    public partial class FrmCautarePrezente : Form
    {
        readonly CManagerDeProiect ManagerDeProiect;
        readonly FrmMeniuPrincipal FrmParent;
        string? m_sSql;
        readonly int m_lMBME = 8;
        public FrmCautarePrezente(FrmMeniuPrincipal FormParent, CManagerDeProiect MM)
        {
            FrmParent = FormParent;
            ManagerDeProiect = MM;
            InitializeComponent();
            GridPersonalizat.CustomGrid.SetConnection(ManagerDeProiect.GetConnection());


            //  if (ManagerDeProiect.GetRolUser_Administrator() != 1)
            //       m_tsbSterge.Enabled = false;

            m_ckActivareFiltruData.Checked = true;
            m_dtpDeLaData.Value = DateTime.Today.AddDays(-7);
            CautaPrezenta();
        }

        private void FrmCautarePrezente_Load(object sender, EventArgs e)
        {
            m_sSql = string.Format(@"SELECT CUFO_Id,CUFO_Nume+' '+CUFO_Prenume
                                        FROM CU_Formatori
                                        ORDER BY CUFO_Nume,CUFO_Prenume");
            GridPersonalizat.CustomCombo.SetConnection(ManagerDeProiect.GetConnection());
            m_cbFormator.SetSql(m_sSql);
        }

        private void m_ebNumeCursant_TextChanged(object sender, EventArgs e)
        {
            CautaPrezenta();
        }

        private void m_ebPrenumeCursant_TextChanged(object sender, EventArgs e)
        {
            CautaPrezenta();
        }
        private void m_cbFormator_SelectedIndexChanged(object sender, EventArgs e)
        {
            CautaPrezenta();
        }
        private void m_tsbCautare_Click(object sender, EventArgs e)
        {
            CautaPrezenta();
        }
        private void m_tsbNou_Click(object sender, EventArgs e)
        {
            if (!FrmGestiunePrezenta.PoateDeschide(ManagerDeProiect)) return;
            FrmGestiunePrezenta frm = new(FrmParent, ManagerDeProiect, 0)
            {
                MdiParent = FrmParent
            };
            frm.Show();
        }

        private void m_tsbSterge_Click(object sender, EventArgs e)
        {
            int lCUPR;
            object? value = m_gPrezente.GetColumnData("CUPR_Id");
            if (value == null) return;

            lCUPR = Convert.ToInt32(value);
            if (MessageBox.Show(CFormatareDate.GetString("IDS_PREZENTA_ASK_DELETE"), CFormatareDate.GetString("IDS_ATENTIE"), MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            CInterogareSQL q = new(ManagerDeProiect.GetConnection());

            m_sSql = string.Format(@"
                                    SELECT COUNT(*)
                                    FROM CU_Prezente
                                    WHERE CUPR_Id = {0}
                                      AND CUPR_DeLaData >= CAST(GETDATE() AS DATE)
                                      AND CUPR_DeLaData < DATEADD(DAY, 1, CAST(GETDATE() AS DATE))
                                ", lCUPR);

            int lFolositIeri = q.ExecuteToInt(m_sSql);

            if (lFolositIeri == 0)
            {
                CFormatareDate.ShowWarning("Prezenta poate fi stearsa doar in ziua curenta!");
                return;
            }

            //         SalveazaIstoric(lCUPR);

            m_sSql = string.Format(@"DELETE FROM CU_PrezenteDetaliu WHERE CUPD_CUPR_Id={0}", lCUPR);
            q.ExecuteStatement(m_sSql);
            q.Close();


            m_sSql = string.Format(@"DELETE FROM CU_Prezente WHERE CUPR_Id={0}", lCUPR);
            q.ExecuteStatement(m_sSql);
            q.Close();

            CautaPrezenta();

        }

        private void m_gPrezente_DoubleClick(object sender, EventArgs e)
        {
            Modifica();
        }

        private void m_gPrezente_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                SchimbaRandulPrezenta(e.RowIndex);
            }
        }



        private void Modifica()
        {
            int lCUPR;
            object? value = m_gPrezente.GetColumnData("CUPR_Id");
            if (value != null)
            {
                lCUPR = Convert.ToInt32(value);
                if (!FrmGestiunePrezenta.PoateDeschide(ManagerDeProiect)) return;
                FrmGestiunePrezenta frm = new(FrmParent, ManagerDeProiect, lCUPR)
                {
                    MdiParent = FrmParent
                };
                frm.Show();
            }
        }
        public void SalveazaIstoric(int lCUPR)
        {
            CInterogareSQL qIstoric = new(ManagerDeProiect.GetConnection());
            m_sSql = string.Format(@" INSERT INTO CU_PrezenteIstoric
                                    (CUPR_MBSO_Id, CUPR_MBDV_Id, CUPR_CUSE_Id,CUPR_DeLaData, CUPR_CUFO_Id,  CUPR_NrPrezenta, CUPR_DataSalvarii, CUPR_MBUT_Id, CUPR_DataModificarii, CUPR_CUPR_Modificat,
                                    CUPR_Observatii,CUPR_PanaLaData)
                                    SELECT CUPR_MBSO_Id,CUPR_MBDV_Id, CUPR_CUSE_Id, CUPR_DeLaData, CUPR_CUFO_Id, CUPR_NrPrezenta, CUPR_DataSalvarii, {0}, GetDate(), {1}, CUPR_Observatii,CUPR_PanaLaData
                                    FROM CU_Prezente
                                    WHERE CUPR_Id={1}", ManagerDeProiect.GetUserId(), lCUPR);
            qIstoric.ExecuteStatement(m_sSql);

            m_sSql = "SELECT SCOPE_IDENTITY() AS SID";
            int lCUPRIstoric = qIstoric.ExecuteToInt(m_sSql);

            m_sSql = string.Format(@" INSERT INTO CU_PrezenteDetaliuIstoric
                                    (CUPD_CUPR_Id, CUPD_CUCU_Id, CUPD_Prezent, CUPD_DataSalvarii, CUPD_CUPD_Modificat,CUPD_ObservatiiOperatiune)
                                    SELECT {0}, CUPD_CUCO_Id, CUPD_Prezent, CUPD_DataSalvarii,CUPD_Id,'Stergere prezenta generala'
                                    FROM CU_PrezenteDetaliu
                                    WHERE CUPD_CUPR_Id={1} ", lCUPRIstoric, lCUPR);
            qIstoric.ExecuteStatement(m_sSql);

            qIstoric.Close();
        }

        private void CautaPrezenta()
        {
            Cursor.Current = Cursors.WaitCursor;

            object? value;
            string sData, sTmp, sFiltru = "";


            if (m_ckActivareFiltruData.Checked)
            {
                // Filtru De La Data
                sFiltru += string.Format(" AND CUPR_DeLaData >= {0} ", CFormatareDate.DateToSQLString(m_dtpDeLaData.Value));

                // Filtru Pana La Data (inclusiv ziua respectivă prin +1)
                sFiltru += string.Format(" AND CUPR_PanaLaData <= {0}+1 ", CFormatareDate.DateToSQLString(m_dtpPanaLaData.Value));
            }

            sData = m_cbFormator.Text;
            if (sData != "")
            {
                value = m_cbFormator.GetColumnData("CUFO_Id");
                sTmp = string.Format(" AND CUPR_CUFO_Id={0} ", Convert.ToInt32(value));
                sFiltru += sTmp;
            }
            string sFiltruCursant = "",sNume="", sPrenume="", sFiltruNume="", sFiltruPrenume="";
            sNume = m_ebNumeCursant.Text;
            sPrenume = m_ebPrenumeCursant.Text;
            if (sNume != "" || sPrenume != "")
            {      
                if(sNume != "")
                {
                    sTmp = string.Format(" AND CUCU_Nume LIKE '{0}%'", sNume);
                    sFiltruNume = sTmp;
                }
                if (sPrenume != "")
                {
                    sTmp = string.Format(" AND CUCU_Prenume LIKE '{0}%'", sPrenume);
                    sFiltruPrenume = sTmp;
                }
                    

                sTmp = string.Format(@"INNER JOIN 
                                            (SELECT DISTINCT CUPD_CUPR_Id
                                            FROM CU_PrezenteDetaliu
                                            INNER JOIN CU_Prezente ON CUPD_CUPR_Id=CUPR_Id 
                                            INNER JOIN CU_Cursanti ON CUPD_CUCU_Id=CUCU_Id 
                                            WHERE 1=1 {0} {1}) 
                                            AS T ON T.CUPD_CUPR_Id=CUPR_Id ", sFiltruNume, sFiltruPrenume);

                sFiltruCursant += sTmp;
            }

            m_sSql = string.Format(@"SELECT ROW_NUMBER() OVER (ORDER BY CUPR_DeLaData DESC) AS Row, CUPR_Id, CUPR_NrPrezenta,
                                     CASE DATEPART(WEEKDAY, CUPR_DeLaData)  
                                         WHEN 1 THEN 'Duminica' 
                                         WHEN 2 THEN 'Luni' 
                                         WHEN 3 THEN 'Marti' 
                                         WHEN 4 THEN 'Miercuri' 
                                         WHEN 5 THEN 'Joi' 
                                         WHEN 6 THEN 'Vineri' 
                                         WHEN 7 THEN 'Sambata' 
                                     END AS ZiSaptamana,
                                     CUPR_DeLaData, CUPR_PanaLaData, CUFO_Nume + ' ' + CUFO_Prenume AS Formator,
                                     ISNULL(Detaliu.Total, 0) AS TotalPrezente,
                                     CUPR_Observatii
                                 FROM CU_Prezente                       
                                 LEFT JOIN CU_Formatori ON CUPR_CUFO_Id = CUFO_Id
                                 OUTER APPLY (
                                     SELECT COUNT(*) AS Total 
                                     FROM CU_PrezenteDetaliu 
                                     WHERE CUPD_CUPR_Id = CUPR_Id
                                 ) AS Detaliu
                                 {2}
                                 WHERE CUPR_MBDV_Id = {0} {1}
                                 ORDER BY CUPR_DeLaData DESC", ManagerDeProiect.GetDiviziune(), sFiltru, sFiltruCursant);
            m_gPrezente.SetSql(m_sSql);

            m_gPrezente.FormatColumn("Nr. crt", 50, colAlign: DataGridViewContentAlignment.MiddleRight);
            m_gPrezente.FormatColumn("CUPR_Id", 0, bVisible: false);
            m_gPrezente.FormatColumn("Nr. prezenta", 60, colAlign: DataGridViewContentAlignment.MiddleRight);
            m_gPrezente.FormatColumn("Ziua prezentei", 60);
            m_gPrezente.FormatColumn("De la data si ora prezentei", 120, sCellStyle: "dd.MM.yyyy HH:mm");
            m_gPrezente.FormatColumn("Pana la data si ora prezentei", 120, sCellStyle: "dd.MM.yyyy HH:mm");
            m_gPrezente.FormatColumn("Formator", 150);
            m_gPrezente.FormatColumn("Număr prezențe curs", 150,colAlign: DataGridViewContentAlignment.MiddleRight);
            m_gPrezente.FormatColumn("Observatii", 200);

            m_gPrezente.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (DataGridViewColumn column in m_gPrezente.Columns)
            {
                m_gPrezente.Columns[column.Name]!.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            SchimbaRandulPrezenta(0);
            Cursor.Current = Cursors.Default;
        }

        private void CautaCursant()
        {
            string sFiltru = "";
            string sNume = m_ebNumeCursant.Text.Trim();
            string sPrenume = m_ebPrenumeCursant.Text.Trim();

            if (!string.IsNullOrEmpty(sNume))
                sFiltru += string.Format(" AND CUCU_Nume LIKE N'{0}%' ", sNume);
            if (!string.IsNullOrEmpty(sPrenume))
                sFiltru += string.Format(" AND CUCU_Prenume LIKE '{0}%' ", sPrenume);

            SchimbaRandulPrezenta(m_gPrezente.CurrentRow!.Index, sFiltru);
        }

        private void SchimbaRandulPrezenta(int lRow, string sFiltru = "")
        {
            int lCUPR = 0;
            object? value = m_gPrezente.GetColumnData("CUPR_Id", lRow);
            if (value != null)
            {
                lCUPR = Convert.ToInt32(value);
            }

            m_sSql = string.Format(@"SELECT ROW_NUMBER() OVER (ORDER BY CUCU_Nume, CUCU_Prenume) AS Row,
                                        CUPD_Id, CUCU_Nume, CUCU_Prenume, CUCU_TelefonMobil, CUAB_Descriere, ISNULL(Prezente.NrPrezenteEfectuate, 0) AS TotalPrezente,
                                    CUCO_NrContract,CUCO_Listat
                                     FROM CU_PrezenteDetaliu
                                     INNER JOIN CU_Prezente ON CUPD_CUPR_Id = CUPR_Id
                                     INNER JOIN CU_Contracte ON CUPD_CUCO_Id = CUCO_Id   
                                     INNER JOIN CU_Abonamente ON CUCO_CUAB_Id = CUAB_Id
                                     INNER JOIN CU_Cursanti ON CUPD_CUCU_Id = CUCU_Id
                                     LEFT JOIN (
                                         SELECT 
                                             CUPD_CUCU_Id, 
                                             COUNT(*) AS NrPrezenteEfectuate
                                         FROM CU_PrezenteDetaliu
                                         INNER JOIN CU_Prezente PR ON CUPD_CUPR_Id = PR.CUPR_Id
                                         INNER JOIN CU_Contracte ON CUPD_CUCO_Id = CUCO_Id
                                         WHERE MONTH(CUPR_DataSalvarii) = (SELECT MONTH(CUPR_DataSalvarii) FROM CU_Prezente WHERE CUPR_Id={0}) AND YEAR(CUPR_DataSalvarii) = YEAR(GETDATE())
                                         GROUP BY CUPD_CUCU_Id
                                     ) Prezente ON Prezente.CUPD_CUCU_Id = CUCU_Id
                                     WHERE CUPD_CUPR_Id = {0} {1}
                                     ORDER BY CUCU_Nume, CUCU_Prenume", lCUPR, sFiltru);

            m_gDetaliuPrezenta.SetSql(m_sSql);

            m_gDetaliuPrezenta.FormatColumn("Nr. crt", 50, colAlign: DataGridViewContentAlignment.MiddleRight);
            m_gDetaliuPrezenta.FormatColumn("CUPD_Id", 0, bVisible: false);
            m_gDetaliuPrezenta.FormatColumn("Nume", 200);
            m_gDetaliuPrezenta.FormatColumn("Prenume", 200);
            m_gDetaliuPrezenta.FormatColumn("Telefon", 100);
            m_gDetaliuPrezenta.FormatColumn("Abonament", 250);
            m_gDetaliuPrezenta.FormatColumn("Numar prezențe luna curentă", 150, colAlign: DataGridViewContentAlignment.MiddleRight);
            m_gDetaliuPrezenta.FormatColumn("Numar contract", 100, colAlign: DataGridViewContentAlignment.MiddleRight);
            m_gDetaliuPrezenta.FormatColumn("Contract Listat", 70);


            m_gDetaliuPrezenta.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (DataGridViewColumn column in m_gDetaliuPrezenta.Columns)
            {
                m_gDetaliuPrezenta.Columns[column.Name]!.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            if (m_gDetaliuPrezenta.Rows.Count > 0)
            {
                int nRowIndex = m_gDetaliuPrezenta.Rows.Count - 1;
                if (nRowIndex > 1)
                {
                    m_gDetaliuPrezenta.FirstDisplayedScrollingRowIndex = nRowIndex - 1;
                    m_gDetaliuPrezenta.ClearSelection();
                    m_gDetaliuPrezenta.Rows[nRowIndex].Selected = true;
                    m_gDetaliuPrezenta.Rows[nRowIndex].Cells[1].Selected = true;
                    m_gDetaliuPrezenta.CurrentCell = m_gDetaliuPrezenta.Rows[nRowIndex].Cells[2];
                }

            }
        }
    }
}

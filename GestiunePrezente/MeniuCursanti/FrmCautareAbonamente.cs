using GestiunePrezente.Common;
using GestiunePrezente.MeniuPrincipal;

namespace GestiunePrezente.MeniuCursanti
{
    public partial class FrmCautareAbonamente : Form
    {
        readonly CManagerDeProiect ManagerDeProiect;
        readonly FrmMeniuPrincipal FrmParent;
        string? m_sSql;
        readonly int m_lMBME = 9;
        public FrmCautareAbonamente(FrmMeniuPrincipal FormParent, CManagerDeProiect MM)
        {
            FrmParent = FormParent;
            ManagerDeProiect = MM;
            InitializeComponent();
            GridPersonalizat.CustomGrid.SetConnection(ManagerDeProiect.GetConnection());

            if (ManagerDeProiect.GetRolUser_Administrator() != 1)
                m_tsbSterge.Enabled = false;
        }
        private void FrmCautareAbonamente_Load(object sender, EventArgs e)
        {
            Cauta();
        }
        private void m_tsbCautare_Click(object sender, EventArgs e)
        {
            Cauta();
        }
        private void m_tsbNou_Click(object sender, EventArgs e)
        {
            if (!FrmGestiuneAbonament.PoateDeschide(ManagerDeProiect)) return;
            FrmGestiuneAbonament frm = new(FrmParent, ManagerDeProiect, 0, m_lMBME)
            {
                MdiParent = FrmParent
            };
            frm.Show();
        }
        private void m_tsbSterge_Click(object sender, EventArgs e)
        {
            int lCUAB;
            object? value = m_gAbonamente.GetColumnData("CUAB_Id");
            if (value == null) return;

            lCUAB = Convert.ToInt32(value);
            if (MessageBox.Show(CFormatareDate.GetString("IDS_ABONEMENT_ASK_DELETE"), CFormatareDate.GetString("IDS_ATENTIE"), MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            CInterogareSQL q = new(ManagerDeProiect.GetConnection());
            int lFolosit;

            m_sSql = string.Format(@"SELECT COUNT(*)
                                     FROM CU_Contracte
                                     WHERE CUCO_CUAB_Id = {0}", lCUAB);
            lFolosit = q.ExecuteToInt(m_sSql);

            if (lFolosit > 0)
            {
                CFormatareDate.ShowWarning("IDS_ABONAMENT_FOLOSIT_IN_CONTRACTE");
                return;
            }

            m_sSql = string.Format(@"DELETE FROM CU_Abonamente WHERE CUAB_Id={0}", lCUAB);
            q.ExecuteStatement(m_sSql);
            q.Close();

            Cauta();
        }
        private void m_gAbonamente_DoubleClick(object sender, EventArgs e)
        {
            Modifica();
        }
        private void Modifica()
        {
            int lCUAB;
            object? value = m_gAbonamente.GetColumnData("CUAB_Id");
            if (value != null)
            {
                lCUAB = Convert.ToInt32(value);
                if (!FrmGestiuneAbonament.PoateDeschide(ManagerDeProiect)) return;
                FrmGestiuneAbonament frm = new(FrmParent, ManagerDeProiect, lCUAB, m_lMBME)
                {
                    MdiParent = FrmParent
                };
                frm.Show();
            }
        }
        private void Cauta()
        {
            m_sSql = string.Format(@"SELECT ROW_NUMBER() OVER (ORDER BY CUAB_Id) AS NrCrt,
                                            CUAB_Id, CUAB_Descriere,
                                            ISNULL((SELECT TOP 1 CUABP_Pret FROM CU_PreturiAbonamente
                                                    WHERE CUABP_CUAB_Id = CUAB_Id
                                                      AND CUABP_DataStart <= GETDATE()
                                                      AND (CUABP_DataSfarsit IS NULL OR CUABP_DataSfarsit >= GETDATE())
                                                    ORDER BY CUABP_DataStart DESC), 0) AS Pret,
                                            CUAB_Perioada, MBTD_Durata AS TipDurata
                                     FROM CU_Abonamente
                                     INNER JOIN MB_TipDurata ON CUAB_MBTD_Id = MBTD_ID");

            m_gAbonamente.SetSql(m_sSql);

            m_gAbonamente.FormatColumn("Nr. crt", 50, colAlign: DataGridViewContentAlignment.MiddleRight);
            m_gAbonamente.FormatColumn("CUAB_Id", 0, bVisible: false);
            m_gAbonamente.FormatColumn("Abonament", 250, colAlign: DataGridViewContentAlignment.MiddleLeft);
            m_gAbonamente.FormatColumn("Pret", 150, colAlign: DataGridViewContentAlignment.MiddleRight);
            m_gAbonamente.FormatColumn("Perioada", 150, colAlign: DataGridViewContentAlignment.MiddleRight);
            m_gAbonamente.FormatColumn("Durata", 150, colAlign: DataGridViewContentAlignment.MiddleRight);

            m_gAbonamente.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
    }
}

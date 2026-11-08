using GestiunePrezente.Common;
using GestiunePrezente.MeniuPrincipal;

namespace GestiunePrezente.MeniuCursanti
{
    public partial class FrmCautareFormatori : Form
    {
        readonly CManagerDeProiect ManagerDeProiect;
        readonly FrmMeniuPrincipal FrmParent;
        string? m_sSql;
        readonly int m_lMBME = 13;
        public FrmCautareFormatori(FrmMeniuPrincipal FormParent, CManagerDeProiect MM)
        {
            FrmParent = FormParent;
            ManagerDeProiect = MM;
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            GridPersonalizat.CustomGrid.SetConnection(ManagerDeProiect.GetConnection());
            m_gFormatori.ReadOnly = true;
        }

        private void m_tsbCautare_Click(object sender, EventArgs e)
        {
            Cauta();
        }

        private void m_tsbNou_Click(object sender, EventArgs e)
        {
            if (!FrmGestiuneFormator.PoateDeschide(ManagerDeProiect)) return;
            FrmGestiuneFormator frm = new(FrmParent, ManagerDeProiect, 0, m_lMBME)
            {
                MdiParent = FrmParent
            };
            frm.Show();
        }

        private void m_tsbSterge_Click(object sender, EventArgs e)
        {
            int lCUFO;
            object? value = m_gFormatori.GetColumnData("CUFO_Id");
            if (value == null) return;

            lCUFO = Convert.ToInt32(value);
            if (MessageBox.Show(CFormatareDate.GetString("IDS_FORMATOR_ASK_DELETE"), CFormatareDate.GetString("IDS_ATENTIE"), MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            CInterogareSQL q = new(ManagerDeProiect.GetConnection());

            int lFolosit;
            m_sSql = string.Format(@"SELECT COUNT(*)
                                        FROM CU_Prezente
                                        WHERE CUPR_CUFO_Id = {0}", lCUFO);
            lFolosit = q.ExecuteToInt(m_sSql);

            if (lFolosit > 0)
            {
                CFormatareDate.ShowWarning("IDS_FORMATOR_FOLOSIT_IN_PREZENTE");
                return;
            }

            m_sSql = string.Format(@"DELETE FROM CU_Formatori WHERE CUFO_Id={0}", lCUFO);
            q.ExecuteStatement(m_sSql);

            q.Close();
            Cauta();

        }

        private void Cauta()
        {
            string sData, sTmp, sFiltru = "";


            sData = m_ebNume.Text;
            if (sData != "")
            {
                sTmp = string.Format(" AND (UPPER(CUFO_Nume) LIKE UPPER('{0}%') )", CFormatareDate.StringToSqlString(sData));
                sFiltru += sTmp;
            }

            sData = m_ebPrenume.Text;
            if (sData != "")
            {
                sTmp = string.Format(" AND UPPER(CUFO_Prenume) LIKE UPPER('{0}%') ", CFormatareDate.StringToSqlString(sData));
                sFiltru += sTmp;
            }


            sData = m_ebTelMobil.Text;
            if (sData != "")
            {
                sTmp = string.Format(" AND CUFO_TelefonMobil LIKE '{0}%' ", CFormatareDate.StringToSqlString(sData));
                sFiltru += sTmp;
            }



            m_sSql = string.Format(@"SELECT DISTINCT ROW_NUMBER() OVER (ORDER BY CUFO_Nume, CUFO_Prenume) AS Row,
                                    CUFO_Id, CUFO_Nume, CUFO_Prenume, CUFO_TelefonMobil
                                    FROM CU_Formatori                                
                                    WHERE 1=1 {0} 
                                    ORDER BY CUFO_Nume, CUFO_Prenume", sFiltru);

            m_gFormatori.SetSql(m_sSql);
            m_gFormatori.FormatColumn("Nr. crt", 50, null, DataGridViewContentAlignment.MiddleRight);
            m_gFormatori.FormatColumn("CUFO_Id", 70, false);
            m_gFormatori.FormatColumn("Nume", 150);
            m_gFormatori.FormatColumn("Prenume", 150);
            m_gFormatori.FormatColumn("Telefon Mobil", 100);

            m_gFormatori.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (DataGridViewColumn column in m_gFormatori.Columns)
            {
                m_gFormatori.Columns[column.Name]!.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void m_gFormatori_DoubleClick(object sender, EventArgs e)
        {
            Modifica();
        }

        private void Modifica()
        {
            int lCUFO;
            object? value = m_gFormatori.GetColumnData("CUFO_Id");
            if (value != null)
            {
                lCUFO = Convert.ToInt32(value);
                if (!FrmGestiuneFormator.PoateDeschide(ManagerDeProiect)) return;
                FrmGestiuneFormator frm = new(FrmParent, ManagerDeProiect, lCUFO, m_lMBME)
                {
                    MdiParent = FrmParent
                };
                frm.Show();
            }
        }

        private void m_ebNume_TextChanged(object sender, EventArgs e)
        {
            Cauta();
        }

        private void m_ebPrenume_TextChanged(object sender, EventArgs e)
        {
            Cauta();
        }

        private void m_ebTelMobil_TextChanged(object sender, EventArgs e)
        {
            Cauta();
        }


    }
}

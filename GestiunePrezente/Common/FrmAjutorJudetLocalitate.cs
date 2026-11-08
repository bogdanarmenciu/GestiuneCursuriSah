using DocumentFormat.OpenXml.Drawing.Charts;
using GestiunePrezente.MeniuPrincipal;

namespace GestiunePrezente.Common
{
    public partial class FrmAjutorJudetLocalitate : Form
    {
        readonly CManagerDeProiect ManagerDeProiect;
        readonly FrmMeniuPrincipal FrmParent;
        string m_sJudet = "";
        string m_sLocalitate = "";
        string? sSql;
        public FrmAjutorJudetLocalitate(FrmMeniuPrincipal FormParent, CManagerDeProiect MM, string sJudet = "", string sLocalitate = "")
        {
            FrmParent = FormParent;
            ManagerDeProiect = MM;

            InitializeComponent();

            GridPersonalizat.CustomGrid.SetConnection(ManagerDeProiect.GetConnection());
            m_gJudet.ReadOnly = true;
            m_gLocalitate.ReadOnly = true;
            m_sJudet = sJudet;
            m_sLocalitate = sLocalitate;
        }

        private void m_tsbCautare_Click(object sender, EventArgs e)
        {
            Cauta();
        }

        private void Cauta()
        {
            string sData, sSql, sTmp, sFiltru = "";

            sData = m_ebJudetul.Text;
            if (sData != "")
            {
                sTmp = string.Format(" AND MBJU_Judet LIKE N'{0}%' ", CFormatareDate.StringToSqlString(sData));
                sFiltru += sTmp;
            }

            sData = m_ebLocalitatea.Text;
            if (sData != "")
            {
                sTmp = string.Format(" AND MBLO_Localitate LIKE N'{0}%' ", CFormatareDate.StringToSqlString(sData));
                sFiltru += sTmp;
            }

            sSql = string.Format(@"SELECT DISTINCT MBJU_AbreviereJudet,MBJU_CodJudet,MBJU_Judet 
                                   FROM MB_Judet 
                                   INNER JOIN MB_Localitate ON MBJU_AbreviereJudet=MBLO_AbreviereJudet 
                                    WHERE 1=1 {0}", sFiltru);
            m_gJudet.SetSql(sSql);
            m_gJudet.FormatColumn("Abreviere", 100);
            m_gJudet.FormatColumn("Cod Judet", 100);
            m_gJudet.FormatColumn("Judet", 150);

            SchimbaRandulJudet(0);
        }

        private void m_gJudet_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                SchimbaRandulJudet(e.RowIndex);
            }
        }

        private void SchimbaRandulJudet(int lRow)
        {
            string sData, sSql, sTmp, sFiltru = "";

            object? value = m_gJudet.GetColumnData("MBJU_AbreviereJudet", lRow);
            if (value != null)
            {
                sTmp = string.Format(" AND MBLO_AbreviereJudet = '{0}' ", value.ToString());
                sFiltru += sTmp;
            }

            sData = m_ebLocalitatea.Text;
            if (sData != "")
            {
                sTmp = string.Format(" AND MBLO_Localitate LIKE '{0}%' ", CFormatareDate.StringToSqlString(sData));
                sFiltru += sTmp;
            }

            sSql = string.Format(@"SELECT MBLO_AbreviereJudet,MBLO_CodLocalitate,MBLO_Localitate
                                   FROM MB_Localitate
                                   WHERE 1=1 {0}
                                   ORDER BY MBLO_Id", sFiltru);
            m_gLocalitate.SetSql(sSql);
            m_gLocalitate.FormatColumn("Abreviere Judet", 0, bVisible: false);
            m_gLocalitate.FormatColumn("Cod Localitate", 100);
            m_gLocalitate.FormatColumn("Localitate", 150);
        }

        private void m_gLocalitate_DoubleClick(object sender, EventArgs e)
        {
            object? value = m_gJudet.GetColumnData("MBJU_Judet");
            if (value != null)
                m_sJudet = value.ToString()!;

            value = m_gLocalitate.GetColumnData("MBLO_Localitate");
            if (value != null)
                m_sLocalitate = value.ToString()!;

            DialogResult = DialogResult.OK;

            Close();
        }

        public string GetJudet()
        {
            return m_sJudet;
        }

        public string GetLocalitate()
        {
            return m_sLocalitate;
        }

        private void m_gJudet_DoubleClick(object sender, EventArgs e)
        {
            //object value = m_gJudet.GetColumnData("MBJU_Judet");
            //if (value != null)
            //    m_sJudet = value.ToString();

            //DialogResult = DialogResult.OK;

            //Close();
        }

        private void m_ebJudetul_TextChanged(object sender, EventArgs e)
        {
            Cauta();
        }

        private void m_ebLocalitatea_TextChanged(object sender, EventArgs e)
        {
            Cauta();
        }
    }
}

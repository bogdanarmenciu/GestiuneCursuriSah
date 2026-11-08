using GestiunePrezente.Common;
using GestiunePrezente.MeniuPrincipal;

namespace GestiunePrezente.MeniuConfigurare
{
    public partial class FrmCautareDiviziuni : Form
    {
        readonly CManagerDeProiect ManagerDeProiect;
        readonly FrmMeniuPrincipal FrmParent;
        string? m_sSql;
        public FrmCautareDiviziuni(FrmMeniuPrincipal FormParent, CManagerDeProiect MM)
        {
            FrmParent = FormParent;
            ManagerDeProiect = MM;

            InitializeComponent();

            GridPersonalizat.CustomGrid.SetConnection(ManagerDeProiect.GetConnection());
            m_gDiviziuni.ReadOnly = true;

            Cauta();

            m_tsbNou.Enabled = false;
            m_stbStergere.Enabled = false;

        }

        private void m_tsbCautare_Click(object sender, EventArgs e)
        {
            Cauta();
        }

        private void m_tsbNou_Click(object sender, EventArgs e)
        {
            if (!FrmGestiuneDiviziune.PoateDeschide(ManagerDeProiect)) return;
            FrmGestiuneDiviziune frm = new(FrmParent, ManagerDeProiect, 0)
            {
                MdiParent = FrmParent
            };
            frm.Show();
        }

        private void Cauta()
        {
            m_sSql = string.Format(@"SELECT MBDV_Id,MBDV_Diviziune,MBDV_TelefonMobil,MBDV_Adresa,MBDV_Mail FROM MB_Diviziune");
            m_gDiviziuni.SetSql(m_sSql);
            m_gDiviziuni.FormatColumn("Nr.Crt", 50, colAlign: DataGridViewContentAlignment.MiddleRight);
            m_gDiviziuni.FormatColumn("Diviziune", 200);
            m_gDiviziuni.FormatColumn("Telefon", 100);
            m_gDiviziuni.FormatColumn("Adresa", 300);
            m_gDiviziuni.FormatColumn("Adresa mail", 300);

            m_gDiviziuni.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn column in m_gDiviziuni.Columns)
            {
                m_gDiviziuni.Columns[column.Name]!.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

        }

        private void m_gDiviziuni_DoubleClick(object sender, EventArgs e)
        {
            int lMBDV;
            object value = m_gDiviziuni.GetColumnData("MBDV_Id")!;
            if (value != null)
            {
                lMBDV = Convert.ToInt32(value);
                if (!FrmGestiuneDiviziune.PoateDeschide(ManagerDeProiect)) return;
                FrmGestiuneDiviziune frm = new(FrmParent, ManagerDeProiect, lMBDV)
                {
                    MdiParent = FrmParent
                };
                frm.Show();
            }
        }
    }
}

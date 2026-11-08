using GestiunePrezente.Common;
using GestiunePrezente.MeniuPrincipal;
using GridPersonalizat;
using Microsoft.Data.SqlClient;

namespace GestiunePrezente.MeniuAdministrare
{
    public partial class FrmCautareParteneri : Form
    {
        readonly CManagerDeProiect ManagerDeProiect;
        readonly FrmMeniuPrincipal FrmParent;

        public FrmCautareParteneri(FrmMeniuPrincipal formParent, CManagerDeProiect mm)
        {
            FrmParent = formParent;
            ManagerDeProiect = mm;
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            CustomGrid.SetConnection(ManagerDeProiect.GetConnection());
            m_gParteneri.ReadOnly = true;
        }

        private void FrmCautareParteneri_Load(object sender, EventArgs e) => Cauta();

        // =========================================================
        //                         CĂUTARE
        // =========================================================
        private void Cauta()
        {
            var f = new System.Text.StringBuilder();
            var pars = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(m_ebDenumire.Text))
            {
                f.Append(" AND ADPA_Denumire LIKE @den ");
                pars.Add(CInterogareSQL.P("@den", m_ebDenumire.Text + "%"));
            }

            if (!string.IsNullOrWhiteSpace(m_ebCUI.Text))
            {
                f.Append(" AND ADPA_CUI LIKE @cui ");
                pars.Add(CInterogareSQL.P("@cui", m_ebCUI.Text + "%"));
            }

            // Filtru tip: dacă niciun checkbox nu e bifat → arată toate
            bool filterClient   = m_ckClient.Checked;
            bool filterFurnizor = m_ckFurnizor.Checked;
            bool filterSponsor  = m_ckSponsor.Checked;

            if (filterClient || filterFurnizor || filterSponsor)
            {
                var tipFiltru = new System.Text.StringBuilder(" AND (");
                bool first = true;
                if (filterClient)   { tipFiltru.Append("ADPA_IsClient = 1");   first = false; }
                if (filterFurnizor) { tipFiltru.Append(first ? "" : " OR "); tipFiltru.Append("ADPA_IsFurnizor = 1"); first = false; }
                if (filterSponsor)  { tipFiltru.Append(first ? "" : " OR "); tipFiltru.Append("ADPA_IsSponsor = 1"); }
                tipFiltru.Append(") ");
                f.Append(tipFiltru);
            }

            pars.Add(CInterogareSQL.P("@div", ManagerDeProiect.GetDiviziune()));

            string sql = $@"
SELECT ROW_NUMBER() OVER (ORDER BY ADPA_Denumire) AS [Nr. crt],
       ADPA_Id,
       ADPA_Denumire          AS [Denumire],
       ISNULL(ADPA_CUI, '')   AS [CUI],
       ISNULL(ADPA_Localitate,'') + CASE WHEN ISNULL(ADPA_Judet,'') <> ''
           THEN ', ' + ADPA_Judet ELSE '' END AS [Localitate],
       ISNULL(ADPA_Telefon, ISNULL(ADPA_TelefonMobil, '')) AS [Telefon],
       ISNULL(ADPA_Email, '') AS [Email],
       CASE WHEN ADPA_IsClient  = 1 THEN 'Da' ELSE '' END AS [Client],
       CASE WHEN ADPA_IsFurnizor= 1 THEN 'Da' ELSE '' END AS [Furnizor],
       CASE WHEN ADPA_IsSponsor = 1 THEN 'Da' ELSE '' END AS [Sponsor]
  FROM AD_Parteneri
 WHERE ADPA_MBDV_Id = @div
       {f}
 ORDER BY ADPA_Denumire;";

            m_gParteneri.SetSql(sql, pars.ToArray());

            m_gParteneri.FormatColumn("Nr. crt",   50,  true, DataGridViewContentAlignment.MiddleRight);
            m_gParteneri.FormatColumn("ADPA_Id",   60,  false);
            m_gParteneri.FormatColumn("Denumire",  280);
            m_gParteneri.FormatColumn("CUI",        90, true, DataGridViewContentAlignment.MiddleCenter);
            m_gParteneri.FormatColumn("Localitate",180);
            m_gParteneri.FormatColumn("Telefon",   110);
            m_gParteneri.FormatColumn("Email",     180);
            m_gParteneri.FormatColumn("Client",     55, true, DataGridViewContentAlignment.MiddleCenter);
            m_gParteneri.FormatColumn("Furnizor",   60, true, DataGridViewContentAlignment.MiddleCenter);
            m_gParteneri.FormatColumn("Sponsor",    60, true, DataGridViewContentAlignment.MiddleCenter);

            m_gParteneri.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn c in m_gParteneri.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        // =========================================================
        //                     ACȚIUNI TOOLBAR
        // =========================================================
        private void m_tsbCautare_Click(object sender, EventArgs e) => Cauta();

        private void m_tsbNou_Click(object sender, EventArgs e)
        {
            if (!FrmGestiunePartener.PoateDeschide(ManagerDeProiect)) return;
            var frm = new FrmGestiunePartener(FrmParent, ManagerDeProiect, 0)
            { MdiParent = FrmParent };
            frm.Show();
        }

        private void m_tsbModifica_Click(object sender, EventArgs e) => Modifica();

        private void m_tsbStergere_Click(object sender, EventArgs e)
        {
            var v = m_gParteneri.GetColumnData("ADPA_Id");
            if (v == null || v == DBNull.Value) return;
            int id = Convert.ToInt32(v);

            if (MessageBox.Show("Confirmați ștergerea partenerului?", "Atenție",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;

            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            try
            {
                q.ExecuteNonQueryP("DELETE FROM AD_Parteneri WHERE ADPA_Id = @id",
                    CInterogareSQL.P("@id", id));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la ștergere: " + ex.Message, "Eroare",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            q.Close();
            Cauta();
        }

        private void m_gParteneri_DoubleClick(object sender, EventArgs e) => Modifica();

        private void Modifica()
        {
            var v = m_gParteneri.GetColumnData("ADPA_Id");
            if (v == null || v == DBNull.Value) return;
            int id = Convert.ToInt32(v);
            if (!FrmGestiunePartener.PoateDeschide(ManagerDeProiect)) return;
            var frm = new FrmGestiunePartener(FrmParent, ManagerDeProiect, id)
            { MdiParent = FrmParent };
            frm.Show();
        }

        // =========================================================
        //                       FILTRE
        // =========================================================
        private void m_ebDenumire_TextChanged(object sender, EventArgs e) => Cauta();
        private void m_ebCUI_TextChanged(object sender, EventArgs e) => Cauta();
        private void m_ckTip_CheckedChanged(object sender, EventArgs e) => Cauta();
    }
}

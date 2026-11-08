using GestiunePrezente.Common;
using GestiunePrezente.MeniuPrincipal;
using GridPersonalizat;
using Microsoft.Data.SqlClient;

namespace GestiunePrezente.MeniuAdministrare
{
    public partial class FrmCautareSolicitari : Form
    {
        readonly CManagerDeProiect ManagerDeProiect;
        readonly FrmMeniuPrincipal FrmParent;

        private static readonly (int Id, string Denumire)[] Statusuri =
        {
            (0, "(toate)"),
            (1, "Nou"),
            (2, "Contactat"),
            (3, "Interesat"),
            (4, "Neinteresat"),
            (5, "Înscris"),
        };

        public FrmCautareSolicitari(FrmMeniuPrincipal formParent, CManagerDeProiect mm)
        {
            FrmParent = formParent;
            ManagerDeProiect = mm;
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            CustomGrid.SetConnection(ManagerDeProiect.GetConnection());
        }

        private void FrmCautareSolicitari_Load(object sender, EventArgs e)
        {
            IncarcaFiltreCombo();
            Cauta();
        }

        private void IncarcaFiltreCombo()
        {
            m_cbStatus.Items.Clear();
            foreach (var s in Statusuri)
                m_cbStatus.Items.Add(s.Denumire);
            m_cbStatus.SelectedIndex = 0;

            m_cbSursa.Items.Clear();
            m_cbSursa.Items.Add("(toate)");
            using var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            q.OpenP("SELECT ADSR_Id, ADSR_Denumire FROM AD_SurseSolicitari ORDER BY ADSR_Id");
            while (q.NextRow())
                m_cbSursa.Items.Add($"{q.GetIntValue(0)}|{q.GetStringValue(1)}");
            q.Close();
            m_cbSursa.SelectedIndex = 0;
        }

        private void Cauta()
        {
            var f = new System.Text.StringBuilder();
            var pars = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(m_ebNume.Text))
            {
                f.Append(" AND (ADSO_NumeCopil LIKE @nume OR ADSO_PrenumeCopil LIKE @nume) ");
                pars.Add(CInterogareSQL.P("@nume", m_ebNume.Text.Trim() + "%"));
            }

            if (!string.IsNullOrWhiteSpace(m_ebTelefon.Text))
            {
                f.Append(" AND ADSO_Telefon LIKE @tel ");
                pars.Add(CInterogareSQL.P("@tel", "%" + m_ebTelefon.Text.Trim() + "%"));
            }

            int statusIdx = m_cbStatus.SelectedIndex;
            if (statusIdx > 0)
            {
                f.Append(" AND ADSO_Status = @st ");
                pars.Add(CInterogareSQL.P("@st", Statusuri[statusIdx].Id));
            }

            string sursaItem = m_cbSursa.SelectedItem as string ?? "";
            if (sursaItem != "(toate)" && sursaItem.Contains('|'))
            {
                int sursaId = int.Parse(sursaItem.Split('|')[0]);
                f.Append(" AND ADSO_ADSR_Id = @sr ");
                pars.Add(CInterogareSQL.P("@sr", sursaId));
            }

            if (m_ckFiltruData.Checked)
            {
                f.Append(" AND ADSO_DataSolicitare BETWEEN @de AND @la ");
                pars.Add(CInterogareSQL.P("@de", m_dtpDeLa.Value.Date));
                pars.Add(CInterogareSQL.P("@la", m_dtpPanaLa.Value.Date));
            }

            pars.Add(CInterogareSQL.P("@div", ManagerDeProiect.GetDiviziune()));

            string sql = $@"
SELECT ROW_NUMBER() OVER (ORDER BY ADSO_DataSolicitare DESC) AS [Nr.crt],
       S.ADSO_Id,
       ISNULL(ADSO_PrenumeCopil,'') + ' ' + ISNULL(ADSO_NumeCopil,'') AS [Copil],
       ISNULL(ADSO_VarstaCopil,'')  AS [Vârstă],
       ISNULL(ADSO_NivelSah,'')     AS [Nivel],
       ISNULL(ADSO_PrenumeContact,'') + ' ' + ISNULL(ADSO_NumeContact,'') AS [Contact],
       ISNULL(ADSO_Telefon,'')      AS [Telefon],
       ISNULL(ADSO_Email,'')        AS [Email],
       ISNULL(SR.ADSR_Denumire,'')  AS [Sursă],
       CASE ADSO_Status
           WHEN 1 THEN 'Nou'
           WHEN 2 THEN 'Contactat'
           WHEN 3 THEN 'Interesat'
           WHEN 4 THEN 'Neinteresat'
           WHEN 5 THEN 'Înscris'
           ELSE '' END              AS [Status],
       CONVERT(varchar(10), ADSO_DataSolicitare, 104) AS [Data],
       CASE WHEN ADSO_CUCU_Id IS NOT NULL THEN '✓' ELSE '' END AS [Cursant]
  FROM AD_Solicitari S
  LEFT JOIN AD_SurseSolicitari SR ON SR.ADSR_Id = S.ADSO_ADSR_Id
 WHERE S.ADSO_MBDV_Id = @div
       {f}
 ORDER BY ADSO_DataSolicitare DESC;";

            m_gSolicitari.SetSql(sql, pars.ToArray());

            m_gSolicitari.FormatColumn("Nr.crt",  50,  true, DataGridViewContentAlignment.MiddleRight);
            m_gSolicitari.FormatColumn("ADSO_Id", 60,  false);
            m_gSolicitari.FormatColumn("Copil",   180);
            m_gSolicitari.FormatColumn("Vârstă",  55,  true, DataGridViewContentAlignment.MiddleCenter);
            m_gSolicitari.FormatColumn("Nivel",   100);
            m_gSolicitari.FormatColumn("Contact", 180);
            m_gSolicitari.FormatColumn("Telefon", 120);
            m_gSolicitari.FormatColumn("Email",   180);
            m_gSolicitari.FormatColumn("Sursă",    90, true, DataGridViewContentAlignment.MiddleCenter);
            m_gSolicitari.FormatColumn("Status",  100);
            m_gSolicitari.FormatColumn("Data",     85, true, DataGridViewContentAlignment.MiddleCenter);
            m_gSolicitari.FormatColumn("Cursant",  60, true, DataGridViewContentAlignment.MiddleCenter);

            m_gSolicitari.CellFormatting += m_gSolicitari_CellFormatting;

            foreach (DataGridViewColumn c in m_gSolicitari.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void m_gSolicitari_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
            if (m_gSolicitari.Columns[e.ColumnIndex].HeaderText != "Status") return;
            string val = e.Value as string ?? "";
            (e.CellStyle.BackColor, e.CellStyle.ForeColor) = val switch
            {
                "Nou"         => (Color.FromArgb(220, 235, 255), Color.Navy),
                "Contactat"   => (Color.FromArgb(255, 245, 200), Color.DarkGoldenrod),
                "Interesat"   => (Color.FromArgb(200, 240, 200), Color.DarkGreen),
                "Neinteresat" => (Color.FromArgb(240, 220, 220), Color.DarkRed),
                "Înscris"     => (Color.FromArgb(180, 240, 180), Color.DarkGreen),
                _             => (m_gSolicitari.DefaultCellStyle.BackColor, m_gSolicitari.DefaultCellStyle.ForeColor)
            };
        }

        private void m_tsbCautare_Click(object sender, EventArgs e) => Cauta();
        private void m_filtru_Changed(object sender, EventArgs e) => Cauta();

        private void m_ckFiltruData_CheckedChanged(object sender, EventArgs e)
        {
            m_dtpDeLa.Enabled  = m_ckFiltruData.Checked;
            m_dtpPanaLa.Enabled = m_ckFiltruData.Checked;
            Cauta();
        }

        private void m_tsbNou_Click(object sender, EventArgs e)
        {
            if (!FrmGestiuneSolicitare.PoateDeschide(ManagerDeProiect)) return;
            var frm = new FrmGestiuneSolicitare(FrmParent, ManagerDeProiect, 0) { MdiParent = FrmParent };
            frm.FormClosed += (s, _) => Cauta();
            frm.Show();
        }

        private void m_tsbModifica_Click(object sender, EventArgs e) => Modifica();
        private void m_gSolicitari_DoubleClick(object sender, EventArgs e) => Modifica();

        private void Modifica()
        {
            var v = m_gSolicitari.GetColumnData("ADSO_Id");
            if (v == null || v == DBNull.Value) return;
            int id = Convert.ToInt32(v);
            if (!FrmGestiuneSolicitare.PoateDeschide(ManagerDeProiect)) return;
            var frm = new FrmGestiuneSolicitare(FrmParent, ManagerDeProiect, id) { MdiParent = FrmParent };
            frm.FormClosed += (s, _) => Cauta();
            frm.Show();
        }

        private void m_tsbStergere_Click(object sender, EventArgs e)
        {
            var v = m_gSolicitari.GetColumnData("ADSO_Id");
            if (v == null || v == DBNull.Value) return;
            int id = Convert.ToInt32(v);

            if (MessageBox.Show("Confirmați ștergerea solicitării?", "Atenție",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;

            using var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            try
            {
                q.ExecuteNonQueryP("DELETE FROM AD_Solicitari WHERE ADSO_Id = @id",
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
    }
}

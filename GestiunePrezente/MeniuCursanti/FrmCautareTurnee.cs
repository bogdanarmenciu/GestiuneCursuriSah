using GestiunePrezente.Common;
using GestiunePrezente.MeniuPrincipal;

namespace GestiunePrezente.MeniuCursanti
{
    public partial class FrmCautareTurnee : Form
    {
        readonly CManagerDeProiect ManagerDeProiect;
        readonly FrmMeniuPrincipal FrmParent;

        public FrmCautareTurnee(FrmMeniuPrincipal formParent, CManagerDeProiect mm)
        {
            FrmParent = formParent;
            ManagerDeProiect = mm;
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            GridPersonalizat.CustomGrid.SetConnection(ManagerDeProiect.GetConnection());
            Cauta();
        }

        private void m_tsbCautare_Click(object sender, EventArgs e) => Cauta();

        private void m_tsbNou_Click(object sender, EventArgs e)
        {
            if (!FrmGestiuneTurnee.PoateDeschide(ManagerDeProiect)) return;
            var frm = new FrmGestiuneTurnee(FrmParent, ManagerDeProiect, 0) { MdiParent = FrmParent };
            frm.FormClosed += (s, ev) => Cauta();
            frm.Show();
        }

        private void m_tsbSterge_Click(object sender, EventArgs e)
        {
            object? val = m_gTurnee.GetColumnData("CUTE_Id");
            if (val == null || val == DBNull.Value) return;
            int cuteId = Convert.ToInt32(val);

            if (MessageBox.Show("Ștergi turneul selectat și toate rezultatele din el?",
                    "Confirmare", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            q.ExecuteNonQueryP(@"DELETE J FROM CU_TurneeJocuri J
                                  JOIN CU_TurneeDetaliu D ON J.CUTJ_CUTD_Id = D.CUTD_Id
                                 WHERE D.CUTD_CUTE_Id = @id",
                CInterogareSQL.P("@id", cuteId));
            q.ExecuteNonQueryP("DELETE FROM CU_TurneeDetaliu WHERE CUTD_CUTE_Id = @id",
                CInterogareSQL.P("@id", cuteId));
            q.ExecuteNonQueryP("DELETE FROM CU_Turnee WHERE CUTE_Id = @id",
                CInterogareSQL.P("@id", cuteId));
            q.Close();
            Cauta();
        }

        private void m_ebDenumire_TextChanged(object sender, EventArgs e) => Cauta();

        private void m_gTurnee_DoubleClick(object sender, EventArgs e) => Modifica();

        private void m_gTurnee_SelectionChanged(object sender, EventArgs e)
        {
            object? val = m_gTurnee.GetColumnData("CUTE_Id");
            m_tsbSterge.Enabled = val != null && val != DBNull.Value;
        }

        private void Cauta()
        {
            string filtru = "";
            string den = m_ebDenumire.Text.Trim();
            if (den != "")
                filtru += $" AND T.CUTE_Denumire LIKE N'{CFormatareDate.StringToSqlString(den)}%'";

            string sql = $@"
                SELECT ROW_NUMBER() OVER (ORDER BY T.CUTE_Data DESC, T.CUTE_Denumire) AS [Nr.],
                       T.CUTE_Id,
                       T.CUTE_Data       AS [Data],
                       T.CUTE_Denumire   AS [Denumire],
                       T.CUTE_Localitate AS [Localitate],
                       COUNT(D.CUTD_Id)  AS [Participanți]
                  FROM CU_Turnee T
                  LEFT JOIN CU_TurneeDetaliu D ON D.CUTD_CUTE_Id = T.CUTE_Id
                 WHERE 1=1 {filtru}
                 GROUP BY T.CUTE_Id, T.CUTE_Data, T.CUTE_Denumire, T.CUTE_Localitate
                 ORDER BY T.CUTE_Data DESC, T.CUTE_Denumire";

            m_gTurnee.SetSql(sql);
            m_gTurnee.FormatColumn("Nr.", 50, true, DataGridViewContentAlignment.MiddleRight);
            m_gTurnee.FormatColumn("CUTE_Id", 60, false);
            m_gTurnee.FormatColumn("Data", 90, true, null, "dd.MM.yyyy");
            m_gTurnee.FormatColumn("Denumire", 300);
            m_gTurnee.FormatColumn("Localitate", 200);
            m_gTurnee.FormatColumn("Participanți", 90, true, DataGridViewContentAlignment.MiddleRight);
            m_gTurnee.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn c in m_gTurnee.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void Modifica()
        {
            object? val = m_gTurnee.GetColumnData("CUTE_Id");
            if (val == null || val == DBNull.Value) return;
            int cuteId = Convert.ToInt32(val);
            if (!FrmGestiuneTurnee.PoateDeschide(ManagerDeProiect)) return;
            var frm = new FrmGestiuneTurnee(FrmParent, ManagerDeProiect, cuteId) { MdiParent = FrmParent };
            frm.FormClosed += (s, ev) => Cauta();
            frm.Show();
        }
    }
}

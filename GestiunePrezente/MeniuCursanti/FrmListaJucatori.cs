using GestiunePrezente.Common;
using GestiunePrezente.MeniuPrincipal;

namespace GestiunePrezente.MeniuCursanti
{
    public partial class FrmListaJucatori : Form
    {
        readonly CManagerDeProiect ManagerDeProiect;

        public FrmListaJucatori(FrmMeniuPrincipal formParent, CManagerDeProiect mm)
        {
            ManagerDeProiect = mm;
            InitializeComponent();
            MdiParent = formParent;
            WindowState = FormWindowState.Maximized;
            GridPersonalizat.CustomGrid.SetConnection(ManagerDeProiect.GetConnection());
            Cauta();
        }

        private void m_ebNume_TextChanged(object sender, EventArgs e) => Cauta();
        private void m_chkDoarCursanti_CheckedChanged(object sender, EventArgs e) => Cauta();

        private void Cauta()
        {
            string filtru = "";
            string nume = m_ebNume.Text.Trim();
            if (nume != "")
                filtru += $" AND (J.CUJA_Nume LIKE N'{CFormatareDate.StringToSqlString(nume)}%'" +
                          $"   OR J.CUJA_Prenume LIKE N'{CFormatareDate.StringToSqlString(nume)}%')";
            if (m_chkDoarCursanti.Checked)
                filtru += " AND J.CUJA_CUCU_Id IS NOT NULL";

            string sql = $@"
                SELECT ROW_NUMBER() OVER (ORDER BY J.CUJA_Nume, J.CUJA_Prenume) AS [Nr.],
                       J.CUJA_Id,
                       J.CUJA_Nume                              AS [Nume],
                       J.CUJA_Prenume                           AS [Prenume],
                       J.CUJA_Fed                               AS [FED],
                       ISNULL(CAST(J.CUJA_FideId AS NVARCHAR(10)), '')
                                                                AS [FIDE ID],
                       ISNULL(CAST(J.CUJA_RatingUltim AS NVARCHAR(6)), '')
                                                                AS [Rating],
                       ISNULL(C.CUCU_Nume + ' ' + C.CUCU_Prenume, '')
                                                                AS [Cursant ONIXTEIA],
                       COUNT(D.CUTD_Id)                         AS [Turnee]
                  FROM CU_Jucatori J
                  LEFT JOIN CU_Cursanti C ON J.CUJA_CUCU_Id = C.CUCU_Id
                  LEFT JOIN CU_TurneeDetaliu D ON D.CUTD_CUJA_Id = J.CUJA_Id
                 WHERE 1=1 {filtru}
                 GROUP BY J.CUJA_Id, J.CUJA_Nume, J.CUJA_Prenume, J.CUJA_Fed,
                          J.CUJA_FideId, J.CUJA_RatingUltim,
                          C.CUCU_Nume, C.CUCU_Prenume
                 ORDER BY J.CUJA_Nume, J.CUJA_Prenume";

            m_gJucatori.SetSql(sql);
            m_gJucatori.FormatColumn("Nr.",            45,  true,  DataGridViewContentAlignment.MiddleRight);
            m_gJucatori.FormatColumn("CUJA_Id",        60,  false);
            m_gJucatori.FormatColumn("Nume",           160, true);
            m_gJucatori.FormatColumn("Prenume",        160, true);
            m_gJucatori.FormatColumn("FED",            50,  true,  DataGridViewContentAlignment.MiddleCenter);
            m_gJucatori.FormatColumn("FIDE ID",        90,  true,  DataGridViewContentAlignment.MiddleRight);
            m_gJucatori.FormatColumn("Rating",         70,  true,  DataGridViewContentAlignment.MiddleRight);
            m_gJucatori.FormatColumn("Cursant ONIXTEIA", 220, true);
            m_gJucatori.FormatColumn("Turnee",         70,  true,  DataGridViewContentAlignment.MiddleRight);
            foreach (DataGridViewColumn c in m_gJucatori.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
        }
    }
}

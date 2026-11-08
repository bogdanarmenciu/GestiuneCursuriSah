using GestiunePrezente.Common;
using GestiunePrezente.MeniuPrincipal;

namespace GestiunePrezente.MeniuCursanti
{
    public partial class FrmSituatieCentralizata : Form
    {
        readonly CManagerDeProiect ManagerDeProiect;
        readonly FrmMeniuPrincipal FrmParent;

        public FrmSituatieCentralizata(FrmMeniuPrincipal formParent, CManagerDeProiect mm)
        {
            FrmParent        = formParent;
            ManagerDeProiect = mm;
            InitializeComponent();
            GridPersonalizat.CustomGrid.SetConnection(ManagerDeProiect.GetConnection());
            m_gCursanti.DataBindingComplete += m_gCursanti_DataBindingComplete;
            m_gCursanti.DoubleClick         += m_gCursanti_DoubleClick;
            CautaCursanti();
        }

        private void m_gCursanti_DoubleClick(object? sender, EventArgs e)
        {
            if (!FrmGestiuneCursant.PoateDeschide(ManagerDeProiect)) return;
            object? val = m_gCursanti.GetColumnData("CUCU_Id");
            if (val == null || val == DBNull.Value) return;
            int cucuId = Convert.ToInt32(val);
            var frm = new FrmGestiuneCursant(FrmParent, ManagerDeProiect, cucuId)
            {
                MdiParent = FrmParent
            };
            frm.Show();
        }

        private void m_gCursanti_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            int totalWidth = m_gCursanti.RowHeadersWidth;
            foreach (DataGridViewColumn col in m_gCursanti.Columns)
                if (col.Visible) totalWidth += col.Width;
            // add small margin so last column isn't clipped
            m_splitMain.SplitterDistance = Math.Min(totalWidth + 6, m_splitMain.Width - m_splitMain.Panel2MinSize - m_splitMain.SplitterWidth);
        }

        private void CautaCursanti()
        {
            string filtruNume   = m_ebNume.Text.Trim();
            string filtruPrenume = m_ebPrenume.Text.Trim();
            bool   doarActivi   = m_ckActivi.Checked;

            string sql = @"
                SELECT ROW_NUMBER() OVER (ORDER BY C.CUCU_Nume, C.CUCU_Prenume) AS [Nr.],
                       C.CUCU_Id                                    AS [CUCU_Id],
                       C.CUCU_Nume                                  AS [Nume],
                       C.CUCU_Prenume                               AS [Prenume],
                       ISNULL(CAST(
                           (SELECT TOP 1 D.CUTD_RatingLaTurneu
                              FROM CU_TurneeDetaliu D
                              JOIN CU_Jucatori J ON D.CUTD_CUJA_Id = J.CUJA_Id
                             WHERE J.CUJA_CUCU_Id = C.CUCU_Id
                             ORDER BY D.CUTD_Id DESC)
                       AS NVARCHAR(6)), N'—')                       AS [Rating]
                  FROM CU_Cursanti C
                 WHERE (@nume    = N'' OR C.CUCU_Nume    LIKE N'%' + @nume    + N'%')
                   AND (@prenume = N'' OR C.CUCU_Prenume LIKE N'%' + @prenume + N'%')
                   AND (@activi  = 0   OR ISNULL(CUCU_FaraContract, 0) = 1
                                       OR C.CUCU_Id IN (
                                           SELECT CUCC_CUCU_Id
                                             FROM CU_ContracteCursanti
                                            INNER JOIN CU_Contracte ON CUCO_Id = CUCC_CUCO_Id
                                            INNER JOIN CU_AbonamenteCursant ON CUAC_CUCO_Id = CUCC_CUCO_Id
                                            WHERE ISNULL(CUCO_Retras, 0) = 0
                                              AND ISNULL(CUCC_Retras, 0) = 0
                                              AND CUAC_DataInceput <= CAST(GETDATE() AS DATE)
                                              AND (CUAC_DataSfarsit IS NULL OR CUAC_DataSfarsit >= CAST(GETDATE() AS DATE))
                                       ))
                 ORDER BY C.CUCU_Nume, C.CUCU_Prenume";

            m_gCursanti.SetSql(sql,
                CInterogareSQL.P("@nume",    filtruNume),
                CInterogareSQL.P("@prenume", filtruPrenume),
                CInterogareSQL.P("@activi",  doarActivi ? 1 : 0));
            m_gCursanti.FormatColumn("Nr.",      40,  true, DataGridViewContentAlignment.MiddleRight);
            m_gCursanti.FormatColumn("CUCU_Id",  0,   false);
            m_gCursanti.FormatColumn("Nume",     150, true);
            m_gCursanti.FormatColumn("Prenume",  130, true);
            m_gCursanti.FormatColumn("Rating",   65,  true, DataGridViewContentAlignment.MiddleRight);
        }

        private void IncarcaSinteza(int cucuId)
        {
            if (cucuId <= 0) { m_lbV.Text = ""; m_lbR.Text = ""; m_lbI.Text = ""; m_lbStats.Text = ""; m_lbSep.Visible = false; return; }

            const string sql = @"
                SELECT COUNT(*)                                                          AS Total,
                       SUM(CASE WHEN J.CUTJ_Rezultat = 1   THEN 1 ELSE 0 END)          AS Victorii,
                       SUM(CASE WHEN J.CUTJ_Rezultat = 0.5 THEN 1 ELSE 0 END)          AS Remize,
                       SUM(CASE WHEN J.CUTJ_Rezultat = 0   THEN 1 ELSE 0 END)          AS Infrangeri,
                       CAST(SUM(J.CUTJ_Rezultat) AS DECIMAL(6,1))                      AS Scor,
                       CAST(AVG(CAST(J.CUTJ_RatingAdv AS FLOAT)) AS DECIMAL(7,0))       AS MediaEloAdv,
                       CAST(
                           AVG(CAST(J.CUTJ_RatingAdv AS FLOAT))
                           + 400.0
                             * (SUM(CASE WHEN J.CUTJ_Rezultat = 1   THEN 1.0 ELSE 0.0 END)
                              - SUM(CASE WHEN J.CUTJ_Rezultat = 0   THEN 1.0 ELSE 0.0 END))
                             / NULLIF(COUNT(*), 0)
                       AS DECIMAL(7,0))                                                AS Performanta,
                       CAST(SUM(ISNULL(J.CUTJ_RtgDelta, 0)) AS DECIMAL(6,1))          AS RtgDeltaTotal,
                       COUNT(DISTINCT D.CUTD_CUTE_Id)                                  AS NrTurnee
                  FROM CU_TurneeJocuri J
                  JOIN CU_TurneeDetaliu D ON J.CUTJ_CUTD_Id  = D.CUTD_Id
                  JOIN CU_Jucatori MINE   ON D.CUTD_CUJA_Id  = MINE.CUJA_Id
                 WHERE MINE.CUJA_CUCU_Id = @cucu
                   AND J.CUTJ_NrStartAdv IS NOT NULL";

            using var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            q.OpenP(sql, CInterogareSQL.P("@cucu", cucuId));
            if (!q.NextRow()) { m_lbV.Text = ""; m_lbR.Text = ""; m_lbI.Text = ""; m_lbStats.Text = ""; return; }

            int     total      = q.GetIntValue(0);
            int     victorii   = q.GetIntValue(1);
            int     remize     = q.GetIntValue(2);
            int     infrangeri = q.GetIntValue(3);
            decimal scor       = q.GetDecimalValue(4) ?? 0;
            decimal? mediaElo  = q.GetDecimalValue(5);
            decimal? perf      = q.GetDecimalValue(6);
            decimal rtgDelta   = q.GetDecimalValue(7) ?? 0;
            int     nrTurnee   = q.GetIntValue(8);
            q.Close();

            if (total == 0)
            {
                m_lbV.Text = ""; m_lbR.Text = ""; m_lbI.Text = "";
                m_lbStats.Text = "Nicio partidă înregistrată.";
                m_lbSep.Visible = true;
                return;
            }

            string scorStr  = scor == Math.Floor(scor) ? $"{scor:0}" : $"{Math.Floor(scor):0}½";
            int    proc     = (int)Math.Round(scor * 100 / total);
            string eloStr   = mediaElo.HasValue ? ((int)mediaElo.Value).ToString() : "—";
            string perfStr  = perf.HasValue     ? ((int)perf.Value).ToString()     : "—";
            string deltaStr = rtgDelta >= 0     ? $"+{rtgDelta:0.#}" : $"{rtgDelta:0.#}";

            m_lbSep.Visible = true;
            m_lbV.Text    = $"V: {victorii}";
            m_lbR.Text    = $"R: {remize}";
            m_lbI.Text    = $"Î: {infrangeri}";
            m_lbStats.Text =
                $"Scor: {scorStr}/{total} ({proc}%)     " +
                $"Elo adv. med.: {eloStr}     " +
                $"Perf.: {perfStr}     " +
                $"Rtg+/-: {deltaStr}     " +
                $"Turnee: {nrTurnee}";
        }

        private void IncarcaPartide(int cucuId)
        {
            if (cucuId <= 0) { m_gPartide.DataSource = null; return; }

            const string sql = @"
                SELECT ROW_NUMBER() OVER (ORDER BY T.CUTE_Data DESC, J.CUTJ_Runda DESC)
                                                                               AS [Nr.],
                       J.CUTJ_Runda                                            AS [Rd.],
                       ISNULL(ADV.CUJA_Nume + N' ' + ADV.CUJA_Prenume, N'—') AS [Adversar],
                       ISNULL(CAST(J.CUTJ_RatingAdv AS NVARCHAR(6)), N'—')    AS [Rtg adv],
                       CASE J.CUTJ_Culoare
                           WHEN 1 THEN N'Alb' WHEN 2 THEN N'Negru' ELSE N'—'
                       END                                                     AS [Culoare],
                       CASE WHEN J.CUTJ_Rezultat = 1   THEN N'1'
                            WHEN J.CUTJ_Rezultat = 0.5 THEN N'½'
                            ELSE N'0' END                                      AS [Rez.],
                       ISNULL(CAST(J.CUTJ_WeExpected AS NVARCHAR(8)), N'—')   AS [We],
                       ISNULL(CAST(J.CUTJ_RtgDelta   AS NVARCHAR(8)), N'—')   AS [Rtg+/-],
                       T.CUTE_Denumire                                         AS [Turneu],
                       CASE T.CUTE_Tip
                           WHEN 1 THEN N'Clasic' WHEN 2 THEN N'Rapid'
                           WHEN 3 THEN N'Blitz'  ELSE N'—' END                AS [Tip],
                       CONVERT(NVARCHAR(10), T.CUTE_Data, 104)                 AS [Data]
                  FROM CU_TurneeJocuri J
                  JOIN CU_TurneeDetaliu D   ON J.CUTJ_CUTD_Id     = D.CUTD_Id
                  JOIN CU_Turnee T          ON D.CUTD_CUTE_Id      = T.CUTE_Id
                  JOIN CU_Jucatori MINE     ON D.CUTD_CUJA_Id      = MINE.CUJA_Id
                 LEFT JOIN CU_Jucatori ADV  ON J.CUTJ_CUJA_Id_Adv  = ADV.CUJA_Id
                 WHERE MINE.CUJA_CUCU_Id = @cucu
                   AND J.CUTJ_NrStartAdv IS NOT NULL
                 ORDER BY T.CUTE_Data DESC, J.CUTJ_Runda DESC";

            m_gPartide.SetSql(sql, CInterogareSQL.P("@cucu", cucuId));
            m_gPartide.FormatColumn("Nr.",      40,  true, DataGridViewContentAlignment.MiddleRight);
            m_gPartide.FormatColumn("Rd.",      38,  true, DataGridViewContentAlignment.MiddleCenter);
            m_gPartide.FormatColumn("Adversar", 210, true);
            m_gPartide.FormatColumn("Rtg adv",  65,  true, DataGridViewContentAlignment.MiddleRight);
            m_gPartide.FormatColumn("Culoare",  65,  true, DataGridViewContentAlignment.MiddleCenter);
            m_gPartide.FormatColumn("Rez.",     45,  true, DataGridViewContentAlignment.MiddleCenter);
            m_gPartide.FormatColumn("We",       55,  true, DataGridViewContentAlignment.MiddleRight);
            m_gPartide.FormatColumn("Rtg+/-",   65,  true, DataGridViewContentAlignment.MiddleRight);
            m_gPartide.FormatColumn("Turneu",   240, true);
            m_gPartide.FormatColumn("Tip",       60, true, DataGridViewContentAlignment.MiddleCenter);
            m_gPartide.FormatColumn("Data",      90, true, DataGridViewContentAlignment.MiddleCenter);
            foreach (DataGridViewColumn c in m_gPartide.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void m_gCursanti_SelectionChanged(object? sender, EventArgs e)
        {
            object? val = m_gCursanti.GetColumnData("CUCU_Id");
            if (val == null || val == DBNull.Value) { m_gPartide.DataSource = null; return; }
            int cucuId = Convert.ToInt32(val);
            m_lbNume.Text = $"{m_gCursanti.GetColumnData("Nume")} {m_gCursanti.GetColumnData("Prenume")}".Trim();
            IncarcaSinteza(cucuId);
            IncarcaPartide(cucuId);
        }

        private void m_ebNume_TextChanged(object sender, EventArgs e)    => CautaCursanti();
        private void m_ebPrenume_TextChanged(object sender, EventArgs e) => CautaCursanti();
        private void m_ckActivi_CheckedChanged(object sender, EventArgs e) => CautaCursanti();
    }
}

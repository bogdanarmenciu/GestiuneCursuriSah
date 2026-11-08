using GestiunePrezente.Common;
using GestiunePrezente.MeniuPrincipal;

namespace GestiunePrezente.MeniuCursanti
{
    public partial class FrmGestiuneTurnee : Form
    {
        readonly CManagerDeProiect ManagerDeProiect;
        readonly FrmMeniuPrincipal FrmParent;
        int m_lCUTE;

        private record CursantItem(int Id, string Nume)
        {
            public override string ToString() => Nume;
        }

        private static readonly string[] TipuriProba = { "— Tip —", "Clasic", "Rapid", "Blitz", "Alte" };

        public const string MBME_NUME = "Gestiune Turnee";
        public static bool PoateDeschide(CManagerDeProiect mm)
            => DrepturiFormular.PoateDeschide(mm, MBME_NUME);

        public FrmGestiuneTurnee(FrmMeniuPrincipal formParent, CManagerDeProiect mm, int cuteId)
        {
            FrmParent = formParent;
            ManagerDeProiect = mm;
            m_lCUTE = cuteId;
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            GridPersonalizat.CustomGrid.SetConnection(ManagerDeProiect.GetConnection());

            m_cbTip.Items.AddRange(TipuriProba);
            m_cbTip.SelectedIndex = 0;

            // discheta se activeaza doar cand antetul e modificat
            m_tsbSalveaza.Enabled = false;
            m_dtpData.ValueChanged      += (s, e) => m_tsbSalveaza.Enabled = true;
            m_ebDenumire.TextChanged    += (s, e) => m_tsbSalveaza.Enabled = true;
            m_ebLocalitate.TextChanged  += (s, e) => m_tsbSalveaza.Enabled = true;
            m_cbTip.SelectedIndexChanged  += (s, e) => m_tsbSalveaza.Enabled = true;
            m_ebOrganizator.TextChanged  += (s, e) => m_tsbSalveaza.Enabled = true;
            m_ebDirector.TextChanged     += (s, e) => m_tsbSalveaza.Enabled = true;
            m_ebArbSef.TextChanged       += (s, e) => m_tsbSalveaza.Enabled = true;
            m_ebCadenta.TextChanged      += (s, e) => m_tsbSalveaza.Enabled = true;

            m_gParticipanti.DataBindingComplete += (s, e) => {
                foreach (DataGridViewRow row in m_gParticipanti.Rows)
                {
                    object? cucu = row.Cells["CUJA_CUCU_Id"]?.Value;
                    bool isOnixteia = cucu != null && cucu != DBNull.Value && Convert.ToInt32(cucu) > 0;
                    row.DefaultCellStyle.BackColor = isOnixteia ? Color.FromArgb(200, 255, 200) : Color.White;
                }
            };

            IncarcaCursanti();

            if (m_lCUTE > 0)
                IncarcaTurneu();
        }

        private void IncarcaCursanti()
        {
            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            q.OpenP("SELECT CUCU_Id, CUCU_Nume + N' ' + CUCU_Prenume FROM CU_Cursanti ORDER BY CUCU_Nume, CUCU_Prenume");
            m_cbCursant.Items.Clear();
            while (q.NextRow())
                m_cbCursant.Items.Add(new CursantItem(q.GetIntValue(0), q.GetStringValue(1)));
            q.Close();
            if (m_cbCursant.Items.Count > 0)
                m_cbCursant.SelectedIndex = 0;
        }

        private void IncarcaTurneu()
        {
            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            q.OpenP(@"SELECT CUTE_Data, CUTE_Denumire, CUTE_Localitate, ISNULL(CAST(CUTE_Tip AS INT),0),
                             ISNULL(CUTE_Organizator,''), ISNULL(CUTE_DirectorTurneu,''),
                             ISNULL(CUTE_ArbSef,''), ISNULL(CUTE_Cadenta,'')
                        FROM CU_Turnee WHERE CUTE_Id = @id",
                CInterogareSQL.P("@id", m_lCUTE));
            if (q.NextRow())
            {
                DateTime? data = q.GetDateValue(0);
                if (data.HasValue) m_dtpData.Value = data.Value;
                m_ebDenumire.Text    = q.GetStringValue(1);
                m_ebLocalitate.Text  = q.GetStringValue(2);
                int tip = q.GetIntValue(3);
                m_cbTip.SelectedIndex = tip >= 0 && tip < m_cbTip.Items.Count ? tip : 0;
                m_ebOrganizator.Text = q.GetStringValue(4);
                m_ebDirector.Text    = q.GetStringValue(5);
                m_ebArbSef.Text      = q.GetStringValue(6);
                m_ebCadenta.Text     = q.GetStringValue(7);
            }
            q.Close();
            m_tsbSalveaza.Enabled = false;
            IncarcaParticipanti();
        }

        private void IncarcaParticipanti()
        {
            if (m_lCUTE <= 0) return;
            string sql = @"
                SELECT D.CUTD_Id,
                       J.CUJA_Id,
                       J.CUJA_CUCU_Id,
                       D.CUTD_NrStart                AS [Nr.],
                       CASE WHEN J.CUJA_CUCU_Id IS NOT NULL
                            THEN N'★ ' + J.CUJA_Nume + N' ' + J.CUJA_Prenume
                            ELSE J.CUJA_Nume + N' ' + J.CUJA_Prenume
                       END                           AS Jucator,
                       J.CUJA_Fed                   AS FED,
                       D.CUTD_RatingLaTurneu         AS Rating,
                       CASE CAST(D.CUTD_Tip AS INT)
                           WHEN 1 THEN N'Clasic'
                           WHEN 2 THEN N'Rapid'
                           WHEN 3 THEN N'Blitz'
                           ELSE N'Alte'
                       END                           AS Tip,
                       D.CUTD_Puncte                AS Puncte,
                       D.CUTD_Loc                   AS Loc,
                       D.CUTD_RatingPerformanta      AS Perf
                  FROM CU_TurneeDetaliu D
                  JOIN CU_Jucatori J ON D.CUTD_CUJA_Id = J.CUJA_Id
                 WHERE D.CUTD_CUTE_Id = @cute
                 ORDER BY D.CUTD_Loc, D.CUTD_NrStart";

            m_gParticipanti.SetSql(sql, CInterogareSQL.P("@cute", m_lCUTE));
            m_gParticipanti.FormatColumn("CUTD_Id",    60, false);
            m_gParticipanti.FormatColumn("CUJA_Id",    60, false);
            m_gParticipanti.FormatColumn("CUJA_CUCU_Id", 60, false);
            m_gParticipanti.FormatColumn("Nr.",         40, true,  DataGridViewContentAlignment.MiddleCenter);
            m_gParticipanti.FormatColumn("Jucator",    260);
            m_gParticipanti.FormatColumn("FED",         40);
            m_gParticipanti.FormatColumn("Rating",      55, true,  DataGridViewContentAlignment.MiddleRight);
            m_gParticipanti.FormatColumn("Tip",         60);
            m_gParticipanti.FormatColumn("Puncte",      60, true,  DataGridViewContentAlignment.MiddleRight);
            m_gParticipanti.FormatColumn("Loc",         45, true,  DataGridViewContentAlignment.MiddleRight);
            m_gParticipanti.FormatColumn("Perf",        55, true,  DataGridViewContentAlignment.MiddleRight);
            m_gParticipanti.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn c in m_gParticipanti.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
            // permite sortare pe coloana Jucator ca sa se poata cauta usor
            if (m_gParticipanti.Columns["Jucator"] != null)
                m_gParticipanti.Columns["Jucator"].SortMode = DataGridViewColumnSortMode.Automatic;
            m_gParticipanti.SelectionChanged += m_gParticipanti_SelectionChanged;
        }

        private void m_gParticipanti_SelectionChanged(object sender, EventArgs e)
        {
            object? cucu = m_gParticipanti.GetColumnData("CUJA_CUCU_Id");
            m_btnAsociazaCursant.Enabled = cucu == null || cucu == DBNull.Value;
        }

        private void m_tsbSalveaza_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(m_ebDenumire.Text))
            {
                MessageBox.Show("Denumirea turneului este obligatorie.", "Atenție", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                m_ebDenumire.Focus();
                return;
            }

            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            try
            {
                object tipVal  = m_cbTip.SelectedIndex > 0 ? (object)(byte)m_cbTip.SelectedIndex : DBNull.Value;
                object locVal  = string.IsNullOrWhiteSpace(m_ebLocalitate.Text)  ? DBNull.Value : (object)m_ebLocalitate.Text.Trim();
                object orgVal  = string.IsNullOrWhiteSpace(m_ebOrganizator.Text) ? DBNull.Value : (object)m_ebOrganizator.Text.Trim();
                object dirVal  = string.IsNullOrWhiteSpace(m_ebDirector.Text)    ? DBNull.Value : (object)m_ebDirector.Text.Trim();
                object arbVal  = string.IsNullOrWhiteSpace(m_ebArbSef.Text)      ? DBNull.Value : (object)m_ebArbSef.Text.Trim();
                object cadVal  = string.IsNullOrWhiteSpace(m_ebCadenta.Text)     ? DBNull.Value : (object)m_ebCadenta.Text.Trim();

                if (m_lCUTE == 0)
                {
                    q.ExecuteNonQueryP(
                        @"INSERT INTO CU_Turnee (CUTE_Data,CUTE_Denumire,CUTE_Localitate,CUTE_Tip,
                                                 CUTE_Organizator,CUTE_DirectorTurneu,CUTE_ArbSef,CUTE_Cadenta)
                          VALUES (@d,@den,@loc,@tip,@org,@dir,@arb,@cad)",
                        CInterogareSQL.P("@d",   m_dtpData.Value.Date),
                        CInterogareSQL.P("@den", m_ebDenumire.Text.Trim()),
                        CInterogareSQL.P("@loc", locVal),
                        CInterogareSQL.P("@tip", tipVal),
                        CInterogareSQL.P("@org", orgVal),
                        CInterogareSQL.P("@dir", dirVal),
                        CInterogareSQL.P("@arb", arbVal),
                        CInterogareSQL.P("@cad", cadVal));
                    m_lCUTE = q.ExecuteToIntP("SELECT CAST(SCOPE_IDENTITY() AS INT)");
                    Text = "Gestiune turneu";
                    IncarcaParticipanti();
                }
                else
                {
                    q.ExecuteNonQueryP(
                        @"UPDATE CU_Turnee SET CUTE_Data=@d,CUTE_Denumire=@den,CUTE_Localitate=@loc,CUTE_Tip=@tip,
                                              CUTE_Organizator=@org,CUTE_DirectorTurneu=@dir,CUTE_ArbSef=@arb,CUTE_Cadenta=@cad
                          WHERE CUTE_Id=@id",
                        CInterogareSQL.P("@d",   m_dtpData.Value.Date),
                        CInterogareSQL.P("@den", m_ebDenumire.Text.Trim()),
                        CInterogareSQL.P("@loc", locVal),
                        CInterogareSQL.P("@tip", tipVal),
                        CInterogareSQL.P("@org", orgVal),
                        CInterogareSQL.P("@dir", dirVal),
                        CInterogareSQL.P("@arb", arbVal),
                        CInterogareSQL.P("@cad", cadVal),
                        CInterogareSQL.P("@id",  m_lCUTE));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la salvare: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally { q.Close(); }
            m_tsbSalveaza.Enabled = false;
        }

        private void m_tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_tabMain.SelectedTab == m_tabCursanti && m_lCUTE > 0)
                IncarcaCursantiTurneu();
        }

        private void IncarcaCursantiTurneu()
        {
            if (m_lCUTE <= 0) return;
            string sql = @"
                SELECT D.CUTD_Id,
                       J.CUJA_CUCU_Id,
                       J.CUJA_Nume + N' ' + J.CUJA_Prenume AS [Cursant],
                       D.CUTD_NrStart                       AS [Nr.],
                       D.CUTD_RatingLaTurneu                AS [Rating],
                       D.CUTD_Puncte                        AS [Puncte],
                       D.CUTD_Loc                           AS [Loc],
                       D.CUTD_RatingPerformanta             AS [Perf]
                  FROM CU_TurneeDetaliu D
                  JOIN CU_Jucatori J ON D.CUTD_CUJA_Id = J.CUJA_Id
                 WHERE D.CUTD_CUTE_Id = @cute
                   AND J.CUJA_CUCU_Id IS NOT NULL
                 ORDER BY D.CUTD_Loc, D.CUTD_NrStart";

            m_gCursantiTurneu.SetSql(sql, CInterogareSQL.P("@cute", m_lCUTE));
            m_gCursantiTurneu.FormatColumn("CUTD_Id",     60,  false);
            m_gCursantiTurneu.FormatColumn("CUJA_CUCU_Id",60,  false);
            m_gCursantiTurneu.FormatColumn("Cursant",     220, true);
            m_gCursantiTurneu.FormatColumn("Nr.",          45, true, DataGridViewContentAlignment.MiddleCenter);
            m_gCursantiTurneu.FormatColumn("Rating",       65, true, DataGridViewContentAlignment.MiddleRight);
            m_gCursantiTurneu.FormatColumn("Puncte",       65, true, DataGridViewContentAlignment.MiddleRight);
            m_gCursantiTurneu.FormatColumn("Loc",          45, true, DataGridViewContentAlignment.MiddleRight);
            m_gCursantiTurneu.FormatColumn("Perf",         60, true, DataGridViewContentAlignment.MiddleRight);
            foreach (DataGridViewColumn c in m_gCursantiTurneu.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;

            m_gCursantiTurneu.CellDoubleClick -= GCursantiTurneu_CellDoubleClick;
            m_gCursantiTurneu.CellDoubleClick += GCursantiTurneu_CellDoubleClick;
        }

        private void GCursantiTurneu_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            object? cucu = m_gCursantiTurneu.GetColumnData("CUJA_CUCU_Id");
            if (cucu == null || cucu == DBNull.Value) return;
            int cucuId = Convert.ToInt32(cucu);
            if (cucuId <= 0) return;
            if (!FrmGestiuneCursant.PoateDeschide(ManagerDeProiect)) return;
            var frm = new FrmGestiuneCursant(FrmParent, ManagerDeProiect, cucuId) { MdiParent = FrmParent };
            frm.Show();
        }

        private void m_gCursantiTurneu_SelectionChanged(object sender, EventArgs e)
        {
            object? val = m_gCursantiTurneu.GetColumnData("CUTD_Id");
            if (val == null || val == DBNull.Value) return;
            IncarcaRundeCursant(Convert.ToInt32(val));
        }

        private void IncarcaRundeCursant(int cutdId)
        {
            if (cutdId <= 0) return;
            string sql = @"
                SELECT J.CUTJ_Runda                                           AS [Rd.],
                       ISNULL(ADV.CUJA_Nume + N' ' + ADV.CUJA_Prenume, N'—') AS [Adversar],
                       ISNULL(CAST(J.CUTJ_RatingAdv AS NVARCHAR(6)), N'—')   AS [Rtg adv],
                       CASE J.CUTJ_Culoare
                           WHEN 1 THEN N'Alb' WHEN 2 THEN N'Negru' ELSE N'—'
                       END                                                    AS [Culoare],
                       CASE WHEN J.CUTJ_Rezultat = 1   THEN N'1'
                            WHEN J.CUTJ_Rezultat = 0.5 THEN N'½'
                            ELSE N'0' END                                     AS [Rez.],
                       ISNULL(CAST(J.CUTJ_WeExpected AS NVARCHAR(8)), N'—')  AS [We],
                       ISNULL(CAST(J.CUTJ_RtgDelta   AS NVARCHAR(8)), N'—')  AS [Rtg+/-]
                  FROM CU_TurneeJocuri J
                  LEFT JOIN CU_Jucatori ADV ON J.CUTJ_CUJA_Id_Adv = ADV.CUJA_Id
                 WHERE J.CUTJ_CUTD_Id = @cutdId
                 ORDER BY J.CUTJ_Runda";

            m_gRunde.SetSql(sql, CInterogareSQL.P("@cutdId", cutdId));
            m_gRunde.FormatColumn("Rd.",      40,  true, DataGridViewContentAlignment.MiddleCenter);
            m_gRunde.FormatColumn("Adversar", 220, true);
            m_gRunde.FormatColumn("Rtg adv",  65,  true, DataGridViewContentAlignment.MiddleRight);
            m_gRunde.FormatColumn("Culoare",  65,  true, DataGridViewContentAlignment.MiddleCenter);
            m_gRunde.FormatColumn("Rez.",     45,  true, DataGridViewContentAlignment.MiddleCenter);
            m_gRunde.FormatColumn("We",       55,  true, DataGridViewContentAlignment.MiddleRight);
            m_gRunde.FormatColumn("Rtg+/-",   65,  true, DataGridViewContentAlignment.MiddleRight);
            foreach (DataGridViewColumn c in m_gRunde.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void m_tsbImport_Click(object sender, EventArgs e)
        {
            using var dlg = new FrmImportChessResults(ManagerDeProiect, m_lCUTE);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                m_lCUTE = dlg.CUTEId;
                IncarcaTurneu();
            }
        }

        private void m_btnAsociazaCursant_Click(object sender, EventArgs e)
        {
            object? cujaVal = m_gParticipanti.GetColumnData("CUJA_Id");
            if (cujaVal == null || cujaVal == DBNull.Value) return;
            int cujaId = Convert.ToInt32(cujaVal);

            // dialog inline — form cu combobox de cursanti
            var frm = new Form
            {
                Text = "Asociază cu cursant ONIXTEIA",
                Size = new Size(420, 150),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MinimizeBox = false, MaximizeBox = false
            };
            var lbl = new Label { Text = "Cursant:", Location = new Point(12, 20), AutoSize = true };
            var cb  = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(80, 16), Size = new Size(300, 23), TabIndex = 0
            };
            foreach (CursantItem c in m_cbCursant.Items) cb.Items.Add(c);
            if (cb.Items.Count > 0) cb.SelectedIndex = 0;

            var btnOk  = new Button { Text = "Asociază", DialogResult = DialogResult.OK,  Location = new Point(190, 60), Width = 100 };
            var btnAnn = new Button { Text = "Anulează", DialogResult = DialogResult.Cancel, Location = new Point(300, 60), Width = 90 };
            frm.Controls.AddRange(new Control[] { lbl, cb, btnOk, btnAnn });
            frm.AcceptButton = btnOk; frm.CancelButton = btnAnn;

            if (frm.ShowDialog(this) != DialogResult.OK) return;
            if (cb.SelectedItem is not CursantItem cursant) return;

            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            try
            {
                q.ExecuteNonQueryP(
                    "UPDATE CU_Jucatori SET CUJA_CUCU_Id=@cucu WHERE CUJA_Id=@cuja",
                    CInterogareSQL.P("@cucu", cursant.Id),
                    CInterogareSQL.P("@cuja", cujaId));
            }
            catch (Exception ex) { MessageBox.Show("Eroare: " + ex.Message); return; }
            finally { q.Close(); }

            IncarcaParticipanti();
            if (m_tabMain.SelectedTab == m_tabCursanti) IncarcaCursantiTurneu();
        }

        private void m_btnStergeParticipant_Click(object sender, EventArgs e)
        {
            object? val = m_gParticipanti.GetColumnData("CUTD_Id");
            if (val == null || val == DBNull.Value) return;
            int cutdId = Convert.ToInt32(val);

            if (MessageBox.Show("Ștergi participantul selectat?", "Confirmare",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            q.ExecuteNonQueryP("DELETE FROM CU_TurneeJocuri  WHERE CUTJ_CUTD_Id = @id", CInterogareSQL.P("@id", cutdId));
            q.ExecuteNonQueryP("DELETE FROM CU_TurneeDetaliu WHERE CUTD_Id      = @id", CInterogareSQL.P("@id", cutdId));
            q.Close();
            IncarcaParticipanti();
        }
    }
}

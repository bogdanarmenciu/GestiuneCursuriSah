using GestiunePrezente.Common;

namespace GestiunePrezente.MeniuCursanti
{
    public class FrmImportChessResults : Form
    {
        readonly CManagerDeProiect ManagerDeProiect;
        int m_lCUTE;
        public int CUTEId => m_lCUTE;

        ChessResultsTournament? m_tournStart;

        DataGridView m_grid = null!;
        Button m_btnStartList = null!, m_btnCrosstable = null!, m_btnImport = null!, m_btnAnuleaza = null!;
        Label m_lblStatus = null!;

        record CursantDB(int Id, string Nume, string Prenume, int FideId);
        List<CursantDB> m_cursanti = new();

        class CursantComboItem
        {
            public int    Id     { get; init; }
            public string Afisaj { get; init; } = "";
        }
        List<CursantComboItem> m_comboItems = new();

        public FrmImportChessResults(CManagerDeProiect mm, int cuteId)
        {
            ManagerDeProiect = mm;
            m_lCUTE = cuteId;
            IncarcaCursantiDB();
            BuildComboItems();
            InitUI();
        }

        private void IncarcaCursantiDB()
        {
            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            q.OpenP("SELECT CUCU_Id, CUCU_Nume, CUCU_Prenume, ISNULL(CUCU_FideId,0) FROM CU_Cursanti ORDER BY CUCU_Nume, CUCU_Prenume");
            while (q.NextRow())
                m_cursanti.Add(new CursantDB(q.GetIntValue(0), q.GetStringValue(1), q.GetStringValue(2), q.GetIntValue(3)));
            q.Close();
        }

        private void BuildComboItems()
        {
            m_comboItems.Add(new CursantComboItem { Id = 0, Afisaj = "— extern —" });
            foreach (var c in m_cursanti)
                m_comboItems.Add(new CursantComboItem { Id = c.Id, Afisaj = $"{c.Nume} {c.Prenume}" });
        }

        private void InitUI()
        {
            Text = "Import chess-results";
            Size = new Size(1150, 680);
            StartPosition = FormStartPosition.CenterParent;

            var panelTop = new FlowLayoutPanel
            {
                Dock = DockStyle.Top, Height = 45, FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(5)
            };

            m_btnStartList  = new Button { Text = "1. Starting List (XLS)", Width = 180, Height = 30 };
            m_btnCrosstable = new Button { Text = "2. Crosstable (XLS)",    Width = 160, Height = 30, Enabled = false };
            m_btnImport     = new Button { Text = "3. Importă",             Width = 100, Height = 30, Enabled = false };
            m_btnAnuleaza   = new Button { Text = "Anulează",               Width = 90,  Height = 30 };
            m_lblStatus     = new Label  { AutoSize = true, ForeColor = Color.DarkBlue, Width = 500 };

            panelTop.Controls.AddRange(new Control[] { m_btnStartList, m_btnCrosstable, m_btnImport, m_btnAnuleaza, m_lblStatus });

            var colCursant = new DataGridViewComboBoxColumn
            {
                Name          = "colCursant",
                HeaderText    = "Cursant ONIXTEIA  (click pt. asociere manuala)",
                Width         = 290,
                DataSource    = m_comboItems,
                DisplayMember = "Afisaj",
                ValueMember   = "Id",
                FlatStyle     = FlatStyle.Flat
            };

            m_grid = new DataGridView
            {
                Dock         = DockStyle.Fill,
                ReadOnly     = false,
                AllowUserToAddRows = false,
                SelectionMode= DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
                RowHeadersWidth = 25,
                EditMode     = DataGridViewEditMode.EditOnEnter
            };

            m_grid.Columns.AddRange(
                new DataGridViewTextBoxColumn { Name = "colNrStart", HeaderText = "Nr.",    Width = 42,  ReadOnly = true },
                new DataGridViewTextBoxColumn { Name = "colNume",    HeaderText = "Jucator (chess-results)", Width = 215, ReadOnly = true },
                new DataGridViewTextBoxColumn { Name = "colFed",     HeaderText = "FED",    Width = 42,  ReadOnly = true },
                new DataGridViewTextBoxColumn { Name = "colRtg",     HeaderText = "Rtg",    Width = 50,  ReadOnly = true },
                new DataGridViewTextBoxColumn { Name = "colLoc",     HeaderText = "Loc",    Width = 42,  ReadOnly = true },
                new DataGridViewTextBoxColumn { Name = "colPct",     HeaderText = "Pct",    Width = 50,  ReadOnly = true },
                new DataGridViewTextBoxColumn { Name = "colPerf",    HeaderText = "Perf",   Width = 50,  ReadOnly = true },
                colCursant
            );

            foreach (DataGridViewColumn c in m_grid.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;

            m_grid.CurrentCellDirtyStateChanged += (s, e) => {
                if (m_grid.IsCurrentCellDirty && m_grid.CurrentCell is DataGridViewComboBoxCell)
                    m_grid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };
            m_grid.CellValueChanged += Grid_CellValueChanged;

            m_btnStartList.Click  += BtnStartList_Click;
            m_btnCrosstable.Click += BtnCrosstable_Click;
            m_btnImport.Click     += BtnImport_Click;
            m_btnAnuleaza.Click   += (_, _) => { DialogResult = DialogResult.Cancel; Close(); };

            Controls.Add(m_grid);
            Controls.Add(panelTop);
        }

        private void Grid_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || m_grid.Columns[e.ColumnIndex].Name != "colCursant") return;
            int cucuId = Convert.ToInt32(m_grid.Rows[e.RowIndex].Cells["colCursant"].Value);
            m_grid.Rows[e.RowIndex].DefaultCellStyle.BackColor = cucuId > 0
                ? Color.FromArgb(200, 255, 200) : Color.White;
        }

        private void BtnStartList_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Title  = "Selectează Starting List XLS de la chess-results",
                Filter = "Excel files (*.xls;*.xlsx)|*.xls;*.xlsx|All files (*.*)|*.*"
            };
            if (ofd.ShowDialog() != DialogResult.OK) return;
            try
            {
                m_tournStart = ChessResultsParser.ParseStartingList(ofd.FileName);
                m_lblStatus.Text = $"Starting list: {m_tournStart.Jucatori.Count} jucători. Acum încarcă crosstable-ul.";
                m_btnCrosstable.Enabled = true;
                m_grid.Rows.Clear();
            }
            catch (Exception ex) { MessageBox.Show("Eroare la citire starting list: " + ex.Message); }
        }

        private void BtnCrosstable_Click(object sender, EventArgs e)
        {
            if (m_tournStart == null) return;
            using var ofd = new OpenFileDialog
            {
                Title  = "Selectează Crosstable XLS de la chess-results",
                Filter = "Excel files (*.xls;*.xlsx)|*.xls;*.xlsx|All files (*.*)|*.*"
            };
            if (ofd.ShowDialog() != DialogResult.OK) return;
            try
            {
                ChessResultsParser.MergeCrosstable(m_tournStart, ofd.FileName);
                var ratingByNr = m_tournStart.Jucatori.Where(p => p.Rating > 0)
                    .ToDictionary(p => p.NrStart, p => p.Rating);
                foreach (var p in m_tournStart.Jucatori)
                    if (p.Runde.Count > 0)
                        p.RatingPerf = ChessResultsParser.CalcPerformance(p.Runde, ratingByNr);

                PopuleazaGrid();
                int recunoscuti = m_grid.Rows.Cast<DataGridViewRow>()
                    .Count(r => Convert.ToInt32(r.Cells["colCursant"].Value) > 0);
                m_btnImport.Text  = $"3. Importă toți ({m_tournStart.Jucatori.Count})";
                m_btnImport.Width = 180;
                m_btnImport.Enabled = true;
                m_lblStatus.Text = $"{m_tournStart.Jucatori.Count} jucători | {recunoscuti} cursanți recunoscuți automat (verde) | restul asociază manual din coloana din dreapta";
            }
            catch (Exception ex) { MessageBox.Show("Eroare la citire crosstable: " + ex.Message); }
        }

        private void PopuleazaGrid()
        {
            m_grid.Rows.Clear();
            if (m_tournStart == null) return;
            foreach (var p in m_tournStart.Jucatori
                .OrderBy(x => x.LocFinal > 0 ? x.LocFinal : 9999).ThenBy(x => x.NrStart))
            {
                var (cursant, _) = GasesteCursant(p);
                int cucuId = cursant?.Id ?? 0;
                int idx = m_grid.Rows.Add(
                    p.NrStart,
                    p.NumeFull != "" ? p.NumeFull : p.Nume,
                    p.Fed,
                    p.Rating > 0 ? p.Rating.ToString() : "",
                    p.LocFinal > 0 ? p.LocFinal.ToString() : "",
                    p.Puncte > 0 ? p.Puncte.ToString("0.#") : "",
                    p.RatingPerf > 0 ? p.RatingPerf.ToString() : "",
                    cucuId);
                m_grid.Rows[idx].DefaultCellStyle.BackColor = cucuId > 0
                    ? Color.FromArgb(200, 255, 200) : Color.White;
            }
        }

        private (CursantDB? cursant, string matchType) GasesteCursant(ChessResultsPlayer p)
        {
            if (p.FideId > 0)
            {
                var c = m_cursanti.FirstOrDefault(x => x.FideId == p.FideId);
                if (c != null) return (c, "FIDE ID");
            }
            if (p.NumeFamilie != "" && p.Prenume != "")
            {
                var c = m_cursanti.FirstOrDefault(x =>
                    string.Equals(x.Nume, p.NumeFamilie, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(x.Prenume, p.Prenume,  StringComparison.OrdinalIgnoreCase));
                if (c != null) return (c, "Nume");

                string prenume1 = p.Prenume.Split('-', ' ')[0];
                c = m_cursanti.FirstOrDefault(x =>
                    string.Equals(x.Nume, p.NumeFamilie, StringComparison.OrdinalIgnoreCase) &&
                    x.Prenume.StartsWith(prenume1, StringComparison.OrdinalIgnoreCase));
                if (c != null) return (c, "Nume~");
            }
            return (null, "");
        }

        // Gaseste sau creeaza jucatorul in CU_Jucatori; returneaza CUJA_Id.
        // Foloseste PThrow — exceptiile sunt gestionate de try-catch-ul apelantului.
        private int UpsertJucator(CInterogareSQL q, ChessResultsPlayer player, int cucuId)
        {
            // 1. cauta dupa FIDE ID
            if (player.FideId > 0)
            {
                int id = q.ExecuteToIntPThrow(
                    "SELECT ISNULL((SELECT TOP 1 CUJA_Id FROM CU_Jucatori WHERE CUJA_FideId=@fide), 0)",
                    CInterogareSQL.P("@fide", player.FideId));
                if (id > 0)
                {
                    q.ExecuteNonQueryPThrow(
                        "UPDATE CU_Jucatori SET CUJA_RatingUltim=@rtg, CUJA_Fed=@fed, CUJA_CUCU_Id=ISNULL(CUJA_CUCU_Id,@cucu) WHERE CUJA_Id=@id",
                        CInterogareSQL.P("@rtg",  player.Rating > 0 ? (object)player.Rating : DBNull.Value),
                        CInterogareSQL.P("@fed",  player.Fed != "" ? (object)player.Fed : DBNull.Value),
                        CInterogareSQL.P("@cucu", cucuId > 0 ? (object)cucuId : DBNull.Value),
                        CInterogareSQL.P("@id",   id));
                    return id;
                }
            }

            // 2. cauta dupa nume + prenume
            string numeDB    = player.NumeFamilie != "" ? player.NumeFamilie : player.Nume;
            string prenumeDB = player.Prenume;
            if (numeDB != "")
            {
                int id = q.ExecuteToIntPThrow(
                    "SELECT ISNULL((SELECT TOP 1 CUJA_Id FROM CU_Jucatori WHERE CUJA_Nume=@n AND CUJA_Prenume=@p), 0)",
                    CInterogareSQL.P("@n", numeDB),
                    CInterogareSQL.P("@p", prenumeDB));
                if (id > 0)
                {
                    // actualizeaza FIDE ID daca acum il avem
                    if (player.FideId > 0)
                        q.ExecuteNonQueryPThrow(
                            "UPDATE CU_Jucatori SET CUJA_FideId=@fide, CUJA_RatingUltim=@rtg, CUJA_CUCU_Id=ISNULL(CUJA_CUCU_Id,@cucu) WHERE CUJA_Id=@id AND CUJA_FideId IS NULL",
                            CInterogareSQL.P("@fide", player.FideId),
                            CInterogareSQL.P("@rtg",  player.Rating > 0 ? (object)player.Rating : DBNull.Value),
                            CInterogareSQL.P("@cucu", cucuId > 0 ? (object)cucuId : DBNull.Value),
                            CInterogareSQL.P("@id",   id));
                    return id;
                }
            }

            // 3. jucator nou — INSERT + SCOPE_IDENTITY() in acelasi batch
            int newId = q.ExecuteToIntPThrow(
                @"INSERT INTO CU_Jucatori (CUJA_FideId, CUJA_Nume, CUJA_Prenume, CUJA_Fed, CUJA_RatingUltim, CUJA_CUCU_Id)
                  VALUES (@fide,@n,@p,@fed,@rtg,@cucu);
                  SELECT CAST(SCOPE_IDENTITY() AS INT)",
                CInterogareSQL.P("@fide", player.FideId > 0 ? (object)player.FideId : DBNull.Value),
                CInterogareSQL.P("@n",    numeDB),
                CInterogareSQL.P("@p",    prenumeDB),
                CInterogareSQL.P("@fed",  player.Fed != "" ? (object)player.Fed : DBNull.Value),
                CInterogareSQL.P("@rtg",  player.Rating > 0 ? (object)player.Rating : DBNull.Value),
                CInterogareSQL.P("@cucu", cucuId > 0 ? (object)cucuId : DBNull.Value));
            if (newId == 0) throw new Exception($"INSERT CU_Jucatori esuat pentru '{numeDB} {prenumeDB}'.");
            return newId;
        }

        private static void ArataMesajEroare(string titlu, string mesaj)
        {
            var frm = new Form
            {
                Text = titlu, Size = new Size(620, 280),
                StartPosition = FormStartPosition.CenterParent,
                MinimizeBox = false, MaximizeBox = false
            };
            var tb = new TextBox
            {
                Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Vertical,
                Dock = DockStyle.Fill, BackColor = Color.White,
                Font = new Font("Consolas", 9F), Text = mesaj
            };
            var btn = new Button { Text = "OK", Dock = DockStyle.Bottom, Height = 32 };
            btn.Click += (_, _) => frm.Close();
            frm.Controls.Add(tb);
            frm.Controls.Add(btn);
            frm.ShowDialog();
        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            if (m_tournStart == null || m_tournStart.Jucatori.Count == 0) return;

            var ratingByNr = m_tournStart.Jucatori.Where(p => p.Rating > 0)
                .ToDictionary(p => p.NrStart, p => p.Rating);

            // citeste asocierile manuale din grid (nr.start → cucu_id)
            var cucuByNr = new Dictionary<int, int>();
            foreach (DataGridViewRow row in m_grid.Rows)
            {
                int nr     = Convert.ToInt32(row.Cells["colNrStart"].Value);
                int cucuId = Convert.ToInt32(row.Cells["colCursant"].Value);
                if (cucuId > 0) cucuByNr[nr] = cucuId;
            }

            // citeste data nasterii cursantilor INAINTE de tranzactie (read-only, aceeasi conexiune)
            var dataNasteriiByNr = new Dictionary<int, DateTime>();
            {
                var qDN = new CInterogareSQL(ManagerDeProiect.GetConnection());
                foreach (var kv in cucuByNr)
                {
                    qDN.OpenP("SELECT CUCU_DataNasterii FROM CU_Cursanti WHERE CUCU_Id=@id",
                        CInterogareSQL.P("@id", kv.Value));
                    if (qDN.NextRow()) { var dn = qDN.GetDateValue(0); if (dn.HasValue) dataNasteriiByNr[kv.Key] = dn.Value; }
                    qDN.Close();
                }
            }

            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            q.BeginTransaction(System.Data.IsolationLevel.Serializable);
            int importati = 0;
            try
            {
                // creeaza sau actualizeaza turneul
                string   den  = !string.IsNullOrWhiteSpace(m_tournStart.Denumire) ? m_tournStart.Denumire : "Turneu importat";
                DateTime data = m_tournStart.DataInceput ?? DateTime.Today;

                if (m_lCUTE == 0)
                {
                    // INSERT + SCOPE_IDENTITY() in acelasi batch — scope diferit ar returna NULL
                    m_lCUTE = q.ExecuteToIntPThrow(
                        @"INSERT INTO CU_Turnee (CUTE_Data,CUTE_Denumire,CUTE_DataSfarsit,CUTE_NrRunde,
                                                 CUTE_Organizator,CUTE_DirectorTurneu,CUTE_ArbSef,CUTE_Cadenta,CUTE_TipSistem)
                          VALUES (@d,@den,@d2,@nr,@org,@dir,@arb,@cad,@tsys);
                          SELECT CAST(SCOPE_IDENTITY() AS INT)",
                        CInterogareSQL.P("@d",    data),
                        CInterogareSQL.P("@den",  den),
                        CInterogareSQL.P("@d2",   m_tournStart.DataSfarsit.HasValue ? (object)m_tournStart.DataSfarsit.Value : DBNull.Value),
                        CInterogareSQL.P("@nr",   m_tournStart.NrRunde > 0 ? (object)m_tournStart.NrRunde : DBNull.Value),
                        CInterogareSQL.P("@org",  string.IsNullOrWhiteSpace(m_tournStart.Organizator)    ? DBNull.Value : (object)m_tournStart.Organizator),
                        CInterogareSQL.P("@dir",  string.IsNullOrWhiteSpace(m_tournStart.DirectorTurneu) ? DBNull.Value : (object)m_tournStart.DirectorTurneu),
                        CInterogareSQL.P("@arb",  string.IsNullOrWhiteSpace(m_tournStart.ArbSef)         ? DBNull.Value : (object)m_tournStart.ArbSef),
                        CInterogareSQL.P("@cad",  string.IsNullOrWhiteSpace(m_tournStart.Cadenta)        ? DBNull.Value : (object)m_tournStart.Cadenta),
                        CInterogareSQL.P("@tsys", string.IsNullOrWhiteSpace(m_tournStart.TipSistem)      ? DBNull.Value : (object)m_tournStart.TipSistem));
                    if (m_lCUTE == 0) throw new Exception("INSERT CU_Turnee a esuat — SCOPE_IDENTITY() a returnat NULL.");
                }
                else
                {
                    var sb    = new System.Text.StringBuilder("CUTE_Denumire=@den,CUTE_Data=@d1,");
                    var parms = new List<(string, object)> { ("@den", den), ("@d1", data) };
                    if (m_tournStart.DataSfarsit.HasValue)
                    { sb.Append("CUTE_DataSfarsit=@d2,"); parms.Add(("@d2", m_tournStart.DataSfarsit.Value)); }
                    if (m_tournStart.NrRunde > 0)
                    { sb.Append("CUTE_NrRunde=@nr,");     parms.Add(("@nr", m_tournStart.NrRunde)); }
                    if (!string.IsNullOrWhiteSpace(m_tournStart.Organizator))
                    { sb.Append("CUTE_Organizator=@org,");    parms.Add(("@org",  m_tournStart.Organizator)); }
                    if (!string.IsNullOrWhiteSpace(m_tournStart.DirectorTurneu))
                    { sb.Append("CUTE_DirectorTurneu=@dir,"); parms.Add(("@dir",  m_tournStart.DirectorTurneu)); }
                    if (!string.IsNullOrWhiteSpace(m_tournStart.ArbSef))
                    { sb.Append("CUTE_ArbSef=@arb,");         parms.Add(("@arb",  m_tournStart.ArbSef)); }
                    if (!string.IsNullOrWhiteSpace(m_tournStart.Cadenta))
                    { sb.Append("CUTE_Cadenta=@cad,");        parms.Add(("@cad",  m_tournStart.Cadenta)); }
                    if (!string.IsNullOrWhiteSpace(m_tournStart.TipSistem))
                    { sb.Append("CUTE_TipSistem=@tsys,");     parms.Add(("@tsys", m_tournStart.TipSistem)); }
                    parms.Add(("@id", m_lCUTE));
                    q.ExecuteNonQueryPThrow($"UPDATE CU_Turnee SET {sb.ToString().TrimEnd(',')} WHERE CUTE_Id=@id",
                        parms.Select(p => CInterogareSQL.P(p.Item1, p.Item2)).ToArray());
                }

                // construim lookup CUJA_Id dupa NrStart (dupa ce inseram jucatorii)
                var cujaByNr = new Dictionary<int, int>();

                // --- pas 1: upsert toti jucatorii in CU_Jucatori ---
                var ratingByNr2 = m_tournStart.Jucatori.Where(p => p.Rating > 0)
                    .ToDictionary(p => p.NrStart, p => p.Rating);

                foreach (var player in m_tournStart.Jucatori)
                {
                    cucuByNr.TryGetValue(player.NrStart, out int cucuId);
                    int cujaId = UpsertJucator(q, player, cucuId);
                    cujaByNr[player.NrStart] = cujaId;

                    if (cucuId > 0)
                    {
                        // automatism: salveaza FIDE ID daca lipsea
                        if (player.FideId > 0)
                            q.ExecuteNonQueryPThrow(
                                "UPDATE CU_Cursanti SET CUCU_FideId=@fide WHERE CUCU_Id=@cucu AND (CUCU_FideId IS NULL OR CUCU_FideId=0)",
                                CInterogareSQL.P("@fide", player.FideId),
                                CInterogareSQL.P("@cucu", cucuId));
                        // actualizeaza rating curent (din Starting List = ratingul la zi)
                        if (player.Rating > 0)
                            q.ExecuteNonQueryPThrow(
                                "UPDATE CU_Cursanti SET CUCU_RatingCurent=@rtg WHERE CUCU_Id=@cucu",
                                CInterogareSQL.P("@rtg",  player.Rating),
                                CInterogareSQL.P("@cucu", cucuId));
                    }

                    // calculeaza rtg+/- per runda (K=40 juniori <18, K=10 >=2400, altfel K=20)
                    if (player.Rating > 0 && player.Runde.Count > 0)
                    {
                        int k = player.Rating >= 2400 ? 10 : 20;
                        if (dataNasteriiByNr.TryGetValue(player.NrStart, out DateTime dn))
                        {
                            int varsta = (data - dn).Days / 365;
                            if (varsta < 18) k = 40;
                        }
                        ChessResultsParser.CalcRtgDeltaPerRunda(player.Runde, ratingByNr2, player.Rating, k);
                    }
                }

                // --- pas 2: upsert in CU_TurneeDetaliu + CU_TurneeJocuri ---
                foreach (var player in m_tournStart.Jucatori)
                {
                    int cujaId = cujaByNr[player.NrStart];

                    int exists = q.ExecuteToIntPThrow(
                        "SELECT COUNT(*) FROM CU_TurneeDetaliu WHERE CUTD_CUTE_Id=@cute AND CUTD_NrStart=@nr",
                        CInterogareSQL.P("@cute", m_lCUTE),
                        CInterogareSQL.P("@nr",   player.NrStart));

                    int cutdId;
                    if (exists > 0)
                    {
                        q.ExecuteNonQueryPThrow(@"UPDATE CU_TurneeDetaliu
                            SET CUTD_CUJA_Id=@cuja, CUTD_RatingLaTurneu=@rtg, CUTD_Puncte=@pct,
                                CUTD_Loc=@loc, CUTD_RatingPerformanta=@perf
                          WHERE CUTD_CUTE_Id=@cute AND CUTD_NrStart=@nr",
                            CInterogareSQL.P("@cuja", cujaId),
                            CInterogareSQL.P("@rtg",  player.Rating   > 0 ? (object)player.Rating   : DBNull.Value),
                            CInterogareSQL.P("@pct",  player.Puncte   > 0 ? (object)player.Puncte   : DBNull.Value),
                            CInterogareSQL.P("@loc",  player.LocFinal > 0 ? (object)player.LocFinal : DBNull.Value),
                            CInterogareSQL.P("@perf", player.RatingPerf > 0 ? (object)player.RatingPerf : DBNull.Value),
                            CInterogareSQL.P("@cute", m_lCUTE),
                            CInterogareSQL.P("@nr",   player.NrStart));
                        cutdId = q.ExecuteToIntPThrow(
                            "SELECT CUTD_Id FROM CU_TurneeDetaliu WHERE CUTD_CUTE_Id=@cute AND CUTD_NrStart=@nr",
                            CInterogareSQL.P("@cute", m_lCUTE), CInterogareSQL.P("@nr", player.NrStart));
                    }
                    else
                    {
                        // INSERT + SCOPE_IDENTITY() in acelasi batch
                        cutdId = q.ExecuteToIntPThrow(
                            @"INSERT INTO CU_TurneeDetaliu
                                (CUTD_CUTE_Id, CUTD_CUJA_Id, CUTD_Tip, CUTD_NrStart,
                                 CUTD_RatingLaTurneu, CUTD_Puncte, CUTD_Loc, CUTD_RatingPerformanta)
                              VALUES (@cute,@cuja,0,@nr,@rtg,@pct,@loc,@perf);
                              SELECT CAST(SCOPE_IDENTITY() AS INT)",
                            CInterogareSQL.P("@cute", m_lCUTE),
                            CInterogareSQL.P("@cuja", cujaId),
                            CInterogareSQL.P("@nr",   player.NrStart),
                            CInterogareSQL.P("@rtg",  player.Rating   > 0 ? (object)player.Rating   : DBNull.Value),
                            CInterogareSQL.P("@pct",  player.Puncte   > 0 ? (object)player.Puncte   : DBNull.Value),
                            CInterogareSQL.P("@loc",  player.LocFinal > 0 ? (object)player.LocFinal : DBNull.Value),
                            CInterogareSQL.P("@perf", player.RatingPerf > 0 ? (object)player.RatingPerf : DBNull.Value));
                    }

                    if (cutdId > 0 && player.Runde.Count > 0)
                    {
                        q.ExecuteNonQueryPThrow("DELETE FROM CU_TurneeJocuri WHERE CUTJ_CUTD_Id=@id",
                            CInterogareSQL.P("@id", cutdId));

                        foreach (var runda in player.Runde)
                        {
                            int cujaAdv = runda.NrStartAdv > 0 && cujaByNr.TryGetValue(runda.NrStartAdv, out int cja) ? cja : 0;
                            int rtgAdv  = runda.NrStartAdv > 0 ? ratingByNr.GetValueOrDefault(runda.NrStartAdv, 0) : 0;

                            q.ExecuteNonQueryPThrow(
                                @"INSERT INTO CU_TurneeJocuri
                                    (CUTJ_CUTD_Id,CUTJ_Runda,CUTJ_NrStartAdv,CUTJ_CUJA_Id_Adv,
                                     CUTJ_RatingAdv,CUTJ_Culoare,CUTJ_Rezultat,CUTJ_WeExpected,CUTJ_RtgDelta)
                                  VALUES (@id,@rd,@nradv,@cujaadv,@rtgadv,@col,@rez,@we,@delta)",
                                CInterogareSQL.P("@id",      cutdId),
                                CInterogareSQL.P("@rd",      (byte)runda.Runda),
                                CInterogareSQL.P("@nradv",   runda.NrStartAdv > 0 ? (object)runda.NrStartAdv : DBNull.Value),
                                CInterogareSQL.P("@cujaadv", cujaAdv > 0 ? (object)cujaAdv : DBNull.Value),
                                CInterogareSQL.P("@rtgadv",  rtgAdv  > 0 ? (object)rtgAdv  : DBNull.Value),
                                CInterogareSQL.P("@col",     (byte)runda.Culoare),
                                CInterogareSQL.P("@rez",     runda.Rezultat),
                                CInterogareSQL.P("@we",      runda.WeExpected.HasValue ? (object)runda.WeExpected.Value : DBNull.Value),
                                CInterogareSQL.P("@delta",   runda.RtgDelta.HasValue   ? (object)runda.RtgDelta.Value   : DBNull.Value));
                        }
                    }
                    importati++;
                }

                q.CommitTransaction();
                MessageBox.Show(
                    $"Import reușit!\n{importati} jucători importați ({cucuByNr.Count} cursanți ONIXTEIA asociați).",
                    "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                q.RollbackTransaction();
                ArataMesajEroare("Eroare la import", ex.Message);
            }
            finally { q.Close(); }
        }
    }
}

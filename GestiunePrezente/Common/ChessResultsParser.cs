using ExcelDataReader;
using System.Data;
using System.Text;

namespace GestiunePrezente.Common
{
    public class ChessResultsPlayer
    {
        public int    NrStart    { get; set; }
        public string Titlu      { get; set; } = "";
        public string Nume       { get; set; } = "";   // "Popescu, Ion" → stocat ca atare
        public string NumeFull   { get; set; } = "";   // "Popescu Ion" (fara virgula)
        public string Prenume    { get; set; } = "";
        public string NumeFamilie{ get; set; } = "";
        public int    FideId     { get; set; }
        public string Fed        { get; set; } = "";
        public int    Rating     { get; set; }
        public string Club       { get; set; } = "";

        // completat din crosstable
        public int     LocFinal    { get; set; }
        public decimal Puncte      { get; set; }
        public int     RatingPerf  { get; set; }
        public List<ChessResultsRound> Runde { get; set; } = new();
    }

    public class ChessResultsRound
    {
        public int      Runda      { get; set; }
        public int      NrStartAdv { get; set; }  // 0 = bye
        public int      Culoare    { get; set; }  // 1=alb 2=negru 0=bye
        public decimal  Rezultat   { get; set; }  // 1.0 / 0.5 / 0.0
        public bool     IsBye      { get; set; }
        public decimal? WeExpected { get; set; }  // scor asteptat FIDE
        public decimal? RtgDelta   { get; set; }  // rtg+/- pentru aceasta runda
    }

    public class ChessResultsTournament
    {
        public string Denumire      { get; set; } = "";
        public DateTime? DataInceput { get; set; }
        public DateTime? DataSfarsit { get; set; }
        public int    NrRunde       { get; set; }
        public string Url           { get; set; } = "";
        public string Organizator   { get; set; } = "";
        public string DirectorTurneu{ get; set; } = "";
        public string ArbSef        { get; set; } = "";
        public string Cadenta       { get; set; } = "";
        public string TipSistem     { get; set; } = "";
        public string LocatiePlin   { get; set; } = "";
        public List<ChessResultsPlayer> Jucatori { get; set; } = new();
    }

    public static class ChessResultsParser
    {
        static ChessResultsParser()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public static ChessResultsTournament ParseStartingList(string filePath)
        {
            var ds    = ReadExcel(filePath);
            var table = ds.Tables[0];
            var t     = new ChessResultsTournament();

            // linia 1 (index 0) = "From the Tournament-Database..."
            // linia 1 (index 1) = denumire turneu
            // cautam header-ul "No." sa stim de unde incep datele
            int headerRow = -1;
            int colNo = -1, colName = -1, colFideId = -1, colFed = -1, colRtg = -1, colClub = -1;

            for (int r = 0; r < Math.Min(table.Rows.Count, 20); r++)
            {
                string? cell = table.Rows[r][0]?.ToString()?.Trim();
                if (cell is "No." or "Nr.")
                {
                    headerRow = r;
                    // identifica coloanele dupa header (suport EN + RO)
                    for (int c = 0; c < table.Columns.Count; c++)
                    {
                        string h = table.Rows[r][c]?.ToString()?.Trim() ?? "";
                        if (h is "No." or "Nr.")               colNo     = c;
                        else if (h is "Name" or "Numele")      colName   = c;
                        else if (h is "FideID" or "Fide ID")   colFideId = c;
                        else if (h == "FED")                   colFed    = c;
                        else if (h is "Rtg" or "RtgI" or "ELO") colRtg  = c;
                        else if (h is "Club/City" or "Club/Oras") colClub = c;
                    }
                    break;
                }
                // extrage denumire din linia 1
                if (r == 1 && !string.IsNullOrWhiteSpace(cell))
                    t.Denumire = cell;
            }

            if (headerRow < 0 || colNo < 0 || colName < 0) return t;

            for (int r = headerRow + 1; r < table.Rows.Count; r++)
            {
                string? sNo = table.Rows[r][colNo]?.ToString()?.Trim();
                if (string.IsNullOrWhiteSpace(sNo) || !int.TryParse(sNo, out int nr)) break;

                var p = new ChessResultsPlayer { NrStart = nr };

                // coloana Name poate fi precedata de titlu (GM, IM, etc.) intr-o coloana separata
                // sau titlul si numele pot fi in aceeasi celula
                string rawName = table.Rows[r][colName]?.ToString()?.Trim() ?? "";

                // daca titlul e in coloana separata (intre No si Name)
                if (colName > colNo + 1)
                    p.Titlu = table.Rows[r][colNo + 1]?.ToString()?.Trim() ?? "";

                p.Nume = rawName;
                ParseNume(rawName, out string numeFam, out string prenume);
                p.NumeFamilie = numeFam;
                p.Prenume     = prenume;
                p.NumeFull    = $"{numeFam} {prenume}".Trim();

                if (colFideId >= 0 && int.TryParse(table.Rows[r][colFideId]?.ToString(), out int fid))
                    p.FideId = fid;
                if (colFed  >= 0) p.Fed    = table.Rows[r][colFed ]?.ToString()?.Trim() ?? "";
                if (colRtg  >= 0 && int.TryParse(table.Rows[r][colRtg]?.ToString(), out int rtg))
                    p.Rating = rtg;
                if (colClub >= 0) p.Club   = table.Rows[r][colClub]?.ToString()?.Trim() ?? "";

                t.Jucatori.Add(p);
            }

            return t;
        }

        public static void MergeCrosstable(ChessResultsTournament t, string filePath)
        {
            var ds    = ReadExcel(filePath);
            var table = ds.Tables[0];

            // gasim header-ul "Rk."
            int headerRow = -1;
            int colRk = -1, colName = -1, colFed = -1, colPts = -1;
            int firstRoundCol = -1;
            int nrRunde = 0;

            for (int r = 0; r < Math.Min(table.Rows.Count, 25); r++)
            {
                string? cell = table.Rows[r][0]?.ToString()?.Trim();

                // extrage data turneului din header daca e disponibila (EN: "Date", RO: "Data")
                if (cell != null && (cell.StartsWith("Date") || cell.StartsWith("Data")))
                {
                    string dateLine = string.Join(" ", Enumerable.Range(0, table.Columns.Count)
                        .Select(c => table.Rows[r][c]?.ToString() ?? "")).Trim();
                    ParseDateRange(dateLine, out DateTime? d1, out DateTime? d2);
                    if (d1.HasValue) t.DataInceput = d1;
                    if (d2.HasValue) t.DataSfarsit = d2;
                }
                if (cell != null && cell.StartsWith("Number of rounds"))
                {
                    string val = string.Join(" ", Enumerable.Range(0, table.Columns.Count)
                        .Select(c => table.Rows[r][c]?.ToString() ?? ""));
                    var m = System.Text.RegularExpressions.Regex.Match(val, @"\d+");
                    if (m.Success) t.NrRunde = nrRunde = int.Parse(m.Value);
                }

                // extrage câmpuri antet (cheie în col 0, valoare continuă în celelalte coloane)
                if (cell != null && cell.Length > 2 && !char.IsDigit(cell[0]))
                {
                    string rowFull = string.Join(" ", Enumerable.Range(0, table.Columns.Count)
                        .Select(c => table.Rows[r][c]?.ToString()?.Trim() ?? ""))
                        .Replace("  ", " ").Trim();
                    string val = ExtractAfterColon(rowFull);
                    if (cell.StartsWith("Organizator") || cell.StartsWith("Organizer"))
                        t.Organizator = val;
                    else if (cell.StartsWith("Director") || cell.StartsWith("Chief organiz"))
                        t.DirectorTurneu = val;
                    else if (cell.StartsWith("Arbitru sef") || cell.StartsWith("Chief Arbiter") || cell.StartsWith("Chief arbiter"))
                        t.ArbSef = val;
                    else if (cell.StartsWith("Bedenkzeit") || cell.StartsWith("Time control"))
                        t.Cadenta = val;
                    else if (cell.StartsWith("Tournament type") || cell.StartsWith("Tip turneu") || cell.StartsWith("Tip de turneu"))
                        t.TipSistem = val;
                    else if (cell.StartsWith("Locatie") || cell.StartsWith("Location") || cell.StartsWith("Localitate"))
                        t.LocatiePlin = val;
                }

                if (cell is "Rk." or "Loc")
                {
                    headerRow = r;
                    for (int c = 0; c < table.Columns.Count; c++)
                    {
                        string h = table.Rows[r][c]?.ToString()?.Trim() ?? "";
                        if (h is "Rk." or "Loc")               colRk   = c;
                        else if (h is "Name" or "Numele")       colName = c;
                        else if (h == "FED")                    colFed  = c;
                        else if (h is "Pts." or "Pts" or "Pct.") colPts = c;
                        else if (System.Text.RegularExpressions.Regex.IsMatch(h, @"^\d+\.Rd$") && firstRoundCol < 0)
                        {
                            firstRoundCol = c;
                            // numara rundele
                            int nr2 = 0;
                            for (int c2 = c; c2 < table.Columns.Count; c2++)
                            {
                                string h2 = table.Rows[r][c2]?.ToString()?.Trim() ?? "";
                                if (System.Text.RegularExpressions.Regex.IsMatch(h2, @"^\d+\.Rd$")) nr2++;
                                else if (h2 is "Pts." or "Pts" or "Pct.") break;
                            }
                            if (nrRunde == 0) nrRunde = nr2;
                        }
                    }
                    break;
                }
            }

            if (headerRow < 0) return;

            // construim lookup dupa numar start (din starting list)
            var byNr = t.Jucatori.ToDictionary(p => p.NrStart);

            for (int r = headerRow + 1; r < table.Rows.Count; r++)
            {
                string? sRk = table.Rows[r][colRk >= 0 ? colRk : 0]?.ToString()?.Trim();
                if (string.IsNullOrWhiteSpace(sRk) || !int.TryParse(sRk, out int locFinal)) break;

                string rawName = colName >= 0 ? (table.Rows[r][colName]?.ToString()?.Trim() ?? "") : "";
                ParseNume(rawName, out string numeFam, out string prenume);

                // matching: cauta jucatorul in starting list dupa nume
                ChessResultsPlayer? player = FindPlayer(byNr, numeFam, prenume);
                if (player == null) continue;

                player.LocFinal = locFinal;
                if (colPts >= 0)
                {
                    string sPts = table.Rows[r][colPts]?.ToString()?.Replace(",", ".") ?? "0";
                    if (decimal.TryParse(sPts, System.Globalization.NumberStyles.Any,
                            System.Globalization.CultureInfo.InvariantCulture, out decimal pts))
                        player.Puncte = pts;
                }

                // runde
                if (firstRoundCol >= 0)
                {
                    for (int rd = 0; rd < nrRunde; rd++)
                    {
                        int col = firstRoundCol + rd;
                        if (col >= table.Columns.Count) break;
                        string cell = table.Rows[r][col]?.ToString()?.Trim() ?? "";
                        var round = ParseRound(rd + 1, cell);
                        player.Runde.Add(round);
                    }
                }
            }

            // chess-results XLS stocheaza in celulele de runde LOCUL FINAL (Rk.), nu NrStart (SNo).
            // Traducem NrStartAdv din rank-based in SNo-based.
            var rankToNrStart = new Dictionary<int, int>();
            foreach (var p in t.Jucatori)
                if (p.LocFinal > 0)
                    rankToNrStart[p.LocFinal] = p.NrStart;

            foreach (var player in t.Jucatori)
                foreach (var round in player.Runde)
                    if (!round.IsBye && round.NrStartAdv > 0 &&
                        rankToNrStart.TryGetValue(round.NrStartAdv, out int sno))
                        round.NrStartAdv = sno;

        }

        // Calculeaza we (expected score) si rtgDelta per runda, stocate direct pe obiectul ChessResultsRound.
        // ratingJucator = ratingul jucatorului la inceputul turneului (din Starting List)
        // K: 40 juniori sub 18 ani, 20 adulti pana la 2400, 10 peste 2400
        public static void CalcRtgDeltaPerRunda(List<ChessResultsRound> runde, Dictionary<int, int> ratingByNrStart, int ratingJucator, int kFactor = 20)
        {
            foreach (var r in runde)
            {
                if (r.IsBye || r.NrStartAdv == 0 || !ratingByNrStart.ContainsKey(r.NrStartAdv))
                {
                    r.WeExpected = null;
                    r.RtgDelta   = null;
                    continue;
                }
                double rtgAdv = ratingByNrStart[r.NrStartAdv];
                double we     = 1.0 / (1.0 + Math.Pow(10.0, (rtgAdv - ratingJucator) / 400.0));
                double delta  = kFactor * ((double)r.Rezultat - we);
                r.WeExpected  = (decimal)Math.Round(we,    4);
                r.RtgDelta    = (decimal)Math.Round(delta, 1);
            }
        }

        // Calculeaza performance rating FIDE din lista de runde + ratinguri adversari
        public static int CalcPerformance(List<ChessResultsRound> runde, Dictionary<int, int> ratingByNrStart)
        {
            var valid = runde.Where(r => !r.IsBye && ratingByNrStart.ContainsKey(r.NrStartAdv)).ToList();
            if (valid.Count == 0) return 0;
            double avgRtg = valid.Average(r => (double)ratingByNrStart[r.NrStartAdv]);
            double score  = (double)valid.Sum(r => r.Rezultat);
            double pct    = score / valid.Count;
            // formula FIDE simplificata
            double dp = pct switch {
                >= 1.0  =>  800,
                >= 0.99 =>  677,
                >= 0.98 =>  589,
                >= 0.95 =>  470,
                >= 0.90 =>  366,
                >= 0.80 =>  240,
                >= 0.70 =>  149,
                >= 0.60 =>   72,
                >= 0.50 =>    0,
                >= 0.40 =>  -72,
                >= 0.30 => -149,
                >= 0.20 => -240,
                >= 0.10 => -366,
                >= 0.05 => -470,
                >= 0.02 => -589,
                >= 0.01 => -677,
                _       => -800
            };
            return (int)Math.Round(avgRtg + dp);
        }

        // ---- helpers ----

        private static string ExtractAfterColon(string line)
        {
            int idx = line.IndexOf(':');
            return idx >= 0 ? line[(idx + 1)..].Trim() : "";
        }

        private static DataSet ReadExcel(string path)
        {
            using var stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            IExcelDataReader reader = Path.GetExtension(path).ToLowerInvariant() == ".xlsx"
                ? ExcelReaderFactory.CreateOpenXmlReader(stream)
                : ExcelReaderFactory.CreateReader(stream);  // auto-detect XLS/HTML-as-XLS
            return reader.AsDataSet(new ExcelDataSetConfiguration
            {
                ConfigureDataTable = _ => new ExcelDataTableConfiguration { UseHeaderRow = false }
            });
        }

        private static void ParseNume(string raw, out string numeFamilie, out string prenume)
        {
            // format "Popescu, Ion" sau "Popescu Ion"
            int comma = raw.IndexOf(',');
            if (comma >= 0)
            {
                numeFamilie = raw[..comma].Trim();
                prenume     = raw[(comma + 1)..].Trim();
            }
            else
            {
                int sp = raw.IndexOf(' ');
                if (sp >= 0)
                {
                    numeFamilie = raw[..sp].Trim();
                    prenume     = raw[(sp + 1)..].Trim();
                }
                else { numeFamilie = raw; prenume = ""; }
            }
        }

        private static ChessResultsPlayer? FindPlayer(Dictionary<int, ChessResultsPlayer> byNr,
            string numeFam, string prenume)
        {
            // incercare exacta (format EN cu virgula: "Popescu" + "Ion")
            var match = byNr.Values.FirstOrDefault(p =>
                string.Equals(p.NumeFamilie, numeFam, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(p.Prenume,     prenume, StringComparison.OrdinalIgnoreCase));
            if (match != null) return match;

            // fallback: format RO fara virgula ("Ohanyan Emin" → numeFam="Ohanyan", prenume="Emin")
            // compara cu NumeFull = "NumeFamilie Prenume" (ex. "Perea Fruet Lorenzo")
            string numeFull = $"{numeFam} {prenume}".Trim();
            return byNr.Values.FirstOrDefault(p =>
                string.Equals(p.NumeFull, numeFull, StringComparison.OrdinalIgnoreCase));
        }

        private static ChessResultsRound ParseRound(int roundNr, string cell)
        {
            var r = new ChessResultsRound { Runda = roundNr };
            if (string.IsNullOrWhiteSpace(cell) || cell == "-")
            { r.IsBye = true; return r; }

            // format: "61b1", "13w1", "37w½", "-½", "-1"
            var m = System.Text.RegularExpressions.Regex.Match(cell,
                @"^(-?)(\d*)([bwBW]?)(1|0|½|[0-9]+/[0-9]+)?$");

            bool isBye = cell.StartsWith("-") || !m.Success;
            if (isBye)
            {
                r.IsBye = true;
                // "-½" = bye cu jumatate de punct, "-1" = bye cu punct intreg
                r.Rezultat = cell.Contains("½") ? 0.5m : cell.Contains("1") ? 1.0m : 0.0m;
                return r;
            }

            if (int.TryParse(m.Groups[2].Value, out int advNr)) r.NrStartAdv = advNr;
            string culoare = m.Groups[3].Value.ToLower();
            r.Culoare = culoare == "b" ? 2 : culoare == "w" ? 1 : 0;
            string rez = m.Groups[4].Value;
            r.Rezultat = rez == "1" ? 1.0m : rez == "½" ? 0.5m : 0.0m;
            return r;
        }

        private static void ParseDateRange(string line, out DateTime? d1, out DateTime? d2)
        {
            d1 = d2 = null;
            var m = System.Text.RegularExpressions.Regex.Match(line,
                @"(\d{4}/\d{2}/\d{2})\s+(?:to|La)\s+(\d{4}/\d{2}/\d{2})");
            if (m.Success)
            {
                if (DateTime.TryParseExact(m.Groups[1].Value, "yyyy/MM/dd",
                        System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.None, out var dt1)) d1 = dt1;
                if (DateTime.TryParseExact(m.Groups[2].Value, "yyyy/MM/dd",
                        System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.None, out var dt2)) d2 = dt2;
            }
        }
    }
}

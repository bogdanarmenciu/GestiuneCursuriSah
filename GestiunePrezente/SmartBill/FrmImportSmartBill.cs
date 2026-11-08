using GestiunePrezente.Common;
using GestiunePrezente.MeniuPrincipal;
using Microsoft.Data.SqlClient;
using static GestiunePrezente.SmartBill.CSmartBillService;

namespace GestiunePrezente.SmartBill
{
    public partial class FrmImportSmartBill : Form
    {
        private readonly CManagerDeProiect m_manager;
        private List<FacturaImportata> m_facturi = [];
        private int m_nrPadding = 4; // detected from DB; most SmartBill accounts use 4-digit numbers

        public const string MBME_NUME = "Import SmartBill";
        public static bool PoateDeschide(CManagerDeProiect mm)
            => DrepturiFormular.PoateDeschide(mm, MBME_NUME);

        public FrmImportSmartBill(FrmMeniuPrincipal formParent, CManagerDeProiect mm)
            : this(formParent, mm, DateTime.Today) { }

        public FrmImportSmartBill(FrmMeniuPrincipal formParent, CManagerDeProiect mm, DateTime luna)
        {
            m_manager = mm;
            InitializeComponent();
            m_dtpLuna.Value = new DateTime(luna.Year, luna.Month, 1);
            Load += (_, _) =>
            {
                IncarcaContracteLuna();
                DetecteazaNumarStart();
                // Remind user: invoice date in grid comes from this picker, not from SmartBill API
                var tip = new ToolTip();
                tip.SetToolTip(m_dtpLuna, "Data facturilor importate din API = prima zi a lunii selectate.\nSelectați luna/anul corect înainte de descărcare.");
                tip.SetToolTip(m_lblStartNr, "Numărul primei facturi de descărcat (ex: 525 pentru ONX0525).");
            };
        }

        // ── Descărcare din API (scanare secvențială SBORO) ───────────────
        private async void m_btnDescarcaApi_Click(object sender, EventArgs e)
        {
            var svc = CSmartBillService.FromDb(m_manager);
            if (svc == null)
            {
                CEroare.Arata("Configurare lipsă",
                    "Email, API Key sau Seria SmartBill nu sunt completate în Setări → Societate.");
                return;
            }

            int numarStart = (int)m_nudStartNr.Value;
            var luna       = m_dtpLuna.Value;
            var startLuna  = new DateTime(luna.Year, luna.Month, 1);
            var endLuna    = startLuna.AddMonths(1).AddDays(-1);

            m_btnDescarcaApi.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                var progress = new Progress<string>(msg => m_lblStatus.Text = msg);
                var result   = await svc.SincronizeazaSecventialAsync(
                    numarStart, m_nrPadding, startLuna, progress);

                if (!result.Success)
                {
                    CEroare.Arata("Eroare SmartBill", result.ErrorText ?? "Eroare necunoscută");
                    m_lblStatus.Text = "Scanare eșuată.";
                    return;
                }

                m_facturi = result.Facturi ?? [];
                AfiseazaFacturi();

                // Advance start number to after last downloaded invoice
                if (result.Facturi?.Count > 0)
                {
                    var ultimul = result.Facturi[^1];
                    if (int.TryParse(ultimul.Numar, out int lastNr))
                        m_nudStartNr.Value = Math.Min(lastNr + 1, m_nudStartNr.Maximum);
                }

                m_lblStatus.Text = $"{m_facturi.Count} facturi descărcate.";
            }
            finally
            {
                m_btnDescarcaApi.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
        }

        private void DetecteazaNumarStart()
        {
            try
            {
                // Find latest invoice number and detect zero-padding from existing records
                using var cmd = new SqlCommand(
                    @"SELECT TOP 1 FVFA_NrFactura
                      FROM FV_Facturi
                      WHERE FVFA_NrFactura IS NOT NULL AND FVFA_NrFactura <> ''
                      ORDER BY FVFA_Id DESC",
                    m_manager.GetConnection());
                var val = cmd.ExecuteScalar();
                if (val == null || val == DBNull.Value) return;

                string nrFact = val.ToString()!.Trim();
                // Extract the numeric part (everything after the first digit group starts)
                int firstDigit = -1;
                for (int i = 0; i < nrFact.Length; i++)
                    if (char.IsDigit(nrFact[i])) { firstDigit = i; break; }
                if (firstDigit < 0) return;

                string numericPart = nrFact[firstDigit..];
                if (!int.TryParse(numericPart, out int lastNr)) return;

                m_nudStartNr.Value = Math.Min(lastNr + 1, m_nudStartNr.Maximum);
            }
            catch { /* leave defaults */ }
        }

        // ── Import Excel ──────────────────────────────────────────────────
        private void m_btnImportXls_Click(object sender, EventArgs e)
        {
            using var dlg = new OpenFileDialog
            {
                Title  = "Selectați fișierul Excel exportat din SmartBill",
                Filter = "Excel (*.xlsx;*.xls)|*.xlsx;*.xls|Toate fișierele|*.*",
            };
            if (dlg.ShowDialog() != DialogResult.OK) return;

            var result = CSmartBillService.ImportaFacturiDinExcel(dlg.FileName);
            if (!result.Success)
            {
                CEroare.Arata("Eroare import Excel", result.ErrorText ?? "Eroare necunoscută");
                return;
            }

            var luna = m_dtpLuna.Value;
            m_facturi = (result.Facturi ?? [])
                .Where(f => f.Data.Year == luna.Year && f.Data.Month == luna.Month)
                .ToList();

            AfiseazaFacturi();
            m_lblStatus.Text = $"{m_facturi.Count} facturi importate pentru {luna:MM/yyyy}.";
        }

        private void AfiseazaFacturi()
        {
            m_gridFacturi.Rows.Clear();

            // Load invoice numbers already saved in FV_Facturi for this month
            var luna = new DateTime(m_dtpLuna.Value.Year, m_dtpLuna.Value.Month, 1);
            var salvate = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            try
            {
                using var cmd = new SqlCommand(
                    @"SELECT ISNULL(FVFA_NrFactura,'') FROM FV_Facturi
                      WHERE DATEFROMPARTS(YEAR(FVFA_PerioadaInceput),MONTH(FVFA_PerioadaInceput),1) = @luna",
                    m_manager.GetConnection());
                cmd.Parameters.AddWithValue("@luna", luna);
                using var rd = cmd.ExecuteReader();
                while (rd.Read()) salvate.Add(rd.GetString(0));
            }
            catch { }

            int noi = 0, deja = 0;
            foreach (var f in m_facturi)
            {
                string nrFact = $"{f.Serie}{f.Numar}".Trim();
                bool esteAsociata = salvate.Contains(nrFact);

                int row = m_gridFacturi.Rows.Add(
                    f.Serie + " " + f.Numar,
                    f.Data.ToString("dd.MM.yyyy"),
                    f.NumeClient,
                    f.Suma.ToString("N2"),
                    esteAsociata ? $"✓ asociată | {f.Descriere}" : f.Descriere);

                if (esteAsociata)
                {
                    m_gridFacturi.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(220, 252, 231);
                    m_gridFacturi.Rows[row].DefaultCellStyle.ForeColor = Color.FromArgb(22, 101, 52);
                    // Don't set Tag — prevents re-associating an already linked invoice
                    deja++;
                }
                else
                {
                    m_gridFacturi.Rows[row].Tag = f;
                    noi++;
                }
            }

            m_lblStatus.Text = $"{m_facturi.Count} facturi: {noi} neasisociate, {deja} deja asociate (verde).";
        }

        // ── Contracte fără factură ────────────────────────────────────────
        private void IncarcaContracteLuna()
        {
            m_gridContracte.Rows.Clear();
            var luna = new DateTime(m_dtpLuna.Value.Year, m_dtpLuna.Value.Month, 1);
            int div  = m_manager.GetDiviziune();

            try
            {
                using var cmd = new SqlCommand(@"
SELECT C.CUCO_Id,
       ISNULL(CONVERT(NVARCHAR(50), C.CUCO_NrContract), '') AS NrContract,
       STUFF((SELECT ', ' + CU2.CUCU_Nume + ' ' + CU2.CUCU_Prenume
              FROM CU_ContracteCursanti CC2
              INNER JOIN CU_Cursanti CU2 ON CC2.CUCC_CUCU_Id = CU2.CUCU_Id
              WHERE CC2.CUCC_CUCO_Id = C.CUCO_Id AND ISNULL(CC2.CUCC_Retras,0) = 0
              FOR XML PATH(''), TYPE).value('.','NVARCHAR(MAX)'), 1, 2, '') AS Cursanti,
       ISNULL(MAX(CUAC.CUAC_Pret), 0) AS Pret
FROM CU_Contracte C
INNER JOIN CU_ContracteCursanti CC ON CC.CUCC_CUCO_Id = C.CUCO_Id
    AND ISNULL(CC.CUCC_Retras, 0) = 0
INNER JOIN CU_Cursanti CU ON CC.CUCC_CUCU_Id = CU.CUCU_Id
    AND CU.CUCU_MBDV_Id = @div
LEFT  JOIN CU_AbonamenteCursant CUAC ON CUAC.CUAC_CUCO_Id = C.CUCO_Id
WHERE C.CUCO_Retras = 0
  AND NOT EXISTS (
      SELECT 1 FROM FV_Facturi F
      WHERE F.FVFA_CUCO_Id = C.CUCO_Id
        AND DATEFROMPARTS(YEAR(F.FVFA_PerioadaInceput), MONTH(F.FVFA_PerioadaInceput), 1) = @luna)
GROUP BY C.CUCO_Id, C.CUCO_NrContract
ORDER BY MIN(CU.CUCU_Nume)", m_manager.GetConnection());

                cmd.Parameters.AddWithValue("@div",  div);
                cmd.Parameters.AddWithValue("@luna", luna);

                using var rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    int cucoId     = rd.GetInt32(0);
                    string nrContr = rd.GetString(1);
                    string cursant = rd.GetString(2);
                    decimal pret   = rd.GetDecimal(3);

                    int row = m_gridContracte.Rows.Add(
                        nrContr,
                        cursant,
                        pret.ToString("N2"),
                        cucoId.ToString());
                    _ = row;
                }
            }
            catch (Exception ex)
            {
                m_lblStatus.Text = "Eroare la încărcarea contractelor: " + ex.Message;
            }
        }

        private void m_dtpLuna_ValueChanged(object sender, EventArgs e)
        {
            IncarcaContracteLuna();
            // Re-filter invoices if already imported
            if (m_facturi.Count > 0)
            {
                var luna = m_dtpLuna.Value;
                m_facturi = m_facturi
                    .Where(f => f.Data.Year == luna.Year && f.Data.Month == luna.Month)
                    .ToList();
                AfiseazaFacturi();
            }
        }

        // ── Asociere ──────────────────────────────────────────────────────
        private void m_btnAsociaza_Click(object sender, EventArgs e)
        {
            if (m_gridFacturi.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selectați o factură din lista de sus.", "Nimic selectat",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (m_gridContracte.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selectați un contract din lista de jos.", "Nimic selectat",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var invRow  = m_gridFacturi.SelectedRows[0];
            var ctrRow  = m_gridContracte.SelectedRows[0];

            if (invRow.Tag is not FacturaImportata f) return;

            if (!int.TryParse(ctrRow.Cells["col_c_cucoId"].Value?.ToString(), out int cucoId)) return;
            string nrContract = ctrRow.Cells["col_c_nr"].Value?.ToString() ?? "";
            string cursant    = ctrRow.Cells["col_c_cursant"].Value?.ToString() ?? "";

            var confirm = MessageBox.Show(
                $"Asociezi factura {f.Serie}{f.Numar} ({f.NumeClient}, {f.Suma:N2} RON)\n" +
                $"cu contractul nr. {nrContract} — {cursant}?",
                "Confirmare asociere", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                // Period = selected month in picker, NOT invoice emission date
                // (invoice may be emitted in a different month than the service period)
                var luna   = new DateTime(m_dtpLuna.Value.Year, m_dtpLuna.Value.Month, 1);
                var lunaSf = luna.AddMonths(1).AddDays(-1);
                string nrFact = $"{f.Serie}{f.Numar}".Trim();

                // Double-check: no invoice already for this contract+month
                using var cmdChk = new SqlCommand(
                    @"SELECT COUNT(*) FROM FV_Facturi
                      WHERE FVFA_CUCO_Id = @cuco
                        AND DATEFROMPARTS(YEAR(FVFA_PerioadaInceput), MONTH(FVFA_PerioadaInceput), 1) = @luna",
                    m_manager.GetConnection());
                cmdChk.Parameters.AddWithValue("@cuco", cucoId);
                cmdChk.Parameters.AddWithValue("@luna", luna);
                if ((int)(cmdChk.ExecuteScalar() ?? 0) > 0)
                {
                    MessageBox.Show("Contractul are deja o factură pentru această lună.",
                        "Factură existentă", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using var cmdIns = new SqlCommand(
                    @"INSERT INTO FV_Facturi
                        (FVFA_CUCO_Id, FVFA_NrFactura, FVFA_DataEmitere,
                         FVFA_PerioadaInceput, FVFA_PerioadaSfarsit, FVFA_Suma,
                         FVFA_MBUT_Id, FVFA_DataSalvarii)
                      VALUES (@cuco,@nrf,@de,@pi,@ps,@suma,@u,GETDATE())",
                    m_manager.GetConnection());
                cmdIns.Parameters.AddWithValue("@cuco", cucoId);
                cmdIns.Parameters.AddWithValue("@nrf",  nrFact);
                cmdIns.Parameters.AddWithValue("@de",   f.Data);
                cmdIns.Parameters.AddWithValue("@pi",   luna);
                cmdIns.Parameters.AddWithValue("@ps",   lunaSf);
                cmdIns.Parameters.AddWithValue("@suma", f.Suma);
                cmdIns.Parameters.AddWithValue("@u",    m_manager.GetUserId());
                cmdIns.ExecuteNonQuery();

                // Mark invoice row green and remove contract from bottom grid
                invRow.DefaultCellStyle.BackColor = Color.FromArgb(220, 252, 231);
                invRow.DefaultCellStyle.ForeColor = Color.FromArgb(22, 101, 52);
                invRow.Cells["col_status_inv"].Value =
                    (invRow.Cells["col_status_inv"].Value?.ToString() ?? "") +
                    $" ✓ → contract {nrContract}";
                invRow.Tag = null; // prevent re-use

                m_gridContracte.Rows.Remove(ctrRow);
                m_lblStatus.Text = $"Factură {nrFact} asociată cu contractul {nrContract}.";
            }
            catch (Exception ex)
            {
                CEroare.Arata("Eroare la salvare", ex.Message);
            }
        }

        // ── Auto-asociere (nume → sumă fallback) ─────────────────────────
        private void m_btnAutoAsociaza_Click(object sender, EventArgs e)
        {
            // Build maps for both strategies
            var contractePeNume = new Dictionary<string, List<DataGridViewRow>>(StringComparer.OrdinalIgnoreCase);
            var contractePeSuma = new Dictionary<decimal, List<DataGridViewRow>>();

            foreach (DataGridViewRow row in m_gridContracte.Rows)
            {
                // Name map: each cursant name → row
                string cursanti = row.Cells["col_c_cursant"].Value?.ToString() ?? "";
                foreach (string parte in cursanti.Split(','))
                {
                    string cheie = NormalizeazaNume(parte);
                    if (string.IsNullOrEmpty(cheie)) continue;
                    if (!contractePeNume.ContainsKey(cheie))
                        contractePeNume[cheie] = new List<DataGridViewRow>();
                    if (!contractePeNume[cheie].Contains(row))
                        contractePeNume[cheie].Add(row);
                }

                // Amount map
                if (decimal.TryParse(row.Cells["col_c_pret"].Value?.ToString(),
                        System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture,
                        out decimal pret))
                {
                    if (!contractePeSuma.ContainsKey(pret))
                        contractePeSuma[pret] = new List<DataGridViewRow>();
                    contractePeSuma[pret].Add(row);
                }
            }

            var matches  = new List<(DataGridViewRow invRow, DataGridViewRow ctrRow, string nrFact, string nrContr, string cursant, decimal suma, string strategie)>();
            var ambigue  = new List<string>();
            var niciuna  = new List<string>();
            var asociateDejaInMatch = new HashSet<DataGridViewRow>();

            foreach (DataGridViewRow invRow in m_gridFacturi.Rows)
            {
                if (invRow.Tag is not FacturaImportata f) continue;
                string nrFact = $"{f.Serie}{f.Numar}".Trim();

                // Strategy 1: match by client name (available after Excel import)
                DataGridViewRow? gasit = null;
                string strategie = "";

                if (!string.IsNullOrWhiteSpace(f.NumeClient))
                {
                    string numeNorm = NormalizeazaNume(f.NumeClient);
                    // Also try reversed (SmartBill: "Prenume Nume" vs DB: "Nume Prenume")
                    string numeRev  = NormalizeazaNumeRevers(f.NumeClient);

                    var candidatiNume = new List<DataGridViewRow>();
                    if (contractePeNume.TryGetValue(numeNorm, out var c1)) candidatiNume.AddRange(c1);
                    if (contractePeNume.TryGetValue(numeRev, out var c2))
                        foreach (var r in c2) if (!candidatiNume.Contains(r)) candidatiNume.Add(r);

                    // Remove already claimed rows
                    candidatiNume = candidatiNume.Where(r => !asociateDejaInMatch.Contains(r)).ToList();

                    if (candidatiNume.Count == 1) { gasit = candidatiNume[0]; strategie = "nume"; }
                    else if (candidatiNume.Count > 1) { ambigue.Add($"{nrFact} — {candidatiNume.Count} contracte cu numele \"{f.NumeClient}\""); continue; }
                }

                // Strategy 2: match by unique amount (fallback)
                if (gasit == null)
                {
                    if (contractePeSuma.TryGetValue(f.Suma, out var candidatiSuma))
                    {
                        var liberi = candidatiSuma.Where(r => !asociateDejaInMatch.Contains(r)).ToList();
                        if (liberi.Count == 1) { gasit = liberi[0]; strategie = "sumă"; }
                        else if (liberi.Count > 1) { ambigue.Add($"{nrFact} ({f.Suma:N2} RON) — {liberi.Count} contracte cu aceeași sumă"); continue; }
                    }
                }

                if (gasit == null) { niciuna.Add(nrFact); continue; }

                string nrContr = gasit.Cells["col_c_nr"].Value?.ToString() ?? "";
                string cursant = gasit.Cells["col_c_cursant"].Value?.ToString() ?? "";
                matches.Add((invRow, gasit, nrFact, nrContr, cursant, f.Suma, strategie));
                asociateDejaInMatch.Add(gasit);
            }

            if (matches.Count == 0)
            {
                string msg = "Nu s-a găsit nicio asociere automată.";
                if (ambigue.Count > 0)  msg += $"\n\nAmbigue ({ambigue.Count}):\n" + string.Join("\n", ambigue.Select(a => "  • " + a));
                if (niciuna.Count > 0)  msg += $"\n\nFără potrivire ({niciuna.Count}):\n" + string.Join("\n", niciuna.Select(a => "  • " + a));
                if (!string.IsNullOrWhiteSpace(m_facturi.FirstOrDefault()?.NumeClient ?? ""))
                    msg += "\n\nSfat: importați facturile via Excel SmartBill — aduce și clientul, care permite asociere automată.";
                MessageBox.Show(msg, "Auto-asociere", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string preview = string.Join("\n", matches.Select(m =>
                $"  {m.nrFact} → {m.nrContr} — {m.cursant}  [{m.strategie}]"));
            if (ambigue.Count > 0 || niciuna.Count > 0)
                preview += $"\n\nRămân nerezolvate: {ambigue.Count + niciuna.Count} (asociere manuală necesară)";

            var confirm = MessageBox.Show(
                $"Se vor asocia automat {matches.Count} facturi:\n\n{preview}\n\nContinui?",
                "Auto-asociere", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            var luna   = new DateTime(m_dtpLuna.Value.Year, m_dtpLuna.Value.Month, 1);
            var lunaSf = luna.AddMonths(1).AddDays(-1);
            int salvate = 0;

            foreach (var (invRow, ctrRow, nrFact, nrContr, cursant, suma, _) in matches)
            {
                if (!int.TryParse(ctrRow.Cells["col_c_cucoId"].Value?.ToString(), out int cucoId)) continue;
                if (invRow.Tag is not FacturaImportata f) continue;

                try
                {
                    using var cmdIns = new SqlCommand(
                        @"INSERT INTO FV_Facturi
                            (FVFA_CUCO_Id, FVFA_NrFactura, FVFA_DataEmitere,
                             FVFA_PerioadaInceput, FVFA_PerioadaSfarsit, FVFA_Suma,
                             FVFA_MBUT_Id, FVFA_DataSalvarii)
                          VALUES (@cuco,@nrf,@de,@pi,@ps,@suma,@u,GETDATE())",
                        m_manager.GetConnection());
                    cmdIns.Parameters.AddWithValue("@cuco", cucoId);
                    cmdIns.Parameters.AddWithValue("@nrf",  nrFact);
                    cmdIns.Parameters.AddWithValue("@de",   f.Data);
                    cmdIns.Parameters.AddWithValue("@pi",   luna);
                    cmdIns.Parameters.AddWithValue("@ps",   lunaSf);
                    cmdIns.Parameters.AddWithValue("@suma", f.Suma);
                    cmdIns.Parameters.AddWithValue("@u",    m_manager.GetUserId());
                    cmdIns.ExecuteNonQuery();

                    invRow.DefaultCellStyle.BackColor = Color.FromArgb(220, 252, 231);
                    invRow.DefaultCellStyle.ForeColor = Color.FromArgb(22, 101, 52);
                    invRow.Cells["col_status_inv"].Value =
                        (invRow.Cells["col_status_inv"].Value?.ToString() ?? "") +
                        $" ✓ → contract {nrContr}";
                    invRow.Tag = null;
                    m_gridContracte.Rows.Remove(ctrRow);
                    salvate++;
                }
                catch (Exception ex)
                {
                    CEroare.Arata("Eroare la salvare", $"{nrFact}: {ex.Message}");
                }
            }

            m_lblStatus.Text = $"{salvate} facturi asociate automat. Rămân {m_gridContracte.Rows.Count} contracte neasisociate.";
        }

        private static string NormalizeazaNume(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return "";
            return string.Join(" ", s.Trim().ToLowerInvariant()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries));
        }

        private static string NormalizeazaNumeRevers(string s)
        {
            var parts = NormalizeazaNume(s).Split(' ');
            return parts.Length >= 2
                ? string.Join(" ", parts.Reverse())
                : NormalizeazaNume(s);
        }

        private void m_btnInchide_Click(object sender, EventArgs e) => Close();
    }
}

using ExcelDataReader;
using GestiunePrezente.Common;
using Microsoft.Data.SqlClient;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace GestiunePrezente.SmartBill
{
    /// <summary>
    /// Integrates with SmartBill API (https://ws.smartbill.ro/SBORO/api/).
    /// Auth: HTTP Basic (email:apikey in Base64).
    /// Config stored in MB_Societate (MBSO_SmartBillEmail, MBSO_SmartBillApiKey, MBSO_SmartBillSerie).
    /// </summary>
    public class CSmartBillService
    {
        private static readonly HttpClient _http = new();
        private const string BASE_URL   = "https://ws.smartbill.ro/SBORO/api/";
        private const string SBWEB_URL  = "https://ws.smartbill.ro/SBWeb/api/";
        private const string USER_AGENT = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/124.0.0.0 Safari/537.36";

        private static readonly JsonSerializerOptions _jsonOpts = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };

        public string Email  { get; }
        public string ApiKey { get; }
        public string Serie  { get; }
        public string CUI    { get; }

        public CSmartBillService(string email, string apiKey, string serie, string cui)
        {
            Email  = email;
            ApiKey = apiKey;
            Serie  = serie;
            CUI    = cui;
        }

        // ── Factory ──────────────────────────────────────────────────────
        /// <summary>Returns null if config columns don't exist or are empty.</summary>
        public static CSmartBillService? FromDb(CManagerDeProiect mm)
        {
            try
            {
                var conn = mm.GetConnection();

                // Check columns exist (graceful before SQL migration)
                using var cmdCheck = new SqlCommand(
                    "SELECT COUNT(*) FROM sys.columns WHERE object_id=OBJECT_ID('MB_Societate') AND name='MBSO_SmartBillEmail'",
                    conn);
                if ((int)(cmdCheck.ExecuteScalar() ?? 0) == 0) return null;

                using var cmd = new SqlCommand(
                    @"SELECT ISNULL(MBSO_SmartBillEmail,''), ISNULL(MBSO_SmartBillApiKey,''),
                             ISNULL(MBSO_SmartBillSerie,''),  ISNULL(MBSO_CUI,'')
                      FROM MB_Societate WHERE MBSO_Id = @id", conn);
                cmd.Parameters.AddWithValue("@id", mm.GetSocietate());
                using var rd = cmd.ExecuteReader();
                if (!rd.Read()) return null;

                string email  = rd.GetString(0).Trim();
                string apiKey = rd.GetString(1).Trim();
                string serie  = rd.GetString(2).Trim();
                string cui    = rd.GetString(3).Trim();

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(serie))
                    return null;

                return new CSmartBillService(email, apiKey, serie, cui);
            }
            catch { return null; }
        }

        // ── Results ──────────────────────────────────────────────────────
        public record FacturaResult(bool Success, string? Serie, string? Numar, string? ErrorText);

        public record FacturaImportata(
            string Serie, string Numar, DateTime Data,
            string NumeClient, decimal Suma, string Descriere)
        {
            // Extrage nr. contract din descriere: "nr 34" sau "nr. 34"
            public int? NrContractDetectat =>
                Regex.Match(Descriere, @"\bnr\.?\s*(\d+)\b", RegexOptions.IgnoreCase) is { Success: true } m
                    ? int.Parse(m.Groups[1].Value) : null;
        }

        public record ListaFacturiResult(bool Success, List<FacturaImportata>? Facturi, string? ErrorText);

        // ── Payload types ────────────────────────────────────────────────
        private record ClientSB(
            string Name, string VatCode, string RegCom, string Address,
            bool IsTaxPayer, string City, string County, string Country,
            string Email, bool SaveToDb, bool IsCompany);

        private record ProdusSB(
            string Name, bool IsDiscount, string MeasuringUnitName, string Currency,
            decimal Quantity, decimal Price, bool IsTaxIncluded,
            string TaxName, int TaxPercentage, bool IsService, bool SaveToDb);

        private record FacturaSB(
            string CompanyVatCode, ClientSB Client, bool IsDraft,
            string IssueDate, string SeriesName, string Language,
            string Currency, ProdusSB[] Products);

        // ── API calls ────────────────────────────────────────────────────
        /// <summary>
        /// Creates an invoice in SmartBill and returns the series + number.
        /// </summary>
        public async Task<FacturaResult> GenereazaFacturaAsync(
            string numeClient, string? cuiClient, bool isCompany,
            DateTime dataEmitere, string descriereProdus, decimal pret,
            string unitateMasura = "luna")
        {
            try
            {
                var payload = new FacturaSB(
                    CompanyVatCode: CUI,
                    Client: new ClientSB(
                        Name:       numeClient,
                        VatCode:    cuiClient ?? "",
                        RegCom:     "",
                        Address:    "",
                        IsTaxPayer: !string.IsNullOrEmpty(cuiClient),
                        City:       "",
                        County:     "",
                        Country:    "Romania",
                        Email:      "",
                        SaveToDb:   false,
                        IsCompany:  isCompany
                    ),
                    IsDraft:    false,
                    IssueDate:  dataEmitere.ToString("yyyy-MM-dd"),
                    SeriesName: Serie,
                    Language:   "RO",
                    Currency:   "RON",
                    Products: [
                        new ProdusSB(
                            Name:               descriereProdus,
                            IsDiscount:         false,
                            MeasuringUnitName:  unitateMasura,
                            Currency:           "RON",
                            Quantity:           1,
                            Price:              pret,
                            IsTaxIncluded:      true,
                            TaxName:            "Fara TVA",
                            TaxPercentage:      0,
                            IsService:          true,
                            SaveToDb:           false
                        )
                    ]
                );

                string json = JsonSerializer.Serialize(payload, _jsonOpts);

                using var req = new HttpRequestMessage(HttpMethod.Post, BASE_URL + "invoice");
                req.Headers.Authorization = BuildAuth();
                req.Headers.TryAddWithoutValidation("User-Agent", USER_AGENT);
                req.Content = new StringContent(json, Encoding.UTF8, "application/json");

                using var resp = await _http.SendAsync(req);
                string body = await resp.Content.ReadAsStringAsync();

                using var doc  = JsonDocument.Parse(body);
                var root = doc.RootElement;

                string? err   = root.TryGetProperty("errorText", out var ep) ? ep.GetString() : null;
                if (!string.IsNullOrWhiteSpace(err))
                    return new FacturaResult(false, null, null, err);

                // SmartBill also returns errors in "message" field for HTTP 4xx
                if (!resp.IsSuccessStatusCode)
                {
                    string? msg = root.TryGetProperty("message", out var mp) ? mp.GetString() : null;
                    return new FacturaResult(false, null, null, msg ?? $"HTTP {(int)resp.StatusCode}");
                }

                string? serie = root.TryGetProperty("series", out var sp) ? sp.GetString() : null;
                string? numar = root.TryGetProperty("number", out var np) ? np.GetString() : null;

                return new FacturaResult(true, serie, numar, null);
            }
            catch (Exception ex)
            {
                return new FacturaResult(false, null, null, ex.Message);
            }
        }

        /// <summary>
        /// Tests the connection by listing invoice series. Returns null on success, error message otherwise.
        /// </summary>
        public async Task<string?> TesteazaConexiuneAsync()
        {
            if (string.IsNullOrWhiteSpace(CUI))
                return "CUI-ul firmei nu este completat în baza de date (MBSO_CUI din MB_Societate).";

            try
            {
                // Test: GET /invoice/series?cif={cif}
                // 200 = totul OK; 401/403 = credentiale gresite; 404 = auth ok, dar CUI neidentificat
                using var req = new HttpRequestMessage(HttpMethod.Get,
                    BASE_URL + "invoice/series?cif=" + Uri.EscapeDataString(CUI));
                req.Headers.Authorization = BuildAuth();
                req.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                req.Headers.TryAddWithoutValidation("User-Agent", USER_AGENT);

                using var resp = await _http.SendAsync(req);
                string body = await resp.Content.ReadAsStringAsync();

                if (resp.IsSuccessStatusCode) return null;

                int code = (int)resp.StatusCode;

                if (code == 401 || code == 403)
                    return "Email sau API key incorect (HTTP " + code + ").";

                // 404 pe invoice/series = auth ok, dar seria nu exista sau firma nu e gasita
                // pentru neplătitori TVA, /tax returnează 404 — verificam daca cel putin auth merge
                if (code == 404)
                {
                    // Verificam explicit autentificarea cu un al doilea apel fara CUI
                    using var req2 = new HttpRequestMessage(HttpMethod.Get, BASE_URL + "invoice/series");
                    req2.Headers.Authorization = BuildAuth();
                    using var resp2 = await _http.SendAsync(req2);
                    if ((int)resp2.StatusCode != 401 && (int)resp2.StatusCode != 403)
                        return null; // autentificare OK — 404 e din cauza CUI-ului/seriei
                    return "Email sau API key incorect.";
                }

                return $"HTTP {code} — {(body.Length > 300 ? body[..300] : body)}";
            }
            catch (Exception ex) { return ex.Message; }
        }

        // ── Descărcare facturi pe perioadă ───────────────────────────────
        /// <summary>
        /// Downloads invoices from SmartBill for a given date range.
        /// Returns list of invoices with auto-detected contract numbers from description.
        /// </summary>
        // SmartBill SBWeb API — GET /invoice/paymentstatus returns invoices with payment status.
        // Note: "seriesname" (lowercase n) is required; "seriesName" returns 403/404.
        public async Task<ListaFacturiResult> DescarcaFacturiLunaAsync(DateTime start, DateTime end)
        {
            string cif = Regex.Replace(CUI.Trim(), @"^RO", "", RegexOptions.IgnoreCase);
            if (string.IsNullOrEmpty(cif))
                return new ListaFacturiResult(false, null, "CUI-ul firmei nu este completat în baza de date.");

            string qs = $"?cif={Uri.EscapeDataString(cif)}" +
                        $"&seriesname={Uri.EscapeDataString(Serie)}" +
                        $"&startDate={start:yyyy-MM-dd}&endDate={end:yyyy-MM-dd}";

            string url = BASE_URL + "invoice/paymentstatus" + qs;

            try
            {
                using var req = new HttpRequestMessage(HttpMethod.Get, url);
                req.Headers.Authorization = BuildAuth();
                req.Headers.Accept.Clear();
                req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                req.Headers.TryAddWithoutValidation("User-Agent", USER_AGENT);

                using var resp = await _http.SendAsync(req);
                string body = await resp.Content.ReadAsStringAsync();
                int code = (int)resp.StatusCode;

                if (!resp.IsSuccessStatusCode)
                {
                    string errDetail = body;
                    try
                    {
                        using var doc = JsonDocument.Parse(body);
                        if (doc.RootElement.TryGetProperty("errorText", out var ep))
                            errDetail = ep.GetString() ?? body;
                        else if (doc.RootElement.TryGetProperty("message", out var mp))
                            errDetail = mp.GetString() ?? body;
                    }
                    catch { }
                    return new ListaFacturiResult(false, null, $"HTTP {code} — {errDetail}");
                }

                var facturi = ParseazaFacturi(body);
                if (facturi == null)
                    return new ListaFacturiResult(false, null,
                        "Răspuns primit (200 OK), dar JSON-ul nu a putut fi parsat.\nConținut:\n" +
                        (body.Length > 400 ? body[..400] : body));

                return new ListaFacturiResult(true, facturi, null);
            }
            catch (Exception ex)
            {
                return new ListaFacturiResult(false, null, $"Eroare la apelul SmartBill: {ex.Message}");
            }
        }

        // ── Scanare secvențială după număr factură ───────────────────────
        /// <summary>
        /// Sequentially fetches invoices by number starting at numarStart.
        /// Stops after 2 consecutive misses (not found / error).
        /// Ideal for typical monthly volumes (tens of invoices).
        /// </summary>
        public async Task<ListaFacturiResult> SincronizeazaSecventialAsync(
            int numarStart,
            int nrPadding = 0,
            DateTime dataDefault = default,
            IProgress<string>? progress = null,
            CancellationToken ct = default)
        {
            string cif = Regex.Replace(CUI.Trim(), @"^RO", "", RegexOptions.IgnoreCase);
            if (string.IsNullOrEmpty(cif))
                return new ListaFacturiResult(false, null, "CUI-ul firmei nu este completat.");

            var facturiNoi = new List<FacturaImportata>();
            int curent = numarStart;
            int eroriConsecutive = 0;
            const int MAX_ERORI = 2;
            string? primaEroare = null;

            while (eroriConsecutive < MAX_ERORI && !ct.IsCancellationRequested)
            {
                string nrStr = nrPadding > 0
                    ? curent.ToString().PadLeft(nrPadding, '0')
                    : curent.ToString();
                string url = BASE_URL +
                    $"invoice/paymentstatus?cif={Uri.EscapeDataString(cif)}" +
                    $"&seriesname={Uri.EscapeDataString(Serie)}" +
                    $"&number={Uri.EscapeDataString(nrStr)}";

                progress?.Report($"Verific {Serie}{nrStr}...");

                try
                {
                    using var req = new HttpRequestMessage(HttpMethod.Get, url);
                    req.Headers.Authorization = BuildAuth();
                    req.Headers.Accept.Clear();
                    req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    req.Headers.TryAddWithoutValidation("User-Agent", USER_AGENT);

                    using var resp = await _http.SendAsync(req, ct);
                    string body = await resp.Content.ReadAsStringAsync(ct);

                    if (!resp.IsSuccessStatusCode)
                    {
                        primaEroare ??= $"HTTP {(int)resp.StatusCode} pentru {Serie}{nrStr} — {(body.Length > 200 ? body[..200] : body)}";
                        eroriConsecutive++;
                        curent++;
                        continue;
                    }

                    using var doc = JsonDocument.Parse(body);
                    var root = doc.RootElement;

                    if (root.TryGetProperty("errorText", out var ep) && !string.IsNullOrWhiteSpace(ep.GetString()))
                    {
                        eroriConsecutive++;
                        curent++;
                        continue;
                    }

                    eroriConsecutive = 0;

                    // paymentstatus response: number/series fields are empty — use the values we sent
                    // Fields available: invoiceTotalAmount, paidAmount, unpaidAmount, paid
                    // issueDate and clientName are NOT returned by this endpoint
                    decimal total = 0m;
                    if (root.TryGetProperty("invoiceTotalAmount", out var totProp))
                    {
                        if (totProp.ValueKind == JsonValueKind.Number) total = totProp.GetDecimal();
                        else decimal.TryParse(totProp.GetString(), out total);
                    }

                    decimal platit   = 0m;
                    decimal restant  = 0m;
                    if (root.TryGetProperty("paidAmount",   out var pprop)) { if (pprop.ValueKind == JsonValueKind.Number) platit  = pprop.GetDecimal(); }
                    if (root.TryGetProperty("unpaidAmount", out var uprop)) { if (uprop.ValueKind == JsonValueKind.Number) restant = uprop.GetDecimal(); }
                    bool paid = root.TryGetProperty("paid", out var paidProp) && paidProp.GetBoolean();

                    string status = paid
                        ? $"Plătită"
                        : platit > 0
                            ? $"Parțial plătită (plătit {platit:N2}, restant {restant:N2})"
                            : $"Neplătită (restant {restant:N2})";

                    // url field: non-empty on some accounts — include if present
                    string invoiceUrl = root.TryGetProperty("url", out var urlProp) ? urlProp.GetString() ?? "" : "";
                    if (!string.IsNullOrEmpty(invoiceUrl)) status += $" | {invoiceUrl}";


                    DateTime data = dataDefault == default ? DateTime.Today : dataDefault;

                    facturiNoi.Add(new FacturaImportata(Serie, nrStr, data, "", total, status));
                    curent++;
                }
                catch (OperationCanceledException) { break; }
                catch (Exception ex)
                {
                    primaEroare ??= $"Excepție la {Serie}{nrStr}: {ex.Message}";
                    eroriConsecutive++;
                    curent++;
                }
            }

            progress?.Report($"Finalizat — {facturiNoi.Count} facturi descărcate.");

            // If nothing found at all, surface the first error so user can diagnose
            if (facturiNoi.Count == 0 && primaEroare != null)
                return new ListaFacturiResult(false, null, primaEroare);

            return new ListaFacturiResult(true, facturiNoi, null);
        }

        // ── Import din Excel exportat din SmartBill ──────────────────────
        /// <summary>
        /// Parses an Excel file exported from SmartBill web interface.
        /// Column names are matched case-insensitively; order doesn't matter.
        /// </summary>
        /// <summary>
        /// Parses SmartBill Excel export. The file has 3 header rows before the column names row.
        /// "Factura" column contains serie+number concatenated (e.g. "ONX0544").
        /// Dates are in dd/MM/yyyy format.
        /// </summary>
        public static ListaFacturiResult ImportaFacturiDinExcel(string filePath)
        {
            try
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                using var stream = File.OpenRead(filePath);
                using var reader = ExcelReaderFactory.CreateReader(stream);

                // Read raw, without treating first row as header (there are 3 header rows)
                var ds = reader.AsDataSet(new ExcelDataSetConfiguration
                {
                    ConfigureDataTable = _ => new ExcelDataTableConfiguration { UseHeaderRow = false }
                });

                if (ds.Tables.Count == 0)
                    return new ListaFacturiResult(false, null, "Fișierul Excel nu conține foi de calcul.");

                var table = ds.Tables[0];

                // Find the row that contains column headers (has both "Client" and "Factura")
                int headerRow = -1;
                var cols = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                for (int r = 0; r < Math.Min(table.Rows.Count, 15); r++)
                {
                    var row = table.Rows[r];
                    bool hasClient = false, hasFactura = false;
                    var tmp = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                    for (int c = 0; c < table.Columns.Count; c++)
                    {
                        string val = row[c]?.ToString()?.Trim() ?? "";
                        if (string.IsNullOrEmpty(val)) continue;
                        tmp[val] = c;
                        if (val.Equals("Client",  StringComparison.OrdinalIgnoreCase)) hasClient  = true;
                        if (val.Equals("Factura", StringComparison.OrdinalIgnoreCase)) hasFactura = true;
                    }

                    if (hasClient && hasFactura) { headerRow = r; cols = tmp; break; }
                }

                if (headerRow < 0)
                    return new ListaFacturiResult(false, null,
                        "Nu s-a găsit rândul cu titlurile coloanelor.\n" +
                        "Asigurați-vă că exportați din SmartBill → Facturi → Export Excel.");

                int iFactura = cols.TryGetValue("Factura",        out var v1) ? v1 : -1;
                int iClient  = cols.TryGetValue("Client",         out var v2) ? v2 : -1;
                int iData    = cols.TryGetValue("Data emiterii",  out var v3) ? v3
                             : cols.TryGetValue("Data Emiterii",  out var v3b) ? v3b : -1;
                int iSuma    = cols.TryGetValue("Valoare Totala", out var v4) ? v4
                             : cols.TryGetValue("Valoare Totală", out var v4b) ? v4b : -1;
                int iStatus  = cols.TryGetValue("Status",         out var v5) ? v5 : -1;
                int iObs     = cols.TryGetValue("Observatii",     out var v6) ? v6
                             : cols.TryGetValue("Observații",     out var v6b) ? v6b : -1;

                if (iFactura < 0)
                    return new ListaFacturiResult(false, null, "Coloana 'Factura' nu a fost găsită.");

                var result = new List<FacturaImportata>();
                var reSN   = new Regex(@"^([A-Za-z]+)(\d+)$");

                for (int r = headerRow + 1; r < table.Rows.Count; r++)
                {
                    var row = table.Rows[r];
                    string Get(int idx) => idx >= 0 && !row.IsNull(idx) ? row[idx]?.ToString()?.Trim() ?? "" : "";

                    string facturaFull = Get(iFactura);
                    string client      = Get(iClient);
                    if (string.IsNullOrEmpty(facturaFull) || string.IsNullOrEmpty(client)) continue;

                    // Split "ONX0544" → serie="ONX", number="0544"
                    var mSN   = reSN.Match(facturaFull);
                    string serie = mSN.Success ? mSN.Groups[1].Value : "";
                    string numar = mSN.Success ? mSN.Groups[2].Value : facturaFull;

                    // Parse date: dd/MM/yyyy (SmartBill export format)
                    DateTime data = DateTime.Today;
                    if (iData >= 0 && !row.IsNull(iData))
                    {
                        var raw = row[iData];
                        if (raw is DateTime dt) data = dt;
                        else DateTime.TryParseExact(raw?.ToString(),
                            new[] { "dd/MM/yyyy", "d/M/yyyy", "dd.MM.yyyy", "yyyy-MM-dd" },
                            System.Globalization.CultureInfo.InvariantCulture,
                            System.Globalization.DateTimeStyles.None, out data);
                    }

                    // Parse amount (Excel may store as double)
                    decimal suma = 0;
                    if (iSuma >= 0 && !row.IsNull(iSuma))
                    {
                        var raw = row[iSuma];
                        if      (raw is double d)   suma = (decimal)d;
                        else if (raw is decimal dec) suma = dec;
                        else
                        {
                            string s = raw?.ToString()?.Replace(" ", "") ?? "";
                            if (s.Contains(',') && s.Contains('.')) s = s.Replace(".", "").Replace(",", ".");
                            else if (s.Contains(',')) s = s.Replace(",", ".");
                            decimal.TryParse(s, System.Globalization.NumberStyles.Any,
                                System.Globalization.CultureInfo.InvariantCulture, out suma);
                        }
                    }

                    // Use Status + Observatii as description (no product description in SmartBill export)
                    string status = Get(iStatus);
                    string obs    = Get(iObs);
                    string desc   = string.IsNullOrEmpty(obs) ? status : $"{status} | {obs}";

                    result.Add(new FacturaImportata(serie, numar, data, client, suma, desc));
                }

                return new ListaFacturiResult(true, result, null);
            }
            catch (Exception ex)
            {
                return new ListaFacturiResult(false, null, "Eroare la citirea fișierului Excel:\n" + ex.Message);
            }
        }

        private static List<FacturaImportata>? ParseazaFacturi(string json)
        {
            try
            {
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                JsonElement arr = root.ValueKind == JsonValueKind.Array ? root
                    : root.TryGetProperty("list",     out var l1) ? l1
                    : root.TryGetProperty("invoices", out var l2) ? l2
                    : root.TryGetProperty("data",     out var l3) ? l3
                    : default;

                if (arr.ValueKind != JsonValueKind.Array) return null;

                var result = new List<FacturaImportata>();
                foreach (var item in arr.EnumerateArray())
                {
                    string serie  = item.TryGetProperty("seriesName", out var sp)  ? sp.GetString()  ?? ""
                                  : item.TryGetProperty("series",     out var sp2) ? sp2.GetString() ?? "" : "";
                    string numar  = item.TryGetProperty("number",     out var np)  ? np.GetString()  ?? "" : "";
                    string client = item.TryGetProperty("clientName", out var cp)  ? cp.GetString()  ?? ""
                                  : item.TryGetProperty("client", out var cp2)
                                       && cp2.TryGetProperty("name", out var cn)   ? cn.GetString()  ?? "" : "";

                    decimal suma = 0m;
                    if (item.TryGetProperty("total", out var tp))
                    {
                        if (tp.ValueKind == JsonValueKind.Number) suma = tp.GetDecimal();
                        else decimal.TryParse(tp.GetString(), out suma);
                    }

                    string status      = item.TryGetProperty("status",      out var stp) ? stp.GetString() ?? "" : "";
                    bool   isCancelled = item.TryGetProperty("isCancelled", out var icp) && icp.GetBoolean();
                    if (isCancelled) status = string.IsNullOrEmpty(status) ? "Anulată" : $"{status} (Anulată)";

                    DateTime data = DateTime.Today;
                    if (item.TryGetProperty("issueDate", out var ip) && DateTime.TryParse(ip.GetString(), out var dt))
                        data = dt;

                    if (!string.IsNullOrEmpty(serie) || !string.IsNullOrEmpty(numar))
                        result.Add(new FacturaImportata(serie, numar, data, client, suma, status));
                }
                return result;
            }
            catch { return null; }
        }

        private AuthenticationHeaderValue BuildAuth() =>
            new("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Email}:{ApiKey}")));
    }
}

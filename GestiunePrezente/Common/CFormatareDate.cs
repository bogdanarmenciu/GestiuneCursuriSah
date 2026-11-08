using System.Globalization;

namespace GestiunePrezente.Common
{
    public static class CFormatareDate
    {
        public static string? GetString(string sId) =>
            StringTable.ResourceManager.GetString(sId, CultureInfo.CurrentUICulture);

        public static void ShowMessage(string sId)
        {
            var text = GetString(sId) ?? sId;
            var title = GetString("IDS_EROARE") ?? "Eroare";
            MessageBox.Show(text, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowWarning(string sId, int lParam = 0)
        {
            var text = lParam == 0 ? GetString(sId) ?? sId : sId;
            var title = GetString("IDS_EROARE") ?? "Avertisment";
            MessageBox.Show(text, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public static void ShowMessageOK(string sId, int lParam = 0)
        {
            var text = lParam == 0 ? GetString(sId) ?? sId : sId;
            var title = GetString("IDS_OK") ?? "OK";
            MessageBox.Show(text, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static string StringToSqlString(string sSql) =>
            sSql.Replace("'", "''");

        public static string DateToSQLString(DateTime dt) =>
            $"CONVERT(datetime, '{dt:yyyy-MM-dd}', 102)";

        public static string DateTimeToSQLString(DateTime dt) =>
            $"CONVERT(datetime, '{dt:yyyy-MM-dd HH:mm:ss}', 102)";

        public static string DecimalToString(object? dec) =>
            dec?.ToString()?.Replace(",", ".", StringComparison.Ordinal) ?? string.Empty;

        public static string DecimalToString(decimal dec) =>
            dec.ToString(CultureInfo.InvariantCulture);

        public static string DateTimeToSQLStringWithoutSeconds(DateTime dt) =>
            $"CONVERT(datetime, '{dt:yyyy-MM-dd HH:mm}', 102)";
    }
}

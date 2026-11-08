using System.Xml;

namespace GestiunePrezente
{
    /// <summary>
    /// Preferinte per-utilizator Windows, stocate in %APPDATA%\ONIXTEIA\preferinte.cfg.
    /// Separat de AppConfig (config server/DB, la nivel de aplicatie).
    /// </summary>
    static class UserPrefs
    {
        private static string FilePath =>
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "ONIXTEIA", "preferinte.cfg");

        public static string Get(string key)
        {
            if (!File.Exists(FilePath)) return string.Empty;
            try
            {
                var doc = new XmlDocument();
                doc.Load(FilePath);
                var node = doc.DocumentElement?.SelectSingleNode(
                    $"//configuration/add[@key='{key}']/@value");
                return node?.Value ?? string.Empty;
            }
            catch { return string.Empty; }
        }

        public static void Set(string key, string value)
        {
            try
            {
                var doc = EnsureFile();
                var node = doc.DocumentElement?.SelectSingleNode(
                    $"//configuration/add[@key='{key}']/@value");
                if (node != null)
                {
                    node.Value = value;
                }
                else
                {
                    var el  = doc.CreateElement("add");
                    var atK = doc.CreateAttribute("key");   atK.Value = key;
                    var atV = doc.CreateAttribute("value"); atV.Value = value;
                    el.Attributes.Append(atK);
                    el.Attributes.Append(atV);
                    doc.DocumentElement?.SelectSingleNode("//configuration")?.AppendChild(el);
                }
                doc.Save(FilePath);
            }
            catch { }
        }

        private static XmlDocument EnsureFile()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);
            if (!File.Exists(FilePath))
                File.WriteAllText(FilePath,
                    "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<configuration>\n</configuration>",
                    System.Text.Encoding.UTF8);

            var doc = new XmlDocument();
            doc.Load(FilePath);
            return doc;
        }
    }
}

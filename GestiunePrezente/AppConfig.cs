using System.Xml;

namespace GestiunePrezente
{
    class AppConfig
    {
        public static string Get(string key)
        {
            XmlDocument doc = new();
            doc.Load(Application.ExecutablePath + ".xml");
            string nodeKey = "//configuration/add[@key='" + key + "']/@value";
            try
            {
                var node = doc.DocumentElement?.SelectSingleNode(nodeKey);
                if (node != null && node.Value != null)
                {
                    return node.Value;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch
            {
                return string.Empty;
            }
        }
        public static void Set(string key, string keyValue)
        {
            XmlDocument doc = MakeConfig();
            string nodeKey = "//configuration/add[@key='" + key + "']/@value";
            var documentElement = doc.DocumentElement;
            if (documentElement != null)
            {
                var node = documentElement.SelectSingleNode(nodeKey);
                if (node != null)
                {
                    node.Value = keyValue;
                }
                else
                {
                    XmlElement add = doc.CreateElement("add");
                    XmlAttribute attrkey = doc.CreateAttribute("key");
                    attrkey.Value = key;
                    XmlAttribute val = doc.CreateAttribute("value");
                    val.Value = keyValue;
                    add.Attributes.Append(attrkey);
                    add.Attributes.Append(val);
                    var configNode = documentElement.SelectSingleNode("//configuration");
                    configNode?.AppendChild(add);
                }
                doc.Save(Application.ExecutablePath + ".xml");
            }
        }
        private static XmlDocument MakeConfig()
        {
            if (!File.Exists(Application.ExecutablePath + ".xml"))
            {
                string config = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" +
                    "<configuration>\n" +
                    "<add key=\"Server\" value=\"\" />\n" +
                    "<add key=\"DataBase\" value=\"\" />\n" +
                    "<add key=\"User\" value=\"\" />\n" +
                    "<add key=\"Pass\" value=\"\" />\n" +
                    "<add key=\"UseIntegratedSecurity\" value=\"1\" />\n" +
                    "</configuration>";
                StreamWriter streamWriter = File.CreateText(Application.ExecutablePath + ".xml");
                streamWriter.AutoFlush = true;
                streamWriter.Write(config);
                streamWriter.Close();
            }
            XmlDocument doc = new();
            doc.Load(Application.ExecutablePath + ".xml");
            return doc;
        }
    }
}

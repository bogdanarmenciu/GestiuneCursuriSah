using System.Data;
using GestiunePrezente.Common;
using System.Globalization;
using System.Management;
using System.Diagnostics;

namespace GestiunePrezente.MeniuPrincipal
{
    public partial class FrmLogin : Form
    {
        private class Item(string name, int value)
        {
            public string Name = name;
            public int Value = value;

            public override string ToString()
            {
                return Name;
            }
        }

        class HardDrive
        {
            public string? Model { get; set; } = null;

            public string? Type { get; set; } = null;

            public string? SerialNo { get; set; } = null;
        }

        readonly CManagerDeProiect ManagerDeProiect;
        private readonly bool stop = false;
        private bool bLogin = false;
        string? m_sSql;
        readonly string sSoc = "";
        readonly string sDiv = "";
        readonly string sUser = "";
        readonly string sPass = "";
        readonly string m_sTipDocument = "";
        readonly string m_sDocumentId = "";
        public FrmLogin(string[] args)
        {

            if (args.Length == 4)
            {
                sSoc = args[0];
                sDiv = args[1];
                sUser = args[2];
                sPass = args[3];
            }
            else
            {
                if (args.Length == 6)
                {
                    sSoc = args[0];
                    sDiv = args[1];
                    sUser = args[2];
                    sPass = args[3];
                    m_sTipDocument = args[4];
                    m_sDocumentId = args[5];
                }
            }

            ManagerDeProiect = new CManagerDeProiect();
            InitializeComponent();

            string sMBSO1 = "Onixteia";
            string sMBSO2 = "";

            string sMBSODefault = sSoc == "" ? AppConfig.Get("SocietateDefault") : sSoc;

            string sMBDVDefault = sDiv == "" ? AppConfig.Get("DiviziuneDefault") : sDiv;



            if (sMBSODefault == "")
            {
                sMBSODefault = "Onixteia";
                sMBDVDefault = "Sibiu";
                sMBSO1 = AppConfig.Get("Database");

                string sMBDV = AppConfig.Get("Diviziune");
                switch (sMBDV)
                {
                    case "1": sMBDVDefault = "Sibiu"; break;
                }
            }


            m_cbSocietate.Items.Add(new Item(sMBSO1, 1));
            if (sMBSO2 != "")
                m_cbSocietate.Items.Add(new Item(sMBSO2, 2));

            m_cbSocietate.Text = sMBSODefault;
            m_cbDiviziune.Text = sMBDVDefault;

            IncarcaServere();

            m_ebUtilizator.Text = sUser == "" ? UserPrefs.Get("Utilizator") : sUser;

#if DEBUG
            m_ebParola.Text = "0Suno&&eishin";
#endif

            if (sPass != "")
                m_ebParola.Text = sPass;

        }

        public bool GetLogin()
        {
            return bLogin;

        }

        private void m_ebUtilizator_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Conectare();
            }
        }

        private void m_btConectare_Click(object sender, EventArgs e)
        {
            Conectare();
        }

        private void m_ebParola_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Conectare();
            }
        }

        private void m_cbSocietate_SelectedIndexChanged(object sender, EventArgs e)
        {
            Item itm = (Item)m_cbSocietate.SelectedItem!;

            this.m_cbDiviziune.Items.Clear();
            m_cbDiviziune.Text = "";


            if (itm.Name == "Onixteia")
            {
                m_cbDiviziune.Items.Add(new Item("Sibiu", 1));
            }
        }

        public void Conectare()
        {
            string db = "", sMBSO = "0", sMBDV = "0", sDiviziune = "";

            string connectionString;
            string srv = m_cbServer.Text.Trim();
            if (srv == "")
                srv = "192.168.1.233";

            Item itmSoc = (Item)m_cbSocietate.SelectedItem!;
            if (itmSoc == null)
            {
                //INCERCAM BD DE TEST
                db = m_cbSocietate.Text;

                if (db == "")
                {
                    CFormatareDate.ShowWarning("IDS_LOGIN_SOC");
                    m_cbSocietate.Focus();
                    return;
                }
                else//AVEM DB DE TEST
                {
                    if ((db.Contains("Onixteia") && (db.Contains("test") || db.Contains("Test"))))
                    {
                        db = "OnixteiaTest";
                        sMBSO = "1";
                    }
                    else
                    {
                        CFormatareDate.ShowWarning("IDS_LOGIN_SOC");
                        return;
                    }

                    sDiviziune = m_cbDiviziune.Text;
                    switch (sDiviziune)
                    {
                        case "Sibiu":
                            { sMBDV = "1"; break; }
                        default: { sMBDV = "0"; break; }
                    }

                    if (sMBSO == "0")
                    {
                        CFormatareDate.ShowWarning("IDS_LOGIN_SOC");
                        return;
                    }

                    if (sMBDV == "0")
                    {
                        CFormatareDate.ShowWarning("IDS_LOGIN_DIV");
                        return;
                    }
                }
            }
            else
            {
                sMBSO = itmSoc.Value.ToString();


                db = itmSoc.Name;
                if (itmSoc.Name == "Onixteia")
                    db = "Onixteia";

                Item itmDiv = (Item)m_cbDiviziune.SelectedItem!;
                if (itmDiv == null)
                {
                    CFormatareDate.ShowWarning("IDS_LOGIN_DIV");
                    m_cbDiviziune.Focus();
                    return;
                }
                sMBDV = itmDiv.Value.ToString();
            }

            string sUser, sPass;
            if (m_ebUtilizator.Text.Length == 0)
            {
                CFormatareDate.ShowWarning("IDS_LOGIN_UTILIZATOR");
                m_ebUtilizator.Focus();
                return;
            }
            sUser = m_ebUtilizator.Text;
            if (m_ebParola.Text.Length == 0)
            {
                CFormatareDate.ShowWarning("IDS_LOGIN_PAROLA");
                m_ebParola.Focus();
                return;
            }
            sPass = m_ebParola.Text;

            ManageControls(false);

            //connectionString = "Data Source=" + srv +
            //                   ";Initial Catalog=" + db +
            //                   ";Trusted_Connection=True" +
            //                   ";Workstation ID=" + System.Windows.Forms.SystemInformation.ComputerName +
            //                   ";Packet Size=4096" +
            //                   ";MultipleActiveResultSets=True" +
            //                   ";Encrypt=False" +
            //                   ";TrustServerCertificate=True";

            connectionString = "Data Source=tcp:" + srv + ",1433" +
                               ";Initial Catalog=" + db +
                               ";User ID=" + m_ebUtilizator.Text.Trim() +
                               ";Password=" + m_ebParola.Text.Trim() +
                               ";Connect Timeout=30" + // Oferă-i timp să răspundă prin rețea
                               ";Encrypt=False" +
                               ";TrustServerCertificate=True" +
                               ";MultipleActiveResultSets=True";


            if (!ValidateConnection(connectionString, out string err))
            {
                MessageBox.Show(
                    "Connection string:\n" + connectionString + "\n\nEroare reală:\n" + err,
                    "Debug conectare",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                CFormatareDate.ShowWarning("IDS_LOGIN_CONN_NEVALIDA");
                ManageControls(true);
                return;
            }
            else
            {
                ManagerDeProiect.OpenConnection(connectionString, sUser, sPass, srv, db, sMBSO, sMBDV);
                if (ManagerDeProiect.GetUserId() == 0)
                {
                    CFormatareDate.ShowWarning("IDS_LOGIN_UT_PASS_NEVALIDE");
                    ManageControls(true);
                    return;
                }

            }

            CInterogareSQL q = new(ManagerDeProiect.GetConnection());

            if (ManagerDeProiect.GetRolUser_DirectorGeneral() != 1)
            {
                // VERIFICARE HDD SERIAL
                List<HardDrive> hdCollection = [];

                // Obținem toate hard disk-urile interne (non-USB)
                ManagementObjectSearcher searcher = new("SELECT * FROM Win32_DiskDrive WHERE InterfaceType<>'USB'");
                foreach (ManagementObject wmi_HD in searcher.Get().Cast<ManagementObject>())
                {
                    HardDrive hd = new()
                    {
                        Model = wmi_HD["Model"]?.ToString() ?? "Unknown",
                        Type = wmi_HD["InterfaceType"]?.ToString() ?? "Unknown"
                    };
                    hdCollection.Add(hd);
                }

                // Obținem serialele hard disk-urilor
                searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
                int i = 0;
                foreach (ManagementObject wmi_HD in searcher.Get().Cast<ManagementObject>())
                {
                    if (i >= hdCollection.Count)
                        break;

                    HardDrive? hd = hdCollection[i];
                    hd?.SerialNo = wmi_HD["SerialNumber"]?.ToString() ?? "None";

                    ++i;
                }
            }

            // CQuery q = new CQuery(ManagerDeProiect.GetConnection());
            //VERIFICARE DACA UTILIZATORUL CORESPUNDE DIVIZIUNII
            int lMBUT = 0;
            m_sSql = string.Format(@"SELECT MBUT_Id
                                    FROM MB_Utilizatori
                                    INNER JOIN MB_Diviziune ON ISNULL(MBUT_ConectareLaDiviziune,{0})=MBDV_Id
                                    WHERE MBUT_Id = {1} AND MBDV_Id={2}", sMBDV, ManagerDeProiect.GetUserId(), sMBDV);

            q.Open(m_sSql);
            if (q.NextRow())
            {
                lMBUT = q.GetIntValue(0);
            }
            q.Close();

            if (lMBUT <= 0)
            {
                bLogin = false;
                CFormatareDate.ShowWarning("IDS_UT_NU_DIVIZIUNE");
                ManageControls(true);

                return;
            }

            int lProces = 0;
            foreach (Process p in Process.GetProcessesByName("GestiuneCursuri"))
            {
                lProces++;
            }
            //string sSql1 = CFormatareDate.GetString("IDS_NR_PRCOCES");

            //m_sSql = string.Format(sSql1, lProces.ToString());
            //CFormatareDate.ShowWarning(m_sSql, 1);
            //bLogin = false;
            //ManageControls(true);
            //return;


            if (lProces == 1)//DOAR PROCESUL CURENT
            {
                m_sSql = string.Format(@"UPDATE MB_UtilizatoriIstoricConectare SET MBUT_DataDeconectarii=MBUT_DataConectarii 
                                         WHERE MBUT_DataDeconectarii IS NULL AND MBUT_NumePC='{0}' AND MBUT_MBUT_Conectat={1}",
                                         CFormatareDate.StringToSqlString(System.Environment.MachineName), ManagerDeProiect.GetUserId());
                q.ExecuteStatement(m_sSql);
            }

            //NU SE POATE FACE CONECTAREA UNUI UTILIZATOR DE 2 ORI PE ACCEEASI DIVIZIUNE DE PE ACELASI CALCULATOR!
            lMBUT = 0;
            m_sSql = string.Format(@"SELECT COUNT(*) 
                                     FROM MB_UtilizatoriIstoricConectare
                                     WHERE MBUT_MBDV_Id={0} AND MBUT_MBUT_Conectat = {1} AND CONVERT(date, getdate())=CONVERT(date, MBUT_DataConectarii) AND MBUT_NumePC='{2}'
                                     AND MBUT_DataDeconectarii IS NULL AND MBUT_MBSO_Id={3}",
                                      sMBDV, ManagerDeProiect.GetUserId(), CFormatareDate.StringToSqlString(System.Environment.MachineName), ManagerDeProiect.GetSocietate());

            q.Open(m_sSql);
            if (q.NextRow())
            {
                lMBUT = q.GetIntValue(0);
            }
            q.Close();

            if (lMBUT > 0)
            {
                //bLogin = false;
                //CFormatareDate.ShowWarning("IDS_UT_NU_ACCEASI_DIVIZIUNE_DE2ORI");
                //ManageControls(true);

                //return;
            }


            SalveazaServer(srv);
            UserPrefs.Set("Utilizator", sUser);
            this.Close();

            bLogin = true;//CONEXIUNE REUSITA

            //INTERNET TIME
            DateTime dtNistTime;// = GetNistTime();

#if DEBUG
            dtNistTime = DateTime.Now;
#else
            //  dtNistTime = GetNistTime();
            //   dtNistTime = GetFastestNISTDate();
#endif
            dtNistTime = DateTime.Now;

            //    string s = Util.DateTimeToSQLString(dtNistTime);
            //    MessageBox.Show(s); 

            CInterogareSQL qIstoricConectare = new(ManagerDeProiect.GetConnection());

            DateTime dtDataUltimeiModificariAExelui = File.GetLastWriteTime(Application.ExecutablePath);

            DateTime dtDataUltimeiModificariAExeluiDinBD = DateTime.MinValue;
            DateTime dtDataUltimeiModificariAExeluiDinBD_PtrUtilizator = DateTime.MinValue;

            //SALVARE DATA CONEXIUNII UTILIZATORULUI
            int lMBUTInserted = 0;
#if DEBUG
            /*        m_sSql = string.Format(@" INSERT INTO MB_UtilizatoriIstoricConectare (MBUT_DataConectarii,MBUT_MBUT_Conectat,MBUT_MBDV_Id,MBUT_DataUltimeiModificariAExelui,MBUT_NumePC,MBUT_MBSO_Id)
                                                              SELECT {0},{1},{2},{3},'{4}',{5}", CFormatareDate.DateTimeToSQLString(DateTime.Now), ManagerDeProiect.GetUserId(), ManagerDeProiect.GetDiviziune(),
                                                             CFormatareDate.DateTimeToSQLString(dtDataUltimeiModificariAExelui), CFormatareDate.StringToSqlString(System.Environment.MachineName), ManagerDeProiect.GetSocietate());
                    qIstoricConectare.ExecuteStatement(m_sSql);

                    m_sSql = "SELECT SCOPE_IDENTITY() AS SID";

                    lMBUTInserted = qIstoricConectare.ExecuteToInt(m_sSql);*/
#else

            m_sSql = string.Format(@" INSERT INTO MB_UtilizatoriIstoricConectare (MBUT_DataConectarii,MBUT_MBUT_Conectat,MBUT_MBDV_Id,MBUT_DataUltimeiModificariAExelui,MBUT_NumePC,MBUT_MBSO_Id)
                                          SELECT {0},{1},{2},{3},'{4}',{5}", CFormatareDate.DateTimeToSQLString(DateTime.Now), ManagerDeProiect.GetUserId(), ManagerDeProiect.GetDiviziune(),
                                      CFormatareDate.DateTimeToSQLString(dtDataUltimeiModificariAExelui), CFormatareDate.StringToSqlString(System.Environment.MachineName), ManagerDeProiect.GetSocietate());
            qIstoricConectare.ExecuteStatement(m_sSql);

            m_sSql = "SELECT SCOPE_IDENTITY() AS SID";

            lMBUTInserted = qIstoricConectare.ExecuteToInt(m_sSql);
#endif

            //Calendar myCal = CultureInfo.InvariantCulture.Calendar;

            //string sDataInternet;
            //if (myCal.GetYear(dtNistTime) < 1000)
            //    sDataInternet = string.Format(" MBUT_DataConectariiDinInternet = NULL");
            //else
            //    sDataInternet = string.Format(" MBUT_DataConectariiDinInternet = {0}", CFormatareDate.DateTimeToSQLString(dtNistTime));

            ////DACA MAM CONECTAT DEJA DE 3 ORI CUM SOFTUL EXPIRAT=>NU MAI PERMINTEM CONEXIUNEA

            //m_sSql = string.Format(@"SELECT MAX(MBUT_DataUltimeiModificariAExelui) FROM MB_UtilizatoriIstoricConectare");
            //qIstoricConectare.Open(m_sSql);
            //if (qIstoricConectare.NextRow())
            //{
            //    dtDataUltimeiModificariAExeluiDinBD = (DateTime)qIstoricConectare.GetDateValue(0)!;
            //}

            //m_sSql = string.Format(@"SELECT TOP 1 MBUT_DataUltimeiModificariAExelui FROM MB_UtilizatoriIstoricConectare WHERE MBUT_MBUT_Conectat = {0} ORDER BY MBUT_Id DESC", ManagerDeProiect.GetUserId());
            //qIstoricConectare.Open(m_sSql);
            //if (qIstoricConectare.NextRow())
            //{
            //    dtDataUltimeiModificariAExeluiDinBD_PtrUtilizator = (DateTime)qIstoricConectare.GetDateValue(0)!;
            //}

            //int lNrConexiuniCuSoftExpirat = 0;

            //m_sSql = string.Format(@"SELECT COUNT(MBUT_Id) FROM MB_UtilizatoriIstoricConectare 
            //                        WHERE MBUT_DataUltimeiModificariAExelui={0}
            //                        AND MBUT_MBUT_Conectat={1}",
            //                        CFormatareDate.DateTimeToSQLString(dtDataUltimeiModificariAExelui),
            //                        ManagerDeProiect.GetUserId());

            //qIstoricConectare.Open(m_sSql);
            //if (qIstoricConectare.NextRow())
            //{
            //    lNrConexiuniCuSoftExpirat = qIstoricConectare.GetIntValue(0);
            //}

            //m_sSql = string.Format(@"UPDATE MB_UtilizatoriIstoricConectare SET MBUT_NrConectariCuSoftExpirat={0},{1} WHERE MBUT_Id={2}", lNrConexiuniCuSoftExpirat, sDataInternet, lMBUTInserted);
            //qIstoricConectare.ExecuteStatement(m_sSql);

#if DEBUG

#else


            if (dtDataUltimeiModificariAExeluiDinBD > dtDataUltimeiModificariAExeluiDinBD_PtrUtilizator)
            {
         //       string sSql;
                //if (lNrConexiuniCuSoftExpirat == 1)
                //    sSql = CFormatareDate.GetString("IDS_UT_NU_ULTIMAVERSIUNE_1");
                //else
                //    sSql = CFormatareDate.GetString("IDS_UT_NU_ULTIMAVERSIUNE_2");

                //     sSql = CFormatareDate.GetString("IDS_UT_NU_ULTIMAVERSIUNE"); 
                //   m_sSql = string.Format(sSql, lNrConexiuniCuSoftExpirat.ToString());
                //CFormatareDate.ShowWarning("IDS_UT_NU_ULTIMAVERSIUNE");
                //bLogin = false;//CONEXIUNE NEREUSITA
                //m_sSql = string.Format(@"UPDATE MB_UtilizatoriIstoricConectare SET MBUT_DataDeconectarii=MBUT_DataConectarii WHERE MBUT_Id={0}", lMBUTInserted);
                //qIstoricConectare.ExecuteStatement(m_sSql);
                //qIstoricConectare.Close();
                //return;
            }

#endif

        //    qIstoricConectare.Close();

            ManageControls(!bLogin);
        }

        private void IncarcaServere()
        {
            m_cbServer.Items.Clear();
            string srv = AppConfig.Get("Server");
            if (srv != "") m_cbServer.Items.Add(srv);
            for (int i = 1; i <= 3; i++)
            {
                string s = AppConfig.Get("Server" + i);
                if (s != "" && !m_cbServer.Items.Contains(s))
                    m_cbServer.Items.Add(s);
            }
            m_cbServer.Text = srv != "" ? srv : "";
        }

        private static void SalveazaServer(string srv)
        {
            if (string.IsNullOrWhiteSpace(srv)) return;
            string prev = AppConfig.Get("Server");
            if (string.Equals(prev, srv, StringComparison.OrdinalIgnoreCase)) return;

            string s1 = AppConfig.Get("Server1");
            string s2 = AppConfig.Get("Server2");

            AppConfig.Set("Server", srv);
            if (prev != "") AppConfig.Set("Server1", prev);
            if (s1 != "" && !string.Equals(s1, srv, StringComparison.OrdinalIgnoreCase))
                AppConfig.Set("Server2", s1);
            // s2 se pierde (păstrăm max 3 intrări)
        }

        private void ManageControls(bool Enable)
        {
            foreach (Control cnt in this.Controls)
            {
                cnt.Enabled = Enable;
            }
        }

        private static bool ValidateConnection(string ConnectionString, out string error)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                return TestConnectionString(ConnectionString, out error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        public static bool TestConnectionString(string ConnectionString, out string error)
        {
            var validator = new ConnectValidator { ConnectionStringToValidate = ConnectionString };
            validator.TryToConnect();
            error = validator.LastError;
            return validator.State == ConnectValidator.ConnectionStateEnum.validated;
        }

        public class ConnectValidator
        {
            public string LastError = "";
            public enum ConnectionStateEnum
            {
                connecting,
                failed,
                validated
            }

            public string ConnectionStringToValidate = string.Empty;
            private ConnectionStateEnum state = ConnectionStateEnum.connecting;
            public ConnectionStateEnum State => state;

            public void TryToConnect()
            {
                using var cn = new Microsoft.Data.SqlClient.SqlConnection(ConnectionStringToValidate);
                try
                {
                    cn.Open();
                    state = ConnectionStateEnum.validated;
                }
                catch (Microsoft.Data.SqlClient.SqlException ex)
                {
                    LastError = $"SQL Error {ex.Number} (State {ex.State}): {ex.Message}";
                    state = ConnectionStateEnum.failed;
                }
                catch (Exception ex)
                {
                    LastError = $"{ex.GetType().Name}: {ex.Message}";
                    state = ConnectionStateEnum.failed;
                }
            }
        }



        public CManagerDeProiect GetManagerDeProiect()
        {
            return ManagerDeProiect;

        }

        private void m_timer_Tick(object sender, EventArgs e)
        {
            if (m_pBar.Value < m_pBar.Maximum && !stop)
            {
                m_pBar.Value += 1;
                Application.DoEvents();
            }
        }
    }
}

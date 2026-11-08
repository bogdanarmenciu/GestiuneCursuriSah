using Microsoft.Data.SqlClient;
using FormsTimer = System.Windows.Forms.Timer;

namespace GestiunePrezente.Common
{
    public class CManagerDeProiect
    {
        public enum ConnectionState
        {
            Connected,
            Disconnected
        }

        private ConnectionState state = ConnectionState.Disconnected;
        private SqlConnection connection = null!;

        // Timer clar, folosind alias
        private static readonly FormsTimer timer = new();
        private static readonly int TimpInactivitate;

        private int userId;
        private string userNumePrenume = string.Empty;

        private int societateId;
        private string societateNume = string.Empty;
        private string dbName = string.Empty;
        private string ipServer = string.Empty;

        private int diviziuneId = 1;
        private string codDiviziune = "SB";
        private string diviziuneNume = string.Empty;
        private string codSocietateSiDiviziune = string.Empty;

        private int administrator;
        private int directorGeneral;
        private int secretariat;

        private string server = string.Empty;
        private string database = string.Empty;
        private string user = string.Empty;
        private string password = string.Empty;

        public CManagerDeProiect()
        {
            timer.Tick += Timer_Tick;
        }

        public ConnectionState GetState() => state;

        public int GetUserId() => userId;
        public string GetUserNumePrenume() => userNumePrenume;

        public int GetSocietate() => societateId;
        public string GetSocietateNume() => societateNume;
        public string GetDBName() => dbName;
        public string GetIPServer() => ipServer;

        public int GetDiviziune() => diviziuneId;
        public string GetCodDiviziune() => codDiviziune;
        public string GetDiviziuneNume() => diviziuneNume;
        public string GetCodSocietateSiDiviziune() => codSocietateSiDiviziune;

        public int GetRolUser_Administrator() => administrator;
        public int GetRolUser_DirectorGeneral() => directorGeneral;
        public int GetRolUser_Secretariat() => secretariat;

        public string GetServer() => server;
        public string GetDatabase() => database;
        public string GetUser() => user;
        public string GetPassword() => password;

        public void OpenConnection(string connectionString, string username, string password, string srv, string db, string sMBSO, string sMBDV)
        {
            state = ConnectionState.Disconnected;
            connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                state = ConnectionState.Connected;
            }
            catch
            {
                state = ConnectionState.Disconnected;
                throw new InvalidOperationException("Nu s-a putut deschide conexiunea la baza de date.");
            }

            server = srv;
            database = db;
            user = username;
            this.password = password;

            var q = new CInterogareSQL(connection);

            string sq = "SELECT MBUT_Id,MBUT_NumePrenume FROM MB_Utilizatori WHERE MBUT_InUz=1 AND MBUT_Login='" +
                        CFormatareDate.StringToSqlString(username) + "' AND MBUT_Pass='" +
                        CFormatareDate.StringToSqlString(password) + "'";
            q.Open(sq);
            if (q.NextRow())
            {
                userId = q.GetIntValue(0);
                userNumePrenume = q.GetStringValue(1) ?? string.Empty;
            }
            q.Close();

            sq = string.Format(@"SELECT MBSO_Id,MBSO_Descriere,MBSO_DBName,MBSO_ServerIP FROM MB_Societate WHERE MBSO_Id = {0}", sMBSO);
            q.Open(sq);
            if (q.NextRow())
            {
                societateId = q.GetIntValue(0);
                societateNume = q.GetStringValue(1) ?? string.Empty;
                dbName = q.GetStringValue(2) ?? string.Empty;
                ipServer = q.GetStringValue(3) ?? string.Empty;
            }
            q.Close();

            sq = string.Format(@"SELECT MBDV_Id,MBDV_CodDiviziune,MBDV_Diviziune FROM MB_Diviziune WHERE MBDV_Id = {0} AND MBDV_MBSO_Id={1}", sMBDV, sMBSO);
            q.Open(sq);
            if (q.NextRow())
            {
                diviziuneId = q.GetIntValue(0);
                codDiviziune = q.GetStringValue(1) ?? string.Empty;
                diviziuneNume = q.GetStringValue(2) ?? string.Empty;

                // Substring modern
                codSocietateSiDiviziune = societateNume.Length >= 7
                    ? $"{societateNume[5..7].ToUpper()}{codDiviziune}"
                    : codDiviziune;
            }
            q.Close();

            SetUserRoles(sMBDV, userId);
        }

        private void SetUserRoles(string diviziuneId, int userId)
        {
            var q = new CInterogareSQL(connection);

            administrator = 0;
            directorGeneral = 0;
            secretariat = 0;

            string sq = string.Format(@"SELECT MBRU_FlagDirector, MBRU_FlagSecretariat, MBRU_Administrator
                                        FROM MB_Utilizatori 
                                        INNER JOIN MB_RoluriUtilizatori ON MBRU_MBUT_Id=MBUT_Id
                                        INNER JOIN MB_Diviziune ON MBRU_MBDV_Id=MBDV_Id 
                                        WHERE MBUT_InUz=1 
                                        AND MBRU_MBDV_Id={0} AND MBUT_Id={1}", diviziuneId, userId);

            q.Open(sq);
            if (q.NextRow())
            {
                directorGeneral = q.GetBoolValue(0) ? 1 : 0;
                secretariat = q.GetBoolValue(1) ? 1 : 0;
                administrator = q.GetBoolValue(2) ? 1 : 0;
            }
            q.Close();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            Application.Exit();
            System.Diagnostics.Process.Start(Application.ExecutablePath);
        }

        public static void StartTimer()
        {
            if (TimpInactivitate > 0)
            {
                timer.Interval = TimpInactivitate * 60000;
                timer.Start();
            }
        }

        public static void StopTimer() => timer.Stop();
        public static int GetTempoInattivita() => TimpInactivitate;

        public void CloseConnection()
        {
            if (state == ConnectionState.Connected)
            {
                connection.Close();
            }
            state = ConnectionState.Disconnected;
        }

        public SqlConnection GetConnection()
        {
            if (state == ConnectionState.Disconnected)
                throw new InvalidOperationException("Conexiunea nu este deschisă.");
            return connection;
        }

        public bool IsValid() => userId > 0;
    }
}

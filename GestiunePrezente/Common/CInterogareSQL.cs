using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace GestiunePrezente.Common
{
    public class CInterogareSQL(SqlConnection connection) : IDisposable
    {
        private readonly SqlConnection conn = connection ?? throw new ArgumentNullException(nameof(connection));
        private SqlCommand? cmd;
        private SqlDataReader? rd;
        private bool _hasRow;
        private SqlTransaction? _tx;

        // ============================================================
        //                       TRANZACTII
        // ============================================================
        public SqlTransaction BeginTransaction(IsolationLevel iso = IsolationLevel.ReadCommitted)
        {
            _tx = conn.BeginTransaction(iso);
            return _tx;
        }
        public void CommitTransaction() { _tx?.Commit(); _tx?.Dispose(); _tx = null; }
        public void RollbackTransaction() { try { _tx?.Rollback(); } catch { } _tx?.Dispose(); _tx = null; }

        // ============================================================
        //                  HELPER PARAMETRI
        // ============================================================
        public static SqlParameter P(string name, object? value)
            => new(name, value ?? DBNull.Value);

        private static void AddParams(SqlCommand c, SqlParameter[]? pars)
        { if (pars != null) foreach (var p in pars) c.Parameters.Add(p); }

        // ============================================================
        //               METODE CU PARAMETRI (RECOMANDATE)
        // ============================================================
        public int ExecuteNonQueryP(string sql, params SqlParameter[] pars)
        {
            using var c = new SqlCommand(sql, conn, _tx);
            AddParams(c, pars);
            try { return c.ExecuteNonQuery(); }
            catch (Exception e) { new FrmErr("Eroare SQL:", sql, e.Message, 2).Dispose(); return 0; }
        }

        // Like ExecuteNonQueryP but lets exceptions propagate to caller's try-catch.
        public int ExecuteNonQueryPThrow(string sql, params SqlParameter[] pars)
        {
            using var c = new SqlCommand(sql, conn, _tx);
            AddParams(c, pars);
            return c.ExecuteNonQuery();
        }

        // Like ExecuteToIntP but lets exceptions propagate to caller's try-catch.
        public int ExecuteToIntPThrow(string sql, params SqlParameter[] pars)
        {
            using var c = new SqlCommand(sql, conn, _tx);
            AddParams(c, pars);
            var r = c.ExecuteScalar();
            return r == null || r == DBNull.Value ? 0 : Convert.ToInt32(r);
        }

        public int ExecuteToIntP(string sql, params SqlParameter[] pars)
        {
            using var c = new SqlCommand(sql, conn, _tx);
            AddParams(c, pars);
            try
            {
                var r = c.ExecuteScalar();
                return r == null || r == DBNull.Value ? 0 : Convert.ToInt32(r);
            }
            catch (Exception e) { new FrmErr("Eroare SQL:", sql, e.Message, 2).Dispose(); return 0; }
        }

        public string? ExecuteToStringP(string sql, params SqlParameter[] pars)
        {
            using var c = new SqlCommand(sql, conn, _tx);
            AddParams(c, pars);
            try
            {
                var r = c.ExecuteScalar();
                return r == null || r == DBNull.Value ? null : r.ToString();
            }
            catch (Exception e) { new FrmErr("Eroare SQL:", sql, e.Message, 2).Dispose(); return null; }
        }

        public decimal ExecuteToDecimalP(string sql, params SqlParameter[] pars)
        {
            using var c = new SqlCommand(sql, conn, _tx);
            AddParams(c, pars);
            try
            {
                var r = c.ExecuteScalar();
                return r == null || r == DBNull.Value ? 0m : Convert.ToDecimal(r);
            }
            catch (Exception e) { new FrmErr("Eroare SQL:", sql, e.Message, 2).Dispose(); return 0m; }
        }

        public void OpenP(string sql, params SqlParameter[] pars)
        {
            _hasRow = false;
            CloseReader();
            try
            {
                cmd = new SqlCommand(sql, conn, _tx);
                AddParams(cmd, pars);
                rd = cmd.ExecuteReader();
            }
            catch (Exception e) { new FrmErr("Eroare SQL", sql, e.Message, 1).Dispose(); }
        }

        public DataTable LoadTableP(string sql, params SqlParameter[] pars)
        {
            using var c = new SqlCommand(sql, conn, _tx);
            AddParams(c, pars);
            using var da = new SqlDataAdapter(c);
            var dt = new DataTable();
            try { da.Fill(dt); }
            catch (Exception e) { new FrmErr("Eroare SQL:", sql, e.Message, 2).Dispose(); }
            return dt;
        }

        // ============================================================
        //          METODE LEGACY (FĂRĂ PARAMETRI) - corectate
        // ============================================================
        public string? ExecuteToString(string SQL)
        {
            using var c = new SqlCommand(SQL, conn, _tx);
            try
            {
                var r = c.ExecuteScalar();
                return r == null || r == DBNull.Value ? null : r.ToString();
            }
            catch (Exception e) { new FrmErr("Eroare de SQL:", SQL, e.Message, 2).Dispose(); return null; }
        }

        public int ExecuteToInt(string SQL)
        {
            using var c = new SqlCommand(SQL, conn, _tx);
            try
            {
                var r = c.ExecuteScalar();
                return r == null || r == DBNull.Value ? 0 : Convert.ToInt32(r);
            }
            catch (Exception e) { new FrmErr("Eroare de SQL:", SQL, e.Message, 2).Dispose(); return 0; }
        }

        public decimal ExecuteToDecimal(string SQL)
        {
            using var c = new SqlCommand(SQL, conn, _tx);
            try
            {
                var r = c.ExecuteScalar();
                return r == null || r == DBNull.Value ? 0m : Convert.ToDecimal(r);
            }
            catch (Exception e) { new FrmErr("Eroare de SQL:", SQL, e.Message, 2).Dispose(); return 0m; }
        }

        public void Open(string SQL)
        {
            _hasRow = false;
            CloseReader();
            try
            {
                cmd = new SqlCommand(SQL, conn, _tx);
                rd = cmd.ExecuteReader();
            }
            catch (Exception e) { new FrmErr("Eroare SQL", SQL, e.Message, 1).Dispose(); }
        }

        public void ExecuteStatement(string SQL)
        {
            using var c = new SqlCommand(SQL, conn, _tx);
            try { c.ExecuteNonQuery(); }
            catch (Exception e) { new FrmErr("Eroare SQL:", SQL, e.Message, 2).Dispose(); }
        }

        public static void ExecSql(string SQL, SqlConnection conn)
        {
            using var c = new SqlCommand(SQL, conn);
            try { c.ExecuteNonQuery(); }
            catch (Exception e) { new FrmErr("Eroare SQL:", SQL, e.Message, 2).Dispose(); }
        }

        // ============================================================
        //                  CITIRE RANDURI
        // ============================================================
        public bool NextRow()
        {
            try { _hasRow = rd?.Read() ?? false; return _hasRow; }
            catch (Exception e) { new FrmErr("Eroare SQL", "Eroare in recuperarea datelor", e.Message, 1).Dispose(); return false; }
        }

        public string? GetStringValue(int position)
        {
            ValidateRow();
            return rd!.IsDBNull(position) ? null : rd.GetString(position);
        }

        public int GetIntValue(int position)
        {
            ValidateRow();
            return rd!.IsDBNull(position) ? 0 : rd.GetInt32(position);
        }

        public DateTime? GetDateValue(int position)
        {
            ValidateRow();
            return rd!.IsDBNull(position) ? null : rd.GetDateTime(position);
        }

        public decimal? GetDecimalValue(int position)
        {
            ValidateRow();
            return rd!.IsDBNull(position) ? null : rd.GetDecimal(position);
        }

        public bool GetBoolValue(int position)
        {
            ValidateRow();
            return !rd!.IsDBNull(position) && rd.GetBoolean(position);
        }

        public string? GetTextValue(int position)
        {
            ValidateRow();
            if (rd!.IsDBNull(position)) return null;

            var sb = new StringBuilder();
            long startIndex = 0;
            const int bufferSize = 1024;
            char[] buffer = new char[bufferSize];
            long charsRead;
            while ((charsRead = rd.GetChars(position, startIndex, buffer, 0, bufferSize)) > 0)
            {
                sb.Append(buffer, 0, (int)charsRead);
                startIndex += charsRead;
            }
            return sb.ToString();
        }

        // ============================================================
        //                  INCHIDERE / DISPOSE
        // ============================================================
        private void CloseReader()
        {
            try { rd?.Close(); } catch { }
            rd?.Dispose(); rd = null;
            cmd?.Dispose(); cmd = null;
            _hasRow = false;
        }

        public void Close() => CloseReader();

        public void Dispose()
        {
            CloseReader();
            if (_tx != null) { try { _tx.Rollback(); } catch { } _tx.Dispose(); _tx = null; }
            GC.SuppressFinalize(this);
        }

        private void ValidateRow()
        {
            if (!_hasRow || rd == null)
                throw new InvalidOperationException("Nu există rând curent.");
        }
    }
}
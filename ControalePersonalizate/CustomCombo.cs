using System.Data;
using Microsoft.Data.SqlClient;

#pragma warning disable CA1416 // îmi afișează mereu că trebuie să fie windows 6.1 sau mai mare

namespace GridPersonalizat
{
    public class CustomCombo : ComboBox
    {
        static SqlConnection? connection;
        DataTable? dataTable;
        string? Sql;

        public delegate void SelChangeHandler(object sender, EventArgs e);
        public event SelChangeHandler? SelChange;
        bool bFireSelChange;

        //// Keeps track of the time of the last click
        //DateTime m_LastClick = DateTime.Now;

        public CustomCombo()
        {
            dataTable = new DataTable();
            bFireSelChange = true;
            SelectedIndexChanged += new EventHandler(CustomCombo_SelectedIndexChanged);
            Font = new Font("Microsoft Sans Serif", 8);
            AutoCompleteMode = AutoCompleteMode.Append;
            AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        void CustomCombo_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (!bFireSelChange)
            {
                SelectedIndex = -1;//BOGDAN
                return;
            }

            SelChange?.Invoke(sender ?? this, e);
        }

        public static void SetConnection(SqlConnection conn)
        {
            connection = conn;
        }

        public void CloseConnection()
        {
            connection = null;
            dataTable = null;
        }
        public void SetSql(string sSql)
        {
            Sql = sSql;

            SqlDataAdapter da = new(sSql, connection);
            dataTable = new DataTable();

            try
            {
                da.Fill(dataTable);
            }
            catch (SqlException)
            {
                MessageBox.Show("Eroare de sql in combo");
                return;
            }

            bFireSelChange = false;

            DataSource = dataTable;
            ValueMember = GetIdColumn();
            DisplayMember = GetColumnName();
            SelectedIndex = -1;

            bFireSelChange = true;
        }


        public int GetCurSelRow()
        {
            return SelectedIndex;
        }
        public object? GetColumnData(string sColumn, int Row)
        {
            if (dataTable == null || Row < 0 || Row >= dataTable.Rows.Count)
                return null;

            return dataTable.Rows[Row][sColumn];
        }

        public object? GetColumnData(string sColumn)
        {
            return GetColumnData(sColumn, GetCurSelRow());
        }

        public object SetColumnData(string sColumn, string Value)
        {
            if (dataTable == null)
            {
                SelectedIndex = -1;
                return false;
            }

            for (int i = 0; i < dataTable.Rows.Count; ++i)
            {
                var columnData = GetColumnData(sColumn, i);
                if (columnData != null && columnData.Equals((object)Value))
                {
                    SelectedIndex = i;
                    return true;
                }
            }
            SelectedIndex = -1;
            return false;
        }

        public int GetComboById(string IDColumn, int Row)
        {
            object? o = GetColumnData(IDColumn, Row);
            if (o != null)
                return (int)o;

            return -1;
        }

        public int GetComboById(string IDColumn)
        {
            return GetComboById(IDColumn, GetCurSelRow());
        }

        public bool SetComboById(string IDColumn, int Value)
        {
            if (dataTable == null)
            {
                SelectedIndex = -1;
                return false;
            }

            var col = dataTable.Columns[IDColumn];
            if (col == null)
            {
                SelectedIndex = -1;
                return false;
            }

            if (dataTable.PrimaryKey.Length == 0 || dataTable.PrimaryKey[0].ColumnName != IDColumn)
            {
                dataTable.PrimaryKey = [col];
            }

            var row = dataTable.Rows.Find(Value);
            if (row != null)
            {
                SelectedIndex = dataTable.Rows.IndexOf(row);
                return true;
            }

            SelectedIndex = -1;
            return false;
        }

        public string GetIdColumn()
        {
            if (dataTable == null)
                return "";

            for (int i = 0; i < dataTable.Columns.Count; ++i)
            {
                string columnName = dataTable.Columns[i].ColumnName;

                if (columnName.Length >= 3 && columnName[^3..] == "_Id" && columnName.Length == 7)
                    return columnName;
            }

            return "";
        }

        public string GetColumnName()
        {
            if (dataTable == null)
                return "";

            for (int i = 0; i < dataTable.Columns.Count; ++i)
            {
                string columnName = dataTable.Columns[i].ColumnName;

                if (columnName.Length < 3 || columnName[^3..] != "_Id" || columnName.Length != 7)
                    return columnName;
            }

            return "";
        }


    }
}

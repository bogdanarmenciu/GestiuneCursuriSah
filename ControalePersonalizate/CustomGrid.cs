using System.Data;
using Microsoft.Data.SqlClient;

#pragma warning disable CA1416 // îmi afișează mereu că trebuie să fie windows 6.1 sau mai mare

namespace GridPersonalizat
{
    public class CustomGrid : DataGridView
    {
        static SqlConnection? connection;
        readonly System.Data.DataTable m_DataTable;
        SqlDataAdapter? m_SqlDataAdapter;
        string? Sql;

        readonly DataSet m_DataSet;
        int m_lRowIndex = -1;

        public delegate void SelChangeHandler(object sender, EventArgs e);
        public event SelChangeHandler? SelChange;
        public bool bFireSelChange;

        public CustomGrid()
        {
            m_DataTable = new DataTable();
            m_DataSet = new DataSet();
            Font = new System.Drawing.Font("Arial", 8);
            bFireSelChange = true;
            this.DoubleBuffered = true;

            SelectionChanged += (sender, e) =>
            {
                if (!bFireSelChange) return;
                SelChange?.Invoke(sender ?? this, e);
            };
        }

        public static void SetConnection(SqlConnection conn)
        {
            connection = conn;
        }
        public void SetSql(string sSql, params SqlParameter[] parameters)
        {
            SetSql(sSql, "", "", parameters);
        }

        public void SetSql(string sSql, string sNume, string sPrimaryKey, params SqlParameter[] parameters)
        {
            Sql = sSql;

            m_DataTable.Clear();
            m_DataSet.Clear();

            using var cmd = new SqlCommand(sSql, connection);
            cmd.CommandTimeout = 0;

            if (parameters != null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters);
            }

            using (var reader = cmd.ExecuteReader())
            {
                m_DataTable.Load(reader);
            }

            cmd.Parameters.Clear();

            if (sNume.Length > 0)
            {
                m_DataTable.TableName = sNume;

                if (sPrimaryKey.Length > 0 && m_DataTable.Columns.Contains(sPrimaryKey))
                    m_DataTable.PrimaryKey = [m_DataTable.Columns[sPrimaryKey]!];

                m_DataSet.Tables.Add(m_DataTable);
            }

            bFireSelChange = false;
            DataSource = m_DataTable;
            bFireSelChange = true;

            m_lRowIndex = -1;
        }

        public int GetCurSelRow()
        {
            if (CurrentRow != null)
                return CurrentRow.Index;
            else
                return -1;
        }
        public object? GetColumnData(string sColumn, int Row)
        {
            if (Row < 0 || Row >= m_DataTable.Rows.Count)
                return null;

            return m_DataTable.Rows[Row][sColumn];
        }

        public object? GetColumnData(string sColumn)
        {
            return GetColumnData(sColumn, GetCurSelRow());
        }


        public string GetIdColumn()
        {
            for (int i = 0; i < m_DataTable.Columns.Count; ++i)
            {
                string columnName = m_DataTable.Columns[i].ColumnName;

                if (columnName[^3..] == "_Id" && columnName.Length == 7)
                    return columnName;
            }

            return "";
        }

        public string GetColumnName()
        {
            for (int i = 0; i < m_DataTable.Columns.Count; ++i)
            {
                string columnName = m_DataTable.Columns[i].ColumnName;

                if (columnName[^3..] != "_Id" || columnName.Length != 7)
                    return columnName;
            }

            return "";
        }

        public void SetColumnData(string sColumn, int Row, string sValue)
        {
            if (Row < 0 || Row >= m_DataTable.Rows.Count)
                return;

            m_DataTable.Rows[Row][sColumn] = sValue;
        }

        public void FormatColumn(string sHeaderText, int lWidth, bool? bVisible = null, DataGridViewContentAlignment? colAlign = null, String sCellStyle = "", Color? c = null)
        {
            m_lRowIndex++;
            if (m_lRowIndex < 0 || m_lRowIndex >= this.Columns.Count)
                return;

            var column = this.Columns[m_lRowIndex];
            column.HeaderText = sHeaderText;
            column.Width = lWidth;

            if (column.CellType?.ToString() == "System.Windows.Forms.DataGridViewCheckBoxCell")
                column.DefaultCellStyle.Alignment = colAlign ?? DataGridViewContentAlignment.MiddleCenter;
            else
                column.DefaultCellStyle.Alignment = colAlign ?? DataGridViewContentAlignment.MiddleLeft;

            column.Visible = bVisible ?? true;

            if (sCellStyle != "")
                column.DefaultCellStyle.Format = sCellStyle;

            column.DefaultCellStyle.BackColor = c ?? Color.White;
        }

        public new void Update()
        {
            if (m_DataTable.GetChanges() != null && m_SqlDataAdapter != null)
            {
                SqlCommandBuilder builder = new(m_SqlDataAdapter);
                try
                {
                    using (builder)
                    {
                        var changes = m_DataTable.GetChanges();
                        if (changes != null)
                        {
                            m_SqlDataAdapter.Update(changes);
                            m_DataTable.AcceptChanges();
                        }
                    }

                    MessageBox.Show("Au fost actualizate modificarile");
                }
                catch (InvalidOperationException)
                {
                    MessageBox.Show("Erroare de Update: Sql nu are primary key", "Update nereusit", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    m_DataTable.RejectChanges();
                }
            }

        }


        public static void ExportToExcel()
        {
            //// Creating a Excel object. 
            //Microsoft.Office.Interop.Excel._Application? excel = new Microsoft.Office.Interop.Excel.Application();
            //Microsoft.Office.Interop.Excel._Workbook workbook = excel.Workbooks.Add(Type.Missing);
            //try
            //{

            //    _Worksheet? worksheet = (_Worksheet?)workbook.ActiveSheet;
            //    if (worksheet != null)
            //    {
            //        worksheet.Name = "ExportedFromDatGrid";
            //    }
            //    else
            //    {
            //        throw new InvalidOperationException("ActiveSheet is null. Cannot set worksheet name.");
            //    }

            //    int cellRowIndex = 1;
            //    int cellColumnIndex = 1;

            //    //Loop through each row and read value from each column. 
            //    for (int i = 0; i < this.Rows.Count; i++)
            //    {
            //        for (int j = 0; j < this.Columns.Count; j++)
            //        {
            //            // Excel index starts from 1,1. As first Row would have the Column headers, adding a condition check. 
            //            if (cellRowIndex == 1)
            //            {
            //                worksheet.Cells[cellRowIndex, cellColumnIndex] = this.Columns[j].HeaderText;
            //            }
            //            else
            //            {
            //                // Fix: Check for null before calling ToString()
            //                object? cellValue = null;
            //                if (i - 1 >= 0 && i - 1 < this.Rows.Count)
            //                {
            //                    var cell = Rows[i - 1].Cells[j];
            //                    cellValue = cell?.Value;
            //                }
            //                worksheet.Cells[cellRowIndex, cellColumnIndex] = cellValue?.ToString() ?? string.Empty;
            //            }
            //            cellColumnIndex++;
            //        }
            //        cellColumnIndex = 1;
            //        cellRowIndex++;
            //    }

            //    //Getting the location and file name of the excel to save from user. 
            //    SaveFileDialog saveDialog = new()
            //    {
            //        Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
            //        FilterIndex = 2
            //    };

            //    if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //    {
            //        workbook.SaveAs(saveDialog.FileName);
            //        MessageBox.Show("Datele din grid au fost exportate cu succes!");
            //    }
            //}
            //catch (System.Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    excel.Quit();
            //}

        }
        private void copyAlltoClipboard()
        {
            this.SelectAll();
            DataObject? dataObj = GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private static void releaseObject(object? obj)
        {
            try
            {
                if (obj != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occurred while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}

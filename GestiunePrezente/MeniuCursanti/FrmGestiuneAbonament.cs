using GestiunePrezente.Common;
using GestiunePrezente.MeniuPrincipal;

namespace GestiunePrezente.MeniuCursanti
{
    public partial class FrmGestiuneAbonament : Form
    {
        readonly CManagerDeProiect ManagerDeProiect;
        readonly FrmMeniuPrincipal FrmParent;
        int m_lCUAB = 0;
        readonly int m_lMBME = 9;
        string? m_sSql;

        public const string MBME_NUME = "Gestiune Abonament";
        public static bool PoateDeschide(CManagerDeProiect mm)
            => DrepturiFormular.PoateDeschide(mm, MBME_NUME);

        public FrmGestiuneAbonament(FrmMeniuPrincipal FormParent, CManagerDeProiect MM, int lCUAB, int lMBME = 9)
        {
            FrmParent = FormParent;
            ManagerDeProiect = MM;
            m_lCUAB = lCUAB;
            m_lMBME = lMBME;

            InitializeComponent();
        }

        private void m_stbSalveaza_Click(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = !Salveaza();
        }

        private void FrmGestiuneAbonament_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_stbSalveaza.Enabled == true)
            {
                if (MessageBox.Show(CFormatareDate.GetString("IDS_SAVE_ASK"), CFormatareDate.GetString("IDS_ATENTIE"), MessageBoxButtons.YesNo) == DialogResult.No)
                    e.Cancel = false;
                else
                    if (!Salveaza())
                        e.Cancel = true;
            }
        }

        private bool Salveaza()
        {
            object? value;
            string sData, sSql, sUpdate = "", sTmp, sNume, sPerioadaZile, sDurata, sSedintaInd;

            sData = m_ebAbonament.Text;
            if (sData == "")
            {
                CFormatareDate.ShowWarning("IDS_ERR_NUMEABONAMENT");
                m_ebAbonament.Focus();
                return false;
            }
            else
            {
                sNume = string.Format(" '{0}' ", CFormatareDate.StringToSqlString(sData));
                sTmp = string.Format(" CUAB_Descriere = '{0}'", CFormatareDate.StringToSqlString(sData));
                sUpdate += sTmp;
            }

            sData = m_ebPerioadaZile.Text;
            if (sData == "")
            {
                CFormatareDate.ShowWarning("IDS_ERR_PRETSEDINTE");
                m_ebPerioadaZile.Focus();
                return false;
            }
            else
            {
                sPerioadaZile = string.Format(" {0} ", sData);
                sTmp = string.Format(" ,CUAB_Perioada = {0}", sData);
                sUpdate += sTmp;
            }

            sData = m_cbDurata.Text;
            if (sData == "")
            {
                CFormatareDate.ShowWarning("IDS_ERR_PRETSEDINTE");
                m_cbDurata.Focus();
                return false;
            }
            else
            {
                value = m_cbDurata.GetColumnData("MBTD_Id");
                sDurata = string.Format(" {0} ", value);
                sTmp = string.Format(",CUAB_MBTD_Id = {0} ", value);
                sUpdate += sTmp;
            }

            sSedintaInd = m_chkSedintaIndividuala.Checked ? "1" : "0";
            sTmp = string.Format(",CUAB_SedintaIndividuala = {0}", sSedintaInd);
            sUpdate += sTmp;

            string sOrePerLuna = "NULL";
            string sOreText = m_ebOrePerLuna.Text.Trim().Replace(',', '.');
            if (!string.IsNullOrEmpty(sOreText))
            {
                if (!decimal.TryParse(sOreText, System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture, out decimal oreVal) || oreVal <= 0)
                {
                    MessageBox.Show("Ore/luna trebuie sa fie un numar pozitiv sau gol (nelimitat).");
                    m_ebOrePerLuna.Focus();
                    return false;
                }
                sOrePerLuna = oreVal.ToString(System.Globalization.CultureInfo.InvariantCulture);
            }
            sUpdate += $",CUAB_OrePerLuna = {sOrePerLuna}";

            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            q.BeginTransaction(System.Data.IsolationLevel.Serializable);
            try
            {
                if (m_lCUAB == 0)
                {
                    sSql = string.Format(@"INSERT INTO CU_Abonamente (CUAB_MBTD_Id, CUAB_Descriere, CUAB_Perioada, CUAB_SedintaIndividuala, CUAB_OrePerLuna)
                                          VALUES ({0},{1},{2},{3},{4})", sDurata, sNume, sPerioadaZile, sSedintaInd, sOrePerLuna);
                    q.ExecuteStatement(sSql);
                    m_lCUAB = q.ExecuteToInt("SELECT SCOPE_IDENTITY()");
                }
                else
                {
                    sSql = string.Format(@"UPDATE CU_Abonamente SET {0} WHERE CUAB_Id = {1}", sUpdate, m_lCUAB);
                    q.ExecuteStatement(sSql);
                }

                if (!SalveazaPreturi(q))
                {
                    q.RollbackTransaction();
                    return false;
                }

                q.CommitTransaction();
            }
            catch (Exception ex)
            {
                q.RollbackTransaction();
                MessageBox.Show("Eroare la salvare: " + ex.Message);
                return false;
            }
            finally { q.Close(); }

            return true;
        }

        private void Modifica()
        {
            GridPersonalizat.CustomCombo.SetConnection(ManagerDeProiect.GetConnection());
            m_sSql = string.Format(@"SELECT MBTD_Id,MBTD_Durata FROM MB_TipDurata");
            m_cbDurata.SetSql(m_sSql);

            if (m_lCUAB > 0)
            {
                m_sSql = string.Format(@"SELECT CUAB_Descriere, CUAB_Perioada, CUAB_MBTD_Id,
                                                CAST(CUAB_SedintaIndividuala AS INT), CUAB_OrePerLuna
                                          FROM CU_Abonamente WHERE CUAB_Id = {0}", m_lCUAB);
                int i = -1;
                CInterogareSQL q = new(ManagerDeProiect.GetConnection());
                q.Open(m_sSql);
                if (q.NextRow())
                {
                    m_ebAbonament.Text    = q.GetStringValue(++i);
                    m_ebPerioadaZile.Text = q.GetDecimalValue(++i).ToString();
                    m_cbDurata.SetComboById("MBTD_Id", q.GetIntValue(++i));
                    m_chkSedintaIndividuala.Checked = q.GetIntValue(++i) == 1;
                    decimal? ore = q.GetDecimalValue(++i);
                    m_ebOrePerLuna.Text = ore.HasValue ? ore.Value.ToString("G") : "";
                }
                q.Close();
            }
            else
                m_cbDurata.SelectedIndex = 3;

            InitGridPreturi();
            IncarcaPreturi();

            m_stbSalveaza.Enabled = false;
        }

        private void InitGridPreturi()
        {
            m_dgvPreturi.Columns.Clear();

            var colId = new DataGridViewTextBoxColumn
            { Name = "colId", HeaderText = "Id", Visible = false, ReadOnly = true };

            var colStart = new DataGridViewTextBoxColumn
            { Name = "colStart", HeaderText = "De la (zz.ll.aaaa)", Width = 160 };

            var colSfarsit = new DataGridViewTextBoxColumn
            { Name = "colSfarsit", HeaderText = "Până la (gol = în prezent)", Width = 195 };

            var colPret = new DataGridViewTextBoxColumn
            { Name = "colPret", HeaderText = "Preț (lei)", Width = 110,
              DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight } };

            m_dgvPreturi.Columns.AddRange(colId, colStart, colSfarsit, colPret);
            m_dgvPreturi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void IncarcaPreturi()
        {
            m_dgvPreturi.Rows.Clear();
            if (m_lCUAB <= 0) return;

            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            q.Open(string.Format(@"
SELECT CUABP_Id, CUABP_DataStart, CUABP_DataSfarsit, CUABP_Pret
FROM CU_PreturiAbonamente
WHERE CUABP_CUAB_Id = {0}
ORDER BY CUABP_DataStart", m_lCUAB));

            while (q.NextRow())
            {
                var dtStart  = q.GetDateValue(1);
                var dtSfarsit = q.GetDateValue(2);
                m_dgvPreturi.Rows.Add(
                    q.GetIntValue(0),
                    dtStart.HasValue   ? dtStart.Value.ToString("dd.MM.yyyy")   : "",
                    dtSfarsit.HasValue ? dtSfarsit.Value.ToString("dd.MM.yyyy") : "",
                    q.GetDecimalValue(3)?.ToString("N2") ?? "");
            }
            q.Close();
        }

        private bool SalveazaPreturi(CInterogareSQL q)
        {
            if (m_lCUAB <= 0) return true;

            q.ExecuteStatement(string.Format(
                "DELETE FROM CU_PreturiAbonamente WHERE CUABP_CUAB_Id = {0}", m_lCUAB));

            foreach (DataGridViewRow row in m_dgvPreturi.Rows)
            {
                if (row.IsNewRow) continue;

                string sStart   = (row.Cells["colStart"].Value?.ToString()   ?? "").Trim();
                string sSfarsit = (row.Cells["colSfarsit"].Value?.ToString() ?? "").Trim();
                string sPret    = (row.Cells["colPret"].Value?.ToString()    ?? "").Trim();

                if (string.IsNullOrEmpty(sStart) || string.IsNullOrEmpty(sPret)) continue;

                if (!DateTime.TryParseExact(sStart, "dd.MM.yyyy",
                        System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.None, out DateTime dtStart))
                {
                    MessageBox.Show($"Data de start invalidă: '{sStart}'. Format corect: zz.ll.aaaa");
                    return false;
                }

                string sSqlSfarsit = "NULL";
                if (!string.IsNullOrEmpty(sSfarsit))
                {
                    if (!DateTime.TryParseExact(sSfarsit, "dd.MM.yyyy",
                            System.Globalization.CultureInfo.InvariantCulture,
                            System.Globalization.DateTimeStyles.None, out DateTime dtSfarsit))
                    {
                        MessageBox.Show($"Data de sfârșit invalidă: '{sSfarsit}'. Format corect: zz.ll.aaaa");
                        return false;
                    }
                    sSqlSfarsit = $"'{dtSfarsit:yyyy-MM-dd}'";
                }

                sPret = sPret.Replace(',', '.');
                if (!decimal.TryParse(sPret, System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture, out decimal pretVal) || pretVal <= 0)
                {
                    MessageBox.Show($"Prețul invalid: '{sPret}'.");
                    return false;
                }

                q.ExecuteStatement(string.Format(@"
INSERT INTO CU_PreturiAbonamente (CUABP_CUAB_Id, CUABP_DataStart, CUABP_DataSfarsit, CUABP_Pret)
VALUES ({0}, '{1:yyyy-MM-dd}', {2}, {3})",
                    m_lCUAB, dtStart, sSqlSfarsit, pretVal.ToString(System.Globalization.CultureInfo.InvariantCulture)));
            }
            return true;
        }

        private void m_dgvPreturi_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            m_stbSalveaza.Enabled = true;
        }

        private void m_dgvPreturi_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            m_stbSalveaza.Enabled = true;
        }

        private void FrmGestiuneAbonament_Load(object sender, EventArgs e)
        {
            Modifica();
        }

        private void m_ebAbonament_TextChanged(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = true;
        }

        private void m_ebNrSedinte_TextChanged(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = true;
        }

        private void m_ebDurataZile_TextChanged(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = true;
        }

        private void m_cbTip_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = true;
        }

        private void m_chkSedintaIndividuala_CheckedChanged(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = true;
        }

        private void m_ebOrePerLuna_TextChanged(object sender, EventArgs e)
        {
            m_stbSalveaza.Enabled = true;
        }
    }
}

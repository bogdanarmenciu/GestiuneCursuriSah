using GestiunePrezente.Common;

namespace GestiunePrezente.MeniuCursanti
{
    public partial class FrmSelectareContract : Form
    {
        readonly CManagerDeProiect ManagerDeProiect;
        public int SelectedCucoId { get; private set; }

        public FrmSelectareContract(CManagerDeProiect mm)
        {
            ManagerDeProiect = mm;
            InitializeComponent();
            GridPersonalizat.CustomGrid.SetConnection(ManagerDeProiect.GetConnection());

            m_gContracte.CellFormatting += m_gContracte_CellFormatting;
            m_gContracte.DoubleClick    += (s, e) => AcceptaSelectie();

            Cauta();
        }

        private void m_ebFiltru_TextChanged(object sender, EventArgs e) => Cauta();

        private void m_btnOk_Click(object sender, EventArgs e) => AcceptaSelectie();

        private void AcceptaSelectie()
        {
            object? val = m_gContracte.GetColumnData("CUCO_Id");
            if (val == null || val == DBNull.Value)
            {
                MessageBox.Show("Selectați un contract din listă.", "Atenție",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SelectedCucoId = Convert.ToInt32(val);
            DialogResult = DialogResult.OK;
        }

        private void Cauta()
        {
            string filtru = "";
            string text = m_ebFiltru.Text.Trim();
            if (text != "")
            {
                string safe = CFormatareDate.StringToSqlString(text);
                filtru = $@" AND (
                    CAST(C.CUCO_NrContract AS varchar(20)) LIKE N'{safe}%'
                    OR EXISTS (
                        SELECT 1 FROM CU_ContracteCursanti CC2
                        INNER JOIN CU_Cursanti CU2 ON CC2.CUCC_CUCU_Id = CU2.CUCU_Id
                        WHERE CC2.CUCC_CUCO_Id = C.CUCO_Id
                          AND ISNULL(CC2.CUCC_Retras, 0) = 0
                          AND (CU2.CUCU_Nume LIKE N'{safe}%' OR CU2.CUCU_Prenume LIKE N'{safe}%')
                    ))";
            }

            string sql = $@"
SELECT
    C.CUCO_Id,
    C.CUCO_NrContract               AS [Nr. contract],
    ISNULL(Ben.NumeComplet, '')      AS [Beneficiari],
    ISNULL(Ab.CUAB_Descriere, '')    AS [Abonament],
    Ab.CUAC_DataInceput              AS [Început abonament],
    Ab.CUAC_DataSfarsit              AS [Sfârșit abonament],
    CASE WHEN Restante.NrRestante = 0 THEN 'La zi'
         ELSE 'Restant: ' + CAST(Restante.NrRestante AS varchar(5))
    END                              AS [Situație]
FROM CU_Contracte C
OUTER APPLY (
    SELECT STUFF((
        SELECT ', ' + CU2.CUCU_Nume + ' ' + CU2.CUCU_Prenume
          FROM CU_ContracteCursanti CC2
         INNER JOIN CU_Cursanti CU2 ON CC2.CUCC_CUCU_Id = CU2.CUCU_Id
         WHERE CC2.CUCC_CUCO_Id = C.CUCO_Id
           AND ISNULL(CC2.CUCC_Retras, 0) = 0
           FOR XML PATH(''), TYPE
    ).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS NumeComplet
) AS Ben
OUTER APPLY (
    SELECT TOP 1 CUAB.CUAB_Descriere, CUAC.CUAC_DataInceput, CUAC.CUAC_DataSfarsit
      FROM CU_AbonamenteCursant CUAC
     INNER JOIN CU_Abonamente CUAB ON CUAC.CUAC_CUAB_Id = CUAB.CUAB_Id
     WHERE CUAC.CUAC_CUCO_Id = C.CUCO_Id
     ORDER BY
         CASE WHEN CUAC.CUAC_DataInceput <= GETDATE() AND CUAC.CUAC_DataSfarsit >= GETDATE()
              THEN 0 ELSE 1 END,
         CUAC.CUAC_DataInceput DESC
) AS Ab
OUTER APPLY (
    SELECT COUNT(*) AS NrRestante
      FROM FV_Facturi F2
     WHERE F2.FVFA_CUCO_Id = C.CUCO_Id
       AND F2.FVFA_ADRN_Id IS NULL
       AND ISNULL(F2.FVFA_Suma, 0) > 0
       AND ISNULL((SELECT SUM(I.CUIN_Valoare) FROM CU_Incasari I
                    WHERE I.CUIN_FVFA_Id = F2.FVFA_Id), 0) < ISNULL(F2.FVFA_Suma, 0)
) AS Restante
WHERE C.CUCO_Retras = 0
{filtru}
ORDER BY C.CUCO_NrContract";

            m_gContracte.SuspendLayout();
            m_gContracte.Visible = false;
            m_gContracte.SetSql(sql);
            m_gContracte.FormatColumn("CUCO_Id",           0, false);
            m_gContracte.FormatColumn("Nr. contract",      70, true, DataGridViewContentAlignment.MiddleRight);
            m_gContracte.FormatColumn("Beneficiari",       280);
            m_gContracte.FormatColumn("Abonament",         150);
            m_gContracte.FormatColumn("Început abonament", 110, true, null, "dd.MM.yyyy");
            m_gContracte.FormatColumn("Sfârșit abonament", 110, true, null, "dd.MM.yyyy");
            m_gContracte.FormatColumn("Situație",          110, true, DataGridViewContentAlignment.MiddleCenter);
            m_gContracte.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn c in m_gContracte.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
            m_gContracte.ResumeLayout();
            m_gContracte.Visible = true;
        }

        private void m_gContracte_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
            if (m_gContracte.Columns[e.ColumnIndex].HeaderText != "Situație") return;
            string val = e.Value as string ?? "";
            (e.CellStyle.BackColor, e.CellStyle.ForeColor) = val.StartsWith("Restant")
                ? (Color.FromArgb(255, 180, 180), Color.DarkRed)
                : (Color.FromArgb(200, 240, 200), Color.DarkGreen);
        }
    }
}

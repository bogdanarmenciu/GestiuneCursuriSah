using GestiunePrezente.Common;
using GestiunePrezente.MeniuPrincipal;

namespace GestiunePrezente.MeniuSituatii
{
    public partial class FrmSituatiePrezente : Form
    {
        readonly CManagerDeProiect ManagerDeProiect;
        readonly FrmMeniuPrincipal FrmParent;

        public FrmSituatiePrezente(FrmMeniuPrincipal formParent, CManagerDeProiect mm)
        {
            FrmParent = formParent;
            ManagerDeProiect = mm;
            InitializeComponent();
            GridPersonalizat.CustomGrid.SetConnection(ManagerDeProiect.GetConnection());

            int anCurent = DateTime.Today.Year;
            for (int an = anCurent; an >= anCurent - 5; an--)
                m_cbAn.Items.Add(an.ToString());
            m_cbAn.SelectedIndex = 0;

            m_cbLuna.Items.Add("Toate");
            for (int luna = 1; luna <= 12; luna++)
                m_cbLuna.Items.Add(luna.ToString("D2"));
            m_cbLuna.SelectedIndex = 0;

            m_gPrezente.CellFormatting += m_gPrezente_CellFormatting;
            Cauta();
        }

        private void m_tsbCautare_Click(object sender, EventArgs e) => Cauta();

        private void m_cbAn_SelectedIndexChanged(object sender, EventArgs e) => Cauta();

        private void m_cbLuna_SelectedIndexChanged(object sender, EventArgs e) => Cauta();

        private void m_ckActiv_CheckedChanged(object sender, EventArgs e) => Cauta();

        private void m_ckDoarDepasiri_CheckedChanged(object sender, EventArgs e) => Cauta();

        private void m_ebCautare_TextChanged(object sender, EventArgs e) => Cauta();

        private void Cauta()
        {
            string filtruAn = "";
            string filtruLuna = "";
            string filtruActivi = "";
            string filtruBeneficiar = "";
            string filtruDepasiri = "";

            if (m_cbAn.SelectedItem is string anStr && int.TryParse(anStr, out int an))
                filtruAn = $" AND YEAR(PD.CUPD_DataIntrare) = {an}";

            if (m_cbLuna.SelectedIndex > 0 && m_cbLuna.SelectedItem is string lunaStr
                && int.TryParse(lunaStr, out int luna))
                filtruLuna = $" AND MONTH(PD.CUPD_DataIntrare) = {luna}";

            if (m_ckActiv.Checked)
                filtruActivi = " AND ISNULL(C.CUCO_Retras, 0) = 0";

            string cautare = m_ebCautare.Text.Trim();
            if (cautare != "")
            {
                string safeStr = CFormatareDate.StringToSqlString(cautare);
                filtruBeneficiar = $@" AND (C.CUCO_NrContract LIKE '{safeStr}%'
                    OR EXISTS (
                        SELECT 1 FROM CU_ContracteCursanti CC2
                        INNER JOIN CU_Cursanti CU2 ON CC2.CUCC_CUCU_Id = CU2.CUCU_Id
                        WHERE CC2.CUCC_CUCO_Id = C.CUCO_Id
                          AND (CU2.CUCU_Nume LIKE N'{safeStr}%' OR CU2.CUCU_Prenume LIKE N'{safeStr}%')
                    ))";
            }

            if (m_ckDoarDepasiri.Checked)
                filtruDepasiri = "AB.CUAB_OrePerLuna IS NOT NULL AND SUM(DATEDIFF(minute, PD.CUPD_DataIntrare, ISNULL(PD.CUPD_DataIesire, PR.CUPR_PanaLaData))) / 60.0 > AB.CUAB_OrePerLuna AND ";

            string sSql = $@"
SELECT
    ROW_NUMBER() OVER (ORDER BY C.CUCO_NrContract, YEAR(PD.CUPD_DataIntrare), MONTH(PD.CUPD_DataIntrare)) AS [Nr. crt.],
    C.CUCO_NrContract AS [Nr. contract],
    (SELECT STUFF((
        SELECT ', ' + CU2.CUCU_Prenume + ' ' + CU2.CUCU_Nume
          FROM CU_ContracteCursanti CC2
         INNER JOIN CU_Cursanti CU2 ON CC2.CUCC_CUCU_Id = CU2.CUCU_Id
         WHERE CC2.CUCC_CUCO_Id = C.CUCO_Id
           FOR XML PATH(''), TYPE
    ).value('.','NVARCHAR(MAX)'), 1, 2, '')) AS [Beneficiari],
    AB.CUAB_Descriere AS [Tip abonament],
    CONVERT(VARCHAR(10), AC.CUAC_DataInceput, 104) + ' - ' +
        ISNULL(CONVERT(VARCHAR(10), AC.CUAC_DataSfarsit, 104), '...') AS [Perioada],
    RIGHT('0' + CAST(MONTH(PD.CUPD_DataIntrare) AS VARCHAR(2)), 2) + '.' +
        CAST(YEAR(PD.CUPD_DataIntrare) AS CHAR(4)) AS [Luna],
    COUNT(PD.CUPD_Id) AS [Nr. prezente],
    AB.CUAB_OrePerLuna AS [Ore/luna],
    CAST(SUM(DATEDIFF(minute, PD.CUPD_DataIntrare, ISNULL(PD.CUPD_DataIesire, PR.CUPR_PanaLaData))) / 60.0
         AS DECIMAL(5,2)) AS [Ore reale],
    CASE WHEN SUM(DATEDIFF(minute, PD.CUPD_DataIntrare, ISNULL(PD.CUPD_DataIesire, PR.CUPR_PanaLaData))) / 60.0
              > AB.CUAB_OrePerLuna
         THEN CAST(SUM(DATEDIFF(minute, PD.CUPD_DataIntrare, ISNULL(PD.CUPD_DataIesire, PR.CUPR_PanaLaData))) / 60.0
                   - AB.CUAB_OrePerLuna AS DECIMAL(5,2))
         ELSE NULL END AS [Depasire],
    CASE WHEN SUM(DATEDIFF(minute, PD.CUPD_DataIntrare, ISNULL(PD.CUPD_DataIesire, PR.CUPR_PanaLaData))) / 60.0 > 0
              AND ISNULL(AB.CUAB_Perioada, 0) > 0
              AND ISNULL(AC.CUAC_Pret, 0) > 0
         THEN CAST(AC.CUAC_Pret / AB.CUAB_Perioada
                   / (SUM(DATEDIFF(minute, PD.CUPD_DataIntrare, ISNULL(PD.CUPD_DataIesire, PR.CUPR_PanaLaData))) / 60.0)
                   AS DECIMAL(8,2))
         ELSE NULL END AS [Cost/ora]
  FROM CU_AbonamenteCursant AC
 INNER JOIN CU_Abonamente AB ON AC.CUAC_CUAB_Id = AB.CUAB_Id
 INNER JOIN CU_Contracte C ON AC.CUAC_CUCO_Id = C.CUCO_Id
 INNER JOIN CU_PrezenteDetaliu PD
         ON PD.CUPD_CUCO_Id = C.CUCO_Id
        AND PD.CUPD_DataIntrare >= AC.CUAC_DataInceput
        AND PD.CUPD_DataIntrare < DATEADD(day, 1, ISNULL(AC.CUAC_DataSfarsit, CAST(GETDATE() AS DATE)))
 INNER JOIN CU_Prezente PR ON PD.CUPD_CUPR_Id = PR.CUPR_Id
 WHERE PD.CUPD_Prezent = 1
   {filtruAn}
   {filtruLuna}
   {filtruActivi}
   {filtruBeneficiar}
 GROUP BY
     C.CUCO_Id, C.CUCO_NrContract,
     AB.CUAB_Descriere, AB.CUAB_OrePerLuna, AB.CUAB_Perioada,
     AC.CUAC_DataInceput, AC.CUAC_DataSfarsit, AC.CUAC_Pret,
     YEAR(PD.CUPD_DataIntrare), MONTH(PD.CUPD_DataIntrare)
 HAVING {filtruDepasiri}1=1
 ORDER BY C.CUCO_NrContract, YEAR(PD.CUPD_DataIntrare), MONTH(PD.CUPD_DataIntrare)";

            m_gPrezente.SuspendLayout();
            m_gPrezente.Visible = false;

            m_gPrezente.SetSql(sSql);

            m_gPrezente.FormatColumn("Nr. crt.", 50, true, DataGridViewContentAlignment.MiddleRight);
            m_gPrezente.FormatColumn("Nr. contract", 80, true, DataGridViewContentAlignment.MiddleRight);
            m_gPrezente.FormatColumn("Beneficiari", 250);
            m_gPrezente.FormatColumn("Tip abonament", 150);
            m_gPrezente.FormatColumn("Perioada", 160);
            m_gPrezente.FormatColumn("Luna", 70, true, DataGridViewContentAlignment.MiddleCenter);
            m_gPrezente.FormatColumn("Nr. prezente", 75, true, DataGridViewContentAlignment.MiddleRight);
            m_gPrezente.FormatColumn("Ore/luna", 70, true, DataGridViewContentAlignment.MiddleRight);
            m_gPrezente.FormatColumn("Ore reale", 80, true, DataGridViewContentAlignment.MiddleRight);
            m_gPrezente.FormatColumn("Depasire", 80, true, DataGridViewContentAlignment.MiddleRight);
            m_gPrezente.FormatColumn("Cost/ora", 80, true, DataGridViewContentAlignment.MiddleRight);
            m_gPrezente.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (DataGridViewColumn column in m_gPrezente.Columns)
                m_gPrezente.Columns[column.Name]!.SortMode = DataGridViewColumnSortMode.NotSortable;

            m_gPrezente.ResumeLayout();
            m_gPrezente.Visible = true;
        }

        private void m_gPrezente_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
            if (m_gPrezente.Columns[e.ColumnIndex].HeaderText != "Depasire") return;
            if (e.Value == null || e.Value == DBNull.Value) return;
            if (Convert.ToDecimal(e.Value) > 0)
            {
                e.CellStyle.BackColor = Color.FromArgb(255, 180, 180);
                e.CellStyle.ForeColor = Color.DarkRed;
            }
        }
    }
}

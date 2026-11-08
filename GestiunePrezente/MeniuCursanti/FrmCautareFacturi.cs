using GestiunePrezente.Common;
using GestiunePrezente.MeniuPrincipal;

namespace GestiunePrezente.MeniuCursanti
{
    public partial class FrmCautareFacturi : Form
    {
        readonly CManagerDeProiect ManagerDeProiect;
        readonly FrmMeniuPrincipal FrmParent;

        public FrmCautareFacturi(FrmMeniuPrincipal formParent, CManagerDeProiect mm)
        {
            FrmParent = formParent;
            ManagerDeProiect = mm;
            InitializeComponent();
            GridPersonalizat.CustomGrid.SetConnection(ManagerDeProiect.GetConnection());

            int anCurent = DateTime.Today.Year;
            m_cbAn.Items.Add("Toți");
            for (int an = anCurent; an >= anCurent - 5; an--)
                m_cbAn.Items.Add(an.ToString());
            m_cbAn.SelectedIndex = 1; // anul curent

            m_cbLuna.Items.Add("Toate");
            for (int luna = 1; luna <= 12; luna++)
                m_cbLuna.Items.Add(luna.ToString("D2"));
            m_cbLuna.SelectedIndex = 0;

            m_cbStatus.Items.AddRange(new object[]
            {
                "Toate", "Neîncasate", "Încasate", "Sponsorizări"
            });
            m_cbStatus.SelectedIndex = 0;

            m_gFacturi.CellFormatting += m_gFacturi_CellFormatting;

            Cauta();
        }

        private void m_tsbCautare_Click(object sender, EventArgs e) => Cauta();
        private void m_cbAn_SelectedIndexChanged(object sender, EventArgs e) => Cauta();
        private void m_cbLuna_SelectedIndexChanged(object sender, EventArgs e) => Cauta();
        private void m_cbStatus_SelectedIndexChanged(object sender, EventArgs e) => Cauta();
        private void m_ebCautare_TextChanged(object sender, EventArgs e) => Cauta();
        private void m_tsbNou_Click(object sender, EventArgs e)
        {
            if (!FrmGestiuneFactura.PoateDeschide(ManagerDeProiect)) return;
            using var frm = new FrmGestiuneFactura(ManagerDeProiect, 0);
            if (frm.ShowDialog(this) == DialogResult.OK)
                Cauta();
        }

        private void m_tsbModifica_Click(object sender, EventArgs e) => Modifica();
        private void m_gFacturi_DoubleClick(object sender, EventArgs e) => Modifica();

        private void Cauta()
        {
            string filtruAn = "";
            string filtruLuna = "";
            string filtruStatus = "";
            string filtruText = "";

            if (m_cbAn.SelectedIndex > 0 && m_cbAn.SelectedItem is string anStr
                && int.TryParse(anStr, out int an))
            {
                filtruAn = $" AND YEAR(F.FVFA_PerioadaInceput) = {an}";
            }

            if (m_cbLuna.SelectedIndex > 0 && m_cbLuna.SelectedItem is string lunaStr
                && int.TryParse(lunaStr, out int luna))
            {
                filtruLuna = $" AND MONTH(F.FVFA_PerioadaInceput) = {luna}";
            }

            filtruStatus = m_cbStatus.SelectedIndex switch
            {
                1 => " AND F.FVFA_ADRN_Id IS NULL AND Inc.NrIncasari = 0",
                2 => " AND F.FVFA_ADRN_Id IS NULL AND Inc.NrIncasari > 0",
                3 => " AND F.FVFA_ADRN_Id IS NOT NULL",
                _ => ""
            };

            string cautare = m_ebCautare.Text.Trim();
            if (cautare != "")
            {
                string safe = CFormatareDate.StringToSqlString(cautare);
                filtruText = $@" AND (
                    F.FVFA_NrFactura LIKE N'{safe}%'
                    OR CAST(C.CUCO_NrContract AS varchar(20)) LIKE N'{safe}%'
                    OR EXISTS (
                        SELECT 1 FROM CU_ContracteCursanti CC2
                        INNER JOIN CU_Cursanti CU2 ON CC2.CUCC_CUCU_Id = CU2.CUCU_Id
                        WHERE CC2.CUCC_CUCO_Id = C.CUCO_Id
                          AND ISNULL(CC2.CUCC_Retras, 0) = 0
                          AND (CU2.CUCU_Nume LIKE N'{safe}%' OR CU2.CUCU_Prenume LIKE N'{safe}%')
                    ))";
            }

            string sSql = $@"
SELECT
    F.FVFA_Id,
    F.FVFA_CUCO_Id,
    ROW_NUMBER() OVER (ORDER BY F.FVFA_PerioadaInceput DESC, F.FVFA_NrFactura) AS [Nr. crt.],
    ISNULL(F.FVFA_NrFactura, '')                                               AS [Nr. factură],
    CAST(ISNULL(C.CUCO_NrContract, 0) AS bigint)                               AS [Nr. contract],
    ISNULL(Ben.NumeComplet, '')                                                AS [Beneficiari],
    CONVERT(varchar(10), F.FVFA_PerioadaInceput, 104) + ' — ' +
        CONVERT(varchar(10), F.FVFA_PerioadaSfarsit, 104)                     AS [Perioadă],
    CAST(ISNULL(F.FVFA_Suma, 0) AS decimal(18,2))                             AS [Sumă (lei)],
    CAST(Inc.TotalIncasat AS decimal(18,2))                                    AS [Încasat (lei)],
    CASE
        WHEN F.FVFA_ADRN_Id IS NOT NULL                              THEN 'Sponsorizare'
        WHEN Inc.NrIncasari = 0                                      THEN 'Neîncasată'
        WHEN Inc.TotalIncasat < ISNULL(F.FVFA_Suma, 0)              THEN 'Parțial'
        WHEN Inc.UltimTipPlata = 1                                   THEN 'Cash'
        WHEN Inc.UltimTipPlata = 2                                   THEN 'OP'
        ELSE 'Încasată'
    END                                                                        AS [Status]
FROM FV_Facturi F
INNER JOIN CU_Contracte C ON F.FVFA_CUCO_Id = C.CUCO_Id
OUTER APPLY (
    SELECT STUFF((
        SELECT ', ' + CU2.CUCU_Prenume + ' ' + CU2.CUCU_Nume
          FROM CU_ContracteCursanti CC2
         INNER JOIN CU_Cursanti CU2 ON CC2.CUCC_CUCU_Id = CU2.CUCU_Id
         WHERE CC2.CUCC_CUCO_Id = C.CUCO_Id
           AND ISNULL(CC2.CUCC_Retras, 0) = 0
           FOR XML PATH(''), TYPE
    ).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS NumeComplet
) AS Ben
OUTER APPLY (
    SELECT ISNULL(SUM(I.CUIN_Valoare), 0) AS TotalIncasat,
           COUNT(*)                        AS NrIncasari,
           MAX(I.CUIN_TipPlata)            AS UltimTipPlata
      FROM CU_Incasari I
     WHERE I.CUIN_FVFA_Id = F.FVFA_Id
) AS Inc
WHERE 1=1
{filtruAn}
{filtruLuna}
{filtruStatus}
{filtruText}
ORDER BY F.FVFA_PerioadaInceput DESC, F.FVFA_NrFactura";

            m_gFacturi.SuspendLayout();
            m_gFacturi.Visible = false;

            m_gFacturi.SetSql(sSql);

            m_gFacturi.FormatColumn("FVFA_Id",      0,   false);
            m_gFacturi.FormatColumn("FVFA_CUCO_Id", 0,   false);
            m_gFacturi.FormatColumn("Nr. crt.",     50,  true, DataGridViewContentAlignment.MiddleRight);
            m_gFacturi.FormatColumn("Nr. factură",  110);
            m_gFacturi.FormatColumn("Nr. contract", 90,  true, DataGridViewContentAlignment.MiddleRight);
            m_gFacturi.FormatColumn("Beneficiari",  260);
            m_gFacturi.FormatColumn("Perioadă",     160);
            m_gFacturi.FormatColumn("Sumă (lei)",   100, true, DataGridViewContentAlignment.MiddleRight);
            m_gFacturi.FormatColumn("Încasat (lei)", 100, true, DataGridViewContentAlignment.MiddleRight);
            m_gFacturi.FormatColumn("Status",       110, true, DataGridViewContentAlignment.MiddleCenter);
            m_gFacturi.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (DataGridViewColumn c in m_gFacturi.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;

            m_gFacturi.ResumeLayout();
            m_gFacturi.Visible = true;
        }

        private void Modifica()
        {
            object? idVal   = m_gFacturi.GetColumnData("FVFA_Id");
            object? cucoVal = m_gFacturi.GetColumnData("FVFA_CUCO_Id");
            if (idVal == null || idVal == DBNull.Value) return;
            if (cucoVal == null || cucoVal == DBNull.Value) return;

            if (!FrmGestiuneFactura.PoateDeschide(ManagerDeProiect)) return;

            using var frm = new FrmGestiuneFactura(
                ManagerDeProiect,
                Convert.ToInt32(cucoVal),
                Convert.ToInt32(idVal));

            if (frm.ShowDialog(this) == DialogResult.OK)
                Cauta();
        }

        private void m_tsbSterge_Click(object sender, EventArgs e)
        {
            object? idVal = m_gFacturi.GetColumnData("FVFA_Id");
            if (idVal == null || idVal == DBNull.Value) return;
            int fvfaId = Convert.ToInt32(idVal);

            // Bloc ștergere dacă există încasări
            var qCk = new CInterogareSQL(ManagerDeProiect.GetConnection());
            int nrIncasari = qCk.ExecuteToInt(
                $"SELECT COUNT(*) FROM CU_Incasari WHERE CUIN_FVFA_Id = {fvfaId}");
            qCk.Close();

            if (nrIncasari > 0)
            {
                MessageBox.Show(
                    $"Factura nu poate fi ștearsă deoarece are {nrIncasari} încasare(i) înregistrată(e).\nȘtergeți mai întâi încasările din modulul de încasări.",
                    "Ștergere imposibilă", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Ștergeți factura selectată?", "Atenție",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            q.ExecuteNonQueryP("DELETE FROM FV_Facturi WHERE FVFA_Id = @id",
                CInterogareSQL.P("@id", fvfaId));
            q.Close();
            Cauta();
        }

        private void m_gFacturi_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
            if (m_gFacturi.Columns[e.ColumnIndex].HeaderText != "Status") return;
            string val = e.Value as string ?? "";
            (e.CellStyle.BackColor, e.CellStyle.ForeColor) = val switch
            {
                "Neîncasată"   => (Color.FromArgb(255, 180, 180), Color.DarkRed),
                "Parțial"      => (Color.FromArgb(255, 230, 150), Color.FromArgb(140, 80, 0)),
                "OP" or "Cash" => (Color.FromArgb(200, 240, 200), Color.DarkGreen),
                "Încasată"     => (Color.FromArgb(200, 240, 200), Color.DarkGreen),
                "Sponsorizare" => (Color.FromArgb(210, 225, 255), Color.Navy),
                _ => (e.CellStyle.BackColor, e.CellStyle.ForeColor)
            };
        }
    }
}

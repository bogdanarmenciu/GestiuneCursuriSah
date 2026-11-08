using GestiunePrezente.Common;
using GestiunePrezente.MeniuPrincipal;

namespace GestiunePrezente.MeniuCursanti
{
    public partial class FrmCautareIncasariAbonamente : Form
    {
        readonly CManagerDeProiect ManagerDeProiect;
        readonly FrmMeniuPrincipal FrmParent;

        public FrmCautareIncasariAbonamente(FrmMeniuPrincipal formParent, CManagerDeProiect mm)
        {
            FrmParent = formParent;
            ManagerDeProiect = mm;
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            GridPersonalizat.CustomGrid.SetConnection(ManagerDeProiect.GetConnection());
            m_gIncasari.ReadOnly = true;
            m_gIncasari.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void FrmCautareIncasariAbonamente_Load(object sender, EventArgs e)
        {
            m_dtpDeLaData.Value  = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            m_dtpPanaLaData.Value = DateTime.Today;
            Cauta();
        }

        private void m_tsbCautare_Click(object sender, EventArgs e) => Cauta();

        private void m_tsbNou_Click(object sender, EventArgs e)
        {
            if (!FrmGestiuneIncasariAbonament.PoateDeschide(ManagerDeProiect)) return;
            var frm = new FrmGestiuneIncasariAbonament(FrmParent, ManagerDeProiect, 0)
            { MdiParent = FrmParent };
            frm.FormClosed += (s, ev) => Cauta();
            frm.Show();
        }

        private void m_tsbSterge_Click(object sender, EventArgs e)
        {
            object? val = m_gIncasari.GetColumnData("CUIN_Id");
            if (val == null || val == DBNull.Value) return;
            int id = Convert.ToInt32(val);
            if (MessageBox.Show("Ștergeți această încasare?", "Atenție",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            // Citim CUIN_FVFA_Id înainte de ștergere, pentru a reseta statusul facturii
            var qRead = new CInterogareSQL(ManagerDeProiect.GetConnection());
            qRead.OpenP("SELECT ISNULL(CUIN_FVFA_Id, 0) FROM CU_Incasari WHERE CUIN_Id = @id",
                CInterogareSQL.P("@id", id));
            int fvfaId = qRead.NextRow() ? qRead.GetIntValue(0) : 0;
            qRead.Close();

            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            q.ExecuteNonQueryP("DELETE FROM CU_Incasari_Cursanti WHERE CUIC_CUIN_Id = @id",
                CInterogareSQL.P("@id", id));
            q.ExecuteNonQueryP("DELETE FROM CU_Incasari WHERE CUIN_Id = @id",
                CInterogareSQL.P("@id", id));
            q.Close();

            // Dacă mai există alte încasări pentru aceeași factură, nu resetăm statusul
            if (fvfaId > 0)
            {
                var qFv = new CInterogareSQL(ManagerDeProiect.GetConnection());
                int ramasIncasari = qFv.ExecuteToInt(
                    $"SELECT COUNT(*) FROM CU_Incasari WHERE CUIN_FVFA_Id = {fvfaId}");
                qFv.Close();

                if (ramasIncasari == 0)
                {
                    var qReset = new CInterogareSQL(ManagerDeProiect.GetConnection());
                    qReset.ExecuteNonQueryP(@"
UPDATE FV_Facturi SET
    FVFA_TipIncasare  = 0,
    FVFA_DataIncasare = NULL,
    FVFA_NrDocument   = NULL
WHERE FVFA_Id = @id",
                        CInterogareSQL.P("@id", fvfaId));
                    qReset.Close();
                }
            }

            Cauta();
        }

        private void m_gIncasari_DoubleClick(object sender, EventArgs e)
        {
            object? val = m_gIncasari.GetColumnData("CUIN_Id");
            if (val == null || val == DBNull.Value) return;
            if (!FrmGestiuneIncasariAbonament.PoateDeschide(ManagerDeProiect)) return;
            var frm = new FrmGestiuneIncasariAbonament(FrmParent, ManagerDeProiect, Convert.ToInt32(val))
            { MdiParent = FrmParent };
            frm.FormClosed += (s, ev) => Cauta();
            frm.Show();
        }

        private void Cauta()
        {
            Cursor.Current = Cursors.WaitCursor;

            string filtru = $" AND I.CUIN_MBDV_Id = {ManagerDeProiect.GetDiviziune()} ";

            if (m_ckFiltruData.Checked)
            {
                filtru += $" AND CAST(I.CUIN_Data AS DATE) >= {CFormatareDate.DateToSQLString(m_dtpDeLaData.Value)} ";
                filtru += $" AND CAST(I.CUIN_Data AS DATE) <= {CFormatareDate.DateToSQLString(m_dtpPanaLaData.Value)} ";
            }

            string textBenef = m_ebBeneficiar.Text.Trim();
            if (textBenef != "")
            {
                string safe = CFormatareDate.StringToSqlString(textBenef);
                filtru += $@" AND EXISTS (
                    SELECT 1 FROM CU_ContracteCursanti CC2
                    INNER JOIN CU_Cursanti CU2 ON CC2.CUCC_CUCU_Id = CU2.CUCU_Id
                    WHERE ISNULL(CC2.CUCC_Retras, 0) = 0
                      AND (CU2.CUCU_Nume LIKE N'{safe}%' OR CU2.CUCU_Prenume LIKE N'{safe}%')
                      AND (
                        (I.CUIN_FVFA_Id IS NOT NULL AND CC2.CUCC_CUCO_Id =
                            (SELECT TOP 1 FVFA_CUCO_Id FROM FV_Facturi WHERE FVFA_Id = I.CUIN_FVFA_Id))
                        OR
                        (I.CUIN_ADRN_Id IS NOT NULL AND CC2.CUCC_CUCO_Id =
                            (SELECT TOP 1 ADRN_CUCO_Id FROM AD_RegistruNumereInterne WHERE ADRN_Id = I.CUIN_ADRN_Id))
                      )) ";
            }

            if (m_cbTipPlata.SelectedIndex == 1) filtru += " AND I.CUIN_TipPlata = 1 ";
            if (m_cbTipPlata.SelectedIndex == 2) filtru += " AND I.CUIN_TipPlata = 2 ";
            if (m_cbTipLegatura.SelectedIndex == 1) filtru += " AND I.CUIN_FVFA_Id IS NOT NULL ";
            if (m_cbTipLegatura.SelectedIndex == 2) filtru += " AND I.CUIN_ADRN_Id IS NOT NULL ";
            if (m_cbTipLegatura.SelectedIndex == 3) filtru += " AND I.CUIN_FVFA_Id IS NULL AND I.CUIN_ADRN_Id IS NULL ";

            string sql = $@"
SELECT ROW_NUMBER() OVER (ORDER BY I.CUIN_Data DESC)                   AS [Nr. crt],
       I.CUIN_Id,
       ISNULL(I.CUIN_Numar, 0)                                           AS [Nr. încasare],
       CONVERT(varchar(16), I.CUIN_Data, 120)                           AS [Data],
       CASE I.CUIN_TipPlata WHEN 1 THEN 'Cash' WHEN 2 THEN 'OP' WHEN 3 THEN 'Sponsorizare' ELSE '—' END AS [Tip plată],
       ISNULL(I.CUIN_NrDocument, '')                                    AS [Nr. document],
       ISNULL(I.CUIN_Valoare, 0)                                        AS [Sumă (lei)],
       ISNULL(I.CUIN_Observatii, '')                                     AS [Reprezentând],
       CASE
           WHEN I.CUIN_FVFA_Id IS NOT NULL THEN
               ISNULL((SELECT TOP 1 CU2.CUCU_Prenume + ' ' + CU2.CUCU_Nume
                         FROM CU_ContracteCursanti CC2
                        INNER JOIN CU_Cursanti CU2 ON CC2.CUCC_CUCU_Id = CU2.CUCU_Id
                        WHERE CC2.CUCC_CUCO_Id = F.FVFA_CUCO_Id
                          AND ISNULL(CC2.CUCC_Retras, 0) = 0
                        ORDER BY CU2.CUCU_Nume), '—')
           WHEN I.CUIN_ADRN_Id IS NOT NULL THEN
               ISNULL((SELECT TOP 1 CU2.CUCU_Prenume + ' ' + CU2.CUCU_Nume
                         FROM CU_ContracteCursanti CC2
                        INNER JOIN CU_Cursanti CU2 ON CC2.CUCC_CUCU_Id = CU2.CUCU_Id
                        WHERE CC2.CUCC_CUCO_Id = A.ADRN_CUCO_Id
                          AND ISNULL(CC2.CUCC_Retras, 0) = 0
                        ORDER BY CU2.CUCU_Nume), '—')
           ELSE '—'
       END                                                              AS [Beneficiar],
       CASE
           WHEN I.CUIN_FVFA_Id IS NOT NULL THEN
               'Factură ' + ISNULL(F.FVFA_NrFactura, '(fără nr.)')
           WHEN I.CUIN_ADRN_Id IS NOT NULL THEN
               'Sponsorizare nr. ' + CAST(A.ADRN_Numar AS varchar(10))
           ELSE '—'
       END                                                              AS [Legătură]
  FROM CU_Incasari I
  LEFT JOIN FV_Facturi F ON I.CUIN_FVFA_Id = F.FVFA_Id
  LEFT JOIN AD_RegistruNumereInterne A ON I.CUIN_ADRN_Id = A.ADRN_Id
 WHERE 1=1 {filtru}
 ORDER BY I.CUIN_Data DESC";

            m_gIncasari.SetSql(sql);
            m_gIncasari.FormatColumn("Nr. crt",      50,   true, DataGridViewContentAlignment.MiddleRight);
            m_gIncasari.FormatColumn("CUIN_Id",      0,    false);
            m_gIncasari.FormatColumn("Nr. încasare", 90,   true, DataGridViewContentAlignment.MiddleRight);
            m_gIncasari.FormatColumn("Data",         140);
            m_gIncasari.FormatColumn("Tip plată",    75,   true, DataGridViewContentAlignment.MiddleCenter);
            m_gIncasari.FormatColumn("Nr. document", 110);
            m_gIncasari.FormatColumn("Sumă (lei)",   100,  true, DataGridViewContentAlignment.MiddleRight);
            m_gIncasari.FormatColumn("Reprezentând", 280);
            m_gIncasari.FormatColumn("Beneficiar",   200);
            m_gIncasari.FormatColumn("Legătură",     200);
            foreach (DataGridViewColumn c in m_gIncasari.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;

            decimal total = 0;
            foreach (DataGridViewRow row in m_gIncasari.Rows)
            {
                object? v = m_gIncasari.GetColumnData("Sumă (lei)", m_gIncasari.Rows.IndexOf(row));
                if (v != null && v != DBNull.Value) total += Convert.ToDecimal(v);
            }
            m_ebTotalIncasat.Text = total.ToString("F2") + " lei";

            Cursor.Current = Cursors.Default;
        }

        private void m_ebBeneficiar_TextChanged(object sender, EventArgs e)    => Cauta();
        private void m_cbTipPlata_SelectedIndexChanged(object sender, EventArgs e)    => Cauta();
        private void m_cbTipLegatura_SelectedIndexChanged(object sender, EventArgs e) => Cauta();
        private void m_ckFiltruData_CheckedChanged(object sender, EventArgs e)        => Cauta();
    }
}

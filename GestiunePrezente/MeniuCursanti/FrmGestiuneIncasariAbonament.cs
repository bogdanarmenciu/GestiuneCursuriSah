using GestiunePrezente.Common;
using GestiunePrezente.MeniuPrincipal;
using System.Collections.Generic;
using System.Globalization;

namespace GestiunePrezente.MeniuCursanti
{
    public partial class FrmGestiuneIncasariAbonament : Form
    {
        readonly CManagerDeProiect ManagerDeProiect;
        readonly FrmMeniuPrincipal FrmParent;
        private int m_lCUIN = 0;
        private int m_lFVFA = 0;
        private int m_lADRN = 0;
        private bool m_bFormaDeschisa = false;

        public const string MBME_NUME = "Gestiune Incasari Abonament";
        public static bool PoateDeschide(CManagerDeProiect mm)
            => DrepturiFormular.PoateDeschide(mm, MBME_NUME);

        public FrmGestiuneIncasariAbonament(FrmMeniuPrincipal formParent, CManagerDeProiect mm,
            int cuinId, int fvfaId = 0, int adrnId = 0)
        {
            FrmParent = formParent;
            ManagerDeProiect = mm;
            m_lCUIN = cuinId;
            m_lFVFA = fvfaId;
            m_lADRN = adrnId;
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            GridPersonalizat.CustomGrid.SetConnection(ManagerDeProiect.GetConnection());
            GridPersonalizat.CustomCombo.SetConnection(ManagerDeProiect.GetConnection());
        }

        private void FrmGestiuneIncasariAbonament_Load(object sender, EventArgs e)
        {
            IncarcaComboFacturi();
            IncarcaComboADRN();
            IncarcaGridCursanti();
            IncarcaDate();
            m_bFormaDeschisa = true;
        }

        private void IncarcaComboFacturi()
        {
            m_cbFactura.SetSql(@"
SELECT F.FVFA_Id,
       ISNULL(F.FVFA_NrFactura, '(fără nr.)') + '  /  ' +
       ISNULL(CONVERT(varchar(10), F.FVFA_DataEmitere, 104), '') + '  /  ' +
       CAST(ISNULL(F.FVFA_Suma, 0) AS varchar(15)) + ' lei  —  ' +
       ISNULL(STUFF((
           SELECT ', ' + CU2.CUCU_Prenume + ' ' + CU2.CUCU_Nume
             FROM CU_ContracteCursanti CC2
            INNER JOIN CU_Cursanti CU2 ON CC2.CUCC_CUCU_Id = CU2.CUCU_Id
            WHERE CC2.CUCC_CUCO_Id = F.FVFA_CUCO_Id AND ISNULL(CC2.CUCC_Retras, 0) = 0
              FOR XML PATH(''), TYPE
       ).value('.', 'NVARCHAR(MAX)'), 1, 2, ''), '') AS Display
  FROM FV_Facturi F
 ORDER BY F.FVFA_DataEmitere DESC, F.FVFA_Id DESC");
        }

        private void IncarcaGridCursanti()
        {
            m_dgCursanti.Rows.Clear();
            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            q.Open(@"SELECT CU.CUCU_Id,
                            CU.CUCU_Prenume + ' ' + CU.CUCU_Nume
                       FROM CU_Cursanti CU
                      WHERE NOT EXISTS (
                          SELECT 1 FROM CU_ContracteCursanti CC
                           WHERE CC.CUCC_CUCU_Id = CU.CUCU_Id
                      )
                      ORDER BY CU.CUCU_Nume, CU.CUCU_Prenume");
            while (q.NextRow())
                m_dgCursanti.Rows.Add(false, q.GetIntValue(0), q.GetStringValue(1));
            q.Close();
        }

        private void m_dgCursanti_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (m_dgCursanti.IsCurrentCellDirty && m_dgCursanti.CurrentCell is DataGridViewCheckBoxCell)
                m_dgCursanti.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void m_dgCursanti_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 0 || e.RowIndex < 0 || !m_bFormaDeschisa) return;
            ActualizeazaReprezentandDinGrid();
            SetDirty();
        }

        private void ActualizeazaReprezentandDinGrid()
        {
            var names = new List<string>();
            foreach (DataGridViewRow row in m_dgCursanti.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Cells[0].Value is bool ck && ck)
                    names.Add(row.Cells[2].Value?.ToString() ?? "");
            }
            m_ebReprezentand.Text = names.Count > 0
                ? "Plată abonament - " + string.Join(", ", names)
                : "Plată abonament";
        }

        private List<int> GetCheckedCucuIds()
        {
            var ids = new List<int>();
            foreach (DataGridViewRow row in m_dgCursanti.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Cells[0].Value is bool ck && ck && row.Cells[1].Value != null)
                    ids.Add(Convert.ToInt32(row.Cells[1].Value));
            }
            return ids;
        }

        private void IncarcaComboADRN()
        {
            m_cbADRN.SetSql(@"
SELECT A.ADRN_Id,
       CAST(A.ADRN_Numar AS varchar(20)) + ' / ' +
       CONVERT(varchar(10), A.ADRN_Data, 104) +
       CASE WHEN ISNULL(A.ADRN_Descriere,'') <> '' THEN ' - ' + A.ADRN_Descriere ELSE '' END +
       '  [' + CAST(ISNULL(A.ADRN_ValoareSponsorizare, 0) AS varchar(15)) + ' lei]' AS Display
  FROM AD_RegistruNumereInterne A
 WHERE A.ADRN_ValoareSponsorizare IS NOT NULL
 ORDER BY A.ADRN_Data DESC, A.ADRN_Numar DESC");
        }

        private void IncarcaDate()
        {
            m_tsbSalveaza.Enabled = false;

            if (m_lCUIN > 0)
            {
                var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
                q.OpenP(@"SELECT CUIN_Numar, CUIN_Data, CAST(ISNULL(CUIN_TipPlata, 1) AS int),
                                 CUIN_NrDocument, CUIN_Valoare, CUIN_Observatii,
                                 ISNULL(CUIN_FVFA_Id, 0), ISNULL(CUIN_ADRN_Id, 0),
                                 ISNULL(CUIN_CUCU_Id, 0)
                            FROM CU_Incasari WHERE CUIN_Id = @id",
                    CInterogareSQL.P("@id", m_lCUIN));
                if (q.NextRow())
                {
                    m_ebNrIncasare.Text    = q.GetIntValue(0).ToString();
                    m_dtpData.Value        = q.GetDateValue(1) ?? DateTime.Now;
                    int tipPlata           = q.GetIntValue(2);
                    m_rbCash.Checked       = tipPlata == 1;
                    m_rbOP.Checked         = tipPlata == 2;
                    m_ebNrDocument.Text    = q.GetStringValue(3) ?? "";
                    m_ebSuma.Text          = (q.GetDecimalValue(4) ?? 0).ToString("F2");
                    m_ebReprezentand.Text  = q.GetStringValue(5) ?? "";
                    int fvfaId             = q.GetIntValue(6);
                    int adrnId             = q.GetIntValue(7);
                    int cucuId             = q.GetIntValue(8);

                    if (fvfaId > 0)
                    {
                        m_rbLegaturaFactura.Checked = true;
                        m_cbFactura.SetComboById("FVFA_Id", fvfaId);
                        ActualizeazaInfoFactura(fvfaId);
                    }
                    else if (adrnId > 0)
                    {
                        m_rbLegaturaSponsorizare.Checked = true;
                        m_cbADRN.SetComboById("ADRN_Id", adrnId);
                        ActualizeazaInfoADRN(adrnId);
                    }
                    else
                    {
                        // Verifică tabela de legătură (multi-cursant) + fallback la CUIN_CUCU_Id vechi
                        var linkedIds = new HashSet<int>();
                        var qLink = new CInterogareSQL(ManagerDeProiect.GetConnection());
                        qLink.Open($"SELECT CUIC_CUCU_Id FROM CU_Incasari_Cursanti WHERE CUIC_CUIN_Id = {m_lCUIN}");
                        while (qLink.NextRow())
                            linkedIds.Add(qLink.GetIntValue(0));
                        qLink.Close();
                        if (linkedIds.Count == 0 && cucuId > 0)
                            linkedIds.Add(cucuId);

                        if (linkedIds.Count > 0)
                        {
                            m_rbLegaturaDirecta.Checked = true;
                            foreach (DataGridViewRow row in m_dgCursanti.Rows)
                            {
                                if (row.IsNewRow) continue;
                                int rowId = row.Cells[1].Value != null ? Convert.ToInt32(row.Cells[1].Value) : 0;
                                row.Cells[0].Value = linkedIds.Contains(rowId);
                            }
                            ActualizeazaReprezentandDinGrid();
                        }
                        else
                        {
                            m_rbFaraLegatura.Checked = true;
                        }
                    }
                }
                q.Close();
            }
            else
            {
                m_dtpData.Value  = DateTime.Now;
                m_rbCash.Checked = true;

                var qNr = new CInterogareSQL(ManagerDeProiect.GetConnection());
                int nextNr = qNr.ExecuteToInt(
                    $"SELECT ISNULL(MAX(CUIN_Numar), 0) + 1 FROM CU_Incasari WHERE CUIN_MBDV_Id = {ManagerDeProiect.GetDiviziune()}");
                qNr.Close();
                m_ebNrIncasare.Text = nextNr.ToString();

                if (m_lFVFA > 0)
                {
                    m_rbLegaturaFactura.Checked = true;
                    m_cbFactura.SetComboById("FVFA_Id", m_lFVFA);
                    ActualizeazaInfoFactura(m_lFVFA);
                }
                else if (m_lADRN > 0)
                {
                    m_rbLegaturaSponsorizare.Checked = true;
                    m_cbADRN.SetComboById("ADRN_Id", m_lADRN);
                    ActualizeazaInfoADRN(m_lADRN);
                }
            }

            ActualizeazaVizibilitateLegatura();
            IncarcaIstoric();
        }

        private void ActualizeazaVizibilitateLegatura()
        {
            m_cbFactura.Visible    = m_rbLegaturaFactura.Checked;
            m_cbADRN.Visible       = m_rbLegaturaSponsorizare.Checked;
            m_dgCursanti.Visible   = m_rbLegaturaDirecta.Checked;
            if (m_rbFaraLegatura.Checked || m_rbLegaturaDirecta.Checked)
                m_lblInfoLegatura.Text = "";
        }

        private void ActualizeazaInfoFactura(int fvfaId)
        {
            if (fvfaId <= 0) { m_lblInfoLegatura.Text = ""; return; }
            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            q.OpenP(@"SELECT F.FVFA_Suma, F.FVFA_NrFactura,
                             ISNULL((SELECT SUM(I.CUIN_Valoare)
                                       FROM CU_Incasari I
                                      WHERE I.CUIN_FVFA_Id = @id AND I.CUIN_Id <> @cuin), 0)
                        FROM FV_Facturi F WHERE F.FVFA_Id = @id",
                CInterogareSQL.P("@id",   fvfaId),
                CInterogareSQL.P("@cuin", m_lCUIN));
            if (q.NextRow())
            {
                decimal total   = q.GetDecimalValue(0) ?? 0;
                string nrFact   = q.GetStringValue(1) ?? "?";
                decimal incasat = q.GetDecimalValue(2) ?? 0;
                decimal rest    = total - incasat;
                m_lblInfoLegatura.Text = $"Factură nr. {nrFact}  |  Total: {total:F2} lei  |  Încasat: {incasat:F2} lei  |  Rest: {rest:F2} lei";
                m_lblInfoLegatura.ForeColor = rest > 0 ? Color.Navy : Color.DarkGreen;

                if (m_lCUIN == 0)
                {
                    m_ebSuma.Text = (rest > 0 ? rest : total).ToString("F2");
                    m_ebReprezentand.Text = $"Contravaloare factură nr. {nrFact}";
                }
            }
            q.Close();
        }

        private void ActualizeazaInfoADRN(int adrnId)
        {
            if (adrnId <= 0) { m_lblInfoLegatura.Text = ""; return; }
            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            q.OpenP(@"SELECT A.ADRN_ValoareSponsorizare, A.ADRN_Numar,
                             ISNULL((SELECT SUM(I.CUIN_Valoare)
                                       FROM CU_Incasari I
                                      WHERE I.CUIN_ADRN_Id = @id AND I.CUIN_Id <> @cuin), 0)
                        FROM AD_RegistruNumereInterne A WHERE A.ADRN_Id = @id",
                CInterogareSQL.P("@id",   adrnId),
                CInterogareSQL.P("@cuin", m_lCUIN));
            if (q.NextRow())
            {
                decimal valoare    = q.GetDecimalValue(0) ?? 0;
                int nrAdrn         = q.GetIntValue(1);
                decimal incasat    = q.GetDecimalValue(2) ?? 0;
                decimal disponibil = valoare - incasat;
                m_lblInfoLegatura.Text = $"Contract sponsorizare nr. {nrAdrn}  |  Valoare: {valoare:F2} lei  |  Încasat: {incasat:F2} lei  |  Disponibil: {disponibil:F2} lei";
                m_lblInfoLegatura.ForeColor = disponibil >= 0 ? Color.Navy : Color.DarkRed;

                if (m_lCUIN == 0 && valoare > 0)
                {
                    m_ebSuma.Text = (disponibil > 0 ? disponibil : 0m).ToString("F2");
                    m_ebReprezentand.Text = $"Încasare contract sponsorizare nr. {nrAdrn}";
                }
            }
            q.Close();
        }

        private void IncarcaIstoric()
        {
            int fvfaId = 0, adrnId = 0;

            if (m_lCUIN > 0)
            {
                var qL = new CInterogareSQL(ManagerDeProiect.GetConnection());
                qL.OpenP("SELECT ISNULL(CUIN_FVFA_Id,0), ISNULL(CUIN_ADRN_Id,0) FROM CU_Incasari WHERE CUIN_Id = @id",
                    CInterogareSQL.P("@id", m_lCUIN));
                if (qL.NextRow()) { fvfaId = qL.GetIntValue(0); adrnId = qL.GetIntValue(1); }
                qL.Close();
            }
            else
            {
                if (m_rbLegaturaFactura.Checked)
                    fvfaId = GetSelectedId(m_cbFactura, "FVFA_Id");
                if (m_rbLegaturaSponsorizare.Checked)
                    adrnId = GetSelectedId(m_cbADRN, "ADRN_Id");
            }

            if (fvfaId <= 0 && adrnId <= 0)
            {
                m_gIstoric.SetSql("SELECT 1 AS [—] WHERE 1=0");
                return;
            }

            string cond = fvfaId > 0 ? $"I.CUIN_FVFA_Id = {fvfaId}" : $"I.CUIN_ADRN_Id = {adrnId}";
            string sql = $@"
SELECT I.CUIN_Id,
       CAST(ISNULL(I.CUIN_Numar, 0) AS bigint)                        AS [Nr.],
       CONVERT(varchar(16), I.CUIN_Data, 120)                         AS [Data],
       CASE I.CUIN_TipPlata WHEN 1 THEN 'Cash' WHEN 2 THEN 'OP' WHEN 3 THEN 'Sponsorizare' ELSE '—' END AS [Tip],
       ISNULL(I.CUIN_NrDocument, '')                                  AS [Nr. doc.],
       CAST(ISNULL(I.CUIN_Valoare, 0) AS decimal(18,2))               AS [Sumă (lei)],
       ISNULL(I.CUIN_Observatii, '')                                  AS [Reprezentând]
  FROM CU_Incasari I WHERE {cond}
 ORDER BY I.CUIN_Data";

            m_gIstoric.SetSql(sql);
            m_gIstoric.FormatColumn("CUIN_Id",    0,   false);
            m_gIstoric.FormatColumn("Nr.",        50,  true, DataGridViewContentAlignment.MiddleRight);
            m_gIstoric.FormatColumn("Data",       130);
            m_gIstoric.FormatColumn("Tip",        60,  true, DataGridViewContentAlignment.MiddleCenter);
            m_gIstoric.FormatColumn("Nr. doc.",   100);
            m_gIstoric.FormatColumn("Sumă (lei)", 100, true, DataGridViewContentAlignment.MiddleRight);
            m_gIstoric.FormatColumn("Reprezentând", 400);
            foreach (DataGridViewColumn c in m_gIstoric.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private int GetSelectedId(GridPersonalizat.CustomCombo cb, string colName)
        {
            object? val = cb.GetColumnData(colName);
            return val == null || val == DBNull.Value ? 0 : Convert.ToInt32(val);
        }

        private void m_rbFaraLegatura_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_bFormaDeschisa || !m_rbFaraLegatura.Checked) return;
            ActualizeazaVizibilitateLegatura();
            IncarcaIstoric();
            SetDirty();
        }

        private void m_rbLegaturaFactura_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_bFormaDeschisa || !m_rbLegaturaFactura.Checked) return;
            ActualizeazaVizibilitateLegatura();
            IncarcaIstoric();
            SetDirty();
        }

        private void m_rbLegaturaSponsorizare_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_bFormaDeschisa || !m_rbLegaturaSponsorizare.Checked) return;
            ActualizeazaVizibilitateLegatura();
            IncarcaIstoric();
            SetDirty();
        }

        private void m_rbLegaturaDirecta_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_bFormaDeschisa || !m_rbLegaturaDirecta.Checked) return;
            ActualizeazaVizibilitateLegatura();
            IncarcaIstoric();
            if (GetCheckedCucuIds().Count == 0)
                m_ebReprezentand.Text = "Plată abonament";
            SetDirty();
        }

        private void m_cbFactura_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_bFormaDeschisa) return;
            ActualizeazaInfoFactura(GetSelectedId(m_cbFactura, "FVFA_Id"));
            IncarcaIstoric();
            SetDirty();
        }

        private void m_cbADRN_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_bFormaDeschisa) return;
            ActualizeazaInfoADRN(GetSelectedId(m_cbADRN, "ADRN_Id"));
            IncarcaIstoric();
            SetDirty();
        }

        private void m_ebSuma_TextChanged(object sender, EventArgs e)
        {
            if (m_bFormaDeschisa) SetDirty();
        }

        private void m_ebReprezentand_TextChanged(object sender, EventArgs e)
        {
            if (m_bFormaDeschisa) SetDirty();
        }

        private void SetDirty() => m_tsbSalveaza.Enabled = true;

        private void m_tsbSalveaza_Click(object sender, EventArgs e)
        {
            if (Salveaza()) m_tsbSalveaza.Enabled = false;
        }

        private bool Salveaza()
        {
            if (!decimal.TryParse(m_ebSuma.Text.Trim().Replace(',', '.'),
                NumberStyles.Any, CultureInfo.InvariantCulture, out decimal suma) || suma <= 0)
            {
                MessageBox.Show("Introduceți o sumă validă și pozitivă.", "Atenție",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                m_ebSuma.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(m_ebReprezentand.Text))
            {
                MessageBox.Show("Câmpul Reprezentând este obligatoriu.", "Atenție",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                m_ebReprezentand.Focus();
                return false;
            }

            byte tipPlata   = m_rbOP.Checked ? (byte)2 : (byte)1;
            string? nrDoc   = !string.IsNullOrWhiteSpace(m_ebNrDocument.Text) ? m_ebNrDocument.Text.Trim() : null;

            int fvfaId = 0, adrnId = 0;
            if (m_rbLegaturaFactura.Checked)      fvfaId = GetSelectedId(m_cbFactura, "FVFA_Id");
            if (m_rbLegaturaSponsorizare.Checked)  adrnId = GetSelectedId(m_cbADRN,   "ADRN_Id");
            var checkedCucuIds = m_rbLegaturaDirecta.Checked ? GetCheckedCucuIds() : new List<int>();
            int cucuId = checkedCucuIds.Count > 0 ? checkedCucuIds[0] : 0;

            if (m_rbLegaturaFactura.Checked && fvfaId == 0)
            {
                MessageBox.Show("Selectați factura.", "Atenție", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (m_rbLegaturaSponsorizare.Checked && adrnId == 0)
            {
                MessageBox.Show("Selectați contractul de sponsorizare.", "Atenție",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (m_rbLegaturaDirecta.Checked && GetCheckedCucuIds().Count == 0)
            {
                MessageBox.Show("Bifați cel puțin un cursant.", "Atenție",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            if (m_lCUIN == 0)
            {
                m_lCUIN = q.ExecuteToIntPThrow(@"
INSERT INTO CU_Incasari
    (CUIN_MBSO_Id, CUIN_MBDV_Id, CUIN_Numar, CUIN_Data, CUIN_TipPlata,
     CUIN_NrDocument, CUIN_Valoare, CUIN_Observatii,
     CUIN_DataSalvarii, CUIN_MBUT_Id, CUIN_FVFA_Id, CUIN_ADRN_Id, CUIN_CUCU_Id)
OUTPUT INSERTED.CUIN_Id
VALUES
    (@so, @dv, @nr, @data, @tip, @nrd, @suma, @rep, GETDATE(), @u, @fvfa, @adrn, @cucu)",
                    CInterogareSQL.P("@so",   ManagerDeProiect.GetSocietate()),
                    CInterogareSQL.P("@dv",   ManagerDeProiect.GetDiviziune()),
                    CInterogareSQL.P("@nr",   int.TryParse(m_ebNrIncasare.Text, out int nr) ? (object)nr : DBNull.Value),
                    CInterogareSQL.P("@data", m_dtpData.Value),
                    CInterogareSQL.P("@tip",  tipPlata),
                    CInterogareSQL.P("@nrd",  nrDoc != null ? (object)nrDoc : DBNull.Value),
                    CInterogareSQL.P("@suma", suma),
                    CInterogareSQL.P("@rep",  m_ebReprezentand.Text.Trim()),
                    CInterogareSQL.P("@u",    ManagerDeProiect.GetUserId()),
                    CInterogareSQL.P("@fvfa", fvfaId > 0 ? (object)fvfaId : DBNull.Value),
                    CInterogareSQL.P("@adrn", adrnId > 0 ? (object)adrnId : DBNull.Value),
                    CInterogareSQL.P("@cucu", cucuId > 0 ? (object)cucuId : DBNull.Value));
            }
            else
            {
                q.ExecuteNonQueryP(@"
UPDATE CU_Incasari SET
    CUIN_Data = @data, CUIN_TipPlata = @tip, CUIN_NrDocument = @nrd,
    CUIN_Valoare = @suma, CUIN_Observatii = @rep,
    CUIN_DataSalvarii = GETDATE(), CUIN_MBUT_Id = @u,
    CUIN_FVFA_Id = @fvfa, CUIN_ADRN_Id = @adrn, CUIN_CUCU_Id = @cucu
WHERE CUIN_Id = @id",
                    CInterogareSQL.P("@data", m_dtpData.Value),
                    CInterogareSQL.P("@tip",  tipPlata),
                    CInterogareSQL.P("@nrd",  nrDoc != null ? (object)nrDoc : DBNull.Value),
                    CInterogareSQL.P("@suma", suma),
                    CInterogareSQL.P("@rep",  m_ebReprezentand.Text.Trim()),
                    CInterogareSQL.P("@u",    ManagerDeProiect.GetUserId()),
                    CInterogareSQL.P("@fvfa", fvfaId > 0 ? (object)fvfaId : DBNull.Value),
                    CInterogareSQL.P("@adrn", adrnId > 0 ? (object)adrnId : DBNull.Value),
                    CInterogareSQL.P("@cucu", cucuId > 0 ? (object)cucuId : DBNull.Value),
                    CInterogareSQL.P("@id",   m_lCUIN));
            }
            q.Close();

            // Actualizează legăturile cursant ↔ încasare
            var qLink = new CInterogareSQL(ManagerDeProiect.GetConnection());
            qLink.ExecuteNonQueryP("DELETE FROM CU_Incasari_Cursanti WHERE CUIC_CUIN_Id = @id",
                CInterogareSQL.P("@id", m_lCUIN));
            foreach (int id in checkedCucuIds)
            {
                qLink.ExecuteNonQueryP(
                    "INSERT INTO CU_Incasari_Cursanti (CUIC_CUIN_Id, CUIC_CUCU_Id) VALUES (@cuin, @cucu)",
                    CInterogareSQL.P("@cuin", m_lCUIN),
                    CInterogareSQL.P("@cucu", id));
            }
            qLink.Close();

            // Actualizează statusul facturii dacă încasarea e legată de o factură
            if (fvfaId > 0)
            {
                var qFv = new CInterogareSQL(ManagerDeProiect.GetConnection());
                qFv.ExecuteNonQueryP(@"
UPDATE FV_Facturi SET
    FVFA_TipIncasare  = @tip,
    FVFA_DataIncasare = @data,
    FVFA_NrDocument   = @nrd
WHERE FVFA_Id = @id",
                    CInterogareSQL.P("@tip",  tipPlata),
                    CInterogareSQL.P("@data", m_dtpData.Value),
                    CInterogareSQL.P("@nrd",  nrDoc != null ? (object)nrDoc : DBNull.Value),
                    CInterogareSQL.P("@id",   fvfaId));
                qFv.Close();
            }

            IncarcaIstoric();
            return true;
        }

        private void FrmGestiuneIncasariAbonament_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!m_tsbSalveaza.Enabled) return;
            var r = MessageBox.Show("Există modificări nesalvate. Salvați?", "Atenție",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (r == DialogResult.Yes && !Salveaza())
                e.Cancel = true;
            else if (r == DialogResult.Cancel)
                e.Cancel = true;
        }

        private void m_tsbListare_Click(object sender, EventArgs e)
        {
            // TODO: listare chitanță
        }
    }
}

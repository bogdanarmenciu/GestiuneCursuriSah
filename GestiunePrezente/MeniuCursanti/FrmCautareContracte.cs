using DocumentFormat.OpenXml.Drawing.Diagrams;
using GestiunePrezente.Common;
using GestiunePrezente.MeniuPrincipal;

namespace GestiunePrezente.MeniuCursanti
{
    public partial class FrmCautareContracte : Form
    {
        readonly CManagerDeProiect ManagerDeProiect;
        readonly FrmMeniuPrincipal FrmParent;
        string? m_sSql;
        readonly int m_lMBME = 2;
        public FrmCautareContracte(FrmMeniuPrincipal FormParent, CManagerDeProiect MM)
        {
            FrmParent = FormParent;
            ManagerDeProiect = MM;
            InitializeComponent();

            GridPersonalizat.CustomGrid.SetConnection(ManagerDeProiect.GetConnection());

            m_ckActiv.Checked = true;
            m_gContracte.CellFormatting += m_gContracte_CellFormatting;
        }
        private void m_ebNume_TextChanged(object sender, EventArgs e)
        {
            Cauta();
        }

        private void m_ebPrenume_TextChanged(object sender, EventArgs e)
        {
            Cauta();
        }

        private void m_tsbCautare_Click(object sender, EventArgs e)
        {
            Cauta();
        }
        private void m_tsbNou_Click(object sender, EventArgs e)
        {
            if (!FrmGestiuneContract.PoateDeschide(ManagerDeProiect)) return;
            FrmGestiuneContract frm = new(FrmParent, ManagerDeProiect, 0, m_lMBME)
            {
                MdiParent = FrmParent
            };
            frm.Show();
        }
        private void m_tsbSterge_Click(object sender, EventArgs e)
        {
            int lCUCO;
            object? value = m_gContracte.GetColumnData("CUCO_Id");
            if (value == null) return;

            lCUCO = Convert.ToInt32(value);
            if (MessageBox.Show(CFormatareDate.GetString("IDS_CONTRACT_ASK_DELETE"), CFormatareDate.GetString("IDS_ATENTIE"), MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            CInterogareSQL q = new(ManagerDeProiect.GetConnection());
            int lFolosit;

            m_sSql = string.Format(@"SELECT COUNT(*)
                                     FROM CU_PrezenteDetaliu
                                     WHERE CUPD_CUCO_Id = {0}", lCUCO);
            lFolosit = q.ExecuteToInt(m_sSql);

            if (lFolosit > 0)
            {
                CFormatareDate.ShowWarning("IDS_CONTRACT_FOLOSIT_IN_PREZENTE");
                return;
            }

            m_sSql = string.Format(@"DELETE FROM CU_ContracteCursanti WHERE CUCC_CUCO_Id={0}", lCUCO);
            q.ExecuteStatement(m_sSql);

            m_sSql = string.Format(@"DELETE FROM CU_Contracte WHERE CUCO_Id={0}", lCUCO);
            q.ExecuteStatement(m_sSql);
            q.Close();

            Cauta();
        }
        private void m_gContracte_DoubleClick(object sender, EventArgs e)
        {
            Modifica();
        }

        private void Cauta()
        {
            string sData, sTmp, sFiltru = "";

            sData = m_ebNume.Text;
            if (sData != "")
            {
                sTmp = string.Format(@" AND EXISTS (
                    SELECT 1 FROM CU_ContracteCursanti CC2
                    INNER JOIN CU_Cursanti CU2 ON CC2.CUCC_CUCU_Id = CU2.CUCU_Id
                    WHERE CC2.CUCC_CUCO_Id = C.CUCO_Id AND CU2.CUCU_Nume LIKE N'{0}%') ", CFormatareDate.StringToSqlString(sData));
                sFiltru += sTmp;
            }

            sData = m_ebPrenume.Text;
            if (sData != "")
            {
                sTmp = string.Format(@" AND EXISTS (
                    SELECT 1 FROM CU_ContracteCursanti CC2
                    INNER JOIN CU_Cursanti CU2 ON CC2.CUCC_CUCU_Id = CU2.CUCU_Id
                    WHERE CC2.CUCC_CUCO_Id = C.CUCO_Id AND CU2.CUCU_Prenume LIKE N'{0}%') ", CFormatareDate.StringToSqlString(sData));
                sFiltru += sTmp;
            }

            if( (m_ckListat.Checked && m_ckNeListat.Checked) || (m_ckListat.Checked==false && m_ckNeListat.Checked==false))
            {

            }
            else 
            {
                    if (m_ckListat.Checked == true)
                    {
                        sTmp = string.Format(" AND C.CUCO_Listat = 1 ");
                        sFiltru += sTmp;
                    }
                    if (m_ckNeListat.Checked == true)
                    {
                        sTmp = string.Format(" AND C.CUCO_Listat = 0 ");
                        sFiltru += sTmp;
                    }
            }
            if (m_ckReziliat.Checked == true)
            {
                sTmp = string.Format(" AND C.CUCO_Retras = 1 ");
                sFiltru += sTmp;
            }

            if (m_ckActiv.Checked == true)
            {
                sTmp = string.Format(" AND C.CUCO_Retras = 0 ");
                sFiltru += sTmp;
            }


            m_sSql = string.Format(@"SELECT ROW_NUMBER() OVER (ORDER BY C.CUCO_NrContract),
                                     C.CUCO_Id, C.CUCO_NrContract,
                                     ISNULL(Beneficiari.NumeComplet, '') AS Beneficiari,
                                     C.CUCO_DataInceputContract, AbCurent.CUAC_DataInceput,
                                     AbCurent.CUAC_DataSfarsit, MBUT.MBUT_Login,
                                     ISNULL(AbCurent.CUAB_Descriere, '') AS CUAB_Descriere,
                                     ISNULL(
                                         (SELECT TOP 1 CUAC_Pret FROM CU_AbonamenteCursant
                                           WHERE CUAC_CUCO_Id = C.CUCO_Id
                                           ORDER BY CASE WHEN CUAC_DataInceput <= GETDATE() AND CUAC_DataSfarsit >= GETDATE() THEN 0 ELSE 1 END,
                                                    CUAC_DataInceput DESC),
                                         C.CUCO_Pret
                                     ) * (ISNULL((SELECT COUNT(*) FROM CU_ContracteCursanti
                                                   WHERE CUCC_CUCO_Id = C.CUCO_Id
                                                     AND ISNULL(CUCC_Retras, 0) = 0), 1) + 1) / 2.0 AS CUCO_Pret,
                                     C.CUCO_Listat, C.CUCO_Retras,
                                     CASE WHEN Restante.NrRestante = 0 THEN 'La zi'
                                          ELSE 'Restant: ' + CAST(Restante.NrRestante AS varchar(5))
                                     END AS [Situatie]
                                 FROM CU_Contracte C
                                 INNER JOIN MB_Utilizatori MBUT ON C.CUCO_MBUT_Id = MBUT.MBUT_Id
                                 OUTER APPLY (
                                     SELECT STUFF((
                                         SELECT ', ' + CU2.CUCU_Nume  + ' ' + CU2.CUCU_Prenume
                                         FROM CU_ContracteCursanti CC2
                                         INNER JOIN CU_Cursanti CU2 ON CC2.CUCC_CUCU_Id = CU2.CUCU_Id
                                         WHERE CC2.CUCC_CUCO_Id = C.CUCO_Id
                                           AND ISNULL(CC2.CUCC_Retras, 0) = 0
                                         ORDER BY CU2.CUCU_Nume, CU2.CUCU_Prenume
                                         FOR XML PATH(''), TYPE
                                     ).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS NumeComplet
                                 ) AS Beneficiari
                                 OUTER APPLY (
                                     SELECT TOP 1 CUAB.CUAB_Descriere, CUAC.CUAC_Pret,
                                            CUAC.CUAC_DataInceput, CUAC.CUAC_DataSfarsit
                                     FROM CU_AbonamenteCursant CUAC
                                     INNER JOIN CU_Abonamente CUAB ON CUAC.CUAC_CUAB_Id = CUAB.CUAB_Id
                                     WHERE CUAC.CUAC_CUCO_Id = C.CUCO_Id
                                     ORDER BY
                                         CASE WHEN CUAC.CUAC_DataInceput <= GETDATE()
                                               AND CUAC.CUAC_DataSfarsit  >= GETDATE()
                                          THEN 0 ELSE 1 END,
                                         CUAC.CUAC_DataInceput DESC
                                 ) AS AbCurent
                                 OUTER APPLY (
                                     SELECT COUNT(*) AS NrRestante
                                     FROM FV_Facturi F2
                                     WHERE F2.FVFA_CUCO_Id = C.CUCO_Id
                                       AND F2.FVFA_ADRN_Id IS NULL
                                       AND ISNULL(F2.FVFA_Suma, 0) > 0
                                       AND ISNULL((SELECT SUM(I.CUIN_Valoare) FROM CU_Incasari I
                                                    WHERE I.CUIN_FVFA_Id = F2.FVFA_Id), 0) < ISNULL(F2.FVFA_Suma, 0)
                                 ) AS Restante
                                 WHERE 1=1 {0}
                                 ORDER BY C.CUCO_NrContract", sFiltru);

            m_gContracte.SuspendLayout();
            m_gContracte.Visible = false;

            m_gContracte.SetSql(m_sSql);

            m_gContracte.FormatColumn("Nr. crt", 50, null, DataGridViewContentAlignment.MiddleRight);
            //     m_gContracte.FormatColumn("Documente arhiva", 65);
            m_gContracte.FormatColumn("CUCO_Id", 70, false);
            m_gContracte.FormatColumn("Nr. Contract", 50, true, DataGridViewContentAlignment.MiddleRight);
            m_gContracte.FormatColumn("Beneficiari", 300);
            m_gContracte.FormatColumn("Data început contract", 110, true, null, "dd.MM.yyyy");
            m_gContracte.FormatColumn("Data început abonament", 110, true, null, "dd.MM.yyyy");
            m_gContracte.FormatColumn("Data sfârșit abonament", 110, true, null, "dd.MM.yyyy");
            m_gContracte.FormatColumn("User", 100);
            m_gContracte.FormatColumn("Abonament", 150);
            m_gContracte.FormatColumn("Pret", 70, true, DataGridViewContentAlignment.MiddleRight);
            m_gContracte.FormatColumn("Contract Listat", 70);
            m_gContracte.FormatColumn("Contract Reziliat", 70);
            m_gContracte.FormatColumn("Situatie", 110, true, DataGridViewContentAlignment.MiddleCenter);
            m_gContracte.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (DataGridViewColumn column in m_gContracte.Columns)
            {
                m_gContracte.Columns[column.Name]!.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            m_gContracte.ResumeLayout();
            m_gContracte.Visible = true;
        }

        private void m_gContracte_SelectionChanged(object sender, EventArgs e)
        {
            object? val = m_gContracte.GetColumnData("CUCO_Id");
            if (val == null || val == DBNull.Value) return;
            IncarcaRegistruContract(Convert.ToInt32(val));
        }

        private void IncarcaRegistruContract(int cucoId)
        {
            var sql = @"
SELECT ADRN_Numar        AS [Nr. înreg.],
       ADRN_Data         AS [Data],
       ADTD_Denumire     AS [Tip document],
       ADRN_Descriere    AS [Descriere]
  FROM AD_RegistruNumereInterne
 INNER JOIN AD_TipuriDocumenteNumarIntern ON ADRN_ADTD_Id = ADTD_Id
 WHERE ADRN_CUCO_Id = @cuco
 ORDER BY ADRN_Numar;";

            m_gRegistruInterne.SetSql(sql, CInterogareSQL.P("@cuco", cucoId));

            m_gRegistruInterne.FormatColumn("Nr. înreg.", 80, true, DataGridViewContentAlignment.MiddleRight);
            m_gRegistruInterne.FormatColumn("Data", 90, true, null, "dd.MM.yyyy");
            m_gRegistruInterne.FormatColumn("Tip document", 180);
            m_gRegistruInterne.FormatColumn("Descriere", 500);

            foreach (DataGridViewColumn c in m_gRegistruInterne.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void Modifica()
        {
            int lCUCO;
            object? value = m_gContracte.GetColumnData("CUCO_Id");
            if (value == null) return;

            lCUCO = Convert.ToInt32(value);
            if (!FrmGestiuneContract.PoateDeschide(ManagerDeProiect)) return;
            FrmGestiuneContract frm = new(FrmParent, ManagerDeProiect, lCUCO, m_lMBME)
            {
                MdiParent = FrmParent
            };
            frm.Show();

        }

        private void m_tsbDeschideArhiva_Click(object sender, EventArgs e)
        {
            object? val = m_gContracte.GetColumnData("CUCO_Id");
            if (val == null || val == DBNull.Value) return;
            int cucoId = Convert.ToInt32(val);

            var frm = new FrmArhiva(FrmParent, ManagerDeProiect, cucoId) { MdiParent = FrmParent };
            frm.Show();
        }

        private void m_ckListat_CheckedChanged(object sender, EventArgs e)
        {
            Cauta();
        }

        private void m_ckNeListat_CheckedChanged(object sender, EventArgs e)
        {
            Cauta();
        }

        private void m_gContracte_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
            if (m_gContracte.Columns[e.ColumnIndex].HeaderText != "Situatie") return;
            string val = e.Value as string ?? "";
            if (val.StartsWith("Restant"))
            {
                e.CellStyle.BackColor = Color.FromArgb(255, 180, 180);
                e.CellStyle.ForeColor = Color.DarkRed;
            }
            else
            {
                e.CellStyle.BackColor = Color.FromArgb(200, 240, 200);
                e.CellStyle.ForeColor = Color.DarkGreen;
            }
        }
    }
}

using GestiunePrezente.Common;
using GestiunePrezente.MeniuPrincipal;

namespace GestiunePrezente.MeniuCursanti
{
    public partial class FrmGestiunePrezenta : Form
    {
        readonly CManagerDeProiect ManagerDeProiect;
        readonly FrmMeniuPrincipal FrmParent;
        int m_lCUPR = 0;
        string? m_sSql;
        readonly int m_lMBME = 22;
        int m_lFormaDeschisa = 0;

        private string _barcodeBuffer = "";
        private DateTime _lastBarcodeKeyTime = DateTime.MinValue;
        private readonly TimeSpan _barcodeTimeout = TimeSpan.FromMilliseconds(150);

        public const string MBME_NUME = "Gestiune Prezenta";
        public static bool PoateDeschide(CManagerDeProiect mm)
            => DrepturiFormular.PoateDeschide(mm, MBME_NUME);

        public FrmGestiunePrezenta(FrmMeniuPrincipal FormParent, CManagerDeProiect MM, int lCUPR)
        {
            FrmParent = FormParent;
            ManagerDeProiect = MM;
            m_lCUPR = lCUPR;
            InitializeComponent();
            GridPersonalizat.CustomGrid.SetConnection(ManagerDeProiect.GetConnection());
        }
        private void FrmGestiunePrezenta_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.KeyPress += FrmGestiunePrezenta_KeyPress_Global;

            m_sSql = string.Format(@"SELECT CUFO_Id,CUFO_Nume+' '+CUFO_Prenume 
                                     FROM CU_Formatori
                                     ORDER BY CUFO_Default,CUFO_Nume,CUFO_Prenume");
            GridPersonalizat.CustomCombo.SetConnection(ManagerDeProiect.GetConnection());
            m_cbFormator.SetSql(m_sSql);

            m_cbTipPrezenta.SetSql("SELECT CUTP_Id, CUTP_Descriere FROM CU_TipPrezenta ORDER BY CUTP_Id");

            Modifica();
            m_lFormaDeschisa = 1;

            //this.Activate();
            //this.TopMost = true;
            ////  m_ebBarcode.Select();
            //m_ebBarcode.TabIndex = 0;
            //this.ActiveControl = m_ebBarcode;
            //m_ebBarcode.Focus();
        }
        private void FrmGestiunePrezenta_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_tsbSalveaza.Enabled == true)
            {
                if (MessageBox.Show(CFormatareDate.GetString("IDS_SAVE_ASK"), CFormatareDate.GetString("IDS_ATENTIE"), MessageBoxButtons.YesNo) == DialogResult.No)
                    e.Cancel = false;
                else
                    if (!Salveaza())
                        e.Cancel = true;
            }
        }

        private void m_ebObservatii_TextChanged(object sender, EventArgs e)
        {
            m_tsbSalveaza.Enabled = true;
        }

        private void m_stbSalveaza_Click(object sender, EventArgs e)
        {
            m_tsbSalveaza.Enabled = !Salveaza();
        }

        private void m_tsbCautare_Click(object sender, EventArgs e)
        {
            IncarcaPrezente();
        }

        private void m_tsbCursantRapid_Click(object sender, EventArgs e)
        {
            if (m_lCUPR <= 0)
            {
                MessageBox.Show("Salvați mai întâi ședința de prezentă înainte de a adăuga un cursant rapid.",
                    "Atenție", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var frm = new FrmCursantRapidPrezenta(ManagerDeProiect, m_lCUPR);
            if (frm.ShowDialog(this) == DialogResult.OK)
                IncarcaPrezente();
        }

        private void m_tsbInsereazaIncasare_Click(object sender, EventArgs e)
        {
            int lCUAC;
            object? value = m_gDetaliuPrezenta.GetColumnData("CUAC_Id");
            if (value != null)
            {
                lCUAC = Convert.ToInt32(value);

                value = m_gDetaliuPrezenta.GetColumnData("RestDePlata");
                if (value != null)
                {
                    decimal dRestDePlata = Convert.ToDecimal(value);

                    if (dRestDePlata > 0)
                    {
                        if (!FrmGestiuneIncasariAbonament.PoateDeschide(ManagerDeProiect)) return;
                        FrmGestiuneIncasariAbonament frm = new(FrmParent, ManagerDeProiect, 0, lCUAC)
                        {
                            MdiParent = FrmParent
                        };
                        frm.Show();
                    }

                }
            }
        }

        private void m_tsbSterge_Click(object sender, EventArgs e)
        {
            int lCUPD;
            object? value = m_gDetaliuPrezenta.GetColumnData("CUPD_Id");
            if (value == null) return;

            lCUPD = Convert.ToInt32(value);
            if (MessageBox.Show(CFormatareDate.GetString("IDS_DETALIUPREZENTA_ASK_DELETE"), CFormatareDate.GetString("IDS_ATENTIE"), MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            SalveazaIstoric("Stergere cursant din prezenta", lCUPD);
            CInterogareSQL q = new(ManagerDeProiect.GetConnection());

            m_sSql = string.Format(@"DELETE FROM CU_PrezenteDetaliu WHERE CUPD_Id={0}", lCUPD);
            q.ExecuteStatement(m_sSql);
            q.Close();

            IncarcaPrezente();
        }

        private void m_dtpDeLaDataPrezenta_ValueChanged(object sender, EventArgs e)
        {
            if (m_lFormaDeschisa <= 0)
                return;

            m_tsbSalveaza.Enabled = true;

            if (m_lCUPR <= 0)
            {
                DateTime dt = m_dtpDeLaDataPrezenta.Value;
                dt = dt.AddHours(2);
                m_dtpPanaLaDataPrezenta.Value = dt;
            }
        }

        private void m_dtpPanaLaDataPrezenta_ValueChanged(object sender, EventArgs e)
        {
            m_tsbSalveaza.Enabled = true;
        }

        private void m_cbFormator_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_lFormaDeschisa <= 0)
                return;

            m_tsbSalveaza.Enabled = true;
        }

        private void m_cbTipPrezenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_lFormaDeschisa <= 0)
                return;

            m_tsbSalveaza.Enabled = true;
        }

        private void m_gDetaliuPrezenta_DoubleClick(object sender, EventArgs e)
        {
            if (m_lFormaDeschisa <= 0)
                return;
            if (m_dtpDeLaDataPrezenta.Value.Date != DateTime.Today)
                return;

                object? vCUPD = m_gDetaliuPrezenta.GetColumnData("CUPD_Id");

            if (vCUPD == null)//INSERT
            {
                m_tsbSalveaza.Enabled = !Salveaza();
                FrmAjutorCursantiPrezenta frm = new(FrmParent, ManagerDeProiect, m_lCUPR);

                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    long lCUCU = frm.GetCUCU();
                    long lCUCO = frm.GetCUCO();
                    int lExistaCUCU;
                    CInterogareSQL q = new(ManagerDeProiect.GetConnection());

                    m_sSql = string.Format(@"SELECT COUNT(*) FROM CU_PrezenteDetaliu WHERE CUPD_CUPR_Id={0} AND CUPD_CUCU_Id={1}", m_lCUPR, lCUCU);
                    lExistaCUCU = q.ExecuteToInt(m_sSql);

                    if (lExistaCUCU > 0)
                    {
                        CFormatareDate.ShowWarning("IDS_ERR_EXISTACURSANTINPREZENTA");
                        q.Close();
                        return;
                    }

                    m_sSql = string.Format(@"INSERT INTO CU_PrezenteDetaliu (CUPD_CUPR_Id, CUPD_CUCO_Id, CUPD_CUCU_Id, CUPD_Prezent, CUPD_DataIntrare)
                                             VALUES ({0}, {1}, {2}, 1, GETDATE())", m_lCUPR, lCUCO, lCUCU);
                    q.ExecuteStatement(m_sSql);

                    //         m_sSql = "SELECT SCOPE_IDENTITY() AS SID";
                    //           int lCUPD = q.ExecuteToInt(m_sSql);
                    //       SalveazaIstoric("Cursant introdus in prezenta", lCUPD);
                    IncarcaPrezente();
                    q.Close();
                }
            }
        }

        private void m_ebBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProceseazaBarcode(m_ebBarcode.Text);
                m_ebBarcode.Text = "";
                e.SuppressKeyPress = true;
            }
        }

        private void SelecteazaRandDupaId(int id)
        {
            foreach (DataGridViewRow row in m_gDetaliuPrezenta.Rows)
            {
                object val = row.Cells["CUPD_Id"].Value;
                if (val == null || val == DBNull.Value) continue;
                if (Convert.ToInt32(val) == id)
                {
                    m_gDetaliuPrezenta.ClearSelection();
                    row.Selected = true;
                    m_gDetaliuPrezenta.CurrentCell = row.Cells[0]; // focus pe prima coloană
                    m_gDetaliuPrezenta.FirstDisplayedScrollingRowIndex = row.Index; // scroll la el
                    break;
                }
            }
        }
        private bool Salveaza()
        {
            if (m_lFormaDeschisa <= 0)
                return false;

            object? value;
            string sData, sSql, sUpdate = "", sTmp, sDeLaDataPrezenta, sPanaLaDataPrezenta, sCUFO, sObs, sCUTP;

            //         sData = m_dtpDeLaDataPrezenta.Text;
            {
                //           value = (object)m_dtpDeLaDataPrezenta.Value;
                sDeLaDataPrezenta = string.Format(" {0} ", CFormatareDate.DateTimeToSQLString(m_dtpDeLaDataPrezenta.Value));
                sTmp = string.Format(" CUPR_DeLaData = {0} ", CFormatareDate.DateTimeToSQLString(m_dtpDeLaDataPrezenta.Value));
                sUpdate += sTmp;
            }

            //DATA CONTRACT
            //        sData = m_dtpPanaLaDataPrezenta.Text;
            {
                //            value = (object)m_dtpPanaLaDataPrezenta.Value;
                sPanaLaDataPrezenta = string.Format(" {0} ", CFormatareDate.DateTimeToSQLString(m_dtpPanaLaDataPrezenta.Value));
                sTmp = string.Format(" ,CUPR_PanaLaData = {0} ", CFormatareDate.DateTimeToSQLString(m_dtpPanaLaDataPrezenta.Value));
                sUpdate += sTmp;
            }

            //FORMATOR
            sData = m_cbFormator.Text;
            if (sData == "")
            {
                CFormatareDate.ShowWarning("IDS_ERR_FORMATOR");
                m_cbFormator.Focus();
                return false;
            }
            else
            {
                value = m_cbFormator.GetColumnData("CUFO_Id");
                sCUFO = string.Format(" {0} ", value);
                sTmp = string.Format(",CUPR_CUFO_Id = {0} ", value);
                sUpdate += sTmp;
            }

            sData = m_ebObservatii.Text;
            if (sData == "")
            {
                sObs = string.Format("NULL");
                sTmp = string.Format(",CUPR_Observatii = NULL");
                sUpdate += sTmp;
            }
            else
            {
                sObs = string.Format(" '{0}' ", CFormatareDate.StringToSqlString(sData));
                sTmp = string.Format(",CUPR_Observatii = '{0}'", CFormatareDate.StringToSqlString(sData));
                sUpdate += sTmp;
            }

            value = m_cbTipPrezenta.GetColumnData("CUTP_Id");
            if (value == null || value == DBNull.Value)
            {
                CFormatareDate.ShowWarning("IDS_ERR_FORMATOR");
                m_cbTipPrezenta.Focus();
                return false;
            }
            sCUTP = string.Format(" {0} ", value);
            sTmp = string.Format(",CUPR_CUTP_Id = {0}", value);
            sUpdate += sTmp;

            if (m_lCUPR == 0)//insert
            {
                CInterogareSQL q = new(ManagerDeProiect.GetConnection());
                sSql = string.Format(@" INSERT INTO CU_Prezente (CUPR_MBSO_Id,CUPR_MBDV_Id, CUPR_DeLaData, CUPR_PanaLaData, CUPR_CUFO_Id,
                                        CUPR_NrPrezenta,
                                        CUPR_DataSalvarii,CUPR_Observatii,CUPR_MBUT_Id,CUPR_CUTP_Id)
                                        SELECT {0},{1},{2},{3},{4},
                                        ISNULL((SELECT MAX(CUPR_NrPrezenta)+1 FROM CU_Prezente WHERE CUPR_MBDV_Id={1}),1),
                                        GetDate(),{5},{6},{7}",
                                         ManagerDeProiect.GetSocietate(), ManagerDeProiect.GetDiviziune(), sDeLaDataPrezenta, sPanaLaDataPrezenta, sCUFO, sObs, ManagerDeProiect.GetUserId(), sCUTP);
                q.ExecuteStatement(sSql);

                sSql = "SELECT SCOPE_IDENTITY() AS SID";
                m_lCUPR = q.ExecuteToInt(sSql);

                q.Close();

            }
            else
            {

                CInterogareSQL qUpdate = new(ManagerDeProiect.GetConnection());
                sSql = string.Format(@"UPDATE CU_Prezente SET {0},CUPR_DataSalvarii=GetDate(),CUPR_MBUT_Id={1} WHERE CUPR_Id = {2}", sUpdate, ManagerDeProiect.GetUserId(), m_lCUPR);
                qUpdate.ExecuteStatement(sSql);
                qUpdate.Close();
            }

            return true;
        }

        private void Modifica()
        {
            if (m_lCUPR > 0)
            {
                m_sSql = string.Format(@"SELECT CUPR_DeLaData,CUPR_PanaLaData,CUPR_CUFO_Id,CUPR_NrPrezenta,CUPR_Observatii,CUPR_CUTP_Id
                                            FROM CU_Prezente
                                            WHERE CUPR_Id={0}", m_lCUPR);
                CInterogareSQL q = new(ManagerDeProiect.GetConnection());
                q.Open(m_sSql);
                if (q.NextRow())
                {
                    m_dtpDeLaDataPrezenta.Value = (DateTime)q.GetDateValue(0)!;
                    if (q.GetDateValue(1) != null)
                        m_dtpPanaLaDataPrezenta.Value = (DateTime)q.GetDateValue(1)!;
                    m_cbFormator.SetComboById("CUFO_Id", q.GetIntValue(2));
                    m_ebNrPrezenta.Text = q.GetIntValue(3).ToString();
                    m_ebObservatii.Text = q.GetStringValue(4);
                    m_cbTipPrezenta.SetComboById("CUTP_Id", q.GetIntValue(5));
                }
                q.Close();

                if(m_dtpDeLaDataPrezenta.Value.Date != DateTime.Today)
                {
                    m_tsbSterge.Enabled = false;
                    m_dtpDeLaDataPrezenta.Enabled = false;
                    m_dtpPanaLaDataPrezenta.Enabled = false;
                    m_cbFormator.Enabled = false;
                    m_cbTipPrezenta.Enabled = false;
                    m_ebBarcode.Enabled = false;
                }


            }
            else
            {
                DateTime astazi = DateTime.Today;
                m_dtpDeLaDataPrezenta.Value = astazi.AddHours(16);
                m_dtpPanaLaDataPrezenta.Value = astazi.AddHours(19);
                m_cbFormator.SetComboById("CUFO_Id", 7);
                m_cbTipPrezenta.SetComboById("CUTP_Id", 1);
                m_lFormaDeschisa = 1;
                Salveaza();
            }
            IncarcaPrezente();
            m_tsbSalveaza.Enabled = false;
        }
        private void IncarcaPrezente()
        {
            m_sSql = string.Format(@"SELECT ROW_NUMBER() OVER (ORDER BY CUCU_Nume, CUCU_Prenume) AS Row,
                                        CUPD_Id, CUCU_Nume, CUCU_Prenume, CUCU_TelefonMobil,
                                        ISNULL(AbCurent.CUAB_Descriere, '') AS CUAB_Descriere,
                                        ISNULL(Prezente.NrPrezenteEfectuate, 0) AS TotalPrezente,
                                        CUPD_DataIntrare, CUPD_DataIesire, ISNULL(CUCO_NrContract, 0) AS CUCO_NrContract, ISNULL(CUCO_Listat, 0) AS CUCO_Listat
                                     FROM CU_PrezenteDetaliu
                                     INNER JOIN CU_Prezente ON CUPD_CUPR_Id = CUPR_Id
                                     LEFT JOIN CU_Contracte ON CUPD_CUCO_Id = CUCO_Id
                                     INNER JOIN CU_Cursanti ON CUPD_CUCU_Id = CUCU_Id
                                     OUTER APPLY (
                                         SELECT TOP 1 CUAB.CUAB_Descriere
                                         FROM CU_AbonamenteCursant CUAC
                                         INNER JOIN CU_Abonamente CUAB ON CUAC.CUAC_CUAB_Id = CUAB.CUAB_Id
                                         WHERE CUAC.CUAC_CUCO_Id = CUCO_Id
                                         ORDER BY CUAC.CUAC_DataInceput DESC
                                     ) AbCurent
                                     LEFT JOIN (
                                         SELECT
                                             CUPD_CUCU_Id,
                                             COUNT(*) AS NrPrezenteEfectuate
                                         FROM CU_PrezenteDetaliu
                                         INNER JOIN CU_Prezente PR ON CUPD_CUPR_Id = PR.CUPR_Id
                                         LEFT JOIN CU_Contracte ON CUPD_CUCO_Id = CUCO_Id
                                         WHERE MONTH(CUPR_DataSalvarii) = (SELECT MONTH(CUPR_DataSalvarii) FROM CU_Prezente WHERE CUPR_Id={0}) AND YEAR(CUPR_DataSalvarii) = YEAR(GETDATE())
                                         GROUP BY CUPD_CUCU_Id
                                     ) Prezente ON Prezente.CUPD_CUCU_Id = CUCU_Id
                                     WHERE CUPD_CUPR_Id = {0}
                                     ORDER BY CUCU_Nume, CUCU_Prenume", m_lCUPR);
            m_gDetaliuPrezenta.SetSql(m_sSql);

            m_gDetaliuPrezenta.FormatColumn("Nr. crt", 50, colAlign: DataGridViewContentAlignment.MiddleRight);
            m_gDetaliuPrezenta.FormatColumn("CUPD_Id", 0, bVisible: false);
            m_gDetaliuPrezenta.FormatColumn("Nume", 200);
            m_gDetaliuPrezenta.FormatColumn("Prenume", 200);
            m_gDetaliuPrezenta.FormatColumn("Telefon", 100);
            m_gDetaliuPrezenta.FormatColumn("Abonament", 250);
            m_gDetaliuPrezenta.FormatColumn("Numar prezențe luna curentă", 150, colAlign: DataGridViewContentAlignment.MiddleRight);
            m_gDetaliuPrezenta.FormatColumn("De la data si ora prezentei", 120, sCellStyle: "dd.MM.yyyy HH:mm");
            m_gDetaliuPrezenta.FormatColumn("Pana la data si ora prezentei", 120, sCellStyle: "dd.MM.yyyy HH:mm");
            m_gDetaliuPrezenta.FormatColumn("Numar contract", 100, colAlign: DataGridViewContentAlignment.MiddleRight);
            m_gDetaliuPrezenta.FormatColumn("Contract listat", 70);


            m_gDetaliuPrezenta.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (DataGridViewColumn column in m_gDetaliuPrezenta.Columns)
            {
                m_gDetaliuPrezenta.Columns[column.Name]!.SortMode = DataGridViewColumnSortMode.NotSortable;
            }


            //int nRowIndex = m_gDetaliuPrezenta.Rows.Count - 1;
            //m_gDetaliuPrezenta.FirstDisplayedScrollingRowIndex = nRowIndex - 1;
            //m_gDetaliuPrezenta.ClearSelection();
            //m_gDetaliuPrezenta.Rows[nRowIndex].Selected = true;
            //m_gDetaliuPrezenta.Rows[nRowIndex].Cells[1].Selected = true;
            //m_gDetaliuPrezenta.CurrentCell = m_gDetaliuPrezenta.Rows[nRowIndex].Cells[2];

            if (m_gDetaliuPrezenta.Rows.Count > 0)
            {
                m_gDetaliuPrezenta.ClearSelection();//If you want

                int nRowIndex = m_gDetaliuPrezenta.Rows.Count - 1;
                int nColumnIndex = 3;

                m_gDetaliuPrezenta.Rows[nRowIndex].Selected = true;
                m_gDetaliuPrezenta.Rows[nRowIndex].Cells[nColumnIndex].Selected = true;

                //In case if you want to scroll down as well.
                m_gDetaliuPrezenta.FirstDisplayedScrollingRowIndex = nRowIndex;
            }


        }

        public void SalveazaIstoric(string sObservatiiOperatiune, int lCUPD)
        {
            CInterogareSQL qIstoric = new(ManagerDeProiect.GetConnection());
            m_sSql = string.Format(@" INSERT INTO CU_PrezenteIstoric
                                    (CUPR_MBSO_Id, CUPR_MBDV_Id, CUPR_CUSE_Id,CUPR_DeLaData, CUPR_CUFO_Id,  CUPR_NrPrezenta, CUPR_DataSalvarii, CUPR_MBUT_Id, CUPR_DataModificarii, CUPR_CUPR_Modificat,
                                    CUPR_Observatii,CUPR_PanaLaData)
                                    SELECT CUPR_MBSO_Id,CUPR_MBDV_Id, CUPR_CUSE_Id, CUPR_DeLaData, CUPR_CUFO_Id, CUPR_NrPrezenta, CUPR_DataSalvarii, {0}, GetDate(), {1}, CUPR_Observatii,CUPR_PanaLaData
                                    FROM CU_Prezente
                                    WHERE CUPR_Id={1}", ManagerDeProiect.GetUserId(), m_lCUPR);
            qIstoric.ExecuteStatement(m_sSql);

            m_sSql = "SELECT SCOPE_IDENTITY() AS SID";
            int lCUPRIstoric = qIstoric.ExecuteToInt(m_sSql);

            m_sSql = string.Format(@" INSERT INTO CU_PrezenteDetaliuIstoric
                                    (CUPD_CUPR_Id, CUPD_CUCU_Id, CUPD_Prezent, CUPD_DataSalvarii, CUPD_CUPD_Modificat,CUPD_ObservatiiOperatiune)
                                    SELECT {0}, CUPD_CUCO_Id, CUPD_Prezent, CUPD_DataSalvarii,CUPD_Id,''
                                    FROM CU_PrezenteDetaliu
                                    WHERE CUPD_CUPR_Id={1} AND CUPD_Id<>{3}
                                    UNION 
                                    SELECT {0}, CUPD_CUCO_Id, CUPD_Prezent, CUPD_DataSalvarii,CUPD_Id,'{2}'
                                    FROM CU_PrezenteDetaliu
                                    WHERE CUPD_CUPR_Id={1}  AND CUPD_Id={3}", lCUPRIstoric, m_lCUPR, sObservatiiOperatiune, lCUPD);
            qIstoric.ExecuteStatement(m_sSql);

            qIstoric.Close();
        }

        private void m_ebBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void FrmGestiunePrezenta_KeyPress_Global(object? sender, KeyPressEventArgs e)
        {
            if (!m_ebBarcode.Enabled)
                return;

            DateTime now = DateTime.Now;

            if (now - _lastBarcodeKeyTime > _barcodeTimeout)
                _barcodeBuffer = "";

            _lastBarcodeKeyTime = now;

            if (e.KeyChar == (char)Keys.Enter)
            {
                string barcode = _barcodeBuffer.Trim();
                _barcodeBuffer = "";

                if (!string.IsNullOrWhiteSpace(barcode))
                {
                    ProceseazaBarcode(barcode);
                    e.Handled = true;
                }

                return;
            }

            if (char.IsControl(e.KeyChar))
                return;

            _barcodeBuffer += e.KeyChar;
        }

        private void ProceseazaBarcode(string sData)
        {
            int lCUCU = 0, lCUCO = 0;

            if (string.IsNullOrWhiteSpace(sData))
                return;

            if (!decimal.TryParse(sData, out _))
                return;

            m_tsbSalveaza.Enabled = !Salveaza();

            m_sSql = string.Format(@"SELECT TOP 1 CUCU_Id
                             FROM CU_Cursanti
                             WHERE CUCU_Barcode = '{0}'",
                                     CFormatareDate.StringToSqlString(sData));

            CInterogareSQL q1 = new(ManagerDeProiect.GetConnection());
            q1.Open(m_sSql);

            if (q1.NextRow())
                lCUCU = q1.GetIntValue(0);

            q1.Close();

            if (lCUCU == 0)
            {
                CFormatareDate.ShowWarning("IDS_ERR_NU_EXISTACURSANT");
                return;
            }

            CInterogareSQL q = new(ManagerDeProiect.GetConnection());

            // Verifică contractul + statusul abonamentului pentru data prezentei
            // Col 0: CUCO_Id, Col 1: 1 dacă abonamentul e activ în data prezentei, 0 dacă e expirat/gap
            m_sSql = string.Format(@"SELECT TOP 1 CUCO_Id,
                                 CASE WHEN EXISTS (
                                     SELECT 1 FROM CU_AbonamenteCursant CUAC2
                                     INNER JOIN CU_Abonamente CUAB2 ON CUAC2.CUAC_CUAB_Id = CUAB2.CUAB_Id
                                     WHERE CUAC2.CUAC_CUCO_Id = CUCO_Id
                                     AND CUAB2.CUAB_SedintaIndividuala = InfoSesiune.CUTP_SedintaIndividuala
                                     AND InfoSesiune.DataPrezenta BETWEEN CUAC2.CUAC_DataInceput AND CUAC2.CUAC_DataSfarsit
                                 ) THEN 1 ELSE 0 END AS AbonamentActiv
                             FROM CU_Contracte
                             INNER JOIN CU_ContracteCursanti ON CUCC_CUCO_Id = CUCO_Id
                             CROSS APPLY (
                                 SELECT TOP 1 CAST(PR.CUPR_DeLaData AS DATE) AS DataPrezenta,
                                              TP.CUTP_SedintaIndividuala
                                 FROM CU_Prezente PR
                                 INNER JOIN CU_TipPrezenta TP ON PR.CUPR_CUTP_Id = TP.CUTP_Id
                                 WHERE PR.CUPR_Id = {0}
                             ) InfoSesiune
                             WHERE CUCC_CUCU_Id = {1}
                             AND EXISTS (
                                 SELECT 1 FROM CU_AbonamenteCursant CUAC
                                 INNER JOIN CU_Abonamente CUAB ON CUAC.CUAC_CUAB_Id = CUAB.CUAB_Id
                                 WHERE CUAC.CUAC_CUCO_Id = CUCO_Id
                                 AND CUAB.CUAB_SedintaIndividuala = InfoSesiune.CUTP_SedintaIndividuala
                             )",
                                     m_lCUPR, lCUCU);

            q.Open(m_sSql);

            bool abonamentActiv = false;
            if (q.NextRow())
            {
                lCUCO = q.GetIntValue(0);
                abonamentActiv = q.GetIntValue(1) == 1;
            }
            q.Close();

            if (lCUCO == 0)
            {
                // Verificăm dacă are vreun contract (indiferent de tipul abonamentului)
                var qHasContract = new CInterogareSQL(ManagerDeProiect.GetConnection());
                bool hasAnyContract = qHasContract.ExecuteToInt(
                    string.Format("SELECT COUNT(*) FROM CU_ContracteCursanti WHERE CUCC_CUCU_Id={0} AND ISNULL(CUCC_Retras,0)=0", lCUCU)) > 0;
                qHasContract.Close();

                if (hasAnyContract)
                {
                    // Are contract, dar fără abonamentul potrivit tipului de ședință
                    // Determinăm tipul ședinței curente
                    var qTip = new CInterogareSQL(ManagerDeProiect.GetConnection());
                    qTip.Open(string.Format(@"SELECT TP.CUTP_SedintaIndividuala
                        FROM CU_Prezente PR
                        INNER JOIN CU_TipPrezenta TP ON PR.CUPR_CUTP_Id = TP.CUTP_Id
                        WHERE PR.CUPR_Id = {0}", m_lCUPR));
                    bool bSedInd = qTip.NextRow() && qTip.GetIntValue(0) == 1;
                    qTip.Close();

                    string msgTip = bSedInd
                        ? "Cursantul nu are abonament de ședință individuală activ pe niciun contract."
                        : "Cursantul nu are abonament de ședință de grup activ pe niciun contract.";
                    MessageBox.Show(msgTip, "Abonament incompatibil",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // blocare — nu se permite adăugarea
                }

                // Niciun contract deloc — avertizare, nu blocare
                DialogResult drNc = MessageBox.Show(
                    "Atenție: acest cursant nu are niciun contract înregistrat.\n\nIntroduci totuși prezența?",
                    "Fără contract",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                if (drNc != DialogResult.Yes)
                    return;
            }
            else if (!abonamentActiv)
            {
                // Contract există dar abonamentul e expirat/gap — avertizare, nu blocare
                DialogResult dr = MessageBox.Show(
                    "Atenție: abonamentul este expirat sau nu acoperă data acestei ședințe.\n\nIntroduci totuși prezența?",
                    "Abonament expirat",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                if (dr != DialogResult.Yes)
                    return;
            }

            int lCUPD = 0;

            // Căutare prezență existentă: CUPD_CUCO_Id poate fi NULL (fără contract)
            string sCucoFilter = lCUCO > 0
                ? $"AND CUPD_CUCO_Id = {lCUCO}"
                : "AND CUPD_CUCO_Id IS NULL";
            m_sSql = string.Format(@"SELECT CUPD_Id
                             FROM CU_PrezenteDetaliu
                             WHERE CUPD_CUPR_Id = {0}
                             {1}
                             AND CUPD_CUCU_Id = {2}",
                                     m_lCUPR, sCucoFilter, lCUCU);

            lCUPD = q.ExecuteToInt(m_sSql);

            if (lCUPD == 0)
            {
                string sCucoVal = lCUCO > 0 ? lCUCO.ToString() : "NULL";
                m_sSql = string.Format(@"INSERT INTO CU_PrezenteDetaliu
                                (CUPD_CUPR_Id,CUPD_CUCO_Id,CUPD_CUCU_Id,CUPD_Prezent,CUPD_DataIntrare)
                                VALUES ({0},{1},{2},1,GETDATE())",
                                        m_lCUPR, sCucoVal, lCUCU);

                q.ExecuteStatement(m_sSql);

                m_sSql = "SELECT SCOPE_IDENTITY() AS SID";
                lCUPD = q.ExecuteToInt(m_sSql);
            }
            else
            {
                m_sSql = string.Format(@"UPDATE CU_PrezenteDetaliu
                                 SET CUPD_DataIesire = GETDATE()
                                 WHERE CUPD_Id={0}
                                 AND CUPD_DataIesire IS NULL", lCUPD);

                q.ExecuteStatement(m_sSql);
            }

            q.Close();

            IncarcaPrezente();
            SelecteazaRandDupaId(lCUPD);

            try { SalveazaIstoric("Cursant introdus in prezenta cu barcode", lCUPD); }
            catch { }
        }
    }
}

using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using GestiunePrezente.Common;
using GestiunePrezente.MeniuPrincipal;

namespace GestiunePrezente.MeniuAdministrare
{
    public partial class FrmGestiuneRegistruNumereInterne : Form
    {
        readonly FrmMeniuPrincipal FrmParent;
        readonly CManagerDeProiect ManagerDeProiect;
        int m_lADRN = 0;
        int m_lCUCO_Leg = 0; // contract legat (0 = fără legătură)

        public const string MBME_NUME = "Gestiune Registru";
        public static bool PoateDeschide(CManagerDeProiect mm)
            => DrepturiFormular.PoateDeschide(mm, MBME_NUME);

        private sealed class PartenerItem
        {
            public int Id { get; init; }
            private string Display { get; init; } = "";
            public PartenerItem(int id, string display) { Id = id; Display = display; }
            public override string ToString() => Display;
        }

        public FrmGestiuneRegistruNumereInterne(FrmMeniuPrincipal FormParent, CManagerDeProiect MM, int lADRN)
        {
            FrmParent = FormParent;
            ManagerDeProiect = MM;
            m_lADRN = lADRN;
            InitializeComponent();
        }

        // =========================================================
        //                          LOAD
        // =========================================================
        private void FrmGestiuneRegistruNumereInterne_Load(object sender, EventArgs e)
        {
            IncarcaTipuriDocument();
            IncarcaSponsori();

            if (m_lADRN > 0)
                IncarcaInregistrare();
            else
            {
                m_rbIesire.Checked = true;
                m_dtpData.Value = DateTime.Now;
                m_ebNumar.Text = "";
                m_ebNumar.PlaceholderText = "auto";
            }

            m_tsbSalveaza.Enabled = false;
        }

        private void IncarcaSponsori()
        {
            m_ebPartener.Items.Clear();
            using var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            q.OpenP(@"SELECT ADPA_Id, ADPA_Denumire FROM AD_Parteneri
                       WHERE ADPA_IsSponsor = 1 AND ADPA_MBDV_Id = @div
                       ORDER BY ADPA_Denumire",
                CInterogareSQL.P("@div", ManagerDeProiect.GetDiviziune()));
            while (q.NextRow())
                m_ebPartener.Items.Add(new PartenerItem(q.GetIntValue(0), q.GetStringValue(1) ?? ""));
            q.Close();
        }

        private void IncarcaTipuriDocument()
        {
            using var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            var dt = q.LoadTableP(@"SELECT ADTD_Id, ADTD_Denumire, ADTD_EsteIesireImplicit,
                                             ISNULL(ADTD_FolderArhiva, '') AS ADTD_FolderArhiva
                                      FROM AD_TipuriDocumenteNumarIntern
                                     WHERE ADTD_Activ = 1
                                  ORDER BY ADTD_OrdineAfisare, ADTD_Denumire");
            m_cbTipDocument.DisplayMember = "ADTD_Denumire";
            m_cbTipDocument.ValueMember = "ADTD_Id";
            m_cbTipDocument.DataSource = dt;
            m_cbTipDocument.SelectedIndex = -1;
        }

        private void IncarcaInregistrare()
        {
            using var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            q.OpenP(@"SELECT ADRN_Numar, ADRN_Data, ADRN_Iesire,
                             ADRN_Descriere, ADRN_Observatii, ADRN_ADTD_Id,
                             ADAH_Cale, ISNULL(ADRN_CUCO_Id, 0),
                             ISNULL(CUCO_NrContract, 0),
                             ISNULL(ADRN_ADPA_Id, 0), ISNULL(ADPA_Denumire, ''),
                             ISNULL(ADRN_ValoareSponsorizare, 0)
                        FROM AD_RegistruNumereInterne
                   LEFT JOIN AD_Arhiva     ON ADAH_ADRN_Id  = ADRN_Id
                   LEFT JOIN CU_Contracte  ON CUCO_Id       = ADRN_CUCO_Id
                   LEFT JOIN AD_Parteneri  ON ADPA_Id       = ADRN_ADPA_Id
                       WHERE ADRN_Id = @id",
                    CInterogareSQL.P("@id", m_lADRN));
            if (q.NextRow())
            {
                m_ebNumar.Text = q.GetIntValue(0).ToString();
                m_dtpData.Value = q.GetDateValue(1) ?? DateTime.Now;
                bool iesire = q.GetBoolValue(2);
                m_rbIntrare.Checked = !iesire;
                m_rbIesire.Checked = iesire;
                m_ebDescriereInregistrare.Text = q.GetStringValue(3) ?? "";
                m_ebObservatii.Text = q.GetStringValue(4) ?? "";
                m_cbTipDocument.SelectedValue = q.GetIntValue(5);

                string? sCale = q.GetStringValue(6);
                if (!string.IsNullOrEmpty(sCale))
                    m_ebDocAtasat.Text = Path.GetFileName(sCale);

                m_lCUCO_Leg = q.GetIntValue(7);
                int nrCo = q.GetIntValue(8);
                if (nrCo > 0)
                {
                    m_ebNrContractLeg.Text = nrCo.ToString();
                    AfiseazaInfoContract();
                }

                int adpaId = q.GetIntValue(9);
                if (adpaId > 0)
                {
                    foreach (PartenerItem item in m_ebPartener.Items)
                        if (item.Id == adpaId) { m_ebPartener.SelectedItem = item; break; }
                }

                decimal valSp = q.GetDecimalValue(11) ?? 0;
                if (valSp != 0)
                    m_ebValoareSponsorizare.Text = valSp.ToString("F2");
            }
            q.Close();
        }

        // =========================================================
        //                          SALVARE
        // =========================================================
        private void m_tsbSalveaza_Click(object sender, EventArgs e)
            => m_tsbSalveaza.Enabled = !Salveaza();

        private bool Salveaza()
        {
            if (m_cbTipDocument.SelectedValue == null)
            { MessageBox.Show("Selectează tipul de document."); m_cbTipDocument.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(m_ebDescriereInregistrare.Text))
            { MessageBox.Show("Introdu descrierea înregistrării."); m_ebDescriereInregistrare.Focus(); return false; }

            int tipId  = Convert.ToInt32(m_cbTipDocument.SelectedValue);
            int adpaId = (m_ebPartener.SelectedItem as PartenerItem)?.Id ?? 0;
            decimal.TryParse(m_ebValoareSponsorizare.Text.Trim().Replace(',', '.'),
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out decimal valSp);

            using var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            q.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                if (m_lADRN == 0)
                {
                    int nr;
                    bool esteManual = int.TryParse(m_ebNumar.Text.Trim(), out int nrManual) && nrManual > 0;

                    if (esteManual)
                    {
                        // Verificare unicitate
                        int exista = q.ExecuteToIntPThrow(
                            @"SELECT COUNT(*) FROM AD_RegistruNumereInterne WHERE ADRN_Numar = @nr",
                            CInterogareSQL.P("@nr", nrManual));
                        if (exista > 0)
                        {
                            MessageBox.Show($"Numărul intern {nrManual} există deja în registru.", "Duplicat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                        nr = nrManual;
                    }
                    else
                    {
                        nr = CGeneratorNumarIntern.Next(q);
                    }

                    m_lADRN = q.ExecuteToIntPThrow(@"
INSERT INTO AD_RegistruNumereInterne
 (ADRN_Numar, ADRN_Data, ADRN_ADTD_Id, ADRN_Iesire, ADRN_MBDV_Id,
  ADRN_CUCO_Id, ADRN_ADPA_Id, ADRN_Descriere, ADRN_Observatii,
  ADRN_ValoareSponsorizare,
  ADRN_MBUT_Id, ADRN_DataSalvarii,
  ADRN_MBUT_Id_UltimeiModificari, ADRN_DataUltimeiModificari)
OUTPUT INSERTED.ADRN_Id
VALUES (@nr, @data, @tip, @ies, @div, @cuco, @adpa, @desc, @obs, @valsp, @u, GETDATE(), @u, GETDATE());",
                        CInterogareSQL.P("@nr",   nr),
                        CInterogareSQL.P("@data", m_dtpData.Value),
                        CInterogareSQL.P("@tip",  tipId),
                        CInterogareSQL.P("@ies",  m_rbIesire.Checked),
                        CInterogareSQL.P("@div",  ManagerDeProiect.GetDiviziune()),
                        CInterogareSQL.P("@cuco",  m_lCUCO_Leg > 0 ? (object)m_lCUCO_Leg : DBNull.Value),
                        CInterogareSQL.P("@adpa",  adpaId > 0 ? (object)adpaId : DBNull.Value),
                        CInterogareSQL.P("@desc",  m_ebDescriereInregistrare.Text),
                        CInterogareSQL.P("@obs",   string.IsNullOrWhiteSpace(m_ebObservatii.Text) ? null : m_ebObservatii.Text),
                        CInterogareSQL.P("@valsp", valSp > 0 ? (object)valSp : DBNull.Value),
                        CInterogareSQL.P("@u",     ManagerDeProiect.GetUserId())
                    );

                    // Dacă s-a introdus manual un nr > secvența curentă, avansăm secvența
                    if (esteManual)
                    {
                        q.ExecuteNonQueryPThrow(
                            @"UPDATE AD_SecventaNumarIntern SET ADSN_UltimulNr = @nr
                              WHERE ADSN_Id = 1 AND ADSN_UltimulNr < @nr",
                            CInterogareSQL.P("@nr", nr));
                    }

                    m_ebNumar.Text = nr.ToString();
                }
                else
                {
                    q.ExecuteNonQueryPThrow(@"
UPDATE AD_RegistruNumereInterne SET
   ADRN_Data                      = @data,
   ADRN_ADTD_Id                   = @tip,
   ADRN_Iesire                    = @ies,
   ADRN_CUCO_Id                   = @cuco,
   ADRN_ADPA_Id                   = @adpa,
   ADRN_Descriere                 = @desc,
   ADRN_Observatii                = @obs,
   ADRN_ValoareSponsorizare       = @valsp,
   ADRN_MBUT_Id_UltimeiModificari = @u,
   ADRN_DataUltimeiModificari     = GETDATE()
 WHERE ADRN_Id = @id;",
                        CInterogareSQL.P("@data", m_dtpData.Value),
                        CInterogareSQL.P("@tip",  tipId),
                        CInterogareSQL.P("@ies",  m_rbIesire.Checked),
                        CInterogareSQL.P("@cuco",  m_lCUCO_Leg > 0 ? (object)m_lCUCO_Leg : DBNull.Value),
                        CInterogareSQL.P("@adpa",  adpaId > 0 ? (object)adpaId : DBNull.Value),
                        CInterogareSQL.P("@desc",  m_ebDescriereInregistrare.Text),
                        CInterogareSQL.P("@obs",   string.IsNullOrWhiteSpace(m_ebObservatii.Text) ? null : m_ebObservatii.Text),
                        CInterogareSQL.P("@valsp", valSp > 0 ? (object)valSp : DBNull.Value),
                        CInterogareSQL.P("@u",     ManagerDeProiect.GetUserId()),
                        CInterogareSQL.P("@id",    m_lADRN)
                    );
                }
                q.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                q.RollbackTransaction();
                MessageBox.Show("Eroare la salvare: " + ex.Message);
                return false;
            }
        }

        // =========================================================
        //                  PARTENER (SPONSOR)
        // =========================================================
        private void m_ebPartener_SelectedIndexChanged(object sender, EventArgs e)
            => m_tsbSalveaza.Enabled = true;

        // =========================================================
        //                  EVENIMENTE STANDARD
        // =========================================================
        private void m_cbTipDocument_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_tsbSalveaza.Enabled = true;
            if (m_cbTipDocument.SelectedItem is System.Data.DataRowView row)
            {
                bool esteIesire = Convert.ToInt32(row["ADTD_EsteIesireImplicit"]) == 1;
                m_rbIesire.Checked  = esteIesire;
                m_rbIntrare.Checked = !esteIesire;
            }
        }
        private void m_ebDescriereInregistrare_TextChanged(object sender, EventArgs e) => m_tsbSalveaza.Enabled = true;
        private void m_ebObservatii_TextChanged(object sender, EventArgs e) => m_tsbSalveaza.Enabled = true;
        private void m_rbIntrare_CheckedChanged(object sender, EventArgs e) => m_tsbSalveaza.Enabled = true;
        private void m_rbIesire_CheckedChanged(object sender, EventArgs e) => m_tsbSalveaza.Enabled = true;
        private void m_ebNumar_TextChanged(object sender, EventArgs e) => m_tsbSalveaza.Enabled = true;
        private void m_ebNrContractLeg_TextChanged(object sender, EventArgs e)
        {
            m_tsbSalveaza.Enabled = true;
            // Resetăm legătura dacă utilizatorul modifică câmpul
            if (m_lCUCO_Leg > 0)
            {
                m_lCUCO_Leg = 0;
                m_lblContractInfo.Text = "";
            }
        }

        private void m_ebValoareSponsorizare_TextChanged(object sender, EventArgs e)
            => m_tsbSalveaza.Enabled = true;

        private void m_ebNrContractLeg_Leave(object sender, EventArgs e)
        {
            AfiseazaInfoContract();
        }

        private void AfiseazaInfoContract()
        {
            string txt = m_ebNrContractLeg.Text.Trim();
            if (string.IsNullOrEmpty(txt)) { m_lCUCO_Leg = 0; m_lblContractInfo.Text = ""; return; }
            if (!int.TryParse(txt, out int nrCo)) { m_lblContractInfo.ForeColor = System.Drawing.Color.Red; m_lblContractInfo.Text = "Număr invalid."; return; }

            using var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            q.OpenP(@"
SELECT C.CUCO_Id,
       STUFF((SELECT ', ' + CU2.CUCU_Nume + ' ' + CU2.CUCU_Prenume
                FROM CU_ContracteCursanti CC2
                INNER JOIN CU_Cursanti CU2 ON CC2.CUCC_CUCU_Id = CU2.CUCU_Id
               WHERE CC2.CUCC_CUCO_Id = C.CUCO_Id
               FOR XML PATH(''), TYPE).value('.','NVARCHAR(MAX)'), 1, 2, '') AS Beneficiari
  FROM CU_Contracte C
 WHERE C.CUCO_NrContract = @nr",
                CInterogareSQL.P("@nr", nrCo));

            if (q.NextRow())
            {
                m_lCUCO_Leg = q.GetIntValue(0);
                string ben = q.GetStringValue(1) ?? "(fără persoane)";
                m_lblContractInfo.ForeColor = System.Drawing.Color.Navy;
                m_lblContractInfo.Text = $"✓  Contract nr. {nrCo}  —  {ben}";

                // Auto-completare descriere pentru intrări noi de tip Contract cursant
                if (m_lADRN == 0
                    && m_cbTipDocument.SelectedValue != null
                    && Convert.ToInt32(m_cbTipDocument.SelectedValue) == 1)
                {
                    m_ebDescriereInregistrare.Text = $"Contract Pregătire Sportivă nr. {nrCo} - {ben}";
                }
            }
            else
            {
                m_lCUCO_Leg = 0;
                m_lblContractInfo.ForeColor = System.Drawing.Color.Red;
                m_lblContractInfo.Text = $"Contractul nr. {nrCo} nu a fost găsit.";
            }
            q.Close();
        }

        private void FrmGestiuneRegistruNumereInterne_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_tsbSalveaza.Enabled)
            {
                if (MessageBox.Show("Salvați modificările?", "Atenție", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    if (!Salveaza()) e.Cancel = true;
            }
        }

        // =========================================================
        //                       ARHIVĂ
        // =========================================================
        // Convenție: numele fișierului PDF = ADRN_Numar.pdf  (numere globale unice)
        // Folder: d:\WORK\ONIXTEIA\ACTE\CONTRACTE SPONSORIZARE\

        private string FolderArhiva()
        {
            const string baza = @"d:\WORK\ONIXTEIA\ACTE\";
            string subfolder = "DIVERSE";
            if (m_cbTipDocument.SelectedItem is System.Data.DataRowView row)
            {
                string folder = row["ADTD_FolderArhiva"]?.ToString() ?? "";
                if (!string.IsNullOrWhiteSpace(folder))
                    subfolder = folder;
            }
            return Path.Combine(baza, subfolder) + Path.DirectorySeparatorChar;
        }

        private void UpsertArhiva(CInterogareSQL q, string sCale)
        {
            // If this exact path already exists, skip (avoids duplicating auto-archived Word docs)
            int pathExists = q.ExecuteToIntP(
                "SELECT COUNT(*) FROM AD_Arhiva WHERE ADAH_Cale = @cale",
                CInterogareSQL.P("@cale", sCale));
            if (pathExists > 0) return;

            // For scan/manual documents: insert a new row linked to the ADRN
            q.ExecuteNonQueryP(
                @"INSERT INTO AD_Arhiva(ADAH_Cale, ADAH_ADRN_Id) VALUES(@cale,@id);",
                CInterogareSQL.P("@cale", sCale),
                CInterogareSQL.P("@id", m_lADRN));
        }


        // Deschide PDF-ul arhivat
        private void m_tsbDeschideArhiva_Click(object sender, EventArgs e)
        {
            if (m_lADRN <= 0) return;

            // doar roluri permise
            if (ManagerDeProiect.GetRolUser_Administrator()
              + ManagerDeProiect.GetRolUser_DirectorGeneral() == 0) return;

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                using var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
                string? sCale = q.ExecuteToStringP(
                    @"SELECT ADAH_Cale FROM AD_Arhiva WHERE ADAH_ADRN_Id=@id;",
                    CInterogareSQL.P("@id", m_lADRN));
                if (string.IsNullOrEmpty(sCale))
                { MessageBox.Show("Nu există fișier arhivat pentru această înregistrare."); return; }

                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(sCale!)
                { UseShellExecute = true });
            }
            catch (Exception ex) { MessageBox.Show("Eroare la deschidere arhivă: " + ex.Message); }
            finally { Cursor.Current = Cursors.Default; }
        }

        // Atașare document prin Ctrl+V (fișier copiat din Explorer cu Ctrl+C)
        // Convenție nume: {nr_intern}_{nume_original} — ex: 68_CI_Popescu_Ion.pdf
        private void m_ebDocAtasat_KeyDown(object sender, KeyEventArgs e)
        {
            if (!(e.Modifiers == Keys.Control && e.KeyCode == Keys.V)) return;
            if (m_lADRN <= 0) { MessageBox.Show("Salvează mai întâi înregistrarea."); return; }
            if (string.IsNullOrWhiteSpace(m_ebNumar.Text)) return;

            try
            {
                var data = Clipboard.GetDataObject();
                if (data == null || !data.GetDataPresent(DataFormats.FileDrop))
                { MessageBox.Show("Copiază mai întâi un fișier în clipboard (Ctrl+C în Explorer)."); return; }

                string[] files = (string[])data.GetData(DataFormats.FileDrop);
                if (files == null || files.Length == 0) return;

                string sNume = m_ebNumar.Text.Trim() + "_" + Path.GetFileName(files[0]);
                string folderArh = FolderArhiva();
                Directory.CreateDirectory(folderArh);
                string sDest = Path.Combine(folderArh, sNume);
                if (File.Exists(sDest)) File.Delete(sDest);
                File.Copy(files[0], sDest);

                using var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
                UpsertArhiva(q, sDest);

                m_ebDocAtasat.Text = sNume;
                MessageBox.Show("Fișier atașat cu succes!");
            }
            catch (Exception ex) { MessageBox.Show("Eroare la atașare: " + ex.Message); }
        }
    }
}
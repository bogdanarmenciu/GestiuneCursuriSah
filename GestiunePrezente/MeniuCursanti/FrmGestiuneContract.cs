using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using GestiunePrezente.Common;
using GestiunePrezente.MeniuPrincipal;
using System.Diagnostics;
using System.Globalization;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Xml.Linq;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace GestiunePrezente.MeniuCursanti
{
    public partial class FrmGestiuneContract : Form
    {
        readonly CManagerDeProiect ManagerDeProiect;
        readonly FrmMeniuPrincipal FrmParent;
        int m_lCUCO = 0;
        readonly int m_lMBME = 0;
        string? sSql, m_sSql;
        int m_lFormaDeschisa = 0;

        public const string MBME_NUME = "Gestiune Contract";
        public static bool PoateDeschide(CManagerDeProiect mm)
            => DrepturiFormular.PoateDeschide(mm, MBME_NUME);

        public FrmGestiuneContract(FrmMeniuPrincipal FormParent, CManagerDeProiect MM, int lCUCO, int lMBME = 3)
        {
            FrmParent = FormParent;
            ManagerDeProiect = MM;
            m_lCUCO = lCUCO;
            m_lMBME = lMBME;
            InitializeComponent();
            GridPersonalizat.CustomGrid.SetConnection(ManagerDeProiect.GetConnection());
            //  m_dtDataNasterii.CustomFormat = "dd.MM.yyyy";

        }

        private void m_dtDataInceputContract_ValueChanged(object sender, EventArgs e)
        {
            if (m_lFormaDeschisa <= 0) return;
            m_tsbSalveaza.Enabled = true;

            // Auto-fill end date only for new contracts: season ends Sept 30
            if (m_lCUCO == 0)
            {
                var d = m_dtDataInceputContract.Value;
                int endYear = d.Month >= 10 ? d.Year + 1 : d.Year;
                m_dtDataSfarsitContract.Value = new DateTime(endYear, 9, 30);
            }
        }

        private void m_ebNrContract_TextChanged(object sender, EventArgs e)
        {
            m_tsbSalveaza.Enabled = true;
        }

        private void m_cbCursant_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_tsbSalveaza.Enabled = true;
        }

        private void m_stbSalveaza_Click(object sender, EventArgs e)
        {
            m_tsbSalveaza.Enabled = !Salveaza();
        }

        public void GenereazaContract() => m_tsbListeazaContract_Click(this, EventArgs.Empty);

        private void m_tsbListeazaContract_Click(object sender, EventArgs e)
        {
            string sNume = "", sPrenume = "", sCnp = "", sNrContract = "", sTelefon = "", sEmail = "", sSocietate = "", sCUISoc = "", sAdresaSoc = "", sContSoc = "",
                sReprezentantSoc = "", sDateReprezentant = "", sAdresaReprezentant = "", sAdresaDiviziune = "", SectiuneaSpeciala = "", numeTutore = "", sPret12 = "",
                sPret9 = "", sPret6 = "", sPret3 = "", sPret1 = "";
            int NrCursantiContract = 0, CUAB_Id = 0;
            decimal? pret = 0;
            DateTime dtDataInceputContract = DateTime.MinValue;
            var abonamente = new List<(int Id, string Descriere, decimal Pret)>();

            if (m_lCUCO <= 0)
                return;

            CInterogareSQL q = new(ManagerDeProiect.GetConnection());

            sSql = string.Format(@"SELECT COUNT(*), CUCO_NrContract, CUCO_DataInceputContract
                                   FROM CU_ContracteCursanti
                                   INNER JOIN CU_Contracte ON CUCC_CUCO_Id = CUCO_Id
                                   INNER JOIN CU_Cursanti ON CUCC_CUCU_Id = CUCU_Id
                                   WHERE CUCO_Id = {0}
                                   GROUP BY CUCO_NrContract, CUCO_DataInceputContract", m_lCUCO);
            q.Open(sSql);
            if (q.NextRow())
            {
                NrCursantiContract = q.GetIntValue(0);
                sNrContract = q.GetIntValue(1).ToString();
                dtDataInceputContract = (DateTime)q.GetDateValue(2)!;
            }

            if (NrCursantiContract == 0)
            {
                CFormatareDate.ShowWarning("IDS_CONTRACT_FARA_CURSANT");
                return;
            }

            string sNrCursantiContract = NrCursantiContract.ToString();

            sSql = string.Format(@"SELECT 
                                      MBSO_Descriere, MBSO_CUI, MBSO_EmailSocietate, MBSO_TelReprezentant,
                                      CASE 
                                          WHEN MBSO_Oras IS NULL OR MBSO_Oras='-' THEN '' 
                                          ELSE 'loc. ' + UPPER(MBSO_Oras) 
                                      END +
                                      CASE WHEN MBSO_Strada IS NULL OR MBSO_Strada='-' THEN '' ELSE ', str. ' + MBSO_Strada END +
                                      CASE WHEN MBSO_NumarStrada IS NULL OR MBSO_NumarStrada='-' THEN '' ELSE ' nr. ' + MBSO_NumarStrada END +
                                      CASE WHEN MBSO_Bloc IS NULL OR MBSO_Bloc='-' THEN '' ELSE ', bl. ' + MBSO_Bloc END +
                                      CASE WHEN MBSO_Scara IS NULL OR MBSO_Scara='-' THEN '' ELSE ', sc. ' + MBSO_Scara END +
                                      CASE WHEN MBSO_Apartament IS NULL OR MBSO_Apartament='-' THEN '' ELSE ', ap. ' + MBSO_Apartament END
                                      AS AdresaSocietate,
  
                                      CASE 
                                          WHEN MBDV_Diviziune IS NULL OR MBDV_Diviziune='-' THEN '' 
                                          ELSE 'loc. ' + UPPER(MBDV_Diviziune)
                                      END + 
                                      CASE 
                                          WHEN MBDV_Adresa IS NULL OR MBDV_Adresa='-' THEN '' 
                                          ELSE ', ' + MBDV_Adresa 
                                      END AS AdresaDiviziune,

                                      MBSO_Cont,
                                      UPPER(ISNULL(MBSO_Reprezentant, '')) AS MBSO_Reprezentant,
                                      CASE 
                                          WHEN MBSO_CISerieReprezentant IS NOT NULL AND MBSO_CISerieReprezentant <> '-' 
                                          THEN 'C.I. seria ' + MBSO_CISerieReprezentant + ', nr. ' + ISNULL(MBSO_CINumarReprezentant, '')
                                          ELSE '' 
                                      END +
                                      CASE 
                                          WHEN MBSO_CNPReprezentant IS NOT NULL AND MBSO_CNPReprezentant <> '-' 
                                          THEN ', CNP ' + MBSO_CNPReprezentant 
                                          ELSE '' 
                                          END AS DateCompleteReprezentant,
                                          MBSO_AdresaReprezentant

                                  FROM MB_Societate
                                  INNER JOIN MB_Diviziune ON MBDV_MBSO_Id = MBSO_Id
                                  WHERE MBSO_Id = {0} AND MBDV_Id = {1}", ManagerDeProiect.GetSocietate(), ManagerDeProiect.GetDiviziune());
            q.Open(sSql);
            if (q.NextRow())
            {
                sSocietate = q.GetStringValue(0)!;
                sCUISoc = q.GetStringValue(1)!;
                sEmail = q.GetStringValue(2)!;
                sTelefon = q.GetStringValue(3)!;
                sAdresaSoc = q.GetStringValue(4)!;
                sAdresaDiviziune = q.GetStringValue(5)!;
                sContSoc = q.GetStringValue(6)!;
                sReprezentantSoc = q.GetStringValue(7)!;
                sDateReprezentant = q.GetStringValue(8)!;
                sAdresaReprezentant = q.GetStringValue(9)!;
            }

            List<string> nume = [];
            List<DateTime> dataNasterii = [];
            List<bool> esteMinor = [];
            List<string> dateBuletin = [];

            sSql = string.Format(@"SELECT DISTINCT
                                       CONCAT(CUCU_Nume, ' ', CUCU_Prenume) AS NumeCursant,
                                       CUCU_DataNasterii,
                                       CASE 
                                           WHEN CUCU_DataNasterii > DATEADD(YEAR, -18, GETDATE()) THEN 1 
                                           ELSE 0 
                                       END AS EsteMinor,
                                       CONCAT(
                                           CASE 
                                               WHEN CUCU_DataNasterii > DATEADD(YEAR, -14, GETDATE()) THEN 'C.N. '
                                               ELSE 'C.I. '
                                           END,
                                           'Seria ', CUCU_CISerie, ' Nr. ', CUCU_CINumar, ', CNP ', CUCU_CNP
                                       ) AS IdentificareCompleta
                                   FROM CU_ContracteCursanti
                                   INNER JOIN CU_Contracte ON CUCO_Id = CUCC_CUCO_Id
                                   INNER JOIN CU_Cursanti ON CUCU_Id = CUCC_CUCU_Id
                                   WHERE CUCO_Id = {0}", m_lCUCO);
            q.Open(sSql);
            while (q.NextRow())
            {
                nume.Add(q.GetStringValue(0)!);
                dataNasterii.Add((DateTime)q.GetDateValue(1)!);
                esteMinor.Add(q.GetIntValue(2) == 1);
                dateBuletin.Add(q.GetStringValue(3)!);
            }

            //aici facem partea cu tutorele
            bool existaMinori = esteMinor.Any(m => m == true);
            sSql = string.Format(@"SELECT TOP 1
                                          CUCU_Judet, CUCU_Oras, CUCU_Strada, CUCU_NumarStrada, 
                                          CUCU_Bloc, CUCU_Scara, CUCU_Apartament, CUCU_Domiciliu,
                                          CUCU_NumeTutore, CUCU_PrenumeTutore, CUCU_CISerieTutore, 
                                          CUCU_CINumarTutore, CUCU_CNPTutore, CUCU_TelefonTutore
                                   FROM CU_ContracteCursanti
                                   INNER JOIN CU_Cursanti ON CUCC_CUCU_Id = CUCU_Id
                                   WHERE CUCC_CUCO_Id = {0}", m_lCUCO);

            q.Open(sSql);
            if (q.NextRow())
            {
                // Date adresă (comune)
                string judet = q.GetStringValue(0);
                string oras = q.GetStringValue(1);
                string strada = q.GetStringValue(2);
                string nrStrada = q.GetStringValue(3);
                string bloc = q.GetStringValue(4);
                string apartament = q.GetStringValue(6);

                if (existaMinori)
                {
                    // Date tutore
                    numeTutore = q.GetStringValue(8) + " " + q.GetStringValue(9);
                    string serieCI = q.GetStringValue(10);
                    string nrCI = q.GetStringValue(11);
                    string cnp = q.GetStringValue(12);
                    string telefon = q.GetStringValue(13);

                    SectiuneaSpeciala = $"Reprezentantul legal al minorului: {numeTutore}, domiciliată în jud. {judet}, loc. {oras}, str. {strada}," +
                                  $" nr. {nrStrada}, bl. {bloc}, ap. {apartament} identificat cu C.I. seria {serieCI}, nr. {nrCI}, CNP {cnp}, Telefon: {telefon}";
                }
                else
                {
                    SectiuneaSpeciala = $"domiciliat în jud. {judet}, loc. {oras}, str. {strada}, nr. {nrStrada}, bl. {bloc}, ap. {apartament}";
                }
            }

            sSql = string.Format(@"SELECT A.CUAB_Id, A.CUAB_Descriere,
                                          ISNULL(P.CUABP_Pret, 0) AS Pret
                                   FROM CU_Abonamente A
                                   OUTER APPLY (
                                       SELECT TOP 1 CUABP_Pret
                                       FROM CU_PreturiAbonamente
                                       WHERE CUABP_CUAB_Id = A.CUAB_Id
                                         AND CUABP_DataStart <= '{0}'
                                         AND (CUABP_DataSfarsit IS NULL OR CUABP_DataSfarsit >= '{0}')
                                       ORDER BY CUABP_DataStart DESC
                                   ) P
                                 WHERE A.CUAB_SedintaIndividuala=0
                                   ORDER BY A.CUAB_Id", dtDataInceputContract.ToString("yyyy-MM-dd"));
            q.Open(sSql);
            while (q.NextRow())
                abonamente.Add((q.GetIntValue(0), q.GetStringValue(1)!, q.GetDecimalValue(2) ?? 0));

            // Override base price with actual contracted price (CUAC_Pret is the per-person base)
            sSql = string.Format(@"SELECT TOP 1 CUAC_CUAB_Id, CUAC_Pret
                                       FROM CU_AbonamenteCursant
                                       WHERE CUAC_CUCO_Id = {0}
                                       ORDER BY CUAC_DataInceput ASC", m_lCUCO);
            q.Open(sSql);
            if (q.NextRow())
            {
                CUAB_Id = q.GetIntValue(0);
                pret = q.GetDecimalValue(1);
                for (int idx = 0; idx < abonamente.Count; idx++)
                {
                    if (abonamente[idx].Id == CUAB_Id)
                    {
                        abonamente[idx] = (abonamente[idx].Id, abonamente[idx].Descriere, pret ?? abonamente[idx].Pret);
                        break;
                    }
                }
            }

            // Person 1 pays full price; each additional person pays 50% — total = base + (n-1) * base/2
            if (NrCursantiContract >= 2)
            {
                for (int idx = 0; idx < abonamente.Count; idx++)
                {
                    decimal pretAjustat = abonamente[idx].Pret + (NrCursantiContract - 1) * (abonamente[idx].Pret / 2);
                    abonamente[idx] = (abonamente[idx].Id, abonamente[idx].Descriere, pretAjustat);
                }
            }

            string folderBaza = @"d:\WORK\ONIXTEIA\ACTE\CONTRACTE\";
            Directory.CreateDirectory(folderBaza);

            string numePartiContract = FaraDiacritice(string.Join("_", nume[0].Split(' ', StringSplitOptions.RemoveEmptyEntries)));
            string numeFisier = $"Contract_{m_ebNrContract.Text}_{numePartiContract}.docx";
            string sNumeDocument = Path.Combine(folderBaza, numeFisier);

            if (sNumeDocument.Length > 250)
            {
                string extensie = Path.GetExtension(sNumeDocument);
                sNumeDocument = string.Concat(sNumeDocument.AsSpan(0, 250 - extensie.Length), extensie);
            }

            //if (File.Exists(sNumeDocument))
            //{
            //    DialogResult dlg = MessageBox.Show(CFormatareDate.GetString("IDS_ERR_GENERARECONTRACT"), CFormatareDate.GetString("IDS_ATENTIE"), MessageBoxButtons.YesNoCancel);
            //    if (dlg == DialogResult.Cancel)
            //        return;

            //    if (dlg == DialogResult.Yes)
            //    {
            //        Process.Start(new ProcessStartInfo(sNumeDocument) { UseShellExecute = true });
            //        return;
            //    }
            //}
            if (File.Exists(sNumeDocument))
            {
                // Construim un mesaj clar pentru utilizator
                string mesaj = "Contractul a fost deja generat.\n\n" +
                               "• Apăsați DA pentru a DESCHIDE fișierul existent.\n" +
                               "• Apăsați NU pentru a REGENERA contractul (cel vechi va fi suprascris).\n" +
                               "• Apăsați CANCEL pentru a anula operațiunea.";

                DialogResult dlg = MessageBox.Show(mesaj, "Contract existent", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (dlg == DialogResult.Cancel)
                {
                    return; // Oprim execuția
                }
                else if (dlg == DialogResult.Yes)
                {
                    // Deschide cel existent și iese din funcție
                    Process.Start(new ProcessStartInfo(sNumeDocument) { UseShellExecute = true });
                    return;
                }
                // Dacă a ales 'No', codul va merge mai departe și va executa partea de generare (File.Copy, etc.)
            }
            string sTemplatePath;

            sTemplatePath = @"d:\WORK\ONIXTEIA\ACTE\SABLOANE\SablonContract.dotx";


            if (!File.Exists(sTemplatePath))
            {
                MessageBox.Show("Nu am găsit șablonul la adresa: " + sTemplatePath);
                return;
            }


            q.Close();

            // Nr. intern din registru pentru acest contract (ADTD_Id=1)
            // Dacă nu există, îl creăm automat la generarea documentului
            int nrInternContract = 0;
            DateTime dataInregistrareContract = DateTime.Today;
            int adrnIdContract = 0;
            {
                var qr = new CInterogareSQL(ManagerDeProiect.GetConnection());
                qr.OpenP(@"SELECT ADRN_Numar, ADRN_Data, ADRN_Id FROM AD_RegistruNumereInterne
                           WHERE ADRN_CUCO_Id = @cuco AND ADRN_ADTD_Id = 1",
                    CInterogareSQL.P("@cuco", m_lCUCO));
                if (qr.NextRow())
                {
                    nrInternContract = qr.GetIntValue(0);
                    adrnIdContract   = qr.GetIntValue(2);
                }
                qr.Close();
                // Data din document = ziua generării, nu data înregistrării din BD
                dataInregistrareContract = DateTime.Today;
            }

            if (adrnIdContract == 0)
            {
                var qSeq = new CInterogareSQL(ManagerDeProiect.GetConnection());
                qSeq.BeginTransaction(System.Data.IsolationLevel.Serializable);
                try
                {
                    nrInternContract = CGeneratorNumarIntern.Next(qSeq);
                    dataInregistrareContract = DateTime.Today;

                    adrnIdContract = qSeq.ExecuteToIntPThrow(@"
INSERT INTO AD_RegistruNumereInterne
    (ADRN_Numar, ADRN_Data, ADRN_ADTD_Id, ADRN_Iesire, ADRN_MBDV_Id,
     ADRN_CUCO_Id, ADRN_Descriere, ADRN_MBUT_Id, ADRN_DataSalvarii)
OUTPUT INSERTED.ADRN_Id
VALUES
    (@nr, GETDATE(), 1, 1, @div,
     @cuco, @desc, @u, GETDATE());",
                        CInterogareSQL.P("@nr",   nrInternContract),
                        CInterogareSQL.P("@div",  ManagerDeProiect.GetDiviziune()),
                        CInterogareSQL.P("@cuco", m_lCUCO),
                        CInterogareSQL.P("@desc", $"Contract Pregătire Sportivă nr. {sNrContract} - {string.Join(", ", nume)}"),
                        CInterogareSQL.P("@u",    ManagerDeProiect.GetUserId())
                    );

                    qSeq.CommitTransaction();
                }
                catch (Exception ex)
                {
                    qSeq.RollbackTransaction();
                    MessageBox.Show("Eroare la creare număr intern: " + ex.Message);
                    return;
                }
                finally { qSeq.Close(); }
            }

            var dateSocietate = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        { "NrContract", sNrContract },
        { "DataInregistrare", dtDataInceputContract.ToString("dd.MM.yyyy") },
        { "NrInternRegistru", nrInternContract > 0 ? nrInternContract.ToString() : "" },
        { "DataInternRegistru", dataInregistrareContract.ToString("dd.MM.yyyy") },
        { "Societate", sSocietate },
        { "SediuSocietate", sAdresaSoc },
        { "SediuDiviziune", sAdresaDiviziune },
        { "NrCIF", sCUISoc },
        { "EmailSocietate", sEmail },
        { "TelSocietate", sTelefon },
        { "ContBancarSocietate", sContSoc },
        { "ReprezentantSocietate", sReprezentantSoc },
        { "DateBuletin", sDateReprezentant },
        { "AdresaReprezentant", sAdresaReprezentant }
    };

            try
            {
                File.Copy(sTemplatePath, sNumeDocument, true);
                using var wordDoc = WordprocessingDocument.Open(sNumeDocument, true);
                wordDoc.ChangeDocumentType(WordprocessingDocumentType.Document);
                var mainPart = wordDoc.MainDocumentPart;
                if (mainPart == null) return;

                var repeatingSection = mainPart.Document.Descendants<SdtBlock>()
                    .FirstOrDefault(sdt => sdt.SdtProperties?.GetFirstChild<Tag>()?.Val == "GrupCursanti");

                // aici facem secventa repetitiva cu copiii
                if (repeatingSection != null)
                {
                    var parent = repeatingSection.Parent;

                    for (int i = 0; i < nume.Count; i++)
                    {
                        var clona = (SdtBlock)repeatingSection.CloneNode(true);
                        var fieldsInClone = clona.Descendants<SdtRun>().ToList();
                        foreach (var field in fieldsInClone)
                        {
                            var tagElement = field.SdtProperties?.GetFirstChild<Tag>();
                            string tag = tagElement?.Val?.Value;

                            if (string.IsNullOrEmpty(tag)) continue;

                            string valoareFinala = tag switch
                            {
                                "NumeCursant" => nume[i],
                                "DataNasteriiCursant" => dataNasterii[i].ToString("dd.MM.yyyy"),
                                "DateBuletinCursant" => dateBuletin[i],
                                "Legatura" => (i > 0) ? " și " : "",
                                _ => ""
                            };

                            if (tag == "Legatura" && i == 0)
                            {
                                var paragraph = field.Ancestors<Paragraph>().FirstOrDefault();
                                paragraph?.Remove();
                            }
                            else
                            {
                                var txt = field.Descendants<Text>().FirstOrDefault();
                                if (txt != null)
                                {
                                    txt.Text = valoareFinala;
                                    field.Descendants<Text>().Skip(1).ToList().ForEach(t => t.Remove());
                                }
                            }
                        }
                        parent.InsertBefore(clona, repeatingSection);
                    }
                    repeatingSection.Remove();
                }

                // aici se face prima parte cu societatea
                var staticControls = mainPart.Document.Descendants<SdtRun>().ToList();
                foreach (var ct in staticControls)
                {
                    var tagElement = ct.SdtProperties?.GetFirstChild<Tag>();
                    string? tagValue = tagElement?.Val?.Value;

                    if (!string.IsNullOrEmpty(tagValue) && dateSocietate.TryGetValue(tagValue, out var replacementValue))
                    {
                        var textElement = ct.Descendants<Text>().FirstOrDefault();

                        if (textElement != null)
                        {
                            textElement.Text = replacementValue;
                            var otherTexts = ct.Descendants<Text>().Skip(1).ToList();
                            foreach (var t in otherTexts) t.Remove();
                        }
                    }
                }

                // Replace hardcoded subscription count — search specifically for "cele 6"
                // to avoid matching "6.1.1." inside the Abonamente content control
                foreach (var para in mainPart.Document.Body!.Descendants<Paragraph>())
                {
                    string paraText = string.Concat(para.Descendants<Text>().Select(t => t.Text));
                    if (!paraText.Contains("variante de program")) continue;

                    var texts = para.Descendants<Text>().ToList();
                    bool replaced = false;
                    for (int ti = 0; ti < texts.Count && !replaced; ti++)
                    {
                        // Case 1: "cele 6" in the same run
                        if (texts[ti].Text.Contains("cele 6"))
                        {
                            texts[ti].Text = texts[ti].Text.Replace("cele 6", $"cele {abonamente.Count}");
                            replaced = true;
                        }
                        // Case 2: "cele" ends one run, "6" starts the next
                        else if (texts[ti].Text.TrimEnd().EndsWith("cele")
                                 && ti + 1 < texts.Count
                                 && texts[ti + 1].Text.TrimStart().StartsWith("6"))
                        {
                            string s = texts[ti + 1].Text;
                            int pad = s.Length - s.TrimStart().Length;
                            texts[ti + 1].Text = s[..pad] + abonamente.Count + s[(pad + 1)..];
                            replaced = true;
                        }
                    }
                    break;
                }

                // Generate Abonamente section dynamically from database
                var sdtAbonamente = mainPart.Document.Descendants<SdtRun>()
                    .FirstOrDefault(s => s.SdtProperties?.GetFirstChild<Tag>()?.Val == "Abonamente");

                if (sdtAbonamente != null)
                {
                    var parentPara = sdtAbonamente.Ancestors<Paragraph>().FirstOrDefault();
                    if (parentPara != null)
                    {
                        var pPr = parentPara.GetFirstChild<ParagraphProperties>() ?? parentPara.PrependChild(new ParagraphProperties());
                        pPr.RemoveAllChildren<SpacingBetweenLines>();
                        pPr.RemoveAllChildren<Indentation>();
                        pPr.RemoveAllChildren<Justification>();
                        pPr.AppendChild(new SpacingBetweenLines { Before = "0", After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto });
                    }

                    var sdtContent = sdtAbonamente.GetFirstChild<SdtContentRun>();
                    sdtContent?.RemoveAllChildren();

                    for (int i = 0; i < abonamente.Count; i++)
                    {
                        bool esteSelectat = abonamente[i].Id == CUAB_Id;
                        string bifa = esteSelectat ? "☒ " : "☐ ";
                        string subpunct = $"6.1.{i + 1}.";
                        string textLinie = $"{bifa}{subpunct} {abonamente[i].Descriere} – {abonamente[i].Pret:N0} lei / lună";
                        Run runRand = new();
                        RunProperties rPr = new(
                            new RunFonts { Ascii = "Calibri", HighAnsi = "Calibri" },
                            new FontSize { Val = "24" }
                        );
                        if (esteSelectat) rPr.AppendChild(new Bold());
                        runRand.AppendChild(rPr);
                        runRand.AppendChild(new Text(textLinie) { Space = SpaceProcessingModeValues.Preserve });
                        sdtContent.AppendChild(runRand);
                        if (i < abonamente.Count - 1)
                            sdtContent.AppendChild(new Run(new Break()));
                    }
                }

                //Sectiunea speciala pentru adresa tutorelui sau a cursantului major
                var sectiune = mainPart.Document.Descendants<SdtRun>()
                    .FirstOrDefault(s => s.SdtProperties?.GetFirstChild<Tag>()?.Val == "SectiuneaSpeciala");

                if (sectiune != null)
                {
                    var txt = sectiune.Descendants<Text>().FirstOrDefault();
                    txt?.Text = SectiuneaSpeciala;
                }

                //de pus numele la ambii cursanti la final daca trebuie 
                sectiune = mainPart.Document.Descendants<SdtRun>()
                .FirstOrDefault(s => s.SdtProperties?.GetFirstChild<Tag>()?.Val == "NumeStudent");
                string rezultat = string.Join(" și ", nume);

                if (sectiune != null)
                {
                    var txt = sectiune.Descendants<Text>().FirstOrDefault();
                    if (txt != null)
                    {
                        txt.Text = rezultat;
                        var restulDeText = sectiune.Descendants<Text>().Skip(1).ToList();
                        foreach (var t in restulDeText) t.Remove();
                    }
                }

                sectiune = mainPart.Document.Descendants<SdtRun>()
                    .FirstOrDefault(s => s.SdtProperties?.GetFirstChild<Tag>()?.Val == "TutoreLegal");

                if (sectiune != null)
                {
                    var paraEticheta = sectiune.Ancestors<Paragraph>().FirstOrDefault();
                    var txt = sectiune.Descendants<Text>().FirstOrDefault();

                    if (txt != null && paraEticheta != null)
                    {
                        if (!string.IsNullOrEmpty(numeTutore))
                        {
                            txt.Text = "Reprezentant legal:";
                            sectiune.Descendants<Text>().Skip(1).ToList().ForEach(t => t.Remove());
                            Paragraph paraNume = new();
                            ParagraphProperties pPr = new(new Justification() { Val = JustificationValues.Center });
                            paraNume.AppendChild(pPr);
                            Run runNume = new();
                            RunProperties rPr = new();
                            RunFonts rf = new() { Ascii = "Calibri", HighAnsi = "Calibri" };
                            FontSize fs = new() { Val = "24" };
                            rPr.AppendChild(rf);
                            rPr.AppendChild(fs);
                            runNume.AppendChild(rPr);
                            runNume.AppendChild(new Text(numeTutore));
                            paraNume.AppendChild(runNume);
                            paraEticheta.InsertAfterSelf(paraNume);
                        }
                        else
                        {
                            txt.Text = "";
                            sectiune.Descendants<Text>().Skip(1).ToList().ForEach(t => t.Remove());
                        }
                    }
                }

                mainPart.Document.Save();
                Process.Start(new ProcessStartInfo(sNumeDocument) { UseShellExecute = true });
                ArhiveazaFisierGenerat(sNumeDocument, adrnIdContract);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la generare: " + ex.Message);
            }

            CInterogareSQL qUpdate = new(ManagerDeProiect.GetConnection());
            sSql = string.Format(@"UPDATE CU_Contracte SET CUCO_Listat=1 WHERE CUCO_Id = {0}", m_lCUCO);
            qUpdate.ExecuteStatement(sSql);
            qUpdate.Close();

            m_ckListat.Checked = true;

        }

        private void FrmGestiuneCursant_FormClosing(object sender, FormClosingEventArgs e)
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

        private void FrmGestiuneContract_Load(object sender, EventArgs e)
        {
            m_ckListat.Enabled = false;
            m_ckRetras.Enabled = false;
            GridPersonalizat.CustomCombo.SetConnection(ManagerDeProiect.GetConnection());
            m_cbAbonamentAb.SetSql("SELECT CUAB_Id, CUAB_Descriere FROM CU_Abonamente ORDER BY CUAB_Descriere");

            ActualizeazaButoanePersoana();
            m_tabControl.SelectedIndexChanged += (s, ev) => ActualizeazaButoanePersoana();

            // Context menu pe grila de persoane
            var ctxCursanti = new ContextMenuStrip();
            var miRetrage = new ToolStripMenuItem("Retrage persoană");
            miRetrage.Click += (s, ev) => RetragePersoanaSelectata();
            ctxCursanti.Items.Add(miRetrage);
            var miReactiveazaCtx = new ToolStripMenuItem("Reactivează persoană");
            miReactiveazaCtx.Click += (s, ev) => ReactiveazaPersoanaSelectata();
            ctxCursanti.Items.Add(miReactiveazaCtx);
            m_gContracte.ContextMenuStrip = ctxCursanti;

            // Adaugă opțiunea de ștergere la context menu-ul existent din Designer
            m_contextAbonamente.Items.Add(new ToolStripSeparator());
            var miStergeAb = new ToolStripMenuItem("Șterge abonament selectat");
            miStergeAb.Click += (s, ev) => StergeAbonamentDinContract();
            m_contextAbonamente.Items.Add(miStergeAb);

            // Colorează în gri persoanele retrase
            m_gContracte.CellFormatting += (s, ev) =>
            {
                if (ev.RowIndex < 0) return;
                try
                {
                    object? retrasVal = m_gContracte.Rows[ev.RowIndex].Cells["CUCC_Retras"].Value;
                    bool retras = retrasVal != null && retrasVal != DBNull.Value && Convert.ToBoolean(retrasVal);
                    if (retras)
                    {
                        ev.CellStyle!.ForeColor = System.Drawing.Color.Gray;
                        ev.CellStyle.BackColor = System.Drawing.Color.WhiteSmoke;
                    }
                }
                catch { }
            };

            Modifica();
            m_lFormaDeschisa = 1;
        }

        private void m_gContracte_DoubleClick(object sender, EventArgs e)
        {
            if (m_lFormaDeschisa <= 0)
                return;

            object? vCUCC = m_gContracte.GetColumnData("CUCC_Id");

            if (vCUCC == null)//INSERT
            {
                bool succesSalvare = Salveaza();
                if (!succesSalvare) return;
                m_tsbSalveaza.Enabled = false;

                FrmAjutorCursanti frm = new(FrmParent, ManagerDeProiect, m_lCUCO);

                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    long lCUCU = frm.GetCUCU();
                    var q = new CInterogareSQL(ManagerDeProiect.GetConnection());

                    // Persoana deja în acest contract
                    int inAcest = q.ExecuteToIntP(
                        @"SELECT COUNT(*) FROM CU_ContracteCursanti
                          WHERE CUCC_CUCU_Id = @cucu AND CUCC_CUCO_Id = @cuco",
                        CInterogareSQL.P("@cucu", lCUCU),
                        CInterogareSQL.P("@cuco", m_lCUCO));
                    if (inAcest > 0)
                    {
                        MessageBox.Show("Persoana este deja în acest contract.",
                            "Atenție", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        q.Close();
                        return;
                    }

                    // Persoana deja activă în alt contract
                    int inAlt = q.ExecuteToIntP(
                        @"SELECT COUNT(*) FROM CU_ContracteCursanti CC
                          INNER JOIN CU_Contracte CO ON CC.CUCC_CUCO_Id = CO.CUCO_Id
                          WHERE CC.CUCC_CUCU_Id = @cucu
                            AND CC.CUCC_Retras = 0
                            AND CO.CUCO_Retras = 0
                            AND CO.CUCO_Id <> @cuco",
                        CInterogareSQL.P("@cucu", lCUCU),
                        CInterogareSQL.P("@cuco", m_lCUCO));
                    if (inAlt > 0)
                    {
                        MessageBox.Show("Persoana este deja activă într-un alt contract. Reziliați acel contract înainte de a o adăuga.",
                            "Atenție", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        q.Close();
                        return;
                    }

                    m_sSql = string.Format(@" INSERT INTO CU_ContracteCursanti (CUCC_CUCU_Id,CUCC_CUCO_Id)
                                              VALUES ({0},{1})", lCUCU, m_lCUCO);
                    q.ExecuteStatement(m_sSql);
                    IncarcaContracte();
                    q.Close();
                }
            }
            else // deschide cursantul selectat
            {
                object? vCUCU = m_gContracte.GetColumnData("CUCU_Id");
                if (vCUCU == null || vCUCU == DBNull.Value) return;
                int cucuId = Convert.ToInt32(vCUCU);
                if (!FrmGestiuneCursant.PoateDeschide(ManagerDeProiect)) return;
                var frm = new FrmGestiuneCursant(FrmParent, ManagerDeProiect, cucuId)
                { MdiParent = FrmParent };
                frm.Show();
            }
        }
        private void Modifica()
        {
            if (m_lCUCO > 0)
            {
                sSql = string.Format(@"SELECT CUCO_NrContract, CUCO_DataInceputContract, CUCO_DataSfarsitContract,
                                       CUCO_Listat, CUCO_Anulat, ISNULL(CUCO_Retras, 0)
                                       FROM CU_Contracte
                                       WHERE CUCO_Id = {0}", m_lCUCO);
                CInterogareSQL q = new(ManagerDeProiect.GetConnection());
                q.Open(sSql);
                if (q.NextRow())
                {
                    m_ebNrContract.Text              = q.GetIntValue(0).ToString();
                    m_dtDataInceputContract.Value    = (DateTime)q.GetDateValue(1)!;
                    m_dtDataSfarsitContract.Value    = (DateTime)q.GetDateValue(2)!;
                    m_ckListat.Checked               = q.GetBoolValue(3);
                    m_ckAnulat.Checked               = q.GetBoolValue(4);
                    m_ckRetras.Checked               = q.GetBoolValue(5);
                }
                q.Close();
            }

            IncarcaContracte();
            IncarcaAbonamente();
            IncarcaDocumenteEmise();
            IncarcaArhiva();
            IncarcaFacturi();
            m_tsbSalveaza.Enabled = false;
        }

        // =========================================================
        //              TAB ABONAMENTE
        // =========================================================
        private void m_btnAdaugaAbonament_Click(object sender, EventArgs e)
        {
            if (m_lCUCO <= 0)
            {
                MessageBox.Show("Salvați mai întâi contractul înainte de a adăuga abonamente.");
                return;
            }

            // Implicit: ziua de după data de sfârșit din prima linie din grid (sortat DESC = cea mai recentă)
            // Columns[4] = CUAC_DataSfarsit
            DateTime defaultStart = DateTime.Today;
            if (m_gAbonamente.Rows.Count > 0)
            {
                try
                {
                    object? val = m_gAbonamente.Rows[0].Cells[4].Value;
                    if (val != null && val != DBNull.Value)
                        defaultStart = Convert.ToDateTime(val).AddDays(1);
                }
                catch { /* fallback la azi */ }
            }

            m_dtDataInceputAb.Value = defaultStart;
            m_ebPretAb.Text = "";
            m_panelNouAbonament.Visible = true;
            m_dtDataInceputAb.Focus();
        }

        private void m_btnAnuleazaAbonament_Click(object sender, EventArgs e)
        {
            m_panelNouAbonament.Visible = false;
        }

        private void m_dtDataInceputAb_ValueChanged(object sender, EventArgs e)
        {
            RecalculeazaAbonamentAb();
        }

        private void m_cbAbonamentAb_SelectedIndexChanged(object sender, EventArgs e)
        {
            RecalculeazaAbonamentAb();
        }

        private void RecalculeazaAbonamentAb()
        {
            object? idVal = m_cbAbonamentAb.GetColumnData("CUAB_Id");
            if (idVal == null || idVal == DBNull.Value) return;

            string sData = string.Format(" {0} ", CFormatareDate.DateToSQLString(m_dtDataInceputAb.Value));
            string sql = string.Format(@"
SELECT ISNULL(
    (SELECT TOP 1 CUABP_Pret FROM CU_PreturiAbonamente
     WHERE CUABP_CUAB_Id = CUAB_Id
       AND CUABP_DataStart <= {0}
       AND (CUABP_DataSfarsit IS NULL OR CUABP_DataSfarsit >= {0})
     ORDER BY CUABP_DataStart DESC),
    0
) AS PretEfectiv,
       CASE CUAB_MBTD_Id
           WHEN 1 THEN DATEADD(day, -1, DATEADD(hour,  CUAB_Perioada, {0}))
           WHEN 2 THEN DATEADD(day, -1, DATEADD(day,   CUAB_Perioada, {0}))
           WHEN 3 THEN DATEADD(day, -1, DATEADD(week,  CUAB_Perioada, {0}))
           WHEN 4 THEN EOMONTH(DATEADD(month, CUAB_Perioada - 1, {0}))
           WHEN 5 THEN DATEADD(day, -1, DATEADD(year,  CUAB_Perioada, {0}))
           ELSE NULL
       END AS DataCalculata,
       CAST(CUAB_SedintaIndividuala AS INT) AS SedintaIndividuala
  FROM CU_Abonamente WHERE CUAB_Id = {1}", sData, Convert.ToInt32(idVal));

            using var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            try
            {
                q.Open(sql);
                if (q.NextRow())
                {
                    m_ebPretAb.Text = CFormatareDate.DecimalToString(q.GetDecimalValue(0)!.Value);
                    bool sedintaInd = q.GetIntValue(2) == 1;
                    if (sedintaInd)
                    {
                        // For individual sessions: end date is user-defined, default = start date
                        m_dtDataSfarsitAb.Enabled = true;
                        m_dtDataSfarsitAb.Value = m_dtDataInceputAb.Value;
                    }
                    else
                    {
                        m_dtDataSfarsitAb.Enabled = false;
                        var dtSf = q.GetDateValue(1);
                        if (dtSf.HasValue) m_dtDataSfarsitAb.Value = dtSf.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Eroare la calculul prețului.\n\nAsigurați-vă că migration_v3.sql a fost rulat în baza de date.\n\nDetalii: " + ex.Message,
                    "Eroare preț abonament", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void m_btnSalveazaAbonament_Click(object sender, EventArgs e)
        {
            object? idVal = m_cbAbonamentAb.GetColumnData("CUAB_Id");
            if (idVal == null || idVal == DBNull.Value)
            {
                MessageBox.Show("Selectați tipul de abonament.");
                return;
            }

            if (!decimal.TryParse(m_ebPretAb.Text.Replace(",", "."),
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture, out decimal pret))
            {
                MessageBox.Show("Prețul introdus nu este valid.");
                m_ebPretAb.Focus();
                return;
            }

            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            q.BeginTransaction(System.Data.IsolationLevel.Serializable);
            try
            {
                q.ExecuteNonQueryP(@"
INSERT INTO CU_AbonamenteCursant
    (CUAC_CUCO_Id, CUAC_CUAB_Id, CUAC_DataInceput, CUAC_DataSfarsit, CUAC_Pret,
     CUAC_MBUT_Id, CUAC_DataSalvarii)
VALUES (@cuco, @ab, @d1, @d2, @pret, @u, GETDATE());",
                    CInterogareSQL.P("@cuco", m_lCUCO),
                    CInterogareSQL.P("@ab",   Convert.ToInt32(idVal)),
                    CInterogareSQL.P("@d1",   m_dtDataInceputAb.Value),
                    CInterogareSQL.P("@d2",   m_dtDataSfarsitAb.Value),
                    CInterogareSQL.P("@pret", pret),
                    CInterogareSQL.P("@u",    ManagerDeProiect.GetUserId())
                );

                q.CommitTransaction();
            }
            catch (Exception ex)
            {
                q.RollbackTransaction();
                MessageBox.Show("Eroare: " + ex.Message);
                return;
            }
            finally
            {
                q.Close();
            }

            m_panelNouAbonament.Visible = false;
            IncarcaAbonamente();
        }

        private void IncarcaAbonamente()
        {
            if (m_lCUCO <= 0) return;

            m_gAbonamente.SetSql(@"
SELECT ROW_NUMBER() OVER (ORDER BY CUAC_DataInceput DESC) AS Row,
       CUAC_Id,
       CUAB_Descriere,
       CUAC_DataInceput,
       CUAC_DataSfarsit,
       CUAC_Pret
  FROM CU_AbonamenteCursant
  INNER JOIN CU_Abonamente ON CUAC_CUAB_Id = CUAB_Id
 WHERE CUAC_CUCO_Id = @id
 ORDER BY CUAC_DataInceput DESC;",
                CInterogareSQL.P("@id", m_lCUCO));

            var g = m_gAbonamente;
            if (g.Columns.Count < 6) return;
            g.Columns[0].HeaderText = "Nr. crt";   g.Columns[0].Width = 50;
            g.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            g.Columns[1].HeaderText = "CUAC_Id";   g.Columns[1].Visible = false;
            g.Columns[2].HeaderText = "Tip abonament"; g.Columns[2].Width = 280;
            g.Columns[3].HeaderText = "Data început"; g.Columns[3].Width = 110;
            g.Columns[3].DefaultCellStyle.Format = "dd.MM.yyyy";
            g.Columns[4].HeaderText = "Data sfârșit"; g.Columns[4].Width = 110;
            g.Columns[4].DefaultCellStyle.Format = "dd.MM.yyyy";
            g.Columns[5].HeaderText = "Preț (lei)"; g.Columns[5].Width = 100;
            g.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            g.Columns[5].DefaultCellStyle.Format = "N2";

            g.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn c in g.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void StergeAbonamentDinContract()
        {
            object? idVal = m_gAbonamente.GetColumnData("CUAC_Id");
            if (idVal == null || idVal == DBNull.Value) return;

            int cuacId = Convert.ToInt32(idVal);
            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            try
            {
                DateTime dtStart = DateTime.MinValue, dtEnd = DateTime.MaxValue;
                q.OpenP("SELECT CUAC_DataInceput, CUAC_DataSfarsit FROM CU_AbonamenteCursant WHERE CUAC_Id = @id",
                    CInterogareSQL.P("@id", cuacId));
                if (q.NextRow())
                {
                    dtStart = q.GetDateValue(0) ?? DateTime.MinValue;
                    dtEnd   = q.GetDateValue(1) ?? DateTime.MaxValue;
                }

                int nrPrezente = q.ExecuteToIntP(
                    @"SELECT COUNT(*) FROM CU_PrezenteDetaliu
                      WHERE CUPD_CUCO_Id = @cuco AND CUPD_Prezent = 1
                        AND CUPD_DataIntrare >= @d1 AND CUPD_DataIntrare <= @d2",
                    CInterogareSQL.P("@cuco", m_lCUCO),
                    CInterogareSQL.P("@d1", dtStart),
                    CInterogareSQL.P("@d2", dtEnd));

                if (nrPrezente > 0)
                {
                    MessageBox.Show(
                        $"Abonamentul are {nrPrezente} prezență/prezențe înregistrate în această perioadă și nu poate fi șters.",
                        "Atenție", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Sigur doriți să ștergeți abonamentul selectat?", "Confirmare",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                q.ExecuteNonQueryP("DELETE FROM CU_AbonamenteCursant WHERE CUAC_Id = @id",
                    CInterogareSQL.P("@id", cuacId));
            }
            finally { q.Close(); }

            IncarcaAbonamente();
        }

        private void IncarcaContracte()
        {
            m_sSql = string.Format(@"SELECT ROW_NUMBER() OVER (ORDER BY CUCU_Nume, CUCU_Prenume),
                                     CUCC_Id, CUCU_Id, CUCU_Nume, CUCU_Prenume, CUCU_NrMatricol, CUCU_CNP,
                                     CUCC_Retras, CUCC_DataRetragere
                                     FROM CU_ContracteCursanti
                                     INNER JOIN CU_Cursanti ON CUCC_CUCU_Id = CUCU_Id
                                     INNER JOIN CU_Contracte ON CUCC_CUCO_Id = CUCO_Id
                                     WHERE CUCC_CUCO_Id = {0}
                                     ORDER BY CUCU_Nume,CUCU_Prenume", m_lCUCO);
            m_gContracte.SetSql(m_sSql);

            m_gContracte.FormatColumn("Nr. crt", 50, colAlign: DataGridViewContentAlignment.MiddleRight);
            m_gContracte.FormatColumn("CUCC_Id", 0, bVisible: false);
            m_gContracte.FormatColumn("CUCU_Id", 0, bVisible: false);
            m_gContracte.FormatColumn("Nume", 200);
            m_gContracte.FormatColumn("Prenume", 200);
            m_gContracte.FormatColumn("Nr. Matricol", 100);
            m_gContracte.FormatColumn("CNP", 250);
            m_gContracte.FormatColumn("Retras", 55);
            m_gContracte.FormatColumn("Data retragere", 100, null, null, "dd.MM.yyyy");

            m_gContracte.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (DataGridViewColumn column in m_gContracte.Columns)
            {
                m_gContracte.Columns[column.Name]!.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            if (m_gContracte.Rows.Count > 0)
            {
                m_gContracte.ClearSelection();
                int nRowIndex = m_gContracte.Rows.Count - 1;

                m_gContracte.Rows[nRowIndex].Selected = true;
                m_gContracte.FirstDisplayedScrollingRowIndex = nRowIndex;
                m_gContracte.CurrentCell = m_gContracte.Rows[nRowIndex].Cells[0];
            }
        }

        private bool Salveaza()
        {
            string sData, sSql, sUpdate = "", sTmp,
                   sDataInceputContract, sDataSfarsitContract, sNrContract;

            sData = m_dtDataInceputContract.Text;
            if (sData == "")
            {
                CFormatareDate.ShowWarning("IDS_ERR_DATACONTRACT");
                m_dtDataInceputContract.Focus();
                return false;
            }
            else
            {
                sDataInceputContract = string.Format(" {0} ", CFormatareDate.DateToSQLString(m_dtDataInceputContract.Value));
                sTmp = string.Format("CUCO_DataInceputContract = {0}", CFormatareDate.DateToSQLString(m_dtDataInceputContract.Value));
                sUpdate += sTmp;
            }

            sData = m_dtDataSfarsitContract.Text;
            if (sData == "")
            {
                CFormatareDate.ShowWarning("IDS_ERR_DATACONTRACT");
                m_dtDataSfarsitContract.Focus();
                return false;
            }
            else
            {
                sDataSfarsitContract = string.Format(" {0} ", CFormatareDate.DateToSQLString(m_dtDataSfarsitContract.Value));
                sTmp = string.Format(", CUCO_DataSfarsitContract = {0}", CFormatareDate.DateToSQLString(m_dtDataSfarsitContract.Value));
                sUpdate += sTmp;
            }

            sData = m_ebNrContract.Text;
            if (m_lCUCO > 0 && sData == "")
            {
                CFormatareDate.ShowWarning("IDS_ERR_NRCONTRACT");
                m_ebNrContract.Focus();
                return false;
            }
            else if (m_lCUCO > 0)
            {
                sNrContract = string.Format("{0}", CFormatareDate.StringToSqlString(sData));
                sTmp = string.Format(", CUCO_NrContract = {0}", CFormatareDate.StringToSqlString(sData));
                sUpdate += sTmp;
            }

            sTmp = string.Format(" ,CUCO_Anulat = {0}", Convert.ToInt32(m_ckAnulat.Checked));
            sUpdate += sTmp;
            sTmp = string.Format(" ,CUCO_Retras = {0}", Convert.ToInt32(m_ckRetras.Checked));
            sUpdate += sTmp;


            if (m_lCUCO == 0)
            {
                var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
                try
                {
                    q.BeginTransaction(System.Data.IsolationLevel.Serializable);

                    int nrContract = CGeneratorNumarContract.Next(q);
                    int nrIntern = CGeneratorNumarIntern.Next(q);

                    m_lCUCO = q.ExecuteToIntPThrow(@"
INSERT INTO CU_Contracte
    (CUCO_MBUT_Id, CUCO_NrContract, CUCO_DataInceputContract,
     CUCO_DataSfarsitContract, CUCO_DataSalvareContract)
OUTPUT INSERTED.CUCO_Id
VALUES (@u, @nr, @d1, @d2, GETDATE());",
                        CInterogareSQL.P("@u",  ManagerDeProiect.GetUserId()),
                        CInterogareSQL.P("@nr", nrContract),
                        CInterogareSQL.P("@d1", m_dtDataInceputContract.Value),
                        CInterogareSQL.P("@d2", m_dtDataSfarsitContract.Value)
                    );

                    if (m_lCUCO <= 0)
                    {
                        q.RollbackTransaction();
                        MessageBox.Show("Contractul nu a putut fi creat (OUTPUT INSERTED.CUCO_Id = 0).",
                            "Eroare creare contract", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    q.ExecuteNonQueryP(@"
INSERT INTO AD_RegistruNumereInterne
    (ADRN_Numar, ADRN_Data, ADRN_ADTD_Id, ADRN_Iesire, ADRN_MBDV_Id,
     ADRN_CUCO_Id, ADRN_Descriere, ADRN_MBUT_Id, ADRN_DataSalvarii)
VALUES
    (@nr, GETDATE(), 1, 1, @div,
     @cuco, @desc, @u, GETDATE());",
                        CInterogareSQL.P("@nr",   nrIntern),
                        CInterogareSQL.P("@div",  ManagerDeProiect.GetDiviziune()),
                        CInterogareSQL.P("@cuco", m_lCUCO),
                        CInterogareSQL.P("@desc", $"Contract Pregătire Sportivă nr. {nrContract}"),
                        CInterogareSQL.P("@u",    ManagerDeProiect.GetUserId())
                    );

                    q.CommitTransaction();
                    m_ebNrContract.Text = nrContract.ToString();
                }
                catch (Exception ex)
                {
                    q.RollbackTransaction();
                    MessageBox.Show("Eroare la salvare contract: " + ex.Message);
                    m_lCUCO = 0;
                    return false;
                }
                finally
                {
                    q.Close();
                }
            }
            else
            {
                CInterogareSQL qUpdate = new(ManagerDeProiect.GetConnection());
                //sSql = string.Format(@" SELECT COUNT(*) FROM CU_Contracte WHERE CUCO_NrContract = {0}", sNrContract);
                //int nCount = qUpdate.ExecuteToInt(sSql);
                //if (nCount > 0)
                //{
                //    CFormatareDate.ShowWarning("IDS_ERR_NRCONTRACT_EXISTA");
                //    return false;
                //}

                sTmp = string.Format(", CUCO_MBUT_Id = {0}, CUCO_DataSalvareContract=GETDATE()", ManagerDeProiect.GetUserId());
                sUpdate += sTmp;
                sSql = string.Format(@"UPDATE CU_Contracte SET {0} WHERE CUCO_Id = {1}", sUpdate, m_lCUCO);

                qUpdate.ExecuteStatement(sSql);

                qUpdate.Close();
            }

            #region Istoric
            /*
                        //ISTORIC
                        sSql = string.Format(@" INSERT INTO CU_CursantiIstoric
                                               (CUCU_MBDV_Id, CUCU_CNP, CUCU_DataNasterii, CUCU_Nume, CUCU_Prenume, CUCU_CISerie,
                                                CUCU_CINumar, CUCU_Strada, CUCU_NumarStrada, CUCU_Bloc, CUCU_Scara, CUCU_Apartament, CUCU_Domiciliu, CUCU_PrenumeMama, CUCU_PrenumeTata,
                                                CUCU_TelefonMobil, CUCU_TelefonFix, CUCU_Email, CUCU_Email2, CUCU_Oras, CUCU_Judet, CUCU_NrMatricol, CUCU_InitialaMama, 
                                                CUCU_InitialaTata, CUCU_MBST_Id, CUCU_LocalitateaNasterii, CUCU_JudetulNasterii, CUCU_MBUT_Id,CUCU_DataModificarii,CUCU_CUCU_Modificat,
                                                CUCU_ActeDosar_CI,CUCU_ActeDosar_CertificatNastere,CUCU_ActeDosar_FemeieCasatorita,CUCU_ActeDosar_CertificatCasatorie,
                                                CUCU_ActeDosar_UltimaDiplomaStudii,CUCU_ActeDosar_AdeverintaMedicala,CUCU_CetateanStrain,
                                                CUCU_DispProgram_Oricand,CUCU_DispProgram_Dimineata,CUCU_DispProgram_Dupamasa,CUCU_DispProgram_Sambata,CUCU_DispProgram_OSaptDimOSaptDupamasa,CUCU_NumeFacebook,CUCU_NumeSkype,CUCU_TelefonMobilInternational,CUCU_DispProgram_Altele,CUCU_AltProgramCursant,CUCU_ParolaApp)
                                            SELECT CUCU_MBDV_Id, CUCU_CNP, CUCU_DataNasterii, CUCU_Nume, CUCU_Prenume, CUCU_CISerie, CUCU_CINumar, CUCU_Strada, 
                                                   CUCU_NumarStrada, CUCU_Bloc, CUCU_Scara, CUCU_Apartament, CUCU_Domiciliu, CUCU_PrenumeMama, CUCU_PrenumeTata, CUCU_TelefonMobil, 
                                                   CUCU_TelefonFix, CUCU_Email, CUCU_Email2, CUCU_Oras, CUCU_Judet, CUCU_NrMatricol, CUCU_InitialaMama, CUCU_InitialaTata, 
                                                   CUCU_MBST_Id, CUCU_LocalitateaNasterii, CUCU_JudetulNasterii, {0},GetDate(),{1},
                                                CUCU_ActeDosar_CI,CUCU_ActeDosar_CertificatNastere,CUCU_ActeDosar_FemeieCasatorita,CUCU_ActeDosar_CertificatCasatorie,
                                                CUCU_ActeDosar_UltimaDiplomaStudii,CUCU_ActeDosar_AdeverintaMedicala,CUCU_CetateanStrain,
                                                CUCU_DispProgram_Oricand,CUCU_DispProgram_Dimineata,CUCU_DispProgram_Dupamasa,CUCU_DispProgram_Sambata,CUCU_DispProgram_OSaptDimOSaptDupamasa,CUCU_NumeFacebook,CUCU_NumeSkype,CUCU_TelefonMobilInternational,CUCU_DispProgram_Altele,CUCU_AltProgramCursant,CUCU_ParolaApp
                                            FROM CU_Cursanti
                                            WHERE CUCU_Id={2}", ManagerDeProiect.GetUserId(), m_lCUCU, m_lCUCU);
                        CInterogareSQL qIstoric = new CInterogareSQL(ManagerDeProiect.GetConnection());
                        qIstoric.ExecuteStatement(sSql);
                        qIstoric.Close();

                        //SALVARE CA SI PARTENER
                        int lADPA = 0, lADPG = 0, lExistaCNPCaSiContClient = 0;
                        sSql = string.Format(@"SELECT ISNULL(ADPA_Id,0),ISNULL(ADPA_ADPG_Id,0)  FROM AD_Parteneri WHERE ADPA_CUCU_Id ={0}", m_lCUCU);
                        {
                            CInterogareSQL q = new CInterogareSQL(ManagerDeProiect.GetConnection());
                            q.Open(sSql);
                            if (q.NextRow())
                            {
                                lADPA = q.GetIntValue(0);
                                lADPG = q.GetIntValue(1);
                            }

                            //RECUPERAM PARTENERUL CLIENT DUPA CNP DACA NU EXISTA DUPA CURSANT(ADPA_CUCU_Id=NULL)!!!!!
                            if (lADPG == 0)
                            {
                                sSql = string.Format(@"SELECT ISNULL(ADPA_Id,0),ISNULL(ADPA_ADPG_Id,0),*  FROM AD_Parteneri WHERE ADPA_CUI = (SELECT CUCU_CNP FROM CU_Cursanti WHERE CUCU_Id={0})
                                                        AND ADPA_MBTC_Id=2", m_lCUCU);
                                {
                                    q.Open(sSql);
                                    if (q.NextRow())
                                    {
                                        lADPA = q.GetIntValue(0);
                                        lADPG = q.GetIntValue(1);

                                        if (lADPG > 0)
                                        {
                                            lExistaCNPCaSiContClient = 1;
                                        }
                                    }
                                }
                            }
                            q.Close();
                        }



                        if (lADPG > 0)
                        {
                            CInterogareSQL qUpdate = new CInterogareSQL(ManagerDeProiect.GetConnection());

                            if (lExistaCNPCaSiContClient > 0)
                            {
                                sSql = string.Format(@"UPDATE AD_ParteneriGenerali SET ADPG_CUI = (SELECT CUCU_CNP FROM CU_Cursanti WHERE CUCU_Id={1}),
                                                                                       ADPG_CUCU_Id = {1}
                                                   WHERE ADPG_Id = {0}", lADPG, m_lCUCU);
                                qUpdate.ExecuteStatement(sSql);

                                sSql = string.Format(@"UPDATE AD_Parteneri SET ADPA_CUI = (SELECT CUCU_CNP FROM CU_Cursanti WHERE CUCU_Id={1}),
                                                                               ADPA_CUCU_Id={1}
                                                   WHERE ADPA_Id = {0} AND ADPA_MBTC_Id=2", lADPA, m_lCUCU);
                                qUpdate.ExecuteStatement(sSql);
                            }

                            sSql = string.Format(@"UPDATE AD_ParteneriGenerali SET ADPG_Denumire= CUCU_Nume+' '+CUCU_Prenume,
                                                                                    ADPG_Strada=CUCU_Strada,
                                                                                    ADPG_Numar=CUCU_NumarStrada,
                                                                                    ADPG_Bloc=CUCU_Bloc,
                                                                                    ADPG_Scara=CUCU_Scara,
                                                                                    ADPG_Apartament=CUCU_Apartament,
                                                                                    ADPG_Localitate=CUCU_Oras,
                                                                                    ADPG_Judet=CUCU_Judet,
                                                                                    ADPG_CUI=CUCU_CNP,
                                                                                    ADPG_Telefon=CUCU_TelefonFix,
                                                                                    ADPG_TelefonMobil=CUCU_TelefonMobil,
                                                                                    ADPG_Email=CUCU_Email,
                                                                                    ADPG_Email2=CUCU_Email2,
                                                                                    ADPG_TelefonMobilInternational=CUCU_TelefonMobilInternational
                                                    FROM CU_Cursanti
                                                    WHERE ADPG_CUCU_Id=CUCU_Id AND ADPG_Id = {0}", lADPG);
                            qUpdate.ExecuteStatement(sSql);

                            sSql = string.Format(@"UPDATE AD_Parteneri SET ADPA_Denumire= CUCU_Nume+' '+CUCU_Prenume,
                                                                            ADPA_Strada=CUCU_Strada,
                                                                            ADPA_Numar=CUCU_NumarStrada,
                                                                            ADPA_Bloc=CUCU_Bloc,
                                                                            ADPA_Scara=CUCU_Scara,
                                                                            ADPA_Apartament=CUCU_Apartament,
                                                                            ADPA_Localitate=CUCU_Oras,
                                                                            ADPA_Judet=CUCU_Judet,
                                                                            ADPA_CUI=CUCU_CNP,
                                                                            ADPA_Telefon=CUCU_TelefonFix,
                                                                            ADPA_TelefonMobil=CUCU_TelefonMobil,
                                                                            ADPA_Email=CUCU_Email,
                                                                            ADPA_Email2=CUCU_Email2
                                                    FROM CU_Cursanti 
                                                    WHERE ADPA_CUCU_Id=CUCU_Id AND ADPA_ADPG_Id = {0}", lADPG);

                            qUpdate.ExecuteStatement(sSql);

                            qUpdate.Close();
                        }
                        else
                        {
                            lADPG = 0;
                            CInterogareSQL qIns = new CInterogareSQL(ManagerDeProiect.GetConnection());
                            sSql = string.Format(@"INSERT INTO AD_ParteneriGenerali (ADPG_MBDV_Id,ADPG_MBTA_Id,ADPG_MBTP_Id,ADPG_CUCU_Id,
                                                                             ADPG_Denumire,ADPG_Strada,ADPG_Numar,ADPG_Bloc,ADPG_Scara,ADPG_Apartament,ADPG_Localitate,ADPG_Judet,
                                                                             ADPG_CUI,ADPG_Telefon,ADPG_TelefonMobil,ADPG_Email,ADPG_Email2,ADPG_MBUT_Id,ADPG_DataSalvarii,ADPG_MBCP_Id,ADPG_TelefonMobilInternational)
                                                    SELECT CUCU_MBDV_Id,1,2,{0},
                                                    CUCU_Nume+' '+CUCU_Prenume,CUCU_Strada,CUCU_NumarStrada,CUCU_Bloc,CUCU_Scara,CUCU_Apartament,CUCU_Oras,CUCU_Judet,CUCU_CNP,CUCU_TelefonFix,
                                                    CUCU_TelefonMobil,CUCU_Email,CUCU_Email2,{1},GETDATE(),15,CUCU_TelefonMobilInternational
                                                    FROM CU_Cursanti 
                                                    WHERE CUCU_Id = {2}", m_lCUCU, ManagerDeProiect.GetUserId(), m_lCUCU);

                            qIns.ExecuteStatement(sSql);

                            sSql = "SELECT SCOPE_IDENTITY() AS SID";
                            lADPG = qIns.ExecuteToInt(sSql);

                            sSql = string.Format(@"INSERT INTO AD_Parteneri (ADPA_MBDV_Id,ADPA_MBTA_Id,ADPA_MBTC_Id,ADPA_MBTP_Id,ADPA_CUCU_Id,
                                                                             ADPA_Denumire,ADPA_Strada,ADPA_Numar,ADPA_Bloc,ADPA_Scara,ADPA_Apartament,ADPA_Localitate,ADPA_Judet,ADPA_CUI,
                                                                             ADPA_Telefon,ADPA_TelefonMobil,ADPA_Email,ADPA_Email2,ADPA_ADPG_Id,ADPA_MBUT_Id,ADPA_DataSalvarii,ADPA_MBPS_Id)
                                                    SELECT CUCU_MBDV_Id,1,2,2,{0},
                                                    CUCU_Nume+' '+CUCU_Prenume,CUCU_Strada,CUCU_NumarStrada,CUCU_Bloc,CUCU_Scara,CUCU_Apartament,CUCU_Oras,CUCU_Judet,CUCU_CNP,CUCU_TelefonFix,
                                                    CUCU_TelefonMobil,CUCU_Email,CUCU_Email2,{1},{2},GETDATE(),2
                                                    FROM CU_Cursanti 
                                                    WHERE CUCU_Id = {2}", m_lCUCU, lADPG, m_lCUCU, ManagerDeProiect.GetUserId());

                            qIns.ExecuteStatement(sSql);

                            qIns.Close();
                        }*/
            #endregion

            // Actualizăm descrierea registrului cu beneficiarii curenți (poate fi apelat repetat pe același contract)
            {
                var qDesc = new CInterogareSQL(ManagerDeProiect.GetConnection());
                qDesc.ExecuteNonQueryP(@"
UPDATE ADRN SET ADRN_Descriere =
    N'Contract Pregătire Sportivă nr. ' + CAST(C.CUCO_NrContract AS NVARCHAR(20)) +
    ISNULL(N' - ' + Ben.Beneficiari, N'')
FROM AD_RegistruNumereInterne ADRN
INNER JOIN CU_Contracte C ON ADRN.ADRN_CUCO_Id = C.CUCO_Id
OUTER APPLY (
    SELECT STUFF((SELECT N', ' + CU2.CUCU_Nume + N' ' + CU2.CUCU_Prenume
                    FROM CU_ContracteCursanti CC2
                    INNER JOIN CU_Cursanti CU2 ON CC2.CUCC_CUCU_Id = CU2.CUCU_Id
                   WHERE CC2.CUCC_CUCO_Id = C.CUCO_Id
                     AND ISNULL(CC2.CUCC_Retras, 0) = 0
                   FOR XML PATH(''), TYPE).value('.','NVARCHAR(MAX)'), 1, 2, N'') AS Beneficiari
) AS Ben
WHERE ADRN.ADRN_CUCO_Id = @cuco AND ADRN.ADRN_ADTD_Id = 1",
                    CInterogareSQL.P("@cuco", m_lCUCO));
                qDesc.Close();
            }

            IncarcaDocumenteEmise();
            return true;
        }

        private void IncarcaDocumenteEmise()
        {
            if (m_lCUCO <= 0) return;

            var sql = @"
SELECT ADRN_Numar        AS [Nr. înreg.],
       ADRN_Data         AS [Data],
       ADTD_Denumire     AS [Tip document],
       ADRN_Descriere    AS [Descriere]
  FROM AD_RegistruNumereInterne
 INNER JOIN AD_TipuriDocumenteNumarIntern ON ADRN_ADTD_Id = ADTD_Id
 WHERE ADRN_CUCO_Id = @cuco
 ORDER BY ADRN_Numar;";

            m_gDocumenteEmise.SetSql(sql, CInterogareSQL.P("@cuco", m_lCUCO));

            m_gDocumenteEmise.FormatColumn("Nr. înreg.", 80, true, DataGridViewContentAlignment.MiddleRight);
            m_gDocumenteEmise.FormatColumn("Data", 90, true, null, "dd.MM.yyyy");
            m_gDocumenteEmise.FormatColumn("Tip document", 200);
            m_gDocumenteEmise.FormatColumn("Descriere", 700);

            foreach (DataGridViewColumn c in m_gDocumenteEmise.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        // =========================================================
        //   ARHIVĂ
        // =========================================================
        private void ArhiveazaFisierGenerat(string caleFisier, int adrnId)
        {
            if (m_lCUCO <= 0) return;
            try
            {
                var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
                int exists = q.ExecuteToIntP(
                    "SELECT COUNT(*) FROM AD_Arhiva WHERE ADAH_Cale = @cale",
                    CInterogareSQL.P("@cale", caleFisier));
                if (exists == 0)
                    q.ExecuteNonQueryP(
                        @"INSERT INTO AD_Arhiva (ADAH_Cale, ADAH_NumeFisier, ADAH_DataAdaugare, ADAH_CUCO_Id, ADAH_ADRN_Id, ADAH_MBDV_Id)
                          VALUES (@cale, @nume, GETDATE(), @cuco, @adrn, @div)",
                        CInterogareSQL.P("@cale", caleFisier),
                        CInterogareSQL.P("@nume", Path.GetFileName(caleFisier)),
                        CInterogareSQL.P("@cuco", m_lCUCO),
                        CInterogareSQL.P("@adrn", adrnId > 0 ? (object)adrnId : DBNull.Value),
                        CInterogareSQL.P("@div",  ManagerDeProiect.GetDiviziune()));
                else
                    q.ExecuteNonQueryP(
                        @"UPDATE AD_Arhiva
                             SET ADAH_CUCO_Id = ISNULL(ADAH_CUCO_Id, @cuco),
                                 ADAH_ADRN_Id = ISNULL(ADAH_ADRN_Id, @adrn)
                           WHERE ADAH_Cale = @cale",
                        CInterogareSQL.P("@cuco", m_lCUCO),
                        CInterogareSQL.P("@adrn", adrnId > 0 ? (object)adrnId : DBNull.Value),
                        CInterogareSQL.P("@cale", caleFisier));
                q.Close();
                IncarcaArhiva();
            }
            catch { /* archive failure should not disrupt document generation */ }
        }

        private void IncarcaArhiva()
        {
            if (m_lCUCO <= 0) return;
            var sql = @"
SELECT ADAH_Id,
       ISNULL(ADAH_NumeFisier, RIGHT(ADAH_Cale, LEN(ADAH_Cale) - LEN(LEFT(ADAH_Cale, LEN(ADAH_Cale) - CHARINDEX('\', REVERSE(ADAH_Cale)))))) AS [Fișier],
       ADAH_DataAdaugare AS [Data],
       ADAH_Cale         AS [Cale]
  FROM AD_Arhiva
 WHERE ADAH_CUCO_Id = @cuco
    OR ADAH_ADRN_Id IN (SELECT ADRN_Id FROM AD_RegistruNumereInterne WHERE ADRN_CUCO_Id = @cuco)
 ORDER BY ADAH_DataAdaugare DESC, ADAH_Id DESC;";
            m_gArhiva.SetSql(sql, CInterogareSQL.P("@cuco", m_lCUCO));
            m_gArhiva.FormatColumn("ADAH_Id", 0, false);
            m_gArhiva.FormatColumn("Fișier", 350);
            m_gArhiva.FormatColumn("Data", 110, true, null, "dd.MM.yyyy HH:mm");
            m_gArhiva.FormatColumn("Cale", 500);
            foreach (DataGridViewColumn c in m_gArhiva.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void m_btnDeschideArhiva_Click(object sender, EventArgs e)
        {
            object? val = m_gArhiva.GetColumnData("Cale");
            if (val == null || val == DBNull.Value) return;
            string cale = val.ToString()!;
            if (!File.Exists(cale)) { MessageBox.Show("Fișierul nu mai există la calea: " + cale); return; }
            Process.Start(new ProcessStartInfo(cale) { UseShellExecute = true });
        }

        private void m_btnAdaugaArhiva_Click(object sender, EventArgs e)
        {
            if (m_lCUCO <= 0) { MessageBox.Show("Salvați mai întâi contractul."); return; }
            using var dlg = new OpenFileDialog { Title = "Selectați fișierul de arhivat", Filter = "Toate fișierele|*.*" };
            if (dlg.ShowDialog() != DialogResult.OK) return;

            string src = dlg.FileName;
            var qFolder = new CInterogareSQL(ManagerDeProiect.GetConnection());
            string? numeCuCo = qFolder.ExecuteToStringP(
                @"SELECT TOP 1 CUCU_Nume + '_' + CUCU_Prenume FROM CU_ContracteCursanti
                  INNER JOIN CU_Cursanti ON CUCC_CUCU_Id = CUCU_Id
                  WHERE CUCC_CUCO_Id = @cuco ORDER BY CUCU_Nume, CUCU_Prenume",
                CInterogareSQL.P("@cuco", m_lCUCO));
            qFolder.Close();
            string numeFolderCo = FaraDiacritice($"{m_ebNrContract.Text.Trim()}_{numeCuCo ?? m_lCUCO.ToString()}");
            string folder = Path.Combine(@"d:\WORK\ONIXTEIA\ACTE\ARHIVA\CONTRACTE", numeFolderCo);
            Directory.CreateDirectory(folder);
            string dest = Path.Combine(folder, Path.GetFileName(src));
            if (File.Exists(dest))
            {
                if (MessageBox.Show("Fișierul există deja. Suprascrieți?", "Atenție", MessageBoxButtons.YesNo) == DialogResult.No) return;
            }
            File.Copy(src, dest, true);
            ArhiveazaFisierGenerat(dest, 0);
        }

        private void m_btnStergeArhiva_Click(object sender, EventArgs e)
        {
            object? val = m_gArhiva.GetColumnData("ADAH_Id");
            if (val == null || val == DBNull.Value) return;
            int id = Convert.ToInt32(val);
            if (MessageBox.Show("Ștergeți înregistrarea din arhivă? (fișierul de pe disc nu va fi șters)", "Atenție",
                    MessageBoxButtons.YesNo) == DialogResult.No) return;
            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            q.ExecuteNonQueryP("DELETE FROM AD_Arhiva WHERE ADAH_Id = @id", CInterogareSQL.P("@id", id));
            q.Close();
            IncarcaArhiva();
        }

        private void m_tsbDeschideArhiva_Click(object sender, EventArgs e)
        {
            if (m_lCUCO <= 0) return;
            m_tabControl.SelectedTab = m_tabPageArhiva;
        }

        private void IncarcaFacturi()
        {
            if (m_lCUCO <= 0) return;
            m_gFacturi.SetSql(@"
SELECT F.FVFA_Id,
       ISNULL(F.FVFA_NrFactura, '') AS [Nr. factura],
       F.FVFA_DataEmitere AS [Data emitere],
       CONVERT(varchar(10), F.FVFA_PerioadaInceput, 104) + ' — ' + CONVERT(varchar(10), F.FVFA_PerioadaSfarsit, 104) AS [Perioada],
       CAST(ISNULL(F.FVFA_Suma, 0) AS decimal(18,2)) AS [Suma],
       CAST(ISNULL(Inc.TotalIncasat, 0) AS decimal(18,2)) AS [Incasat],
       CASE
           WHEN F.FVFA_ADRN_Id IS NOT NULL                              THEN 'Sponsorizare'
           WHEN ISNULL(Inc.NrIncasari, 0) = 0                           THEN 'Neincasata'
           WHEN Inc.TotalIncasat < ISNULL(F.FVFA_Suma, 0)              THEN 'Partial'
           WHEN Inc.UltimTipPlata = 1                                   THEN 'Cash'
           WHEN Inc.UltimTipPlata = 2                                   THEN 'OP'
           ELSE 'Incasata'
       END AS [Status],
       ISNULL(F.FVFA_NrDocument, '') AS [Nr. document]
  FROM FV_Facturi F
  OUTER APPLY (
      SELECT ISNULL(SUM(I.CUIN_Valoare), 0) AS TotalIncasat,
             COUNT(*)                        AS NrIncasari,
             MAX(I.CUIN_TipPlata)            AS UltimTipPlata
        FROM CU_Incasari I WHERE I.CUIN_FVFA_Id = F.FVFA_Id
  ) AS Inc
 WHERE F.FVFA_CUCO_Id = @cuco
 ORDER BY F.FVFA_PerioadaInceput DESC",
                CInterogareSQL.P("@cuco", m_lCUCO));
            m_gFacturi.FormatColumn("FVFA_Id",     0,   false);
            m_gFacturi.FormatColumn("Nr. factura", 100);
            m_gFacturi.FormatColumn("Data emitere",100, true, null, "dd.MM.yyyy");
            m_gFacturi.FormatColumn("Perioada",    180);
            m_gFacturi.FormatColumn("Suma",         90, true, DataGridViewContentAlignment.MiddleRight);
            m_gFacturi.FormatColumn("Incasat",      90, true, DataGridViewContentAlignment.MiddleRight);
            m_gFacturi.FormatColumn("Status",      110, true, DataGridViewContentAlignment.MiddleCenter);
            m_gFacturi.FormatColumn("Nr. document",110);
            foreach (DataGridViewColumn c in m_gFacturi.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void m_btnAdaugaFactura_Click(object sender, EventArgs e)
        {
            if (m_lCUCO <= 0) { MessageBox.Show("Salvați mai întâi contractul."); return; }
            if (!FrmGestiuneFactura.PoateDeschide(ManagerDeProiect)) return;
            using var frm = new FrmGestiuneFactura(ManagerDeProiect, m_lCUCO);
            if (frm.ShowDialog(this) == DialogResult.OK)
                IncarcaFacturi();
        }

        private void m_btnModificaFactura_Click(object sender, EventArgs e)
        {
            object? val = m_gFacturi.GetColumnData("FVFA_Id");
            if (val == null || val == DBNull.Value) return;
            if (!FrmGestiuneFactura.PoateDeschide(ManagerDeProiect)) return;
            using var frm = new FrmGestiuneFactura(ManagerDeProiect, m_lCUCO, Convert.ToInt32(val));
            if (frm.ShowDialog(this) == DialogResult.OK)
                IncarcaFacturi();
        }

        private void m_btnStergeFactura_Click(object sender, EventArgs e)
        {
            object? val = m_gFacturi.GetColumnData("FVFA_Id");
            if (val == null || val == DBNull.Value) return;
            if (MessageBox.Show("Ștergeți factura selectată?", "Atenție", MessageBoxButtons.YesNo) == DialogResult.No) return;
            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            q.ExecuteNonQueryP("DELETE FROM FV_Facturi WHERE FVFA_Id = @id", CInterogareSQL.P("@id", Convert.ToInt32(val)));
            q.Close();
            IncarcaFacturi();
        }

        private void m_gFacturi_DoubleClick(object sender, EventArgs e)
        {
            m_btnModificaFactura_Click(sender, e);
        }

        private void m_ckAnulat_CheckedChanged(object sender, EventArgs e)
        {
            m_tsbSalveaza.Enabled = true;
        }

        private void m_ckRetras_Click(object sender, EventArgs e)
        {
            // Bifa este readonly — se setează doar la generarea notei de reziliere
            m_ckRetras.Checked = !m_ckRetras.Checked;
        }

        private void m_tsbStergePersoana_Click(object sender, EventArgs e)
            => StergePersoanaSelectata();

        private void m_tsbNotaReziliere_Click(object sender, EventArgs e)
        {
            if (m_lCUCO <= 0) return;

            if (m_ckRetras.Checked)
            {
                MessageBox.Show("Contractul este deja reziliat.", "Atenție", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show(
                    "Se va rezilia ÎNTREG contractul.\nToate persoanele vor fi marcate ca retrase.\n\nContinuați?",
                    "Reziliere contract", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            DateTime? dataRet = CereDateRetragere();
            if (!dataRet.HasValue) return;

            ReziliazaContractIntreg(dataRet.Value);
        }

        // =========================================================
        //   Context menu grilă abonamente
        // =========================================================
        private void m_contextAbonamente_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            object? id = m_gAbonamente.GetColumnData("CUAC_Id");
            if (id == null || id == DBNull.Value)
            { e.Cancel = true; return; }

            // Disable act adițional for the first (earliest) subscription period —
            // that period is covered by the original contract document.
            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            q.Open(string.Format(@"
SELECT CASE WHEN CUAC_DataInceput = (
    SELECT MIN(CUAC_DataInceput) FROM CU_AbonamenteCursant WHERE CUAC_CUCO_Id = {0}
) THEN 1 ELSE 0 END
FROM CU_AbonamenteCursant WHERE CUAC_Id = {1}",
                m_lCUCO, Convert.ToInt32(id)));
            bool estePrimaPeriada = q.NextRow() && q.GetIntValue(0) == 1;
            q.Close();

            m_menuItemGenActAditional.Enabled  = !estePrimaPeriada;
        }

        private void m_menuItemGenActAditional_Click(object sender, EventArgs e)
        {
            object? idVal = m_gAbonamente.GetColumnData("CUAC_Id");
            if (idVal == null) return;
            GenereazaActAditional(Convert.ToInt32(idVal));
        }

        private void GenereazaActAditional(int cuacId)
        {
            if (m_lCUCO <= 0) return;

            string tipAbonament = "", nrContract = "", dataContract = "", numeCursanti = "";
            string sSocietate = "", sAdresaSoc = "", sAdresaDiviziune = "", sCUISoc = "";
            string sEmail = "", sTel = "", sContSoc = "", sReprezentantSoc = "";
            string sDateReprezentant = "", sAdresaReprezentant = "";
            DateTime dataInceput = DateTime.Today, dataSfarsit = DateTime.Today;
            decimal pret = 0;
            int nrActAditional = 0;
            int nrCursantiContract = 0;

            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());

            // Date perioadă selectată
            q.Open(string.Format(@"
SELECT ab.CUAB_Descriere, ac.CUAC_DataInceput, ac.CUAC_DataSfarsit, ac.CUAC_Pret
FROM CU_AbonamenteCursant ac
INNER JOIN CU_Abonamente ab ON ac.CUAC_CUAB_Id = ab.CUAB_Id
WHERE ac.CUAC_Id = {0}", cuacId));
            if (!q.NextRow()) { q.Close(); MessageBox.Show("Perioada nu a fost găsită."); return; }
            tipAbonament = q.GetStringValue(0)!;
            dataInceput  = q.GetDateValue(1)!.Value;
            dataSfarsit  = q.GetDateValue(2)!.Value;
            pret         = q.GetDecimalValue(3) ?? 0;
            q.Close();

            // Aplică reducerea de 50% pentru a doua persoană — numărăm doar persoanele ACTIVE (neretrase)
            nrCursantiContract = q.ExecuteToInt(
                string.Format("SELECT COUNT(*) FROM CU_ContracteCursanti WHERE CUCC_CUCO_Id = {0} AND ISNULL(CUCC_Retras, 0) = 0", m_lCUCO));
            if (nrCursantiContract == 2)
                pret = pret + pret / 2m;

            // Nr. și dată contract
            q.Open(string.Format(@"
SELECT CUCO_NrContract, CUCO_DataInceputContract
FROM CU_Contracte WHERE CUCO_Id = {0}", m_lCUCO));
            if (q.NextRow())
            {
                nrContract = q.GetIntValue(0).ToString();
                var dtCo   = q.GetDateValue(1);
                if (dtCo.HasValue) dataContract = dtCo.Value.ToString("dd.MM.yyyy");
            }
            q.Close();

            // Beneficiari — doar persoanele ACTIVE (neretrase)
            q.Open(string.Format(@"
SELECT STUFF((
    SELECT N' și ' + CUCU_Prenume + N' ' + CUCU_Nume
    FROM CU_ContracteCursanti
    INNER JOIN CU_Cursanti ON CUCC_CUCU_Id = CUCU_Id
    WHERE CUCC_CUCO_Id = {0} AND ISNULL(CUCC_Retras, 0) = 0
    FOR XML PATH(''), TYPE
).value('.', 'NVARCHAR(MAX)'), 1, 4, '')", m_lCUCO));
            if (q.NextRow()) numeCursanti = q.GetStringValue(0) ?? "";
            q.Close();

            // Persoane retrase de pe contract (pentru menționare în act adițional)
            var persoaneRetrase = new System.Collections.Generic.List<(string Nume, System.DateTime? DataRetragere)>();
            {
                var qRet = new CInterogareSQL(ManagerDeProiect.GetConnection());
                qRet.OpenP(@"SELECT CUCU_Prenume + ' ' + CUCU_Nume, CUCC_DataRetragere
                              FROM CU_ContracteCursanti
                              INNER JOIN CU_Cursanti ON CUCC_CUCU_Id = CUCU_Id
                              WHERE CUCC_CUCO_Id = @cuco AND ISNULL(CUCC_Retras, 0) = 1
                              ORDER BY CUCC_DataRetragere, CUCU_Nume",
                    CInterogareSQL.P("@cuco", m_lCUCO));
                while (qRet.NextRow())
                    persoaneRetrase.Add((qRet.GetStringValue(0) ?? "", qRet.GetDateValue(1)));
                qRet.Close();
            }

            // Toți cursanții pentru numele fișierului (NUME_PRENUME concatenate, fără diacritice)
            var sbFisierAb = new System.Text.StringBuilder();
            var qFisAb = new CInterogareSQL(ManagerDeProiect.GetConnection());
            qFisAb.OpenP(
                @"SELECT CUCU_Prenume + ' ' + CUCU_Nume
                    FROM CU_ContracteCursanti
                    INNER JOIN CU_Cursanti ON CUCC_CUCU_Id = CUCU_Id
                   WHERE CUCC_CUCO_Id = @cuco AND ISNULL(CUCC_Retras, 0) = 0
                   ORDER BY CUCU_Nume, CUCU_Prenume",
                CInterogareSQL.P("@cuco", m_lCUCO));
            while (qFisAb.NextRow())
            {
                var p = (qFisAb.GetStringValue(0) ?? "").Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string rev = p.Length >= 2
                    ? p[p.Length - 1] + "_" + string.Join("_", p, 0, p.Length - 1)
                    : string.Join("_", p);
                if (sbFisierAb.Length > 0) sbFisierAb.Append('_');
                sbFisierAb.Append(rev);
            }
            qFisAb.Close();
            string numePentruFisierAb = sbFisierAb.Length > 0 ? sbFisierAb.ToString() : "cursant";

            // Date complete societate + diviziune (identic cu generarea contractului)
            q.Open(string.Format(@"
SELECT MBSO_Descriere, MBSO_CUI, MBSO_EmailSocietate, MBSO_TelReprezentant,
       CASE WHEN MBSO_Oras IS NULL OR MBSO_Oras='-' THEN '' ELSE 'loc. ' + UPPER(MBSO_Oras) END +
       CASE WHEN MBSO_Strada IS NULL OR MBSO_Strada='-' THEN '' ELSE ', str. ' + MBSO_Strada END +
       CASE WHEN MBSO_NumarStrada IS NULL OR MBSO_NumarStrada='-' THEN '' ELSE ' nr. ' + MBSO_NumarStrada END +
       CASE WHEN MBSO_Bloc IS NULL OR MBSO_Bloc='-' THEN '' ELSE ', bl. ' + MBSO_Bloc END +
       CASE WHEN MBSO_Scara IS NULL OR MBSO_Scara='-' THEN '' ELSE ', sc. ' + MBSO_Scara END +
       CASE WHEN MBSO_Apartament IS NULL OR MBSO_Apartament='-' THEN '' ELSE ', ap. ' + MBSO_Apartament END
       AS AdresaSocietate,
       CASE WHEN MBDV_Diviziune IS NULL OR MBDV_Diviziune='-' THEN ''
            ELSE 'loc. ' + UPPER(MBDV_Diviziune) END +
       CASE WHEN MBDV_Adresa IS NULL OR MBDV_Adresa='-' THEN ''
            ELSE ', ' + MBDV_Adresa END AS AdresaDiviziune,
       MBSO_Cont,
       UPPER(ISNULL(MBSO_Reprezentant, '')),
       CASE WHEN MBSO_CISerieReprezentant IS NOT NULL AND MBSO_CISerieReprezentant <> '-'
            THEN 'C.I. seria ' + MBSO_CISerieReprezentant + ', nr. ' + ISNULL(MBSO_CINumarReprezentant, '')
            ELSE '' END +
       CASE WHEN MBSO_CNPReprezentant IS NOT NULL AND MBSO_CNPReprezentant <> '-'
            THEN ', CNP ' + MBSO_CNPReprezentant ELSE '' END AS DateReprezentant,
       MBSO_AdresaReprezentant
FROM MB_Societate
INNER JOIN MB_Diviziune ON MBDV_MBSO_Id = MBSO_Id
WHERE MBSO_Id = {0} AND MBDV_Id = {1}", ManagerDeProiect.GetSocietate(), ManagerDeProiect.GetDiviziune()));
            if (q.NextRow())
            {
                sSocietate          = q.GetStringValue(0)!;
                sCUISoc             = q.GetStringValue(1)!;
                sEmail              = q.GetStringValue(2)!;
                sTel                = q.GetStringValue(3)!;
                sAdresaSoc          = q.GetStringValue(4)!;
                sAdresaDiviziune    = q.GetStringValue(5)!;
                sContSoc            = q.GetStringValue(6)!;
                sReprezentantSoc    = q.GetStringValue(7)!;
                sDateReprezentant   = q.GetStringValue(8)!;
                sAdresaReprezentant = q.GetStringValue(9)!;
            }
            q.Close();

            // Nr. act adițional per-contract: câte perioade de abonament există înaintea celei curente
            // Prima perioadă (CUAC_Id minim) = parte din contract, nu act adițional → COUNT = 0, dar meniul e dezactivat pentru ea
            // A 2-a perioadă → COUNT = 1 → Act Adițional nr. 1; a 3-a → 2 → Act Adițional nr. 2 etc.
            nrActAditional = q.ExecuteToIntP(
                @"SELECT COUNT(*) FROM CU_AbonamenteCursant
                  WHERE CUAC_CUCO_Id = @cuco AND CUAC_Id < @cuac",
                CInterogareSQL.P("@cuco", m_lCUCO),
                CInterogareSQL.P("@cuac", cuacId));

            // Nr. intern global: al N-lea (nrActAditional) entry cu ADTD_Id=6 pentru contractul curent, ordonat după ADRN_Numar
            int nrInternGlobal = 0;
            DateTime dataInregistrareAct = DateTime.Today;
            int adrnIdAct = 0;
            if (nrActAditional > 0)
            {
                q.OpenP(
                    @"SELECT ADRN_Numar, ADRN_Data, ADRN_Id
                      FROM (SELECT ADRN_Numar, ADRN_Data, ADRN_Id,
                                   ROW_NUMBER() OVER (ORDER BY ADRN_Numar) AS rn
                              FROM AD_RegistruNumereInterne
                             WHERE ADRN_CUCO_Id = @cuco AND ADRN_ADTD_Id = 6) sub
                     WHERE rn = @seq",
                    CInterogareSQL.P("@cuco", m_lCUCO),
                    CInterogareSQL.P("@seq", nrActAditional));
                if (q.NextRow())
                {
                    nrInternGlobal = q.GetIntValue(0);
                    adrnIdAct      = q.GetIntValue(2);
                }
                q.Close();
                // Data din document = ziua generării, nu data înregistrării din BD
                dataInregistrareAct = DateTime.Today;
            }

            // Dacă nr. intern nu există (perioadă adăugată înainte de auto-creare), îl creăm acum
            if (adrnIdAct == 0 && nrActAditional > 0)
            {
                q.Close();
                var qSeq = new CInterogareSQL(ManagerDeProiect.GetConnection());
                qSeq.BeginTransaction(System.Data.IsolationLevel.Serializable);
                try
                {
                    nrInternGlobal = CGeneratorNumarIntern.Next(qSeq);
                    dataInregistrareAct = DateTime.Today;
                    string descriere = $"Act adițional nr. {nrActAditional} la Contract nr. {nrContract} ({tipAbonament} {dataInceput:dd.MM.yyyy}–{dataSfarsit:dd.MM.yyyy})";
                    adrnIdAct = qSeq.ExecuteToIntPThrow(@"
INSERT INTO AD_RegistruNumereInterne
    (ADRN_Numar, ADRN_Data, ADRN_ADTD_Id, ADRN_Iesire, ADRN_MBDV_Id,
     ADRN_CUCO_Id, ADRN_Descriere, ADRN_MBUT_Id, ADRN_DataSalvarii)
OUTPUT INSERTED.ADRN_Id
VALUES (@nr, GETDATE(), 6, 1, @div, @cuco, @desc, @u, GETDATE());",
                        CInterogareSQL.P("@nr",   nrInternGlobal),
                        CInterogareSQL.P("@div",  ManagerDeProiect.GetDiviziune()),
                        CInterogareSQL.P("@cuco", m_lCUCO),
                        CInterogareSQL.P("@desc", descriere),
                        CInterogareSQL.P("@u",    ManagerDeProiect.GetUserId())
                    );
                    qSeq.CommitTransaction();
                }
                catch (Exception ex)
                {
                    qSeq.RollbackTransaction();
                    MessageBox.Show("Eroare la creare număr intern: " + ex.Message);
                    return;
                }
                finally { qSeq.Close(); }
            }

            string folder = @"d:\WORK\ONIXTEIA\ACTE\CONTRACTE\";
            Directory.CreateDirectory(folder);

            string sActTemplatePath = @"d:\WORK\ONIXTEIA\ACTE\SABLOANE\SablonActAditional.dotx";
            if (!File.Exists(sActTemplatePath))
            {
                MessageBox.Show("Nu am găsit șablonul act adițional:\n" + sActTemplatePath);
                return;
            }

            string numeFisier = Path.Combine(folder, $"ActAditional_{nrContract}_{nrActAditional}_{FaraDiacritice(numePentruFisierAb)}.docx");

            if (File.Exists(numeFisier))
            {
                string mesaj = "Actul adițional a fost deja generat.\n\n" +
                               "• Apăsați DA pentru a DESCHIDE fișierul existent.\n" +
                               "• Apăsați NU pentru a REGENERA actul (cel vechi va fi suprascris).\n" +
                               "• Apăsați CANCEL pentru a anula operațiunea.";
                DialogResult dlg = MessageBox.Show(mesaj, "Act adițional existent", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dlg == DialogResult.Cancel) return;
                if (dlg == DialogResult.Yes)
                {
                    Process.Start(new ProcessStartInfo(numeFisier) { UseShellExecute = true });
                    return;
                }
            }

            try
            {
                File.Copy(sActTemplatePath, numeFisier, true);
                using var wordDoc = WordprocessingDocument.Open(numeFisier, true);
                wordDoc.ChangeDocumentType(WordprocessingDocumentType.Document);
                var mainPart = wordDoc.MainDocumentPart;
                if (mainPart == null) return;
                var body = mainPart.Document.Body!;

                // Păstrează SectionProperties (conține referințele la antet/subsol din șablon)
                var sectPr = body.Elements<SectionProperties>().LastOrDefault();
                var sectPrClone = sectPr != null ? (SectionProperties)sectPr.CloneNode(true) : null;
                body.RemoveAllChildren();

                void AddParagraph(string text, bool bold = false, JustificationValues? align = null, int size = 24)
                {
                    var p = new Paragraph();
                    var pPr = new ParagraphProperties(
                        new Justification { Val = align ?? JustificationValues.Left },
                        new SpacingBetweenLines { After = "100" });
                    p.AppendChild(pPr);
                    var run = new Run();
                    var rPr = new RunProperties(
                        new RunFonts { Ascii = "Calibri", HighAnsi = "Calibri" },
                        new FontSize { Val = size.ToString() });
                    if (bold) rPr.AppendChild(new Bold());
                    run.AppendChild(rPr);
                    run.AppendChild(new Text(text) { Space = SpaceProcessingModeValues.Preserve });
                    p.AppendChild(run);
                    body.AppendChild(p);
                }

                void AddEmptyLine() => body.AppendChild(new Paragraph(
                    new ParagraphProperties(new SpacingBetweenLines { Before = "0", After = "120" })));

                // Paragraf de text justificat
                void AddText(string text, bool bold = false, JustificationValues? align = null, int leftIndent = 0)
                {
                    var p = new Paragraph();
                    var pPr = new ParagraphProperties(
                        new Justification { Val = align ?? JustificationValues.Both },
                        new SpacingBetweenLines { Before = "0", After = "120" });
                    if (leftIndent > 0)
                        pPr.AppendChild(new Indentation { Left = leftIndent.ToString() });
                    p.AppendChild(pPr);
                    var run = new Run();
                    var rPr = new RunProperties(
                        new RunFonts { Ascii = "Calibri", HighAnsi = "Calibri" },
                        new FontSize { Val = "24" });
                    if (bold) rPr.AppendChild(new Bold());
                    run.AppendChild(rPr);
                    run.AppendChild(new Text(text) { Space = SpaceProcessingModeValues.Preserve });
                    p.AppendChild(run);
                    body.AppendChild(p);
                }

                // Titlu de secțiune — stil ListParagraph cu numId=4 (identic cu SablonContract)
                void AddSectionTitle(string text)
                {
                    var p = new Paragraph();
                    var pPr = new ParagraphProperties(
                        new ParagraphStyleId { Val = "ListParagraph" },
                        new NumberingProperties(
                            new NumberingLevelReference { Val = 0 },
                            new NumberingId { Val = 4 }),
                        new SpacingBetweenLines { Before = "160", After = "80" });
                    p.AppendChild(pPr);
                    var run = new Run();
                    var rPr = new RunProperties(
                        new RunFonts { Ascii = "Calibri", HighAnsi = "Calibri" },
                        new FontSize { Val = "24" });
                    run.AppendChild(rPr);
                    run.AppendChild(new Text(text) { Space = SpaceProcessingModeValues.Preserve });
                    p.AppendChild(run);
                    body.AppendChild(p);
                }

                // ── NR. ÎNREG. (dreapta, ca la contract) ─────────────────────────
                if (nrInternGlobal > 0)
                    AddParagraph($"Nr. înreg. {nrInternGlobal}/{dataInregistrareAct:dd.MM.yyyy}", align: JustificationValues.Right);
                AddEmptyLine();

                // ── TITLU ────────────────────────────────────────────────────────
                AddText($"ACT ADIȚIONAL NR. {nrActAditional}", bold: true, align: JustificationValues.Center);
                AddText($"la Contractul de Pregătire Sportivă nr. {nrContract} / {dataContract}", align: JustificationValues.Center);
                AddEmptyLine();

                // ── TEMEIUL LEGAL ────────────────────────────────────────────────
                AddText(
                    "Încheiat în conformitate cu prevederile Legii educației fizice și sportului " +
                    "nr. 69/2000, cu modificările și completările ulterioare, ale Ordinului MTS " +
                    "nr. 664/2018 privind aprobarea Regulamentului de clasificare a structurilor sportive, " +
                    "ale Statutului și Regulamentelor Federației Române de Șah (FRȘah) și ale " +
                    "Statutului și Regulamentului de Organizare și Funcționare al Clubului.");
                AddEmptyLine();

                // ── SECȚIUNEA 1 — PĂRȚILE ────────────────────────────────────────
                AddSectionTitle("PĂRȚILE CONTRACTANTE");
                AddEmptyLine();

                AddText(
                    $"1.1.  {sSocietate}, cu sediul social în {sAdresaSoc}, și sediul secundar în " +
                    $"{sAdresaDiviziune}, CIF {sCUISoc}, cont bancar {sContSoc}, e-mail {sEmail}, " +
                    $"tel. {sTel}, reprezentată legal prin {sReprezentantSoc}, domiciliat în " +
                    $"{sAdresaReprezentant}, identificat cu {sDateReprezentant}, " +
                    "în calitate de Președinte, denumit în continuare «Clubul»,",
                    leftIndent: 360);
                AddEmptyLine();
                AddText("și", align: JustificationValues.Center);
                AddEmptyLine();
                AddText(
                    $"1.2.  {numeCursanti}, identificat(ă/ți) conform datelor înscrise în " +
                    $"Contractul de Pregătire Sportivă nr. {nrContract} / {dataContract}, " +
                    "denumit(ă/ți) în continuare «Sportivul»,",
                    leftIndent: 360);
                AddEmptyLine();
                AddText(
                    "au convenit de comun acord prezentul Act Adițional, " +
                    "cu următoarele clauze contractuale:");
                AddEmptyLine();

                // ── MENȚIUNE RETRAGERE (dacă există persoane retrase) ─────────────
                if (persoaneRetrase.Count > 0)
                {
                    var sbRet = new System.Text.StringBuilder();
                    foreach (var pr in persoaneRetrase)
                    {
                        if (sbRet.Length > 0) sbRet.Append(", ");
                        sbRet.Append(pr.Nume);
                        if (pr.DataRetragere.HasValue)
                            sbRet.Append($" (retras/ă la data de {pr.DataRetragere.Value:dd.MM.yyyy})");
                    }
                    AddText(
                        $"Urmare retragerii din contractul nr. {nrContract} a d-nei/d-lui {sbRet}, " +
                        "prezentul act adițional stabilește noile condiții pentru beneficiarii rămași.");
                    AddEmptyLine();
                }

                // ── SECȚIUNEA 2 — OBIECT ─────────────────────────────────────────
                AddSectionTitle("OBIECTUL ACTULUI ADIȚIONAL");

                AddText(
                    "2.1.  Prin prezentul act adițional, Clubul se obligă să continue prestarea " +
                    "serviciilor de pregătire sportivă în disciplina ȘAH, conform condițiilor " +
                    "stabilite prin contractul principal, pentru o nouă perioadă, astfel:",
                    leftIndent: 360);
                AddText($"        a)  Tipul programului de pregătire:  {tipAbonament}", leftIndent: 360);
                AddText($"        b)  Perioada de pregătire:  {dataInceput:dd.MM.yyyy} – {dataSfarsit:dd.MM.yyyy}", leftIndent: 360);
                AddText($"        c)  Contravaloarea lunară a programului:  {pret:N2} lei", leftIndent: 360);
                AddEmptyLine();

                // ── SECȚIUNEA 3 — PREȚ ───────────────────────────────────────────
                AddSectionTitle("PREȚUL ȘI MODALITATEA DE PLATĂ");

                AddText(
                    $"3.1.  Sportivul/Reprezentantul legal al acestuia se obligă să achite " +
                    $"contravaloarea de {pret:N2} lei, aferentă perioadei convenite prin prezentul act " +
                    "adițional, în condițiile și la termenele stabilite prin contractul principal.",
                    leftIndent: 360);
                AddText(
                    "3.2.  Nerespectarea termenelor de plată atrage aplicarea penalităților " +
                    "prevăzute în contractul de bază.",
                    leftIndent: 360);
                AddEmptyLine();

                // ── SECȚIUNEA 4 — DISPOZIȚII FINALE ─────────────────────────────
                AddSectionTitle("DISPOZIȚII FINALE");

                AddText(
                    $"4.1.  Toate celelalte clauze ale Contractului de Pregătire Sportivă " +
                    $"nr. {nrContract} / {dataContract} rămân nemodificate și integral aplicabile.",
                    leftIndent: 360);
                AddText(
                    "4.2.  Prezentul act adițional face parte integrantă din contractul de bază " +
                    "și completează clauzele acestuia în mod corespunzător.",
                    leftIndent: 360);
                AddText(
                    $"4.3.  Actul adițional a fost semnat astăzi, {DateTime.Today:dd.MM.yyyy}, " +
                    "în 2 (două) exemplare originale, câte un exemplar pentru fiecare parte contractantă.",
                    leftIndent: 360);
                AddEmptyLine();
                AddEmptyLine();
                AddEmptyLine();

                // ── SEMNĂTURI ────────────────────────────────────────────────────
                int tabSemn = 4680;

                void AddSignatureLine(string left, string right, bool bold = false)
                {
                    var p = new Paragraph();
                    p.AppendChild(new ParagraphProperties(
                        new Tabs(new TabStop { Val = TabStopValues.Left, Position = tabSemn }),
                        new SpacingBetweenLines { Before = "0", After = "80" }));

                    void AppendRun(string text)
                    {
                        var r = new Run();
                        var rPr = new RunProperties(
                            new RunFonts { Ascii = "Calibri", HighAnsi = "Calibri" },
                            new FontSize { Val = "24" });
                        if (bold) rPr.AppendChild(new Bold());
                        r.AppendChild(rPr);
                        r.AppendChild(new Text(text) { Space = SpaceProcessingModeValues.Preserve });
                        p.AppendChild(r);
                    }

                    AppendRun(left);
                    p.AppendChild(new Run(new TabChar()));
                    AppendRun(right);
                    body.AppendChild(p);
                }

                AddSignatureLine("CLUBUL", "SPORTIVUL / REPREZENTANT LEGAL", bold: true);
                AddSignatureLine(sSocietate, numeCursanti);
                AddSignatureLine(sReprezentantSoc, "");

                // Restaurează antetul/subsolul din șablon
                if (sectPrClone != null)
                    body.AppendChild(sectPrClone);

                mainPart.Document.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la generare act adițional: " + ex.Message);
                return;
            }

            Process.Start(new ProcessStartInfo(numeFisier) { UseShellExecute = true });
            ArhiveazaFisierGenerat(numeFisier, adrnIdAct);
        }

        // =========================================================
        //   Notă de reziliere
        // =========================================================
        private void GenereazaNotaReziliere(DateTime dataRetragere)
        {
            if (m_lCUCO <= 0) return;

            string nrContract = "", dataContract = "", numeCursanti = "";
            string sSocietate = "", sAdresaSoc = "", sAdresaDiviziune = "", sCUISoc = "";
            string sEmail = "", sTel = "", sContSoc = "", sReprezentantSoc = "";
            string sDateReprezentant = "", sAdresaReprezentant = "";
            int nrNota = 0;

            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());

            // Nr. și dată contract
            q.Open(string.Format(@"
SELECT CUCO_NrContract, CUCO_DataInceputContract
FROM CU_Contracte WHERE CUCO_Id = {0}", m_lCUCO));
            if (q.NextRow())
            {
                nrContract  = q.GetIntValue(0).ToString();
                var dtCo    = q.GetDateValue(1);
                if (dtCo.HasValue) dataContract = dtCo.Value.ToString("dd.MM.yyyy");
            }
            q.Close();

            // Toți beneficiarii contractului (nota e per-contract)
            q.Open(string.Format(@"
SELECT CUCU_Prenume + ' ' + CUCU_Nume
FROM CU_ContracteCursanti
INNER JOIN CU_Cursanti ON CUCC_CUCU_Id = CUCU_Id
WHERE CUCC_CUCO_Id = {0}
ORDER BY CUCU_Nume, CUCU_Prenume", m_lCUCO));
            var _persoaneNota = new System.Collections.Generic.List<string>();
            while (q.NextRow()) _persoaneNota.Add(q.GetStringValue(0) ?? "");
            numeCursanti = string.Join(", ", _persoaneNota);
            q.Close();

            // Date societate + diviziune
            q.Open(string.Format(@"
SELECT MBSO_Descriere, MBSO_CUI, MBSO_EmailSocietate, MBSO_TelReprezentant,
       CASE WHEN MBSO_Oras IS NULL OR MBSO_Oras='-' THEN '' ELSE 'loc. ' + UPPER(MBSO_Oras) END +
       CASE WHEN MBSO_Strada IS NULL OR MBSO_Strada='-' THEN '' ELSE ', str. ' + MBSO_Strada END +
       CASE WHEN MBSO_NumarStrada IS NULL OR MBSO_NumarStrada='-' THEN '' ELSE ' nr. ' + MBSO_NumarStrada END +
       CASE WHEN MBSO_Bloc IS NULL OR MBSO_Bloc='-' THEN '' ELSE ', bl. ' + MBSO_Bloc END +
       CASE WHEN MBSO_Scara IS NULL OR MBSO_Scara='-' THEN '' ELSE ', sc. ' + MBSO_Scara END +
       CASE WHEN MBSO_Apartament IS NULL OR MBSO_Apartament='-' THEN '' ELSE ', ap. ' + MBSO_Apartament END
       AS AdresaSocietate,
       CASE WHEN MBDV_Diviziune IS NULL OR MBDV_Diviziune='-' THEN ''
            ELSE 'loc. ' + UPPER(MBDV_Diviziune) END +
       CASE WHEN MBDV_Adresa IS NULL OR MBDV_Adresa='-' THEN ''
            ELSE ', ' + MBDV_Adresa END AS AdresaDiviziune,
       MBSO_Cont,
       UPPER(ISNULL(MBSO_Reprezentant, '')),
       CASE WHEN MBSO_CISerieReprezentant IS NOT NULL AND MBSO_CISerieReprezentant <> '-'
            THEN 'C.I. seria ' + MBSO_CISerieReprezentant + ', nr. ' + ISNULL(MBSO_CINumarReprezentant, '')
            ELSE '' END +
       CASE WHEN MBSO_CNPReprezentant IS NOT NULL AND MBSO_CNPReprezentant <> '-'
            THEN ', CNP ' + MBSO_CNPReprezentant ELSE '' END AS DateReprezentant,
       MBSO_AdresaReprezentant
FROM MB_Societate
INNER JOIN MB_Diviziune ON MBDV_MBSO_Id = MBSO_Id
WHERE MBSO_Id = {0} AND MBDV_Id = {1}", ManagerDeProiect.GetSocietate(), ManagerDeProiect.GetDiviziune()));
            if (q.NextRow())
            {
                sSocietate          = q.GetStringValue(0)!;
                sCUISoc             = q.GetStringValue(1)!;
                sEmail              = q.GetStringValue(2)!;
                sTel                = q.GetStringValue(3)!;
                sAdresaSoc          = q.GetStringValue(4)!;
                sAdresaDiviziune    = q.GetStringValue(5)!;
                sContSoc            = q.GetStringValue(6)!;
                sReprezentantSoc    = q.GetStringValue(7)!;
                sDateReprezentant   = q.GetStringValue(8)!;
                sAdresaReprezentant = q.GetStringValue(9)!;
            }
            q.Close();

            // Număr intern (ADTD_Id=7 = notă de reziliere)
            // Căutăm per persoană (ADRN_CUCU_Id) — fiecare persoană retrasă are propria notă
            DateTime dataInregistrareNota = DateTime.Today;
            int adrnIdNota = 0;
            {
                var qr = new CInterogareSQL(ManagerDeProiect.GetConnection());
                qr.OpenP(@"SELECT ADRN_Numar, ADRN_Data, ADRN_Id
                           FROM AD_RegistruNumereInterne
                           WHERE ADRN_CUCO_Id = @cuco AND ADRN_ADTD_Id = 7
                           ORDER BY ADRN_Numar DESC",
                    CInterogareSQL.P("@cuco", m_lCUCO));
                if (qr.NextRow())
                {
                    nrNota     = qr.GetIntValue(0);
                    adrnIdNota = qr.GetIntValue(2);
                }
                qr.Close();
                dataInregistrareNota = DateTime.Today;
            }

            if (adrnIdNota == 0)
            {
                var qSeq = new CInterogareSQL(ManagerDeProiect.GetConnection());
                qSeq.BeginTransaction(System.Data.IsolationLevel.Serializable);
                try
                {
                    nrNota               = CGeneratorNumarIntern.Next(qSeq);
                    dataInregistrareNota = DateTime.Today;

                    adrnIdNota = qSeq.ExecuteToIntPThrow(@"
INSERT INTO AD_RegistruNumereInterne
    (ADRN_Numar, ADRN_Data, ADRN_ADTD_Id, ADRN_Iesire, ADRN_MBDV_Id,
     ADRN_CUCO_Id, ADRN_Descriere, ADRN_MBUT_Id, ADRN_DataSalvarii)
OUTPUT INSERTED.ADRN_Id
VALUES
    (@nr, GETDATE(), 7, 1, @div,
     @cuco, @desc, @u, GETDATE());",
                        CInterogareSQL.P("@nr",   nrNota),
                        CInterogareSQL.P("@div",  ManagerDeProiect.GetDiviziune()),
                        CInterogareSQL.P("@cuco", m_lCUCO),
                        CInterogareSQL.P("@desc", $"Notă reziliere – Contract nr. {nrContract}"),
                        CInterogareSQL.P("@u",    ManagerDeProiect.GetUserId())
                    );

                    qSeq.CommitTransaction();
                }
                catch (Exception ex)
                {
                    qSeq.RollbackTransaction();
                    MessageBox.Show("Eroare la înregistrare: " + ex.Message);
                    return;
                }
                finally { qSeq.Close(); }
            }

            // Generare document
            string folder = @"d:\WORK\ONIXTEIA\ACTE\CONTRACTE\";
            Directory.CreateDirectory(folder);

            string folderSabloane = @"d:\WORK\ONIXTEIA\ACTE\SABLOANE\";
            // Folosim SablonNotaReziliere.dotx; dacă nu există, căutăm SablonActAditional.dotx ca fallback
            string templatePath = Path.Combine(folderSabloane, "SablonNotaReziliere.dotx");
            if (!File.Exists(templatePath))
                templatePath = Path.Combine(folderSabloane, "SablonActAditional.dotx");
            if (!File.Exists(templatePath))
            {
                MessageBox.Show("Nu am găsit șablonul:\n" + Path.Combine(folderSabloane, "SablonNotaReziliere.dotx") +
                                "\nCreați fișierul șablon sau copiați SablonActAditional.dotx în Sabloane\\.");
                return;
            }

            string numeFisier = Path.Combine(folder, $"NotaReziliere_{nrContract}_{nrNota}.docx");

            if (File.Exists(numeFisier))
            {
                string mesaj = "Nota de reziliere a fost deja generată.\n\n" +
                               "• Apăsați DA pentru a DESCHIDE fișierul existent.\n" +
                               "• Apăsați NU pentru a REGENERA nota (cea veche va fi suprascrisă).\n" +
                               "• Apăsați CANCEL pentru a anula operațiunea.";
                DialogResult dlg = MessageBox.Show(mesaj, "Notă reziliere existentă", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dlg == DialogResult.Cancel) return;
                if (dlg == DialogResult.Yes)
                {
                    Process.Start(new ProcessStartInfo(numeFisier) { UseShellExecute = true });
                    return;
                }
            }

            try
            {
                File.Copy(templatePath, numeFisier, true);
                using var wordDoc = WordprocessingDocument.Open(numeFisier, true);
                wordDoc.ChangeDocumentType(WordprocessingDocumentType.Document);
                var mainPart = wordDoc.MainDocumentPart;
                if (mainPart == null) return;
                var body = mainPart.Document.Body!;

                var sectPr      = body.Elements<SectionProperties>().LastOrDefault();
                var sectPrClone = sectPr != null ? (SectionProperties)sectPr.CloneNode(true) : null;
                body.RemoveAllChildren();

                void AddText(string text, bool bold = false, JustificationValues? align = null, int leftIndent = 0)
                {
                    var p   = new Paragraph();
                    var pPr = new ParagraphProperties(
                        new Justification { Val = align ?? JustificationValues.Both },
                        new SpacingBetweenLines { Before = "0", After = "120" });
                    if (leftIndent > 0) pPr.AppendChild(new Indentation { Left = leftIndent.ToString() });
                    p.AppendChild(pPr);
                    var run = new Run();
                    var rPr = new RunProperties(
                        new RunFonts { Ascii = "Calibri", HighAnsi = "Calibri" },
                        new FontSize { Val = "24" });
                    if (bold) rPr.AppendChild(new Bold());
                    run.AppendChild(rPr);
                    run.AppendChild(new Text(text) { Space = SpaceProcessingModeValues.Preserve });
                    p.AppendChild(run);
                    body.AppendChild(p);
                }

                void AddEmptyLine() => body.AppendChild(new Paragraph(
                    new ParagraphProperties(new SpacingBetweenLines { Before = "0", After = "120" })));

                void AddSectionTitle(string text)
                {
                    var p   = new Paragraph();
                    var pPr = new ParagraphProperties(
                        new ParagraphStyleId { Val = "ListParagraph" },
                        new NumberingProperties(
                            new NumberingLevelReference { Val = 0 },
                            new NumberingId { Val = 4 }),
                        new SpacingBetweenLines { Before = "160", After = "80" });
                    p.AppendChild(pPr);
                    var run = new Run();
                    var rPr = new RunProperties(
                        new RunFonts { Ascii = "Calibri", HighAnsi = "Calibri" },
                        new FontSize { Val = "24" });
                    run.AppendChild(rPr);
                    run.AppendChild(new Text(text) { Space = SpaceProcessingModeValues.Preserve });
                    p.AppendChild(run);
                    body.AppendChild(p);
                }

                // ── NR. ÎNREG. (dreapta sus, ca la act adițional) ──────────────────
                if (nrNota > 0)
                    AddText($"Nr. înreg. {nrNota}/{dataInregistrareNota:dd.MM.yyyy}", align: JustificationValues.Right);
                AddEmptyLine();

                // ── TITLU ────────────────────────────────────────────────────────
                AddText("NOTĂ DE REZILIERE", bold: true, align: JustificationValues.Center);
                AddText($"la Contractul de Pregătire Sportivă nr. {nrContract} / {dataContract}", align: JustificationValues.Center);
                AddEmptyLine();

                // ── TEMEIUL LEGAL ────────────────────────────────────────────────
                AddText(
                    "Încheiată în conformitate cu prevederile Legii educației fizice și sportului " +
                    "nr. 69/2000, cu modificările și completările ulterioare, ale Codului Civil " +
                    "– art. 1.276 privind denunțarea unilaterală sau acordul comun al părților, " +
                    "ale Statutului și Regulamentelor Federației Române de Șah (FRȘah) și ale " +
                    "Statutului și Regulamentului de Organizare și Funcționare al Clubului.");
                AddEmptyLine();

                // ── SECȚIUNEA 1 — PĂRȚILE ────────────────────────────────────────
                AddSectionTitle("PĂRȚILE CONTRACTANTE");
                AddEmptyLine();
                AddText(
                    $"1.1.  {sSocietate}, cu sediul social în {sAdresaSoc}, și sediul secundar în " +
                    $"{sAdresaDiviziune}, CIF {sCUISoc}, cont bancar {sContSoc}, e-mail {sEmail}, " +
                    $"tel. {sTel}, reprezentată legal prin {sReprezentantSoc}, domiciliat în " +
                    $"{sAdresaReprezentant}, identificat cu {sDateReprezentant}, " +
                    "în calitate de Președinte, denumit în continuare «Clubul»,",
                    leftIndent: 360);
                AddEmptyLine();
                AddText("și", align: JustificationValues.Center);
                AddEmptyLine();
                AddText(
                    $"1.2.  {numeCursanti}, identificat(ă/ți) conform datelor înscrise în " +
                    $"Contractul de Pregătire Sportivă nr. {nrContract} / {dataContract}, " +
                    "denumit(ă/ți) în continuare «Sportivul»,",
                    leftIndent: 360);
                AddEmptyLine();
                AddText("au convenit de comun acord prezenta Notă de Reziliere, cu următoarele clauze:");
                AddEmptyLine();

                // ── SECȚIUNEA 2 — OBIECT ─────────────────────────────────────────
                AddSectionTitle("OBIECTUL NOTEI DE REZILIERE");

                AddText(
                    $"2.1.  Prin prezenta notă, părțile convin încetarea, de comun acord, a " +
                    $"Contractului de Pregătire Sportivă nr. {nrContract} / {dataContract}, " +
                    $"cu efect de la data de {dataRetragere:dd.MM.yyyy}.",
                    leftIndent: 360);
                AddText(
                    "2.2.  Începând cu data menționată, toate drepturile și obligațiile asumate " +
                    "prin contractul principal încetează, cu excepția celor care, prin natura lor " +
                    "sau prin voința expresă a părților, supraviețuiesc rezilierii.",
                    leftIndent: 360);
                AddText(
                    "2.3.  Obligațiile financiare scadente și neachitate până la data rezilierii " +
                    "rămân în sarcina Sportivului și sunt exigibile în continuare.",
                    leftIndent: 360);
                AddEmptyLine();

                // ── SECȚIUNEA 3 — DISPOZIȚII FINALE ─────────────────────────────
                AddSectionTitle("DISPOZIȚII FINALE");

                AddText(
                    $"3.1.  Prezenta notă de reziliere a fost semnată astăzi, {dataRetragere:dd.MM.yyyy}, " +
                    "în 2 (două) exemplare originale, câte un exemplar pentru fiecare parte contractantă.",
                    leftIndent: 360);
                AddEmptyLine();
                AddEmptyLine();
                AddEmptyLine();

                // ── SEMNĂTURI ────────────────────────────────────────────────────
                int tabSemn = 4680;

                void AddSignatureLine(string left, string right, bool bold = false)
                {
                    var p = new Paragraph();
                    p.AppendChild(new ParagraphProperties(
                        new Tabs(new TabStop { Val = TabStopValues.Left, Position = tabSemn }),
                        new SpacingBetweenLines { Before = "0", After = "80" }));

                    void AppendRun(string text)
                    {
                        var r   = new Run();
                        var rPr = new RunProperties(
                            new RunFonts { Ascii = "Calibri", HighAnsi = "Calibri" },
                            new FontSize { Val = "24" });
                        if (bold) rPr.AppendChild(new Bold());
                        r.AppendChild(rPr);
                        r.AppendChild(new Text(text) { Space = SpaceProcessingModeValues.Preserve });
                        p.AppendChild(r);
                    }

                    AppendRun(left);
                    p.AppendChild(new Run(new TabChar()));
                    AppendRun(right);
                    body.AppendChild(p);
                }

                AddSignatureLine("CLUBUL", "SPORTIVUL / REPREZENTANT LEGAL", bold: true);
                AddSignatureLine(sSocietate, numeCursanti);
                AddSignatureLine(sReprezentantSoc, "");

                if (sectPrClone != null) body.AppendChild(sectPrClone);
                mainPart.Document.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la generare notă de reziliere: " + ex.Message);
                return;
            }

            Process.Start(new ProcessStartInfo(numeFisier) { UseShellExecute = true });

            ArhiveazaFisierGenerat(numeFisier, adrnIdNota);
        }

        // =========================================================
        //   Reziliere / reactivare per persoană
        // =========================================================
        private void ActualizeazaButoanePersoana()
        {
            bool peCursanti = m_tabControl.SelectedTab == m_tabPageCursanti;
            m_tsbStergePersoana.Enabled = peCursanti;
        }

        private void StergePersoanaSelectata()
        {
            object? cuccVal = m_gContracte.GetColumnData("CUCC_Id");
            object? cucuVal = m_gContracte.GetColumnData("CUCU_Id");
            if (cuccVal == null || cuccVal == DBNull.Value)
            {
                MessageBox.Show("Selectați o persoană din lista contractului.",
                    "Atenție", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int cuccId = Convert.ToInt32(cuccVal);
            int cucuId = cucuVal != null && cucuVal != DBNull.Value ? Convert.ToInt32(cucuVal) : 0;

            // Verifică dacă persoana are prezențe în acest contract
            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            int nrPrezente = q.ExecuteToIntP(
                @"SELECT COUNT(*) FROM CU_PrezenteDetaliu
                  WHERE CUPD_CUCO_Id = @cuco AND CUPD_CUCU_Id = @cucu",
                CInterogareSQL.P("@cuco", m_lCUCO),
                CInterogareSQL.P("@cucu", cucuId));
            q.Close();

            if (nrPrezente > 0)
            {
                MessageBox.Show("Persoana are prezențe înregistrate în acest contract și nu poate fi ștearsă.",
                    "Atenție", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string prenume    = m_gContracte.GetColumnData("CUCU_Prenume")?.ToString() ?? "";
            string nume       = m_gContracte.GetColumnData("CUCU_Nume")?.ToString() ?? "";
            string numeAfisat = $"{prenume} {nume}".Trim();
            if (MessageBox.Show($"Ștergeți persoana {numeAfisat} din contract?",
                    "Confirmare ștergere", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            var qDel = new CInterogareSQL(ManagerDeProiect.GetConnection());
            qDel.ExecuteNonQueryP("DELETE FROM CU_ContracteCursanti WHERE CUCC_Id = @id",
                CInterogareSQL.P("@id", cuccId));
            qDel.Close();
            IncarcaContracte();
        }

        private void ReactiveazaPersoanaSelectata()
        {
            object? cuccVal   = m_gContracte.GetColumnData("CUCC_Id");
            object? retrasVal = m_gContracte.GetColumnData("CUCC_Retras");
            if (cuccVal == null || cuccVal == DBNull.Value)
            {
                MessageBox.Show("Selectați o persoană din lista contractului.",
                    "Atenție", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            bool retras = retrasVal != null && retrasVal != DBNull.Value && Convert.ToBoolean(retrasVal);
            if (!retras)
            {
                MessageBox.Show("Persoana selectată nu este retrasă.",
                    "Atenție", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ReactiveazaPersoana(Convert.ToInt32(cuccVal));
        }

        private DateTime? CereDateRetragere()
        {
            using var frm = new Form
            {
                Text = "Data retragere",
                Width = 310, Height = 130,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MinimizeBox = false, MaximizeBox = false
            };
            var lbl       = new Label { Text = "Data retragere:", Left = 12, Top = 18, AutoSize = true };
            var dtp       = new DateTimePicker { Left = 130, Top = 14, Width = 150, Value = DateTime.Today, Format = DateTimePickerFormat.Custom, CustomFormat = "dd.MM.yyyy" };
            var btnOk     = new Button { Text = "OK",       DialogResult = DialogResult.OK,     Left = 105, Top = 58, Width = 80 };
            var btnCancel = new Button { Text = "Anulează", DialogResult = DialogResult.Cancel, Left = 195, Top = 58, Width = 90 };
            frm.Controls.AddRange([lbl, dtp, btnOk, btnCancel]);
            frm.AcceptButton = btnOk;
            frm.CancelButton = btnCancel;
            return frm.ShowDialog(this) == DialogResult.OK ? dtp.Value.Date : null;
        }

        private void ReziliazaContractIntreg(DateTime dataRetragere)
        {
            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            try
            {
                q.ExecuteNonQueryP(@"UPDATE CU_ContracteCursanti
                                     SET CUCC_Retras = 1, CUCC_DataRetragere = @d
                                     WHERE CUCC_CUCO_Id = @cuco AND ISNULL(CUCC_Retras, 0) = 0",
                    CInterogareSQL.P("@d",    dataRetragere),
                    CInterogareSQL.P("@cuco", m_lCUCO));

                q.ExecuteNonQueryP("UPDATE CU_Contracte SET CUCO_Retras = 1 WHERE CUCO_Id = @id",
                    CInterogareSQL.P("@id", m_lCUCO));
                m_ckRetras.Checked = true;
            }
            finally { q.Close(); }

            IncarcaContracte();
            GenereazaNotaReziliere(dataRetragere);
        }

        private void ReziliazaPersoana(int cuccId, DateTime dataRetragere)
        {
            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            try
            {
                q.ExecuteNonQueryP(@"UPDATE CU_ContracteCursanti
                                     SET CUCC_Retras = 1, CUCC_DataRetragere = @d
                                     WHERE CUCC_Id = @id",
                    CInterogareSQL.P("@d",  dataRetragere),
                    CInterogareSQL.P("@id", cuccId));

                // Dacă toate persoanele sunt retrase, marchează și contractul ca retras
                int active = q.ExecuteToIntP(
                    @"SELECT COUNT(*) FROM CU_ContracteCursanti
                      WHERE CUCC_CUCO_Id = @cuco AND ISNULL(CUCC_Retras, 0) = 0",
                    CInterogareSQL.P("@cuco", m_lCUCO));
                if (active == 0)
                {
                    q.ExecuteNonQueryP("UPDATE CU_Contracte SET CUCO_Retras = 1 WHERE CUCO_Id = @id",
                        CInterogareSQL.P("@id", m_lCUCO));
                    m_ckRetras.Checked = true;
                }
            }
            finally { q.Close(); }

            IncarcaContracte();
        }

        private void RetragePersoanaSelectata()
        {
            object? cuccVal = m_gContracte.GetColumnData("CUCC_Id");
            if (cuccVal == null || cuccVal == DBNull.Value)
            {
                MessageBox.Show("Selectați o persoană din lista contractului.",
                    "Atenție", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            object? retrasVal = m_gContracte.GetColumnData("CUCC_Retras");
            if (retrasVal != null && retrasVal != DBNull.Value && Convert.ToBoolean(retrasVal))
            {
                MessageBox.Show("Persoana selectată este deja retrasă.",
                    "Atenție", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime? dataRet = CereDateRetragere();
            if (!dataRet.HasValue) return;

            ReziliazaPersoana(Convert.ToInt32(cuccVal), dataRet.Value);

            if (MessageBox.Show(
                    "Persoana a fost retrasă.\n\nDoriți să generați actul adițional pentru persoanele rămase?",
                    "Act adițional", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            // Caută cel mai recent abonament (altul decât primul — care e acoperit de contract)
            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            int cuacId = q.ExecuteToIntP(
                @"SELECT TOP 1 CUAC_Id FROM CU_AbonamenteCursant
                  WHERE CUAC_CUCO_Id = @cuco
                  ORDER BY CUAC_DataInceput DESC",
                CInterogareSQL.P("@cuco", m_lCUCO));

            int nrAct = cuacId > 0 ? q.ExecuteToIntP(
                @"SELECT COUNT(*) FROM CU_AbonamenteCursant
                  WHERE CUAC_CUCO_Id = @cuco AND CUAC_Id < @cuac",
                CInterogareSQL.P("@cuco", m_lCUCO),
                CInterogareSQL.P("@cuac", cuacId)) : 0;
            q.Close();

            if (cuacId <= 0 || nrAct == 0)
            {
                MessageBox.Show(
                    "Nu există o perioadă de abonament ulterioară contractului inițial.\n\n" +
                    "Introduceți noua perioadă de abonament, apoi click-dreapta pe ea → Generare act adițional.",
                    "Act adițional", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            GenereazaActAditional(cuacId);
        }

        private void ReactiveazaPersoana(int cuccId)
        {
            var q = new CInterogareSQL(ManagerDeProiect.GetConnection());
            try
            {
                q.ExecuteNonQueryP(@"UPDATE CU_ContracteCursanti
                                     SET CUCC_Retras = 0, CUCC_DataRetragere = NULL
                                     WHERE CUCC_Id = @id",
                    CInterogareSQL.P("@id", cuccId));

                q.ExecuteNonQueryP("UPDATE CU_Contracte SET CUCO_Retras = 0 WHERE CUCO_Id = @id",
                    CInterogareSQL.P("@id", m_lCUCO));
                m_ckRetras.Checked = false;
            }
            finally { q.Close(); }

            IncarcaContracte();
        }

        private static string FaraDiacritice(string s) =>
            s.Replace('ă', 'a').Replace('Ă', 'A')
             .Replace('â', 'a').Replace('Â', 'A')
             .Replace('î', 'i').Replace('Î', 'I')
             .Replace('ș', 's').Replace('Ș', 'S')
             .Replace('ş', 's').Replace('Ş', 'S')
             .Replace('ț', 't').Replace('Ț', 'T')
             .Replace('ţ', 't').Replace('Ţ', 'T');
    }
}

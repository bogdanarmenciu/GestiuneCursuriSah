using GestiunePrezente.Common;
using Microsoft.Data.SqlClient;

namespace GestiunePrezente.MeniuPrincipal
{
    public class FrmDashboard : Form
    {
        private readonly CManagerDeProiect m_manager;

        private Label? m_lblCursanti;
        private Label? m_lblContracte;
        private Label? m_lblPrezente;
        private Label? m_lblIncasari;
        private Label? m_lblSubContracte;

        // KPI financiar
        private Label? m_lblDeFact;
        private Label? m_lblDeIncas;
        private Label? m_lblTotalFact;
        private Label? m_lblTotalIncas;
        private Label? m_lblValContracte;

        private static readonly Color C_BLUE   = Color.FromArgb(37,  99,  235);
        private static readonly Color C_ORANGE = Color.FromArgb(234, 88,  12);
        private static readonly Color C_GREEN  = Color.FromArgb(22,  163, 74);
        private static readonly Color C_TEAL   = Color.FromArgb(8,   145, 178);
        private static readonly Color C_RED    = Color.FromArgb(185, 28,  28);
        private static readonly Color C_PURPLE = Color.FromArgb(109, 40,  217);
        private static readonly Color C_BG     = Color.FromArgb(248, 250, 252);
        private static readonly Color C_CARD   = Color.White;
        private static readonly Color C_BORDER = Color.FromArgb(229, 231, 235);
        private static readonly Color C_TEXT   = Color.FromArgb(17,  24,  39);
        private static readonly Color C_SUBTLE = Color.FromArgb(107, 114, 128);

        public FrmDashboard(FrmMeniuPrincipal formParent, CManagerDeProiect mm)
        {
            m_manager = mm;

            Text            = "Dashboard";
            BackColor       = C_BG;
            Font            = new Font("Segoe UI", 9.5F);
            AutoScroll      = true;
            MinimumSize     = new Size(600, 400);

            BuildUI();
            Load += (_, _) => LoadData();
        }

        // ── Construire UI ────────────────────────────────────────────────
        private void BuildUI()
        {
            // Header
            var pHeader = new Panel
            {
                Dock      = DockStyle.Top,
                Height    = 72,
                BackColor = Color.White,
                Padding   = new Padding(28, 0, 28, 0)
            };
            var sep = new Panel { Dock = DockStyle.Bottom, Height = 1, BackColor = C_BORDER };
            pHeader.Controls.Add(sep);

            string luna  = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
            string titlu = $"Dashboard — {char.ToUpper(luna[0])}{luna[1..]} {DateTime.Now.Year}";

            var lblTitle = new Label
            {
                Text      = titlu,
                Dock      = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font      = new Font("Segoe UI Semibold", 16F, FontStyle.Regular),
                ForeColor = C_TEXT,
                BackColor = Color.Transparent
            };
            pHeader.Controls.Add(lblTitle);
            Controls.Add(pHeader);

            // Carduri
            var pCards = new FlowLayoutPanel
            {
                Dock        = DockStyle.Top,
                AutoSize    = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents  = true,
                Padding     = new Padding(24, 24, 24, 0),
                BackColor   = C_BG
            };

            (m_lblCursanti,    _)                = AddCard(pCards, "👥", "Cursanți activi",          C_BLUE,   "contracte active");
            (m_lblContracte,   m_lblSubContracte) = AddCard(pCards, "📋", "Contracte ce expiră",      C_ORANGE, "în 30 de zile");
            (m_lblPrezente,    _)                = AddCard(pCards, "✅", "Prezențe luna curentă",     C_GREEN,  "total intrări");
            (m_lblIncasari,    _)                = AddCard(pCards, "💰", "Încasări luna curentă",    C_TEAL,   "lei");

            (m_lblDeFact,      _) = AddCard(pCards, "⚠",  "Contracte de facturat",    C_RED,    "luni nefacturate");
            (m_lblDeIncas,     _) = AddCard(pCards, "⏳", "Contracte de încasat",     C_ORANGE, "facturi restante");
            (m_lblTotalFact,   _) = AddCard(pCards, "🧾", "Total facturat",           C_BLUE,   "lei — toate timpurile");
            (m_lblTotalIncas,  _) = AddCard(pCards, "✔",  "Total încasat",            C_GREEN,  "lei — toate timpurile");
            (m_lblValContracte,_) = AddCard(pCards, "📌", "Val. contracte / lună",    C_PURPLE, "lei — abonamente active");

            Controls.Add(pCards);
            Controls.SetChildIndex(pCards, 0);
        }

        private (Label main, Label sub) AddCard(FlowLayoutPanel parent, string icon,
                                                 string title, Color accent, string subtitle)
        {
            const int W = 230, H = 140;

            var card = new Panel
            {
                Size      = new Size(W, H),
                BackColor = C_CARD,
                Margin    = new Padding(0, 0, 16, 16),
                Cursor    = Cursors.Default
            };

            card.Paint += (_, pe) =>
            {
                var g   = pe.Graphics;
                var rc  = new Rectangle(0, 0, card.Width - 1, card.Height - 1);
                using var borderPen  = new Pen(C_BORDER);
                using var accentBrush = new SolidBrush(accent);
                g.DrawRectangle(borderPen, rc);
                g.FillRectangle(accentBrush, 0, 0, card.Width, 4);
            };

            var lblIcon = new Label
            {
                Text      = icon,
                Location  = new Point(18, 14),
                Size      = new Size(36, 36),
                Font      = new Font("Segoe UI", 18F),
                BackColor = Color.Transparent,
                ForeColor = accent
            };

            var lblTitle = new Label
            {
                Text      = title,
                Location  = new Point(18, 54),
                Size      = new Size(W - 36, 18),
                Font      = new Font("Segoe UI", 9F),
                ForeColor = C_SUBTLE,
                BackColor = Color.Transparent,
                AutoEllipsis = true
            };

            var lblMain = new Label
            {
                Text      = "—",
                Location  = new Point(16, 74),
                Size      = new Size(W - 32, 38),
                Font      = new Font("Segoe UI Semibold", 26F),
                ForeColor = C_TEXT,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleLeft
            };

            var lblSub = new Label
            {
                Text      = subtitle,
                Location  = new Point(18, 115),
                Size      = new Size(W - 36, 16),
                Font      = new Font("Segoe UI", 8.5F),
                ForeColor = C_SUBTLE,
                BackColor = Color.Transparent
            };

            card.Controls.AddRange([lblIcon, lblTitle, lblMain, lblSub]);
            parent.Controls.Add(card);

            return (lblMain, lblSub);
        }

        // ── Încărcare date ───────────────────────────────────────────────
        private void LoadData()
        {
            try
            {
                var conn = m_manager.GetConnection();
                int div  = m_manager.GetDiviziune();

                m_lblCursanti!.Text  = QueryInt(conn, @"
                    SELECT COUNT(DISTINCT CUCC_CUCU_Id)
                    FROM CU_ContracteCursanti
                    INNER JOIN CU_Contracte  ON CUCC_CUCO_Id = CUCO_Id
                    INNER JOIN CU_Cursanti   ON CUCC_CUCU_Id = CUCU_Id
                    WHERE CUCO_Retras = 0
                      AND ISNULL(CUCC_Retras, 0) = 0
                      AND CUCU_MBDV_Id = @div", div).ToString();

                int expiring = QueryInt(conn, @"
                    SELECT COUNT(*)
                    FROM CU_Contracte C
                    WHERE C.CUCO_Retras = 0
                      AND EXISTS (
                          SELECT 1 FROM CU_AbonamenteCursant CUAC
                          WHERE CUAC.CUAC_CUCO_Id = C.CUCO_Id
                            AND CUAC.CUAC_DataInceput <= GETDATE()
                            AND CUAC.CUAC_DataSfarsit >= GETDATE()
                            AND CUAC.CUAC_DataSfarsit <= DATEADD(DAY, 30, GETDATE()))
                      AND EXISTS (
                          SELECT 1 FROM CU_ContracteCursanti CC
                          INNER JOIN CU_Cursanti CU ON CC.CUCC_CUCU_Id = CU.CUCU_Id
                          WHERE CC.CUCC_CUCO_Id = C.CUCO_Id
                            AND CU.CUCU_MBDV_Id = @div
                            AND ISNULL(CC.CUCC_Retras, 0) = 0)", div);
                m_lblContracte!.Text    = expiring.ToString();
                m_lblSubContracte!.Text = expiring == 0 ? "niciun contract nu expiră curând"
                                       : expiring == 1 ? "contract expiră în 30 de zile"
                                       :                 "contracte expiră în 30 de zile";

                m_lblPrezente!.Text = QueryInt(conn, @"
                    SELECT COUNT(*)
                    FROM CU_PrezenteDetaliu
                    INNER JOIN CU_Prezente ON CUPD_CUPR_Id = CUPR_Id
                    WHERE MONTH(CUPR_DeLaData) = MONTH(GETDATE())
                      AND YEAR(CUPR_DeLaData)  = YEAR(GETDATE())
                      AND CUPR_MBDV_Id = @div", div).ToString();

                decimal incasari = QueryDecimal(conn, @"
                    SELECT ISNULL(SUM(CUIN_Valoare), 0)
                    FROM CU_Incasari
                    WHERE MONTH(CUIN_Data) = MONTH(GETDATE())
                      AND YEAR(CUIN_Data)  = YEAR(GETDATE())
                      AND CUIN_MBDV_Id = @div", div);
                m_lblIncasari!.Text = incasari.ToString("N0") + " lei";

                m_lblDeFact!.Text = QueryInt(conn, @"
                    SELECT COUNT(DISTINCT C.CUCO_Id)
                    FROM CU_Contracte C
                    INNER JOIN CU_ContracteCursanti CC ON CC.CUCC_CUCO_Id = C.CUCO_Id AND ISNULL(CC.CUCC_Retras,0)=0
                    INNER JOIN CU_Cursanti CU ON CC.CUCC_CUCU_Id = CU.CUCU_Id AND CU.CUCU_MBDV_Id = @div
                    WHERE C.CUCO_Retras = 0
                      AND EXISTS (
                        SELECT 1 FROM CU_AbonamenteCursant CUAC2
                        INNER JOIN CU_Abonamente AB2 ON CUAC2.CUAC_CUAB_Id = AB2.CUAB_Id AND AB2.CUAB_SedintaIndividuala = 0
                        CROSS JOIN (VALUES(0),(1),(2),(3),(4),(5),(6),(7),(8),(9),(10),(11)) n(n)
                        WHERE CUAC2.CUAC_CUCO_Id = C.CUCO_Id AND CUAC2.CUAC_DataSfarsit >= GETDATE()
                          AND DATEADD(MONTH,n.n,DATEFROMPARTS(YEAR(CUAC2.CUAC_DataInceput),MONTH(CUAC2.CUAC_DataInceput),1)) <= CUAC2.CUAC_DataSfarsit
                          AND DATEADD(MONTH,n.n,DATEFROMPARTS(YEAR(CUAC2.CUAC_DataInceput),MONTH(CUAC2.CUAC_DataInceput),1)) <= GETDATE()
                          AND NOT EXISTS (SELECT 1 FROM FV_Facturi F WHERE F.FVFA_CUCO_Id=C.CUCO_Id
                            AND DATEFROMPARTS(YEAR(F.FVFA_PerioadaInceput),MONTH(F.FVFA_PerioadaInceput),1)
                              = DATEFROMPARTS(YEAR(DATEADD(MONTH,n.n,DATEFROMPARTS(YEAR(CUAC2.CUAC_DataInceput),MONTH(CUAC2.CUAC_DataInceput),1))),
                                             MONTH(DATEADD(MONTH,n.n,DATEFROMPARTS(YEAR(CUAC2.CUAC_DataInceput),MONTH(CUAC2.CUAC_DataInceput),1))),1))
                      )", div).ToString();

                m_lblDeIncas!.Text = QueryInt(conn, @"
                    SELECT COUNT(DISTINCT F.FVFA_CUCO_Id)
                    FROM FV_Facturi F
                    INNER JOIN CU_Contracte C2 ON F.FVFA_CUCO_Id = C2.CUCO_Id
                    INNER JOIN CU_ContracteCursanti CC2 ON CC2.CUCC_CUCO_Id = C2.CUCO_Id AND ISNULL(CC2.CUCC_Retras,0)=0
                    INNER JOIN CU_Cursanti CU2 ON CC2.CUCC_CUCU_Id = CU2.CUCU_Id AND CU2.CUCU_MBDV_Id = @div
                    WHERE F.FVFA_Suma > ISNULL((SELECT SUM(CUIN_Valoare) FROM CU_Incasari WHERE CUIN_FVFA_Id=F.FVFA_Id),0)", div).ToString();

                m_lblTotalFact!.Text = QueryDecimal(conn, @"
                    SELECT ISNULL(SUM(F2.FVFA_Suma),0)
                    FROM FV_Facturi F2
                    INNER JOIN CU_Contracte C3 ON F2.FVFA_CUCO_Id = C3.CUCO_Id
                    INNER JOIN CU_ContracteCursanti CC3 ON CC3.CUCC_CUCO_Id = C3.CUCO_Id AND ISNULL(CC3.CUCC_Retras,0)=0
                    INNER JOIN CU_Cursanti CU3 ON CC3.CUCC_CUCU_Id = CU3.CUCU_Id AND CU3.CUCU_MBDV_Id = @div", div).ToString("N0") + " lei";

                m_lblTotalIncas!.Text = QueryDecimal(conn, @"
                    SELECT ISNULL(SUM(I.CUIN_Valoare),0)
                    FROM CU_Incasari I
                    INNER JOIN FV_Facturi F3 ON I.CUIN_FVFA_Id = F3.FVFA_Id
                    INNER JOIN CU_Contracte C4 ON F3.FVFA_CUCO_Id = C4.CUCO_Id
                    INNER JOIN CU_ContracteCursanti CC4 ON CC4.CUCC_CUCO_Id = C4.CUCO_Id AND ISNULL(CC4.CUCC_Retras,0)=0
                    INNER JOIN CU_Cursanti CU4 ON CC4.CUCC_CUCU_Id = CU4.CUCU_Id AND CU4.CUCU_MBDV_Id = @div", div).ToString("N0") + " lei";

                m_lblValContracte!.Text = QueryDecimal(conn, @"
                    SELECT ISNULL(SUM(CUAC.CUAC_Pret),0)
                    FROM CU_AbonamenteCursant CUAC
                    INNER JOIN CU_Contracte C5 ON CUAC.CUAC_CUCO_Id = C5.CUCO_Id AND C5.CUCO_Retras = 0
                    INNER JOIN CU_ContracteCursanti CC5 ON CC5.CUCC_CUCO_Id = C5.CUCO_Id AND ISNULL(CC5.CUCC_Retras,0)=0
                    INNER JOIN CU_Cursanti CU5 ON CC5.CUCC_CUCU_Id = CU5.CUCU_Id AND CU5.CUCU_MBDV_Id = @div
                    WHERE CUAC.CUAC_DataInceput <= GETDATE() AND CUAC.CUAC_DataSfarsit >= GETDATE()", div).ToString("N0") + " lei";
            }
            catch { /* non-critical */ }
        }

        private static int QueryInt(SqlConnection conn, string sql, int div)
        {
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@div", div);
            var r = cmd.ExecuteScalar();
            return r == null || r == DBNull.Value ? 0 : Convert.ToInt32(r);
        }

        private static decimal QueryDecimal(SqlConnection conn, string sql, int div)
        {
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@div", div);
            var r = cmd.ExecuteScalar();
            return r == null || r == DBNull.Value ? 0m : Convert.ToDecimal(r);
        }
    }
}

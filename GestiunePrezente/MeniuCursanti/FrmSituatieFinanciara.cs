using GestiunePrezente.Common;
using GestiunePrezente.MeniuPrincipal;
using GestiunePrezente.SmartBill;
using Microsoft.Data.SqlClient;

namespace GestiunePrezente.MeniuCursanti
{
    public class FrmSituatieFinanciara : Form
    {
        private readonly FrmMeniuPrincipal m_parent;
        private readonly CManagerDeProiect m_manager;

        // Top grid — contracts
        private DataGridView m_gridContracte = null!;
        private Label        m_lblRezumat    = null!;

        // KPI tiles
        private Label m_kpiNefact   = null!;
        private Label m_kpiNeincas  = null!;
        private Label m_kpiFact     = null!;
        private Label m_kpiIncas    = null!;
        private Label m_kpiTotal    = null!;

        // Bottom tabs
        private TabControl   m_tabs          = null!;
        private DataGridView m_gridLunar     = null!;
        private DataGridView m_gridSedinte   = null!;
        private DateTimePicker m_dtpSedLuna  = null!;
        private Label          m_lblSedInfo  = null!;

        // Filter state
        private bool m_doarActiv = true;

        // Selected contract
        private int  m_cucoId   = 0;
        private int  m_cucoDiv  = 0;

        public const string MBME_NUME = "Situatie financiara";
        public static bool PoateDeschide(CManagerDeProiect mm)
            => DrepturiFormular.PoateDeschide(mm, MBME_NUME);

        // ── Culori status ─────────────────────────────────────────────────
        private static readonly Color C_VERDE   = Color.FromArgb(220, 252, 231);
        private static readonly Color C_PORTOC  = Color.FromArgb(255, 237, 213);
        private static readonly Color C_ROSU    = Color.FromArgb(254, 226, 226);
        private static readonly Color C_GRIS    = Color.FromArgb(243, 244, 246);

        public FrmSituatieFinanciara(FrmMeniuPrincipal parent, CManagerDeProiect mm)
        {
            m_parent  = parent;
            m_manager = mm;

            Text      = "Situație financiară";
            BackColor = Color.FromArgb(248, 250, 252);
            Font      = new Font("Segoe UI", 9.5F);
            MinimumSize = new Size(900, 600);

            BuildUI();
            Load += (_, _) => IncarcaContracte();
        }

        // ════════════════════════════════════════════════════════════════
        // CONSTRUIRE UI
        // ════════════════════════════════════════════════════════════════
        private void BuildUI()
        {
            // ── Toolbar + KPI (un singur panou) ─────────────────────────
            var toolbar = new Panel
            {
                Dock = DockStyle.Top, Height = 120, BackColor = Color.White
            };
            toolbar.Controls.Add(new Panel { Dock = DockStyle.Bottom, Height = 1,
                BackColor = Color.FromArgb(229, 231, 235) });

            // Rând 1: checkbox + buton import (y=8)
            var chkActiv = new CheckBox
            {
                Text = "Doar contracte active", Checked = true,
                Location = new Point(12, 12), Width = 180, Height = 24,
                TextAlign = ContentAlignment.MiddleLeft, Cursor = Cursors.Hand
            };
            chkActiv.CheckedChanged += (_, _) => { m_doarActiv = chkActiv.Checked; IncarcaContracte(); };
            toolbar.Controls.Add(chkActiv);

            var btnImport = new Button
            {
                Text = "Importă facturi SmartBill",
                Location = new Point(toolbar.Width - 232, 8), Width = 220, Height = 30,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(37, 99, 235), ForeColor = Color.White,
                Cursor = Cursors.Hand, UseVisualStyleBackColor = false
            };
            btnImport.FlatAppearance.BorderSize = 0;
            btnImport.Click += (_, _) =>
            {
                var frm = new FrmImportSmartBill(m_parent, m_manager) { MdiParent = m_parent };
                frm.FormClosed += (_, _) => { IncarcaContracte(); if (m_cucoId > 0) IncarcaLunarAbonament(); };
                frm.Show();
            };
            toolbar.Controls.Add(btnImport);

            // Separator între rândul 1 și KPI
            toolbar.Controls.Add(new Panel { Location = new Point(0, 46), Size = new Size(2000, 1),
                BackColor = Color.FromArgb(229, 231, 235), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right });

            // Rând 2: KPI tiles (y=52)
            (m_kpiNefact,  var t1) = CreazaTile("—", "De facturat",        Color.FromArgb(254, 226, 226), Color.FromArgb(185, 28,  28));
            (m_kpiNeincas, var t2) = CreazaTile("—", "De încasat",         Color.FromArgb(255, 237, 213), Color.FromArgb(154, 52,  18));
            (m_kpiFact,    var t3) = CreazaTile("—", "Total facturat",     Color.FromArgb(219, 234, 254), Color.FromArgb(29,  78, 216));
            (m_kpiIncas,   var t4) = CreazaTile("—", "Total încasat",      Color.FromArgb(220, 252, 231), Color.FromArgb(22, 101,  52));
            (m_kpiTotal,   var t5) = CreazaTile("—", "Val. contracte/lună",Color.FromArgb(243, 232, 255), Color.FromArgb(109,  40, 217));

            int tileX = 10;
            foreach (var tile in new[] { t1, t2, t3, t4, t5 })
            {
                tile.Location = new Point(tileX, 52);
                tile.Size     = new Size(178, 60);
                toolbar.Controls.Add(tile);
                tileX += 186;
            }

            Controls.Add(toolbar);

            // ── Banner rezumat ───────────────────────────────────────────
            m_lblRezumat = new Label
            {
                Dock = DockStyle.Top, Height = 30,
                BackColor = Color.FromArgb(239, 246, 255),
                ForeColor = Color.FromArgb(30, 64, 175),
                Font = new Font("Segoe UI Semibold", 9.5F),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(14, 0, 0, 0),
                Text = ""
            };
            Controls.Add(m_lblRezumat);

            // ── Splitter vertical ────────────────────────────────────────
            var split = new SplitContainer
            {
                Dock = DockStyle.Fill, Orientation = Orientation.Horizontal,
                SplitterDistance = 220, BackColor = Color.FromArgb(229, 231, 235)
            };

            // Panel sus — contracte
            var pSus = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(0) };
            m_gridContracte = CreeazaGrid();
            m_gridContracte.Dock = DockStyle.Fill;
            m_gridContracte.SelectionChanged += (_, _) => OnContractSelectat();
            pSus.Controls.Add(m_gridContracte);
            split.Panel1.Controls.Add(pSus);

            // Panel jos — tabs
            m_tabs = new TabControl { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 9.5F) };

            var tabLunar = new TabPage("Abonament lunar");
            m_gridLunar  = CreeazaGridLunar();
            m_gridLunar.Dock = DockStyle.Fill;
            tabLunar.Controls.Add(m_gridLunar);
            m_tabs.TabPages.Add(tabLunar);

            var tabSedinte = new TabPage("Ședințe individuale");

            // Toolbar for individual sessions tab
            var pSedTop = new Panel { Dock = DockStyle.Top, Height = 36,
                BackColor = Color.FromArgb(249, 250, 251),
                Padding = new Padding(6, 4, 6, 4) };
            pSedTop.Controls.Add(new Label { Text = "Luna:", AutoSize = true,
                Location = new Point(6, 10), Name = "lblLunaSed" });

            m_dtpSedLuna = new DateTimePicker
            {
                Format = DateTimePickerFormat.Custom, CustomFormat = "MM / yyyy",
                ShowUpDown = true, Width = 100, Location = new Point(50, 6),
                Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
            };
            m_dtpSedLuna.ValueChanged += (_, _) => IncarcaSedinteIndividuale();
            pSedTop.Controls.Add(m_dtpSedLuna);

            var btnFactLuna = new Button
            {
                Text = "⚡ Facturează luna selectată",
                Location = new Point(162, 4), Width = 200, Height = 26,
                FlatStyle = FlatStyle.Flat, UseVisualStyleBackColor = true
            };
            btnFactLuna.Click += BtnFacturLuna_Click;
            pSedTop.Controls.Add(btnFactLuna);

            m_lblSedInfo = new Label
            {
                AutoSize = true, Location = new Point(372, 10),
                ForeColor = Color.FromArgb(107, 114, 128)
            };
            pSedTop.Controls.Add(m_lblSedInfo);

            m_gridSedinte = CreeazaGridSedinte();
            m_gridSedinte.Dock = DockStyle.Fill;

            tabSedinte.Controls.Add(m_gridSedinte);
            tabSedinte.Controls.Add(pSedTop);
            m_tabs.TabPages.Add(tabSedinte);

            split.Panel2.Controls.Add(m_tabs);
            Controls.Add(split);
            Controls.SetChildIndex(split, 0);
        }

        private static (Label valLabel, Panel tile) CreazaTile(string val, string caption, Color bg, Color fg)
        {
            var tile = new Panel { BackColor = bg };
            var lblCap = new Label
            {
                Text = caption, ForeColor = fg,
                Font = new Font("Segoe UI", 7.5F),
                Location = new Point(8, 5), Size = new Size(162, 16),
                AutoSize = false
            };
            var lblVal = new Label
            {
                Text = val, ForeColor = fg,
                Font = new Font("Segoe UI Semibold", 13F),
                Location = new Point(6, 22), Size = new Size(166, 28),
                AutoSize = false
            };
            tile.Controls.Add(lblCap);
            tile.Controls.Add(lblVal);
            return (lblVal, tile);
        }

        private void IncarcaStatistici()
        {
            int div = m_manager.GetDiviziune();
            string sql = @"
SELECT
  -- contracte cu luni nefacturate
  (SELECT COUNT(DISTINCT C.CUCO_Id)
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
     )) AS NrDeFact,
  -- contracte cu facturi neîncasate integral
  (SELECT COUNT(DISTINCT F.FVFA_CUCO_Id)
   FROM FV_Facturi F
   INNER JOIN CU_Contracte C2 ON F.FVFA_CUCO_Id = C2.CUCO_Id
   INNER JOIN CU_ContracteCursanti CC2 ON CC2.CUCC_CUCO_Id = C2.CUCO_Id AND ISNULL(CC2.CUCC_Retras,0)=0
   INNER JOIN CU_Cursanti CU2 ON CC2.CUCC_CUCU_Id = CU2.CUCU_Id AND CU2.CUCU_MBDV_Id = @div
   WHERE F.FVFA_Suma > ISNULL((SELECT SUM(CUIN_Valoare) FROM CU_Incasari WHERE CUIN_FVFA_Id=F.FVFA_Id),0)) AS NrDeIncas,
  -- total facturat
  (SELECT ISNULL(SUM(F2.FVFA_Suma),0)
   FROM FV_Facturi F2
   INNER JOIN CU_Contracte C3 ON F2.FVFA_CUCO_Id = C3.CUCO_Id
   INNER JOIN CU_ContracteCursanti CC3 ON CC3.CUCC_CUCO_Id = C3.CUCO_Id AND ISNULL(CC3.CUCC_Retras,0)=0
   INNER JOIN CU_Cursanti CU3 ON CC3.CUCC_CUCU_Id = CU3.CUCU_Id AND CU3.CUCU_MBDV_Id = @div) AS TotalFact,
  -- total încasat
  (SELECT ISNULL(SUM(I.CUIN_Valoare),0)
   FROM CU_Incasari I
   INNER JOIN FV_Facturi F3 ON I.CUIN_FVFA_Id = F3.FVFA_Id
   INNER JOIN CU_Contracte C4 ON F3.FVFA_CUCO_Id = C4.CUCO_Id
   INNER JOIN CU_ContracteCursanti CC4 ON CC4.CUCC_CUCO_Id = C4.CUCO_Id AND ISNULL(CC4.CUCC_Retras,0)=0
   INNER JOIN CU_Cursanti CU4 ON CC4.CUCC_CUCU_Id = CU4.CUCU_Id AND CU4.CUCU_MBDV_Id = @div) AS TotalIncas,
  -- valoarea lunară a contractelor active (suma prețurilor abonamentelor active)
  (SELECT ISNULL(SUM(CUAC.CUAC_Pret),0)
   FROM CU_AbonamenteCursant CUAC
   INNER JOIN CU_Contracte C5 ON CUAC.CUAC_CUCO_Id = C5.CUCO_Id AND C5.CUCO_Retras = 0
   INNER JOIN CU_ContracteCursanti CC5 ON CC5.CUCC_CUCO_Id = C5.CUCO_Id AND ISNULL(CC5.CUCC_Retras,0)=0
   INNER JOIN CU_Cursanti CU5 ON CC5.CUCC_CUCU_Id = CU5.CUCU_Id AND CU5.CUCU_MBDV_Id = @div
   WHERE CUAC.CUAC_DataInceput <= GETDATE() AND CUAC.CUAC_DataSfarsit >= GETDATE()) AS TotalValoare";

            try
            {
                using var cmd = new SqlCommand(sql, m_manager.GetConnection());
                cmd.Parameters.AddWithValue("@div", div);
                using var rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    int nefact      = rd.GetInt32(0);
                    int neincas     = rd.GetInt32(1);
                    decimal fact    = rd.GetDecimal(2);
                    decimal incas   = rd.GetDecimal(3);
                    decimal valoare = rd.GetDecimal(4);

                    m_kpiNefact.Text  = nefact.ToString();
                    m_kpiNeincas.Text = neincas.ToString();
                    m_kpiFact.Text    = fact.ToString("N0")    + " lei";
                    m_kpiIncas.Text   = incas.ToString("N0")   + " lei";
                    m_kpiTotal.Text   = valoare.ToString("N0") + " lei";
                }
            }
            catch { /* statisticile sunt opționale, nu bloca UI */ }
        }

        private static DataGridView CreeazaGrid()
        {
            var g = new DataGridView
            {
                ReadOnly = true, AllowUserToAddRows = false, AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false, BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None, RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor = Color.FromArgb(229, 231, 235)
            };
            g.ColumnHeadersDefaultCellStyle.BackColor  = Color.FromArgb(249, 250, 251);
            g.ColumnHeadersDefaultCellStyle.ForeColor  = Color.FromArgb(107, 114, 128);
            g.ColumnHeadersDefaultCellStyle.Font       = new Font("Segoe UI Semibold", 8.5F);
            g.ColumnHeadersHeightSizeMode              = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            g.ColumnHeadersHeight                      = 32;
            g.DefaultCellStyle.SelectionBackColor      = Color.FromArgb(239, 246, 255);
            g.DefaultCellStyle.SelectionForeColor      = Color.FromArgb(17, 24, 39);
            return g;
        }

        private DataGridView CreeazaGridLunar()
        {
            var g = CreeazaGrid();
            g.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_luna",    HeaderText = "Luna",            FillWeight = 15 });
            g.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_nrfact",  HeaderText = "Nr. factură",     FillWeight = 14 });
            g.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_suma",    HeaderText = "Sumă (lei)",      FillWeight = 10, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight } });
            g.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_incasat", HeaderText = "Încasat (lei)",   FillWeight = 10, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight } });
            g.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_rest",    HeaderText = "Rest (lei)",      FillWeight = 10, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight } });
            g.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_status",  HeaderText = "Status",          FillWeight = 14 });

            var colBtn = new DataGridViewButtonColumn
            {
                Name = "col_actiune", HeaderText = "", FillWeight = 12,
                FlatStyle = FlatStyle.Flat, UseColumnTextForButtonValue = false
            };
            g.Columns.Add(colBtn);

            g.CellFormatting += GridLunar_CellFormatting;
            g.CellClick      += GridLunar_CellClick;

            // Store extra data per row
            g.Tag = new Dictionary<int, (int fvfaId, DateTime perioadaInc, decimal suma)>();
            return g;
        }

        private DataGridView CreeazaGridSedinte()
        {
            var g = CreeazaGrid();
            g.Columns.Add(new DataGridViewTextBoxColumn  { Name = "col_data",     HeaderText = "Data",          FillWeight = 12 });
            g.Columns.Add(new DataGridViewTextBoxColumn  { Name = "col_cursant",  HeaderText = "Cursant",       FillWeight = 22 });
            g.Columns.Add(new DataGridViewTextBoxColumn  { Name = "col_formator", HeaderText = "Formator",      FillWeight = 18 });
            g.Columns.Add(new DataGridViewTextBoxColumn  { Name = "col_tip",      HeaderText = "Tip ședință",   FillWeight = 14 });
            g.Columns.Add(new DataGridViewTextBoxColumn  { Name = "col_pret",     HeaderText = "Preț (lei)",    FillWeight = 9, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight } });
            g.Columns.Add(new DataGridViewTextBoxColumn  { Name = "col_sfact",    HeaderText = "Status factură",FillWeight = 12 });
            g.Columns.Add(new DataGridViewButtonColumn   { Name = "col_act_sed",  HeaderText = "",              FillWeight = 10, FlatStyle = FlatStyle.Flat, UseColumnTextForButtonValue = false });

            // Hidden IDs
            g.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_cupd_id",  Visible = false });
            g.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_cucu_id",  Visible = false });
            g.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_pret_val", Visible = false });

            g.CellFormatting += GridSedinte_CellFormatting;
            g.CellClick      += GridSedinte_CellClick;
            return g;
        }

        // ════════════════════════════════════════════════════════════════
        // ÎNCĂRCARE DATE — CONTRACTE
        // ════════════════════════════════════════════════════════════════
        private void IncarcaContracte()
        {
            m_gridContracte.Rows.Clear();
            m_gridContracte.Columns.Clear();
            m_gridContracte.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_cuco_id", Visible = false });
            m_gridContracte.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_cursanti", HeaderText = "Cursant(i)",       FillWeight = 30 });
            m_gridContracte.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_nrcont",   HeaderText = "Nr. contract",     FillWeight = 12 });
            m_gridContracte.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_abon",     HeaderText = "Abonament",        FillWeight = 20 });
            m_gridContracte.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_pret",     HeaderText = "Preț/lună (lei)", FillWeight = 12 });
            m_gridContracte.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_badge",    HeaderText = "Nefacturate",     FillWeight = 14,
                DefaultCellStyle = { ForeColor = Color.FromArgb(220, 38, 38), Font = new Font("Segoe UI Semibold", 9F) } });

            int div = m_manager.GetDiviziune();
            string filtruActiv = m_doarActiv
                ? " AND C.CUCO_Retras = 0 AND CUAC.CUAC_DataInceput <= GETDATE() AND CUAC.CUAC_DataSfarsit >= GETDATE()"
                : " AND C.CUCO_Retras = 0";

            string sql = $@"
SELECT C.CUCO_Id,
       STUFF((SELECT ', ' + CU2.CUCU_Nume + ' ' + CU2.CUCU_Prenume
              FROM CU_ContracteCursanti CC2
              INNER JOIN CU_Cursanti CU2 ON CC2.CUCC_CUCU_Id = CU2.CUCU_Id
              WHERE CC2.CUCC_CUCO_Id = C.CUCO_Id AND ISNULL(CC2.CUCC_Retras,0) = 0
              FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS Cursanti,
       ISNULL(CONVERT(NVARCHAR(50), C.CUCO_NrContract), ''),
       ISNULL(AB.CUAB_Descriere, ''),
       ISNULL(CUAC.CUAC_Pret, 0),
       -- Luni nefacturate din abonament lunar
       (SELECT COUNT(*)
        FROM (SELECT DISTINCT DATEFROMPARTS(YEAR(d.d), MONTH(d.d), 1) AS luna
              FROM (SELECT DATEADD(MONTH, n.n, DATEFROMPARTS(YEAR(CUAC2.CUAC_DataInceput), MONTH(CUAC2.CUAC_DataInceput), 1)) AS d
                    FROM CU_AbonamenteCursant CUAC2
                    INNER JOIN CU_Abonamente AB2 ON CUAC2.CUAC_CUAB_Id = AB2.CUAB_Id
                    CROSS JOIN (VALUES(0),(1),(2),(3),(4),(5),(6),(7),(8),(9),(10),(11),(12),(13),(14),(15),(16),(17),(18),(19),(20),(21),(22),(23)) n(n)
                    WHERE CUAC2.CUAC_CUCO_Id = C.CUCO_Id AND AB2.CUAB_SedintaIndividuala = 0
                      AND CUAC2.CUAC_DataSfarsit >= GETDATE()
                      AND DATEADD(MONTH, n.n, DATEFROMPARTS(YEAR(CUAC2.CUAC_DataInceput), MONTH(CUAC2.CUAC_DataInceput), 1)) <= CUAC2.CUAC_DataSfarsit
                      AND DATEADD(MONTH, n.n, DATEFROMPARTS(YEAR(CUAC2.CUAC_DataInceput), MONTH(CUAC2.CUAC_DataInceput), 1)) <= GETDATE()) d
             ) luniFact
        WHERE NOT EXISTS (SELECT 1 FROM FV_Facturi F
                          WHERE F.FVFA_CUCO_Id = C.CUCO_Id
                            AND DATEFROMPARTS(YEAR(F.FVFA_PerioadaInceput), MONTH(F.FVFA_PerioadaInceput), 1) = luniFact.luna))
       +
       -- Sedinte individuale nefacturate
       (SELECT COUNT(*) FROM CU_PrezenteDetaliu PD
        INNER JOIN CU_Prezente PR ON PD.CUPD_CUPR_Id = PR.CUPR_Id
        INNER JOIN CU_TipPrezenta TP ON PR.CUPR_CUTP_Id = TP.CUTP_Id
        WHERE PD.CUPD_CUCO_Id = C.CUCO_Id AND TP.CUTP_SedintaIndividuala = 1
          AND PD.CUPD_FVFA_Id IS NULL)
       AS NrNefacturate
FROM CU_Contracte C
INNER JOIN CU_ContracteCursanti CC ON CC.CUCC_CUCO_Id = C.CUCO_Id AND ISNULL(CC.CUCC_Retras,0) = 0
INNER JOIN CU_Cursanti CU ON CC.CUCC_CUCU_Id = CU.CUCU_Id AND CU.CUCU_MBDV_Id = @div
LEFT JOIN CU_AbonamenteCursant CUAC ON CUAC.CUAC_CUCO_Id = C.CUCO_Id
    AND CUAC.CUAC_DataInceput <= GETDATE() AND CUAC.CUAC_DataSfarsit >= GETDATE()
LEFT JOIN CU_Abonamente AB ON CUAC.CUAC_CUAB_Id = AB.CUAB_Id AND AB.CUAB_SedintaIndividuala = 0
WHERE 1=1 {filtruActiv}
GROUP BY C.CUCO_Id, C.CUCO_NrContract, AB.CUAB_Descriere, CUAC.CUAC_Pret
ORDER BY NrNefacturate DESC, MAX(CU.CUCU_Nume)";

            int totalNefact = 0, contracteProblema = 0;
            int primulProblemaRow = -1;

            try
            {
                var conn = m_manager.GetConnection();
                using var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@div", div);
                using var rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    int cucoId      = rd.GetInt32(0);
                    string cursanti = rd.GetString(1);
                    string nrCont   = rd.GetString(2);
                    string abon     = rd.GetString(3);
                    decimal pret    = rd.GetDecimal(4);
                    int nefact      = rd.GetInt32(5);

                    int row = m_gridContracte.Rows.Add(
                        cucoId, cursanti, nrCont, abon,
                        pret.ToString("N0"),
                        nefact > 0 ? $"⚠ {nefact}" : "✓ La zi");

                    if (nefact > 0)
                    {
                        m_gridContracte.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(255, 251, 235);
                        totalNefact += nefact;
                        contracteProblema++;
                        if (primulProblemaRow < 0) primulProblemaRow = row;
                    }
                    else
                    {
                        m_gridContracte.Rows[row].DefaultCellStyle.BackColor = C_VERDE;
                    }
                }
            }
            catch (Exception ex)
            {
                CEroare.Arata("Eroare încărcare contracte", ex.Message);
            }

            // Actualizare banner
            if (totalNefact == 0)
            {
                m_lblRezumat.BackColor = Color.FromArgb(220, 252, 231);
                m_lblRezumat.ForeColor = Color.FromArgb(22, 101, 52);
                m_lblRezumat.Text = "✓  Toate contractele sunt la zi — nimic nefacturat.";
            }
            else
            {
                m_lblRezumat.BackColor = Color.FromArgb(254, 226, 226);
                m_lblRezumat.ForeColor = Color.FromArgb(185, 28, 28);
                m_lblRezumat.Text = $"⚠  {totalNefact} luni nefacturate pe {contracteProblema} contract{(contracteProblema > 1 ? "e" : "")} — importați facturile din SmartBill și asociați-le.";
            }

            // Autoselectare primul contract cu probleme (SelectionChanged va încărca tab-urile)
            if (primulProblemaRow >= 0)
                m_gridContracte.Rows[primulProblemaRow].Selected = true;
            else if (m_gridContracte.Rows.Count > 0)
                m_gridContracte.Rows[0].Selected = true;

            IncarcaStatistici();
        }

        private void OnContractSelectat()
        {
            if (m_gridContracte.SelectedRows.Count == 0) return;
            var row = m_gridContracte.SelectedRows[0];
            m_cucoId = Convert.ToInt32(row.Cells["col_cuco_id"].Value);

            IncarcaLunarAbonament();
            IncarcaSedinteIndividuale();
        }

        // ════════════════════════════════════════════════════════════════
        // TAB 1 — ABONAMENT LUNAR
        // ════════════════════════════════════════════════════════════════
        private void IncarcaLunarAbonament()
        {
            m_gridLunar.Rows.Clear();
            if (m_cucoId == 0) return;
            var extra = (Dictionary<int, (int, DateTime, decimal)>)m_gridLunar.Tag!;
            extra.Clear();

            // Generam lista de luni din abonamentele lunare ale contractului
            string sql = @"
SELECT DATEFROMPARTS(YEAR(d.luna), MONTH(d.luna), 1) AS PerioadaInceput,
       F.FVFA_Id,
       ISNULL(F.FVFA_NrFactura, ''),
       ISNULL(F.FVFA_Suma, 0),
       ISNULL((SELECT SUM(CUIN_Valoare) FROM CU_Incasari WHERE CUIN_FVFA_Id = F.FVFA_Id), 0),
       CUAC.CUAC_Pret
FROM (
    SELECT DISTINCT DATEADD(MONTH, n.n, DATEFROMPARTS(YEAR(CUAC2.CUAC_DataInceput), MONTH(CUAC2.CUAC_DataInceput), 1)) AS luna
    FROM CU_AbonamenteCursant CUAC2
    INNER JOIN CU_Abonamente AB2 ON CUAC2.CUAC_CUAB_Id = AB2.CUAB_Id
    CROSS JOIN (VALUES(0),(1),(2),(3),(4),(5),(6),(7),(8),(9),(10),(11),(12),(13),(14),(15),(16),(17),(18),(19),(20),(21),(22),(23)) n(n)
    WHERE CUAC2.CUAC_CUCO_Id = @cuco AND AB2.CUAB_SedintaIndividuala = 0
      AND CUAC2.CUAC_DataSfarsit >= GETDATE()
      AND DATEADD(MONTH, n.n, DATEFROMPARTS(YEAR(CUAC2.CUAC_DataInceput), MONTH(CUAC2.CUAC_DataInceput), 1)) <= CUAC2.CUAC_DataSfarsit
      AND DATEADD(MONTH, n.n, DATEFROMPARTS(YEAR(CUAC2.CUAC_DataInceput), MONTH(CUAC2.CUAC_DataInceput), 1)) <= GETDATE()
) d
LEFT JOIN FV_Facturi F ON F.FVFA_CUCO_Id = @cuco
    AND DATEFROMPARTS(YEAR(F.FVFA_PerioadaInceput), MONTH(F.FVFA_PerioadaInceput), 1) = d.luna
LEFT JOIN CU_AbonamenteCursant CUAC ON CUAC.CUAC_CUCO_Id = @cuco
    AND CUAC.CUAC_DataInceput <= d.luna
    AND CUAC.CUAC_DataSfarsit >= d.luna
    AND EXISTS (SELECT 1 FROM CU_Abonamente AB3 WHERE AB3.CUAB_Id = CUAC.CUAC_CUAB_Id AND AB3.CUAB_SedintaIndividuala = 0)
ORDER BY d.luna DESC";

            try
            {
                var conn = m_manager.GetConnection();
                using var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@cuco", m_cucoId);
                using var rd  = cmd.ExecuteReader();
                while (rd.Read())
                {
                    var luna     = rd.GetDateTime(0);
                    int fvfaId   = rd.IsDBNull(1) ? 0 : rd.GetInt32(1);
                    string nrFact = rd.GetString(2);
                    decimal suma  = rd.GetDecimal(3);
                    decimal inc   = rd.GetDecimal(4);
                    decimal pret  = rd.IsDBNull(5) ? 0m : rd.GetDecimal(5);
                    decimal rest  = suma - inc;

                    bool areFactura = fvfaId > 0;
                    string status   = !areFactura ? "Lipsă factură"
                                    : rest <= 0  ? "Încasat"
                                    : inc  > 0   ? "Parțial"
                                    :              "Neîncasat";
                    string btnText  = areFactura ? "Deschide" : "→ Importă";

                    string numaLuna = luna.ToString("MMMM yyyy",
                        System.Globalization.CultureInfo.GetCultureInfo("ro-RO"));
                    numaLuna = char.ToUpper(numaLuna[0]) + numaLuna[1..];

                    int row = m_gridLunar.Rows.Add(
                        numaLuna,
                        areFactura ? nrFact : "",
                        areFactura ? suma.ToString("N2") : "",
                        areFactura ? inc.ToString("N2")  : "",
                        areFactura ? rest.ToString("N2") : "",
                        status,
                        btnText);

                    // Color row by status
                    m_gridLunar.Rows[row].DefaultCellStyle.BackColor =
                        status == "Încasat"       ? C_VERDE  :
                        status == "Parțial"       ? C_PORTOC :
                        status == "Neîncasat"     ? C_PORTOC :
                        status == "Lipsă factură" ? C_ROSU   : C_GRIS;

                    extra[row] = (fvfaId, luna, pret);
                }
            }
            catch (Exception ex)
            {
                CEroare.Arata("Eroare abonament lunar", ex.Message);
            }
        }

        private void GridLunar_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (m_gridLunar.Columns[e.ColumnIndex].Name != "col_actiune") return;
            var extra = (Dictionary<int, (int fvfaId, DateTime, decimal)>)m_gridLunar.Tag!;
            if (!extra.TryGetValue(e.RowIndex, out var d)) return;
            e.Value = d.fvfaId > 0 ? "Deschide" : "→ Importă";
            e.FormattingApplied = true;
        }

        private void GridLunar_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || m_gridLunar.Columns[e.ColumnIndex].Name != "col_actiune") return;
            var extra = (Dictionary<int, (int fvfaId, DateTime perioadaInc, decimal pret)>)m_gridLunar.Tag!;
            if (!extra.TryGetValue(e.RowIndex, out var d)) return;

            if (d.fvfaId > 0)
            {
                if (!FrmGestiuneFactura.PoateDeschide(m_manager)) return;
                var frm = new FrmGestiuneFactura(m_manager, m_cucoId, d.fvfaId)
                    { MdiParent = m_parent };
                frm.FormClosed += (_, _) => IncarcaLunarAbonament();
                frm.Show();
            }
            else
            {
                // Deschide Import SmartBill cu luna pre-selectată
                var frm = new FrmImportSmartBill(m_parent, m_manager, d.perioadaInc)
                    { MdiParent = m_parent };
                frm.FormClosed += (_, _) => { IncarcaContracte(); };
                frm.Show();
            }
        }

        // ════════════════════════════════════════════════════════════════
        // TAB 2 — ȘEDINȚE INDIVIDUALE
        // ════════════════════════════════════════════════════════════════
        private void IncarcaSedinteIndividuale()
        {
            m_gridSedinte.Rows.Clear();
            if (m_cucoId == 0) return;

            var luna = new DateTime(m_dtpSedLuna.Value.Year, m_dtpSedLuna.Value.Month, 1);
            var lunaSf = luna.AddMonths(1).AddDays(-1);

            string sql = @"
SELECT PD.CUPD_Id,
       PR.CUPR_DeLaData,
       CU.CUCU_Nume + ' ' + CU.CUCU_Prenume,
       ISNULL(FO.CUFO_Nume + ' ' + FO.CUFO_Prenume, ''),
       TP.CUTP_Descriere,
       ISNULL(CUAC.CUAC_Pret, 0),
       CASE WHEN PD.CUPD_FVFA_Id IS NULL THEN 'Nefacturată' ELSE 'Facturată' END,
       PD.CUPD_CUCU_Id,
       ISNULL(CUAC.CUAC_Pret, 0)
FROM CU_PrezenteDetaliu PD
INNER JOIN CU_Prezente PR ON PD.CUPD_CUPR_Id = PR.CUPR_Id
INNER JOIN CU_TipPrezenta TP ON PR.CUPR_CUTP_Id = TP.CUTP_Id
INNER JOIN CU_Cursanti CU ON PD.CUPD_CUCU_Id = CU.CUCU_Id
LEFT JOIN CU_Formatori FO ON PR.CUPR_CUFO_Id = FO.CUFO_Id
LEFT JOIN CU_AbonamenteCursant CUAC ON CUAC.CUAC_CUCO_Id = PD.CUPD_CUCO_Id
    AND CUAC.CUAC_DataInceput <= PR.CUPR_DeLaData
    AND CUAC.CUAC_DataSfarsit >= PR.CUPR_DeLaData
    AND EXISTS (SELECT 1 FROM CU_Abonamente AB WHERE AB.CUAB_Id = CUAC.CUAC_CUAB_Id AND AB.CUAB_SedintaIndividuala = 1)
WHERE PD.CUPD_CUCO_Id = @cuco AND TP.CUTP_SedintaIndividuala = 1
  AND PR.CUPR_DeLaData >= @luna AND PR.CUPR_DeLaData <= @lunaSf
ORDER BY PR.CUPR_DeLaData DESC";

            int nrNefact = 0;
            decimal totalNefactSuma = 0m;
            try
            {
                var conn = m_manager.GetConnection();
                using var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@cuco",   m_cucoId);
                cmd.Parameters.AddWithValue("@luna",   luna);
                cmd.Parameters.AddWithValue("@lunaSf", lunaSf);
                using var rd  = cmd.ExecuteReader();
                while (rd.Read())
                {
                    int cupdId    = rd.GetInt32(0);
                    string data   = rd.GetDateTime(1).ToString("dd.MM.yyyy");
                    string curs   = rd.GetString(2);
                    string form   = rd.GetString(3);
                    string tip    = rd.GetString(4);
                    decimal pret  = rd.GetDecimal(5);
                    string sfact  = rd.GetString(6);
                    int cucuId    = rd.GetInt32(7);

                    bool nefact   = sfact == "Nefacturată";
                    if (nefact) { nrNefact++; totalNefactSuma += pret; }

                    int row = m_gridSedinte.Rows.Add(
                        data, curs, form, tip,
                        pret > 0 ? pret.ToString("N0") : "— preț nedefinit",
                        sfact,
                        cupdId, cucuId, pret);

                    m_gridSedinte.Rows[row].DefaultCellStyle.BackColor =
                        nefact ? C_ROSU : C_VERDE;

                    if (pret == 0 && nefact)
                        m_gridSedinte.Rows[row].DefaultCellStyle.ForeColor = Color.FromArgb(220, 38, 38);
                }
            }
            catch (Exception ex)
            {
                CEroare.Arata("Eroare ședințe individuale", ex.Message);
            }

            m_lblSedInfo.Text = nrNefact == 0
                ? "✓ Toate facturate"
                : $"⚠ {nrNefact} nefacturate · total {totalNefactSuma:N0} lei";
        }

        private void GridSedinte_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || m_gridSedinte.Columns[e.ColumnIndex].Name != "col_act_sed") return;
            string sfact = m_gridSedinte.Rows[e.RowIndex].Cells["col_sfact"].Value?.ToString() ?? "";
            e.Value = sfact == "Nefacturată" ? "→ Factură" : "Deschide";
            e.FormattingApplied = true;
        }

        private void GridSedinte_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || m_gridSedinte.Columns[e.ColumnIndex].Name != "col_act_sed") return;
            var row   = m_gridSedinte.Rows[e.RowIndex];
            string sfact = row.Cells["col_sfact"].Value?.ToString() ?? "";
            int cupdId   = Convert.ToInt32(row.Cells["col_cupd_id"].Value);

            if (sfact == "Nefacturată")
            {
                string cursant = row.Cells["col_cursant"].Value?.ToString() ?? "";
                string data    = row.Cells["col_data"].Value?.ToString() ?? "";
                decimal pret   = decimal.TryParse(row.Cells["col_pret_val"].Value?.ToString(), out var p) ? p : 0m;

                MarcheazaFacurataSedintas(cupdId, cursant, data, pret);
            }
            else
            {
                // Retrieve FVFA_Id from CUPD and open invoice form
                try
                {
                    using var cmd = new SqlCommand(
                        "SELECT CUPD_FVFA_Id FROM CU_PrezenteDetaliu WHERE CUPD_Id = @id",
                        m_manager.GetConnection());
                    cmd.Parameters.AddWithValue("@id", cupdId);
                    var val = cmd.ExecuteScalar();
                    if (val == null || val == DBNull.Value) return;
                    int fvfaId = Convert.ToInt32(val);
                    if (!FrmGestiuneFactura.PoateDeschide(m_manager)) return;
                    var frm = new FrmGestiuneFactura(m_manager, m_cucoId, fvfaId) { MdiParent = m_parent };
                    frm.FormClosed += (_, _) => IncarcaSedinteIndividuale();
                    frm.Show();
                }
                catch (Exception ex) { CEroare.Arata("Eroare", ex.Message); }
            }
        }

        private void BtnFacturLuna_Click(object? sender, EventArgs e)
        {
            if (m_cucoId == 0) { MessageBox.Show("Selectați un contract.", "Atenție", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            var luna   = new DateTime(m_dtpSedLuna.Value.Year, m_dtpSedLuna.Value.Month, 1);
            var lunaSf = luna.AddMonths(1).AddDays(-1);

            // Collect all uninvoiced sessions for selected month
            var sedinte = new List<(int cupdId, string cursant, string data, decimal pret)>();
            try
            {
                string sqlSed = @"
SELECT PD.CUPD_Id,
       CU.CUCU_Nume + ' ' + CU.CUCU_Prenume,
       PR.CUPR_DeLaData,
       ISNULL(CUAC.CUAC_Pret, 0)
FROM CU_PrezenteDetaliu PD
INNER JOIN CU_Prezente PR ON PD.CUPD_CUPR_Id = PR.CUPR_Id
INNER JOIN CU_TipPrezenta TP ON PR.CUPR_CUTP_Id = TP.CUTP_Id
INNER JOIN CU_Cursanti CU ON PD.CUPD_CUCU_Id = CU.CUCU_Id
LEFT JOIN CU_AbonamenteCursant CUAC ON CUAC.CUAC_CUCO_Id = PD.CUPD_CUCO_Id
    AND CUAC.CUAC_DataInceput <= PR.CUPR_DeLaData AND CUAC.CUAC_DataSfarsit >= PR.CUPR_DeLaData
    AND EXISTS (SELECT 1 FROM CU_Abonamente AB WHERE AB.CUAB_Id = CUAC.CUAC_CUAB_Id AND AB.CUAB_SedintaIndividuala = 1)
WHERE PD.CUPD_CUCO_Id = @cuco AND TP.CUTP_SedintaIndividuala = 1
  AND PD.CUPD_FVFA_Id IS NULL
  AND PR.CUPR_DeLaData >= @luna AND PR.CUPR_DeLaData <= @lunaSf
ORDER BY PR.CUPR_DeLaData";
                using var cmd = new SqlCommand(sqlSed, m_manager.GetConnection());
                cmd.Parameters.AddWithValue("@cuco",   m_cucoId);
                cmd.Parameters.AddWithValue("@luna",   luna);
                cmd.Parameters.AddWithValue("@lunaSf", lunaSf);
                using var rd = cmd.ExecuteReader();
                while (rd.Read())
                    sedinte.Add((rd.GetInt32(0), rd.GetString(1), rd.GetDateTime(2).ToString("dd.MM.yyyy"), rd.GetDecimal(3)));
            }
            catch (Exception ex) { CEroare.Arata("Eroare", ex.Message); return; }

            if (sedinte.Count == 0)
            {
                MessageBox.Show("Nu există ședințe nefacturate pentru luna selectată.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            decimal totalSuma = sedinte.Sum(s => s.pret);
            string numeLuna   = luna.ToString("MMMM yyyy", System.Globalization.CultureInfo.GetCultureInfo("ro-RO"));
            numeLuna          = char.ToUpper(numeLuna[0]) + numeLuna[1..];

            // Dialog confirmare + nr. factură
            using var dlg = new Form
            {
                Text = $"Facturează toate ședințele — {numeLuna}",
                Size = new Size(440, 270),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false, MinimizeBox = false
            };

            var lblInfo = new Label
            {
                Text = $"{sedinte.Count} ședință(e) nefacturată(e) în {numeLuna}.\nSe va crea O singură factură pentru toate.",
                Left = 12, Top = 12, Width = 410, Height = 40, AutoSize = false
            };
            var lblNr = new Label { Text = "Nr. factură SmartBill:", Left = 12, Top = 60, AutoSize = true };
            var tbNr  = new TextBox { Left = 12, Top = 80, Width = 400 };
            var lblDt = new Label { Text = "Data facturii:", Left = 12, Top = 108, AutoSize = true };
            var dtp   = new DateTimePicker { Left = 12, Top = 128, Width = 180,
                Format = DateTimePickerFormat.Short, Value = DateTime.Today };
            var lblSuma = new Label { Text = "Sumă totală (lei):", Left = 210, Top = 108, AutoSize = true };
            var nudSuma = new NumericUpDown { Left = 210, Top = 128, Width = 200,
                Minimum = 0, Maximum = 999999, DecimalPlaces = 2, Value = totalSuma };
            var btnOk  = new Button { Text = "Salvează", Left = 230, Top = 190, Width = 90, DialogResult = DialogResult.OK };
            var btnAnn = new Button { Text = "Anulează", Left = 330, Top = 190, Width = 90, DialogResult = DialogResult.Cancel };

            dlg.Controls.AddRange(new Control[] { lblInfo, lblNr, tbNr, lblDt, dtp, lblSuma, nudSuma, btnOk, btnAnn });
            dlg.AcceptButton = btnOk;
            dlg.CancelButton = btnAnn;

            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            string nrFact = tbNr.Text.Trim();
            if (string.IsNullOrEmpty(nrFact))
            {
                MessageBox.Show("Introduceți numărul facturii.", "Câmp obligatoriu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Create one FV_Facturi record and link all sessions
                using var cmdIns = new SqlCommand(@"
                    INSERT INTO FV_Facturi
                        (FVFA_CUCO_Id, FVFA_NrFactura, FVFA_DataEmitere,
                         FVFA_PerioadaInceput, FVFA_PerioadaSfarsit, FVFA_Suma,
                         FVFA_MBUT_Id, FVFA_DataSalvarii)
                    OUTPUT INSERTED.FVFA_Id
                    VALUES (@cuco,@nrf,@de,@pi,@ps,@suma,@u,GETDATE())",
                    m_manager.GetConnection());
                cmdIns.Parameters.AddWithValue("@cuco", m_cucoId);
                cmdIns.Parameters.AddWithValue("@nrf",  nrFact);
                cmdIns.Parameters.AddWithValue("@de",   dtp.Value);
                cmdIns.Parameters.AddWithValue("@pi",   luna);
                cmdIns.Parameters.AddWithValue("@ps",   lunaSf);
                cmdIns.Parameters.AddWithValue("@suma", nudSuma.Value);
                cmdIns.Parameters.AddWithValue("@u",    m_manager.GetUserId());
                int fvfaId = (int)cmdIns.ExecuteScalar();

                foreach (var sed in sedinte)
                {
                    using var cmdUpd = new SqlCommand(
                        "UPDATE CU_PrezenteDetaliu SET CUPD_FVFA_Id = @fvfa WHERE CUPD_Id = @id",
                        m_manager.GetConnection());
                    cmdUpd.Parameters.AddWithValue("@fvfa", fvfaId);
                    cmdUpd.Parameters.AddWithValue("@id",   sed.cupdId);
                    cmdUpd.ExecuteNonQuery();
                }

                IncarcaSedinteIndividuale();
                IncarcaContracte();
                MessageBox.Show($"Facturat: {sedinte.Count} ședință(e) → factura {nrFact}.", "Succes",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { CEroare.Arata("Eroare la salvare", ex.Message); }
        }

        private void MarcheazaFacurataSedintas(int cupdId, string cursant, string dataStr, decimal pretDefault)
        {
            // Build a simple dialog inline
            using var dlg = new Form
            {
                Text = "Marchează ședință facturată",
                Size = new Size(400, 230),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false, MinimizeBox = false
            };

            var lbl = new Label { Text = $"Ședință: {cursant}  ({dataStr})", Left = 12, Top = 12, Width = 370, AutoSize = false };
            var lblNr = new Label { Text = "Nr. factură SmartBill:", Left = 12, Top = 44, AutoSize = true };
            var tbNr  = new TextBox { Left = 12, Top = 64, Width = 360 };
            var lblDt = new Label { Text = "Data facturii:", Left = 12, Top = 96, AutoSize = true };
            var dtp   = new DateTimePicker { Left = 12, Top = 116, Width = 180,
                Format = DateTimePickerFormat.Short, Value = DateTime.Today };
            var lblSuma = new Label { Text = "Sumă (lei):", Left = 210, Top = 96, AutoSize = true };
            var nudSuma = new NumericUpDown { Left = 210, Top = 116, Width = 160,
                Minimum = 0, Maximum = 99999, DecimalPlaces = 2, Value = pretDefault };
            var btnOk  = new Button { Text = "Salvează", Left = 190, Top = 160, Width = 90,
                DialogResult = DialogResult.OK };
            var btnAnn = new Button { Text = "Anulează", Left = 290, Top = 160, Width = 90,
                DialogResult = DialogResult.Cancel };

            dlg.Controls.AddRange(new Control[] { lbl, lblNr, tbNr, lblDt, dtp, lblSuma, nudSuma, btnOk, btnAnn });
            dlg.AcceptButton = btnOk;
            dlg.CancelButton = btnAnn;

            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            string nrFact = tbNr.Text.Trim();
            if (string.IsNullOrEmpty(nrFact))
            {
                MessageBox.Show("Introduceți numărul facturii.", "Câmp obligatoriu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var luna   = new DateTime(dtp.Value.Year, dtp.Value.Month, 1);
                var lunaSf = luna.AddMonths(1).AddDays(-1);
                decimal suma = nudSuma.Value;

                using var cmdIns = new SqlCommand(@"
                    INSERT INTO FV_Facturi
                        (FVFA_CUCO_Id, FVFA_NrFactura, FVFA_DataEmitere,
                         FVFA_PerioadaInceput, FVFA_PerioadaSfarsit, FVFA_Suma,
                         FVFA_MBUT_Id, FVFA_DataSalvarii)
                    OUTPUT INSERTED.FVFA_Id
                    VALUES (@cuco,@nrf,@de,@pi,@ps,@suma,@u,GETDATE())",
                    m_manager.GetConnection());
                cmdIns.Parameters.AddWithValue("@cuco", m_cucoId);
                cmdIns.Parameters.AddWithValue("@nrf",  nrFact);
                cmdIns.Parameters.AddWithValue("@de",   dtp.Value);
                cmdIns.Parameters.AddWithValue("@pi",   luna);
                cmdIns.Parameters.AddWithValue("@ps",   lunaSf);
                cmdIns.Parameters.AddWithValue("@suma", suma);
                cmdIns.Parameters.AddWithValue("@u",    m_manager.GetUserId());
                int fvfaId = (int)cmdIns.ExecuteScalar();

                using var cmdUpd = new SqlCommand(
                    "UPDATE CU_PrezenteDetaliu SET CUPD_FVFA_Id = @fvfa WHERE CUPD_Id = @id",
                    m_manager.GetConnection());
                cmdUpd.Parameters.AddWithValue("@fvfa", fvfaId);
                cmdUpd.Parameters.AddWithValue("@id",   cupdId);
                cmdUpd.ExecuteNonQuery();

                IncarcaSedinteIndividuale();
                IncarcaContracte();
            }
            catch (Exception ex) { CEroare.Arata("Eroare la salvare", ex.Message); }
        }
    }
}

using GestiunePrezente.Common;
using GestiunePrezente.MeniuAdministrare;
using GestiunePrezente.MeniuConfigurare;
using GestiunePrezente.MeniuCursanti;
using GestiunePrezente.MeniuSituatii;
using GestiunePrezente.SmartBill;
using System.Diagnostics;

namespace GestiunePrezente.MeniuPrincipal
{
    public partial class FrmMeniuPrincipal : Form
    {
        private record Dictionar(int Valoare, string Nume, int Id);
        private readonly List<Dictionar> dictionarMeniu = [];
        private readonly List<Dictionar> dictionarConfigurare = [];

        CManagerDeProiect? ManagerDeProiect;
        internal static Form? MainForm;


        private readonly object _lock = new();

        Point _imageLocation = new(15, 5);
        Point _imgHitArea = new(13, 2);

        private Thread? t;

        private Image? closeIcon;
        private Font? boldFont;
        private Brush? selectedTextBrush;
        private Panel? activeSubMenu;
        readonly string m_sTipDocument = "";
        readonly string m_sDocumentId = "";
        private volatile bool _stopThread = false;
        private readonly int sidebarExpandedWidth;

        private Button? selectedButton;

        private float m_menuFontSize = 11.5f;

        private Color SidebarBack        = Color.White;
        private Color MenuButtonBack     = Color.White;
        private Color MenuButtonHover    = Color.FromArgb(240, 245, 255);
        private Color MenuButtonSelected = Color.FromArgb(235, 242, 255);
        private Color MenuText           = Color.FromArgb(55, 65, 81);
        private Color MenuTextSelected   = Color.FromArgb(29, 78, 216);
        private Color SubMenuBack        = Color.White;
        private Color SubMenuHover       = Color.FromArgb(240, 245, 255);
        private Color AccentColor        = Color.FromArgb(37, 99, 235);
        private Color SeparatorColor     = Color.FromArgb(229, 231, 235);

        private static Color Blend(Color c, Color bg, float opacity) => Color.FromArgb(
            (int)(c.R * opacity + bg.R * (1 - opacity)),
            (int)(c.G * opacity + bg.G * (1 - opacity)),
            (int)(c.B * opacity + bg.B * (1 - opacity)));

        private void UpdateDerivedColors()
        {
            MenuButtonHover    = Blend(AccentColor, Color.White, 0.10f);
            MenuButtonSelected = Blend(AccentColor, Color.White, 0.16f);
            MenuTextSelected   = AccentColor;
            SubMenuHover       = Blend(AccentColor, Color.White, 0.10f);
        }

        private string? activeSubMenuPrefix;

        public FrmMeniuPrincipal(string[] args)
        {
            MainForm = this;
            ManagerDeProiect = null;
            InitializeComponent();

            if (!Conexiune(args))
            {
                this.Close();
                return;
            }

            IncarcaPreferinteUtilizator();
            ApplySidebarStyle();

            if (args.Length == 6)
            {
                //sSoc = args[0];
                //sDiv = args[1];
                //sUser = args[2];
                //sPass = args[3];
                m_sTipDocument = args[4];
                m_sDocumentId = args[5];
            }
            //AFISEZ DIVIZIUNEA DE LUCRU SI UTLIZATORUL

            string sDB = ManagerDeProiect!.GetDatabase();

            if (sDB.Contains("test") || sDB.Contains("Test"))
                this.Text = "TEST----------------------------------" + ManagerDeProiect.GetSocietateNume() + "  -  Diviziunea:  " + ManagerDeProiect.GetDiviziuneNume() + "  -  Utilizator:  " + ManagerDeProiect.GetUserNumePrenume() + "  -  Server:  " + ManagerDeProiect.GetServer();
            else
                this.Text = ManagerDeProiect.GetSocietateNume() + "  -  Diviziunea:  " + ManagerDeProiect.GetDiviziuneNume() + "  -  Utilizator:  " + ManagerDeProiect.GetUserNumePrenume() + "  -  Server:  " + ManagerDeProiect.GetServer();
            string sIconName = "IcoanaDefault.ico";
            if (ManagerDeProiect.GetSocietate() == 1)
                sIconName = "IcoanaGestiuneCursuri.ico";

            string sPath = Application.ExecutablePath;
            string sPathLower = sPath.ToLower();
            int index = sPathLower.IndexOf("GestiuneCursuri.exe");
            if (index < 0)
                index = 0;
            sPath = sPath[..index];
            string SpathClose = sPath + "Icoane\\Close_blue.png";
            sPath += "Icoane\\" + sIconName;
            string exeFolder = Path.GetDirectoryName(Application.ExecutablePath)!;
            Icon icon = Icon.ExtractAssociatedIcon(sPath)!;
            string sIconPath = Path.Combine(exeFolder, "Icoane", sIconName);
            if (File.Exists(sIconPath))
            {
                this.Icon = new Icon(sIconPath);
            }
            closeIcon = new Bitmap(SpathClose);

            sidebarExpandedWidth = Sidebar.Width;

            InsereazaMeniuCursuri(0);
            InsereazaMeniuConfigurare(0);

            if (ManagerDeProiect.GetRolUser_Administrator() == 1)
            {
                var dashboard = new FrmDashboard(this, ManagerDeProiect!);
                dashboard.MdiParent = this;
                dashboard.WindowState = FormWindowState.Maximized;
                dashboard.Show();
            }

            RetrageMeniu.Click += async (_, _) => await ToggleSidebarAsync();
            btnMeniu.Click += async (_, _) =>
            {
                SetSelectedButton(btnMeniu, false);
                await ToggleSubMenuAsync("Meniu", 4, MenuButtonBack);
            };


            if (ManagerDeProiect.GetRolUser_Administrator() == 1)
            {
                btnSetari.Click += async (_, _) =>
                {
                    SetSelectedButton(btnSetari, false);
                    await ToggleSubMenuAsync("Setări", 1, MenuButtonBack);
                };
            }
            else
            {
                btnSetari.Visible = false;
            }


            Refresh();

            m_tabPP.DrawMode = TabDrawMode.OwnerDrawFixed;
            m_tabPP.DrawItem += new DrawItemEventHandler(m_tabPP_DrawItem);
        }

        public CManagerDeProiect GetManagerDeProiect() => ManagerDeProiect!;

        private bool Conexiune(string[] args)
        {
            //CONEXIUNE BAZA DE DATE
            FrmLogin frm = new(args);
            frm.ShowDialog();
            if (frm.GetLogin())
            {
                ManagerDeProiect = frm.GetManagerDeProiect();
                return true;
            }

            return false;
        }

        private Button CreateMenuButton(string text)
        {
            var btn = new Button
            {
                Text      = text,
                Dock      = DockStyle.Top,
                Height    = 44,
                FlatStyle = FlatStyle.Flat,
                BackColor = MenuButtonBack,
                ForeColor = MenuText,
                Font      = new Font("Segoe UI Semibold", m_menuFontSize, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding   = new Padding(22, 0, 0, 0),
                Cursor    = Cursors.Hand,
                UseVisualStyleBackColor = false
            };

            btn.FlatAppearance.BorderSize         = 0;
            btn.FlatAppearance.MouseOverBackColor = MenuButtonHover;
            btn.FlatAppearance.MouseDownBackColor = MenuButtonSelected;

            btn.MouseEnter += (_, _) =>
            {
                if (btn != selectedButton) btn.BackColor = MenuButtonHover;
            };
            btn.MouseLeave += (_, _) =>
            {
                if (btn != selectedButton) btn.BackColor = MenuButtonBack;
            };

            btn.Paint += (s, pe) =>
            {
                if (s is Button b && b == selectedButton)
                {
                    int barH = 22;
                    int barY = (b.Height - barH) / 2;
                    using var brush = new SolidBrush(AccentColor);
                    pe.Graphics.FillRectangle(brush, 0, barY, 3, barH);
                }
            };

            return btn;
        }

        private Button CreateSubMenuButton(string text)
        {
            var btn = new Button
            {
                Text      = "·  " + text,
                Dock      = DockStyle.Top,
                Height    = 38,
                FlatStyle = FlatStyle.Flat,
                BackColor = SubMenuBack,
                ForeColor = Color.FromArgb(75, 85, 99),
                Font      = new Font("Segoe UI", Math.Max(8f, m_menuFontSize - 0.5f), FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding   = new Padding(36, 0, 0, 0),
                Cursor    = Cursors.Hand,
                UseVisualStyleBackColor = false
            };

            btn.FlatAppearance.BorderSize         = 0;
            btn.FlatAppearance.MouseOverBackColor = SubMenuHover;
            btn.FlatAppearance.MouseDownBackColor = MenuButtonSelected;

            btn.MouseEnter += (_, _) =>
            {
                if (btn != selectedButton) btn.BackColor = SubMenuHover;
            };
            btn.MouseLeave += (_, _) =>
            {
                if (btn != selectedButton) btn.BackColor = SubMenuBack;
            };

            btn.Paint += (s, pe) =>
            {
                if (s is Button b && b == selectedButton)
                {
                    int barH = 16;
                    int barY = (b.Height - barH) / 2;
                    using var brush = new SolidBrush(AccentColor);
                    pe.Graphics.FillRectangle(brush, 0, barY, 3, barH);
                }
            };

            return btn;
        }

        private void SetSelectedButton(Button btn, bool isSubmenu)
        {
            if (selectedButton != null)
            {
                bool prevSub = selectedButton.AccessibleName == "submenu";
                selectedButton.BackColor = prevSub ? SubMenuBack : MenuButtonBack;
                selectedButton.ForeColor = prevSub ? Color.FromArgb(75, 85, 99) : MenuText;
                selectedButton.Font      = prevSub
                    ? new Font("Segoe UI", 11F, FontStyle.Regular)
                    : new Font("Segoe UI Semibold", 11.5F, FontStyle.Regular);
                selectedButton.Invalidate();
            }

            btn.BackColor = MenuButtonSelected;
            btn.ForeColor = MenuTextSelected;
            btn.Font      = isSubmenu
                ? new Font("Segoe UI", 11F, FontStyle.Bold)
                : new Font("Segoe UI Semibold", 11.5F, FontStyle.Bold);

            selectedButton = btn;
            btn.Invalidate();
        }

        private void ApplySidebarStyle()
        {
            Sidebar.BackColor   = SidebarBack;
            panelTop.BackColor  = SidebarBack;
            panelTop.Padding    = new Padding(0, 4, 0, 0);

            // ── Logo panel ──────────────────────────────────────────
            string exeDir    = Path.GetDirectoryName(Application.ExecutablePath)!;
            string logoPath  = Path.Combine(exeDir, "Icoane", "logo.jpg");
            var logoPanel    = new Panel
            {
                Dock      = DockStyle.Top,
                Height    = 88,
                BackColor = Color.White,
                Padding   = new Padding(20, 10, 20, 8)
            };
            if (File.Exists(logoPath))
            {
                var pb = new PictureBox
                {
                    Image    = Image.FromFile(logoPath),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Dock     = DockStyle.Fill,
                    BackColor = Color.White
                };
                logoPanel.Controls.Add(pb);
            }
            else
            {
                var lbl = new Label
                {
                    Text      = "ONIXTEIA",
                    Dock      = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font      = new Font("Segoe UI", 14F, FontStyle.Bold),
                    ForeColor = AccentColor,
                    BackColor = Color.White
                };
                logoPanel.Controls.Add(lbl);
            }

            // ── Separator sub logo ───────────────────────────────────
            var logoSep = new Panel
            {
                Dock      = DockStyle.Top,
                Height    = 1,
                BackColor = SeparatorColor
            };

            // ── User info strip ──────────────────────────────────────
            var userStrip = new Panel
            {
                Dock      = DockStyle.Bottom,
                Height    = 48,
                BackColor = Color.FromArgb(248, 250, 252),
                Padding   = new Padding(16, 0, 8, 0)
            };
            var userSep = new Panel
            {
                Dock      = DockStyle.Top,
                Height    = 1,
                BackColor = SeparatorColor
            };
            string userName = ManagerDeProiect?.GetUserNumePrenume() ?? "";
            var lblUser = new Label
            {
                Text      = userName,
                Dock      = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font      = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = Color.FromArgb(107, 114, 128),
                BackColor = Color.Transparent,
                AutoEllipsis = true
            };
            var btnPersonalizare = new Button
            {
                Text      = "🎨",
                Dock      = DockStyle.Right,
                Width     = 36,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Color.FromArgb(107, 114, 128),
                Font      = new Font("Segoe UI", 13F),
                Cursor    = Cursors.Hand,
                UseVisualStyleBackColor = false,
                TabStop   = false
            };
            btnPersonalizare.FlatAppearance.BorderSize        = 0;
            btnPersonalizare.FlatAppearance.MouseOverBackColor = MenuButtonHover;
            btnPersonalizare.Click += (_, _) =>
            {
                using var dlg = new FrmPersonalizareMeniu(this, AccentColor, m_menuFontSize);
                dlg.ShowDialog(this);
            };

            userStrip.Controls.Add(lblUser);
            userStrip.Controls.Add(btnPersonalizare);
            userStrip.Controls.Add(userSep);

            Sidebar.Controls.Add(logoSep);
            Sidebar.Controls.Add(logoPanel);
            Sidebar.Controls.Add(userStrip);

            // pune logo deasupra panelTop
            Sidebar.Controls.SetChildIndex(logoPanel, Sidebar.Controls.Count - 1);
            Sidebar.Controls.SetChildIndex(logoSep,   Sidebar.Controls.Count - 1);

            // ── Butoane footer ───────────────────────────────────────
            StyleTopButton(btnMeniu,  "☰  Meniu");
            StyleTopButton(btnSetari, "⚙  Setări");

            RetrageMeniu.Cursor   = Cursors.Hand;
            RetrageMeniu.BackColor = SidebarBack;

            // Separator deasupra butoanelor footer
            var footerSep = new Panel
            {
                Height    = 1,
                Dock      = DockStyle.Top,
                BackColor = SeparatorColor
            };
            Sidebar.Controls.Add(footerSep);
            Sidebar.Controls.SetChildIndex(footerSep, 1);
        }

        private void StyleTopButton(Button btn, string text)
        {
            btn.Text      = text;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize          = 0;
            btn.FlatAppearance.MouseOverBackColor  = MenuButtonHover;
            btn.FlatAppearance.MouseDownBackColor  = MenuButtonSelected;
            btn.BackColor = Color.FromArgb(248, 250, 252);
            btn.ForeColor = Color.FromArgb(107, 114, 128);
            btn.Font      = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding   = new Padding(20, 0, 0, 0);
            btn.Cursor    = Cursors.Hand;
            btn.UseVisualStyleBackColor = false;
        }

        private void IncarcaPreferinteUtilizator()
        {
            if (ManagerDeProiect == null) return;
            try
            {
                var conn = ManagerDeProiect.GetConnection();

                // Verifică dacă coloana există înainte să o citim — evităm FrmErr la prima rulare
                using var cmdCheck = new Microsoft.Data.SqlClient.SqlCommand(
                    "SELECT COUNT(*) FROM sys.columns WHERE object_id = OBJECT_ID('MB_Utilizatori') AND name = 'MBUT_MenuAccentColor'",
                    conn);
                int colExists = (int)(cmdCheck.ExecuteScalar() ?? 0);
                if (colExists == 0) return;

                using var cmd = new Microsoft.Data.SqlClient.SqlCommand(
                    "SELECT MBUT_MenuAccentColor, MBUT_MenuFontSize FROM MB_Utilizatori WHERE MBUT_Id = @uid",
                    conn);
                cmd.Parameters.AddWithValue("@uid", ManagerDeProiect.GetUserId());
                using var rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    string? hex = rd.IsDBNull(0) ? null : rd.GetString(0);
                    if (!string.IsNullOrWhiteSpace(hex))
                    {
                        try { AccentColor = ColorTranslator.FromHtml(hex); UpdateDerivedColors(); } catch { }
                    }
                    if (!rd.IsDBNull(1))
                    {
                        decimal fontSize = rd.GetDecimal(1);
                        if (fontSize >= 8 && fontSize <= 20)
                            m_menuFontSize = (float)fontSize;
                    }
                }
            }
            catch { /* non-critical — keep defaults */ }
        }

        internal void SalveazaPreferinteUtilizator(Color accent, float fontSize)
        {
            if (ManagerDeProiect == null) return;
            try
            {
                new CInterogareSQL(ManagerDeProiect.GetConnection()).ExecuteNonQueryP(
                    @"UPDATE MB_Utilizatori
                      SET MBUT_MenuAccentColor = @c, MBUT_MenuFontSize = @f
                      WHERE MBUT_Id = @uid",
                    CInterogareSQL.P("@c",   ColorTranslator.ToHtml(accent)),
                    CInterogareSQL.P("@f",   (decimal)fontSize),
                    CInterogareSQL.P("@uid", ManagerDeProiect.GetUserId()));

                AccentColor    = accent;
                m_menuFontSize = fontSize;
                UpdateDerivedColors();

                // Închide submeniu activ — la redeschidere butoanele se recreează cu noile culori
                if (activeSubMenu != null)
                {
                    panelTop.Controls.Remove(activeSubMenu);
                    activeSubMenu.Dispose();
                    activeSubMenu = null;
                    activeSubMenuPrefix = null;
                }

                // Actualizează butonul selectat curent
                if (selectedButton != null)
                {
                    selectedButton.BackColor = MenuButtonSelected;
                    selectedButton.ForeColor = MenuTextSelected;
                    selectedButton.Invalidate();
                }

                Sidebar.Invalidate(true);
            }
            catch { }
        }

        private static double EaseInOutCubic(double t) => t < 0.5 ? 4 * t * t * t : 1 - Math.Pow(-2 * t + 2, 3) / 2;

        private static async Task AnimateHeightAsync(Control control, int from, int to, int durationMs)
        {
            var sw = Stopwatch.StartNew();
            while (true)
            {
                double t = Math.Min(1.0, sw.ElapsedMilliseconds / (double)durationMs);
                double eased = EaseInOutCubic(t);
                control.Height = (int)(from + (to - from) * eased);
                if (t >= 1.0) break;
                await Task.Delay(15);
            }
            control.Height = to;
        }

        private async Task ToggleSidebarAsync()
        {
            int compactWidth = 64;
            bool isExpanded = Sidebar.Width > compactWidth;

            if (isExpanded)
            {
                btnMeniu.Text = "";
                btnSetari.Text = "";

                btnMeniu.Padding = new Padding(0);
                btnSetari.Padding = new Padding(0);

                btnMeniu.TextAlign = ContentAlignment.MiddleCenter;
                btnSetari.TextAlign = ContentAlignment.MiddleCenter;

                await AnimateWidthAsync(Sidebar, sidebarExpandedWidth, compactWidth, 200);
            }
            else
            {
                await AnimateWidthAsync(Sidebar, compactWidth, sidebarExpandedWidth, 200);

                btnMeniu.Text = "Meniu";
                btnSetari.Text = "Setări";

                btnMeniu.Padding = new Padding(18, 0, 0, 0);
                btnSetari.Padding = new Padding(18, 0, 0, 0);

                btnMeniu.TextAlign = ContentAlignment.MiddleLeft;
                btnSetari.TextAlign = ContentAlignment.MiddleLeft;
            }
        }

        private static async Task AnimateWidthAsync(Control c, int from, int to, int duration)
        {
            var sw = Stopwatch.StartNew();
            while (true)
            {
                double t = Math.Min(1.0, sw.ElapsedMilliseconds / (double)duration);
                double eased = EaseInOutCubic(t);
                c.Width = (int)(from + (to - from) * eased);
                if (t >= 1.0) break;
                await Task.Delay(15);
            }
            c.Width = to;
        }

        private async Task ToggleSubMenuAsync(string prefix, int count, Color color)
        {
            // Dacă este apăsat același meniu deja deschis -> îl închidem
            if (activeSubMenu != null && activeSubMenuPrefix == prefix)
            {
                await AnimateHeightAsync(activeSubMenu, activeSubMenu.Height, 0, 180);
                panelTop.Controls.Remove(activeSubMenu);
                activeSubMenu.Dispose();
                activeSubMenu = null;
                activeSubMenuPrefix = null;

                if (selectedButton == btnMeniu || selectedButton == btnSetari)
                {
                    selectedButton.BackColor = MenuButtonBack;
                    selectedButton.ForeColor = MenuText;
                    selectedButton.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Regular);
                    selectedButton = null;
                }

                return;
            }

            // Dacă e deschis alt submenu, îl închidem
            if (activeSubMenu != null)
            {
                await AnimateHeightAsync(activeSubMenu, activeSubMenu.Height, 0, 180);
                panelTop.Controls.Remove(activeSubMenu);
                activeSubMenu.Dispose();
                activeSubMenu = null;
                activeSubMenuPrefix = null;
            }

            var submenu = new Panel
            {
                Dock      = DockStyle.Top,
                Height    = 0,
                BackColor = SidebarBack,
                Padding   = new Padding(0, 2, 0, 4)
            };

            int buttonHeight = 46;
            List<Dictionar> dictionar = prefix == "Meniu" ? dictionarMeniu : dictionarConfigurare;
            var radacini = dictionar.Where(x => x.Id == 0).OrderBy(x => x.Valoare).ToList();

            foreach (var radacina in radacini)
            {
                var btn = CreateMenuButton(radacina.Nume);
                btn.Tag = false;

                btn.Click += async (s, _) =>
                {
                    if (s is Button btnSender)
                    {
                        SetSelectedButton(btnSender, false);
                        await HandleSubButtonClickAsync(radacina.Valoare, prefix, submenu, btnSender);
                    }
                };

                submenu.Controls.Add(btn);
                submenu.Controls.SetChildIndex(btn, 0);
            }

            panelTop.Controls.Add(submenu);
            panelTop.Controls.SetChildIndex(submenu, 0);
            panelTop.Refresh();

            activeSubMenu = submenu;
            activeSubMenuPrefix = prefix;

            int totalHeight = radacini.Count * buttonHeight + 8;
            await AnimateHeightAsync(submenu, 0, totalHeight, 220);
        }

        private async Task HandleSubButtonClickAsync(int id, string prefix, Panel parentPanel, Button clickedButton)
        {
            int subButtonHeight = 40;
            string parentKey = clickedButton.Text;

            var allGeneratedButtons = parentPanel.Controls
                .OfType<Button>()
                .Where(b => b.AccessibleName == "submenu")
                .ToList();

            foreach (var btn in allGeneratedButtons)
            {
                parentPanel.Controls.Remove(btn);
                btn.Dispose();
            }

            foreach (var parentBtn in parentPanel.Controls.OfType<Button>())
            {
                if (parentBtn.AccessibleName != "submenu")
                    parentBtn.Tag = false;
            }

            if (clickedButton.Tag is bool alreadyExpanded && alreadyExpanded)
            {
                clickedButton.Tag = false;

                int baseHeight = parentPanel.Controls
                    .OfType<Button>()
                    .Count(b => b.AccessibleName != "submenu") * 46 + 8;

                await AnimateHeightAsync(parentPanel, parentPanel.Height, baseHeight, 180);
                return;
            }

            clickedButton.Tag = true;
            int clickedIndex = parentPanel.Controls.GetChildIndex(clickedButton);

            List<Dictionar> dictionar = prefix == "Meniu" ? dictionarMeniu : dictionarConfigurare;
            var subItems = dictionar.Where(x => x.Id == id).OrderBy(x => x.Valoare).ToList();

            // Dacă nu există submeniuri, deschide direct formularul
            if (subItems.Count == 0)
            {
                string? numeItem = dictionar.FirstOrDefault(x => x.Valoare == id)?.Nume;
                Form? frmDirect = CreeazaForm(id, prefix, numeItem);
                if (frmDirect != null)
                {
                    frmDirect.MdiParent = this;
                    frmDirect.Show();
                }
                clickedButton.Tag = false;
                return;
            }

            foreach (var radacina in subItems)
            {
                var sub = CreateSubMenuButton(radacina.Nume);
                sub.AccessibleName = "submenu";

                sub.Click += (_, _) =>
                {
                    SetSelectedButton(sub, true);

                    Form? frm = CreeazaForm(radacina.Valoare, prefix, radacina.Nume);
                    if (frm != null)
                    {
                        frm.MdiParent = this;
                        frm.Show();
                    }
                };

                parentPanel.Controls.Add(sub);
                parentPanel.Controls.SetChildIndex(sub, clickedIndex);
            }

            int mainButtonsHeight = parentPanel.Controls
                .OfType<Button>()
                .Count(b => b.AccessibleName != "submenu") * 46 + 8;

            int totalHeight = mainButtonsHeight + (subItems.Count * subButtonHeight);
            await AnimateHeightAsync(parentPanel, parentPanel.Height, totalHeight, 220);
        }
   
        private void InsereazaMeniuCursuri(int MBME)
        {
            string sSql = string.Format(@"SELECT DISTINCT MBME_Id, MBME_Nume, MBME_OrdineAfisare
                                  FROM MB_Meniu
                                  INNER JOIN MB_DrepturiMeniu ON MBDM_MBME_Id=MBME_Id
                                  WHERE MBME_Aria = '1CU' AND MBME_MBME_Id={0} 
                                    AND MBDM_MBUT_Id={1} AND MBDM_DreptFolosinta=1 
                                    AND MBME_Ascuns=0 AND MBDM_MBDV_Id={2}
                                  ORDER BY MBME_OrdineAfisare",
                                        MBME, ManagerDeProiect!.GetUserId(), ManagerDeProiect.GetDiviziune());

            // Citim TOATE rândurile întâi
            var copii = new List<(int id, string nume)>();

            CInterogareSQL q = new(ManagerDeProiect.GetConnection());
            q.Open(sSql);
            while (q.NextRow())
            {
                copii.Add((q.GetIntValue(0), q.GetStringValue(1)!));
            }
            q.Close();   // ← reader-ul e închis ÎNAINTE de recursivitate

            // Acum putem recursiva în siguranță
            foreach (var (id, nume) in copii)
            {
                dictionarMeniu.Add(new Dictionar(id, nume, MBME));
                InsereazaMeniuCursuri(id);   // ✅ acum nu mai e niciun reader deschis
            }
        }


        private void InsereazaMeniuConfigurare(int MBME)
        {
            string sSql = string.Format(@"SELECT DISTINCT MBME_Id, MBME_Nume, MBME_OrdineAfisare
                                          FROM MB_Meniu
                                          LEFT JOIN MB_DrepturiMeniu ON MBDM_MBME_Id=MBME_Id
                                          WHERE MBME_Aria = '0CG' AND MBME_MBME_Id={0} 
                                            AND MBME_Ascuns=0 
                                          ORDER BY MBME_OrdineAfisare", MBME);

            // Citim TOATE rândurile întâi
            var copii = new List<(int id, string nume)>();

            CInterogareSQL q = new(ManagerDeProiect!.GetConnection());
            q.Open(sSql);
            while (q.NextRow())
            {
                copii.Add((q.GetIntValue(0), q.GetStringValue(1)!));
            }
            q.Close();   // ← reader-ul e închis ÎNAINTE de recursivitate

            // Acum putem recursiva în siguranță
            foreach (var (id, nume) in copii)
            {
                dictionarConfigurare.Add(new Dictionar(id, nume, MBME));
                InsereazaMeniuConfigurare(id);   // ✅ niciun reader deschis
            }
        }

        private Form? CreeazaForm(int id, string tipMeniu, string? nome = null)
        {
            // For forms with unique menu names, match by name (database-independent).
            // For forms whose name is ambiguous (e.g. "Cautare" appears many times), keep ID-based match.
            if (nome != null)
            {
                Form? byName = (tipMeniu, nome) switch
                {
                    ("Meniu", "Turnee")              => new FrmCautareTurnee(this, ManagerDeProiect!),
                    ("Meniu", "Jucători")            => new FrmListaJucatori(this, ManagerDeProiect!),
                    ("Meniu", "Facturi")             => new FrmCautareFacturi(this, ManagerDeProiect!),
                    ("Meniu", "Situatie prezente")   => new FrmSituatiePrezente(this, ManagerDeProiect!),
                    ("Meniu", "Situatie centralizata") or ("Meniu", "Turnee cursanti")
                                                     => new FrmSituatieCentralizata(this, ManagerDeProiect!),
                    ("Meniu", "Situatie financiara") => new FrmSituatieFinanciara(this, ManagerDeProiect!),
                    ("Setări", "SmartBill")          => new MeniuConfigurare.FrmSetariGenerale(this, ManagerDeProiect!),
                    ("Meniu", "Import SmartBill")   => new FrmImportSmartBill(this, ManagerDeProiect!),
                    _ => null
                };
                if (byName != null) return byName;
            }

            return tipMeniu switch
            {
                "Meniu" => id switch
                {
                    1 => new FrmCautareFormatori(this, ManagerDeProiect!),
                    2 => new FrmCautareFormatori(this, ManagerDeProiect!),
                    3 => new FrmCautareCursanti(this, ManagerDeProiect!),
                    4 => new FrmCautareCursanti(this, ManagerDeProiect!),
                    5 => new FrmCautareContracte(this, ManagerDeProiect!),
                    6 => new FrmCautareContracte(this, ManagerDeProiect!),
                    9 => new FrmCautarePrezente(this, ManagerDeProiect!),
                    10 => new FrmCautareAbonamente(this, ManagerDeProiect!),
                    12 => new FrmCautareIncasariAbonamente(this, ManagerDeProiect!),
                    17 => new FrmCautareRegistruNumereInterne(this, ManagerDeProiect!),
                    1017 => new FrmListaJucatori(this, ManagerDeProiect!),
                    18 => new FrmCautareParteneri(this, ManagerDeProiect!),
                    19 => new FrmCautareSolicitari(this, ManagerDeProiect!),
                    _ => null
                },

                "Setări" => id switch
                {
                    14 => new FrmCautareSocietate(this, ManagerDeProiect!),
                    15 => new FrmCautareDiviziuni(this, ManagerDeProiect!),
                    20 => new FrmGestiuneDrepturiMeniu(this, ManagerDeProiect!),
                    _ => null
                },
                _ => null
            };
        }

        private void InchidereAplicatie()
        {
            string sSQL;
            CInterogareSQL q = new(ManagerDeProiect!.GetConnection());
            sSQL = string.Format(@"UPDATE MB_UtilizatoriIstoricConectare SET MBUT_DataDeconectarii=GetDate() WHERE MBUT_Id = (SELECT MAX(MBUT_Id) FROM MB_UtilizatoriIstoricConectare WHERE MBUT_MBDV_Id={0} AND MBUT_MBUT_Conectat={1})",
                                    ManagerDeProiect.GetDiviziune(), ManagerDeProiect.GetUserId());
            q.ExecuteStatement(sSQL);
            q.Close();
        }

        private async void FrmMeniuPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                InchidereAplicatie();

                if (t != null)
                {
                    _stopThread = true;
                    bool stopped = await Task.Run(() => t.Join(2000));

                    if (!stopped)
                    {
                        Debug.WriteLine("[WARN] Thread-ul nu s-a oprit la timp.");
                    }

                    t = null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Eroare la închiderea thread-ului: {ex}");
            }
        }
        private void FrmMeniuPrincipal_MdiChildActivate(object? sender, EventArgs e)
        {
            Form? activeChild = ActiveMdiChild;

            m_tabPP.Visible = activeChild is not null;
            if (activeChild is null) return;

            if (activeChild.WindowState != FormWindowState.Maximized)
                activeChild.WindowState = FormWindowState.Maximized;

            if (activeChild.Tag is not TabPage)
            {
                TabPage tp = new(activeChild.Text + "          ") { Tag = activeChild };
                m_tabPP.TabPages.Add(tp);
                m_tabPP.SelectedTab = tp;

                activeChild.Tag = tp;

                activeChild.FormClosed -= ActiveMdiChild_FormClosed;
                activeChild.FormClosed += ActiveMdiChild_FormClosed;
            }
            else if (activeChild.Tag is TabPage existingTab)
            {
                // Selectăm tabul existent dacă form-ul devine activ din nou
                m_tabPP.SelectedTab = existingTab;
            }
        }


        private void ActiveMdiChild_FormClosed(object? sender, FormClosedEventArgs e)
        {
            if (sender is Form closedForm && closedForm.Tag is TabPage tp)
            {
                if (m_tabPP.TabPages.Contains(tp))
                {
                    m_tabPP.TabPages.Remove(tp);
                    tp.Dispose();
                }
            }
            m_tabPP.Visible = m_tabPP.TabCount > 0;
        }


        private void m_tabPP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_tabPP.SelectedTab?.Tag is Form selectedForm)
            {
                selectedForm.Select();
            }
        }

        private void m_tabPP_MouseClick(object sender, MouseEventArgs e)
        {
            if (sender is not TabControl tabControl) return;
            if (tabControl.TabCount == 0) return;

            TabPage? tabToRemove = null;
            Form? formToClose = null;

            for (int i = 0; i < tabControl.TabCount; i++)
            {
                Rectangle tabRect = tabControl.GetTabRect(i);

                int closeButtonSize = 16;
                int padding = 6;
                Rectangle closeButtonRect = new(
                    tabRect.Right - closeButtonSize - padding,
                    tabRect.Top + (tabRect.Height - closeButtonSize) / 2,
                    closeButtonSize,
                    closeButtonSize
                );

                if (closeButtonRect.Contains(e.Location))
                {
                    tabToRemove = tabControl.TabPages[i];

                    if (tabToRemove.Tag is Form frm)
                        formToClose = frm;

                    break;
                }
            }

            formToClose?.Close();

            if (tabToRemove != null && tabControl.TabPages.Contains(tabToRemove))
            {
                tabControl.TabPages.Remove(tabToRemove);

                if (tabControl.TabCount > 0)
                {
                    //aici trebuie corectat sa raman ultimul tab
                    int newIndex = Math.Min(tabControl.SelectedIndex, tabControl.TabCount - 1);
                    tabControl.SelectedIndex = newIndex;
                }
            }
        }

        private void m_tabPP_DrawItem(object? sender, DrawItemEventArgs e)
        {
            if (sender is not TabControl tabControl || e.Index < 0 || e.Index >= tabControl.TabCount)
                return;

            TabPage tabPage = tabControl.TabPages[e.Index];
            Rectangle tabRect = tabControl.GetTabRect(e.Index);
            e.Graphics.FillRectangle(SystemBrushes.Control, tabRect);

            boldFont ??= new Font(tabControl.Font, FontStyle.Bold);
            selectedTextBrush ??= Brushes.Red;

            Font fontToUse = (e.Index == tabControl.SelectedIndex) ? boldFont : tabControl.Font;
            Brush textBrush = (e.Index == tabControl.SelectedIndex) ? selectedTextBrush : Brushes.Black;

            string text = tabPage.Text ?? "";

            SizeF textSize = e.Graphics.MeasureString(text, fontToUse);
            RectangleF tabRectF = new(tabRect.X + 3, tabRect.Y, Math.Max(tabRect.Width, textSize.Width + 10), tabRect.Height);
            using StringFormat sf = new() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near };
            e.Graphics.DrawString(text, fontToUse, textBrush, tabRectF, sf);

            int iconSize = 16;
            int padding = 3;
            Rectangle iconRect = new(
                tabRect.Right - iconSize - padding,
                tabRect.Top + (tabRect.Height - iconSize) / 2,
                iconSize,
                iconSize
            );
            e.Graphics.DrawImage(closeIcon!, iconRect);

            e.DrawFocusRectangle();
        }

    }
}

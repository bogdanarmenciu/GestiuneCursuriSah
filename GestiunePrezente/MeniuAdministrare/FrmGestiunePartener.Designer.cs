namespace GestiunePrezente.MeniuAdministrare
{
    partial class FrmGestiunePartener
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            m_toolStrip         = new ToolStrip();
            m_tsbSalveaza       = new ToolStripButton();
            // GroupBox Date firma
            m_gbDateFirma       = new GroupBox();
            m_lblDenumire       = new Label();
            m_ebDenumire        = new TextBox();
            m_lblCUI            = new Label();
            m_ebCUI             = new TextBox();
            m_lblRegCom         = new Label();
            m_ebRegCom1         = new TextBox();
            m_lblSep1           = new Label();
            m_ebRegCom2         = new TextBox();
            m_lblSep2           = new Label();
            m_ebRegCom3         = new TextBox();
            m_lblContBancar     = new Label();
            m_ebContBancar      = new TextBox();
            m_ckIsClient        = new CheckBox();
            m_ckIsFurnizor      = new CheckBox();
            m_ckIsSponsor       = new CheckBox();
            // GroupBox Adresa
            m_gbAdresa          = new GroupBox();
            m_lblStrada         = new Label();
            m_ebStrada          = new TextBox();
            m_lblNumar          = new Label();
            m_ebNumar           = new TextBox();
            m_lblBloc           = new Label();
            m_ebBloc            = new TextBox();
            m_lblScara          = new Label();
            m_ebScara           = new TextBox();
            m_lblEtaj           = new Label();
            m_ebEtaj            = new TextBox();
            m_lblApartament     = new Label();
            m_ebApartament      = new TextBox();
            m_lblCodPostal      = new Label();
            m_ebCodPostal       = new TextBox();
            m_lblLocalitate     = new Label();
            m_ebLocalitate      = new TextBox();
            m_lblJudet          = new Label();
            m_ebJudet           = new TextBox();
            m_lblSector         = new Label();
            m_ebSector          = new TextBox();
            // GroupBox Contact
            m_gbContact         = new GroupBox();
            m_lblTelefon        = new Label();
            m_ebTelefon         = new TextBox();
            m_lblTelefonMobil   = new Label();
            m_ebTelefonMobil    = new TextBox();
            m_lblEmail          = new Label();
            m_ebEmail           = new TextBox();
            m_lblPersoanaContact= new Label();
            m_ebPersoanaContact = new TextBox();

            m_toolStrip.SuspendLayout();
            m_gbDateFirma.SuspendLayout();
            m_gbAdresa.SuspendLayout();
            m_gbContact.SuspendLayout();
            SuspendLayout();

            // ---- ToolStrip ----
            m_toolStrip.Items.AddRange(new ToolStripItem[] { m_tsbSalveaza });
            m_toolStrip.Dock = DockStyle.Top;
            m_toolStrip.Name = "m_toolStrip";

            m_tsbSalveaza.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            m_tsbSalveaza.Image = Properties.Resources.save;
            m_tsbSalveaza.Name = "m_tsbSalveaza";
            m_tsbSalveaza.Text = "Salvează";
            m_tsbSalveaza.Enabled = false;
            m_tsbSalveaza.Click += m_tsbSalveaza_Click;

            // ---- GroupBox Date firmă ----
            m_gbDateFirma.Text = "Date firmă";
            m_gbDateFirma.Location = new Point(10, 32);
            m_gbDateFirma.Size = new Size(760, 150);
            m_gbDateFirma.Name = "m_gbDateFirma";

            // Row 1: Denumire + tip
            m_lblDenumire.AutoSize = true;
            m_lblDenumire.Location = new Point(10, 28);
            m_lblDenumire.Text = "Denumire:";

            m_ebDenumire.Location = new Point(100, 24);
            m_ebDenumire.Size = new Size(340, 27);
            m_ebDenumire.Name = "m_ebDenumire";
            m_ebDenumire.TextChanged += m_eb_Changed;

            m_ckIsClient.AutoSize = true;
            m_ckIsClient.Location = new Point(460, 26);
            m_ckIsClient.Name = "m_ckIsClient";
            m_ckIsClient.Text = "Client";
            m_ckIsClient.CheckedChanged += m_eb_Changed;

            m_ckIsFurnizor.AutoSize = true;
            m_ckIsFurnizor.Location = new Point(540, 26);
            m_ckIsFurnizor.Name = "m_ckIsFurnizor";
            m_ckIsFurnizor.Text = "Furnizor";
            m_ckIsFurnizor.CheckedChanged += m_eb_Changed;

            m_ckIsSponsor.AutoSize = true;
            m_ckIsSponsor.Location = new Point(630, 26);
            m_ckIsSponsor.Name = "m_ckIsSponsor";
            m_ckIsSponsor.Text = "Sponsor";
            m_ckIsSponsor.CheckedChanged += m_eb_Changed;

            // Row 2: CUI + RegCom
            m_lblCUI.AutoSize = true;
            m_lblCUI.Location = new Point(10, 65);
            m_lblCUI.Text = "CUI:";

            m_ebCUI.Location = new Point(100, 61);
            m_ebCUI.Size = new Size(120, 27);
            m_ebCUI.Name = "m_ebCUI";
            m_ebCUI.TextChanged += m_eb_Changed;

            m_lblRegCom.AutoSize = true;
            m_lblRegCom.Location = new Point(238, 65);
            m_lblRegCom.Text = "Nr. Reg. Com.:";

            m_ebRegCom1.Location = new Point(348, 61);
            m_ebRegCom1.Size = new Size(50, 27);
            m_ebRegCom1.Name = "m_ebRegCom1";
            m_ebRegCom1.PlaceholderText = "J40";
            m_ebRegCom1.TextChanged += m_eb_Changed;

            m_lblSep1.AutoSize = true;
            m_lblSep1.Location = new Point(402, 65);
            m_lblSep1.Text = "/";

            m_ebRegCom2.Location = new Point(415, 61);
            m_ebRegCom2.Size = new Size(80, 27);
            m_ebRegCom2.Name = "m_ebRegCom2";
            m_ebRegCom2.PlaceholderText = "12345";
            m_ebRegCom2.TextChanged += m_eb_Changed;

            m_lblSep2.AutoSize = true;
            m_lblSep2.Location = new Point(499, 65);
            m_lblSep2.Text = "/";

            m_ebRegCom3.Location = new Point(512, 61);
            m_ebRegCom3.Size = new Size(55, 27);
            m_ebRegCom3.Name = "m_ebRegCom3";
            m_ebRegCom3.PlaceholderText = "2025";
            m_ebRegCom3.TextChanged += m_eb_Changed;

            // Row 3: Cont bancar
            m_lblContBancar.AutoSize = true;
            m_lblContBancar.Location = new Point(10, 102);
            m_lblContBancar.Text = "Cont bancar:";

            m_ebContBancar.Location = new Point(100, 98);
            m_ebContBancar.Size = new Size(300, 27);
            m_ebContBancar.Name = "m_ebContBancar";
            m_ebContBancar.TextChanged += m_eb_Changed;

            m_gbDateFirma.Controls.AddRange(new Control[] {
                m_lblDenumire, m_ebDenumire, m_ckIsClient, m_ckIsFurnizor, m_ckIsSponsor,
                m_lblCUI, m_ebCUI, m_lblRegCom,
                m_ebRegCom1, m_lblSep1, m_ebRegCom2, m_lblSep2, m_ebRegCom3,
                m_lblContBancar, m_ebContBancar });

            // ---- GroupBox Adresă ----
            m_gbAdresa.Text = "Adresă";
            m_gbAdresa.Location = new Point(10, 192);
            m_gbAdresa.Size = new Size(375, 410);
            m_gbAdresa.Name = "m_gbAdresa";

            SetAdresaRow(m_lblStrada,       m_ebStrada,      "Stradă:",       0,  200);
            SetAdresaRow(m_lblNumar,         m_ebNumar,       "Nr.:",          1,  80);
            SetAdresaRow(m_lblBloc,          m_ebBloc,        "Bloc:",         2,  60);
            SetAdresaRow(m_lblScara,         m_ebScara,       "Scară:",        3,  50);
            SetAdresaRow(m_lblEtaj,          m_ebEtaj,        "Etaj:",         4,  50);
            SetAdresaRow(m_lblApartament,    m_ebApartament,  "Apartament:",   5,  60);
            SetAdresaRow(m_lblCodPostal,     m_ebCodPostal,   "Cod poștal:",   6,  90);
            SetAdresaRow(m_lblLocalitate,    m_ebLocalitate,  "Localitate:",   7,  200);
            SetAdresaRow(m_lblJudet,         m_ebJudet,       "Județ:",        8,  150);
            SetAdresaRow(m_lblSector,        m_ebSector,      "Sector:",       9,  50);

            m_gbAdresa.Controls.AddRange(new Control[] {
                m_lblStrada, m_ebStrada, m_lblNumar, m_ebNumar,
                m_lblBloc, m_ebBloc, m_lblScara, m_ebScara,
                m_lblEtaj, m_ebEtaj, m_lblApartament, m_ebApartament,
                m_lblCodPostal, m_ebCodPostal, m_lblLocalitate, m_ebLocalitate,
                m_lblJudet, m_ebJudet, m_lblSector, m_ebSector });

            // ---- GroupBox Contact ----
            m_gbContact.Text = "Contact";
            m_gbContact.Location = new Point(395, 192);
            m_gbContact.Size = new Size(375, 200);
            m_gbContact.Name = "m_gbContact";

            SetContactRow(m_lblTelefon,         m_ebTelefon,         "Telefon:",          0, 130);
            SetContactRow(m_lblTelefonMobil,     m_ebTelefonMobil,    "Telefon mobil:",    1, 130);
            SetContactRow(m_lblEmail,            m_ebEmail,           "Email:",            2, 220);
            SetContactRow(m_lblPersoanaContact,  m_ebPersoanaContact, "Persoană contact:", 3, 220);

            m_gbContact.Controls.AddRange(new Control[] {
                m_lblTelefon, m_ebTelefon, m_lblTelefonMobil, m_ebTelefonMobil,
                m_lblEmail, m_ebEmail, m_lblPersoanaContact, m_ebPersoanaContact });

            // ---- Form ----
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(780, 615);
            Controls.Add(m_gbContact);
            Controls.Add(m_gbAdresa);
            Controls.Add(m_gbDateFirma);
            Controls.Add(m_toolStrip);
            Name = "FrmGestiunePartener";
            Text = "Partener";
            Load += FrmGestiunePartener_Load;
            FormClosing += FrmGestiunePartener_FormClosing;

            m_toolStrip.ResumeLayout(false);
            m_toolStrip.PerformLayout();
            m_gbDateFirma.ResumeLayout(false);
            m_gbDateFirma.PerformLayout();
            m_gbAdresa.ResumeLayout(false);
            m_gbAdresa.PerformLayout();
            m_gbContact.ResumeLayout(false);
            m_gbContact.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private static void SetAdresaRow(Label lbl, TextBox tb, string text, int row, int width)
        {
            int y = 25 + row * 38;
            lbl.AutoSize = true;
            lbl.Location = new Point(10, y + 4);
            lbl.Text = text;
            tb.Location = new Point(115, y);
            tb.Size = new Size(width, 27);
            tb.Name = tb.Name;
        }

        private static void SetContactRow(Label lbl, TextBox tb, string text, int row, int width)
        {
            int y = 25 + row * 40;
            lbl.AutoSize = true;
            lbl.Location = new Point(10, y + 4);
            lbl.Text = text;
            tb.Location = new Point(130, y);
            tb.Size = new Size(width, 27);
        }

        private ToolStrip m_toolStrip;
        private ToolStripButton m_tsbSalveaza;
        private GroupBox m_gbDateFirma;
        private Label m_lblDenumire;
        private TextBox m_ebDenumire;
        private Label m_lblCUI;
        private TextBox m_ebCUI;
        private Label m_lblRegCom;
        private TextBox m_ebRegCom1;
        private Label m_lblSep1;
        private TextBox m_ebRegCom2;
        private Label m_lblSep2;
        private TextBox m_ebRegCom3;
        private Label m_lblContBancar;
        private TextBox m_ebContBancar;
        private CheckBox m_ckIsClient;
        private CheckBox m_ckIsFurnizor;
        private CheckBox m_ckIsSponsor;
        private GroupBox m_gbAdresa;
        private Label m_lblStrada;
        private TextBox m_ebStrada;
        private Label m_lblNumar;
        private TextBox m_ebNumar;
        private Label m_lblBloc;
        private TextBox m_ebBloc;
        private Label m_lblScara;
        private TextBox m_ebScara;
        private Label m_lblEtaj;
        private TextBox m_ebEtaj;
        private Label m_lblApartament;
        private TextBox m_ebApartament;
        private Label m_lblCodPostal;
        private TextBox m_ebCodPostal;
        private Label m_lblLocalitate;
        private TextBox m_ebLocalitate;
        private Label m_lblJudet;
        private TextBox m_ebJudet;
        private Label m_lblSector;
        private TextBox m_ebSector;
        private GroupBox m_gbContact;
        private Label m_lblTelefon;
        private TextBox m_ebTelefon;
        private Label m_lblTelefonMobil;
        private TextBox m_ebTelefonMobil;
        private Label m_lblEmail;
        private TextBox m_ebEmail;
        private Label m_lblPersoanaContact;
        private TextBox m_ebPersoanaContact;
    }
}

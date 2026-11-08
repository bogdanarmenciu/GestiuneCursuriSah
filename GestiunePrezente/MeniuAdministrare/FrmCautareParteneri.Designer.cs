using GridPersonalizat;

namespace GestiunePrezente.MeniuAdministrare
{
    partial class FrmCautareParteneri
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            m_toolStrip        = new ToolStrip();
            m_tsbCautare       = new ToolStripButton();
            m_tsbNou           = new ToolStripButton();
            m_tsbModifica      = new ToolStripButton();
            m_tsbStergere      = new ToolStripButton();
            m_panelFiltru      = new Panel();
            m_lblDenumire      = new Label();
            m_ebDenumire       = new TextBox();
            m_lblCUI           = new Label();
            m_ebCUI            = new TextBox();
            m_ckClient         = new CheckBox();
            m_ckFurnizor       = new CheckBox();
            m_ckSponsor        = new CheckBox();
            m_gParteneri       = new CustomGrid();
            m_toolStrip.SuspendLayout();
            m_panelFiltru.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_gParteneri).BeginInit();
            SuspendLayout();
            //
            // m_toolStrip
            //
            m_toolStrip.Items.AddRange(new ToolStripItem[] {
                m_tsbCautare, m_tsbNou, m_tsbModifica, m_tsbStergere });
            m_toolStrip.Dock = DockStyle.Top;
            m_toolStrip.ImageScalingSize = new Size(20, 20);
            m_toolStrip.Name = "m_toolStrip";
            //
            // m_tsbCautare
            //
            m_tsbCautare.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            m_tsbCautare.Image = Properties.Resources.search;
            m_tsbCautare.Name = "m_tsbCautare";
            m_tsbCautare.Text = "Caută";
            m_tsbCautare.Click += m_tsbCautare_Click;
            //
            // m_tsbNou
            //
            m_tsbNou.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            m_tsbNou.Image = Properties.Resources._new;
            m_tsbNou.Name = "m_tsbNou";
            m_tsbNou.Text = "Nou";
            m_tsbNou.Click += m_tsbNou_Click;
            //
            // m_tsbModifica
            //
            m_tsbModifica.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            m_tsbModifica.Image = Properties.Resources.open;
            m_tsbModifica.Name = "m_tsbModifica";
            m_tsbModifica.Text = "Modifică";
            m_tsbModifica.Click += m_tsbModifica_Click;
            //
            // m_tsbStergere
            //
            m_tsbStergere.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            m_tsbStergere.Image = Properties.Resources.Delete;
            m_tsbStergere.Name = "m_tsbStergere";
            m_tsbStergere.Text = "Șterge";
            m_tsbStergere.Click += m_tsbStergere_Click;
            //
            // m_panelFiltru
            //
            m_panelFiltru.Controls.Add(m_lblDenumire);
            m_panelFiltru.Controls.Add(m_ebDenumire);
            m_panelFiltru.Controls.Add(m_lblCUI);
            m_panelFiltru.Controls.Add(m_ebCUI);
            m_panelFiltru.Controls.Add(m_ckClient);
            m_panelFiltru.Controls.Add(m_ckFurnizor);
            m_panelFiltru.Controls.Add(m_ckSponsor);
            m_panelFiltru.Dock = DockStyle.Top;
            m_panelFiltru.Height = 55;
            m_panelFiltru.Name = "m_panelFiltru";
            //
            // m_lblDenumire
            //
            m_lblDenumire.AutoSize = true;
            m_lblDenumire.Location = new Point(8, 17);
            m_lblDenumire.Text = "Denumire:";
            //
            // m_ebDenumire
            //
            m_ebDenumire.Location = new Point(80, 13);
            m_ebDenumire.Size = new Size(220, 27);
            m_ebDenumire.Name = "m_ebDenumire";
            m_ebDenumire.TextChanged += m_ebDenumire_TextChanged;
            //
            // m_lblCUI
            //
            m_lblCUI.AutoSize = true;
            m_lblCUI.Location = new Point(313, 17);
            m_lblCUI.Text = "CUI:";
            //
            // m_ebCUI
            //
            m_ebCUI.Location = new Point(345, 13);
            m_ebCUI.Size = new Size(130, 27);
            m_ebCUI.Name = "m_ebCUI";
            m_ebCUI.TextChanged += m_ebCUI_TextChanged;
            //
            // m_ckClient
            //
            m_ckClient.AutoSize = true;
            m_ckClient.Location = new Point(500, 15);
            m_ckClient.Name = "m_ckClient";
            m_ckClient.Text = "Client";
            m_ckClient.CheckedChanged += m_ckTip_CheckedChanged;
            //
            // m_ckFurnizor
            //
            m_ckFurnizor.AutoSize = true;
            m_ckFurnizor.Location = new Point(580, 15);
            m_ckFurnizor.Name = "m_ckFurnizor";
            m_ckFurnizor.Text = "Furnizor";
            m_ckFurnizor.CheckedChanged += m_ckTip_CheckedChanged;
            //
            // m_ckSponsor
            //
            m_ckSponsor.AutoSize = true;
            m_ckSponsor.Location = new Point(670, 15);
            m_ckSponsor.Name = "m_ckSponsor";
            m_ckSponsor.Text = "Sponsor";
            m_ckSponsor.CheckedChanged += m_ckTip_CheckedChanged;
            //
            // m_gParteneri
            //
            m_gParteneri.Dock = DockStyle.Fill;
            m_gParteneri.Name = "m_gParteneri";
            m_gParteneri.ReadOnly = true;
            m_gParteneri.DoubleClick += m_gParteneri_DoubleClick;
            //
            // FrmCautareParteneri
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 650);
            Controls.Add(m_gParteneri);
            Controls.Add(m_panelFiltru);
            Controls.Add(m_toolStrip);
            Name = "FrmCautareParteneri";
            Text = "Căutare Parteneri";
            Load += FrmCautareParteneri_Load;
            m_toolStrip.ResumeLayout(false);
            m_toolStrip.PerformLayout();
            m_panelFiltru.ResumeLayout(false);
            m_panelFiltru.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)m_gParteneri).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private ToolStrip m_toolStrip;
        private ToolStripButton m_tsbCautare;
        private ToolStripButton m_tsbNou;
        private ToolStripButton m_tsbModifica;
        private ToolStripButton m_tsbStergere;
        private Panel m_panelFiltru;
        private Label m_lblDenumire;
        private TextBox m_ebDenumire;
        private Label m_lblCUI;
        private TextBox m_ebCUI;
        private CheckBox m_ckClient;
        private CheckBox m_ckFurnizor;
        private CheckBox m_ckSponsor;
        private CustomGrid m_gParteneri;
    }
}

using GridPersonalizat;

namespace GestiunePrezente.MeniuCursanti
{
    partial class FrmCautareIncasariAbonamente
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
            m_tsbSterge        = new ToolStripButton();
            m_panelFiltru      = new Panel();
            m_ckFiltruData     = new CheckBox();
            m_labelDeLaData    = new Label();
            m_dtpDeLaData      = new DateTimePicker();
            m_labelPanaLaData  = new Label();
            m_dtpPanaLaData    = new DateTimePicker();
            m_labelBeneficiar  = new Label();
            m_ebBeneficiar     = new TextBox();
            m_labelTipPlata    = new Label();
            m_cbTipPlata       = new ComboBox();
            m_labelTipLegatura = new Label();
            m_cbTipLegatura    = new ComboBox();
            m_panelTotal       = new Panel();
            m_labelTotal       = new Label();
            m_ebTotalIncasat   = new TextBox();
            m_panelGrid        = new Panel();
            m_gIncasari        = new CustomGrid();

            m_toolStrip.SuspendLayout();
            m_panelFiltru.SuspendLayout();
            m_panelTotal.SuspendLayout();
            m_panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_gIncasari).BeginInit();
            SuspendLayout();

            // ToolStrip
            m_toolStrip.ImageScalingSize = new Size(20, 20);
            m_toolStrip.Items.AddRange(new ToolStripItem[] { m_tsbCautare, m_tsbNou, m_tsbSterge });
            m_toolStrip.Location = new Point(0, 0);
            m_toolStrip.Size = new Size(1252, 27);
            m_toolStrip.TabIndex = 0;

            m_tsbCautare.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbCautare.Image = GestiunePrezente.Properties.Resources.search;
            m_tsbCautare.Name = "m_tsbCautare";
            m_tsbCautare.Text = "Caută";
            m_tsbCautare.Click += m_tsbCautare_Click;

            m_tsbNou.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbNou.Image = GestiunePrezente.Properties.Resources._new;
            m_tsbNou.Name = "m_tsbNou";
            m_tsbNou.Text = "Încasare nouă";
            m_tsbNou.Click += m_tsbNou_Click;

            m_tsbSterge.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbSterge.Image = GestiunePrezente.Properties.Resources.Delete;
            m_tsbSterge.Name = "m_tsbSterge";
            m_tsbSterge.Text = "Șterge";
            m_tsbSterge.Click += m_tsbSterge_Click;

            // ── Panel filtre (dock top, 95px) ─────────────────────────────
            m_panelFiltru.Dock = DockStyle.Top;
            m_panelFiltru.Location = new Point(0, 27);
            m_panelFiltru.Size = new Size(1252, 95);
            m_panelFiltru.TabIndex = 1;
            m_panelFiltru.BackColor = Color.FromArgb(248, 248, 252);

            // Rând 1 (y=12)
            m_ckFiltruData.AutoSize = true;
            m_ckFiltruData.Location = new Point(12, 15);
            m_ckFiltruData.Text = "Filtru dată:";
            m_ckFiltruData.CheckedChanged += m_ckFiltruData_CheckedChanged;

            m_labelDeLaData.AutoSize = true;
            m_labelDeLaData.Location = new Point(130, 15);
            m_labelDeLaData.Text = "De la:";

            m_dtpDeLaData.Location = new Point(185, 11);
            m_dtpDeLaData.Size = new Size(130, 27);
            m_dtpDeLaData.Format = DateTimePickerFormat.Custom;
            m_dtpDeLaData.CustomFormat = "dd.MM.yyyy";
            m_dtpDeLaData.TabIndex = 1;

            m_labelPanaLaData.AutoSize = true;
            m_labelPanaLaData.Location = new Point(325, 15);
            m_labelPanaLaData.Text = "Până la:";

            m_dtpPanaLaData.Location = new Point(380, 11);
            m_dtpPanaLaData.Size = new Size(130, 27);
            m_dtpPanaLaData.Format = DateTimePickerFormat.Custom;
            m_dtpPanaLaData.CustomFormat = "dd.MM.yyyy";
            m_dtpPanaLaData.TabIndex = 2;

            m_labelBeneficiar.AutoSize = true;
            m_labelBeneficiar.Location = new Point(540, 15);
            m_labelBeneficiar.Text = "Beneficiar:";

            m_ebBeneficiar.Location = new Point(620, 11);
            m_ebBeneficiar.Size = new Size(220, 27);
            m_ebBeneficiar.TabIndex = 3;
            m_ebBeneficiar.TextChanged += m_ebBeneficiar_TextChanged;

            // Rând 2 (y=52)
            m_labelTipPlata.AutoSize = true;
            m_labelTipPlata.Location = new Point(12, 56);
            m_labelTipPlata.Text = "Tip plată:";

            m_cbTipPlata.DropDownStyle = ComboBoxStyle.DropDownList;
            m_cbTipPlata.Location = new Point(90, 52);
            m_cbTipPlata.Size = new Size(130, 27);
            m_cbTipPlata.Items.AddRange(new object[] { "Toate", "Cash", "OP" });
            m_cbTipPlata.SelectedIndex = 0;
            m_cbTipPlata.TabIndex = 4;
            m_cbTipPlata.SelectedIndexChanged += m_cbTipPlata_SelectedIndexChanged;

            m_labelTipLegatura.AutoSize = true;
            m_labelTipLegatura.Location = new Point(240, 56);
            m_labelTipLegatura.Text = "Leagă de:";

            m_cbTipLegatura.DropDownStyle = ComboBoxStyle.DropDownList;
            m_cbTipLegatura.Location = new Point(320, 52);
            m_cbTipLegatura.Size = new Size(210, 27);
            m_cbTipLegatura.Items.AddRange(new object[] { "Toate", "Facturi", "Sponsorizare", "Fără legătură" });
            m_cbTipLegatura.SelectedIndex = 0;
            m_cbTipLegatura.TabIndex = 5;
            m_cbTipLegatura.SelectedIndexChanged += m_cbTipLegatura_SelectedIndexChanged;

            m_panelFiltru.Controls.Add(m_ckFiltruData);
            m_panelFiltru.Controls.Add(m_labelDeLaData);
            m_panelFiltru.Controls.Add(m_dtpDeLaData);
            m_panelFiltru.Controls.Add(m_labelPanaLaData);
            m_panelFiltru.Controls.Add(m_dtpPanaLaData);
            m_panelFiltru.Controls.Add(m_labelBeneficiar);
            m_panelFiltru.Controls.Add(m_ebBeneficiar);
            m_panelFiltru.Controls.Add(m_labelTipPlata);
            m_panelFiltru.Controls.Add(m_cbTipPlata);
            m_panelFiltru.Controls.Add(m_labelTipLegatura);
            m_panelFiltru.Controls.Add(m_cbTipLegatura);

            // ── Panel total (dock bottom, 42px) ───────────────────────────
            m_panelTotal.Dock = DockStyle.Bottom;
            m_panelTotal.Size = new Size(1252, 42);
            m_panelTotal.TabIndex = 3;
            m_panelTotal.BackColor = Color.FromArgb(240, 248, 240);

            m_labelTotal.AutoSize = true;
            m_labelTotal.Location = new Point(950, 12);
            m_labelTotal.Font = new Font("Arial", 9F, FontStyle.Bold);
            m_labelTotal.Text = "Total încasat filtrat:";

            m_ebTotalIncasat.Location = new Point(1090, 8);
            m_ebTotalIncasat.Size = new Size(140, 27);
            m_ebTotalIncasat.ReadOnly = true;
            m_ebTotalIncasat.BackColor = Color.LightGreen;
            m_ebTotalIncasat.TextAlign = HorizontalAlignment.Right;
            m_ebTotalIncasat.Font = new Font("Arial", 9F, FontStyle.Bold);
            m_ebTotalIncasat.TabStop = false;

            m_panelTotal.Controls.Add(m_labelTotal);
            m_panelTotal.Controls.Add(m_ebTotalIncasat);

            // ── Panel grid (fill) ─────────────────────────────────────────
            m_panelGrid.Dock = DockStyle.Fill;
            m_panelGrid.TabIndex = 2;

            m_gIncasari.Dock = DockStyle.Fill;
            m_gIncasari.Font = new Font("Arial", 8F);
            m_gIncasari.RowHeadersWidth = 25;
            m_gIncasari.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_gIncasari.TabIndex = 0;
            m_gIncasari.DoubleClick += m_gIncasari_DoubleClick;

            m_panelGrid.Controls.Add(m_gIncasari);

            // Form
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1252, 497);
            Controls.Add(m_panelGrid);
            Controls.Add(m_panelTotal);
            Controls.Add(m_panelFiltru);
            Controls.Add(m_toolStrip);
            Name = "FrmCautareIncasariAbonamente";
            Text = "Căutare Încasări";
            Load += FrmCautareIncasariAbonamente_Load;

            m_toolStrip.ResumeLayout(false);
            m_toolStrip.PerformLayout();
            m_panelFiltru.ResumeLayout(false);
            m_panelFiltru.PerformLayout();
            m_panelTotal.ResumeLayout(false);
            m_panelTotal.PerformLayout();
            m_panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)m_gIncasari).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.ToolStrip m_toolStrip;
        private System.Windows.Forms.ToolStripButton m_tsbCautare;
        private System.Windows.Forms.ToolStripButton m_tsbNou;
        private System.Windows.Forms.ToolStripButton m_tsbSterge;
        private System.Windows.Forms.Panel m_panelFiltru;
        private System.Windows.Forms.CheckBox m_ckFiltruData;
        private System.Windows.Forms.Label m_labelDeLaData;
        private System.Windows.Forms.DateTimePicker m_dtpDeLaData;
        private System.Windows.Forms.Label m_labelPanaLaData;
        private System.Windows.Forms.DateTimePicker m_dtpPanaLaData;
        private System.Windows.Forms.Label m_labelBeneficiar;
        private System.Windows.Forms.TextBox m_ebBeneficiar;
        private System.Windows.Forms.Label m_labelTipPlata;
        private System.Windows.Forms.ComboBox m_cbTipPlata;
        private System.Windows.Forms.Label m_labelTipLegatura;
        private System.Windows.Forms.ComboBox m_cbTipLegatura;
        private System.Windows.Forms.Panel m_panelTotal;
        private System.Windows.Forms.Label m_labelTotal;
        private System.Windows.Forms.TextBox m_ebTotalIncasat;
        private System.Windows.Forms.Panel m_panelGrid;
        private GridPersonalizat.CustomGrid m_gIncasari;
    }
}

using GridPersonalizat;

namespace GestiunePrezente.MeniuSituatii
{
    partial class FrmSituatiePrezente
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            toolStrip1 = new ToolStrip();
            m_tsbCautare = new ToolStripButton();
            panel1 = new Panel();
            m_lbAn = new Label();
            m_cbAn = new ComboBox();
            m_lbLuna = new Label();
            m_cbLuna = new ComboBox();
            m_lbCautare = new Label();
            m_ebCautare = new TextBox();
            m_ckActiv = new CheckBox();
            m_ckDoarDepasiri = new CheckBox();
            m_gPrezente = new CustomGrid();
            toolStrip1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_gPrezente).BeginInit();
            SuspendLayout();
            //
            // toolStrip1
            //
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { m_tsbCautare });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1400, 27);
            toolStrip1.TabIndex = 0;
            //
            // m_tsbCautare
            //
            m_tsbCautare.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbCautare.Image = Properties.Resources.search;
            m_tsbCautare.ImageTransparentColor = Color.Magenta;
            m_tsbCautare.Name = "m_tsbCautare";
            m_tsbCautare.Size = new Size(29, 24);
            m_tsbCautare.Text = "Cautare";
            m_tsbCautare.Click += m_tsbCautare_Click;
            //
            // panel1
            //
            panel1.Controls.Add(m_lbAn);
            panel1.Controls.Add(m_cbAn);
            panel1.Controls.Add(m_lbLuna);
            panel1.Controls.Add(m_cbLuna);
            panel1.Controls.Add(m_lbCautare);
            panel1.Controls.Add(m_ebCautare);
            panel1.Controls.Add(m_ckActiv);
            panel1.Controls.Add(m_ckDoarDepasiri);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 27);
            panel1.Name = "panel1";
            panel1.Size = new Size(1400, 60);
            panel1.TabIndex = 1;
            //
            // m_lbAn
            //
            m_lbAn.AutoSize = true;
            m_lbAn.Location = new Point(12, 18);
            m_lbAn.Name = "m_lbAn";
            m_lbAn.Size = new Size(25, 20);
            m_lbAn.TabIndex = 0;
            m_lbAn.Text = "An:";
            //
            // m_cbAn
            //
            m_cbAn.DropDownStyle = ComboBoxStyle.DropDownList;
            m_cbAn.Location = new Point(48, 14);
            m_cbAn.Name = "m_cbAn";
            m_cbAn.Size = new Size(80, 28);
            m_cbAn.TabIndex = 1;
            m_cbAn.SelectedIndexChanged += m_cbAn_SelectedIndexChanged;
            //
            // m_lbLuna
            //
            m_lbLuna.AutoSize = true;
            m_lbLuna.Location = new Point(148, 18);
            m_lbLuna.Name = "m_lbLuna";
            m_lbLuna.TabIndex = 2;
            m_lbLuna.Text = "Luna:";
            //
            // m_cbLuna
            //
            m_cbLuna.DropDownStyle = ComboBoxStyle.DropDownList;
            m_cbLuna.Location = new Point(195, 14);
            m_cbLuna.Name = "m_cbLuna";
            m_cbLuna.Size = new Size(100, 28);
            m_cbLuna.TabIndex = 3;
            m_cbLuna.SelectedIndexChanged += m_cbLuna_SelectedIndexChanged;
            //
            // m_lbCautare
            //
            m_lbCautare.AutoSize = true;
            m_lbCautare.Location = new Point(320, 18);
            m_lbCautare.Name = "m_lbCautare";
            m_lbCautare.Size = new Size(120, 20);
            m_lbCautare.TabIndex = 4;
            m_lbCautare.Text = "Contract/Beneficiar:";
            //
            // m_ebCautare
            //
            m_ebCautare.Location = new Point(455, 14);
            m_ebCautare.Name = "m_ebCautare";
            m_ebCautare.Size = new Size(200, 27);
            m_ebCautare.TabIndex = 5;
            m_ebCautare.TextChanged += m_ebCautare_TextChanged;
            //
            // m_ckActiv
            //
            m_ckActiv.AutoSize = true;
            m_ckActiv.Checked = true;
            m_ckActiv.CheckState = CheckState.Checked;
            m_ckActiv.Location = new Point(675, 17);
            m_ckActiv.Name = "m_ckActiv";
            m_ckActiv.Size = new Size(63, 24);
            m_ckActiv.TabIndex = 6;
            m_ckActiv.Text = "Activi";
            m_ckActiv.CheckedChanged += m_ckActiv_CheckedChanged;
            //
            // m_ckDoarDepasiri
            //
            m_ckDoarDepasiri.AutoSize = true;
            m_ckDoarDepasiri.Location = new Point(755, 17);
            m_ckDoarDepasiri.Name = "m_ckDoarDepasiri";
            m_ckDoarDepasiri.Size = new Size(130, 24);
            m_ckDoarDepasiri.TabIndex = 4;
            m_ckDoarDepasiri.Text = "Doar depasiri";
            m_ckDoarDepasiri.CheckedChanged += m_ckDoarDepasiri_CheckedChanged;
            //
            // m_gPrezente
            //
            m_gPrezente.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_gPrezente.Dock = DockStyle.Fill;
            m_gPrezente.Font = new Font("Arial", 8F);
            m_gPrezente.Location = new Point(0, 87);
            m_gPrezente.Name = "m_gPrezente";
            m_gPrezente.ReadOnly = true;
            m_gPrezente.RowHeadersWidth = 51;
            m_gPrezente.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            m_gPrezente.Size = new Size(1400, 513);
            m_gPrezente.TabIndex = 5;
            //
            // FrmSituatiePrezente
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1400, 600);
            Controls.Add(m_gPrezente);
            Controls.Add(panel1);
            Controls.Add(toolStrip1);
            Margin = new Padding(3, 5, 3, 5);
            MaximizeBox = false;
            Name = "FrmSituatiePrezente";
            Text = "Situatie prezente";
            WindowState = FormWindowState.Maximized;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)m_gPrezente).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton m_tsbCautare;
        private Panel panel1;
        private Label m_lbAn;
        private ComboBox m_cbAn;
        private Label m_lbLuna;
        private ComboBox m_cbLuna;
        private Label m_lbCautare;
        private TextBox m_ebCautare;
        private CheckBox m_ckActiv;
        private CheckBox m_ckDoarDepasiri;
        private CustomGrid m_gPrezente;
    }
}

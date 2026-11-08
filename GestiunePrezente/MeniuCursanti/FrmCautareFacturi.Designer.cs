using GridPersonalizat;

namespace GestiunePrezente.MeniuCursanti
{
    partial class FrmCautareFacturi
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
            toolStrip1    = new ToolStrip();
            m_tsbCautare  = new ToolStripButton();
            m_tsbNou      = new ToolStripButton();
            m_tsbModifica = new ToolStripButton();
            m_tsbSterge   = new ToolStripButton();
            panel1        = new Panel();
            m_labelAn     = new Label();
            m_cbAn        = new ComboBox();
            m_labelLuna   = new Label();
            m_cbLuna      = new ComboBox();
            m_labelStatus = new Label();
            m_cbStatus    = new ComboBox();
            m_labelCautare = new Label();
            m_ebCautare   = new TextBox();
            m_gFacturi    = new CustomGrid();
            toolStrip1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_gFacturi).BeginInit();
            SuspendLayout();
            // toolStrip1
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[]
            {
                m_tsbCautare, new ToolStripSeparator(), m_tsbNou, m_tsbModifica, m_tsbSterge
            });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Padding = new Padding(0, 0, 2, 0);
            toolStrip1.Size = new Size(1400, 27);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // m_tsbCautare
            m_tsbCautare.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbCautare.Image = Properties.Resources.search;
            m_tsbCautare.ImageTransparentColor = Color.Magenta;
            m_tsbCautare.Name = "m_tsbCautare";
            m_tsbCautare.Size = new Size(29, 24);
            m_tsbCautare.Text = "Caută";
            m_tsbCautare.Click += m_tsbCautare_Click;
            // m_tsbNou
            m_tsbNou.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbNou.Image = Properties.Resources._new;
            m_tsbNou.ImageTransparentColor = Color.Magenta;
            m_tsbNou.Name = "m_tsbNou";
            m_tsbNou.Size = new Size(29, 24);
            m_tsbNou.Text = "Factură nouă";
            m_tsbNou.Click += m_tsbNou_Click;
            // m_tsbModifica
            m_tsbModifica.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbModifica.Image = Properties.Resources.open;
            m_tsbModifica.ImageTransparentColor = Color.Magenta;
            m_tsbModifica.Name = "m_tsbModifica";
            m_tsbModifica.Size = new Size(29, 24);
            m_tsbModifica.Text = "Modifică / Încasează";
            m_tsbModifica.Click += m_tsbModifica_Click;
            // m_tsbSterge
            m_tsbSterge.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbSterge.Image = Properties.Resources.Delete;
            m_tsbSterge.ImageTransparentColor = Color.Magenta;
            m_tsbSterge.Name = "m_tsbSterge";
            m_tsbSterge.Size = new Size(29, 24);
            m_tsbSterge.Text = "Șterge";
            m_tsbSterge.Click += m_tsbSterge_Click;
            // panel1
            panel1.Controls.Add(m_ebCautare);
            panel1.Controls.Add(m_labelCautare);
            panel1.Controls.Add(m_cbStatus);
            panel1.Controls.Add(m_labelStatus);
            panel1.Controls.Add(m_cbLuna);
            panel1.Controls.Add(m_labelLuna);
            panel1.Controls.Add(m_cbAn);
            panel1.Controls.Add(m_labelAn);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 27);
            panel1.Margin = new Padding(3, 5, 3, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(1400, 40);
            panel1.TabIndex = 1;
            // m_labelAn
            m_labelAn.AutoSize = true;
            m_labelAn.Location = new Point(10, 10);
            m_labelAn.Name = "m_labelAn";
            m_labelAn.Size = new Size(24, 20);
            m_labelAn.TabIndex = 0;
            m_labelAn.Text = "An:";
            // m_cbAn
            m_cbAn.DropDownStyle = ComboBoxStyle.DropDownList;
            m_cbAn.Location = new Point(40, 7);
            m_cbAn.Name = "m_cbAn";
            m_cbAn.Size = new Size(80, 28);
            m_cbAn.TabIndex = 1;
            m_cbAn.SelectedIndexChanged += m_cbAn_SelectedIndexChanged;
            // m_labelLuna
            m_labelLuna.AutoSize = true;
            m_labelLuna.Location = new Point(132, 10);
            m_labelLuna.Name = "m_labelLuna";
            m_labelLuna.Size = new Size(40, 20);
            m_labelLuna.TabIndex = 2;
            m_labelLuna.Text = "Luna:";
            // m_cbLuna
            m_cbLuna.DropDownStyle = ComboBoxStyle.DropDownList;
            m_cbLuna.Location = new Point(178, 7);
            m_cbLuna.Name = "m_cbLuna";
            m_cbLuna.Size = new Size(70, 28);
            m_cbLuna.TabIndex = 3;
            m_cbLuna.SelectedIndexChanged += m_cbLuna_SelectedIndexChanged;
            // m_labelStatus
            m_labelStatus.AutoSize = true;
            m_labelStatus.Location = new Point(262, 10);
            m_labelStatus.Name = "m_labelStatus";
            m_labelStatus.Size = new Size(50, 20);
            m_labelStatus.TabIndex = 4;
            m_labelStatus.Text = "Status:";
            // m_cbStatus
            m_cbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            m_cbStatus.Location = new Point(318, 7);
            m_cbStatus.Name = "m_cbStatus";
            m_cbStatus.Size = new Size(130, 28);
            m_cbStatus.TabIndex = 5;
            m_cbStatus.SelectedIndexChanged += m_cbStatus_SelectedIndexChanged;
            // m_labelCautare
            m_labelCautare.AutoSize = true;
            m_labelCautare.Location = new Point(464, 10);
            m_labelCautare.Name = "m_labelCautare";
            m_labelCautare.Size = new Size(55, 20);
            m_labelCautare.TabIndex = 6;
            m_labelCautare.Text = "Căutare:";
            // m_ebCautare
            m_ebCautare.Location = new Point(526, 7);
            m_ebCautare.Margin = new Padding(3, 5, 3, 5);
            m_ebCautare.Name = "m_ebCautare";
            m_ebCautare.Size = new Size(220, 27);
            m_ebCautare.TabIndex = 7;
            m_ebCautare.TextChanged += m_ebCautare_TextChanged;
            // m_gFacturi
            m_gFacturi.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_gFacturi.Dock = DockStyle.Fill;
            m_gFacturi.Font = new Font("Arial", 8F);
            m_gFacturi.Location = new Point(0, 67);
            m_gFacturi.Margin = new Padding(2, 1, 2, 1);
            m_gFacturi.Name = "m_gFacturi";
            m_gFacturi.ReadOnly = true;
            m_gFacturi.RowHeadersWidth = 25;
            m_gFacturi.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            m_gFacturi.Size = new Size(1400, 546);
            m_gFacturi.TabIndex = 2;
            m_gFacturi.DoubleClick += m_gFacturi_DoubleClick;
            // FrmCautareFacturi
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1400, 613);
            Controls.Add(m_gFacturi);
            Controls.Add(panel1);
            Controls.Add(toolStrip1);
            Margin = new Padding(3, 5, 3, 5);
            MaximizeBox = false;
            Name = "FrmCautareFacturi";
            Text = "Facturi";
            WindowState = FormWindowState.Maximized;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)m_gFacturi).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton m_tsbCautare;
        private System.Windows.Forms.ToolStripButton m_tsbNou;
        private System.Windows.Forms.ToolStripButton m_tsbModifica;
        private System.Windows.Forms.ToolStripButton m_tsbSterge;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label m_labelAn;
        private System.Windows.Forms.ComboBox m_cbAn;
        private System.Windows.Forms.Label m_labelLuna;
        private System.Windows.Forms.ComboBox m_cbLuna;
        private System.Windows.Forms.Label m_labelStatus;
        private System.Windows.Forms.ComboBox m_cbStatus;
        private System.Windows.Forms.Label m_labelCautare;
        private System.Windows.Forms.TextBox m_ebCautare;
        private GridPersonalizat.CustomGrid m_gFacturi;
    }
}

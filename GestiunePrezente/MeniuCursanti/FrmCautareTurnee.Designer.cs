using GridPersonalizat;

namespace GestiunePrezente.MeniuCursanti
{
    partial class FrmCautareTurnee
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
            toolStrip1          = new ToolStrip();
            m_tsbCautare        = new ToolStripButton();
            m_tsbNou            = new ToolStripButton();
            m_tsbSterge         = new ToolStripButton();
            panel1              = new Panel();
            m_stDenumire        = new Label();
            m_ebDenumire        = new TextBox();
            m_gTurnee           = new CustomGrid();
            toolStrip1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_gTurnee).BeginInit();
            SuspendLayout();

            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { m_tsbCautare, m_tsbNou, m_tsbSterge });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1040, 27);
            toolStrip1.TabIndex = 0;

            m_tsbCautare.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbCautare.Image = GestiunePrezente.Properties.Resources.search;
            m_tsbCautare.ImageTransparentColor = Color.Magenta;
            m_tsbCautare.Name = "m_tsbCautare";
            m_tsbCautare.Size = new Size(24, 24);
            m_tsbCautare.Text = "Cautare";
            m_tsbCautare.Click += m_tsbCautare_Click;

            m_tsbNou.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbNou.Image = GestiunePrezente.Properties.Resources._new;
            m_tsbNou.ImageTransparentColor = Color.Magenta;
            m_tsbNou.Name = "m_tsbNou";
            m_tsbNou.Size = new Size(24, 24);
            m_tsbNou.Text = "Turneu nou";
            m_tsbNou.Click += m_tsbNou_Click;

            m_tsbSterge.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbSterge.Enabled = false;
            m_tsbSterge.Image = GestiunePrezente.Properties.Resources.Delete;
            m_tsbSterge.ImageTransparentColor = Color.Magenta;
            m_tsbSterge.Name = "m_tsbSterge";
            m_tsbSterge.Size = new Size(24, 24);
            m_tsbSterge.Text = "Sterge";
            m_tsbSterge.Click += m_tsbSterge_Click;

            panel1.Controls.Add(m_ebDenumire);
            panel1.Controls.Add(m_stDenumire);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 27);
            panel1.Name = "panel1";
            panel1.Size = new Size(1040, 55);
            panel1.TabIndex = 1;

            m_stDenumire.AutoSize = true;
            m_stDenumire.Location = new Point(12, 18);
            m_stDenumire.Name = "m_stDenumire";
            m_stDenumire.Text = "Denumire:";

            m_ebDenumire.Location = new Point(90, 15);
            m_ebDenumire.Name = "m_ebDenumire";
            m_ebDenumire.Size = new Size(250, 23);
            m_ebDenumire.TabIndex = 0;
            m_ebDenumire.TextChanged += m_ebDenumire_TextChanged;

            m_gTurnee.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_gTurnee.Dock = DockStyle.Fill;
            m_gTurnee.Font = new Font("Arial", 8F);
            m_gTurnee.Location = new Point(0, 82);
            m_gTurnee.Name = "m_gTurnee";
            m_gTurnee.ReadOnly = true;
            m_gTurnee.RowHeadersWidth = 25;
            m_gTurnee.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            m_gTurnee.Size = new Size(1040, 415);
            m_gTurnee.TabIndex = 2;
            m_gTurnee.DoubleClick += m_gTurnee_DoubleClick;
            m_gTurnee.SelectionChanged += m_gTurnee_SelectionChanged;

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1040, 497);
            Controls.Add(m_gTurnee);
            Controls.Add(panel1);
            Controls.Add(toolStrip1);
            Name = "FrmCautareTurnee";
            Text = "Turnee";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)m_gTurnee).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton m_tsbCautare;
        private ToolStripButton m_tsbNou;
        private ToolStripButton m_tsbSterge;
        private Panel panel1;
        private Label m_stDenumire;
        private TextBox m_ebDenumire;
        private CustomGrid m_gTurnee;
    }
}

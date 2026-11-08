namespace GestiunePrezente.MeniuConfigurare
{
    partial class FrmCautareDiviziuni
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            toolStrip1 = new ToolStrip();
            m_tsbCautare = new ToolStripButton();
            m_tsbNou = new ToolStripButton();
            m_stbStergere = new ToolStripButton();
            m_gDiviziuni = new GridPersonalizat.CustomGrid();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_gDiviziuni).BeginInit();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { m_tsbCautare, m_tsbNou, m_stbStergere });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Padding = new Padding(0, 0, 2, 0);
            toolStrip1.Size = new Size(933, 27);
            toolStrip1.TabIndex = 5;
            toolStrip1.Text = "toolStrip1";
            // 
            // m_tsbCautare
            // 
            m_tsbCautare.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbCautare.Image = GestiunePrezente.Properties.Resources.search;
            m_tsbCautare.ImageTransparentColor = Color.Magenta;
            m_tsbCautare.Name = "m_tsbCautare";
            m_tsbCautare.Size = new Size(24, 24);
            m_tsbCautare.Text = "Cautare";
            m_tsbCautare.Click += m_tsbCautare_Click;
            // 
            // m_tsbNou
            // 
            m_tsbNou.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbNou.Image = GestiunePrezente.Properties.Resources._new;
            m_tsbNou.ImageTransparentColor = Color.Magenta;
            m_tsbNou.Name = "m_tsbNou";
            m_tsbNou.Size = new Size(24, 24);
            m_tsbNou.Text = "Nou";
            m_tsbNou.Click += m_tsbNou_Click;
            // 
            // m_stbStergere
            // 
            m_stbStergere.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_stbStergere.Image = GestiunePrezente.Properties.Resources.Delete;
            m_stbStergere.ImageTransparentColor = Color.Magenta;
            m_stbStergere.Name = "m_stbStergere";
            m_stbStergere.Size = new Size(24, 24);
            m_stbStergere.Text = "Sterge";
            // 
            // m_gDiviziuni
            // 
            m_gDiviziuni.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_gDiviziuni.Dock = DockStyle.Fill;
            m_gDiviziuni.Font = new Font("Arial", 8F);
            m_gDiviziuni.Location = new Point(0, 27);
            m_gDiviziuni.Name = "m_gDiviziuni";
            m_gDiviziuni.RowHeadersWidth = 82;
            m_gDiviziuni.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            m_gDiviziuni.Size = new Size(933, 470);
            m_gDiviziuni.TabIndex = 6;
            m_gDiviziuni.DoubleClick += m_gDiviziuni_DoubleClick;
            // 
            // FrmCautareDiviziuni
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(933, 497);
            Controls.Add(m_gDiviziuni);
            Controls.Add(toolStrip1);
            Name = "FrmCautareDiviziuni";
            Text = "Cautare Diviziuni";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)m_gDiviziuni).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton m_tsbCautare;
        private System.Windows.Forms.ToolStripButton m_tsbNou;
        private System.Windows.Forms.ToolStripButton m_stbStergere;
        private GridPersonalizat.CustomGrid m_gDiviziuni;
    }
}
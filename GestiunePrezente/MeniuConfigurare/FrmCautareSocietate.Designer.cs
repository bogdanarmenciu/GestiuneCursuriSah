namespace GestiunePrezente.MeniuConfigurare
{
    partial class FrmCautareSocietate
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
            m_gSocietate = new GridPersonalizat.CustomGrid();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_gSocietate).BeginInit();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { m_tsbCautare });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Padding = new Padding(0, 0, 2, 0);
            toolStrip1.Size = new Size(933, 27);
            toolStrip1.TabIndex = 6;
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
            // m_gSocietate
            // 
            m_gSocietate.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_gSocietate.Dock = DockStyle.Fill;
            m_gSocietate.Font = new Font("Arial", 8F);
            m_gSocietate.Location = new Point(0, 27);
            m_gSocietate.Name = "m_gSocietate";
            m_gSocietate.RowHeadersWidth = 82;
            m_gSocietate.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            m_gSocietate.Size = new Size(933, 470);
            m_gSocietate.TabIndex = 7;
            m_gSocietate.DoubleClick += m_gSocietate_DoubleClick;
            // 
            // FrmCautareSocietate
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(933, 497);
            Controls.Add(m_gSocietate);
            Controls.Add(toolStrip1);
            Name = "FrmCautareSocietate";
            Text = "Cautare Societate";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)m_gSocietate).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton m_tsbCautare;
        private GridPersonalizat.CustomGrid m_gSocietate;
    }
}
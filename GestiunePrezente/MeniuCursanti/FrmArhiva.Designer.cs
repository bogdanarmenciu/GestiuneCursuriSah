using GridPersonalizat;

namespace GestiunePrezente.MeniuCursanti
{
    partial class FrmArhiva
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            m_toolStrip = new ToolStrip();
            m_tsbAdauga = new ToolStripButton();
            m_tsbDeschide = new ToolStripButton();
            m_tsbSterge = new ToolStripButton();
            m_gArhiva = new CustomGrid();
            m_toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_gArhiva).BeginInit();
            SuspendLayout();
            //
            // m_toolStrip
            //
            m_toolStrip.Items.AddRange(new ToolStripItem[] { m_tsbAdauga, m_tsbDeschide, m_tsbSterge });
            m_toolStrip.Location = new Point(0, 0);
            m_toolStrip.Name = "m_toolStrip";
            m_toolStrip.Size = new Size(1100, 27);
            m_toolStrip.TabIndex = 0;
            //
            // m_tsbAdauga
            //
            m_tsbAdauga.Name = "m_tsbAdauga";
            m_tsbAdauga.Size = new Size(104, 24);
            m_tsbAdauga.Text = "Adaugă fișier";
            m_tsbAdauga.Click += m_tsbAdauga_Click;
            //
            // m_tsbDeschide
            //
            m_tsbDeschide.Name = "m_tsbDeschide";
            m_tsbDeschide.Size = new Size(74, 24);
            m_tsbDeschide.Text = "Deschide";
            m_tsbDeschide.Click += m_tsbDeschide_Click;
            //
            // m_tsbSterge
            //
            m_tsbSterge.Name = "m_tsbSterge";
            m_tsbSterge.Size = new Size(57, 24);
            m_tsbSterge.Text = "Șterge";
            m_tsbSterge.Click += m_tsbSterge_Click;
            //
            // m_gArhiva
            //
            m_gArhiva.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_gArhiva.Dock = DockStyle.Fill;
            m_gArhiva.Font = new Font("Arial", 8F);
            m_gArhiva.Name = "m_gArhiva";
            m_gArhiva.ReadOnly = true;
            m_gArhiva.RowHeadersWidth = 51;
            m_gArhiva.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            m_gArhiva.TabIndex = 1;
            //
            // FrmArhiva
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 600);
            Controls.Add(m_gArhiva);
            Controls.Add(m_toolStrip);
            Name = "FrmArhiva";
            Text = "Arhivă";
            WindowState = FormWindowState.Maximized;
            Load += FrmArhiva_Load;
            m_toolStrip.ResumeLayout(false);
            m_toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)m_gArhiva).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private ToolStrip m_toolStrip;
        private ToolStripButton m_tsbAdauga;
        private ToolStripButton m_tsbDeschide;
        private ToolStripButton m_tsbSterge;
        private CustomGrid m_gArhiva;
    }
}

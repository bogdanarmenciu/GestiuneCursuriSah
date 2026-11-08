using GestiunePrezente.Common;

namespace GestiunePrezente.MeniuCursanti
{
    partial class FrmCautareAbonamente
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
            m_tsbSterge = new ToolStripButton();
            m_gAbonamente = new GridPersonalizat.CustomGrid();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_gAbonamente).BeginInit();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { m_tsbCautare, m_tsbNou, m_tsbSterge });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Padding = new Padding(0, 0, 2, 0);
            toolStrip1.Size = new Size(934, 27);
            toolStrip1.TabIndex = 2;
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
            // m_tsbSterge
            // 
            m_tsbSterge.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbSterge.Enabled = false;
            m_tsbSterge.Image = GestiunePrezente.Properties.Resources.Delete;
            m_tsbSterge.ImageTransparentColor = Color.Magenta;
            m_tsbSterge.Name = "m_tsbSterge";
            m_tsbSterge.Size = new Size(24, 24);
            m_tsbSterge.Text = "Sterge";
            m_tsbSterge.Click += m_tsbSterge_Click;
            // 
            // m_gAbonamente
            // 
            m_gAbonamente.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_gAbonamente.Dock = DockStyle.Fill;
            m_gAbonamente.Font = new Font("Arial", 8F);
            m_gAbonamente.Location = new Point(0, 27);
            m_gAbonamente.Margin = new Padding(3, 4, 3, 4);
            m_gAbonamente.Name = "m_gAbonamente";
            m_gAbonamente.ReadOnly = true;
            m_gAbonamente.RowHeadersWidth = 51;
            m_gAbonamente.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            m_gAbonamente.Size = new Size(934, 470);
            m_gAbonamente.TabIndex = 3;
            m_gAbonamente.DoubleClick += m_gAbonamente_DoubleClick;
            // 
            // FrmCautareAbonamente
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(934, 497);
            Controls.Add(m_gAbonamente);
            Controls.Add(toolStrip1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FrmCautareAbonamente";
            Text = "Cautare Abonamente";
            WindowState = FormWindowState.Maximized;
            Load += FrmCautareAbonamente_Load;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)m_gAbonamente).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton m_tsbCautare;
        private System.Windows.Forms.ToolStripButton m_tsbNou;
        private System.Windows.Forms.ToolStripButton m_tsbSterge;
        private GridPersonalizat.CustomGrid m_gAbonamente;
    }
}
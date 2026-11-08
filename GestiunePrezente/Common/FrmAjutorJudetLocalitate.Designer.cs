namespace GestiunePrezente.Common
{
    partial class FrmAjutorJudetLocalitate
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
            panel1 = new Panel();
            m_ebJudetul = new TextBox();
            m_ebLocalitatea = new TextBox();
            label20 = new Label();
            label3 = new Label();
            panel2 = new Panel();
            label2 = new Label();
            panel3 = new Panel();
            m_gJudet = new GridPersonalizat.CustomGrid();
            panel4 = new Panel();
            label1 = new Label();
            panel5 = new Panel();
            m_gLocalitate = new GridPersonalizat.CustomGrid();
            toolStrip1.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_gJudet).BeginInit();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_gLocalitate).BeginInit();
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
            toolStrip1.TabIndex = 1;
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
            // panel1
            // 
            panel1.Controls.Add(m_ebJudetul);
            panel1.Controls.Add(m_ebLocalitatea);
            panel1.Controls.Add(label20);
            panel1.Controls.Add(label3);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 27);
            panel1.Name = "panel1";
            panel1.Size = new Size(933, 52);
            panel1.TabIndex = 2;
            // 
            // m_ebJudetul
            // 
            m_ebJudetul.BackColor = SystemColors.Window;
            m_ebJudetul.Location = new Point(151, 13);
            m_ebJudetul.Name = "m_ebJudetul";
            m_ebJudetul.Size = new Size(153, 23);
            m_ebJudetul.TabIndex = 34;
            m_ebJudetul.TextChanged += m_ebJudetul_TextChanged;
            // 
            // m_ebLocalitatea
            // 
            m_ebLocalitatea.BackColor = SystemColors.Window;
            m_ebLocalitatea.Location = new Point(452, 15);
            m_ebLocalitatea.Name = "m_ebLocalitatea";
            m_ebLocalitatea.Size = new Size(153, 23);
            m_ebLocalitatea.TabIndex = 33;
            m_ebLocalitatea.TextChanged += m_ebLocalitatea_TextChanged;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(47, 15);
            label20.Name = "label20";
            label20.Size = new Size(45, 15);
            label20.TabIndex = 36;
            label20.Text = "Judetul";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(348, 17);
            label3.Name = "label3";
            label3.Size = new Size(64, 15);
            label3.TabIndex = 35;
            label3.Text = "Localitatea";
            // 
            // panel2
            // 
            panel2.Controls.Add(label2);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 79);
            panel2.Name = "panel2";
            panel2.Size = new Size(933, 23);
            panel2.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 6);
            label2.Name = "label2";
            label2.Size = new Size(35, 15);
            label2.TabIndex = 37;
            label2.Text = "Judet";
            // 
            // panel3
            // 
            panel3.Controls.Add(m_gJudet);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 102);
            panel3.Name = "panel3";
            panel3.Size = new Size(933, 189);
            panel3.TabIndex = 5;
            // 
            // m_gJudet
            // 
            m_gJudet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_gJudet.Dock = DockStyle.Fill;
            m_gJudet.Font = new Font("Arial", 8F);
            m_gJudet.Location = new Point(0, 0);
            m_gJudet.Name = "m_gJudet";
            m_gJudet.RowHeadersWidth = 82;
            m_gJudet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            m_gJudet.Size = new Size(933, 189);
            m_gJudet.TabIndex = 0;
            m_gJudet.CellMouseDown += m_gJudet_CellMouseDown;
            m_gJudet.DoubleClick += m_gJudet_DoubleClick;
            // 
            // panel4
            // 
            panel4.Controls.Add(label1);
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 291);
            panel4.Name = "panel4";
            panel4.Size = new Size(933, 21);
            panel4.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 3);
            label1.Name = "label1";
            label1.Size = new Size(58, 15);
            label1.TabIndex = 37;
            label1.Text = "Localitate";
            // 
            // panel5
            // 
            panel5.Controls.Add(m_gLocalitate);
            panel5.Dock = DockStyle.Top;
            panel5.Location = new Point(0, 312);
            panel5.Name = "panel5";
            panel5.Size = new Size(933, 189);
            panel5.TabIndex = 7;
            // 
            // m_gLocalitate
            // 
            m_gLocalitate.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_gLocalitate.Dock = DockStyle.Fill;
            m_gLocalitate.Font = new Font("Arial", 8F);
            m_gLocalitate.Location = new Point(0, 0);
            m_gLocalitate.Name = "m_gLocalitate";
            m_gLocalitate.RowHeadersWidth = 82;
            m_gLocalitate.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            m_gLocalitate.Size = new Size(933, 189);
            m_gLocalitate.TabIndex = 0;
            m_gLocalitate.CellMouseDown += m_gJudet_CellMouseDown;
            m_gLocalitate.DoubleClick += m_gLocalitate_DoubleClick;
            // 
            // FrmAjutorJudetLocalitate
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(933, 497);
            Controls.Add(panel5);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(toolStrip1);
            Name = "FrmAjutorJudetLocalitate";
            Text = "Ajutor Judet Localitate";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)m_gJudet).EndInit();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)m_gLocalitate).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton m_tsbCautare;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox m_ebJudetul;
        private System.Windows.Forms.TextBox m_ebLocalitatea;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel5;
        private GridPersonalizat.CustomGrid customGrid1;
        private GridPersonalizat.CustomGrid m_gJudet;
        private GridPersonalizat.CustomGrid m_gLocalitate;
    }
}
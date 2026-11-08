namespace GestiunePrezente.MeniuCursanti
{
    partial class FrmCautareFormatori
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
            panel1 = new Panel();
            label1 = new Label();
            m_ebPrenume = new TextBox();
            m_ebTelMobil = new TextBox();
            m_st2 = new Label();
            m_ebNume = new TextBox();
            m_st1 = new Label();
            m_gFormatori = new GridPersonalizat.CustomGrid();
            toolStrip1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_gFormatori).BeginInit();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { m_tsbCautare, m_tsbNou, m_tsbSterge });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Padding = new Padding(0, 0, 2, 0);
            toolStrip1.Size = new Size(1040, 27);
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
            // panel1
            // 
            panel1.Controls.Add(label1);
            panel1.Controls.Add(m_ebPrenume);
            panel1.Controls.Add(m_ebTelMobil);
            panel1.Controls.Add(m_st2);
            panel1.Controls.Add(m_ebNume);
            panel1.Controls.Add(m_st1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 27);
            panel1.Name = "panel1";
            panel1.Size = new Size(1040, 78);
            panel1.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(698, 33);
            label1.Name = "label1";
            label1.Size = new Size(80, 15);
            label1.TabIndex = 18;
            label1.Text = "Telefon mobil";
            // 
            // m_ebPrenume
            // 
            m_ebPrenume.Location = new Point(482, 27);
            m_ebPrenume.Name = "m_ebPrenume";
            m_ebPrenume.Size = new Size(153, 23);
            m_ebPrenume.TabIndex = 14;
            m_ebPrenume.TextChanged += m_ebPrenume_TextChanged;
            // 
            // m_ebTelMobil
            // 
            m_ebTelMobil.Location = new Point(804, 30);
            m_ebTelMobil.Name = "m_ebTelMobil";
            m_ebTelMobil.Size = new Size(153, 23);
            m_ebTelMobil.TabIndex = 17;
            m_ebTelMobil.TextChanged += m_ebTelMobil_TextChanged;
            // 
            // m_st2
            // 
            m_st2.AutoSize = true;
            m_st2.Location = new Point(348, 30);
            m_st2.Name = "m_st2";
            m_st2.Size = new Size(55, 15);
            m_st2.TabIndex = 16;
            m_st2.Text = "Prenume";
            // 
            // m_ebNume
            // 
            m_ebNume.Location = new Point(165, 27);
            m_ebNume.Name = "m_ebNume";
            m_ebNume.Size = new Size(153, 23);
            m_ebNume.TabIndex = 13;
            m_ebNume.TextChanged += m_ebNume_TextChanged;
            // 
            // m_st1
            // 
            m_st1.AutoSize = true;
            m_st1.Location = new Point(58, 30);
            m_st1.Name = "m_st1";
            m_st1.Size = new Size(40, 15);
            m_st1.TabIndex = 15;
            m_st1.Text = "Nume";
            // 
            // m_gFormatori
            // 
            m_gFormatori.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_gFormatori.Dock = DockStyle.Fill;
            m_gFormatori.Font = new Font("Arial", 8F);
            m_gFormatori.Location = new Point(0, 105);
            m_gFormatori.Name = "m_gFormatori";
            m_gFormatori.RowHeadersWidth = 82;
            m_gFormatori.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            m_gFormatori.Size = new Size(1040, 392);
            m_gFormatori.TabIndex = 15;
            m_gFormatori.DoubleClick += m_gFormatori_DoubleClick;
            // 
            // FrmCautareFormatori
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1040, 497);
            Controls.Add(m_gFormatori);
            Controls.Add(panel1);
            Controls.Add(toolStrip1);
            Name = "FrmCautareFormatori";
            Text = "Cautare Formatori";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)m_gFormatori).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton m_tsbCautare;
        private System.Windows.Forms.ToolStripButton m_tsbNou;
        private System.Windows.Forms.ToolStripButton m_tsbSterge;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox m_ebPrenume;
        private System.Windows.Forms.Label m_st2;
        private System.Windows.Forms.TextBox m_ebNume;
        private System.Windows.Forms.Label m_st1;
        private GridPersonalizat.CustomGrid m_gFormatori;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_ebTelMobil;
    }
}
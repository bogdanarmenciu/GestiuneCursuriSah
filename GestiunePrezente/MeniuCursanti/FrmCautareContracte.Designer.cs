using GridPersonalizat;

namespace GestiunePrezente.MeniuCursanti
{
    partial class FrmCautareContracte
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
            m_tsbDeschideArhiva = new ToolStripButton();
            panel1 = new Panel();
            m_ckActiv = new CheckBox();
            m_ckReziliat = new CheckBox();
            m_ckNeListat = new CheckBox();
            m_ckListat = new CheckBox();
            m_ebPrenume = new TextBox();
            m_st2 = new Label();
            m_ebNume = new TextBox();
            m_st1 = new Label();
            m_splitMain = new SplitContainer();
            m_gContracte = new CustomGrid();
            m_gRegistruInterne = new CustomGrid();
            m_stRegistru = new Label();
            toolStrip1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_splitMain).BeginInit();
            m_splitMain.Panel1.SuspendLayout();
            m_splitMain.Panel2.SuspendLayout();
            m_splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_gContracte).BeginInit();
            ((System.ComponentModel.ISupportInitialize)m_gRegistruInterne).BeginInit();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { m_tsbCautare, m_tsbNou, m_tsbSterge, m_tsbDeschideArhiva });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Padding = new Padding(0, 0, 2, 0);
            toolStrip1.Size = new Size(1683, 27);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
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
            // m_tsbNou
            // 
            m_tsbNou.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbNou.Image = Properties.Resources._new;
            m_tsbNou.ImageTransparentColor = Color.Magenta;
            m_tsbNou.Name = "m_tsbNou";
            m_tsbNou.Size = new Size(29, 24);
            m_tsbNou.Text = "Nou";
            m_tsbNou.Click += m_tsbNou_Click;
            // 
            // m_tsbSterge
            // 
            m_tsbSterge.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbSterge.Image = Properties.Resources.Delete;
            m_tsbSterge.ImageTransparentColor = Color.Magenta;
            m_tsbSterge.Name = "m_tsbSterge";
            m_tsbSterge.Size = new Size(29, 24);
            m_tsbSterge.Text = "Sterge";
            m_tsbSterge.Click += m_tsbSterge_Click;
            // 
            // m_tsbDeschideArhiva
            // 
            m_tsbDeschideArhiva.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbDeschideArhiva.Image = Properties.Resources.CursantArhiva;
            m_tsbDeschideArhiva.ImageTransparentColor = Color.Magenta;
            m_tsbDeschideArhiva.Name = "m_tsbDeschideArhiva";
            m_tsbDeschideArhiva.Size = new Size(29, 24);
            m_tsbDeschideArhiva.Text = "Deschide arhiva";
            m_tsbDeschideArhiva.Click += m_tsbDeschideArhiva_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(m_ckActiv);
            panel1.Controls.Add(m_ckReziliat);
            panel1.Controls.Add(m_ckNeListat);
            panel1.Controls.Add(m_ckListat);
            panel1.Controls.Add(m_ebPrenume);
            panel1.Controls.Add(m_st2);
            panel1.Controls.Add(m_ebNume);
            panel1.Controls.Add(m_st1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 27);
            panel1.Margin = new Padding(3, 5, 3, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(1683, 76);
            panel1.TabIndex = 13;
            // 
            // m_ckActiv
            // 
            m_ckActiv.AutoSize = true;
            m_ckActiv.Location = new Point(1110, 19);
            m_ckActiv.Name = "m_ckActiv";
            m_ckActiv.Size = new Size(64, 24);
            m_ckActiv.TabIndex = 82;
            m_ckActiv.Text = "Activ";
            m_ckActiv.UseVisualStyleBackColor = true;
            // 
            // m_ckReziliat
            // 
            m_ckReziliat.AutoSize = true;
            m_ckReziliat.Location = new Point(1024, 19);
            m_ckReziliat.Name = "m_ckReziliat";
            m_ckReziliat.Size = new Size(80, 24);
            m_ckReziliat.TabIndex = 81;
            m_ckReziliat.Text = "Reziliat";
            m_ckReziliat.UseVisualStyleBackColor = true;
            // 
            // m_ckNeListat
            // 
            m_ckNeListat.AutoSize = true;
            m_ckNeListat.Location = new Point(907, 19);
            m_ckNeListat.Name = "m_ckNeListat";
            m_ckNeListat.Size = new Size(82, 24);
            m_ckNeListat.TabIndex = 80;
            m_ckNeListat.Text = "Nelistat";
            m_ckNeListat.UseVisualStyleBackColor = true;
            m_ckNeListat.CheckedChanged += m_ckNeListat_CheckedChanged;
            // 
            // m_ckListat
            // 
            m_ckListat.AutoSize = true;
            m_ckListat.Location = new Point(818, 19);
            m_ckListat.Name = "m_ckListat";
            m_ckListat.Size = new Size(66, 24);
            m_ckListat.TabIndex = 79;
            m_ckListat.Text = "Listat";
            m_ckListat.UseVisualStyleBackColor = true;
            m_ckListat.CheckedChanged += m_ckListat_CheckedChanged;
            // 
            // m_ebPrenume
            // 
            m_ebPrenume.Location = new Point(518, 19);
            m_ebPrenume.Margin = new Padding(3, 5, 3, 5);
            m_ebPrenume.Name = "m_ebPrenume";
            m_ebPrenume.Size = new Size(175, 27);
            m_ebPrenume.TabIndex = 9;
            m_ebPrenume.TextChanged += m_ebPrenume_TextChanged;
            // 
            // m_st2
            // 
            m_st2.AutoSize = true;
            m_st2.Location = new Point(366, 23);
            m_st2.Name = "m_st2";
            m_st2.Size = new Size(67, 20);
            m_st2.TabIndex = 12;
            m_st2.Text = "Prenume";
            // 
            // m_ebNume
            // 
            m_ebNume.Location = new Point(157, 19);
            m_ebNume.Margin = new Padding(3, 5, 3, 5);
            m_ebNume.Name = "m_ebNume";
            m_ebNume.Size = new Size(175, 27);
            m_ebNume.TabIndex = 8;
            m_ebNume.TextChanged += m_ebNume_TextChanged;
            // 
            // m_st1
            // 
            m_st1.AutoSize = true;
            m_st1.Location = new Point(34, 23);
            m_st1.Name = "m_st1";
            m_st1.Size = new Size(49, 20);
            m_st1.TabIndex = 11;
            m_st1.Text = "Nume";
            // 
            // m_splitMain
            // 
            m_splitMain.Dock = DockStyle.Fill;
            m_splitMain.Location = new Point(0, 103);
            m_splitMain.Name = "m_splitMain";
            m_splitMain.Orientation = Orientation.Horizontal;
            // 
            // m_splitMain.Panel1
            // 
            m_splitMain.Panel1.Controls.Add(m_gContracte);
            // 
            // m_splitMain.Panel2
            // 
            m_splitMain.Panel2.Controls.Add(m_gRegistruInterne);
            m_splitMain.Panel2.Controls.Add(m_stRegistru);
            m_splitMain.Size = new Size(1683, 510);
            m_splitMain.SplitterDistance = 362;
            m_splitMain.TabIndex = 0;
            // 
            // m_gContracte
            // 
            m_gContracte.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_gContracte.Dock = DockStyle.Fill;
            m_gContracte.Font = new Font("Arial", 8F);
            m_gContracte.Location = new Point(0, 0);
            m_gContracte.Margin = new Padding(2, 1, 2, 1);
            m_gContracte.Name = "m_gContracte";
            m_gContracte.ReadOnly = true;
            m_gContracte.RowHeadersWidth = 82;
            m_gContracte.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            m_gContracte.Size = new Size(1683, 362);
            m_gContracte.TabIndex = 0;
            m_gContracte.SelectionChanged += m_gContracte_SelectionChanged;
            m_gContracte.DoubleClick += m_gContracte_DoubleClick;
            // 
            // m_gRegistruInterne
            // 
            m_gRegistruInterne.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_gRegistruInterne.Dock = DockStyle.Fill;
            m_gRegistruInterne.Font = new Font("Arial", 8F);
            m_gRegistruInterne.Location = new Point(0, 26);
            m_gRegistruInterne.Margin = new Padding(2, 1, 2, 1);
            m_gRegistruInterne.Name = "m_gRegistruInterne";
            m_gRegistruInterne.ReadOnly = true;
            m_gRegistruInterne.RowHeadersWidth = 82;
            m_gRegistruInterne.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            m_gRegistruInterne.Size = new Size(1683, 118);
            m_gRegistruInterne.TabIndex = 1;
            // 
            // m_stRegistru
            // 
            m_stRegistru.AutoSize = true;
            m_stRegistru.Dock = DockStyle.Top;
            m_stRegistru.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            m_stRegistru.Location = new Point(0, 0);
            m_stRegistru.Name = "m_stRegistru";
            m_stRegistru.Padding = new Padding(4, 4, 0, 2);
            m_stRegistru.Size = new Size(357, 26);
            m_stRegistru.TabIndex = 16;
            m_stRegistru.Text = "Numere interne emise pentru contractul selectat:";
            // 
            // FrmCautareContracte
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1683, 613);
            Controls.Add(m_splitMain);
            Controls.Add(panel1);
            Controls.Add(toolStrip1);
            Margin = new Padding(3, 5, 3, 5);
            MaximizeBox = false;
            Name = "FrmCautareContracte";
            Text = "Cautare Contracte";
            WindowState = FormWindowState.Maximized;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            m_splitMain.Panel1.ResumeLayout(false);
            m_splitMain.Panel2.ResumeLayout(false);
            m_splitMain.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)m_splitMain).EndInit();
            m_splitMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)m_gContracte).EndInit();
            ((System.ComponentModel.ISupportInitialize)m_gRegistruInterne).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton m_tsbCautare;
        private System.Windows.Forms.ToolStripButton m_tsbNou;
        private System.Windows.Forms.ToolStripButton m_tsbSterge;
        private System.Windows.Forms.ToolStripButton m_tsbDeschideArhiva;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox m_ebPrenume;
        private System.Windows.Forms.Label m_st2;
        private System.Windows.Forms.TextBox m_ebNume;
        private System.Windows.Forms.Label m_st1;
        private GridPersonalizat.CustomGrid m_gContracte;
        private CheckBox m_ckListat;
        private CheckBox m_ckNeListat;
        private CheckBox m_ckReziliat;
        private CheckBox m_ckActiv;
        private SplitContainer m_splitMain;
        private Label m_stRegistru;
        private GridPersonalizat.CustomGrid m_gRegistruInterne;
    }
}
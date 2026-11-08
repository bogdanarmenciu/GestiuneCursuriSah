using GridPersonalizat;

namespace GestiunePrezente.MeniuCursanti
{
    partial class FrmCautareCursanti
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
            m_tsbInscriereRapida = new ToolStripButton();
            m_tsbSterge = new ToolStripButton();
            m_tsbDeschideArhiva = new ToolStripButton();
            m_tsbListeazaBarcode = new ToolStripButton();
            panel1 = new Panel();
            m_ebEmail = new TextBox();
            label3 = new Label();
            label1 = new Label();
            m_ebTelMobil = new TextBox();
            m_ebPrenume = new TextBox();
            m_st2 = new Label();
            m_ebNume = new TextBox();
            m_st1 = new Label();
            m_st3 = new Label();
            m_ebCNP = new TextBox();
            m_ckActiv = new CheckBox();
            m_ckFaraContract = new CheckBox();
            m_gCursanti = new CustomGrid();
            toolStrip1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_gCursanti).BeginInit();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { m_tsbCautare, m_tsbNou, m_tsbInscriereRapida, m_tsbSterge, m_tsbDeschideArhiva, m_tsbListeazaBarcode });
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
            // m_tsbInscriereRapida
            // 
            m_tsbInscriereRapida.Image = Properties.Resources._new;
            m_tsbInscriereRapida.ImageTransparentColor = Color.Magenta;
            m_tsbInscriereRapida.Name = "m_tsbInscriereRapida";
            m_tsbInscriereRapida.Size = new Size(135, 24);
            m_tsbInscriereRapida.Text = "Inscriere rapidă";
            m_tsbInscriereRapida.Click += m_tsbInscriereRapida_Click;
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
            m_tsbDeschideArhiva.Enabled = false;
            m_tsbDeschideArhiva.Image = Properties.Resources.CursantArhiva;
            m_tsbDeschideArhiva.ImageTransparentColor = Color.Magenta;
            m_tsbDeschideArhiva.Name = "m_tsbDeschideArhiva";
            m_tsbDeschideArhiva.Size = new Size(29, 24);
            m_tsbDeschideArhiva.Text = "Deschide arhiva";
            // 
            // m_tsbListeazaBarcode
            // 
            m_tsbListeazaBarcode.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbListeazaBarcode.Image = Properties.Resources.Scanner;
            m_tsbListeazaBarcode.ImageTransparentColor = Color.Magenta;
            m_tsbListeazaBarcode.Name = "m_tsbListeazaBarcode";
            m_tsbListeazaBarcode.Size = new Size(29, 24);
            m_tsbListeazaBarcode.Text = "Listeaza Barcode";
            m_tsbListeazaBarcode.Click += m_tsbListeazaBarcode_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(m_ebEmail);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(m_ebTelMobil);
            panel1.Controls.Add(m_ebPrenume);
            panel1.Controls.Add(m_st2);
            panel1.Controls.Add(m_ebNume);
            panel1.Controls.Add(m_st1);
            panel1.Controls.Add(m_st3);
            panel1.Controls.Add(m_ebCNP);
            panel1.Controls.Add(m_ckActiv);
            panel1.Controls.Add(m_ckFaraContract);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 27);
            panel1.Margin = new Padding(3, 5, 3, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(1683, 117);
            panel1.TabIndex = 13;
            // 
            // m_ebEmail
            // 
            m_ebEmail.Location = new Point(597, 59);
            m_ebEmail.Margin = new Padding(3, 5, 3, 5);
            m_ebEmail.Name = "m_ebEmail";
            m_ebEmail.Size = new Size(175, 27);
            m_ebEmail.TabIndex = 15;
            m_ebEmail.TextChanged += m_ebEmail_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(445, 63);
            label3.Name = "label3";
            label3.Size = new Size(46, 20);
            label3.TabIndex = 17;
            label3.Text = "Email";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(113, 63);
            label1.Name = "label1";
            label1.Size = new Size(101, 20);
            label1.TabIndex = 16;
            label1.Text = "Telefon mobil";
            // 
            // m_ebTelMobil
            // 
            m_ebTelMobil.Location = new Point(235, 59);
            m_ebTelMobil.Margin = new Padding(3, 5, 3, 5);
            m_ebTelMobil.Name = "m_ebTelMobil";
            m_ebTelMobil.Size = new Size(175, 27);
            m_ebTelMobil.TabIndex = 14;
            m_ebTelMobil.TextChanged += m_ebTelMobil_TextChanged;
            // 
            // m_ebPrenume
            // 
            m_ebPrenume.Location = new Point(597, 19);
            m_ebPrenume.Margin = new Padding(3, 5, 3, 5);
            m_ebPrenume.Name = "m_ebPrenume";
            m_ebPrenume.Size = new Size(175, 27);
            m_ebPrenume.TabIndex = 9;
            m_ebPrenume.TextChanged += m_ebPrenume_TextChanged;
            // 
            // m_st2
            // 
            m_st2.AutoSize = true;
            m_st2.Location = new Point(445, 23);
            m_st2.Name = "m_st2";
            m_st2.Size = new Size(67, 20);
            m_st2.TabIndex = 12;
            m_st2.Text = "Prenume";
            // 
            // m_ebNume
            // 
            m_ebNume.Location = new Point(235, 19);
            m_ebNume.Margin = new Padding(3, 5, 3, 5);
            m_ebNume.Name = "m_ebNume";
            m_ebNume.Size = new Size(175, 27);
            m_ebNume.TabIndex = 8;
            m_ebNume.TextChanged += m_ebNume_TextChanged;
            // 
            // m_st1
            // 
            m_st1.AutoSize = true;
            m_st1.Location = new Point(113, 23);
            m_st1.Name = "m_st1";
            m_st1.Size = new Size(49, 20);
            m_st1.TabIndex = 11;
            m_st1.Text = "Nume";
            // 
            // m_st3
            // 
            m_st3.AutoSize = true;
            m_st3.Location = new Point(829, 23);
            m_st3.Name = "m_st3";
            m_st3.Size = new Size(37, 20);
            m_st3.TabIndex = 13;
            m_st3.Text = "CNP";
            //
            // m_ebCNP
            //
            m_ebCNP.Location = new Point(965, 19);
            m_ebCNP.Margin = new Padding(3, 5, 3, 5);
            m_ebCNP.Name = "m_ebCNP";
            m_ebCNP.Size = new Size(175, 27);
            m_ebCNP.TabIndex = 10;
            m_ebCNP.TextChanged += m_ebCNP_TextChanged;
            //
            // m_ckActiv
            //
            m_ckActiv.AutoSize = true;
            m_ckActiv.Location = new Point(965, 62);
            m_ckActiv.Name = "m_ckActiv";
            m_ckActiv.Size = new Size(63, 24);
            m_ckActiv.TabIndex = 18;
            m_ckActiv.Text = "Activ";
            m_ckActiv.CheckedChanged += m_ckActiv_CheckedChanged;
            //
            // m_ckFaraContract
            //
            m_ckFaraContract.AutoSize = true;
            m_ckFaraContract.Location = new Point(1060, 62);
            m_ckFaraContract.Name = "m_ckFaraContract";
            m_ckFaraContract.Size = new Size(120, 24);
            m_ckFaraContract.TabIndex = 19;
            m_ckFaraContract.Text = "Fără contract";
            m_ckFaraContract.CheckedChanged += m_ckFaraContract_CheckedChanged;
            //
            // m_gCursanti
            // 
            m_gCursanti.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_gCursanti.Dock = DockStyle.Fill;
            m_gCursanti.Font = new Font("Arial", 8F);
            m_gCursanti.Location = new Point(0, 144);
            m_gCursanti.Margin = new Padding(3, 5, 3, 5);
            m_gCursanti.Name = "m_gCursanti";
            m_gCursanti.ReadOnly = true;
            m_gCursanti.RowHeadersWidth = 51;
            m_gCursanti.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            m_gCursanti.Size = new Size(1683, 469);
            m_gCursanti.TabIndex = 14;
            m_gCursanti.DoubleClick += m_gCursanti_DoubleClick;
            // 
            // FrmCautareCursanti
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1683, 613);
            Controls.Add(m_gCursanti);
            Controls.Add(panel1);
            Controls.Add(toolStrip1);
            Margin = new Padding(3, 5, 3, 5);
            MaximizeBox = false;
            Name = "FrmCautareCursanti";
            Text = "Cautare Cursanti";
            WindowState = FormWindowState.Maximized;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)m_gCursanti).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton m_tsbCautare;
        private System.Windows.Forms.ToolStripButton m_tsbNou;
        private System.Windows.Forms.ToolStripButton m_tsbSterge;
        private System.Windows.Forms.ToolStripButton m_tsbInscriereRapida;
        private System.Windows.Forms.ToolStripButton m_tsbDeschideArhiva;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox m_ebEmail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_ebTelMobil;
        private System.Windows.Forms.TextBox m_ebPrenume;
        private System.Windows.Forms.Label m_st2;
        private System.Windows.Forms.TextBox m_ebNume;
        private System.Windows.Forms.Label m_st1;
        private System.Windows.Forms.Label m_st3;
        private System.Windows.Forms.TextBox m_ebCNP;
        private GridPersonalizat.CustomGrid m_gCursanti;
        private ToolStripButton m_tsbListeazaBarcode;
        private CheckBox m_ckActiv;
        private CheckBox m_ckFaraContract;
    }
}
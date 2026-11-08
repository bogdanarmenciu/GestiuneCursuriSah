namespace GestiunePrezente.MeniuCursanti
{
    partial class FrmGestiuneFormator
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
            m_stbSalveaza = new ToolStripButton();
            m_ebNume = new TextBox();
            m_st1 = new Label();
            m_ebPrenume = new TextBox();
            m_ebTelMobil = new TextBox();
            m_st2 = new Label();
            label12 = new Label();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { m_stbSalveaza });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Padding = new Padding(0, 0, 2, 0);
            toolStrip1.Size = new Size(553, 27);
            toolStrip1.TabIndex = 3;
            toolStrip1.Text = "toolStrip1";
            // 
            // m_stbSalveaza
            // 
            m_stbSalveaza.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_stbSalveaza.Image = GestiunePrezente.Properties.Resources.save;
            m_stbSalveaza.ImageTransparentColor = Color.Magenta;
            m_stbSalveaza.Name = "m_stbSalveaza";
            m_stbSalveaza.Size = new Size(24, 24);
            m_stbSalveaza.Text = "Salveaza";
            m_stbSalveaza.Click += m_stbSalveaza_Click;
            // 
            // m_ebNume
            // 
            m_ebNume.BackColor = Color.FromArgb(255, 255, 192);
            m_ebNume.Location = new Point(125, 51);
            m_ebNume.Name = "m_ebNume";
            m_ebNume.Size = new Size(310, 23);
            m_ebNume.TabIndex = 21;
            m_ebNume.TextChanged += m_ebNume_TextChanged;
            // 
            // m_st1
            // 
            m_st1.AutoSize = true;
            m_st1.Location = new Point(30, 55);
            m_st1.Name = "m_st1";
            m_st1.Size = new Size(40, 15);
            m_st1.TabIndex = 24;
            m_st1.Text = "Nume";
            // 
            // m_ebPrenume
            // 
            m_ebPrenume.BackColor = Color.FromArgb(255, 255, 192);
            m_ebPrenume.Location = new Point(125, 76);
            m_ebPrenume.Name = "m_ebPrenume";
            m_ebPrenume.Size = new Size(310, 23);
            m_ebPrenume.TabIndex = 22;
            m_ebPrenume.TextChanged += m_ebPrenume_TextChanged;
            // 
            // m_ebTelMobil
            // 
            m_ebTelMobil.BackColor = Color.White;
            m_ebTelMobil.Location = new Point(125, 102);
            m_ebTelMobil.Name = "m_ebTelMobil";
            m_ebTelMobil.Size = new Size(153, 23);
            m_ebTelMobil.TabIndex = 23;
            m_ebTelMobil.TextChanged += m_ebTelMobil_TextChanged;
            // 
            // m_st2
            // 
            m_st2.AutoSize = true;
            m_st2.Location = new Point(30, 80);
            m_st2.Name = "m_st2";
            m_st2.Size = new Size(55, 15);
            m_st2.TabIndex = 25;
            m_st2.Text = "Prenume";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(30, 106);
            label12.Name = "label12";
            label12.Size = new Size(80, 15);
            label12.TabIndex = 26;
            label12.Text = "Telefon mobil";
            // 
            // FrmGestiuneFormator
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(553, 163);
            Controls.Add(m_ebNume);
            Controls.Add(m_st1);
            Controls.Add(m_ebPrenume);
            Controls.Add(m_ebTelMobil);
            Controls.Add(m_st2);
            Controls.Add(label12);
            Controls.Add(toolStrip1);
            Name = "FrmGestiuneFormator";
            Text = "Gestiune Formator";
            Load += FrmGestiuneFormator_Load;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton m_stbSalveaza;
        private System.Windows.Forms.TextBox m_ebNume;
        private System.Windows.Forms.Label m_st1;
        private System.Windows.Forms.TextBox m_ebPrenume;
        private System.Windows.Forms.TextBox m_ebTelMobil;
        private System.Windows.Forms.Label m_st2;
        private System.Windows.Forms.Label label12;
    }
}
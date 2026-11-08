namespace GestiunePrezente.MeniuConfigurare
{
    partial class FrmGestiuneDiviziune
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
            m_tsbSalvare = new ToolStripButton();
            m_ebAdresaMail = new TextBox();
            label4 = new Label();
            m_ebAdresa = new TextBox();
            label3 = new Label();
            m_ebTelefon = new TextBox();
            label2 = new Label();
            m_ebDiviziune = new TextBox();
            label1 = new Label();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { m_tsbSalvare });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Padding = new Padding(0, 0, 2, 0);
            toolStrip1.Size = new Size(805, 27);
            toolStrip1.TabIndex = 58;
            toolStrip1.Text = "toolStrip1";
            // 
            // m_tsbSalvare
            // 
            m_tsbSalvare.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbSalvare.Image = GestiunePrezente.Properties.Resources.save;
            m_tsbSalvare.ImageTransparentColor = Color.Magenta;
            m_tsbSalvare.Name = "m_tsbSalvare";
            m_tsbSalvare.Size = new Size(24, 24);
            m_tsbSalvare.Text = "Salvare";
            m_tsbSalvare.Click += m_tsbSalvare_Click;
            // 
            // m_ebAdresaMail
            // 
            m_ebAdresaMail.Location = new Point(109, 136);
            m_ebAdresaMail.Name = "m_ebAdresaMail";
            m_ebAdresaMail.Size = new Size(233, 23);
            m_ebAdresaMail.TabIndex = 64;
            m_ebAdresaMail.TextChanged += m_ebAdresaMail_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(22, 140);
            label4.Name = "label4";
            label4.Size = new Size(69, 15);
            label4.TabIndex = 65;
            label4.Text = "Adresa mail";
            // 
            // m_ebAdresa
            // 
            m_ebAdresa.Location = new Point(109, 106);
            m_ebAdresa.Name = "m_ebAdresa";
            m_ebAdresa.Size = new Size(647, 23);
            m_ebAdresa.TabIndex = 62;
            m_ebAdresa.TextChanged += m_ebAdresa_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(22, 110);
            label3.Name = "label3";
            label3.Size = new Size(43, 15);
            label3.TabIndex = 63;
            label3.Text = "Adresa";
            // 
            // m_ebTelefon
            // 
            m_ebTelefon.Location = new Point(109, 76);
            m_ebTelefon.Name = "m_ebTelefon";
            m_ebTelefon.Size = new Size(233, 23);
            m_ebTelefon.TabIndex = 60;
            m_ebTelefon.TextChanged += m_ebTelefon_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(22, 80);
            label2.Name = "label2";
            label2.Size = new Size(46, 15);
            label2.TabIndex = 61;
            label2.Text = "Telefon";
            // 
            // m_ebDiviziune
            // 
            m_ebDiviziune.Location = new Point(109, 46);
            m_ebDiviziune.Name = "m_ebDiviziune";
            m_ebDiviziune.Size = new Size(233, 23);
            m_ebDiviziune.TabIndex = 57;
            m_ebDiviziune.TextChanged += m_ebDiviziune_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 50);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 59;
            label1.Text = "Diviziune";
            // 
            // FrmGestiuneDiviziune
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(805, 178);
            Controls.Add(toolStrip1);
            Controls.Add(m_ebAdresaMail);
            Controls.Add(label4);
            Controls.Add(m_ebAdresa);
            Controls.Add(label3);
            Controls.Add(m_ebTelefon);
            Controls.Add(label2);
            Controls.Add(m_ebDiviziune);
            Controls.Add(label1);
            Name = "FrmGestiuneDiviziune";
            Text = "Gestiune Diviziune";
            FormClosing += FrmGestiuneDiviziune_FormClosing;
            Load += FrmGestiuneDiviziune_Load;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton m_tsbSalvare;
        private System.Windows.Forms.TextBox m_ebAdresaMail;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox m_ebAdresa;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox m_ebTelefon;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox m_ebDiviziune;
        private System.Windows.Forms.Label label1;
    }
}
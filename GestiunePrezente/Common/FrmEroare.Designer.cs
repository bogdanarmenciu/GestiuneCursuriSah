namespace GestiunePrezente.Common
{
    partial class FrmEroare
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            m_tbMesaj   = new TextBox();
            m_btnCopie  = new Button();
            m_btnInchide = new Button();
            m_pButtons  = new Panel();
            m_pButtons.SuspendLayout();
            SuspendLayout();

            // m_tbMesaj
            m_tbMesaj.Dock        = DockStyle.Fill;
            m_tbMesaj.Multiline   = true;
            m_tbMesaj.ReadOnly    = true;
            m_tbMesaj.ScrollBars  = ScrollBars.Vertical;
            m_tbMesaj.Font        = new Font("Consolas", 9.5F);
            m_tbMesaj.BackColor   = Color.White;
            m_tbMesaj.TabIndex    = 0;
            m_tbMesaj.Name        = "m_tbMesaj";

            // m_pButtons
            m_pButtons.Dock       = DockStyle.Bottom;
            m_pButtons.Height     = 44;
            m_pButtons.Padding    = new Padding(8, 7, 8, 7);
            m_pButtons.Name       = "m_pButtons";
            m_pButtons.TabIndex   = 1;

            // m_btnCopie
            m_btnCopie.Text       = "Copiază în clipboard";
            m_btnCopie.Width      = 160;
            m_btnCopie.Dock       = DockStyle.Left;
            m_btnCopie.TabIndex   = 0;
            m_btnCopie.Name       = "m_btnCopie";
            m_btnCopie.UseVisualStyleBackColor = true;
            m_btnCopie.Click     += new EventHandler(m_btnCopie_Click);

            // m_btnInchide
            m_btnInchide.Text     = "Închide";
            m_btnInchide.Width    = 90;
            m_btnInchide.Dock     = DockStyle.Right;
            m_btnInchide.TabIndex = 1;
            m_btnInchide.Name     = "m_btnInchide";
            m_btnInchide.UseVisualStyleBackColor = true;
            m_btnInchide.Click   += new EventHandler(m_btnInchide_Click);

            m_pButtons.Controls.Add(m_btnCopie);
            m_pButtons.Controls.Add(m_btnInchide);

            // FrmEroare
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode       = AutoScaleMode.Font;
            ClientSize          = new Size(580, 320);
            MinimumSize         = new Size(400, 200);
            Controls.Add(m_tbMesaj);
            Controls.Add(m_pButtons);
            FormBorderStyle     = FormBorderStyle.Sizable;
            MaximizeBox         = false;
            MinimizeBox         = false;
            Name                = "FrmEroare";
            StartPosition       = FormStartPosition.CenterScreen;
            Text                = "Eroare";
            AcceptButton        = m_btnInchide;

            m_pButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        private TextBox m_tbMesaj    = null!;
        private Button  m_btnCopie   = null!;
        private Button  m_btnInchide = null!;
        private Panel   m_pButtons   = null!;
    }
}

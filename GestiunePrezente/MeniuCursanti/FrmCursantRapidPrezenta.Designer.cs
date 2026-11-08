namespace GestiunePrezente.MeniuCursanti
{
    partial class FrmCursantRapidPrezenta
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
            lblNume       = new Label();
            m_ebNume      = new TextBox();
            lblPrenume    = new Label();
            m_ebPrenume   = new TextBox();
            m_btnSalveaza = new Button();
            m_btnAnuleaza = new Button();
            SuspendLayout();
            //
            // lblNume
            //
            lblNume.AutoSize = true;
            lblNume.Location = new Point(12, 22);
            lblNume.Name     = "lblNume";
            lblNume.Size     = new Size(44, 20);
            lblNume.Text     = "Nume:";
            //
            // m_ebNume
            //
            m_ebNume.CharacterCasing = CharacterCasing.Upper;
            m_ebNume.Location        = new Point(130, 19);
            m_ebNume.Name            = "m_ebNume";
            m_ebNume.Size            = new Size(240, 27);
            m_ebNume.TabIndex        = 0;
            //
            // lblPrenume
            //
            lblPrenume.AutoSize = true;
            lblPrenume.Location = new Point(12, 62);
            lblPrenume.Name     = "lblPrenume";
            lblPrenume.Size     = new Size(71, 20);
            lblPrenume.Text     = "Prenume:";
            //
            // m_ebPrenume
            //
            m_ebPrenume.CharacterCasing = CharacterCasing.Upper;
            m_ebPrenume.Location        = new Point(130, 59);
            m_ebPrenume.Name            = "m_ebPrenume";
            m_ebPrenume.Size            = new Size(240, 27);
            m_ebPrenume.TabIndex        = 1;
            //
            // m_btnSalveaza
            //
            m_btnSalveaza.Location = new Point(80, 108);
            m_btnSalveaza.Name     = "m_btnSalveaza";
            m_btnSalveaza.Size     = new Size(200, 35);
            m_btnSalveaza.TabIndex = 2;
            m_btnSalveaza.Text     = "Salvează și listează barcode";
            m_btnSalveaza.Click   += m_btnSalveaza_Click;
            //
            // m_btnAnuleaza
            //
            m_btnAnuleaza.DialogResult = DialogResult.Cancel;
            m_btnAnuleaza.Location     = new Point(290, 108);
            m_btnAnuleaza.Name         = "m_btnAnuleaza";
            m_btnAnuleaza.Size         = new Size(90, 35);
            m_btnAnuleaza.TabIndex     = 3;
            m_btnAnuleaza.Text         = "Anulează";
            m_btnAnuleaza.Click       += m_btnAnuleaza_Click;
            //
            // FrmCursantRapidPrezenta
            //
            AcceptButton          = m_btnSalveaza;
            CancelButton          = m_btnAnuleaza;
            AutoScaleDimensions   = new SizeF(8F, 20F);
            AutoScaleMode         = AutoScaleMode.Font;
            ClientSize            = new Size(396, 162);
            Controls.Add(lblNume);
            Controls.Add(m_ebNume);
            Controls.Add(lblPrenume);
            Controls.Add(m_ebPrenume);
            Controls.Add(m_btnSalveaza);
            Controls.Add(m_btnAnuleaza);
            FormBorderStyle       = FormBorderStyle.FixedDialog;
            MaximizeBox           = false;
            MinimizeBox           = false;
            Name                  = "FrmCursantRapidPrezenta";
            StartPosition         = FormStartPosition.CenterParent;
            Text                  = "Cursant rapid — prezentă";
            ResumeLayout(false);
            PerformLayout();
        }

        private Label   lblNume;
        private TextBox m_ebNume;
        private Label   lblPrenume;
        private TextBox m_ebPrenume;
        private Button  m_btnSalveaza;
        private Button  m_btnAnuleaza;
    }
}

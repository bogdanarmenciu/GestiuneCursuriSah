namespace GestiunePrezente.MeniuConfigurare
{
    partial class FrmSetariGenerale
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
            m_lblEmail  = new Label();
            m_tbEmail   = new TextBox();
            m_lblApiKey = new Label();
            m_tbApiKey  = new TextBox();
            m_lblSerie  = new Label();
            m_tbSerie   = new TextBox();
            m_btnSalvare = new Button();
            m_btnTestSB  = new Button();
            m_grpSmartBill = new GroupBox();
            m_grpSmartBill.SuspendLayout();
            SuspendLayout();

            // m_grpSmartBill
            m_grpSmartBill.Controls.Add(m_tbSerie);
            m_grpSmartBill.Controls.Add(m_lblSerie);
            m_grpSmartBill.Controls.Add(m_tbApiKey);
            m_grpSmartBill.Controls.Add(m_lblApiKey);
            m_grpSmartBill.Controls.Add(m_tbEmail);
            m_grpSmartBill.Controls.Add(m_lblEmail);
            m_grpSmartBill.Location = new Point(12, 12);
            m_grpSmartBill.Name = "m_grpSmartBill";
            m_grpSmartBill.Size = new Size(456, 136);
            m_grpSmartBill.TabIndex = 0;
            m_grpSmartBill.TabStop = false;
            m_grpSmartBill.Text = "SmartBill";

            // m_lblEmail
            m_lblEmail.AutoSize = true;
            m_lblEmail.Location = new Point(12, 30);
            m_lblEmail.Name = "m_lblEmail";
            m_lblEmail.Size = new Size(120, 15);
            m_lblEmail.TabIndex = 0;
            m_lblEmail.Text = "Email cont SmartBill:";

            // m_tbEmail
            m_tbEmail.Location = new Point(160, 27);
            m_tbEmail.Name = "m_tbEmail";
            m_tbEmail.Size = new Size(280, 23);
            m_tbEmail.TabIndex = 1;
            m_tbEmail.TextChanged += new EventHandler(m_tbEmail_TextChanged);

            // m_lblApiKey
            m_lblApiKey.AutoSize = true;
            m_lblApiKey.Location = new Point(12, 68);
            m_lblApiKey.Name = "m_lblApiKey";
            m_lblApiKey.Size = new Size(50, 15);
            m_lblApiKey.TabIndex = 2;
            m_lblApiKey.Text = "API key:";

            // m_tbApiKey
            m_tbApiKey.Location = new Point(160, 65);
            m_tbApiKey.Name = "m_tbApiKey";
            m_tbApiKey.Size = new Size(280, 23);
            m_tbApiKey.TabIndex = 3;
            m_tbApiKey.UseSystemPasswordChar = true;
            m_tbApiKey.TextChanged += new EventHandler(m_tbApiKey_TextChanged);

            // m_lblSerie
            m_lblSerie.AutoSize = true;
            m_lblSerie.Location = new Point(12, 106);
            m_lblSerie.Name = "m_lblSerie";
            m_lblSerie.Size = new Size(75, 15);
            m_lblSerie.TabIndex = 4;
            m_lblSerie.Text = "Serie factură:";

            // m_tbSerie
            m_tbSerie.Location = new Point(160, 103);
            m_tbSerie.MaxLength = 20;
            m_tbSerie.Name = "m_tbSerie";
            m_tbSerie.Size = new Size(120, 23);
            m_tbSerie.TabIndex = 5;
            m_tbSerie.TextChanged += new EventHandler(m_tbSerie_TextChanged);

            // m_btnSalvare
            m_btnSalvare.Enabled = false;
            m_btnSalvare.Location = new Point(393, 164);
            m_btnSalvare.Name = "m_btnSalvare";
            m_btnSalvare.Size = new Size(75, 28);
            m_btnSalvare.TabIndex = 7;
            m_btnSalvare.Text = "Salvează";
            m_btnSalvare.UseVisualStyleBackColor = true;
            m_btnSalvare.Click += new EventHandler(m_btnSalvare_Click);

            // m_btnTestSB
            m_btnTestSB.Location = new Point(224, 164);
            m_btnTestSB.Name = "m_btnTestSB";
            m_btnTestSB.Size = new Size(163, 28);
            m_btnTestSB.TabIndex = 6;
            m_btnTestSB.Text = "Test conexiune";
            m_btnTestSB.UseVisualStyleBackColor = true;
            m_btnTestSB.Click += new EventHandler(m_btnTestSB_Click);

            // FrmSetariGenerale
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 206);
            Controls.Add(m_btnTestSB);
            Controls.Add(m_btnSalvare);
            Controls.Add(m_grpSmartBill);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmSetariGenerale";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Setări generale";

            m_grpSmartBill.ResumeLayout(false);
            m_grpSmartBill.PerformLayout();
            ResumeLayout(false);
        }

        private Label    m_lblEmail   = null!;
        private TextBox  m_tbEmail    = null!;
        private Label    m_lblApiKey  = null!;
        private TextBox  m_tbApiKey   = null!;
        private Label    m_lblSerie   = null!;
        private TextBox  m_tbSerie    = null!;
        private Button   m_btnSalvare = null!;
        private Button   m_btnTestSB  = null!;
        private GroupBox m_grpSmartBill = null!;
    }
}

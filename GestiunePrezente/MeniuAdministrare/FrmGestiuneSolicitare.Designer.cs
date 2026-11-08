namespace GestiunePrezente.MeniuAdministrare
{
    partial class FrmGestiuneSolicitare
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            m_toolStrip = new ToolStrip();
            m_tsbSalveaza = new ToolStripButton();
            m_gbSolicitare = new GroupBox();
            m_lblSursa = new Label();
            m_cbSursa = new ComboBox();
            m_lblData = new Label();
            m_dtpData = new DateTimePicker();
            m_lblNivelSah = new Label();
            m_cbNivelSah = new ComboBox();
            m_gbCopil = new GroupBox();
            m_lblPrenumeCopil = new Label();
            m_ebPrenumeCopil = new TextBox();
            m_lblNumeCopil = new Label();
            m_ebNumeCopil = new TextBox();
            m_lblVarsta = new Label();
            m_ebVarsta = new TextBox();
            m_gbContact = new GroupBox();
            m_lblMesaj = new Label();
            m_lblNumeContact = new Label();
            m_ebNumeContact = new TextBox();
            m_lblPrenumeContact = new Label();
            m_ebPrenumeContact = new TextBox();
            m_lblEmail = new Label();
            m_ebEmail = new TextBox();
            m_lblTelefon = new Label();
            m_ebTelefon = new TextBox();
            m_ebMesaj = new TextBox();
            m_lblInboxURL = new Label();
            m_ebInboxURL = new TextBox();
            m_gbFollowUp = new GroupBox();
            m_lblStatus = new Label();
            m_cbStatus = new ComboBox();
            m_ckDataApel = new CheckBox();
            m_dtpDataApel = new DateTimePicker();
            m_lblObservatii = new Label();
            m_ebObservatii = new TextBox();
            m_btnCursant = new Button();
            m_lblDataApel = new Label();
            m_toolStrip.SuspendLayout();
            m_gbSolicitare.SuspendLayout();
            m_gbCopil.SuspendLayout();
            m_gbContact.SuspendLayout();
            m_gbFollowUp.SuspendLayout();
            SuspendLayout();
            // 
            // m_toolStrip
            // 
            m_toolStrip.ImageScalingSize = new Size(20, 20);
            m_toolStrip.Items.AddRange(new ToolStripItem[] { m_tsbSalveaza });
            m_toolStrip.Location = new Point(0, 0);
            m_toolStrip.Name = "m_toolStrip";
            m_toolStrip.Size = new Size(810, 27);
            m_toolStrip.TabIndex = 5;
            // 
            // m_tsbSalveaza
            // 
            m_tsbSalveaza.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            m_tsbSalveaza.Image = Properties.Resources.save;
            m_tsbSalveaza.Enabled = false;
            m_tsbSalveaza.Name = "m_tsbSalveaza";
            m_tsbSalveaza.Text = "Salvează";
            m_tsbSalveaza.Click += m_tsbSalveaza_Click;
            // 
            // m_gbSolicitare
            // 
            m_gbSolicitare.Controls.Add(m_lblSursa);
            m_gbSolicitare.Controls.Add(m_cbSursa);
            m_gbSolicitare.Controls.Add(m_lblData);
            m_gbSolicitare.Controls.Add(m_dtpData);
            m_gbSolicitare.Controls.Add(m_lblNivelSah);
            m_gbSolicitare.Controls.Add(m_cbNivelSah);
            m_gbSolicitare.Location = new Point(10, 32);
            m_gbSolicitare.Name = "m_gbSolicitare";
            m_gbSolicitare.Size = new Size(780, 70);
            m_gbSolicitare.TabIndex = 3;
            m_gbSolicitare.TabStop = false;
            m_gbSolicitare.Text = "Solicitare";
            // 
            // m_lblSursa
            // 
            m_lblSursa.AutoSize = true;
            m_lblSursa.Location = new Point(10, 30);
            m_lblSursa.Name = "m_lblSursa";
            m_lblSursa.Size = new Size(47, 20);
            m_lblSursa.TabIndex = 0;
            m_lblSursa.Text = "Sursă:";
            // 
            // m_cbSursa
            // 
            m_cbSursa.DropDownStyle = ComboBoxStyle.DropDownList;
            m_cbSursa.Location = new Point(80, 26);
            m_cbSursa.Name = "m_cbSursa";
            m_cbSursa.Size = new Size(140, 28);
            m_cbSursa.TabIndex = 1;
            m_cbSursa.SelectedIndexChanged += m_eb_Changed;
            // 
            // m_lblData
            // 
            m_lblData.AutoSize = true;
            m_lblData.Location = new Point(240, 30);
            m_lblData.Name = "m_lblData";
            m_lblData.Size = new Size(44, 20);
            m_lblData.TabIndex = 2;
            m_lblData.Text = "Data:";
            // 
            // m_dtpData
            // 
            m_dtpData.CustomFormat = "dd.MM.yyyy";
            m_dtpData.Format = DateTimePickerFormat.Custom;
            m_dtpData.Location = new Point(285, 26);
            m_dtpData.Name = "m_dtpData";
            m_dtpData.Size = new Size(150, 27);
            m_dtpData.TabIndex = 3;
            m_dtpData.ValueChanged += m_eb_Changed;
            // 
            // m_lblNivelSah
            // 
            m_lblNivelSah.AutoSize = true;
            m_lblNivelSah.Location = new Point(455, 30);
            m_lblNivelSah.Name = "m_lblNivelSah";
            m_lblNivelSah.Size = new Size(72, 20);
            m_lblNivelSah.TabIndex = 4;
            m_lblNivelSah.Text = "Nivel șah:";
            // 
            // m_cbNivelSah
            // 
            m_cbNivelSah.DropDownStyle = ComboBoxStyle.DropDownList;
            m_cbNivelSah.Items.AddRange(new object[] { "", "Începător", "Intermediar", "Avansat" });
            m_cbNivelSah.Location = new Point(535, 26);
            m_cbNivelSah.Name = "m_cbNivelSah";
            m_cbNivelSah.Size = new Size(150, 28);
            m_cbNivelSah.TabIndex = 5;
            m_cbNivelSah.SelectedIndexChanged += m_eb_Changed;
            // 
            // m_gbCopil
            // 
            m_gbCopil.Controls.Add(m_lblPrenumeCopil);
            m_gbCopil.Controls.Add(m_ebPrenumeCopil);
            m_gbCopil.Controls.Add(m_lblNumeCopil);
            m_gbCopil.Controls.Add(m_ebNumeCopil);
            m_gbCopil.Controls.Add(m_lblVarsta);
            m_gbCopil.Controls.Add(m_ebVarsta);
            m_gbCopil.Location = new Point(10, 112);
            m_gbCopil.Name = "m_gbCopil";
            m_gbCopil.Size = new Size(380, 100);
            m_gbCopil.TabIndex = 2;
            m_gbCopil.TabStop = false;
            m_gbCopil.Text = "Date copil";
            // 
            // m_lblPrenumeCopil
            // 
            m_lblPrenumeCopil.AutoSize = true;
            m_lblPrenumeCopil.Location = new Point(10, 30);
            m_lblPrenumeCopil.Name = "m_lblPrenumeCopil";
            m_lblPrenumeCopil.Size = new Size(70, 20);
            m_lblPrenumeCopil.TabIndex = 0;
            m_lblPrenumeCopil.Text = "Prenume:";
            // 
            // m_ebPrenumeCopil
            // 
            m_ebPrenumeCopil.Location = new Point(90, 26);
            m_ebPrenumeCopil.Name = "m_ebPrenumeCopil";
            m_ebPrenumeCopil.Size = new Size(160, 27);
            m_ebPrenumeCopil.TabIndex = 1;
            m_ebPrenumeCopil.TextChanged += m_eb_Changed;
            // 
            // m_lblNumeCopil
            // 
            m_lblNumeCopil.AutoSize = true;
            m_lblNumeCopil.Location = new Point(10, 66);
            m_lblNumeCopil.Name = "m_lblNumeCopil";
            m_lblNumeCopil.Size = new Size(52, 20);
            m_lblNumeCopil.TabIndex = 2;
            m_lblNumeCopil.Text = "Nume:";
            // 
            // m_ebNumeCopil
            // 
            m_ebNumeCopil.Location = new Point(90, 62);
            m_ebNumeCopil.Name = "m_ebNumeCopil";
            m_ebNumeCopil.Size = new Size(160, 27);
            m_ebNumeCopil.TabIndex = 3;
            m_ebNumeCopil.TextChanged += m_eb_Changed;
            // 
            // m_lblVarsta
            // 
            m_lblVarsta.AutoSize = true;
            m_lblVarsta.Location = new Point(270, 30);
            m_lblVarsta.Name = "m_lblVarsta";
            m_lblVarsta.Size = new Size(52, 20);
            m_lblVarsta.TabIndex = 4;
            m_lblVarsta.Text = "Vârstă:";
            // 
            // m_ebVarsta
            // 
            m_ebVarsta.Location = new Point(320, 26);
            m_ebVarsta.Name = "m_ebVarsta";
            m_ebVarsta.Size = new Size(45, 27);
            m_ebVarsta.TabIndex = 5;
            m_ebVarsta.TextChanged += m_eb_Changed;
            //
            // m_gbContact
            //
            m_gbContact.Controls.AddRange(new Control[] {
                m_lblPrenumeContact, m_ebPrenumeContact,
                m_lblNumeContact,    m_ebNumeContact,
                m_lblTelefon,        m_ebTelefon,
                m_lblEmail,          m_ebEmail,
                m_lblInboxURL,       m_ebInboxURL,
                m_lblMesaj,          m_ebMesaj });
            m_gbContact.Location = new Point(400, 112);
            m_gbContact.Name = "m_gbContact";
            m_gbContact.Size = new Size(390, 340);
            m_gbContact.TabIndex = 1;
            m_gbContact.TabStop = false;
            m_gbContact.Text = "Contact (părinte / persoană)";
            //
            // m_lblPrenumeContact
            //
            m_lblPrenumeContact.AutoSize = true;
            m_lblPrenumeContact.Location = new Point(10, 30);
            m_lblPrenumeContact.Name = "m_lblPrenumeContact";
            m_lblPrenumeContact.Text = "Prenume:";
            //
            // m_ebPrenumeContact
            //
            m_ebPrenumeContact.Location = new Point(95, 26);
            m_ebPrenumeContact.Name = "m_ebPrenumeContact";
            m_ebPrenumeContact.Size = new Size(200, 27);
            m_ebPrenumeContact.TabIndex = 1;
            m_ebPrenumeContact.TextChanged += m_eb_Changed;
            //
            // m_lblNumeContact
            //
            m_lblNumeContact.AutoSize = true;
            m_lblNumeContact.Location = new Point(10, 66);
            m_lblNumeContact.Name = "m_lblNumeContact";
            m_lblNumeContact.Text = "Nume:";
            //
            // m_ebNumeContact
            //
            m_ebNumeContact.Location = new Point(95, 62);
            m_ebNumeContact.Name = "m_ebNumeContact";
            m_ebNumeContact.Size = new Size(200, 27);
            m_ebNumeContact.TabIndex = 2;
            m_ebNumeContact.TextChanged += m_eb_Changed;
            //
            // m_lblTelefon
            //
            m_lblTelefon.AutoSize = true;
            m_lblTelefon.Location = new Point(10, 102);
            m_lblTelefon.Name = "m_lblTelefon";
            m_lblTelefon.Text = "Telefon:";
            //
            // m_ebTelefon
            //
            m_ebTelefon.Location = new Point(95, 98);
            m_ebTelefon.Name = "m_ebTelefon";
            m_ebTelefon.Size = new Size(160, 27);
            m_ebTelefon.TabIndex = 3;
            m_ebTelefon.TextChanged += m_eb_Changed;
            //
            // m_lblEmail
            //
            m_lblEmail.AutoSize = true;
            m_lblEmail.Location = new Point(10, 138);
            m_lblEmail.Name = "m_lblEmail";
            m_lblEmail.Text = "Email:";
            //
            // m_ebEmail
            //
            m_ebEmail.Location = new Point(95, 134);
            m_ebEmail.Name = "m_ebEmail";
            m_ebEmail.Size = new Size(270, 27);
            m_ebEmail.TabIndex = 4;
            m_ebEmail.TextChanged += m_eb_Changed;
            //
            // m_lblInboxURL
            //
            m_lblInboxURL.AutoSize = true;
            m_lblInboxURL.Location = new Point(10, 174);
            m_lblInboxURL.Name = "m_lblInboxURL";
            m_lblInboxURL.Text = "Inbox URL:";
            //
            // m_ebInboxURL
            //
            m_ebInboxURL.Location = new Point(95, 170);
            m_ebInboxURL.Name = "m_ebInboxURL";
            m_ebInboxURL.Size = new Size(270, 27);
            m_ebInboxURL.TabIndex = 5;
            m_ebInboxURL.TextChanged += m_eb_Changed;
            //
            // m_lblMesaj
            //
            m_lblMesaj.AutoSize = true;
            m_lblMesaj.Location = new Point(10, 210);
            m_lblMesaj.Name = "m_lblMesaj";
            m_lblMesaj.Text = "Mesaj:";
            //
            // m_ebMesaj
            //
            m_ebMesaj.Location = new Point(10, 230);
            m_ebMesaj.Multiline = true;
            m_ebMesaj.Name = "m_ebMesaj";
            m_ebMesaj.ScrollBars = ScrollBars.Vertical;
            m_ebMesaj.Size = new Size(360, 90);
            m_ebMesaj.TabIndex = 6;
            m_ebMesaj.TextChanged += m_eb_Changed;
            // 
            // m_gbFollowUp
            // 
            m_gbFollowUp.Controls.Add(m_lblStatus);
            m_gbFollowUp.Controls.Add(m_cbStatus);
            m_gbFollowUp.Controls.Add(m_ckDataApel);
            m_gbFollowUp.Controls.Add(m_dtpDataApel);
            m_gbFollowUp.Controls.Add(m_lblObservatii);
            m_gbFollowUp.Controls.Add(m_ebObservatii);
            m_gbFollowUp.Controls.Add(m_btnCursant);
            m_gbFollowUp.Location = new Point(10, 222);
            m_gbFollowUp.Name = "m_gbFollowUp";
            m_gbFollowUp.Size = new Size(380, 230);
            m_gbFollowUp.TabIndex = 0;
            m_gbFollowUp.TabStop = false;
            m_gbFollowUp.Text = "Follow-up";
            // 
            // m_lblStatus
            // 
            m_lblStatus.AutoSize = true;
            m_lblStatus.Location = new Point(10, 30);
            m_lblStatus.Name = "m_lblStatus";
            m_lblStatus.Size = new Size(52, 20);
            m_lblStatus.TabIndex = 0;
            m_lblStatus.Text = "Status:";
            // 
            // m_cbStatus
            // 
            m_cbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            m_cbStatus.Items.AddRange(new object[] { "Nou", "Contactat", "Interesat", "Neinteresat", "Înscris" });
            m_cbStatus.Location = new Point(90, 26);
            m_cbStatus.Name = "m_cbStatus";
            m_cbStatus.Size = new Size(160, 28);
            m_cbStatus.TabIndex = 1;
            m_cbStatus.SelectedIndexChanged += m_eb_Changed;
            // 
            // m_ckDataApel
            // 
            m_ckDataApel.AutoSize = true;
            m_ckDataApel.Location = new Point(10, 65);
            m_ckDataApel.Name = "m_ckDataApel";
            m_ckDataApel.Size = new Size(99, 24);
            m_ckDataApel.TabIndex = 2;
            m_ckDataApel.Text = "Data apel:";
            m_ckDataApel.CheckedChanged += m_ckDataApel_CheckedChanged;
            // 
            // m_dtpDataApel
            // 
            m_dtpDataApel.CustomFormat = "dd.MM.yyyy";
            m_dtpDataApel.Enabled = false;
            m_dtpDataApel.Format = DateTimePickerFormat.Custom;
            m_dtpDataApel.Location = new Point(90, 61);
            m_dtpDataApel.Name = "m_dtpDataApel";
            m_dtpDataApel.Size = new Size(150, 27);
            m_dtpDataApel.TabIndex = 3;
            m_dtpDataApel.ValueChanged += m_eb_Changed;
            // 
            // m_lblObservatii
            // 
            m_lblObservatii.AutoSize = true;
            m_lblObservatii.Location = new Point(10, 100);
            m_lblObservatii.Name = "m_lblObservatii";
            m_lblObservatii.Size = new Size(79, 20);
            m_lblObservatii.TabIndex = 4;
            m_lblObservatii.Text = "Observații:";
            // 
            // m_ebObservatii
            // 
            m_ebObservatii.Location = new Point(10, 118);
            m_ebObservatii.Multiline = true;
            m_ebObservatii.Name = "m_ebObservatii";
            m_ebObservatii.ScrollBars = ScrollBars.Vertical;
            m_ebObservatii.Size = new Size(350, 60);
            m_ebObservatii.TabIndex = 5;
            m_ebObservatii.TextChanged += m_eb_Changed;
            // 
            // m_btnCursant
            // 
            m_btnCursant.Location = new Point(10, 185);
            m_btnCursant.Name = "m_btnCursant";
            m_btnCursant.Size = new Size(180, 30);
            m_btnCursant.TabIndex = 6;
            m_btnCursant.Text = "Generează cursant";
            m_btnCursant.Click += m_btnCursant_Click;
            // 
            // m_lblDataApel
            // 
            m_lblDataApel.AutoSize = true;
            m_lblDataApel.Location = new Point(105, 65);
            m_lblDataApel.Name = "m_lblDataApel";
            m_lblDataApel.Size = new Size(100, 23);
            m_lblDataApel.TabIndex = 0;
            m_lblDataApel.Visible = false;
            // 
            // FrmGestiuneSolicitare
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(810, 470);
            Controls.Add(m_gbFollowUp);
            Controls.Add(m_gbContact);
            Controls.Add(m_gbCopil);
            Controls.Add(m_gbSolicitare);
            Controls.Add(m_toolStrip);
            Name = "FrmGestiuneSolicitare";
            Text = "Solicitare";
            FormClosing += FrmGestiuneSolicitare_FormClosing;
            Load += FrmGestiuneSolicitare_Load;
            m_toolStrip.ResumeLayout(false);
            m_toolStrip.PerformLayout();
            m_gbSolicitare.ResumeLayout(false);
            m_gbSolicitare.PerformLayout();
            m_gbCopil.ResumeLayout(false);
            m_gbCopil.PerformLayout();
            m_gbContact.ResumeLayout(false);
            m_gbContact.PerformLayout();
            m_gbFollowUp.ResumeLayout(false);
            m_gbFollowUp.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private static void SetRow(GroupBox gb, Label lbl, TextBox tb, string text, int row, int width, string name)
        {
            int y = 26 + row * 38;
            lbl.AutoSize = true; lbl.Location = new Point(10, y + 4); lbl.Text = text;
            tb.Location = new Point(90, y); tb.Size = new Size(width, 27); tb.Name = name;
            gb.Controls.AddRange(new Control[] { lbl, tb });
        }

        private ToolStrip m_toolStrip;
        private ToolStripButton m_tsbSalveaza;
        private GroupBox m_gbSolicitare, m_gbCopil, m_gbContact, m_gbFollowUp;
        private Label m_lblSursa, m_lblData, m_lblNivelSah;
        private ComboBox m_cbSursa, m_cbNivelSah;
        private DateTimePicker m_dtpData;
        private Label m_lblNumeCopil, m_lblPrenumeCopil, m_lblVarsta;
        private TextBox m_ebNumeCopil, m_ebPrenumeCopil, m_ebVarsta;
        private Label m_lblNumeContact, m_lblPrenumeContact, m_lblEmail, m_lblTelefon, m_lblMesaj, m_lblInboxURL;
        private TextBox m_ebNumeContact, m_ebPrenumeContact, m_ebEmail, m_ebTelefon, m_ebMesaj, m_ebInboxURL;
        private Label m_lblStatus, m_lblDataApel, m_lblObservatii;
        private ComboBox m_cbStatus;
        private CheckBox m_ckDataApel;
        private DateTimePicker m_dtpDataApel;
        private TextBox m_ebObservatii;
        private Button m_btnCursant;
    }
}

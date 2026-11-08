using GridPersonalizat;

namespace GestiunePrezente.MeniuCursanti
{
    partial class FrmInscriereCopil
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            toolStrip1 = new ToolStrip();
            m_tsbInscriere = new ToolStripButton();
            m_tsbListeazaBarcode = new ToolStripButton();
            panelCursant = new Panel();
            gbCursant = new GroupBox();
            lblPrenume = new Label();
            m_ebPrenume = new TextBox();
            lblNume = new Label();
            m_ebNume = new TextBox();
            lblCNP = new Label();
            m_ebCNP = new TextBox();
            lblData = new Label();
            m_dtDataNasterii = new DateTimePicker();
            lblTel = new Label();
            m_ebTelefon = new TextBox();
            lblEmail = new Label();
            m_ebEmail = new TextBox();
            lblBarcode = new Label();
            m_ebBarcode = new TextBox();
            panelTutore = new Panel();
            gbTutore = new GroupBox();
            lblPrenumeTutore = new Label();
            m_ebPrenumeTutore = new TextBox();
            lblNumeTutore = new Label();
            m_ebNumeTutore = new TextBox();
            lblTelTutore = new Label();
            m_ebTelefonTutore = new TextBox();
            lblCISerieTutore = new Label();
            m_ebCISerieTutore = new TextBox();
            lblCINrTutore = new Label();
            m_ebCINumarTutore = new TextBox();
            lblCNPTutore = new Label();
            m_ebCNPTutore = new TextBox();
            panelContract = new Panel();
            gbContract = new GroupBox();
            lblDataInceput = new Label();
            m_dtDataInceput = new DateTimePicker();
            lblAbonament = new Label();
            m_cbAbonament = new CustomCombo();
            lblPret = new Label();
            m_ebPret = new TextBox();
            lblDataSfarsit = new Label();
            m_dtDataSfarsit = new DateTimePicker();
            toolStrip1.SuspendLayout();
            panelCursant.SuspendLayout();
            gbCursant.SuspendLayout();
            panelTutore.SuspendLayout();
            gbTutore.SuspendLayout();
            panelContract.SuspendLayout();
            gbContract.SuspendLayout();
            SuspendLayout();
            //
            // toolStrip1
            //
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { m_tsbInscriere, m_tsbListeazaBarcode });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1044, 27);
            toolStrip1.TabIndex = 4;
            //
            // m_tsbInscriere
            //
            m_tsbInscriere.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbInscriere.Image = Properties.Resources.save;
            m_tsbInscriere.ImageTransparentColor = Color.Magenta;
            m_tsbInscriere.Name = "m_tsbInscriere";
            m_tsbInscriere.Size = new Size(24, 24);
            m_tsbInscriere.Text = "Inscrie și generează contract";
            m_tsbInscriere.Click += m_tsbInscriere_Click;
            //
            // m_tsbListeazaBarcode
            //
            m_tsbListeazaBarcode.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbListeazaBarcode.Image = Properties.Resources.Scanner;
            m_tsbListeazaBarcode.ImageTransparentColor = Color.Magenta;
            m_tsbListeazaBarcode.Name = "m_tsbListeazaBarcode";
            m_tsbListeazaBarcode.Size = new Size(24, 24);
            m_tsbListeazaBarcode.Text = "Listează barcode";
            m_tsbListeazaBarcode.Click += m_tsbListeazaBarcode_Click;
            //
            // panelCursant
            //
            panelCursant.Controls.Add(gbCursant);
            panelCursant.Dock = DockStyle.Top;
            panelCursant.Location = new Point(0, 0);
            panelCursant.Name = "panelCursant";
            panelCursant.Size = new Size(1044, 155);
            panelCursant.TabIndex = 0;
            // 
            // gbCursant
            // 
            gbCursant.Controls.Add(lblPrenume);
            gbCursant.Controls.Add(m_ebPrenume);
            gbCursant.Controls.Add(lblNume);
            gbCursant.Controls.Add(m_ebNume);
            gbCursant.Controls.Add(lblCNP);
            gbCursant.Controls.Add(m_ebCNP);
            gbCursant.Controls.Add(lblData);
            gbCursant.Controls.Add(m_dtDataNasterii);
            gbCursant.Controls.Add(lblTel);
            gbCursant.Controls.Add(m_ebTelefon);
            gbCursant.Controls.Add(lblEmail);
            gbCursant.Controls.Add(m_ebEmail);
            gbCursant.Controls.Add(lblBarcode);
            gbCursant.Controls.Add(m_ebBarcode);
            gbCursant.Dock = DockStyle.Fill;
            gbCursant.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            gbCursant.ForeColor = Color.FromArgb(30, 64, 124);
            gbCursant.Location = new Point(0, 0);
            gbCursant.Name = "gbCursant";
            gbCursant.Size = new Size(1044, 155);
            gbCursant.TabIndex = 0;
            gbCursant.TabStop = false;
            gbCursant.Text = "Date cursant";
            // 
            // lblPrenume
            // 
            lblPrenume.AutoSize = true;
            lblPrenume.Font = new Font("Segoe UI", 8.5F);
            lblPrenume.ForeColor = Color.FromArgb(60, 60, 60);
            lblPrenume.Location = new Point(10, 25);
            lblPrenume.Name = "lblPrenume";
            lblPrenume.Size = new Size(77, 20);
            lblPrenume.TabIndex = 0;
            lblPrenume.Text = "Prenume *";
            // 
            // m_ebPrenume
            // 
            m_ebPrenume.Font = new Font("Segoe UI", 9F);
            m_ebPrenume.Location = new Point(95, 22);
            m_ebPrenume.Name = "m_ebPrenume";
            m_ebPrenume.Size = new Size(190, 27);
            m_ebPrenume.TabIndex = 0;
            // 
            // lblNume
            // 
            lblNume.AutoSize = true;
            lblNume.Font = new Font("Segoe UI", 8.5F);
            lblNume.ForeColor = Color.FromArgb(60, 60, 60);
            lblNume.Location = new Point(300, 25);
            lblNume.Name = "lblNume";
            lblNume.Size = new Size(59, 20);
            lblNume.TabIndex = 0;
            lblNume.Text = "Nume *";
            // 
            // m_ebNume
            // 
            m_ebNume.Font = new Font("Segoe UI", 9F);
            m_ebNume.Location = new Point(369, 22);
            m_ebNume.Name = "m_ebNume";
            m_ebNume.Size = new Size(190, 27);
            m_ebNume.TabIndex = 1;
            // 
            // lblCNP
            // 
            lblCNP.AutoSize = true;
            lblCNP.Font = new Font("Segoe UI", 8.5F);
            lblCNP.ForeColor = Color.FromArgb(60, 60, 60);
            lblCNP.Location = new Point(582, 25);
            lblCNP.Name = "lblCNP";
            lblCNP.Size = new Size(47, 20);
            lblCNP.TabIndex = 0;
            lblCNP.Text = "CNP *";
            // 
            // m_ebCNP
            // 
            m_ebCNP.Font = new Font("Segoe UI", 9F);
            m_ebCNP.Location = new Point(656, 22);
            m_ebCNP.Name = "m_ebCNP";
            m_ebCNP.Size = new Size(245, 27);
            m_ebCNP.TabIndex = 2;
            // 
            // lblData
            // 
            lblData.AutoSize = true;
            lblData.Font = new Font("Segoe UI", 8.5F);
            lblData.ForeColor = Color.FromArgb(60, 60, 60);
            lblData.Location = new Point(10, 65);
            lblData.Name = "lblData";
            lblData.Size = new Size(103, 20);
            lblData.TabIndex = 0;
            lblData.Text = "Data naşterii *";
            // 
            // m_dtDataNasterii
            // 
            m_dtDataNasterii.CustomFormat = "dd.MM.yyyy";
            m_dtDataNasterii.Font = new Font("Segoe UI", 9F);
            m_dtDataNasterii.Format = DateTimePickerFormat.Custom;
            m_dtDataNasterii.Location = new Point(110, 62);
            m_dtDataNasterii.Name = "m_dtDataNasterii";
            m_dtDataNasterii.Size = new Size(175, 27);
            m_dtDataNasterii.TabIndex = 3;
            m_dtDataNasterii.ValueChanged += m_dtDataNasterii_ValueChanged;
            // 
            // lblTel
            // 
            lblTel.AutoSize = true;
            lblTel.Font = new Font("Segoe UI", 8.5F);
            lblTel.ForeColor = Color.FromArgb(60, 60, 60);
            lblTel.Location = new Point(300, 65);
            lblTel.Name = "lblTel";
            lblTel.Size = new Size(58, 20);
            lblTel.TabIndex = 0;
            lblTel.Text = "Telefon";
            // 
            // m_ebTelefon
            // 
            m_ebTelefon.Font = new Font("Segoe UI", 9F);
            m_ebTelefon.Location = new Point(369, 62);
            m_ebTelefon.Name = "m_ebTelefon";
            m_ebTelefon.Size = new Size(190, 27);
            m_ebTelefon.TabIndex = 4;
            // 
            // lblBarcode
            //
            lblBarcode.AutoSize = true;
            lblBarcode.Font = new Font("Segoe UI", 8.5F);
            lblBarcode.ForeColor = Color.FromArgb(60, 60, 60);
            lblBarcode.Location = new Point(10, 100);
            lblBarcode.Name = "lblBarcode";
            lblBarcode.Size = new Size(55, 20);
            lblBarcode.TabIndex = 0;
            lblBarcode.Text = "Barcode";
            //
            // m_ebBarcode
            //
            m_ebBarcode.BackColor = SystemColors.Control;
            m_ebBarcode.Font = new Font("Segoe UI", 9F);
            m_ebBarcode.Location = new Point(72, 97);
            m_ebBarcode.Name = "m_ebBarcode";
            m_ebBarcode.ReadOnly = true;
            m_ebBarcode.Size = new Size(310, 27);
            m_ebBarcode.TabIndex = 6;
            //
            // panelTutore
            // 
            panelTutore.Controls.Add(gbTutore);
            panelTutore.Dock = DockStyle.Top;
            panelTutore.Location = new Point(0, 155);
            panelTutore.Name = "panelTutore";
            panelTutore.Size = new Size(1044, 120);
            panelTutore.TabIndex = 1;
            panelTutore.Visible = false;
            // 
            // gbTutore
            // 
            gbTutore.Controls.Add(lblPrenumeTutore);
            gbTutore.Controls.Add(m_ebPrenumeTutore);
            gbTutore.Controls.Add(lblNumeTutore);
            gbTutore.Controls.Add(m_ebNumeTutore);
            gbTutore.Controls.Add(lblTelTutore);
            gbTutore.Controls.Add(m_ebTelefonTutore);
            gbTutore.Controls.Add(lblCISerieTutore);
            gbTutore.Controls.Add(m_ebCISerieTutore);
            gbTutore.Controls.Add(lblCINrTutore);
            gbTutore.Controls.Add(m_ebCINumarTutore);
            gbTutore.Controls.Add(lblCNPTutore);
            gbTutore.Controls.Add(m_ebCNPTutore);
            gbTutore.Dock = DockStyle.Fill;
            gbTutore.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            gbTutore.ForeColor = Color.DarkRed;
            gbTutore.Location = new Point(0, 0);
            gbTutore.Name = "gbTutore";
            gbTutore.Size = new Size(1044, 120);
            gbTutore.TabIndex = 0;
            gbTutore.TabStop = false;
            gbTutore.Text = "⚠ Cursant minor – Date tutore legal";
            // 
            // lblPrenumeTutore
            // 
            lblPrenumeTutore.AutoSize = true;
            lblPrenumeTutore.Font = new Font("Segoe UI", 8.5F);
            lblPrenumeTutore.ForeColor = Color.FromArgb(60, 60, 60);
            lblPrenumeTutore.Location = new Point(10, 25);
            lblPrenumeTutore.Name = "lblPrenumeTutore";
            lblPrenumeTutore.Size = new Size(121, 20);
            lblPrenumeTutore.TabIndex = 0;
            lblPrenumeTutore.Text = "Prenume tutore *";
            // 
            // m_ebPrenumeTutore
            // 
            m_ebPrenumeTutore.Font = new Font("Segoe UI", 9F);
            m_ebPrenumeTutore.Location = new Point(144, 22);
            m_ebPrenumeTutore.Name = "m_ebPrenumeTutore";
            m_ebPrenumeTutore.Size = new Size(180, 27);
            m_ebPrenumeTutore.TabIndex = 10;
            // 
            // lblNumeTutore
            // 
            lblNumeTutore.AutoSize = true;
            lblNumeTutore.Font = new Font("Segoe UI", 8.5F);
            lblNumeTutore.ForeColor = Color.FromArgb(60, 60, 60);
            lblNumeTutore.Location = new Point(331, 25);
            lblNumeTutore.Name = "lblNumeTutore";
            lblNumeTutore.Size = new Size(103, 20);
            lblNumeTutore.TabIndex = 0;
            lblNumeTutore.Text = "Nume tutore *";
            // 
            // m_ebNumeTutore
            // 
            m_ebNumeTutore.Font = new Font("Segoe UI", 9F);
            m_ebNumeTutore.Location = new Point(446, 22);
            m_ebNumeTutore.Name = "m_ebNumeTutore";
            m_ebNumeTutore.Size = new Size(180, 27);
            m_ebNumeTutore.TabIndex = 11;
            // 
            // lblTelTutore
            // 
            lblTelTutore.AutoSize = true;
            lblTelTutore.Font = new Font("Segoe UI", 8.5F);
            lblTelTutore.ForeColor = Color.FromArgb(60, 60, 60);
            lblTelTutore.Location = new Point(633, 25);
            lblTelTutore.Name = "lblTelTutore";
            lblTelTutore.Size = new Size(102, 20);
            lblTelTutore.TabIndex = 0;
            lblTelTutore.Text = "Telefon tutore";
            // 
            // m_ebTelefonTutore
            // 
            m_ebTelefonTutore.Font = new Font("Segoe UI", 9F);
            m_ebTelefonTutore.Location = new Point(748, 22);
            m_ebTelefonTutore.Name = "m_ebTelefonTutore";
            m_ebTelefonTutore.Size = new Size(170, 27);
            m_ebTelefonTutore.TabIndex = 12;
            // 
            // lblCISerieTutore
            // 
            lblCISerieTutore.AutoSize = true;
            lblCISerieTutore.Font = new Font("Segoe UI", 8.5F);
            lblCISerieTutore.ForeColor = Color.FromArgb(60, 60, 60);
            lblCISerieTutore.Location = new Point(10, 62);
            lblCISerieTutore.Name = "lblCISerieTutore";
            lblCISerieTutore.Size = new Size(59, 20);
            lblCISerieTutore.TabIndex = 0;
            lblCISerieTutore.Text = "CI Serie";
            // 
            // m_ebCISerieTutore
            // 
            m_ebCISerieTutore.Font = new Font("Segoe UI", 9F);
            m_ebCISerieTutore.Location = new Point(72, 59);
            m_ebCISerieTutore.Name = "m_ebCISerieTutore";
            m_ebCISerieTutore.Size = new Size(60, 27);
            m_ebCISerieTutore.TabIndex = 13;
            // 
            // lblCINrTutore
            // 
            lblCINrTutore.AutoSize = true;
            lblCINrTutore.Font = new Font("Segoe UI", 8.5F);
            lblCINrTutore.ForeColor = Color.FromArgb(60, 60, 60);
            lblCINrTutore.Location = new Point(145, 62);
            lblCINrTutore.Name = "lblCINrTutore";
            lblCINrTutore.Size = new Size(45, 20);
            lblCINrTutore.TabIndex = 0;
            lblCINrTutore.Text = "CI Nr.";
            // 
            // m_ebCINumarTutore
            // 
            m_ebCINumarTutore.Font = new Font("Segoe UI", 9F);
            m_ebCINumarTutore.Location = new Point(199, 59);
            m_ebCINumarTutore.Name = "m_ebCINumarTutore";
            m_ebCINumarTutore.Size = new Size(100, 27);
            m_ebCINumarTutore.TabIndex = 14;
            // 
            // lblCNPTutore
            // 
            lblCNPTutore.AutoSize = true;
            lblCNPTutore.Font = new Font("Segoe UI", 8.5F);
            lblCNPTutore.ForeColor = Color.FromArgb(60, 60, 60);
            lblCNPTutore.Location = new Point(310, 62);
            lblCNPTutore.Name = "lblCNPTutore";
            lblCNPTutore.Size = new Size(91, 20);
            lblCNPTutore.TabIndex = 0;
            lblCNPTutore.Text = "CNP tutore *";
            // 
            // m_ebCNPTutore
            // 
            m_ebCNPTutore.Font = new Font("Segoe UI", 9F);
            m_ebCNPTutore.Location = new Point(409, 59);
            m_ebCNPTutore.Name = "m_ebCNPTutore";
            m_ebCNPTutore.Size = new Size(180, 27);
            m_ebCNPTutore.TabIndex = 15;
            //
            // lblEmail
            //
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 8.5F);
            lblEmail.ForeColor = Color.FromArgb(60, 60, 60);
            lblEmail.Location = new Point(575, 65);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(36, 20);
            lblEmail.TabIndex = 0;
            lblEmail.Text = "Email";
            //
            // m_ebEmail
            //
            m_ebEmail.Font = new Font("Segoe UI", 9F);
            m_ebEmail.Location = new Point(620, 62);
            m_ebEmail.Name = "m_ebEmail";
            m_ebEmail.Size = new Size(290, 27);
            m_ebEmail.TabIndex = 5;
            //
            // panelContract
            // 
            panelContract.Controls.Add(gbContract);
            panelContract.Dock = DockStyle.Top;
            panelContract.Location = new Point(0, 275);
            panelContract.Name = "panelContract";
            panelContract.Size = new Size(1044, 100);
            panelContract.TabIndex = 2;
            // 
            // gbContract
            // 
            gbContract.Controls.Add(lblDataInceput);
            gbContract.Controls.Add(m_dtDataInceput);
            gbContract.Controls.Add(lblAbonament);
            gbContract.Controls.Add(m_cbAbonament);
            gbContract.Controls.Add(lblPret);
            gbContract.Controls.Add(m_ebPret);
            gbContract.Controls.Add(lblDataSfarsit);
            gbContract.Controls.Add(m_dtDataSfarsit);
            gbContract.Dock = DockStyle.Fill;
            gbContract.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            gbContract.ForeColor = Color.FromArgb(30, 64, 124);
            gbContract.Location = new Point(0, 0);
            gbContract.Name = "gbContract";
            gbContract.Size = new Size(1044, 100);
            gbContract.TabIndex = 0;
            gbContract.TabStop = false;
            gbContract.Text = "Contract";
            // 
            // lblDataInceput
            // 
            lblDataInceput.AutoSize = true;
            lblDataInceput.Font = new Font("Segoe UI", 8.5F);
            lblDataInceput.ForeColor = Color.FromArgb(60, 60, 60);
            lblDataInceput.Location = new Point(10, 35);
            lblDataInceput.Name = "lblDataInceput";
            lblDataInceput.Size = new Size(104, 20);
            lblDataInceput.TabIndex = 0;
            lblDataInceput.Text = "Data început *";
            // 
            // m_dtDataInceput
            // 
            m_dtDataInceput.CustomFormat = "dd.MM.yyyy";
            m_dtDataInceput.Font = new Font("Segoe UI", 9F);
            m_dtDataInceput.Format = DateTimePickerFormat.Custom;
            m_dtDataInceput.Location = new Point(126, 32);
            m_dtDataInceput.Name = "m_dtDataInceput";
            m_dtDataInceput.Size = new Size(175, 27);
            m_dtDataInceput.TabIndex = 20;
            m_dtDataInceput.ValueChanged += m_dtDataInceput_ValueChanged;
            // 
            // lblAbonament
            // 
            lblAbonament.AutoSize = true;
            lblAbonament.Font = new Font("Segoe UI", 8.5F);
            lblAbonament.ForeColor = Color.FromArgb(60, 60, 60);
            lblAbonament.Location = new Point(314, 35);
            lblAbonament.Name = "lblAbonament";
            lblAbonament.Size = new Size(120, 20);
            lblAbonament.TabIndex = 0;
            lblAbonament.Text = "Tip abonament *";
            // 
            // m_cbAbonament
            // 
            m_cbAbonament.AutoCompleteMode = AutoCompleteMode.Append;
            m_cbAbonament.AutoCompleteSource = AutoCompleteSource.ListItems;
            m_cbAbonament.Font = new Font("Microsoft Sans Serif", 8F);
            m_cbAbonament.FormattingEnabled = true;
            m_cbAbonament.Location = new Point(443, 32);
            m_cbAbonament.Margin = new Padding(4, 6, 4, 6);
            m_cbAbonament.Name = "m_cbAbonament";
            m_cbAbonament.Size = new Size(280, 24);
            m_cbAbonament.TabIndex = 21;
            m_cbAbonament.SelectedIndexChanged += m_cbAbonament_SelectedIndexChanged;
            // 
            // lblPret
            // 
            lblPret.AutoSize = true;
            lblPret.Font = new Font("Segoe UI", 8.5F);
            lblPret.ForeColor = Color.FromArgb(60, 60, 60);
            lblPret.Location = new Point(738, 35);
            lblPret.Name = "lblPret";
            lblPret.Size = new Size(65, 20);
            lblPret.TabIndex = 0;
            lblPret.Text = "Preț (lei)";
            // 
            // m_ebPret
            // 
            m_ebPret.BackColor = SystemColors.Info;
            m_ebPret.Font = new Font("Segoe UI", 9F);
            m_ebPret.Location = new Point(819, 32);
            m_ebPret.Name = "m_ebPret";
            m_ebPret.ReadOnly = true;
            m_ebPret.Size = new Size(100, 27);
            m_ebPret.TabIndex = 22;
            // 
            // lblDataSfarsit
            // 
            lblDataSfarsit.AutoSize = true;
            lblDataSfarsit.Font = new Font("Segoe UI", 8.5F);
            lblDataSfarsit.ForeColor = Color.FromArgb(60, 60, 60);
            lblDataSfarsit.Location = new Point(13, 67);
            lblDataSfarsit.Name = "lblDataSfarsit";
            lblDataSfarsit.Size = new Size(56, 20);
            lblDataSfarsit.TabIndex = 0;
            lblDataSfarsit.Text = "Până la";
            // 
            // m_dtDataSfarsit
            // 
            m_dtDataSfarsit.CustomFormat = "dd.MM.yyyy";
            m_dtDataSfarsit.Enabled = false;
            m_dtDataSfarsit.Font = new Font("Segoe UI", 9F);
            m_dtDataSfarsit.Format = DateTimePickerFormat.Custom;
            m_dtDataSfarsit.Location = new Point(126, 65);
            m_dtDataSfarsit.Name = "m_dtDataSfarsit";
            m_dtDataSfarsit.Size = new Size(175, 27);
            m_dtDataSfarsit.TabIndex = 23;
            //
            // FrmInscriereCopil
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1044, 393);
            Controls.Add(panelContract);
            Controls.Add(panelTutore);
            Controls.Add(panelCursant);
            Controls.Add(toolStrip1);
            Margin = new Padding(4);
            MinimumSize = new Size(920, 490);
            Name = "FrmInscriereCopil";
            Text = "Înscriere rapidă cursant";
            Load += FrmInscriereCopil_Load;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            panelCursant.ResumeLayout(false);
            gbCursant.ResumeLayout(false);
            gbCursant.PerformLayout();
            panelTutore.ResumeLayout(false);
            gbTutore.ResumeLayout(false);
            gbTutore.PerformLayout();
            panelContract.ResumeLayout(false);
            gbContract.ResumeLayout(false);
            gbContract.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelCursant;
        private System.Windows.Forms.Panel panelTutore;
        private System.Windows.Forms.Panel panelContract;
        private System.Windows.Forms.GroupBox gbCursant;
        private System.Windows.Forms.GroupBox gbTutore;
        private System.Windows.Forms.GroupBox gbContract;
        private System.Windows.Forms.Label lblPrenume;
        private System.Windows.Forms.Label lblNume;
        private System.Windows.Forms.Label lblCNP;
        private System.Windows.Forms.Label lblData;
        private System.Windows.Forms.Label lblTel;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblBarcode;
        private System.Windows.Forms.TextBox m_ebBarcode;
        private System.Windows.Forms.TextBox m_ebPrenume;
        private System.Windows.Forms.TextBox m_ebNume;
        private System.Windows.Forms.TextBox m_ebCNP;
        private System.Windows.Forms.TextBox m_ebTelefon;
        private System.Windows.Forms.TextBox m_ebEmail;
        private System.Windows.Forms.DateTimePicker m_dtDataNasterii;
        private System.Windows.Forms.Label lblPrenumeTutore;
        private System.Windows.Forms.Label lblNumeTutore;
        private System.Windows.Forms.Label lblTelTutore;
        private System.Windows.Forms.Label lblCISerieTutore;
        private System.Windows.Forms.Label lblCINrTutore;
        private System.Windows.Forms.Label lblCNPTutore;
        private System.Windows.Forms.TextBox m_ebPrenumeTutore;
        private System.Windows.Forms.TextBox m_ebNumeTutore;
        private System.Windows.Forms.TextBox m_ebTelefonTutore;
        private System.Windows.Forms.TextBox m_ebCISerieTutore;
        private System.Windows.Forms.TextBox m_ebCINumarTutore;
        private System.Windows.Forms.TextBox m_ebCNPTutore;
        private System.Windows.Forms.Label lblDataInceput;
        private System.Windows.Forms.Label lblAbonament;
        private System.Windows.Forms.Label lblPret;
        private System.Windows.Forms.Label lblDataSfarsit;
        private System.Windows.Forms.DateTimePicker m_dtDataInceput;
        private GridPersonalizat.CustomCombo m_cbAbonament;
        private System.Windows.Forms.TextBox m_ebPret;
        private System.Windows.Forms.DateTimePicker m_dtDataSfarsit;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton m_tsbInscriere;
        private System.Windows.Forms.ToolStripButton m_tsbListeazaBarcode;
    }
}

namespace GestiunePrezente.MeniuCursanti
{
    partial class FrmGestiuneFactura
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            m_labelSponsorizareADRN  = new Label();
            m_cbSponsorizareADRN     = new ComboBox();
            m_lblBalantaSponsorizare = new Label();
            m_labelNrFactura         = new Label();
            m_ebNrFactura            = new TextBox();
            m_labelDataEmitere       = new Label();
            m_dtDataEmitere          = new DateTimePicker();
            m_labelPerioadaInc       = new Label();
            m_dtPerioadaInc          = new DateTimePicker();
            m_labelPerioadaSf        = new Label();
            m_dtPerioadaSf           = new DateTimePicker();
            m_labelSuma              = new Label();
            m_ebSuma                 = new TextBox();
            m_labelTipIncasare       = new Label();
            m_cbTipIncasare          = new ComboBox();
            m_lblInfoIncasare        = new Label();
            m_lblSumaIncasare        = new Label();
            m_ebSumaIncasare         = new TextBox();
            m_labelNrDocument        = new Label();
            m_ebNrDocument           = new TextBox();
            m_labelDataIncasare      = new Label();
            m_dtDataIncasare         = new DateTimePicker();
            m_btnOK                  = new Button();
            m_btnCancel              = new Button();
            SuspendLayout();
            //
            // m_labelNrFactura
            //
            m_labelNrFactura.AutoSize = true;
            m_labelNrFactura.Location = new Point(12, 19);
            m_labelNrFactura.Text = "Nr. factură:";
            //
            // m_ebNrFactura
            //
            m_ebNrFactura.Location = new Point(200, 15);
            m_ebNrFactura.Size = new Size(200, 27);
            m_ebNrFactura.Name = "m_ebNrFactura";
            m_ebNrFactura.TabIndex = 1;
            //
            // m_labelDataEmitere
            //
            m_labelDataEmitere.AutoSize = true;
            m_labelDataEmitere.Location = new Point(12, 56);
            m_labelDataEmitere.Text = "Data emitere:";
            //
            // m_dtDataEmitere
            //
            m_dtDataEmitere.Location = new Point(200, 52);
            m_dtDataEmitere.Size = new Size(200, 27);
            m_dtDataEmitere.Format = DateTimePickerFormat.Custom;
            m_dtDataEmitere.CustomFormat = "dd.MM.yyyy";
            m_dtDataEmitere.Name = "m_dtDataEmitere";
            m_dtDataEmitere.TabIndex = 2;
            //
            // m_labelPerioadaInc
            //
            m_labelPerioadaInc.AutoSize = true;
            m_labelPerioadaInc.Location = new Point(12, 93);
            m_labelPerioadaInc.Text = "Perioadă început:";
            //
            // m_dtPerioadaInc
            //
            m_dtPerioadaInc.Location = new Point(200, 89);
            m_dtPerioadaInc.Size = new Size(200, 27);
            m_dtPerioadaInc.Format = DateTimePickerFormat.Custom;
            m_dtPerioadaInc.CustomFormat = "dd.MM.yyyy";
            m_dtPerioadaInc.Name = "m_dtPerioadaInc";
            m_dtPerioadaInc.TabIndex = 3;
            //
            // m_labelPerioadaSf
            //
            m_labelPerioadaSf.AutoSize = true;
            m_labelPerioadaSf.Location = new Point(12, 130);
            m_labelPerioadaSf.Text = "Perioadă sfârșit:";
            //
            // m_dtPerioadaSf
            //
            m_dtPerioadaSf.Location = new Point(200, 126);
            m_dtPerioadaSf.Size = new Size(200, 27);
            m_dtPerioadaSf.Format = DateTimePickerFormat.Custom;
            m_dtPerioadaSf.CustomFormat = "dd.MM.yyyy";
            m_dtPerioadaSf.Name = "m_dtPerioadaSf";
            m_dtPerioadaSf.TabIndex = 4;
            //
            // m_labelSuma
            //
            m_labelSuma.AutoSize = true;
            m_labelSuma.Location = new Point(12, 167);
            m_labelSuma.Text = "Sumă (lei):";
            //
            // m_ebSuma
            //
            m_ebSuma.Location = new Point(200, 163);
            m_ebSuma.Size = new Size(130, 27);
            m_ebSuma.Name = "m_ebSuma";
            m_ebSuma.TabIndex = 5;
            //
            // m_labelTipIncasare
            //
            m_labelTipIncasare.AutoSize = true;
            m_labelTipIncasare.Location = new Point(12, 204);
            m_labelTipIncasare.Text = "Tip încasare:";
            //
            // m_cbTipIncasare
            //
            m_cbTipIncasare.DropDownStyle = ComboBoxStyle.DropDownList;
            m_cbTipIncasare.Location = new Point(200, 200);
            m_cbTipIncasare.Size = new Size(220, 27);
            m_cbTipIncasare.Name = "m_cbTipIncasare";
            m_cbTipIncasare.TabIndex = 6;
            m_cbTipIncasare.Items.AddRange(new object[] { "Neîncasată", "OP", "Cash", "Contract sponsorizare" });
            m_cbTipIncasare.SelectedIndexChanged += m_cbTipIncasare_SelectedIndexChanged;
            //
            // m_lblInfoIncasare  (vizibil la tip 1/2 când există factură)
            //
            m_lblInfoIncasare.AutoSize = false;
            m_lblInfoIncasare.Location = new Point(200, 238);
            m_lblInfoIncasare.Size = new Size(510, 22);
            m_lblInfoIncasare.Name = "m_lblInfoIncasare";
            m_lblInfoIncasare.ForeColor = Color.Navy;
            m_lblInfoIncasare.Font = new Font("Arial", 8.5F, FontStyle.Bold);
            m_lblInfoIncasare.Text = "";
            m_lblInfoIncasare.Visible = false;
            //
            // m_lblSumaIncasare  (vizibil la tip 1/2)
            //
            m_lblSumaIncasare.AutoSize = true;
            m_lblSumaIncasare.Location = new Point(12, 275);
            m_lblSumaIncasare.Text = "Sumă de încasat (lei):";
            m_lblSumaIncasare.Visible = false;
            //
            // m_ebSumaIncasare  (vizibil la tip 1/2)
            //
            m_ebSumaIncasare.Location = new Point(200, 271);
            m_ebSumaIncasare.Size = new Size(130, 27);
            m_ebSumaIncasare.Name = "m_ebSumaIncasare";
            m_ebSumaIncasare.TabIndex = 7;
            m_ebSumaIncasare.Visible = false;
            //
            // m_labelNrDocument  (vizibil la tip 1 / 2)
            //
            m_labelNrDocument.AutoSize = true;
            m_labelNrDocument.Location = new Point(12, 318);
            m_labelNrDocument.Text = "Nr. OP / chitanță:";
            m_labelNrDocument.Visible = false;
            //
            // m_ebNrDocument  (vizibil la tip 1 / 2)
            //
            m_ebNrDocument.Location = new Point(200, 314);
            m_ebNrDocument.Size = new Size(200, 27);
            m_ebNrDocument.Name = "m_ebNrDocument";
            m_ebNrDocument.TabIndex = 8;
            m_ebNrDocument.Visible = false;
            //
            // m_labelDataIncasare  (vizibil la tip 1 / 2)
            //
            m_labelDataIncasare.AutoSize = true;
            m_labelDataIncasare.Location = new Point(12, 355);
            m_labelDataIncasare.Text = "Data încasare:";
            m_labelDataIncasare.Visible = false;
            //
            // m_dtDataIncasare  (vizibil la tip 1 / 2)
            //
            m_dtDataIncasare.Location = new Point(200, 351);
            m_dtDataIncasare.Size = new Size(200, 27);
            m_dtDataIncasare.Format = DateTimePickerFormat.Custom;
            m_dtDataIncasare.CustomFormat = "dd.MM.yyyy";
            m_dtDataIncasare.Name = "m_dtDataIncasare";
            m_dtDataIncasare.TabIndex = 9;
            m_dtDataIncasare.Visible = false;
            //
            // m_labelSponsorizareADRN  (vizibil la tip 3)
            //
            m_labelSponsorizareADRN.AutoSize = true;
            m_labelSponsorizareADRN.Location = new Point(12, 318);
            m_labelSponsorizareADRN.Text = "Contract sponsorizare:";
            m_labelSponsorizareADRN.Visible = false;
            //
            // m_cbSponsorizareADRN  (vizibil la tip 3)
            //
            m_cbSponsorizareADRN.DropDownStyle = ComboBoxStyle.DropDownList;
            m_cbSponsorizareADRN.Location = new Point(200, 314);
            m_cbSponsorizareADRN.Size = new Size(310, 27);
            m_cbSponsorizareADRN.Name = "m_cbSponsorizareADRN";
            m_cbSponsorizareADRN.TabIndex = 8;
            m_cbSponsorizareADRN.Visible = false;
            m_cbSponsorizareADRN.SelectedIndexChanged += m_cbSponsorizareADRN_SelectedIndexChanged;
            //
            // m_lblBalantaSponsorizare  (vizibil la tip 3 + ADRN selectat)
            //
            m_lblBalantaSponsorizare.AutoSize = false;
            m_lblBalantaSponsorizare.Size = new Size(600, 40);
            m_lblBalantaSponsorizare.Location = new Point(200, 351);
            m_lblBalantaSponsorizare.Name = "m_lblBalantaSponsorizare";
            m_lblBalantaSponsorizare.ForeColor = Color.Navy;
            m_lblBalantaSponsorizare.Font = new Font("Arial", 8.5F, FontStyle.Bold);
            m_lblBalantaSponsorizare.Text = "";
            m_lblBalantaSponsorizare.Visible = false;
            //
            // m_btnOK
            //
            m_btnOK.Location = new Point(200, 395);
            m_btnOK.Size = new Size(95, 30);
            m_btnOK.Text = "Salvează";
            m_btnOK.BackColor = Color.LightGreen;
            m_btnOK.Name = "m_btnOK";
            m_btnOK.TabIndex = 9;
            m_btnOK.UseVisualStyleBackColor = false;
            m_btnOK.Click += m_btnOK_Click;
            //
            // m_btnCancel
            //
            m_btnCancel.Location = new Point(305, 395);
            m_btnCancel.Size = new Size(95, 30);
            m_btnCancel.Text = "Anulează";
            m_btnCancel.Name = "m_btnCancel";
            m_btnCancel.TabIndex = 10;
            m_btnCancel.Click += m_btnCancel_Click;
            //
            // FrmGestiuneFactura
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 442);
            AcceptButton = m_btnOK;
            CancelButton = m_btnCancel;
            Controls.Add(m_labelNrFactura);
            Controls.Add(m_ebNrFactura);
            Controls.Add(m_labelDataEmitere);
            Controls.Add(m_dtDataEmitere);
            Controls.Add(m_labelPerioadaInc);
            Controls.Add(m_dtPerioadaInc);
            Controls.Add(m_labelPerioadaSf);
            Controls.Add(m_dtPerioadaSf);
            Controls.Add(m_labelSuma);
            Controls.Add(m_ebSuma);
            Controls.Add(m_labelTipIncasare);
            Controls.Add(m_cbTipIncasare);
            Controls.Add(m_lblInfoIncasare);
            Controls.Add(m_lblSumaIncasare);
            Controls.Add(m_ebSumaIncasare);
            Controls.Add(m_labelNrDocument);
            Controls.Add(m_ebNrDocument);
            Controls.Add(m_labelDataIncasare);
            Controls.Add(m_dtDataIncasare);
            Controls.Add(m_labelSponsorizareADRN);
            Controls.Add(m_cbSponsorizareADRN);
            Controls.Add(m_lblBalantaSponsorizare);
            Controls.Add(m_btnOK);
            Controls.Add(m_btnCancel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            Name = "FrmGestiuneFactura";
            Text = "Factură";
            ResumeLayout(false);
            PerformLayout();
        }

        private Label m_labelSponsorizareADRN;
        private ComboBox m_cbSponsorizareADRN;
        private Label m_lblBalantaSponsorizare;
        private Label m_lblInfoIncasare;
        private Label m_lblSumaIncasare;
        private TextBox m_ebSumaIncasare;
        private Label m_labelNrFactura;
        private TextBox m_ebNrFactura;
        private Label m_labelDataEmitere;
        private DateTimePicker m_dtDataEmitere;
        private Label m_labelPerioadaInc;
        private DateTimePicker m_dtPerioadaInc;
        private Label m_labelPerioadaSf;
        private DateTimePicker m_dtPerioadaSf;
        private Label m_labelSuma;
        private TextBox m_ebSuma;
        private Label m_labelTipIncasare;
        private ComboBox m_cbTipIncasare;
        private Label m_labelNrDocument;
        private TextBox m_ebNrDocument;
        private Label m_labelDataIncasare;
        private DateTimePicker m_dtDataIncasare;
        private Button m_btnOK;
        private Button m_btnCancel;
    }
}

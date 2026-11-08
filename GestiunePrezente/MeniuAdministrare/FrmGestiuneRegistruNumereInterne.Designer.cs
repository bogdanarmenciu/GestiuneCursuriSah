namespace GestiunePrezente.MeniuAdministrare
{
    partial class FrmGestiuneRegistruNumereInterne
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
            m_tsbSalveaza = new ToolStripButton();
            m_tsbDeschideArhiva = new ToolStripButton();
            m_dtpData = new DateTimePicker();
            m_st5 = new Label();
            m_rbIntrare = new RadioButton();
            m_rbIesire = new RadioButton();
            m_st1 = new Label();
            m_ebNumar = new TextBox();
            m_ebDescriereInregistrare = new TextBox();
            label1 = new Label();
            m_ebObservatii = new TextBox();
            label2 = new Label();
            m_ebPartener = new ComboBox();
            label8 = new Label();
            m_cbTipDocument = new ComboBox();
            m_stTipDoc = new Label();
            label41 = new Label();
            m_ebDocAtasat = new TextBox();
            m_stNrContractLeg = new Label();
            m_ebNrContractLeg = new TextBox();
            m_lblContractInfo = new Label();
            m_lblValoareSponsorizare = new Label();
            m_ebValoareSponsorizare = new TextBox();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { m_tsbSalveaza, m_tsbDeschideArhiva });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1200, 27);
            toolStrip1.TabIndex = 19;
            // 
            // m_tsbSalveaza
            // 
            m_tsbSalveaza.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbSalveaza.Image = Properties.Resources.save;
            m_tsbSalveaza.Name = "m_tsbSalveaza";
            m_tsbSalveaza.Size = new Size(29, 24);
            m_tsbSalveaza.Text = "Salveaza";
            m_tsbSalveaza.Click += m_tsbSalveaza_Click;
            // 
            // m_tsbDeschideArhiva
            // 
            m_tsbDeschideArhiva.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbDeschideArhiva.Image = Properties.Resources.CursantArhiva;
            m_tsbDeschideArhiva.Name = "m_tsbDeschideArhiva";
            m_tsbDeschideArhiva.Size = new Size(29, 24);
            m_tsbDeschideArhiva.Text = "Deschide arhiva";
            m_tsbDeschideArhiva.Click += m_tsbDeschideArhiva_Click;
            // 
            // m_dtpData
            // 
            m_dtpData.CustomFormat = "dd.MM.yyyy";
            m_dtpData.Format = DateTimePickerFormat.Custom;
            m_dtpData.Location = new Point(254, 95);
            m_dtpData.Margin = new Padding(4, 5, 4, 5);
            m_dtpData.Name = "m_dtpData";
            m_dtpData.Size = new Size(251, 27);
            m_dtpData.TabIndex = 15;
            // 
            // m_st5
            // 
            m_st5.AutoSize = true;
            m_st5.Location = new Point(59, 100);
            m_st5.Margin = new Padding(4, 0, 4, 0);
            m_st5.Name = "m_st5";
            m_st5.Size = new Size(41, 20);
            m_st5.TabIndex = 16;
            m_st5.Text = "Data";
            // 
            // m_rbIntrare
            // 
            m_rbIntrare.AutoSize = true;
            m_rbIntrare.Location = new Point(540, 63);
            m_rbIntrare.Margin = new Padding(4, 5, 4, 5);
            m_rbIntrare.Name = "m_rbIntrare";
            m_rbIntrare.Size = new Size(73, 24);
            m_rbIntrare.TabIndex = 14;
            m_rbIntrare.Text = "Intrare";
            m_rbIntrare.CheckedChanged += m_rbIntrare_CheckedChanged;
            // 
            // m_rbIesire
            // 
            m_rbIesire.AutoSize = true;
            m_rbIesire.Location = new Point(608, 63);
            m_rbIesire.Margin = new Padding(4, 5, 4, 5);
            m_rbIesire.Name = "m_rbIesire";
            m_rbIesire.Size = new Size(65, 24);
            m_rbIesire.TabIndex = 13;
            m_rbIesire.Text = "Iesire";
            m_rbIesire.CheckedChanged += m_rbIesire_CheckedChanged;
            // 
            // m_st1
            // 
            m_st1.AutoSize = true;
            m_st1.Location = new Point(59, 66);
            m_st1.Margin = new Padding(4, 0, 4, 0);
            m_st1.Name = "m_st1";
            m_st1.Size = new Size(54, 20);
            m_st1.TabIndex = 18;
            m_st1.Text = "Numar";
            // 
            // m_ebNumar
            // 
            m_ebNumar.Location = new Point(254, 62);
            m_ebNumar.Margin = new Padding(4, 5, 4, 5);
            m_ebNumar.Name = "m_ebNumar";
            m_ebNumar.Size = new Size(148, 27);
            m_ebNumar.TabIndex = 17;
            m_ebNumar.TextChanged += m_ebNumar_TextChanged;
            // 
            // m_ebDescriereInregistrare
            // 
            m_ebDescriereInregistrare.Location = new Point(254, 165);
            m_ebDescriereInregistrare.Margin = new Padding(4, 5, 4, 5);
            m_ebDescriereInregistrare.Name = "m_ebDescriereInregistrare";
            m_ebDescriereInregistrare.Size = new Size(724, 27);
            m_ebDescriereInregistrare.TabIndex = 9;
            m_ebDescriereInregistrare.TextChanged += m_ebDescriereInregistrare_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(59, 169);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(71, 20);
            label1.TabIndex = 10;
            label1.Text = "Descriere";
            // 
            // m_ebObservatii
            // 
            m_ebObservatii.Location = new Point(254, 202);
            m_ebObservatii.Margin = new Padding(4, 5, 4, 5);
            m_ebObservatii.Name = "m_ebObservatii";
            m_ebObservatii.Size = new Size(724, 27);
            m_ebObservatii.TabIndex = 7;
            m_ebObservatii.TextChanged += m_ebObservatii_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(59, 206);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(76, 20);
            label2.TabIndex = 8;
            label2.Text = "Observatii";
            // 
            // m_ebPartener
            // 
            m_ebPartener.DropDownStyle = ComboBoxStyle.DropDownList;
            m_ebPartener.Location = new Point(254, 238);
            m_ebPartener.Margin = new Padding(4, 5, 4, 5);
            m_ebPartener.Name = "m_ebPartener";
            m_ebPartener.Size = new Size(532, 28);
            m_ebPartener.TabIndex = 5;
            m_ebPartener.SelectedIndexChanged += m_ebPartener_SelectedIndexChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(59, 243);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(63, 20);
            label8.TabIndex = 6;
            label8.Text = "Sponsor";
            // 
            // m_cbTipDocument
            // 
            m_cbTipDocument.DropDownStyle = ComboBoxStyle.DropDownList;
            m_cbTipDocument.Location = new Point(254, 128);
            m_cbTipDocument.Margin = new Padding(4, 5, 4, 5);
            m_cbTipDocument.Name = "m_cbTipDocument";
            m_cbTipDocument.Size = new Size(439, 28);
            m_cbTipDocument.TabIndex = 11;
            m_cbTipDocument.SelectedIndexChanged += m_cbTipDocument_SelectedIndexChanged;
            // 
            // m_stTipDoc
            // 
            m_stTipDoc.AutoSize = true;
            m_stTipDoc.Location = new Point(59, 132);
            m_stTipDoc.Margin = new Padding(4, 0, 4, 0);
            m_stTipDoc.Name = "m_stTipDoc";
            m_stTipDoc.Size = new Size(101, 20);
            m_stTipDoc.TabIndex = 12;
            m_stTipDoc.Text = "Tip document";
            // 
            // label41
            // 
            label41.AutoSize = true;
            label41.Location = new Point(59, 318);
            label41.Margin = new Padding(4, 0, 4, 0);
            label41.Name = "label41";
            label41.Size = new Size(122, 20);
            label41.TabIndex = 0;
            label41.Text = "Document atasat";
            // 
            // m_ebDocAtasat
            // 
            m_ebDocAtasat.Location = new Point(254, 314);
            m_ebDocAtasat.Margin = new Padding(4, 5, 4, 5);
            m_ebDocAtasat.Name = "m_ebDocAtasat";
            m_ebDocAtasat.Size = new Size(724, 27);
            m_ebDocAtasat.TabIndex = 1;
            m_ebDocAtasat.KeyDown += m_ebDocAtasat_KeyDown;
            // 
            // m_stNrContractLeg
            // 
            m_stNrContractLeg.AutoSize = true;
            m_stNrContractLeg.Location = new Point(59, 280);
            m_stNrContractLeg.Margin = new Padding(4, 0, 4, 0);
            m_stNrContractLeg.Name = "m_stNrContractLeg";
            m_stNrContractLeg.Size = new Size(88, 20);
            m_stNrContractLeg.TabIndex = 4;
            m_stNrContractLeg.Text = "Nr. Contract";
            // 
            // m_ebNrContractLeg
            // 
            m_ebNrContractLeg.Location = new Point(254, 275);
            m_ebNrContractLeg.Margin = new Padding(4, 5, 4, 5);
            m_ebNrContractLeg.Name = "m_ebNrContractLeg";
            m_ebNrContractLeg.Size = new Size(105, 27);
            m_ebNrContractLeg.TabIndex = 3;
            m_ebNrContractLeg.TextChanged += m_ebNrContractLeg_TextChanged;
            m_ebNrContractLeg.Leave += m_ebNrContractLeg_Leave;
            // 
            // m_lblContractInfo
            // 
            m_lblContractInfo.AutoSize = true;
            m_lblContractInfo.ForeColor = Color.Navy;
            m_lblContractInfo.Location = new Point(280, 280);
            m_lblContractInfo.Margin = new Padding(4, 0, 4, 0);
            m_lblContractInfo.Name = "m_lblContractInfo";
            m_lblContractInfo.Size = new Size(0, 20);
            m_lblContractInfo.TabIndex = 2;
            // 
            // m_lblValoareSponsorizare
            // 
            m_lblValoareSponsorizare.AutoSize = true;
            m_lblValoareSponsorizare.Location = new Point(59, 358);
            m_lblValoareSponsorizare.Margin = new Padding(4, 0, 4, 0);
            m_lblValoareSponsorizare.Name = "m_lblValoareSponsorizare";
            m_lblValoareSponsorizare.Size = new Size(180, 20);
            m_lblValoareSponsorizare.TabIndex = 20;
            m_lblValoareSponsorizare.Text = "Valoare sponsorizare (lei):";
            // 
            // m_ebValoareSponsorizare
            // 
            m_ebValoareSponsorizare.Location = new Point(254, 354);
            m_ebValoareSponsorizare.Margin = new Padding(4, 5, 4, 5);
            m_ebValoareSponsorizare.Name = "m_ebValoareSponsorizare";
            m_ebValoareSponsorizare.PlaceholderText = "0.00";
            m_ebValoareSponsorizare.Size = new Size(148, 27);
            m_ebValoareSponsorizare.TabIndex = 21;
            m_ebValoareSponsorizare.TextChanged += m_ebValoareSponsorizare_TextChanged;
            // 
            // FrmGestiuneRegistruNumereInterne
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 420);
            Controls.Add(m_lblValoareSponsorizare);
            Controls.Add(m_ebValoareSponsorizare);
            Controls.Add(label41);
            Controls.Add(m_ebDocAtasat);
            Controls.Add(m_lblContractInfo);
            Controls.Add(m_ebNrContractLeg);
            Controls.Add(m_stNrContractLeg);
            Controls.Add(m_ebPartener);
            Controls.Add(label8);
            Controls.Add(m_ebObservatii);
            Controls.Add(label2);
            Controls.Add(m_ebDescriereInregistrare);
            Controls.Add(label1);
            Controls.Add(m_cbTipDocument);
            Controls.Add(m_stTipDoc);
            Controls.Add(m_rbIesire);
            Controls.Add(m_rbIntrare);
            Controls.Add(m_dtpData);
            Controls.Add(m_st5);
            Controls.Add(m_ebNumar);
            Controls.Add(m_st1);
            Controls.Add(toolStrip1);
            Margin = new Padding(4, 5, 4, 5);
            Name = "FrmGestiuneRegistruNumereInterne";
            Text = "Gestiune Registru Numere Interne";
            FormClosing += FrmGestiuneRegistruNumereInterne_FormClosing;
            Load += FrmGestiuneRegistruNumereInterne_Load;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton m_tsbSalveaza;
        private System.Windows.Forms.ToolStripButton m_tsbDeschideArhiva;
        private System.Windows.Forms.DateTimePicker m_dtpData;
        private System.Windows.Forms.Label m_st5;
        private System.Windows.Forms.RadioButton m_rbIntrare;
        private System.Windows.Forms.RadioButton m_rbIesire;
        private System.Windows.Forms.Label m_st1;
        private System.Windows.Forms.TextBox m_ebNumar;
        private System.Windows.Forms.TextBox m_ebDescriereInregistrare;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_ebObservatii;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox m_ebPartener;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox m_cbTipDocument;
        private System.Windows.Forms.Label m_stTipDoc;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.TextBox m_ebDocAtasat;
        private System.Windows.Forms.Label m_stNrContractLeg;
        private System.Windows.Forms.TextBox m_ebNrContractLeg;
        private System.Windows.Forms.Label m_lblContractInfo;
        private System.Windows.Forms.Label m_lblValoareSponsorizare;
        private System.Windows.Forms.TextBox m_ebValoareSponsorizare;
    }
}
namespace GestiunePrezente.MeniuAdministrare
{
    partial class FrmCautareRegistruNumereInterne
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
            m_tsbCautare = new ToolStripButton();
            m_tsbNou = new ToolStripButton();
            m_tsbStergere = new ToolStripButton();
            m_tsbDeschideArhiva = new ToolStripButton();
            m_tsbPrint = new ToolStripButton();
            panel1 = new Panel();
            groupBox3 = new GroupBox();
            label6 = new Label();
            label7 = new Label();
            m_dtpDeLaData = new DateTimePicker();
            m_ckActivareFiltruData = new CheckBox();
            m_dtpPanaLaData = new DateTimePicker();
            groupBox1 = new GroupBox();
            m_ckIesire = new CheckBox();
            m_ckIntrare = new CheckBox();
            groupBox2 = new GroupBox();
            m_ckArhivaNU = new CheckBox();
            m_ckArhivaDA = new CheckBox();
            label11 = new Label();
            m_cbDiviziune = new GridPersonalizat.CustomCombo();
            label1 = new Label();
            m_ebDescriereInregistrare = new TextBox();
            label2 = new Label();
            m_ebObservatii = new TextBox();
            label8 = new Label();
            m_ebPartener = new TextBox();
            m_stTipDoc = new Label();
            m_cbTipDocument = new ComboBox();
            m_gRegistrulNumereInterne = new GridPersonalizat.CustomGrid();
            toolStrip1.SuspendLayout();
            panel1.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_gRegistrulNumereInterne).BeginInit();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { m_tsbCautare, m_tsbNou, m_tsbStergere, m_tsbDeschideArhiva, m_tsbPrint });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1467, 27);
            toolStrip1.TabIndex = 2;
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
            m_tsbNou.Name = "m_tsbNou";
            m_tsbNou.Size = new Size(29, 24);
            m_tsbNou.Text = "Nou";
            m_tsbNou.Click += m_tsbNou_Click;
            // 
            // m_tsbStergere
            // 
            m_tsbStergere.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbStergere.Image = Properties.Resources.Delete;
            m_tsbStergere.Name = "m_tsbStergere";
            m_tsbStergere.Size = new Size(29, 24);
            m_tsbStergere.Text = "Sterge";
            m_tsbStergere.Click += m_tsbStergere_Click;
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
            // m_tsbPrint
            // 
            m_tsbPrint.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbPrint.Image = Properties.Resources.Listare;
            m_tsbPrint.Name = "m_tsbPrint";
            m_tsbPrint.Size = new Size(29, 24);
            m_tsbPrint.Text = "Generare Word";
            m_tsbPrint.Click += m_tsbPrint_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(groupBox3);
            panel1.Controls.Add(groupBox1);
            panel1.Controls.Add(groupBox2);
            panel1.Controls.Add(label11);
            panel1.Controls.Add(m_cbDiviziune);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(m_ebDescriereInregistrare);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(m_ebObservatii);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(m_ebPartener);
            panel1.Controls.Add(m_stTipDoc);
            panel1.Controls.Add(m_cbTipDocument);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 27);
            panel1.Margin = new Padding(4, 5, 4, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(1467, 246);
            panel1.TabIndex = 1;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label6);
            groupBox3.Controls.Add(label7);
            groupBox3.Controls.Add(m_dtpDeLaData);
            groupBox3.Controls.Add(m_ckActivareFiltruData);
            groupBox3.Controls.Add(m_dtpPanaLaData);
            groupBox3.Location = new Point(16, 5);
            groupBox3.Margin = new Padding(4, 5, 4, 5);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(4, 5, 4, 5);
            groupBox3.Size = new Size(604, 105);
            groupBox3.TabIndex = 0;
            groupBox3.TabStop = false;
            groupBox3.Text = "Data registru";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(191, 34);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(78, 20);
            label6.TabIndex = 0;
            label6.Text = "De la data";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(191, 66);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(90, 20);
            label7.TabIndex = 1;
            label7.Text = "Pana la data";
            // 
            // m_dtpDeLaData
            // 
            m_dtpDeLaData.Location = new Point(308, 28);
            m_dtpDeLaData.Margin = new Padding(4, 5, 4, 5);
            m_dtpDeLaData.Name = "m_dtpDeLaData";
            m_dtpDeLaData.Size = new Size(265, 27);
            m_dtpDeLaData.TabIndex = 2;
            // 
            // m_ckActivareFiltruData
            // 
            m_ckActivareFiltruData.AutoSize = true;
            m_ckActivareFiltruData.Location = new Point(20, 46);
            m_ckActivareFiltruData.Margin = new Padding(4, 5, 4, 5);
            m_ckActivareFiltruData.Name = "m_ckActivareFiltruData";
            m_ckActivareFiltruData.Size = new Size(154, 24);
            m_ckActivareFiltruData.TabIndex = 3;
            m_ckActivareFiltruData.Text = "Activare filtru data";
            // 
            // m_dtpPanaLaData
            // 
            m_dtpPanaLaData.Location = new Point(308, 62);
            m_dtpPanaLaData.Margin = new Padding(4, 5, 4, 5);
            m_dtpPanaLaData.Name = "m_dtpPanaLaData";
            m_dtpPanaLaData.Size = new Size(265, 27);
            m_dtpPanaLaData.TabIndex = 4;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(m_ckIesire);
            groupBox1.Controls.Add(m_ckIntrare);
            groupBox1.Location = new Point(16, 111);
            groupBox1.Margin = new Padding(4, 5, 4, 5);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 5, 4, 5);
            groupBox1.Size = new Size(604, 51);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Tip inregistrare";
            // 
            // m_ckIesire
            // 
            m_ckIesire.AutoSize = true;
            m_ckIesire.Location = new Point(269, 17);
            m_ckIesire.Margin = new Padding(4, 5, 4, 5);
            m_ckIesire.Name = "m_ckIesire";
            m_ckIesire.Size = new Size(66, 24);
            m_ckIesire.TabIndex = 0;
            m_ckIesire.Text = "Iesire";
            // 
            // m_ckIntrare
            // 
            m_ckIntrare.AutoSize = true;
            m_ckIntrare.Location = new Point(196, 17);
            m_ckIntrare.Margin = new Padding(4, 5, 4, 5);
            m_ckIntrare.Name = "m_ckIntrare";
            m_ckIntrare.Size = new Size(74, 24);
            m_ckIntrare.TabIndex = 1;
            m_ckIntrare.Text = "Intrare";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(m_ckArhivaNU);
            groupBox2.Controls.Add(m_ckArhivaDA);
            groupBox2.Location = new Point(16, 163);
            groupBox2.Margin = new Padding(4, 5, 4, 5);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 5, 4, 5);
            groupBox2.Size = new Size(604, 51);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Arhiva";
            // 
            // m_ckArhivaNU
            // 
            m_ckArhivaNU.AutoSize = true;
            m_ckArhivaNU.Location = new Point(269, 17);
            m_ckArhivaNU.Margin = new Padding(4, 5, 4, 5);
            m_ckArhivaNU.Name = "m_ckArhivaNU";
            m_ckArhivaNU.Size = new Size(50, 24);
            m_ckArhivaNU.TabIndex = 0;
            m_ckArhivaNU.Text = "Nu";
            // 
            // m_ckArhivaDA
            // 
            m_ckArhivaDA.AutoSize = true;
            m_ckArhivaDA.Location = new Point(196, 17);
            m_ckArhivaDA.Margin = new Padding(4, 5, 4, 5);
            m_ckArhivaDA.Name = "m_ckArhivaDA";
            m_ckArhivaDA.Size = new Size(50, 24);
            m_ckArhivaDA.TabIndex = 1;
            m_ckArhivaDA.Text = "Da";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(643, 18);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(70, 20);
            label11.TabIndex = 3;
            label11.Text = "Diviziune";
            // 
            // m_cbDiviziune
            // 
            m_cbDiviziune.AutoCompleteMode = AutoCompleteMode.Append;
            m_cbDiviziune.AutoCompleteSource = AutoCompleteSource.ListItems;
            m_cbDiviziune.Font = new Font("Microsoft Sans Serif", 8F);
            m_cbDiviziune.Location = new Point(747, 11);
            m_cbDiviziune.Margin = new Padding(4, 5, 4, 5);
            m_cbDiviziune.Name = "m_cbDiviziune";
            m_cbDiviziune.Size = new Size(292, 24);
            m_cbDiviziune.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(643, 51);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(71, 20);
            label1.TabIndex = 5;
            label1.Text = "Descriere";
            // 
            // m_ebDescriereInregistrare
            // 
            m_ebDescriereInregistrare.Location = new Point(747, 46);
            m_ebDescriereInregistrare.Margin = new Padding(4, 5, 4, 5);
            m_ebDescriereInregistrare.Name = "m_ebDescriereInregistrare";
            m_ebDescriereInregistrare.Size = new Size(692, 27);
            m_ebDescriereInregistrare.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(643, 85);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(76, 20);
            label2.TabIndex = 7;
            label2.Text = "Observatii";
            // 
            // m_ebObservatii
            // 
            m_ebObservatii.Location = new Point(747, 80);
            m_ebObservatii.Margin = new Padding(4, 5, 4, 5);
            m_ebObservatii.Name = "m_ebObservatii";
            m_ebObservatii.Size = new Size(692, 27);
            m_ebObservatii.TabIndex = 8;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(643, 122);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(63, 20);
            label8.TabIndex = 9;
            label8.Text = "Partener";
            // 
            // m_ebPartener
            // 
            m_ebPartener.BackColor = Color.Yellow;
            m_ebPartener.Location = new Point(747, 114);
            m_ebPartener.Margin = new Padding(4, 5, 4, 5);
            m_ebPartener.Name = "m_ebPartener";
            m_ebPartener.Size = new Size(692, 27);
            m_ebPartener.TabIndex = 10;
            m_ebPartener.TextChanged += m_ebPartener_TextChanged;
            m_ebPartener.KeyDown += m_ebPartener_KeyDown;
            // 
            // m_stTipDoc
            // 
            m_stTipDoc.AutoSize = true;
            m_stTipDoc.Location = new Point(1067, 18);
            m_stTipDoc.Margin = new Padding(4, 0, 4, 0);
            m_stTipDoc.Name = "m_stTipDoc";
            m_stTipDoc.Size = new Size(101, 20);
            m_stTipDoc.TabIndex = 11;
            m_stTipDoc.Text = "Tip document";
            // 
            // m_cbTipDocument
            // 
            m_cbTipDocument.DropDownStyle = ComboBoxStyle.DropDownList;
            m_cbTipDocument.Location = new Point(1173, 11);
            m_cbTipDocument.Margin = new Padding(4, 5, 4, 5);
            m_cbTipDocument.Name = "m_cbTipDocument";
            m_cbTipDocument.Size = new Size(265, 28);
            m_cbTipDocument.TabIndex = 12;
            // 
            // m_gRegistrulNumereInterne
            // 
            m_gRegistrulNumereInterne.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_gRegistrulNumereInterne.Dock = DockStyle.Fill;
            m_gRegistrulNumereInterne.Font = new Font("Arial", 8F);
            m_gRegistrulNumereInterne.Location = new Point(0, 273);
            m_gRegistrulNumereInterne.Margin = new Padding(4, 5, 4, 5);
            m_gRegistrulNumereInterne.Name = "m_gRegistrulNumereInterne";
            m_gRegistrulNumereInterne.RowHeadersWidth = 51;
            m_gRegistrulNumereInterne.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            m_gRegistrulNumereInterne.Size = new Size(1467, 496);
            m_gRegistrulNumereInterne.TabIndex = 0;
            m_gRegistrulNumereInterne.DoubleClick += m_gRegistrulNumereInterne_DoubleClick;
            // 
            // FrmCautareRegistruNumereInterne
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1467, 769);
            Controls.Add(m_gRegistrulNumereInterne);
            Controls.Add(panel1);
            Controls.Add(toolStrip1);
            Margin = new Padding(4, 5, 4, 5);
            Name = "FrmCautareRegistruNumereInterne";
            Text = "Cautare Registru Numere Interne";
            Load += FrmCautareRegistruNumereInterne_Load;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)m_gRegistrulNumereInterne).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton m_tsbCautare;
        private System.Windows.Forms.ToolStripButton m_tsbNou;
        private System.Windows.Forms.ToolStripButton m_tsbStergere;
        private System.Windows.Forms.ToolStripButton m_tsbDeschideArhiva;
        private System.Windows.Forms.ToolStripButton m_tsbPrint;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker m_dtpDeLaData;
        private System.Windows.Forms.CheckBox m_ckActivareFiltruData;
        private System.Windows.Forms.DateTimePicker m_dtpPanaLaData;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox m_ckIesire;
        private System.Windows.Forms.CheckBox m_ckIntrare;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox m_ckArhivaNU;
        private System.Windows.Forms.CheckBox m_ckArhivaDA;
        private System.Windows.Forms.Label label11;
        private GridPersonalizat.CustomCombo m_cbDiviziune;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_ebDescriereInregistrare;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox m_ebObservatii;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox m_ebPartener;
        private System.Windows.Forms.Label m_stTipDoc;
        private System.Windows.Forms.ComboBox m_cbTipDocument;
        private GridPersonalizat.CustomGrid m_gRegistrulNumereInterne;
    }
}
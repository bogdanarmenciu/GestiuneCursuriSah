using GridPersonalizat;

namespace GestiunePrezente.MeniuCursanti
{
    partial class FrmGestiunePrezenta
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
            m_tsbSalveaza = new ToolStripButton();
            m_tsbSterge = new ToolStripButton();
            m_tsbInsereazaIncasare = new ToolStripButton();
            m_tsbCautare = new ToolStripButton();
            m_tsbCursantRapid = new ToolStripButton();
            panel1 = new Panel();
            m_cbFormator = new CustomCombo();
            m_cbTipPrezenta = new CustomCombo();
            labelTipPrezenta = new Label();
            m_ebBarcode = new TextBox();
            label3 = new Label();
            m_ebNrPrezenta = new TextBox();
            m_st3 = new Label();
            label2 = new Label();
            m_ebObservatii = new TextBox();
            label10 = new Label();
            m_dtpPanaLaDataPrezenta = new DateTimePicker();
            label11 = new Label();
            m_dtpDeLaDataPrezenta = new DateTimePicker();
            label1 = new Label();
            m_gDetaliuPrezenta = new CustomGrid();
            toolStrip1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_gDetaliuPrezenta).BeginInit();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { m_tsbSalveaza, m_tsbSterge, m_tsbInsereazaIncasare, m_tsbCautare, m_tsbCursantRapid });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Padding = new Padding(0, 0, 2, 0);
            toolStrip1.Size = new Size(1066, 27);
            toolStrip1.TabIndex = 3;
            toolStrip1.Text = "toolStrip1";
            // 
            // m_tsbSalveaza
            // 
            m_tsbSalveaza.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbSalveaza.Image = Properties.Resources.save;
            m_tsbSalveaza.ImageTransparentColor = Color.Magenta;
            m_tsbSalveaza.Name = "m_tsbSalveaza";
            m_tsbSalveaza.Size = new Size(29, 24);
            m_tsbSalveaza.Text = "Salveaza";
            m_tsbSalveaza.Click += m_stbSalveaza_Click;
            // 
            // m_tsbSterge
            // 
            m_tsbSterge.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbSterge.Image = Properties.Resources.Delete;
            m_tsbSterge.ImageTransparentColor = Color.Magenta;
            m_tsbSterge.Name = "m_tsbSterge";
            m_tsbSterge.Size = new Size(29, 24);
            m_tsbSterge.Text = "Sterge";
            m_tsbSterge.Click += m_tsbSterge_Click;
            // 
            // m_tsbInsereazaIncasare
            // 
            m_tsbInsereazaIncasare.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbInsereazaIncasare.Enabled = false;
            m_tsbInsereazaIncasare.Image = Properties.Resources.CursantArhiva;
            m_tsbInsereazaIncasare.ImageTransparentColor = Color.Magenta;
            m_tsbInsereazaIncasare.Name = "m_tsbInsereazaIncasare";
            m_tsbInsereazaIncasare.Size = new Size(29, 24);
            m_tsbInsereazaIncasare.Text = "Incasare noua";
            m_tsbInsereazaIncasare.ToolTipText = "Incasare noua";
            m_tsbInsereazaIncasare.Click += m_tsbInsereazaIncasare_Click;
            // 
            // m_tsbCautare
            // 
            m_tsbCautare.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbCautare.Image = Properties.Resources.search;
            m_tsbCautare.ImageTransparentColor = Color.Magenta;
            m_tsbCautare.Name = "m_tsbCautare";
            m_tsbCautare.Size = new Size(29, 24);
            m_tsbCautare.Text = "Refresh date prezenta";
            m_tsbCautare.Click += m_tsbCautare_Click;
            //
            // m_tsbCursantRapid
            //
            m_tsbCursantRapid.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbCursantRapid.Image = Properties.Resources._new;
            m_tsbCursantRapid.ImageTransparentColor = Color.Magenta;
            m_tsbCursantRapid.Name = "m_tsbCursantRapid";
            m_tsbCursantRapid.Size = new Size(29, 24);
            m_tsbCursantRapid.Text = "Cursant rapid (fara contract)";
            m_tsbCursantRapid.ToolTipText = "Adaugă cursant rapid fără contract";
            m_tsbCursantRapid.Click += m_tsbCursantRapid_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(m_cbTipPrezenta);
            panel1.Controls.Add(labelTipPrezenta);
            panel1.Controls.Add(m_cbFormator);
            panel1.Controls.Add(m_ebBarcode);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(m_ebNrPrezenta);
            panel1.Controls.Add(m_st3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(m_ebObservatii);
            panel1.Controls.Add(label10);
            panel1.Controls.Add(m_dtpPanaLaDataPrezenta);
            panel1.Controls.Add(label11);
            panel1.Controls.Add(m_dtpDeLaDataPrezenta);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 27);
            panel1.Margin = new Padding(5, 4, 5, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(1066, 180);
            panel1.TabIndex = 4;
            // 
            // m_cbFormator
            // 
            m_cbFormator.AutoCompleteMode = AutoCompleteMode.Append;
            m_cbFormator.AutoCompleteSource = AutoCompleteSource.ListItems;
            m_cbFormator.Font = new Font("Microsoft Sans Serif", 8F);
            m_cbFormator.FormattingEnabled = true;
            m_cbFormator.Location = new Point(167, 111);
            m_cbFormator.Margin = new Padding(2, 1, 2, 1);
            m_cbFormator.Name = "m_cbFormator";
            m_cbFormator.Size = new Size(266, 24);
            m_cbFormator.TabIndex = 61;
            m_cbFormator.SelectedIndexChanged += m_cbFormator_SelectedIndexChanged;
            //
            // labelTipPrezenta
            //
            labelTipPrezenta.AutoSize = true;
            labelTipPrezenta.Location = new Point(38, 148);
            labelTipPrezenta.Margin = new Padding(5, 0, 5, 0);
            labelTipPrezenta.Name = "labelTipPrezenta";
            labelTipPrezenta.Size = new Size(82, 20);
            labelTipPrezenta.TabIndex = 62;
            labelTipPrezenta.Text = "Tip sedinta";
            //
            // m_cbTipPrezenta
            //
            m_cbTipPrezenta.AutoCompleteMode = AutoCompleteMode.Append;
            m_cbTipPrezenta.AutoCompleteSource = AutoCompleteSource.ListItems;
            m_cbTipPrezenta.Font = new Font("Microsoft Sans Serif", 8F);
            m_cbTipPrezenta.FormattingEnabled = true;
            m_cbTipPrezenta.Location = new Point(167, 144);
            m_cbTipPrezenta.Margin = new Padding(2, 1, 2, 1);
            m_cbTipPrezenta.Name = "m_cbTipPrezenta";
            m_cbTipPrezenta.Size = new Size(266, 24);
            m_cbTipPrezenta.TabIndex = 63;
            m_cbTipPrezenta.SelectedIndexChanged += m_cbTipPrezenta_SelectedIndexChanged;
            //
            // m_ebBarcode
            // 
            m_ebBarcode.BackColor = Color.Gold;
            m_ebBarcode.Location = new Point(563, 4);
            m_ebBarcode.Margin = new Padding(3, 4, 3, 4);
            m_ebBarcode.Name = "m_ebBarcode";
            m_ebBarcode.Size = new Size(474, 27);
            m_ebBarcode.TabIndex = 60;
            m_ebBarcode.KeyDown += m_ebBarcode_KeyDown;
            m_ebBarcode.KeyPress += m_ebBarcode_KeyPress;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(486, 12);
            label3.Margin = new Padding(5, 0, 5, 0);
            label3.Name = "label3";
            label3.Size = new Size(64, 20);
            label3.TabIndex = 59;
            label3.Text = "Barcode";
            // 
            // m_ebNrPrezenta
            // 
            m_ebNrPrezenta.Enabled = false;
            m_ebNrPrezenta.Location = new Point(167, 7);
            m_ebNrPrezenta.Margin = new Padding(5, 4, 5, 4);
            m_ebNrPrezenta.Name = "m_ebNrPrezenta";
            m_ebNrPrezenta.Size = new Size(163, 27);
            m_ebNrPrezenta.TabIndex = 57;
            m_ebNrPrezenta.TextAlign = HorizontalAlignment.Right;
            // 
            // m_st3
            // 
            m_st3.AutoSize = true;
            m_st3.Location = new Point(38, 12);
            m_st3.Margin = new Padding(5, 0, 5, 0);
            m_st3.Name = "m_st3";
            m_st3.Size = new Size(116, 20);
            m_st3.TabIndex = 58;
            m_st3.Text = "Numar prezenta";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(38, 111);
            label2.Margin = new Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new Size(70, 20);
            label2.TabIndex = 56;
            label2.Text = "Formator";
            // 
            // m_ebObservatii
            // 
            m_ebObservatii.Location = new Point(563, 40);
            m_ebObservatii.Margin = new Padding(5, 4, 5, 4);
            m_ebObservatii.Multiline = true;
            m_ebObservatii.Name = "m_ebObservatii";
            m_ebObservatii.Size = new Size(474, 103);
            m_ebObservatii.TabIndex = 53;
            m_ebObservatii.TextChanged += m_ebObservatii_TextChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(486, 51);
            label10.Margin = new Padding(5, 0, 5, 0);
            label10.Name = "label10";
            label10.Size = new Size(76, 20);
            label10.TabIndex = 54;
            label10.Text = "Observatii";
            // 
            // m_dtpPanaLaDataPrezenta
            // 
            m_dtpPanaLaDataPrezenta.CustomFormat = "dd.MM.yyyy HH:mm";
            m_dtpPanaLaDataPrezenta.Format = DateTimePickerFormat.Custom;
            m_dtpPanaLaDataPrezenta.Location = new Point(167, 72);
            m_dtpPanaLaDataPrezenta.Margin = new Padding(5, 4, 5, 4);
            m_dtpPanaLaDataPrezenta.Name = "m_dtpPanaLaDataPrezenta";
            m_dtpPanaLaDataPrezenta.Size = new Size(163, 27);
            m_dtpPanaLaDataPrezenta.TabIndex = 50;
            m_dtpPanaLaDataPrezenta.ValueChanged += m_dtpPanaLaDataPrezenta_ValueChanged;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(38, 77);
            label11.Margin = new Padding(5, 0, 5, 0);
            label11.Name = "label11";
            label11.Size = new Size(90, 20);
            label11.TabIndex = 52;
            label11.Text = "Pana la data";
            // 
            // m_dtpDeLaDataPrezenta
            // 
            m_dtpDeLaDataPrezenta.CustomFormat = "dd.MM.yyyy HH:mm";
            m_dtpDeLaDataPrezenta.Format = DateTimePickerFormat.Custom;
            m_dtpDeLaDataPrezenta.Location = new Point(167, 40);
            m_dtpDeLaDataPrezenta.Margin = new Padding(5, 4, 5, 4);
            m_dtpDeLaDataPrezenta.Name = "m_dtpDeLaDataPrezenta";
            m_dtpDeLaDataPrezenta.Size = new Size(163, 27);
            m_dtpDeLaDataPrezenta.TabIndex = 49;
            m_dtpDeLaDataPrezenta.ValueChanged += m_dtpDeLaDataPrezenta_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(38, 44);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(78, 20);
            label1.TabIndex = 51;
            label1.Text = "De la data";
            // 
            // m_gDetaliuPrezenta
            // 
            m_gDetaliuPrezenta.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_gDetaliuPrezenta.Dock = DockStyle.Fill;
            m_gDetaliuPrezenta.Font = new Font("Arial", 8F);
            m_gDetaliuPrezenta.Location = new Point(0, 178);
            m_gDetaliuPrezenta.Margin = new Padding(2, 1, 2, 1);
            m_gDetaliuPrezenta.Name = "m_gDetaliuPrezenta";
            m_gDetaliuPrezenta.ReadOnly = true;
            m_gDetaliuPrezenta.RowHeadersWidth = 82;
            m_gDetaliuPrezenta.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            m_gDetaliuPrezenta.Size = new Size(1066, 485);
            m_gDetaliuPrezenta.TabIndex = 5;
            m_gDetaliuPrezenta.DoubleClick += m_gDetaliuPrezenta_DoubleClick;
            // 
            // FrmGestiunePrezenta
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1066, 663);
            Controls.Add(m_gDetaliuPrezenta);
            Controls.Add(panel1);
            Controls.Add(toolStrip1);
            Margin = new Padding(5, 4, 5, 4);
            Name = "FrmGestiunePrezenta";
            Text = "Gestiune Prezenta";
            WindowState = FormWindowState.Maximized;
            FormClosing += FrmGestiunePrezenta_FormClosing;
            Load += FrmGestiunePrezenta_Load;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)m_gDetaliuPrezenta).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton m_tsbSalveaza;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox m_ebObservatii;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker m_dtpPanaLaDataPrezenta;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker m_dtpDeLaDataPrezenta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton m_tsbSterge;
        private System.Windows.Forms.Label label2;
        private CustomCombo m_cbFormator;
        private System.Windows.Forms.TextBox m_ebNrPrezenta;
        private System.Windows.Forms.Label m_st3;
        private System.Windows.Forms.ToolStripButton m_tsbInsereazaIncasare;
        private System.Windows.Forms.ToolStripButton m_tsbCautare;
        private TextBox m_ebBarcode;
        private Label label3;
        private CustomGrid m_gDetaliuPrezenta;
        private CustomCombo m_cbTipPrezenta;
        private Label labelTipPrezenta;
        private System.Windows.Forms.ToolStripButton m_tsbCursantRapid;
    }
}
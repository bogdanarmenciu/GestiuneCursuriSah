namespace GestiunePrezente.MeniuCursanti
{
    partial class FrmModificaNumarPrezenteRetroactiv
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
            m_ebNrPrezente = new TextBox();
            m_st3 = new Label();
            m_dtpDataExpirare = new DateTimePicker();
            label1 = new Label();
            m_dtpDataExpirareAbCurent = new DateTimePicker();
            label2 = new Label();
            m_dtpDataInceputAbCurent = new DateTimePicker();
            label3 = new Label();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { m_tsbSalveaza });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Padding = new Padding(0, 0, 2, 0);
            toolStrip1.Size = new Size(394, 27);
            toolStrip1.TabIndex = 4;
            toolStrip1.Text = "toolStrip1";
            // 
            // m_tsbSalveaza
            // 
            m_tsbSalveaza.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbSalveaza.Image = GestiunePrezente.Properties.Resources.save;
            m_tsbSalveaza.ImageTransparentColor = Color.Magenta;
            m_tsbSalveaza.Name = "m_tsbSalveaza";
            m_tsbSalveaza.Size = new Size(24, 24);
            m_tsbSalveaza.Text = "Salveaza";
            m_tsbSalveaza.Click += m_tsbSalveaza_Click;
            // 
            // m_ebNrPrezente
            // 
            m_ebNrPrezente.Location = new Point(218, 61);
            m_ebNrPrezente.Name = "m_ebNrPrezente";
            m_ebNrPrezente.Size = new Size(143, 23);
            m_ebNrPrezente.TabIndex = 59;
            m_ebNrPrezente.TextAlign = HorizontalAlignment.Right;
            m_ebNrPrezente.TextChanged += m_ebNrPrezente_TextChanged;
            // 
            // m_st3
            // 
            m_st3.AutoSize = true;
            m_st3.Location = new Point(23, 66);
            m_st3.Name = "m_st3";
            m_st3.Size = new Size(144, 15);
            m_st3.TabIndex = 60;
            m_st3.Text = "Numar prezente efectuate";
            // 
            // m_dtpDataExpirare
            // 
            m_dtpDataExpirare.CustomFormat = "dd.MM.yyyy HH:mm";
            m_dtpDataExpirare.Format = DateTimePickerFormat.Custom;
            m_dtpDataExpirare.Location = new Point(218, 91);
            m_dtpDataExpirare.Name = "m_dtpDataExpirare";
            m_dtpDataExpirare.Size = new Size(143, 23);
            m_dtpDataExpirare.TabIndex = 61;
            m_dtpDataExpirare.Value = new DateTime(2022, 11, 8, 0, 0, 0, 0);
            m_dtpDataExpirare.ValueChanged += m_dtpDataExpirare_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(23, 95);
            label1.Name = "label1";
            label1.Size = new Size(151, 15);
            label1.TabIndex = 62;
            label1.Text = "Data expirare prezente retro";
            // 
            // m_dtpDataExpirareAbCurent
            // 
            m_dtpDataExpirareAbCurent.CustomFormat = "dd.MM.yyyy HH:mm";
            m_dtpDataExpirareAbCurent.Format = DateTimePickerFormat.Custom;
            m_dtpDataExpirareAbCurent.Location = new Point(218, 151);
            m_dtpDataExpirareAbCurent.Name = "m_dtpDataExpirareAbCurent";
            m_dtpDataExpirareAbCurent.Size = new Size(143, 23);
            m_dtpDataExpirareAbCurent.TabIndex = 63;
            m_dtpDataExpirareAbCurent.Value = new DateTime(2022, 11, 8, 0, 0, 0, 0);
            m_dtpDataExpirareAbCurent.ValueChanged += m_dtpDataExpirareAbCurent_ValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(23, 155);
            label2.Name = "label2";
            label2.Size = new Size(176, 15);
            label2.TabIndex = 64;
            label2.Text = "Data expirare abonament curent";
            // 
            // m_dtpDataInceputAbCurent
            // 
            m_dtpDataInceputAbCurent.CustomFormat = "dd.MM.yyyy HH:mm";
            m_dtpDataInceputAbCurent.Format = DateTimePickerFormat.Custom;
            m_dtpDataInceputAbCurent.Location = new Point(218, 121);
            m_dtpDataInceputAbCurent.Name = "m_dtpDataInceputAbCurent";
            m_dtpDataInceputAbCurent.Size = new Size(143, 23);
            m_dtpDataInceputAbCurent.TabIndex = 65;
            m_dtpDataInceputAbCurent.Value = new DateTime(2022, 11, 8, 0, 0, 0, 0);
            m_dtpDataInceputAbCurent.ValueChanged += m_dtpDataInceputAbCurent_ValueChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(23, 125);
            label3.Name = "label3";
            label3.Size = new Size(175, 15);
            label3.TabIndex = 66;
            label3.Text = "Data inceput abonament curent";
            // 
            // FrmModificaNumarPrezenteRetroactiv
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(394, 207);
            Controls.Add(m_dtpDataInceputAbCurent);
            Controls.Add(label3);
            Controls.Add(m_dtpDataExpirareAbCurent);
            Controls.Add(label2);
            Controls.Add(m_dtpDataExpirare);
            Controls.Add(label1);
            Controls.Add(m_ebNrPrezente);
            Controls.Add(m_st3);
            Controls.Add(toolStrip1);
            Name = "FrmModificaNumarPrezenteRetroactiv";
            Text = "Modifica Numar Prezente Retroactiv";
            Load += FrmModificaNumarPrezenteRetroactiv_Load;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton m_tsbSalveaza;
        private System.Windows.Forms.TextBox m_ebNrPrezente;
        private System.Windows.Forms.Label m_st3;
        private System.Windows.Forms.DateTimePicker m_dtpDataExpirare;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker m_dtpDataExpirareAbCurent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker m_dtpDataInceputAbCurent;
        private System.Windows.Forms.Label label3;
    }
}
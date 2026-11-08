using GridPersonalizat;

namespace GestiunePrezente.MeniuCursanti
{
    partial class FrmGestiuneIncasariAbonament
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            m_toolStrip              = new ToolStrip();
            m_tsbSalveaza            = new ToolStripButton();
            m_tsbListare             = new ToolStripButton();
            m_panelCampuri           = new Panel();
            m_labelNrIncasare        = new Label();
            m_ebNrIncasare           = new TextBox();
            m_labelData              = new Label();
            m_dtpData                = new DateTimePicker();
            m_labelLegatura          = new Label();
            m_panelLegaturaGroup     = new Panel();
            m_rbFaraLegatura         = new RadioButton();
            m_rbLegaturaFactura      = new RadioButton();
            m_rbLegaturaSponsorizare = new RadioButton();
            m_rbLegaturaDirecta      = new RadioButton();
            m_cbFactura              = new CustomCombo();
            m_cbADRN                 = new CustomCombo();
            m_dgCursanti             = new DataGridView();
            m_lblInfoLegatura        = new Label();
            m_labelTipPlata          = new Label();
            m_panelTipPlataGroup     = new Panel();
            m_rbCash                 = new RadioButton();
            m_rbOP                   = new RadioButton();
            m_labelNrDocument        = new Label();
            m_ebNrDocument           = new TextBox();
            m_labelSuma              = new Label();
            m_ebSuma                 = new TextBox();
            m_labelReprezentand      = new Label();
            m_ebReprezentand         = new TextBox();
            m_panelTitluGrid         = new Panel();
            m_lblTitluGrid           = new Label();
            m_gIstoric               = new CustomGrid();

            m_toolStrip.SuspendLayout();
            m_panelCampuri.SuspendLayout();
            m_panelLegaturaGroup.SuspendLayout();
            m_panelTipPlataGroup.SuspendLayout();
            m_panelTitluGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_dgCursanti).BeginInit();
            ((System.ComponentModel.ISupportInitialize)m_gIstoric).BeginInit();
            SuspendLayout();

            // ── ToolStrip ────────────────────────────────────────────────────
            m_toolStrip.ImageScalingSize = new Size(20, 20);
            m_toolStrip.Items.AddRange(new ToolStripItem[] { m_tsbSalveaza, m_tsbListare });
            m_toolStrip.Location = new Point(0, 0);
            m_toolStrip.Size = new Size(1160, 27);
            m_toolStrip.TabIndex = 0;

            m_tsbSalveaza.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbSalveaza.Image = GestiunePrezente.Properties.Resources.save;
            m_tsbSalveaza.Name = "m_tsbSalveaza";
            m_tsbSalveaza.Text = "Salvează";
            m_tsbSalveaza.Click += m_tsbSalveaza_Click;

            m_tsbListare.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbListare.Image = GestiunePrezente.Properties.Resources.Listare;
            m_tsbListare.Name = "m_tsbListare";
            m_tsbListare.Text = "Listare chitanță";
            m_tsbListare.Click += m_tsbListare_Click;

            // ── Panel câmpuri (dock top, 440px) ──────────────────────────────
            m_panelCampuri.Dock = DockStyle.Top;
            m_panelCampuri.Location = new Point(0, 27);
            m_panelCampuri.Size = new Size(1160, 440);
            m_panelCampuri.TabIndex = 1;

            // Rând 1 — Nr. încasare (y=15)
            m_labelNrIncasare.AutoSize = true;
            m_labelNrIncasare.Location = new Point(12, 15);
            m_labelNrIncasare.Text = "Nr. încasare:";

            m_ebNrIncasare.Location = new Point(200, 11);
            m_ebNrIncasare.Size = new Size(100, 27);
            m_ebNrIncasare.ReadOnly = true;
            m_ebNrIncasare.BackColor = Color.WhiteSmoke;
            m_ebNrIncasare.TabStop = false;

            // Rând 2 — Data și ora (y=52)
            m_labelData.AutoSize = true;
            m_labelData.Location = new Point(12, 52);
            m_labelData.Text = "Data și ora:";

            m_dtpData.Location = new Point(200, 48);
            m_dtpData.Size = new Size(220, 27);
            m_dtpData.Format = DateTimePickerFormat.Custom;
            m_dtpData.CustomFormat = "dd.MM.yyyy HH:mm";
            m_dtpData.TabIndex = 1;

            // Rând 3 — Leagă de (sub-panel izolat, y=89)
            m_labelLegatura.AutoSize = true;
            m_labelLegatura.Location = new Point(12, 92);
            m_labelLegatura.Text = "Leagă de:";
            m_labelLegatura.Font = new Font("Arial", 9F, FontStyle.Bold);

            m_panelLegaturaGroup.Location = new Point(200, 86);
            m_panelLegaturaGroup.Size = new Size(940, 27);
            m_panelLegaturaGroup.TabIndex = 2;

            m_rbFaraLegatura.AutoSize = true;
            m_rbFaraLegatura.Location = new Point(0, 3);
            m_rbFaraLegatura.Text = "Fără legătură";
            m_rbFaraLegatura.Checked = true;
            m_rbFaraLegatura.TabIndex = 0;
            m_rbFaraLegatura.CheckedChanged += m_rbFaraLegatura_CheckedChanged;

            m_rbLegaturaFactura.AutoSize = true;
            m_rbLegaturaFactura.Location = new Point(130, 3);
            m_rbLegaturaFactura.Text = "Factură";
            m_rbLegaturaFactura.TabIndex = 1;
            m_rbLegaturaFactura.CheckedChanged += m_rbLegaturaFactura_CheckedChanged;

            m_rbLegaturaSponsorizare.AutoSize = true;
            m_rbLegaturaSponsorizare.Location = new Point(230, 3);
            m_rbLegaturaSponsorizare.Text = "Contract sponsorizare";
            m_rbLegaturaSponsorizare.TabIndex = 2;
            m_rbLegaturaSponsorizare.CheckedChanged += m_rbLegaturaSponsorizare_CheckedChanged;

            m_rbLegaturaDirecta.AutoSize = true;
            m_rbLegaturaDirecta.Location = new Point(430, 3);
            m_rbLegaturaDirecta.Text = "Cursant (fără contract)";
            m_rbLegaturaDirecta.TabIndex = 3;
            m_rbLegaturaDirecta.CheckedChanged += m_rbLegaturaDirecta_CheckedChanged;

            m_panelLegaturaGroup.Controls.Add(m_rbFaraLegatura);
            m_panelLegaturaGroup.Controls.Add(m_rbLegaturaFactura);
            m_panelLegaturaGroup.Controls.Add(m_rbLegaturaSponsorizare);
            m_panelLegaturaGroup.Controls.Add(m_rbLegaturaDirecta);

            // Rând 4 — Combo factură / ADRN (y=122, h=27, vizibil condiționat)
            m_cbFactura.AutoCompleteMode = AutoCompleteMode.Append;
            m_cbFactura.AutoCompleteSource = AutoCompleteSource.ListItems;
            m_cbFactura.FormattingEnabled = true;
            m_cbFactura.Location = new Point(200, 122);
            m_cbFactura.Size = new Size(700, 27);
            m_cbFactura.TabIndex = 3;
            m_cbFactura.Visible = false;
            m_cbFactura.SelectedIndexChanged += m_cbFactura_SelectedIndexChanged;

            m_cbADRN.AutoCompleteMode = AutoCompleteMode.Append;
            m_cbADRN.AutoCompleteSource = AutoCompleteSource.ListItems;
            m_cbADRN.FormattingEnabled = true;
            m_cbADRN.Location = new Point(200, 122);
            m_cbADRN.Size = new Size(700, 27);
            m_cbADRN.TabIndex = 4;
            m_cbADRN.Visible = false;
            m_cbADRN.SelectedIndexChanged += m_cbADRN_SelectedIndexChanged;

            // Grid cursanți fără contract (y=122, h=130, vizibil doar pt. "Cursant fără contract")
            var colCk = new DataGridViewCheckBoxColumn();
            colCk.Name = "Selectat";
            colCk.HeaderText = "✓";
            colCk.Width = 28;
            colCk.ReadOnly = false;

            var colId = new DataGridViewTextBoxColumn();
            colId.Name = "CUCU_Id";
            colId.Visible = false;

            var colNume = new DataGridViewTextBoxColumn();
            colNume.Name = "Nume";
            colNume.HeaderText = "Cursant (fără contract activ)";
            colNume.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colNume.ReadOnly = true;

            m_dgCursanti.Columns.AddRange(colCk, colId, colNume);
            m_dgCursanti.Location = new Point(200, 122);
            m_dgCursanti.Size = new Size(600, 130);
            m_dgCursanti.TabIndex = 5;
            m_dgCursanti.Visible = false;
            m_dgCursanti.AllowUserToAddRows = false;
            m_dgCursanti.AllowUserToDeleteRows = false;
            m_dgCursanti.RowHeadersVisible = false;
            m_dgCursanti.AllowUserToResizeRows = false;
            m_dgCursanti.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            m_dgCursanti.ReadOnly = false;
            m_dgCursanti.Font = new Font("Arial", 9F);
            m_dgCursanti.CellValueChanged += m_dgCursanti_CellValueChanged;
            m_dgCursanti.CurrentCellDirtyStateChanged += m_dgCursanti_CurrentCellDirtyStateChanged;

            // Rând 5 — Info legătură (y=258)
            m_lblInfoLegatura.AutoSize = true;
            m_lblInfoLegatura.Location = new Point(200, 258);
            m_lblInfoLegatura.ForeColor = Color.Navy;
            m_lblInfoLegatura.Font = new Font("Arial", 8.5F, FontStyle.Bold);
            m_lblInfoLegatura.Text = "";

            // Rând 6 — Tip plată (sub-panel izolat, y=290)
            m_labelTipPlata.AutoSize = true;
            m_labelTipPlata.Location = new Point(12, 296);
            m_labelTipPlata.Text = "Tip plată:";

            m_panelTipPlataGroup.Location = new Point(200, 290);
            m_panelTipPlataGroup.Size = new Size(400, 27);
            m_panelTipPlataGroup.TabIndex = 6;

            m_rbCash.AutoSize = true;
            m_rbCash.Location = new Point(0, 3);
            m_rbCash.Text = "Cash";
            m_rbCash.Checked = true;
            m_rbCash.TabIndex = 0;

            m_rbOP.AutoSize = true;
            m_rbOP.Location = new Point(90, 3);
            m_rbOP.Text = "OP (ordin de plată)";
            m_rbOP.TabIndex = 1;

            m_panelTipPlataGroup.Controls.Add(m_rbCash);
            m_panelTipPlataGroup.Controls.Add(m_rbOP);

            // Rând 7 — Nr. document (y=333)
            m_labelNrDocument.AutoSize = true;
            m_labelNrDocument.Location = new Point(12, 333);
            m_labelNrDocument.Text = "Nr. chitanță / OP:";

            m_ebNrDocument.Location = new Point(200, 329);
            m_ebNrDocument.Size = new Size(220, 27);
            m_ebNrDocument.TabIndex = 7;

            // Rând 8 — Sumă (y=370)
            m_labelSuma.AutoSize = true;
            m_labelSuma.Location = new Point(12, 370);
            m_labelSuma.Text = "Sumă (lei):";

            m_ebSuma.Location = new Point(200, 366);
            m_ebSuma.Size = new Size(130, 27);
            m_ebSuma.TextAlign = HorizontalAlignment.Right;
            m_ebSuma.TabIndex = 8;
            m_ebSuma.TextChanged += m_ebSuma_TextChanged;

            // Rând 9 — Reprezentând (y=407)
            m_labelReprezentand.AutoSize = true;
            m_labelReprezentand.Location = new Point(12, 407);
            m_labelReprezentand.Text = "Reprezentând:";

            m_ebReprezentand.Location = new Point(200, 403);
            m_ebReprezentand.Size = new Size(900, 27);
            m_ebReprezentand.TabIndex = 9;
            m_ebReprezentand.TextChanged += m_ebReprezentand_TextChanged;

            m_panelCampuri.Controls.Add(m_labelNrIncasare);
            m_panelCampuri.Controls.Add(m_ebNrIncasare);
            m_panelCampuri.Controls.Add(m_labelData);
            m_panelCampuri.Controls.Add(m_dtpData);
            m_panelCampuri.Controls.Add(m_labelLegatura);
            m_panelCampuri.Controls.Add(m_panelLegaturaGroup);
            m_panelCampuri.Controls.Add(m_cbFactura);
            m_panelCampuri.Controls.Add(m_cbADRN);
            m_panelCampuri.Controls.Add(m_dgCursanti);
            m_panelCampuri.Controls.Add(m_lblInfoLegatura);
            m_panelCampuri.Controls.Add(m_labelTipPlata);
            m_panelCampuri.Controls.Add(m_panelTipPlataGroup);
            m_panelCampuri.Controls.Add(m_labelNrDocument);
            m_panelCampuri.Controls.Add(m_ebNrDocument);
            m_panelCampuri.Controls.Add(m_labelSuma);
            m_panelCampuri.Controls.Add(m_ebSuma);
            m_panelCampuri.Controls.Add(m_labelReprezentand);
            m_panelCampuri.Controls.Add(m_ebReprezentand);

            // ── Panel titlu grid (dock top, 28px) ────────────────────────────
            m_panelTitluGrid.Dock = DockStyle.Top;
            m_panelTitluGrid.Size = new Size(1160, 28);
            m_panelTitluGrid.TabIndex = 2;

            m_lblTitluGrid.AutoSize = true;
            m_lblTitluGrid.Location = new Point(5, 6);
            m_lblTitluGrid.Text = "Istoric încasări (factură / contract sponsorizare)";
            m_lblTitluGrid.Font = new Font("Arial", 9F, FontStyle.Bold);

            m_panelTitluGrid.Controls.Add(m_lblTitluGrid);

            // ── Grid istoric (fill) ──────────────────────────────────────────
            m_gIstoric.Dock = DockStyle.Fill;
            m_gIstoric.ReadOnly = true;
            m_gIstoric.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            m_gIstoric.Font = new Font("Arial", 8F);
            m_gIstoric.RowHeadersWidth = 25;
            m_gIstoric.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_gIstoric.TabIndex = 3;

            // ── Form ─────────────────────────────────────────────────────────
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1160, 500);
            Controls.Add(m_gIstoric);
            Controls.Add(m_panelTitluGrid);
            Controls.Add(m_panelCampuri);
            Controls.Add(m_toolStrip);
            Name = "FrmGestiuneIncasariAbonament";
            Text = "Gestiune Încasare";
            FormClosing += FrmGestiuneIncasariAbonament_FormClosing;
            Load += FrmGestiuneIncasariAbonament_Load;

            m_toolStrip.ResumeLayout(false);
            m_toolStrip.PerformLayout();
            m_panelLegaturaGroup.ResumeLayout(false);
            m_panelLegaturaGroup.PerformLayout();
            m_panelTipPlataGroup.ResumeLayout(false);
            m_panelTipPlataGroup.PerformLayout();
            m_panelCampuri.ResumeLayout(false);
            m_panelCampuri.PerformLayout();
            m_panelTitluGrid.ResumeLayout(false);
            m_panelTitluGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)m_dgCursanti).EndInit();
            ((System.ComponentModel.ISupportInitialize)m_gIstoric).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.ToolStrip m_toolStrip;
        private System.Windows.Forms.ToolStripButton m_tsbSalveaza;
        private System.Windows.Forms.ToolStripButton m_tsbListare;
        private System.Windows.Forms.Panel m_panelCampuri;
        private System.Windows.Forms.Label m_labelNrIncasare;
        private System.Windows.Forms.TextBox m_ebNrIncasare;
        private System.Windows.Forms.Label m_labelData;
        private System.Windows.Forms.DateTimePicker m_dtpData;
        private System.Windows.Forms.Label m_labelLegatura;
        private System.Windows.Forms.Panel m_panelLegaturaGroup;
        private System.Windows.Forms.RadioButton m_rbFaraLegatura;
        private System.Windows.Forms.RadioButton m_rbLegaturaFactura;
        private System.Windows.Forms.RadioButton m_rbLegaturaSponsorizare;
        private System.Windows.Forms.RadioButton m_rbLegaturaDirecta;
        private GridPersonalizat.CustomCombo m_cbFactura;
        private GridPersonalizat.CustomCombo m_cbADRN;
        private System.Windows.Forms.DataGridView m_dgCursanti;
        private System.Windows.Forms.Label m_lblInfoLegatura;
        private System.Windows.Forms.Label m_labelTipPlata;
        private System.Windows.Forms.Panel m_panelTipPlataGroup;
        private System.Windows.Forms.RadioButton m_rbCash;
        private System.Windows.Forms.RadioButton m_rbOP;
        private System.Windows.Forms.Label m_labelNrDocument;
        private System.Windows.Forms.TextBox m_ebNrDocument;
        private System.Windows.Forms.Label m_labelSuma;
        private System.Windows.Forms.TextBox m_ebSuma;
        private System.Windows.Forms.Label m_labelReprezentand;
        private System.Windows.Forms.TextBox m_ebReprezentand;
        private System.Windows.Forms.Panel m_panelTitluGrid;
        private System.Windows.Forms.Label m_lblTitluGrid;
        private GridPersonalizat.CustomGrid m_gIstoric;
    }
}

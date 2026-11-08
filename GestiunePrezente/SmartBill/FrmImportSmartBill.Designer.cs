namespace GestiunePrezente.SmartBill
{
    partial class FrmImportSmartBill
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
            m_pTop          = new Panel();
            m_lblLuna       = new Label();
            m_dtpLuna       = new DateTimePicker();
            m_lblStartNr    = new Label();
            m_nudStartNr    = new NumericUpDown();
            m_btnDescarcaApi = new Button();
            m_btnImportXls  = new Button();
            m_btnInchide    = new Button();

            m_splitMain     = new SplitContainer();
            m_lblFacturi    = new Label();
            m_gridFacturi   = new DataGridView();
            m_pAsociaza       = new Panel();
            m_btnAsociaza     = new Button();
            m_btnAutoAsociaza = new Button();
            m_lblContracte  = new Label();
            m_gridContracte = new DataGridView();

            m_lblStatus     = new Label();

            // invoice grid columns
            col_serie_nr    = new DataGridViewTextBoxColumn();
            col_data        = new DataGridViewTextBoxColumn();
            col_client      = new DataGridViewTextBoxColumn();
            col_suma        = new DataGridViewTextBoxColumn();
            col_status_inv  = new DataGridViewTextBoxColumn();

            // contract grid columns
            col_c_nr        = new DataGridViewTextBoxColumn();
            col_c_cursant   = new DataGridViewTextBoxColumn();
            col_c_pret      = new DataGridViewTextBoxColumn();
            col_c_cucoId    = new DataGridViewTextBoxColumn();

            m_pTop.SuspendLayout();
            m_pAsociaza.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_splitMain).BeginInit();
            m_splitMain.Panel1.SuspendLayout();
            m_splitMain.Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_gridFacturi).BeginInit();
            ((System.ComponentModel.ISupportInitialize)m_gridContracte).BeginInit();
            SuspendLayout();

            // ── m_pTop ────────────────────────────────────────────────────
            m_pTop.Dock     = DockStyle.Top;
            m_pTop.Height   = 44;
            m_pTop.Padding  = new Padding(8, 7, 8, 5);
            m_pTop.Name     = "m_pTop";
            m_pTop.Controls.AddRange(new Control[] { m_lblLuna, m_dtpLuna, m_lblStartNr, m_nudStartNr, m_btnDescarcaApi, m_btnImportXls, m_btnInchide });

            m_lblLuna.Text     = "Luna:";
            m_lblLuna.AutoSize = true;
            m_lblLuna.Location = new Point(8, 12);
            m_lblLuna.Name     = "m_lblLuna";

            m_dtpLuna.Format       = DateTimePickerFormat.Custom;
            m_dtpLuna.CustomFormat = "MM / yyyy";
            m_dtpLuna.ShowUpDown   = true;
            m_dtpLuna.Width        = 100;
            m_dtpLuna.Location     = new Point(48, 8);
            m_dtpLuna.Name         = "m_dtpLuna";
            m_dtpLuna.ValueChanged += new EventHandler(m_dtpLuna_ValueChanged);

            m_lblStartNr.Text      = "De la nr.:";
            m_lblStartNr.AutoSize  = true;
            m_lblStartNr.Location  = new Point(158, 12);
            m_lblStartNr.Name      = "m_lblStartNr";

            ((System.ComponentModel.ISupportInitialize)m_nudStartNr).BeginInit();
            m_nudStartNr.Minimum   = 1;
            m_nudStartNr.Maximum   = 999999;
            m_nudStartNr.Value     = 1;
            m_nudStartNr.Width     = 65;
            m_nudStartNr.Location  = new Point(222, 8);
            m_nudStartNr.Name      = "m_nudStartNr";
            ((System.ComponentModel.ISupportInitialize)m_nudStartNr).EndInit();

            m_btnDescarcaApi.Text     = "⬇ Descarcă API";
            m_btnDescarcaApi.Width    = 130;
            m_btnDescarcaApi.Location = new Point(297, 8);
            m_btnDescarcaApi.Name     = "m_btnDescarcaApi";
            m_btnDescarcaApi.UseVisualStyleBackColor = true;
            m_btnDescarcaApi.Click   += new EventHandler(m_btnDescarcaApi_Click);

            m_btnImportXls.Text     = "Importă Excel SmartBill";
            m_btnImportXls.Width    = 170;
            m_btnImportXls.Location = new Point(437, 8);
            m_btnImportXls.Name     = "m_btnImportXls";
            m_btnImportXls.UseVisualStyleBackColor = true;
            m_btnImportXls.Click   += new EventHandler(m_btnImportXls_Click);

            m_btnInchide.Text     = "Închide";
            m_btnInchide.Width    = 80;
            m_btnInchide.Anchor   = AnchorStyles.Top | AnchorStyles.Right;
            m_btnInchide.Location = new Point(800, 8);
            m_btnInchide.Name     = "m_btnInchide";
            m_btnInchide.UseVisualStyleBackColor = true;
            m_btnInchide.Click   += new EventHandler(m_btnInchide_Click);

            // ── m_lblStatus ───────────────────────────────────────────────
            m_lblStatus.Dock    = DockStyle.Bottom;
            m_lblStatus.Height  = 24;
            m_lblStatus.Padding = new Padding(8, 4, 8, 0);
            m_lblStatus.Name    = "m_lblStatus";
            m_lblStatus.Text    = "";

            // ── m_splitMain ───────────────────────────────────────────────
            m_splitMain.Dock        = DockStyle.Fill;
            m_splitMain.Orientation = Orientation.Horizontal;
            m_splitMain.SplitterDistance = 220;
            m_splitMain.Name        = "m_splitMain";

            // ── SplitMain.Panel1 — Facturi SmartBill ──────────────────────
            m_lblFacturi.Text      = "Facturi din SmartBill (Excel importat):";
            m_lblFacturi.Dock      = DockStyle.Top;
            m_lblFacturi.Height    = 20;
            m_lblFacturi.Padding   = new Padding(4, 3, 0, 0);
            m_lblFacturi.Font      = new Font(SystemFonts.DefaultFont, FontStyle.Bold);
            m_lblFacturi.Name      = "m_lblFacturi";

            col_serie_nr.Name       = "col_serie_nr";
            col_serie_nr.HeaderText = "Factură";
            col_serie_nr.Width      = 80;
            col_serie_nr.ReadOnly   = true;

            col_data.Name           = "col_data";
            col_data.HeaderText     = "Dată";
            col_data.Width          = 80;
            col_data.ReadOnly       = true;

            col_client.Name         = "col_client";
            col_client.HeaderText   = "Client SmartBill";
            col_client.Width        = 200;
            col_client.ReadOnly     = true;

            col_suma.Name           = "col_suma";
            col_suma.HeaderText     = "Sumă";
            col_suma.Width          = 70;
            col_suma.ReadOnly       = true;
            col_suma.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            col_status_inv.Name         = "col_status_inv";
            col_status_inv.HeaderText   = "Status";
            col_status_inv.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            col_status_inv.ReadOnly     = true;

            m_gridFacturi.Dock               = DockStyle.Fill;
            m_gridFacturi.AllowUserToAddRows = false;
            m_gridFacturi.AllowUserToDeleteRows = false;
            m_gridFacturi.ReadOnly           = true;
            m_gridFacturi.SelectionMode      = DataGridViewSelectionMode.FullRowSelect;
            m_gridFacturi.MultiSelect        = false;
            m_gridFacturi.RowHeadersVisible  = false;
            m_gridFacturi.RowTemplate.Height = 22;
            m_gridFacturi.BackgroundColor    = SystemColors.Window;
            m_gridFacturi.BorderStyle        = BorderStyle.None;
            m_gridFacturi.Name               = "m_gridFacturi";
            m_gridFacturi.Columns.AddRange(new DataGridViewColumn[] {
                col_serie_nr, col_data, col_client, col_suma, col_status_inv });

            m_splitMain.Panel1.Controls.Add(m_gridFacturi);
            m_splitMain.Panel1.Controls.Add(m_lblFacturi);

            // ── SplitMain.Panel2 — Contracte fără factură ─────────────────
            m_pAsociaza.Dock    = DockStyle.Top;
            m_pAsociaza.Height  = 36;
            m_pAsociaza.Padding = new Padding(4, 4, 4, 2);
            m_pAsociaza.Name    = "m_pAsociaza";
            m_pAsociaza.Controls.AddRange(new Control[] { m_btnAsociaza, m_btnAutoAsociaza });

            m_btnAsociaza.Text     = "↑  Asociază factura selectată cu contractul selectat  ↓";
            m_btnAsociaza.Dock     = DockStyle.Left;
            m_btnAsociaza.Width    = 390;
            m_btnAsociaza.Name     = "m_btnAsociaza";
            m_btnAsociaza.UseVisualStyleBackColor = true;
            m_btnAsociaza.Click   += new EventHandler(m_btnAsociaza_Click);

            m_btnAutoAsociaza.Text     = "⚡ Auto-asociază după sumă";
            m_btnAutoAsociaza.Dock     = DockStyle.Left;
            m_btnAutoAsociaza.Width    = 180;
            m_btnAutoAsociaza.Name     = "m_btnAutoAsociaza";
            m_btnAutoAsociaza.UseVisualStyleBackColor = true;
            m_btnAutoAsociaza.Click   += new EventHandler(m_btnAutoAsociaza_Click);

            m_lblContracte.Text    = "Contracte fără factură pentru luna selectată:";
            m_lblContracte.Dock    = DockStyle.Top;
            m_lblContracte.Height  = 20;
            m_lblContracte.Padding = new Padding(4, 3, 0, 0);
            m_lblContracte.Font    = new Font(SystemFonts.DefaultFont, FontStyle.Bold);
            m_lblContracte.Name    = "m_lblContracte";

            col_c_nr.Name       = "col_c_nr";
            col_c_nr.HeaderText = "Nr. Contract";
            col_c_nr.Width      = 100;
            col_c_nr.ReadOnly   = true;

            col_c_cursant.Name         = "col_c_cursant";
            col_c_cursant.HeaderText   = "Cursant(i)";
            col_c_cursant.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            col_c_cursant.ReadOnly     = true;

            col_c_pret.Name       = "col_c_pret";
            col_c_pret.HeaderText = "Preț";
            col_c_pret.Width      = 70;
            col_c_pret.ReadOnly   = true;
            col_c_pret.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            col_c_cucoId.Name    = "col_c_cucoId";
            col_c_cucoId.Visible = false;

            m_gridContracte.Dock               = DockStyle.Fill;
            m_gridContracte.AllowUserToAddRows = false;
            m_gridContracte.AllowUserToDeleteRows = false;
            m_gridContracte.ReadOnly           = true;
            m_gridContracte.SelectionMode      = DataGridViewSelectionMode.FullRowSelect;
            m_gridContracte.MultiSelect        = false;
            m_gridContracte.RowHeadersVisible  = false;
            m_gridContracte.RowTemplate.Height = 22;
            m_gridContracte.BackgroundColor    = SystemColors.Window;
            m_gridContracte.BorderStyle        = BorderStyle.None;
            m_gridContracte.Name               = "m_gridContracte";
            m_gridContracte.Columns.AddRange(new DataGridViewColumn[] {
                col_c_nr, col_c_cursant, col_c_pret, col_c_cucoId });

            m_splitMain.Panel2.Controls.Add(m_gridContracte);
            m_splitMain.Panel2.Controls.Add(m_lblContracte);
            m_splitMain.Panel2.Controls.Add(m_pAsociaza);

            // ── FrmImportSmartBill ────────────────────────────────────────
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode       = AutoScaleMode.Font;
            ClientSize          = new Size(900, 580);
            MinimumSize         = new Size(700, 450);
            Controls.Add(m_splitMain);
            Controls.Add(m_lblStatus);
            Controls.Add(m_pTop);
            FormBorderStyle     = FormBorderStyle.Sizable;
            Name                = "FrmImportSmartBill";
            Text                = "Import facturi din SmartBill";
            StartPosition       = FormStartPosition.CenterParent;

            m_pTop.ResumeLayout(false);
            m_pTop.PerformLayout();
            m_pAsociaza.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)m_splitMain).EndInit();
            m_splitMain.Panel1.ResumeLayout(false);
            m_splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)m_gridFacturi).EndInit();
            ((System.ComponentModel.ISupportInitialize)m_gridContracte).EndInit();
            ResumeLayout(false);
        }

        private Panel           m_pTop;
        private Label           m_lblLuna;
        private DateTimePicker  m_dtpLuna;
        private Label           m_lblStartNr;
        private NumericUpDown   m_nudStartNr;
        private Button          m_btnDescarcaApi;
        private Button          m_btnImportXls;
        private Button          m_btnInchide;

        private SplitContainer  m_splitMain;
        private Label           m_lblFacturi;
        private DataGridView    m_gridFacturi;
        private Panel           m_pAsociaza;
        private Button          m_btnAsociaza;
        private Button          m_btnAutoAsociaza;
        private Label           m_lblContracte;
        private DataGridView    m_gridContracte;

        private Label           m_lblStatus;

        private DataGridViewTextBoxColumn col_serie_nr;
        private DataGridViewTextBoxColumn col_data;
        private DataGridViewTextBoxColumn col_client;
        private DataGridViewTextBoxColumn col_suma;
        private DataGridViewTextBoxColumn col_status_inv;

        private DataGridViewTextBoxColumn col_c_nr;
        private DataGridViewTextBoxColumn col_c_cursant;
        private DataGridViewTextBoxColumn col_c_pret;
        private DataGridViewTextBoxColumn col_c_cucoId;
    }
}

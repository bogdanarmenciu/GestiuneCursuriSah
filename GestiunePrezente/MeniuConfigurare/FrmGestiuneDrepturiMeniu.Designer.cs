namespace GestiunePrezente.MeniuConfigurare
{
    partial class FrmGestiuneDrepturiMeniu
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
            sep = new ToolStripSeparator();
            m_tsbUtilizatorNou = new ToolStripButton();
            m_tabs = new TabControl();
            m_tabDrepturi = new TabPage();
            m_split = new SplitContainer();
            m_gUtilizatori = new DataGridView();
            m_lblUseri = new Label();
            m_gDrepturi = new DataGridView();
            m_lblMeniu = new Label();
            m_panelTop = new Panel();
            m_lblDiviziune = new Label();
            m_cbDiviziune = new ComboBox();
            m_tabVoci = new TabPage();
            m_gVoci = new DataGridView();
            m_tsVoci = new ToolStrip();
            m_tsbVoceNoua = new ToolStripButton();
            m_tsbSalveazaVoci = new ToolStripButton();
            m_toolStrip.SuspendLayout();
            m_tabs.SuspendLayout();
            m_tabDrepturi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_split).BeginInit();
            m_split.Panel1.SuspendLayout();
            m_split.Panel2.SuspendLayout();
            m_split.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_gUtilizatori).BeginInit();
            ((System.ComponentModel.ISupportInitialize)m_gDrepturi).BeginInit();
            m_panelTop.SuspendLayout();
            m_tabVoci.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_gVoci).BeginInit();
            m_tsVoci.SuspendLayout();
            SuspendLayout();
            // 
            // m_toolStrip
            // 
            m_toolStrip.ImageScalingSize = new Size(20, 20);
            m_toolStrip.Items.AddRange(new ToolStripItem[] { m_tsbSalveaza, sep, m_tsbUtilizatorNou });
            m_toolStrip.Location = new Point(0, 0);
            m_toolStrip.Name = "m_toolStrip";
            m_toolStrip.Size = new Size(1000, 27);
            m_toolStrip.TabIndex = 1;
            // 
            // m_tsbSalveaza
            // 
            m_tsbSalveaza.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            m_tsbSalveaza.Image = Properties.Resources.save;
            m_tsbSalveaza.Enabled = false;
            m_tsbSalveaza.Name = "m_tsbSalveaza";
            m_tsbSalveaza.Text = "Salvează drepturi";
            m_tsbSalveaza.Click += m_tsbSalveaza_Click;
            // 
            // sep
            // 
            sep.Name = "sep";
            sep.Size = new Size(6, 27);
            // 
            // m_tsbUtilizatorNou
            // 
            m_tsbUtilizatorNou.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            m_tsbUtilizatorNou.Image = Properties.Resources._new;
            m_tsbUtilizatorNou.Name = "m_tsbUtilizatorNou";
            m_tsbUtilizatorNou.Text = "Utilizator nou";
            m_tsbUtilizatorNou.Click += m_tsbUtilizatorNou_Click;
            // 
            // m_tabs
            // 
            m_tabs.Controls.Add(m_tabDrepturi);
            m_tabs.Controls.Add(m_tabVoci);
            m_tabs.Dock = DockStyle.Fill;
            m_tabs.Location = new Point(0, 27);
            m_tabs.Name = "m_tabs";
            m_tabs.SelectedIndex = 0;
            m_tabs.Size = new Size(1000, 623);
            m_tabs.TabIndex = 0;
            m_tabs.SelectedIndexChanged += m_tabs_SelectedIndexChanged;
            // 
            // m_tabDrepturi
            // 
            m_tabDrepturi.Controls.Add(m_split);
            m_tabDrepturi.Controls.Add(m_panelTop);
            m_tabDrepturi.Location = new Point(4, 29);
            m_tabDrepturi.Name = "m_tabDrepturi";
            m_tabDrepturi.Size = new Size(992, 590);
            m_tabDrepturi.TabIndex = 0;
            m_tabDrepturi.Text = "Drepturi utilizatori";
            // 
            // m_split
            // 
            m_split.Dock = DockStyle.Fill;
            m_split.Location = new Point(0, 42);
            m_split.Name = "m_split";
            // 
            // m_split.Panel1
            // 
            m_split.Panel1.Controls.Add(m_gUtilizatori);
            m_split.Panel1.Controls.Add(m_lblUseri);
            // 
            // m_split.Panel2
            // 
            m_split.Panel2.Controls.Add(m_gDrepturi);
            m_split.Panel2.Controls.Add(m_lblMeniu);
            m_split.Size = new Size(992, 548);
            m_split.SplitterDistance = 479;
            m_split.TabIndex = 0;
            // 
            // m_gUtilizatori
            // 
            m_gUtilizatori.AllowUserToAddRows = false;
            m_gUtilizatori.AllowUserToDeleteRows = false;
            m_gUtilizatori.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_gUtilizatori.Dock = DockStyle.Fill;
            m_gUtilizatori.Location = new Point(0, 22);
            m_gUtilizatori.MultiSelect = false;
            m_gUtilizatori.Name = "m_gUtilizatori";
            m_gUtilizatori.RowHeadersVisible = false;
            m_gUtilizatori.RowHeadersWidth = 51;
            m_gUtilizatori.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            m_gUtilizatori.Size = new Size(479, 526);
            m_gUtilizatori.TabIndex = 0;
            m_gUtilizatori.CellDoubleClick += m_gUtilizatori_CellDoubleClick;
            m_gUtilizatori.SelectionChanged += m_gUtilizatori_SelectionChanged;
            // 
            // m_lblUseri
            // 
            m_lblUseri.BackColor = Color.FromArgb(220, 230, 245);
            m_lblUseri.Dock = DockStyle.Top;
            m_lblUseri.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            m_lblUseri.Location = new Point(0, 0);
            m_lblUseri.Name = "m_lblUseri";
            m_lblUseri.Size = new Size(479, 22);
            m_lblUseri.TabIndex = 1;
            m_lblUseri.Text = "Utilizatori activi";
            m_lblUseri.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // m_gDrepturi
            // 
            m_gDrepturi.AllowUserToAddRows = false;
            m_gDrepturi.AllowUserToDeleteRows = false;
            m_gDrepturi.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_gDrepturi.Dock = DockStyle.Fill;
            m_gDrepturi.Location = new Point(0, 22);
            m_gDrepturi.MultiSelect = false;
            m_gDrepturi.Name = "m_gDrepturi";
            m_gDrepturi.RowHeadersVisible = false;
            m_gDrepturi.RowHeadersWidth = 51;
            m_gDrepturi.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            m_gDrepturi.Size = new Size(509, 526);
            m_gDrepturi.TabIndex = 0;
            m_gDrepturi.CellPainting += m_gDrepturi_CellPainting;
            m_gDrepturi.CellValueChanged += m_gDrepturi_CellValueChanged;
            m_gDrepturi.ColumnHeaderMouseClick += m_gDrepturi_ColumnHeaderMouseClick;
            m_gDrepturi.CurrentCellDirtyStateChanged += m_gDrepturi_CurrentCellDirtyStateChanged;
            // 
            // m_lblMeniu
            // 
            m_lblMeniu.BackColor = Color.FromArgb(220, 245, 220);
            m_lblMeniu.Dock = DockStyle.Top;
            m_lblMeniu.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            m_lblMeniu.Location = new Point(0, 0);
            m_lblMeniu.Name = "m_lblMeniu";
            m_lblMeniu.Size = new Size(509, 22);
            m_lblMeniu.TabIndex = 1;
            m_lblMeniu.Text = "Voci meniu";
            m_lblMeniu.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // m_panelTop
            // 
            m_panelTop.Controls.Add(m_lblDiviziune);
            m_panelTop.Controls.Add(m_cbDiviziune);
            m_panelTop.Dock = DockStyle.Top;
            m_panelTop.Location = new Point(0, 0);
            m_panelTop.Name = "m_panelTop";
            m_panelTop.Size = new Size(992, 42);
            m_panelTop.TabIndex = 1;
            // 
            // m_lblDiviziune
            // 
            m_lblDiviziune.AutoSize = true;
            m_lblDiviziune.Location = new Point(8, 12);
            m_lblDiviziune.Name = "m_lblDiviziune";
            m_lblDiviziune.Size = new Size(73, 20);
            m_lblDiviziune.TabIndex = 0;
            m_lblDiviziune.Text = "Diviziune:";
            // 
            // m_cbDiviziune
            // 
            m_cbDiviziune.DropDownStyle = ComboBoxStyle.DropDownList;
            m_cbDiviziune.Location = new Point(80, 8);
            m_cbDiviziune.Name = "m_cbDiviziune";
            m_cbDiviziune.Size = new Size(220, 28);
            m_cbDiviziune.TabIndex = 1;
            m_cbDiviziune.SelectedIndexChanged += m_cbDiviziune_SelectedIndexChanged;
            // 
            // m_tabVoci
            // 
            m_tabVoci.Controls.Add(m_gVoci);
            m_tabVoci.Controls.Add(m_tsVoci);
            m_tabVoci.Location = new Point(4, 29);
            m_tabVoci.Name = "m_tabVoci";
            m_tabVoci.Size = new Size(192, 67);
            m_tabVoci.TabIndex = 1;
            m_tabVoci.Text = "Voci meniu";
            // 
            // m_gVoci
            // 
            m_gVoci.AllowUserToAddRows = false;
            m_gVoci.AllowUserToDeleteRows = false;
            m_gVoci.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_gVoci.Dock = DockStyle.Fill;
            m_gVoci.Location = new Point(0, 27);
            m_gVoci.Name = "m_gVoci";
            m_gVoci.RowHeadersVisible = false;
            m_gVoci.RowHeadersWidth = 51;
            m_gVoci.Size = new Size(192, 40);
            m_gVoci.TabIndex = 0;
            m_gVoci.CellValueChanged += m_gVoci_CellValueChanged;
            m_gVoci.CurrentCellDirtyStateChanged += m_gVoci_CurrentCellDirtyStateChanged;
            // 
            // m_tsVoci
            // 
            m_tsVoci.ImageScalingSize = new Size(20, 20);
            m_tsVoci.Items.AddRange(new ToolStripItem[] { m_tsbVoceNoua, m_tsbSalveazaVoci });
            m_tsVoci.Location = new Point(0, 0);
            m_tsVoci.Name = "m_tsVoci";
            m_tsVoci.Size = new Size(192, 27);
            m_tsVoci.TabIndex = 1;
            // 
            // m_tsbVoceNoua
            // 
            m_tsbVoceNoua.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            m_tsbVoceNoua.Image = Properties.Resources._new;
            m_tsbVoceNoua.Name = "m_tsbVoceNoua";
            m_tsbVoceNoua.Text = "Voce nouă";
            m_tsbVoceNoua.Click += m_tsbVoceNoua_Click;
            // 
            // m_tsbSalveazaVoci
            // 
            m_tsbSalveazaVoci.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            m_tsbSalveazaVoci.Image = Properties.Resources.save;
            m_tsbSalveazaVoci.Enabled = false;
            m_tsbSalveazaVoci.Name = "m_tsbSalveazaVoci";
            m_tsbSalveazaVoci.Text = "Salvează voci";
            m_tsbSalveazaVoci.Click += m_tsbSalveazaVoci_Click;
            // 
            // FrmGestiuneDrepturiMeniu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 650);
            Controls.Add(m_tabs);
            Controls.Add(m_toolStrip);
            Name = "FrmGestiuneDrepturiMeniu";
            Text = "Drepturi meniu utilizatori";
            WindowState = FormWindowState.Maximized;
            Load += FrmGestiuneDrepturiMeniu_Load;
            m_toolStrip.ResumeLayout(false);
            m_toolStrip.PerformLayout();
            m_tabs.ResumeLayout(false);
            m_tabDrepturi.ResumeLayout(false);
            m_split.Panel1.ResumeLayout(false);
            m_split.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)m_split).EndInit();
            m_split.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)m_gUtilizatori).EndInit();
            ((System.ComponentModel.ISupportInitialize)m_gDrepturi).EndInit();
            m_panelTop.ResumeLayout(false);
            m_panelTop.PerformLayout();
            m_tabVoci.ResumeLayout(false);
            m_tabVoci.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)m_gVoci).EndInit();
            m_tsVoci.ResumeLayout(false);
            m_tsVoci.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private ToolStrip m_toolStrip;
        private ToolStripButton m_tsbSalveaza;
        private ToolStripButton m_tsbUtilizatorNou;
        private TabControl m_tabs;
        private TabPage m_tabDrepturi, m_tabVoci;
        private Panel m_panelTop;
        private Label m_lblDiviziune, m_lblUseri, m_lblMeniu;
        private ComboBox m_cbDiviziune;
        private SplitContainer m_split;
        private DataGridView m_gUtilizatori;
        private DataGridView m_gDrepturi;
        private ToolStrip m_tsVoci;
        private ToolStripButton m_tsbVoceNoua, m_tsbSalveazaVoci;
        private DataGridView m_gVoci;
        private ToolStripSeparator sep;
    }
}

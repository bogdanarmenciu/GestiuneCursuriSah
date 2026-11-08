using GridPersonalizat;

namespace GestiunePrezente.MeniuCursanti
{
    partial class FrmGestiuneContract
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            toolStrip1 = new ToolStrip();
            m_tsbSalveaza = new ToolStripButton();
            m_tsbDeschideArhiva = new ToolStripButton();
            m_tsbListeazaContract = new ToolStripButton();
            m_tsbNotaReziliere = new ToolStripButton();
            m_tsbSepPersoana = new ToolStripSeparator();
            m_tsbStergePersoana = new ToolStripButton();
            panel1 = new Panel();
            m_tabControl = new TabControl();
            m_tabPageCursanti = new TabPage();
            m_gContracte = new CustomGrid();
            m_tabPageAbonamente = new TabPage();
            m_gAbonamente = new CustomGrid();
            m_contextAbonamente = new ContextMenuStrip(components);
            m_menuItemGenActAditional = new ToolStripMenuItem();
            m_panelNouAbonament = new Panel();
            m_labelDataAb = new Label();
            m_labelAbonamentAb = new Label();
            m_cbAbonamentAb = new CustomCombo();
            m_dtDataInceputAb = new DateTimePicker();
            m_labelPretAb = new Label();
            m_ebPretAb = new TextBox();
            m_labelSfarsitAb = new Label();
            m_dtDataSfarsitAb = new DateTimePicker();
            m_btnSalveazaAbonament = new Button();
            m_btnAnuleazaAbonament = new Button();
            m_panelAbonamenteButtons = new Panel();
            m_btnAdaugaAbonament = new Button();
            m_tabPageDocumenteEmise = new TabPage();
            m_gDocumenteEmise = new CustomGrid();
            m_tabPageArhiva = new TabPage();
            m_gArhiva = new CustomGrid();
            m_panelArhivaButtons = new Panel();
            m_btnAdaugaArhiva = new Button();
            m_btnDeschideArhiva = new Button();
            m_btnStergeArhiva = new Button();
            m_tabPageFacturi = new TabPage();
            m_gFacturi = new CustomGrid();
            m_panelFacturiButtons = new Panel();
            m_btnAdaugaFactura = new Button();
            m_btnModificaFactura = new Button();
            m_btnStergeFactura = new Button();
            panel2 = new Panel();
            m_ckAnulat = new CheckBox();
            m_ckListat = new CheckBox();
            m_ckRetras = new CheckBox();
            label4 = new Label();
            label1 = new Label();
            label20 = new Label();
            m_dtDataSfarsitContract = new DateTimePicker();
            m_ebNrContract = new TextBox();
            m_dtDataInceputContract = new DateTimePicker();
            toolStrip1.SuspendLayout();
            panel1.SuspendLayout();
            m_tabControl.SuspendLayout();
            m_tabPageCursanti.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_gContracte).BeginInit();
            m_tabPageAbonamente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_gAbonamente).BeginInit();
            m_contextAbonamente.SuspendLayout();
            m_panelNouAbonament.SuspendLayout();
            m_panelAbonamenteButtons.SuspendLayout();
            m_tabPageDocumenteEmise.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_gDocumenteEmise).BeginInit();
            m_tabPageArhiva.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_gArhiva).BeginInit();
            m_panelArhivaButtons.SuspendLayout();
            m_tabPageFacturi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_gFacturi).BeginInit();
            m_panelFacturiButtons.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { m_tsbSalveaza, m_tsbDeschideArhiva, m_tsbListeazaContract, m_tsbNotaReziliere, m_tsbSepPersoana, m_tsbStergePersoana });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Padding = new Padding(0, 0, 2, 0);
            toolStrip1.Size = new Size(1354, 27);
            toolStrip1.TabIndex = 2;
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
            // m_tsbDeschideArhiva
            // 
            m_tsbDeschideArhiva.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbDeschideArhiva.Image = Properties.Resources.CursantArhiva;
            m_tsbDeschideArhiva.ImageTransparentColor = Color.Magenta;
            m_tsbDeschideArhiva.Name = "m_tsbDeschideArhiva";
            m_tsbDeschideArhiva.Size = new Size(29, 24);
            m_tsbDeschideArhiva.Text = "Deschide arhiva";
            m_tsbDeschideArhiva.Click += m_tsbDeschideArhiva_Click;
            // 
            // m_tsbListeazaContract
            // 
            m_tsbListeazaContract.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbListeazaContract.Image = Properties.Resources.Listare;
            m_tsbListeazaContract.ImageTransparentColor = Color.Magenta;
            m_tsbListeazaContract.Name = "m_tsbListeazaContract";
            m_tsbListeazaContract.Size = new Size(29, 24);
            m_tsbListeazaContract.Text = "Listeaza Contracte";
            m_tsbListeazaContract.Click += m_tsbListeazaContract_Click;
            // 
            // m_tsbNotaReziliere
            // 
            m_tsbNotaReziliere.Image = Properties.Resources.Listare;
            m_tsbNotaReziliere.ImageTransparentColor = Color.Magenta;
            m_tsbNotaReziliere.Name = "m_tsbNotaReziliere";
            m_tsbNotaReziliere.Size = new Size(144, 24);
            m_tsbNotaReziliere.Text = "Notă de reziliere";
            m_tsbNotaReziliere.Click += m_tsbNotaReziliere_Click;
            // 
            // m_tsbSepPersoana
            // 
            m_tsbSepPersoana.Name = "m_tsbSepPersoana";
            m_tsbSepPersoana.Size = new Size(6, 27);
            // 
            // m_tsbStergePersoana
            // 
            m_tsbStergePersoana.Image = Properties.Resources.Delete;
            m_tsbStergePersoana.Name = "m_tsbStergePersoana";
            m_tsbStergePersoana.Size = new Size(141, 24);
            m_tsbStergePersoana.Text = "Șterge persoană";
            m_tsbStergePersoana.ToolTipText = "Șterge persoana selectată din contract (fără prezențe)";
            m_tsbStergePersoana.Click += m_tsbStergePersoana_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(m_tabControl);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 27);
            panel1.Margin = new Padding(4);
            panel1.Name = "panel1";
            panel1.Size = new Size(1354, 635);
            panel1.TabIndex = 3;
            // 
            // m_tabControl
            // 
            m_tabControl.Controls.Add(m_tabPageCursanti);
            m_tabControl.Controls.Add(m_tabPageAbonamente);
            m_tabControl.Controls.Add(m_tabPageDocumenteEmise);
            m_tabControl.Controls.Add(m_tabPageArhiva);
            m_tabControl.Controls.Add(m_tabPageFacturi);
            m_tabControl.Dock = DockStyle.Fill;
            m_tabControl.Location = new Point(0, 90);
            m_tabControl.Name = "m_tabControl";
            m_tabControl.SelectedIndex = 0;
            m_tabControl.Size = new Size(1354, 545);
            m_tabControl.TabIndex = 80;
            // 
            // m_tabPageCursanti
            // 
            m_tabPageCursanti.Controls.Add(m_gContracte);
            m_tabPageCursanti.Location = new Point(4, 29);
            m_tabPageCursanti.Name = "m_tabPageCursanti";
            m_tabPageCursanti.Padding = new Padding(3);
            m_tabPageCursanti.Size = new Size(1346, 512);
            m_tabPageCursanti.TabIndex = 0;
            m_tabPageCursanti.Text = "Cursanți";
            m_tabPageCursanti.UseVisualStyleBackColor = true;
            // 
            // m_gContracte
            // 
            m_gContracte.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_gContracte.Dock = DockStyle.Fill;
            m_gContracte.Font = new Font("Arial", 8F);
            m_gContracte.Location = new Point(3, 3);
            m_gContracte.Margin = new Padding(4, 6, 4, 6);
            m_gContracte.Name = "m_gContracte";
            m_gContracte.ReadOnly = true;
            m_gContracte.RowHeadersWidth = 51;
            m_gContracte.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            m_gContracte.Size = new Size(1340, 506);
            m_gContracte.TabIndex = 79;
            m_gContracte.DoubleClick += m_gContracte_DoubleClick;
            // 
            // m_tabPageAbonamente
            // 
            m_tabPageAbonamente.Controls.Add(m_gAbonamente);
            m_tabPageAbonamente.Controls.Add(m_panelNouAbonament);
            m_tabPageAbonamente.Controls.Add(m_panelAbonamenteButtons);
            m_tabPageAbonamente.Location = new Point(4, 29);
            m_tabPageAbonamente.Name = "m_tabPageAbonamente";
            m_tabPageAbonamente.Padding = new Padding(3);
            m_tabPageAbonamente.Size = new Size(1346, 512);
            m_tabPageAbonamente.TabIndex = 1;
            m_tabPageAbonamente.Text = "Abonamente";
            m_tabPageAbonamente.UseVisualStyleBackColor = true;
            // 
            // m_gAbonamente
            // 
            m_gAbonamente.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_gAbonamente.ContextMenuStrip = m_contextAbonamente;
            m_gAbonamente.Dock = DockStyle.Fill;
            m_gAbonamente.Font = new Font("Arial", 8F);
            m_gAbonamente.Location = new Point(3, 88);
            m_gAbonamente.Margin = new Padding(4, 6, 4, 6);
            m_gAbonamente.Name = "m_gAbonamente";
            m_gAbonamente.ReadOnly = true;
            m_gAbonamente.RowHeadersWidth = 51;
            m_gAbonamente.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            m_gAbonamente.Size = new Size(1340, 421);
            m_gAbonamente.TabIndex = 2;
            // 
            // m_contextAbonamente
            // 
            m_contextAbonamente.ImageScalingSize = new Size(20, 20);
            m_contextAbonamente.Items.AddRange(new ToolStripItem[] { m_menuItemGenActAditional });
            m_contextAbonamente.Name = "m_contextAbonamente";
            m_contextAbonamente.Size = new Size(236, 28);
            m_contextAbonamente.Opening += m_contextAbonamente_Opening;
            // 
            // m_menuItemGenActAditional
            // 
            m_menuItemGenActAditional.Name = "m_menuItemGenActAditional";
            m_menuItemGenActAditional.Size = new Size(235, 24);
            m_menuItemGenActAditional.Text = "Generează act adițional";
            m_menuItemGenActAditional.Click += m_menuItemGenActAditional_Click;
            // 
            // m_panelNouAbonament
            // 
            m_panelNouAbonament.BackColor = SystemColors.Info;
            m_panelNouAbonament.Controls.Add(m_labelDataAb);
            m_panelNouAbonament.Controls.Add(m_labelAbonamentAb);
            m_panelNouAbonament.Controls.Add(m_cbAbonamentAb);
            m_panelNouAbonament.Controls.Add(m_dtDataInceputAb);
            m_panelNouAbonament.Controls.Add(m_labelPretAb);
            m_panelNouAbonament.Controls.Add(m_ebPretAb);
            m_panelNouAbonament.Controls.Add(m_labelSfarsitAb);
            m_panelNouAbonament.Controls.Add(m_dtDataSfarsitAb);
            m_panelNouAbonament.Controls.Add(m_btnSalveazaAbonament);
            m_panelNouAbonament.Controls.Add(m_btnAnuleazaAbonament);
            m_panelNouAbonament.Dock = DockStyle.Top;
            m_panelNouAbonament.Location = new Point(3, 38);
            m_panelNouAbonament.Name = "m_panelNouAbonament";
            m_panelNouAbonament.Size = new Size(1340, 50);
            m_panelNouAbonament.TabIndex = 1;
            m_panelNouAbonament.Visible = false;
            // 
            // m_labelDataAb
            // 
            m_labelDataAb.AutoSize = true;
            m_labelDataAb.Location = new Point(507, 14);
            m_labelDataAb.Name = "m_labelDataAb";
            m_labelDataAb.Size = new Size(97, 20);
            m_labelDataAb.TabIndex = 0;
            m_labelDataAb.Text = "Data început:";
            // 
            // m_labelAbonamentAb
            // 
            m_labelAbonamentAb.AutoSize = true;
            m_labelAbonamentAb.Location = new Point(12, 13);
            m_labelAbonamentAb.Name = "m_labelAbonamentAb";
            m_labelAbonamentAb.Size = new Size(113, 20);
            m_labelAbonamentAb.TabIndex = 2;
            m_labelAbonamentAb.Text = "Tip abonament:";
            // 
            // m_cbAbonamentAb
            // 
            m_cbAbonamentAb.AutoCompleteMode = AutoCompleteMode.Append;
            m_cbAbonamentAb.AutoCompleteSource = AutoCompleteSource.ListItems;
            m_cbAbonamentAb.Font = new Font("Microsoft Sans Serif", 8F);
            m_cbAbonamentAb.FormattingEnabled = true;
            m_cbAbonamentAb.Location = new Point(132, 9);
            m_cbAbonamentAb.Margin = new Padding(4, 6, 4, 6);
            m_cbAbonamentAb.Name = "m_cbAbonamentAb";
            m_cbAbonamentAb.Size = new Size(238, 24);
            m_cbAbonamentAb.TabIndex = 2;
            m_cbAbonamentAb.SelectedIndexChanged += m_cbAbonamentAb_SelectedIndexChanged;
            // 
            // m_dtDataInceputAb
            // 
            m_dtDataInceputAb.Location = new Point(605, 10);
            m_dtDataInceputAb.Name = "m_dtDataInceputAb";
            m_dtDataInceputAb.Size = new Size(248, 27);
            m_dtDataInceputAb.TabIndex = 1;
            m_dtDataInceputAb.ValueChanged += m_dtDataInceputAb_ValueChanged;
            // 
            // m_labelPretAb
            // 
            m_labelPretAb.AutoSize = true;
            m_labelPretAb.Location = new Point(376, 13);
            m_labelPretAb.Name = "m_labelPretAb";
            m_labelPretAb.Size = new Size(38, 20);
            m_labelPretAb.TabIndex = 3;
            m_labelPretAb.Text = "Preț:";
            // 
            // m_ebPretAb
            // 
            m_ebPretAb.Location = new Point(422, 9);
            m_ebPretAb.Name = "m_ebPretAb";
            m_ebPretAb.Size = new Size(90, 27);
            m_ebPretAb.TabIndex = 3;
            // 
            // m_labelSfarsitAb
            // 
            m_labelSfarsitAb.AutoSize = true;
            m_labelSfarsitAb.Location = new Point(858, 15);
            m_labelSfarsitAb.Name = "m_labelSfarsitAb";
            m_labelSfarsitAb.Size = new Size(59, 20);
            m_labelSfarsitAb.TabIndex = 4;
            m_labelSfarsitAb.Text = "Până la:";
            // 
            // m_dtDataSfarsitAb
            // 
            m_dtDataSfarsitAb.Enabled = false;
            m_dtDataSfarsitAb.Location = new Point(924, 11);
            m_dtDataSfarsitAb.Name = "m_dtDataSfarsitAb";
            m_dtDataSfarsitAb.Size = new Size(251, 27);
            m_dtDataSfarsitAb.TabIndex = 4;
            // 
            // m_btnSalveazaAbonament
            // 
            m_btnSalveazaAbonament.BackColor = Color.LightGreen;
            m_btnSalveazaAbonament.Location = new Point(1178, 11);
            m_btnSalveazaAbonament.Name = "m_btnSalveazaAbonament";
            m_btnSalveazaAbonament.Size = new Size(50, 27);
            m_btnSalveazaAbonament.TabIndex = 5;
            m_btnSalveazaAbonament.Text = "OK";
            m_btnSalveazaAbonament.UseVisualStyleBackColor = false;
            m_btnSalveazaAbonament.Click += m_btnSalveazaAbonament_Click;
            // 
            // m_btnAnuleazaAbonament
            // 
            m_btnAnuleazaAbonament.Location = new Point(1234, 11);
            m_btnAnuleazaAbonament.Name = "m_btnAnuleazaAbonament";
            m_btnAnuleazaAbonament.Size = new Size(50, 27);
            m_btnAnuleazaAbonament.TabIndex = 6;
            m_btnAnuleazaAbonament.Text = "X";
            m_btnAnuleazaAbonament.UseVisualStyleBackColor = true;
            m_btnAnuleazaAbonament.Click += m_btnAnuleazaAbonament_Click;
            // 
            // m_panelAbonamenteButtons
            // 
            m_panelAbonamenteButtons.Controls.Add(m_btnAdaugaAbonament);
            m_panelAbonamenteButtons.Dock = DockStyle.Top;
            m_panelAbonamenteButtons.Location = new Point(3, 3);
            m_panelAbonamenteButtons.Name = "m_panelAbonamenteButtons";
            m_panelAbonamenteButtons.Size = new Size(1340, 35);
            m_panelAbonamenteButtons.TabIndex = 0;
            // 
            // m_btnAdaugaAbonament
            // 
            m_btnAdaugaAbonament.Location = new Point(4, 4);
            m_btnAdaugaAbonament.Name = "m_btnAdaugaAbonament";
            m_btnAdaugaAbonament.Size = new Size(190, 27);
            m_btnAdaugaAbonament.TabIndex = 0;
            m_btnAdaugaAbonament.Text = "Adaugă perioadă abonament";
            m_btnAdaugaAbonament.UseVisualStyleBackColor = true;
            m_btnAdaugaAbonament.Click += m_btnAdaugaAbonament_Click;
            // 
            // m_tabPageDocumenteEmise
            // 
            m_tabPageDocumenteEmise.Controls.Add(m_gDocumenteEmise);
            m_tabPageDocumenteEmise.Location = new Point(4, 29);
            m_tabPageDocumenteEmise.Name = "m_tabPageDocumenteEmise";
            m_tabPageDocumenteEmise.Padding = new Padding(3);
            m_tabPageDocumenteEmise.Size = new Size(1346, 512);
            m_tabPageDocumenteEmise.TabIndex = 2;
            m_tabPageDocumenteEmise.Text = "Documente emise";
            m_tabPageDocumenteEmise.UseVisualStyleBackColor = true;
            // 
            // m_gDocumenteEmise
            // 
            m_gDocumenteEmise.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_gDocumenteEmise.Dock = DockStyle.Fill;
            m_gDocumenteEmise.Font = new Font("Arial", 8F);
            m_gDocumenteEmise.Location = new Point(3, 3);
            m_gDocumenteEmise.Name = "m_gDocumenteEmise";
            m_gDocumenteEmise.ReadOnly = true;
            m_gDocumenteEmise.RowHeadersWidth = 51;
            m_gDocumenteEmise.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            m_gDocumenteEmise.Size = new Size(1340, 506);
            m_gDocumenteEmise.TabIndex = 0;
            // 
            // m_tabPageArhiva
            // 
            m_tabPageArhiva.Controls.Add(m_gArhiva);
            m_tabPageArhiva.Controls.Add(m_panelArhivaButtons);
            m_tabPageArhiva.Location = new Point(4, 29);
            m_tabPageArhiva.Name = "m_tabPageArhiva";
            m_tabPageArhiva.Padding = new Padding(3);
            m_tabPageArhiva.Size = new Size(1346, 512);
            m_tabPageArhiva.TabIndex = 3;
            m_tabPageArhiva.Text = "Arhivă";
            m_tabPageArhiva.UseVisualStyleBackColor = true;
            // 
            // m_gArhiva
            // 
            m_gArhiva.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_gArhiva.Dock = DockStyle.Fill;
            m_gArhiva.Font = new Font("Arial", 8F);
            m_gArhiva.Location = new Point(3, 39);
            m_gArhiva.Name = "m_gArhiva";
            m_gArhiva.ReadOnly = true;
            m_gArhiva.RowHeadersWidth = 51;
            m_gArhiva.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            m_gArhiva.Size = new Size(1340, 470);
            m_gArhiva.TabIndex = 0;
            // 
            // m_panelArhivaButtons
            // 
            m_panelArhivaButtons.Controls.Add(m_btnAdaugaArhiva);
            m_panelArhivaButtons.Controls.Add(m_btnDeschideArhiva);
            m_panelArhivaButtons.Controls.Add(m_btnStergeArhiva);
            m_panelArhivaButtons.Dock = DockStyle.Top;
            m_panelArhivaButtons.Location = new Point(3, 3);
            m_panelArhivaButtons.Name = "m_panelArhivaButtons";
            m_panelArhivaButtons.Size = new Size(1340, 36);
            m_panelArhivaButtons.TabIndex = 1;
            // 
            // m_btnAdaugaArhiva
            // 
            m_btnAdaugaArhiva.Location = new Point(3, 5);
            m_btnAdaugaArhiva.Name = "m_btnAdaugaArhiva";
            m_btnAdaugaArhiva.Size = new Size(130, 26);
            m_btnAdaugaArhiva.TabIndex = 0;
            m_btnAdaugaArhiva.Text = "Adaugă fișier";
            m_btnAdaugaArhiva.Click += m_btnAdaugaArhiva_Click;
            // 
            // m_btnDeschideArhiva
            // 
            m_btnDeschideArhiva.Location = new Point(140, 5);
            m_btnDeschideArhiva.Name = "m_btnDeschideArhiva";
            m_btnDeschideArhiva.Size = new Size(100, 26);
            m_btnDeschideArhiva.TabIndex = 1;
            m_btnDeschideArhiva.Text = "Deschide";
            m_btnDeschideArhiva.Click += m_btnDeschideArhiva_Click;
            // 
            // m_btnStergeArhiva
            // 
            m_btnStergeArhiva.Location = new Point(247, 5);
            m_btnStergeArhiva.Name = "m_btnStergeArhiva";
            m_btnStergeArhiva.Size = new Size(100, 26);
            m_btnStergeArhiva.TabIndex = 2;
            m_btnStergeArhiva.Text = "Șterge";
            m_btnStergeArhiva.Click += m_btnStergeArhiva_Click;
            // 
            // m_tabPageFacturi
            // 
            m_tabPageFacturi.Controls.Add(m_gFacturi);
            m_tabPageFacturi.Controls.Add(m_panelFacturiButtons);
            m_tabPageFacturi.Location = new Point(4, 29);
            m_tabPageFacturi.Name = "m_tabPageFacturi";
            m_tabPageFacturi.Padding = new Padding(3);
            m_tabPageFacturi.Size = new Size(1346, 512);
            m_tabPageFacturi.TabIndex = 4;
            m_tabPageFacturi.Text = "Facturi";
            m_tabPageFacturi.UseVisualStyleBackColor = true;
            // 
            // m_gFacturi
            // 
            m_gFacturi.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_gFacturi.Dock = DockStyle.Fill;
            m_gFacturi.Font = new Font("Arial", 8F);
            m_gFacturi.Location = new Point(3, 39);
            m_gFacturi.Name = "m_gFacturi";
            m_gFacturi.ReadOnly = true;
            m_gFacturi.RowHeadersWidth = 51;
            m_gFacturi.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            m_gFacturi.Size = new Size(1340, 470);
            m_gFacturi.TabIndex = 0;
            m_gFacturi.DoubleClick += m_gFacturi_DoubleClick;
            // 
            // m_panelFacturiButtons
            // 
            m_panelFacturiButtons.Controls.Add(m_btnAdaugaFactura);
            m_panelFacturiButtons.Controls.Add(m_btnModificaFactura);
            m_panelFacturiButtons.Controls.Add(m_btnStergeFactura);
            m_panelFacturiButtons.Dock = DockStyle.Top;
            m_panelFacturiButtons.Location = new Point(3, 3);
            m_panelFacturiButtons.Name = "m_panelFacturiButtons";
            m_panelFacturiButtons.Size = new Size(1340, 36);
            m_panelFacturiButtons.TabIndex = 1;
            // 
            // m_btnAdaugaFactura
            // 
            m_btnAdaugaFactura.Location = new Point(3, 5);
            m_btnAdaugaFactura.Name = "m_btnAdaugaFactura";
            m_btnAdaugaFactura.Size = new Size(130, 26);
            m_btnAdaugaFactura.TabIndex = 0;
            m_btnAdaugaFactura.Text = "Adaugă factură";
            m_btnAdaugaFactura.Click += m_btnAdaugaFactura_Click;
            // 
            // m_btnModificaFactura
            // 
            m_btnModificaFactura.Location = new Point(140, 5);
            m_btnModificaFactura.Name = "m_btnModificaFactura";
            m_btnModificaFactura.Size = new Size(100, 26);
            m_btnModificaFactura.TabIndex = 1;
            m_btnModificaFactura.Text = "Modifică";
            m_btnModificaFactura.Click += m_btnModificaFactura_Click;
            // 
            // m_btnStergeFactura
            // 
            m_btnStergeFactura.Location = new Point(247, 5);
            m_btnStergeFactura.Name = "m_btnStergeFactura";
            m_btnStergeFactura.Size = new Size(100, 26);
            m_btnStergeFactura.TabIndex = 2;
            m_btnStergeFactura.Text = "Șterge";
            m_btnStergeFactura.Click += m_btnStergeFactura_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(m_ckAnulat);
            panel2.Controls.Add(m_ckListat);
            panel2.Controls.Add(m_ckRetras);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(label20);
            panel2.Controls.Add(m_dtDataSfarsitContract);
            panel2.Controls.Add(m_ebNrContract);
            panel2.Controls.Add(m_dtDataInceputContract);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Margin = new Padding(4);
            panel2.Name = "panel2";
            panel2.Size = new Size(1354, 90);
            panel2.TabIndex = 78;
            // 
            // m_ckAnulat
            // 
            m_ckAnulat.AutoSize = true;
            m_ckAnulat.Location = new Point(1179, 18);
            m_ckAnulat.Name = "m_ckAnulat";
            m_ckAnulat.Size = new Size(74, 24);
            m_ckAnulat.TabIndex = 79;
            m_ckAnulat.Text = "Anulat";
            m_ckAnulat.UseVisualStyleBackColor = true;
            m_ckAnulat.Visible = false;
            m_ckAnulat.CheckedChanged += m_ckAnulat_CheckedChanged;
            // 
            // m_ckListat
            // 
            m_ckListat.AutoSize = true;
            m_ckListat.Location = new Point(972, 22);
            m_ckListat.Name = "m_ckListat";
            m_ckListat.Size = new Size(66, 24);
            m_ckListat.TabIndex = 78;
            m_ckListat.Text = "Listat";
            m_ckListat.UseVisualStyleBackColor = true;
            // 
            // m_ckRetras
            // 
            m_ckRetras.AutoSize = true;
            m_ckRetras.Location = new Point(972, 50);
            m_ckRetras.Name = "m_ckRetras";
            m_ckRetras.Size = new Size(80, 24);
            m_ckRetras.TabIndex = 81;
            m_ckRetras.Text = "Reziliat";
            m_ckRetras.UseVisualStyleBackColor = true;
            m_ckRetras.Click += m_ckRetras_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(54, 22);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(152, 20);
            label4.TabIndex = 71;
            label4.Text = "Data început contract";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(496, 22);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(142, 20);
            label1.TabIndex = 77;
            label1.Text = "Data sfârșit contract";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(54, 54);
            label20.Margin = new Padding(4, 0, 4, 0);
            label20.Name = "label20";
            label20.Size = new Size(112, 20);
            label20.TabIndex = 15;
            label20.Text = "Număr contract";
            // 
            // m_dtDataSfarsitContract
            // 
            m_dtDataSfarsitContract.Location = new Point(638, 16);
            m_dtDataSfarsitContract.Margin = new Padding(4);
            m_dtDataSfarsitContract.Name = "m_dtDataSfarsitContract";
            m_dtDataSfarsitContract.Size = new Size(251, 27);
            m_dtDataSfarsitContract.TabIndex = 76;
            // 
            // m_ebNrContract
            // 
            m_ebNrContract.Location = new Point(231, 51);
            m_ebNrContract.Margin = new Padding(4);
            m_ebNrContract.Name = "m_ebNrContract";
            m_ebNrContract.ReadOnly = true;
            m_ebNrContract.Size = new Size(174, 27);
            m_ebNrContract.TabIndex = 14;
            m_ebNrContract.TextChanged += m_ebNrContract_TextChanged;
            // 
            // m_dtDataInceputContract
            // 
            m_dtDataInceputContract.Location = new Point(231, 16);
            m_dtDataInceputContract.Margin = new Padding(4);
            m_dtDataInceputContract.Name = "m_dtDataInceputContract";
            m_dtDataInceputContract.Size = new Size(251, 27);
            m_dtDataInceputContract.TabIndex = 70;
            m_dtDataInceputContract.ValueChanged += m_dtDataInceputContract_ValueChanged;
            // 
            // FrmGestiuneContract
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1354, 662);
            Controls.Add(panel1);
            Controls.Add(toolStrip1);
            Margin = new Padding(4);
            Name = "FrmGestiuneContract";
            Text = "Gestiune Contract";
            FormClosing += FrmGestiuneCursant_FormClosing;
            Load += FrmGestiuneContract_Load;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            m_tabControl.ResumeLayout(false);
            m_tabPageCursanti.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)m_gContracte).EndInit();
            m_tabPageAbonamente.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)m_gAbonamente).EndInit();
            m_contextAbonamente.ResumeLayout(false);
            m_panelNouAbonament.ResumeLayout(false);
            m_panelNouAbonament.PerformLayout();
            m_panelAbonamenteButtons.ResumeLayout(false);
            m_tabPageDocumenteEmise.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)m_gDocumenteEmise).EndInit();
            m_tabPageArhiva.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)m_gArhiva).EndInit();
            m_panelArhivaButtons.ResumeLayout(false);
            m_tabPageFacturi.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)m_gFacturi).EndInit();
            m_panelFacturiButtons.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton m_tsbSalveaza;
        private System.Windows.Forms.ToolStripButton m_tsbDeschideArhiva;
        private System.Windows.Forms.ToolStripButton m_tsbNotaReziliere;
        private System.Windows.Forms.ToolStripSeparator m_tsbSepPersoana;
        private System.Windows.Forms.ToolStripButton m_tsbStergePersoana;
        private TabPage m_tabPageFacturi;
        private Panel m_panelFacturiButtons;
        private Button m_btnAdaugaFactura;
        private Button m_btnModificaFactura;
        private Button m_btnStergeFactura;
        private GridPersonalizat.CustomGrid m_gFacturi;
        private System.Windows.Forms.Panel panel1;
        private TextBox m_ebNrContract;
        private Label label20;
        private Label label4;
        private DateTimePicker m_dtDataInceputContract;
        private Label label1;
        private DateTimePicker m_dtDataSfarsitContract;
        private Panel panel2;
        private CustomGrid m_gContracte;
        private ToolStripButton m_tsbListeazaContract;
        private CheckBox m_ckListat;
        private CheckBox m_ckAnulat;
        private CheckBox m_ckRetras;
        // Tab control
        private TabControl m_tabControl;
        private TabPage m_tabPageCursanti;
        private TabPage m_tabPageAbonamente;
        // Abonamente tab controls
        private Panel m_panelAbonamenteButtons;
        private Button m_btnAdaugaAbonament;
        private Panel m_panelNouAbonament;
        private Label m_labelDataAb;
        private DateTimePicker m_dtDataInceputAb;
        private Label m_labelAbonamentAb;
        private CustomCombo m_cbAbonamentAb;
        private Label m_labelPretAb;
        private TextBox m_ebPretAb;
        private Label m_labelSfarsitAb;
        private DateTimePicker m_dtDataSfarsitAb;
        private Button m_btnSalveazaAbonament;
        private Button m_btnAnuleazaAbonament;
        private CustomGrid m_gAbonamente;
        private ContextMenuStrip m_contextAbonamente;
        private ToolStripMenuItem m_menuItemGenActAditional;
        // Documente emise tab
        private TabPage m_tabPageDocumenteEmise;
        private CustomGrid m_gDocumenteEmise;
        // Arhivă tab
        private TabPage m_tabPageArhiva;
        private Panel m_panelArhivaButtons;
        private Button m_btnAdaugaArhiva;
        private Button m_btnDeschideArhiva;
        private Button m_btnStergeArhiva;
        private CustomGrid m_gArhiva;
    }
}

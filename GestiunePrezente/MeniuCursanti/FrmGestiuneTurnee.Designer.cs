using GridPersonalizat;

namespace GestiunePrezente.MeniuCursanti
{
    partial class FrmGestiuneTurnee
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            toolStrip1 = new ToolStrip();
            m_tsbSalveaza = new ToolStripButton();
            m_tsbImport = new ToolStripButton();
            m_panelDetalii = new Panel();
            m_stData = new Label();
            m_dtpData = new DateTimePicker();
            m_stDenumire = new Label();
            m_ebDenumire = new TextBox();
            m_stLocalitate = new Label();
            m_ebLocalitate = new TextBox();
            m_stTip = new Label();
            m_cbTip = new ComboBox();
            m_stOrganizator = new Label();
            m_ebOrganizator = new TextBox();
            m_stDirector = new Label();
            m_ebDirector = new TextBox();
            m_stArbSef = new Label();
            m_ebArbSef = new TextBox();
            m_stCadenta = new Label();
            m_ebCadenta = new TextBox();
            m_tabMain = new TabControl();
            m_tabToti = new TabPage();
            m_gParticipanti = new CustomGrid();
            m_panelAdauga = new Panel();
            m_btnStergeParticipant = new Button();
            m_btnAsociazaCursant = new Button();
            m_tabCursanti = new TabPage();
            m_splitCursanti = new SplitContainer();
            m_gCursantiTurneu = new CustomGrid();
            m_gRunde = new CustomGrid();
            m_stCursant = new Label();
            m_cbCursant = new ComboBox();
            m_stPuncte = new Label();
            m_ebPuncte = new TextBox();
            m_stLoc = new Label();
            m_ebLoc = new TextBox();
            m_btnAdauga = new Button();
            toolStrip1.SuspendLayout();
            m_panelDetalii.SuspendLayout();
            m_tabMain.SuspendLayout();
            m_tabToti.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_gParticipanti).BeginInit();
            m_panelAdauga.SuspendLayout();
            m_tabCursanti.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_splitCursanti).BeginInit();
            m_splitCursanti.Panel1.SuspendLayout();
            m_splitCursanti.Panel2.SuspendLayout();
            m_splitCursanti.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_gCursantiTurneu).BeginInit();
            ((System.ComponentModel.ISupportInitialize)m_gRunde).BeginInit();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { m_tsbSalveaza, m_tsbImport });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1333, 27);
            toolStrip1.TabIndex = 0;
            // 
            // m_tsbSalveaza
            // 
            m_tsbSalveaza.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbSalveaza.Image = Properties.Resources.save;
            m_tsbSalveaza.ImageTransparentColor = Color.Magenta;
            m_tsbSalveaza.Name = "m_tsbSalveaza";
            m_tsbSalveaza.Size = new Size(29, 24);
            m_tsbSalveaza.Text = "Salvează";
            m_tsbSalveaza.Click += m_tsbSalveaza_Click;
            // 
            // m_tsbImport
            // 
            m_tsbImport.Image = Properties.Resources.search;
            m_tsbImport.ImageTransparentColor = Color.Magenta;
            m_tsbImport.Name = "m_tsbImport";
            m_tsbImport.Size = new Size(165, 24);
            m_tsbImport.Text = "Import chess-results";
            m_tsbImport.Click += m_tsbImport_Click;
            // 
            // m_panelDetalii
            // 
            m_panelDetalii.Controls.Add(m_stData);
            m_panelDetalii.Controls.Add(m_dtpData);
            m_panelDetalii.Controls.Add(m_stDenumire);
            m_panelDetalii.Controls.Add(m_ebDenumire);
            m_panelDetalii.Controls.Add(m_stLocalitate);
            m_panelDetalii.Controls.Add(m_ebLocalitate);
            m_panelDetalii.Controls.Add(m_stTip);
            m_panelDetalii.Controls.Add(m_cbTip);
            m_panelDetalii.Controls.Add(m_stOrganizator);
            m_panelDetalii.Controls.Add(m_ebOrganizator);
            m_panelDetalii.Controls.Add(m_stDirector);
            m_panelDetalii.Controls.Add(m_ebDirector);
            m_panelDetalii.Controls.Add(m_stArbSef);
            m_panelDetalii.Controls.Add(m_ebArbSef);
            m_panelDetalii.Controls.Add(m_stCadenta);
            m_panelDetalii.Controls.Add(m_ebCadenta);
            m_panelDetalii.Dock = DockStyle.Top;
            m_panelDetalii.Location = new Point(0, 27);
            m_panelDetalii.Margin = new Padding(3, 4, 3, 4);
            m_panelDetalii.Name = "m_panelDetalii";
            m_panelDetalii.Size = new Size(1333, 120);
            m_panelDetalii.TabIndex = 1;
            // 
            // m_stData
            // 
            m_stData.AutoSize = true;
            m_stData.Location = new Point(14, 24);
            m_stData.Name = "m_stData";
            m_stData.Size = new Size(44, 20);
            m_stData.TabIndex = 0;
            m_stData.Text = "Data:";
            // 
            // m_dtpData
            // 
            m_dtpData.CustomFormat = "dd.MM.yyyy";
            m_dtpData.Format = DateTimePickerFormat.Custom;
            m_dtpData.Location = new Point(109, 19);
            m_dtpData.Margin = new Padding(3, 4, 3, 4);
            m_dtpData.Name = "m_dtpData";
            m_dtpData.Size = new Size(137, 27);
            m_dtpData.TabIndex = 0;
            // 
            // m_stDenumire
            // 
            m_stDenumire.AutoSize = true;
            m_stDenumire.Location = new Point(469, 22);
            m_stDenumire.Name = "m_stDenumire";
            m_stDenumire.Size = new Size(77, 20);
            m_stDenumire.TabIndex = 1;
            m_stDenumire.Text = "Denumire:";
            // 
            // m_ebDenumire
            // 
            m_ebDenumire.Location = new Point(554, 19);
            m_ebDenumire.Margin = new Padding(3, 4, 3, 4);
            m_ebDenumire.Name = "m_ebDenumire";
            m_ebDenumire.Size = new Size(767, 27);
            m_ebDenumire.TabIndex = 1;
            // 
            // m_stLocalitate
            // 
            m_stLocalitate.AutoSize = true;
            m_stLocalitate.Location = new Point(1056, 56);
            m_stLocalitate.Name = "m_stLocalitate";
            m_stLocalitate.Size = new Size(77, 20);
            m_stLocalitate.TabIndex = 2;
            m_stLocalitate.Text = "Localitate:";
            // 
            // m_ebLocalitate
            // 
            m_ebLocalitate.Location = new Point(1139, 51);
            m_ebLocalitate.Margin = new Padding(3, 4, 3, 4);
            m_ebLocalitate.Name = "m_ebLocalitate";
            m_ebLocalitate.Size = new Size(182, 27);
            m_ebLocalitate.TabIndex = 2;
            // 
            // m_stTip
            // 
            m_stTip.AutoSize = true;
            m_stTip.Location = new Point(311, 24);
            m_stTip.Name = "m_stTip";
            m_stTip.Size = new Size(33, 20);
            m_stTip.TabIndex = 3;
            m_stTip.Text = "Tip:";
            // 
            // m_cbTip
            // 
            m_cbTip.DropDownStyle = ComboBoxStyle.DropDownList;
            m_cbTip.Location = new Point(351, 19);
            m_cbTip.Margin = new Padding(3, 4, 3, 4);
            m_cbTip.Name = "m_cbTip";
            m_cbTip.Size = new Size(102, 28);
            m_cbTip.TabIndex = 3;
            // 
            // m_stOrganizator
            // 
            m_stOrganizator.AutoSize = true;
            m_stOrganizator.Location = new Point(14, 56);
            m_stOrganizator.Name = "m_stOrganizator";
            m_stOrganizator.Size = new Size(91, 20);
            m_stOrganizator.TabIndex = 4;
            m_stOrganizator.Text = "Organizator:";
            // 
            // m_ebOrganizator
            // 
            m_ebOrganizator.Location = new Point(109, 51);
            m_ebOrganizator.Margin = new Padding(3, 4, 3, 4);
            m_ebOrganizator.Name = "m_ebOrganizator";
            m_ebOrganizator.Size = new Size(344, 27);
            m_ebOrganizator.TabIndex = 10;
            // 
            // m_stDirector
            // 
            m_stDirector.AutoSize = true;
            m_stDirector.Location = new Point(14, 87);
            m_stDirector.Name = "m_stDirector";
            m_stDirector.Size = new Size(66, 20);
            m_stDirector.TabIndex = 11;
            m_stDirector.Text = "Director:";
            // 
            // m_ebDirector
            // 
            m_ebDirector.Location = new Point(109, 82);
            m_ebDirector.Margin = new Padding(3, 4, 3, 4);
            m_ebDirector.Name = "m_ebDirector";
            m_ebDirector.Size = new Size(344, 27);
            m_ebDirector.TabIndex = 11;
            // 
            // m_stArbSef
            // 
            m_stArbSef.AutoSize = true;
            m_stArbSef.Location = new Point(469, 56);
            m_stArbSef.Name = "m_stArbSef";
            m_stArbSef.Size = new Size(58, 20);
            m_stArbSef.TabIndex = 12;
            m_stArbSef.Text = "Arbitru:";
            // 
            // m_ebArbSef
            // 
            m_ebArbSef.Location = new Point(554, 51);
            m_ebArbSef.Margin = new Padding(3, 4, 3, 4);
            m_ebArbSef.Name = "m_ebArbSef";
            m_ebArbSef.Size = new Size(344, 27);
            m_ebArbSef.TabIndex = 12;
            // 
            // m_stCadenta
            // 
            m_stCadenta.AutoSize = true;
            m_stCadenta.Location = new Point(469, 85);
            m_stCadenta.Name = "m_stCadenta";
            m_stCadenta.Size = new Size(67, 20);
            m_stCadenta.TabIndex = 13;
            m_stCadenta.Text = "Cadentă:";
            // 
            // m_ebCadenta
            // 
            m_ebCadenta.Location = new Point(554, 82);
            m_ebCadenta.Margin = new Padding(3, 4, 3, 4);
            m_ebCadenta.Name = "m_ebCadenta";
            m_ebCadenta.Size = new Size(344, 27);
            m_ebCadenta.TabIndex = 13;
            // 
            // m_tabMain
            // 
            m_tabMain.Controls.Add(m_tabToti);
            m_tabMain.Controls.Add(m_tabCursanti);
            m_tabMain.Dock = DockStyle.Fill;
            m_tabMain.Location = new Point(0, 147);
            m_tabMain.Margin = new Padding(3, 4, 3, 4);
            m_tabMain.Name = "m_tabMain";
            m_tabMain.SelectedIndex = 0;
            m_tabMain.Size = new Size(1333, 720);
            m_tabMain.TabIndex = 2;
            m_tabMain.SelectedIndexChanged += m_tabMain_SelectedIndexChanged;
            // 
            // m_tabToti
            // 
            m_tabToti.Controls.Add(m_gParticipanti);
            m_tabToti.Controls.Add(m_panelAdauga);
            m_tabToti.Location = new Point(4, 29);
            m_tabToti.Margin = new Padding(3, 4, 3, 4);
            m_tabToti.Name = "m_tabToti";
            m_tabToti.Size = new Size(1325, 687);
            m_tabToti.TabIndex = 0;
            m_tabToti.Text = "Toți participanții";
            m_tabToti.UseVisualStyleBackColor = true;
            // 
            // m_gParticipanti
            // 
            m_gParticipanti.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_gParticipanti.Dock = DockStyle.Fill;
            m_gParticipanti.Font = new Font("Arial", 8F);
            m_gParticipanti.Location = new Point(0, 0);
            m_gParticipanti.Margin = new Padding(3, 4, 3, 4);
            m_gParticipanti.Name = "m_gParticipanti";
            m_gParticipanti.ReadOnly = true;
            m_gParticipanti.RowHeadersWidth = 25;
            m_gParticipanti.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            m_gParticipanti.Size = new Size(1325, 636);
            m_gParticipanti.TabIndex = 0;
            // 
            // m_panelAdauga
            // 
            m_panelAdauga.Controls.Add(m_btnStergeParticipant);
            m_panelAdauga.Controls.Add(m_btnAsociazaCursant);
            m_panelAdauga.Dock = DockStyle.Bottom;
            m_panelAdauga.Location = new Point(0, 636);
            m_panelAdauga.Margin = new Padding(3, 4, 3, 4);
            m_panelAdauga.Name = "m_panelAdauga";
            m_panelAdauga.Size = new Size(1325, 51);
            m_panelAdauga.TabIndex = 2;
            // 
            // m_btnStergeParticipant
            // 
            m_btnStergeParticipant.Location = new Point(11, 9);
            m_btnStergeParticipant.Margin = new Padding(3, 4, 3, 4);
            m_btnStergeParticipant.Name = "m_btnStergeParticipant";
            m_btnStergeParticipant.Size = new Size(137, 33);
            m_btnStergeParticipant.TabIndex = 0;
            m_btnStergeParticipant.Text = "Șterge selectat";
            m_btnStergeParticipant.Click += m_btnStergeParticipant_Click;
            // 
            // m_btnAsociazaCursant
            // 
            m_btnAsociazaCursant.Enabled = false;
            m_btnAsociazaCursant.Location = new Point(160, 9);
            m_btnAsociazaCursant.Margin = new Padding(3, 4, 3, 4);
            m_btnAsociazaCursant.Name = "m_btnAsociazaCursant";
            m_btnAsociazaCursant.Size = new Size(160, 33);
            m_btnAsociazaCursant.TabIndex = 1;
            m_btnAsociazaCursant.Text = "Asociază cursant...";
            m_btnAsociazaCursant.Click += m_btnAsociazaCursant_Click;
            // 
            // m_tabCursanti
            // 
            m_tabCursanti.Controls.Add(m_splitCursanti);
            m_tabCursanti.Location = new Point(4, 29);
            m_tabCursanti.Margin = new Padding(3, 4, 3, 4);
            m_tabCursanti.Name = "m_tabCursanti";
            m_tabCursanti.Size = new Size(221, 100);
            m_tabCursanti.TabIndex = 1;
            m_tabCursanti.Text = "Cursanți ONIXTEIA";
            m_tabCursanti.UseVisualStyleBackColor = true;
            // 
            // m_splitCursanti
            // 
            m_splitCursanti.Dock = DockStyle.Fill;
            m_splitCursanti.Location = new Point(0, 0);
            m_splitCursanti.Margin = new Padding(3, 4, 3, 4);
            m_splitCursanti.Name = "m_splitCursanti";
            m_splitCursanti.Orientation = Orientation.Horizontal;
            // 
            // m_splitCursanti.Panel1
            // 
            m_splitCursanti.Panel1.Controls.Add(m_gCursantiTurneu);
            // 
            // m_splitCursanti.Panel2
            // 
            m_splitCursanti.Panel2.Controls.Add(m_gRunde);
            m_splitCursanti.Size = new Size(221, 100);
            m_splitCursanti.SplitterDistance = 70;
            m_splitCursanti.SplitterWidth = 5;
            m_splitCursanti.TabIndex = 0;
            // 
            // m_gCursantiTurneu
            // 
            m_gCursantiTurneu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_gCursantiTurneu.Dock = DockStyle.Fill;
            m_gCursantiTurneu.Font = new Font("Arial", 8F);
            m_gCursantiTurneu.Location = new Point(0, 0);
            m_gCursantiTurneu.Margin = new Padding(3, 4, 3, 4);
            m_gCursantiTurneu.Name = "m_gCursantiTurneu";
            m_gCursantiTurneu.ReadOnly = true;
            m_gCursantiTurneu.RowHeadersWidth = 25;
            m_gCursantiTurneu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            m_gCursantiTurneu.Size = new Size(221, 70);
            m_gCursantiTurneu.TabIndex = 0;
            m_gCursantiTurneu.SelectionChanged += m_gCursantiTurneu_SelectionChanged;
            // 
            // m_gRunde
            // 
            m_gRunde.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_gRunde.Dock = DockStyle.Fill;
            m_gRunde.Font = new Font("Arial", 8F);
            m_gRunde.Location = new Point(0, 0);
            m_gRunde.Margin = new Padding(3, 4, 3, 4);
            m_gRunde.Name = "m_gRunde";
            m_gRunde.ReadOnly = true;
            m_gRunde.RowHeadersWidth = 25;
            m_gRunde.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            m_gRunde.Size = new Size(221, 25);
            m_gRunde.TabIndex = 0;
            // 
            // m_stCursant
            // 
            m_stCursant.AutoSize = true;
            m_stCursant.Location = new Point(0, 0);
            m_stCursant.Name = "m_stCursant";
            m_stCursant.Size = new Size(100, 23);
            m_stCursant.TabIndex = 0;
            m_stCursant.Visible = false;
            // 
            // m_cbCursant
            // 
            m_cbCursant.Location = new Point(0, 0);
            m_cbCursant.Name = "m_cbCursant";
            m_cbCursant.Size = new Size(121, 28);
            m_cbCursant.TabIndex = 0;
            m_cbCursant.Visible = false;
            // 
            // m_stPuncte
            // 
            m_stPuncte.Location = new Point(0, 0);
            m_stPuncte.Name = "m_stPuncte";
            m_stPuncte.Size = new Size(100, 23);
            m_stPuncte.TabIndex = 0;
            m_stPuncte.Visible = false;
            // 
            // m_ebPuncte
            // 
            m_ebPuncte.Location = new Point(0, 0);
            m_ebPuncte.Name = "m_ebPuncte";
            m_ebPuncte.Size = new Size(100, 27);
            m_ebPuncte.TabIndex = 0;
            m_ebPuncte.Visible = false;
            // 
            // m_stLoc
            // 
            m_stLoc.Location = new Point(0, 0);
            m_stLoc.Name = "m_stLoc";
            m_stLoc.Size = new Size(100, 23);
            m_stLoc.TabIndex = 0;
            m_stLoc.Visible = false;
            // 
            // m_ebLoc
            // 
            m_ebLoc.Location = new Point(0, 0);
            m_ebLoc.Name = "m_ebLoc";
            m_ebLoc.Size = new Size(100, 27);
            m_ebLoc.TabIndex = 0;
            m_ebLoc.Visible = false;
            // 
            // m_btnAdauga
            // 
            m_btnAdauga.Location = new Point(0, 0);
            m_btnAdauga.Name = "m_btnAdauga";
            m_btnAdauga.Size = new Size(75, 23);
            m_btnAdauga.TabIndex = 0;
            m_btnAdauga.Visible = false;
            // 
            // FrmGestiuneTurnee
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1333, 867);
            Controls.Add(m_tabMain);
            Controls.Add(m_panelDetalii);
            Controls.Add(toolStrip1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FrmGestiuneTurnee";
            Text = "Gestiune turneu";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            m_panelDetalii.ResumeLayout(false);
            m_panelDetalii.PerformLayout();
            m_tabMain.ResumeLayout(false);
            m_tabToti.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)m_gParticipanti).EndInit();
            m_panelAdauga.ResumeLayout(false);
            m_tabCursanti.ResumeLayout(false);
            m_splitCursanti.Panel1.ResumeLayout(false);
            m_splitCursanti.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)m_splitCursanti).EndInit();
            m_splitCursanti.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)m_gCursantiTurneu).EndInit();
            ((System.ComponentModel.ISupportInitialize)m_gRunde).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton m_tsbSalveaza;
        private ToolStripButton m_tsbImport;
        private Panel m_panelDetalii;
        private Label m_stData;
        private DateTimePicker m_dtpData;
        private Label m_stDenumire;
        private TextBox m_ebDenumire;
        private Label m_stLocalitate;
        private TextBox m_ebLocalitate;
        private TabControl m_tabMain;
        private TabPage m_tabToti;
        private Panel m_panelAdauga;
        private Label m_stCursant;
        private ComboBox m_cbCursant;
        private Label m_stTip;
        private ComboBox m_cbTip;
        private Label m_stPuncte;
        private TextBox m_ebPuncte;
        private Label m_stLoc;
        private TextBox m_ebLoc;
        private Button m_btnAdauga;
        private Button m_btnStergeParticipant;
        private Button m_btnAsociazaCursant;
        private CustomGrid m_gParticipanti;
        private TabPage m_tabCursanti;
        private SplitContainer m_splitCursanti;
        private CustomGrid m_gCursantiTurneu;
        private CustomGrid m_gRunde;
        private Label m_stOrganizator;
        private TextBox m_ebOrganizator;
        private Label m_stDirector;
        private TextBox m_ebDirector;
        private Label m_stArbSef;
        private TextBox m_ebArbSef;
        private Label m_stCadenta;
        private TextBox m_ebCadenta;
    }
}

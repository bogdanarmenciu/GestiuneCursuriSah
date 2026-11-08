using GridPersonalizat;

namespace GestiunePrezente.MeniuAdministrare
{
    partial class FrmCautareSolicitari
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
            m_tsbCautare = new ToolStripButton();
            m_tsbNou = new ToolStripButton();
            m_tsbModifica = new ToolStripButton();
            m_tsbStergere = new ToolStripButton();
            m_panelFiltru = new Panel();
            m_lblNume = new Label();
            m_ebNume = new TextBox();
            m_lblTelefon = new Label();
            m_ebTelefon = new TextBox();
            m_lblStatus = new Label();
            m_cbStatus = new ComboBox();
            m_lblSursa = new Label();
            m_cbSursa = new ComboBox();
            m_ckFiltruData = new CheckBox();
            m_lblDeLa = new Label();
            m_dtpDeLa = new DateTimePicker();
            m_lblPanaLa = new Label();
            m_dtpPanaLa = new DateTimePicker();
            m_gSolicitari = new CustomGrid();
            m_toolStrip.SuspendLayout();
            m_panelFiltru.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_gSolicitari).BeginInit();
            SuspendLayout();
            // 
            // m_toolStrip
            // 
            m_toolStrip.ImageScalingSize = new Size(20, 20);
            m_toolStrip.Items.AddRange(new ToolStripItem[] { m_tsbCautare, m_tsbNou, m_tsbModifica, m_tsbStergere });
            m_toolStrip.Location = new Point(0, 0);
            m_toolStrip.Name = "m_toolStrip";
            m_toolStrip.Size = new Size(1200, 27);
            m_toolStrip.TabIndex = 2;
            // 
            // m_tsbCautare
            // 
            m_tsbCautare.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            m_tsbCautare.Image = Properties.Resources.search;
            m_tsbCautare.Name = "m_tsbCautare";
            m_tsbCautare.Text = "Caută";
            m_tsbCautare.Click += m_tsbCautare_Click;
            //
            // m_tsbNou
            //
            m_tsbNou.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            m_tsbNou.Image = Properties.Resources._new;
            m_tsbNou.Name = "m_tsbNou";
            m_tsbNou.Text = "Nou";
            m_tsbNou.Click += m_tsbNou_Click;
            //
            // m_tsbModifica
            //
            m_tsbModifica.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            m_tsbModifica.Image = Properties.Resources.open;
            m_tsbModifica.Name = "m_tsbModifica";
            m_tsbModifica.Text = "Modifică";
            m_tsbModifica.Click += m_tsbModifica_Click;
            //
            // m_tsbStergere
            //
            m_tsbStergere.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            m_tsbStergere.Image = Properties.Resources.Delete;
            m_tsbStergere.Name = "m_tsbStergere";
            m_tsbStergere.Text = "Șterge";
            m_tsbStergere.Click += m_tsbStergere_Click;
            // 
            // m_panelFiltru
            // 
            m_panelFiltru.Controls.Add(m_lblNume);
            m_panelFiltru.Controls.Add(m_ebNume);
            m_panelFiltru.Controls.Add(m_lblTelefon);
            m_panelFiltru.Controls.Add(m_ebTelefon);
            m_panelFiltru.Controls.Add(m_lblStatus);
            m_panelFiltru.Controls.Add(m_cbStatus);
            m_panelFiltru.Controls.Add(m_lblSursa);
            m_panelFiltru.Controls.Add(m_cbSursa);
            m_panelFiltru.Controls.Add(m_ckFiltruData);
            m_panelFiltru.Controls.Add(m_lblDeLa);
            m_panelFiltru.Controls.Add(m_dtpDeLa);
            m_panelFiltru.Controls.Add(m_lblPanaLa);
            m_panelFiltru.Controls.Add(m_dtpPanaLa);
            m_panelFiltru.Dock = DockStyle.Top;
            m_panelFiltru.Location = new Point(0, 27);
            m_panelFiltru.Name = "m_panelFiltru";
            m_panelFiltru.Size = new Size(1200, 82);
            m_panelFiltru.TabIndex = 1;
            // 
            // m_lblNume
            // 
            m_lblNume.AutoSize = true;
            m_lblNume.Location = new Point(8, 14);
            m_lblNume.Name = "m_lblNume";
            m_lblNume.Size = new Size(89, 20);
            m_lblNume.TabIndex = 0;
            m_lblNume.Text = "Nume copil:";
            // 
            // m_ebNume
            // 
            m_ebNume.Location = new Point(99, 10);
            m_ebNume.Name = "m_ebNume";
            m_ebNume.Size = new Size(160, 27);
            m_ebNume.TabIndex = 1;
            m_ebNume.TextChanged += m_filtru_Changed;
            // 
            // m_lblTelefon
            // 
            m_lblTelefon.AutoSize = true;
            m_lblTelefon.Location = new Point(265, 14);
            m_lblTelefon.Name = "m_lblTelefon";
            m_lblTelefon.Size = new Size(61, 20);
            m_lblTelefon.TabIndex = 2;
            m_lblTelefon.Text = "Telefon:";
            // 
            // m_ebTelefon
            // 
            m_ebTelefon.Location = new Point(330, 10);
            m_ebTelefon.Name = "m_ebTelefon";
            m_ebTelefon.Size = new Size(130, 27);
            m_ebTelefon.TabIndex = 3;
            m_ebTelefon.TextChanged += m_filtru_Changed;
            // 
            // m_lblStatus
            // 
            m_lblStatus.AutoSize = true;
            m_lblStatus.Location = new Point(470, 14);
            m_lblStatus.Name = "m_lblStatus";
            m_lblStatus.Size = new Size(52, 20);
            m_lblStatus.TabIndex = 4;
            m_lblStatus.Text = "Status:";
            // 
            // m_cbStatus
            // 
            m_cbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            m_cbStatus.Location = new Point(520, 10);
            m_cbStatus.Name = "m_cbStatus";
            m_cbStatus.Size = new Size(140, 28);
            m_cbStatus.TabIndex = 5;
            m_cbStatus.SelectedIndexChanged += m_filtru_Changed;
            // 
            // m_lblSursa
            // 
            m_lblSursa.AutoSize = true;
            m_lblSursa.Location = new Point(675, 14);
            m_lblSursa.Name = "m_lblSursa";
            m_lblSursa.Size = new Size(47, 20);
            m_lblSursa.TabIndex = 6;
            m_lblSursa.Text = "Sursă:";
            // 
            // m_cbSursa
            // 
            m_cbSursa.DropDownStyle = ComboBoxStyle.DropDownList;
            m_cbSursa.Location = new Point(720, 10);
            m_cbSursa.Name = "m_cbSursa";
            m_cbSursa.Size = new Size(130, 28);
            m_cbSursa.TabIndex = 7;
            m_cbSursa.SelectedIndexChanged += m_filtru_Changed;
            // 
            // m_ckFiltruData
            // 
            m_ckFiltruData.AutoSize = true;
            m_ckFiltruData.Location = new Point(8, 48);
            m_ckFiltruData.Name = "m_ckFiltruData";
            m_ckFiltruData.Size = new Size(92, 24);
            m_ckFiltruData.TabIndex = 8;
            m_ckFiltruData.Text = "Perioadă:";
            m_ckFiltruData.CheckedChanged += m_ckFiltruData_CheckedChanged;
            // 
            // m_lblDeLa
            // 
            m_lblDeLa.AutoSize = true;
            m_lblDeLa.Location = new Point(110, 48);
            m_lblDeLa.Name = "m_lblDeLa";
            m_lblDeLa.Size = new Size(47, 20);
            m_lblDeLa.TabIndex = 9;
            m_lblDeLa.Text = "De la:";
            // 
            // m_dtpDeLa
            // 
            m_dtpDeLa.CustomFormat = "dd.MM.yyyy";
            m_dtpDeLa.Enabled = false;
            m_dtpDeLa.Format = DateTimePickerFormat.Custom;
            m_dtpDeLa.Location = new Point(155, 44);
            m_dtpDeLa.Name = "m_dtpDeLa";
            m_dtpDeLa.Size = new Size(140, 27);
            m_dtpDeLa.TabIndex = 10;
            m_dtpDeLa.ValueChanged += m_filtru_Changed;
            // 
            // m_lblPanaLa
            // 
            m_lblPanaLa.AutoSize = true;
            m_lblPanaLa.Location = new Point(308, 48);
            m_lblPanaLa.Name = "m_lblPanaLa";
            m_lblPanaLa.Size = new Size(61, 20);
            m_lblPanaLa.TabIndex = 11;
            m_lblPanaLa.Text = "până la:";
            // 
            // m_dtpPanaLa
            // 
            m_dtpPanaLa.CustomFormat = "dd.MM.yyyy";
            m_dtpPanaLa.Enabled = false;
            m_dtpPanaLa.Format = DateTimePickerFormat.Custom;
            m_dtpPanaLa.Location = new Point(360, 44);
            m_dtpPanaLa.Name = "m_dtpPanaLa";
            m_dtpPanaLa.Size = new Size(140, 27);
            m_dtpPanaLa.TabIndex = 12;
            m_dtpPanaLa.ValueChanged += m_filtru_Changed;
            // 
            // m_gSolicitari
            // 
            m_gSolicitari.ColumnHeadersHeight = 29;
            m_gSolicitari.Dock = DockStyle.Fill;
            m_gSolicitari.Font = new Font("Arial", 8F);
            m_gSolicitari.Location = new Point(0, 109);
            m_gSolicitari.Name = "m_gSolicitari";
            m_gSolicitari.ReadOnly = true;
            m_gSolicitari.RowHeadersWidth = 51;
            m_gSolicitari.Size = new Size(1200, 591);
            m_gSolicitari.TabIndex = 0;
            m_gSolicitari.DoubleClick += m_gSolicitari_DoubleClick;
            // 
            // FrmCautareSolicitari
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 700);
            Controls.Add(m_gSolicitari);
            Controls.Add(m_panelFiltru);
            Controls.Add(m_toolStrip);
            Name = "FrmCautareSolicitari";
            Text = "Solicitări";
            Load += FrmCautareSolicitari_Load;
            m_toolStrip.ResumeLayout(false);
            m_toolStrip.PerformLayout();
            m_panelFiltru.ResumeLayout(false);
            m_panelFiltru.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)m_gSolicitari).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private ToolStrip m_toolStrip;
        private ToolStripButton m_tsbCautare, m_tsbNou, m_tsbModifica, m_tsbStergere;
        private Panel m_panelFiltru;
        private Label m_lblNume, m_lblTelefon, m_lblStatus, m_lblSursa, m_lblDeLa, m_lblPanaLa;
        private TextBox m_ebNume, m_ebTelefon;
        private ComboBox m_cbStatus, m_cbSursa;
        private CheckBox m_ckFiltruData;
        private DateTimePicker m_dtpDeLa, m_dtpPanaLa;
        private CustomGrid m_gSolicitari;
    }
}

using GridPersonalizat;

namespace GestiunePrezente.MeniuCursanti
{
    partial class FrmSituatieCentralizata
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
            m_splitMain    = new SplitContainer();
            m_panelFiltru  = new Panel();
            m_ckActivi     = new CheckBox();
            m_ebPrenume    = new TextBox();
            label2         = new Label();
            m_ebNume       = new TextBox();
            label1         = new Label();
            m_gCursanti    = new CustomGrid();
            m_panelDreapta = new Panel();
            m_panelTop     = new Panel();
            m_flowTop      = new FlowLayoutPanel();
            m_lbNume       = new Label();
            m_lbSep        = new Label();
            m_lbV          = new Label();
            m_lbR          = new Label();
            m_lbI          = new Label();
            m_lbStats      = new Label();
            m_gPartide     = new CustomGrid();
            ((System.ComponentModel.ISupportInitialize)m_splitMain).BeginInit();
            m_splitMain.Panel1.SuspendLayout();
            m_splitMain.Panel2.SuspendLayout();
            m_splitMain.SuspendLayout();
            m_panelFiltru.SuspendLayout();
            m_panelDreapta.SuspendLayout();
            m_panelTop.SuspendLayout();
            m_flowTop.SuspendLayout();
            SuspendLayout();
            //
            // m_splitMain
            //
            m_splitMain.Dock = DockStyle.Fill;
            m_splitMain.Location = new Point(0, 0);
            m_splitMain.Name = "m_splitMain";
            m_splitMain.SplitterDistance = 420;
            m_splitMain.Size = new Size(1200, 700);
            m_splitMain.TabIndex = 0;
            //
            // m_splitMain.Panel1  (stânga)
            //
            m_splitMain.Panel1.Controls.Add(m_gCursanti);
            m_splitMain.Panel1.Controls.Add(m_panelFiltru);
            //
            // m_splitMain.Panel2  (dreapta)
            //
            m_splitMain.Panel2.Controls.Add(m_panelDreapta);
            //
            // m_panelFiltru
            //
            m_panelFiltru.Controls.Add(m_ckActivi);
            m_panelFiltru.Controls.Add(m_ebPrenume);
            m_panelFiltru.Controls.Add(label2);
            m_panelFiltru.Controls.Add(m_ebNume);
            m_panelFiltru.Controls.Add(label1);
            m_panelFiltru.Dock = DockStyle.Top;
            m_panelFiltru.Height = 34;
            m_panelFiltru.Name = "m_panelFiltru";
            //
            // label1
            //
            label1.AutoSize = true;
            label1.Location = new Point(4, 8);
            label1.Name = "label1";
            label1.Text = "Nume:";
            //
            // m_ebNume
            //
            m_ebNume.Location = new Point(48, 5);
            m_ebNume.Name = "m_ebNume";
            m_ebNume.Size = new Size(100, 23);
            m_ebNume.TabIndex = 0;
            m_ebNume.TextChanged += m_ebNume_TextChanged;
            //
            // label2
            //
            label2.AutoSize = true;
            label2.Location = new Point(155, 8);
            label2.Name = "label2";
            label2.Text = "Prenume:";
            //
            // m_ebPrenume
            //
            m_ebPrenume.Location = new Point(210, 5);
            m_ebPrenume.Name = "m_ebPrenume";
            m_ebPrenume.Size = new Size(100, 23);
            m_ebPrenume.TabIndex = 1;
            m_ebPrenume.TextChanged += m_ebPrenume_TextChanged;
            //
            // m_ckActivi
            //
            m_ckActivi.AutoSize = true;
            m_ckActivi.Checked = true;
            m_ckActivi.CheckState = CheckState.Checked;
            m_ckActivi.Location = new Point(320, 7);
            m_ckActivi.Name = "m_ckActivi";
            m_ckActivi.Text = "Doar activi";
            m_ckActivi.TabIndex = 2;
            m_ckActivi.CheckedChanged += m_ckActivi_CheckedChanged;
            //
            // m_gCursanti
            //
            m_gCursanti.Dock = DockStyle.Fill;
            m_gCursanti.Name = "m_gCursanti";
            m_gCursanti.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            m_gCursanti.SelectionChanged += m_gCursanti_SelectionChanged;
            //
            // m_panelDreapta
            //
            m_panelDreapta.Controls.Add(m_gPartide);
            m_panelDreapta.Controls.Add(m_panelTop);
            m_panelDreapta.Dock = DockStyle.Fill;
            m_panelDreapta.Name = "m_panelDreapta";
            //
            // m_panelTop  (un singur rând: nume + statistici)
            //
            m_panelTop.Controls.Add(m_flowTop);
            m_panelTop.Dock = DockStyle.Top;
            m_panelTop.Height = 32;
            m_panelTop.Name = "m_panelTop";
            //
            // m_flowTop
            //
            m_flowTop.Controls.Add(m_lbNume);
            m_flowTop.Controls.Add(m_lbSep);
            m_flowTop.Controls.Add(m_lbV);
            m_flowTop.Controls.Add(m_lbR);
            m_flowTop.Controls.Add(m_lbI);
            m_flowTop.Controls.Add(m_lbStats);
            m_flowTop.Dock = DockStyle.Fill;
            m_flowTop.FlowDirection = FlowDirection.LeftToRight;
            m_flowTop.WrapContents = false;
            m_flowTop.Name = "m_flowTop";
            m_flowTop.Padding = new Padding(2, 4, 0, 0);
            //
            // m_lbNume  (numele cursantului, bold)
            //
            m_lbNume.AutoSize = true;
            m_lbNume.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            m_lbNume.Name = "m_lbNume";
            m_lbNume.Margin = new Padding(2, 1, 6, 0);
            m_lbNume.Text = "";
            //
            // m_lbSep
            //
            m_lbSep.AutoSize = true;
            m_lbSep.ForeColor = SystemColors.GrayText;
            m_lbSep.Name = "m_lbSep";
            m_lbSep.Margin = new Padding(0, 1, 6, 0);
            m_lbSep.Text = "│";
            m_lbSep.Visible = false;
            //
            // m_lbV  (Victorii — verde)
            //
            m_lbV.AutoSize = true;
            m_lbV.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            m_lbV.ForeColor = Color.FromArgb(0, 140, 0);
            m_lbV.Name = "m_lbV";
            m_lbV.Margin = new Padding(0, 1, 8, 0);
            m_lbV.Text = "";
            //
            // m_lbR  (Remize — portocaliu)
            //
            m_lbR.AutoSize = true;
            m_lbR.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            m_lbR.ForeColor = Color.FromArgb(180, 100, 0);
            m_lbR.Name = "m_lbR";
            m_lbR.Margin = new Padding(0, 1, 8, 0);
            m_lbR.Text = "";
            //
            // m_lbI  (Înfrângeri — roșu)
            //
            m_lbI.AutoSize = true;
            m_lbI.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            m_lbI.ForeColor = Color.FromArgb(180, 0, 0);
            m_lbI.Name = "m_lbI";
            m_lbI.Margin = new Padding(0, 1, 12, 0);
            m_lbI.Text = "";
            //
            // m_lbStats  (restul statisticilor — text normal)
            //
            m_lbStats.AutoSize = true;
            m_lbStats.Name = "m_lbStats";
            m_lbStats.Margin = new Padding(0, 1, 0, 0);
            m_lbStats.Text = "";
            //
            // m_gPartide
            //
            m_gPartide.Dock = DockStyle.Fill;
            m_gPartide.Name = "m_gPartide";
            //
            // FrmSituatieCentralizata
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 700);
            Controls.Add(m_splitMain);
            Name = "FrmSituatieCentralizata";
            Text = "Situatie centralizata partide";
            WindowState = FormWindowState.Maximized;
            m_splitMain.Panel1.ResumeLayout(false);
            m_splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)m_splitMain).EndInit();
            m_splitMain.ResumeLayout(false);
            m_panelFiltru.ResumeLayout(false);
            m_panelFiltru.PerformLayout();
            m_panelTop.ResumeLayout(false);
            m_panelTop.PerformLayout();
            m_flowTop.ResumeLayout(false);
            m_flowTop.PerformLayout();
            m_panelDreapta.ResumeLayout(false);
            m_panelDreapta.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer   m_splitMain;
        private Panel            m_panelFiltru;
        private Label            label1;
        private TextBox          m_ebNume;
        private Label            label2;
        private TextBox          m_ebPrenume;
        private CheckBox         m_ckActivi;
        private CustomGrid       m_gCursanti;
        private Panel            m_panelDreapta;
        private Panel            m_panelTop;
        private FlowLayoutPanel  m_flowTop;
        private Label            m_lbNume;
        private Label            m_lbSep;
        private Label            m_lbV;
        private Label            m_lbR;
        private Label            m_lbI;
        private Label            m_lbStats;
        private CustomGrid       m_gPartide;
    }
}

namespace GestiunePrezente.MeniuConfigurare
{
    partial class FrmGestiuneUtilizator
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            m_toolStrip         = new ToolStrip();
            m_tsbSalveaza       = new ToolStripButton();
            m_gbDate            = new GroupBox();
            m_lblNumePrenume    = new Label();
            m_ebNumePrenume     = new TextBox();
            m_lblLogin          = new Label();
            m_ebLogin           = new TextBox();
            m_lblParola         = new Label();
            m_ebParola          = new TextBox();
            m_lblEmail          = new Label();
            m_ebEmail           = new TextBox();
            m_ckActiv           = new CheckBox();
            m_lblConectare      = new Label();
            m_cbConectare       = new ComboBox();
            m_lblDivImplicita   = new Label();
            m_cbDivImplicita    = new ComboBox();
            m_gbRoluri          = new GroupBox();
            m_gRoluri           = new DataGridView();

            m_toolStrip.SuspendLayout();
            m_gbDate.SuspendLayout();
            m_gbRoluri.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_gRoluri).BeginInit();
            SuspendLayout();

            // ToolStrip
            m_toolStrip.Items.AddRange(new ToolStripItem[] { m_tsbSalveaza });
            m_toolStrip.Dock = DockStyle.Top;
            m_tsbSalveaza.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            m_tsbSalveaza.Image = Properties.Resources.save;
            m_tsbSalveaza.Text = "Salvează";
            m_tsbSalveaza.Enabled = false;
            m_tsbSalveaza.Click += m_tsbSalveaza_Click;

            // GroupBox Date
            m_gbDate.Text = "Date utilizator";
            m_gbDate.Dock = DockStyle.Top;
            m_gbDate.Height = 200;
            m_gbDate.Padding = new Padding(4);

            m_lblNumePrenume.AutoSize = true;
            m_lblNumePrenume.Location = new Point(10, 26);
            m_lblNumePrenume.Text = "Nume complet:";
            m_ebNumePrenume.Location = new Point(130, 23);
            m_ebNumePrenume.Size = new Size(320, 27);
            m_ebNumePrenume.TextChanged += m_eb_Changed;

            m_lblLogin.AutoSize = true;
            m_lblLogin.Location = new Point(10, 62);
            m_lblLogin.Text = "Login:";
            m_ebLogin.Location = new Point(130, 59);
            m_ebLogin.Size = new Size(200, 27);
            m_ebLogin.TextChanged += m_eb_Changed;

            m_lblParola.AutoSize = true;
            m_lblParola.Location = new Point(10, 98);
            m_lblParola.Text = "Parolă:";
            m_ebParola.Location = new Point(130, 95);
            m_ebParola.Size = new Size(200, 27);
            m_ebParola.PasswordChar = '●';
            m_ebParola.TextChanged += m_eb_Changed;

            m_lblEmail.AutoSize = true;
            m_lblEmail.Location = new Point(10, 134);
            m_lblEmail.Text = "Email:";
            m_ebEmail.Location = new Point(130, 131);
            m_ebEmail.Size = new Size(320, 27);
            m_ebEmail.TextChanged += m_eb_Changed;

            m_ckActiv.AutoSize = true;
            m_ckActiv.Location = new Point(130, 163);
            m_ckActiv.Text = "Utilizator activ";
            m_ckActiv.CheckedChanged += m_eb_Changed;

            m_lblConectare.AutoSize = true;
            m_lblConectare.Location = new Point(10, 196);
            m_lblConectare.Text = "Conectare la:";
            m_cbConectare.Location = new Point(130, 193);
            m_cbConectare.Size = new Size(260, 27);
            m_cbConectare.DropDownStyle = ComboBoxStyle.DropDownList;
            m_cbConectare.SelectedIndexChanged += m_eb_Changed;

            m_lblDivImplicita.AutoSize = true;
            m_lblDivImplicita.Location = new Point(10, 232);
            m_lblDivImplicita.Text = "Div. implicită:";
            m_cbDivImplicita.Location = new Point(130, 229);
            m_cbDivImplicita.Size = new Size(260, 27);
            m_cbDivImplicita.DropDownStyle = ComboBoxStyle.DropDownList;
            m_cbDivImplicita.SelectedIndexChanged += m_eb_Changed;

            m_gbDate.Height = 270;
            m_gbDate.Controls.AddRange(new Control[] {
                m_lblNumePrenume, m_ebNumePrenume,
                m_lblLogin,       m_ebLogin,
                m_lblParola,      m_ebParola,
                m_lblEmail,       m_ebEmail,
                m_ckActiv,
                m_lblConectare,   m_cbConectare,
                m_lblDivImplicita, m_cbDivImplicita });

            // GroupBox Roluri
            m_gbRoluri.Text = "Roluri per diviziune";
            m_gbRoluri.Dock = DockStyle.Fill;

            m_gRoluri.Dock = DockStyle.Fill;
            m_gRoluri.AllowUserToAddRows = false;
            m_gRoluri.AllowUserToDeleteRows = false;
            m_gRoluri.RowHeadersVisible = false;
            m_gRoluri.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_gRoluri.CellValueChanged += m_gRoluri_CellValueChanged;
            m_gRoluri.CurrentCellDirtyStateChanged += m_gRoluri_CurrentCellDirtyStateChanged;

            m_gbRoluri.Controls.Add(m_gRoluri);

            // Form
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 520);
            MinimumSize = new Size(600, 460);
            StartPosition = FormStartPosition.CenterParent;
            // Ordinea contează pentru Dock: Fill se adaugă primul
            Controls.Add(m_gbRoluri);
            Controls.Add(m_gbDate);
            Controls.Add(m_toolStrip);
            Name = "FrmGestiuneUtilizator";
            Text = "Utilizator";
            Load += FrmGestiuneUtilizator_Load;

            m_toolStrip.ResumeLayout(false); m_toolStrip.PerformLayout();
            m_gbDate.ResumeLayout(false); m_gbDate.PerformLayout();
            m_gbRoluri.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)m_gRoluri).EndInit();
            ResumeLayout(false); PerformLayout();
        }

        private ToolStrip m_toolStrip;
        private ToolStripButton m_tsbSalveaza;
        private GroupBox m_gbDate, m_gbRoluri;
        private Label m_lblNumePrenume, m_lblLogin, m_lblParola, m_lblEmail;
        private Label m_lblConectare, m_lblDivImplicita;
        private TextBox m_ebNumePrenume, m_ebLogin, m_ebParola, m_ebEmail;
        private CheckBox m_ckActiv;
        private ComboBox m_cbConectare, m_cbDivImplicita;
        private DataGridView m_gRoluri;
    }
}

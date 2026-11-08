using GridPersonalizat;

namespace GestiunePrezente.Common
{
    partial class FrmAjutorCursantiPrezenta
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
            m_tsbCautare = new ToolStripButton();
            m_tsbSalveaza = new ToolStripButton();
            m_tsbModificaNrPrezente = new ToolStripButton();
            panel1 = new Panel();
            m_cbAbonament = new CustomCombo();
            m_ebPrenume = new TextBox();
            m_ebTelMobil = new TextBox();
            m_ebNume = new TextBox();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label1 = new Label();
            m_gCursanti = new CustomGrid();
            toolStrip1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_gCursanti).BeginInit();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { m_tsbCautare, m_tsbSalveaza, m_tsbModificaNrPrezente });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Padding = new Padding(0, 0, 2, 0);
            toolStrip1.Size = new Size(1553, 27);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // m_tsbCautare
            // 
            m_tsbCautare.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbCautare.Image = Properties.Resources.search;
            m_tsbCautare.ImageTransparentColor = Color.Magenta;
            m_tsbCautare.Name = "m_tsbCautare";
            m_tsbCautare.Size = new Size(24, 24);
            m_tsbCautare.Text = "Cautare";
            m_tsbCautare.Click += m_tsbCautare_Click;
            // 
            // m_tsbSalveaza
            // 
            m_tsbSalveaza.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbSalveaza.Enabled = false;
            m_tsbSalveaza.Image = Properties.Resources.save;
            m_tsbSalveaza.ImageTransparentColor = Color.Magenta;
            m_tsbSalveaza.Name = "m_tsbSalveaza";
            m_tsbSalveaza.Size = new Size(24, 24);
            m_tsbSalveaza.Text = "Salveaza";
            m_tsbSalveaza.Click += m_tsbSalveaza_Click;
            // 
            // m_tsbModificaNrPrezente
            // 
            m_tsbModificaNrPrezente.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbModificaNrPrezente.Enabled = false;
            m_tsbModificaNrPrezente.Image = Properties.Resources.CursantArhiva;
            m_tsbModificaNrPrezente.ImageTransparentColor = Color.Magenta;
            m_tsbModificaNrPrezente.Name = "m_tsbModificaNrPrezente";
            m_tsbModificaNrPrezente.Size = new Size(24, 24);
            m_tsbModificaNrPrezente.Text = "Modifica număr prezențe";
            m_tsbModificaNrPrezente.Click += m_tsbModificaNrPrezente_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(m_cbAbonament);
            panel1.Controls.Add(m_ebPrenume);
            panel1.Controls.Add(m_ebTelMobil);
            panel1.Controls.Add(m_ebNume);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 27);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(1553, 70);
            panel1.TabIndex = 1;
            // 
            // m_cbAbonament
            // 
            m_cbAbonament.AutoCompleteMode = AutoCompleteMode.Append;
            m_cbAbonament.AutoCompleteSource = AutoCompleteSource.ListItems;
            m_cbAbonament.Font = new Font("Microsoft Sans Serif", 8F);
            m_cbAbonament.FormattingEnabled = true;
            m_cbAbonament.Location = new Point(872, 16);
            m_cbAbonament.Margin = new Padding(3, 2, 3, 2);
            m_cbAbonament.Name = "m_cbAbonament";
            m_cbAbonament.Size = new Size(269, 21);
            m_cbAbonament.TabIndex = 12;
            m_cbAbonament.SelectedIndexChanged += m_cbAbonament_SelectedIndexChanged;
            // 
            // m_ebPrenume
            // 
            m_ebPrenume.Location = new Point(335, 16);
            m_ebPrenume.Margin = new Padding(3, 2, 3, 2);
            m_ebPrenume.Name = "m_ebPrenume";
            m_ebPrenume.Size = new Size(154, 23);
            m_ebPrenume.TabIndex = 10;
            m_ebPrenume.TextChanged += m_ebNume_TextChanged;
            // 
            // m_ebTelMobil
            // 
            m_ebTelMobil.Location = new Point(606, 16);
            m_ebTelMobil.Margin = new Padding(3, 2, 3, 2);
            m_ebTelMobil.Name = "m_ebTelMobil";
            m_ebTelMobil.Size = new Size(154, 23);
            m_ebTelMobil.TabIndex = 9;
            // 
            // m_ebNume
            // 
            m_ebNume.Location = new Point(81, 16);
            m_ebNume.Margin = new Padding(3, 2, 3, 2);
            m_ebNume.Name = "m_ebNume";
            m_ebNume.Size = new Size(154, 23);
            m_ebNume.TabIndex = 7;
            m_ebNume.TextChanged += m_ebNume_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(783, 17);
            label5.Name = "label5";
            label5.Size = new Size(70, 15);
            label5.TabIndex = 4;
            label5.Text = "Abonament";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(512, 17);
            label4.Name = "label4";
            label4.Size = new Size(80, 15);
            label4.TabIndex = 3;
            label4.Text = "Telefon mobil";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(255, 17);
            label3.Name = "label3";
            label3.Size = new Size(55, 15);
            label3.TabIndex = 2;
            label3.Text = "Prenume";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 17);
            label1.Name = "label1";
            label1.Size = new Size(40, 15);
            label1.TabIndex = 0;
            label1.Text = "Nume";
            // 
            // m_gCursanti
            // 
            m_gCursanti.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_gCursanti.Dock = DockStyle.Fill;
            m_gCursanti.Font = new Font("Arial", 8F);
            m_gCursanti.Location = new Point(0, 97);
            m_gCursanti.Margin = new Padding(3, 2, 3, 2);
            m_gCursanti.Name = "m_gCursanti";
            m_gCursanti.ReadOnly = true;
            m_gCursanti.RowHeadersWidth = 51;
            m_gCursanti.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            m_gCursanti.Size = new Size(1553, 319);
            m_gCursanti.TabIndex = 2;
            m_gCursanti.DoubleClick += m_gCursanti_DoubleClick;
            m_gCursanti.KeyDown += m_gCursanti_KeyDown;
            // 
            // FrmAjutorCursantiPrezenta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1553, 416);
            Controls.Add(m_gCursanti);
            Controls.Add(panel1);
            Controls.Add(toolStrip1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "FrmAjutorCursantiPrezenta";
            Text = "Ajutor Cursanti Prezenta";
            WindowState = FormWindowState.Maximized;
            Load += FrmAjutorCursantiPrezenta_Load;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)m_gCursanti).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private Panel panel1;
        private GridPersonalizat.CustomGrid m_gCursanti;
        private ToolStripButton m_tsbCautare;
        private ToolStripButton m_tsbSalveaza;
        private ToolStripButton m_tsbModificaNrPrezente;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label1;
        private TextBox m_ebPrenume;
        private TextBox m_ebTelMobil;
        private TextBox m_ebNume;
        private CustomCombo m_cbAbonament;
    }
}
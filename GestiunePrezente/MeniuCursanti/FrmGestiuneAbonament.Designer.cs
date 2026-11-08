namespace GestiunePrezente.MeniuCursanti
{
    partial class FrmGestiuneAbonament
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
            m_ebAbonament = new TextBox();
            m_st1 = new Label();
            m_ebPerioadaZile = new TextBox();
            label2 = new Label();
            label3 = new Label();
            m_cbDurata = new GridPersonalizat.CustomCombo();
            m_chkSedintaIndividuala = new CheckBox();
            m_lblOrePerLuna = new Label();
            m_ebOrePerLuna = new TextBox();
            toolStrip1 = new ToolStrip();
            m_stbSalveaza = new ToolStripButton();
            m_lblPreturi = new Label();
            m_dgvPreturi = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)m_dgvPreturi).BeginInit();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            //
            // m_ebAbonament
            //
            m_ebAbonament.BackColor = Color.FromArgb(255, 255, 192);
            m_ebAbonament.Location = new Point(107, 39);
            m_ebAbonament.Margin = new Padding(4, 3, 4, 3);
            m_ebAbonament.Name = "m_ebAbonament";
            m_ebAbonament.Size = new Size(310, 23);
            m_ebAbonament.TabIndex = 1;
            m_ebAbonament.TextChanged += m_ebAbonament_TextChanged;
            //
            // m_st1
            //
            m_st1.AutoSize = true;
            m_st1.Location = new Point(13, 44);
            m_st1.Margin = new Padding(4, 0, 4, 0);
            m_st1.Name = "m_st1";
            m_st1.Size = new Size(70, 15);
            m_st1.TabIndex = 13;
            m_st1.Text = "Abonament";
            //
            // m_ebPerioadaZile
            //
            m_ebPerioadaZile.BackColor = Color.FromArgb(255, 255, 192);
            m_ebPerioadaZile.Location = new Point(107, 66);
            m_ebPerioadaZile.Margin = new Padding(4, 3, 4, 3);
            m_ebPerioadaZile.Name = "m_ebPerioadaZile";
            m_ebPerioadaZile.Size = new Size(103, 23);
            m_ebPerioadaZile.TabIndex = 31;
            m_ebPerioadaZile.TextAlign = HorizontalAlignment.Right;
            m_ebPerioadaZile.TextChanged += m_ebDurataZile_TextChanged;
            //
            // label2
            //
            label2.AutoSize = true;
            label2.Location = new Point(13, 69);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(53, 15);
            label2.TabIndex = 32;
            label2.Text = "Perioada";
            //
            // label3
            //
            label3.AutoSize = true;
            label3.Location = new Point(13, 95);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(42, 15);
            label3.TabIndex = 33;
            label3.Text = "Durata";
            //
            // m_cbDurata
            //
            m_cbDurata.AutoCompleteMode = AutoCompleteMode.Append;
            m_cbDurata.AutoCompleteSource = AutoCompleteSource.ListItems;
            m_cbDurata.BackColor = Color.FromArgb(255, 255, 192);
            m_cbDurata.Font = new Font("Microsoft Sans Serif", 8F);
            m_cbDurata.FormattingEnabled = true;
            m_cbDurata.Location = new Point(107, 93);
            m_cbDurata.Margin = new Padding(2, 1, 2, 1);
            m_cbDurata.Name = "m_cbDurata";
            m_cbDurata.Size = new Size(132, 21);
            m_cbDurata.TabIndex = 35;
            m_cbDurata.SelectedIndexChanged += m_cbTip_SelectedIndexChanged;
            //
            // m_chkSedintaIndividuala
            //
            m_chkSedintaIndividuala.AutoSize = true;
            m_chkSedintaIndividuala.Location = new Point(107, 121);
            m_chkSedintaIndividuala.Margin = new Padding(4, 3, 4, 3);
            m_chkSedintaIndividuala.Name = "m_chkSedintaIndividuala";
            m_chkSedintaIndividuala.Size = new Size(142, 19);
            m_chkSedintaIndividuala.TabIndex = 36;
            m_chkSedintaIndividuala.Text = "Sedinta individuala";
            m_chkSedintaIndividuala.UseVisualStyleBackColor = true;
            m_chkSedintaIndividuala.CheckedChanged += m_chkSedintaIndividuala_CheckedChanged;
            //
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { m_stbSalveaza });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Padding = new Padding(0, 0, 2, 0);
            toolStrip1.Size = new Size(591, 27);
            toolStrip1.TabIndex = 36;
            toolStrip1.Text = "toolStrip1";
            // 
            // m_stbSalveaza
            // 
            m_stbSalveaza.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_stbSalveaza.Image = Properties.Resources.save;
            m_stbSalveaza.ImageTransparentColor = Color.Magenta;
            m_stbSalveaza.Name = "m_stbSalveaza";
            m_stbSalveaza.Size = new Size(24, 24);
            m_stbSalveaza.Text = "Salveaza";
            m_stbSalveaza.Click += m_stbSalveaza_Click;
            //
            // m_lblOrePerLuna
            //
            m_lblOrePerLuna.AutoSize = true;
            m_lblOrePerLuna.Location = new Point(13, 148);
            m_lblOrePerLuna.Name = "m_lblOrePerLuna";
            m_lblOrePerLuna.Size = new Size(80, 15);
            m_lblOrePerLuna.TabIndex = 37;
            m_lblOrePerLuna.Text = "Ore/luna:";
            //
            // m_ebOrePerLuna
            //
            m_ebOrePerLuna.BackColor = Color.FromArgb(255, 255, 192);
            m_ebOrePerLuna.Location = new Point(107, 145);
            m_ebOrePerLuna.Name = "m_ebOrePerLuna";
            m_ebOrePerLuna.Size = new Size(103, 23);
            m_ebOrePerLuna.TabIndex = 38;
            m_ebOrePerLuna.TextAlign = HorizontalAlignment.Right;
            m_ebOrePerLuna.TextChanged += m_ebOrePerLuna_TextChanged;
            //
            // m_lblPreturi
            //
            m_lblPreturi.AutoSize = true;
            m_lblPreturi.Location = new Point(13, 175);
            m_lblPreturi.Name = "m_lblPreturi";
            m_lblPreturi.Size = new Size(120, 15);
            m_lblPreturi.Text = "Istoricul prețurilor:";
            //
            // m_dgvPreturi
            //
            m_dgvPreturi.AllowUserToAddRows = true;
            m_dgvPreturi.AllowUserToDeleteRows = true;
            m_dgvPreturi.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_dgvPreturi.Location = new Point(13, 195);
            m_dgvPreturi.Name = "m_dgvPreturi";
            m_dgvPreturi.RowHeadersWidth = 30;
            m_dgvPreturi.Size = new Size(560, 150);
            m_dgvPreturi.TabIndex = 40;
            m_dgvPreturi.CellValueChanged += m_dgvPreturi_CellValueChanged;
            m_dgvPreturi.UserDeletedRow += m_dgvPreturi_UserDeletedRow;
            //
            // FrmGestiuneAbonament
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(591, 367);
            Controls.Add(m_dgvPreturi);
            Controls.Add(m_lblPreturi);
            Controls.Add(m_ebOrePerLuna);
            Controls.Add(m_lblOrePerLuna);
            Controls.Add(toolStrip1);
            Controls.Add(m_chkSedintaIndividuala);
            Controls.Add(m_cbDurata);
            Controls.Add(label3);
            Controls.Add(m_ebPerioadaZile);
            Controls.Add(label2);
            Controls.Add(m_ebAbonament);
            Controls.Add(m_st1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "FrmGestiuneAbonament";
            Text = "Gestiune Abonament";
            FormClosing += FrmGestiuneAbonament_FormClosing;
            Load += FrmGestiuneAbonament_Load;
            ((System.ComponentModel.ISupportInitialize)m_dgvPreturi).EndInit();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox m_ebAbonament;
        private System.Windows.Forms.Label m_st1;
        private System.Windows.Forms.TextBox m_ebPerioadaZile;
        private System.Windows.Forms.Label label2;
        private Label label3;
        private GridPersonalizat.CustomCombo m_cbDurata;
        private ToolStrip toolStrip1;
        private ToolStripButton m_stbSalveaza;
        private Label m_lblPreturi;
        private DataGridView m_dgvPreturi;
        private CheckBox m_chkSedintaIndividuala;
        private Label m_lblOrePerLuna;
        private TextBox m_ebOrePerLuna;
    }
}
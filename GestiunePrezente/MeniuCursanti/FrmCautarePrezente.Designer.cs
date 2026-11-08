using GridPersonalizat;

namespace GestiunePrezente.MeniuCursanti
{
    partial class FrmCautarePrezente
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
            m_tsbNou = new ToolStripButton();
            m_tsbSterge = new ToolStripButton();
            panel1 = new Panel();
            label13 = new Label();
            m_cbFormator = new CustomCombo();
            groupBox1 = new GroupBox();
            m_ckActivareFiltruData = new CheckBox();
            m_dtpDeLaData = new DateTimePicker();
            label1 = new Label();
            label5 = new Label();
            m_dtpPanaLaData = new DateTimePicker();
            m_ebPrenumeCursant = new TextBox();
            label6 = new Label();
            m_ebNumeCursant = new TextBox();
            label8 = new Label();
            m_gPrezente = new CustomGrid();
            panel2 = new Panel();
            panel3 = new Panel();
            Prezente = new Label();
            panel4 = new Panel();
            m_gDetaliuPrezenta = new CustomGrid();
            toolStrip1.SuspendLayout();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_gPrezente).BeginInit();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_gDetaliuPrezenta).BeginInit();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { m_tsbCautare, m_tsbNou, m_tsbSterge });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Padding = new Padding(0, 0, 2, 0);
            toolStrip1.Size = new Size(1066, 27);
            toolStrip1.TabIndex = 3;
            toolStrip1.Text = "toolStrip1";
            // 
            // m_tsbCautare
            // 
            m_tsbCautare.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbCautare.Image = Properties.Resources.search;
            m_tsbCautare.ImageTransparentColor = Color.Magenta;
            m_tsbCautare.Name = "m_tsbCautare";
            m_tsbCautare.Size = new Size(29, 24);
            m_tsbCautare.Text = "Cautare";
            m_tsbCautare.Click += m_tsbCautare_Click;
            // 
            // m_tsbNou
            // 
            m_tsbNou.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbNou.Image = Properties.Resources._new;
            m_tsbNou.ImageTransparentColor = Color.Magenta;
            m_tsbNou.Name = "m_tsbNou";
            m_tsbNou.Size = new Size(29, 24);
            m_tsbNou.Text = "Nou";
            m_tsbNou.Click += m_tsbNou_Click;
            // 
            // m_tsbSterge
            // 
            m_tsbSterge.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbSterge.Image = Properties.Resources.Delete;
            m_tsbSterge.ImageTransparentColor = Color.Magenta;
            m_tsbSterge.Name = "m_tsbSterge";
            m_tsbSterge.Size = new Size(29, 24);
            m_tsbSterge.Text = "Sterge";
            m_tsbSterge.Click += m_tsbSterge_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(label13);
            panel1.Controls.Add(m_cbFormator);
            panel1.Controls.Add(groupBox1);
            panel1.Controls.Add(m_ebPrenumeCursant);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(m_ebNumeCursant);
            panel1.Controls.Add(label8);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 27);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(1066, 221);
            panel1.TabIndex = 4;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(72, 180);
            label13.Name = "label13";
            label13.Size = new Size(70, 20);
            label13.TabIndex = 66;
            label13.Text = "Formator";
            // 
            // m_cbFormator
            // 
            m_cbFormator.AutoCompleteMode = AutoCompleteMode.Append;
            m_cbFormator.AutoCompleteSource = AutoCompleteSource.ListItems;
            m_cbFormator.Font = new Font("Microsoft Sans Serif", 8F);
            m_cbFormator.FormattingEnabled = true;
            m_cbFormator.Location = new Point(208, 180);
            m_cbFormator.Margin = new Padding(3, 4, 3, 4);
            m_cbFormator.Name = "m_cbFormator";
            m_cbFormator.Size = new Size(307, 24);
            m_cbFormator.TabIndex = 65;
            m_cbFormator.SelectedIndexChanged += m_cbFormator_SelectedIndexChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(m_ckActivareFiltruData);
            groupBox1.Controls.Add(m_dtpDeLaData);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(m_dtpPanaLaData);
            groupBox1.Location = new Point(50, 20);
            groupBox1.Margin = new Padding(3, 4, 3, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 4, 3, 4);
            groupBox1.Size = new Size(681, 99);
            groupBox1.TabIndex = 48;
            groupBox1.TabStop = false;
            groupBox1.Text = "Filtru data";
            // 
            // m_ckActivareFiltruData
            // 
            m_ckActivareFiltruData.AutoSize = true;
            m_ckActivareFiltruData.Location = new Point(51, 32);
            m_ckActivareFiltruData.Margin = new Padding(3, 4, 3, 4);
            m_ckActivareFiltruData.Name = "m_ckActivareFiltruData";
            m_ckActivareFiltruData.Size = new Size(154, 24);
            m_ckActivareFiltruData.TabIndex = 11;
            m_ckActivareFiltruData.Text = "Activare filtru data";
            m_ckActivareFiltruData.UseVisualStyleBackColor = true;
            // 
            // m_dtpDeLaData
            // 
            m_dtpDeLaData.Location = new Point(402, 28);
            m_dtpDeLaData.Margin = new Padding(3, 4, 3, 4);
            m_dtpDeLaData.Name = "m_dtpDeLaData";
            m_dtpDeLaData.Size = new Size(266, 27);
            m_dtpDeLaData.TabIndex = 12;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(277, 32);
            label1.Name = "label1";
            label1.Size = new Size(78, 20);
            label1.TabIndex = 39;
            label1.Text = "De la data";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(274, 64);
            label5.Name = "label5";
            label5.Size = new Size(90, 20);
            label5.TabIndex = 40;
            label5.Text = "Pana la data";
            // 
            // m_dtpPanaLaData
            // 
            m_dtpPanaLaData.Location = new Point(402, 61);
            m_dtpPanaLaData.Margin = new Padding(3, 4, 3, 4);
            m_dtpPanaLaData.Name = "m_dtpPanaLaData";
            m_dtpPanaLaData.Size = new Size(266, 27);
            m_dtpPanaLaData.TabIndex = 13;
            // 
            // m_ebPrenumeCursant
            // 
            m_ebPrenumeCursant.Location = new Point(561, 141);
            m_ebPrenumeCursant.Margin = new Padding(3, 4, 3, 4);
            m_ebPrenumeCursant.Name = "m_ebPrenumeCursant";
            m_ebPrenumeCursant.Size = new Size(170, 27);
            m_ebPrenumeCursant.TabIndex = 44;
            m_ebPrenumeCursant.TextChanged += m_ebPrenumeCursant_TextChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(415, 148);
            label6.Name = "label6";
            label6.Size = new Size(120, 20);
            label6.TabIndex = 45;
            label6.Text = "Prenume Cursant";
            // 
            // m_ebNumeCursant
            // 
            m_ebNumeCursant.Location = new Point(208, 141);
            m_ebNumeCursant.Margin = new Padding(3, 4, 3, 4);
            m_ebNumeCursant.Name = "m_ebNumeCursant";
            m_ebNumeCursant.Size = new Size(153, 27);
            m_ebNumeCursant.TabIndex = 43;
            m_ebNumeCursant.TextChanged += m_ebNumeCursant_TextChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(72, 148);
            label8.Name = "label8";
            label8.Size = new Size(102, 20);
            label8.TabIndex = 46;
            label8.Text = "Nume Cursant";
            // 
            // m_gPrezente
            // 
            m_gPrezente.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_gPrezente.Dock = DockStyle.Fill;
            m_gPrezente.Font = new Font("Arial", 8F);
            m_gPrezente.Location = new Point(0, 0);
            m_gPrezente.Margin = new Padding(3, 4, 3, 4);
            m_gPrezente.Name = "m_gPrezente";
            m_gPrezente.ReadOnly = true;
            m_gPrezente.RowHeadersWidth = 82;
            m_gPrezente.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            m_gPrezente.Size = new Size(1066, 244);
            m_gPrezente.TabIndex = 5;
            m_gPrezente.CellMouseDown += m_gPrezente_CellMouseDown;
            m_gPrezente.DoubleClick += m_gPrezente_DoubleClick;
            // 
            // panel2
            // 
            panel2.Controls.Add(m_gPrezente);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 248);
            panel2.Margin = new Padding(3, 4, 3, 4);
            panel2.Name = "panel2";
            panel2.Size = new Size(1066, 244);
            panel2.TabIndex = 5;
            // 
            // panel3
            // 
            panel3.Controls.Add(Prezente);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 492);
            panel3.Margin = new Padding(3, 4, 3, 4);
            panel3.Name = "panel3";
            panel3.Size = new Size(1066, 33);
            panel3.TabIndex = 6;
            // 
            // Prezente
            // 
            Prezente.AutoSize = true;
            Prezente.Location = new Point(2, 4);
            Prezente.Name = "Prezente";
            Prezente.Size = new Size(120, 20);
            Prezente.TabIndex = 43;
            Prezente.Text = "Cursanti prezenti";
            // 
            // panel4
            // 
            panel4.Controls.Add(m_gDetaliuPrezenta);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(0, 525);
            panel4.Margin = new Padding(3, 4, 3, 4);
            panel4.Name = "panel4";
            panel4.Size = new Size(1066, 138);
            panel4.TabIndex = 7;
            // 
            // m_gDetaliuPrezenta
            // 
            m_gDetaliuPrezenta.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_gDetaliuPrezenta.Dock = DockStyle.Fill;
            m_gDetaliuPrezenta.Font = new Font("Arial", 8F);
            m_gDetaliuPrezenta.Location = new Point(0, 0);
            m_gDetaliuPrezenta.Margin = new Padding(3, 4, 3, 4);
            m_gDetaliuPrezenta.Name = "m_gDetaliuPrezenta";
            m_gDetaliuPrezenta.ReadOnly = true;
            m_gDetaliuPrezenta.RowHeadersWidth = 82;
            m_gDetaliuPrezenta.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            m_gDetaliuPrezenta.Size = new Size(1066, 138);
            m_gDetaliuPrezenta.TabIndex = 0;
            // 
            // FrmCautarePrezente
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1066, 663);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(toolStrip1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FrmCautarePrezente";
            Text = "Cautare Prezente";
            WindowState = FormWindowState.Maximized;
            Load += FrmCautarePrezente_Load;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)m_gPrezente).EndInit();
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)m_gDetaliuPrezenta).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton m_tsbCautare;
        private System.Windows.Forms.ToolStripButton m_tsbNou;
        private System.Windows.Forms.ToolStripButton m_tsbSterge;
        private System.Windows.Forms.Panel panel1;
        private GridPersonalizat.CustomGrid m_gPrezente;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox m_ckActivareFiltruData;
        private System.Windows.Forms.DateTimePicker m_dtpDeLaData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker m_dtpPanaLaData;
        private System.Windows.Forms.TextBox m_ebPrenumeCursant;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox m_ebNumeCursant;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label Prezente;
        private System.Windows.Forms.Panel panel4;
        private GridPersonalizat.CustomGrid m_gDetaliuPrezenta;
        private Label label13;
        private CustomCombo m_cbFormator;
    }
}
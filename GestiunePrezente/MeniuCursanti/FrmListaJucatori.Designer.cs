using GridPersonalizat;

namespace GestiunePrezente.MeniuCursanti
{
    partial class FrmListaJucatori
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
            panel1              = new Panel();
            m_stNume            = new Label();
            m_ebNume            = new TextBox();
            m_chkDoarCursanti   = new CheckBox();
            m_gJucatori         = new CustomGrid();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_gJucatori).BeginInit();
            SuspendLayout();

            panel1.Controls.Add(m_ebNume);
            panel1.Controls.Add(m_stNume);
            panel1.Controls.Add(m_chkDoarCursanti);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1100, 50);
            panel1.TabIndex = 0;

            m_stNume.AutoSize = true;
            m_stNume.Location = new Point(12, 15);
            m_stNume.Name = "m_stNume";
            m_stNume.Text = "Caută:";

            m_ebNume.Location = new Point(65, 12);
            m_ebNume.Name = "m_ebNume";
            m_ebNume.Size = new Size(280, 23);
            m_ebNume.TabIndex = 0;
            m_ebNume.TextChanged += m_ebNume_TextChanged;

            m_chkDoarCursanti.AutoSize = true;
            m_chkDoarCursanti.Location = new Point(370, 14);
            m_chkDoarCursanti.Name = "m_chkDoarCursanti";
            m_chkDoarCursanti.Text = "Doar cursanți ONIXTEIA";
            m_chkDoarCursanti.TabIndex = 1;
            m_chkDoarCursanti.CheckedChanged += m_chkDoarCursanti_CheckedChanged;

            m_gJucatori.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_gJucatori.Dock = DockStyle.Fill;
            m_gJucatori.Font = new Font("Arial", 8F);
            m_gJucatori.Location = new Point(0, 50);
            m_gJucatori.Name = "m_gJucatori";
            m_gJucatori.ReadOnly = true;
            m_gJucatori.RowHeadersWidth = 25;
            m_gJucatori.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            m_gJucatori.Size = new Size(1100, 450);
            m_gJucatori.TabIndex = 1;

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 500);
            Controls.Add(m_gJucatori);
            Controls.Add(panel1);
            Name = "FrmListaJucatori";
            Text = "Jucători (toți)";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)m_gJucatori).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label m_stNume;
        private TextBox m_ebNume;
        private CheckBox m_chkDoarCursanti;
        private CustomGrid m_gJucatori;
    }
}

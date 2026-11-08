using GridPersonalizat;

namespace GestiunePrezente.MeniuCursanti
{
    partial class FrmSelectareContract
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
            panel1      = new Panel();
            m_labelFiltru = new Label();
            m_ebFiltru  = new TextBox();
            m_gContracte = new CustomGrid();
            panel2      = new Panel();
            m_btnOk     = new Button();
            m_btnAnuleaza = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_gContracte).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // panel1 — filtru sus
            panel1.Controls.Add(m_ebFiltru);
            panel1.Controls.Add(m_labelFiltru);
            panel1.Dock = DockStyle.Top;
            panel1.Height = 40;
            panel1.Name = "panel1";
            panel1.TabIndex = 0;
            // m_labelFiltru
            m_labelFiltru.AutoSize = true;
            m_labelFiltru.Location = new Point(10, 10);
            m_labelFiltru.Name = "m_labelFiltru";
            m_labelFiltru.Text = "Caută (nr. contract / nume):";
            // m_ebFiltru
            m_ebFiltru.Location = new Point(205, 7);
            m_ebFiltru.Name = "m_ebFiltru";
            m_ebFiltru.Size = new Size(240, 27);
            m_ebFiltru.TabIndex = 0;
            m_ebFiltru.TextChanged += m_ebFiltru_TextChanged;
            // panel2 — butoane jos
            panel2.Controls.Add(m_btnOk);
            panel2.Controls.Add(m_btnAnuleaza);
            panel2.Dock = DockStyle.Bottom;
            panel2.Height = 44;
            panel2.Name = "panel2";
            panel2.TabIndex = 2;
            // m_btnOk
            m_btnOk.Location = new Point(10, 7);
            m_btnOk.Name = "m_btnOk";
            m_btnOk.Size = new Size(90, 30);
            m_btnOk.TabIndex = 0;
            m_btnOk.Text = "OK";
            m_btnOk.Click += m_btnOk_Click;
            // m_btnAnuleaza
            m_btnAnuleaza.DialogResult = DialogResult.Cancel;
            m_btnAnuleaza.Location = new Point(110, 7);
            m_btnAnuleaza.Name = "m_btnAnuleaza";
            m_btnAnuleaza.Size = new Size(90, 30);
            m_btnAnuleaza.TabIndex = 1;
            m_btnAnuleaza.Text = "Anulează";
            // m_gContracte
            m_gContracte.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_gContracte.Dock = DockStyle.Fill;
            m_gContracte.Font = new Font("Arial", 8F);
            m_gContracte.Name = "m_gContracte";
            m_gContracte.ReadOnly = true;
            m_gContracte.RowHeadersWidth = 25;
            m_gContracte.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            m_gContracte.TabIndex = 1;
            // FrmSelectareContract
            AcceptButton = m_btnOk;
            CancelButton = m_btnAnuleaza;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 500);
            Controls.Add(m_gContracte);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(700, 400);
            Name = "FrmSelectareContract";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Selectează contractul";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)m_gContracte).EndInit();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label m_labelFiltru;
        private System.Windows.Forms.TextBox m_ebFiltru;
        private GridPersonalizat.CustomGrid m_gContracte;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button m_btnOk;
        private System.Windows.Forms.Button m_btnAnuleaza;
    }
}

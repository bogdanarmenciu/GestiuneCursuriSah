namespace GestiunePrezente.Common
{
    partial class FrmErr
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
            pictureBox  = new System.Windows.Forms.PictureBox();
            m_ebError   = new System.Windows.Forms.TextBox();
            btndett     = new System.Windows.Forms.Button();
            btnok       = new System.Windows.Forms.Button();
            richtb      = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            this.SuspendLayout();
            // pictureBox
            pictureBox.Image    = System.Drawing.SystemIcons.Error.ToBitmap();
            pictureBox.Location = new System.Drawing.Point(12, 12);
            pictureBox.Size     = new System.Drawing.Size(32, 32);
            pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox.TabStop  = false;
            // m_ebError
            m_ebError.BackColor  = System.Drawing.SystemColors.Control;
            m_ebError.BorderStyle = System.Windows.Forms.BorderStyle.None;
            m_ebError.Location   = new System.Drawing.Point(54, 12);
            m_ebError.Multiline  = true;
            m_ebError.ReadOnly   = true;
            m_ebError.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            m_ebError.Size       = new System.Drawing.Size(400, 60);
            m_ebError.TabIndex   = 0;
            // richtb
            richtb.Location  = new System.Drawing.Point(12, 84);
            richtb.ReadOnly  = true;
            richtb.Size      = new System.Drawing.Size(442, 120);
            richtb.TabIndex  = 1;
            richtb.Visible   = false;
            // btndett
            btndett.Location = new System.Drawing.Point(12, 216);
            btndett.Size     = new System.Drawing.Size(100, 28);
            btndett.TabIndex = 2;
            btndett.Text     = "Detalii >>";
            btndett.Click   += new System.EventHandler(this.btndett_Click);
            // btnok
            btnok.DialogResult = System.Windows.Forms.DialogResult.OK;
            btnok.Location     = new System.Drawing.Point(354, 216);
            btnok.Size         = new System.Drawing.Size(100, 28);
            btnok.TabIndex     = 3;
            btnok.Text         = "OK";
            btnok.Click       += new System.EventHandler(this.btnok_Click);
            // FrmErr
            this.AcceptButton        = btnok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(466, 256);
            this.Controls.Add(pictureBox);
            this.Controls.Add(m_ebError);
            this.Controls.Add(richtb);
            this.Controls.Add(btndett);
            this.Controls.Add(btnok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox     = false;
            this.MinimizeBox     = false;
            this.Name            = "FrmErr";
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text            = "Eroare";
            this.Load           += new System.EventHandler(this.FrmErr_Load);
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.TextBox m_ebError;
        private System.Windows.Forms.Button btndett;
        private System.Windows.Forms.Button btnok;
        private System.Windows.Forms.RichTextBox richtb;
    }
}
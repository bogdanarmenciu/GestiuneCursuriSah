using GridPersonalizat;

namespace GestiunePrezente.MeniuPrincipal
{
    partial class FrmLogin
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
            components = new System.ComponentModel.Container();
            m_pBar = new ProgressBar();
            m_lblInfo = new Label();
            m_btConectare = new Button();
            m_ebParola = new TextBox();
            m_ebUtilizator = new TextBox();
            label2 = new Label();
            label1 = new Label();
            m_timer = new System.Windows.Forms.Timer(components);
            label4 = new Label();
            label3 = new Label();
            m_lblServer = new Label();
            m_cbServer = new ComboBox();
            m_cbDiviziune = new CustomCombo();
            m_cbSocietate = new CustomCombo();
            SuspendLayout();
            // 
            // m_pBar
            // 
            m_pBar.Location = new Point(43, 378);
            m_pBar.Margin = new Padding(4, 5, 4, 5);
            m_pBar.Name = "m_pBar";
            m_pBar.Size = new Size(388, 31);
            m_pBar.TabIndex = 17;
            // 
            // m_lblInfo
            // 
            m_lblInfo.AutoSize = true;
            m_lblInfo.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Italic, GraphicsUnit.Point, 0);
            m_lblInfo.ForeColor = Color.Blue;
            m_lblInfo.Location = new Point(43, 336);
            m_lblInfo.Margin = new Padding(4, 0, 4, 0);
            m_lblInfo.Name = "m_lblInfo";
            m_lblInfo.Size = new Size(198, 17);
            m_lblInfo.TabIndex = 18;
            m_lblInfo.Text = "Conexiune în curs,  aşteptaţi...";
            // 
            // m_btConectare
            // 
            m_btConectare.Location = new Point(144, 281);
            m_btConectare.Margin = new Padding(4, 5, 4, 5);
            m_btConectare.Name = "m_btConectare";
            m_btConectare.Size = new Size(287, 32);
            m_btConectare.TabIndex = 16;
            m_btConectare.Text = "Conectare";
            m_btConectare.UseVisualStyleBackColor = true;
            m_btConectare.Click += m_btConectare_Click;
            // 
            // m_ebParola
            // 
            m_ebParola.Location = new Point(144, 83);
            m_ebParola.Margin = new Padding(4, 5, 4, 5);
            m_ebParola.Name = "m_ebParola";
            m_ebParola.PasswordChar = '*';
            m_ebParola.Size = new Size(285, 27);
            m_ebParola.TabIndex = 14;
            m_ebParola.KeyDown += m_ebParola_KeyDown;
            // 
            // m_ebUtilizator
            // 
            m_ebUtilizator.Location = new Point(144, 38);
            m_ebUtilizator.Margin = new Padding(4, 5, 4, 5);
            m_ebUtilizator.Name = "m_ebUtilizator";
            m_ebUtilizator.Size = new Size(285, 27);
            m_ebUtilizator.TabIndex = 12;
            m_ebUtilizator.KeyDown += m_ebUtilizator_KeyDown;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(43, 88);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(53, 20);
            label2.TabIndex = 15;
            label2.Text = "Parola:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(43, 43);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(73, 20);
            label1.TabIndex = 13;
            label1.Text = "Utilizator:";
            // 
            // m_timer
            // 
            m_timer.Enabled = true;
            m_timer.Tick += m_timer_Tick;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(43, 179);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(74, 20);
            label4.TabIndex = 20;
            label4.Text = "Societate:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(43, 229);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(73, 20);
            label3.TabIndex = 19;
            label3.Text = "Diviziune:";
            // 
            // m_lblServer
            // 
            m_lblServer.AutoSize = true;
            m_lblServer.Location = new Point(43, 133);
            m_lblServer.Margin = new Padding(4, 0, 4, 0);
            m_lblServer.Name = "m_lblServer";
            m_lblServer.Size = new Size(53, 20);
            m_lblServer.TabIndex = 24;
            m_lblServer.Text = "Server:";
            // 
            // m_cbServer
            // 
            m_cbServer.Location = new Point(144, 129);
            m_cbServer.Margin = new Padding(4, 5, 4, 5);
            m_cbServer.Name = "m_cbServer";
            m_cbServer.Size = new Size(285, 28);
            m_cbServer.TabIndex = 25;
            // 
            // m_cbDiviziune
            // 
            m_cbDiviziune.AutoCompleteMode = AutoCompleteMode.Append;
            m_cbDiviziune.AutoCompleteSource = AutoCompleteSource.ListItems;
            m_cbDiviziune.Font = new Font("Microsoft Sans Serif", 8F);
            m_cbDiviziune.FormattingEnabled = true;
            m_cbDiviziune.Location = new Point(144, 224);
            m_cbDiviziune.Margin = new Padding(4, 5, 4, 5);
            m_cbDiviziune.Name = "m_cbDiviziune";
            m_cbDiviziune.Size = new Size(285, 24);
            m_cbDiviziune.TabIndex = 23;
            // 
            // m_cbSocietate
            // 
            m_cbSocietate.AutoCompleteMode = AutoCompleteMode.Append;
            m_cbSocietate.AutoCompleteSource = AutoCompleteSource.ListItems;
            m_cbSocietate.Font = new Font("Microsoft Sans Serif", 8F);
            m_cbSocietate.FormattingEnabled = true;
            m_cbSocietate.Location = new Point(144, 175);
            m_cbSocietate.Margin = new Padding(4, 5, 4, 5);
            m_cbSocietate.Name = "m_cbSocietate";
            m_cbSocietate.Size = new Size(285, 24);
            m_cbSocietate.TabIndex = 22;
            m_cbSocietate.SelectedIndexChanged += m_cbSocietate_SelectedIndexChanged;
            // 
            // FrmLogin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(490, 442);
            Controls.Add(m_cbServer);
            Controls.Add(m_lblServer);
            Controls.Add(m_cbDiviziune);
            Controls.Add(m_cbSocietate);
            Controls.Add(m_pBar);
            Controls.Add(m_lblInfo);
            Controls.Add(m_btConectare);
            Controls.Add(m_ebParola);
            Controls.Add(m_ebUtilizator);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(label4);
            Controls.Add(label3);
            Margin = new Padding(4, 5, 4, 5);
            Name = "FrmLogin";
            Text = "Conectare la Baza de Date";
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar m_pBar;
        private System.Windows.Forms.Label m_lblInfo;
        private System.Windows.Forms.Button m_btConectare;
        private System.Windows.Forms.TextBox m_ebParola;
        private System.Windows.Forms.TextBox m_ebUtilizator;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer m_timer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private CustomCombo m_cbSocietate;
        private CustomCombo m_cbDiviziune;
        private Label m_lblServer;
        private ComboBox m_cbServer;
    }
}
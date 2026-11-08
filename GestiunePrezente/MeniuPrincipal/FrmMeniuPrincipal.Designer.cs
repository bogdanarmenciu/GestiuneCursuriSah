namespace GestiunePrezente.MeniuPrincipal
{
    partial class FrmMeniuPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMeniuPrincipal));
            imageList1 = new ImageList(components);
            Sidebar = new Panel();
            panel1 = new Panel();
            RetrageMeniu = new PictureBox();
            panelTop = new Panel();
            btnMeniu = new Button();
            btnSetari = new Button();
            m_tabPP = new TabControl();
            Sidebar.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)RetrageMeniu).BeginInit();
            SuspendLayout();
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth8Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "Director.ico");
            imageList1.Images.SetKeyName(1, "Cautare.ico");
            imageList1.Images.SetKeyName(2, "Nou.ico");
            // 
            // Sidebar
            // 
            Sidebar.BackColor = Color.FromArgb(23, 24, 29);
            Sidebar.Controls.Add(panel1);
            Sidebar.Controls.Add(panelTop);
            Sidebar.Controls.Add(btnMeniu);
            Sidebar.Controls.Add(btnSetari);
            Sidebar.Dock = DockStyle.Left;
            Sidebar.Location = new Point(0, 0);
            Sidebar.Margin = new Padding(5);
            Sidebar.Name = "Sidebar";
            Sidebar.Size = new Size(349, 1459);
            Sidebar.TabIndex = 10;
            // 
            // panel1
            // 
            panel1.Controls.Add(RetrageMeniu);
            panel1.Location = new Point(0, 1399);
            panel1.Margin = new Padding(5);
            panel1.Name = "panel1";
            panel1.Size = new Size(75, 60);
            panel1.TabIndex = 4;
            // 
            // RetrageMeniu
            // 
            RetrageMeniu.BackColor = Color.FromArgb(23, 24, 29);
            RetrageMeniu.Dock = DockStyle.Fill;
     //       RetrageMeniu.Image = Properties.Resources.checkout;
            RetrageMeniu.Location = new Point(0, 0);
            RetrageMeniu.Margin = new Padding(5);
            RetrageMeniu.Name = "RetrageMeniu";
            RetrageMeniu.Size = new Size(75, 60);
            RetrageMeniu.TabIndex = 0;
            RetrageMeniu.TabStop = false;
            // 
            // panelTop
            // 
            panelTop.Dock = DockStyle.Left;
            panelTop.Location = new Point(0, 0);
            panelTop.Margin = new Padding(5);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(349, 1271);
            panelTop.TabIndex = 1;
            // 
            // btnMeniu
            // 
            btnMeniu.BackColor = Color.FromArgb(23, 24, 29);
            btnMeniu.Dock = DockStyle.Bottom;
            btnMeniu.FlatAppearance.BorderSize = 0;
            btnMeniu.FlatStyle = FlatStyle.Flat;
            btnMeniu.ForeColor = Color.Gainsboro;
            btnMeniu.Location = new Point(0, 1271);
            btnMeniu.Margin = new Padding(5);
            btnMeniu.Name = "btnMeniu";
            btnMeniu.Size = new Size(349, 94);
            btnMeniu.TabIndex = 0;
            btnMeniu.Text = "Meniu";
            btnMeniu.UseVisualStyleBackColor = false;
            // 
            // btnSetari
            // 
            btnSetari.BackColor = Color.FromArgb(23, 24, 29);
            btnSetari.Dock = DockStyle.Bottom;
            btnSetari.FlatAppearance.BorderSize = 0;
            btnSetari.FlatStyle = FlatStyle.Flat;
            btnSetari.ForeColor = Color.Gainsboro;
            btnSetari.ImageAlign = ContentAlignment.MiddleLeft;
            btnSetari.Location = new Point(0, 1365);
            btnSetari.Margin = new Padding(5);
            btnSetari.Name = "btnSetari";
            btnSetari.Size = new Size(349, 94);
            btnSetari.TabIndex = 0;
            btnSetari.Text = "Setări";
            btnSetari.UseVisualStyleBackColor = false;
            // 
            // m_tabPP
            // 
            m_tabPP.Dock = DockStyle.Top;
            m_tabPP.DrawMode = TabDrawMode.OwnerDrawFixed;
            m_tabPP.Location = new Point(349, 0);
            m_tabPP.Margin = new Padding(5);
            m_tabPP.Name = "m_tabPP";
            m_tabPP.SelectedIndex = 0;
            m_tabPP.Size = new Size(1954, 61);
            m_tabPP.TabIndex = 12;
            m_tabPP.SelectedIndexChanged += m_tabPP_SelectedIndexChanged;
            m_tabPP.MouseClick += m_tabPP_MouseClick;
            // 
            // FrmMeniuPrincipal
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2303, 1459);
            Controls.Add(m_tabPP);
            Controls.Add(Sidebar);
            IsMdiContainer = true;
            Margin = new Padding(6, 8, 6, 8);
            Name = "FrmMeniuPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
            FormClosing += FrmMeniuPrincipal_FormClosing;
            MdiChildActivate += FrmMeniuPrincipal_MdiChildActivate;
            Sidebar.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)RetrageMeniu).EndInit();
            ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ImageList imageList1;
        private Panel Sidebar;
        private Panel panelTop;
        private Panel panel1;
        private PictureBox RetrageMeniu;
        private Button btnMeniu;
        private Button btnSetari;
        private TabControl m_tabPP;
    }
}
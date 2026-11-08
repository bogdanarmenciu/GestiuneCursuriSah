using GridPersonalizat;

namespace GestiunePrezente.MeniuCursanti
{
    partial class FrmGestiuneCursant
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
            m_stbSalveaza = new ToolStripButton();
            m_tsbDeschideArhiva = new ToolStripButton();
            m_tsbListeazaBarcode = new ToolStripButton();
            m_tslSepNume = new ToolStripSeparator();
            m_tslNume = new ToolStripLabel();
            panel1 = new Panel();
            groupBox4 = new GroupBox();
            label21 = new Label();
            m_ebTelefonTutore = new TextBox();
            m_ebPrenumeTutore = new TextBox();
            m_ebEmailTutore = new TextBox();
            label17 = new Label();
            m_ebCNPTutore = new TextBox();
            label18 = new Label();
            label19 = new Label();
            groupBox8 = new GroupBox();
            m_ebCISerieTutore = new TextBox();
            label10 = new Label();
            label13 = new Label();
            m_ebCINumarTutore = new TextBox();
            m_ebNumeTutore = new TextBox();
            label9 = new Label();
            groupBox3 = new GroupBox();
            groupBox5 = new GroupBox();
            m_ebTelMobil = new TextBox();
            label12 = new Label();
            label11 = new Label();
            m_ebTelFix = new TextBox();
            label22 = new Label();
            label14 = new Label();
            m_ebEmail = new TextBox();
            label23 = new Label();
            m_ebNumeFB = new TextBox();
            m_ebNumeSkype = new TextBox();
            groupBox2 = new GroupBox();
            m_ebJudet = new TextBox();
            label16 = new Label();
            m_ebAp = new TextBox();
            m_ebOras = new TextBox();
            label8 = new Label();
            m_ebScara = new TextBox();
            label15 = new Label();
            label7 = new Label();
            m_ebBloc = new TextBox();
            label6 = new Label();
            m_ebNumarStrada = new TextBox();
            label5 = new Label();
            m_ebStrada = new TextBox();
            label4 = new Label();
            m_ebJudetulNasterii = new TextBox();
            label20 = new Label();
            m_ebLocalitateaNasterii = new TextBox();
            m_ebBarcode = new TextBox();
            m_ebFideId = new TextBox();
            m_stFideId = new Label();
            label3 = new Label();
            m_dtDataNasterii = new DateTimePicker();
            m_st5 = new Label();
            groupBox1 = new GroupBox();
            m_lblTipDocument = new Label();
            m_cbTipDocument = new ComboBox();
            m_ebCISerie = new TextBox();
            label2 = new Label();
            label1 = new Label();
            m_ebCINumar = new TextBox();
            label24 = new Label();
            m_ebNume = new TextBox();
            m_ebNrMatricol = new TextBox();
            m_st3 = new Label();
            m_st1 = new Label();
            m_ebPrenume = new TextBox();
            m_st4 = new Label();
            m_st2 = new Label();
            m_ebCNP = new TextBox();
            m_cbCetAteanStrain = new CheckBox();
            m_lblNrDocument = new Label();
            m_ebNrDocumentStrain = new TextBox();
            m_tabControl = new TabControl();
            m_tabPageDate = new TabPage();
            m_tabPageContracte = new TabPage();
            m_splitContracte = new SplitContainer();
            m_gContracteCursant = new CustomGrid();
            m_gAbonamenteCursant = new CustomGrid();
            m_tabPageArhivaCursant = new TabPage();
            m_gArhivaCursant = new CustomGrid();
            m_panelArhivaCursantButtons = new Panel();
            m_btnStergeArhivaCursant = new Button();
            m_btnDeschideArhivaCursant = new Button();
            m_btnAdaugaArhivaCursant = new Button();
            toolStrip1.SuspendLayout();
            panel1.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox8.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            m_tabControl.SuspendLayout();
            m_tabPageDate.SuspendLayout();
            m_tabPageContracte.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_splitContracte).BeginInit();
            m_splitContracte.Panel1.SuspendLayout();
            m_splitContracte.Panel2.SuspendLayout();
            m_splitContracte.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_gContracteCursant).BeginInit();
            ((System.ComponentModel.ISupportInitialize)m_gAbonamenteCursant).BeginInit();
            m_tabPageArhivaCursant.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_gArhivaCursant).BeginInit();
            m_panelArhivaCursantButtons.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { m_stbSalveaza, m_tsbDeschideArhiva, m_tsbListeazaBarcode, m_tslSepNume, m_tslNume });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Padding = new Padding(0, 0, 2, 0);
            toolStrip1.Size = new Size(1151, 27);
            toolStrip1.TabIndex = 2;
            toolStrip1.Text = "toolStrip1";
            // 
            // m_stbSalveaza
            // 
            m_stbSalveaza.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_stbSalveaza.Image = Properties.Resources.save;
            m_stbSalveaza.ImageTransparentColor = Color.Magenta;
            m_stbSalveaza.Name = "m_stbSalveaza";
            m_stbSalveaza.Size = new Size(29, 24);
            m_stbSalveaza.Text = "Salveaza";
            m_stbSalveaza.Click += m_stbSalveaza_Click;
            // 
            // m_tsbDeschideArhiva
            // 
            m_tsbDeschideArhiva.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbDeschideArhiva.Image = Properties.Resources.CursantArhiva;
            m_tsbDeschideArhiva.ImageTransparentColor = Color.Magenta;
            m_tsbDeschideArhiva.Name = "m_tsbDeschideArhiva";
            m_tsbDeschideArhiva.Size = new Size(29, 24);
            m_tsbDeschideArhiva.Text = "Deschide arhiva";
            m_tsbDeschideArhiva.Click += m_tsbDeschideArhiva_Click;
            // 
            // m_tsbListeazaBarcode
            // 
            m_tsbListeazaBarcode.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_tsbListeazaBarcode.Image = Properties.Resources.Scanner;
            m_tsbListeazaBarcode.ImageTransparentColor = Color.Magenta;
            m_tsbListeazaBarcode.Name = "m_tsbListeazaBarcode";
            m_tsbListeazaBarcode.Size = new Size(29, 24);
            m_tsbListeazaBarcode.Text = "Listeaza Barcode";
            m_tsbListeazaBarcode.Click += m_tsbListeazaBarcode_Click;
            //
            // m_tslSepNume
            //
            m_tslSepNume.Name = "m_tslSepNume";
            m_tslSepNume.Size = new Size(6, 24);
            //
            // m_tslNume
            //
            m_tslNume.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            m_tslNume.Name = "m_tslNume";
            m_tslNume.Size = new Size(0, 24);
            m_tslNume.Text = "";
            //
            // panel1
            // 
            panel1.Controls.Add(groupBox4);
            panel1.Controls.Add(groupBox3);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(5, 4, 5, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(1143, 987);
            panel1.TabIndex = 3;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(label21);
            groupBox4.Controls.Add(m_ebTelefonTutore);
            groupBox4.Controls.Add(m_ebPrenumeTutore);
            groupBox4.Controls.Add(m_ebEmailTutore);
            groupBox4.Controls.Add(label17);
            groupBox4.Controls.Add(m_ebCNPTutore);
            groupBox4.Controls.Add(label18);
            groupBox4.Controls.Add(label19);
            groupBox4.Controls.Add(groupBox8);
            groupBox4.Controls.Add(m_ebNumeTutore);
            groupBox4.Controls.Add(label9);
            groupBox4.Location = new Point(21, 687);
            groupBox4.Margin = new Padding(5, 4, 5, 4);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new Padding(5, 4, 5, 4);
            groupBox4.Size = new Size(1089, 317);
            groupBox4.TabIndex = 10;
            groupBox4.TabStop = false;
            groupBox4.Text = "Date reprezentant legal";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(24, 139);
            label21.Margin = new Padding(5, 0, 5, 0);
            label21.Name = "label21";
            label21.Size = new Size(37, 20);
            label21.TabIndex = 24;
            label21.Text = "CNP";
            // 
            // m_ebTelefonTutore
            // 
            m_ebTelefonTutore.BackColor = Color.White;
            m_ebTelefonTutore.Location = new Point(691, 56);
            m_ebTelefonTutore.Margin = new Padding(5, 4, 5, 4);
            m_ebTelefonTutore.Name = "m_ebTelefonTutore";
            m_ebTelefonTutore.Size = new Size(174, 27);
            m_ebTelefonTutore.TabIndex = 31;
            m_ebTelefonTutore.TextChanged += m_ebTelefonTutore_TextChanged;
            // 
            // m_ebPrenumeTutore
            // 
            m_ebPrenumeTutore.BackColor = Color.White;
            m_ebPrenumeTutore.Location = new Point(131, 95);
            m_ebPrenumeTutore.Margin = new Padding(5, 4, 5, 4);
            m_ebPrenumeTutore.Name = "m_ebPrenumeTutore";
            m_ebPrenumeTutore.Size = new Size(354, 27);
            m_ebPrenumeTutore.TabIndex = 16;
            m_ebPrenumeTutore.TextChanged += m_ebPrenumeTutore_TextChanged;
            // 
            // m_ebEmailTutore
            // 
            m_ebEmailTutore.BackColor = SystemColors.Window;
            m_ebEmailTutore.ForeColor = SystemColors.WindowText;
            m_ebEmailTutore.Location = new Point(694, 95);
            m_ebEmailTutore.Margin = new Padding(5, 4, 5, 4);
            m_ebEmailTutore.Name = "m_ebEmailTutore";
            m_ebEmailTutore.Size = new Size(354, 27);
            m_ebEmailTutore.TabIndex = 31;
            m_ebEmailTutore.TextChanged += m_ebEmailTutore_TextChanged;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(567, 60);
            label17.Margin = new Padding(5, 0, 5, 0);
            label17.Name = "label17";
            label17.Size = new Size(58, 20);
            label17.TabIndex = 30;
            label17.Text = "Telefon";
            label17.Click += label17_Click;
            // 
            // m_ebCNPTutore
            // 
            m_ebCNPTutore.BackColor = Color.White;
            m_ebCNPTutore.Location = new Point(131, 133);
            m_ebCNPTutore.Margin = new Padding(5, 4, 5, 4);
            m_ebCNPTutore.Name = "m_ebCNPTutore";
            m_ebCNPTutore.Size = new Size(126, 27);
            m_ebCNPTutore.TabIndex = 23;
            m_ebCNPTutore.TextChanged += m_ebCNPTutore_TextChanged;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(567, 99);
            label18.Margin = new Padding(5, 0, 5, 0);
            label18.Name = "label18";
            label18.Size = new Size(46, 20);
            label18.TabIndex = 30;
            label18.Text = "Email";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(24, 99);
            label19.Margin = new Padding(5, 0, 5, 0);
            label19.Name = "label19";
            label19.Size = new Size(67, 20);
            label19.TabIndex = 17;
            label19.Text = "Prenume";
            // 
            // groupBox8
            // 
            groupBox8.Controls.Add(m_ebCISerieTutore);
            groupBox8.Controls.Add(label10);
            groupBox8.Controls.Add(label13);
            groupBox8.Controls.Add(m_ebCINumarTutore);
            groupBox8.Location = new Point(24, 175);
            groupBox8.Margin = new Padding(5, 4, 5, 4);
            groupBox8.Name = "groupBox8";
            groupBox8.Padding = new Padding(5, 4, 5, 4);
            groupBox8.Size = new Size(277, 127);
            groupBox8.TabIndex = 23;
            groupBox8.TabStop = false;
            groupBox8.Text = "Carte de identitate";
            // 
            // m_ebCISerieTutore
            // 
            m_ebCISerieTutore.BackColor = Color.White;
            m_ebCISerieTutore.Location = new Point(131, 44);
            m_ebCISerieTutore.Margin = new Padding(5, 4, 5, 4);
            m_ebCISerieTutore.Name = "m_ebCISerieTutore";
            m_ebCISerieTutore.Size = new Size(105, 27);
            m_ebCISerieTutore.TabIndex = 7;
            m_ebCISerieTutore.TextChanged += m_ebCISerieTutore_TextChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(25, 83);
            label10.Margin = new Padding(5, 0, 5, 0);
            label10.Name = "label10";
            label10.Size = new Size(54, 20);
            label10.TabIndex = 22;
            label10.Text = "Numar";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(25, 49);
            label13.Margin = new Padding(5, 0, 5, 0);
            label13.Name = "label13";
            label13.Size = new Size(42, 20);
            label13.TabIndex = 20;
            label13.Text = "Serie";
            // 
            // m_ebCINumarTutore
            // 
            m_ebCINumarTutore.BackColor = Color.White;
            m_ebCINumarTutore.Location = new Point(131, 79);
            m_ebCINumarTutore.Margin = new Padding(5, 4, 5, 4);
            m_ebCINumarTutore.Name = "m_ebCINumarTutore";
            m_ebCINumarTutore.Size = new Size(105, 27);
            m_ebCINumarTutore.TabIndex = 8;
            m_ebCINumarTutore.TextChanged += m_ebCINumarTutore_TextChanged;
            // 
            // m_ebNumeTutore
            // 
            m_ebNumeTutore.BackColor = Color.White;
            m_ebNumeTutore.Location = new Point(131, 56);
            m_ebNumeTutore.Margin = new Padding(5, 4, 5, 4);
            m_ebNumeTutore.Name = "m_ebNumeTutore";
            m_ebNumeTutore.Size = new Size(354, 27);
            m_ebNumeTutore.TabIndex = 10;
            m_ebNumeTutore.TextChanged += m_ebNumeTutore_TextChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(24, 60);
            label9.Margin = new Padding(5, 0, 5, 0);
            label9.Name = "label9";
            label9.Size = new Size(49, 20);
            label9.TabIndex = 22;
            label9.Text = "Nume";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(groupBox5);
            groupBox3.Controls.Add(groupBox2);
            groupBox3.Controls.Add(m_ebJudetulNasterii);
            groupBox3.Controls.Add(label20);
            groupBox3.Controls.Add(m_ebLocalitateaNasterii);
            groupBox3.Controls.Add(m_ebBarcode);
            groupBox3.Controls.Add(m_ebFideId);
            groupBox3.Controls.Add(m_stFideId);
            groupBox3.Controls.Add(label3);
            groupBox3.Controls.Add(m_dtDataNasterii);
            groupBox3.Controls.Add(m_st5);
            groupBox3.Controls.Add(groupBox1);
            groupBox3.Controls.Add(label24);
            groupBox3.Controls.Add(m_ebNume);
            groupBox3.Controls.Add(m_ebNrMatricol);
            groupBox3.Controls.Add(m_st3);
            groupBox3.Controls.Add(m_st1);
            groupBox3.Controls.Add(m_ebPrenume);
            groupBox3.Controls.Add(m_st4);
            groupBox3.Controls.Add(m_st2);
            groupBox3.Controls.Add(m_ebCNP);
            groupBox3.Controls.Add(m_cbCetAteanStrain);
            groupBox3.Controls.Add(m_lblNrDocument);
            groupBox3.Controls.Add(m_ebNrDocumentStrain);
            groupBox3.Location = new Point(16, 20);
            groupBox3.Margin = new Padding(5, 4, 5, 4);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(5, 4, 5, 4);
            groupBox3.Size = new Size(1089, 661);
            groupBox3.TabIndex = 1;
            groupBox3.TabStop = false;
            groupBox3.Text = "Date generale cursant";
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(m_ebTelMobil);
            groupBox5.Controls.Add(label12);
            groupBox5.Controls.Add(label11);
            groupBox5.Controls.Add(m_ebTelFix);
            groupBox5.Controls.Add(label22);
            groupBox5.Controls.Add(label14);
            groupBox5.Controls.Add(m_ebEmail);
            groupBox5.Controls.Add(label23);
            groupBox5.Controls.Add(m_ebNumeFB);
            groupBox5.Controls.Add(m_ebNumeSkype);
            groupBox5.Location = new Point(24, 488);
            groupBox5.Margin = new Padding(5, 4, 5, 4);
            groupBox5.Name = "groupBox5";
            groupBox5.Padding = new Padding(5, 4, 5, 4);
            groupBox5.Size = new Size(1029, 153);
            groupBox5.TabIndex = 31;
            groupBox5.TabStop = false;
            groupBox5.Text = "Date de contact";
            // 
            // m_ebTelMobil
            // 
            m_ebTelMobil.BackColor = Color.White;
            m_ebTelMobil.Location = new Point(131, 29);
            m_ebTelMobil.Margin = new Padding(5, 4, 5, 4);
            m_ebTelMobil.Name = "m_ebTelMobil";
            m_ebTelMobil.Size = new Size(126, 27);
            m_ebTelMobil.TabIndex = 2;
            m_ebTelMobil.TextChanged += m_ebTelMobil_TextChanged;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(24, 36);
            label12.Margin = new Padding(5, 0, 5, 0);
            label12.Name = "label12";
            label12.Size = new Size(101, 20);
            label12.TabIndex = 20;
            label12.Text = "Telefon mobil";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(272, 36);
            label11.Margin = new Padding(5, 0, 5, 0);
            label11.Name = "label11";
            label11.Size = new Size(80, 20);
            label11.TabIndex = 22;
            label11.Text = "Telefon Fix";
            // 
            // m_ebTelFix
            // 
            m_ebTelFix.Location = new Point(359, 32);
            m_ebTelFix.Margin = new Padding(5, 4, 5, 4);
            m_ebTelFix.Name = "m_ebTelFix";
            m_ebTelFix.Size = new Size(126, 27);
            m_ebTelFix.TabIndex = 24;
            m_ebTelFix.TextChanged += m_ebTelFix_TextChanged;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(494, 108);
            label22.Margin = new Padding(5, 0, 5, 0);
            label22.Name = "label22";
            label22.Size = new Size(75, 20);
            label22.TabIndex = 29;
            label22.Text = "Instagram";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(24, 69);
            label14.Margin = new Padding(5, 0, 5, 0);
            label14.Name = "label14";
            label14.Size = new Size(46, 20);
            label14.TabIndex = 24;
            label14.Text = "Email";
            // 
            // m_ebEmail
            // 
            m_ebEmail.BackColor = SystemColors.Window;
            m_ebEmail.ForeColor = SystemColors.WindowText;
            m_ebEmail.Location = new Point(131, 65);
            m_ebEmail.Margin = new Padding(5, 4, 5, 4);
            m_ebEmail.Name = "m_ebEmail";
            m_ebEmail.Size = new Size(354, 27);
            m_ebEmail.TabIndex = 26;
            m_ebEmail.TextChanged += m_ebEmail_TextChanged;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new Point(24, 109);
            label23.Margin = new Padding(5, 0, 5, 0);
            label23.Name = "label23";
            label23.Size = new Size(72, 20);
            label23.TabIndex = 27;
            label23.Text = "Facebook";
            // 
            // m_ebNumeFB
            // 
            m_ebNumeFB.BackColor = SystemColors.Window;
            m_ebNumeFB.ForeColor = SystemColors.WindowText;
            m_ebNumeFB.Location = new Point(131, 103);
            m_ebNumeFB.Margin = new Padding(5, 4, 5, 4);
            m_ebNumeFB.Name = "m_ebNumeFB";
            m_ebNumeFB.Size = new Size(354, 27);
            m_ebNumeFB.TabIndex = 28;
            m_ebNumeFB.TextChanged += m_ebNumeFB_TextChanged;
            // 
            // m_ebNumeSkype
            // 
            m_ebNumeSkype.Location = new Point(601, 103);
            m_ebNumeSkype.Margin = new Padding(5, 4, 5, 4);
            m_ebNumeSkype.Name = "m_ebNumeSkype";
            m_ebNumeSkype.Size = new Size(354, 27);
            m_ebNumeSkype.TabIndex = 29;
            m_ebNumeSkype.TextChanged += m_ebNumeSkype_TextChanged;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(m_ebJudet);
            groupBox2.Controls.Add(label16);
            groupBox2.Controls.Add(m_ebAp);
            groupBox2.Controls.Add(m_ebOras);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(m_ebScara);
            groupBox2.Controls.Add(label15);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(m_ebBloc);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(m_ebNumarStrada);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(m_ebStrada);
            groupBox2.Controls.Add(label4);
            groupBox2.Location = new Point(24, 371);
            groupBox2.Margin = new Padding(5, 4, 5, 4);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(5, 4, 5, 4);
            groupBox2.Size = new Size(1029, 109);
            groupBox2.TabIndex = 15;
            groupBox2.TabStop = false;
            groupBox2.Text = "Adresa";
            // 
            // m_ebJudet
            // 
            m_ebJudet.BackColor = Color.Yellow;
            m_ebJudet.Location = new Point(379, 79);
            m_ebJudet.Margin = new Padding(5, 4, 5, 4);
            m_ebJudet.Name = "m_ebJudet";
            m_ebJudet.Size = new Size(174, 27);
            m_ebJudet.TabIndex = 21;
            m_ebJudet.TextChanged += m_ebJudet_TextChanged;
            m_ebJudet.DoubleClick += m_ebJudet_DoubleClick;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(320, 83);
            label16.Margin = new Padding(5, 0, 5, 0);
            label16.Name = "label16";
            label16.Size = new Size(44, 20);
            label16.TabIndex = 30;
            label16.Text = "Judet";
            // 
            // m_ebAp
            // 
            m_ebAp.Location = new Point(751, 44);
            m_ebAp.Margin = new Padding(5, 4, 5, 4);
            m_ebAp.Name = "m_ebAp";
            m_ebAp.Size = new Size(58, 27);
            m_ebAp.TabIndex = 19;
            m_ebAp.TextChanged += m_ebAp_TextChanged;
            // 
            // m_ebOras
            // 
            m_ebOras.BackColor = Color.Yellow;
            m_ebOras.Location = new Point(123, 79);
            m_ebOras.Margin = new Padding(5, 4, 5, 4);
            m_ebOras.Name = "m_ebOras";
            m_ebOras.Size = new Size(174, 27);
            m_ebOras.TabIndex = 20;
            m_ebOras.TextChanged += m_ebOras_TextChanged;
            m_ebOras.DoubleClick += m_ebOras_DoubleClick;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(704, 49);
            label8.Margin = new Padding(5, 0, 5, 0);
            label8.Name = "label8";
            label8.Size = new Size(31, 20);
            label8.TabIndex = 30;
            label8.Text = "Ap.";
            // 
            // m_ebScara
            // 
            m_ebScara.Location = new Point(633, 44);
            m_ebScara.Margin = new Padding(5, 4, 5, 4);
            m_ebScara.Name = "m_ebScara";
            m_ebScara.Size = new Size(58, 27);
            m_ebScara.TabIndex = 18;
            m_ebScara.TextChanged += m_ebScara_TextChanged;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(34, 83);
            label15.Margin = new Padding(5, 0, 5, 0);
            label15.Name = "label15";
            label15.Size = new Size(74, 20);
            label15.TabIndex = 28;
            label15.Text = "Localitate";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(570, 49);
            label7.Margin = new Padding(5, 0, 5, 0);
            label7.Name = "label7";
            label7.Size = new Size(45, 20);
            label7.TabIndex = 28;
            label7.Text = "Scara";
            // 
            // m_ebBloc
            // 
            m_ebBloc.Location = new Point(497, 44);
            m_ebBloc.Margin = new Padding(5, 4, 5, 4);
            m_ebBloc.Name = "m_ebBloc";
            m_ebBloc.Size = new Size(58, 27);
            m_ebBloc.TabIndex = 17;
            m_ebBloc.TextChanged += m_ebBloc_TextChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(447, 49);
            label6.Margin = new Padding(5, 0, 5, 0);
            label6.Name = "label6";
            label6.Size = new Size(38, 20);
            label6.TabIndex = 26;
            label6.Text = "Bloc";
            // 
            // m_ebNumarStrada
            // 
            m_ebNumarStrada.Location = new Point(379, 44);
            m_ebNumarStrada.Margin = new Padding(5, 4, 5, 4);
            m_ebNumarStrada.Name = "m_ebNumarStrada";
            m_ebNumarStrada.Size = new Size(58, 27);
            m_ebNumarStrada.TabIndex = 16;
            m_ebNumarStrada.TextChanged += m_ebNumarStrada_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(315, 49);
            label5.Margin = new Padding(5, 0, 5, 0);
            label5.Name = "label5";
            label5.Size = new Size(54, 20);
            label5.TabIndex = 24;
            label5.Text = "Numar";
            // 
            // m_ebStrada
            // 
            m_ebStrada.BackColor = Color.White;
            m_ebStrada.Location = new Point(123, 44);
            m_ebStrada.Margin = new Padding(5, 4, 5, 4);
            m_ebStrada.Name = "m_ebStrada";
            m_ebStrada.Size = new Size(174, 27);
            m_ebStrada.TabIndex = 15;
            m_ebStrada.TextChanged += m_ebStrada_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(33, 49);
            label4.Margin = new Padding(5, 0, 5, 0);
            label4.Name = "label4";
            label4.Size = new Size(52, 20);
            label4.TabIndex = 20;
            label4.Text = "Strada";
            // 
            // m_ebJudetulNasterii
            // 
            m_ebJudetulNasterii.BackColor = Color.Yellow;
            m_ebJudetulNasterii.Location = new Point(891, 325);
            m_ebJudetulNasterii.Margin = new Padding(5, 4, 5, 4);
            m_ebJudetulNasterii.Name = "m_ebJudetulNasterii";
            m_ebJudetulNasterii.Size = new Size(174, 27);
            m_ebJudetulNasterii.TabIndex = 5;
            m_ebJudetulNasterii.TextChanged += m_ebJudetulNasterii_TextChanged;
            m_ebJudetulNasterii.DoubleClick += m_ebJudetulNasterii_DoubleClick;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(773, 328);
            label20.Margin = new Padding(5, 0, 5, 0);
            label20.Name = "label20";
            label20.Size = new Size(108, 20);
            label20.TabIndex = 32;
            label20.Text = "Judetul nasterii";
            // 
            // m_ebLocalitateaNasterii
            // 
            m_ebLocalitateaNasterii.BackColor = Color.Yellow;
            m_ebLocalitateaNasterii.Location = new Point(559, 325);
            m_ebLocalitateaNasterii.Margin = new Padding(5, 4, 5, 4);
            m_ebLocalitateaNasterii.Name = "m_ebLocalitateaNasterii";
            m_ebLocalitateaNasterii.Size = new Size(174, 27);
            m_ebLocalitateaNasterii.TabIndex = 4;
            m_ebLocalitateaNasterii.TextChanged += m_ebLocalitateaNasterii_TextChanged;
            m_ebLocalitateaNasterii.DoubleClick += m_ebLocalitateaNasterii_DoubleClick;
            // 
            // m_ebBarcode
            //
            m_ebBarcode.Location = new Point(647, 76);
            m_ebBarcode.Margin = new Padding(5, 4, 5, 4);
            m_ebBarcode.Name = "m_ebBarcode";
            m_ebBarcode.ReadOnly = true;
            m_ebBarcode.Size = new Size(174, 27);
            m_ebBarcode.TabIndex = 21;
            //
            // m_ebFideId
            //
            m_ebFideId.Location = new Point(647, 115);
            m_ebFideId.Margin = new Padding(5, 4, 5, 4);
            m_ebFideId.Name = "m_ebFideId";
            m_ebFideId.Size = new Size(174, 27);
            m_ebFideId.TabIndex = 22;
            m_ebFideId.TextChanged += m_ebFideId_TextChanged;
            //
            // m_stFideId
            //
            m_stFideId.AutoSize = true;
            m_stFideId.Location = new Point(521, 119);
            m_stFideId.Margin = new Padding(5, 0, 5, 0);
            m_stFideId.Name = "m_stFideId";
            m_stFideId.Text = "FIDE ID";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(423, 328);
            label3.Margin = new Padding(5, 0, 5, 0);
            label3.Name = "label3";
            label3.Size = new Size(134, 20);
            label3.TabIndex = 22;
            label3.Text = "Localitatea nasterii";
            // 
            // m_dtDataNasterii
            // 
            m_dtDataNasterii.Location = new Point(131, 325);
            m_dtDataNasterii.Margin = new Padding(5, 4, 5, 4);
            m_dtDataNasterii.Name = "m_dtDataNasterii";
            m_dtDataNasterii.Size = new Size(251, 27);
            m_dtDataNasterii.TabIndex = 5;
            m_dtDataNasterii.ValueChanged += m_dtDataNasterii_ValueChanged;
            // 
            // m_st5
            // 
            m_st5.AutoSize = true;
            m_st5.Location = new Point(24, 328);
            m_st5.Margin = new Padding(5, 0, 5, 0);
            m_st5.Name = "m_st5";
            m_st5.Size = new Size(96, 20);
            m_st5.TabIndex = 17;
            m_st5.Text = "Data Nasterii";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(m_lblTipDocument);
            groupBox1.Controls.Add(m_cbTipDocument);
            groupBox1.Controls.Add(m_ebCISerie);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(m_ebCINumar);
            groupBox1.Location = new Point(29, 177);
            groupBox1.Margin = new Padding(5, 4, 5, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(5, 4, 5, 4);
            groupBox1.Size = new Size(300, 138);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Document de identitate";
            // 
            // m_lblTipDocument
            // 
            m_lblTipDocument.AutoSize = true;
            m_lblTipDocument.Location = new Point(5, 29);
            m_lblTipDocument.Name = "m_lblTipDocument";
            m_lblTipDocument.Size = new Size(33, 20);
            m_lblTipDocument.TabIndex = 40;
            m_lblTipDocument.Text = "Tip:";
            // 
            // m_cbTipDocument
            // 
            m_cbTipDocument.DropDownStyle = ComboBoxStyle.DropDownList;
            m_cbTipDocument.Items.AddRange(new object[] { "", "Carte de identitate", "Certificat de nastere", "Pasaport" });
            m_cbTipDocument.Location = new Point(45, 25);
            m_cbTipDocument.Name = "m_cbTipDocument";
            m_cbTipDocument.Size = new Size(240, 28);
            m_cbTipDocument.TabIndex = 6;
            m_cbTipDocument.SelectedIndexChanged += m_cbTipDocument_SelectedIndexChanged;
            // 
            // m_ebCISerie
            // 
            m_ebCISerie.BackColor = Color.White;
            m_ebCISerie.Location = new Point(131, 63);
            m_ebCISerie.Margin = new Padding(5, 4, 5, 4);
            m_ebCISerie.Name = "m_ebCISerie";
            m_ebCISerie.Size = new Size(105, 27);
            m_ebCISerie.TabIndex = 7;
            m_ebCISerie.TextChanged += m_ebCISerie_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(24, 102);
            label2.Margin = new Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new Size(54, 20);
            label2.TabIndex = 22;
            label2.Text = "Numar";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(24, 68);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(42, 20);
            label1.TabIndex = 20;
            label1.Text = "Serie";
            // 
            // m_ebCINumar
            // 
            m_ebCINumar.BackColor = Color.White;
            m_ebCINumar.Location = new Point(131, 98);
            m_ebCINumar.Margin = new Padding(5, 4, 5, 4);
            m_ebCINumar.Name = "m_ebCINumar";
            m_ebCINumar.Size = new Size(105, 27);
            m_ebCINumar.TabIndex = 8;
            m_ebCINumar.TextChanged += m_ebCINumar_TextChanged;
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Location = new Point(521, 80);
            label24.Margin = new Padding(5, 0, 5, 0);
            label24.Name = "label24";
            label24.Size = new Size(64, 20);
            label24.TabIndex = 22;
            label24.Text = "Barcode";
            // 
            // m_ebNume
            // 
            m_ebNume.BackColor = Color.FromArgb(255, 255, 192);
            m_ebNume.Location = new Point(131, 37);
            m_ebNume.Margin = new Padding(5, 4, 5, 4);
            m_ebNume.Name = "m_ebNume";
            m_ebNume.Size = new Size(354, 27);
            m_ebNume.TabIndex = 0;
            m_ebNume.TextChanged += m_ebNume_TextChanged;
            // 
            // m_ebNrMatricol
            // 
            m_ebNrMatricol.Location = new Point(647, 37);
            m_ebNrMatricol.Margin = new Padding(5, 4, 5, 4);
            m_ebNrMatricol.Name = "m_ebNrMatricol";
            m_ebNrMatricol.ReadOnly = true;
            m_ebNrMatricol.Size = new Size(174, 27);
            m_ebNrMatricol.TabIndex = 1;
            // 
            // m_st3
            // 
            m_st3.AutoSize = true;
            m_st3.Location = new Point(521, 43);
            m_st3.Margin = new Padding(5, 0, 5, 0);
            m_st3.Name = "m_st3";
            m_st3.Size = new Size(113, 20);
            m_st3.TabIndex = 9;
            m_st3.Text = "Numar matricol";
            // 
            // m_st1
            // 
            m_st1.AutoSize = true;
            m_st1.Location = new Point(24, 43);
            m_st1.Margin = new Padding(5, 0, 5, 0);
            m_st1.Name = "m_st1";
            m_st1.Size = new Size(49, 20);
            m_st1.TabIndex = 11;
            m_st1.Text = "Nume";
            // 
            // m_ebPrenume
            // 
            m_ebPrenume.BackColor = Color.FromArgb(255, 255, 192);
            m_ebPrenume.Location = new Point(131, 71);
            m_ebPrenume.Margin = new Padding(5, 4, 5, 4);
            m_ebPrenume.Name = "m_ebPrenume";
            m_ebPrenume.Size = new Size(354, 27);
            m_ebPrenume.TabIndex = 1;
            m_ebPrenume.TextChanged += m_ebPrenume_TextChanged;
            // 
            // m_st4
            // 
            m_st4.AutoSize = true;
            m_st4.Location = new Point(24, 112);
            m_st4.Margin = new Padding(5, 0, 5, 0);
            m_st4.Name = "m_st4";
            m_st4.Size = new Size(37, 20);
            m_st4.TabIndex = 15;
            m_st4.Text = "CNP";
            // 
            // m_st2
            // 
            m_st2.AutoSize = true;
            m_st2.Location = new Point(24, 76);
            m_st2.Margin = new Padding(5, 0, 5, 0);
            m_st2.Name = "m_st2";
            m_st2.Size = new Size(67, 20);
            m_st2.TabIndex = 13;
            m_st2.Text = "Prenume";
            // 
            // m_ebCNP
            // 
            m_ebCNP.BackColor = Color.White;
            m_ebCNP.Location = new Point(131, 107);
            m_ebCNP.Margin = new Padding(5, 4, 5, 4);
            m_ebCNP.Name = "m_ebCNP";
            m_ebCNP.Size = new Size(126, 27);
            m_ebCNP.TabIndex = 4;
            m_ebCNP.TextChanged += m_ebCNP_TextChanged;
            // 
            // m_cbCetAteanStrain
            // 
            m_cbCetAteanStrain.AutoSize = true;
            m_cbCetAteanStrain.Location = new Point(24, 143);
            m_cbCetAteanStrain.Margin = new Padding(5, 4, 5, 4);
            m_cbCetAteanStrain.Name = "m_cbCetAteanStrain";
            m_cbCetAteanStrain.Size = new Size(130, 24);
            m_cbCetAteanStrain.TabIndex = 5;
            m_cbCetAteanStrain.Text = "Cetățean străin";
            m_cbCetAteanStrain.CheckedChanged += m_cbCetAteanStrain_CheckedChanged;
            // 
            // m_lblNrDocument
            // 
            m_lblNrDocument.AutoSize = true;
            m_lblNrDocument.Location = new Point(159, 145);
            m_lblNrDocument.Margin = new Padding(5, 0, 5, 0);
            m_lblNrDocument.Name = "m_lblNrDocument";
            m_lblNrDocument.Size = new Size(99, 20);
            m_lblNrDocument.TabIndex = 40;
            m_lblNrDocument.Text = "Nr. document";
            m_lblNrDocument.Visible = false;
            // 
            // m_ebNrDocumentStrain
            // 
            m_ebNrDocumentStrain.BackColor = Color.White;
            m_ebNrDocumentStrain.Location = new Point(264, 140);
            m_ebNrDocumentStrain.Margin = new Padding(5, 4, 5, 4);
            m_ebNrDocumentStrain.Name = "m_ebNrDocumentStrain";
            m_ebNrDocumentStrain.Size = new Size(221, 27);
            m_ebNrDocumentStrain.TabIndex = 6;
            m_ebNrDocumentStrain.Visible = false;
            m_ebNrDocumentStrain.TextChanged += m_ebNrDocumentStrain_TextChanged;
            // 
            // m_tabControl
            // 
            m_tabControl.Controls.Add(m_tabPageDate);
            m_tabControl.Controls.Add(m_tabPageContracte);
            m_tabControl.Controls.Add(m_tabPageArhivaCursant);
            m_tabControl.Dock = DockStyle.Fill;
            m_tabControl.Location = new Point(0, 27);
            m_tabControl.Margin = new Padding(3, 4, 3, 4);
            m_tabControl.Name = "m_tabControl";
            m_tabControl.SelectedIndex = 0;
            m_tabControl.Size = new Size(1151, 1020);
            m_tabControl.TabIndex = 4;
            // 
            // m_tabPageDate
            // 
            m_tabPageDate.Controls.Add(panel1);
            m_tabPageDate.Location = new Point(4, 29);
            m_tabPageDate.Margin = new Padding(3, 4, 3, 4);
            m_tabPageDate.Name = "m_tabPageDate";
            m_tabPageDate.Size = new Size(1143, 987);
            m_tabPageDate.TabIndex = 0;
            m_tabPageDate.Text = "Date cursant";
            m_tabPageDate.UseVisualStyleBackColor = true;
            // 
            // m_tabPageContracte
            // 
            m_tabPageContracte.Controls.Add(m_splitContracte);
            m_tabPageContracte.Location = new Point(4, 29);
            m_tabPageContracte.Margin = new Padding(3, 4, 3, 4);
            m_tabPageContracte.Name = "m_tabPageContracte";
            m_tabPageContracte.Size = new Size(1143, 987);
            m_tabPageContracte.TabIndex = 1;
            m_tabPageContracte.Text = "Contracte";
            m_tabPageContracte.UseVisualStyleBackColor = true;
            // 
            // m_splitContracte
            // 
            m_splitContracte.Dock = DockStyle.Fill;
            m_splitContracte.Location = new Point(0, 0);
            m_splitContracte.Name = "m_splitContracte";
            m_splitContracte.Orientation = Orientation.Horizontal;
            // 
            // m_splitContracte.Panel1
            // 
            m_splitContracte.Panel1.Controls.Add(m_gContracteCursant);
            // 
            // m_splitContracte.Panel2
            // 
            m_splitContracte.Panel2.Controls.Add(m_gAbonamenteCursant);
            m_splitContracte.Size = new Size(1143, 987);
            m_splitContracte.SplitterDistance = 280;
            m_splitContracte.TabIndex = 0;
            // 
            // m_gContracteCursant
            // 
            m_gContracteCursant.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_gContracteCursant.Dock = DockStyle.Fill;
            m_gContracteCursant.Font = new Font("Arial", 8F);
            m_gContracteCursant.Location = new Point(0, 0);
            m_gContracteCursant.Name = "m_gContracteCursant";
            m_gContracteCursant.ReadOnly = true;
            m_gContracteCursant.RowHeadersWidth = 51;
            m_gContracteCursant.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            m_gContracteCursant.Size = new Size(1143, 280);
            m_gContracteCursant.TabIndex = 0;
            m_gContracteCursant.SelectionChanged += m_gContracteCursant_SelectionChanged;
            // 
            // m_gAbonamenteCursant
            // 
            m_gAbonamenteCursant.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_gAbonamenteCursant.Dock = DockStyle.Fill;
            m_gAbonamenteCursant.Font = new Font("Arial", 8F);
            m_gAbonamenteCursant.Location = new Point(0, 0);
            m_gAbonamenteCursant.Name = "m_gAbonamenteCursant";
            m_gAbonamenteCursant.ReadOnly = true;
            m_gAbonamenteCursant.RowHeadersWidth = 51;
            m_gAbonamenteCursant.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            m_gAbonamenteCursant.Size = new Size(1143, 703);
            m_gAbonamenteCursant.TabIndex = 0;
            // 
            // m_tabPageArhivaCursant
            // 
            m_tabPageArhivaCursant.Controls.Add(m_gArhivaCursant);
            m_tabPageArhivaCursant.Controls.Add(m_panelArhivaCursantButtons);
            m_tabPageArhivaCursant.Location = new Point(4, 29);
            m_tabPageArhivaCursant.Margin = new Padding(3, 4, 3, 4);
            m_tabPageArhivaCursant.Name = "m_tabPageArhivaCursant";
            m_tabPageArhivaCursant.Size = new Size(1143, 987);
            m_tabPageArhivaCursant.TabIndex = 2;
            m_tabPageArhivaCursant.Text = "Arhivă";
            m_tabPageArhivaCursant.UseVisualStyleBackColor = true;
            // 
            // m_gArhivaCursant
            // 
            m_gArhivaCursant.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_gArhivaCursant.Dock = DockStyle.Fill;
            m_gArhivaCursant.Font = new Font("Arial", 8F);
            m_gArhivaCursant.Location = new Point(0, 48);
            m_gArhivaCursant.Margin = new Padding(3, 4, 3, 4);
            m_gArhivaCursant.Name = "m_gArhivaCursant";
            m_gArhivaCursant.ReadOnly = true;
            m_gArhivaCursant.RowHeadersWidth = 51;
            m_gArhivaCursant.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            m_gArhivaCursant.Size = new Size(1143, 939);
            m_gArhivaCursant.TabIndex = 1;
            // 
            // m_panelArhivaCursantButtons
            // 
            m_panelArhivaCursantButtons.Controls.Add(m_btnStergeArhivaCursant);
            m_panelArhivaCursantButtons.Controls.Add(m_btnDeschideArhivaCursant);
            m_panelArhivaCursantButtons.Controls.Add(m_btnAdaugaArhivaCursant);
            m_panelArhivaCursantButtons.Dock = DockStyle.Top;
            m_panelArhivaCursantButtons.Location = new Point(0, 0);
            m_panelArhivaCursantButtons.Margin = new Padding(3, 4, 3, 4);
            m_panelArhivaCursantButtons.Name = "m_panelArhivaCursantButtons";
            m_panelArhivaCursantButtons.Size = new Size(1143, 48);
            m_panelArhivaCursantButtons.TabIndex = 0;
            // 
            // m_btnStergeArhivaCursant
            // 
            m_btnStergeArhivaCursant.Location = new Point(246, 7);
            m_btnStergeArhivaCursant.Margin = new Padding(3, 4, 3, 4);
            m_btnStergeArhivaCursant.Name = "m_btnStergeArhivaCursant";
            m_btnStergeArhivaCursant.Size = new Size(91, 35);
            m_btnStergeArhivaCursant.TabIndex = 2;
            m_btnStergeArhivaCursant.Text = "Șterge";
            m_btnStergeArhivaCursant.Click += m_btnStergeArhivaCursant_Click;
            // 
            // m_btnDeschideArhivaCursant
            // 
            m_btnDeschideArhivaCursant.Location = new Point(136, 7);
            m_btnDeschideArhivaCursant.Margin = new Padding(3, 4, 3, 4);
            m_btnDeschideArhivaCursant.Name = "m_btnDeschideArhivaCursant";
            m_btnDeschideArhivaCursant.Size = new Size(103, 35);
            m_btnDeschideArhivaCursant.TabIndex = 1;
            m_btnDeschideArhivaCursant.Text = "Deschide";
            m_btnDeschideArhivaCursant.Click += m_btnDeschideArhivaCursant_Click;
            // 
            // m_btnAdaugaArhivaCursant
            // 
            m_btnAdaugaArhivaCursant.Location = new Point(3, 7);
            m_btnAdaugaArhivaCursant.Margin = new Padding(3, 4, 3, 4);
            m_btnAdaugaArhivaCursant.Name = "m_btnAdaugaArhivaCursant";
            m_btnAdaugaArhivaCursant.Size = new Size(126, 35);
            m_btnAdaugaArhivaCursant.TabIndex = 0;
            m_btnAdaugaArhivaCursant.Text = "Adaugă fișier";
            m_btnAdaugaArhivaCursant.Click += m_btnAdaugaArhivaCursant_Click;
            // 
            // FrmGestiuneCursant
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1151, 1047);
            Controls.Add(m_tabControl);
            Controls.Add(toolStrip1);
            Margin = new Padding(5, 4, 5, 4);
            Name = "FrmGestiuneCursant";
            Text = "Gestiune Cursant";
            WindowState = FormWindowState.Maximized;
            FormClosing += FrmGestiuneCursant_FormClosing;
            Load += FrmGestiuneCursant_Load;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox8.ResumeLayout(false);
            groupBox8.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            m_tabControl.ResumeLayout(false);
            m_tabPageDate.ResumeLayout(false);
            m_tabPageContracte.ResumeLayout(false);
            m_splitContracte.Panel1.ResumeLayout(false);
            m_splitContracte.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)m_splitContracte).EndInit();
            m_splitContracte.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)m_gContracteCursant).EndInit();
            ((System.ComponentModel.ISupportInitialize)m_gAbonamenteCursant).EndInit();
            m_tabPageArhivaCursant.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)m_gArhivaCursant).EndInit();
            m_panelArhivaCursantButtons.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton m_stbSalveaza;
        private System.Windows.Forms.ToolStripButton m_tsbDeschideArhiva;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox m_ebNumeFB;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox m_ebNumeSkype;
        private System.Windows.Forms.TextBox m_ebEmail;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox m_ebTelMobil;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox m_ebTelFix;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox m_ebJudet;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox m_ebAp;
        private System.Windows.Forms.TextBox m_ebOras;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox m_ebScara;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox m_ebBloc;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox m_ebNumarStrada;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox m_ebStrada;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox m_ebNumeTutore;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox m_ebCISerie;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_ebCINumar;
        private Label m_lblTipDocument;
        private ComboBox m_cbTipDocument;
        private System.Windows.Forms.TextBox m_ebJudetulNasterii;
        private System.Windows.Forms.TextBox m_ebLocalitateaNasterii;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.DateTimePicker m_dtDataNasterii;
        private System.Windows.Forms.Label m_st5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox m_ebNume;
        private System.Windows.Forms.Label m_st1;
        private System.Windows.Forms.TextBox m_ebPrenume;
        private System.Windows.Forms.Label m_st4;
        private System.Windows.Forms.Label m_st2;
        private System.Windows.Forms.TextBox m_ebCNP;
        private CheckBox m_cbCetAteanStrain;
        private Label m_lblNrDocument;
        private TextBox m_ebNrDocumentStrain;
        private System.Windows.Forms.TextBox m_ebPrenumeTutore;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox m_ebEmailTutore;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox m_ebTelefonTutore;
        private System.Windows.Forms.Label label17;
        private GroupBox groupBox8;
        private TextBox m_ebCISerieTutore;
        private Label label10;
        private Label label13;
        private TextBox m_ebCINumarTutore;
        private Label label21;
        private TextBox m_ebCNPTutore;
        private TextBox m_ebBarcode;
        private TextBox m_ebFideId;
        private Label m_stFideId;
        private Label label24;
        private TextBox m_ebNrMatricol;
        private Label m_st3;
        private ToolStripButton m_tsbListeazaBarcode;
        private ToolStripSeparator m_tslSepNume;
        private ToolStripLabel m_tslNume;
        private GroupBox groupBox5;
        private TabControl m_tabControl;
        private TabPage m_tabPageDate;
        private TabPage m_tabPageContracte;
        private SplitContainer m_splitContracte;
        private TabPage m_tabPageArhivaCursant;
        private CustomGrid m_gContracteCursant;
        private CustomGrid m_gAbonamenteCursant;
        private Panel m_panelArhivaCursantButtons;
        private Button m_btnAdaugaArhivaCursant;
        private Button m_btnDeschideArhivaCursant;
        private Button m_btnStergeArhivaCursant;
        private CustomGrid m_gArhivaCursant;
    }
}
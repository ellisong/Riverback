namespace Riverback
{
    partial class MainForm
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
            if (disposing && (components != null)) {
                components.Dispose();
                bitmapLevel.Dispose();
                bitmapTilemapTile.Dispose();
                bitmapTileset.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainPanel = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.radioButton_field_edit = new System.Windows.Forms.RadioButton();
            this.radioButton_physmap_edit = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.checkBox_field_show = new System.Windows.Forms.CheckBox();
            this.checkBox_physmap_show = new System.Windows.Forms.CheckBox();
            this.checkBox_bytes_show = new System.Windows.Forms.CheckBox();
            this.checkBox_grid_show = new System.Windows.Forms.CheckBox();
            this.button_deselect = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_Tileset = new System.Windows.Forms.TabPage();
            this.pictureBox_tileset = new System.Windows.Forms.PictureBox();
            this.tabPage_Phystiles = new System.Windows.Forms.TabPage();
            this.pictureBox_phystiles = new System.Windows.Forms.PictureBox();
            this.tabPage_Indextiles = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button_applyindices = new System.Windows.Forms.Button();
            this.textBox_tilesremaining = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox_indexTiles = new System.Windows.Forms.PictureBox();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage_Level = new System.Windows.Forms.TabPage();
            this.pictureBox_level = new System.Windows.Forms.PictureBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.checkBox_wavywater = new System.Windows.Forms.CheckBox();
            this.textBox_levelpointer = new System.Windows.Forms.TextBox();
            this.textBox_headeraddress = new System.Windows.Forms.TextBox();
            this.textBox_headerpointer = new System.Windows.Forms.TextBox();
            this.textBox_headernumber = new System.Windows.Forms.TextBox();
            this.button_applyheader = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.numericUpDown_paletteindices6 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_paletteindices5 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_paletteindices4 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_paletteindices3 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_paletteindices2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_paletteindices1 = new System.Windows.Forms.NumericUpDown();
            this.label18 = new System.Windows.Forms.Label();
            this.numericUpDown_waterheight = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_objecttype7 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_spawnrate8 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_spawnrate7 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_objecttype6 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_spawnrate6 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_objecttype5 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_spawnrate5 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_objecttype4 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_spawnrate4 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_objecttype3 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_spawnrate3 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_objecttype2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_spawnrate2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_objecttype1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_spawnrate1 = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.numericUpDown_doorexit4 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_doorexit3 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_doorexit2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_doorexit1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_leveltimer = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.numericUpDown_enemytype6 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_enemytype5 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_enemytype4 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_enemytype3 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_enemytype2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_enemytype1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_musicselect = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_fieldnumber = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_graphicsbankindex = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown_levelSelector = new System.Windows.Forms.NumericUpDown();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pictureBox_phystile = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.checkBox_vflip = new System.Windows.Forms.CheckBox();
            this.checkBox_hflip = new System.Windows.Forms.CheckBox();
            this.checkBox_priority = new System.Windows.Forms.CheckBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown_tilePalette = new System.Windows.Forms.NumericUpDown();
            this.pictureBox_tilemaptile = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.MainMenu_File = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_File_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.saveLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.mainPanel.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.flowLayoutPanel5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage_Tileset.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_tileset)).BeginInit();
            this.tabPage_Phystiles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_phystiles)).BeginInit();
            this.tabPage_Indextiles.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_indexTiles)).BeginInit();
            this.tabControl2.SuspendLayout();
            this.tabPage_Level.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_level)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_paletteindices6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_paletteindices5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_paletteindices4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_paletteindices3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_paletteindices2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_paletteindices1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_waterheight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_objecttype7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_spawnrate8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_spawnrate7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_objecttype6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_spawnrate6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_objecttype5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_spawnrate5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_objecttype4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_spawnrate4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_objecttype3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_spawnrate3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_objecttype2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_spawnrate2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_objecttype1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_spawnrate1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_doorexit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_doorexit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_doorexit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_doorexit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_leveltimer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_enemytype6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_enemytype5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_enemytype4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_enemytype3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_enemytype2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_enemytype1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_musicselect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_fieldnumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_graphicsbankindex)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_levelSelector)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_phystile)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_tilePalette)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_tilemaptile)).BeginInit();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.AutoSize = true;
            this.mainPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mainPanel.Controls.Add(this.groupBox2);
            this.mainPanel.Controls.Add(this.groupBox1);
            this.mainPanel.Controls.Add(this.button_deselect);
            this.mainPanel.Controls.Add(this.flowLayoutPanel1);
            this.mainPanel.Controls.Add(this.panel2);
            this.mainPanel.Controls.Add(this.panel5);
            this.mainPanel.Controls.Add(this.MainMenu);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(977, 953);
            this.mainPanel.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.flowLayoutPanel5);
            this.groupBox2.Location = new System.Drawing.Point(184, 31);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(137, 43);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Edit";
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.AutoSize = true;
            this.flowLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel5.Controls.Add(this.radioButton_field_edit);
            this.flowLayoutPanel5.Controls.Add(this.radioButton_physmap_edit);
            this.flowLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel5.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(131, 24);
            this.flowLayoutPanel5.TabIndex = 0;
            // 
            // radioButton_field_edit
            // 
            this.radioButton_field_edit.AutoSize = true;
            this.radioButton_field_edit.Checked = true;
            this.radioButton_field_edit.Location = new System.Drawing.Point(3, 3);
            this.radioButton_field_edit.Name = "radioButton_field_edit";
            this.radioButton_field_edit.Size = new System.Drawing.Size(47, 17);
            this.radioButton_field_edit.TabIndex = 0;
            this.radioButton_field_edit.TabStop = true;
            this.radioButton_field_edit.Text = "Field";
            this.radioButton_field_edit.UseVisualStyleBackColor = true;
            // 
            // radioButton_physmap_edit
            // 
            this.radioButton_physmap_edit.AutoSize = true;
            this.radioButton_physmap_edit.Location = new System.Drawing.Point(56, 3);
            this.radioButton_physmap_edit.Name = "radioButton_physmap_edit";
            this.radioButton_physmap_edit.Size = new System.Drawing.Size(68, 17);
            this.radioButton_physmap_edit.TabIndex = 1;
            this.radioButton_physmap_edit.TabStop = true;
            this.radioButton_physmap_edit.Text = "Physmap";
            this.radioButton_physmap_edit.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flowLayoutPanel4);
            this.groupBox1.Location = new System.Drawing.Point(184, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(244, 42);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Show";
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.AutoSize = true;
            this.flowLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel4.Controls.Add(this.checkBox_field_show);
            this.flowLayoutPanel4.Controls.Add(this.checkBox_physmap_show);
            this.flowLayoutPanel4.Controls.Add(this.checkBox_bytes_show);
            this.flowLayoutPanel4.Controls.Add(this.checkBox_grid_show);
            this.flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(238, 23);
            this.flowLayoutPanel4.TabIndex = 0;
            // 
            // checkBox_field_show
            // 
            this.checkBox_field_show.AutoSize = true;
            this.checkBox_field_show.Checked = true;
            this.checkBox_field_show.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_field_show.Location = new System.Drawing.Point(3, 3);
            this.checkBox_field_show.Name = "checkBox_field_show";
            this.checkBox_field_show.Size = new System.Drawing.Size(48, 17);
            this.checkBox_field_show.TabIndex = 1;
            this.checkBox_field_show.Text = "Field";
            this.checkBox_field_show.UseVisualStyleBackColor = true;
            this.checkBox_field_show.CheckedChanged += new System.EventHandler(this.checkBox_field_show_CheckedChanged);
            // 
            // checkBox_physmap_show
            // 
            this.checkBox_physmap_show.AutoSize = true;
            this.checkBox_physmap_show.Location = new System.Drawing.Point(57, 3);
            this.checkBox_physmap_show.Name = "checkBox_physmap_show";
            this.checkBox_physmap_show.Size = new System.Drawing.Size(69, 17);
            this.checkBox_physmap_show.TabIndex = 0;
            this.checkBox_physmap_show.Text = "Physmap";
            this.checkBox_physmap_show.UseVisualStyleBackColor = true;
            this.checkBox_physmap_show.CheckedChanged += new System.EventHandler(this.checkBox_physmap_show_CheckedChanged);
            // 
            // checkBox_bytes_show
            // 
            this.checkBox_bytes_show.AutoSize = true;
            this.checkBox_bytes_show.Location = new System.Drawing.Point(132, 3);
            this.checkBox_bytes_show.Name = "checkBox_bytes_show";
            this.checkBox_bytes_show.Size = new System.Drawing.Size(52, 17);
            this.checkBox_bytes_show.TabIndex = 2;
            this.checkBox_bytes_show.Text = "Bytes";
            this.checkBox_bytes_show.UseVisualStyleBackColor = true;
            this.checkBox_bytes_show.CheckedChanged += new System.EventHandler(this.checkBox_bytes_CheckedChanged);
            // 
            // checkBox_grid_show
            // 
            this.checkBox_grid_show.AutoSize = true;
            this.checkBox_grid_show.Location = new System.Drawing.Point(190, 3);
            this.checkBox_grid_show.Name = "checkBox_grid_show";
            this.checkBox_grid_show.Size = new System.Drawing.Size(45, 17);
            this.checkBox_grid_show.TabIndex = 3;
            this.checkBox_grid_show.Text = "Grid";
            this.checkBox_grid_show.UseVisualStyleBackColor = true;
            this.checkBox_grid_show.CheckedChanged += new System.EventHandler(this.checkBox_grid_show_CheckedChanged);
            // 
            // button_deselect
            // 
            this.button_deselect.Location = new System.Drawing.Point(327, 48);
            this.button_deselect.Name = "button_deselect";
            this.button_deselect.Size = new System.Drawing.Size(98, 21);
            this.button_deselect.TabIndex = 7;
            this.button_deselect.Text = "Deselect Tiles";
            this.button_deselect.UseVisualStyleBackColor = true;
            this.button_deselect.Click += new System.EventHandler(this.button_deselect_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.tabControl1);
            this.flowLayoutPanel1.Controls.Add(this.tabControl2);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(4, 125);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(808, 550);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_Tileset);
            this.tabControl1.Controls.Add(this.tabPage_Phystiles);
            this.tabControl1.Controls.Add(this.tabPage_Indextiles);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(270, 544);
            this.tabControl1.TabIndex = 13;
            // 
            // tabPage_Tileset
            // 
            this.tabPage_Tileset.Controls.Add(this.pictureBox_tileset);
            this.tabPage_Tileset.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Tileset.Name = "tabPage_Tileset";
            this.tabPage_Tileset.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Tileset.Size = new System.Drawing.Size(262, 518);
            this.tabPage_Tileset.TabIndex = 0;
            this.tabPage_Tileset.Text = "Field Tiles";
            this.tabPage_Tileset.UseVisualStyleBackColor = true;
            // 
            // pictureBox_tileset
            // 
            this.pictureBox_tileset.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBox_tileset.ErrorImage = null;
            this.pictureBox_tileset.InitialImage = null;
            this.pictureBox_tileset.Location = new System.Drawing.Point(3, 3);
            this.pictureBox_tileset.Name = "pictureBox_tileset";
            this.pictureBox_tileset.Size = new System.Drawing.Size(256, 512);
            this.pictureBox_tileset.TabIndex = 0;
            this.pictureBox_tileset.TabStop = false;
            this.pictureBox_tileset.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_tileset_MouseClick);
            // 
            // tabPage_Phystiles
            // 
            this.tabPage_Phystiles.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_Phystiles.Controls.Add(this.pictureBox_phystiles);
            this.tabPage_Phystiles.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Phystiles.Name = "tabPage_Phystiles";
            this.tabPage_Phystiles.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Phystiles.Size = new System.Drawing.Size(262, 518);
            this.tabPage_Phystiles.TabIndex = 1;
            this.tabPage_Phystiles.Text = "Physmap Tiles";
            // 
            // pictureBox_phystiles
            // 
            this.pictureBox_phystiles.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBox_phystiles.ErrorImage = null;
            this.pictureBox_phystiles.InitialImage = null;
            this.pictureBox_phystiles.Location = new System.Drawing.Point(3, 3);
            this.pictureBox_phystiles.Name = "pictureBox_phystiles";
            this.pictureBox_phystiles.Size = new System.Drawing.Size(256, 256);
            this.pictureBox_phystiles.TabIndex = 0;
            this.pictureBox_phystiles.TabStop = false;
            this.pictureBox_phystiles.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_phystiles_MouseClick);
            // 
            // tabPage_Indextiles
            // 
            this.tabPage_Indextiles.Controls.Add(this.textBox1);
            this.tabPage_Indextiles.Controls.Add(this.button_applyindices);
            this.tabPage_Indextiles.Controls.Add(this.textBox_tilesremaining);
            this.tabPage_Indextiles.Controls.Add(this.label4);
            this.tabPage_Indextiles.Controls.Add(this.panel1);
            this.tabPage_Indextiles.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Indextiles.Name = "tabPage_Indextiles";
            this.tabPage_Indextiles.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Indextiles.Size = new System.Drawing.Size(262, 518);
            this.tabPage_Indextiles.TabIndex = 2;
            this.tabPage_Indextiles.Text = "Index Tiles";
            this.tabPage_Indextiles.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(161, 216);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(89, 283);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // button_applyindices
            // 
            this.button_applyindices.Location = new System.Drawing.Point(161, 59);
            this.button_applyindices.Name = "button_applyindices";
            this.button_applyindices.Size = new System.Drawing.Size(89, 23);
            this.button_applyindices.TabIndex = 3;
            this.button_applyindices.Text = "Apply Indices";
            this.button_applyindices.UseVisualStyleBackColor = true;
            this.button_applyindices.Click += new System.EventHandler(this.button_applyindices_Click);
            // 
            // textBox_tilesremaining
            // 
            this.textBox_tilesremaining.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_tilesremaining.Location = new System.Drawing.Point(173, 23);
            this.textBox_tilesremaining.Name = "textBox_tilesremaining";
            this.textBox_tilesremaining.ReadOnly = true;
            this.textBox_tilesremaining.Size = new System.Drawing.Size(77, 29);
            this.textBox_tilesremaining.TabIndex = 2;
            this.textBox_tilesremaining.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(158, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Tiles remaining:";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.pictureBox_indexTiles);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(148, 496);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox_indexTiles
            // 
            this.pictureBox_indexTiles.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBox_indexTiles.ErrorImage = null;
            this.pictureBox_indexTiles.InitialImage = null;
            this.pictureBox_indexTiles.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_indexTiles.Name = "pictureBox_indexTiles";
            this.pictureBox_indexTiles.Size = new System.Drawing.Size(128, 1024);
            this.pictureBox_indexTiles.TabIndex = 0;
            this.pictureBox_indexTiles.TabStop = false;
            this.pictureBox_indexTiles.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_indexTiles_MouseClick);
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage_Level);
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Location = new System.Drawing.Point(279, 3);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(526, 544);
            this.tabControl2.TabIndex = 13;
            // 
            // tabPage_Level
            // 
            this.tabPage_Level.Controls.Add(this.pictureBox_level);
            this.tabPage_Level.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Level.Name = "tabPage_Level";
            this.tabPage_Level.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Level.Size = new System.Drawing.Size(518, 518);
            this.tabPage_Level.TabIndex = 0;
            this.tabPage_Level.Text = "Field Display";
            this.tabPage_Level.UseVisualStyleBackColor = true;
            // 
            // pictureBox_level
            // 
            this.pictureBox_level.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBox_level.ErrorImage = null;
            this.pictureBox_level.InitialImage = null;
            this.pictureBox_level.Location = new System.Drawing.Point(3, 3);
            this.pictureBox_level.Name = "pictureBox_level";
            this.pictureBox_level.Size = new System.Drawing.Size(512, 512);
            this.pictureBox_level.TabIndex = 1;
            this.pictureBox_level.TabStop = false;
            this.pictureBox_level.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_level_MouseDown);
            this.pictureBox_level.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_level_MouseMove);
            this.pictureBox_level.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_level_MouseUp);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label25);
            this.tabPage1.Controls.Add(this.label24);
            this.tabPage1.Controls.Add(this.label23);
            this.tabPage1.Controls.Add(this.label22);
            this.tabPage1.Controls.Add(this.label21);
            this.tabPage1.Controls.Add(this.label20);
            this.tabPage1.Controls.Add(this.label17);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.checkBox_wavywater);
            this.tabPage1.Controls.Add(this.textBox_levelpointer);
            this.tabPage1.Controls.Add(this.textBox_headeraddress);
            this.tabPage1.Controls.Add(this.textBox_headerpointer);
            this.tabPage1.Controls.Add(this.textBox_headernumber);
            this.tabPage1.Controls.Add(this.button_applyheader);
            this.tabPage1.Controls.Add(this.label19);
            this.tabPage1.Controls.Add(this.numericUpDown_paletteindices6);
            this.tabPage1.Controls.Add(this.numericUpDown_paletteindices5);
            this.tabPage1.Controls.Add(this.numericUpDown_paletteindices4);
            this.tabPage1.Controls.Add(this.numericUpDown_paletteindices3);
            this.tabPage1.Controls.Add(this.numericUpDown_paletteindices2);
            this.tabPage1.Controls.Add(this.numericUpDown_paletteindices1);
            this.tabPage1.Controls.Add(this.label18);
            this.tabPage1.Controls.Add(this.numericUpDown_waterheight);
            this.tabPage1.Controls.Add(this.numericUpDown_objecttype7);
            this.tabPage1.Controls.Add(this.numericUpDown_spawnrate8);
            this.tabPage1.Controls.Add(this.numericUpDown_spawnrate7);
            this.tabPage1.Controls.Add(this.numericUpDown_objecttype6);
            this.tabPage1.Controls.Add(this.numericUpDown_spawnrate6);
            this.tabPage1.Controls.Add(this.numericUpDown_objecttype5);
            this.tabPage1.Controls.Add(this.numericUpDown_spawnrate5);
            this.tabPage1.Controls.Add(this.numericUpDown_objecttype4);
            this.tabPage1.Controls.Add(this.numericUpDown_spawnrate4);
            this.tabPage1.Controls.Add(this.numericUpDown_objecttype3);
            this.tabPage1.Controls.Add(this.numericUpDown_spawnrate3);
            this.tabPage1.Controls.Add(this.numericUpDown_objecttype2);
            this.tabPage1.Controls.Add(this.numericUpDown_spawnrate2);
            this.tabPage1.Controls.Add(this.numericUpDown_objecttype1);
            this.tabPage1.Controls.Add(this.numericUpDown_spawnrate1);
            this.tabPage1.Controls.Add(this.label16);
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Controls.Add(this.numericUpDown_doorexit4);
            this.tabPage1.Controls.Add(this.numericUpDown_doorexit3);
            this.tabPage1.Controls.Add(this.numericUpDown_doorexit2);
            this.tabPage1.Controls.Add(this.numericUpDown_doorexit1);
            this.tabPage1.Controls.Add(this.numericUpDown_leveltimer);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.numericUpDown_enemytype6);
            this.tabPage1.Controls.Add(this.numericUpDown_enemytype5);
            this.tabPage1.Controls.Add(this.numericUpDown_enemytype4);
            this.tabPage1.Controls.Add(this.numericUpDown_enemytype3);
            this.tabPage1.Controls.Add(this.numericUpDown_enemytype2);
            this.tabPage1.Controls.Add(this.numericUpDown_enemytype1);
            this.tabPage1.Controls.Add(this.numericUpDown_musicselect);
            this.tabPage1.Controls.Add(this.numericUpDown_fieldnumber);
            this.tabPage1.Controls.Add(this.numericUpDown_graphicsbankindex);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(518, 518);
            this.tabPage1.TabIndex = 1;
            this.tabPage1.Text = "Level Header Editor";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(454, 340);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(55, 13);
            this.label25.TabIndex = 75;
            this.label25.Text = "07, 3F, 47";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(382, 340);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(56, 13);
            this.label24.TabIndex = 74;
            this.label24.Text = "06, 3E, 46";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(306, 340);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(57, 13);
            this.label23.TabIndex = 73;
            this.label23.Text = "05, 3D, 45";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(230, 340);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(56, 13);
            this.label22.TabIndex = 72;
            this.label22.Text = "04, 3C, 44";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(154, 340);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(56, 13);
            this.label21.TabIndex = 71;
            this.label21.Text = "03, 3B, 43";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(78, 340);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(56, 13);
            this.label20.TabIndex = 70;
            this.label20.Text = "02, 3A, 42";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(6, 340);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(55, 13);
            this.label17.TabIndex = 69;
            this.label17.Text = "01, 39, 41";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(8, 301);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(69, 13);
            this.label14.TabIndex = 68;
            this.label14.Text = "Object types:";
            // 
            // checkBox_wavywater
            // 
            this.checkBox_wavywater.AutoSize = true;
            this.checkBox_wavywater.Location = new System.Drawing.Point(215, 170);
            this.checkBox_wavywater.Name = "checkBox_wavywater";
            this.checkBox_wavywater.Size = new System.Drawing.Size(109, 17);
            this.checkBox_wavywater.TabIndex = 67;
            this.checkBox_wavywater.Text = "Water has waves";
            this.checkBox_wavywater.UseVisualStyleBackColor = true;
            // 
            // textBox_levelpointer
            // 
            this.textBox_levelpointer.Location = new System.Drawing.Point(327, 5);
            this.textBox_levelpointer.Name = "textBox_levelpointer";
            this.textBox_levelpointer.ReadOnly = true;
            this.textBox_levelpointer.Size = new System.Drawing.Size(63, 20);
            this.textBox_levelpointer.TabIndex = 65;
            // 
            // textBox_headeraddress
            // 
            this.textBox_headeraddress.Location = new System.Drawing.Point(327, 34);
            this.textBox_headeraddress.Name = "textBox_headeraddress";
            this.textBox_headeraddress.ReadOnly = true;
            this.textBox_headeraddress.Size = new System.Drawing.Size(63, 20);
            this.textBox_headeraddress.TabIndex = 64;
            // 
            // textBox_headerpointer
            // 
            this.textBox_headerpointer.Location = new System.Drawing.Point(134, 34);
            this.textBox_headerpointer.Name = "textBox_headerpointer";
            this.textBox_headerpointer.ReadOnly = true;
            this.textBox_headerpointer.Size = new System.Drawing.Size(63, 20);
            this.textBox_headerpointer.TabIndex = 63;
            // 
            // textBox_headernumber
            // 
            this.textBox_headernumber.Location = new System.Drawing.Point(134, 5);
            this.textBox_headernumber.Name = "textBox_headernumber";
            this.textBox_headernumber.ReadOnly = true;
            this.textBox_headernumber.Size = new System.Drawing.Size(63, 20);
            this.textBox_headernumber.TabIndex = 62;
            // 
            // button_applyheader
            // 
            this.button_applyheader.Location = new System.Drawing.Point(251, 84);
            this.button_applyheader.Name = "button_applyheader";
            this.button_applyheader.Size = new System.Drawing.Size(98, 23);
            this.button_applyheader.TabIndex = 61;
            this.button_applyheader.Text = "Apply Changes";
            this.button_applyheader.UseVisualStyleBackColor = true;
            this.button_applyheader.Click += new System.EventHandler(this.button_applyheader_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(8, 397);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(79, 13);
            this.label19.TabIndex = 60;
            this.label19.Text = "Palette indices:";
            // 
            // numericUpDown_paletteindices6
            // 
            this.numericUpDown_paletteindices6.Hexadecimal = true;
            this.numericUpDown_paletteindices6.Location = new System.Drawing.Point(395, 390);
            this.numericUpDown_paletteindices6.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDown_paletteindices6.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_paletteindices6.Name = "numericUpDown_paletteindices6";
            this.numericUpDown_paletteindices6.Size = new System.Drawing.Size(46, 20);
            this.numericUpDown_paletteindices6.TabIndex = 59;
            this.numericUpDown_paletteindices6.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDown_paletteindices5
            // 
            this.numericUpDown_paletteindices5.Hexadecimal = true;
            this.numericUpDown_paletteindices5.Location = new System.Drawing.Point(343, 390);
            this.numericUpDown_paletteindices5.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDown_paletteindices5.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_paletteindices5.Name = "numericUpDown_paletteindices5";
            this.numericUpDown_paletteindices5.Size = new System.Drawing.Size(46, 20);
            this.numericUpDown_paletteindices5.TabIndex = 58;
            this.numericUpDown_paletteindices5.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDown_paletteindices4
            // 
            this.numericUpDown_paletteindices4.Hexadecimal = true;
            this.numericUpDown_paletteindices4.Location = new System.Drawing.Point(291, 390);
            this.numericUpDown_paletteindices4.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDown_paletteindices4.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_paletteindices4.Name = "numericUpDown_paletteindices4";
            this.numericUpDown_paletteindices4.Size = new System.Drawing.Size(46, 20);
            this.numericUpDown_paletteindices4.TabIndex = 57;
            this.numericUpDown_paletteindices4.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDown_paletteindices3
            // 
            this.numericUpDown_paletteindices3.Hexadecimal = true;
            this.numericUpDown_paletteindices3.Location = new System.Drawing.Point(239, 390);
            this.numericUpDown_paletteindices3.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDown_paletteindices3.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_paletteindices3.Name = "numericUpDown_paletteindices3";
            this.numericUpDown_paletteindices3.Size = new System.Drawing.Size(46, 20);
            this.numericUpDown_paletteindices3.TabIndex = 56;
            this.numericUpDown_paletteindices3.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDown_paletteindices2
            // 
            this.numericUpDown_paletteindices2.Hexadecimal = true;
            this.numericUpDown_paletteindices2.Location = new System.Drawing.Point(187, 390);
            this.numericUpDown_paletteindices2.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDown_paletteindices2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_paletteindices2.Name = "numericUpDown_paletteindices2";
            this.numericUpDown_paletteindices2.Size = new System.Drawing.Size(46, 20);
            this.numericUpDown_paletteindices2.TabIndex = 55;
            this.numericUpDown_paletteindices2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDown_paletteindices1
            // 
            this.numericUpDown_paletteindices1.Hexadecimal = true;
            this.numericUpDown_paletteindices1.Location = new System.Drawing.Point(135, 390);
            this.numericUpDown_paletteindices1.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDown_paletteindices1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_paletteindices1.Name = "numericUpDown_paletteindices1";
            this.numericUpDown_paletteindices1.Size = new System.Drawing.Size(46, 20);
            this.numericUpDown_paletteindices1.TabIndex = 54;
            this.numericUpDown_paletteindices1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(8, 253);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(148, 13);
            this.label18.TabIndex = 53;
            this.label18.Text = "Spawn rates (Enemies 0 to 3):";
            // 
            // numericUpDown_waterheight
            // 
            this.numericUpDown_waterheight.Hexadecimal = true;
            this.numericUpDown_waterheight.Location = new System.Drawing.Point(134, 167);
            this.numericUpDown_waterheight.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_waterheight.Name = "numericUpDown_waterheight";
            this.numericUpDown_waterheight.Size = new System.Drawing.Size(63, 20);
            this.numericUpDown_waterheight.TabIndex = 50;
            // 
            // numericUpDown_objecttype7
            // 
            this.numericUpDown_objecttype7.Hexadecimal = true;
            this.numericUpDown_objecttype7.Location = new System.Drawing.Point(460, 317);
            this.numericUpDown_objecttype7.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_objecttype7.Name = "numericUpDown_objecttype7";
            this.numericUpDown_objecttype7.Size = new System.Drawing.Size(46, 20);
            this.numericUpDown_objecttype7.TabIndex = 49;
            // 
            // numericUpDown_spawnrate8
            // 
            this.numericUpDown_spawnrate8.Hexadecimal = true;
            this.numericUpDown_spawnrate8.Location = new System.Drawing.Point(388, 269);
            this.numericUpDown_spawnrate8.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_spawnrate8.Name = "numericUpDown_spawnrate8";
            this.numericUpDown_spawnrate8.Size = new System.Drawing.Size(46, 20);
            this.numericUpDown_spawnrate8.TabIndex = 48;
            // 
            // numericUpDown_spawnrate7
            // 
            this.numericUpDown_spawnrate7.Hexadecimal = true;
            this.numericUpDown_spawnrate7.Location = new System.Drawing.Point(341, 269);
            this.numericUpDown_spawnrate7.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_spawnrate7.Name = "numericUpDown_spawnrate7";
            this.numericUpDown_spawnrate7.Size = new System.Drawing.Size(46, 20);
            this.numericUpDown_spawnrate7.TabIndex = 47;
            // 
            // numericUpDown_objecttype6
            // 
            this.numericUpDown_objecttype6.Hexadecimal = true;
            this.numericUpDown_objecttype6.Location = new System.Drawing.Point(388, 317);
            this.numericUpDown_objecttype6.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_objecttype6.Name = "numericUpDown_objecttype6";
            this.numericUpDown_objecttype6.Size = new System.Drawing.Size(46, 20);
            this.numericUpDown_objecttype6.TabIndex = 46;
            // 
            // numericUpDown_spawnrate6
            // 
            this.numericUpDown_spawnrate6.Hexadecimal = true;
            this.numericUpDown_spawnrate6.Location = new System.Drawing.Point(278, 269);
            this.numericUpDown_spawnrate6.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_spawnrate6.Name = "numericUpDown_spawnrate6";
            this.numericUpDown_spawnrate6.Size = new System.Drawing.Size(46, 20);
            this.numericUpDown_spawnrate6.TabIndex = 45;
            // 
            // numericUpDown_objecttype5
            // 
            this.numericUpDown_objecttype5.Hexadecimal = true;
            this.numericUpDown_objecttype5.Location = new System.Drawing.Point(312, 317);
            this.numericUpDown_objecttype5.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_objecttype5.Name = "numericUpDown_objecttype5";
            this.numericUpDown_objecttype5.Size = new System.Drawing.Size(46, 20);
            this.numericUpDown_objecttype5.TabIndex = 44;
            // 
            // numericUpDown_spawnrate5
            // 
            this.numericUpDown_spawnrate5.Hexadecimal = true;
            this.numericUpDown_spawnrate5.Location = new System.Drawing.Point(231, 269);
            this.numericUpDown_spawnrate5.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_spawnrate5.Name = "numericUpDown_spawnrate5";
            this.numericUpDown_spawnrate5.Size = new System.Drawing.Size(46, 20);
            this.numericUpDown_spawnrate5.TabIndex = 43;
            // 
            // numericUpDown_objecttype4
            // 
            this.numericUpDown_objecttype4.Hexadecimal = true;
            this.numericUpDown_objecttype4.Location = new System.Drawing.Point(236, 317);
            this.numericUpDown_objecttype4.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_objecttype4.Name = "numericUpDown_objecttype4";
            this.numericUpDown_objecttype4.Size = new System.Drawing.Size(46, 20);
            this.numericUpDown_objecttype4.TabIndex = 42;
            // 
            // numericUpDown_spawnrate4
            // 
            this.numericUpDown_spawnrate4.Hexadecimal = true;
            this.numericUpDown_spawnrate4.Location = new System.Drawing.Point(167, 269);
            this.numericUpDown_spawnrate4.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_spawnrate4.Name = "numericUpDown_spawnrate4";
            this.numericUpDown_spawnrate4.Size = new System.Drawing.Size(46, 20);
            this.numericUpDown_spawnrate4.TabIndex = 41;
            // 
            // numericUpDown_objecttype3
            // 
            this.numericUpDown_objecttype3.Hexadecimal = true;
            this.numericUpDown_objecttype3.Location = new System.Drawing.Point(160, 317);
            this.numericUpDown_objecttype3.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_objecttype3.Name = "numericUpDown_objecttype3";
            this.numericUpDown_objecttype3.Size = new System.Drawing.Size(46, 20);
            this.numericUpDown_objecttype3.TabIndex = 40;
            // 
            // numericUpDown_spawnrate3
            // 
            this.numericUpDown_spawnrate3.Hexadecimal = true;
            this.numericUpDown_spawnrate3.Location = new System.Drawing.Point(120, 269);
            this.numericUpDown_spawnrate3.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_spawnrate3.Name = "numericUpDown_spawnrate3";
            this.numericUpDown_spawnrate3.Size = new System.Drawing.Size(46, 20);
            this.numericUpDown_spawnrate3.TabIndex = 39;
            // 
            // numericUpDown_objecttype2
            // 
            this.numericUpDown_objecttype2.Hexadecimal = true;
            this.numericUpDown_objecttype2.Location = new System.Drawing.Point(84, 317);
            this.numericUpDown_objecttype2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_objecttype2.Name = "numericUpDown_objecttype2";
            this.numericUpDown_objecttype2.Size = new System.Drawing.Size(46, 20);
            this.numericUpDown_objecttype2.TabIndex = 38;
            // 
            // numericUpDown_spawnrate2
            // 
            this.numericUpDown_spawnrate2.Hexadecimal = true;
            this.numericUpDown_spawnrate2.Location = new System.Drawing.Point(58, 269);
            this.numericUpDown_spawnrate2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_spawnrate2.Name = "numericUpDown_spawnrate2";
            this.numericUpDown_spawnrate2.Size = new System.Drawing.Size(46, 20);
            this.numericUpDown_spawnrate2.TabIndex = 37;
            // 
            // numericUpDown_objecttype1
            // 
            this.numericUpDown_objecttype1.Hexadecimal = true;
            this.numericUpDown_objecttype1.Location = new System.Drawing.Point(8, 317);
            this.numericUpDown_objecttype1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_objecttype1.Name = "numericUpDown_objecttype1";
            this.numericUpDown_objecttype1.Size = new System.Drawing.Size(46, 20);
            this.numericUpDown_objecttype1.TabIndex = 36;
            // 
            // numericUpDown_spawnrate1
            // 
            this.numericUpDown_spawnrate1.Hexadecimal = true;
            this.numericUpDown_spawnrate1.Location = new System.Drawing.Point(11, 269);
            this.numericUpDown_spawnrate1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_spawnrate1.Name = "numericUpDown_spawnrate1";
            this.numericUpDown_spawnrate1.Size = new System.Drawing.Size(46, 20);
            this.numericUpDown_spawnrate1.TabIndex = 35;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(8, 228);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(58, 13);
            this.label16.TabIndex = 33;
            this.label16.Text = "Door Exits:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(8, 202);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(114, 13);
            this.label15.TabIndex = 32;
            this.label15.Text = "Level Timer (seconds):";
            // 
            // numericUpDown_doorexit4
            // 
            this.numericUpDown_doorexit4.Location = new System.Drawing.Point(341, 221);
            this.numericUpDown_doorexit4.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_doorexit4.Name = "numericUpDown_doorexit4";
            this.numericUpDown_doorexit4.Size = new System.Drawing.Size(63, 20);
            this.numericUpDown_doorexit4.TabIndex = 31;
            // 
            // numericUpDown_doorexit3
            // 
            this.numericUpDown_doorexit3.Location = new System.Drawing.Point(272, 221);
            this.numericUpDown_doorexit3.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_doorexit3.Name = "numericUpDown_doorexit3";
            this.numericUpDown_doorexit3.Size = new System.Drawing.Size(63, 20);
            this.numericUpDown_doorexit3.TabIndex = 30;
            // 
            // numericUpDown_doorexit2
            // 
            this.numericUpDown_doorexit2.Location = new System.Drawing.Point(203, 221);
            this.numericUpDown_doorexit2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_doorexit2.Name = "numericUpDown_doorexit2";
            this.numericUpDown_doorexit2.Size = new System.Drawing.Size(63, 20);
            this.numericUpDown_doorexit2.TabIndex = 29;
            // 
            // numericUpDown_doorexit1
            // 
            this.numericUpDown_doorexit1.Location = new System.Drawing.Point(134, 221);
            this.numericUpDown_doorexit1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_doorexit1.Name = "numericUpDown_doorexit1";
            this.numericUpDown_doorexit1.Size = new System.Drawing.Size(63, 20);
            this.numericUpDown_doorexit1.TabIndex = 28;
            // 
            // numericUpDown_leveltimer
            // 
            this.numericUpDown_leveltimer.Location = new System.Drawing.Point(134, 195);
            this.numericUpDown_leveltimer.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDown_leveltimer.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_leveltimer.Name = "numericUpDown_leveltimer";
            this.numericUpDown_leveltimer.Size = new System.Drawing.Size(63, 20);
            this.numericUpDown_leveltimer.TabIndex = 27;
            this.numericUpDown_leveltimer.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(7, 174);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(73, 13);
            this.label13.TabIndex = 23;
            this.label13.Text = "Water Height:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 146);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(74, 13);
            this.label12.TabIndex = 20;
            this.label12.Text = "Enemy Types:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 120);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 13);
            this.label11.TabIndex = 19;
            this.label11.Text = "Music Select:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 94);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "Field Number:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 68);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(109, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Graphics Bank Index:";
            // 
            // numericUpDown_enemytype6
            // 
            this.numericUpDown_enemytype6.Hexadecimal = true;
            this.numericUpDown_enemytype6.Location = new System.Drawing.Point(394, 139);
            this.numericUpDown_enemytype6.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_enemytype6.Name = "numericUpDown_enemytype6";
            this.numericUpDown_enemytype6.Size = new System.Drawing.Size(46, 20);
            this.numericUpDown_enemytype6.TabIndex = 16;
            // 
            // numericUpDown_enemytype5
            // 
            this.numericUpDown_enemytype5.Hexadecimal = true;
            this.numericUpDown_enemytype5.Location = new System.Drawing.Point(342, 139);
            this.numericUpDown_enemytype5.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_enemytype5.Name = "numericUpDown_enemytype5";
            this.numericUpDown_enemytype5.Size = new System.Drawing.Size(46, 20);
            this.numericUpDown_enemytype5.TabIndex = 15;
            // 
            // numericUpDown_enemytype4
            // 
            this.numericUpDown_enemytype4.Hexadecimal = true;
            this.numericUpDown_enemytype4.Location = new System.Drawing.Point(290, 139);
            this.numericUpDown_enemytype4.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_enemytype4.Name = "numericUpDown_enemytype4";
            this.numericUpDown_enemytype4.Size = new System.Drawing.Size(46, 20);
            this.numericUpDown_enemytype4.TabIndex = 14;
            // 
            // numericUpDown_enemytype3
            // 
            this.numericUpDown_enemytype3.Hexadecimal = true;
            this.numericUpDown_enemytype3.Location = new System.Drawing.Point(238, 139);
            this.numericUpDown_enemytype3.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_enemytype3.Name = "numericUpDown_enemytype3";
            this.numericUpDown_enemytype3.Size = new System.Drawing.Size(46, 20);
            this.numericUpDown_enemytype3.TabIndex = 13;
            // 
            // numericUpDown_enemytype2
            // 
            this.numericUpDown_enemytype2.Hexadecimal = true;
            this.numericUpDown_enemytype2.Location = new System.Drawing.Point(186, 139);
            this.numericUpDown_enemytype2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_enemytype2.Name = "numericUpDown_enemytype2";
            this.numericUpDown_enemytype2.Size = new System.Drawing.Size(46, 20);
            this.numericUpDown_enemytype2.TabIndex = 12;
            // 
            // numericUpDown_enemytype1
            // 
            this.numericUpDown_enemytype1.Hexadecimal = true;
            this.numericUpDown_enemytype1.Location = new System.Drawing.Point(134, 139);
            this.numericUpDown_enemytype1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_enemytype1.Name = "numericUpDown_enemytype1";
            this.numericUpDown_enemytype1.Size = new System.Drawing.Size(46, 20);
            this.numericUpDown_enemytype1.TabIndex = 11;
            // 
            // numericUpDown_musicselect
            // 
            this.numericUpDown_musicselect.Location = new System.Drawing.Point(134, 113);
            this.numericUpDown_musicselect.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDown_musicselect.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_musicselect.Name = "numericUpDown_musicselect";
            this.numericUpDown_musicselect.Size = new System.Drawing.Size(63, 20);
            this.numericUpDown_musicselect.TabIndex = 10;
            this.numericUpDown_musicselect.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDown_fieldnumber
            // 
            this.numericUpDown_fieldnumber.Location = new System.Drawing.Point(134, 87);
            this.numericUpDown_fieldnumber.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_fieldnumber.Name = "numericUpDown_fieldnumber";
            this.numericUpDown_fieldnumber.Size = new System.Drawing.Size(63, 20);
            this.numericUpDown_fieldnumber.TabIndex = 9;
            // 
            // numericUpDown_graphicsbankindex
            // 
            this.numericUpDown_graphicsbankindex.Location = new System.Drawing.Point(134, 61);
            this.numericUpDown_graphicsbankindex.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numericUpDown_graphicsbankindex.Name = "numericUpDown_graphicsbankindex";
            this.numericUpDown_graphicsbankindex.Size = new System.Drawing.Size(63, 20);
            this.numericUpDown_graphicsbankindex.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(235, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Level Pointer:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(235, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Header Address:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(121, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Header Pointer Number:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Header Number:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.numericUpDown_levelSelector);
            this.panel2.Location = new System.Drawing.Point(684, 96);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(128, 26);
            this.panel2.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label3.Location = new System.Drawing.Point(3, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Level:";
            // 
            // numericUpDown_levelSelector
            // 
            this.numericUpDown_levelSelector.Location = new System.Drawing.Point(46, 2);
            this.numericUpDown_levelSelector.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.numericUpDown_levelSelector.Name = "numericUpDown_levelSelector";
            this.numericUpDown_levelSelector.Size = new System.Drawing.Size(53, 20);
            this.numericUpDown_levelSelector.TabIndex = 1;
            this.numericUpDown_levelSelector.ValueChanged += new System.EventHandler(this.numericUpDown_levelSelector_ValueChanged);
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.pictureBox_phystile);
            this.panel5.Controls.Add(this.flowLayoutPanel2);
            this.panel5.Controls.Add(this.pictureBox_tilemaptile);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Location = new System.Drawing.Point(4, 31);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(176, 92);
            this.panel5.TabIndex = 5;
            // 
            // pictureBox_phystile
            // 
            this.pictureBox_phystile.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pictureBox_phystile.ErrorImage = null;
            this.pictureBox_phystile.InitialImage = null;
            this.pictureBox_phystile.Location = new System.Drawing.Point(8, 53);
            this.pictureBox_phystile.Name = "pictureBox_phystile";
            this.pictureBox_phystile.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_phystile.TabIndex = 3;
            this.pictureBox_phystile.TabStop = false;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.flowLayoutPanel2.Controls.Add(this.checkBox_vflip);
            this.flowLayoutPanel2.Controls.Add(this.checkBox_hflip);
            this.flowLayoutPanel2.Controls.Add(this.checkBox_priority);
            this.flowLayoutPanel2.Controls.Add(this.panel4);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(46, -1);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(130, 92);
            this.flowLayoutPanel2.TabIndex = 2;
            // 
            // checkBox_vflip
            // 
            this.checkBox_vflip.AutoSize = true;
            this.checkBox_vflip.Location = new System.Drawing.Point(3, 3);
            this.checkBox_vflip.Name = "checkBox_vflip";
            this.checkBox_vflip.Size = new System.Drawing.Size(80, 17);
            this.checkBox_vflip.TabIndex = 0;
            this.checkBox_vflip.Text = "Vertical Flip";
            this.checkBox_vflip.UseVisualStyleBackColor = true;
            this.checkBox_vflip.CheckedChanged += new System.EventHandler(this.checkBox_vflip_CheckedChanged);
            // 
            // checkBox_hflip
            // 
            this.checkBox_hflip.AutoSize = true;
            this.checkBox_hflip.Location = new System.Drawing.Point(3, 26);
            this.checkBox_hflip.Name = "checkBox_hflip";
            this.checkBox_hflip.Size = new System.Drawing.Size(92, 17);
            this.checkBox_hflip.TabIndex = 1;
            this.checkBox_hflip.Text = "Horizontal Flip";
            this.checkBox_hflip.UseVisualStyleBackColor = true;
            this.checkBox_hflip.CheckedChanged += new System.EventHandler(this.checkBox_hflip_CheckedChanged);
            // 
            // checkBox_priority
            // 
            this.checkBox_priority.AutoSize = true;
            this.checkBox_priority.Location = new System.Drawing.Point(3, 49);
            this.checkBox_priority.Name = "checkBox_priority";
            this.checkBox_priority.Size = new System.Drawing.Size(112, 17);
            this.checkBox_priority.TabIndex = 2;
            this.checkBox_priority.Text = "Draw over objects";
            this.checkBox_priority.UseVisualStyleBackColor = true;
            this.checkBox_priority.CheckedChanged += new System.EventHandler(this.checkBox_priority_CheckedChanged);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.numericUpDown_tilePalette);
            this.panel4.Location = new System.Drawing.Point(0, 69);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(130, 23);
            this.panel4.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Palette:";
            // 
            // numericUpDown_tilePalette
            // 
            this.numericUpDown_tilePalette.Location = new System.Drawing.Point(52, 1);
            this.numericUpDown_tilePalette.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.numericUpDown_tilePalette.Name = "numericUpDown_tilePalette";
            this.numericUpDown_tilePalette.Size = new System.Drawing.Size(64, 20);
            this.numericUpDown_tilePalette.TabIndex = 0;
            this.numericUpDown_tilePalette.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown_tilePalette.ValueChanged += new System.EventHandler(this.numericUpDown_tilePalette_ValueChanged);
            // 
            // pictureBox_tilemaptile
            // 
            this.pictureBox_tilemaptile.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pictureBox_tilemaptile.ErrorImage = null;
            this.pictureBox_tilemaptile.InitialImage = null;
            this.pictureBox_tilemaptile.Location = new System.Drawing.Point(8, 17);
            this.pictureBox_tilemaptile.Name = "pictureBox_tilemaptile";
            this.pictureBox_tilemaptile.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_tilemaptile.TabIndex = 0;
            this.pictureBox_tilemaptile.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label2.Location = new System.Drawing.Point(2, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tile:";
            // 
            // MainMenu
            // 
            this.MainMenu.BackColor = System.Drawing.SystemColors.Control;
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenu_File});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(977, 24);
            this.MainMenu.TabIndex = 3;
            this.MainMenu.Text = "menuStrip1";
            // 
            // MainMenu_File
            // 
            this.MainMenu_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenu_File_Open,
            this.saveLevelToolStripMenuItem,
            this.exportLevelToolStripMenuItem,
            this.importLevelToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.MainMenu_File.Name = "MainMenu_File";
            this.MainMenu_File.Size = new System.Drawing.Size(37, 20);
            this.MainMenu_File.Text = "File";
            // 
            // MainMenu_File_Open
            // 
            this.MainMenu_File_Open.Name = "MainMenu_File_Open";
            this.MainMenu_File_Open.ShortcutKeyDisplayString = "Ctrl+O";
            this.MainMenu_File_Open.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.MainMenu_File_Open.Size = new System.Drawing.Size(209, 22);
            this.MainMenu_File_Open.Text = "&Open ROM";
            this.MainMenu_File_Open.Click += new System.EventHandler(this.MainMenu_FileOpen_Click);
            // 
            // saveLevelToolStripMenuItem
            // 
            this.saveLevelToolStripMenuItem.Name = "saveLevelToolStripMenuItem";
            this.saveLevelToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveLevelToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.saveLevelToolStripMenuItem.Text = "&Save level to ROM";
            this.saveLevelToolStripMenuItem.Click += new System.EventHandler(this.MainMenu_SaveLevel_Click);
            // 
            // exportLevelToolStripMenuItem
            // 
            this.exportLevelToolStripMenuItem.Name = "exportLevelToolStripMenuItem";
            this.exportLevelToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.exportLevelToolStripMenuItem.Text = "Export level";
            this.exportLevelToolStripMenuItem.Click += new System.EventHandler(this.exportLevelToolStripMenuItem_Click);
            // 
            // importLevelToolStripMenuItem
            // 
            this.importLevelToolStripMenuItem.Name = "importLevelToolStripMenuItem";
            this.importLevelToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.importLevelToolStripMenuItem.Text = "Import level";
            this.importLevelToolStripMenuItem.Click += new System.EventHandler(this.importLevelToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.MainMenu_Exit_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "SNES Rom File (*.smc)|*.smc|All files (*.*)|*.*";
            this.saveFileDialog.RestoreDirectory = true;
            // 
            // MainForm
            // 
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(977, 953);
            this.Controls.Add(this.mainPanel);
            this.MainMenuStrip = this.MainMenu;
            this.Name = "MainForm";
            this.Text = "Riverback";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.flowLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage_Tileset.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_tileset)).EndInit();
            this.tabPage_Phystiles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_phystiles)).EndInit();
            this.tabPage_Indextiles.ResumeLayout(false);
            this.tabPage_Indextiles.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_indexTiles)).EndInit();
            this.tabControl2.ResumeLayout(false);
            this.tabPage_Level.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_level)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_paletteindices6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_paletteindices5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_paletteindices4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_paletteindices3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_paletteindices2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_paletteindices1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_waterheight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_objecttype7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_spawnrate8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_spawnrate7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_objecttype6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_spawnrate6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_objecttype5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_spawnrate5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_objecttype4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_spawnrate4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_objecttype3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_spawnrate3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_objecttype2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_spawnrate2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_objecttype1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_spawnrate1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_doorexit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_doorexit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_doorexit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_doorexit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_leveltimer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_enemytype6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_enemytype5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_enemytype4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_enemytype3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_enemytype2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_enemytype1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_musicselect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_fieldnumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_graphicsbankindex)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_levelSelector)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_phystile)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_tilePalette)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_tilemaptile)).EndInit();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox pictureBox_tilemaptile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown_tilePalette;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown_levelSelector;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_File;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_File_Open;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.CheckBox checkBox_vflip;
        private System.Windows.Forms.CheckBox checkBox_hflip;
        private System.Windows.Forms.CheckBox checkBox_priority;
        private System.Windows.Forms.ToolStripMenuItem saveLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.PictureBox pictureBox_tileset;
        private System.Windows.Forms.PictureBox pictureBox_level;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button button_deselect;
        private System.Windows.Forms.PictureBox pictureBox_phystiles;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.CheckBox checkBox_field_show;
        private System.Windows.Forms.CheckBox checkBox_physmap_show;
        private System.Windows.Forms.CheckBox checkBox_bytes_show;
        private System.Windows.Forms.CheckBox checkBox_grid_show;
        private System.Windows.Forms.RadioButton radioButton_field_edit;
        private System.Windows.Forms.RadioButton radioButton_physmap_edit;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_Tileset;
        private System.Windows.Forms.TabPage tabPage_Phystiles;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage_Level;
        private System.Windows.Forms.PictureBox pictureBox_phystile;
        private System.Windows.Forms.ToolStripMenuItem exportLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importLevelToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage_Indextiles;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox_indexTiles;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_tilesremaining;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button_applyindices;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDown_enemytype1;
        private System.Windows.Forms.NumericUpDown numericUpDown_musicselect;
        private System.Windows.Forms.NumericUpDown numericUpDown_fieldnumber;
        private System.Windows.Forms.NumericUpDown numericUpDown_graphicsbankindex;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numericUpDown_enemytype6;
        private System.Windows.Forms.NumericUpDown numericUpDown_enemytype5;
        private System.Windows.Forms.NumericUpDown numericUpDown_enemytype4;
        private System.Windows.Forms.NumericUpDown numericUpDown_enemytype3;
        private System.Windows.Forms.NumericUpDown numericUpDown_enemytype2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown numericUpDown_doorexit4;
        private System.Windows.Forms.NumericUpDown numericUpDown_doorexit3;
        private System.Windows.Forms.NumericUpDown numericUpDown_doorexit2;
        private System.Windows.Forms.NumericUpDown numericUpDown_doorexit1;
        private System.Windows.Forms.NumericUpDown numericUpDown_leveltimer;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.NumericUpDown numericUpDown_waterheight;
        private System.Windows.Forms.NumericUpDown numericUpDown_objecttype7;
        private System.Windows.Forms.NumericUpDown numericUpDown_spawnrate8;
        private System.Windows.Forms.NumericUpDown numericUpDown_spawnrate7;
        private System.Windows.Forms.NumericUpDown numericUpDown_objecttype6;
        private System.Windows.Forms.NumericUpDown numericUpDown_spawnrate6;
        private System.Windows.Forms.NumericUpDown numericUpDown_objecttype5;
        private System.Windows.Forms.NumericUpDown numericUpDown_spawnrate5;
        private System.Windows.Forms.NumericUpDown numericUpDown_objecttype4;
        private System.Windows.Forms.NumericUpDown numericUpDown_spawnrate4;
        private System.Windows.Forms.NumericUpDown numericUpDown_objecttype3;
        private System.Windows.Forms.NumericUpDown numericUpDown_spawnrate3;
        private System.Windows.Forms.NumericUpDown numericUpDown_objecttype2;
        private System.Windows.Forms.NumericUpDown numericUpDown_spawnrate2;
        private System.Windows.Forms.NumericUpDown numericUpDown_objecttype1;
        private System.Windows.Forms.NumericUpDown numericUpDown_spawnrate1;
        private System.Windows.Forms.Button button_applyheader;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.NumericUpDown numericUpDown_paletteindices6;
        private System.Windows.Forms.NumericUpDown numericUpDown_paletteindices5;
        private System.Windows.Forms.NumericUpDown numericUpDown_paletteindices4;
        private System.Windows.Forms.NumericUpDown numericUpDown_paletteindices3;
        private System.Windows.Forms.NumericUpDown numericUpDown_paletteindices2;
        private System.Windows.Forms.NumericUpDown numericUpDown_paletteindices1;
        private System.Windows.Forms.TextBox textBox_levelpointer;
        private System.Windows.Forms.TextBox textBox_headeraddress;
        private System.Windows.Forms.TextBox textBox_headerpointer;
        private System.Windows.Forms.TextBox textBox_headernumber;
        private System.Windows.Forms.CheckBox checkBox_wavywater;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label17;
    }
}
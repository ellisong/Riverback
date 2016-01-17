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
            this.MainMenu_File_Open.Size = new System.Drawing.Size(169, 22);
            this.MainMenu_File_Open.Text = "Open ROM";
            this.MainMenu_File_Open.Click += new System.EventHandler(this.MainMenu_FileOpen_Click);
            // 
            // saveLevelToolStripMenuItem
            // 
            this.saveLevelToolStripMenuItem.Name = "saveLevelToolStripMenuItem";
            this.saveLevelToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.saveLevelToolStripMenuItem.Text = "Save level to ROM";
            this.saveLevelToolStripMenuItem.Click += new System.EventHandler(this.MainMenu_SaveLevel_Click);
            // 
            // exportLevelToolStripMenuItem
            // 
            this.exportLevelToolStripMenuItem.Name = "exportLevelToolStripMenuItem";
            this.exportLevelToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.exportLevelToolStripMenuItem.Text = "Export level";
            this.exportLevelToolStripMenuItem.Click += new System.EventHandler(this.exportLevelToolStripMenuItem_Click);
            // 
            // importLevelToolStripMenuItem
            // 
            this.importLevelToolStripMenuItem.Name = "importLevelToolStripMenuItem";
            this.importLevelToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.importLevelToolStripMenuItem.Text = "Import level";
            this.importLevelToolStripMenuItem.Click += new System.EventHandler(this.importLevelToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
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
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
    }
}
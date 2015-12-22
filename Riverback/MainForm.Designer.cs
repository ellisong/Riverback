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
                bitmapTile.Dispose();
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label_TileValue = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox_tile = new System.Windows.Forms.PictureBox();
            this.pictureBox_tileset = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.checkBox_vflip = new System.Windows.Forms.CheckBox();
            this.checkBox_hflip = new System.Windows.Forms.CheckBox();
            this.checkBox_priority = new System.Windows.Forms.CheckBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown_tilePalette = new System.Windows.Forms.NumericUpDown();
            this.pictureBox_level = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown_levelSelector = new System.Windows.Forms.NumericUpDown();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.MainMenu_File = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_File_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.saveLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.panel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_tile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_tileset)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_tilePalette)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_level)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_levelSelector)).BeginInit();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.MainMenu);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(669, 552);
            this.panel1.TabIndex = 1;
            // 
            // panel6
            // 
            this.panel6.AutoSize = true;
            this.panel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel6.Controls.Add(this.panel3);
            this.panel6.Controls.Add(this.pictureBox_tileset);
            this.panel6.Controls.Add(this.flowLayoutPanel2);
            this.panel6.Controls.Add(this.pictureBox_level);
            this.panel6.Controls.Add(this.panel2);
            this.panel6.Location = new System.Drawing.Point(4, 27);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(653, 521);
            this.panel6.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label_TileValue);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.pictureBox_tile);
            this.panel3.Location = new System.Drawing.Point(4, 3);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(128, 71);
            this.panel3.TabIndex = 0;
            // 
            // label_TileValue
            // 
            this.label_TileValue.AutoSize = true;
            this.label_TileValue.Location = new System.Drawing.Point(8, 32);
            this.label_TileValue.Name = "label_TileValue";
            this.label_TileValue.Size = new System.Drawing.Size(30, 13);
            this.label_TileValue.TabIndex = 2;
            this.label_TileValue.Text = "0x00";
            this.label_TileValue.Paint += new System.Windows.Forms.PaintEventHandler(this.label_TileValue_Paint);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tile:";
            // 
            // pictureBox_tile
            // 
            this.pictureBox_tile.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pictureBox_tile.ErrorImage = null;
            this.pictureBox_tile.InitialImage = null;
            this.pictureBox_tile.Location = new System.Drawing.Point(53, 3);
            this.pictureBox_tile.Name = "pictureBox_tile";
            this.pictureBox_tile.Size = new System.Drawing.Size(64, 64);
            this.pictureBox_tile.TabIndex = 0;
            this.pictureBox_tile.TabStop = false;
            // 
            // pictureBox_tileset
            // 
            this.pictureBox_tileset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox_tileset.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBox_tileset.ErrorImage = null;
            this.pictureBox_tileset.InitialImage = null;
            this.pictureBox_tileset.Location = new System.Drawing.Point(4, 259);
            this.pictureBox_tileset.Name = "pictureBox_tileset";
            this.pictureBox_tileset.Size = new System.Drawing.Size(128, 256);
            this.pictureBox_tileset.TabIndex = 0;
            this.pictureBox_tileset.TabStop = false;
            this.pictureBox_tileset.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_tileset_MouseClick);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.flowLayoutPanel2.Controls.Add(this.checkBox_vflip);
            this.flowLayoutPanel2.Controls.Add(this.checkBox_hflip);
            this.flowLayoutPanel2.Controls.Add(this.checkBox_priority);
            this.flowLayoutPanel2.Controls.Add(this.panel4);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(4, 77);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(128, 144);
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
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.numericUpDown_tilePalette);
            this.panel4.Location = new System.Drawing.Point(0, 69);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(128, 22);
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
            this.numericUpDown_tilePalette.Location = new System.Drawing.Point(52, 2);
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
            // pictureBox_level
            // 
            this.pictureBox_level.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_level.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBox_level.ErrorImage = null;
            this.pictureBox_level.InitialImage = null;
            this.pictureBox_level.Location = new System.Drawing.Point(138, 3);
            this.pictureBox_level.Name = "pictureBox_level";
            this.pictureBox_level.Size = new System.Drawing.Size(512, 512);
            this.pictureBox_level.TabIndex = 1;
            this.pictureBox_level.TabStop = false;
            this.pictureBox_level.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_level_MouseClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.numericUpDown_levelSelector);
            this.panel2.Location = new System.Drawing.Point(4, 227);
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
            // MainMenu
            // 
            this.MainMenu.BackColor = System.Drawing.SystemColors.Control;
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenu_File});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(669, 24);
            this.MainMenu.TabIndex = 3;
            this.MainMenu.Text = "menuStrip1";
            // 
            // MainMenu_File
            // 
            this.MainMenu_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenu_File_Open,
            this.saveLevelToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.MainMenu_File.Name = "MainMenu_File";
            this.MainMenu_File.Size = new System.Drawing.Size(37, 20);
            this.MainMenu_File.Text = "File";
            // 
            // MainMenu_File_Open
            // 
            this.MainMenu_File_Open.Name = "MainMenu_File_Open";
            this.MainMenu_File_Open.Size = new System.Drawing.Size(152, 22);
            this.MainMenu_File_Open.Text = "Open ROM";
            this.MainMenu_File_Open.Click += new System.EventHandler(this.MainMenu_FileOpen_Click);
            // 
            // saveLevelToolStripMenuItem
            // 
            this.saveLevelToolStripMenuItem.Name = "saveLevelToolStripMenuItem";
            this.saveLevelToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveLevelToolStripMenuItem.Text = "Save Level";
            this.saveLevelToolStripMenuItem.Click += new System.EventHandler(this.MainMenu_SaveLevel_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
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
            this.ClientSize = new System.Drawing.Size(669, 552);
            this.Controls.Add(this.panel1);
            this.MainMenuStrip = this.MainMenu;
            this.Name = "MainForm";
            this.Text = "Riverback";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_tile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_tileset)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_tilePalette)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_level)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_levelSelector)).EndInit();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox_tile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown_tilePalette;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_TileValue;
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
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.PictureBox pictureBox_tileset;
        private System.Windows.Forms.PictureBox pictureBox_level;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}
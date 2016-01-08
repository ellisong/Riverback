﻿namespace Riverback
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
            this.label_prop = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button_deselect = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox_tileset = new System.Windows.Forms.PictureBox();
            this.pictureBox_level = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown_levelSelector = new System.Windows.Forms.NumericUpDown();
            this.panel5 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.checkBox_vflip = new System.Windows.Forms.CheckBox();
            this.checkBox_hflip = new System.Windows.Forms.CheckBox();
            this.checkBox_priority = new System.Windows.Forms.CheckBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown_tilePalette = new System.Windows.Forms.NumericUpDown();
            this.pictureBox_tile = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.MainMenu_File = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_File_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.saveLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.savePhysmapdebuggingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.writePhysmapdebuggingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_tileset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_level)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_levelSelector)).BeginInit();
            this.panel5.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_tilePalette)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_tile)).BeginInit();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.label_prop);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.button_deselect);
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.MainMenu);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(790, 651);
            this.panel1.TabIndex = 1;
            // 
            // label_prop
            // 
            this.label_prop.AutoSize = true;
            this.label_prop.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.label_prop.Location = new System.Drawing.Point(317, 36);
            this.label_prop.Name = "label_prop";
            this.label_prop.Size = new System.Drawing.Size(39, 29);
            this.label_prop.TabIndex = 9;
            this.label_prop.Text = "0x";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.label4.Location = new System.Drawing.Point(187, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 29);
            this.label4.TabIndex = 8;
            this.label4.Text = "Phys prop:";
            // 
            // button_deselect
            // 
            this.button_deselect.Location = new System.Drawing.Point(183, 102);
            this.button_deselect.Name = "button_deselect";
            this.button_deselect.Size = new System.Drawing.Size(61, 21);
            this.button_deselect.TabIndex = 7;
            this.button_deselect.Text = "Deselect";
            this.button_deselect.UseVisualStyleBackColor = true;
            this.button_deselect.Click += new System.EventHandler(this.button_deselect_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.pictureBox_tileset);
            this.flowLayoutPanel1.Controls.Add(this.pictureBox_level);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(4, 125);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(780, 519);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // pictureBox_tileset
            // 
            this.pictureBox_tileset.Anchor = System.Windows.Forms.AnchorStyles.None;
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
            // pictureBox_level
            // 
            this.pictureBox_level.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox_level.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBox_level.ErrorImage = null;
            this.pictureBox_level.InitialImage = null;
            this.pictureBox_level.Location = new System.Drawing.Point(265, 3);
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
            this.panel2.Location = new System.Drawing.Point(656, 97);
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
            this.panel5.Controls.Add(this.flowLayoutPanel2);
            this.panel5.Controls.Add(this.pictureBox_tile);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Location = new System.Drawing.Point(4, 31);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(176, 92);
            this.panel5.TabIndex = 5;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.flowLayoutPanel2.Controls.Add(this.checkBox_vflip);
            this.flowLayoutPanel2.Controls.Add(this.checkBox_hflip);
            this.flowLayoutPanel2.Controls.Add(this.checkBox_priority);
            this.flowLayoutPanel2.Controls.Add(this.panel4);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(46, 0);
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
            // pictureBox_tile
            // 
            this.pictureBox_tile.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pictureBox_tile.ErrorImage = null;
            this.pictureBox_tile.InitialImage = null;
            this.pictureBox_tile.Location = new System.Drawing.Point(8, 31);
            this.pictureBox_tile.Name = "pictureBox_tile";
            this.pictureBox_tile.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_tile.TabIndex = 0;
            this.pictureBox_tile.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 24);
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
            this.MainMenu.Size = new System.Drawing.Size(790, 24);
            this.MainMenu.TabIndex = 3;
            this.MainMenu.Text = "menuStrip1";
            // 
            // MainMenu_File
            // 
            this.MainMenu_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenu_File_Open,
            this.saveLevelToolStripMenuItem,
            this.savePhysmapdebuggingToolStripMenuItem,
            this.writePhysmapdebuggingToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.MainMenu_File.Name = "MainMenu_File";
            this.MainMenu_File.Size = new System.Drawing.Size(37, 20);
            this.MainMenu_File.Text = "File";
            // 
            // MainMenu_File_Open
            // 
            this.MainMenu_File_Open.Name = "MainMenu_File_Open";
            this.MainMenu_File_Open.Size = new System.Drawing.Size(219, 22);
            this.MainMenu_File_Open.Text = "Open ROM";
            this.MainMenu_File_Open.Click += new System.EventHandler(this.MainMenu_FileOpen_Click);
            // 
            // saveLevelToolStripMenuItem
            // 
            this.saveLevelToolStripMenuItem.Name = "saveLevelToolStripMenuItem";
            this.saveLevelToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.saveLevelToolStripMenuItem.Text = "Save Level";
            this.saveLevelToolStripMenuItem.Click += new System.EventHandler(this.MainMenu_SaveLevel_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
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
            // savePhysmapdebuggingToolStripMenuItem
            // 
            this.savePhysmapdebuggingToolStripMenuItem.Name = "savePhysmapdebuggingToolStripMenuItem";
            this.savePhysmapdebuggingToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.savePhysmapdebuggingToolStripMenuItem.Text = "Save Physmap (debugging)";
            this.savePhysmapdebuggingToolStripMenuItem.Click += new System.EventHandler(this.savePhysmapdebuggingToolStripMenuItem_Click);
            // 
            // writePhysmapdebuggingToolStripMenuItem
            // 
            this.writePhysmapdebuggingToolStripMenuItem.Name = "writePhysmapdebuggingToolStripMenuItem";
            this.writePhysmapdebuggingToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.writePhysmapdebuggingToolStripMenuItem.Text = "Write physmap (debugging)";
            this.writePhysmapdebuggingToolStripMenuItem.Click += new System.EventHandler(this.writePhysmapdebuggingToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(790, 651);
            this.Controls.Add(this.panel1);
            this.MainMenuStrip = this.MainMenu;
            this.Name = "MainForm";
            this.Text = "Riverback";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_tileset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_level)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_levelSelector)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_tilePalette)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_tile)).EndInit();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox pictureBox_tile;
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
        private System.Windows.Forms.Label label_prop;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripMenuItem savePhysmapdebuggingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem writePhysmapdebuggingToolStripMenuItem;
    }
}
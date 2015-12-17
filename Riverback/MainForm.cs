using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Riverback
{
    public partial class MainForm : Form
    {
        private readonly System.Drawing.Color fillColor = System.Drawing.Color.DarkGray;

        private LevelEditor levelEditor;
        private byte[] romdata;
        private int selectedTileNumber;

        public MainForm()
        {
            levelEditor = new LevelEditor();
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void MainMenu_FileOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                if (openFileDialog.CheckFileExists) {
                    romdata = File.ReadAllBytes(openFileDialog.FileName);
                    if (romdata != null) {
                        levelEditor.openLevel(romdata, (int)numericUpDown_levelSelector.Value);
                        levelEditor.updateGraphicsBanks(romdata);
                        levelEditor.updateLevelBank();
                        selectedTileNumber = 1;
                        pictureBox_tileset.Refresh();
                        pictureBox_tile.Refresh();
                        pictureBox_level.Refresh();
                    }
                }
            }
        }

        private void numericUpDown_levelSelector_ValueChanged(object sender, EventArgs e)
        {
            if (romdata != null) {
                levelEditor.openLevel(romdata, (int)numericUpDown_levelSelector.Value);
                levelEditor.updateGraphicsBanks(romdata);
                levelEditor.updateLevelBank();
                selectedTileNumber = 1;
                pictureBox_tileset.Refresh();
                pictureBox_level.Refresh();
                pictureBox_tile.Refresh();
                label_TileValue.Refresh();
            }
        }

        private void numericUpDown_tilePalette_ValueChanged(object sender, EventArgs e)
        {
            if ((levelEditor.Level != null) && (levelEditor.LevelBank != null)) {
                pictureBox_tileset.Refresh();
                pictureBox_tile.Refresh();
                label_TileValue.Refresh();
            }
        }

        private void pictureBox_tileset_MouseClick(object sender, MouseEventArgs e)
        {
            if ((levelEditor.LevelBank != null) && (levelEditor.LevelBank != null) && (e.Button == MouseButtons.Left)) {
                int tileNum = TileDrawer.getTileNumberFromMouseCoordinates(e.X, e.Y, TileDrawer.LEVEL_TILESET_WIDTH);
                if (tileNum < levelEditor.LevelBank.tileAmount) {
                    selectedTileNumber = tileNum;
                    pictureBox_tile.Refresh();
                    label_TileValue.Refresh();
                }
            }
        }

        private void pictureBox_tileset_Paint(object sender, PaintEventArgs e)
        {
            if ((levelEditor.Level != null) && (levelEditor.LevelBank != null)) {
                e.Graphics.Clear(fillColor);
                byte paletteNum = (byte)(levelEditor.Level.PaletteIndex[(int)numericUpDown_tilePalette.Value] - 1);
                TileDrawer.drawAllTilesOnCanvas(levelEditor.LevelBank, e.Graphics,
                                                TileDrawer.LEVEL_TILESET_WIDTH_TILEAMOUNT, paletteNum);
            }
        }

        private void pictureBox_level_Paint(object sender, PaintEventArgs e)
        {
            if ((levelEditor.Level != null) && (levelEditor.LevelBank != null)) {
                e.Graphics.Clear(fillColor);
                TileDrawer.drawLevelOnCanvas(e.Graphics, levelEditor.Level, levelEditor.LevelBank);
            }
        }

        private void pictureBox_tile_Paint(object sender, PaintEventArgs e)
        {
            if ((levelEditor.Level != null) && (levelEditor.LevelBank != null)) {
                e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                e.Graphics.Clear(fillColor);
                byte paletteNum = (byte)(levelEditor.Level.PaletteIndex[(int)numericUpDown_tilePalette.Value] - 1);
                TileDrawer.drawTileOnTileSelectorCanvas(levelEditor.LevelBank, e.Graphics,
                                                        selectedTileNumber, paletteNum);
            }
        }

        private void label_TileValue_Paint(object sender, PaintEventArgs e)
        {
            label_TileValue.Text = String.Format("0x{0:X}", selectedTileNumber);
        }
    }
}

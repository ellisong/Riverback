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
        readonly System.Drawing.Color fillColor = System.Drawing.Color.DarkGray;

        LevelEditor levelEditor;
        byte[] romdata;
        int selectedTileNumber;

        public MainForm()
        {
            levelEditor = new LevelEditor();
            InitializeComponent();
        }

        private void updateTilesetPictureBox()
        {
            if ((levelEditor.Level != null) && (levelEditor.LevelBank != null)) {
                Graphics g = pictureBox_tileset.CreateGraphics();
                g.Clear(fillColor);
                byte paletteNum = (byte)(levelEditor.Level.PaletteIndex[(int)numericUpDown_tilePalette.Value] - 1);
                TileDrawer.drawAllTilesOnCanvas(levelEditor.LevelBank, g,
                                                TileDrawer.LEVEL_TILESET_WIDTH_TILEAMOUNT, paletteNum);
                g.Dispose();
            }
        }

        private void updateLevelPictureBox()
        {
            if ((levelEditor.Level != null) && (levelEditor.LevelBank != null)) {
                Graphics g = pictureBox_level.CreateGraphics();
                g.Clear(fillColor);
                TileDrawer.drawLevelOnCanvas(g, levelEditor.Level, levelEditor.LevelBank);
                g.Dispose();
            }
        }

        private void updateTilePictureBox()
        {
            if ((levelEditor.Level != null) && (levelEditor.LevelBank != null)) {
                Graphics g = pictureBox_Tile.CreateGraphics();
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.Clear(fillColor);
                byte paletteNum = (byte)(levelEditor.Level.PaletteIndex[(int)numericUpDown_tilePalette.Value] - 1);
                TileDrawer.drawTileOnTileSelectorCanvas(levelEditor.LevelBank, g,
                                                        selectedTileNumber, paletteNum);
                g.Dispose();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
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
                        updateTilesetPictureBox();
                        selectedTileNumber = 0;
                        label_TileValue.Text = String.Format("0x{0:X}", selectedTileNumber);
                        updateTilePictureBox();
                        updateLevelPictureBox();
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
                updateTilesetPictureBox();
                selectedTileNumber = 0;
                label_TileValue.Text = String.Format("0x{0:X}", selectedTileNumber);
                updateTilePictureBox();
                updateLevelPictureBox();
            }
        }

        private void numericUpDown_tilePalette_ValueChanged(object sender, EventArgs e)
        {
            if ((levelEditor.Level != null) && (levelEditor.LevelBank != null)) {
                updateTilesetPictureBox();
                updateTilePictureBox();
            }
        }

        private void pictureBox_tileset_MouseClick(object sender, MouseEventArgs e)
        {
            if ((levelEditor.LevelBank != null) && (levelEditor.LevelBank != null) && (e.Button == MouseButtons.Left)) {
                int tileNum = TileDrawer.getTileNumberFromMouseCoordinates(e.X, e.Y, TileDrawer.LEVEL_TILESET_WIDTH);
                if (tileNum < levelEditor.LevelBank.tileAmount) {
                    selectedTileNumber = tileNum;
                    label_TileValue.Text = String.Format("0x{0:X}", selectedTileNumber);
                    updateTilePictureBox();
                }
            }
        }
    }
}

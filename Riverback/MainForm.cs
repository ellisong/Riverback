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
        private readonly System.Drawing.Brush fillBrush = System.Drawing.Brushes.DarkGray;
        public const float TILE_SELECTOR_SCALE = 4.0f;
        public const int TILEMAP_SCALE_INT = 2;
        public const float TILEMAP_SCALE = 2.0f;

        private LevelEditor levelEditor;
        private byte[] romdata;
        private int selectedTileValue;
        private Bitmap bitmapTileset;
        private Bitmap bitmapTile;
        private Bitmap bitmapLevel;

        public MainForm()
        {
            InitializeComponent();
            levelEditor = new LevelEditor();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            bitmapTileset = new Bitmap(pictureBox_tileset.Width, pictureBox_tileset.Height);
            pictureBox_tileset.Image = bitmapTileset;
            bitmapTile = new Bitmap(pictureBox_tile.Width, pictureBox_tile.Height);
            pictureBox_tile.Image = bitmapTile;
            bitmapLevel = new Bitmap(pictureBox_level.Width, pictureBox_level.Height);
            pictureBox_level.Image = bitmapLevel;
        }

        private void MainMenu_FileOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                if (openFileDialog.CheckFileExists) {
                    byte[] openedData = File.ReadAllBytes(openFileDialog.FileName);
                    romdata = RomWriter.expandRom(openedData);
                    if (romdata != null) {
                        levelEditor.openLevel(romdata, (byte)numericUpDown_levelSelector.Value);
                        levelEditor.updateGraphicsBanks(romdata);
                        levelEditor.updateLevelBank();
                        selectedTileValue = 1;
                        updateImage_Tileset();
                        updateImage_Tile();
                        updateImage_Level();
                    }
                }
            }
        }

        private void MainMenu_SaveLevel_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                string fileName;
                if ((fileName = saveFileDialog.FileName) != "") {
                    RomWriter writer = new RomWriter(romdata);
                    writer.writeLevel(levelEditor.Level);
                    File.WriteAllBytes(fileName, romdata);
                }
            }
        }

        private void MainMenu_Exit_Click(object sender, EventArgs e)
        {
            // TODO: save prompt on level change
            Application.Exit();
        }

        private void numericUpDown_levelSelector_ValueChanged(object sender, EventArgs e)
        {
            if (romdata != null) {
                levelEditor.openLevel(romdata, (byte)numericUpDown_levelSelector.Value);
                levelEditor.updateGraphicsBanks(romdata);
                levelEditor.updateLevelBank();
                selectedTileValue = 1;
                updateImage_Tileset();
                updateImage_Tile();
                updateImage_Level();
            }
        }

        private void numericUpDown_tilePalette_ValueChanged(object sender, EventArgs e)
        {
            if ((levelEditor.Level != null) && (levelEditor.LevelBank != null)) {
                updateImage_Tileset();
                updateImage_Tile();
            }
        }

        private void pictureBox_tileset_MouseClick(object sender, MouseEventArgs e)
        {
            if ((levelEditor.LevelBank != null) && (levelEditor.LevelBank != null) && (e.Button == MouseButtons.Left)) {
                Point mouseCoords = new Point(e.X, e.Y);
                int tileNum = CoordinateConverter.getTileNumberFromMouseCoords(mouseCoords, TileDrawer.LEVEL_TILESET_TILEAMOUNT_WIDTH, TILEMAP_SCALE_INT);
                if (tileNum < levelEditor.LevelBank.tileAmount) {
                    selectedTileValue = tileNum;
                    updateImage_Tile();
                }
            }
        }

        private void pictureBox_level_MouseClick(object sender, MouseEventArgs e)
        {
            if ((levelEditor.LevelBank != null) && (levelEditor.LevelBank != null)) {
                Point mouseCoords = new Point(e.X, e.Y);
                int tileNum = CoordinateConverter.getTileNumberFromMouseCoords(mouseCoords, TileDrawer.LEVEL_CANVAS_TILEAMOUNT_WIDTH);
                if (e.Button == MouseButtons.Right) {
                    levelEditor.setTileInTilemap(tileNum, selectedTileValue, checkBox_vflip.Checked, checkBox_hflip.Checked, 
                                                 checkBox_priority.Checked, (byte)numericUpDown_tilePalette.Value);
                    using (Graphics g = Graphics.FromImage(bitmapLevel)) {
                        Point alignedCoords = CoordinateConverter.getMouseCoordsFromTileNumber(tileNum, TileDrawer.LEVEL_CANVAS_TILEAMOUNT_WIDTH);
                        if (selectedTileValue != 0) {
                            byte paletteNum = (byte)(levelEditor.Level.PaletteIndex[(int)numericUpDown_tilePalette.Value] - 1);
                            TileDrawer.drawTileOnCanvas(levelEditor.LevelBank, g, alignedCoords.X, alignedCoords.Y, selectedTileValue, 
                                                        paletteNum, checkBox_hflip.Checked, checkBox_vflip.Checked);
                        } else {
                            TileDrawer.clearTileOnCanvas(g, fillBrush, alignedCoords.X, alignedCoords.Y);
                        }
                        invalidateTile(pictureBox_level, tileNum);
                    }
                } else if (e.Button == MouseButtons.Left) {
                    TilemapTile tile = levelEditor.Level.Tilemap[tileNum];
                    System.Console.WriteLine("#: " + tileNum + "    tile: " + tile.Tile);
                    System.Console.WriteLine("bank: " + tile.Bank + "    palette: " + tile.Palette);
                    System.Console.WriteLine("hflip: " + tile.HFlip + "    vflip: " + tile.VFlip + "    priority: " + tile.Priority);
                }
            }
        }

        private void updateImage_Tileset()
        {
            if ((levelEditor.Level != null) && (levelEditor.LevelBank != null)) {
                using (Graphics g = Graphics.FromImage(bitmapTileset)) {
                    g.Clear(fillColor);
                    byte paletteNum = (byte)(levelEditor.Level.PaletteIndex[(int)numericUpDown_tilePalette.Value] - 1);
                    TileDrawer.drawAllTilesOnCanvas(levelEditor.LevelBank, g, TileDrawer.LEVEL_TILESET_TILEAMOUNT_WIDTH, paletteNum, TILEMAP_SCALE);
                    pictureBox_tileset.Invalidate();
                }
            }
        }

        private void updateImage_Tile()
        {
            if ((levelEditor.Level != null) && (levelEditor.LevelBank != null)) {
                using (Graphics g = Graphics.FromImage(bitmapTile)) {
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    g.PixelOffsetMode = PixelOffsetMode.Half;
                    g.Clear(fillColor);
                    byte paletteNum = (byte)(levelEditor.Level.PaletteIndex[(int)numericUpDown_tilePalette.Value] - 1);
                    TileDrawer.drawTileOnCanvas(levelEditor.LevelBank, g, 0, 0, selectedTileValue, paletteNum, 
                                                checkBox_hflip.Checked, checkBox_vflip.Checked, TILE_SELECTOR_SCALE);
                    pictureBox_tile.Invalidate();
                }
            }
        }

        private void updateImage_Level()
        {
            if ((levelEditor.Level != null) && (levelEditor.LevelBank != null)) {
                using (Graphics g = Graphics.FromImage(bitmapLevel)) {
                    g.Clear(fillColor);
                    TileDrawer.drawLevelOnCanvas(g, levelEditor.Level, levelEditor.LevelBank);
                    pictureBox_level.Invalidate();
                }
            }
        }

        private void invalidateTile(Control control, int tileNum, int scale = 1)
        {
            Point tileCoords = CoordinateConverter.getMouseCoordsFromTileNumber(tileNum, TileDrawer.LEVEL_CANVAS_TILEAMOUNT_WIDTH, scale);
            Rectangle rc = new Rectangle(tileCoords.X, tileCoords.Y, GraphicBank.TILE_WIDTH, GraphicBank.TILE_HEIGHT);
            control.Invalidate(rc);
        }

        private void checkBox_vflip_CheckedChanged(object sender, EventArgs e)
        {
            updateImage_Tile();
        }

        private void checkBox_hflip_CheckedChanged(object sender, EventArgs e)
        {
            updateImage_Tile();
        }
    }
}

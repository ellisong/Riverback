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

        private TileSelector<TilemapTile> tilemapTileSelector;
        private int currentTilesetTile;
        private int lastLevelTileSelected;
        private byte bankPaletteNum;

        private Bitmap bitmapTileset;
        private Bitmap bitmapTile;
        private Bitmap bitmapLevel;

        public MainForm()
        {
            InitializeComponent();
            levelEditor = new LevelEditor();
            tilemapTileSelector = new TileSelector<TilemapTile>();
            lastLevelTileSelected = -1;
            currentTilesetTile = 1;
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
                        bankPaletteNum = (byte)(levelEditor.Level.PaletteIndex[(int)numericUpDown_tilePalette.Value] - 1);
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
                currentTilesetTile = 1;
                updateImage_Tileset();
                updateImage_Tile();
                updateImage_Level();
            }
        }

        private void numericUpDown_tilePalette_ValueChanged(object sender, EventArgs e)
        {
            if ((levelEditor.Level != null) && (levelEditor.LevelBank != null)) {
                bankPaletteNum = (byte)(levelEditor.Level.PaletteIndex[(int)numericUpDown_tilePalette.Value] - 1);
                updateImage_Tileset();
                updateImage_Tile();
            }
        }

        private void pictureBox_tileset_MouseClick(object sender, MouseEventArgs e)
        {
            if ((levelEditor.LevelBank != null) && (e.Button == MouseButtons.Left)) {
                Point mouseCoords = new Point(e.X, e.Y);
                int tileNum = CoordinateConverter.getTileNumberFromMouseCoords(mouseCoords, TileDrawer.LEVEL_TILESET_TILEAMOUNT_WIDTH, TILEMAP_SCALE_INT);
                if (tileNum < levelEditor.LevelBank.tileAmount) {
                    currentTilesetTile = tileNum;
                    updateImage_Tile();
                }
            }
        }

        private TilemapTile getCurrentTilesetTileAsTilemapTile()
        {
            TilemapTile tile = new TilemapTile();
            tile.setTileAndBankValue(levelEditor.TileOffset, currentTilesetTile);
            tile.VFlip = checkBox_vflip.Checked;
            tile.HFlip = checkBox_hflip.Checked;
            tile.Priority = checkBox_priority.Checked;
            tile.Palette = (byte)numericUpDown_tilePalette.Value;
            return tile;
        }

        private void updateTilesInLevelEditor(Point mouseCoords)
        {
            if (levelEditor.Level != null) {
                List<TileSelection<TilemapTile>> tileList;
                if (tilemapTileSelector.Selected) {
                    tileList = tilemapTileSelector.getTilesFromSelection(levelEditor.Level.Tilemap, TileDrawer.LEVEL_CANVAS_TILEAMOUNT_WIDTH);
                } else {
                    tileList = new List<TileSelection<TilemapTile>>();
                    TilemapTile item = getCurrentTilesetTileAsTilemapTile();
                    tileList.Add(new TileSelection<TilemapTile>(new Point(0, 0), item));
                }

                Point tileCoords = CoordinateConverter.getTileCoordsFromMouseCoords(mouseCoords);
                Point placementCoords = new Point();
                foreach (var item in tileList) {
                    placementCoords.X = tileCoords.X + item.tileCoords.X;
                    placementCoords.Y = tileCoords.Y + item.tileCoords.Y;
                    int tileNum = CoordinateConverter.getTileNumberFromTileCoords(placementCoords, TileDrawer.LEVEL_CANVAS_TILEAMOUNT_WIDTH);
                    if ((item.tile.Tile != 0) || (item.tile.Bank != 0)) {
                        levelEditor.setTileInTilemap(tileNum, item.tile);
                    } else {
                        if (tilemapTileSelector.Selected == false) {
                            TilemapTile tile = new TilemapTile();
                            levelEditor.setTileInTilemap(tileNum, tile);
                        }
                    }
                }
            }
        }

        private void drawTilesInLevelEditor(Point mouseCoords)
        {
            if (levelEditor.LevelBank != null) {
                List<TileSelection<TilemapTile>> tileList;

                if (tilemapTileSelector.Selected) {
                    tileList = tilemapTileSelector.getTilesFromSelection(levelEditor.Level.Tilemap, TileDrawer.LEVEL_CANVAS_TILEAMOUNT_WIDTH);
                } else {
                    tileList = new List<TileSelection<TilemapTile>>();
                    TilemapTile item = getCurrentTilesetTileAsTilemapTile();
                    tileList.Add(new TileSelection<TilemapTile>(new Point(0, 0), item));
                }

                Point tileCoords = CoordinateConverter.getTileCoordsFromMouseCoords(mouseCoords);
                Point invalidateBottomRightPoint = new Point();
                using (Graphics g = Graphics.FromImage(bitmapLevel)) {
                    Point tempCoord = new Point();
                    foreach (var item in tileList) {
                        tempCoord.X = tileCoords.X + item.tileCoords.X;
                        tempCoord.Y = tileCoords.Y + item.tileCoords.Y;
                        Point alignedCoords = CoordinateConverter.getMouseCoordsFromTileCoords(tempCoord);
                        if ((item.tile.Tile != 0) || (item.tile.Bank != 0)) {
                            byte bankPaletteNumber = (byte)(levelEditor.Level.PaletteIndex[item.tile.Palette] - 1);
                            int bankTileNum;
                            if (tilemapTileSelector.Selected)
                                bankTileNum = (item.tile.Bank * 256) + item.tile.Tile;
                            else
                                bankTileNum = currentTilesetTile;
                                
                            TileDrawer.drawTileOnCanvas(levelEditor.LevelBank, g, alignedCoords.X, alignedCoords.Y, 
                                                        bankTileNum, bankPaletteNumber, item.tile.VFlip, item.tile.HFlip);
                        } else {
                            if (tilemapTileSelector.Selected == false) {
                                TileDrawer.clearTileOnCanvas(g, fillBrush, alignedCoords.X, alignedCoords.Y);
                            }
                        }
                        if (invalidateBottomRightPoint.X < alignedCoords.X)
                            invalidateBottomRightPoint.X = alignedCoords.X;
                        if (invalidateBottomRightPoint.Y < alignedCoords.Y)
                            invalidateBottomRightPoint.Y = alignedCoords.Y;
                    }
                }
                if ((invalidateBottomRightPoint.X > 0) && (invalidateBottomRightPoint.Y > 0)) {
                    int width = invalidateBottomRightPoint.X - tileCoords.X + GraphicBank.TILE_WIDTH;
                    int height = invalidateBottomRightPoint.Y - tileCoords.Y + GraphicBank.TILE_HEIGHT;
                    pictureBox_level.Invalidate(new Rectangle(tileCoords.X, tileCoords.Y, width, height));
                }
            }
        }

        private void updateImage_Tileset()
        {
            if ((levelEditor.Level != null) && (levelEditor.LevelBank != null)) {
                using (Graphics g = Graphics.FromImage(bitmapTileset)) {
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    g.PixelOffsetMode = PixelOffsetMode.Half;
                    g.Clear(fillColor);
                    TileDrawer.drawAllTilesOnCanvas(levelEditor.LevelBank, g, TileDrawer.LEVEL_TILESET_TILEAMOUNT_WIDTH, bankPaletteNum, TILEMAP_SCALE);
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
                    TileDrawer.drawTileOnCanvas(levelEditor.LevelBank, g, 0, 0, currentTilesetTile, bankPaletteNum,
                                                checkBox_vflip.Checked, checkBox_hflip.Checked, TILE_SELECTOR_SCALE);
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

        private void invalidateTile(Control control, Point tileCoords, int scale = 1)
        {
            Rectangle rc = new Rectangle(tileCoords.X, tileCoords.Y, GraphicBank.TILE_WIDTH * scale, GraphicBank.TILE_HEIGHT * scale);
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

        private void checkBox_priority_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox_level_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) {
                tilemapTileSelector.selectStart(new Point(e.X, e.Y));
            } else if (e.Button == MouseButtons.Right) {
                Point mouseCoords = new Point(e.X, e.Y);
                updateTilesInLevelEditor(mouseCoords);
                drawTilesInLevelEditor(mouseCoords);
                if (tilemapTileSelector.Selected == false) {
                    int tileNum = CoordinateConverter.getTileNumberFromMouseCoords(new Point(e.X, e.Y), TileDrawer.LEVEL_CANVAS_TILEAMOUNT_WIDTH);
                    lastLevelTileSelected = tileNum;
                }
            } else if (e.Button == MouseButtons.Middle) {
                tilemapTileSelector.clearSelection();
                lastLevelTileSelected = -1;
            }
        }

        private void pictureBox_level_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) {
                Point mouseCoords = new Point(e.X, e.Y);
                // Do rectangle drawing here
            } else if (e.Button == MouseButtons.Right) {
                Point mouseCoords = new Point(e.X, e.Y);
                int tileNum = CoordinateConverter.getTileNumberFromMouseCoords(mouseCoords, TileDrawer.LEVEL_CANVAS_TILEAMOUNT_WIDTH);
                if ((tileNum != lastLevelTileSelected) && (tilemapTileSelector.Selected == false)) {
                    updateTilesInLevelEditor(mouseCoords);
                    drawTilesInLevelEditor(mouseCoords);
                    lastLevelTileSelected = tileNum;
                }
            }
        }

        private void pictureBox_level_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) {
                tilemapTileSelector.selectEnd(new Point(e.X, e.Y));
            }
        }

        private void button_deselect_Click(object sender, EventArgs e)
        {
            tilemapTileSelector.clearSelection();
            lastLevelTileSelected = -1;
        }
    }
}

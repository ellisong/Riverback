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
using System.Reflection;

namespace Riverback
{
    public partial class MainForm : Form
    {
        private const int LEVEL_TILEAMOUNT_WIDTH = 64;
        private const int LEVEL_TILESET_TILEAMOUNT_WIDTH = 16;
        private const float TILE_SELECTOR_SCALE = 4.0f;
        private const int TILEMAP_SCALE_INT = 2;
        private const float TILEMAP_SCALE = 2.0f;

        private System.Drawing.Color fillColor;
        private System.Drawing.Brush fillBrush;
        private System.Drawing.Color highlightColor;
        private System.Drawing.Brush highlightBrush;

        private LevelEditor levelEditor;
        private bool isLevelLoaded;
        private byte[] romdata;

        private CoordinateConverter coordConverterLevel;
        private CoordinateConverter coordConverterTileset;

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
            coordConverterLevel = new CoordinateConverter(LEVEL_TILEAMOUNT_WIDTH, TileDrawer.TILE_WIDTH);
            coordConverterTileset = new CoordinateConverter(LEVEL_TILESET_TILEAMOUNT_WIDTH, 
                                                            TILEMAP_SCALE_INT * TileDrawer.TILE_WIDTH);
            lastLevelTileSelected = -1;
            currentTilesetTile = 0;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            fillColor = System.Drawing.Color.DarkGray;
            fillBrush = System.Drawing.Brushes.DarkGray;
            highlightColor = System.Drawing.Color.FromArgb(192, System.Drawing.Color.FloralWhite);
            highlightBrush = new System.Drawing.SolidBrush(highlightColor);

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
                    isLevelLoaded = false;
                    byte[] openedData = File.ReadAllBytes(openFileDialog.FileName);
                    romdata = RomWriter.expandRom(openedData);
                    if (romdata != null) {
                        deselectTiles();
                        openLevel();
                        isLevelLoaded = true;
                    }
                }
            }
        }

        private void MainMenu_SaveLevel_Click(object sender, EventArgs e)
        {
            if (isLevelLoaded) {
                if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                    string fileName;
                    if ((fileName = saveFileDialog.FileName) != "") {
                        isLevelLoaded = false;
                        RomWriter writer = new RomWriter(romdata);
                        writer.writeLevel(levelEditor.Level);
                        File.WriteAllBytes(fileName, romdata);
                        isLevelLoaded = true;
                    }
                }
            }
        }

        private void savePhysmapdebuggingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isLevelLoaded) {
                if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                    string fileName;
                    if ((fileName = saveFileDialog.FileName) != "") {
                        isLevelLoaded = false;
                        File.WriteAllBytes(fileName, levelEditor.Level.Physmap);
                        isLevelLoaded = true;
                    }
                }
            }
        }

        private void writePhysmapdebuggingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isLevelLoaded) {
                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    if (openFileDialog.CheckFileExists) {
                        isLevelLoaded = false;
                        byte[] openedData = File.ReadAllBytes(openFileDialog.FileName);
                        Array.ConstrainedCopy(openedData, 0, levelEditor.Level.Physmap, 0, Level.LEVEL_TILE_AMOUNT);
                        deselectTiles();
                        isLevelLoaded = true;
                    }
                }
            }
        }

        private void MainMenu_Exit_Click(object sender, EventArgs e)
        {
            // TODO: save prompt on level change, don't forget isLevelLoaded = false then true
            Application.Exit();
        }

        private void numericUpDown_levelSelector_ValueChanged(object sender, EventArgs e)
        {
            if (romdata != null) {
                if (isLevelLoaded) {
                    isLevelLoaded = false;
                    deselectTiles();
                    openLevel();
                    isLevelLoaded = true;
                }
            }
        }

        private void numericUpDown_tilePalette_ValueChanged(object sender, EventArgs e)
        {
            if (isLevelLoaded) {
                bankPaletteNum = (byte)(levelEditor.Level.PaletteIndex[(int)numericUpDown_tilePalette.Value] - 1);
                updateImage_Tileset();
                updateImage_Tile();
            }
        }

        private void pictureBox_tileset_MouseClick(object sender, MouseEventArgs e)
        {
            if (isLevelLoaded) {
                Point mouseCoords = new Point(e.X, e.Y);
                int tileNum = coordConverterTileset.getTileNumberFromMouseCoords(mouseCoords);
                if (tileNum < levelEditor.LevelBank.tileAmount) {
                    if (tilemapTileSelector.Selected) {
                        deselectTiles();
                    }
                    currentTilesetTile = tileNum;
                    updateImage_Tile();
                }
            }
        }

        private void checkBox_vflip_CheckedChanged(object sender, EventArgs e)
        {
            if (isLevelLoaded)
                updateImage_Tile();
        }

        private void checkBox_hflip_CheckedChanged(object sender, EventArgs e)
        {
            if (isLevelLoaded)
                updateImage_Tile();
        }

        private void checkBox_priority_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox_level_MouseDown(object sender, MouseEventArgs e)
        {
            if (isLevelLoaded) {
                if (e.Button == MouseButtons.Left) {
                    if (tilemapTileSelector.Selected) {
                        deselectTiles();
                    }
                    tilemapTileSelector.selectStart(new Point(e.X, e.Y), TileDrawer.TILE_WIDTH);
                } else if (e.Button == MouseButtons.Right) {
                    Point mouseCoords = new Point(e.X, e.Y);
                    isLevelLoaded = false;
                    updateTilesInLevelEditor(mouseCoords);
                    isLevelLoaded = true;
                    drawTilesInLevelEditor(mouseCoords);
                    highlightSelectedTilesInLevelEditor();
                    if (tilemapTileSelector.Selected == false) {
                        int tileNum = coordConverterLevel.getTileNumberFromMouseCoords(new Point(e.X, e.Y));
                        lastLevelTileSelected = tileNum;
                    }
                } else if (e.Button == MouseButtons.Middle) {
                    if (tilemapTileSelector.Selected) {
                        deselectTiles();
                    }
                }
            }
        }

        private void pictureBox_level_MouseMove(object sender, MouseEventArgs e)
        {
            if (isLevelLoaded) {
                if (e.Button == MouseButtons.Left) {
                    Point mouseCoords = new Point(e.X, e.Y);
                    // Do rectangle drawing here
                } else if (e.Button == MouseButtons.Right) {
                    Point mouseCoords = new Point(e.X, e.Y);
                    int tileNum = coordConverterLevel.getTileNumberFromMouseCoords(mouseCoords);
                    if ((tileNum != lastLevelTileSelected) && (tilemapTileSelector.Selected == false)) {
                        isLevelLoaded = false;
                        updateTilesInLevelEditor(mouseCoords);
                        isLevelLoaded = true;
                        drawTilesInLevelEditor(mouseCoords);
                        lastLevelTileSelected = tileNum;
                    }
                }
            }
        }

        private void pictureBox_level_MouseUp(object sender, MouseEventArgs e)
        {
            if (isLevelLoaded) {
                if (e.Button == MouseButtons.Left) {
                    tilemapTileSelector.selectEnd(new Point(e.X, e.Y), TileDrawer.TILE_WIDTH);
                    highlightSelectedTilesInLevelEditor();
                }
            }
        }

        private void button_deselect_Click(object sender, EventArgs e)
        {
            if (isLevelLoaded) {
                if (tilemapTileSelector.Selected) {
                    deselectTiles();
                }
            }
        }

        private void openLevel()
        {
            levelEditor.openLevel(romdata, (byte)numericUpDown_levelSelector.Value);
            levelEditor.updateGraphicsBanks(romdata);
            levelEditor.updateLevelBank();
            bankPaletteNum = (byte)(levelEditor.Level.PaletteIndex[(int)numericUpDown_tilePalette.Value] - 1);
            currentTilesetTile = 0;
            updateImage_Tileset();
            updateImage_Tile();
            updateImage_Level();
        }

        private TilemapTile getCurrentTilesetTileAsTilemapTile()
        {
            TilemapTile tile = new TilemapTile();
            tile.Bank = (byte)(currentTilesetTile / 256);
            tile.Tile = (byte)(currentTilesetTile % 256);
            tile.VFlip = checkBox_vflip.Checked;
            tile.HFlip = checkBox_hflip.Checked;
            tile.Priority = checkBox_priority.Checked;
            tile.Palette = (byte)numericUpDown_tilePalette.Value;
            return tile;
        }

        private List<TileSelection<TilemapTile>> getSelectedTiles()
        {
            List<TileSelection<TilemapTile>> tileList;
            if (tilemapTileSelector.Selected) {
                tileList = tilemapTileSelector.getTilesFromSelection(levelEditor.Level.Tilemap, LEVEL_TILEAMOUNT_WIDTH);
            } else {
                tileList = new List<TileSelection<TilemapTile>>();
                TilemapTile item = getCurrentTilesetTileAsTilemapTile();
                tileList.Add(new TileSelection<TilemapTile>(new Point(0, 0), item));
            }
            return tileList;
        }

        private void updateTilesInLevelEditor(Point mouseCoords)
        {
            if (levelEditor.Level != null) {
                var tileList = getSelectedTiles();
                Point tileCoords = coordConverterLevel.getTileCoordsFromMouseCoords(mouseCoords);
                Point placementCoords = new Point();
                foreach (var item in tileList) {
                    placementCoords.X = tileCoords.X + item.tileCoords.X;
                    placementCoords.Y = tileCoords.Y + item.tileCoords.Y;
                    if ((placementCoords.X < LEVEL_TILEAMOUNT_WIDTH) 
                        && (placementCoords.Y < LEVEL_TILEAMOUNT_WIDTH)) {
                        int tileNum = coordConverterLevel.getTileNumberFromTileCoords(placementCoords);
                        int tileValue = item.tile.Tile + (item.tile.Bank * 256);
                        if (tileValue > 0) {
                            levelEditor.setTileInTilemap(tileNum, 
                                                         tileValue, 
                                                         item.tile.VFlip, 
                                                         item.tile.HFlip, 
                                                         item.tile.Priority, 
                                                         item.tile.Palette);
                        } else {
                            if (tilemapTileSelector.Selected == false) {
                                TilemapTile tile = new TilemapTile();
                                levelEditor.setTileInTilemap(tileNum, 
                                                             tileValue, 
                                                             item.tile.VFlip, 
                                                             item.tile.HFlip, 
                                                             item.tile.Priority, 
                                                             item.tile.Palette);
                            }
                        }
                    }
                }
            }
        }

        private void drawTilesInLevelEditor(Point mouseCoords, bool clearEmptyTiles = false)
        {
            if (levelEditor.LevelBank != null) {
                var tileList = getSelectedTiles();
                Point tileCoords = coordConverterLevel.getTileCoordsFromMouseCoords(mouseCoords);
                Point invalidateBottomRightPoint = new Point();
                using (Graphics g = Graphics.FromImage(bitmapLevel)) {
                    Point tempCoord = new Point();
                    foreach (var item in tileList) {
                        tempCoord.X = tileCoords.X + item.tileCoords.X;
                        tempCoord.Y = tileCoords.Y + item.tileCoords.Y;
                        Point alignedCoords = coordConverterLevel.getMouseCoordsFromTileCoords(tempCoord);
                        if ((item.tile.Tile != 0) || (item.tile.Bank != 0)) {
                            byte bankPaletteNumber = (byte)(levelEditor.Level.PaletteIndex[item.tile.Palette] - 1);
                            int bankTileNum;
                            if (tilemapTileSelector.Selected)
                                bankTileNum = (item.tile.Bank * 256) + item.tile.Tile;
                            else
                                bankTileNum = currentTilesetTile;
                            TileDrawer.clearTileOnCanvas(g, fillBrush, alignedCoords.X, alignedCoords.Y);
                            Bitmap tileImg = levelEditor.LevelBank.getTileImage(bankTileNum, 
                                                                                bankPaletteNumber, 
                                                                                TileDrawer.TILE_WIDTH);
                            TileDrawer.drawTileOnCanvas(tileImg, 
                                                        g, 
                                                        alignedCoords.X, 
                                                        alignedCoords.Y,
                                                        item.tile.VFlip, 
                                                        item.tile.HFlip);
                        } else {
                            if ((clearEmptyTiles == true) || (tilemapTileSelector.Selected == false))
                                TileDrawer.clearTileOnCanvas(g, fillBrush, alignedCoords.X, alignedCoords.Y);
                        }
                        if (invalidateBottomRightPoint.X < alignedCoords.X)
                            invalidateBottomRightPoint.X = alignedCoords.X;
                        if (invalidateBottomRightPoint.Y < alignedCoords.Y)
                            invalidateBottomRightPoint.Y = alignedCoords.Y;
                    }
                }
                if ((invalidateBottomRightPoint.X > 0) && (invalidateBottomRightPoint.Y > 0)) {
                    int width = invalidateBottomRightPoint.X - tileCoords.X + TileDrawer.TILE_WIDTH;
                    int height = invalidateBottomRightPoint.Y - tileCoords.Y + TileDrawer.TILE_WIDTH;
                    pictureBox_level.Invalidate(new Rectangle(tileCoords.X, tileCoords.Y, width, height));
                }
            }
        }

        private void highlightSelectedTilesInLevelEditor()
        {
            if (tilemapTileSelector.Selected) {
                Rectangle rect = coordConverterLevel.getMouseCoordsFromRectCoords(tilemapTileSelector.TileCoords);
                using (Graphics g = Graphics.FromImage(bitmapLevel)) {
                    drawTilesInLevelEditor(new Point(rect.X, rect.Y), true);
                    g.FillRectangle(highlightBrush, rect);
                }
            }
        }

        private void deselectTiles()
        {
            if (tilemapTileSelector.Selected) {
                Point tileCoords = new Point(tilemapTileSelector.TileCoords.X, tilemapTileSelector.TileCoords.Y);
                Point mouseCoords = coordConverterLevel.getMouseCoordsFromTileCoords(tileCoords);
                drawTilesInLevelEditor(mouseCoords, true);
            }
            tilemapTileSelector.clearSelection();
            lastLevelTileSelected = -1;
        }

        private Bitmap getImageFromResources(string filename)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream imgStream = assembly.GetManifestResourceStream("Riverback." + filename);
            return new Bitmap(imgStream);
        }

        private void updateImage_Tileset()
        {
            if ((levelEditor.Level != null) && (levelEditor.LevelBank != null)) {
                using (Graphics g = Graphics.FromImage(bitmapTileset)) {
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    g.PixelOffsetMode = PixelOffsetMode.Half;
                    g.Clear(fillColor);
                    TileDrawer.drawAllTilesOnCanvas(levelEditor.LevelBank, 
                                                    g, 
                                                    LEVEL_TILESET_TILEAMOUNT_WIDTH, 
                                                    bankPaletteNum, 
                                                    TILEMAP_SCALE);
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
                    Bitmap tileImg = levelEditor.LevelBank.getTileImage(currentTilesetTile, 
                                                                        bankPaletteNum, 
                                                                        TileDrawer.TILE_WIDTH);
                    TileDrawer.drawTileOnCanvas(tileImg, 
                                                g, 
                                                0, 
                                                0, 
                                                checkBox_vflip.Checked, 
                                                checkBox_hflip.Checked, 
                                                TILE_SELECTOR_SCALE);
                    pictureBox_tile.Invalidate();
                }
            }
        }

        private void updateImage_Level()
        {
            if ((levelEditor.Level != null) && (levelEditor.LevelBank != null)) {
                using (Graphics g = Graphics.FromImage(bitmapLevel)) {
                    g.Clear(fillColor);
                    TileDrawer.drawLevelOnCanvas(g, levelEditor.Level, levelEditor.LevelBank, LEVEL_TILEAMOUNT_WIDTH);
                    pictureBox_level.Invalidate();
                }
            }
        }
    }
}

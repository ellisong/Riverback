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
using System.Resources;

namespace Riverback
{
    public partial class MainForm : Form
    {
        private const int LEVEL_TILEAMOUNT_WIDTH = 64;
        private const int LEVEL_TILESET_TILEAMOUNT_WIDTH = 16;
        private const int LEVEL_PHYSMAP_TILEAMOUNT_WIDTH = 16;
        private const int PHYSTILE_TILEAMOUNT_WIDTH = 16;
        private const int PHYSTILE_TILEAMOUNT = 256;
        private const float TILE_SELECTOR_SCALE = 4.0f;
        private const float TILEMAP_SCALE = 2.0f;
        private const int IMAGE_DPI = 72;

        private System.Drawing.Color fillColor;
        private System.Drawing.Brush fillBrush;
        private System.Drawing.Color highlightColor;
        private System.Drawing.Brush highlightBrush;

        private LevelEditor levelEditor;
        private bool isLevelLoaded;
        private byte[] romdata;

        private CoordinateConverter coordConverterLevel;
        private CoordinateConverter coordConverterTileset;
        private CoordinateConverter coordConverterPhysmap;

        private TileSelector tilemapTileSelector;
        private int currentTilesetTile;
        private int currentPhysmapTile;
        private int lastLevelTileSelected;
        private byte bankPaletteNum;

        private Bitmap bitmapTileset;
        private Bitmap bitmapTilemapTile;
        private Image imagePhysTileset;
        private Bitmap bitmapPhysTile;
        private Bitmap bitmapLevel;

        public MainForm()
        {
            InitializeComponent();
            levelEditor = new LevelEditor();
            tilemapTileSelector = new TileSelector();
            coordConverterLevel = new CoordinateConverter(LEVEL_TILEAMOUNT_WIDTH, TileDrawer.TILE_WIDTH);
            coordConverterTileset = new CoordinateConverter(LEVEL_TILESET_TILEAMOUNT_WIDTH, 
                                                            (int)TILEMAP_SCALE * TileDrawer.TILE_WIDTH);
            coordConverterPhysmap = new CoordinateConverter(LEVEL_PHYSMAP_TILEAMOUNT_WIDTH, TileDrawer.TILE_WIDTH);
            lastLevelTileSelected = -1;
            currentTilesetTile = 0;
            currentPhysmapTile = 0;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            fillColor = System.Drawing.Color.DarkGray;
            fillBrush = System.Drawing.Brushes.DarkGray;
            highlightColor = System.Drawing.Color.FromArgb(192, System.Drawing.Color.FloralWhite);
            highlightBrush = new System.Drawing.SolidBrush(highlightColor);

            bitmapTileset = new Bitmap(pictureBox_tileset.Width, pictureBox_tileset.Height);
            bitmapTileset.SetResolution(IMAGE_DPI, IMAGE_DPI);
            pictureBox_tileset.Image = bitmapTileset;
            bitmapTilemapTile = new Bitmap(pictureBox_tilemaptile.Width, pictureBox_tilemaptile.Height);
            bitmapTilemapTile.SetResolution(IMAGE_DPI, IMAGE_DPI);
            pictureBox_tilemaptile.Image = bitmapTilemapTile;
            bitmapPhysTile = new Bitmap(pictureBox_phystile.Width, pictureBox_phystile.Height);
            bitmapPhysTile.SetResolution(IMAGE_DPI, IMAGE_DPI);
            pictureBox_phystile.Image = bitmapPhysTile;
            bitmapLevel = new Bitmap(pictureBox_level.Width, pictureBox_level.Height);
            bitmapLevel.SetResolution(IMAGE_DPI, IMAGE_DPI);
            pictureBox_level.Image = bitmapLevel;

            imagePhysTileset = (Bitmap)Riverback.Properties.Resources.physmap;
            pictureBox_phystiles.Image = (Bitmap)Riverback.Properties.Resources.physmap2;
            pictureBox_phystiles.Invalidate();
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
            // TODO: save prompt on level change
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
                updateImage_TilemapTile();
            }
        }

        private void checkBox_vflip_CheckedChanged(object sender, EventArgs e)
        {
            if (isLevelLoaded)
                updateImage_TilemapTile();
        }

        private void checkBox_hflip_CheckedChanged(object sender, EventArgs e)
        {
            if (isLevelLoaded)
                updateImage_TilemapTile();
        }

        private void checkBox_priority_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox_field_show_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox_physmap_show_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox_bytes_CheckedChanged(object sender, EventArgs e)
        {
            updateImage_Physmap();
            updateImage_PhysmapTile();
        }

        private void checkBox_grid_show_CheckedChanged(object sender, EventArgs e)
        {
            updateImage_Tileset();
            updateImage_Physmap();
            updateImage_PhysmapTile();
            updateImage_Level();
        }

        private void button_deselect_Click(object sender, EventArgs e)
        {
            if (isLevelLoaded) {
                if (tilemapTileSelector.Selected) {
                    deselectTiles();
                }
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
                    updateImage_TilemapTile();
                }
            }
        }

        private void pictureBox_phystiles_MouseClick(object sender, MouseEventArgs e)
        {
            if (isLevelLoaded) {
                Point mouseCoords = new Point(e.X, e.Y);
                int tileNum = coordConverterTileset.getTileNumberFromMouseCoords(mouseCoords);
                Point alignedMouseCoords = coordConverterTileset.getMouseCoordsFromTileNumber(tileNum);
                if (tileNum < PHYSTILE_TILEAMOUNT) {
                    if (tilemapTileSelector.Selected) {
                        deselectTiles();
                    }
                    currentPhysmapTile = tileNum;
                    updateImage_PhysmapTile();
                }
            }
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
                    setTilemapTilesInLevelEditor(mouseCoords);
                    drawTilemapTilesInLevelEditor(mouseCoords);
                    highlightSelectedTilesInLevelEditor();
                    invalidateTilesInLevelEditor(mouseCoords);
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
                        setTilemapTilesInLevelEditor(mouseCoords);
                        drawTilemapTilesInLevelEditor(mouseCoords);
                        invalidateTilesInLevelEditor(mouseCoords);
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

        private void openLevel()
        {
            levelEditor.openLevel(romdata, (byte)numericUpDown_levelSelector.Value);
            levelEditor.updateGraphicsBanks(romdata);
            levelEditor.updateLevelBank();
            bankPaletteNum = (byte)(levelEditor.Level.PaletteIndex[(int)numericUpDown_tilePalette.Value] - 1);
            currentTilesetTile = 0;
            currentPhysmapTile = 0;
            updateImage_Tileset();
            updateImage_TilemapTile();
            updateImage_PhysmapTile();
            updateImage_Level();
        }

        private List<TileSelection<TilemapTile>> getSelectedTiles()
        {
            List<TileSelection<TilemapTile>> tileList;
            if (tilemapTileSelector.Selected) {
                tileList = tilemapTileSelector.getTilesFromSelection(levelEditor.Level.Tilemap, LEVEL_TILEAMOUNT_WIDTH);
            } else {
                tileList = new List<TileSelection<TilemapTile>>();
                TilemapTile item = new TilemapTile(currentTilesetTile, 
                                                   checkBox_vflip.Checked, 
                                                   checkBox_hflip.Checked, 
                                                   checkBox_priority.Checked, 
                                                   (byte)numericUpDown_tilePalette.Value);
                tileList.Add(new TileSelection<TilemapTile>(new Point(0, 0), item));
            }
            return tileList;
        }

        private void setTilemapTilesInLevelEditor(Point mouseCoords)
        {
            var tileList = getSelectedTiles();
            Point tileCoords = coordConverterLevel.getTileCoordsFromMouseCoords(mouseCoords);
            Point placementCoords = new Point();
            foreach (var item in tileList) {
                placementCoords.X = tileCoords.X + item.tileCoords.X;
                placementCoords.Y = tileCoords.Y + item.tileCoords.Y;
                if ((placementCoords.X < LEVEL_TILEAMOUNT_WIDTH) && (placementCoords.Y < LEVEL_TILEAMOUNT_WIDTH)) {
                    int tileNum = coordConverterLevel.getTileNumberFromTileCoords(placementCoords);
                    if ((item.tile.Bank > 0) || (item.tile.Tile > 0)) {
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

        private void drawTilemapTilesInLevelEditor(Point mouseCoords, bool clearEmptyTiles = false)
        {
            var tileList = getSelectedTiles();
            Point tileCoords = coordConverterLevel.getTileCoordsFromMouseCoords(mouseCoords);
            using (Graphics g = Graphics.FromImage(bitmapLevel)) {
                Point tempCoord = new Point();
                foreach (var item in tileList) {
                    tempCoord.X = tileCoords.X + item.tileCoords.X;
                    tempCoord.Y = tileCoords.Y + item.tileCoords.Y;
                    Point alignedCoords = coordConverterLevel.getMouseCoordsFromTileCoords(tempCoord);
                    if ((item.tile.Tile > 0) || (item.tile.Bank > 0)) {
                        byte bankPaletteNumber = (byte)(levelEditor.Level.PaletteIndex[item.tile.Palette] - 1);
                        int bankTileNum = currentTilesetTile;
                        if (tilemapTileSelector.Selected)
                            bankTileNum = (item.tile.Bank * 256) + item.tile.Tile;

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
                    if (checkBox_grid_show.Checked)
                        TileDrawer.drawGridCellOnCanvas(g, alignedCoords.X, alignedCoords.Y);
                }
            }
        }

        private void invalidateTilesInLevelEditor(Point mouseCoords)
        {
            var tileList = getSelectedTiles();
            Point tileCoords = coordConverterLevel.getTileCoordsFromMouseCoords(mouseCoords);
            Point tempCoord = new Point();
            Point invalidateBottomRightPoint = new Point();
            foreach (var item in tileList) {
                tempCoord.X = tileCoords.X + item.tileCoords.X;
                tempCoord.Y = tileCoords.Y + item.tileCoords.Y;
                Point alignedCoords = coordConverterLevel.getMouseCoordsFromTileCoords(tempCoord);
                if (invalidateBottomRightPoint.X < alignedCoords.X)
                    invalidateBottomRightPoint.X = alignedCoords.X;
                if (invalidateBottomRightPoint.Y < alignedCoords.Y)
                    invalidateBottomRightPoint.Y = alignedCoords.Y;
            }
            if ((invalidateBottomRightPoint.X > 0) && (invalidateBottomRightPoint.Y > 0)) {
                Point topLeft = coordConverterLevel.getMouseCoordsFromTileCoords(tileCoords);
                int width = invalidateBottomRightPoint.X - topLeft.X + TileDrawer.TILE_WIDTH;
                int height = invalidateBottomRightPoint.Y - topLeft.Y + TileDrawer.TILE_WIDTH;
                pictureBox_level.Invalidate(new Rectangle(topLeft.X, topLeft.Y, width, height));
            }
        }

        private void highlightSelectedTilesInLevelEditor()
        {
            if (tilemapTileSelector.Selected) {
                Rectangle rect = coordConverterLevel.getMouseCoordsFromRectCoords(tilemapTileSelector.TileCoords);
                using (Graphics g = Graphics.FromImage(bitmapLevel)) {
                    Point point = new Point(rect.X, rect.Y);
                    drawTilemapTilesInLevelEditor(point, true);
                    g.FillRectangle(highlightBrush, rect);
                    invalidateTilesInLevelEditor(point);
                }
            }
        }

        private void deselectTiles()
        {
            if (tilemapTileSelector.Selected) {
                Point tileCoords = new Point(tilemapTileSelector.TileCoords.X, tilemapTileSelector.TileCoords.Y);
                Point mouseCoords = coordConverterLevel.getMouseCoordsFromTileCoords(tileCoords);
                drawTilemapTilesInLevelEditor(mouseCoords, true);
                invalidateTilesInLevelEditor(mouseCoords);
            }
            tilemapTileSelector.clearSelection();
            lastLevelTileSelected = -1;
        }

        private void updateImage_Tileset()
        {
            if ((levelEditor.Level != null) && (levelEditor.LevelBank != null)) {
                using (Graphics g = Graphics.FromImage(pictureBox_tileset.Image)) {
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    g.PixelOffsetMode = PixelOffsetMode.Half;
                    g.Clear(fillColor);
                    TileDrawer.drawAllTilesOnCanvas(levelEditor.LevelBank, 
                                                    g, 
                                                    LEVEL_TILESET_TILEAMOUNT_WIDTH, 
                                                    bankPaletteNum, 
                                                    TILEMAP_SCALE);
                    if (checkBox_grid_show.Checked)
                        g.DrawImage(Riverback.Properties.Resources.gridtile16_256x512, 0, 0);
                    pictureBox_tileset.Invalidate();
                }
            }
        }

        private void updateImage_Physmap()
        {
            if (checkBox_bytes_show.Checked) {
                imagePhysTileset = (Bitmap)Riverback.Properties.Resources.physmap_bytes;
                pictureBox_phystiles.Image = (Bitmap)Riverback.Properties.Resources.physmap_bytes2;
            } else {
                imagePhysTileset = (Bitmap)Riverback.Properties.Resources.physmap;
                pictureBox_phystiles.Image = (Bitmap)Riverback.Properties.Resources.physmap2;
            }
            if (checkBox_grid_show.Checked)
                using (Graphics g = Graphics.FromImage(pictureBox_phystiles.Image))
                    g.DrawImage(Riverback.Properties.Resources.gridtile16_256x256, 
                                0, 
                                0, 
                                new Rectangle(0, 0, 256, 256), 
                                GraphicsUnit.Pixel);
            pictureBox_phystiles.Invalidate();

        }

        private void updateImage_TilemapTile()
        {
            if ((levelEditor.Level != null) && (levelEditor.LevelBank != null)) {
                using (Graphics g = Graphics.FromImage(bitmapTilemapTile)) {
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
                    pictureBox_tilemaptile.Invalidate();
                }
            }
        }

        private void updateImage_PhysmapTile()
        {
            Point alignedMouseCoords = coordConverterPhysmap.getMouseCoordsFromTileNumber(currentPhysmapTile);
            float srcWidth = TileDrawer.TILE_WIDTH;
            float destWidth = TileDrawer.TILE_WIDTH * TILE_SELECTOR_SCALE;
            using (Graphics g = Graphics.FromImage(bitmapPhysTile)) {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = PixelOffsetMode.Half;
                g.Clear(fillColor);
                RectangleF srcRect = new RectangleF(alignedMouseCoords.X, alignedMouseCoords.Y, srcWidth, srcWidth);
                RectangleF destRect = new RectangleF(0, 0, destWidth, destWidth);
                g.DrawImage(imagePhysTileset, destRect, srcRect, GraphicsUnit.Pixel);
                pictureBox_phystile.Invalidate();
            }
        }

        private void updateImage_Level()
        {
            if ((levelEditor.Level != null) && (levelEditor.LevelBank != null)) {
                using (Graphics g = Graphics.FromImage(bitmapLevel)) {
                    g.Clear(fillColor);
                    TileDrawer.drawLevelOnCanvas(g, levelEditor.Level, levelEditor.LevelBank, LEVEL_TILEAMOUNT_WIDTH);
                    if (checkBox_grid_show.Checked)
                        g.DrawImage(Riverback.Properties.Resources.gridtile8_512x512, 0, 0);
                    if (tilemapTileSelector.Selected)
                        highlightSelectedTilesInLevelEditor();
                    pictureBox_level.Invalidate();
                }
            }
        }
    }
}

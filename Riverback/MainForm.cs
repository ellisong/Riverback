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
        private const int LEVEL_TILEINDEX_TILEAMOUNT_WIDTH = 16;
        private const int PHYSTILE_TILEAMOUNT_WIDTH = 16;
        private const int PHYSTILE_TILEAMOUNT = 256;
        private const float TILE_SELECTOR_SCALE = 4.0f;
        private const float TILEMAP_SCALE = 2.0f;
        private const int IMAGE_DPI = 72;
        private const int INDEXTILES_MAX = 0x200;

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
        private CoordinateConverter coordConverterTileIndex;

        private TileSelector tilemapTileSelector;
        private int currentTilesetTile;
        private byte currentPhysmapTile;
        private int lastLevelTileSelected;
        private byte bankPaletteNum;
        private bool[] selectedTileIndices;

        private Bitmap bitmapTileset;
        private Bitmap bitmapTilemapTile;
        private Bitmap bitmapTileIndex;
        private Bitmap bitmapPhysTileset;
        private Bitmap bitmapPhysTileset2;
        private Bitmap bitmapPhysTile;
        private Bitmap bitmapLevel;

        int indexTilesRemaining = 0;

        public MainForm()
        {
            InitializeComponent();
            levelEditor = new LevelEditor();
            tilemapTileSelector = new TileSelector();
            coordConverterLevel = new CoordinateConverter(LEVEL_TILEAMOUNT_WIDTH, TileDrawer.TILE_WIDTH);
            coordConverterTileset = new CoordinateConverter(LEVEL_TILESET_TILEAMOUNT_WIDTH, 
                                                            (int)TILEMAP_SCALE * TileDrawer.TILE_WIDTH);
            coordConverterPhysmap = new CoordinateConverter(LEVEL_PHYSMAP_TILEAMOUNT_WIDTH, TileDrawer.TILE_WIDTH);
            coordConverterTileIndex = new CoordinateConverter(LEVEL_TILEINDEX_TILEAMOUNT_WIDTH, TileDrawer.TILE_WIDTH);
            selectedTileIndices = new bool[2048];
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
            bitmapTileIndex = new Bitmap(pictureBox_indexTiles.Width, pictureBox_indexTiles.Height);
            bitmapTileIndex.SetResolution(IMAGE_DPI, IMAGE_DPI);
            pictureBox_indexTiles.Image = bitmapTileIndex;
            bitmapPhysTileset = (Bitmap)Riverback.Properties.Resources.physmap;
            bitmapPhysTileset.SetResolution(IMAGE_DPI, IMAGE_DPI);
            bitmapPhysTileset2 = (Bitmap)Riverback.Properties.Resources.physmap2;
            bitmapPhysTileset2.SetResolution(IMAGE_DPI, IMAGE_DPI);
            pictureBox_phystiles.Image = bitmapPhysTileset2;
            pictureBox_phystiles.Invalidate();
            bitmapPhysTile = new Bitmap(pictureBox_phystile.Width, pictureBox_phystile.Height);
            bitmapPhysTile.SetResolution(IMAGE_DPI, IMAGE_DPI);
            pictureBox_phystile.Image = bitmapPhysTile;
            bitmapLevel = new Bitmap(pictureBox_level.Width, pictureBox_level.Height);
            bitmapLevel.SetResolution(IMAGE_DPI, IMAGE_DPI);
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

        private void exportLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isLevelLoaded) {
                if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                    string fileName;
                    if ((fileName = saveFileDialog.FileName) != "") {
                        isLevelLoaded = false;
                        RomWriter writer = new RomWriter(romdata);
                        byte[] data = writer.exportLevel(levelEditor.LevelHeader, levelEditor.Level);
                        File.WriteAllBytes(fileName, data);
                        isLevelLoaded = true;
                    }
                }
            }
        }

        private void importLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isLevelLoaded) {
                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    if (openFileDialog.CheckFileExists) {
                        openLevel(openFileDialog.FileName);
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
                updateImages(false, true, true, false, false, false);
            }
        }

        private void checkBox_vflip_CheckedChanged(object sender, EventArgs e)
        {
            if (isLevelLoaded)
                updateImages(false, false, true, false, false, false);
        }

        private void checkBox_hflip_CheckedChanged(object sender, EventArgs e)
        {
            if (isLevelLoaded)
                updateImages(false, false, true, false, false, false);
        }

        private void checkBox_priority_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox_field_show_CheckedChanged(object sender, EventArgs e)
        {
            updateImages(true, false, false, false, false, false);
        }

        private void checkBox_physmap_show_CheckedChanged(object sender, EventArgs e)
        {
            updateImages(true, false, false, false, false, false);
        }

        private void checkBox_bytes_CheckedChanged(object sender, EventArgs e)
        {
            updateImages(true, false, false, true, true, false);
        }

        private void checkBox_grid_show_CheckedChanged(object sender, EventArgs e)
        {
            updateImages(true, true, false, true, true, true);
        }

        private void button_deselect_Click(object sender, EventArgs e)
        {
            if (isLevelLoaded) {
                if (tilemapTileSelector.Selected) {
                    deselectTiles();
                }
            }
        }

        private void button_applyindices_Click(object sender, EventArgs e)
        {
            if (isLevelLoaded) {
                levelEditor.Level.TileIndex = selectedTileIndices.ToList();
                levelEditor.updateLevelBank();
                currentTilesetTile = 0;
                updateImages(true, true, true, false, false, true);
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
                    updateImages(false, false, true, false, false, false);
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
                    currentPhysmapTile = (byte)tileNum;
                    updateImages(false, false, false, false, true, false);
                }
            }
        }

        private void pictureBox_indexTiles_MouseClick(object sender, MouseEventArgs e)
        {
            if (isLevelLoaded) {
                Point mouseCoords = new Point(e.X, e.Y);
                int tileNum = coordConverterTileIndex.getTileNumberFromMouseCoords(mouseCoords);
                if (tileNum > 0) {
                    int index = levelEditor.LevelHeader.graphicsBankIndex * 2;
                    int tileAmount = levelEditor.Banks[index].tileAmount;
                    if (tileNum > tileAmount)
                        index += 1;
                    if (selectedTileIndices[tileNum]) {
                        indexTilesRemaining += 1;
                        selectedTileIndices[tileNum] = false;
                    } else {
                        if (indexTilesRemaining > 0) {
                            indexTilesRemaining -= 1;
                            selectedTileIndices[tileNum] = true;
                        }
                    }
                    updateTextBox_IndexTiles();
                    drawIndexTile(index, tileNum, tileAmount, selectedTileIndices[tileNum], checkBox_grid_show.Checked);
                    Point alignedMouseCoords = coordConverterTileIndex.getMouseCoordsFromTileNumber(tileNum);
                    invalidateIndexTile(alignedMouseCoords);
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
                    drawAndSetTiles(mouseCoords);
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
                        drawAndSetTiles(mouseCoords);
                        lastLevelTileSelected = tileNum;
                    }
                }
            }
        }

        private void pictureBox_level_MouseUp(object sender, MouseEventArgs e)
        {
            if (isLevelLoaded) {
                if (e.Button == MouseButtons.Left) {
                    Point mouseCoords = new Point(e.X, e.Y);
                    tilemapTileSelector.selectEnd(mouseCoords, TileDrawer.TILE_WIDTH);
                    highlightSelectedTilesInLevelEditor(true);
                }
            }
        }

        private void openLevel(string importFileName = "")
        {
            isLevelLoaded = false;
            if (importFileName == "") {
                levelEditor.openLevel(romdata, (byte)numericUpDown_levelSelector.Value);
            } else {
                byte[] openedData = File.ReadAllBytes(importFileName);
                RomWriter writer = new RomWriter(romdata);
                writer.importLevel(openedData, levelEditor.Level, levelEditor.LevelHeader);
            }
            levelEditor.updateGraphicsBanks(romdata);
            levelEditor.updateLevelBank();
            levelEditor.Level.TileIndex.CopyTo(selectedTileIndices);
            isLevelLoaded = true;
            indexTilesRemaining = INDEXTILES_MAX - levelEditor.LevelBank.tileAmount;
            updateTextBox_IndexTiles();
            bankPaletteNum = (byte)(levelEditor.Level.PaletteIndex[(int)numericUpDown_tilePalette.Value] - 1);
            currentTilesetTile = 0;
            currentPhysmapTile = 0;
            updateImages(true, true, true, false, true, true);
        }

        private List<TileSelection<TilemapTile>> getSelectedTilemapTiles()
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

        private List<TileSelection<byte>> getSelectedPhysmapTiles()
        {
            List<TileSelection<byte>> tileList;
            if (tilemapTileSelector.Selected) {
                tileList = tilemapTileSelector.getTilesFromSelection(levelEditor.Level.Physmap, LEVEL_TILEAMOUNT_WIDTH);
            } else {
                tileList = new List<TileSelection<byte>>();
                byte item = currentPhysmapTile;
                tileList.Add(new TileSelection<byte>(new Point(0, 0), item));
            }
            return tileList;
        }

        private void drawAndSetTiles(Point mouseCoords, bool drawHighlight = true)
        {
            clearSelectedAreaInLevelEditor(mouseCoords);
            if (checkBox_field_show.Checked) {
                if (radioButton_field_edit.Checked)
                    setTilemapTilesInLevelEditor(mouseCoords);
                drawTilemapTilesInLevelEditor(mouseCoords);
            }
            if (checkBox_physmap_show.Checked) {
                if (radioButton_physmap_edit.Checked)
                    setPhysmapTilesInLevelEditor(mouseCoords);
                drawPhysmapTilesInLevelEditor(mouseCoords);
            }

            if (checkBox_grid_show.Checked)
                drawGridTilesInLevelEditor(mouseCoords);
            if ((tilemapTileSelector.Selected) && (drawHighlight))
                highlightSelectedTilesInLevelEditor();
            invalidateTilesInLevelEditor(mouseCoords);
        }

        private void setTilemapTilesInLevelEditor(Point mouseCoords)
        {
            var tileList = getSelectedTilemapTiles();
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

        private void setPhysmapTilesInLevelEditor(Point mouseCoords)
        {
            var tileList = getSelectedPhysmapTiles();
            Point tileCoords = coordConverterLevel.getTileCoordsFromMouseCoords(mouseCoords);
            Point placementCoords = new Point();
            foreach (var item in tileList) {
                placementCoords.X = tileCoords.X + item.tileCoords.X;
                placementCoords.Y = tileCoords.Y + item.tileCoords.Y;
                if ((placementCoords.X < LEVEL_TILEAMOUNT_WIDTH) && (placementCoords.Y < LEVEL_TILEAMOUNT_WIDTH)) {
                    int tileNum = coordConverterLevel.getTileNumberFromTileCoords(placementCoords);
                    if (item.tile > 0) {
                        levelEditor.setTileInPhysmap(tileNum, item.tile);
                    } else {
                        if (tilemapTileSelector.Selected == false) {
                            levelEditor.setTileInPhysmap(tileNum, 0);
                        }
                    }
                }
            }
        }

        private void drawTilemapTilesInLevelEditor(Point mouseCoords)
        {
            var tileList = getSelectedTilemapTiles();
            Point tileCoords = coordConverterLevel.getTileCoordsFromMouseCoords(mouseCoords);
            using (Graphics g = Graphics.FromImage(bitmapLevel)) {
                Point tempCoord = new Point();
                foreach (var item in tileList) {
                    tempCoord.X = tileCoords.X + item.tileCoords.X;
                    tempCoord.Y = tileCoords.Y + item.tileCoords.Y;
                    Point alignedCoords = coordConverterLevel.getMouseCoordsFromTileCoords(tempCoord);
                    int index = tempCoord.Y * LEVEL_TILEAMOUNT_WIDTH + tempCoord.X;
                    if (index < Level.LEVEL_TILE_AMOUNT) {
                        TilemapTile t = levelEditor.Level.Tilemap[index];
                        int bankTileNum = t.Bank * 256 + t.Tile;
                        if (bankTileNum > 0) {
                            byte bankPaletteNumber = (byte)(levelEditor.Level.PaletteIndex[t.Palette] - 1);
                            TileDrawer.clearTileOnCanvas(g, fillBrush, alignedCoords.X, alignedCoords.Y);
                            Bitmap tileImg = levelEditor.LevelBank.getTileImage(bankTileNum,
                                                                                bankPaletteNumber,
                                                                                TileDrawer.TILE_WIDTH);
                            TileDrawer.drawTileOnCanvas(tileImg,
                                                        g,
                                                        alignedCoords.X,
                                                        alignedCoords.Y,
                                                        t.VFlip,
                                                        t.HFlip);
                        }
                    }
                }
            }
        }

        private void drawPhysmapTilesInLevelEditor(Point mouseCoords)
        {
            var tileList = getSelectedPhysmapTiles();
            Point tileCoords = coordConverterLevel.getTileCoordsFromMouseCoords(mouseCoords);
            using (Graphics g = Graphics.FromImage(bitmapLevel)) {
                Point tempCoord = new Point();
                foreach (var item in tileList) {
                    tempCoord.X = tileCoords.X + item.tileCoords.X;
                    tempCoord.Y = tileCoords.Y + item.tileCoords.Y;
                    Point destCoords = coordConverterLevel.getMouseCoordsFromTileCoords(tempCoord);
                    int index = tempCoord.Y * LEVEL_TILEAMOUNT_WIDTH + tempCoord.X;
                    if (index < Level.LEVEL_TILE_AMOUNT) {
                        byte tileNum = levelEditor.Level.Physmap[index];
                        if (tileNum > 0) {
                            Point srcCoords = coordConverterPhysmap.getMouseCoordsFromTileNumber(tileNum);
                            if (checkBox_field_show.Checked == false)
                                TileDrawer.clearTileOnCanvas(g, fillBrush, destCoords.X, destCoords.Y);
                            TileDrawer.drawTileFromImageOnCanvas(bitmapPhysTileset, g, srcCoords, destCoords);
                        }
                    }
                }
            }
        }

        private void drawGridTilesInLevelEditor(Point mouseCoords)
        {
            var tileList = getSelectedTilemapTiles();
            Point tileCoords = coordConverterLevel.getTileCoordsFromMouseCoords(mouseCoords);
            using (Graphics g = Graphics.FromImage(bitmapLevel)) {
                Point tempCoord = new Point();
                foreach (var item in tileList) {
                    tempCoord.X = tileCoords.X + item.tileCoords.X;
                    tempCoord.Y = tileCoords.Y + item.tileCoords.Y;
                    Point destCoords = coordConverterLevel.getMouseCoordsFromTileCoords(tempCoord);
                    int index = tempCoord.Y * LEVEL_TILEAMOUNT_WIDTH + tempCoord.X;
                    if (index < Level.LEVEL_TILE_AMOUNT)
                        TileDrawer.drawGridCellOnCanvas(g, destCoords.X, destCoords.Y);
                }
            }
        }

        private void invalidateTilesInLevelEditor(Point mouseCoords)
        {
            var tileList = getSelectedTilemapTiles();
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

        private void highlightSelectedTilesInLevelEditor(bool invalidate = false)
        {
            if (tilemapTileSelector.Selected) {
                Rectangle rect = coordConverterLevel.getMouseCoordsFromRectCoords(tilemapTileSelector.TileCoords);
                using (Graphics g = Graphics.FromImage(bitmapLevel)) {
                    Point point = new Point(rect.X, rect.Y);
                    g.FillRectangle(highlightBrush, rect);
                    if (invalidate)
                        invalidateTilesInLevelEditor(point);
                }
            }
        }

        private void clearSelectedAreaInLevelEditor(Point mouseCoords)
        {
            var tileList = getSelectedTilemapTiles();
            Point tileCoords = coordConverterLevel.getTileCoordsFromMouseCoords(mouseCoords);
            using (Graphics g = Graphics.FromImage(bitmapLevel)) {
                Point tempCoord = new Point();
                foreach (var item in tileList) {
                    tempCoord.X = tileCoords.X + item.tileCoords.X;
                    tempCoord.Y = tileCoords.Y + item.tileCoords.Y;
                    Point alignedCoords = coordConverterLevel.getMouseCoordsFromTileCoords(tempCoord);
                    TileDrawer.clearTileOnCanvas(g, fillBrush, alignedCoords.X, alignedCoords.Y);
                }
            }
        }

        private void drawIndexTiles(bool highlight = true, bool grid = false)
        {
            int index = levelEditor.LevelHeader.graphicsBankIndex * 2;
            int tileAmount = levelEditor.Banks[index].tileAmount;
            for (int tileIndexNum = 0; tileIndexNum < tileAmount * 2; tileIndexNum++) {
                if (tileIndexNum == tileAmount)
                    index += 1;
                drawIndexTile(index, tileIndexNum, tileAmount, highlight, grid);
            }
        }

        private void drawIndexTile(int bankIndex, int tileIndexNum, int tileAmount, bool highlight = true, bool grid = false)
        {
            int i = 0;
            if (tileIndexNum >= tileAmount)
                i = 1;
            int tileNum = tileIndexNum - (i * tileAmount);
            
            Bitmap tileImg = levelEditor.Banks[bankIndex].getTileImage(tileNum, 15, coordConverterTileIndex.TileWidth);
            Point mouseCoords = coordConverterTileIndex.getMouseCoordsFromTileNumber(tileNum);
            using (Graphics g = Graphics.FromImage(bitmapTileIndex)) {
                TileDrawer.clearTileOnCanvas(g, fillBrush, mouseCoords.X, mouseCoords.Y + (i * 512));
                TileDrawer.drawTileOnCanvas(tileImg, g, mouseCoords.X, mouseCoords.Y + (i * 512), false, false);
            }
            if (highlight)
                if (selectedTileIndices[tileIndexNum])
                    highlightIndexTile(tileIndexNum);
            if (grid)
                drawGridTileOnIndexTile(tileIndexNum);
        }

        private void highlightIndexTile(int tileIndexNum)
        {
            Point mouseCoords = coordConverterTileIndex.getMouseCoordsFromTileNumber(tileIndexNum);
            int tileWidth = coordConverterTileIndex.TileWidth;
            Rectangle rect = new Rectangle(mouseCoords, new Size(tileWidth, tileWidth));
            using (Graphics g = Graphics.FromImage(bitmapTileIndex)) {
                Point point = new Point(rect.X, rect.Y);
                g.FillRectangle(highlightBrush, rect);
            }
        }

        private void drawGridTileOnIndexTile(int tileIndexNum)
        {
            Point mouseCoords = coordConverterTileIndex.getMouseCoordsFromTileNumber(tileIndexNum);
            using (Graphics g = Graphics.FromImage(bitmapTileIndex))
                TileDrawer.drawGridCellOnCanvas(g, mouseCoords.X, mouseCoords.Y);
        }

        private void invalidateIndexTile(Point alignedMouseCoords)
        {
            pictureBox_indexTiles.Invalidate(new Rectangle(alignedMouseCoords.X, 
                                                           alignedMouseCoords.Y, 
                                                           TileDrawer.TILE_WIDTH, 
                                                           TileDrawer.TILE_WIDTH));
        }

        private void deselectTiles()
        {
            if (tilemapTileSelector.Selected) {
                Point tileCoords = new Point(tilemapTileSelector.TileCoords.X, tilemapTileSelector.TileCoords.Y);
                Point mouseCoords = coordConverterLevel.getMouseCoordsFromTileCoords(tileCoords);
                clearSelectedAreaInLevelEditor(mouseCoords);
                drawAndSetTiles(mouseCoords, false);
            }
            tilemapTileSelector.clearSelection();
            lastLevelTileSelected = -1;
        }

        private void updateImages(bool level, bool tileset, bool tilesetTile, bool physmap, bool physmapTile, bool tileIndex)
        {
            if (level)
                updateImage_Level();
            if (tileset)
                updateImage_Tileset();
            if (tilesetTile)
                updateImage_TilemapTile();
            if (physmap)
                updateImage_Physmap();
            if (physmapTile)
                updateImage_PhysmapTile();
            if (tileIndex)
                updateImage_TileIndex();
        }

        private void updateImage_Tileset()
        {
            if (isLevelLoaded) {
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
            if (isLevelLoaded) {
                if (checkBox_bytes_show.Checked) {
                    bitmapPhysTileset = (Bitmap)Riverback.Properties.Resources.physmap_bytes;
                    bitmapPhysTileset.SetResolution(IMAGE_DPI, IMAGE_DPI);
                    Bitmap bmp = (Bitmap)Riverback.Properties.Resources.physmap_bytes2;
                    bmp.SetResolution(IMAGE_DPI, IMAGE_DPI);
                    pictureBox_phystiles.Image = bmp;
                } else {
                    bitmapPhysTileset = (Bitmap)Riverback.Properties.Resources.physmap;
                    bitmapPhysTileset.SetResolution(IMAGE_DPI, IMAGE_DPI);
                    Bitmap bmp = (Bitmap)Riverback.Properties.Resources.physmap2;
                    bmp.SetResolution(IMAGE_DPI, IMAGE_DPI);
                    pictureBox_phystiles.Image = bmp;
                }
                if (checkBox_grid_show.Checked)
                    using (Graphics g = Graphics.FromImage(pictureBox_phystiles.Image))
                        g.DrawImage(Riverback.Properties.Resources.gridtile16_256x256, 0, 0);
                pictureBox_phystiles.Invalidate();
            }
        }

        private void updateImage_TileIndex()
        {
            if (isLevelLoaded) {
                using (Graphics g = Graphics.FromImage(bitmapTileIndex))
                    g.Clear(fillColor);
                drawIndexTiles(true, checkBox_grid_show.Checked);
                pictureBox_indexTiles.Invalidate();
            }
        }

        private void updateImage_TilemapTile()
        {
            if (isLevelLoaded) {
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
            if (isLevelLoaded) {
                Point alignedMouseCoords = coordConverterPhysmap.getMouseCoordsFromTileNumber(currentPhysmapTile);
                using (Graphics g = Graphics.FromImage(bitmapPhysTile)) {
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    g.PixelOffsetMode = PixelOffsetMode.Half;
                    g.Clear(fillColor);
                    TileDrawer.drawTileFromImageOnCanvas(bitmapPhysTileset,
                                                         g,
                                                         alignedMouseCoords,
                                                         new Point(0, 0),
                                                         TILE_SELECTOR_SCALE);
                    pictureBox_phystile.Invalidate();
                }
            }
        }

        private void updateImage_Level()
        {
            if (isLevelLoaded) {
                using (Graphics g = Graphics.FromImage(bitmapLevel)) {
                    g.Clear(fillColor);
                    TileDrawer.drawLevelOnCanvas(g, 
                                                 bitmapPhysTileset, 
                                                 levelEditor.Level, 
                                                 levelEditor.LevelBank, 
                                                 LEVEL_TILEAMOUNT_WIDTH, 
                                                 checkBox_field_show.Checked, 
                                                 checkBox_physmap_show.Checked);
                    if (checkBox_grid_show.Checked)
                        g.DrawImage(Riverback.Properties.Resources.gridtile8_512x512, 0, 0);
                    if (tilemapTileSelector.Selected)
                        highlightSelectedTilesInLevelEditor();
                    pictureBox_level.Invalidate();
                }
            }
        }

        private void updateTextBox_IndexTiles()
        {
            if (isLevelLoaded) {
                textBox_tilesremaining.Text = String.Format("{0}", indexTilesRemaining);
                textBox_tilesremaining.Invalidate();
            }
        }
    }
}

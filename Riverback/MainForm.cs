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
        private const int LEVEL_TILESET_TILEAMOUNT_HEIGHT = 32;
        private const int LEVEL_PHYSMAP_TILEAMOUNT_WIDTH = 16;
        private const int LEVEL_TILEINDEX_TILEAMOUNT_WIDTH = 16;
        private const int LEVEL_TILEINDEX_TILEAMOUNT_HEIGHT = 128;
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
        private int lastIndexTileSelected;
        private byte bankPaletteNum;
        private bool[] selectedTileIndices;
        private LevelHeader selectedLevelHeader;
        private byte[] selectedPaletteIndex;

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
            coordConverterLevel = new CoordinateConverter(LEVEL_TILEAMOUNT_WIDTH, 
                                                          LEVEL_TILEAMOUNT_WIDTH, 
                                                          TileDrawer.TILE_WIDTH);
            coordConverterTileset = new CoordinateConverter(LEVEL_TILESET_TILEAMOUNT_WIDTH, 
                                                            LEVEL_TILESET_TILEAMOUNT_HEIGHT, 
                                                            (int)TILEMAP_SCALE * TileDrawer.TILE_WIDTH);
            coordConverterPhysmap = new CoordinateConverter(LEVEL_PHYSMAP_TILEAMOUNT_WIDTH, 
                                                            LEVEL_PHYSMAP_TILEAMOUNT_WIDTH, 
                                                            TileDrawer.TILE_WIDTH);
            coordConverterTileIndex = new CoordinateConverter(LEVEL_TILEINDEX_TILEAMOUNT_WIDTH, 
                                                              LEVEL_TILEINDEX_TILEAMOUNT_HEIGHT, 
                                                              (int)TILEMAP_SCALE * TileDrawer.TILE_WIDTH);
			tilemapTileSelector = new TileSelector(coordConverterLevel);
			selectedTileIndices = new bool[2048];
            selectedLevelHeader = new LevelHeader();
            selectedPaletteIndex = new byte[Level.LEVEL_PALETTE_INDEX_AMOUNT];
            lastLevelTileSelected = -1;
            lastIndexTileSelected = -1;
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
            bitmapPhysTileset = Riverback.Properties.Resources.physmap;
            bitmapPhysTileset.SetResolution(IMAGE_DPI, IMAGE_DPI);
            bitmapPhysTileset2 = Riverback.Properties.Resources.physmap2;
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Left | Keys.Control)) {
                if (numericUpDown_tilePalette.Value > numericUpDown_tilePalette.Minimum) {
                    numericUpDown_tilePalette.Value -= 1;
                    numericUpDown_tilePalette.Invalidate();
                    return true;
                }
            } else if (keyData == (Keys.Right | Keys.Control)) {
                if (numericUpDown_tilePalette.Value < numericUpDown_tilePalette.Maximum) {
                    numericUpDown_tilePalette.Value += 1;
                    numericUpDown_tilePalette.Invalidate();
                    return true;
                }
            } else if (keyData == Keys.Left) {
                if (isLevelLoaded) {
                    if (radioButton_field_edit.Checked) {
                        if (currentTilesetTile > 0) {
                            currentTilesetTile -= 1;
                            updateImages(false, false, true, false, false, false);
                        }
                    } else {
                        if (currentPhysmapTile > 0) {
                            currentPhysmapTile -= 1;
                            updateImages(false, false, false, false, true, false);
                        }
                    }
                    return true;
                }
            } else if (keyData == Keys.Right) {
                if (isLevelLoaded) {
                    if (radioButton_field_edit.Checked) {
                        if (currentTilesetTile < levelEditor.LevelBank.tileAmount - 1) {
                            currentTilesetTile += 1;
                            updateImages(false, false, true, false, false, false);
                        }
                    } else {
                        if (currentPhysmapTile < PHYSTILE_TILEAMOUNT - 1) {
                            currentPhysmapTile += 1;
                            updateImages(false, false, false, false, true, false);
                        }
                    }
                    return true;
                }
            } else if (keyData == Keys.Up) {
                if (isLevelLoaded) {
                    if (radioButton_field_edit.Checked) {
                        if (currentTilesetTile >= LEVEL_TILESET_TILEAMOUNT_WIDTH) {
                            currentTilesetTile -= LEVEL_TILESET_TILEAMOUNT_WIDTH;
                            updateImages(false, false, true, false, false, false);
                        }
                    } else {
                        if (currentPhysmapTile >= LEVEL_PHYSMAP_TILEAMOUNT_WIDTH) {
                            currentPhysmapTile -= LEVEL_PHYSMAP_TILEAMOUNT_WIDTH;
                            updateImages(false, false, false, false, true, false);
                        }
                    }
                    return true;
                }
            } else if (keyData == Keys.Down) {
                if (isLevelLoaded) {
                    if (radioButton_field_edit.Checked) {
                        if (currentTilesetTile < levelEditor.LevelBank.tileAmount - LEVEL_PHYSMAP_TILEAMOUNT_WIDTH) {
                            currentTilesetTile += LEVEL_TILESET_TILEAMOUNT_WIDTH;
                            updateImages(false, false, true, false, false, false);
                        }
                    } else {
                        if (currentPhysmapTile < PHYSTILE_TILEAMOUNT - LEVEL_PHYSMAP_TILEAMOUNT_WIDTH) {
                            currentPhysmapTile += LEVEL_PHYSMAP_TILEAMOUNT_WIDTH;
                            updateImages(false, false, false, false, true, false);
                        }
                    }
                    return true;
                }
            } else if (keyData == Keys.D1) {
                if (checkBox_field_show.Checked)
                    checkBox_field_show.Checked = false;
                else
                    checkBox_field_show.Checked = true;
                return true;
            } else if (keyData == Keys.D2) {
                if (checkBox_physmap_show.Checked)
                    checkBox_physmap_show.Checked = false;
                else
                    checkBox_physmap_show.Checked = true;
                return true;
            } else if (keyData == Keys.D3) {
                if (checkBox_bytes_show.Checked)
                    checkBox_bytes_show.Checked = false;
                else
                    checkBox_bytes_show.Checked = true;
                return true;
            } else if (keyData == Keys.D4) {
                if (checkBox_grid_show.Checked)
                    checkBox_grid_show.Checked = false;
                else
                    checkBox_grid_show.Checked = true;
                return true;
            } else if (keyData == Keys.V) {
                if (checkBox_vflip.Checked)
                    checkBox_vflip.Checked = false;
                else
                    checkBox_vflip.Checked = true;
                return true;
            } else if (keyData == Keys.H) {
                if (checkBox_hflip.Checked)
                    checkBox_hflip.Checked = false;
                else
                    checkBox_hflip.Checked = true;
                return true;
            } else if (keyData == Keys.O) {
                if (checkBox_priority.Checked)
                    checkBox_priority.Checked = false;
                else
                    checkBox_priority.Checked = true;
                return true;
            } else if (keyData == Keys.D) {
                if (isLevelLoaded) {
                    if (tilemapTileSelector.Selected) {
                        deselectTiles();
                        return true;
                    }
                }
            } else if (keyData == Keys.Tab) {
                if (radioButton_field_edit.Checked)
                    radioButton_physmap_edit.Checked = true;
                else
                    radioButton_field_edit.Checked = true;
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void MainMenu_FileOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                if (openFileDialog.CheckFileExists) {
                    isLevelLoaded = false;
                    byte[] openedData = File.ReadAllBytes(openFileDialog.FileName);

                    if (openedData.Length >= 0x8000) {
                        byte[] str = Encoding.ASCII.GetBytes("UMIHARAKAWASE");
                        byte[] checksum = new byte[str.Length];
                        Array.ConstrainedCopy(openedData, 0x7FC0, checksum, 0, str.Length);
                        if (str.SequenceEqual(checksum) == false) {
                            MessageBox.Show("The file you opened is not a valid headerless Umihara Kawase ROM.", 
                                            "Error", 
                                            MessageBoxButtons.OK, 
                                            MessageBoxIcon.Exclamation);
                            return;
                        }
                    } else {
                        MessageBox.Show("The file you opened is not a valid headerless Umihara Kawase ROM.",
                                            "Error",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation);
                        return;
                    }

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
                        writer.writeLevel(levelEditor.Level, levelEditor.LevelHeader);
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

        private void clearLevelTilemapTilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isLevelLoaded) {
                for (int tileNum = 0; tileNum < Level.LEVEL_TILE_AMOUNT; tileNum++ ) {
                    levelEditor.setTileInTilemap(tileNum, 0, false, false, false, 0);
                }
                updateImages(true, false, false, false, false, false);
            }
        }

        private void clearLevelPhysmapTilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isLevelLoaded) {
                for (int tileNum = 0; tileNum < Level.LEVEL_TILE_AMOUNT; tileNum++) {
                    levelEditor.setTileInPhysmap(tileNum, 0);
                }
                updateImages(true, false, false, false, false, false);
            }
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
                updateImages(false, true, true, false, false, true);
            }
        }

        private void radioButton_field_edit_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_field_edit.Checked)
                checkBox_field_show.Checked = true;
        }

        private void radioButton_physmap_edit_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_physmap_edit.Checked)
                checkBox_physmap_show.Checked = true;
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
				int[] tileOffsetList = new int[levelEditor.Level.TileIndexAmount];
				List<int> removedTiles = new List<int>();
				int offset = 0;
				int tileNum = 0;
				for (int i = 0; i < levelEditor.Level.TileIndex.Count; i++) {
					if (levelEditor.Level.TileIndex[i] != selectedTileIndices[i]) {
						if (selectedTileIndices[i]) {
							offset++;
						} else {
							offset--;
							removedTiles.Add(tileNum);
						}
					}
					if (levelEditor.Level.TileIndex[i]) {
						tileOffsetList[tileNum] = offset;
						tileNum++;
					}
				}
				levelEditor.removeInvalidTiles(removedTiles);
				for (int i = 0; i < Level.LEVEL_TILE_AMOUNT; i++) {
					int tileValue = levelEditor.Level.Tilemap[i].Bank * 256 + levelEditor.Level.Tilemap[i].Tile;
					levelEditor.setTileInTilemap(i, tileValue + tileOffsetList[tileValue]);
				}

                levelEditor.Level.TileIndex = selectedTileIndices.ToList();
                levelEditor.updateLevelBank();
                currentTilesetTile = 0;
                updateImages(true, true, true, false, false, true);
            }
        }

        private void button_clearindices_Click(object sender, EventArgs e)
        {
            if (isLevelLoaded) {
                selectedTileIndices[0] = true;
                for (int index = 1; index < selectedTileIndices.Length; index++) {
                    selectedTileIndices[index] = false;
                }
                indexTilesRemaining = INDEXTILES_MAX - 1;
                updateImages(false, false, false, false, false, true);
                updateTextBox_IndexTiles();
            }
        }

        private void button_applyheader_Click(object sender, EventArgs e)
        {
            if (isLevelLoaded) {
                updateLevelHeaderValues();
                levelEditor.updateLevelHeader(selectedLevelHeader);
                levelEditor.updateLevelBank();
                updateImages(true, true, true, true, true, true);
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
                    radioButton_field_edit.Select();
                    checkBox_field_show.Checked = true;
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
                    radioButton_physmap_edit.Select();
                    checkBox_physmap_show.Checked = true;
                    updateImages(false, false, false, false, true, false);
                }
            }
        }

        private void pictureBox_indexTiles_MouseDown(object sender, MouseEventArgs e)
        {
            setIndexTilesFromMouseClick(e);
        }

        private void pictureBox_indexTiles_MouseMove(object sender, MouseEventArgs e)
        {
            setIndexTilesFromMouseClick(e);
        }

        private void pictureBox_indexTiles_MouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Left) || (e.Button == MouseButtons.Right))
                lastIndexTileSelected = -1;
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
                if (writer.importLevel(openedData, levelEditor.Level, levelEditor.LevelHeader) == false) {
                    isLevelLoaded = true;
                    return;
                }
            }
            levelEditor.updateGraphicsBanks(romdata);
            levelEditor.updateLevelBank();
            levelEditor.Level.TileIndex.CopyTo(selectedTileIndices);
            isLevelLoaded = true;
            indexTilesRemaining = INDEXTILES_MAX - levelEditor.LevelBank.tileAmount;
            updateTextBox_IndexTiles();
            selectedLevelHeader = new LevelHeader(levelEditor.LevelHeader);
            selectedPaletteIndex = (byte[])levelEditor.Level.PaletteIndex.Clone();
            bankPaletteNum = (byte)(levelEditor.Level.PaletteIndex[(int)numericUpDown_tilePalette.Value] - 1);
            updateLevelHeaderControls();
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
            if ((mouseCoords.X >= 0) && (mouseCoords.Y >= 0)) {
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

        private void setIndexTilesFromMouseClick(MouseEventArgs e)
        {
            if (((e.Button == MouseButtons.Left) || (e.Button == MouseButtons.Right)) && (isLevelLoaded)) {
                Point mouseCoords = new Point(e.X, e.Y);
                int tileNum = coordConverterTileIndex.getTileNumberFromMouseCoords(mouseCoords);
                if ((tileNum > 0) && (tileNum != lastIndexTileSelected)) {
                    int index = levelEditor.LevelHeader.graphicsBankIndex * 2;
                    int tileAmount = levelEditor.Banks[index].tileAmount;
                    if (tileNum > tileAmount)
                        index += 1;
                    if (e.Button == MouseButtons.Left) {
                        if ((indexTilesRemaining < INDEXTILES_MAX - 1) && (selectedTileIndices[tileNum] == true)) {
                            selectedTileIndices[tileNum] = false;
                            indexTilesRemaining += 1;
                            lastIndexTileSelected = tileNum;
                        }
                    } else {
                        if ((indexTilesRemaining > 0) && (selectedTileIndices[tileNum] == false)) {
                            selectedTileIndices[tileNum] = true;
                            indexTilesRemaining -= 1;
                            lastIndexTileSelected = tileNum;
                        }
                    }
                    updateTextBox_IndexTiles();
                    Graphics g = Graphics.FromImage(bitmapTileIndex);
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    g.PixelOffsetMode = PixelOffsetMode.Half;
                    drawIndexTile(g, index, tileNum, tileAmount, selectedTileIndices[tileNum], checkBox_grid_show.Checked);
                    Point alignedMouseCoords = coordConverterTileIndex.getMouseCoordsFromTileNumber(tileNum);
                    invalidateIndexTile(alignedMouseCoords);
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
            if ((invalidateBottomRightPoint.X >= 0) && (invalidateBottomRightPoint.Y >= 0)) {
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

        private void drawIndexTiles(Graphics g, bool highlight = true, bool grid = false)
        {
            int index = levelEditor.LevelHeader.graphicsBankIndex * 2;
            int tileAmount = levelEditor.Banks[index].tileAmount;
            for (int tileIndexNum = 0; tileIndexNum < tileAmount * 2; tileIndexNum++) {
                if (tileIndexNum == tileAmount)
                    index += 1;
                drawIndexTile(g, index, tileIndexNum, tileAmount, highlight, grid);
            }
        }

        private void drawIndexTile(Graphics g, int bankIndex, int tileIndexNum, int tileAmount, bool highlight = true, bool grid = false)
        {
            int i = 0;
            if (tileIndexNum >= tileAmount)
                i = 1;
            int tileNum = tileIndexNum - (i * tileAmount);

            byte bankPaletteNumber = (byte)(levelEditor.Level.PaletteIndex[(int)numericUpDown_tilePalette.Value] - 1);
            Bitmap tileImg = levelEditor.Banks[bankIndex].getTileImage(tileNum, bankPaletteNumber, TileDrawer.TILE_WIDTH);
            Point mouseCoords = coordConverterTileIndex.getMouseCoordsFromTileNumber(tileNum);
            TileDrawer.clearTileOnCanvas(g, fillBrush, mouseCoords.X, mouseCoords.Y + (i * 1024), TILEMAP_SCALE);
            TileDrawer.drawTileOnCanvas(tileImg, g, mouseCoords.X, mouseCoords.Y + (i * 1024), false, false, TILEMAP_SCALE);
            if (highlight)
                if (selectedTileIndices[tileIndexNum])
                    highlightIndexTile(g, tileIndexNum);
            if (grid)
                drawGridTileOnIndexTile(g, tileIndexNum);
        }

        private void highlightIndexTile(Graphics g, int tileIndexNum)
        {
            Point mouseCoords = coordConverterTileIndex.getMouseCoordsFromTileNumber(tileIndexNum);
            int tileWidth = coordConverterTileIndex.TileWidth;
            Rectangle rect = new Rectangle(mouseCoords, new Size(tileWidth, tileWidth));
            Point point = new Point(rect.X, rect.Y);
            g.FillRectangle(highlightBrush, rect);
        }

        private void drawGridTileOnIndexTile(Graphics g, int tileIndexNum)
        {
            Point mouseCoords = coordConverterTileIndex.getMouseCoordsFromTileNumber(tileIndexNum);
            TileDrawer.drawGridCellOnCanvas(g, mouseCoords.X, mouseCoords.Y, TILEMAP_SCALE);
        }

        private void invalidateIndexTile(Point alignedMouseCoords)
        {
            pictureBox_indexTiles.Invalidate(new Rectangle(alignedMouseCoords.X, 
                                                           alignedMouseCoords.Y, 
                                                           TileDrawer.TILE_WIDTH * (int)TILEMAP_SCALE, 
                                                           TileDrawer.TILE_WIDTH * (int)TILEMAP_SCALE));
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

        private void updateLevelHeaderValues()
        {
            if (isLevelLoaded) {
                //selectedLevelHeader.headerNumber = (byte)numericUpDown_headernumber.Value;
                //selectedLevelHeader.headerPointerAddress = (int)numericUpDown_headerpointer.Value;
                //selectedLevelHeader.headerAddress = (int)numericUpDown_headeraddress.Value;
                //selectedLevelHeader.levelPointer = (int)numericUpDown_levelpointer.Value;
                selectedLevelHeader.graphicsBankIndex = (byte)numericUpDown_graphicsbankindex.Value;
                selectedLevelHeader.fieldNumber = (byte)numericUpDown_fieldnumber.Value;
                selectedLevelHeader.musicSelect = (byte)numericUpDown_musicselect.Value;
                selectedLevelHeader.enemyType[0] = (byte)numericUpDown_enemytype1.Value;
                selectedLevelHeader.enemyType[1] = (byte)numericUpDown_enemytype2.Value;
                selectedLevelHeader.enemyType[2] = (byte)numericUpDown_enemytype3.Value;
                selectedLevelHeader.enemyType[3] = (byte)numericUpDown_enemytype4.Value;
                selectedLevelHeader.enemyType[4] = (byte)numericUpDown_enemytype5.Value;
                selectedLevelHeader.enemyType[5] = (byte)numericUpDown_enemytype6.Value;
                selectedLevelHeader.spawnRates[0] = (byte)numericUpDown_spawnrate1.Value;
                selectedLevelHeader.spawnRates[1] = (byte)numericUpDown_spawnrate2.Value;
                selectedLevelHeader.spawnRates[2] = (byte)numericUpDown_spawnrate3.Value;
                selectedLevelHeader.spawnRates[3] = (byte)numericUpDown_spawnrate4.Value;
                selectedLevelHeader.spawnRates[4] = (byte)numericUpDown_spawnrate5.Value;
                selectedLevelHeader.spawnRates[5] = (byte)numericUpDown_spawnrate6.Value;
                selectedLevelHeader.spawnRates[6] = (byte)numericUpDown_spawnrate7.Value;
                selectedLevelHeader.spawnRates[7] = (byte)numericUpDown_spawnrate8.Value;
                selectedLevelHeader.objectType[0] = (byte)numericUpDown_objecttype1.Value;
                selectedLevelHeader.objectType[1] = (byte)numericUpDown_objecttype2.Value;
                selectedLevelHeader.objectType[2] = (byte)numericUpDown_objecttype3.Value;
                selectedLevelHeader.objectType[3] = (byte)numericUpDown_objecttype4.Value;
                selectedLevelHeader.objectType[4] = (byte)numericUpDown_objecttype5.Value;
                selectedLevelHeader.objectType[5] = (byte)numericUpDown_objecttype6.Value;
                selectedLevelHeader.objectType[6] = (byte)numericUpDown_objecttype7.Value;
                selectedLevelHeader.waterHeight = (byte)numericUpDown_waterheight.Value;

                selectedLevelHeader.waterType = 2;
                if (checkBox_wavywater.Checked)
                    selectedLevelHeader.waterType = 3;
                
                selectedLevelHeader.levelTimer = (int)numericUpDown_leveltimer.Value;
                selectedLevelHeader.doorExits[0] = (byte)numericUpDown_doorexit1.Value;
                selectedLevelHeader.doorExits[1] = (byte)numericUpDown_doorexit2.Value;
                selectedLevelHeader.doorExits[2] = (byte)numericUpDown_doorexit3.Value;
                selectedLevelHeader.doorExits[3] = (byte)numericUpDown_doorexit4.Value;
                levelEditor.Level.PaletteIndex[2] = (byte)numericUpDown_paletteindices1.Value;
                levelEditor.Level.PaletteIndex[3] = (byte)numericUpDown_paletteindices2.Value;
                levelEditor.Level.PaletteIndex[4] = (byte)numericUpDown_paletteindices3.Value;
                levelEditor.Level.PaletteIndex[5] = (byte)numericUpDown_paletteindices4.Value;
                levelEditor.Level.PaletteIndex[6] = (byte)numericUpDown_paletteindices5.Value;
                levelEditor.Level.PaletteIndex[7] = (byte)numericUpDown_paletteindices6.Value;
            }
        }

        private void updateLevelHeaderControls()
        {
            if (isLevelLoaded) {
                textBox_headernumber.Text = String.Format("{0}", selectedLevelHeader.headerNumber);
                textBox_headernumber.Invalidate();
                textBox_headerpointer.Text = String.Format("{0:X}", selectedLevelHeader.headerPointerAddress);
                textBox_headerpointer.Invalidate();
                textBox_headeraddress.Text = String.Format("{0:X}", selectedLevelHeader.headerAddress);
                textBox_headeraddress.Invalidate();
                textBox_levelpointer.Text = String.Format("{0:X}", selectedLevelHeader.levelPointer);
                textBox_levelpointer.Invalidate();
                numericUpDown_graphicsbankindex.Value = selectedLevelHeader.graphicsBankIndex;
                numericUpDown_fieldnumber.Value = selectedLevelHeader.fieldNumber;
                numericUpDown_musicselect.Value = selectedLevelHeader.musicSelect;
                numericUpDown_enemytype1.Value = selectedLevelHeader.enemyType[0];
                numericUpDown_enemytype2.Value = selectedLevelHeader.enemyType[1];
                numericUpDown_enemytype3.Value = selectedLevelHeader.enemyType[2];
                numericUpDown_enemytype4.Value = selectedLevelHeader.enemyType[3];
                numericUpDown_enemytype5.Value = selectedLevelHeader.enemyType[4];
                numericUpDown_enemytype6.Value = selectedLevelHeader.enemyType[5];
                numericUpDown_spawnrate1.Value = selectedLevelHeader.spawnRates[0];
                numericUpDown_spawnrate2.Value = selectedLevelHeader.spawnRates[1];
                numericUpDown_spawnrate3.Value = selectedLevelHeader.spawnRates[2];
                numericUpDown_spawnrate4.Value = selectedLevelHeader.spawnRates[3];
                numericUpDown_spawnrate5.Value = selectedLevelHeader.spawnRates[4];
                numericUpDown_spawnrate6.Value = selectedLevelHeader.spawnRates[5];
                numericUpDown_spawnrate7.Value = selectedLevelHeader.spawnRates[6];
                numericUpDown_spawnrate8.Value = selectedLevelHeader.spawnRates[7];
                numericUpDown_objecttype1.Value = selectedLevelHeader.objectType[0];
                numericUpDown_objecttype2.Value = selectedLevelHeader.objectType[1];
                numericUpDown_objecttype3.Value = selectedLevelHeader.objectType[2];
                numericUpDown_objecttype4.Value = selectedLevelHeader.objectType[3];
                numericUpDown_objecttype5.Value = selectedLevelHeader.objectType[4];
                numericUpDown_objecttype6.Value = selectedLevelHeader.objectType[5];
                numericUpDown_objecttype7.Value = selectedLevelHeader.objectType[6];
                numericUpDown_waterheight.Value = selectedLevelHeader.waterHeight;

                checkBox_wavywater.Checked = false;
                if (selectedLevelHeader.waterType == 3)
                    checkBox_wavywater.Checked = true;
                
                numericUpDown_leveltimer.Value = selectedLevelHeader.levelTimer;
                numericUpDown_doorexit1.Value = selectedLevelHeader.doorExits[0];
                numericUpDown_doorexit2.Value = selectedLevelHeader.doorExits[1];
                numericUpDown_doorexit3.Value = selectedLevelHeader.doorExits[2];
                numericUpDown_doorexit4.Value = selectedLevelHeader.doorExits[3];
                numericUpDown_paletteindices1.Value = levelEditor.Level.PaletteIndex[2];
                numericUpDown_paletteindices2.Value = levelEditor.Level.PaletteIndex[3];
                numericUpDown_paletteindices3.Value = levelEditor.Level.PaletteIndex[4];
                numericUpDown_paletteindices4.Value = levelEditor.Level.PaletteIndex[5];
                numericUpDown_paletteindices5.Value = levelEditor.Level.PaletteIndex[6];
                numericUpDown_paletteindices6.Value = levelEditor.Level.PaletteIndex[7];
            }
        }

        private void updateImages(bool level, bool tileset, bool tilesetTile, bool physmap, bool physmapTile, bool tileIndex)
        {
            if (tileIndex)
                updateImage_TileIndex();
            if (tileset)
                updateImage_Tileset();
            if (tilesetTile)
                updateImage_TilemapTile();
            if (physmap)
                updateImage_Physmap();
            if (physmapTile)
                updateImage_PhysmapTile();
            if (level)
                updateImage_Level();
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
                        g.DrawImage(Riverback.Properties.Resources.gridtile16_256x2048, 0, 0);
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
                using (Graphics g = Graphics.FromImage(bitmapTileIndex)) {
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    g.PixelOffsetMode = PixelOffsetMode.Half;
                    g.Clear(fillColor);
                    drawIndexTiles(g, true, checkBox_grid_show.Checked);
                }
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

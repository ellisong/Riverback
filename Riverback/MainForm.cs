using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Riverback.Properties;

namespace Riverback
{
    public partial class MainForm : Form
    {
        private const int LevelTileAmountWidth = 64;
        private const int LevelTilesetTileAmountWidth = 16;
        private const int LevelTilesetTileAmountHeight = 128;
        private const int LevelPhysmapTileAmountWidth = 16;
        private const int LevelTileindexTileAmountWidth = 16;
        private const int LevelTileindexTileAmountHeight = 128;
        private const int PhystileTileAmount = 256;
        private const float TilemapLevelScale = 2.0f;
        private const float TileSelectorScale = 4.0f;
        private const float TilemapScale = 2.0f;
        private const int ImageDpi = 72;
        private const int MaxBankTileAmount = 0x200;

        private System.Drawing.Color _fillColor;
        private Brush _fillBrush;
        private System.Drawing.Color _highlightColor;
        private Brush _highlightBrush;

        private readonly LevelEditor _levelEditor;
        private bool _isLevelLoaded;
        private byte[] _romdata;

        private readonly CoordinateConverter _coordConverterLevel;
        private readonly CoordinateConverter _coordConverterTileset;
        private readonly CoordinateConverter _coordConverterPhysmap;
        private readonly CoordinateConverter _coordConverterTileIndex;

        private readonly TileSelector _tilemapTileSelector;
        private int _currentTilesetTileIndex;
        private int _currentTilesetTileNum;
        private byte _currentPhysmapTile;
        private int _lastLevelTileSelected;
        private int _lastIndexTileSelected;
        private byte _bankPaletteNum;
        private readonly bool[] _selectedTileIndices;
        private LevelHeader _selectedLevelHeader;

        private Bitmap _bitmapTileset;
        private Bitmap _bitmapTilemapTile;
        private Bitmap _bitmapTileIndex;
        private Bitmap _bitmapPhysTileset;
        private Bitmap _bitmapPhysTile;
        private Bitmap _bitmapLevel;

        int _indexTilesRemaining;

        public MainForm()
        {
            InitializeComponent();
            _levelEditor = new LevelEditor();
            _coordConverterLevel = new CoordinateConverter(LevelTileAmountWidth, 
                                                          LevelTileAmountWidth, 
                                                          (int)TilemapLevelScale * TileDrawer.TileWidth);
            _coordConverterTileset = new CoordinateConverter(LevelTilesetTileAmountWidth, 
                                                            LevelTilesetTileAmountHeight, 
                                                            (int)TilemapScale * TileDrawer.TileWidth);
            _coordConverterPhysmap = new CoordinateConverter(LevelPhysmapTileAmountWidth, 
                                                            LevelPhysmapTileAmountWidth,
                                                            (int)TilemapLevelScale * TileDrawer.TileWidth);
            _coordConverterTileIndex = new CoordinateConverter(LevelTileindexTileAmountWidth, 
                                                              LevelTileindexTileAmountHeight, 
                                                              (int)TilemapScale * TileDrawer.TileWidth);
            _tilemapTileSelector = new TileSelector(_coordConverterLevel);
            _selectedTileIndices = new bool[2048];
            _selectedLevelHeader = new LevelHeader();
            _lastLevelTileSelected = -1;
            _lastIndexTileSelected = -1;
            _currentTilesetTileIndex = 0;
            _currentTilesetTileNum = 0;
            _currentPhysmapTile = 0;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _fillColor = System.Drawing.Color.DarkGray;
            _fillBrush = Brushes.DarkGray;
            _highlightColor = System.Drawing.Color.FromArgb(192, System.Drawing.Color.FloralWhite);
            _highlightBrush = new SolidBrush(_highlightColor);

            _bitmapTileset = new Bitmap(pictureBox_tileset.Width, pictureBox_tileset.Height);
            _bitmapTileset.SetResolution(ImageDpi, ImageDpi);
            pictureBox_tileset.Image = _bitmapTileset;
            _bitmapTilemapTile = new Bitmap(pictureBox_tilemaptile.Width, pictureBox_tilemaptile.Height);
            _bitmapTilemapTile.SetResolution(ImageDpi, ImageDpi);
            pictureBox_tilemaptile.Image = _bitmapTilemapTile;
            _bitmapTileIndex = new Bitmap(pictureBox_indexTiles.Width, pictureBox_indexTiles.Height);
            _bitmapTileIndex.SetResolution(ImageDpi, ImageDpi);
            pictureBox_indexTiles.Image = _bitmapTileIndex;
            _bitmapPhysTileset = Resources.physmap2;
            _bitmapPhysTileset.SetResolution(ImageDpi, ImageDpi);
            pictureBox_phystiles.Image = _bitmapPhysTileset;
            pictureBox_phystiles.Invalidate();
            _bitmapPhysTile = new Bitmap(pictureBox_phystile.Width, pictureBox_phystile.Height);
            _bitmapPhysTile.SetResolution(ImageDpi, ImageDpi);
            pictureBox_phystile.Image = _bitmapPhysTile;
            _bitmapLevel = new Bitmap(pictureBox_level.Width, pictureBox_level.Height);
            _bitmapLevel.SetResolution(ImageDpi, ImageDpi);
            pictureBox_level.Image = _bitmapLevel;
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
                if (_isLevelLoaded) {
                    if (radioButton_field_edit.Checked) {
                        if (_currentTilesetTileIndex > 0) {
                            SetCurrentTilesetTileByIndex(_currentTilesetTileIndex - 1);
                            UpdateImages(false, false, true, false, false, false);
                        }
                    } else {
                        if (_currentPhysmapTile > 0) {
                            _currentPhysmapTile -= 1;
                            UpdateImages(false, false, false, false, true, false);
                        }
                    }
                    return true;
                }
            } else if (keyData == Keys.Right) {
                if (_isLevelLoaded) {
                    if (radioButton_field_edit.Checked) {
                        if (_currentTilesetTileIndex < _levelEditor.Level.TileIndex.GetBankTileIndexSize() - 1) {
                            SetCurrentTilesetTileByIndex(_currentTilesetTileIndex + 1);
                            UpdateImages(false, false, true, false, false, false);
                        }
                    } else {
                        if (_currentPhysmapTile < PhystileTileAmount - 1) {
                            _currentPhysmapTile += 1;
                            UpdateImages(false, false, false, false, true, false);
                        }
                    }
                    return true;
                }
            } else if (keyData == Keys.Up) {
                if (_isLevelLoaded) {
                    if (radioButton_field_edit.Checked) {
                        SetCurrentTilesetTileByNum(_currentTilesetTileNum - LevelTilesetTileAmountWidth);
                        UpdateImages(false, false, true, false, false, false);
                    } else {
                        if (_currentPhysmapTile >= LevelPhysmapTileAmountWidth) {
                            _currentPhysmapTile -= LevelPhysmapTileAmountWidth;
                            UpdateImages(false, false, false, false, true, false);
                        }
                    }
                    return true;
                }
            } else if (keyData == Keys.Down) {
                if (_isLevelLoaded) {
                    if (radioButton_field_edit.Checked) {
                        SetCurrentTilesetTileByNum(_currentTilesetTileNum + LevelTilesetTileAmountWidth);
                        UpdateImages(false, false, true, false, false, false);
                    } else {
                        if (_currentPhysmapTile < PhystileTileAmount - LevelPhysmapTileAmountWidth) {
                            _currentPhysmapTile += LevelPhysmapTileAmountWidth;
                            UpdateImages(false, false, false, false, true, false);
                        }
                    }
                    return true;
                }
            } else if (keyData == (Keys.D1 | Keys.Control)) {
                checkBox_field_show.Checked = !checkBox_field_show.Checked;
                return true;
            } else if (keyData == (Keys.D2 | Keys.Control)) {
                checkBox_physmap_show.Checked = !checkBox_physmap_show.Checked;
                return true;
            } else if (keyData == (Keys.D3 | Keys.Control)) {
                checkBox_bytes_show.Checked = !checkBox_bytes_show.Checked;
                return true;
            } else if (keyData == (Keys.D4 | Keys.Control)) {
                checkBox_grid_show.Checked = !checkBox_grid_show.Checked;
                return true;
            } else if (keyData == Keys.V) {
                checkBox_vflip.Checked = !checkBox_vflip.Checked;
                return true;
            } else if (keyData == Keys.H) {
                checkBox_hflip.Checked = !checkBox_hflip.Checked;
                return true;
            } else if (keyData == Keys.O) {
                checkBox_priority.Checked = !checkBox_priority.Checked;
                return true;
            } else if (keyData == Keys.D) {
                if (_isLevelLoaded) {
                    if (_tilemapTileSelector.Selected) {
                        DeselectTiles();
                        return true;
                    }
                }
            } else if (keyData == Keys.Tab) {
                if (radioButton_field_edit.Checked) {
                    radioButton_physmap_edit.Checked = true;
                } else {
                    radioButton_field_edit.Checked = true;
                }
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void MainMenu_FileOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                if (openFileDialog.CheckFileExists) {
                    _isLevelLoaded = false;
                    byte[] openedData = File.ReadAllBytes(openFileDialog.FileName);

                    if (openedData.Length >= 0x8000) {
                        byte[] str = Encoding.ASCII.GetBytes("UMIHARAKAWASE");
                        byte[] checksum = new byte[str.Length];
                        Array.ConstrainedCopy(openedData, 0x7FC0, checksum, 0, str.Length);
                        if (str.SequenceEqual(checksum) == false) {
                            MessageBox.Show(Resources.MainForm_MainMenu_FileOpen_Click_InvalidRom, 
                                            Resources.MainForm_MainMenu_FileOpen_Click_Error, 
                                            MessageBoxButtons.OK, 
                                            MessageBoxIcon.Exclamation);
                            return;
                        }
                    } else {
                        MessageBox.Show(Resources.MainForm_MainMenu_FileOpen_Click_InvalidRom,
                                            Resources.MainForm_MainMenu_FileOpen_Click_Error,
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation);
                        return;
                    }

                    _romdata = RomWriter.ExpandRom(openedData);
                    if (_romdata != null) {
                        DeselectTiles();
                        OpenLevel();
                        _isLevelLoaded = true;
                    }
                }
            }
        }

        private void MainMenu_SaveLevel_Click(object sender, EventArgs e)
        {
            if (_isLevelLoaded) {
                if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                    string fileName;
                    if ((fileName = saveFileDialog.FileName) != "") {
                        _isLevelLoaded = false;
                        RomWriter writer = new RomWriter(_romdata);
                        writer.WriteLevel(_levelEditor.Level, _levelEditor.LevelHeader);
                        File.WriteAllBytes(fileName, _romdata);
                        _isLevelLoaded = true;
                    }
                }
            }
        }

        private void exportLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_isLevelLoaded) {
                if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                    string fileName;
                    if ((fileName = saveFileDialog.FileName) != "") {
                        _isLevelLoaded = false;
                        RomWriter writer = new RomWriter(_romdata);
                        byte[] data = writer.ExportLevel(_levelEditor.LevelHeader, _levelEditor.Level);
                        File.WriteAllBytes(fileName, data);
                        _isLevelLoaded = true;
                    }
                }
            }
        }

        private void importLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_isLevelLoaded) {
                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    if (openFileDialog.CheckFileExists) {
                        OpenLevel(openFileDialog.FileName);
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
            if (_isLevelLoaded) {
                for (int tileNum = 0; tileNum < Level.LevelTileAmount; tileNum++ ) {
                    _levelEditor.SetTileInTilemap(tileNum, 0, false, false, false, 0);
                }
                UpdateImages(true, false, false, false, false, false);
            }
        }

        private void clearLevelPhysmapTilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_isLevelLoaded) {
                for (int tileNum = 0; tileNum < Level.LevelTileAmount; tileNum++) {
                    _levelEditor.SetTileInPhysmap(tileNum, 0);
                }
                UpdateImages(true, false, false, false, false, false);
            }
        }

        private void numericUpDown_levelSelector_ValueChanged(object sender, EventArgs e)
        {
            if (_romdata != null) {
                if (_isLevelLoaded) {
                    _isLevelLoaded = false;
                    DeselectTiles();
                    OpenLevel();
                    _isLevelLoaded = true;
                }
            }
        }

        private void numericUpDown_tilePalette_ValueChanged(object sender, EventArgs e)
        {
            if (_isLevelLoaded) {
                _bankPaletteNum = (byte)(_levelEditor.Level.PaletteIndex[(int)numericUpDown_tilePalette.Value] - 1);
                UpdateImages(false, true, true, false, false, true);
            }
        }

        private void radioButton_field_edit_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_field_edit.Checked) {
                checkBox_field_show.Checked = true;
            }
        }

        private void radioButton_physmap_edit_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_physmap_edit.Checked) {
                checkBox_physmap_show.Checked = true;
            }
        }

        private void checkBox_vflip_CheckedChanged(object sender, EventArgs e)
        {
            if (_isLevelLoaded) {
                UpdateImages(false, false, true, false, false, false);
            }
        }

        private void checkBox_hflip_CheckedChanged(object sender, EventArgs e)
        {
            if (_isLevelLoaded) {
                UpdateImages(false, false, true, false, false, false);
            }
        }

        private void checkBox_priority_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox_field_show_CheckedChanged(object sender, EventArgs e)
        {
            UpdateImages(true, false, false, false, false, false);
        }

        private void checkBox_physmap_show_CheckedChanged(object sender, EventArgs e)
        {
            UpdateImages(true, false, false, false, false, false);
        }

        private void checkBox_bytes_CheckedChanged(object sender, EventArgs e)
        {
            UpdateImages(true, false, false, true, true, false);
        }

        private void checkBox_grid_show_CheckedChanged(object sender, EventArgs e)
        {
            UpdateImages(true, true, false, true, true, true);
        }

        private void button_deselect_Click(object sender, EventArgs e)
        {
            if (_isLevelLoaded) {
                if (_tilemapTileSelector.Selected) {
                    DeselectTiles();
                }
            }
        }

        private void button_applyindices_Click(object sender, EventArgs e)
        {
            if (_isLevelLoaded) {
                int[] tileOffsetList = new int[_levelEditor.Level.TileIndex.GetBankTileIndexSize()];
                List<int> removedTiles = new List<int>();
                int offset = 0;
                int tileNum = 0;
                for (int i = 0; i < _levelEditor.Level.TileIndex.MaxIndexAmount; i++) {
                    if (_levelEditor.Level.TileIndex[i] != _selectedTileIndices[i]) {
                        if (_selectedTileIndices[i]) {
                            offset++;
                        } else {
                            offset--;
                            removedTiles.Add(tileNum);
                        }
                    }
                    if (_levelEditor.Level.TileIndex[i]) {
                        tileOffsetList[tileNum] = offset;
                        tileNum++;
                    }
                }
                _levelEditor.RemoveInvalidTiles(removedTiles);
                for (int i = 0; i < Level.LevelTileAmount; i++) {
                    int tileValue = _levelEditor.Level.Tilemap[i].Bank * 256 + _levelEditor.Level.Tilemap[i].Tile;
                    _levelEditor.SetTileInTilemap(i, tileValue + tileOffsetList[tileValue]);
                }

                _levelEditor.Level.TileIndex.SetTileIndexList(_selectedTileIndices.ToList());
                _currentTilesetTileIndex = 0;
                UpdateImages(true, true, true, false, false, true);
            }
        }

        private void button_clearindices_Click(object sender, EventArgs e)
        {
            if (_isLevelLoaded) {
                _selectedTileIndices[0] = true;
                for (int index = 1; index < _selectedTileIndices.Length; index++) {
                    _selectedTileIndices[index] = false;
                }
                _indexTilesRemaining = MaxBankTileAmount - 1;
                UpdateImages(false, false, false, false, false, true);
                updateTextBox_IndexTiles();
            }
        }

        private void button_applyheader_Click(object sender, EventArgs e)
        {
            if (_isLevelLoaded) {
                UpdateLevelHeaderValues();
                _levelEditor.UpdateLevelHeader(_selectedLevelHeader);
                UpdateImages(true, true, true, true, true, true);
            }
        }

        private void SetCurrentTilesetTileByNum(int tilesetTileNum)
        {
            _currentTilesetTileNum = tilesetTileNum;
            if (_currentTilesetTileNum < 0) {
                _currentTilesetTileNum = 0;
            } else if (_currentTilesetTileNum >= _levelEditor.Level.TileIndex.MaxIndexAmount) {
                _currentTilesetTileNum = _levelEditor.Level.TileIndex.MaxIndexAmount - 1;
            }

            _currentTilesetTileIndex = _levelEditor.Level.TileIndex.GetBankIndex(_currentTilesetTileNum);
            if (_currentTilesetTileIndex < 0) {
                _currentTilesetTileIndex = 0;
            } else if (_currentTilesetTileIndex >= _levelEditor.Level.TileIndex.GetBankTileIndexSize()) {
                _currentTilesetTileIndex = _levelEditor.Level.TileIndex.GetBankTileIndexSize() - 1;
            }
        }

        private void SetCurrentTilesetTileByIndex(int tilesetTileIndex)
        {
            _currentTilesetTileIndex = tilesetTileIndex;
            if (_currentTilesetTileIndex < 0) {
                _currentTilesetTileIndex = 0;
            } else if (_currentTilesetTileIndex >= _levelEditor.Level.TileIndex.GetBankTileIndexSize()) {
                _currentTilesetTileIndex = _levelEditor.Level.TileIndex.GetBankTileIndexSize() - 1;
            }

            _currentTilesetTileNum = _levelEditor.Level.TileIndex.GetBankIndexTile(tilesetTileIndex);
            if (_currentTilesetTileNum < 0) {
                _currentTilesetTileNum = 0;
            } else if (_currentTilesetTileNum >= _levelEditor.Level.TileIndex.MaxIndexAmount) {
                _currentTilesetTileNum = _levelEditor.Level.TileIndex.MaxIndexAmount - 1;
            }
        }

        private void pictureBox_tileset_MouseClick(object sender, MouseEventArgs e)
        {
            if (_isLevelLoaded) {
                Point mouseCoords = new Point(e.X, e.Y);
                SetCurrentTilesetTileByNum(_coordConverterTileset.GetTileNumberFromMouseCoords(mouseCoords));
                if (_tilemapTileSelector.Selected) {
                    DeselectTiles();
                }
                radioButton_field_edit.Select();
                checkBox_field_show.Checked = true;
                UpdateImages(false, false, true, false, false, false);
            }
        }

        private void pictureBox_phystiles_MouseClick(object sender, MouseEventArgs e)
        {
            if (_isLevelLoaded) {
                Point mouseCoords = new Point(e.X, e.Y);
                int tileNum = _coordConverterTileset.GetTileNumberFromMouseCoords(mouseCoords);
                if (tileNum < PhystileTileAmount) {
                    if (_tilemapTileSelector.Selected) {
                        DeselectTiles();
                    }
                    _currentPhysmapTile = (byte)tileNum;
                    radioButton_physmap_edit.Select();
                    checkBox_physmap_show.Checked = true;
                    UpdateImages(false, false, false, false, true, false);
                }
            }
        }

        private void pictureBox_indexTiles_MouseDown(object sender, MouseEventArgs e)
        {
            SetIndexTilesFromMouseClick(e);
        }

        private void pictureBox_indexTiles_MouseMove(object sender, MouseEventArgs e)
        {
            SetIndexTilesFromMouseClick(e);
        }

        private void pictureBox_indexTiles_MouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Left) || (e.Button == MouseButtons.Right)) {
                _lastIndexTileSelected = -1;
            }
        }

        private void pictureBox_level_MouseDown(object sender, MouseEventArgs e)
        {
            if (_isLevelLoaded) {
                if (e.Button == MouseButtons.Left) {
                    if (_tilemapTileSelector.Selected) {
                        DeselectTiles();
                    }
                    _tilemapTileSelector.SelectStart(new Point(e.X, e.Y), (int)TilemapLevelScale * TileDrawer.TileWidth);
                } else if (e.Button == MouseButtons.Right) {
                    Point mouseCoords = new Point(e.X, e.Y);
                    DrawAndSetTiles(mouseCoords);
                    if (_tilemapTileSelector.Selected == false) {
                        int tileNum = _coordConverterLevel.GetTileNumberFromMouseCoords(new Point(e.X, e.Y));
                        _lastLevelTileSelected = tileNum;
                    }
                } else if (e.Button == MouseButtons.Middle) {
                    if (_tilemapTileSelector.Selected) {
                        DeselectTiles();
                    }
                }
            }
        }

        private void pictureBox_level_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isLevelLoaded) {
                if (e.Button == MouseButtons.Left) {
                    // Point mouseCoords = new Point(e.X, e.Y);
                    // Do rectangle drawing here
                } else if (e.Button == MouseButtons.Right) {
                    Point mouseCoords = new Point(e.X, e.Y);
                    int tileNum = _coordConverterLevel.GetTileNumberFromMouseCoords(mouseCoords);
                    if ((tileNum != _lastLevelTileSelected) && (_tilemapTileSelector.Selected == false)) {
                        DrawAndSetTiles(mouseCoords);
                        _lastLevelTileSelected = tileNum;
                    }
                }
            }
        }

        private void pictureBox_level_MouseUp(object sender, MouseEventArgs e)
        {
            if (_isLevelLoaded) {
                if (e.Button == MouseButtons.Left) {
                    Point mouseCoords = new Point(e.X, e.Y);
                    _tilemapTileSelector.SelectEnd(mouseCoords, (int)TilemapLevelScale * TileDrawer.TileWidth);
                    HighlightSelectedTilesInLevelEditor(true);
                }
            }
        }

        private void OpenLevel(string importFileName = "")
        {
            _isLevelLoaded = false;
            if (importFileName == "") {
                _levelEditor.OpenLevel(_romdata, (byte)numericUpDown_levelSelector.Value);
            } else {
                byte[] openedData = File.ReadAllBytes(importFileName);
                RomWriter writer = new RomWriter(_romdata);
                if (writer.ImportLevel(openedData, _levelEditor.Level, _levelEditor.LevelHeader) == false) {
                    _isLevelLoaded = true;
                    return;
                }
            }
            _levelEditor.UpdateGraphicsBanks(_romdata);
            for (int i = 0; i < _levelEditor.Level.TileIndex.MaxIndexAmount; i++) {
                _selectedTileIndices[i] = _levelEditor.Level.TileIndex[i];
            }
            
            _isLevelLoaded = true;
            _indexTilesRemaining = MaxBankTileAmount - _levelEditor.Level.TileIndex.GetBankTileIndexSize();
            updateTextBox_IndexTiles();
            _selectedLevelHeader = new LevelHeader(_levelEditor.LevelHeader);
            _bankPaletteNum = (byte)(_levelEditor.Level.PaletteIndex[(int)numericUpDown_tilePalette.Value] - 1);
            UpdateLevelHeaderControls();
            _currentTilesetTileIndex = 0;
            _currentPhysmapTile = 0;
            UpdateImages(true, true, true, false, true, true);
        }

        private List<TileSelection<TilemapTile>> GetSelectedTilemapTiles()
        {
            List<TileSelection<TilemapTile>> tileList;
            if (_tilemapTileSelector.Selected) {
                tileList = _tilemapTileSelector.GetTilesFromSelection(_levelEditor.Level.Tilemap, LevelTileAmountWidth);
            } else {
                tileList = new List<TileSelection<TilemapTile>>();
                TilemapTile item = new TilemapTile(_currentTilesetTileIndex, 
                                                   checkBox_vflip.Checked, 
                                                   checkBox_hflip.Checked, 
                                                   checkBox_priority.Checked, 
                                                   (byte)numericUpDown_tilePalette.Value);
                tileList.Add(new TileSelection<TilemapTile>(new Point(0, 0), item));
            }
            return tileList;
        }

        private List<TileSelection<byte>> GetSelectedPhysmapTiles()
        {
            List<TileSelection<byte>> tileList;
            if (_tilemapTileSelector.Selected) {
                tileList = _tilemapTileSelector.GetTilesFromSelection(_levelEditor.Level.Physmap, LevelTileAmountWidth);
            } else {
                tileList = new List<TileSelection<byte>>();
                byte item = _currentPhysmapTile;
                tileList.Add(new TileSelection<byte>(new Point(0, 0), item));
            }
            return tileList;
        }

        private void DrawAndSetTiles(Point mouseCoords, bool drawHighlight = true)
        {
            if ((mouseCoords.X >= 0) && (mouseCoords.Y >= 0)) {
                ClearSelectedAreaInLevelEditor(mouseCoords);
                if (checkBox_field_show.Checked) {
                    if (radioButton_field_edit.Checked) {
                        SetTilemapTilesInLevelEditor(mouseCoords);
                    }
                    DrawTilemapTilesInLevelEditor(mouseCoords);
                }
                if (checkBox_physmap_show.Checked) {
                    if (radioButton_physmap_edit.Checked) {
                        SetPhysmapTilesInLevelEditor(mouseCoords);
                    }
                    DrawPhysmapTilesInLevelEditor(mouseCoords);
                }

                if (checkBox_grid_show.Checked) {
                    DrawGridTilesInLevelEditor(mouseCoords);
                }
                if ((_tilemapTileSelector.Selected) && (drawHighlight)) {
                    HighlightSelectedTilesInLevelEditor();
                }
                InvalidateTilesInLevelEditor(mouseCoords);
            }
        }

        private void SetTilemapTilesInLevelEditor(Point mouseCoords)
        {
            var tileList = GetSelectedTilemapTiles();
            Point tileCoords = _coordConverterLevel.GetTileCoordsFromMouseCoords(mouseCoords);
            Point placementCoords = new Point();
            foreach (var item in tileList) {
                placementCoords.X = tileCoords.X + item.TileCoords.X;
                placementCoords.Y = tileCoords.Y + item.TileCoords.Y;
                if ((placementCoords.X < LevelTileAmountWidth) && (placementCoords.Y < LevelTileAmountWidth)) {
                    int tileNum = _coordConverterLevel.GetTileNumberFromTileCoords(placementCoords);
                    if ((item.Tile.Bank > 0) || (item.Tile.Tile > 0)) {
                        _levelEditor.SetTileInTilemap(tileNum, item.Tile);
                    } else {
                        if (_tilemapTileSelector.Selected == false) {
                            TilemapTile tile = new TilemapTile();
                            _levelEditor.SetTileInTilemap(tileNum, tile);
                        }
                    }
                }
            }
        }

        private void SetPhysmapTilesInLevelEditor(Point mouseCoords)
        {
            var tileList = GetSelectedPhysmapTiles();
            Point tileCoords = _coordConverterLevel.GetTileCoordsFromMouseCoords(mouseCoords);
            Point placementCoords = new Point();
            foreach (var item in tileList) {
                placementCoords.X = tileCoords.X + item.TileCoords.X;
                placementCoords.Y = tileCoords.Y + item.TileCoords.Y;
                if ((placementCoords.X < LevelTileAmountWidth) && (placementCoords.Y < LevelTileAmountWidth)) {
                    int tileNum = _coordConverterLevel.GetTileNumberFromTileCoords(placementCoords);
                    if (item.Tile > 0) {
                        _levelEditor.SetTileInPhysmap(tileNum, item.Tile);
                    } else {
                        if (_tilemapTileSelector.Selected == false) {
                            _levelEditor.SetTileInPhysmap(tileNum, 0);
                        }
                    }
                }
            }
        }

        private void SetIndexTilesFromMouseClick(MouseEventArgs e)
        {
            if (((e.Button == MouseButtons.Left) || (e.Button == MouseButtons.Right)) && (_isLevelLoaded)) {
                Point mouseCoords = new Point(e.X, e.Y);
                int tileNum = _coordConverterTileIndex.GetTileNumberFromMouseCoords(mouseCoords);
                if ((tileNum > 0) && (tileNum != _lastIndexTileSelected)) {
                    int index = _levelEditor.LevelHeader.GraphicsBankIndex * 2;
                    int tileAmount = _levelEditor.Banks[index].TileAmount;
                    if (tileNum > tileAmount) {
                        index += 1;
                    }
                    if (e.Button == MouseButtons.Left) {
                        if ((_indexTilesRemaining < MaxBankTileAmount - 1) && (_selectedTileIndices[tileNum])) {
                            _selectedTileIndices[tileNum] = false;
                            _indexTilesRemaining += 1;
                            _lastIndexTileSelected = tileNum;
                        }
                    } else {
                        if ((_indexTilesRemaining > 0) && (_selectedTileIndices[tileNum] == false)) {
                            _selectedTileIndices[tileNum] = true;
                            _indexTilesRemaining -= 1;
                            _lastIndexTileSelected = tileNum;
                        }
                    }
                    updateTextBox_IndexTiles();
                    Graphics g = Graphics.FromImage(_bitmapTileIndex);
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    g.PixelOffsetMode = PixelOffsetMode.Half;
                    DrawIndexTile(g, index, tileNum, tileAmount, _selectedTileIndices[tileNum], checkBox_grid_show.Checked);
                    Point alignedMouseCoords = _coordConverterTileIndex.GetMouseCoordsFromTileNumber(tileNum);
                    InvalidateIndexTile(alignedMouseCoords);
                }
            }
        }

        private void DrawTilemapTilesInLevelEditor(Point mouseCoords)
        {
            var tileList = GetSelectedTilemapTiles();
            Point tileCoords = _coordConverterLevel.GetTileCoordsFromMouseCoords(mouseCoords);
            using (Graphics g = Graphics.FromImage(_bitmapLevel)) {
                Point tempCoord = new Point();
                foreach (var item in tileList) {
                    tempCoord.X = tileCoords.X + item.TileCoords.X;
                    tempCoord.Y = tileCoords.Y + item.TileCoords.Y;
                    Point alignedCoords = _coordConverterLevel.GetMouseCoordsFromTileCoords(tempCoord);
                    int index = tempCoord.Y * LevelTileAmountWidth + tempCoord.X;
                    if (index < Level.LevelTileAmount) {
                        TilemapTile t = _levelEditor.Level.Tilemap[index];
                        int tileValue = _levelEditor.Level.TileIndex.GetBankIndexTile(t.Bank * 256 + t.Tile);
                        if (tileValue > 0) {
                            GraphicBank bank = _levelEditor.Banks[_levelEditor.LevelHeader.GraphicsBankIndex * 2 + (tileValue / 1024)];
                            byte bankPaletteNumber = (byte)(_levelEditor.Level.PaletteIndex[t.Palette] - 1);
                            TileDrawer.ClearTileOnCanvas(g, _fillBrush, alignedCoords.X, alignedCoords.Y, TilemapLevelScale);
                            if (tileValue >= 1024) {
                                tileValue -= 1024;
                            }
                            Bitmap tileImg = bank.GetTileImage(tileValue, bankPaletteNumber, TileDrawer.TileWidth);
                            TileDrawer.DrawTileOnCanvas(tileImg,
                                                        g,
                                                        alignedCoords.X,
                                                        alignedCoords.Y,
                                                        t.VFlip,
                                                        t.HFlip,
                                                        TilemapLevelScale);
                        }
                    }
                }
            }
        }

        private void DrawPhysmapTilesInLevelEditor(Point mouseCoords)
        {
            var tileList = GetSelectedPhysmapTiles();
            Point tileCoords = _coordConverterLevel.GetTileCoordsFromMouseCoords(mouseCoords);
            using (Graphics g = Graphics.FromImage(_bitmapLevel)) {
                Point tempCoord = new Point();
                foreach (var item in tileList) {
                    tempCoord.X = tileCoords.X + item.TileCoords.X;
                    tempCoord.Y = tileCoords.Y + item.TileCoords.Y;
                    Point destCoords = _coordConverterLevel.GetMouseCoordsFromTileCoords(tempCoord);
                    int index = tempCoord.Y * LevelTileAmountWidth + tempCoord.X;
                    if (index < Level.LevelTileAmount) {
                        byte tileNum = _levelEditor.Level.Physmap[index];
                        if (tileNum > 0) {
                            Point srcCoords = _coordConverterPhysmap.GetMouseCoordsFromTileNumber(tileNum);
                            if (checkBox_field_show.Checked == false)
                                TileDrawer.ClearTileOnCanvas(g, _fillBrush, destCoords.X, destCoords.Y, TilemapLevelScale);
                            TileDrawer.DrawTileFromImageOnCanvas(_bitmapPhysTileset, g, srcCoords, destCoords, TilemapLevelScale, TilemapLevelScale);
                        }
                    }
                }
            }
        }

        private void DrawGridTilesInLevelEditor(Point mouseCoords)
        {
            var tileList = GetSelectedTilemapTiles();
            Point tileCoords = _coordConverterLevel.GetTileCoordsFromMouseCoords(mouseCoords);
            using (Graphics g = Graphics.FromImage(_bitmapLevel)) {
                Point tempCoord = new Point();
                foreach (var item in tileList) {
                    tempCoord.X = tileCoords.X + item.TileCoords.X;
                    tempCoord.Y = tileCoords.Y + item.TileCoords.Y;
                    Point destCoords = _coordConverterLevel.GetMouseCoordsFromTileCoords(tempCoord);
                    int index = tempCoord.Y * LevelTileAmountWidth + tempCoord.X;
                    if (index < Level.LevelTileAmount) {
                        TileDrawer.DrawGridCellOnCanvas(g, destCoords.X, destCoords.Y, TilemapLevelScale);
                    }
                }
            }
        }

        private void InvalidateTilesInLevelEditor(Point mouseCoords)
        {
            var tileList = GetSelectedTilemapTiles();
            Point tileCoords = _coordConverterLevel.GetTileCoordsFromMouseCoords(mouseCoords);
            Point tempCoord = new Point();
            Point invalidateBottomRightPoint = new Point();
            foreach (var item in tileList) {
                tempCoord.X = tileCoords.X + item.TileCoords.X;
                tempCoord.Y = tileCoords.Y + item.TileCoords.Y;
                Point alignedCoords = _coordConverterLevel.GetMouseCoordsFromTileCoords(tempCoord);
                if (invalidateBottomRightPoint.X < alignedCoords.X) {
                    invalidateBottomRightPoint.X = alignedCoords.X;
                }
                if (invalidateBottomRightPoint.Y < alignedCoords.Y) {
                    invalidateBottomRightPoint.Y = alignedCoords.Y;
                }
            }
            if ((invalidateBottomRightPoint.X >= 0) && (invalidateBottomRightPoint.Y >= 0)) {
                Point topLeft = _coordConverterLevel.GetMouseCoordsFromTileCoords(tileCoords);
                int width = invalidateBottomRightPoint.X - topLeft.X + (int)TilemapLevelScale * TileDrawer.TileWidth;
                int height = invalidateBottomRightPoint.Y - topLeft.Y + (int)TilemapLevelScale * TileDrawer.TileWidth;
                pictureBox_level.Invalidate(new Rectangle(topLeft.X, topLeft.Y, width, height));
            }
        }

        private void HighlightSelectedTilesInLevelEditor(bool invalidate = false)
        {
            if (_tilemapTileSelector.Selected) {
                Rectangle rect = _coordConverterLevel.GetMouseCoordsFromRectCoords(_tilemapTileSelector.TileCoords);
                using (Graphics g = Graphics.FromImage(_bitmapLevel)) {
                    Point point = new Point(rect.X, rect.Y);
                    g.FillRectangle(_highlightBrush, rect);
                    if (invalidate) {
                        InvalidateTilesInLevelEditor(point);
                    }
                }
            }
        }

        private void ClearSelectedAreaInLevelEditor(Point mouseCoords)
        {
            var tileList = GetSelectedTilemapTiles();
            Point tileCoords = _coordConverterLevel.GetTileCoordsFromMouseCoords(mouseCoords);
            using (Graphics g = Graphics.FromImage(_bitmapLevel)) {
                Point tempCoord = new Point();
                foreach (var item in tileList) {
                    tempCoord.X = tileCoords.X + item.TileCoords.X;
                    tempCoord.Y = tileCoords.Y + item.TileCoords.Y;
                    Point alignedCoords = _coordConverterLevel.GetMouseCoordsFromTileCoords(tempCoord);
                    TileDrawer.ClearTileOnCanvas(g, _fillBrush, alignedCoords.X, alignedCoords.Y, TilemapLevelScale);
                }
            }
        }

        private void DrawIndexTiles(Graphics g, bool skipUnusedBankTiles, bool highlight = true, bool grid = false)
        {
            int index = _levelEditor.LevelHeader.GraphicsBankIndex * 2;
            int tileAmount = _levelEditor.Banks[index].TileAmount;
            for (int tileIndexNum = 0; tileIndexNum < tileAmount * 2; tileIndexNum++) {
                if (tileIndexNum == tileAmount) {
                    index += 1;
                }
                if (skipUnusedBankTiles == false) {
                    DrawIndexTile(g, index, tileIndexNum, tileAmount, highlight, grid);
                } else {
                    if (_levelEditor.Level.TileIndex.GetBankIndex(tileIndexNum) > 0) {
                        DrawIndexTile(g, index, tileIndexNum, tileAmount, highlight, grid);
                    }
                }
            }
        }

        private void DrawIndexTile(Graphics g, int bankIndex, int tileIndexNum, int tileAmount, bool highlight = true, bool grid = false)
        {
            int i = 0;
            if (tileIndexNum >= tileAmount) {
                i = 1;
            }
            int tileNum = tileIndexNum - (i * tileAmount);

            byte bankPaletteNumber = (byte)(_levelEditor.Level.PaletteIndex[(int)numericUpDown_tilePalette.Value] - 1);
            Bitmap tileImg = _levelEditor.Banks[bankIndex].GetTileImage(tileNum, bankPaletteNumber, TileDrawer.TileWidth);
            Point mouseCoords = _coordConverterTileIndex.GetMouseCoordsFromTileNumber(tileNum);
            TileDrawer.ClearTileOnCanvas(g, _fillBrush, mouseCoords.X, mouseCoords.Y + (i * 1024), TilemapScale);
            TileDrawer.DrawTileOnCanvas(tileImg, g, mouseCoords.X, mouseCoords.Y + (i * 1024), false, false, TilemapScale);
            if (highlight) {
                if (_selectedTileIndices[tileIndexNum]) {
                    HighlightIndexTile(g, tileIndexNum);
                }
            }
            if (grid) {
                DrawGridTileOnIndexTile(g, tileIndexNum);
            }
        }

        private void HighlightIndexTile(Graphics g, int tileIndexNum)
        {
            Point mouseCoords = _coordConverterTileIndex.GetMouseCoordsFromTileNumber(tileIndexNum);
            int tileWidth = _coordConverterTileIndex.TileWidth;
            Rectangle rect = new Rectangle(mouseCoords, new Size(tileWidth, tileWidth));
            g.FillRectangle(_highlightBrush, rect);
        }

        private void DrawGridTileOnIndexTile(Graphics g, int tileIndexNum)
        {
            Point mouseCoords = _coordConverterTileIndex.GetMouseCoordsFromTileNumber(tileIndexNum);
            TileDrawer.DrawGridCellOnCanvas(g, mouseCoords.X, mouseCoords.Y, TilemapScale);
        }

        private void InvalidateIndexTile(Point alignedMouseCoords)
        {
            pictureBox_indexTiles.Invalidate(new Rectangle(alignedMouseCoords.X, 
                                                           alignedMouseCoords.Y, 
                                                           TileDrawer.TileWidth * (int)TilemapScale, 
                                                           TileDrawer.TileWidth * (int)TilemapScale));
        }

        private void DeselectTiles()
        {
            if (_tilemapTileSelector.Selected) {
                Point tileCoords = new Point(_tilemapTileSelector.TileCoords.X, _tilemapTileSelector.TileCoords.Y);
                Point mouseCoords = _coordConverterLevel.GetMouseCoordsFromTileCoords(tileCoords);
                ClearSelectedAreaInLevelEditor(mouseCoords);
                DrawAndSetTiles(mouseCoords, false);
            }
            _tilemapTileSelector.ClearSelection();
            _lastLevelTileSelected = -1;
        }

        private void UpdateLevelHeaderValues()
        {
            if (_isLevelLoaded) {
                //selectedLevelHeader.headerNumber = (byte)numericUpDown_headernumber.Value;
                //selectedLevelHeader.headerPointerAddress = (int)numericUpDown_headerpointer.Value;
                //selectedLevelHeader.headerAddress = (int)numericUpDown_headeraddress.Value;
                //selectedLevelHeader.levelPointer = (int)numericUpDown_levelpointer.Value;
                _selectedLevelHeader.GraphicsBankIndex = (byte)numericUpDown_graphicsbankindex.Value;
                _selectedLevelHeader.FieldNumber = (byte)numericUpDown_fieldnumber.Value;
                _selectedLevelHeader.MusicSelect = (byte)numericUpDown_musicselect.Value;
                _selectedLevelHeader.EnemyType[0] = (byte)numericUpDown_enemytype1.Value;
                _selectedLevelHeader.EnemyType[1] = (byte)numericUpDown_enemytype2.Value;
                _selectedLevelHeader.EnemyType[2] = (byte)numericUpDown_enemytype3.Value;
                _selectedLevelHeader.EnemyType[3] = (byte)numericUpDown_enemytype4.Value;
                _selectedLevelHeader.EnemyType[4] = (byte)numericUpDown_enemytype5.Value;
                _selectedLevelHeader.EnemyType[5] = (byte)numericUpDown_enemytype6.Value;
                _selectedLevelHeader.SpawnRates[0] = (byte)numericUpDown_spawnrate1.Value;
                _selectedLevelHeader.SpawnRates[1] = (byte)numericUpDown_spawnrate2.Value;
                _selectedLevelHeader.SpawnRates[2] = (byte)numericUpDown_spawnrate3.Value;
                _selectedLevelHeader.SpawnRates[3] = (byte)numericUpDown_spawnrate4.Value;
                _selectedLevelHeader.SpawnRates[4] = (byte)numericUpDown_spawnrate5.Value;
                _selectedLevelHeader.SpawnRates[5] = (byte)numericUpDown_spawnrate6.Value;
                _selectedLevelHeader.SpawnRates[6] = (byte)numericUpDown_spawnrate7.Value;
                _selectedLevelHeader.SpawnRates[7] = (byte)numericUpDown_spawnrate8.Value;
                _selectedLevelHeader.ObjectType[0] = (byte)numericUpDown_objecttype1.Value;
                _selectedLevelHeader.ObjectType[1] = (byte)numericUpDown_objecttype2.Value;
                _selectedLevelHeader.ObjectType[2] = (byte)numericUpDown_objecttype3.Value;
                _selectedLevelHeader.ObjectType[3] = (byte)numericUpDown_objecttype4.Value;
                _selectedLevelHeader.ObjectType[4] = (byte)numericUpDown_objecttype5.Value;
                _selectedLevelHeader.ObjectType[5] = (byte)numericUpDown_objecttype6.Value;
                _selectedLevelHeader.ObjectType[6] = (byte)numericUpDown_objecttype7.Value;
                _selectedLevelHeader.WaterHeight = (byte)numericUpDown_waterheight.Value;

                _selectedLevelHeader.WaterType = 2;
                if (checkBox_wavywater.Checked) {
                    _selectedLevelHeader.WaterType = 3;
                }
                
                _selectedLevelHeader.LevelTimer = (int)numericUpDown_leveltimer.Value;
                _selectedLevelHeader.DoorExits[0] = (byte)numericUpDown_doorexit1.Value;
                _selectedLevelHeader.DoorExits[1] = (byte)numericUpDown_doorexit2.Value;
                _selectedLevelHeader.DoorExits[2] = (byte)numericUpDown_doorexit3.Value;
                _selectedLevelHeader.DoorExits[3] = (byte)numericUpDown_doorexit4.Value;
                _levelEditor.Level.PaletteIndex[2] = (byte)numericUpDown_paletteindices1.Value;
                _levelEditor.Level.PaletteIndex[3] = (byte)numericUpDown_paletteindices2.Value;
                _levelEditor.Level.PaletteIndex[4] = (byte)numericUpDown_paletteindices3.Value;
                _levelEditor.Level.PaletteIndex[5] = (byte)numericUpDown_paletteindices4.Value;
                _levelEditor.Level.PaletteIndex[6] = (byte)numericUpDown_paletteindices5.Value;
                _levelEditor.Level.PaletteIndex[7] = (byte)numericUpDown_paletteindices6.Value;
            }
        }

        private void UpdateLevelHeaderControls()
        {
            if (_isLevelLoaded) {
                textBox_headernumber.Text = $"{_selectedLevelHeader.HeaderNumber}";
                textBox_headernumber.Invalidate();
                textBox_headerpointer.Text = $"{_selectedLevelHeader.HeaderPointerAddress:X}";
                textBox_headerpointer.Invalidate();
                textBox_headeraddress.Text = $"{_selectedLevelHeader.HeaderAddress:X}";
                textBox_headeraddress.Invalidate();
                textBox_levelpointer.Text = $"{_selectedLevelHeader.LevelPointer:X}";
                textBox_levelpointer.Invalidate();
                numericUpDown_graphicsbankindex.Value = _selectedLevelHeader.GraphicsBankIndex;
                numericUpDown_fieldnumber.Value = _selectedLevelHeader.FieldNumber;
                numericUpDown_musicselect.Value = _selectedLevelHeader.MusicSelect;
                numericUpDown_enemytype1.Value = _selectedLevelHeader.EnemyType[0];
                numericUpDown_enemytype2.Value = _selectedLevelHeader.EnemyType[1];
                numericUpDown_enemytype3.Value = _selectedLevelHeader.EnemyType[2];
                numericUpDown_enemytype4.Value = _selectedLevelHeader.EnemyType[3];
                numericUpDown_enemytype5.Value = _selectedLevelHeader.EnemyType[4];
                numericUpDown_enemytype6.Value = _selectedLevelHeader.EnemyType[5];
                numericUpDown_spawnrate1.Value = _selectedLevelHeader.SpawnRates[0];
                numericUpDown_spawnrate2.Value = _selectedLevelHeader.SpawnRates[1];
                numericUpDown_spawnrate3.Value = _selectedLevelHeader.SpawnRates[2];
                numericUpDown_spawnrate4.Value = _selectedLevelHeader.SpawnRates[3];
                numericUpDown_spawnrate5.Value = _selectedLevelHeader.SpawnRates[4];
                numericUpDown_spawnrate6.Value = _selectedLevelHeader.SpawnRates[5];
                numericUpDown_spawnrate7.Value = _selectedLevelHeader.SpawnRates[6];
                numericUpDown_spawnrate8.Value = _selectedLevelHeader.SpawnRates[7];
                numericUpDown_objecttype1.Value = _selectedLevelHeader.ObjectType[0];
                numericUpDown_objecttype2.Value = _selectedLevelHeader.ObjectType[1];
                numericUpDown_objecttype3.Value = _selectedLevelHeader.ObjectType[2];
                numericUpDown_objecttype4.Value = _selectedLevelHeader.ObjectType[3];
                numericUpDown_objecttype5.Value = _selectedLevelHeader.ObjectType[4];
                numericUpDown_objecttype6.Value = _selectedLevelHeader.ObjectType[5];
                numericUpDown_objecttype7.Value = _selectedLevelHeader.ObjectType[6];
                numericUpDown_waterheight.Value = _selectedLevelHeader.WaterHeight;
                checkBox_wavywater.Checked = _selectedLevelHeader.WaterType == 3;
                numericUpDown_leveltimer.Value = _selectedLevelHeader.LevelTimer;
                numericUpDown_doorexit1.Value = _selectedLevelHeader.DoorExits[0];
                numericUpDown_doorexit2.Value = _selectedLevelHeader.DoorExits[1];
                numericUpDown_doorexit3.Value = _selectedLevelHeader.DoorExits[2];
                numericUpDown_doorexit4.Value = _selectedLevelHeader.DoorExits[3];
                numericUpDown_paletteindices1.Value = _levelEditor.Level.PaletteIndex[2];
                numericUpDown_paletteindices2.Value = _levelEditor.Level.PaletteIndex[3];
                numericUpDown_paletteindices3.Value = _levelEditor.Level.PaletteIndex[4];
                numericUpDown_paletteindices4.Value = _levelEditor.Level.PaletteIndex[5];
                numericUpDown_paletteindices5.Value = _levelEditor.Level.PaletteIndex[6];
                numericUpDown_paletteindices6.Value = _levelEditor.Level.PaletteIndex[7];
            }
        }

        private void UpdateImages(bool level, bool tileset, bool tilesetTile, bool physmap, bool physmapTile, bool tileIndex)
        {
            if (tileIndex) {
                updateImage_TileIndex();
            }
            if (tileset) {
                updateImage_Tileset();
            }
            if (tilesetTile) {
                updateImage_TilemapTile();
            }
            if (physmap) {
                updateImage_Physmap();
            }
            if (physmapTile) {
                updateImage_PhysmapTile();
            }
            if (level) {
                updateImage_Level();
            }
        }

        private void updateImage_Tileset()
        {
            if (_isLevelLoaded) {
                using (Graphics g = Graphics.FromImage(pictureBox_tileset.Image)) {
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    g.PixelOffsetMode = PixelOffsetMode.Half;
                    g.Clear(_fillColor);
                    DrawIndexTiles(g, true, false);
                    if (checkBox_grid_show.Checked) {
                        g.DrawImage(Resources.gridtile16_256x2048, 0, 0);
                    }
                    pictureBox_tileset.Invalidate();
                }
            }
        }

        private void updateImage_Physmap()
        {
            if (_isLevelLoaded) {
                if (checkBox_bytes_show.Checked) {
                    _bitmapPhysTileset = Resources.physmap_bytes2;
                    _bitmapPhysTileset.SetResolution(ImageDpi, ImageDpi);
                    Bitmap bmp = Resources.physmap_bytes2;
                    bmp.SetResolution(ImageDpi, ImageDpi);
                    pictureBox_phystiles.Image = bmp;
                } else {
                    _bitmapPhysTileset = Resources.physmap2;
                    _bitmapPhysTileset.SetResolution(ImageDpi, ImageDpi);
                    Bitmap bmp = Resources.physmap2;
                    bmp.SetResolution(ImageDpi, ImageDpi);
                    pictureBox_phystiles.Image = bmp;
                }
                if (checkBox_grid_show.Checked) {
                    using (Graphics g = Graphics.FromImage(pictureBox_phystiles.Image)) {
                        g.DrawImage(Resources.gridtile16_256x256, 0, 0);
                    }
                }
                pictureBox_phystiles.Invalidate();
            }
        }

        private void updateImage_TileIndex()
        {
            if (_isLevelLoaded) {
                using (Graphics g = Graphics.FromImage(_bitmapTileIndex)) {
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    g.PixelOffsetMode = PixelOffsetMode.Half;
                    g.Clear(_fillColor);
                    DrawIndexTiles(g, false, true, checkBox_grid_show.Checked);
                }
                pictureBox_indexTiles.Invalidate();
            }
        }

        private void updateImage_TilemapTile()
        {
            if (_isLevelLoaded) {
                using (Graphics g = Graphics.FromImage(_bitmapTilemapTile)) {
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    g.PixelOffsetMode = PixelOffsetMode.Half;
                    g.Clear(_fillColor);

                    int i = 0;
                    int tileNum = _levelEditor.Level.TileIndex.GetBankIndexTile(_currentTilesetTileIndex);
                    if (tileNum < 0) {
                        tileNum = 0;
                    }
                    else if (tileNum >= 1024) {
                        i = 1;
                    }
                    tileNum = tileNum - (i * 1024);

                    int bankIndex = _levelEditor.Level.LevelHeader.GraphicsBankIndex * 2 + i;
                    Bitmap tileImg = _levelEditor.Banks[bankIndex].GetTileImage(tileNum, _bankPaletteNum, TileDrawer.TileWidth);
                    TileDrawer.DrawTileOnCanvas(tileImg, g,  0, 0, checkBox_vflip.Checked, checkBox_hflip.Checked, TileSelectorScale);
                    pictureBox_tilemaptile.Invalidate();
                }
            }
        }

        private void updateImage_PhysmapTile()
        {
            if (_isLevelLoaded) {
                Point alignedMouseCoords = _coordConverterPhysmap.GetMouseCoordsFromTileNumber(_currentPhysmapTile);
                using (Graphics g = Graphics.FromImage(_bitmapPhysTile)) {
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    g.PixelOffsetMode = PixelOffsetMode.Half;
                    g.Clear(_fillColor);
                    TileDrawer.DrawTileFromImageOnCanvas(_bitmapPhysTileset,
                                                         g,
                                                         alignedMouseCoords,
                                                         new Point(0, 0),
                                                         TilemapLevelScale,
                                                         TileSelectorScale);
                    pictureBox_phystile.Invalidate();
                }
            }
        }

        private void updateImage_Level()
        {
            if (_isLevelLoaded) {
                using (Graphics g = Graphics.FromImage(_bitmapLevel)) {
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    g.PixelOffsetMode = PixelOffsetMode.Half;
                    g.Clear(_fillColor);
                    TileDrawer.DrawLevelOnCanvas(g,
                                                 _bitmapPhysTileset, 
                                                 _levelEditor, 
                                                 LevelTileAmountWidth, 
                                                 checkBox_field_show.Checked, 
                                                 checkBox_physmap_show.Checked,
                                                 TilemapLevelScale);
                    if (checkBox_grid_show.Checked) {
                        g.DrawImage(Resources.gridtile16_1024x1024, 0, 0);
                    }
                    if (_tilemapTileSelector.Selected) {
                        HighlightSelectedTilesInLevelEditor();
                    }
                    pictureBox_level.Invalidate();
                }
            }
        }

        private void updateTextBox_IndexTiles()
        {
            if (_isLevelLoaded) {
                textBox_tilesremaining.Text = $"{_indexTilesRemaining}";
                textBox_tilesremaining.Invalidate();
            }
        }
    }
}

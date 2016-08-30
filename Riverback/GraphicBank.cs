using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace Riverback
{
    public class PlanarTilesWithOffset
    {
        public List<byte> PlanarTiles;
        public int Offset;

        public PlanarTilesWithOffset(List<byte> planarTiles, int offset)
        {
            PlanarTiles = planarTiles;
            Offset = offset;
        }
    }

    public class GraphicBank
    {
        private const int PaletteAmount = 15;
        private const int PaletteColorAmount = 16;
        private readonly Color[] _colorsHardcoded1 =
            {new Color(25, 33, 16, 0, true), new Color(33, 41, 25, 255, true),
             new Color(41, 49, 33, 255, true), new Color(49, 58, 41, 255, true),
             new Color(58, 66, 49, 255, true), new Color(66, 74, 58, 255, true),
             new Color(74, 82, 66, 255, true), new Color(82, 90, 74, 255, true),
             new Color(90, 99, 82, 255, true), new Color(99, 107, 90, 255, true),
             new Color(107, 115, 99, 255, true), new Color(115, 123, 107, 255, true),
             new Color(123, 132, 115, 255, true), new Color(132, 140, 123, 255, true),
             new Color(140, 148, 132, 255, true), new Color(148, 156, 140, 255, true)};
        private readonly Color[] _colorsHardcoded2 =
            {new Color(66, 99, 0, 0, true), new Color(49, 49, 82, 255, true),
             new Color(239, 230, 255, 255, true), new Color(214, 156, 255, 255, true),
             new Color(66, 99, 0, 255, true), new Color(82, 49, 49, 255, true),
             new Color(255, 107, 140, 255, true), new Color(255, 49, 107, 255, true),
             new Color(66, 99, 0, 255, true), new Color(49, 74, 49, 255, true),
             new Color(230, 255, 132, 255, true), new Color(189, 255, 0, 255, true),
             new Color(66, 99, 0, 255, true), new Color(214, 82, 148, 255, true),
             new Color(255, 148, 206, 255, true), new Color(247, 230, 255, 255, true)};

        public bool BankHasPalettes;
        public Palette[] Palettes;
        private readonly byte[] _data;
        public int TileAmount;
        private int _tileOffset;
        public int TileOffset => _tileOffset;
        public int CompressedDataSize { get; set; }

        public GraphicBank(byte[] data, bool bankHasPalettes = false)
        {
            _data = data;
            BankHasPalettes = bankHasPalettes;
            Palettes = null;
            if ((BankHasPalettes) && (data.Length > 0)) {
                Palettes = GetPalettesFromBankData();
                // Two hardcoded palettes, not sure where they are in ROM yet
                // or why they are part of tilemap's palette selection
                Palette hardcodedPalette1 = new Palette(true);
                Palette hardcodedPalette2 = new Palette(true);
                for (int x = 0; x < PaletteColorAmount; x++) {
                    hardcodedPalette1.Append(_colorsHardcoded1[x]);
                    hardcodedPalette2.Append(_colorsHardcoded2[x]);
                }
                Palettes[15] = hardcodedPalette1;
                Palettes[16] = hardcodedPalette2;
            }
            TileAmount = 1024;
        }

        public byte[] GetPlanarTileFromBankData(int tileNumber)
        {
            int offset = 0;
            if (BankHasPalettes) {
                offset = 0x1E0;
            }
            offset += tileNumber * 0x20;
            byte[] planarTile = new byte[0x20];
            Array.ConstrainedCopy(_data, offset, planarTile, 0, 0x20);
            return planarTile;
        }

        public void ResetTileOffset()
        {
            _tileOffset = 0;
        }

        public PlanarTilesWithOffset GetPlanarTilesFromBankData(TileIndex tileIndex, int offset = 0)
        {
            List<byte> tiles = new List<byte>();
            int tileNumber = 0;
            for (int xx = 0; xx < TileAmount; xx++) {
                if ((offset + xx) > tileIndex.MaxIndexAmount) {
                    break;
                }
                bool bit = tileIndex[offset + xx];
                if (bit) {
                    byte[] tile = GetPlanarTileFromBankData(tileNumber);
                    tiles.AddRange(tile);
                    _tileOffset += 1;
                }
                tileNumber += 1;
            }
            return new PlanarTilesWithOffset(tiles, tileNumber);
        }

        public byte[] GetTileArgBarray(int tileNumber, byte paletteNumber)
        {
            byte[] planarTileData = GetPlanarTileFromBankData(tileNumber);
            byte[] linearTileData = TileEditor.ConvertPlanarTileToLinearTile(planarTileData);
            Color[] coloredTileData = TileEditor.ColorLinearTileWithPalette(linearTileData, Palettes[paletteNumber]);
            return TileEditor.GetArgbArrayFromColoredLinearTile(coloredTileData);
        }

        public Bitmap GetTileImage(int tileNumber, byte paletteNumber, int tileWidth)
        {
            byte[] tiledata = GetTileArgBarray(tileNumber, paletteNumber);
            Bitmap img = new Bitmap(tileWidth, tileWidth, PixelFormat.Format32bppArgb);
            int pointer = 0;
            for (int y = 0; y < img.Height; y++) {
                for (int x = 0; x < img.Width; x++) {
                    var col = System.Drawing.Color.FromArgb(tiledata[pointer], tiledata[pointer + 1], 
                        tiledata[pointer + 2], tiledata[pointer + 3]);
                    pointer += 4;
                    img.SetPixel(x, y, col);
                }
            }
            //BitmapData imgData = img.LockBits(new Rectangle(0, 0, img.Width, img.Height),
            //                                  ImageLockMode.WriteOnly, img.PixelFormat);
            //IntPtr pointer = imgData.Scan0;
            //System.Runtime.InteropServices.Marshal.Copy(tiledata, 0, pointer,
            //                                            TileDrawer.TILE_WIDTH * GraphicBank.TILE_HEIGHT * 2);
            //img.UnlockBits(imgData);
            return img;
        }

        private Palette[] GetPalettesFromBankData(int extraPalettes = 2)
        {
            Palette[] paletteList = new Palette[15 + extraPalettes];
            int pointer = 0;
            for (int palNum = 0; palNum < PaletteAmount; palNum++) {
                Palette pal = new Palette();
                for (int colorNum = 0; colorNum < PaletteColorAmount; colorNum++) {
                    byte b = (byte)((_data[pointer + 1] & 0x7C) >> 2);
                    byte g = (byte)(((_data[pointer + 1] & 0x03) << 3) + ((_data[pointer] & 0xE0) >> 5));
                    byte r = (byte)(_data[pointer] & 0x1F);
                    Color col = new Color(r, g, b);
                    if (colorNum == 0) {
                        // Color 0 of a palette is assumed to be transparent for the game
                        col.Alpha = 0;
                    }
                    pal.Colors.Add(col);
                    pointer += 2;
                }
                pal.Type = true;
                paletteList[palNum] = pal;
            }
            return paletteList;
        }
    }
}

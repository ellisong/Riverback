using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace Riverback
{
    public class PlanarTilesWithOffset
    {
        public List<byte> planarTiles;
        public int offset;

        public PlanarTilesWithOffset(List<byte> planarTiles, int offset)
        {
            this.planarTiles = planarTiles;
            this.offset = offset;
        }
    }

    public class GraphicBank
    {
        public const int PALETTE_AMOUNT = 15;
        public const int PALETTE_COLOR_AMOUNT = 16;
        private Color[] COLORS_HARDCODED_1 =
            {new Color(25, 33, 16, 0, true), new Color(33, 41, 25, 255, true),
             new Color(41, 49, 33, 255, true), new Color(49, 58, 41, 255, true),
             new Color(58, 66, 49, 255, true), new Color(66, 74, 58, 255, true),
             new Color(74, 82, 66, 255, true), new Color(82, 90, 74, 255, true),
             new Color(90, 99, 82, 255, true), new Color(99, 107, 90, 255, true),
             new Color(107, 115, 99, 255, true), new Color(115, 123, 107, 255, true),
             new Color(123, 132, 115, 255, true), new Color(132, 140, 123, 255, true),
             new Color(140, 148, 132, 255, true), new Color(148, 156, 140, 255, true)};
        private Color[] COLORS_HARDCODED_2 =
            {new Color(66, 99, 0, 0, true), new Color(49, 49, 82, 255, true),
             new Color(239, 230, 255, 255, true), new Color(214, 156, 255, 255, true),
             new Color(66, 99, 0, 255, true), new Color(82, 49, 49, 255, true),
             new Color(255, 107, 140, 255, true), new Color(255, 49, 107, 255, true),
             new Color(66, 99, 0, 255, true), new Color(49, 74, 49, 255, true),
             new Color(230, 255, 132, 255, true), new Color(189, 255, 0, 255, true),
             new Color(66, 99, 0, 255, true), new Color(214, 82, 148, 255, true),
             new Color(255, 148, 206, 255, true), new Color(247, 230, 255, 255, true)};

        public bool bankHasPalettes;
        public Palette[] palettes;
        private byte[] data;
        public int tileAmount;
        private int tileOffset;
        public int TileOffset{ get { return tileOffset; } }
        public int CompressedDataSize { get; set; }

        public GraphicBank(byte[] data, bool bankHasPalettes = false)
        {
            this.data = data;
            this.bankHasPalettes = bankHasPalettes;
            palettes = null;
            if ((this.bankHasPalettes) && (data.Length > 0)) {
                palettes = getPalettesFromBankData();
                // Two hardcoded palettes, not sure where they are in ROM yet
                // or why they are part of tilemap's palette selection
                Palette hardcodedPalette1 = new Palette(true);
                Palette hardcodedPalette2 = new Palette(true);
                for (int x = 0; x < PALETTE_COLOR_AMOUNT; x++) {
                    hardcodedPalette1.append(COLORS_HARDCODED_1[x]);
                    hardcodedPalette2.append(COLORS_HARDCODED_2[x]);
                }
                palettes[15] = hardcodedPalette1;
                palettes[16] = hardcodedPalette2;
            }
            tileAmount = 1024;
        }

        public byte[] getPlanarTileFromBankData(int tileNumber)
        {
            int offset = 0;
            if (bankHasPalettes) {
                offset = 0x1E0;
            }
            offset += tileNumber * 0x20;
            byte[] planarTile = new byte[0x20];
            Array.ConstrainedCopy(data, offset, planarTile, 0, 0x20);
            return planarTile;
        }

        public void resetTileOffset()
        {
            tileOffset = 0;
        }

        public PlanarTilesWithOffset getPlanarTilesFromBankData(List<bool> tileIndex, int offset = 0)
        {
            List<byte> tiles = new List<byte>();
            int tileNumber = 0;
            for (int xx = 0; xx < tileAmount; xx++) {
                if ((offset + xx) > tileIndex.Count)
                    break;
                bool bit = tileIndex[offset + xx];
                if (bit) {
                    byte[] tile = getPlanarTileFromBankData(tileNumber);
                    tiles.AddRange(tile);
                    tileOffset += 1;
                }
                tileNumber += 1;
            }
            return new PlanarTilesWithOffset(tiles, tileNumber);
        }

        public byte[] getTileARGBarray(int tileNumber, byte paletteNumber)
        {
            byte[] planarTileData = getPlanarTileFromBankData(tileNumber);
            byte[] linearTileData = TileEditor.convertPlanarTileToLinearTile(planarTileData);
            Color[] coloredTileData = TileEditor.colorLinearTileWithPalette(linearTileData, palettes[paletteNumber]);
            return TileEditor.getARGBarrayFromColoredLinearTile(coloredTileData);
        }

        public Bitmap getTileImage(int tileNumber, byte paletteNumber, int tileWidth)
        {
            byte[] tiledata = getTileARGBarray(tileNumber, paletteNumber);
            Bitmap img = new Bitmap(tileWidth, tileWidth, PixelFormat.Format32bppArgb);
            int pointer = 0;
            for (int y = 0; y < img.Height; y++) {
                for (int x = 0; x < img.Width; x++) {
                    System.Drawing.Color col;
                    col = System.Drawing.Color.FromArgb(tiledata[pointer], tiledata[pointer + 1], 
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

        private Palette[] getPalettesFromBankData(int extraPalettes = 2)
        {
            Palette[] paletteList = new Palette[15 + extraPalettes];
            int pointer = 0;
            for (int palNum = 0; palNum < PALETTE_AMOUNT; palNum++) {
                Palette pal = new Palette(false);
                for (int colorNum = 0; colorNum < PALETTE_COLOR_AMOUNT; colorNum++) {
                    byte B = (byte)((data[pointer + 1] & 0x7C) >> 2);
                    byte G = (byte)(((data[pointer + 1] & 0x03) << 3) + ((data[pointer] & 0xE0) >> 5));
                    byte R = (byte)(data[pointer] & 0x1F);
                    Color col = new Color(R, G, B, 255, false);
                    if (colorNum == 0)
                        // Color 0 of a palette is assumed to be transparent for the game
                        col.Alpha = 0;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Riverback
{
    public static class TileDrawer
    {
        public const int LEVEL_CANVAS_WIDTH = 512;
        public const int LEVEL_CANVAS_WIDTH_TILEAMOUNT = LEVEL_CANVAS_WIDTH / GraphicBank.TILE_WIDTH;
        public const int LEVEL_CANVAS_HEIGHT = 512;
        public const int LEVEL_TILESET_WIDTH = 128;
        public const int LEVEL_TILESET_WIDTH_TILEAMOUNT = LEVEL_TILESET_WIDTH / GraphicBank.TILE_WIDTH;
        public const int LEVEL_TILESET_HEIGHT = 512;
        // shift and & constants
        const int AND_TILE_VFLIP = 0x80;
        const int AND_TILE_VFLIP_SHIFT = 7;
        const int AND_TILE_HFLIP = 0x40;
        const int AND_TILE_HFLIP_SHIFT = 6;
        const int AND_TILE_PRIORITY = 0x20;
        const int AND_TILE_PRIORITY_SHIFT = 5;
        const int AND_TILE_PALETTE = 0x1C;
        const int AND_TILE_PALETTE_SHIFT = 2;
        const int AND_TILE_BANK = 0x03;
        const int AND_TILE_BANK_SHIFT = 0;

        public static void drawTileOnCanvas(GraphicBank bank, Graphics pictureBoxGraphics,
                                           int tileAmountWidth, int tileNumber, byte paletteNumber, float scale = 1.0f)
        {
            Bitmap tileImg = bank.getTileImage(tileNumber, paletteNumber);
            int x = GraphicBank.TILE_WIDTH * (tileNumber % tileAmountWidth);
            int y = GraphicBank.TILE_HEIGHT * (tileNumber / tileAmountWidth);
            pictureBoxGraphics.DrawImage(tileImg, x, y);
        }

        public static void drawTileOnTileSelectorCanvas(GraphicBank bank, Graphics pictureBoxGraphics,
                                                        int tileNumber, byte paletteNumber)
        {
            Bitmap tileImg = bank.getTileImage(tileNumber, paletteNumber);
            RectangleF sourceRect = new RectangleF(0, 0, GraphicBank.TILE_WIDTH, GraphicBank.TILE_HEIGHT);
            float scale = 8.0f;
            RectangleF destinationRect = new RectangleF(0, 0, GraphicBank.TILE_WIDTH * scale, 
                                                              GraphicBank.TILE_HEIGHT * scale);
            pictureBoxGraphics.DrawImage(tileImg, destinationRect, sourceRect, GraphicsUnit.Pixel);
        }

        public static void drawAllTilesOnCanvas(GraphicBank bank, Graphics pictureBoxGraphics,
                                               int tileAmountWidth, byte paletteNumber)
        {
            for (int tileNumber = 0; tileNumber < bank.tileAmount; tileNumber++) {
                drawTileOnCanvas(bank, pictureBoxGraphics, tileAmountWidth, tileNumber, paletteNumber);
            }
        }

        public static void drawLevelOnCanvas(Graphics pictureBoxGraphics, Level level, GraphicBank levelBank)
        {
            int levelPointer = 0;
            for (int y = 0; y < LEVEL_CANVAS_HEIGHT; y += GraphicBank.TILE_HEIGHT) {
                for (int x = 0; x < LEVEL_CANVAS_WIDTH; x += GraphicBank.TILE_WIDTH) {
                    byte tile = level.Tilemap[levelPointer];
                    byte prop = level.Tilemap[levelPointer + 1];
                    levelPointer += 2;

                    int vflip = ((prop & AND_TILE_VFLIP) >> AND_TILE_VFLIP_SHIFT);
                    int hflip = ((prop & AND_TILE_HFLIP) >> AND_TILE_HFLIP_SHIFT);
                    int priority = ((prop & AND_TILE_PRIORITY) >> AND_TILE_PRIORITY_SHIFT);
                    int palette = ((prop & AND_TILE_PALETTE) >> AND_TILE_PALETTE_SHIFT);
                    int bank = ((prop & AND_TILE_BANK) >> AND_TILE_BANK_SHIFT);
                    Image tileImg = levelBank.getTileImage(tile + (bank * 256), (byte)(level.PaletteIndex[palette] - 1));
                    if ((hflip > 0) && (vflip > 0))
                        tileImg.RotateFlip(RotateFlipType.RotateNoneFlipXY);
                    else if (hflip > 0)
                        tileImg.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    else if (vflip > 0)
                        tileImg.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    pictureBoxGraphics.DrawImage(tileImg, x, y, GraphicBank.TILE_WIDTH, GraphicBank.TILE_HEIGHT);
                }
            }
        }

        public static int getTileNumberFromMouseCoordinates(int x, int y, int graphicWidth)
        {
            return (y / GraphicBank.TILE_HEIGHT * (graphicWidth / GraphicBank.TILE_WIDTH)) + (x / GraphicBank.TILE_WIDTH);
        }
    }
}

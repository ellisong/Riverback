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
        public const int LEVEL_TILEAMOUNT_WIDTH = 64;
        public const int LEVEL_TILEAMOUNT_HEIGHT = 64;
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
                                           int tileAmountWidth, int tileNumber, byte paletteNumber)
        {
            Bitmap tileImg = bank.getTileImage(tileNumber, paletteNumber);
            pictureBoxGraphics.DrawImage(tileImg, (GraphicBank.TILE_WIDTH * (tileNumber % tileAmountWidth)),
                                                  (GraphicBank.TILE_HEIGHT * (tileNumber / tileAmountWidth)));
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
            for (int y = 0; y < LEVEL_TILEAMOUNT_HEIGHT; y += GraphicBank.TILE_HEIGHT) {
                for (int x = 0; x < LEVEL_TILEAMOUNT_WIDTH; x += GraphicBank.TILE_WIDTH) {
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
    }
}

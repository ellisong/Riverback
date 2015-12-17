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
        public const int LEVEL_CANVAS_TILEAMOUNT_WIDTH = 64;
        public const int LEVEL_CANVAS_TILEAMOUNT_HEIGHT = 64;
        public const int LEVEL_TILESET_TILEAMOUNT_WIDTH = 16;
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
            int tileNum = 0;
            for (int y = 0; y < LEVEL_CANVAS_TILEAMOUNT_HEIGHT; y++) {
                for (int x = 0; x < LEVEL_CANVAS_TILEAMOUNT_WIDTH; x++) {
                    TilemapTile tile = level.Tilemap[y * LEVEL_CANVAS_TILEAMOUNT_WIDTH + x];
                    byte tileValue = tile.Tile;
                    tileNum += 1;
                    if (tileValue != 0) {
                        bool vflip = tile.VFlip;
                        bool hflip = tile.HFlip;
                        bool priority = tile.Priority;
                        Image tileImg = levelBank.getTileImage(tileValue + (tile.Bank * 256), 
                                                               (byte)(level.PaletteIndex[tile.Palette] - 1));
                        if ((tile.HFlip) && (tile.VFlip))
                            tileImg.RotateFlip(RotateFlipType.RotateNoneFlipXY);
                        else if (tile.HFlip)
                            tileImg.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        else if (tile.VFlip)
                            tileImg.RotateFlip(RotateFlipType.RotateNoneFlipY);
                        pictureBoxGraphics.DrawImage(tileImg, x * GraphicBank.TILE_WIDTH, y * GraphicBank.TILE_HEIGHT, 
                                                     GraphicBank.TILE_WIDTH, GraphicBank.TILE_HEIGHT);
                    }
                }
            }
        }

        public static int getTileNumberFromMouseCoordinates(int x, int y, int tileAmountWidth)
        {
            return (y / GraphicBank.TILE_HEIGHT * tileAmountWidth) + (x / GraphicBank.TILE_WIDTH);
        }
    }
}

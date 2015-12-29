using System.Drawing;

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

        

        public static void drawTileOnCanvas(GraphicBank bank, Graphics graphics, float x, float y, int bankTileNumber,
                                            byte paletteNumber, bool vflip, bool hflip, float scale = 1.0f)
        {
            Bitmap tileImg = bank.getTileImage(bankTileNumber, paletteNumber);
            if ((hflip) && (vflip))
                tileImg.RotateFlip(RotateFlipType.RotateNoneFlipXY);
            else if (hflip)
                tileImg.RotateFlip(RotateFlipType.RotateNoneFlipX);
            else if (vflip)
                tileImg.RotateFlip(RotateFlipType.RotateNoneFlipY);
            drawTileImageOnCanvas(tileImg, graphics, x, y, scale);
        }

        private static void drawTileImageOnCanvas(Bitmap tileImg, Graphics graphics, float x, float y, float scale = 1.0f)
        {
            RectangleF sourceRect = new RectangleF(0, 0, GraphicBank.TILE_WIDTH, GraphicBank.TILE_HEIGHT);
            RectangleF destinationRect = new RectangleF(x, y, GraphicBank.TILE_WIDTH * scale, GraphicBank.TILE_HEIGHT * scale);
            graphics.DrawImage(tileImg, destinationRect, sourceRect, GraphicsUnit.Pixel);
        }

        public static void drawAllTilesOnCanvas(GraphicBank bank, Graphics graphics, int tileAmountWidth, byte paletteNumber, float scale = 1.0f)
        {
            for (int bankTileNumber = 0; bankTileNumber < bank.tileAmount; bankTileNumber++) {
                Bitmap tileImg = bank.getTileImage(bankTileNumber, paletteNumber);
                float x = GraphicBank.TILE_WIDTH * (bankTileNumber % tileAmountWidth) * scale;
                float y = GraphicBank.TILE_HEIGHT * (bankTileNumber / tileAmountWidth) * scale;
                drawTileImageOnCanvas(tileImg, graphics, x, y, scale);
            }
        }

        public static void clearTileOnCanvas(Graphics graphics, Brush fillBrush, float x, float y, float scale = 1.0f)
        {
            RectangleF clearRect = new RectangleF(x, y, GraphicBank.TILE_WIDTH * scale, GraphicBank.TILE_HEIGHT * scale);
            graphics.FillRectangle(fillBrush, clearRect);
        }

        public static void drawLevelOnCanvas(Graphics graphics, Level level, GraphicBank levelBank)
        {
            int tileNum = 0;
            for (int y = 0; y < LEVEL_CANVAS_TILEAMOUNT_HEIGHT; y++) {
                for (int x = 0; x < LEVEL_CANVAS_TILEAMOUNT_WIDTH; x++) {
                    TilemapTile tile = level.Tilemap[y * LEVEL_CANVAS_TILEAMOUNT_WIDTH + x];
                    byte tileValue = tile.Tile;
                    tileNum += 1;
                    if ((tileValue != 0) || (tile.Bank != 0)) {
                        Image tileImg = levelBank.getTileImage(tileValue + (tile.Bank * 256), 
                                                               (byte)(level.PaletteIndex[tile.Palette] - 1));
                        if ((tile.HFlip) && (tile.VFlip))
                            tileImg.RotateFlip(RotateFlipType.RotateNoneFlipXY);
                        else if (tile.HFlip)
                            tileImg.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        else if (tile.VFlip)
                            tileImg.RotateFlip(RotateFlipType.RotateNoneFlipY);
                        graphics.DrawImage(tileImg, x * GraphicBank.TILE_WIDTH, y * GraphicBank.TILE_HEIGHT, 
                                                     GraphicBank.TILE_WIDTH, GraphicBank.TILE_HEIGHT);
                    }
                }
            }
        }
    }
}

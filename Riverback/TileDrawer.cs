using System.Drawing;

namespace Riverback
{
    public static class TileDrawer
    {
        public const int TILE_WIDTH = 8;
        
        public static void drawTileOnCanvas(Bitmap tileImg,
                                            Graphics graphics,
                                            float x,
                                            float y,
                                            bool vflip,
                                            bool hflip,
                                            float scale = 1.0f)
        {
            if ((hflip) && (vflip)) {
                tileImg.RotateFlip(RotateFlipType.RotateNoneFlipXY);
            } else if (hflip) {
                tileImg.RotateFlip(RotateFlipType.RotateNoneFlipX);
            } else if (vflip) {
                tileImg.RotateFlip(RotateFlipType.RotateNoneFlipY);
            }
            drawTileOnCanvas(tileImg, graphics, x, y, scale);
        }

        private static void drawTileOnCanvas(Bitmap tileImg, Graphics graphics, float x, float y, float scale)
        {
            RectangleF sourceRect = new RectangleF(0, 0, TILE_WIDTH, TILE_WIDTH);
            RectangleF destinationRect = new RectangleF(x, y, TILE_WIDTH * scale, TILE_WIDTH * scale);
            graphics.DrawImage(tileImg, destinationRect, sourceRect, GraphicsUnit.Pixel);
        }

        public static void drawTileFromImageOnCanvas(Image phystiles, 
                                                     Graphics graphics, 
                                                     Point srcPoint, 
                                                     Point destPoint, 
                                                     float scale = 1.0f)
        {
            RectangleF srcRect = new RectangleF(srcPoint.X, srcPoint.Y, TILE_WIDTH, TILE_WIDTH);
            RectangleF destRect = new RectangleF(destPoint.X, 
                                                 destPoint.Y, 
                                                 TILE_WIDTH * scale, 
                                                 TILE_WIDTH * scale);
            graphics.DrawImage(phystiles, destRect, srcRect, GraphicsUnit.Pixel);
        }

        public static void drawAllTilesOnCanvas(GraphicBank bank, 
                                                Graphics graphics, 
                                                int tileAmountWidth, 
                                                byte paletteNumber, 
                                                float scale = 1.0f)
        {
            for (int bankTileNumber = 0; bankTileNumber < bank.tileAmount; bankTileNumber++) {
                Bitmap tileImg = bank.getTileImage(bankTileNumber, paletteNumber, TILE_WIDTH);
                float x = TILE_WIDTH * (bankTileNumber % tileAmountWidth) * scale;
                float y = TILE_WIDTH * (bankTileNumber / tileAmountWidth) * scale;
                drawTileOnCanvas(tileImg, graphics, x, y, scale);
            }
        }

        public static void clearTileOnCanvas(Graphics graphics, Brush fillBrush, float x, float y, float scale = 1.0f)
        {
            RectangleF clearRect = new RectangleF(x, y, TILE_WIDTH * scale, TILE_WIDTH * scale);
            graphics.FillRectangle(fillBrush, clearRect);
        }

        public static void drawGridCellOnCanvas(Graphics graphics, float x, float y, float scale = 1.0f)
        {
            RectangleF sourceRect = new RectangleF(0, 0, TILE_WIDTH * scale, TILE_WIDTH * scale);
            RectangleF destinationRect = new RectangleF(x, y, TILE_WIDTH * scale, TILE_WIDTH * scale);
            Image img;
            if (scale == 2.0f) {
                img = Properties.Resources.gridtile16;
            } else {
                img = Properties.Resources.gridtile8;
            }
            graphics.DrawImage(img, destinationRect, sourceRect, GraphicsUnit.Pixel);
        }

        public static void drawLevelOnCanvas(Graphics graphics, 
                                             Image imagePhysTileset, 
                                             Level level, 
                                             GraphicBank levelBank, 
                                             int tileAmountWidth, 
                                             bool displayTilemapTiles = true, 
                                             bool displayPhysmapTiles = false)
        {
            int tileNum = 0;
            for (int y = 0; y < tileAmountWidth; y++) {
                for (int x = 0; x < tileAmountWidth; x++) {
                    TilemapTile tile = level.Tilemap[y * tileAmountWidth + x];
                    byte phys = level.Physmap[y * tileAmountWidth + x];
                    byte tileValue = tile.Tile;
                    tileNum += 1;
                    if (((tileValue != 0) || (tile.Bank != 0)) && (displayTilemapTiles)) {
                        Bitmap tileImg = levelBank.getTileImage(tile.Bank * 256 + tileValue,
                                                               (byte)(level.PaletteIndex[tile.Palette] - 1),
                                                               TILE_WIDTH);
                        drawTileOnCanvas(tileImg, graphics, x * TILE_WIDTH, y * TILE_WIDTH, tile.VFlip, tile.HFlip);
                    }
                    if ((phys != 0) && (displayPhysmapTiles)) {
                        Point srcCoords = new Point(phys % 16 * TILE_WIDTH,
                                                    phys / 16 * TILE_WIDTH);
                        Point destCoords = new Point(x * TILE_WIDTH, y * TILE_WIDTH);
                        drawTileFromImageOnCanvas(imagePhysTileset, graphics, srcCoords, destCoords);
                    }
                }
            }
        }
    }
}

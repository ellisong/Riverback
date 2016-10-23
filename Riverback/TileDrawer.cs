using System.Drawing;
using System.Drawing.Drawing2D;

namespace Riverback
{
    public static class TileDrawer
    {
        public const int TileWidth = 8;
        
        public static void DrawTileOnCanvas(Bitmap tileImg,
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
            DrawTileOnCanvas(tileImg, graphics, x, y, scale);
        }

        private static void DrawTileOnCanvas(Bitmap tileImg, Graphics graphics, float x, float y, float scale)
        {
            RectangleF sourceRect = new RectangleF(0, 0, TileWidth, TileWidth);
            RectangleF destinationRect = new RectangleF(x, y, TileWidth * scale, TileWidth * scale);
            graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            graphics.PixelOffsetMode = PixelOffsetMode.Half;
            graphics.DrawImage(tileImg, destinationRect, sourceRect, GraphicsUnit.Pixel);
        }

        public static void DrawTileFromImageOnCanvas(Image phystiles, 
                                                     Graphics graphics, 
                                                     Point srcPoint, 
                                                     Point destPoint, 
                                                     float srcScale = 1.0f,
                                                     float destScale = 1.0f)
        {
            RectangleF srcRect = new RectangleF(srcPoint.X, srcPoint.Y, TileWidth * srcScale, TileWidth * srcScale);
            RectangleF destRect = new RectangleF(destPoint.X, 
                                                 destPoint.Y, 
                                                 TileWidth * destScale, 
                                                 TileWidth * destScale);
            graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            graphics.PixelOffsetMode = PixelOffsetMode.Half;
            graphics.DrawImage(phystiles, destRect, srcRect, GraphicsUnit.Pixel);
        }

        public static void DrawAllTilesOnCanvas(GraphicBank bank, 
                                                Graphics graphics, 
                                                int tileAmountWidth, 
                                                byte paletteNumber, 
                                                float scale = 1.0f)
        {
            for (int bankTileNumber = 0; bankTileNumber < bank.TileAmount; bankTileNumber++) {
                Bitmap tileImg = bank.GetTileImage(bankTileNumber, paletteNumber, TileWidth);
                float x = TileWidth * (bankTileNumber % tileAmountWidth) * scale;
                float y = TileWidth * (bankTileNumber / tileAmountWidth) * scale;
                graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                graphics.PixelOffsetMode = PixelOffsetMode.Half;
                DrawTileOnCanvas(tileImg, graphics, x, y, scale);
            }
        }

        public static void ClearTileOnCanvas(Graphics graphics, Brush fillBrush, float x, float y, float scale = 1.0f)
        {
            RectangleF clearRect = new RectangleF(x, y, TileWidth * scale, TileWidth * scale);
            graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            graphics.PixelOffsetMode = PixelOffsetMode.Half;
            graphics.FillRectangle(fillBrush, clearRect);
        }

        public static void DrawGridCellOnCanvas(Graphics graphics, float x, float y, float scale = 1.0f)
        {
            RectangleF sourceRect = new RectangleF(0, 0, TileWidth * scale, TileWidth * scale);
            RectangleF destinationRect = new RectangleF(x, y, TileWidth * scale, TileWidth * scale);
            Image img = scale == 2.0f ? Properties.Resources.gridtile16 : Properties.Resources.gridtile8;
            graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            graphics.PixelOffsetMode = PixelOffsetMode.Half;
            graphics.DrawImage(img, destinationRect, sourceRect, GraphicsUnit.Pixel);
        }

        public static void DrawLevelOnCanvas(Graphics graphics, 
                                             Image imagePhysTileset, 
                                             LevelEditor levelEditor,
                                             int tileAmountWidth, 
                                             bool displayTilemapTiles = true, 
                                             bool displayPhysmapTiles = false,
                                             float scale = 1.0f)
        {
            for (int y = 0; y < tileAmountWidth; y++) {
                for (int x = 0; x < tileAmountWidth; x++) {
                    TilemapTile tile = levelEditor.Level.Tilemap[y * tileAmountWidth + x];
                    byte phys = levelEditor.Level.Physmap[y * tileAmountWidth + x];
                    int tileValue = levelEditor.Level.TileIndex.GetBankIndexTile(tile.Bank * 256 + tile.Tile);
                    if ((tileValue > 0) && (displayTilemapTiles)) {
                        GraphicBank bank = levelEditor.Banks[levelEditor.LevelHeader.GraphicsBankIndex * 2 + tileValue / 1024];
                        if (tileValue >= 1024) {
                            tileValue -= 1024;
                        }
                        Bitmap tileImg = bank.GetTileImage(tileValue, (byte)(levelEditor.Level.PaletteIndex[tile.Palette] - 1), TileWidth);
                        DrawTileOnCanvas(tileImg, graphics, x * TileWidth * scale, y * TileWidth * scale, tile.VFlip, tile.HFlip, scale);
                    }
                    if ((phys != 0) && displayPhysmapTiles) {
                        Point srcCoords = new Point((int)(phys % 16 * TileWidth * scale),
                                                    (int)(phys / 16 * TileWidth * scale));
                        Point destCoords = new Point((int)(x * TileWidth * scale), (int)(y * TileWidth * scale));
                        DrawTileFromImageOnCanvas(imagePhysTileset, graphics, srcCoords, destCoords, scale, scale);
                    }
                }
            }
        }
    }
}

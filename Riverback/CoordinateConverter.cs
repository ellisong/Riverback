using System.Drawing;

namespace Riverback
{
    public static class CoordinateConverter
    {
        public static int getTileNumberFromMouseCoords(Point mouseCoords, int tileAmountWidth, int scale = 1)
        {
            Point tileCoords = getTileCoordsFromMouseCoords(mouseCoords, scale);
            return getTileNumberFromTileCoords(tileCoords, tileAmountWidth);
        }

        public static Point getMouseCoordsFromTileNumber(int tileNumber, int tileAmountWidth, int scale = 1)
        {
            Point tileCoords = getTileCoordsFromTileNumber(tileNumber, tileAmountWidth);
            return getMouseCoordsFromTileCoords(tileCoords, scale);
        }

        public static Point getTileCoordsFromMouseCoords(Point mouseCoords, int scale = 1)
        {
            Point tileCoords = new Point();
            tileCoords.X = mouseCoords.X / (GraphicBank.TILE_WIDTH * scale);
            tileCoords.Y = mouseCoords.Y / (GraphicBank.TILE_HEIGHT * scale);
            return tileCoords;
        }

        public static int getTileNumberFromTileCoords(Point tileCoords, int tileAmountWidth)
        {
            return (tileCoords.Y * tileAmountWidth + tileCoords.X);
        }

        public static Point getMouseCoordsFromTileCoords(Point tileCoords, int scale = 1)
        {
            Point mouseCoords = new Point();
            mouseCoords.X = tileCoords.X * (GraphicBank.TILE_WIDTH * scale);
            mouseCoords.Y = tileCoords.Y * (GraphicBank.TILE_HEIGHT * scale);
            return mouseCoords;
        }

        public static Point getTileCoordsFromTileNumber(int tileNumber, int tileAmountWidth)
        {
            Point tileCoords = new Point();
            tileCoords.X = tileNumber % tileAmountWidth;
            tileCoords.Y = tileNumber / tileAmountWidth;
            return tileCoords;
        }
    }
}

using System.Drawing;

namespace Riverback
{
    public static class CoordinateConverter
    {
        public static int getTileNumberFromMouseCoords(Point mouseCoords, int tileAmountWidth, int tileWidth)
        {
            Point tileCoords = getTileCoordsFromMouseCoords(mouseCoords, tileWidth);
            return getTileNumberFromTileCoords(tileCoords, tileAmountWidth);
        }

        public static Point getMouseCoordsFromTileNumber(int tileNumber, int tileAmountWidth, int tileWidth)
        {
            Point tileCoords = getTileCoordsFromTileNumber(tileNumber, tileAmountWidth);
            return getMouseCoordsFromTileCoords(tileCoords, tileWidth);
        }

        public static Point getTileCoordsFromMouseCoords(Point mouseCoords, int tileWidth)
        {
            Point tileCoords = new Point();
            tileCoords.X = mouseCoords.X / tileWidth;
            tileCoords.Y = mouseCoords.Y / tileWidth;
            return tileCoords;
        }

        public static int getTileNumberFromTileCoords(Point tileCoords, int tileAmountWidth)
        {
            return (tileCoords.Y * tileAmountWidth + tileCoords.X);
        }

        public static Point getMouseCoordsFromTileCoords(Point tileCoords, int tileWidth)
        {
            Point mouseCoords = new Point();
            mouseCoords.X = tileCoords.X * tileWidth;
            mouseCoords.Y = tileCoords.Y * tileWidth;
            return mouseCoords;
        }

        public static Point getTileCoordsFromTileNumber(int tileNumber, int tileAmountWidth)
        {
            Point tileCoords = new Point();
            tileCoords.X = tileNumber % tileAmountWidth;
            tileCoords.Y = tileNumber / tileAmountWidth;
            return tileCoords;
        }

        public static Rectangle getMouseCoordsFromRectangleTileCoords(Rectangle rect, int tileWidth)
        {
            Rectangle mouseCoords = new Rectangle();
            mouseCoords.X = rect.X * tileWidth;
            mouseCoords.Y = rect.Y * tileWidth;
            mouseCoords.Width = (rect.Width + 1) * tileWidth;
            mouseCoords.Height = (rect.Height + 1) * tileWidth;
            return mouseCoords;
        }
    }
}

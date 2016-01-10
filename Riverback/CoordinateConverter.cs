using System.Drawing;

namespace Riverback
{
    public class CoordinateConverter
    {
        public int TileAmountWidth { get; set; }
        public int TileWidth { get; set; }

        public CoordinateConverter() {}

        public CoordinateConverter(int tileAmountWidth, int tileWidth)
        {
            TileAmountWidth = tileAmountWidth;
            TileWidth = tileWidth;
        }

        public int getTileNumberFromMouseCoords(Point mouseCoords)
        {
            Point tileCoords = getTileCoordsFromMouseCoords(mouseCoords);
            return getTileNumberFromTileCoords(tileCoords);
        }

        public Point getMouseCoordsFromTileNumber(int tileNumber)
        {
            Point tileCoords = getTileCoordsFromTileNumber(tileNumber);
            return getMouseCoordsFromTileCoords(tileCoords);
        }

        public Point getTileCoordsFromMouseCoords(Point mouseCoords)
        {
            Point tileCoords = new Point();
            tileCoords.X = mouseCoords.X / TileWidth;
            tileCoords.Y = mouseCoords.Y / TileWidth;
            return tileCoords;
        }

        public int getTileNumberFromTileCoords(Point tileCoords)
        {
            return (tileCoords.Y * TileAmountWidth + tileCoords.X);
        }

        public Point getMouseCoordsFromTileCoords(Point tileCoords)
        {
            Point mouseCoords = new Point();
            mouseCoords.X = tileCoords.X * TileWidth;
            mouseCoords.Y = tileCoords.Y * TileWidth;
            return mouseCoords;
        }

        public Point getTileCoordsFromTileNumber(int tileNumber)
        {
            Point tileCoords = new Point();
            tileCoords.X = tileNumber % TileAmountWidth;
            tileCoords.Y = tileNumber / TileAmountWidth;
            return tileCoords;
        }

        public Rectangle getMouseCoordsFromRectCoords(Rectangle rect)
        {
            Rectangle mouseCoords = new Rectangle();
            mouseCoords.X = rect.X * TileWidth;
            mouseCoords.Y = rect.Y * TileWidth;
            mouseCoords.Width = (rect.Width + 1) * TileWidth;
            mouseCoords.Height = (rect.Height + 1) * TileWidth;
            return mouseCoords;
        }
    }
}

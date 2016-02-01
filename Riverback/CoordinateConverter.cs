using System.Drawing;

namespace Riverback
{
    public class CoordinateConverter
    {
        public int TileAmountWidth { get; set; }
        public int TileAmountHeight { get; set; }
        public int TileWidth { get; set; }

        public CoordinateConverter() {}

        public CoordinateConverter(int tileAmountWidth, int tileAmountHeight, int tileWidth)
        {
            TileAmountWidth = tileAmountWidth;
            TileAmountHeight = tileAmountHeight;
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
			Point tempCoords = checkMouseCoords(mouseCoords);
            Point tileCoords = new Point();
			tileCoords.X = tempCoords.X / TileWidth;
			tileCoords.Y = tempCoords.Y / TileWidth;
            return tileCoords;
        }

        public int getTileNumberFromTileCoords(Point tileCoords)
        {
			Point tempCoords = checkTileCoords(tileCoords);
			return (tempCoords.Y * TileAmountWidth + tempCoords.X);
        }

        public Point getMouseCoordsFromTileCoords(Point tileCoords)
        {
			Point tempCoords = checkTileCoords(tileCoords);
            Point mouseCoords = new Point();
			mouseCoords.X = tempCoords.X * TileWidth;
			mouseCoords.Y = tempCoords.Y * TileWidth;
            return mouseCoords;
        }

        public Point getTileCoordsFromTileNumber(int tileNumber)
        {
			int tempNum = checkTileNum(tileNumber);
            Point tileCoords = new Point();
			tileCoords.X = tempNum % TileAmountWidth;
			tileCoords.Y = tempNum / TileAmountWidth;
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

		private Point checkMouseCoords(Point mouseCoords)
		{
			Point tempCoords = new Point(mouseCoords.X, mouseCoords.Y);
			if (tempCoords.X < 0)
				tempCoords.X = 0;
			else if (tempCoords.X >= TileAmountWidth * TileWidth)
				tempCoords.X = TileAmountWidth * TileWidth - 1;

			if (tempCoords.Y < 0)
				tempCoords.Y = 0;
			else if (tempCoords.Y >= TileAmountHeight * TileWidth)
				tempCoords.Y = TileAmountHeight * TileWidth - 1;

			return tempCoords;
		}

		private Point checkTileCoords(Point tileCoords)
		{
			Point tempCoords = new Point(tileCoords.X, tileCoords.Y);
			if (tempCoords.X < 0)
				tempCoords.X = 0;
			else if (tempCoords.X >= TileAmountWidth)
				tempCoords.X = TileAmountWidth - 1;

			if (tempCoords.Y < 0)
				tempCoords.Y = 0;
			else if (tempCoords.Y >= TileAmountHeight)
				tempCoords.Y = TileAmountHeight - 1;

			return tempCoords;
		}

		private int checkTileNum(int tileNum)
		{
			if (tileNum > TileAmountWidth * TileAmountHeight)
				return TileAmountWidth * TileAmountHeight;
			if (tileNum < 0)
				return 0;
			return tileNum;
		}
    }
}

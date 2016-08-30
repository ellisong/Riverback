using System.Drawing;

namespace Riverback
{
	public class CoordinateConverter
	{
		public int TileAmountWidth { get; set; }
		public int TileAmountHeight { get; set; }
		public int TileWidth { get; set; }

		public CoordinateConverter() { }

		public CoordinateConverter(int tileAmountWidth, int tileAmountHeight, int tileWidth)
		{
			TileAmountWidth = tileAmountWidth;
			TileAmountHeight = tileAmountHeight;
			TileWidth = tileWidth;
		}

		public int GetTileNumberFromMouseCoords(Point mouseCoords)
		{
			Point tileCoords = GetTileCoordsFromMouseCoords(mouseCoords);
			return GetTileNumberFromTileCoords(tileCoords);
		}

		public Point GetMouseCoordsFromTileNumber(int tileNumber)
		{
			Point tileCoords = GetTileCoordsFromTileNumber(tileNumber);
			return GetMouseCoordsFromTileCoords(tileCoords);
		}

		public Point GetTileCoordsFromMouseCoords(Point mouseCoords)
		{
			Point tempCoords = CheckMouseCoords(mouseCoords);
		    Point tileCoords = new Point
		    {
		        X = tempCoords.X / TileWidth,
		        Y = tempCoords.Y / TileWidth
		    };
		    return tileCoords;
		}

		public int GetTileNumberFromTileCoords(Point tileCoords)
		{
			Point tempCoords = CheckTileCoords(tileCoords);
			return (tempCoords.Y * TileAmountWidth + tempCoords.X);
		}

		public Point GetMouseCoordsFromTileCoords(Point tileCoords)
		{
			Point tempCoords = CheckTileCoords(tileCoords);
		    Point mouseCoords = new Point
		    {
		        X = tempCoords.X * TileWidth,
		        Y = tempCoords.Y * TileWidth
		    };
		    return mouseCoords;
		}

		public Point GetTileCoordsFromTileNumber(int tileNumber)
		{
			int tempNum = CheckTileNum(tileNumber);
		    Point tileCoords = new Point
		    {
		        X = tempNum % TileAmountWidth,
		        Y = tempNum / TileAmountWidth
		    };
		    return tileCoords;
		}

		public Rectangle GetMouseCoordsFromRectCoords(Rectangle rect)
		{
		    Rectangle mouseCoords = new Rectangle
		    {
		        X = rect.X * TileWidth,
		        Y = rect.Y * TileWidth,
		        Width = (rect.Width + 1) * TileWidth,
		        Height = (rect.Height + 1) * TileWidth
		    };
		    return mouseCoords;
		}

		private Point CheckMouseCoords(Point mouseCoords)
		{
			Point tempCoords = new Point(mouseCoords.X, mouseCoords.Y);
			if (tempCoords.X < 0) {
				tempCoords.X = 0;
			} else if (tempCoords.X >= TileAmountWidth * TileWidth) {
				tempCoords.X = TileAmountWidth * TileWidth - 1;
			}
			if (tempCoords.Y < 0) {
				tempCoords.Y = 0;
			} else if (tempCoords.Y >= TileAmountHeight * TileWidth) {
				tempCoords.Y = TileAmountHeight * TileWidth - 1;
			}
			return tempCoords;
		}

		private Point CheckTileCoords(Point tileCoords)
		{
			Point tempCoords = new Point(tileCoords.X, tileCoords.Y);
			if (tempCoords.X < 0) {
				tempCoords.X = 0;
			} else if (tempCoords.X >= TileAmountWidth) {
				tempCoords.X = TileAmountWidth - 1;
			}
			if (tempCoords.Y < 0) {
				tempCoords.Y = 0;
			} else if (tempCoords.Y >= TileAmountHeight) {
				tempCoords.Y = TileAmountHeight - 1;
			}
			return tempCoords;
		}

		private int CheckTileNum(int tileNum)
		{
			if (tileNum > TileAmountWidth * TileAmountHeight) {
				return TileAmountWidth * TileAmountHeight;
			}
			if (tileNum < 0) {
				return 0;
			}
			return tileNum;
		}
	}
}

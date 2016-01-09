using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riverback
{
    public class TileSelection<T>
    {
        public Point tileCoords;
        public T tile;

        public TileSelection(Point tileCoords, T tile)
        {
            this.tileCoords = tileCoords;
            this.tile = tile;
        }
    }

    public class TileSelector<T>
    {
        private const int UNUSED_COORD_NUMBER = -1;
        private Point tileCoordsStart;
        private Rectangle tileCoords;
        public Rectangle TileCoords { get { return tileCoords; } }
        private bool isSelecting;
        public bool IsSelecting { get { return isSelecting; } }
        private bool selected;
        public bool Selected { get { return selected; } }

        public TileSelector()
        {
            tileCoordsStart = new Point(UNUSED_COORD_NUMBER, UNUSED_COORD_NUMBER);
            tileCoords = new Rectangle();
        }

        public void selectStart(Point mouseCoords, int tileScale = 1)
        {
            if (isSelecting == false) {
                tileCoordsStart = CoordinateConverter.getTileCoordsFromMouseCoords(mouseCoords, TileDrawer.TILE_WIDTH);
                tileCoords.X = UNUSED_COORD_NUMBER + 1;
                tileCoords.Y = UNUSED_COORD_NUMBER + 1;
                tileCoords.Width = UNUSED_COORD_NUMBER;
                tileCoords.Height = UNUSED_COORD_NUMBER;
                isSelecting = true;
                selected = false;
            }
        }

        public void selectEnd(Point mouseCoords, int tileScale = 1)
        {
            if (isSelecting == true) {
                Point tileCoordsEnd = CoordinateConverter.getTileCoordsFromMouseCoords(mouseCoords, TileDrawer.TILE_WIDTH);
                Point topLeft = new Point(Math.Min(tileCoordsStart.X, tileCoordsEnd.X), 
                                          Math.Min(tileCoordsStart.Y, tileCoordsEnd.Y));
                Point bottomRight = new Point(Math.Max(tileCoordsStart.X, tileCoordsEnd.X), 
                                              Math.Max(tileCoordsStart.Y, tileCoordsEnd.Y));
                tileCoords.X = topLeft.X;
                tileCoords.Y = topLeft.Y;
                tileCoords.Width = bottomRight.X - topLeft.X;
                tileCoords.Height = bottomRight.Y - topLeft.Y;
                isSelecting = false;
                selected = true;
            }
        }

        public void clearSelection()
        {
            isSelecting = false;
            selected = false;
        }

        public List<TileSelection<T>> getTilesFromSelection(T[] tilemap, int tileAmountWidth)
        {
            var selectedTiles = new List<TileSelection<T>>();
            Point tempCoord = new Point();
            for (int y = tileCoords.Y; y <= (tileCoords.Y + tileCoords.Height); y++) {
                for (int x = tileCoords.X; x <= (tileCoords.X + tileCoords.Width); x++) {
                    tempCoord.X = x;
                    tempCoord.Y = y;
                    int tileNumber = CoordinateConverter.getTileNumberFromTileCoords(tempCoord, tileAmountWidth);
                    selectedTiles.Add(new TileSelection<T>(new Point(x - tileCoords.X, y - tileCoords.Y), 
                                                                     tilemap[tileNumber]));
                }
            }
            return selectedTiles;
        }
    }
}

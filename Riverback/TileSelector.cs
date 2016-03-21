using System;
using System.Collections.Generic;
using System.Drawing;

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

    public class TileSelector
    {
        private const int UNUSED_COORD_NUMBER = -1;
        private Point tileCoordsStart;
        private CoordinateConverter coordConverter;
        private Rectangle tileCoords;
        public Rectangle TileCoords { get { return tileCoords; } }
        private bool isSelecting;
        public bool IsSelecting { get { return isSelecting; } }
        private bool selected;
        public bool Selected { get { return selected; } }

        public TileSelector(CoordinateConverter coordConverter)
        {
            tileCoordsStart = new Point(UNUSED_COORD_NUMBER, UNUSED_COORD_NUMBER);
            tileCoords = new Rectangle();
            this.coordConverter = coordConverter;
        }

        public void selectStart(Point mouseCoords, int tileWidth, int tileScale = 1)
        {
            if (isSelecting == false) {
                tileCoordsStart = coordConverter.getTileCoordsFromMouseCoords(mouseCoords);
                tileCoords.X = UNUSED_COORD_NUMBER + 1;
                tileCoords.Y = UNUSED_COORD_NUMBER + 1;
                tileCoords.Width = UNUSED_COORD_NUMBER;
                tileCoords.Height = UNUSED_COORD_NUMBER;
                isSelecting = true;
                selected = false;
            }
        }

        public void selectEnd(Point mouseCoords, int tileWidth, int tileScale = 1)
        {
            if (isSelecting == true) {
                Point tileCoordsEnd = coordConverter.getTileCoordsFromMouseCoords(mouseCoords);
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

        public bool isPointInSelection(Point tileCoord)
        {
            if (Selected) {
                if ((tileCoord.X >= tileCoords.X) && (tileCoord.X < tileCoords.X + tileCoords.Width)) {
                    return true;
                }
                if ((tileCoord.Y >= tileCoords.Y) && (tileCoord.Y < tileCoords.Y + tileCoords.Height)) {
                    return true;
                }
            }
            return false;
        }

        public List<TileSelection<TilemapTile>> getTilesFromSelection(TilemapTile[] tilemap, int tileAmountWidth)
        {
            var selectedTiles = new List<TileSelection<TilemapTile>>();
            Point tempCoord = new Point();
            for (int y = tileCoords.Y; y <= (tileCoords.Y + tileCoords.Height); y++) {
                for (int x = tileCoords.X; x <= (tileCoords.X + tileCoords.Width); x++) {
                    tempCoord.X = x;
                    tempCoord.Y = y;
                    int tileNumber = coordConverter.getTileNumberFromTileCoords(tempCoord);
                    Point point = new Point(x - tileCoords.X, y - tileCoords.Y);
                    selectedTiles.Add(new TileSelection<TilemapTile>(point, tilemap[tileNumber]));
                }
            }
            return selectedTiles;
        }

        public List<TileSelection<byte>> getTilesFromSelection(byte[] physmap, int tileAmountWidth)
        {
            var selectedTiles = new List<TileSelection<byte>>();
            Point tempCoord = new Point();
            for (int y = tileCoords.Y; y <= (tileCoords.Y + tileCoords.Height); y++) {
                for (int x = tileCoords.X; x <= (tileCoords.X + tileCoords.Width); x++) {
                    tempCoord.X = x;
                    tempCoord.Y = y;
                    int tileNumber = coordConverter.getTileNumberFromTileCoords(tempCoord);
                    Point point = new Point(x - tileCoords.X, y - tileCoords.Y);
                    selectedTiles.Add(new TileSelection<byte>(point, physmap[tileNumber]));
                }
            }
            return selectedTiles;
        }
    }
}

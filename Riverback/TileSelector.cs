using System;
using System.Collections.Generic;
using System.Drawing;

namespace Riverback
{
    public class TileSelection<T>
    {
        public Point TileCoords;
        public T Tile;

        public TileSelection(Point tileCoords, T tile)
        {
            TileCoords = tileCoords;
            Tile = tile;
        }
    }

    public class TileSelector
    {
        private const int UnusedCoordNumber = -1;
        private Point _tileCoordsStart;
        private readonly CoordinateConverter _coordConverter;
        private Rectangle _tileCoords;
        public Rectangle TileCoords => _tileCoords;
        private bool _isSelecting;
        public bool IsSelecting => _isSelecting;
        private bool _selected;
        public bool Selected => _selected;

        public TileSelector(CoordinateConverter coordConverter)
        {
            _tileCoordsStart = new Point(UnusedCoordNumber, UnusedCoordNumber);
            _tileCoords = new Rectangle();
            _coordConverter = coordConverter;
        }

        public void SelectStart(Point mouseCoords, int tileWidth, int tileScale = 1)
        {
            if (_isSelecting == false) {
                _tileCoordsStart = _coordConverter.GetTileCoordsFromMouseCoords(mouseCoords);
                _tileCoords.X = UnusedCoordNumber + 1;
                _tileCoords.Y = UnusedCoordNumber + 1;
                _tileCoords.Width = UnusedCoordNumber;
                _tileCoords.Height = UnusedCoordNumber;
                _isSelecting = true;
                _selected = false;
            }
        }

        public void SelectEnd(Point mouseCoords, int tileWidth, int tileScale = 1)
        {
            if (_isSelecting) {
                Point tileCoordsEnd = _coordConverter.GetTileCoordsFromMouseCoords(mouseCoords);
                Point topLeft = new Point(Math.Min(_tileCoordsStart.X, tileCoordsEnd.X), 
                                          Math.Min(_tileCoordsStart.Y, tileCoordsEnd.Y));
                Point bottomRight = new Point(Math.Max(_tileCoordsStart.X, tileCoordsEnd.X), 
                                              Math.Max(_tileCoordsStart.Y, tileCoordsEnd.Y));
                _tileCoords.X = topLeft.X;
                _tileCoords.Y = topLeft.Y;
                _tileCoords.Width = bottomRight.X - topLeft.X;
                _tileCoords.Height = bottomRight.Y - topLeft.Y;
                _isSelecting = false;
                _selected = true;
            }
        }

        public void ClearSelection()
        {
            _isSelecting = false;
            _selected = false;
        }

        public bool IsPointInSelection(Point tileCoord)
        {
            if (Selected) {
                if ((tileCoord.X >= _tileCoords.X) && (tileCoord.X < _tileCoords.X + _tileCoords.Width)) {
                    return true;
                }
                if ((tileCoord.Y >= _tileCoords.Y) && (tileCoord.Y < _tileCoords.Y + _tileCoords.Height)) {
                    return true;
                }
            }
            return false;
        }

        public List<TileSelection<TilemapTile>> GetTilesFromSelection(TilemapTile[] tilemap, int tileAmountWidth)
        {
            var selectedTiles = new List<TileSelection<TilemapTile>>();
            Point tempCoord = new Point();
            for (int y = _tileCoords.Y; y <= (_tileCoords.Y + _tileCoords.Height); y++) {
                for (int x = _tileCoords.X; x <= (_tileCoords.X + _tileCoords.Width); x++) {
                    tempCoord.X = x;
                    tempCoord.Y = y;
                    int tileNumber = _coordConverter.GetTileNumberFromTileCoords(tempCoord);
                    Point point = new Point(x - _tileCoords.X, y - _tileCoords.Y);
                    selectedTiles.Add(new TileSelection<TilemapTile>(point, tilemap[tileNumber]));
                }
            }
            return selectedTiles;
        }

        public List<TileSelection<byte>> GetTilesFromSelection(byte[] physmap, int tileAmountWidth)
        {
            var selectedTiles = new List<TileSelection<byte>>();
            Point tempCoord = new Point();
            for (int y = _tileCoords.Y; y <= (_tileCoords.Y + _tileCoords.Height); y++) {
                for (int x = _tileCoords.X; x <= (_tileCoords.X + _tileCoords.Width); x++) {
                    tempCoord.X = x;
                    tempCoord.Y = y;
                    int tileNumber = _coordConverter.GetTileNumberFromTileCoords(tempCoord);
                    Point point = new Point(x - _tileCoords.X, y - _tileCoords.Y);
                    selectedTiles.Add(new TileSelection<byte>(point, physmap[tileNumber]));
                }
            }
            return selectedTiles;
        }
    }
}

using System;
using Riverback.Properties;

namespace Riverback
{
    public class TilemapTile
    {
        // shift and & constants
        const int AndTileVflip = 0x80;
        const int AndTileVflipShift = 7;
        const int AndTileHflip = 0x40;
        const int AndTileHflipShift = 6;
        const int AndTilePriority = 0x20;
        const int AndTilePriorityShift = 5;
        const int AndTilePalette = 0x1C;
        const int AndTilePaletteShift = 2;
        const int AndTileBank = 0x03;
        const int AndTileBankShift = 0;

        public byte Tile { get; set; }
        public byte Property { get; set; }

        public bool VFlip
        {
            get
            {
                if (((Property & AndTileVflip) >> AndTileVflipShift) == 1) {
                    return true;
                }
                return false;
            }
            set
            {
                Property = (byte)(Property & (0xFF - AndTileVflip));
                if (value) {
                    Property = (byte)(Property + (1 << AndTileVflipShift));
                } else {
                    Property = (byte)(Property + (0 << AndTileVflipShift));
                }
            }
        }

        public bool HFlip
        {
            get
            {
                if (((Property & AndTileHflip) >> AndTileHflipShift) == 1) {
                    return true;
                }
                return false;
            }
            set
            {
                Property = (byte)(Property & (0xFF - AndTileHflip));
                if (value) {
                    Property = (byte)(Property + (1 << AndTileHflipShift));
                } else {
                    Property = (byte)(Property + (0 << AndTileHflipShift));
                }
            }
        }
        
        public bool Priority
        {
            get
            {
                if (((Property & AndTilePriority) >> AndTilePriorityShift) == 1) {
                    return true;
                }
                return false;
            }
            set
            {
                Property = (byte)(Property & (0xFF - AndTilePriority));
                if (value) {
                    Property = (byte)(Property + (1 << AndTilePriorityShift));
                } else {
                    Property = (byte)(Property + (0 << AndTilePriorityShift));
                }
            }
        }
        
        public byte Palette
        {
            get { return (byte)((Property & AndTilePalette) >> AndTilePaletteShift); }
            set
            {
                if (value < 8) {
                    Property = (byte)(Property & (0xFF - AndTilePalette));
                    Property = (byte)(Property + (value << AndTilePaletteShift));
                } else {
                    throw new ArgumentOutOfRangeException("TilesetTile.Palette",
                                                          value,
                                                          Resources.TilemapTile_Palette_OutOfRangeException);
                }
            }
        }
        
        public byte Bank
        {
            get { return (byte)((Property & AndTileBank) >> AndTileBankShift); }
            set
            {
                // Can hold two bits, but banks 2 and 3 are never used in-game for the tilemaps
                if (value < 2) {
                    Property = (byte)(Property & (0xFF - AndTileBank));
                    Property = (byte)(Property + (value << AndTileBankShift));
                } else {
                    throw new ArgumentOutOfRangeException("TilesetTile.Bank",
                                                          value,
                                                          Resources.TilemapTile_Bank_OutOfRangeException);
                }
            }
        }

        public TilemapTile()
        {

        }

        public TilemapTile(TilemapTile tile)
        {
            Tile = tile.Tile;
            Property = tile.Property;
        }

        public TilemapTile(int tileValue, bool vflip, bool hflip, bool priority, byte palette)
        {
            Bank = (byte)(tileValue / 256);
            Tile = (byte)(tileValue % 256);
            VFlip = vflip;
            HFlip = hflip;
            Priority = priority;
            Palette = palette;
        }

        public void SetTileFromLevelData(byte[] leveldata, int tileNumber)
        {
            Tile = leveldata[Level.LevelTileAmount + (tileNumber * 2)];
            Property = leveldata[Level.LevelTileAmount + (tileNumber * 2) + 1];
            VFlip = (Property & AndTileVflip) >> AndTileVflipShift == 1;
            HFlip = (Property & AndTileHflip) >> AndTileHflipShift == 1;
            Priority = (Property & AndTilePriority) >> AndTilePriorityShift == 1;
            Palette = (byte)((Property & AndTilePalette) >> AndTilePaletteShift);
            Bank = (byte)((Property & AndTileBank) >> AndTileBankShift);
        }

        public static TilemapTile[] GetAllLevelTilesFromLevelData(byte[] leveldata)
        {
            TilemapTile[] tiles = new TilemapTile[Level.LevelTileAmount];
            for (int tileNum = 0; tileNum < Level.LevelTileAmount; tileNum++) {
                TilemapTile tile = new TilemapTile();
                tile.SetTileFromLevelData(leveldata, tileNum);
                tiles[tileNum] = tile;
            }
            return tiles;
        }
    }
}

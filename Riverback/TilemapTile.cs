using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riverback
{
    public class TilemapTile
    {
        // shift and & constants
        const int AND_TILE_VFLIP = 0x80;
        const int AND_TILE_VFLIP_SHIFT = 7;
        const int AND_TILE_HFLIP = 0x40;
        const int AND_TILE_HFLIP_SHIFT = 6;
        const int AND_TILE_PRIORITY = 0x20;
        const int AND_TILE_PRIORITY_SHIFT = 5;
        const int AND_TILE_PALETTE = 0x1C;
        const int AND_TILE_PALETTE_SHIFT = 2;
        const int AND_TILE_BANK = 0x03;
        const int AND_TILE_BANK_SHIFT = 0;

        public byte Tile { get; set; }
        public byte Property { get; set; }

        public bool VFlip
        {
            get
            {
                if (((this.Property & AND_TILE_VFLIP) >> AND_TILE_VFLIP_SHIFT) == 1)
                    return true;
                return false;
            }
            set
            {
                this.Property = (byte)(Property & (0xFF - AND_TILE_VFLIP));
                if (value)
                    this.Property = (byte)(Property + (1 << AND_TILE_VFLIP_SHIFT));
                else
                    this.Property = (byte)(Property + (0 << AND_TILE_VFLIP_SHIFT));
            }
        }

        public bool HFlip
        {
            get
            {
                if (((this.Property & AND_TILE_HFLIP) >> AND_TILE_HFLIP_SHIFT) == 1)
                    return true;
                return false;
            }
            set
            {
                this.Property = (byte)(Property & (0xFF - AND_TILE_HFLIP));
                if (value)
                    this.Property = (byte)(Property + (1 << AND_TILE_HFLIP_SHIFT));
                else
                    this.Property = (byte)(Property + (0 << AND_TILE_HFLIP_SHIFT));
            }
        }
        
        public bool Priority
        {
            get
            {
                if (((this.Property & AND_TILE_PRIORITY) >> AND_TILE_PRIORITY_SHIFT) == 1)
                    return true;
                return false;
            }
            set
            {
                this.Property = (byte)(Property & (0xFF - AND_TILE_PRIORITY));
                if (value)
                    this.Property = (byte)(Property + (1 << AND_TILE_PRIORITY_SHIFT));
                else
                    this.Property = (byte)(Property + (0 << AND_TILE_PRIORITY_SHIFT));
            }
        }
        
        public byte Palette
        {
            get { return (byte)((this.Property & AND_TILE_PALETTE) >> AND_TILE_PALETTE_SHIFT); }
            set
            {
                if ((value < 8) && (value >= 0)) {
                    this.Property = (byte)(Property & (0xFF - AND_TILE_PALETTE));
                    this.Property = (byte)(Property + (value << AND_TILE_PALETTE_SHIFT));
                }
                else
                    throw new ArgumentOutOfRangeException("TilesetTile.Palette", 
                                                          value, 
                                                          "The argument for TilesetTile.Palette is out of range");
            }
        }
        
        public byte Bank
        {
            get { return (byte)((this.Property & AND_TILE_BANK) >> AND_TILE_BANK_SHIFT); }
            set
            {
                // Can hold two bits, but banks 2 and 3 are never used in-game for the tilemaps
                if ((value < 2) && (value >= 0)) {
                    this.Property = (byte)(Property & (0xFF - AND_TILE_BANK));
                    this.Property = (byte)(Property + (value << AND_TILE_BANK_SHIFT));
                }
                else
                    throw new ArgumentOutOfRangeException("TilesetTile.Bank", 
                                                          value, 
                                                          "The argument for TilesetTile.Bank is out of range");
            }
        }

        public TilemapTile()
        {

        }

        public TilemapTile(TilemapTile tile)
        {
            this.Tile = tile.Tile;
            this.Property = tile.Property;
        }

        public TilemapTile(int tileValue, bool vflip, bool hflip, bool priority, byte palette)
        {
			this.Bank = (byte)(tileValue / 256);
			this.Tile = (byte)(tileValue % 256);
            this.VFlip = vflip;
            this.HFlip = hflip;
            this.Priority = priority;
            this.Palette = palette;
        }

        public void setTileFromLevelData(byte[] leveldata, int tileNumber)
        {
            this.Tile = leveldata[Level.LEVEL_TILE_AMOUNT + (tileNumber * 2)];
            this.Property = leveldata[Level.LEVEL_TILE_AMOUNT + (tileNumber * 2) + 1];
            if (((this.Property & AND_TILE_VFLIP) >> AND_TILE_VFLIP_SHIFT) == 1)
                this.VFlip = true;
            else
                this.VFlip = false;
            if (((this.Property & AND_TILE_HFLIP) >> AND_TILE_HFLIP_SHIFT) == 1)
                this.HFlip = true;
            else
                this.HFlip = false;
            if (((this.Property & AND_TILE_PRIORITY) >> AND_TILE_PRIORITY_SHIFT) == 1)
                this.Priority = true;
            else
                this.Priority = false;
            this.Palette = (byte)((this.Property & AND_TILE_PALETTE) >> AND_TILE_PALETTE_SHIFT);
            this.Bank = (byte)((this.Property & AND_TILE_BANK) >> AND_TILE_BANK_SHIFT);
        }

        public static TilemapTile[] getAllLevelTilesFromLevelData(byte[] leveldata)
        {
            TilemapTile[] tiles = new TilemapTile[Level.LEVEL_TILE_AMOUNT];
            for (int tileNum = 0; tileNum < Level.LEVEL_TILE_AMOUNT; tileNum++) {
                TilemapTile tile = new TilemapTile();
                tile.setTileFromLevelData(leveldata, tileNum);
                tiles[tileNum] = tile;
            }
            return tiles;
        }
    }
}

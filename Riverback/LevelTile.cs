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

        public bool VFlip { get; set; }
        public bool HFlip { get; set; }
        public bool Priority { get; set; }
        private byte palette;
        public byte Palette
        {
            get { return palette; }
            set
            {
                if (value >= Level.LEVEL_PALETTE_INDEX_AMOUNT)
                    palette = Level.LEVEL_PALETTE_INDEX_AMOUNT - 1;
                else if (value < 0)
                    palette = 0;
                else
                    palette = value;
            }
        }
        private byte bank;
        public byte Bank
        {
            get { return bank; }
            set
            {
                if (value >= 4)
                    bank = 3;
                else if (value < 0)
                    bank = 0;
                else
                    bank = value;
            }
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

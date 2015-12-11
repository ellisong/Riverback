using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riverback
{
    public class Level
    {
        public const int LEVEL_TILE_AMOUNT = 4096;
        public const int LEVEL_TILE_INDEX_SIZE = 256;
        public const int LEVEL_PALETTE_INDEX_AMOUNT = 8;

        private LevelHeader levelHeader;
        public LevelHeader LevelHeader { get { return levelHeader; } }
        private byte[] physmap;
        public byte[] Physmap { get { return physmap; } }
        private byte[] tilemap;
        public byte[] Tilemap { get { return tilemap; } }
        private int tileIndexAmount;
        public int TileIndexAmount { get { return tileIndexAmount; } }
        private List<bool> tileIndex;
        public List<bool> TileIndex { get { return tileIndex; } }
        private byte[] paletteIndex;
        public byte[] PaletteIndex { get { return paletteIndex; } }

        public Level(LevelHeader levelHeader)
        {
            this.levelHeader = levelHeader;
            physmap = new byte[LEVEL_TILE_AMOUNT];
            tilemap = new byte[LEVEL_TILE_AMOUNT * 2];
            tileIndex = new List<bool>();
            paletteIndex = new byte[LEVEL_PALETTE_INDEX_AMOUNT];
        }

        public void update(byte[] levelData)
        {
            setPhysmap(levelData);
            setTilemap(levelData);
            setTileIndexAmount(levelData);
            setTileIndex(levelData);
            setPaletteIndex(levelData);
        }

        public void setPhysmap(byte[] levelData)
        {
            Array.ConstrainedCopy(levelData, 0, physmap, 0, LEVEL_TILE_AMOUNT);
        }

        public void setTilemap(byte[] levelData)
        {
            Array.ConstrainedCopy(levelData, LEVEL_TILE_AMOUNT, tilemap, 0, LEVEL_TILE_AMOUNT*2);
        }

        public void setTileIndexAmount(byte[] levelData)
        {
            int offset = LEVEL_TILE_AMOUNT * 3;
            tileIndexAmount = levelData[offset + 1] * 0x100 + levelData[offset];
        }

        public void setTileIndex(byte[] levelData)
        {
            int offset = LEVEL_TILE_AMOUNT * 3 + 2;
            List<byte> tileIndexBytes = new List<byte>();
            for (int index = 0; index < LEVEL_TILE_INDEX_SIZE; index++)
                tileIndexBytes.Add(levelData[offset + index]);
            tileIndex = DataFormatter.byteListIntoBitList(tileIndexBytes, false);
        }

        public void setPaletteIndex(byte[] levelData)
        {
            Array.ConstrainedCopy(levelData, LEVEL_TILE_AMOUNT * 3 + 2 + LEVEL_TILE_INDEX_SIZE, paletteIndex, 2, LEVEL_PALETTE_INDEX_AMOUNT - 2);
            paletteIndex[0] = 16;
            paletteIndex[1] = 17;
        }
    }
}

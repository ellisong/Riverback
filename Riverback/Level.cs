using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riverback
{
    class Level
    {
        public const int LEVEL_TILE_AMOUNT = 4096;
        public const int LEVEL_TILE_INDEX_SIZE = 256;
        public const int LEVEL_PALETTE_INDEX_AMOUNT = 8;

        LevelHeader levelHeader;
        byte[] physmap;
        byte[] tilemap;
        int tileIndexAmount;
        List<bool> tileIndex;
        byte[] paletteIndex;

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
            paletteIndex[0] = 8;
            paletteIndex[1] = 7;
        }
    }
}

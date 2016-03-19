using System;
using System.Collections.Generic;

namespace Riverback
{
    public class Level
    {
        public const int LEVEL_TILE_AMOUNT = 4096;
        public const int LEVEL_TILE_INDEX_SIZE = 256;
        public const int LEVEL_PALETTE_INDEX_AMOUNT = 8;
        private const int INDEXTILES_MAX = 0x800;

        public int CompressedDataSize { get; set; }
        public LevelHeader LevelHeader { get; set; }
        public byte[] Physmap { get; set; }
        public TilemapTile[] Tilemap { get; set; }
        public TileIndex TileIndex { get; set; }
        public byte[] PaletteIndex { get; set; }

        public Level(LevelHeader levelHeader)
        {
            LevelHeader = levelHeader;
            Physmap = new byte[LEVEL_TILE_AMOUNT];
            Tilemap = new TilemapTile[LEVEL_TILE_AMOUNT];
            TileIndex = new TileIndex(INDEXTILES_MAX);
            PaletteIndex = new byte[LEVEL_PALETTE_INDEX_AMOUNT];
        }

        public void update(byte[] levelData)
        {
            setPhysmap(levelData);
            setTilemap(levelData);
            setTileIndex(levelData);
            setPaletteIndex(levelData);
        }

        public void setPhysmap(byte[] levelData)
        {
            Array.ConstrainedCopy(levelData, 0, Physmap, 0, LEVEL_TILE_AMOUNT);
        }

        public void setTilemap(byte[] levelData)
        {
            Tilemap = TilemapTile.getAllLevelTilesFromLevelData(levelData);
        }

        public void setTileIndex(byte[] levelData)
        {
            int offset = LEVEL_TILE_AMOUNT * 3 + 2;
            List<byte> tileIndexBytes = new List<byte>();
            for (int index = 0; index < LEVEL_TILE_INDEX_SIZE; index++) {
                tileIndexBytes.Add(levelData[offset + index]);
            }
            TileIndex.setTileIndexList(DataFormatter.byteListIntoBitList(tileIndexBytes, false));
        }

        public void setPaletteIndex(byte[] levelData)
        {
            Array.ConstrainedCopy(levelData, 
                                  LEVEL_TILE_AMOUNT * 3 + 2 + LEVEL_TILE_INDEX_SIZE, 
                                  PaletteIndex, 
                                  2, 
                                  LEVEL_PALETTE_INDEX_AMOUNT - 2);
            PaletteIndex[0] = 16;
            PaletteIndex[1] = 17;
        }

        public byte[] serialize(bool compress = true)
        {
            List<byte> data = new List<byte>();
            data.AddRange(Physmap);
            foreach (TilemapTile tile in Tilemap) {
                data.Add(tile.Tile);
                data.Add(tile.Property);
            }

            byte[] tileAmountBytes = new byte[2];
            tileAmountBytes[0] = (byte)(TileIndex.getBankTileIndexSize() & 0x00FF);
            tileAmountBytes[1] = (byte)(TileIndex.getBankTileIndexSize() >> 8);
            data.AddRange(tileAmountBytes);

            int tileIndexNumber = 0;
            List<bool> bitList = new List<bool>();
            while (tileIndexNumber < TileIndex.MaxIndexAmount) {
                bitList.Clear();
                for (int x = 0; x < 8; x++) {
                    if (tileIndexNumber >= TileIndex.MaxIndexAmount) {
                        break;
                    }
                    bitList.Add(TileIndex[tileIndexNumber]);
                    tileIndexNumber += 1;
                }
                bitList.Reverse();
                data.Add(DataFormatter.bitsIntoByte(bitList));
            }

            for (int x = 2; x < 8; x++) {
                data.Add(PaletteIndex[x]);
            }

            if (compress) {
                return DataCompressor.compress(data.ToArray());
            }
            return data.ToArray();
        }
    }
}

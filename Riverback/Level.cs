using System;
using System.Collections.Generic;

namespace Riverback
{
    public class Level
    {
        public const int LevelTileAmount = 4096;
        public const int LevelTileIndexSize = 256;
        public const int LevelPaletteIndexAmount = 8;
        private const int IndexTilesMax = 0x800;

        public int CompressedDataSize { get; set; }
        public LevelHeader LevelHeader { get; set; }
        public byte[] Physmap { get; set; }
        public TilemapTile[] Tilemap { get; set; }
        public TileIndex TileIndex { get; set; }
        public byte[] PaletteIndex { get; set; }

        public Level(LevelHeader levelHeader)
        {
            LevelHeader = levelHeader;
            Physmap = new byte[LevelTileAmount];
            Tilemap = new TilemapTile[LevelTileAmount];
            TileIndex = new TileIndex(IndexTilesMax);
            PaletteIndex = new byte[LevelPaletteIndexAmount];
        }

        public void Update(byte[] levelData)
        {
            SetPhysmap(levelData);
            SetTilemap(levelData);
            SetTileIndex(levelData);
            SetPaletteIndex(levelData);
        }

        public void SetPhysmap(byte[] levelData)
        {
            Array.ConstrainedCopy(levelData, 0, Physmap, 0, LevelTileAmount);
        }

        public void SetTilemap(byte[] levelData)
        {
            Tilemap = TilemapTile.GetAllLevelTilesFromLevelData(levelData);
        }

        public void SetTileIndex(byte[] levelData)
        {
            int offset = LevelTileAmount * 3 + 2;
            List<byte> tileIndexBytes = new List<byte>();
            for (int index = 0; index < LevelTileIndexSize; index++) {
                tileIndexBytes.Add(levelData[offset + index]);
            }
            TileIndex.SetTileIndexList(DataFormatter.ByteListIntoBitList(tileIndexBytes, false));
        }

        public void SetPaletteIndex(byte[] levelData)
        {
            Array.ConstrainedCopy(levelData, 
                                  LevelTileAmount * 3 + 2 + LevelTileIndexSize, 
                                  PaletteIndex, 
                                  2, 
                                  LevelPaletteIndexAmount - 2);
            PaletteIndex[0] = 16;
            PaletteIndex[1] = 17;
        }

        public byte[] Serialize(bool compress = true)
        {
            List<byte> data = new List<byte>();
            data.AddRange(Physmap);
            foreach (TilemapTile tile in Tilemap) {
                data.Add(tile.Tile);
                data.Add(tile.Property);
            }

            byte[] tileAmountBytes = new byte[2];
            tileAmountBytes[0] = (byte)(TileIndex.GetBankTileIndexSize() & 0x00FF);
            tileAmountBytes[1] = (byte)(TileIndex.GetBankTileIndexSize() >> 8);
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
                data.Add(DataFormatter.BitsIntoByte(bitList));
            }

            for (int x = 2; x < 8; x++) {
                data.Add(PaletteIndex[x]);
            }

            if (compress) {
                return DataCompressor.Compress(data.ToArray());
            }
            return data.ToArray();
        }
    }
}

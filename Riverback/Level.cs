﻿using System;
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

        public int CompressedDataSize { get; set; }
        public LevelHeader LevelHeader { get; private set; }
        public byte[] Physmap { get; private set; }
        public TilemapTile[] Tilemap { get; private set; }
        public int TileIndexAmount { get { return TileIndex.Where(x => x == true).Count(); } }
        public List<bool> TileIndex { get; private set; }
        public byte[] PaletteIndex { get; private set; }

        public Level(LevelHeader levelHeader)
        {
            this.LevelHeader = levelHeader;
            Physmap = new byte[LEVEL_TILE_AMOUNT];
            Tilemap = new TilemapTile[LEVEL_TILE_AMOUNT];
            TileIndex = new List<bool>();
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
            for (int index = 0; index < LEVEL_TILE_INDEX_SIZE; index++)
                tileIndexBytes.Add(levelData[offset + index]);
            TileIndex = DataFormatter.byteListIntoBitList(tileIndexBytes, false);
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

        public byte[] serialize()
        {
            List<byte> compressedData = new List<byte>();
            compressedData.AddRange(Physmap);
            foreach (TilemapTile tile in Tilemap) {
                compressedData.Add(tile.Tile);
                compressedData.Add(tile.Property);
            }

            byte[] tileAmountBytes = new byte[2];
            int tileAmount = TileIndex.Where(x => x == true).Count();
            tileAmountBytes[0] = (byte)(tileAmount & 0x00FF);
            tileAmountBytes[1] = (byte)(tileAmount >> 8);
            compressedData.AddRange(tileAmountBytes);

            int tileIndexNumber = 0;
            List<bool> bitList = new List<bool>();
            while (tileIndexNumber < TileIndex.Count()) {
                bitList.Clear();
                for (int x = 0; x < 8; x++) {
                    if (tileIndexNumber >= TileIndex.Count())
                        break;
                    bitList.Add(TileIndex[tileIndexNumber]);
                    tileIndexNumber += 1;
                }
                bitList.Reverse();
                compressedData.Add(DataFormatter.bitsIntoByte(bitList));
            }

            for (int x = 2; x < 8; x++)
                compressedData.Add(PaletteIndex[x]);

            return DataCompressor.compress(compressedData.ToArray());
        }
    }
}

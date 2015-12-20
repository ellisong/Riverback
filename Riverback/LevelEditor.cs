using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riverback
{
    public class LevelEditor
    {
        public const uint GRAPHICS_BANK_HEADER_ADDRESS = 0x02E80;
        public const int BANK_AMOUNT = 7;
        public const int DEFAULT_BANK_PALETTE = 15;

        private LevelHeader levelHeader;
        public LevelHeader LevelHeader { get { return levelHeader; } }
        private Level level;
        public Level Level { get { return level; } }
        private GraphicBank levelBank;
        public GraphicBank LevelBank { get { return levelBank; } }
        private List<GraphicBank> banks;
        private int tileOffset;
        public int TileOffset { get { return tileOffset; } }

        public void openLevel(byte[] romdata, byte levelNumber)
        {
            levelHeader = new LevelHeader(levelNumber);
            levelHeader.update(romdata);
            byte[] levelData = DataCompressor.decompress(romdata, levelHeader.levelPointer);
            level = new Level(levelHeader);
            level.update(levelData);
        }

        public void updateGraphicsBanks(byte[] romdata)
        {
            banks = new List<GraphicBank>();
            List<uint> bankAddresses = new List<uint>();
            for (int bankNum = 0; bankNum < BANK_AMOUNT; bankNum++) {
                uint bankPointer = GRAPHICS_BANK_HEADER_ADDRESS + ((uint)bankNum * 8);
                bankAddresses.Add(DataFormatter.readSnesPointerToRomPointer(romdata, bankPointer));
                bankAddresses.Add(DataFormatter.readSnesPointerToRomPointer(romdata, bankPointer + 3));
            }
            for (int bankNum = 0; bankNum < BANK_AMOUNT * 2; bankNum++) {
                byte[] bankData = DataCompressor.decompress(romdata, bankAddresses[bankNum]);
                bool hasPalettes = true;
                if ((bankNum % 2) == 1)
                    hasPalettes = false;
                GraphicBank bank = new GraphicBank(bankData, hasPalettes);
                if (hasPalettes == false)
                    bank.palettes = banks[bankNum - 1].palettes;
                banks.Add(bank);
            }
        }

        public void updateLevelBank()
        {
            int index = levelHeader.graphicsBankIndex * 2;
            banks[index].resetTileOffset();
            PlanarTilesWithOffset pt = banks[index].getPlanarTilesFromBankData(level.TileIndex, 0);
            tileOffset = banks[index].TileOffset;
            List<byte> levelBankData = pt.planarTiles;
            PlanarTilesWithOffset pt2 = banks[index+1].getPlanarTilesFromBankData(level.TileIndex, pt.offset);
            levelBankData.AddRange(pt2.planarTiles);
            levelBank = new GraphicBank(levelBankData.ToArray(), false);
            levelBank.tileAmount = level.TileIndexAmount;
            levelBank.palettes = banks[index].palettes;
        }

        public void setTileInTilemap(int tileNum, int tileValue, bool vflip, bool hflip, 
                                     bool priority, byte palette)
        {
            if (tileNum < Level.LEVEL_TILE_AMOUNT) {
                TilemapTile tile = this.Level.Tilemap[tileNum];
                if (tileValue < this.TileOffset) {
                    tile.Bank = 0;
                    tile.Tile = (byte)tileValue;
                } else {
                    tile.Bank = 1;
                    tile.Tile = (byte)(tileValue - this.LevelBank.TileOffset);
                }
                tile.VFlip = vflip;
                tile.HFlip = hflip;
                tile.Priority = priority;
                tile.Palette = palette;
            }
        }
    }
}

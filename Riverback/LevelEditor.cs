using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riverback
{
    public class LevelEditor
    {
        const uint GRAPHICS_BANK_HEADER_ADDRESS = 0x02E80;
        const int BANK_AMOUNT = 7;
        const int DEFAULT_BANK_PALETTE = 15;
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

        private LevelHeader levelHeader;
        public LevelHeader LevelHeader { get { return levelHeader; } }
        private Level level;
        public Level Level { get { return level; } }
        private GraphicBank levelBank;
        public GraphicBank LevelBank { get { return levelBank; } }
        private List<GraphicBank> banks;
        private int tileOffset;
        public int TileOffset { get { return tileOffset; } }

        public void openLevel(byte[] romdata, int levelNumber)
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
                bankAddresses.Add(DataFormatter.readSnesPointer(romdata, bankPointer));
                bankAddresses.Add(DataFormatter.readSnesPointer(romdata, bankPointer + 3));
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
            PlanarTilesWithOffset pt2 = banks[index].getPlanarTilesFromBankData(level.TileIndex, pt.offset);
            levelBankData.AddRange(pt2.planarTiles);
            levelBank = new GraphicBank(levelBankData.ToArray(), false);
            levelBank.tileAmount = level.TileIndexAmount;
            levelBank.palettes = banks[index].palettes;
        }
    }
}

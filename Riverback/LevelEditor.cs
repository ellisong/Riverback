using System.Collections.Generic;

namespace Riverback
{
    public class LevelEditor
    {
        private const int GRAPHICS_BANK_HEADER_ADDRESS = 0x02E80;
        private const int BANK_AMOUNT = 7;
        private const int DEFAULT_BANK_PALETTE = 15;

        private LevelHeader levelHeader;
        public LevelHeader LevelHeader { get { return levelHeader; } }
        private Level level;
        public Level Level { get { return level; } }
        private List<GraphicBank> banks;
        public List<GraphicBank> Banks { get { return banks; } }
        
        public void openLevel(byte[] romdata, byte levelNumber)
        {
            levelHeader = new LevelHeader(levelNumber);
            levelHeader.update(romdata);
            int compressedSize;
            byte[] levelData = DataCompressor.decompress(romdata, levelHeader.levelPointer, out compressedSize);
            level = new Level(levelHeader);
            level.CompressedDataSize = compressedSize;
            level.update(levelData);
        }

        public void updateGraphicsBanks(byte[] romdata)
        {
            banks = new List<GraphicBank>();
            List<int> bankAddresses = new List<int>();
            for (int bankNum = 0; bankNum < BANK_AMOUNT; bankNum++) {
                int bankPointer = GRAPHICS_BANK_HEADER_ADDRESS + bankNum * 8;
                bankAddresses.Add(DataFormatter.readSnesPointerToRomPointer(romdata, bankPointer));
                bankAddresses.Add(DataFormatter.readSnesPointerToRomPointer(romdata, bankPointer + 3));
            }
            for (int bankNum = 0; bankNum < BANK_AMOUNT * 2; bankNum++) {
                int compressedSize;
                byte[] bankData = DataCompressor.decompress(romdata, bankAddresses[bankNum], out compressedSize);
                bool hasPalettes = true;
                if ((bankNum % 2) == 1) {
                    hasPalettes = false;
                }
                GraphicBank bank = new GraphicBank(bankData, hasPalettes);
                bank.CompressedDataSize = compressedSize;
                if (hasPalettes == false) {
                    bank.palettes = banks[bankNum - 1].palettes;
                }
                banks.Add(bank);
            }
        }

        public void updateLevelHeader(LevelHeader levelHeader)
        {
            this.levelHeader = new LevelHeader(levelHeader);
        }

        public GraphicBank getBankFromTileNumber(int tileNum)
        {
            int bankIndex = LevelHeader.graphicsBankIndex * 2;
            if (tileNum < Banks[bankIndex].tileAmount)
                return Banks[bankIndex];
            if (tileNum < Banks[bankIndex].tileAmount + Banks[bankIndex + 1].tileAmount)
                return Banks[bankIndex + 1];
            return null;
        }

        public void removeInvalidTiles(List<int> removedTiles)
        {
            int tileValue;
            for (int index = 0; index < Level.LEVEL_TILE_AMOUNT; index++) {
                tileValue = level.Tilemap[index].Bank * 256 + level.Tilemap[index].Tile;
                if ((tileValue >= Level.TileIndex.getBankTileIndexSize()) || (removedTiles.Contains(tileValue))) {
                    level.Tilemap[index].Bank = 0;
                    level.Tilemap[index].Tile = 0;
                }
            }
        }

        public void setTileInPhysmap(int tileNum, byte tile)
        {
            if (tileNum < Level.LEVEL_TILE_AMOUNT) {
                Level.Physmap[tileNum] = tile;
            }
        }

        public void setTileInTilemap(int tileNum, TilemapTile tile)
        {
            if (tileNum < Level.LEVEL_TILE_AMOUNT) {
                TilemapTile levelTile = Level.Tilemap[tileNum];
                levelTile.Bank = tile.Bank;
                levelTile.Tile = tile.Tile;
                levelTile.VFlip = tile.VFlip;
                levelTile.HFlip = tile.HFlip;
                levelTile.Priority = tile.Priority;
                levelTile.Palette = tile.Palette;
            }
        }

        public void setTileInTilemap(int tileNum, int tileValue)
        {
            if (tileNum < Level.LEVEL_TILE_AMOUNT) {
                TilemapTile tile = Level.Tilemap[tileNum];
                tile.Bank = (byte)(tileValue / 256);
                tile.Tile = (byte)(tileValue % 256);
            }
        }

        public void setTileInTilemap(int tileNum, int tileValue, bool vflip, bool hflip, bool priority, byte palette)
        {
            if (tileNum < Level.LEVEL_TILE_AMOUNT) {
                TilemapTile tile = Level.Tilemap[tileNum];
                tile.Bank = (byte)(tileValue / 256);
                tile.Tile = (byte)(tileValue % 256);
                tile.VFlip = vflip;
                tile.HFlip = hflip;
                tile.Priority = priority;
                tile.Palette = palette;
            }
        }
    }
}

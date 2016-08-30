using System.Collections.Generic;

namespace Riverback
{
    public class LevelEditor
    {
        private const int GraphicsBankHeaderAddress = 0x02E80;
        private const int BankAmount = 7;
        //private const int DefaultBankPalette = 15;

        private LevelHeader _levelHeader;
        public LevelHeader LevelHeader => _levelHeader;
        private Level _level;
        public Level Level => _level;
        private List<GraphicBank> _banks;
        public List<GraphicBank> Banks => _banks;

        public void OpenLevel(byte[] romdata, byte levelNumber)
        {
            _levelHeader = new LevelHeader(levelNumber);
            _levelHeader.Update(romdata);
            int compressedSize;
            byte[] levelData = DataCompressor.Decompress(romdata, _levelHeader.LevelPointer, out compressedSize);
            _level = new Level(_levelHeader);
            _level.CompressedDataSize = compressedSize;
            _level.Update(levelData);
        }

        public void UpdateGraphicsBanks(byte[] romdata)
        {
            _banks = new List<GraphicBank>();
            List<int> bankAddresses = new List<int>();
            for (int bankNum = 0; bankNum < BankAmount; bankNum++) {
                int bankPointer = GraphicsBankHeaderAddress + bankNum * 8;
                bankAddresses.Add(DataFormatter.ReadSnesPointerToRomPointer(romdata, bankPointer));
                bankAddresses.Add(DataFormatter.ReadSnesPointerToRomPointer(romdata, bankPointer + 3));
            }
            for (int bankNum = 0; bankNum < BankAmount * 2; bankNum++) {
                int compressedSize;
                byte[] bankData = DataCompressor.Decompress(romdata, bankAddresses[bankNum], out compressedSize);
                var hasPalettes = bankNum % 2 != 1;
                GraphicBank bank = new GraphicBank(bankData, hasPalettes);
                bank.CompressedDataSize = compressedSize;
                if (hasPalettes == false) {
                    bank.Palettes = _banks[bankNum - 1].Palettes;
                }
                _banks.Add(bank);
            }
        }

        public void UpdateLevelHeader(LevelHeader levelHeader)
        {
            _levelHeader = new LevelHeader(levelHeader);
        }

        public GraphicBank GetBankFromTileNumber(int tileNum)
        {
            int bankIndex = LevelHeader.GraphicsBankIndex * 2;
            if (tileNum < Banks[bankIndex].TileAmount)
                return Banks[bankIndex];
            if (tileNum < Banks[bankIndex].TileAmount + Banks[bankIndex + 1].TileAmount)
                return Banks[bankIndex + 1];
            return null;
        }

        public void RemoveInvalidTiles(List<int> removedTiles)
        {
            for (int index = 0; index < Level.LevelTileAmount; index++)
            {
                var tileValue = _level.Tilemap[index].Bank * 256 + _level.Tilemap[index].Tile;
                if ((tileValue >= Level.TileIndex.GetBankTileIndexSize()) || (removedTiles.Contains(tileValue))) {
                    _level.Tilemap[index].Bank = 0;
                    _level.Tilemap[index].Tile = 0;
                }
            }
        }

        public void SetTileInPhysmap(int tileNum, byte tile)
        {
            if (tileNum < Level.LevelTileAmount) {
                Level.Physmap[tileNum] = tile;
            }
        }

        public void SetTileInTilemap(int tileNum, TilemapTile tile)
        {
            if (tileNum < Level.LevelTileAmount) {
                TilemapTile levelTile = Level.Tilemap[tileNum];
                levelTile.Bank = tile.Bank;
                levelTile.Tile = tile.Tile;
                levelTile.VFlip = tile.VFlip;
                levelTile.HFlip = tile.HFlip;
                levelTile.Priority = tile.Priority;
                levelTile.Palette = tile.Palette;
            }
        }

        public void SetTileInTilemap(int tileNum, int tileValue)
        {
            if (tileNum < Level.LevelTileAmount) {
                TilemapTile tile = Level.Tilemap[tileNum];
                tile.Bank = (byte)(tileValue / 256);
                tile.Tile = (byte)(tileValue % 256);
            }
        }

        public void SetTileInTilemap(int tileNum, int tileValue, bool vflip, bool hflip, bool priority, byte palette)
        {
            if (tileNum < Level.LevelTileAmount) {
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

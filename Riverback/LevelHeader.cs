using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riverback
{
    class LevelHeader
    {
        // CONSTANTS
        public const uint LEVEL_HEADER_ADDRESS = 0xF298;
        public const uint LEVEL_HEADER_SIZE = 37;
        // Level header amount might be possible to alter due to free space after the level header data
        public const uint LEVEL_HEADER_AMOUNT = 64;

        public int levelNumber;
        public uint levelHeaderAddress;
        public uint levelPointer;
        public int graphicsBankIndex;
        public int fieldNumber;
        public int musicSelect;
        public byte[] enemyType;
        public int waterHeight;
        public int waterType;
        public int levelTimer;
        public byte[] doorExits;

        public LevelHeader(int levelNumber = 0)
        {
            this.levelNumber = levelNumber;
            this.levelHeaderAddress = LEVEL_HEADER_ADDRESS + ((uint)this.levelNumber * LEVEL_HEADER_SIZE);
            enemyType = new byte[6];
            doorExits = new byte[4];
        }

        public void update(byte[] romdata)
        {
            levelPointer = DataFormatter.readSnesPointer(romdata, levelHeaderAddress);
            graphicsBankIndex = romdata[levelHeaderAddress + 0x03];
            fieldNumber = romdata[levelHeaderAddress + 0x04];
            musicSelect = romdata[levelHeaderAddress + 0x05];
            enemyType[0] = romdata[levelHeaderAddress + 0x06];
            enemyType[1] = romdata[levelHeaderAddress + 0x07];
            enemyType[2] = romdata[levelHeaderAddress + 0x08];
            enemyType[3] = romdata[levelHeaderAddress + 0x09];
            enemyType[4] = romdata[levelHeaderAddress + 0x0A];
            enemyType[5] = romdata[levelHeaderAddress + 0x0B];
            waterHeight = romdata[levelHeaderAddress + 0x1C];
            waterType = romdata[levelHeaderAddress + 0x1D];
            levelTimer = DataFormatter.switchReadBytesIntoUInt16(romdata, levelHeaderAddress + 0x1F);
            doorExits[0] = romdata[levelHeaderAddress + 0x21];
            doorExits[1] = romdata[levelHeaderAddress + 0x22];
            doorExits[2] = romdata[levelHeaderAddress + 0x23];
            doorExits[3] = romdata[levelHeaderAddress + 0x24];
        }
    }
}

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

        public uint levelNumber;
        public uint levelHeaderAddress;
        public uint levelPointer;
        public uint graphicsBankIndex;
        public uint fieldNumber;
        public uint musicSelect;
        public uint[] enemyType = new uint[6];
        public uint waterHeight;
        public uint waterType;
        public uint levelTimer;
        public uint[] doorExits = new uint[4];

        public LevelHeader(uint levelNumber = 0)
        {
            this.levelNumber = levelNumber;
            this.levelHeaderAddress = LevelHeader.LEVEL_HEADER_ADDRESS
                                      + (this.levelNumber * LevelHeader.LEVEL_HEADER_SIZE);
        }

        public void update(byte[] romdata)
        {
            levelPointer = DataFormatter.readSnesPointer(romdata, this.levelHeaderAddress);
            graphicsBankIndex = romdata[this.levelHeaderAddress + 0x03];
            this.fieldNumber = romdata[this.levelHeaderAddress + 0x04];
            this.musicSelect = romdata[this.levelHeaderAddress + 0x05];
            this.enemyType[0] = romdata[this.levelHeaderAddress + 0x06];
            this.enemyType[1] = romdata[this.levelHeaderAddress + 0x07];
            this.enemyType[2] = romdata[this.levelHeaderAddress + 0x08];
            this.enemyType[3] = romdata[this.levelHeaderAddress + 0x09];
            this.enemyType[4] = romdata[this.levelHeaderAddress + 0x0A];
            this.enemyType[5] = romdata[this.levelHeaderAddress + 0x0B];
            this.waterHeight = romdata[this.levelHeaderAddress + 0x1C];
            this.waterType = romdata[this.levelHeaderAddress + 0x1D];
            this.levelTimer = DataFormatter.switchReadBytesIntoUInt16(romdata, this.levelHeaderAddress + 0x1F);
            this.doorExits[0] = romdata[this.levelHeaderAddress + 0x21];
            this.doorExits[1] = romdata[this.levelHeaderAddress + 0x22];
            this.doorExits[2] = romdata[this.levelHeaderAddress + 0x23];
            this.doorExits[3] = romdata[this.levelHeaderAddress + 0x24];
        }
    }
}

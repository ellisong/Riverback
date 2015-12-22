using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riverback
{
    public class LevelHeader
    {
        // CONSTANTS
        public const int LEVEL_HEADER_POINTER_ADDRESS = 0xF218;
        public const byte LEVEL_HEADER_POINTER_SIZE = 2;
        public const byte LEVEL_HEADER_POINTER_AMOUNT = 64;
        public const int LEVEL_HEADER_ADDRESS = 0xF298;
        public const byte LEVEL_HEADER_SIZE = 37;
        public const byte LEVEL_HEADER_AMOUNT = 48;

        public byte headerNumber;
        public int headerPointerAddress;
        public int headerAddress;
        public int levelPointer;
        public byte graphicsBankIndex;
        public byte fieldNumber;
        public byte musicSelect;
        public byte[] enemyType;
        public byte[] unknownData;
        public byte waterHeight;
        public byte waterType;
        public byte unknownData2;
        public int levelTimer;
        public byte[] doorExits;

        public LevelHeader(byte headerNumber = 0)
        {
            this.headerNumber = headerNumber;
            enemyType = new byte[6];
            doorExits = new byte[4];
            unknownData = new byte[16];
        }

        public void update(byte[] romdata)
        {
            headerPointerAddress = LEVEL_HEADER_POINTER_ADDRESS + (int)this.headerNumber * LEVEL_HEADER_POINTER_SIZE;
            headerAddress = DataFormatter.switchReadBytesIntoint16(romdata, headerPointerAddress);
            levelPointer = DataFormatter.readSnesPointerToRomPointer(romdata, headerAddress);
            graphicsBankIndex = romdata[headerAddress + 0x03];
            fieldNumber = romdata[headerAddress + 0x04];
            musicSelect = romdata[headerAddress + 0x05];
            enemyType[0] = romdata[headerAddress + 0x06];
            enemyType[1] = romdata[headerAddress + 0x07];
            enemyType[2] = romdata[headerAddress + 0x08];
            enemyType[3] = romdata[headerAddress + 0x09];
            enemyType[4] = romdata[headerAddress + 0x0A];
            enemyType[5] = romdata[headerAddress + 0x0B];
            unknownData[0] = romdata[headerAddress + 0x0C];
            unknownData[1] = romdata[headerAddress + 0x0D];
            unknownData[2] = romdata[headerAddress + 0x0E];
            unknownData[3] = romdata[headerAddress + 0x0F];
            unknownData[4] = romdata[headerAddress + 0x10];
            unknownData[5] = romdata[headerAddress + 0x11];
            unknownData[6] = romdata[headerAddress + 0x12];
            unknownData[7] = romdata[headerAddress + 0x13];
            unknownData[8] = romdata[headerAddress + 0x14];
            unknownData[9] = romdata[headerAddress + 0x15];
            unknownData[10] = romdata[headerAddress + 0x16];
            unknownData[11] = romdata[headerAddress + 0x17];
            unknownData[12] = romdata[headerAddress + 0x18];
            unknownData[13] = romdata[headerAddress + 0x19];
            unknownData[14] = romdata[headerAddress + 0x1A];
            unknownData[15] = romdata[headerAddress + 0x1B];
            waterHeight = romdata[headerAddress + 0x1C];
            waterType = romdata[headerAddress + 0x1D];
            unknownData2 = romdata[headerAddress + 0x1E];
            levelTimer = DataFormatter.switchReadBytesIntoint16(romdata, headerAddress + 0x1F);
            doorExits[0] = romdata[headerAddress + 0x21];
            doorExits[1] = romdata[headerAddress + 0x22];
            doorExits[2] = romdata[headerAddress + 0x23];
            doorExits[3] = romdata[headerAddress + 0x24];
        }

        public byte[] serialize()
        {
            List<byte> compressedData = new List<byte>();
            compressedData.AddRange(DataFormatter.convertRomPointerToSnesPointer(levelPointer));
            compressedData.Add(graphicsBankIndex);
            compressedData.Add(fieldNumber);
            compressedData.Add(musicSelect);
            compressedData.AddRange(enemyType);
            compressedData.AddRange(unknownData);
            compressedData.Add(waterHeight);
            compressedData.Add(waterType);
            compressedData.Add(unknownData2);
            compressedData.Add((byte)(levelTimer & 0x00FF));
            compressedData.Add((byte)((levelTimer & 0xFF00) >> 8));
            compressedData.AddRange(doorExits);
            return compressedData.ToArray();
        }
    }
}

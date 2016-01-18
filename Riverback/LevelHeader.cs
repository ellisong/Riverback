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

        public LevelHeader(LevelHeader levelHeader)
        {
            this.headerNumber = levelHeader.headerNumber;
            this.headerPointerAddress = levelHeader.headerPointerAddress;
            this.headerAddress = levelHeader.headerAddress;
            this.levelPointer = levelHeader.levelPointer;
            this.graphicsBankIndex = levelHeader.graphicsBankIndex;
            this.fieldNumber = levelHeader.fieldNumber;
            this.musicSelect = levelHeader.musicSelect;
            this.enemyType = (byte[])levelHeader.enemyType.Clone();
            this.unknownData = (byte[])levelHeader.unknownData.Clone();
            this.waterHeight = levelHeader.waterHeight;
            this.waterType = levelHeader.waterType;
            this.unknownData2 = levelHeader.unknownData2;
            this.levelTimer = levelHeader.levelTimer;
            this.doorExits = (byte[])levelHeader.doorExits.Clone();
        }

        public void update(byte[] romdata)
        {
            headerPointerAddress = LEVEL_HEADER_POINTER_ADDRESS + (int)this.headerNumber * LEVEL_HEADER_POINTER_SIZE;
            headerAddress = DataFormatter.switchReadBytesIntoint16(romdata, headerPointerAddress);
            deserialize(romdata, headerAddress);
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

        // Requires headerPointerAddress and headerAddress to be set
        public void deserialize(byte[] data, int offset)
        {
            if (data.Length >= LEVEL_HEADER_SIZE) {
                levelPointer = DataFormatter.readSnesPointerToRomPointer(data, offset);
                graphicsBankIndex = data[offset + 0x03];
                fieldNumber = data[offset + 0x04];
                musicSelect = data[offset + 0x05];
                enemyType[0] = data[offset + 0x06];
                enemyType[1] = data[offset + 0x07];
                enemyType[2] = data[offset + 0x08];
                enemyType[3] = data[offset + 0x09];
                enemyType[4] = data[offset + 0x0A];
                enemyType[5] = data[offset + 0x0B];
                unknownData[0] = data[offset + 0x0C];
                unknownData[1] = data[offset + 0x0D];
                unknownData[2] = data[offset + 0x0E];
                unknownData[3] = data[offset + 0x0F];
                unknownData[4] = data[offset + 0x10];
                unknownData[5] = data[offset + 0x11];
                unknownData[6] = data[offset + 0x12];
                unknownData[7] = data[offset + 0x13];
                unknownData[8] = data[offset + 0x14];
                unknownData[9] = data[offset + 0x15];
                unknownData[10] = data[offset + 0x16];
                unknownData[11] = data[offset + 0x17];
                unknownData[12] = data[offset + 0x18];
                unknownData[13] = data[offset + 0x19];
                unknownData[14] = data[offset + 0x1A];
                unknownData[15] = data[offset + 0x1B];
                waterHeight = data[offset + 0x1C];
                waterType = data[offset + 0x1D];
                unknownData2 = data[offset + 0x1E];
                levelTimer = DataFormatter.switchReadBytesIntoint16(data, offset + 0x1F);
                doorExits[0] = data[offset + 0x21];
                doorExits[1] = data[offset + 0x22];
                doorExits[2] = data[offset + 0x23];
                doorExits[3] = data[offset + 0x24];
            }
        }
    }
}

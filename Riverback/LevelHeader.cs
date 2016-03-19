using System.Collections.Generic;

namespace Riverback
{
    public class LevelHeader
    {
        private const int LEVEL_HEADER_POINTER_ADDRESS = 0xF218;
        private const byte LEVEL_HEADER_POINTER_AMOUNT = 64;
        private const int LEVEL_HEADER_ADDRESS = 0xF298;
        private const byte LEVEL_HEADER_AMOUNT = 48;

        public byte headerNumber;
        public int headerPointerAddress;
        public int headerAddress;
        public int levelPointer;
        public byte graphicsBankIndex;
        public byte fieldNumber;
        public byte musicSelect;
        public byte[] enemyType;
        public byte[] spawnRates;
        public byte[] objectType;
        public byte waterHeight;
        public byte displayWater;
        public byte waterType;
        public byte alwaysE6;
        public int levelTimer;
        public byte[] doorExits;

        public LevelHeader(byte headerNumber = 0)
        {
            this.headerNumber = headerNumber;
            enemyType = new byte[6];
            doorExits = new byte[4];
            spawnRates = new byte[8];
            objectType = new byte[7];
        }

        public LevelHeader(LevelHeader levelHeader)
        {
            headerNumber = levelHeader.headerNumber;
            headerPointerAddress = levelHeader.headerPointerAddress;
            headerAddress = levelHeader.headerAddress;
            levelPointer = levelHeader.levelPointer;
            graphicsBankIndex = levelHeader.graphicsBankIndex;
            fieldNumber = levelHeader.fieldNumber;
            musicSelect = levelHeader.musicSelect;
            enemyType = (byte[])levelHeader.enemyType.Clone();
            spawnRates = (byte[])levelHeader.spawnRates.Clone();
            objectType = (byte[])levelHeader.objectType.Clone();
            waterHeight = levelHeader.waterHeight;
            displayWater = levelHeader.displayWater;
            waterType = levelHeader.waterType;
            alwaysE6 = levelHeader.alwaysE6;
            levelTimer = levelHeader.levelTimer;
            doorExits = (byte[])levelHeader.doorExits.Clone();
        }

        public void update(byte[] romdata)
        {
            headerPointerAddress = LEVEL_HEADER_POINTER_ADDRESS + headerNumber * 2;
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
            compressedData.AddRange(spawnRates);
            compressedData.AddRange(objectType);
            compressedData.Add(waterHeight);
            compressedData.Add(displayWater);
            compressedData.Add(waterType);
            compressedData.Add(alwaysE6);
            compressedData.Add((byte)(levelTimer & 0x00FF));
            compressedData.Add((byte)((levelTimer & 0xFF00) >> 8));
            compressedData.AddRange(doorExits);
            return compressedData.ToArray();
        }

        // Requires headerPointerAddress and headerAddress to be set
        public void deserialize(byte[] data, int offset)
        {
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
            spawnRates[0] = data[offset + 0x0C];
            spawnRates[1] = data[offset + 0x0D];
            spawnRates[2] = data[offset + 0x0E];
            spawnRates[3] = data[offset + 0x0F];
            spawnRates[4] = data[offset + 0x10];
            spawnRates[5] = data[offset + 0x11];
            spawnRates[6] = data[offset + 0x12];
            spawnRates[7] = data[offset + 0x13];
            objectType[0] = data[offset + 0x14];
            objectType[1] = data[offset + 0x15];
            objectType[2] = data[offset + 0x16];
            objectType[3] = data[offset + 0x17];
            objectType[4] = data[offset + 0x18];
            objectType[5] = data[offset + 0x19];
            objectType[6] = data[offset + 0x1A];
            waterHeight = data[offset + 0x1B];
            displayWater = data[offset + 0x1C];
            waterType = data[offset + 0x1D];
            alwaysE6 = data[offset + 0x1E];
            levelTimer = DataFormatter.switchReadBytesIntoint16(data, offset + 0x1F);
            doorExits[0] = data[offset + 0x21];
            doorExits[1] = data[offset + 0x22];
            doorExits[2] = data[offset + 0x23];
            doorExits[3] = data[offset + 0x24];
        }
    }
}

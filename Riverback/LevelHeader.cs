using System.Collections.Generic;

namespace Riverback
{
    public class LevelHeader
    {
        private const int LevelHeaderPointerAddress = 0xF218;
        //private const byte LevelHeaderPointerAmount = 64;
        private const int LevelHeaderAddress = 0xF298;
        //private const byte LevelHeaderAmount = 48;
        public const byte LevelHeaderSize = 37;

        public byte HeaderPointerNumber;
        public int HeaderPointerAddress;
        public int HeaderNumber => (HeaderAddress - LevelHeaderAddress) / LevelHeaderSize;
        public int HeaderAddress;
        public int LevelPointer;
        public byte GraphicsBankIndex;
        public byte FieldNumber;
        public byte MusicSelect;
        public byte[] EnemyType;
        public byte[] SpawnRates;
        public byte[] ObjectType;
        public byte WaterHeight;
        public byte DisplayWater;
        public byte WaterType;
        public byte AlwaysE6;
        public int LevelTimer;
        public byte[] DoorExits;

        public LevelHeader(byte headerPointerNumber = 0)
        {
            HeaderPointerNumber = headerPointerNumber;
            EnemyType = new byte[6];
            DoorExits = new byte[4];
            SpawnRates = new byte[8];
            ObjectType = new byte[7];
        }

        public LevelHeader(LevelHeader levelHeader)
        {
            HeaderPointerNumber = levelHeader.HeaderPointerNumber;
            HeaderPointerAddress = levelHeader.HeaderPointerAddress;
            HeaderAddress = levelHeader.HeaderAddress;
            LevelPointer = levelHeader.LevelPointer;
            GraphicsBankIndex = levelHeader.GraphicsBankIndex;
            FieldNumber = levelHeader.FieldNumber;
            MusicSelect = levelHeader.MusicSelect;
            EnemyType = (byte[])levelHeader.EnemyType.Clone();
            SpawnRates = (byte[])levelHeader.SpawnRates.Clone();
            ObjectType = (byte[])levelHeader.ObjectType.Clone();
            WaterHeight = levelHeader.WaterHeight;
            DisplayWater = levelHeader.DisplayWater;
            WaterType = levelHeader.WaterType;
            AlwaysE6 = levelHeader.AlwaysE6;
            LevelTimer = levelHeader.LevelTimer;
            DoorExits = (byte[])levelHeader.DoorExits.Clone();
        }

        public void Update(byte[] romdata)
        {
            HeaderPointerAddress = LevelHeaderPointerAddress + HeaderPointerNumber * 2;
            HeaderAddress = DataFormatter.SwitchReadBytesIntoint16(romdata, HeaderPointerAddress);
            Deserialize(romdata, HeaderAddress);
        }

        public byte[] Serialize()
        {
            List<byte> compressedData = new List<byte>();
            compressedData.AddRange(DataFormatter.ConvertRomPointerToSnesPointer(LevelPointer));
            compressedData.Add(GraphicsBankIndex);
            compressedData.Add(FieldNumber);
            compressedData.Add(MusicSelect);
            compressedData.AddRange(EnemyType);
            compressedData.AddRange(SpawnRates);
            compressedData.AddRange(ObjectType);
            compressedData.Add(WaterHeight);
            compressedData.Add(DisplayWater);
            compressedData.Add(WaterType);
            compressedData.Add(AlwaysE6);
            compressedData.Add((byte)(LevelTimer & 0x00FF));
            compressedData.Add((byte)((LevelTimer & 0xFF00) >> 8));
            compressedData.AddRange(DoorExits);
            return compressedData.ToArray();
        }

        // Requires headerPointerAddress and headerAddress to be set
        public void Deserialize(byte[] data, int offset)
        {
            LevelPointer = DataFormatter.ReadSnesPointerToRomPointer(data, offset);
            GraphicsBankIndex = data[offset + 0x03];
            FieldNumber = data[offset + 0x04];
            MusicSelect = data[offset + 0x05];
            EnemyType[0] = data[offset + 0x06];
            EnemyType[1] = data[offset + 0x07];
            EnemyType[2] = data[offset + 0x08];
            EnemyType[3] = data[offset + 0x09];
            EnemyType[4] = data[offset + 0x0A];
            EnemyType[5] = data[offset + 0x0B];
            SpawnRates[0] = data[offset + 0x0C];
            SpawnRates[1] = data[offset + 0x0D];
            SpawnRates[2] = data[offset + 0x0E];
            SpawnRates[3] = data[offset + 0x0F];
            SpawnRates[4] = data[offset + 0x10];
            SpawnRates[5] = data[offset + 0x11];
            SpawnRates[6] = data[offset + 0x12];
            SpawnRates[7] = data[offset + 0x13];
            ObjectType[0] = data[offset + 0x14];
            ObjectType[1] = data[offset + 0x15];
            ObjectType[2] = data[offset + 0x16];
            ObjectType[3] = data[offset + 0x17];
            ObjectType[4] = data[offset + 0x18];
            ObjectType[5] = data[offset + 0x19];
            ObjectType[6] = data[offset + 0x1A];
            WaterHeight = data[offset + 0x1B];
            DisplayWater = data[offset + 0x1C];
            WaterType = data[offset + 0x1D];
            AlwaysE6 = data[offset + 0x1E];
            LevelTimer = DataFormatter.SwitchReadBytesIntoint16(data, offset + 0x1F);
            DoorExits[0] = data[offset + 0x21];
            DoorExits[1] = data[offset + 0x22];
            DoorExits[2] = data[offset + 0x23];
            DoorExits[3] = data[offset + 0x24];
        }
    }
}

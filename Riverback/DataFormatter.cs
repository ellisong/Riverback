using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riverback
{
    public static class DataFormatter
    {
        public static byte bitsIntoByte(bool[] bitList)
        {
            byte result = 0;
            int count = bitList.Count();
            if (count > 8)
                count = 8;
            for (int x = 0; x < count; x++) {
                if (bitList[x] == true)
                    result += (byte)(0x80 >> x);
            }
            return result;
        }

        public static bool[] byteIntoBits(byte value)
        {
            bool[] bitList = new bool[8];
            int andbyte = 0x80;
            for (int x = 0; x < 8; x++) {
                byte bit = (byte)(value & andbyte);
                bool boolBit = false;
                if (bit > 0)
                    boolBit = true;
                andbyte >>= 1;
                bitList[x] = boolBit;
            }
            return bitList;
        }

        public static List<bool> byteListIntoBitList(List<byte> byteList, bool leftToRight = true)
        {
            List<bool> bitList = new List<bool>();
            foreach (byte value in byteList) {
                bool[] bits = DataFormatter.byteIntoBits(value);
                if (leftToRight == false)
                    Array.Reverse(bits);
                bitList.AddRange(bits);
            }
            return bitList;
        }

        public static ushort switchReadBytesIntoUInt16(byte[] data, uint offset)
        {
            ushort value = (ushort)(data[offset + 1] * 0x100);
            value += data[offset];
            return value;
        }

        public static uint createSnesPointer(byte bank, ushort pointer)
        {
            if (bank < 0x80)
                throw new ArgumentOutOfRangeException("bank must be greater or equal to 0x80");
            if (pointer < 0x8000)
                throw new ArgumentOutOfRangeException("pointer must be greater or equal to 0x8000");
            return ((uint)bank - 0x80) * 0x8000 + ((uint)pointer - 0x8000);
        }

        public static uint readSnesPointer(byte[] data, uint offset)
        {
            byte bank = data[offset + 2];
            ushort pointer = DataFormatter.switchReadBytesIntoUInt16(data, offset);
            return DataFormatter.createSnesPointer(bank, pointer);
        }
    }
}

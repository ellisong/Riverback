using System;
using System.Collections.Generic;

namespace Riverback
{
    public static class DataFormatter
    {
        public static byte BitsIntoByte(List<bool> bitList)
        {
            byte result = 0;
            int count = bitList.Count;
            if (count > 8) {
                count = 8;
            }
            for (int x = 0; x < count; x++) {
                if (bitList[x]) {
                    result += (byte)(0x80 >> x);
                }
            }
            return result;
        }

        public static byte BitsIntoByte(bool[] bitList)
        {
            byte result = 0;
            int count = bitList.Length;
            if (count > 8) {
                count = 8;
            }
            for (int x = 0; x < count; x++) {
                if (bitList[x]) {
                    result += (byte)(0x80 >> x);
                }
            }
            return result;
        }

        public static bool[] ByteIntoBits(byte value)
        {
            bool[] bitList = new bool[8];
            int andbyte = 0x80;
            for (int x = 0; x < 8; x++) {
                byte bit = (byte)(value & andbyte);
                bool boolBit = bit > 0;
                andbyte >>= 1;
                bitList[x] = boolBit;
            }
            return bitList;
        }

        public static List<bool> ByteListIntoBitList(List<byte> byteList, bool leftToRight = true)
        {
            List<bool> bitList = new List<bool>();
            foreach (byte value in byteList) {
                bool[] bits = ByteIntoBits(value);
                if (leftToRight == false) {
                    Array.Reverse(bits);
                }
                bitList.AddRange(bits);
            }
            return bitList;
        }

        public static ushort SwitchReadBytesIntoint16(byte[] data, int offset)
        {
            ushort value = (ushort)(data[offset + 1] * 0x100);
            value += data[offset];
            return value;
        }

        public static int ConvertSnesPointerToRomPointer(byte bank, ushort pointer)
        {
            if (bank < 0x80) {
                throw new ArgumentOutOfRangeException("bank must be greater or equal to 0x80");
            }
            if (pointer < 0x8000) {
                throw new ArgumentOutOfRangeException("pointer must be greater or equal to 0x8000");
            }
            return (bank - 0x80) * 0x8000 + (pointer - 0x8000);
        }

        public static int ReadSnesPointerToRomPointer(byte[] data, int offset)
        {
            byte bank = data[offset + 2];
            ushort pointer = SwitchReadBytesIntoint16(data, offset);
            return ConvertSnesPointerToRomPointer(bank, pointer);
        }

        public static byte[] ConvertRomPointerToSnesPointer(int pointer)
        {
            byte[] snesPointer = new byte[3];
            snesPointer[2] = (byte)(pointer / 0x8000 + 0x80);
            if (snesPointer[2] % 2 == 0) {
                pointer += 0x8000;
            }
            snesPointer[1] = (byte)((pointer & 0x00FF00) >> 8);
            snesPointer[0] = (byte)(pointer & 0x0000FF);
            return snesPointer;
        }

        public static byte[] ConvertRomPointerToUInt16Pointer(ushort pointer)
        {
            byte[] uint16Pointer = new byte[2];
            uint16Pointer[1] = (byte)((pointer & 0x00FF00) >> 8);
            uint16Pointer[0] = (byte)(pointer & 0x0000FF);
            return uint16Pointer;
        }
    }
}

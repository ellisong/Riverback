using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riverback;

namespace Riverback_UnitTests
{
    [TestClass]
    public sealed class DataFormatterTests
    {
        [TestMethod()]
        public void bitsIntoByte_ValidBitList_Calculated()
        {
            byte expected = 0x25;
            bool[] input = { false, false, true, false, false, true, false, true };

            byte actual = DataFormatter.BitsIntoByte(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void byteIntoBits_ValidParameter_Calculated()
        {
            bool[] expected = { false, false, true, false, false, true, false, true};
            byte input = 0x25;

            bool[] actual = DataFormatter.ByteIntoBits(input);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void byteListIntoBitList_ValidByteAndLeftToRightIsTrue_Calculated()
        {
            bool[] expectedArray = { false, false, true, false, false, true, false, true, true, true, false, true, true, false, false, false };
            List<bool> expected = new List<bool>(expectedArray);
            byte[] inputArray = { 0x25, 0xD8 };
            List<byte> input = new List<byte>(inputArray);

            List<bool> actual = DataFormatter.ByteListIntoBitList(input);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void switchReadBytesIntoint16_ValidParameters_BytesAreSwitched()
        {
            ushort expected = 0x4962;
            byte[] input = { 0xFF, 0x62, 0x49, 0x00 };
            int offset = 1;

            ushort actual = DataFormatter.SwitchReadBytesIntoint16(input, offset);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void convertSnesPointerToRomPointer_ValidParameters_Calculated()
        {
            int expected = 0x92C74;
            byte bank = 0x92;
            ushort pointer = 0xAC74;

            int actual = DataFormatter.ConvertSnesPointerToRomPointer(bank, pointer);

            Assert.AreEqual(expected, actual);
        }
    }
}

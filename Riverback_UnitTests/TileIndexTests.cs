using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riverback;

namespace Riverback_UnitTests
{
    [TestClass]
    public class TileIndexTests
    {
        private static TileIndex _tileIndex;
        private const int Size = 8;

        [ClassInitialize()]
        public static void Initialize(TestContext tc)
        {
            _tileIndex = new TileIndex(Size);
        }

        [TestMethod()]
        public void setTileIndexArray_ValidParameters_ContainsDataFromArray()
        {
            bool[] input = { false, false, true, false, false, false, true, false };
            int expected = 2;

            _tileIndex.SetTileIndexArray(input);

            Assert.AreEqual(expected, _tileIndex.GetBankTileIndexSize());
            Assert.AreEqual(2, _tileIndex.GetBankIndexTile(0));
            Assert.AreEqual(-1, _tileIndex.GetBankIndex(4));
            Assert.AreEqual(6, _tileIndex.GetBankIndexTile(1));
            Assert.AreEqual(1, _tileIndex.GetBankIndex(6));
            for (int i = 0; i < Size; i++) {
                Assert.AreEqual(input[i], _tileIndex[i]);
            }
        }

        [TestMethod()]
        public void setTileIndexList_ValidParameters_ContainsDataFromList()
        {
            bool[] inputData = { false, false, true, false, false, false, true, false };
            List<bool> input = new List<bool>();
            input.AddRange(inputData);
            int expected = 2;

            _tileIndex.SetTileIndexList(input);

            Assert.AreEqual(expected, _tileIndex.GetBankTileIndexSize());
            Assert.AreEqual(2, _tileIndex.GetBankIndexTile(0));
            Assert.AreEqual(-1, _tileIndex.GetBankIndex(4));
            Assert.AreEqual(6, _tileIndex.GetBankIndexTile(1));
            Assert.AreEqual(1, _tileIndex.GetBankIndex(6));
            for (int i = 0; i < Size; i++) {
                Assert.AreEqual(input[i], _tileIndex[i]);
            }
        }
    }
}

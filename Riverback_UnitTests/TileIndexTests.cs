using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Riverback.Tests
{
    [TestClass]
    public class TileIndexTests
    {
        private static TileIndex tileIndex;
        private const int SIZE = 8;

        [ClassInitialize()]
        public static void initialize(TestContext tc)
        {
            tileIndex = new TileIndex(SIZE);
        }

        [TestMethod()]
        public void setTileIndexArray_ValidParameters_ContainsDataFromArray()
        {
            bool[] input = { false, false, true, false, false, false, true, false };
            int expected = 2;

            tileIndex.setTileIndexArray(input);

            Assert.AreEqual(expected, tileIndex.getBankTileIndexSize());
            Assert.AreEqual(2, tileIndex.getBankIndexTile(0));
            Assert.AreEqual(-1, tileIndex.getBankIndex(4));
            Assert.AreEqual(6, tileIndex.getBankIndexTile(1));
            Assert.AreEqual(1, tileIndex.getBankIndex(6));
            for (int i = 0; i < SIZE; i++) {
                Assert.AreEqual(input[i], tileIndex[i]);
            }
        }

        [TestMethod()]
        public void setTileIndexList_ValidParameters_ContainsDataFromList()
        {
            bool[] inputData = { false, false, true, false, false, false, true, false };
            List<bool> input = new List<bool>();
            input.AddRange(inputData);
            int expected = 2;

            tileIndex.setTileIndexList(input);

            Assert.AreEqual(expected, tileIndex.getBankTileIndexSize());
            Assert.AreEqual(2, tileIndex.getBankIndexTile(0));
            Assert.AreEqual(-1, tileIndex.getBankIndex(4));
            Assert.AreEqual(6, tileIndex.getBankIndexTile(1));
            Assert.AreEqual(1, tileIndex.getBankIndex(6));
            for (int i = 0; i < SIZE; i++) {
                Assert.AreEqual(input[i], tileIndex[i]);
            }
        }
    }
}

using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riverback;

namespace Riverback_UnitTests
{
    [TestClass()]
    public sealed class CoordinateConverterTests
    {
        private static CoordinateConverter _coordConverter;

        [ClassInitialize()]
        public static void Initialize(TestContext tc)
        {
            _coordConverter = new CoordinateConverter(8, 8, 8);
        }

        [TestMethod()]
        public void getTileNumberFromMouseCoords_ValidParameters_Calculated()
        {
            Point input = new Point(55, 17);
            int expected = 22;

            int actual = _coordConverter.GetTileNumberFromMouseCoords(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void getMouseCoordsFromTileNumber_ValidParameters_Calculated()
        {
            int input = 22;
            Point expected = new Point(48, 16);

            Point actual = _coordConverter.GetMouseCoordsFromTileNumber(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void getTileCoordsFromMouseCoords_ValidParameters_Calculated()
        {
            Point input = new Point(55, 17);
            Point expected = new Point(6, 2);

            Point actual = _coordConverter.GetTileCoordsFromMouseCoords(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void getTileNumberFromTileCoords_ValidParameters_Calculated()
        {
            Point input = new Point(6, 2);
            int expected = 22;

            int actual = _coordConverter.GetTileNumberFromTileCoords(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void getMouseCoordsFromTileCoords_ValidParameters_Calculated()
        {
            Point input = new Point(6, 2);
            Point expected = new Point(48, 16);

            Point actual = _coordConverter.GetMouseCoordsFromTileCoords(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void getTileCoordsFromTileNumber_ValidParameters_Calculated()
        {
            int input = 22;
            Point expected = new Point(6, 2);

            Point actual = _coordConverter.GetTileCoordsFromTileNumber(input);

            Assert.AreEqual(expected, actual);
        }
    }
}
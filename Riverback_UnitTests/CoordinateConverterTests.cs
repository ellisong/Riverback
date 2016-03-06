using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riverback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Riverback.Tests
{
    [TestClass()]
    public sealed class CoordinateConverterTests
    {
        private static CoordinateConverter coordConverter;

        [ClassInitialize()]
        public static void initialize(TestContext tc)
        {
            coordConverter = new CoordinateConverter(8, 8, 8);
        }

        [TestMethod()]
        public void getTileNumberFromMouseCoords_ValidParameters_Calculated()
        {
            Point input = new Point(55, 17);
            int expected = 22;

            int actual = coordConverter.getTileNumberFromMouseCoords(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void getMouseCoordsFromTileNumber_ValidParameters_Calculated()
        {
            int input = 22;
            Point expected = new Point(48, 16);

            Point actual = coordConverter.getMouseCoordsFromTileNumber(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void getTileCoordsFromMouseCoords_ValidParameters_Calculated()
        {
            Point input = new Point(55, 17);
            Point expected = new Point(6, 2);

            Point actual = coordConverter.getTileCoordsFromMouseCoords(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void getTileNumberFromTileCoords_ValidParameters_Calculated()
        {
            Point input = new Point(6, 2);
            int expected = 22;

            int actual = coordConverter.getTileNumberFromTileCoords(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void getMouseCoordsFromTileCoords_ValidParameters_Calculated()
        {
            Point input = new Point(6, 2);
            Point expected = new Point(48, 16);

            Point actual = coordConverter.getMouseCoordsFromTileCoords(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void getTileCoordsFromTileNumber_ValidParameters_Calculated()
        {
            int input = 22;
            Point expected = new Point(6, 2);

            Point actual = coordConverter.getTileCoordsFromTileNumber(input);

            Assert.AreEqual(expected, actual);
        }
    }
}
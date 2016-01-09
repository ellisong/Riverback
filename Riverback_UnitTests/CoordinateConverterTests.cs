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
    public class CoordinateConverterTests
    {
        [TestMethod()]
        public void getTileNumberFromMouseCoords_ValidParameters_Calculated()
        {
            Point input = new Point(55, 17);
            int tileAmountWidth = 8;
            int tileWidth = 8;
            int expected = 22;

            int actual = CoordinateConverter.getTileNumberFromMouseCoords(input, tileAmountWidth, tileWidth);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void getMouseCoordsFromTileNumber_ValidParameters_Calculated()
        {
            int input = 22;
            Point expected = new Point(48, 16);
            int tileAmountWidth = 8;
            int tileWidth = 8;

            Point actual = CoordinateConverter.getMouseCoordsFromTileNumber(input, tileAmountWidth, tileWidth);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void getTileCoordsFromMouseCoords_ValidParameters_Calculated()
        {
            Point input = new Point(55, 17);
            Point expected = new Point(6, 2);
            int tileWidth = 8;

            Point actual = CoordinateConverter.getTileCoordsFromMouseCoords(input, tileWidth);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void getTileNumberFromTileCoords_ValidParameters_Calculated()
        {
            Point input = new Point(6, 2);
            int tileAmountWidth = 8;
            int expected = 22;

            int actual = CoordinateConverter.getTileNumberFromTileCoords(input, tileAmountWidth);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void getMouseCoordsFromTileCoords_ValidParameters_Calculated()
        {
            Point input = new Point(6, 2);
            Point expected = new Point(48, 16);
            int tileWidth = 8;

            Point actual = CoordinateConverter.getMouseCoordsFromTileCoords(input, tileWidth);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void getTileCoordsFromTileNumber_ValidParameters_Calculated()
        {
            int input = 22;
            int tileAmountWidth = 8;
            Point expected = new Point(6, 2);

            Point actual = CoordinateConverter.getTileCoordsFromTileNumber(input, tileAmountWidth);

            Assert.AreEqual(expected, actual);
        }
    }
}
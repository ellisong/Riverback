﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riverback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riverback.Tests
{
    [TestClass()]
    public class TileEditorTests
    {
        [TestMethod()]
        public void convertPlanarTileToLinearTile_ValidParameters_ConvertedToLinearTile()
        {
            byte[] input =    { 0x88, 0x76, 0x48, 0x34, 0x30, 0x08, 0xF7, 0xE0,
                                0xEF, 0x07, 0x14, 0x08, 0x0A, 0x34, 0x09, 0x76,
                                0x0F, 0x18, 0x0E, 0x18, 0x14, 0x18, 0xE8, 0xE7,
                                0x17, 0xE7, 0x30, 0x18, 0x68, 0x18, 0xE8, 0x18 };
            byte[] expected = { 0x12, 0x2A, 0xD6, 0x64, 0x01, 0x2A, 0xD6, 0x40,
                                0x00, 0x1D, 0xA4, 0x00, 0xFF, 0xF1, 0x49, 0x99,
                                0x99, 0x94, 0x1F, 0xFF, 0x00, 0x4D, 0xA1, 0x00,
                                0x04, 0x6A, 0xD2, 0x10, 0x46, 0x6A, 0xD2, 0x21 };

            byte[] actual = TileEditor.convertPlanarTileToLinearTile(input);

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
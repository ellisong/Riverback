using System.Collections.Generic;

namespace Riverback
{
    public static class TileEditor
    {
        public static byte[] convertPlanarTileToLinearTile(byte[] tiledata)
        {
            if (tiledata.Length != 32) {
                return null;
            }
            List<byte[]> bitplane = new List<byte[]>();
            int offset = 0;
            for (int planeNum = 0; planeNum < 4; planeNum++) {
                byte[] plane = new byte[64];
                if (planeNum == 2) {
                    offset = 16;
                }
                for (int byteNum = 0; byteNum < 8; byteNum++) {
                    byte bitmask = 0x80;
                    for (int bit = 0; bit < 8; bit++) {
                        int bitValue = (tiledata[offset + (byteNum * 2)] & bitmask);
                        if (bitValue > 1) {
                            bitValue = 1;
                        }
                        plane[byteNum * 8 + bit] = (byte)(bitValue * (0x01 << planeNum));
                        bitmask >>= 1;
                    }
                }
                bitplane.Add(plane);
                offset += 1;
            }
            byte[] lineardata = new byte[32];
            byte pix = 0;
            for (int pixel = 0; pixel < 64; pixel++) {
                if ((pixel % 2) == 0) {
                    pix = 0;
                }
                for (int planeNum = 0; planeNum < 4; planeNum++) {
                    pix += (bitplane[planeNum])[pixel];
                }
                if ((pixel % 2) == 0) {
                    pix = (byte)(pix << 4);
                } else {
                    lineardata[pixel / 2] = pix;
                }
            }
            return lineardata;
        }

        public static Color[] colorLinearTileWithPalette(byte[] tiledata, Palette palette)
        {
            if (tiledata.Length != 32) {
                return null;
            }
            Color[] colors = new Color[64];
            int pointer = 0;
            int tile;
            while (pointer < 32) {
                tile = (tiledata[pointer] & 0xF0) >> 4;
                colors[pointer*2] = palette.Colors[tile];
                tile = (tiledata[pointer] & 0x0F);
                colors[pointer*2+1] = palette.Colors[tile];
                pointer += 1;
            }
            return colors;
        }

        public static byte[] getARGBarrayFromColoredLinearTile(Color[] colordata)
        {
            byte[] argbArray = new byte[colordata.Length * 4];
            int pointer = 0;
            foreach (Color col in colordata) {
                argbArray[pointer++] = col.Alpha;
                argbArray[pointer++] = col.Red;
                argbArray[pointer++] = col.Green;
                argbArray[pointer++] = col.Blue;
            }
            return argbArray;
        }
    }
}

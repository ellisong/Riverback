using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riverback
{
    class Color
    {
        private byte red;
        public byte Red
        {
            get { return red; }
            set
            {
                if ((type == false) && (value > 31))
                    red = 31;
                else
                    red = value;
            }
        }

        private byte green;
        public byte Green
        {
            get { return green; }
            set
            {
                if ((type == false) && (value > 31))
                    green = 31;
                else
                    green = value;
            }
        }

        private byte blue;
        public byte Blue
        {
            get { return blue; }
            set
            {
                if ((type == false) && (value > 31))
                    blue = 31;
                else
                    blue = value;
            }
        }

        // False = 15bit, True = 24bit
        private bool type;
        public bool Type
        {
            get { return type; }
            set
            {
                if (type != value) {
                    this.type = value;
                    switchType();
                }
            }
        }

        public Color(byte red = 0, byte green = 0, byte blue = 0, bool type = false)
        {
            this.type = type;
            this.Red = red;
            this.Green = green;
            this.Blue = blue;
        }

        private void switchType()
        {
            if (type == true) {
                ColorConvert15BitTo24Bit();
            } else {
                ColorConvert24BitTo15Bit();
            }
        }

        private static Color ColorConvert24BitTo15Bit(byte red, byte green, byte blue)
        {
            red = (byte)(red / 8);
            green = (byte)(green / 8);
            blue = (byte)(blue / 8);
            return new Color(red, green, blue, false);
        }

        private void ColorConvert24BitTo15Bit()
        {
            Color col = ColorConvert24BitTo15Bit(red, green, blue);
            Red = col.red;
            Green = col.green;
            Blue = col.blue;
        }

        private static Color ColorConvert15BitTo24Bit(byte red, byte green, byte blue)
        {
            red = (byte)(red * 8);
            red += (byte)(red / 32);
            green = (byte)(green * 8);
            green += (byte)(green / 32);
            blue = (byte)(blue * 8);
            blue += (byte)(blue / 32);
            return new Color(red, green, blue, true);
        }

        private void ColorConvert15BitTo24Bit()
        {
            Color col = ColorConvert15BitTo24Bit(red, green, blue);
            Red = col.red;
            Green = col.green;
            Blue = col.blue;
        }

        public int get24BitColor()
        {
            if (type == true) {
                return Red * 0x10000 + Green * 0x100 + Blue;
            } else {
                Color col = ColorConvert15BitTo24Bit(Red, Green, Blue);
                return col.red * 0x10000 + col.green * 0x100 + col.blue;
            }
        }

        public int get15BitColor()
        {
            if (type == false) {
                return Blue * 1024 + Green * 32 + Red;
            } else {
                Color col = ColorConvert24BitTo15Bit(Red, Green, Blue);
                return col.blue * 1024 + col.green * 32 + col.red;
            }
        }
    }
}

namespace Riverback
{
    public class Color
    {
        private byte _red;
        public byte Red
        {
            get { return _red; }
            set
            {
                if ((_type == false) && (value > 31)) {
                    _red = 31;
                } else {
                    _red = value;
                }
            }
        }

        private byte _green;
        public byte Green
        {
            get { return _green; }
            set
            {
                if ((_type == false) && (value > 31)) {
                    _green = 31;
                } else {
                    _green = value;
                }
            }
        }

        private byte _blue;
        public byte Blue
        {
            get { return _blue; }
            set
            {
                if ((_type == false) && (value > 31)) {
                    _blue = 31;
                } else {
                    _blue = value;
                }
            }
        }

        public byte Alpha { get; set; }

        // False = 15bit, True = 24bit
        private bool _type;
        public bool Type
        {
            get { return _type; }
            set
            {
                if (_type != value) {
                    _type = value;
                    SwitchType();
                }
            }
        }

        public Color(byte red = 0, byte green = 0, byte blue = 0, byte alpha = 255, bool type = false)
        {
            _type = type;
            Red = red;
            Green = green;
            Blue = blue;
            Alpha = alpha;
        }

        // Copy constructor
        public Color(Color col)
        {
            _type = col._type;
            _red = col._red;
            _green = col._green;
            _blue = col._blue;
            Alpha = col.Alpha;
        }

        public void SwitchType()
        {
            if (_type) {
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
            return new Color(red, green, blue);
        }

        private void ColorConvert24BitTo15Bit()
        {
            Color col = ColorConvert24BitTo15Bit(_red, _green, _blue);
            Red = col._red;
            Green = col._green;
            Blue = col._blue;
        }

        private static Color ColorConvert15BitTo24Bit(byte red, byte green, byte blue)
        {
            red = (byte)(red * 8);
            red += (byte)(red / 32);
            green = (byte)(green * 8);
            green += (byte)(green / 32);
            blue = (byte)(blue * 8);
            blue += (byte)(blue / 32);
            return new Color(red, green, blue, 255, true);
        }

        private void ColorConvert15BitTo24Bit()
        {
            Color col = ColorConvert15BitTo24Bit(_red, _green, _blue);
            Red = col._red;
            Green = col._green;
            Blue = col._blue;
        }

        public int Get24BitColor()
        {
            if (_type) {
                return Red * 0x10000 + Green * 0x100 + Blue;
            } else {
                Color col = ColorConvert15BitTo24Bit(Red, Green, Blue);
                return col._red * 0x10000 + col._green * 0x100 + col._blue;
            }
        }

        public int Get15BitColor()
        {
            if (_type == false) {
                return Blue * 1024 + Green * 32 + Red;
            } else {
                Color col = ColorConvert24BitTo15Bit(Red, Green, Blue);
                return col._blue * 1024 + col._green * 32 + col._red;
            }
        }
    }
}

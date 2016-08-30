using System.Collections.Generic;

namespace Riverback
{
    public class Palette
    {
        public List<Color> Colors { get; set; }

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

        public Palette(bool type = false)
        {
            Colors = new List<Color>();
            _type = type;
        }

        // Copy constructor
        public Palette(Palette pal)
        {
            Colors = pal.Colors;
            _type = pal._type;
        }

        public void Append(Color col)
        {
            Colors.Add(new Color(col));
        }

        public void SwitchType()
        {
            foreach (Color col in Colors) {
                col.Type = _type;
            }
        }

        public List<int> Get15BitColors()
        {
            List<int> colors15Bit = new List<int>();
            foreach (Color col in Colors) {
                colors15Bit.Add(col.Get15BitColor());
            }
            return colors15Bit;
        }

        public List<int> Get24BitColors()
        {
            List<int> colors24Bit = new List<int>();
            foreach (Color col in Colors) {
                colors24Bit.Add(col.Get24BitColor());
            }
            return colors24Bit;
        }

        public List<byte> Get15BitColorsAsByteList()
        {
            List<byte> byteList = new List<byte>();
            List<int> colors15Bit = Get15BitColors();
            foreach (int col in colors15Bit) {
                byteList.Add((byte)(col & 0xFF));
                byteList.Add((byte)((col & 0xFF00) >> 8));
            }
            return byteList;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riverback
{
    public class Palette
    {
        public List<Color> Colors { get; set; }

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

        public Palette(bool type = false)
        {
            Colors = new List<Color>();
            this.type = type;
        }

        // Copy constructor
        public Palette(Palette pal)
        {
            Colors = pal.Colors;
            type = pal.type;
        }

        public void append(Color col)
        {
            Colors.Add(new Color(col));
        }

        public void switchType()
        {
            foreach (Color col in Colors) {
                if (type == true) {
                    col.Type = true;
                } else {
                    col.Type = false;
                }
            }
        }

        public List<int> get15BitColors()
        {
            List<int> colors15Bit = new List<int>();
            foreach (Color col in Colors) {
                colors15Bit.Add(col.get15BitColor());
            }
            return colors15Bit;
        }

        public List<int> get24BitColors()
        {
            List<int> colors24Bit = new List<int>();
            foreach (Color col in Colors) {
                colors24Bit.Add(col.get24BitColor());
            }
            return colors24Bit;
        }

        public List<byte> get15BitColorsAsByteList()
        {
            List<byte> byteList = new List<byte>();
            List<int> colors15Bit = get15BitColors();
            foreach (int col in colors15Bit) {
                byteList.Add((byte)(col & 0xFF));
                byteList.Add((byte)((col & 0xFF00) >> 8));
            }
            return byteList;
        }
    }
}

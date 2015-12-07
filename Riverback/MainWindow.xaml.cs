using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace Riverback
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Color col = new Color(255, 143, 32, true);
            Debug.WriteLine("Color: {0}, {1}, {2}, {3}", col.Red, col.Green, col.Blue, col.Type);
            col.Type = false;
            Debug.WriteLine("Color: {0}, {1}, {2}, {3}", col.Red, col.Green, col.Blue, col.Type);
            col.Type = true;
            Debug.WriteLine("Color: {0}, {1}, {2}, {3}", col.Red, col.Green, col.Blue, col.Type);
            Debug.WriteLine(col.get15BitColor().ToString());
            Debug.WriteLine(col.get24BitColor().ToString());

            Palette pal = new Palette(true);
            for (int x = 0; x < 16; x++) {
                pal.append(col);
                Debug.WriteLine("Color24 {0} in palette: {1}, {2}, {3}, {4}", x, 
                    pal.Colors[x].Red, pal.Colors[x].Green, pal.Colors[x].Blue, pal.Colors[x].Type);
            }
            pal.Type = false;
            for (int x = 0; x < 16; x++) {
                Debug.WriteLine("Color15 {0} in palette: {1}, {2}, {3}, {4}", x,
                    pal.Colors[x].Red, pal.Colors[x].Green, pal.Colors[x].Blue, pal.Colors[x].Type);
            }
            pal.Type = true;
            for (int x = 0; x < 16; x++) {
                Debug.WriteLine("Color24 {0} in palette: {1}, {2}, {3}, {4}", x,
                    pal.Colors[x].Red, pal.Colors[x].Green, pal.Colors[x].Blue, pal.Colors[x].Type);
            }
            List<int> colors15Bit = pal.get15BitColors();
            for (int x = 0; x < 16; x++) {
               Debug.WriteLine("Color_int_15 {0} in palette: {1}", x, colors15Bit[x]);
            }
            List<int> colors24Bit = pal.get24BitColors();
            for (int x = 0; x < 16; x++) {
                Debug.WriteLine("Color_int_24 {0} in palette: {1}", x, colors24Bit[x]);
            }
            List<byte> byteList = pal.get15BitColorsAsByteList();
            for (int x = 0; x < 16; x++) {
                Debug.WriteLine("Color_byte_15 {0} in palette: {1}", x, byteList[x]);
            }
        }
    }
}

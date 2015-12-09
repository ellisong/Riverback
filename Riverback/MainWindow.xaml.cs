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
using System.IO;

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
            byte[] romdata = File.ReadAllBytes("D:/projects/Riverback/test.smc");
            byte[] decompressedData = DataCompressor.decompress(romdata, 0x70000);
            File.WriteAllBytes("D:/projects/Riverback/out.smc", decompressedData);
            byte[] compressedData = DataCompressor.compress(decompressedData);
            File.WriteAllBytes("D:/projects/Riverback/out2.smc", compressedData);
            decompressedData = DataCompressor.decompress(compressedData, 0);
            File.WriteAllBytes("D:/projects/Riverback/out3.smc", decompressedData);
        }
    }
}

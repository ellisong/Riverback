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
        }
    }
}

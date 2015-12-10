using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Riverback
{
    public partial class MainForm : Form
    {
        LevelEditor levelEditor;
        byte[] romdata;

        public MainForm()
        {
            levelEditor = new LevelEditor();
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MainMenu_FileOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                if (openFileDialog.CheckFileExists) {
                    romdata = File.ReadAllBytes(openFileDialog.FileName);
                    if (romdata != null) {
                        levelEditor.openLevel(romdata, (int)numericUpDown_levelSelector.Value);
                        levelEditor.updateGraphicsBanks(romdata);
                        levelEditor.updateLevelBank();
                        if ((levelEditor.Level != null) && (levelEditor.LevelBank != null)) {
                            Graphics g = pictureBox_tileset.CreateGraphics();
                            TileDrawer.drawAllTilesOnCanvas(levelEditor.LevelBank, g, 16, LevelEditor.DEFAULT_BANK_PALETTE);
                            g.Dispose();
                            g = pictureBox_level.CreateGraphics();
                            TileDrawer.drawLevelOnCanvas(g, levelEditor.Level, levelEditor.LevelBank);
                            g.Dispose();
                        }
                    }
                }
            }
        }
    }
}

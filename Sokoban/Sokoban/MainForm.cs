using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sokoban
{
    public partial class MainForm : Form
    {
        private SokobanManager m_SokobanManager;

        public MainForm()
        {
            this.InitializeComponent();
            this.m_SokobanManager = new Sokoban.SokobanManager(this.m_PictureBox);
        }

        private void m_PictureBox_SizeChanged(object sender, EventArgs e)
        {
            this.m_SokobanManager.Refresh();
        }

        private void m_PictureBox_Paint(object sender, PaintEventArgs e)
        {
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    this.m_SokobanManager.Move(0);
                    break;
                case Keys.Down:
                    this.m_SokobanManager.Move(1);
                    break;
                case Keys.Left:
                    this.m_SokobanManager.Move(2);
                    break;
                case Keys.Right:
                    this.m_SokobanManager.Move(3);
                    break;
                case Keys.E:
                    this.m_SokobanManager.Ronate(0);
                    break;
                case Keys.Q:
                    this.m_SokobanManager.Ronate(1);
                    break;
                default:break;
            }
        }
    }
}
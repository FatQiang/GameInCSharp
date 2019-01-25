using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeper
{
    public partial class Mainform : Form
    {
        MineClearManager m_MineManager;

        public Mainform()
        {
            this.InitializeComponent();
        }

        private void Mainform_Load(object sender, EventArgs e)
        {
            this.m_ComboBoxType.SelectedIndex = 0;
            this.m_ButtonOK_Click(null, null);
        }

        private void m_ButtonOK_Click(object sender, EventArgs e)
        {
            int tSize = 0;
            switch (this.m_ComboBoxType.SelectedIndex)
            {
                case -1: return;
                case 0: tSize = 5; break;
                case 1: tSize = 10; break;
                case 2: tSize = 20; break;
                case 3: tSize = 30; break;
                case 4: tSize = 40; break;
                case 5: tSize = 50; break;
                case 6: tSize = 100; break;
                default: return;
            }
            int tMineCount = int.Parse(this.m_TextBoxMineCount.Text);
            this.m_MineManager = new MineClearManager(tSize, tSize, tMineCount);

            this.RefreshBackground();
        }

        private void RefreshBackground()
        {
            Bitmap tImage = new Bitmap(this.m_PictureBoxMain.Width, this.m_PictureBoxMain.Height);
            Graphics tGraphics = Graphics.FromImage(tImage);

            int tRowCount = this.m_MineManager.RowCount;
            int tColumnCount = this.m_MineManager.ColumnCount;

            int tWidth = tImage.Width / tColumnCount;
            int tHeight = tImage.Height / tRowCount;

            //绘制扫雷地图
            for (int i = 0; i < tRowCount; i++)
            {
                for (int j = 0; j < tColumnCount; j++)
                {
                    if (this.m_MineManager.m_Clear[i, j] == 1)
                    {
                        if (this.m_MineManager.m_Mine[i, j] == 0)
                        {
                            tGraphics.FillRectangle(Brushes.Wheat, j * tWidth, i * tHeight, tWidth, tHeight);
                            int tMineCount = this.m_MineManager.GetMineCount(i, j);
                            if (tMineCount != 0)
                            {
                                tGraphics.DrawString(tMineCount.ToString(), SystemFonts.DefaultFont, Brushes.Black, j * tWidth + tWidth / 2, i * tHeight + tHeight / 2);
                            }
                        }
                        else
                        {
                            tGraphics.FillRectangle(Brushes.Red, j * tWidth, i * tHeight, tWidth, tHeight);
                        }
                    }
                    else if (this.m_MineManager.m_Clear[i, j] == 0)
                    {
                        tGraphics.FillRectangle(Brushes.LightGreen, j * tWidth, i * tHeight, tWidth, tHeight);
                    }
                    else
                    {
                        tGraphics.FillRectangle(Brushes.LightBlue, j * tWidth, i * tHeight, tWidth, tHeight);
                    }
                }
            }

            for (int i = 0; i <= tRowCount; i++)
            {
                Point tFromPnt = new Point(0, i * tHeight);
                Point tToPnt = new Point(tImage.Width, i * tHeight);
                tGraphics.DrawLine(Pens.White, tFromPnt, tToPnt);
            }
            for (int i = 0; i <= tColumnCount; i++)
            {
                Point tFromPnt = new Point(i * tWidth, 0);
                Point tToPnt = new Point(i * tWidth, tImage.Height);
                tGraphics.DrawLine(Pens.White, tFromPnt, tToPnt);
            }

            this.m_PictureBoxMain.Image = tImage;
        }

        private void m_PictureBoxMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (!this.m_MineManager.GetStatus())
            {
                int tRowCount = this.m_MineManager.RowCount;
                int tColumnCount = this.m_MineManager.ColumnCount;

                int tWidth = this.m_PictureBoxMain.Width / tColumnCount;
                int tHeight = this.m_PictureBoxMain.Height / tRowCount;

                //判断是在哪一个块
                int tRowIndex = e.Y / tHeight;
                int tColumnIndex = e.X / tWidth;

                if (e.Button == MouseButtons.Left)
                {
                    if (this.m_MineManager.ClearMine(tRowIndex, tColumnIndex))
                    {
                        this.RefreshBackground();
                        if (this.m_MineManager.GetStatus())
                        {
                            MessageBox.Show("You Win");
                        }
                    }
                    else
                    {
                        this.RefreshBackground();
                        MessageBox.Show("You Lost");
                    }
                }
                else
                {
                    this.m_MineManager.FlagMine(tRowIndex, tColumnIndex);
                    this.RefreshBackground();
                }
            }
        }

        private void m_ComboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        
        private void m_PictureBoxMain_SizeChanged(object sender, EventArgs e)
        {
            this.RefreshBackground();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SokobanLevelEditor
{
    public partial class MainForm : Form
    {
        private enum MouseType
        {
            Floor,
            Wall,
            Target,
            Man,
            Box,
            Delete
        }

        private MouseType m_MouseType;

        /// <summary>
        /// 用于记录地图，0表示地面，1表示墙,5表示箱子的终点
        /// </summary>
        private int[,] m_Map;

        /// <summary>
        /// 人的行索引
        /// </summary>
        private int m_ManRowIndex = -1;

        /// <summary>
        /// 人的列索引
        /// </summary>
        private int m_ManColumnIndex = -1;

        /// <summary>
        /// 箱子序列
        /// </summary>
        private List<int[]> m_BoxList;



        public MainForm()
        {
            this.InitializeComponent();
            this.m_ButtonInitMap_Click(null, null);
        }

        private void m_ButtonInitMap_Click(object sender, EventArgs e)
        {
            this.m_BoxList = new List<int[]>();

            int tRowCount = (int)this.m_NumericUpDownRow.Value;
            int tColumnCount = (int)this.m_NumericUpDownColumn.Value;
            int tWidth = this.m_PictureBox.Width;
            int tHeight = this.m_PictureBox.Height;
            int tCellWidth = tWidth / tColumnCount;
            int tCellHeight = tHeight / tRowCount;

            //默认全部都是地面
            this.m_Map = new int[tRowCount, tColumnCount];
            for (int i = 0; i < tRowCount; i++)
            {
                for (int j = 0; j < tColumnCount; j++)
                {
                    this.m_Map[i, j] = 0;
                }
            }

            this.DrawMap();
        }

        private void m_ButtonAddWall_Click(object sender, EventArgs e)
        {
            this.m_MouseType = MouseType.Wall;
        }

        private void m_ButtonAddTarget_Click(object sender, EventArgs e)
        {
            this.m_MouseType = MouseType.Target;
        }

        private void m_ButtonAddMan_Click(object sender, EventArgs e)
        {
            this.m_MouseType = MouseType.Man;
        }

        private void m_ButtonFloor_Click(object sender, EventArgs e)
        {
            this.m_MouseType = MouseType.Floor;
        }

        private void m_ButtonAddBox_Click(object sender, EventArgs e)
        {
            this.m_MouseType = MouseType.Box;
        }

        private void m_ButtonDelete_Click(object sender, EventArgs e)
        {
            this.m_MouseType = MouseType.Delete;
        }

        private void InitMap()
        {
            int tRowCount = (int)this.m_NumericUpDownRow.Value;
            int tColumnCount = (int)this.m_NumericUpDownColumn.Value;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void m_MenuItemSelectLevel_Click(object sender, EventArgs e)
        {

        }

        private void m_PictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            int tX = e.X;
            int tY = e.Y;
            //计算行列索引
            int tRowCount = this.m_Map.GetLength(0);
            int tColumnCount = this.m_Map.GetLength(1);
            int tWidth = this.m_PictureBox.Width;
            int tHeight = this.m_PictureBox.Height;
            int tCellWidth = tWidth / tColumnCount;
            int tCellHeight = tHeight / tRowCount;

            int tColumnIndex = (int)(tX / tCellWidth);
            int tRowIndex = (int)(tY / tCellHeight);

            if (tRowIndex < 0 || tRowIndex >= tRowCount || tColumnIndex < 0 || tColumnIndex >= tColumnCount)
            {
                return;
            }

            switch (this.m_MouseType)
            {
                case MouseType.Floor:
                    this.m_Map[tRowIndex, tColumnIndex] = 0;
                    break;
                case MouseType.Wall:
                    this.m_Map[tRowIndex, tColumnIndex] = 1;
                    if (this.m_ManRowIndex == tRowIndex && this.m_ManColumnIndex == tColumnIndex)
                    {
                        this.m_ManRowIndex = this.m_ManColumnIndex = -1;
                    }
                    if (this.m_BoxList.Count(t => t[0] == tRowIndex && t[1] == tColumnIndex) >= 1)
                    {
                        int tBoxIndex = this.m_BoxList.FindIndex(t => t[0] == tRowIndex && t[1] == tColumnIndex);
                        this.m_BoxList.RemoveAt(tBoxIndex);
                    }
                    break;
                case MouseType.Man:
                    if (this.m_Map[tRowIndex, tColumnIndex] == 0 && this.m_BoxList.Count(t => t[0] == tRowIndex && t[1] == tColumnIndex) == 0)
                    {
                        this.m_ManRowIndex = tRowIndex;
                        this.m_ManColumnIndex = tColumnIndex;
                    }
                    break;
                case MouseType.Box:
                    //将地图该位置改为地面
                    this.m_Map[tRowIndex, tColumnIndex] = 0;
                    //先判断当前的位置是否存在箱子
                    if (this.m_BoxList.Count(t => t[0] == tRowIndex && t[1] == tColumnIndex) == 0)
                    {
                        this.m_BoxList.Add(new int[] { tRowIndex, tColumnIndex });
                    }
                    //判断该位置是否存在人
                    if (this.m_ManRowIndex == tRowIndex && this.m_ManColumnIndex == tColumnIndex)
                    {
                        this.m_ManRowIndex = this.m_ManColumnIndex = -1;
                    }
                    break;
                case MouseType.Target:
                    this.m_Map[tRowIndex, tColumnIndex] = 5;
                    break;
                case MouseType.Delete:
                    //如果有人
                    if (this.m_ManRowIndex == tRowIndex && this.m_ManColumnIndex == tColumnIndex)
                    {
                        this.m_ManRowIndex = this.m_ManColumnIndex = -1;
                    }
                    else if (this.m_BoxList.Count(t => t[0] == tRowIndex && t[1] == tColumnIndex) > 0)//如果有箱子
                    {
                        int tBoxIndex = this.m_BoxList.FindIndex(t => t[0] == tRowIndex && t[1] == tColumnIndex);
                        this.m_BoxList.RemoveAt(tBoxIndex);
                    }
                    else
                    {
                        this.m_Map[tRowIndex, tColumnIndex] = -1;
                    }
                    break;
                default: break;
            }

            this.DrawMap();
        }

        private void m_PictureBox_SizeChanged(object sender, EventArgs e)
        {
            this.DrawMap();
        }

        private void DrawMap()
        {

            int tRowCount = this.m_Map.GetLength(0);
            int tColumnCount = this.m_Map.GetLength(1);
            int tWidth = this.m_PictureBox.Width;
            int tHeight = this.m_PictureBox.Height;
            int tCellWidth = tWidth / tColumnCount;
            int tCellHeight = tHeight / tRowCount;

            //绘制网格
            Bitmap tBitmap = new Bitmap(tWidth, tHeight);
            Graphics tGraphics = Graphics.FromImage(tBitmap);
            tGraphics.Clear(Color.SkyBlue);

            for (int i = 0; i < tRowCount; i++)
            {
                for (int j = 0; j < tColumnCount; j++)
                {

                    Rectangle tRect = new Rectangle(j * tCellWidth, i * tCellHeight, tCellWidth, tCellHeight);
                    if (this.m_Map[i, j] == 0)
                    {
                        tGraphics.FillRectangle(Brushes.LightGreen, tRect);
                        tGraphics.DrawRectangle(Pens.White, tRect);
                    }
                    else if (this.m_Map[i, j] == 1)
                    {
                        tGraphics.FillRectangle(Brushes.Wheat, tRect);
                        tGraphics.DrawRectangle(Pens.Gray, tRect);
                    }
                    else if (this.m_Map[i, j] == 5)
                    {
                        tGraphics.FillRectangle(Brushes.LightSalmon, tRect);
                        tGraphics.DrawRectangle(Pens.White, tRect);
                    }
                }
            }

            //画人
            if (this.m_ManRowIndex >= 0 && this.m_ManColumnIndex >= 0)
            {
                Rectangle tRect = new Rectangle(this.m_ManColumnIndex * tCellWidth, this.m_ManRowIndex * tCellHeight, tCellWidth, tCellHeight);
                tGraphics.DrawImage(Properties.Resources.face, tRect);
            }
            //画箱子
            for (int i = 0; i < this.m_BoxList.Count; i++)
            {
                int tRowIndex = this.m_BoxList[i][0];
                int tColumnIndex = this.m_BoxList[i][1];

                Rectangle tRect = new Rectangle(tColumnIndex * tCellWidth, tRowIndex * tCellHeight, tCellWidth, tCellHeight);
                tGraphics.FillRectangle(Brushes.Orange, tRect);
                tGraphics.DrawRectangle(Pens.White, tRect);
            }

            this.m_PictureBox.Image = tBitmap;
        }

        private void m_MenuItemSaveLevel_Click(object sender, EventArgs e)
        {
            //判断是否符合保存的条件
            if (this.m_ManRowIndex < 0 || this.m_ManColumnIndex < 0)
            {
                MessageBox.Show("请先设置人物位置！");
                return;
            }
            int tBoxCount = this.m_BoxList.Count;
            if (tBoxCount == 0)
            {
                MessageBox.Show("请设置箱子位置！");
                return;
            }
            //判断箱子数量与终点数量是否一致
            int tTargetCount = 0;
            for (int i = 0; i < this.m_Map.GetLength(0); i++)
            {
                for (int j = 0; j < this.m_Map.GetLength(1); j++)
                {
                    if (this.m_Map[i, j] == 5)
                    {
                        tTargetCount++;
                    }
                }
            }
            if (tBoxCount > tTargetCount)
            {
                MessageBox.Show("箱子数量不能大于终点的数量！");
                return;
            }

            //全部通过，保存
            string tFileName = Application.StartupPath + "\\Levels\\" + this.m_TextBoxName.Text;
            if (File.Exists(tFileName))
            {
                if (MessageBox.Show("已经存在相同名称的关卡，是否覆盖？","提示",MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    return;
                }
            }
            FileStream tFileStream = new FileStream(tFileName, FileMode.Create);
            StreamWriter tStreamWriter = new StreamWriter(tFileStream);
            tStreamWriter.WriteLine("Man Location");
            tStreamWriter.WriteLine(this.m_ManRowIndex + "," + this.m_ManColumnIndex);
            tStreamWriter.WriteLine("Box Count");
            tStreamWriter.WriteLine(this.m_BoxList.Count);
            tStreamWriter.WriteLine("Boxes Location");
            for (int i = 0; i < this.m_BoxList.Count; i++)
            {
                tStreamWriter.WriteLine(this.m_BoxList[i][0] + "," + this.m_BoxList[i][1]);
            }
            tStreamWriter.WriteLine("Map");
            for (int i = 0; i < this.m_Map.GetLength(0); i++)
            {
                string tRowStr = this.m_Map[i, 0].ToString();
                for (int j = 1; j < this.m_Map.GetLength(1); j++)
                {
                    tRowStr += "," + this.m_Map[i, j].ToString();
                }
                tStreamWriter.WriteLine(tRowStr);
            }
            tStreamWriter.Flush();
            tStreamWriter.Close();
            tFileStream.Close();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sokoban
{
    public class SokobanManager
    {
        /// <summary>
        /// 用二维数组表示地图，0表示地面，1表示墙,3表示人，5表示箱子的终点
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
        /// 动态效果时长
        /// </summary>
        private int m_IntervalMax = 20;

        /// <summary>
        /// 动态效果计时
        /// </summary>
        private int m_Interval = 0;

        /// <summary>
        /// 箱子序列
        /// </summary>
        private Dictionary<int, int[]> m_BoxList;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="pPictureBox"></param>
        public SokobanManager(PictureBox pPictureBox,LevelClass pLevel)
        {
            this.m_PictureBox = pPictureBox;

            this.m_Map = pLevel.Map;

            this.m_ManRowIndex = pLevel.ManLocation[0];
            this.m_ManColumnIndex = pLevel.ManLocation[1];

            this.m_BoxList = new Dictionary<int, int[]>();

            for (int i = 0; i < pLevel.BoxList.Count; i++)
            {
                this.m_BoxList.Add(i, pLevel.BoxList[i]);
            }

            this.m_ManImage = Properties.Resources.right;

            this.Refresh();
        }

        /// <summary>
        /// 标记是否正在移动
        /// </summary>
        private bool m_Moving = false;

        /// <summary>
        /// 标记是否正在移动
        /// </summary>
        public bool Moving
        {
            get
            {
                return this.m_Moving;
            }
        }

        /// <summary>
        /// 画板所在的picturebox
        /// </summary>
        private PictureBox m_PictureBox;

        /// <summary>
        /// 计时器
        /// </summary>
        private Timer m_Timer;

        /// <summary>
        /// 缓存当前人的状态的图片
        /// </summary>
        private Bitmap m_ManImage = null;

        /// <summary>
        /// 人移动方向，0向上，1向下，2向左，3向右
        /// </summary>
        /// <param name="pDirection"></param>
        /// <returns></returns>
        public void Move(int pDirection)
        {
            if (!this.Moving)
            {
                int tManRowIndex = this.m_ManRowIndex, tManColumnIndex = this.m_ManColumnIndex;
                int tNextRowIndex = 0, tNextColumnIndex = 0, tNextNextRowIndex = 0, tNextNextColumnIndex = 0;

                //判断能否移动
                #region --判断能否移动--

                switch (pDirection)
                {
                    case 0:
                        tNextRowIndex = tManRowIndex - 1;
                        tNextColumnIndex = tManColumnIndex;
                        tNextNextRowIndex = tManRowIndex - 2;
                        tNextNextColumnIndex = tManColumnIndex;
                        this.m_ManImage = Properties.Resources.up;
                        break;
                    case 1:
                        tNextRowIndex = tManRowIndex + 1;
                        tNextColumnIndex = tManColumnIndex;
                        tNextNextRowIndex = tManRowIndex + 2;
                        tNextNextColumnIndex = tManColumnIndex;
                        this.m_ManImage = Properties.Resources.down;
                        break;
                    case 2:
                        tNextRowIndex = tManRowIndex;
                        tNextColumnIndex = tManColumnIndex - 1;
                        tNextNextRowIndex = tManRowIndex;
                        tNextNextColumnIndex = tManColumnIndex - 2;
                        this.m_ManImage = Properties.Resources.left;
                        break;
                    case 3:
                        tNextRowIndex = tManRowIndex;
                        tNextColumnIndex = tManColumnIndex + 1;
                        tNextNextRowIndex = tManRowIndex;
                        tNextNextColumnIndex = tManColumnIndex + 2;
                        this.m_ManImage = Properties.Resources.right;
                        break;
                    default: break;
                }

                //下一方向超出地图时，不移动
                if (tNextRowIndex >= this.m_Map.GetLength(0) || tNextRowIndex < 0 || tNextColumnIndex >= this.m_Map.GetLength(1) || tNextColumnIndex < 0)
                {
                    return;
                }

                //下一方向时墙，不移动
                if (this.m_Map[tNextRowIndex, tNextColumnIndex] == 1 || this.m_Map[tNextRowIndex, tNextColumnIndex] == -1)
                {
                    return;
                }
                else
                {
                    //下一方向时箱子时，需要继续判断
                    if (this.m_BoxList.Count(t => t.Value[0] == tNextRowIndex && t.Value[1] == tNextColumnIndex) == 1)
                    {
                        //箱子会被推出地图时，不移动
                        if (tNextNextRowIndex >= this.m_Map.GetLength(0) || tNextNextRowIndex < 0 || tNextNextColumnIndex >= this.m_Map.GetLength(1) || tNextNextColumnIndex < 0)
                        {
                            return;
                        }
                        //箱子下一步是墙或者其他箱子时，不移动
                        if (this.m_BoxList.Count(t => t.Value[0] == tNextNextRowIndex && t.Value[1] == tNextNextColumnIndex) == 1 || this.m_Map[tNextNextRowIndex, tNextNextColumnIndex] == 1)
                        {
                            return;
                        }
                    }
                }

                #endregion --判断能否移动--

                this.m_Moving = true;
                this.m_Timer = new Timer();
                this.m_Timer.Interval = 20;
                this.m_Timer.Tick += this.Timer_Tick;
                this.m_Timer.Tag = pDirection;
                this.m_Timer.Start();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            int tDirection = int.Parse(this.m_Timer.Tag.ToString());

            int tWidth = this.m_PictureBox.Width;
            int tHeight = this.m_PictureBox.Height;

            int tRowCount = this.m_Map.GetLength(0);
            int tColumnCount = this.m_Map.GetLength(1);

            Bitmap tBackgroundImage = new Bitmap(tWidth, tHeight);
            Graphics tGraphics = Graphics.FromImage(tBackgroundImage);
            tGraphics.Clear(Color.SkyBlue);

            PointF tPointUpCenter = new PointF(tWidth / 2, 0);
            PointF tPointBottomCenter = new PointF(tWidth / 2, tHeight);
            PointF tPointLeftCenter = new PointF(0, tHeight / 2);
            PointF tPointRightCenter = new PointF(tWidth, tHeight / 2);

            //增量
            PointF tVectorRow = new PointF((tPointRightCenter.X - tPointUpCenter.X) / tColumnCount, (tPointRightCenter.Y - tPointUpCenter.Y) / tColumnCount);
            PointF tVectorColumn = new PointF((tPointLeftCenter.X - tPointUpCenter.X) / tRowCount, (tPointLeftCenter.Y - tPointUpCenter.Y) / tRowCount);

            int tWallHeight = tHeight / 20;
            int tBoxHeight = tWallHeight * 2;

            int tManRowIndex = this.m_ManRowIndex, tManColumnIndex = this.m_ManColumnIndex;
            int tManNextRowIndex = tManRowIndex, tManNextColumnIndex = tManColumnIndex;

            #region --计算方向--

            PointF tSpeed = new PointF(0, 0);

            switch (tDirection)
            {
                case 0:
                    tSpeed = new PointF(-tVectorColumn.X / this.m_IntervalMax, -tVectorColumn.Y / this.m_IntervalMax);
                    tManNextRowIndex = tManRowIndex - 1;
                    break;
                case 1:
                    tSpeed = new PointF(tVectorColumn.X / this.m_IntervalMax, tVectorColumn.Y / this.m_IntervalMax);
                    tManNextRowIndex = tManRowIndex + 1;
                    break;
                case 2:
                    tSpeed = new PointF(-tVectorRow.X / this.m_IntervalMax, -tVectorRow.Y / this.m_IntervalMax);
                    tManNextColumnIndex = tManColumnIndex - 1;
                    break;
                case 3:
                    tSpeed = new PointF(tVectorRow.X / this.m_IntervalMax, tVectorRow.Y / this.m_IntervalMax);
                    tManNextColumnIndex = tManColumnIndex + 1;
                    break;
                default: break;
            }

            #endregion --计算方向--

            //判断是否由箱子需要推动
            int tMovingBoxKey = -1;

            if (this.m_BoxList.Count(t => t.Value[0] == tManNextRowIndex && t.Value[1] == tManNextColumnIndex) == 1)
            {
                tMovingBoxKey = this.m_BoxList.First(t => t.Value[0] == tManNextRowIndex && t.Value[1] == tManNextColumnIndex).Key;
            }

            int tBoxNextRowIndex = -1, tBoxNextColumnIndex = -1;

            if (tMovingBoxKey >= 0)
            {
                switch (tDirection)
                {
                    case 0:
                        tBoxNextRowIndex = this.m_BoxList[tMovingBoxKey][0] - 1;
                        tBoxNextColumnIndex = this.m_BoxList[tMovingBoxKey][1];
                        break;
                    case 1:
                        tBoxNextRowIndex = this.m_BoxList[tMovingBoxKey][0] + 1;
                        tBoxNextColumnIndex = this.m_BoxList[tMovingBoxKey][1];
                        break;
                    case 2:
                        tBoxNextRowIndex = this.m_BoxList[tMovingBoxKey][0];
                        tBoxNextColumnIndex = this.m_BoxList[tMovingBoxKey][1] - 1;
                        break;
                    case 3:
                        tBoxNextRowIndex = this.m_BoxList[tMovingBoxKey][0];
                        tBoxNextColumnIndex = this.m_BoxList[tMovingBoxKey][1] + 1;
                        break;
                    default: break;
                }
            }

            #region --绘制--

            //绘制墙壁
            for (int i = 0; i < tRowCount; i++)
            {
                for (int j = 0; j < tColumnCount; j++)
                {
                    this.Draw(tGraphics, i, j, tVectorRow, tVectorColumn, tPointUpCenter, tWallHeight);

                    if ((i == tManRowIndex && j == tManColumnIndex) || (i == tManNextRowIndex && j == tManNextColumnIndex))
                    {
                        this.DrawMan(tGraphics, tManRowIndex, tManColumnIndex, tVectorRow, tVectorColumn, tPointUpCenter, tWallHeight, tSpeed);
                    }
                    if (this.m_BoxList.Count(t => t.Value[0] == i && t.Value[1] == j) == 1)
                    {
                        KeyValuePair<int, int[]> tTempKeyValuePair = this.m_BoxList.First(t => t.Value[0] == i && t.Value[1] == j);
                        if (tTempKeyValuePair.Key != tMovingBoxKey)
                        {
                            this.DrawBox(tGraphics, tTempKeyValuePair.Value[0], tTempKeyValuePair.Value[1], tVectorRow, tVectorColumn, tPointUpCenter, tWallHeight, new PointF(0, 0));
                        }
                    }

                    if (tMovingBoxKey != -1)
                    {
                        if ((i == this.m_BoxList[tMovingBoxKey][0] && j == this.m_BoxList[tMovingBoxKey][1]) || (i == tBoxNextRowIndex && j == tBoxNextColumnIndex))
                        {
                            this.DrawBox(tGraphics, this.m_BoxList[tMovingBoxKey][0], this.m_BoxList[tMovingBoxKey][1], tVectorRow, tVectorColumn, tPointUpCenter, tWallHeight, tSpeed);
                        }
                    }
                }
            }

            #region --重新绘制被人和箱子覆盖的部分--

            switch (tDirection)
            {
                case 0:
                case 1:
                    //绘制墙壁
                    for (int i = 0; i < tRowCount; i++)
                    {
                        for (int j = tManColumnIndex + 1; j < tColumnCount; j++)
                        {
                            this.Draw(tGraphics, i, j, tVectorRow, tVectorColumn, tPointUpCenter, tWallHeight);

                            if (this.m_BoxList.Count(t => t.Value[0] == i && t.Value[1] == j) == 1)
                            {
                                KeyValuePair<int, int[]> tTempKeyValuePair = this.m_BoxList.First(t => t.Value[0] == i && t.Value[1] == j);
                                if (tTempKeyValuePair.Key != tMovingBoxKey)
                                {
                                    this.DrawBox(tGraphics, tTempKeyValuePair.Value[0], tTempKeyValuePair.Value[1], tVectorRow, tVectorColumn, tPointUpCenter, tWallHeight, new PointF(0, 0));
                                }
                            }

                        }
                    }
                    break;
                default: break;
            }

            #endregion --重新绘制被人和箱子覆盖的部分--

            #endregion --绘制--

            this.m_PictureBox.Image = tBackgroundImage;

            this.m_Interval++;
            if (this.m_Interval > this.m_IntervalMax)
            {
                this.m_Interval = 0;
                this.m_Timer.Stop();
                this.m_Moving = false;

                //更新地图

                switch (tDirection)
                {
                    case 0:
                        this.m_ManRowIndex--;
                        break;
                    case 1:
                        this.m_ManRowIndex++;
                        break;
                    case 2:
                        this.m_ManColumnIndex--;
                        break;
                    case 3:
                        this.m_ManColumnIndex++;
                        break;
                }
                if (tMovingBoxKey > -1)
                {
                    this.m_BoxList.Remove(tMovingBoxKey);
                    this.m_BoxList.Add(tMovingBoxKey, new int[] { tBoxNextRowIndex, tBoxNextColumnIndex });
                }

                bool tWin = true;
                //判断是否赢了
                foreach (KeyValuePair<int, int[]> tKeyValue in this.m_BoxList)
                {
                    if (this.m_Map[tKeyValue.Value[0], tKeyValue.Value[1]] != 5)
                    {
                        tWin = false;
                        break;
                    }
                }
                if (tWin)
                {
                    MessageBox.Show("成功！");
                    this.m_Moving = true;
                }

                this.Refresh();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pRonate">0视图向左旋转，1视图向右旋转，-1不旋转视图</param>
        public void Ronate(int pRonate)
        {
            int[,] tNewMap = new int[this.m_Map.GetLength(1), this.m_Map.GetLength(0)];

            if (pRonate == 0)
            {

            }
            else
            {

            }

            this.Refresh();
        }

        /// <summary>
        /// 绘制地面和墙壁
        /// </summary>
        /// <param name="pGraphics">画板</param>
        /// <param name="pRowIndex">行索引</param>
        /// <param name="pColumnIndex">列索引</param>
        /// <param name="pVectorRow">行向量</param>
        /// <param name="pVectorColumn">列向量</param>
        /// <param name="pUpCenter">上部中心</param>
        /// <param name="pWallHeight">墙高度</param>
        private void Draw(Graphics pGraphics, int pRowIndex, int pColumnIndex, PointF pVectorRow, PointF pVectorColumn,PointF pUpCenter,float pWallHeight)
        {
            PointF tPoint0 = new PointF(pUpCenter.X + pColumnIndex * pVectorRow.X + pRowIndex * pVectorColumn.X, pUpCenter.Y + pColumnIndex * pVectorColumn.Y + pRowIndex * pVectorColumn.Y);
            PointF tPoint1 = new PointF(tPoint0.X, tPoint0.Y - pWallHeight);
            PointF tPoint2 = new PointF(tPoint1.X + pVectorRow.X, tPoint1.Y + pVectorRow.Y);
            PointF tPoint3 = new PointF(tPoint2.X + pVectorColumn.X, tPoint2.Y + pVectorColumn.Y);
            PointF tPoint4 = new PointF(tPoint1.X + pVectorColumn.X, tPoint1.Y + pVectorColumn.Y);
            PointF tPoint5 = new PointF(tPoint3.X, tPoint3.Y + pWallHeight);
            PointF tPoint6 = new PointF(tPoint4.X, tPoint4.Y + pWallHeight);
            PointF tPoint7 = new PointF(tPoint2.X, tPoint2.Y + pWallHeight);
            PointF tPoint8 = new PointF(tPoint1.X, tPoint1.Y - pWallHeight);
            PointF tPoint9 = new PointF(tPoint2.X, tPoint2.Y - pWallHeight);
            PointF tPoint10 = new PointF(tPoint3.X, tPoint3.Y - pWallHeight);
            PointF tPoint11 = new PointF(tPoint4.X, tPoint4.Y - pWallHeight);

            if (this.m_Map[pRowIndex, pColumnIndex] != -1)//地面
            {
                //GDI
                pGraphics.DrawPolygon(Pens.White, new PointF[] { tPoint0, tPoint7, tPoint5, tPoint6 });
                pGraphics.FillPolygon(Brushes.LightGreen, new PointF[] { tPoint0, tPoint7, tPoint5, tPoint6 });

                //贴图
            }

            if (this.m_Map[pRowIndex, pColumnIndex] == 1)//墙壁
            {
                //GDI
                pGraphics.FillPolygon(Brushes.Wheat, new PointF[] { tPoint1, tPoint2, tPoint3, tPoint4 });
                pGraphics.FillPolygon(Brushes.Wheat, new PointF[] { tPoint3, tPoint4, tPoint6, tPoint5 });
                pGraphics.FillPolygon(Brushes.Wheat, new PointF[] { tPoint2, tPoint3, tPoint5, tPoint7 });
                pGraphics.DrawPolygon(Pens.Gray, new PointF[] { tPoint1, tPoint2, tPoint3, tPoint4 });
                pGraphics.DrawPolygon(Pens.Gray, new PointF[] { tPoint3, tPoint4, tPoint6, tPoint5 });
                pGraphics.DrawPolygon(Pens.Gray, new PointF[] { tPoint2, tPoint3, tPoint5, tPoint7 });
            }
            else if (this.m_Map[pRowIndex, pColumnIndex] == 5)
            {
                //GDI
                pGraphics.DrawPolygon(Pens.White, new PointF[] { tPoint0, tPoint7, tPoint5, tPoint6 });
                pGraphics.FillPolygon(Brushes.LightSalmon, new PointF[] { tPoint0, tPoint7, tPoint5, tPoint6 });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pGraphics"></param>
        /// <param name="pRowIndex"></param>
        /// <param name="pColumnIndex"></param>
        /// <param name="pVectorRow"></param>
        /// <param name="pVectorColumn"></param>
        /// <param name="pUpCenter"></param>
        /// <param name="pWallHeight"></param>
        /// <param name="pSpeed"></param>
        private void DrawMan(Graphics pGraphics, int pRowIndex, int pColumnIndex, PointF pVectorRow, PointF pVectorColumn, PointF pUpCenter, float pWallHeight, PointF pSpeed)
        {
            PointF tPoint0 = new PointF(pUpCenter.X + pColumnIndex * pVectorRow.X + pRowIndex * pVectorColumn.X + pSpeed.X * this.m_Interval, pUpCenter.Y + pColumnIndex * pVectorColumn.Y + pRowIndex * pVectorColumn.Y + pSpeed.Y * this.m_Interval + pWallHeight/2);
            PointF[] tPoints = null;
            PointF tPoint1 = new PointF(tPoint0.X + pVectorColumn.X, tPoint0.Y + pVectorColumn.Y);
            PointF tPoint2 = new PointF(tPoint1.X, tPoint1.Y - 4 * pWallHeight);
            PointF tPoint3 = new PointF(tPoint0.X + pVectorRow.X, tPoint0.Y + pVectorRow.Y);
            PointF tPoint4 = new PointF(tPoint3.X, tPoint3.Y - 4 * pWallHeight);
            tPoints = new PointF[] { tPoint2, tPoint4, tPoint1 };
            pGraphics.DrawImage(this.m_ManImage, tPoints);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pGraphics"></param>
        /// <param name="pRowIndex"></param>
        /// <param name="pColumnIndex"></param>
        /// <param name="pVectorRow"></param>
        /// <param name="pVectorColumn"></param>
        /// <param name="pUpCenter"></param>
        /// <param name="pWallHeight"></param>
        /// <param name="pSpeed"></param>
        private void DrawBox(Graphics pGraphics, int pRowIndex, int pColumnIndex, PointF pVectorRow, PointF pVectorColumn, PointF pUpCenter, float pWallHeight, PointF pSpeed)
        {
            PointF tPoint0 = new PointF(pUpCenter.X + pColumnIndex * pVectorRow.X + pRowIndex * pVectorColumn.X + pSpeed.X * this.m_Interval, pUpCenter.Y + pColumnIndex * pVectorColumn.Y + pRowIndex * pVectorColumn.Y + pSpeed.Y * this.m_Interval);
            PointF tPoint1 = new PointF(tPoint0.X, tPoint0.Y - pWallHeight);
            PointF tPoint2 = new PointF(tPoint1.X + pVectorRow.X, tPoint1.Y + pVectorRow.Y);
            PointF tPoint3 = new PointF(tPoint2.X + pVectorColumn.X, tPoint2.Y + pVectorColumn.Y);
            PointF tPoint4 = new PointF(tPoint1.X + pVectorColumn.X, tPoint1.Y + pVectorColumn.Y);
            PointF tPoint5 = new PointF(tPoint3.X, tPoint3.Y + pWallHeight);
            PointF tPoint6 = new PointF(tPoint4.X, tPoint4.Y + pWallHeight);
            PointF tPoint7 = new PointF(tPoint2.X, tPoint2.Y + pWallHeight);
            PointF tPoint8 = new PointF(tPoint1.X, tPoint1.Y - pWallHeight);
            PointF tPoint9 = new PointF(tPoint2.X, tPoint2.Y - pWallHeight);
            PointF tPoint10 = new PointF(tPoint3.X, tPoint3.Y - pWallHeight);
            PointF tPoint11 = new PointF(tPoint4.X, tPoint4.Y - pWallHeight);

            //GDI
            pGraphics.FillPolygon(Brushes.Orange, new PointF[] { tPoint8, tPoint9, tPoint10, tPoint11 });
            pGraphics.FillPolygon(Brushes.Orange, new PointF[] { tPoint10, tPoint11, tPoint6, tPoint5 });
            pGraphics.FillPolygon(Brushes.Orange, new PointF[] { tPoint9, tPoint10, tPoint5, tPoint7 });
            pGraphics.DrawPolygon(Pens.Gray, new PointF[] { tPoint8, tPoint9, tPoint10, tPoint11 });
            pGraphics.DrawPolygon(Pens.Gray, new PointF[] { tPoint10, tPoint11, tPoint6, tPoint5 });
            pGraphics.DrawPolygon(Pens.Gray, new PointF[] { tPoint9, tPoint10, tPoint5, tPoint7 });

            //贴图
            //tGraphics.DrawImage(Properties.Resources.boxWood, new PointF[] { tPoint8, tPoint9, tPoint11 });
            //tGraphics.DrawImage(Properties.Resources.boxWood, new PointF[] { tPoint10, tPoint9, tPoint5 });
            //tGraphics.DrawImage(Properties.Resources.boxWood, new PointF[] { tPoint11, tPoint10, tPoint6 });
        }
       
        /// <summary>
        /// 刷新地图
        /// </summary>
        public void Refresh()
        {
            int tWidth = this.m_PictureBox.Width;
            int tHeight = this.m_PictureBox.Height;

            int tRowCount = this.m_Map.GetLength(0);
            int tColumnCount = this.m_Map.GetLength(1);

            Bitmap tBackgroundImage = new Bitmap(tWidth, tHeight);
            Graphics tGraphics = Graphics.FromImage(tBackgroundImage);
            tGraphics.Clear(Color.SkyBlue);

            PointF tPointUpCenter = new PointF(tWidth / 2, 0);
            PointF tPointBottomCenter = new PointF(tWidth / 2, tHeight);
            PointF tPointLeftCenter = new PointF(0, tHeight / 2);
            PointF tPointRightCenter = new PointF(tWidth, tHeight / 2);

            //增量
            PointF tVectorRow = new PointF((tPointRightCenter.X - tPointUpCenter.X) / tColumnCount, (tPointRightCenter.Y - tPointUpCenter.Y) / tColumnCount);
            PointF tVectorColumn = new PointF((tPointLeftCenter.X - tPointUpCenter.X) / tRowCount, (tPointLeftCenter.Y - tPointUpCenter.Y) / tRowCount);

            int tWallHeight = tHeight / 20;
            int tBoxHeight = tWallHeight * 2;

            int tManRowIndex = this.m_ManRowIndex, tManColumnIndex = this.m_ManColumnIndex;

            //绘制墙壁
            for (int i = 0; i < tRowCount; i++)
            {
                for (int j = 0; j < tColumnCount; j++)
                {
                    this.Draw(tGraphics, i, j, tVectorRow, tVectorColumn, tPointUpCenter, tWallHeight);

                    if (i == this.m_ManRowIndex && j == this.m_ManColumnIndex)
                    {
                        this.DrawMan(tGraphics, i, j, tVectorRow, tVectorColumn, tPointUpCenter, tWallHeight, new PointF(0, 0));
                    }
                    else
                    {
                        if (this.m_BoxList.Count(t => t.Value[0] == i && t.Value[1] == j) == 1)
                        {
                            this.DrawBox(tGraphics, i, j, tVectorRow, tVectorColumn, tPointUpCenter, tWallHeight, new PointF(0, 0));
                        }
                    }
                }
            }

            this.m_PictureBox.Image = tBackgroundImage;
        }
    }
}
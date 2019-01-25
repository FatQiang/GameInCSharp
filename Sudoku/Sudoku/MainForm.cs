using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// 用于存储数独的数组
        /// </summary>
        private int[,] m_Sudoku = new int[9, 9];

        public MainForm()
        {
            this.InitializeComponent();
            this.Reload();
        }

        /// <summary>
        /// 只允许输入数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TNewTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar)&&e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void btnCompute_Click(object sender, EventArgs e)
        {
            DateTime tStartTime = DateTime.Now;

            #region --数组赋值--

            //数组赋值
            foreach (Control tControl in this.panelMain.Controls)
            {
                if (tControl is TextBox)
                {
                    TextBox tTextBox = tControl as TextBox;
                    int tIndex = int.Parse(tTextBox.Tag.ToString());
                    int tRowIndex = tIndex / 9;
                    int tColIndex = tIndex % 9;
                    if (string.IsNullOrEmpty(tTextBox.Text))
                    {
                        this.m_Sudoku[tRowIndex, tColIndex] = 0;
                    }
                    else
                    {
                        int tValue = int.Parse(tTextBox.Text);
                        this.m_Sudoku[tRowIndex, tColIndex] = tValue;
                    }
                }
            }

            #endregion --数组赋值--

            #region --判断是否有输入错误--

            //用于初始数字计数，至少17个才能完成数独且有唯一解
            int tCount = 0;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (this.m_Sudoku[i, j] == 0)
                    {
                        continue;
                    }

                    tCount++;

                    int tValue = this.m_Sudoku[i, j];
                    int m, n;

                    for (m = i + 1; m < 9; m++)
                    {
                        //判断这一列是否有重复
                        if (this.m_Sudoku[m, j] == tValue)
                        {
                            MessageBox.Show(string.Format("你傻逼啊！在第{0}列存在重复数字{1}，这列有重复数字我解你大爷啊！", j + 1, tValue));
                            return;
                        }
                    }
                    for (n = j + 1; n < 9; n++)
                    {
                        //判断这一行是否有重复
                        if (this.m_Sudoku[i, n] == tValue)
                        {
                            MessageBox.Show(string.Format("你傻逼啊！在第{0}行存在重复数字{1}，这行有重复数字我解个屁啊", i + 1, tValue));
                            return;
                        }
                    }

                    //判断宫内是否有重复数字
                    //判断是在哪个九宫格
                    int tNineFRow = i / 3 * 3;
                    int tNineFCol = j / 3 * 3;

                    for (int k = tNineFRow; k < tNineFRow + 3; k++)
                    {
                        for (int l = tNineFCol; l < tNineFCol + 3; l++)
                        {
                            if (k == i && l == j)
                            {
                                continue;
                            }
                            if (this.m_Sudoku[k, l] == tValue)
                            {
                                MessageBox.Show(string.Format("你是不是傻啊！在第{0}个九宫格存在重复数字{1}，有重复数字我解条毛啊！", i / 3 * 3 + tNineFCol / 3 + 1, tValue));
                                return;
                            }
                        }
                    }
                }
            }

            if (tCount < 17)
            {
                MessageBox.Show(string.Format("你是不是傻啊！初始数字要至少17个才有唯一解啊，现在只有{0}个数字我解你妹啊！", tCount));
                return;
            }

            #endregion --判断是否有输入错误--

            //开始求解
            int[,] tAnswer = new int[9, 9];
            if (this.Calculate(this.m_Sudoku, out tAnswer))
            {
                //赋值到textbox上
                foreach (Control tControl in this.panelMain.Controls)
                {
                    if (tControl is TextBox)
                    {
                        TextBox tTextBox = tControl as TextBox;
                        int tIndex = int.Parse(tTextBox.Tag.ToString());
                        int tRowIndex = tIndex / 9;
                        int tColIndex = tIndex % 9;
                        tTextBox.Text = tAnswer[tRowIndex, tColIndex].ToString();
                    }
                }

                TimeSpan tTimeSpan = DateTime.Now - tStartTime  ;
                double tTotalSec = tTimeSpan.TotalSeconds;
                string tMsg = "";
                if (tTotalSec < 1)
                {
                    tMsg = "这题太简单了，来个难的！";
                }
                else if (tTotalSec < 2)
                {
                    tMsg = "这题还行，我可以做更难的！";
                }
                else
                {
                    tMsg = "这题不错哦，不过难不倒我！";
                }
                MessageBox.Show(tMsg);
            }
            else
            {
                MessageBox.Show("兄弟，你这个数独可能无解哦！");
            }
        }

        private bool Calculate(int[,] pSudoku, out int[,] pAnswer)
        {
            //搜索第一个空白格子
            int tBRIdx = -1, tBCIdx = -1;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (pSudoku[i, j] == 0)
                    {
                        tBRIdx = i;
                        tBCIdx = j;
                        i = 9;
                        j = 9;
                        break;
                    }
                }
            }

            //-1，表示找到答案了
            if (tBRIdx == -1 || tBCIdx == -1)
            {
                pAnswer = pSudoku;
                return true;
            }

            #region --查找可能的答案--

            //找到第一个空白格子的可能值
            //所在列查找
            int[] tValues = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            for (int k = 0; k < 9; k++)
            {
                if (k == tBRIdx) continue;
                int tValue = pSudoku[k, tBCIdx];
                if (tValue != 0)
                {
                    if (tValues.Contains(tValue))
                    {
                        tValues = tValues.Where(t => t != tValue).ToArray();
                    }
                }
            }

            //所在行查找
            for (int k = 0; k < 9; k++)
            {
                if (k == tBCIdx) continue;
                int tValue = pSudoku[tBRIdx, k];
                if (tValue != 0)
                {
                    if (tValues.Contains(tValue))
                    {
                        tValues = tValues.Where(t => t != tValue).ToArray();
                    }
                }
            }

            //所在宫查找
            int tNineFRow = tBRIdx / 3 * 3;
            int tNineFCol = tBCIdx / 3 * 3;

            for (int k = tNineFRow; k < tNineFRow + 3; k++)
            {
                for (int l = tNineFCol; l < tNineFCol + 3; l++)
                {
                    if (k == tBRIdx && l == tBCIdx)
                    {
                        continue;
                    }
                    int tValue = pSudoku[k, l];
                    if (tValue != 0)
                    {
                        tValues = tValues.Where(t => t != tValue).ToArray();
                    }
                }
            }

            #endregion --查找可能的答案--

            if (tValues.Length == 0)
            {
                pAnswer = null;
                return false;
            }

            for (int q = 0; q < tValues.Length; q++)
            {
                //填入可能值
                int[,] tNextMatrix = new int[9, 9];
                for (int m = 0; m < 9; m++)
                {
                    for (int n = 0; n < 9; n++)
                    {
                        tNextMatrix[m, n] = pSudoku[m, n];
                    }
                }

                tNextMatrix[tBRIdx, tBCIdx] = tValues[q];

                pAnswer = tNextMatrix;

                if (this.Calculate(tNextMatrix, out pAnswer))
                {
                    return true;
                }
            }

            pAnswer = null;            
            return false;
        }

        private bool Check(int[,] pSudoku)
        {
            #region --判断是否有输入错误--

            //用于初始数字计数，至少17个才能完成数独且有唯一解
            int tCount = 0;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (pSudoku[i, j] == 0)
                    {
                        continue;
                    }

                    int tValue = pSudoku[i, j];
                    int m, n;

                    for (m = i + 1; m < 9; m++)
                    {
                        //判断这一列是否有重复
                        if (pSudoku[m, j] == tValue)
                        {
                            return false;
                        }
                    }
                    for (n = j + 1; n < 9; n++)
                    {
                        //判断这一行是否有重复
                        if (pSudoku[i, n] == tValue)
                        {
                            return false;
                        }
                    }

                    //判断宫内是否有重复数字
                    //判断是在哪个九宫格
                    int tNineFRow = i / 3 * 3;
                    int tNineFCol = j / 3 * 3;

                    for (int k = tNineFRow; k < tNineFRow + 3; k++)
                    {
                        for (int l = tNineFRow; l < tNineFCol + 3; l++)
                        {
                            if (k == i && l == j)
                            {
                                continue;
                            }
                            if (pSudoku[k, l] == tValue)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            if (tCount < 17)
            {
                return false;
            }

            return true;

            #endregion --判断是否有输入错误--
        }

        private void Reload()
        {
            this.panelMain.Controls.Clear();
            this.m_Sudoku = new int[9, 9];
            //初始化数独输入框
            for (int i = 0; i < 9; i++)
            {
                int tStartX = 0;
                int tStartY = i * 30;
                if (i >= 3 && i < 6)
                {
                    tStartY += 10;
                }
                else if (i >= 6)
                {
                    tStartY += 20;
                }
                for (int j = 0; j < 9; j++)
                {
                    TextBox tNewTextBox = new TextBox();
                    tNewTextBox.Location = new Point(tStartX, tStartY);
                    tNewTextBox.MaxLength = 1;
                    tNewTextBox.KeyPress += this.TNewTextBox_KeyPress;
                    tNewTextBox.Multiline = true;
                    tNewTextBox.Size = new Size(30, 30);
                    tNewTextBox.TextAlign = HorizontalAlignment.Center;
                    tNewTextBox.Tag = i * 9 + j;
                    tNewTextBox.Name = string.Format("textBoxN{0}{1}", i, j);
                    this.panelMain.Controls.Add(tNewTextBox);
                    if (j == 2 || j == 5)
                    {
                        tStartX += 40;
                    }
                    else
                    {
                        tStartX += 30;
                    }
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.Reload();
        }
    }
}
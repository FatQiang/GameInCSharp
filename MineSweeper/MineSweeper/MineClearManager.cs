using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    public class MineClearManager
    {
        public int[,] m_Mine;

        public int[,] m_Clear;

        public int[,] m_SurroundMineCount;

        public int m_MineCount = 0;

        public int RowCount
        {
            get
            {
                return this.m_Mine.GetLength(0);
            }
        }

        public int ColumnCount
        {
            get
            {
                return this.m_Mine.GetLength(1);
            }
        }

        public bool m_Continue = true;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pRowCount">行数</param>
        /// <param name="pColumnCount">列数</param>
        /// <param name="pMineCount">雷总数</param>
        public MineClearManager(int pRowCount, int pColumnCount, int pMineCount)
        {
            this.m_Mine = new int[pRowCount, pColumnCount];
            this.m_Clear = new int[pRowCount, pColumnCount];
            this.m_SurroundMineCount = new int[pRowCount, pColumnCount];
            this.m_MineCount = pMineCount;
            for (int i = 0; i < pRowCount; i++)
            {
                for (int j = 0; j < pColumnCount; j++)
                {
                    this.m_Mine[i, j] = 0;
                    this.m_Clear[i, j] = 0;
                }
            }
            //随机生成雷
            int tFlag = pMineCount;
            Random tRandom = new Random();
            while (tFlag != 0)
            {
                int tRowIndex = tRandom.Next(0, pRowCount);
                int tColumnIndex = tRandom.Next(0, pColumnCount);
                if (this.m_Mine[tRowIndex, tColumnIndex] == 0)
                {
                    this.m_Mine[tRowIndex, tColumnIndex] = 1;
                    tFlag--;
                }
            }

            for (int i = 0; i < pRowCount; i++)
            {
                for (int j = 0; j < pColumnCount; j++)
                {
                    this.m_SurroundMineCount[i, j] = this.GetMineCount(i, j);
                }
            }
        }

        private void Search(int pRowIndex, int pColumnIndex)
        {
            if (pRowIndex < 0 || pColumnIndex < 0 || pRowIndex >= this.m_Mine.GetLength(0) || pColumnIndex >= this.m_Mine.GetLength(1))
            {
                return;
            }
            if (this.m_Mine[pRowIndex, pColumnIndex] == 1)
            {
                return;
            }
            else
            {
                if (this.m_Clear[pRowIndex, pColumnIndex] == 1)
                {
                    return;
                }
                if (this.m_SurroundMineCount[pRowIndex, pColumnIndex] == 0)
                {
                    //将四周不是雷的区域，但是该区域周围有雷的翻开
                    this.ClearSurround(pRowIndex, pColumnIndex);
                    this.m_Clear[pRowIndex, pColumnIndex] = 1;
                    //向四个方向扩展
                    this.Search(pRowIndex - 1, pColumnIndex);
                    this.Search(pRowIndex + 1, pColumnIndex);
                    this.Search(pRowIndex, pColumnIndex - 1);
                    this.Search(pRowIndex, pColumnIndex + 1);
                }
            }
        }

        private void ClearSurround(int pRowIndex, int pColumnIndex)
        {    
            //将四周不是雷的区域，但是该区域周围有雷的翻开
            for (int i = pRowIndex - 1; i <= pRowIndex + 1; i++)
            {
                if (i < 0 || i >= this.m_Mine.GetLength(0))
                {
                    continue;
                }
                for (int j = pColumnIndex - 1; j <= pColumnIndex + 1; j++)
                {
                    if (j < 0 || j >= this.m_Mine.GetLength(1))
                    {
                        continue;
                    }
                    if (this.m_SurroundMineCount[i, j] > 0 && this.m_Mine[i, j] == 0)
                    {
                        this.m_Clear[i, j] = 1;
                    }
                }
            }
        }

        public bool ClearMine(int pRowIndex, int pColumnIndex)
        {
            if (this.m_Continue)
            {
                if (this.m_Clear[pRowIndex, pColumnIndex] == 1)
                {
                    return true;
                }
                this.m_Clear[pRowIndex, pColumnIndex] = 1;
                if (this.m_Mine[pRowIndex, pColumnIndex] == 1)
                {
                    this.m_Continue = false;
                    return false;
                }
                if (this.m_SurroundMineCount[pRowIndex, pColumnIndex] == 0)
                {
                    this.ClearSurround(pRowIndex, pColumnIndex);
                }
                //清除相连的不是雷的块
                this.Search(pRowIndex - 1, pColumnIndex);
                this.Search(pRowIndex + 1, pColumnIndex);
                this.Search(pRowIndex, pColumnIndex - 1);
                this.Search(pRowIndex, pColumnIndex + 1);
            }
            return true;
        }

        public void FlagMine(int pRowIndex, int pColumnIndex)
        {
            switch (this.m_Clear[pRowIndex, pColumnIndex])
            {
                case 0:
                    this.m_Clear[pRowIndex, pColumnIndex] = -1; break;
                case 1:
                    this.m_Clear[pRowIndex, pColumnIndex] = 1; break;
                case -1:
                    this.m_Clear[pRowIndex, pColumnIndex] = 0; break;
            }
        }

        public bool GetStatus()
        {
            for (int i = 0; i < this.m_Mine.GetLength(0); i++)
            {
                for (int j = 0; j < this.m_Mine.GetLength(1); j++)
                {
                    if (this.m_Clear[i, j] == 0 && this.m_Mine[i, j] == 0)
                    {
                        return false;
                    }
                }
            }
            this.m_Continue = false;
            return true;
        }

        public int GetMineCount(int pRowIndex, int pColumnIndex)
        {
            int tMineCount = 0;
            for (int i = pRowIndex - 1; i <= pRowIndex + 1; i++)
            {
                for (int j = pColumnIndex - 1; j <= pColumnIndex + 1; j++)
                {
                    if (i < 0 || j < 0 || i >= this.m_Mine.GetLength(0) || j >= this.m_Mine.GetLength(1))
                    {
                        continue;
                    }
                    else
                    {
                        tMineCount += this.m_Mine[i, j];
                    }
                }
            }
            return tMineCount;
        }
    }
}
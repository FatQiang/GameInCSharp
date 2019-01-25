using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tetris
{
    public class GameManager
    {
        private int m_StartX = 260;
        private int m_ScreenWidth = 500;
        private int m_ScreenHeight = 700;
        private Timer m_TimerUpdateData;
        private Timer m_TimerUpdateCanvas;
        private IBlock m_MovingBlock;
        private IBlock m_NextBlock;
        private BlockUnit[,] m_BackgroundBlock;
        private Graphics m_Graphics;
        private Graphics m_NextGraphics;
        private Graphics m_GraphicsBackup;
        private Bitmap m_GraphicsBitmapBackup;
        private int m_Speed = 300;
        private Random m_Random;
        private int m_Score = 0;
        private Boolean m_Lost = false;
        public delegate void ScoreChange();
        public event ScoreChange AfterScoreChanged;

        /// <summary>
        /// 获取目前得分
        /// </summary>
        public int Score
        {
            get
            {
                return this.m_Score;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="pGraphics"></param>
        /// <param name="pNextGraphics"></param>
        /// <param name="pWidth"></param>
        /// <param name="pHeight"></param>
        public GameManager(Graphics pGraphics, Graphics pNextGraphics, int pWidth = 500, int pHeight = 300)
        {
            this.m_ScreenWidth = pWidth;
            this.m_ScreenHeight = pHeight;
            this.m_Graphics = pGraphics;
            this.m_NextGraphics = pNextGraphics;
            this.ReLoad();
        }

        /// <summary>
        /// 重新加载游戏
        /// </summary>
        public void ReLoad()
        {
            this.m_Graphics.Clear(Color.White);
            this.m_GraphicsBitmapBackup = new Bitmap(this.m_ScreenWidth, this.m_ScreenHeight);
            this.m_GraphicsBackup = Graphics.FromImage(this.m_GraphicsBitmapBackup);

            this.m_NextBlock = null;

            this.m_Lost = false;
            this.m_Score = 0;

            //初始化背景方块
            this.m_BackgroundBlock = new BlockUnit[this.m_ScreenHeight / BlockBackground.Size, this.m_ScreenWidth / BlockBackground.Size];
            for (int i = 0; i < this.m_BackgroundBlock.GetLength(0); i++)
            {
                for (int j = 0; j < this.m_BackgroundBlock.GetLength(1); j++)
                {
                    this.m_BackgroundBlock[i, j] = new BlockUnit(j * BlockBackground.Size, i * BlockBackground.Size, BlockBackground.BlockBackgroundType.None);
                    this.m_BackgroundBlock[i, j].IsNull = true;
                }
            }

            this.m_TimerUpdateData = new Timer();
            this.m_TimerUpdateData.Interval = this.m_Speed;
            this.m_TimerUpdateData.Tick += this.TimerUpdateData;

            this.m_TimerUpdateCanvas = new Timer();
            this.m_TimerUpdateCanvas.Interval = 100;
            this.m_TimerUpdateCanvas.Tick += this.TimerUpdateCanvas_Tick;

            this.m_Random = new Random();
            this.InitRandomBlock();
        }

        /// <summary>
        /// 用于绘图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerUpdateCanvas_Tick(object sender, EventArgs e)
        {
            if (!this.m_Lost)
            {
                #region --绘制画面--

                this.m_GraphicsBackup.Clear(Color.White);
                //绘制
                for (int i = 0; i < this.m_BackgroundBlock.GetLength(0); i++)
                {
                    for (int j = 0; j < this.m_BackgroundBlock.GetLength(1); j++)
                    {
                        this.m_BackgroundBlock[i, j].Draw(this.m_GraphicsBackup);
                    }
                }

                this.m_MovingBlock.Draw(this.m_GraphicsBackup);

                this.m_Graphics.DrawImage(this.m_GraphicsBitmapBackup, 0, 0);

                #endregion --绘制画面--
            }
            else
            {
                //绘制失败画面
                int tX,tY;
                tX=(this.m_ScreenWidth-Properties.Resources.lost.Width)/2;
                tY = (this.m_ScreenHeight - Properties.Resources.lost.Height) / 2;
                this.m_Graphics.DrawImage(Properties.Resources.lost,tX,tY);

                //停止
                this.Stop();
            }
        }

        /// <summary>
        /// 用于更新数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerUpdateData(object sender, EventArgs e)
        {
            if (!this.m_Lost)
            {
                #region --满行处理--

                int tFullRowCount = 0;//记录有多少满行

                for (int i = this.m_BackgroundBlock.GetLength(0) - 1; i >= 0; i--)
                {
                    Boolean tIsFull = true;
                    //判断是否有满行
                    for (int j = 0; j < this.m_BackgroundBlock.GetLength(1); j++)
                    {
                        if (this.m_BackgroundBlock[i, j].IsNull)
                        {
                            tIsFull = false;
                            break;
                        }
                    }
                    if (tIsFull)//满行往下移动一行
                    {
                        tFullRowCount++;
                        for (int k = i; k > 0; k--)
                        {
                            Boolean tIsEmpty = true;
                            for (int z = 0; z < this.m_BackgroundBlock.GetLength(1); z++)
                            {
                                if (tIsEmpty || !this.m_BackgroundBlock[k, z].IsNull)
                                {
                                    tIsEmpty = false;
                                }
                                this.m_BackgroundBlock[k, z].IsNull = this.m_BackgroundBlock[k - 1, z].IsNull;
                                this.m_BackgroundBlock[k, z].BlockBackgroundType = this.m_BackgroundBlock[k - 1, z].BlockBackgroundType;
                            }
                            if (tIsEmpty)
                            {
                                break;
                            }
                        }
                        //处理第一行
                        for (int z = 0; z < this.m_BackgroundBlock.GetLength(1); z++)
                        {
                            this.m_BackgroundBlock[0, z].IsNull = true;
                            this.m_BackgroundBlock[0, z].BlockBackgroundType = BlockBackground.BlockBackgroundType.None;
                        }
                        i++;
                    }
                }

                //计分 按照等级 
                //1行 每行加10分
                //2行 每行加15分
                //3行 每行加30分
                //4行 每行加40分

                switch (tFullRowCount)
                {
                    case 1: this.m_Score += 10; break;
                    case 2: this.m_Score += 30; break;
                    case 3: this.m_Score += 50; break;
                    case 4: this.m_Score += 120; break;
                    default: break;
                }

                this.AfterScoreChanged();

                #endregion --满行处理--

                #region --移动当前方块--

                //判断是否还能移动方块
                if (JudgeMove(MoveDirection.Down))
                {
                    //向下移动当前方块
                    this.m_MovingBlock.Move(MoveDirection.Down);
                }
                else
                {
                    //若不能移动方块则重新生成新方块
                    BlockUnit[] tBlockSet = this.m_MovingBlock.GetBlockSet();

                    //当不能移动方块，而且该方块部分在界外时，游戏失败
                    for (int i = 0; i < tBlockSet.Length; i++)
                    {
                        //计算索引号
                        int tR, tC;
                        tR = tBlockSet[i].Y / BlockBackground.Size;
                        tC = tBlockSet[i].X / BlockBackground.Size;
                        if (tR >= 0 && tC >= 0)
                        {
                            this.m_BackgroundBlock[tR, tC].BlockBackgroundType = tBlockSet[i].BlockBackgroundType;
                            this.m_BackgroundBlock[tR, tC].IsNull = tBlockSet[i].IsNull;
                        }
                        else if (tR < 0)//游戏失败
                        {
                            this.m_Lost = true;
                            break;
                        }
                    }
                    this.InitRandomBlock();
                }

                #endregion --移动当前方块--
            }
        }

        /// <summary>
        /// 判断是否能按照指定方向移动
        /// </summary>
        /// <param name="pMoveDirection"></param>
        /// <returns></returns>
        public Boolean JudgeMove(MoveDirection pMoveDirection)
        {
            //判断下方是否超过边界或者存在已有的方块
            BlockUnit[] tBlockSet = this.m_MovingBlock.GetBlockSet();
            int tMaxRowIndex = this.m_ScreenHeight / BlockBackground.Size - 1;
            int tMaxColumnIndex = this.m_ScreenWidth / BlockBackground.Size - 1;
            for (int i = 0; i < tBlockSet.Length; i++)
            {
                int tRowIndex, tColumnIndex;
                tRowIndex = tBlockSet[i].Y / BlockBackground.Size;
                tColumnIndex = tBlockSet[i].X / BlockBackground.Size;

                switch (pMoveDirection)
                {
                    case MoveDirection.Down:
                        {
                            //判断是否超出界限
                            if (tRowIndex + 1 > tMaxRowIndex)
                            {
                                return false;
                            }
                            //判断下方是否已存在方块
                            if (tRowIndex + 1 >= 0 && !this.m_BackgroundBlock[tRowIndex + 1, tColumnIndex].IsNull)
                            {
                                return false;
                            }
                            break;
                        }
                    case MoveDirection.Left:
                        {
                            //判断是否超出界限
                            if (tColumnIndex - 1 < 0)
                            {
                                return false;
                            }
                            //判断左边是否已存在方块
                            if (tRowIndex >= 0 && !this.m_BackgroundBlock[tRowIndex, tColumnIndex - 1].IsNull)
                            {
                                return false;
                            }
                            break;
                        }
                    case MoveDirection.Right:
                        {
                            //判断是否超出界限
                            if (tColumnIndex + 1 > tMaxColumnIndex)
                            {
                                return false;
                            }
                            //判断左边是否已存在方块
                            if (tRowIndex >= 0 && !this.m_BackgroundBlock[tRowIndex, tColumnIndex + 1].IsNull)
                            {
                                return false;
                            }
                            break;
                        }
                    default: break;
                }

            }
            return true;
        }

        /// <summary>
        /// 判断是否能旋转
        /// </summary>
        /// <returns></returns>
        public Boolean JudgeRotate()
        {
            //判断下方是否超过边界或者存在已有的方块
            BlockUnit[] tBlockSet = this.m_MovingBlock.AfterRotateBlockUnits();
            int tMaxRowIndex = this.m_ScreenHeight / BlockBackground.Size - 1;
            int tMaxColumnIndex = this.m_ScreenWidth / BlockBackground.Size - 1;
            for (int i = 0; i < tBlockSet.Length; i++)
            {
                int tRowIndex, tColumnIndex;
                tRowIndex = tBlockSet[i].Y / BlockBackground.Size;
                tColumnIndex = tBlockSet[i].X / BlockBackground.Size;

                if (tRowIndex > tMaxRowIndex) return false;
                if (tColumnIndex < 0) return false;
                if (tColumnIndex > tMaxColumnIndex) return false;
                if (tRowIndex >= 0 && !this.m_BackgroundBlock[tRowIndex, tColumnIndex].IsNull) return false;
            }
            return true;
        }

        /// <summary>
        /// 随机生成新方块
        /// </summary>
        public void InitRandomBlock()
        {
            BlockType tBlockType;

            if (this.m_NextBlock != null)
            {
                tBlockType = this.m_NextBlock.GetBlockType();
            }
            else
            {
                tBlockType = (BlockType)this.m_Random.Next(0, 6);
            }

            switch (tBlockType)
            {
                case BlockType.LineBlock: this.m_MovingBlock = new LineBlock(this.m_StartX, -5 * BlockBackground.Size); break;
                case BlockType.LBlock: this.m_MovingBlock = new LBlock(this.m_StartX, -4 * BlockBackground.Size); break;
                case BlockType.LReserveBlock: this.m_MovingBlock = new LReserveBlock(this.m_StartX, -4 * BlockBackground.Size); break;
                case BlockType.TBlock: this.m_MovingBlock = new TBlock(this.m_StartX, -3 * BlockBackground.Size); break;
                case BlockType.OBlock: this.m_MovingBlock = new OBlock(this.m_StartX, -3 * BlockBackground.Size); break;
                case BlockType.ZBlock: this.m_MovingBlock = new ZBlock(this.m_StartX, -3 * BlockBackground.Size); break;
                case BlockType.ZReserveBlock: this.m_MovingBlock = new ZReserveBlock(this.m_StartX, -3 * BlockBackground.Size); break;
                default: break;
            }

            if (this.m_NextBlock != null)
            {
                this.m_MovingBlock.SetBackground(this.m_NextBlock.GetBlockSet()[0].BlockBackgroundType);
            }

            tBlockType = (BlockType)this.m_Random.Next(0, 6);
            this.m_NextBlock = new BaseBlock(0, 0);
            switch (tBlockType)
            {
                case BlockType.LineBlock: this.m_NextBlock = new LineBlock(30, 0); break;
                case BlockType.LBlock: this.m_NextBlock = new LBlock(20, 10); break;
                case BlockType.LReserveBlock: this.m_NextBlock = new LReserveBlock(40, 10); break;
                case BlockType.TBlock: this.m_NextBlock = new TBlock(10, 20); break;
                case BlockType.OBlock: this.m_NextBlock = new OBlock(20, 20); break;
                case BlockType.ZBlock: this.m_NextBlock = new ZBlock(10, 20); break;
                case BlockType.ZReserveBlock: this.m_NextBlock = new ZReserveBlock(10, 20); break;
                default: break;
            }
            // 在下个方块的canvas中绘制
            this.m_NextGraphics.Clear(Color.White);
            this.m_NextBlock.Draw(this.m_NextGraphics);
            this.m_NextGraphics.Clear(Color.White);
            this.m_NextBlock.Draw(this.m_NextGraphics);
        }

        /// <summary>
        /// 开始
        /// </summary>
        public void Start()
        {
            this.m_TimerUpdateData.Start();
            this.m_TimerUpdateCanvas.Start();
        }

        /// <summary>
        /// 暂停
        /// </summary>
        public void Stop()
        {
            this.m_TimerUpdateData.Stop();
            this.m_TimerUpdateCanvas.Stop();
        }

        /// <summary>
        /// 接收按键输入
        /// </summary>
        /// <param name="pKey"></param>
        public void KeyUp(Keys pKey)
        {
            switch (pKey)
            {
                case Keys.Left: if (JudgeMove(MoveDirection.Left)) this.m_MovingBlock.Move(MoveDirection.Left); break;
                case Keys.Right: if (JudgeMove(MoveDirection.Right)) this.m_MovingBlock.Move(MoveDirection.Right); break;
                case Keys.Down: if (JudgeMove(MoveDirection.Down)) this.m_MovingBlock.Move(MoveDirection.Down); break;
                case Keys.Up: if (this.JudgeRotate()) this.m_MovingBlock.Rotate(); break;
                default: break;
            }
        }
    }
}
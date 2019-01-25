using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Tetris
{
    /// <summary>
    /// 移动的方向
    /// </summary>
    public enum MoveDirection
    {
        /// <summary>
        /// 向左
        /// </summary>
        Left = 0,
        /// <summary>
        /// 向右
        /// </summary>
        Right = 1,
        /// <summary>
        /// 向下
        /// </summary>
        Down = 2
    }

    public class BlockBackground
    {
        public static int Size = 20;

        private static Random m_Random;

        /// <summary>
        /// The color of blockunit
        /// </summary>
        public enum BlockBackgroundType
        {
            LightBlue = 0,
            LightGreen = 1,
            DeepBlue = 2,
            Pink = 3,
            Orange = 4,
            Red = 5,
            Purple = 6,
            Yellow = 7,
            Grey = 8,
            None = 9
        }

        public static Bitmap GetBackground(BlockBackgroundType pBlockBackgroundType)
        {
            switch (pBlockBackgroundType)
            {
                case BlockBackgroundType.LightBlue: return Properties.Resources._1;
                case BlockBackgroundType.LightGreen: return Properties.Resources._2;
                case BlockBackgroundType.DeepBlue: return Properties.Resources._3;
                case BlockBackgroundType.Pink: return Properties.Resources._4;
                case BlockBackgroundType.Orange: return Properties.Resources._5;
                case BlockBackgroundType.Red: return Properties.Resources._6;
                case BlockBackgroundType.Purple: return Properties.Resources._7;
                case BlockBackgroundType.Yellow: return Properties.Resources._8;
                case BlockBackgroundType.Grey: return Properties.Resources._9;
                default: return null;
            }
        }

        public static BlockBackgroundType GetRandomBackground()
        {
            if (BlockBackground.m_Random == null)
            {
                BlockBackground.m_Random = new Random();
            }
            return (BlockBackgroundType)BlockBackground.m_Random.Next(0, 8);
        }
    }

    /// <summary>
    /// Seven types of block
    /// </summary>
    public enum BlockType
    {
        LineBlock = 0,
        LBlock = 1,
        LReserveBlock = 2,
        TBlock = 3,
        OBlock = 4,
        ZBlock = 5,
        ZReserveBlock = 6,
        None = 7
    }

    /// <summary>
    /// Smallest unit of block
    /// </summary>
    public class BlockUnit
    {
        /// <summary>
        /// 方块左上角坐标X
        /// </summary>
        public int X;
        /// <summary>
        /// 方块左上角坐标Y
        /// </summary>
        public int Y;
        /// <summary>
        /// 标志该方块是否为空
        /// </summary>
        public Boolean IsNull = false;
        /// <summary>
        /// 方块的背景
        /// </summary>
        public BlockBackground.BlockBackgroundType BlockBackgroundType;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="pX">左上角x</param>
        /// <param name="pY">左上角y</param>
        /// <param name="pBlockBackgroundType"></param>
        public BlockUnit(int pX, int pY, BlockBackground.BlockBackgroundType pBlockBackgroundType)
        {
            this.X = pX;
            this.Y = pY;
            this.BlockBackgroundType = pBlockBackgroundType;
        }
        /// <summary>
        /// 方块向指定方向移动一个单位
        /// </summary>
        /// <param name="pDirection"></param>
        public void Move(MoveDirection pDirection)
        {
            switch (pDirection)
            {
                case MoveDirection.Down: Y += BlockBackground.Size; break;
                case MoveDirection.Left: X -= BlockBackground.Size; break;
                case MoveDirection.Right: X += BlockBackground.Size; break;
                default: break;
            }
        }
        /// <summary>
        /// 在画板上绘制方块
        /// </summary>
        /// <param name="pGraphics"></param>
        public void Draw(Graphics pGraphics)
        {
            if (!this.IsNull)
            {
                pGraphics.DrawImage(BlockBackground.GetBackground(this.BlockBackgroundType) as Image, X, Y, BlockBackground.Size, BlockBackground.Size);
            }
        }
    }

    /// <summary>
    /// The interface of each kinds of block,provide the method of move and draw
    /// </summary>
    public interface IBlock
    {
        void Move(MoveDirection pMoveDirection);
        void Draw(Graphics pGraphics);
        void Rotate();
        void SetBackground(BlockBackground.BlockBackgroundType pBlockBackgroundType);
        BlockType GetBlockType();
        BlockUnit[] GetBlockSet();
        BlockUnit[] AfterRotateBlockUnits();
    }

    /// <summary>
    /// Base type of block
    /// </summary>
    public class BaseBlock : IBlock
    {
        public int X;
        public int Y;
        public int RotationState = 1;

        /// <summary>
        /// The set of blockunits
        /// </summary>
        public BlockUnit[] BlockSet;

        public BaseBlock(int pX, int pY)
        {
            this.X = pX;
            this.Y = pY;
        }

        /// <summary>
        /// Move the block
        /// </summary>
        /// <param name="pMoveDirection">Direction</param>
        public void Move(MoveDirection pMoveDirection)
        {
            for (int i = 0; i < this.BlockSet.Length; i++)
            {
                this.BlockSet[i].Move(pMoveDirection);
            }
        }

        /// <summary>
        /// Draw the block on canvas
        /// </summary>
        public void Draw(Graphics pGraphics)
        {
            for (int i = 0; i < this.BlockSet.Length; i++)
            {
                this.BlockSet[i].Draw(pGraphics);
            }
        }

        /// <summary>
        /// Ronate the block
        /// </summary>
        public virtual void Rotate()
        { }

        public virtual BlockUnit[] AfterRotateBlockUnits()
        {
            return null;
        }

        public BlockUnit[] GetBlockSet()
        {
            return this.BlockSet;
        }

        public void SetBackground(BlockBackground.BlockBackgroundType pBlockBackgroundType)
        {
            for (int i = 0; i < this.BlockSet.Length; i++)
            {
                this.BlockSet[i].BlockBackgroundType = pBlockBackgroundType;
            }
        }

        public virtual BlockType GetBlockType()
        {
            return BlockType.None;
        }
    }

    /// <summary>
    /// 口  
    /// 口    口口口口
    /// 口
    /// 口
    /// </summary>
    public class LineBlock : BaseBlock
    {
        public LineBlock(int pX, int pY)
            : base(pX, pY)
        {
            base.BlockSet = new BlockUnit[4];
            BlockBackground.BlockBackgroundType tBackgroundType = BlockBackground.GetRandomBackground();
            //pX,pY作为第一个方块单元的左上角坐标
            base.BlockSet[0] = new BlockUnit(pX, pY, tBackgroundType);
            base.BlockSet[1] = new BlockUnit(pX, pY + BlockBackground.Size, tBackgroundType);
            base.BlockSet[2] = new BlockUnit(pX, pY + 2 * BlockBackground.Size, tBackgroundType);
            base.BlockSet[3] = new BlockUnit(pX, pY + 3 * BlockBackground.Size, tBackgroundType);
        }

        public override void Rotate()
        {
            if (base.RotationState == 1)
            {
                base.BlockSet[1] = new BlockUnit(base.BlockSet[0].X + BlockBackground.Size, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                base.BlockSet[2] = new BlockUnit(base.BlockSet[0].X + 2 * BlockBackground.Size, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                base.BlockSet[3] = new BlockUnit(base.BlockSet[0].X + 3 * BlockBackground.Size, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                base.RotationState = 2;
            }
            else
            {
                base.BlockSet[1] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y + BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                base.BlockSet[2] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y + 2 * BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                base.BlockSet[3] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y + 3 * BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                base.RotationState = 1;
            }
        }

        public override BlockUnit[] AfterRotateBlockUnits()
        {
            BlockUnit[] tBlockUnits = new BlockUnit[4];
            if (base.RotationState == 1)
            {
                tBlockUnits[0] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                tBlockUnits[1] = new BlockUnit(base.BlockSet[0].X + BlockBackground.Size, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                tBlockUnits[2] = new BlockUnit(base.BlockSet[0].X + 2 * BlockBackground.Size, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                tBlockUnits[3] = new BlockUnit(base.BlockSet[0].X + 3 * BlockBackground.Size, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
            }
            else
            {
                tBlockUnits[0] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                tBlockUnits[1] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y + BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                tBlockUnits[2] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y + 2 * BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                tBlockUnits[3] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y + 3 * BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
            }
            return tBlockUnits;
        }

        public override BlockType GetBlockType()
        {
            return BlockType.LineBlock;
        }
    }

    /// <summary>
    ///  口      口口口    口口        口
    ///  口      口          口    口口口
    ///  口口                口
    /// </summary>
    public class LBlock : BaseBlock
    {
        public LBlock(int pX, int pY)
            : base(pX, pY)
        {
            base.BlockSet = new BlockUnit[4];
            BlockBackground.BlockBackgroundType tBackgroundType = BlockBackground.GetRandomBackground();
            //pX,pY作为第一个方块单元的左上角坐标
            base.BlockSet[0] = new BlockUnit(pX, pY, tBackgroundType);
            base.BlockSet[1] = new BlockUnit(pX, pY + BlockBackground.Size, tBackgroundType);
            base.BlockSet[2] = new BlockUnit(pX, pY + 2 * BlockBackground.Size, tBackgroundType);
            base.BlockSet[3] = new BlockUnit(pX + BlockBackground.Size, pY + 2 * BlockBackground.Size, tBackgroundType);
        }

        public override void Rotate()
        {
            switch (base.RotationState)
            {
                case 1:
                    {
                        base.BlockSet[1] = new BlockUnit(base.BlockSet[0].X - BlockBackground.Size, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                        base.BlockSet[2] = new BlockUnit(base.BlockSet[0].X - 2 * BlockBackground.Size, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                        base.BlockSet[3] = new BlockUnit(base.BlockSet[0].X - 2 * BlockBackground.Size, base.BlockSet[0].Y + BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        base.RotationState = 2;
                        break;
                    }
                case 2:
                    {
                        base.BlockSet[1] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y - BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        base.BlockSet[2] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y - 2 * BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        base.BlockSet[3] = new BlockUnit(base.BlockSet[0].X - BlockBackground.Size, base.BlockSet[0].Y - 2 * BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        base.RotationState = 3;
                        break;
                    }
                case 3:
                    {
                        base.BlockSet[1] = new BlockUnit(base.BlockSet[0].X + BlockBackground.Size, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                        base.BlockSet[2] = new BlockUnit(base.BlockSet[0].X + 2 * BlockBackground.Size, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                        base.BlockSet[3] = new BlockUnit(base.BlockSet[0].X + 2 * BlockBackground.Size, base.BlockSet[0].Y - BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        base.RotationState = 4;
                        break;
                    }
                case 4:
                    {
                        base.BlockSet[1] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y + BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        base.BlockSet[2] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y + 2 * BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        base.BlockSet[3] = new BlockUnit(base.BlockSet[0].X + BlockBackground.Size, base.BlockSet[0].Y + 2 * BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        base.RotationState = 1;
                        break;
                    }
                default: break;
            }
        }

        public override BlockUnit[] AfterRotateBlockUnits()
        {
            BlockUnit[] tBlockUnits = new BlockUnit[4];
            switch (base.RotationState)
            {
                case 1:
                    {
                        tBlockUnits[0] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                        tBlockUnits[1] = new BlockUnit(base.BlockSet[0].X - BlockBackground.Size, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                        tBlockUnits[2] = new BlockUnit(base.BlockSet[0].X - 2 * BlockBackground.Size, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                        tBlockUnits[3] = new BlockUnit(base.BlockSet[0].X - 2 * BlockBackground.Size, base.BlockSet[0].Y + BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        break;
                    }
                case 2:
                    {
                        tBlockUnits[0] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                        tBlockUnits[1] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y - BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        tBlockUnits[2] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y - 2 * BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        tBlockUnits[3] = new BlockUnit(base.BlockSet[0].X - BlockBackground.Size, base.BlockSet[0].Y - 2 * BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        break;
                    }
                case 3:
                    {
                        tBlockUnits[0] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                        tBlockUnits[1] = new BlockUnit(base.BlockSet[0].X + BlockBackground.Size, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                        tBlockUnits[2] = new BlockUnit(base.BlockSet[0].X + 2 * BlockBackground.Size, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                        tBlockUnits[3] = new BlockUnit(base.BlockSet[0].X + 2 * BlockBackground.Size, base.BlockSet[0].Y - BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        break;
                    }
                case 4:
                    {
                        tBlockUnits[0] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                        tBlockUnits[1] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y + BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        tBlockUnits[2] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y + 2 * BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        tBlockUnits[3] = new BlockUnit(base.BlockSet[0].X + BlockBackground.Size, base.BlockSet[0].Y + 2 * BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        break;
                    }
                default: break;
            }
            return tBlockUnits;
        }

        public override BlockType GetBlockType()
        {
            return BlockType.LBlock;
        }
    }

    /// <summary>
    ///    口             口口   
    ///    口    口       口     口口口
    ///  口口    口口口   口         口
    /// </summary>
    public class LReserveBlock : BaseBlock
    {
        public LReserveBlock(int pX, int pY)
            : base(pX, pY)
        {
            base.BlockSet = new BlockUnit[4];
            BlockBackground.BlockBackgroundType tBackgroundType = BlockBackground.GetRandomBackground();
            //pX,pY作为第一个方块单元的左上角坐标
            base.BlockSet[0] = new BlockUnit(pX, pY, tBackgroundType);
            base.BlockSet[1] = new BlockUnit(pX, pY + BlockBackground.Size, tBackgroundType);
            base.BlockSet[2] = new BlockUnit(pX, pY + 2 * BlockBackground.Size, tBackgroundType);
            base.BlockSet[3] = new BlockUnit(pX - BlockBackground.Size, pY + 2 * BlockBackground.Size, tBackgroundType);
        }

        public override void Rotate()
        {
            switch (base.RotationState)
            {
                case 1:
                    {
                        base.BlockSet[1] = new BlockUnit(base.BlockSet[0].X - BlockBackground.Size, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                        base.BlockSet[2] = new BlockUnit(base.BlockSet[0].X - 2 * BlockBackground.Size, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                        base.BlockSet[3] = new BlockUnit(base.BlockSet[0].X - 2 * BlockBackground.Size, base.BlockSet[0].Y - BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        base.RotationState = 2;
                        break;
                    }
                case 2:
                    {
                        base.BlockSet[1] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y - BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        base.BlockSet[2] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y - 2 * BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        base.BlockSet[3] = new BlockUnit(base.BlockSet[0].X + BlockBackground.Size, base.BlockSet[0].Y - 2 * BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        base.RotationState = 3;
                        break;
                    }
                case 3:
                    {
                        base.BlockSet[1] = new BlockUnit(base.BlockSet[0].X + BlockBackground.Size, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                        base.BlockSet[2] = new BlockUnit(base.BlockSet[0].X + 2 * BlockBackground.Size, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                        base.BlockSet[3] = new BlockUnit(base.BlockSet[0].X + 2 * BlockBackground.Size, base.BlockSet[0].Y + BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        base.RotationState = 4;
                        break;
                    }
                case 4:
                    {
                        base.BlockSet[1] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y + BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        base.BlockSet[2] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y + 2 * BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        base.BlockSet[3] = new BlockUnit(base.BlockSet[0].X - BlockBackground.Size, base.BlockSet[0].Y + 2 * BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        base.RotationState = 1;
                        break;
                    }
                default: break;
            }
        }

        public override BlockUnit[] AfterRotateBlockUnits()
        {
            BlockUnit[] tBlockUnits = new BlockUnit[4];
            tBlockUnits[0] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
            switch (base.RotationState)
            {
                case 1:
                    {
                        tBlockUnits[1] = new BlockUnit(base.BlockSet[0].X - BlockBackground.Size, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                        tBlockUnits[2] = new BlockUnit(base.BlockSet[0].X - 2 * BlockBackground.Size, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                        tBlockUnits[3] = new BlockUnit(base.BlockSet[0].X - 2 * BlockBackground.Size, base.BlockSet[0].Y - BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        break;
                    }
                case 2:
                    {
                        tBlockUnits[1] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y - BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        tBlockUnits[2] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y - 2 * BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        tBlockUnits[3] = new BlockUnit(base.BlockSet[0].X + BlockBackground.Size, base.BlockSet[0].Y - 2 * BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        break;
                    }
                case 3:
                    {
                        tBlockUnits[1] = new BlockUnit(base.BlockSet[0].X + BlockBackground.Size, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                        tBlockUnits[2] = new BlockUnit(base.BlockSet[0].X + 2 * BlockBackground.Size, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                        tBlockUnits[3] = new BlockUnit(base.BlockSet[0].X + 2 * BlockBackground.Size, base.BlockSet[0].Y + BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        break;
                    }
                case 4:
                    {
                        tBlockUnits[1] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y + BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        tBlockUnits[2] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y + 2 * BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        tBlockUnits[3] = new BlockUnit(base.BlockSet[0].X - BlockBackground.Size, base.BlockSet[0].Y + 2 * BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        break;
                    }
                default: break;
            }
            return tBlockUnits;
        }

        public override BlockType GetBlockType()
        {
            return BlockType.LReserveBlock;
        }
    }

    /// <summary>
    ///  口口口     口      口    口
    ///    口     口口    口口口  口口
    ///             口            口
    /// </summary>
    public class TBlock : BaseBlock
    {
        public TBlock(int pX, int pY)
            : base(pX, pY)
        {
            base.BlockSet = new BlockUnit[4];
            BlockBackground.BlockBackgroundType tBackgroundType = BlockBackground.GetRandomBackground();
            //pX,pY作为第一个方块单元的左上角坐标
            base.BlockSet[0] = new BlockUnit(pX, pY, tBackgroundType);
            base.BlockSet[1] = new BlockUnit(pX + BlockBackground.Size, pY, tBackgroundType);
            base.BlockSet[2] = new BlockUnit(pX + 2 * BlockBackground.Size, pY, tBackgroundType);
            base.BlockSet[3] = new BlockUnit(pX + BlockBackground.Size, pY + BlockBackground.Size, tBackgroundType);
        }

        public override void Rotate()
        {
            switch (base.RotationState)
            {
                case 1:
                    {
                        base.BlockSet[1] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y + BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        base.BlockSet[2] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y + 2 * BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        base.BlockSet[3] = new BlockUnit(base.BlockSet[0].X - BlockBackground.Size, base.BlockSet[0].Y + BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        base.RotationState = 2;
                        break;
                    }
                case 2:
                    {
                        base.BlockSet[1] = new BlockUnit(base.BlockSet[0].X + BlockBackground.Size, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                        base.BlockSet[2] = new BlockUnit(base.BlockSet[0].X + 2 * BlockBackground.Size, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                        base.BlockSet[3] = new BlockUnit(base.BlockSet[0].X + BlockBackground.Size, base.BlockSet[0].Y - BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        base.RotationState = 3;
                        break;
                    }
                case 3:
                    {
                        base.BlockSet[1] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y + BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        base.BlockSet[2] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y + 2 * BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        base.BlockSet[3] = new BlockUnit(base.BlockSet[0].X + BlockBackground.Size, base.BlockSet[0].Y + BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        base.RotationState = 4;
                        break;
                    }
                case 4:
                    {
                        base.BlockSet[1] = new BlockUnit(base.BlockSet[0].X + BlockBackground.Size, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                        base.BlockSet[2] = new BlockUnit(base.BlockSet[0].X + 2 * BlockBackground.Size, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                        base.BlockSet[3] = new BlockUnit(base.BlockSet[0].X + BlockBackground.Size, base.BlockSet[0].Y + BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        base.RotationState = 1;
                        break;
                    }
                default: break;
            }
        }

        public override BlockUnit[] AfterRotateBlockUnits()
        {
            BlockUnit[] tBlockUnits = new BlockUnit[4];
            tBlockUnits[0] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
            switch (base.RotationState)
            {
                case 1:
                    {
                        tBlockUnits[1] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y + BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        tBlockUnits[2] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y + 2 * BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        tBlockUnits[3] = new BlockUnit(base.BlockSet[0].X - BlockBackground.Size, base.BlockSet[0].Y + BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        break;
                    }
                case 2:
                    {
                        tBlockUnits[1] = new BlockUnit(base.BlockSet[0].X + BlockBackground.Size, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                        tBlockUnits[2] = new BlockUnit(base.BlockSet[0].X + 2 * BlockBackground.Size, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                        tBlockUnits[3] = new BlockUnit(base.BlockSet[0].X + BlockBackground.Size, base.BlockSet[0].Y - BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        break;
                    }
                case 3:
                    {
                        tBlockUnits[1] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y + BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        tBlockUnits[2] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y + 2 * BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        tBlockUnits[3] = new BlockUnit(base.BlockSet[0].X + BlockBackground.Size, base.BlockSet[0].Y + BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        break;
                    }
                case 4:
                    {
                        tBlockUnits[1] = new BlockUnit(base.BlockSet[0].X + BlockBackground.Size, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                        tBlockUnits[2] = new BlockUnit(base.BlockSet[0].X + 2 * BlockBackground.Size, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                        tBlockUnits[3] = new BlockUnit(base.BlockSet[0].X + BlockBackground.Size, base.BlockSet[0].Y + BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        break;
                    }
                default: break;
            }
            return tBlockUnits;
        }

        public override BlockType GetBlockType()
        {
            return BlockType.TBlock;
        }
    }

    /// <summary>
    ///  口口
    ///  口口
    /// </summary>
    public class OBlock : BaseBlock
    {
        public OBlock(int pX, int pY)
            : base(pX, pY)
        {
            base.BlockSet = new BlockUnit[4];
            BlockBackground.BlockBackgroundType tBackgroundType = BlockBackground.GetRandomBackground();
            //pX,pY作为第一个方块单元的左上角坐标
            base.BlockSet[0] = new BlockUnit(pX, pY, tBackgroundType);
            base.BlockSet[1] = new BlockUnit(pX + BlockBackground.Size, pY, tBackgroundType);
            base.BlockSet[2] = new BlockUnit(pX, pY + BlockBackground.Size, tBackgroundType);
            base.BlockSet[3] = new BlockUnit(pX + BlockBackground.Size, pY + BlockBackground.Size, tBackgroundType);
        }

        public override void Rotate()
        {
            base.Rotate();
        }

        public override BlockUnit[] AfterRotateBlockUnits()
        {

            return base.BlockSet;
        }

        public override BlockType GetBlockType()
        {
            return BlockType.OBlock;
        }
    }

    /// <summary>
    ///  口口       口    
    ///    口口   口口
    ///           口
    /// </summary>
    public class ZBlock : BaseBlock
    {
        public ZBlock(int pX, int pY)
            : base(pX, pY)
        {
            base.BlockSet = new BlockUnit[4];
            BlockBackground.BlockBackgroundType tBackgroundType = BlockBackground.GetRandomBackground();
            //pX,pY作为第一个方块单元的左上角坐标
            base.BlockSet[0] = new BlockUnit(pX, pY, tBackgroundType);
            base.BlockSet[1] = new BlockUnit(pX + BlockBackground.Size, pY, tBackgroundType);
            base.BlockSet[2] = new BlockUnit(pX + BlockBackground.Size, pY + BlockBackground.Size, tBackgroundType);
            base.BlockSet[3] = new BlockUnit(pX + 2 * BlockBackground.Size, pY + BlockBackground.Size, tBackgroundType);
        }

        public override void Rotate()
        {
            switch (base.RotationState)
            {
                case 1:
                    {
                        base.BlockSet[1] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y + BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        base.BlockSet[2] = new BlockUnit(base.BlockSet[0].X - BlockBackground.Size, base.BlockSet[0].Y + BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        base.BlockSet[3] = new BlockUnit(base.BlockSet[0].X - BlockBackground.Size, base.BlockSet[0].Y + 2 * BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        base.RotationState = 2;
                        break;
                    }
                case 2:
                    {
                        base.BlockSet[1] = new BlockUnit(base.BlockSet[0].X + BlockBackground.Size, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                        base.BlockSet[2] = new BlockUnit(base.BlockSet[0].X + BlockBackground.Size, base.BlockSet[0].Y + BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        base.BlockSet[3] = new BlockUnit(base.BlockSet[0].X + 2 * BlockBackground.Size, base.BlockSet[0].Y + BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        base.RotationState = 1;
                        break;
                    }
                default: break;
            }
        }

        public override BlockUnit[] AfterRotateBlockUnits()
        {
            BlockUnit[] tBlockUnits = new BlockUnit[4];
            tBlockUnits[0] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
            switch (base.RotationState)
            {
                case 1:
                    {
                        tBlockUnits[1] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y + BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        tBlockUnits[2] = new BlockUnit(base.BlockSet[0].X - BlockBackground.Size, base.BlockSet[0].Y + BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        tBlockUnits[3] = new BlockUnit(base.BlockSet[0].X - BlockBackground.Size, base.BlockSet[0].Y + 2 * BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        break;
                    }
                case 2:
                    {
                        tBlockUnits[1] = new BlockUnit(base.BlockSet[0].X + BlockBackground.Size, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                        tBlockUnits[2] = new BlockUnit(base.BlockSet[0].X + BlockBackground.Size, base.BlockSet[0].Y + BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        tBlockUnits[3] = new BlockUnit(base.BlockSet[0].X + 2 * BlockBackground.Size, base.BlockSet[0].Y + BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        break;
                    }
                default: break;
            }
            return tBlockUnits;
        }

        public override BlockType GetBlockType()
        {
            return BlockType.ZBlock;
        }
    }

    /// <summary>
    ///    口口  口
    ///  口口    口口
    ///            口
    /// </summary>
    public class ZReserveBlock : BaseBlock
    {
        public ZReserveBlock(int pX, int pY)
            : base(pX, pY)
        {
            base.BlockSet = new BlockUnit[4];
            BlockBackground.BlockBackgroundType tBackgroundType = BlockBackground.GetRandomBackground();
            //pX,pY作为第一个方块单元的左上角坐标
            base.BlockSet[0] = new BlockUnit(pX, pY, tBackgroundType);
            base.BlockSet[1] = new BlockUnit(pX + BlockBackground.Size, pY, tBackgroundType);
            base.BlockSet[2] = new BlockUnit(pX - BlockBackground.Size, pY + BlockBackground.Size, tBackgroundType);
            base.BlockSet[3] = new BlockUnit(pX, pY + BlockBackground.Size, tBackgroundType);
        }

        public override void Rotate()
        {
            switch (base.RotationState)
            {
                case 1:
                    {
                        base.BlockSet[1] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y + BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        base.BlockSet[2] = new BlockUnit(base.BlockSet[0].X - BlockBackground.Size, base.BlockSet[0].Y - BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        base.BlockSet[3] = new BlockUnit(base.BlockSet[0].X - BlockBackground.Size, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                        base.RotationState = 2;
                        break;
                    }
                case 2:
                    {
                        base.BlockSet[1] = new BlockUnit(base.BlockSet[0].X + BlockBackground.Size, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                        base.BlockSet[2] = new BlockUnit(base.BlockSet[0].X - BlockBackground.Size, base.BlockSet[0].Y + BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        base.BlockSet[3] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y + BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        base.RotationState = 1;
                        break;
                    }
                default: break;
            }
        }

        public override BlockUnit[] AfterRotateBlockUnits()
        {
            BlockUnit[] tBlockUnits = new BlockUnit[4];
            tBlockUnits[0] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
            switch (base.RotationState)
            {
                case 1:
                    {
                        tBlockUnits[1] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y + BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        tBlockUnits[2] = new BlockUnit(base.BlockSet[0].X - BlockBackground.Size, base.BlockSet[0].Y - BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        tBlockUnits[3] = new BlockUnit(base.BlockSet[0].X - BlockBackground.Size, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                        break;
                    }
                case 2:
                    {
                        tBlockUnits[1] = new BlockUnit(base.BlockSet[0].X + BlockBackground.Size, base.BlockSet[0].Y, base.BlockSet[0].BlockBackgroundType);
                        tBlockUnits[2] = new BlockUnit(base.BlockSet[0].X - BlockBackground.Size, base.BlockSet[0].Y + BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        tBlockUnits[3] = new BlockUnit(base.BlockSet[0].X, base.BlockSet[0].Y + BlockBackground.Size, base.BlockSet[0].BlockBackgroundType);
                        break;
                    }
                default: break;
            }
            return tBlockUnits;
        }

        public override BlockType GetBlockType()
        {
            return BlockType.ZReserveBlock;
        }
    }
}
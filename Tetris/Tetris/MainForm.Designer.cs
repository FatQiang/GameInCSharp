namespace Tetris
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox_Canvas = new System.Windows.Forms.PictureBox();
            this.pictureBox_NextBlock = new System.Windows.Forms.PictureBox();
            this.label_Score = new System.Windows.Forms.Label();
            this.label_ScoreNumber = new System.Windows.Forms.Label();
            this.button_StartOrStop = new System.Windows.Forms.Button();
            this.button_ReStart = new System.Windows.Forms.Button();
            this.label_Next = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Canvas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_NextBlock)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_Canvas
            // 
            this.pictureBox_Canvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_Canvas.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_Canvas.Name = "pictureBox_Canvas";
            this.pictureBox_Canvas.Size = new System.Drawing.Size(500, 540);
            this.pictureBox_Canvas.TabIndex = 0;
            this.pictureBox_Canvas.TabStop = false;
            // 
            // pictureBox_NextBlock
            // 
            this.pictureBox_NextBlock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_NextBlock.Location = new System.Drawing.Point(504, 21);
            this.pictureBox_NextBlock.Name = "pictureBox_NextBlock";
            this.pictureBox_NextBlock.Size = new System.Drawing.Size(80, 80);
            this.pictureBox_NextBlock.TabIndex = 1;
            this.pictureBox_NextBlock.TabStop = false;
            // 
            // label_Score
            // 
            this.label_Score.AutoSize = true;
            this.label_Score.Location = new System.Drawing.Point(507, 109);
            this.label_Score.Name = "label_Score";
            this.label_Score.Size = new System.Drawing.Size(41, 12);
            this.label_Score.TabIndex = 2;
            this.label_Score.Text = "得分：";
            // 
            // label_ScoreNumber
            // 
            this.label_ScoreNumber.AutoSize = true;
            this.label_ScoreNumber.Location = new System.Drawing.Point(507, 133);
            this.label_ScoreNumber.Name = "label_ScoreNumber";
            this.label_ScoreNumber.Size = new System.Drawing.Size(11, 12);
            this.label_ScoreNumber.TabIndex = 3;
            this.label_ScoreNumber.Text = "0";
            // 
            // button_StartOrStop
            // 
            this.button_StartOrStop.Location = new System.Drawing.Point(509, 152);
            this.button_StartOrStop.Name = "button_StartOrStop";
            this.button_StartOrStop.Size = new System.Drawing.Size(75, 23);
            this.button_StartOrStop.TabIndex = 4;
            this.button_StartOrStop.Tag = "1";
            this.button_StartOrStop.Text = "开始";
            this.button_StartOrStop.UseVisualStyleBackColor = true;
            this.button_StartOrStop.Click += new System.EventHandler(this.button_StartOrStop_Click);
            // 
            // button_ReStart
            // 
            this.button_ReStart.Location = new System.Drawing.Point(509, 181);
            this.button_ReStart.Name = "button_ReStart";
            this.button_ReStart.Size = new System.Drawing.Size(75, 23);
            this.button_ReStart.TabIndex = 5;
            this.button_ReStart.Text = "重新开始";
            this.button_ReStart.UseVisualStyleBackColor = true;
            this.button_ReStart.Click += new System.EventHandler(this.button_ReStart_Click);
            // 
            // label_Next
            // 
            this.label_Next.AutoSize = true;
            this.label_Next.Location = new System.Drawing.Point(505, 3);
            this.label_Next.Name = "label_Next";
            this.label_Next.Size = new System.Drawing.Size(65, 12);
            this.label_Next.TabIndex = 6;
            this.label_Next.Text = "下一个方块";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 541);
            this.Controls.Add(this.label_Next);
            this.Controls.Add(this.button_ReStart);
            this.Controls.Add(this.button_StartOrStop);
            this.Controls.Add(this.label_ScoreNumber);
            this.Controls.Add(this.label_Score);
            this.Controls.Add(this.pictureBox_NextBlock);
            this.Controls.Add(this.pictureBox_Canvas);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(600, 300);
            this.Name = "MainForm";
            this.Text = "俄罗斯方块";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Canvas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_NextBlock)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_Canvas;
        private System.Windows.Forms.PictureBox pictureBox_NextBlock;
        private System.Windows.Forms.Label label_Score;
        private System.Windows.Forms.Label label_ScoreNumber;
        private System.Windows.Forms.Button button_StartOrStop;
        private System.Windows.Forms.Button button_ReStart;
        private System.Windows.Forms.Label label_Next;
    }
}


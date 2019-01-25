using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tetris
{
    public partial class MainForm : Form
    {
        private GameManager m_GameManager;

        public MainForm()
        {
            this.InitializeComponent();
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.m_GameManager != null)
            {
                this.m_GameManager.KeyUp(e.KeyCode);
            }
        }

        private void button_StartOrStop_Click(object sender, EventArgs e)
        {
            if (this.button_StartOrStop.Tag.ToString() == "1")
            {
                if (this.m_GameManager == null)
                {
                    this.m_GameManager = new GameManager(this.pictureBox_Canvas.CreateGraphics(), this.pictureBox_NextBlock.CreateGraphics(), 500, 540);
                    this.m_GameManager.AfterScoreChanged += this.m_GameManager_AfterScoreChanged;
                }
                this.m_GameManager.Start();
                this.button_StartOrStop.Tag = "2";
                this.button_StartOrStop.Text = "暂停";
            }
            else
            {
                this.m_GameManager.Stop();
                this.button_StartOrStop.Tag = "1";
                this.button_StartOrStop.Text = "开始";
            }
            this.pictureBox_Canvas.Focus();
        }

        public void m_GameManager_AfterScoreChanged()
        {
            this.label_ScoreNumber.Text = this.m_GameManager.Score.ToString();
        }

        private void button_ReStart_Click(object sender, EventArgs e)
        {
            this.m_GameManager.Stop();
            this.m_GameManager.ReLoad();
            this.pictureBox_Canvas.Focus();
            this.button_StartOrStop.Tag = "1";
            this.button_StartOrStop.Text = "开始";
        }

        //取消方向键对控件的焦点的控件，用自己自定义的函数处理各个方向键的处理函数
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up: return true;//不继续处理
                case Keys.Down: return true;
                case Keys.Left: return true;
                case Keys.Right: return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
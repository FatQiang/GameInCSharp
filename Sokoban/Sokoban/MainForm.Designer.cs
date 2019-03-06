namespace Sokoban
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.m_MenuStrip = new System.Windows.Forms.MenuStrip();
            this.m_MenuItemLastLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemNextLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemRestart = new System.Windows.Forms.ToolStripMenuItem();
            this.m_PictureBox = new System.Windows.Forms.PictureBox();
            this.m_StatusStrip = new System.Windows.Forms.StatusStrip();
            this.m_StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_MenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_PictureBox)).BeginInit();
            this.m_StatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_MenuStrip
            // 
            this.m_MenuStrip.BackColor = System.Drawing.Color.SkyBlue;
            this.m_MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_MenuItemLastLevel,
            this.m_MenuItemNextLevel,
            this.m_MenuItemRestart});
            this.m_MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.m_MenuStrip.Name = "m_MenuStrip";
            this.m_MenuStrip.Size = new System.Drawing.Size(833, 25);
            this.m_MenuStrip.TabIndex = 1;
            this.m_MenuStrip.Text = "menuStrip1";
            // 
            // m_MenuItemLastLevel
            // 
            this.m_MenuItemLastLevel.BackColor = System.Drawing.Color.LightGreen;
            this.m_MenuItemLastLevel.Image = global::Sokoban.Properties.Resources.last;
            this.m_MenuItemLastLevel.Name = "m_MenuItemLastLevel";
            this.m_MenuItemLastLevel.Size = new System.Drawing.Size(72, 21);
            this.m_MenuItemLastLevel.Text = "上一关";
            this.m_MenuItemLastLevel.ToolTipText = "下一关";
            this.m_MenuItemLastLevel.Click += new System.EventHandler(this.m_MenuItemLastLevel_Click);
            // 
            // m_MenuItemNextLevel
            // 
            this.m_MenuItemNextLevel.BackColor = System.Drawing.Color.LightGreen;
            this.m_MenuItemNextLevel.Image = global::Sokoban.Properties.Resources.next;
            this.m_MenuItemNextLevel.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.m_MenuItemNextLevel.Name = "m_MenuItemNextLevel";
            this.m_MenuItemNextLevel.Size = new System.Drawing.Size(72, 21);
            this.m_MenuItemNextLevel.Text = "下一关";
            this.m_MenuItemNextLevel.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.m_MenuItemNextLevel.ToolTipText = "下一关";
            this.m_MenuItemNextLevel.Click += new System.EventHandler(this.m_MenuItemNextLevel_Click);
            // 
            // m_MenuItemRestart
            // 
            this.m_MenuItemRestart.BackColor = System.Drawing.Color.LightGreen;
            this.m_MenuItemRestart.Image = global::Sokoban.Properties.Resources.restart;
            this.m_MenuItemRestart.Name = "m_MenuItemRestart";
            this.m_MenuItemRestart.Size = new System.Drawing.Size(84, 21);
            this.m_MenuItemRestart.Text = "重新开始";
            this.m_MenuItemRestart.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.m_MenuItemRestart.ToolTipText = "重新开始本关";
            this.m_MenuItemRestart.Click += new System.EventHandler(this.m_MenuItemRestart_Click);
            // 
            // m_PictureBox
            // 
            this.m_PictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_PictureBox.Location = new System.Drawing.Point(0, 25);
            this.m_PictureBox.Name = "m_PictureBox";
            this.m_PictureBox.Size = new System.Drawing.Size(833, 480);
            this.m_PictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.m_PictureBox.TabIndex = 0;
            this.m_PictureBox.TabStop = false;
            this.m_PictureBox.SizeChanged += new System.EventHandler(this.m_PictureBox_SizeChanged);
            this.m_PictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.m_PictureBox_Paint);
            this.m_PictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.m_PictureBox_MouseClick);
            this.m_PictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.m_PictureBox_MouseMove);
            // 
            // m_StatusStrip
            // 
            this.m_StatusStrip.BackColor = System.Drawing.Color.SkyBlue;
            this.m_StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_StatusLabel});
            this.m_StatusStrip.Location = new System.Drawing.Point(0, 483);
            this.m_StatusStrip.Name = "m_StatusStrip";
            this.m_StatusStrip.Size = new System.Drawing.Size(833, 22);
            this.m_StatusStrip.TabIndex = 2;
            this.m_StatusStrip.Text = "statusStrip1";
            // 
            // m_StatusLabel
            // 
            this.m_StatusLabel.Name = "m_StatusLabel";
            this.m_StatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 505);
            this.Controls.Add(this.m_StatusStrip);
            this.Controls.Add(this.m_PictureBox);
            this.Controls.Add(this.m_MenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.m_MenuStrip;
            this.Name = "MainForm";
            this.Text = "推箱子";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.m_MenuStrip.ResumeLayout(false);
            this.m_MenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_PictureBox)).EndInit();
            this.m_StatusStrip.ResumeLayout(false);
            this.m_StatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox m_PictureBox;
        private System.Windows.Forms.MenuStrip m_MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemLastLevel;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemNextLevel;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemRestart;
        private System.Windows.Forms.StatusStrip m_StatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel m_StatusLabel;
    }
}


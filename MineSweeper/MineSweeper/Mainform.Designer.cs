namespace MineSweeper
{
    partial class Mainform
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
            this.m_PictureBoxMain = new System.Windows.Forms.PictureBox();
            this.m_ComboBoxType = new System.Windows.Forms.ComboBox();
            this.m_LabelType = new System.Windows.Forms.Label();
            this.m_ButtonOK = new System.Windows.Forms.Button();
            this.m_TextBoxMineCount = new System.Windows.Forms.TextBox();
            this.m_LabelMineCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.m_PictureBoxMain)).BeginInit();
            this.SuspendLayout();
            // 
            // m_PictureBoxMain
            // 
            this.m_PictureBoxMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_PictureBoxMain.Location = new System.Drawing.Point(0, 0);
            this.m_PictureBoxMain.Name = "m_PictureBoxMain";
            this.m_PictureBoxMain.Size = new System.Drawing.Size(500, 500);
            this.m_PictureBoxMain.TabIndex = 0;
            this.m_PictureBoxMain.TabStop = false;
            this.m_PictureBoxMain.SizeChanged += new System.EventHandler(this.m_PictureBoxMain_SizeChanged);
            this.m_PictureBoxMain.MouseClick += new System.Windows.Forms.MouseEventHandler(this.m_PictureBoxMain_MouseClick);
            // 
            // m_ComboBoxType
            // 
            this.m_ComboBoxType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ComboBoxType.FormattingEnabled = true;
            this.m_ComboBoxType.Items.AddRange(new object[] {
            "5X5",
            "10X10",
            "20X20",
            "30X30",
            "40X40"});
            this.m_ComboBoxType.Location = new System.Drawing.Point(546, 24);
            this.m_ComboBoxType.Name = "m_ComboBoxType";
            this.m_ComboBoxType.Size = new System.Drawing.Size(83, 20);
            this.m_ComboBoxType.TabIndex = 1;
            this.m_ComboBoxType.SelectedIndexChanged += new System.EventHandler(this.m_ComboBoxType_SelectedIndexChanged);
            // 
            // m_LabelType
            // 
            this.m_LabelType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_LabelType.AutoSize = true;
            this.m_LabelType.Location = new System.Drawing.Point(511, 27);
            this.m_LabelType.Name = "m_LabelType";
            this.m_LabelType.Size = new System.Drawing.Size(29, 12);
            this.m_LabelType.TabIndex = 2;
            this.m_LabelType.Text = "难度";
            // 
            // m_ButtonOK
            // 
            this.m_ButtonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ButtonOK.Location = new System.Drawing.Point(554, 73);
            this.m_ButtonOK.Name = "m_ButtonOK";
            this.m_ButtonOK.Size = new System.Drawing.Size(75, 23);
            this.m_ButtonOK.TabIndex = 3;
            this.m_ButtonOK.Text = "确定";
            this.m_ButtonOK.UseVisualStyleBackColor = true;
            this.m_ButtonOK.Click += new System.EventHandler(this.m_ButtonOK_Click);
            // 
            // m_TextBoxMineCount
            // 
            this.m_TextBoxMineCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_TextBoxMineCount.Location = new System.Drawing.Point(546, 50);
            this.m_TextBoxMineCount.Name = "m_TextBoxMineCount";
            this.m_TextBoxMineCount.Size = new System.Drawing.Size(83, 21);
            this.m_TextBoxMineCount.TabIndex = 4;
            this.m_TextBoxMineCount.Text = "10";
            // 
            // m_LabelMineCount
            // 
            this.m_LabelMineCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_LabelMineCount.AutoSize = true;
            this.m_LabelMineCount.Location = new System.Drawing.Point(511, 53);
            this.m_LabelMineCount.Name = "m_LabelMineCount";
            this.m_LabelMineCount.Size = new System.Drawing.Size(29, 12);
            this.m_LabelMineCount.TabIndex = 5;
            this.m_LabelMineCount.Text = "难度";
            // 
            // Mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 500);
            this.Controls.Add(this.m_LabelMineCount);
            this.Controls.Add(this.m_TextBoxMineCount);
            this.Controls.Add(this.m_ButtonOK);
            this.Controls.Add(this.m_LabelType);
            this.Controls.Add(this.m_ComboBoxType);
            this.Controls.Add(this.m_PictureBoxMain);
            this.MinimumSize = new System.Drawing.Size(648, 539);
            this.Name = "Mainform";
            this.Text = "Express";
            this.Load += new System.EventHandler(this.Mainform_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_PictureBoxMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox m_PictureBoxMain;
        private System.Windows.Forms.ComboBox m_ComboBoxType;
        private System.Windows.Forms.Label m_LabelType;
        private System.Windows.Forms.Button m_ButtonOK;
        private System.Windows.Forms.TextBox m_TextBoxMineCount;
        private System.Windows.Forms.Label m_LabelMineCount;
    }
}


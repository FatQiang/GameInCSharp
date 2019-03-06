namespace SokobanLevelEditor
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
            this.m_PictureBox = new System.Windows.Forms.PictureBox();
            this.m_ButtonAddWall = new System.Windows.Forms.Button();
            this.m_ButtonAddMan = new System.Windows.Forms.Button();
            this.m_ButtonAddBox = new System.Windows.Forms.Button();
            this.m_NumericUpDownRow = new System.Windows.Forms.NumericUpDown();
            this.m_NumericUpDownColumn = new System.Windows.Forms.NumericUpDown();
            this.m_LabelRowCount = new System.Windows.Forms.Label();
            this.m_LabelColumnCount = new System.Windows.Forms.Label();
            this.m_ButtonInitMap = new System.Windows.Forms.Button();
            this.m_ButtonAddTarget = new System.Windows.Forms.Button();
            this.m_ButtonFloor = new System.Windows.Forms.Button();
            this.m_ButtonDelete = new System.Windows.Forms.Button();
            this.m_GroupBoxBasic = new System.Windows.Forms.GroupBox();
            this.m_GroupBoxElement = new System.Windows.Forms.GroupBox();
            this.m_MenuStrip = new System.Windows.Forms.MenuStrip();
            this.m_MenuItemSelectLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemSaveLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.m_TextBoxName = new System.Windows.Forms.TextBox();
            this.m_LabelName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.m_PictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownColumn)).BeginInit();
            this.m_GroupBoxBasic.SuspendLayout();
            this.m_GroupBoxElement.SuspendLayout();
            this.m_MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_PictureBox
            // 
            this.m_PictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_PictureBox.Location = new System.Drawing.Point(0, 26);
            this.m_PictureBox.Name = "m_PictureBox";
            this.m_PictureBox.Size = new System.Drawing.Size(465, 399);
            this.m_PictureBox.TabIndex = 0;
            this.m_PictureBox.TabStop = false;
            this.m_PictureBox.SizeChanged += new System.EventHandler(this.m_PictureBox_SizeChanged);
            this.m_PictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.m_PictureBox_MouseClick);
            // 
            // m_ButtonAddWall
            // 
            this.m_ButtonAddWall.Location = new System.Drawing.Point(27, 20);
            this.m_ButtonAddWall.Name = "m_ButtonAddWall";
            this.m_ButtonAddWall.Size = new System.Drawing.Size(75, 23);
            this.m_ButtonAddWall.TabIndex = 1;
            this.m_ButtonAddWall.Text = "墙";
            this.m_ButtonAddWall.UseVisualStyleBackColor = true;
            this.m_ButtonAddWall.Click += new System.EventHandler(this.m_ButtonAddWall_Click);
            // 
            // m_ButtonAddMan
            // 
            this.m_ButtonAddMan.Location = new System.Drawing.Point(27, 49);
            this.m_ButtonAddMan.Name = "m_ButtonAddMan";
            this.m_ButtonAddMan.Size = new System.Drawing.Size(75, 23);
            this.m_ButtonAddMan.TabIndex = 2;
            this.m_ButtonAddMan.Text = "人";
            this.m_ButtonAddMan.UseVisualStyleBackColor = true;
            this.m_ButtonAddMan.Click += new System.EventHandler(this.m_ButtonAddMan_Click);
            // 
            // m_ButtonAddBox
            // 
            this.m_ButtonAddBox.Location = new System.Drawing.Point(27, 78);
            this.m_ButtonAddBox.Name = "m_ButtonAddBox";
            this.m_ButtonAddBox.Size = new System.Drawing.Size(75, 23);
            this.m_ButtonAddBox.TabIndex = 3;
            this.m_ButtonAddBox.Text = "箱子";
            this.m_ButtonAddBox.UseVisualStyleBackColor = true;
            this.m_ButtonAddBox.Click += new System.EventHandler(this.m_ButtonAddBox_Click);
            // 
            // m_NumericUpDownRow
            // 
            this.m_NumericUpDownRow.Location = new System.Drawing.Point(39, 18);
            this.m_NumericUpDownRow.Name = "m_NumericUpDownRow";
            this.m_NumericUpDownRow.Size = new System.Drawing.Size(144, 21);
            this.m_NumericUpDownRow.TabIndex = 4;
            this.m_NumericUpDownRow.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // m_NumericUpDownColumn
            // 
            this.m_NumericUpDownColumn.Location = new System.Drawing.Point(39, 45);
            this.m_NumericUpDownColumn.Name = "m_NumericUpDownColumn";
            this.m_NumericUpDownColumn.Size = new System.Drawing.Size(144, 21);
            this.m_NumericUpDownColumn.TabIndex = 5;
            this.m_NumericUpDownColumn.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // m_LabelRowCount
            // 
            this.m_LabelRowCount.AutoSize = true;
            this.m_LabelRowCount.Location = new System.Drawing.Point(4, 20);
            this.m_LabelRowCount.Name = "m_LabelRowCount";
            this.m_LabelRowCount.Size = new System.Drawing.Size(29, 12);
            this.m_LabelRowCount.TabIndex = 6;
            this.m_LabelRowCount.Text = "行数";
            // 
            // m_LabelColumnCount
            // 
            this.m_LabelColumnCount.AutoSize = true;
            this.m_LabelColumnCount.Location = new System.Drawing.Point(4, 47);
            this.m_LabelColumnCount.Name = "m_LabelColumnCount";
            this.m_LabelColumnCount.Size = new System.Drawing.Size(29, 12);
            this.m_LabelColumnCount.TabIndex = 7;
            this.m_LabelColumnCount.Text = "列数";
            // 
            // m_ButtonInitMap
            // 
            this.m_ButtonInitMap.Location = new System.Drawing.Point(108, 100);
            this.m_ButtonInitMap.Name = "m_ButtonInitMap";
            this.m_ButtonInitMap.Size = new System.Drawing.Size(75, 23);
            this.m_ButtonInitMap.TabIndex = 8;
            this.m_ButtonInitMap.Text = "生成";
            this.m_ButtonInitMap.UseVisualStyleBackColor = true;
            this.m_ButtonInitMap.Click += new System.EventHandler(this.m_ButtonInitMap_Click);
            // 
            // m_ButtonAddTarget
            // 
            this.m_ButtonAddTarget.Location = new System.Drawing.Point(108, 20);
            this.m_ButtonAddTarget.Name = "m_ButtonAddTarget";
            this.m_ButtonAddTarget.Size = new System.Drawing.Size(75, 23);
            this.m_ButtonAddTarget.TabIndex = 9;
            this.m_ButtonAddTarget.Text = "目标";
            this.m_ButtonAddTarget.UseVisualStyleBackColor = true;
            this.m_ButtonAddTarget.Click += new System.EventHandler(this.m_ButtonAddTarget_Click);
            // 
            // m_ButtonFloor
            // 
            this.m_ButtonFloor.Location = new System.Drawing.Point(108, 49);
            this.m_ButtonFloor.Name = "m_ButtonFloor";
            this.m_ButtonFloor.Size = new System.Drawing.Size(75, 23);
            this.m_ButtonFloor.TabIndex = 10;
            this.m_ButtonFloor.Text = "地板";
            this.m_ButtonFloor.UseVisualStyleBackColor = true;
            this.m_ButtonFloor.Click += new System.EventHandler(this.m_ButtonFloor_Click);
            // 
            // m_ButtonDelete
            // 
            this.m_ButtonDelete.Location = new System.Drawing.Point(108, 78);
            this.m_ButtonDelete.Name = "m_ButtonDelete";
            this.m_ButtonDelete.Size = new System.Drawing.Size(75, 23);
            this.m_ButtonDelete.TabIndex = 11;
            this.m_ButtonDelete.Text = "删除";
            this.m_ButtonDelete.UseVisualStyleBackColor = true;
            this.m_ButtonDelete.Click += new System.EventHandler(this.m_ButtonDelete_Click);
            // 
            // m_GroupBoxBasic
            // 
            this.m_GroupBoxBasic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_GroupBoxBasic.Controls.Add(this.m_LabelName);
            this.m_GroupBoxBasic.Controls.Add(this.m_TextBoxName);
            this.m_GroupBoxBasic.Controls.Add(this.m_NumericUpDownColumn);
            this.m_GroupBoxBasic.Controls.Add(this.m_NumericUpDownRow);
            this.m_GroupBoxBasic.Controls.Add(this.m_LabelRowCount);
            this.m_GroupBoxBasic.Controls.Add(this.m_LabelColumnCount);
            this.m_GroupBoxBasic.Controls.Add(this.m_ButtonInitMap);
            this.m_GroupBoxBasic.Location = new System.Drawing.Point(471, 26);
            this.m_GroupBoxBasic.Name = "m_GroupBoxBasic";
            this.m_GroupBoxBasic.Size = new System.Drawing.Size(226, 125);
            this.m_GroupBoxBasic.TabIndex = 12;
            this.m_GroupBoxBasic.TabStop = false;
            this.m_GroupBoxBasic.Text = "地图";
            // 
            // m_GroupBoxElement
            // 
            this.m_GroupBoxElement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_GroupBoxElement.Controls.Add(this.m_ButtonAddWall);
            this.m_GroupBoxElement.Controls.Add(this.m_ButtonAddMan);
            this.m_GroupBoxElement.Controls.Add(this.m_ButtonDelete);
            this.m_GroupBoxElement.Controls.Add(this.m_ButtonAddBox);
            this.m_GroupBoxElement.Controls.Add(this.m_ButtonFloor);
            this.m_GroupBoxElement.Controls.Add(this.m_ButtonAddTarget);
            this.m_GroupBoxElement.Location = new System.Drawing.Point(471, 157);
            this.m_GroupBoxElement.Name = "m_GroupBoxElement";
            this.m_GroupBoxElement.Size = new System.Drawing.Size(226, 124);
            this.m_GroupBoxElement.TabIndex = 13;
            this.m_GroupBoxElement.TabStop = false;
            this.m_GroupBoxElement.Text = "元素";
            // 
            // m_MenuStrip
            // 
            this.m_MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_MenuItemSelectLevel,
            this.m_MenuItemSaveLevel});
            this.m_MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.m_MenuStrip.Name = "m_MenuStrip";
            this.m_MenuStrip.Size = new System.Drawing.Size(709, 25);
            this.m_MenuStrip.TabIndex = 14;
            this.m_MenuStrip.Text = "menuStrip1";
            // 
            // m_MenuItemSelectLevel
            // 
            this.m_MenuItemSelectLevel.Name = "m_MenuItemSelectLevel";
            this.m_MenuItemSelectLevel.Size = new System.Drawing.Size(68, 21);
            this.m_MenuItemSelectLevel.Text = "选择关卡";
            this.m_MenuItemSelectLevel.Click += new System.EventHandler(this.m_MenuItemSelectLevel_Click);
            // 
            // m_MenuItemSaveLevel
            // 
            this.m_MenuItemSaveLevel.Name = "m_MenuItemSaveLevel";
            this.m_MenuItemSaveLevel.Size = new System.Drawing.Size(68, 21);
            this.m_MenuItemSaveLevel.Text = "保存关卡";
            this.m_MenuItemSaveLevel.Click += new System.EventHandler(this.m_MenuItemSaveLevel_Click);
            // 
            // m_TextBoxName
            // 
            this.m_TextBoxName.Location = new System.Drawing.Point(39, 73);
            this.m_TextBoxName.Name = "m_TextBoxName";
            this.m_TextBoxName.Size = new System.Drawing.Size(144, 21);
            this.m_TextBoxName.TabIndex = 9;
            this.m_TextBoxName.Text = "New Level";
            // 
            // m_LabelName
            // 
            this.m_LabelName.AutoSize = true;
            this.m_LabelName.Location = new System.Drawing.Point(4, 76);
            this.m_LabelName.Name = "m_LabelName";
            this.m_LabelName.Size = new System.Drawing.Size(29, 12);
            this.m_LabelName.TabIndex = 10;
            this.m_LabelName.Text = "名称";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 425);
            this.Controls.Add(this.m_MenuStrip);
            this.Controls.Add(this.m_GroupBoxElement);
            this.Controls.Add(this.m_GroupBoxBasic);
            this.Controls.Add(this.m_PictureBox);
            this.MainMenuStrip = this.m_MenuStrip;
            this.Name = "MainForm";
            this.Text = "关卡编辑器";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_PictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownColumn)).EndInit();
            this.m_GroupBoxBasic.ResumeLayout(false);
            this.m_GroupBoxBasic.PerformLayout();
            this.m_GroupBoxElement.ResumeLayout(false);
            this.m_MenuStrip.ResumeLayout(false);
            this.m_MenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox m_PictureBox;
        private System.Windows.Forms.Button m_ButtonAddWall;
        private System.Windows.Forms.Button m_ButtonAddMan;
        private System.Windows.Forms.Button m_ButtonAddBox;
        private System.Windows.Forms.NumericUpDown m_NumericUpDownRow;
        private System.Windows.Forms.NumericUpDown m_NumericUpDownColumn;
        private System.Windows.Forms.Label m_LabelRowCount;
        private System.Windows.Forms.Label m_LabelColumnCount;
        private System.Windows.Forms.Button m_ButtonInitMap;
        private System.Windows.Forms.Button m_ButtonAddTarget;
        private System.Windows.Forms.Button m_ButtonFloor;
        private System.Windows.Forms.Button m_ButtonDelete;
        private System.Windows.Forms.GroupBox m_GroupBoxBasic;
        private System.Windows.Forms.GroupBox m_GroupBoxElement;
        private System.Windows.Forms.MenuStrip m_MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemSelectLevel;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemSaveLevel;
        private System.Windows.Forms.Label m_LabelName;
        private System.Windows.Forms.TextBox m_TextBoxName;
    }
}


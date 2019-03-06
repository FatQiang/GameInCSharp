using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace Sokoban
{
    public partial class MainForm : Form
    {
        private SokobanManager m_SokobanManager;

        private List<LevelClass> m_LevelList;

        private int m_LevelNow = 0;

        public MainForm()
        {
            this.InitializeComponent();

            //读取关卡信息
            this.m_LevelList = new List<LevelClass>();
            XmlDocument tXmlDocument = new XmlDocument();
            tXmlDocument.Load(Application.StartupPath + "\\Levels\\Levels.xml");
            XmlNode tLevelRootNode = tXmlDocument.ChildNodes[1];
            foreach (XmlNode tLevelNode in tLevelRootNode.ChildNodes)
            {
                XmlNode tNumNode = tLevelNode.ChildNodes[0];
                XmlNode tNameNode = tLevelNode.ChildNodes[1];
                XmlNode tPathNode = tLevelNode.ChildNodes[2];
                LevelClass tLevelClass = new Sokoban.LevelClass();
                tLevelClass.Num = int.Parse(tNumNode.InnerText);
                tLevelClass.Name = tNameNode.InnerText;
                string tLevelPath = Application.StartupPath + "\\Levels\\" + tPathNode.InnerText;
                FileStream tFileStream = new FileStream(tLevelPath, FileMode.Open);
                StreamReader tStreamReader = new StreamReader(tFileStream);
                tStreamReader.ReadLine();//第一行是Man Loaction标注
                string[] tManLocationSplit = tStreamReader.ReadLine().Split(',');//读取人物位置
                tLevelClass.ManLocation = new int[] { int.Parse(tManLocationSplit[0]), int.Parse(tManLocationSplit[1]) };
                tStreamReader.ReadLine();//第三行是Box Count标注
                int tBoxCount = int.Parse(tStreamReader.ReadLine());//读取箱子数量
                tStreamReader.ReadLine();//第五行是Box Location标注
                tLevelClass.BoxList = new List<int[]>();
                for (int i = 0; i < tBoxCount; i++)
                {
                    string[] tBoxLocationSplit = tStreamReader.ReadLine().Split(',');//读取箱子位置
                    tLevelClass.BoxList.Add(new int[] { int.Parse(tBoxLocationSplit[0]), int.Parse(tBoxLocationSplit[1]) });
                }
                tStreamReader.ReadLine();//这一行是Map标注
                string[] tMapSizeSplit = tStreamReader.ReadLine().Split(',');//地图尺寸
                int tRowCount = int.Parse(tMapSizeSplit[0]);
                int tColumnCount = int.Parse(tMapSizeSplit[1]);
                tLevelClass.Map = new int[tRowCount, tColumnCount];
                for (int i = 0; i < tRowCount; i++)
                {
                    string[] tMapRowEles = tStreamReader.ReadLine().Split(',');
                    for (int j = 0; j < tColumnCount; j++)
                    {
                        tLevelClass.Map[i, j] = int.Parse(tMapRowEles[j]);
                    }
                }
                this.m_LevelList.Add(tLevelClass);
                tStreamReader.Close();
                tFileStream.Close();
            }

            this.m_SokobanManager = new Sokoban.SokobanManager(this.m_PictureBox, this.m_LevelList[this.m_LevelNow]);
            this.m_StatusLabel.Text = this.m_LevelList[this.m_LevelNow].Name;
        }

        private void m_PictureBox_SizeChanged(object sender, EventArgs e)
        {
            this.m_SokobanManager.Refresh();
        }

        private void m_PictureBox_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    this.m_SokobanManager.Move(0);
                    break;
                case Keys.Down:
                    this.m_SokobanManager.Move(1);
                    break;
                case Keys.Left:
                    this.m_SokobanManager.Move(2);
                    break;
                case Keys.Right:
                    this.m_SokobanManager.Move(3);
                    break;
                case Keys.E:
                    this.m_SokobanManager.Ronate(0);
                    break;
                case Keys.Q:
                    this.m_SokobanManager.Ronate(1);
                    break;
                default:break;
            }
        }

        private void m_PictureBox_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void m_PictureBox_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void m_MenuItemNextLevel_Click(object sender, EventArgs e)
        {
            if (this.m_LevelNow == (this.m_LevelList.Count - 1))
            {
                MessageBox.Show("现在已经是最后一关啦！");
                return;
            }
            this.m_LevelNow++;
            this.m_SokobanManager = new Sokoban.SokobanManager(this.m_PictureBox, this.m_LevelList[this.m_LevelNow]);
            this.m_StatusLabel.Text = this.m_LevelList[this.m_LevelNow].Name;
        }

        private void m_MenuItemLastLevel_Click(object sender, EventArgs e)
        {
            if (this.m_LevelNow == 0)
            {
                MessageBox.Show("现在是第一关，没有上一关啦！");
                return;
            }
            this.m_LevelNow--;
            this.m_SokobanManager = new Sokoban.SokobanManager(this.m_PictureBox, this.m_LevelList[this.m_LevelNow]);
            this.m_StatusLabel.Text = this.m_LevelList[this.m_LevelNow].Name;
        }

        private void m_MenuItemRestart_Click(object sender, EventArgs e)
        {
            this.m_SokobanManager = new Sokoban.SokobanManager(this.m_PictureBox, this.m_LevelList[this.m_LevelNow]);
            this.m_StatusLabel.Text = this.m_LevelList[this.m_LevelNow].Name;
        }
    }
}
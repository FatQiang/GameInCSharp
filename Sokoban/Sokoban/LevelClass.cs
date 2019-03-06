using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    public class LevelClass
    {
        public int Num = -1;
        public string Name = "";
        public int[] ManLocation = new int[] { -1, -1 };
        public List<int[]> BoxList = null;
        public int[,] Map = null;
    }
}
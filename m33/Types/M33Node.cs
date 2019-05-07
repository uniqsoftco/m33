using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using m33.Types;

namespace m33.Types
{
    public class M33Node
    {
        //private int[,] _matrix;
        public int[,] Matrix { get; set; }
        public int Heuristic = 0;
        public int Depth = 0;
        public Point BlankSpaceCoord { get; set; }
        public string Path { get; set; }

        public M33Node(int[,] matrix, Point blankSpaceCoord, int heuristic, int depth, string path)
        {
            Path = path;
            Matrix = matrix;
            BlankSpaceCoord = blankSpaceCoord;
            Heuristic = heuristic;
            Depth = depth;
        }
        public M33Node(int[,] matrix, int heuristic)
        {
            Matrix = matrix;
            BlankSpaceCoord = Helper.FindBlankSpace(matrix);
            Heuristic = heuristic;
            Path = "";
            Depth = 0;
        }

        public override string ToString()
        {
            string tmp = string.Empty;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    tmp += Matrix[i, j].ToString();
                }

                tmp += "\r\n";
            }

            return tmp;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using m33.Algorithm.Interfaces;
using m33.Types;

namespace m33.Algorithm.Heuristics
{
    public class ManhattanHeuristic : IM33Heuristic
    {
        public int CalcHeuristic(int[,] matrix)
        {
            int heuristic = 0;
            Point tmpPoint;

            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    tmpPoint = Const.ManhatanTarget[matrix[x, y]];
                    heuristic += Math.Abs(x - tmpPoint.X) + Math.Abs(y - tmpPoint.Y);
                }
            }

            return heuristic;
        }
    }
}

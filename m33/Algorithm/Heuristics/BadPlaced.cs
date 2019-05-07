using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using m33.Algorithm.Interfaces;

namespace m33.Algorithm.Heuristics
{
    public class BadPlacedHeuristic : IM33Heuristic
    {
        public int CalcHeuristic(int[,] matrix)
        {
            int heuristic = 0;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (matrix[i, j] != Const.Target[i, j])
                    {
                        heuristic++;
                    }
                }
            }

            return heuristic;
        }
    }
}

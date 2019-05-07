using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using m33.Algorithm.Interfaces;

namespace m33.Algorithm.Heuristics
{
    public class MultipleHeuristic : IM33Heuristic
    {
        private readonly List<IM33Heuristic> _heuristics = new List<IM33Heuristic>()
        {
            new RowHeuristic(),
            new ManhattanHeuristic(),
            new BadPlacedHeuristic()
        };
        public int CalcHeuristic(int[,] matrix)
        {
            int intHeuristic = 0;

            foreach (IM33Heuristic heuristic in _heuristics)
            {
                intHeuristic += heuristic.CalcHeuristic(matrix);
            }

            return intHeuristic;
        }
    }
}

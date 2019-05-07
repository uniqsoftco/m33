using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using m33.Types;

namespace m33.Algorithm.Interfaces
{
    public interface IM33Heuristic
    {
        int CalcHeuristic(int[,] matrix);
    }
}

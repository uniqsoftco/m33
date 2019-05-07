using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using m33.Types;

namespace m33.Algorithm.Interfaces
{
    public interface IM33Algorithm
    {
        M33Node SelectBestNode(List<M33Node> queue, M33Node node = null);
        void DoAfterSelection(List<M33Node> queue, M33Node node = null);
        bool IsTarget(M33Node node);
        int CalculateHeuristic(int[,] matrix);
        void Go(int[,] matrix);
    }
}

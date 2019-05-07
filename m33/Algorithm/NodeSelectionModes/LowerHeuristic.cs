using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using m33.NodeSelector;
using m33.Types;

namespace m33.Algorithm.NodeSelectionModes
{
    public class LowerHeuristic : IM33BestNodeSelector
    {
        private M33Node _lowerHeuristic;
        public M33Node SelectBestNode(List<M33Node> queue, M33Node previousNode)
        {
            if (_lowerHeuristic == null)
            {
                _lowerHeuristic = queue[0];
                goto first;
            }

            for (int i = 0; i < queue.Count; i++)
            {
                if (queue[i].Heuristic <= _lowerHeuristic.Heuristic)
                {
                    _lowerHeuristic = queue[i];
                }
            }

            first: ;
            if (_lowerHeuristic == previousNode)
            {
                _lowerHeuristic = null;
            }
            else
            {
                previousNode = _lowerHeuristic;
            }

            return _lowerHeuristic;
        }
    }
}

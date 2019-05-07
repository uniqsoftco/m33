using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using m33.NodeSelector;
using m33.Types;

namespace m33.Algorithm.NodeSelectionModes
{
    public class LowestHeuristicPlusDepth : IM33BestNodeSelector
    {
        public M33Node SelectBestNode(List<M33Node> queue, M33Node node = null)
        {
            M33Node lowestHeuristic = queue[0];

            for (int i = 1; i < queue.Count; i++)
            {
                if (queue[i].Heuristic/* + queue[i].Depth */< lowestHeuristic.Heuristic/* + lowestHeuristic.Depth*/)
                {
                    lowestHeuristic = queue[i];
                }
            }

            queue.Remove(lowestHeuristic);
            return lowestHeuristic;
        }
    }
}

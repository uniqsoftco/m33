using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using m33.Types;

namespace m33
{
    public static class Stats
    {
        public static uint SearchedNodesCount = 0;
        public static double TotalTime = .0d;
        public static Timer Timer = new Timer(100);
        public static int MaxSeenDepth = 0;
        public static long MaxUsedMemory = 0;

        static Stats()
        {
            Timer.Elapsed += Timer_Elapsed;
            Timer.Enabled = true;
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            TotalTime += .1d;
        }

        public static void UpdateStats(M33Node node)
        {
            if (node == null)
                return;

            SearchedNodesCount++;

            if (MaxSeenDepth < node.Depth)
                MaxSeenDepth = node.Depth;

            long totalMemory = Debug.GetTotalMemory();
            if (MaxUsedMemory < totalMemory)
                MaxUsedMemory = totalMemory;
        }
    }
}

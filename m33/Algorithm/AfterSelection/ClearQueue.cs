using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using m33.Algorithm.Interfaces;
using m33.Types;

namespace m33.Algorithm.AfterSelection
{
    public class ClearQueue : IM33DoAfterSelection
    {
        public void DoAfterSelection(List<M33Node> queue, M33Node node = null)
        {
            queue.Clear();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using m33.Types;

namespace m33.NodeSelector
{
    public interface IM33BestNodeSelector
    {
        M33Node SelectBestNode(List<M33Node> queue, M33Node previousNode = null);
    }
}

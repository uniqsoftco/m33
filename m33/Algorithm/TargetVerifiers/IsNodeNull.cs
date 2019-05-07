using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using m33.Algorithm.Interfaces;
using m33.Types;

namespace m33.Algorithm.TargetVerifiers
{
    public class IsNodeNull : IM33TargetVerifier
    {
        public bool IsTarget(M33Node node)
        {
            return node == null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using m33.Types;

namespace m33
{
    public static class Const
    {
        public static readonly int[] WrongBlankSpaces = new[] { 0 };
        public static readonly int[,] Target = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

        public static readonly Dictionary<int, Point> ManhatanTarget = new Dictionary<int, Point>()
        {
            {1,new Point(0,0)},
            {2,new Point(0,1)},
            {3,new Point(0,2)},
            {4,new Point(1,0)},
            {5,new Point(1,1)},
            {6,new Point(1,2)},
            {7,new Point(2,0)},
            {8,new Point(2,1)},
            {9,new Point(2,2)}
        };
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using m33.Algorithm.Algorithms;
using m33.Algorithm.Interfaces;
using m33.Types;

namespace m33
{
    public class Program
    {
        public static int[,] MotherMatrix = (int[,])Const.Target.Clone();
        public static readonly AStar Algorithm = new AStar();
        //public static readonly HillClimbing Algorithm = new HillClimbing();

        static void Main(string[] args)
        {
            Helper.Args = args;
            Helper.ParseArguments();
            Helper.CorrectBlankSpace(MotherMatrix);
            Algorithm.Go(MotherMatrix);
            Helper.ExitProgram();
        }

    }
}

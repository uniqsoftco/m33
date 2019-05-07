using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using m33.Algorithm.Algorithms;
using m33.Algorithm.Interfaces;
using m33.NodeSelector;
using m33.Types;

namespace m33
{
    public static class Helper
    {
        public static string[] Args { private get; set; }
        public static bool MatrixEq(int[,] m1, int[,] m2)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    //Console.WriteLine($"{m1[i, j]}-{m2[i, j]}-{m1[i, j] == m2[i, j]}");
                    if (m1[i, j] != m2[i, j])
                        return false;
                }
            }

            return true;
        }

        public static int[,] RandMatrix(int d)
        {
            int?[] rand = new int?[d * d];
            Random random = new Random();
            int tmpRandomNumber = 0;
            int[,] randomMatrix = new int[d, d];

            for (int i = 0; i < (d * d); i++)
            {
                rand[i] = null;
                while (true)
                {
                    tmpRandomNumber = random.Next(1, (d * d) + 1)/* - 1*/;
                    if ((tmpRandomNumber < (d * d) + 1) && !rand.Contains(tmpRandomNumber))
                    {
                        rand[i] = tmpRandomNumber;
                        break;
                    }
                }
            }

            for (int i = 0; i < d; i++)
            {
                for (int j = 0; j < d; j++)
                {
                    randomMatrix[i, j] = rand[(i * d) + j] ?? 0;
                }
            }

            return randomMatrix;
        }

        public static Point FindBlankSpace(int[,] matrix)
        {
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (matrix[x, y] == Config.BlankSpace)
                    {
                        return new Point(x, y);
                    }
                }
            }

            return null;
        }

        public static void CorrectBlankSpace(int[,] matrix)
        {
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    foreach (int n in Const.WrongBlankSpaces)
                    {
                        if (matrix[x, y] == n)
                        {
                            matrix[x, y] = Config.BlankSpace;
                            return;
                        }
                    }
                }
            }
        }

        public static void WaitForKey()
        {
            if (!Args.Contains("-q"))
                Console.ReadLine();
        }

        public static void ExitProgram(string msg = null, int exitCode = 0)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                Debug.WriteDebugMsgLine(msg);
            }

            WaitForKey();
            Environment.Exit(exitCode);
        }

        public static void ParseArguments()
        {
            string stringMatrix = string.Empty;

            if (Args.Contains("-d"))
                Debug.EnableDebug = true;

            if (Args.Contains("-fm"))
            {
                Debug.WriteDebugMsgLine("Loading Matrix from file...");
                for (int i = 0; i < Args.Length; i++)
                {
                    if (Args[i] == "-fm")
                    {
                        stringMatrix = File.ReadAllText(Args[i + 1]).Trim();
                        Debug.WriteDebugMsgLine("Matrix file loaded.");
                        break;
                    }
                }
            }
            else if (Args.Contains("-tm"))
            {
                Program.MotherMatrix = M33Test.TestMatrix;
                Debug.WriteDebugMsgLine("Using predefined test matrix.");
            }
            else if (Args.Contains("-rm"))
            {
                Program.MotherMatrix = RandMatrix(3);
                Debug.WriteDebugMsgLine("Using randomly generated matrix.");
            }
            else
            {
                Console.Write("Enter Matrix eg. 1,2,3;4,5,6;7,8,9 :");
                stringMatrix = Console.ReadLine().Trim();
            }

            if (stringMatrix != string.Empty)
            {
                string[] rows = stringMatrix.Split(';');

                for (int x = 0; x < 3; x++)
                {
                    string[] col = rows[x].Split(',');
                    for (int y = 0; y < 3; y++)
                    {
                        int o;
                        if (int.TryParse(col[y], out o))
                        {
                            Program.MotherMatrix[x, y] = o;
                        }
                        else
                        {
                            ExitProgram("Invalid input!!!", 1);
                        }
                    }
                }
            }
        }
        public static M33Node GenUpNode(M33Node parent)
        {
            if (!parent.Path.EndsWith("D") && parent.BlankSpaceCoord.X > 0 && parent.Depth <= Config.MaxDepth)
            {
                int[,] upMatrix = (int[,])parent.Matrix.Clone();
                Point upBlankSpaceCoord = new Point(parent.BlankSpaceCoord.X - 1, parent.BlankSpaceCoord.Y);
                upMatrix[parent.BlankSpaceCoord.X, parent.BlankSpaceCoord.Y] = upMatrix[upBlankSpaceCoord.X, upBlankSpaceCoord.Y];
                upMatrix[upBlankSpaceCoord.X, upBlankSpaceCoord.Y] = Config.BlankSpace;

                return new M33Node(upMatrix, upBlankSpaceCoord, Program.Algorithm.CalculateHeuristic(upMatrix), parent.Depth + 1, parent.Path + "U");
            }
            else
            {
                return null;
            }
        }
        public static M33Node GenDownNode(M33Node parent)
        {
            if (!parent.Path.EndsWith("U") && parent.BlankSpaceCoord.X < 2 && parent.Depth <= Config.MaxDepth)
            {
                int[,] downMatrix = (int[,])parent.Matrix.Clone();
                Point downBlankSpaceCoord = new Point(parent.BlankSpaceCoord.X + 1, parent.BlankSpaceCoord.Y);
                downMatrix[parent.BlankSpaceCoord.X, parent.BlankSpaceCoord.Y] = downMatrix[downBlankSpaceCoord.X, downBlankSpaceCoord.Y];
                downMatrix[downBlankSpaceCoord.X, downBlankSpaceCoord.Y] = Config.BlankSpace;

                return new M33Node(downMatrix, downBlankSpaceCoord, Program.Algorithm.CalculateHeuristic(downMatrix), parent.Depth + 1, parent.Path + "D");
            }
            else
            {
                return null;
            }
        }
        public static M33Node GenLeftNode(M33Node parent)
        {
            if (!parent.Path.EndsWith("R") && parent.BlankSpaceCoord.Y > 0 && parent.Depth <= Config.MaxDepth)
            {
                int[,] leftMatrix = (int[,])parent.Matrix.Clone();
                Point leftBlankSpaceCoord = new Point(parent.BlankSpaceCoord.X, parent.BlankSpaceCoord.Y - 1);
                leftMatrix[parent.BlankSpaceCoord.X, parent.BlankSpaceCoord.Y] = leftMatrix[leftBlankSpaceCoord.X, leftBlankSpaceCoord.Y];
                leftMatrix[leftBlankSpaceCoord.X, leftBlankSpaceCoord.Y] = Config.BlankSpace;

                return new M33Node(leftMatrix, leftBlankSpaceCoord, Program.Algorithm.CalculateHeuristic(leftMatrix), parent.Depth + 1, parent.Path + "L");
            }
            else
            {
                return null;
            }
        }
        public static M33Node GenRightNode(M33Node parent)
        {
            if (!parent.Path.EndsWith("L") && parent.BlankSpaceCoord.Y < 2 && parent.Depth <= Config.MaxDepth)
            {
                int[,] rightMatrix = (int[,])parent.Matrix.Clone();
                Point rightBlankSpaceCoord = new Point(parent.BlankSpaceCoord.X, parent.BlankSpaceCoord.Y + 1);
                rightMatrix[parent.BlankSpaceCoord.X, parent.BlankSpaceCoord.Y] = rightMatrix[rightBlankSpaceCoord.X, rightBlankSpaceCoord.Y];
                rightMatrix[rightBlankSpaceCoord.X, rightBlankSpaceCoord.Y] = Config.BlankSpace;

                return new M33Node(rightMatrix, rightBlankSpaceCoord, Program.Algorithm.CalculateHeuristic(rightMatrix), parent.Depth + 1, parent.Path + "R");
            }
            else
            {
                return null;
            }
        }
    }
}

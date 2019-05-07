using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using m33.Types;

namespace m33
{
    public class Debug
    {
        public static bool EnableDebug = false;

        private static long
            kb = 1024,
            mb = kb * kb,
            gb = kb * kb * kb;

        private List<PrintDetailsEnum> GetMatricesInPathMotherNodePrintDetails { get; }
        private List<PrintDetailsEnum> GetMatricesInPathPrintDetails { get; }
        private List<PrintDetailsEnum> PrintDebugPrintDetails { get; }
        private List<PrintDetailsEnum> PrintResultPrintDetails { get; }
        private List<M33Node> Queue;
        public enum PrintDetailsEnum
        {
            Title,
            Matrix,
            Depth,
            MaxSeenDepth,
            Heuristic,
            SearchedNodesCount,
            QueueCount,
            MemoryUsage,
            MaxUsedMemory,
            Time,
            Path,
            MatricesFromPath,
            EndMessageLine
        }

        public Debug(
            List<PrintDetailsEnum> getMatricesInPathMotherNodePrintDetails,
            List<PrintDetailsEnum> getMatricesInPathPrintDetails,
            List<PrintDetailsEnum> printDebugPrintDetails,
            List<PrintDetailsEnum> printResultPrintDetails,
            List<M33Node> queue)
        {
            GetMatricesInPathMotherNodePrintDetails = getMatricesInPathMotherNodePrintDetails;
            GetMatricesInPathPrintDetails = getMatricesInPathPrintDetails;
            PrintDebugPrintDetails = printDebugPrintDetails;
            PrintResultPrintDetails = printResultPrintDetails;
            this.Queue = queue;
        }
        public static void MatrixPrint(M33Node n)
        {
            Console.WriteLine(n.ToString());
        }

        public static long GetTotalMemory()
        {
            return GC.GetTotalMemory(false);
        }

        public string ConvertToStringWithUnit(long? mem)
        {
            if (mem >= gb)
            {
                return $"{mem / gb} GB";
            }

            if (mem >= mb)
            {
                return $"{mem / mb} MB";
            }

            return $"{mem / kb} KB";
        }
        public string CreateDebugString(
            List<PrintDetailsEnum> printDetails,
            M33Node detailsNode,
            string title,
            M33Node printNode = null)
        {
            string print = "";

            if (detailsNode == null && printNode == null)
                goto ret;

            if (printDetails.Contains(PrintDetailsEnum.Title))
            {
                print += $"{title}:\r\n";
                //printDetails.Remove(PrintDetailsEnum.Title);
            }

            if (printDetails.Contains(PrintDetailsEnum.Matrix))
            {
                if (printNode == null)
                    print += detailsNode.ToString();
                else
                    print += printNode.ToString();
                //printDetails.Remove(PrintDetailsEnum.Matrix);
            }

            if (printDetails.Contains(PrintDetailsEnum.Depth))
            {
                print += $"Depth: {detailsNode.Depth}\r\n";
                //printDetails.Remove(PrintDetailsEnum.Depth);
            }

            if (printDetails.Contains(PrintDetailsEnum.MaxSeenDepth))
            {
                print += $"Max Seen Depth: {Stats.MaxSeenDepth}\r\n";
                //printDetails.Remove(PrintDetailsEnum.MaxSeenDepth);
            }

            if (printDetails.Contains(PrintDetailsEnum.Heuristic))
            {
                print += $"Heuristic: {detailsNode.Heuristic}\r\n";
                //printDetails.Remove(PrintDetailsEnum.Heuristic);
            }

            if (printDetails.Contains(PrintDetailsEnum.QueueCount))
            {
                print += $"Queue Count: {Queue.Count}\r\n";
                //printDetails.Remove(PrintDetailsEnum.QueueCount);
            }

            if (printDetails.Contains(PrintDetailsEnum.SearchedNodesCount))
            {
                print += $"Searched Nodes Count: {Stats.SearchedNodesCount}\r\n";
                //printDetails.Remove(PrintDetailsEnum.SearchedNodesCount);
            }

            if (printDetails.Contains(PrintDetailsEnum.MemoryUsage))
            {
                print += $"Memory Usage: {ConvertToStringWithUnit(GetTotalMemory())}\r\n";
                //printDetails.Remove(PrintDetailsEnum.MemoryUsage);
            }

            if (printDetails.Contains(PrintDetailsEnum.MaxUsedMemory))
            {
                print += $"Max Used Memory: {ConvertToStringWithUnit(Stats.MaxUsedMemory)}\r\n";
                //printDetails.Remove(PrintDetailsEnum.MaxUsedMemory);
            }

            if (printDetails.Contains(PrintDetailsEnum.Time))
            {
                print += $"Time: {Stats.TotalTime.ToString("00.0")}s\r\n";
                //printDetails.Remove(PrintDetailsEnum.Time);
            }

            if (printDetails.Contains(PrintDetailsEnum.Path))
            {
                print += $"Path: {detailsNode.Path}\r\n";
                //printDetails.Remove(PrintDetailsEnum.Path);
            }

            if (printDetails.Contains(PrintDetailsEnum.MatricesFromPath) && printNode != null)
            {
                print += "\r\n" + GetMatricesInPath(printNode, detailsNode.Path);
                //printDetails.Remove(PrintDetailsEnum.MatricesFromPath);
            }

            if (printDetails.Contains(PrintDetailsEnum.EndMessageLine))
            {
                print += "==========================\r\n";
                //printDetails.Remove(PrintDetailsEnum.EndMessageLine);
            }

        ret:;
            return print;
        }
        public string GetMatricesInPath(M33Node motherNode, string path)
        {
            List<M33Node> pathList = new List<M33Node>() { motherNode };

            string print = string.Empty;

            print += CreateDebugString(
                         GetMatricesInPathMotherNodePrintDetails, motherNode, "Mother Node") + "\r\n";

            foreach (char direction in path)
            {
                switch (direction)
                {
                    case 'U':
                        pathList.Add(Helper.GenUpNode(pathList.LastOrDefault()));
                        break;
                    case 'D':
                        pathList.Add(Helper.GenDownNode(pathList.LastOrDefault()));
                        break;
                    case 'L':
                        pathList.Add(Helper.GenLeftNode(pathList.LastOrDefault()));
                        break;
                    case 'R':
                        pathList.Add(Helper.GenRightNode(pathList.LastOrDefault()));
                        break;
                }

                print += CreateDebugString(
                             GetMatricesInPathPrintDetails, pathList.LastOrDefault(), direction.ToString()) + "\r\n";
            }

            return print;
        }
        public void PrintDebug(M33Node node, string title)
        {
            if (!EnableDebug)
                return;

            string print = CreateDebugString(
                PrintDebugPrintDetails,
                node, title);

            Console.WriteLine(print);
        }


        public void PrintResult(M33Node foundNode, string title, M33Node motherNode)
        {

            Console.WriteLine(
                CreateDebugString(
                    PrintResultPrintDetails, foundNode, "Solved", motherNode)
            );
        }

        public static void WriteDebugMsgLine(string msg)
        {
            WriteDebugMsg(msg + "\r\n");
        }
        public static void WriteDebugMsg(string msg)
        {
            if (EnableDebug)
            {
                Console.WriteLine(msg);
            }
        }
    }
}

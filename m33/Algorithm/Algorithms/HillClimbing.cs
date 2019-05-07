using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using m33.Algorithm.AfterSelection;
using m33.Algorithm.Heuristics;
using m33.Algorithm.Interfaces;
using m33.Algorithm.NodeSelectionModes;
using m33.Algorithm.TargetVerifiers;
using m33.NodeSelector;
using m33.Types;

namespace m33.Algorithm.Algorithms
{
    public class HillClimbing
    {
        private readonly IM33Heuristic _heuristic = new MultipleHeuristic();
        private readonly IM33BestNodeSelector _nodeSelector = new LowerHeuristic();
        private readonly IM33TargetVerifier _targetVerifier = new IsNodeNull();
        private readonly IM33DoAfterSelection _doAfterSelection = new ClearQueue();
        private readonly Debug _debug;
        private const string AlgorithmTitle = "Hill Climbing";

        public List<Debug.PrintDetailsEnum> GetMatricesInPathMotherNodePrintDetails =
            new List<Debug.PrintDetailsEnum>()
            {
                Debug.PrintDetailsEnum.Title,
                Debug.PrintDetailsEnum.Matrix,
                Debug.PrintDetailsEnum.Heuristic
            };

        public List<Debug.PrintDetailsEnum> GetMatricesInPathPrintDetails =
            new List<Debug.PrintDetailsEnum>()
            {
                Debug.PrintDetailsEnum.Title,
                Debug.PrintDetailsEnum.Matrix,
                Debug.PrintDetailsEnum.Heuristic,
                Debug.PrintDetailsEnum.Path
            };

        public List<Debug.PrintDetailsEnum> PrintDebugPrintDetails =
            new List<Debug.PrintDetailsEnum>()
            {
                Debug.PrintDetailsEnum.Title,
                Debug.PrintDetailsEnum.Matrix,
                Debug.PrintDetailsEnum.Depth,
                Debug.PrintDetailsEnum.Heuristic,
                Debug.PrintDetailsEnum.SearchedNodesCount,
                Debug.PrintDetailsEnum.MemoryUsage,
                Debug.PrintDetailsEnum.MaxUsedMemory,
                Debug.PrintDetailsEnum.Time,
                Debug.PrintDetailsEnum.EndMessageLine
            };

        public List<Debug.PrintDetailsEnum> PrintResultPrintDetails =
            new List<Debug.PrintDetailsEnum>()
            {
                Debug.PrintDetailsEnum.Title,
                Debug.PrintDetailsEnum.Matrix,
                Debug.PrintDetailsEnum.Depth,
                Debug.PrintDetailsEnum.SearchedNodesCount,
                Debug.PrintDetailsEnum.MaxUsedMemory,
                Debug.PrintDetailsEnum.Time,
                Debug.PrintDetailsEnum.Path,
                Debug.PrintDetailsEnum.MatricesFromPath,
                Debug.PrintDetailsEnum.EndMessageLine
            };
        private readonly List<M33Node> _queue = new List<M33Node>();

        public HillClimbing()
        {
            _debug = new Debug(
                GetMatricesInPathMotherNodePrintDetails,
                GetMatricesInPathPrintDetails,
                PrintDebugPrintDetails,
                PrintResultPrintDetails,
                _queue);
        }
        public M33Node SelectBestNode(M33Node prevNode)
        {
            M33Node selectedNode = null;

            if (prevNode == null)
            {
                return _queue[0];
            }

            for (int i = 0; i < _queue.Count; i++)
            {
                if (_queue[i].Heuristic <= prevNode.Heuristic)
                {
                    selectedNode = _queue[i];
                }
            }

            return selectedNode;
        }

        public void DoAfterSelection(List<M33Node> queue, M33Node node = null)
        {
            _doAfterSelection.DoAfterSelection(queue, null);
        }

        public bool IsTarget(M33Node node)
        {
            return _targetVerifier.IsTarget(node);
        }

        public int CalculateHeuristic(int[,] matrix)
        {
            return _heuristic.CalcHeuristic(matrix);
        }

        public void Go(int[,] matrix)
        {
            M33Node motherNode = new M33Node(matrix, CalculateHeuristic(matrix));

            Console.WriteLine("Matrix:");
            Debug.MatrixPrint(motherNode);
            Console.WriteLine($"========={AlgorithmTitle}=========");

            _queue.Add(motherNode);

            M33Node selectedNode = null, tmpChildNode, previousNode;
            while (true)
            {
                previousNode = selectedNode;
                selectedNode = SelectBestNode(previousNode);
                _queue.Clear();

                if (IsTarget(selectedNode))
                {
                    Stats.Timer.Enabled = false;
                    _debug.PrintResult(previousNode, "Solved", motherNode);
                    break;
                }

                _debug.PrintDebug(selectedNode, "Selected");
                Stats.UpdateStats(selectedNode);

                tmpChildNode = Helper.GenUpNode(selectedNode);
                if (tmpChildNode != null)
                {
                    _debug.PrintDebug(tmpChildNode, "Up");
                    _queue.Add(tmpChildNode);
                }

                tmpChildNode = Helper.GenDownNode(selectedNode);
                if (tmpChildNode != null)
                {
                    _debug.PrintDebug(tmpChildNode, "Down");
                    _queue.Add(tmpChildNode);
                }

                tmpChildNode = Helper.GenLeftNode(selectedNode);
                if (tmpChildNode != null)
                {
                    _debug.PrintDebug(tmpChildNode, "Left");
                    _queue.Add(tmpChildNode);
                }

                tmpChildNode = Helper.GenRightNode(selectedNode);
                if (tmpChildNode != null)
                {
                    _debug.PrintDebug(tmpChildNode, "Right");
                    _queue.Add(tmpChildNode);
                }
            }
        }
    }
}

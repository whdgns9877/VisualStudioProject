using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarAlgorithm.Net6
{
    public class ANode
    {
        public bool ISBlock { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        // G score = Costs from the starting point to the current node
        public int G { get; set; }

        // H Score = Estimated cost from current node to destination (Heuristic)
        public int H { get; set; }

        // F Score = G Score + H Score
        public int F { get { return G + H; } }

        public ANode(int x, int y)
        {
            X = x;
            Y = y;
        }

        // ParentNode =  When coming to the current node, the node that has just passed through
        public ANode? parentNode = null;
    }

    internal class AstarMain
    {
        // Func Algorithm Run
        public void Run()
        {
            bool[,] cells =
{
                {false, false, false, false, false, false },
                {false, false, false, false, false, false },
                {false, false, false, false, false, false },
                {false, false, false, false, false, false },
            };

            // Make Map Data
            for (int y = 0; y < map_height; y++)
                for (int x = 0; x < map_width; x++)
                    myMaps[y, x] = new ANode(x,y) {ISBlock = cells[y, x] };

            // FindPath
            FindPath(2, 4, 1, 1);

            if(findedList.Count > 0)
            {
                foreach(ANode node in findedList)
                {
                    Console.WriteLine($" [{node.Y}, {node.X}] ");
                }
            }
        }

        // Map Data
        const int map_height = 4;
        const int map_width = 6;
        ANode[,] myMaps = new ANode[map_height, map_width];

        // OpenList = List of will be visit
        List<ANode> openList = new List<ANode>();
        // ClosedList = List of already visited
        List<ANode> closedList = new List<ANode>();

        LinkedList<ANode> findedList = new LinkedList<ANode>();

        // StartPos = sy, sx
        // EndPos = ey, ex
        void FindPath(int sy, int sx, int ey, int ex)
        {
            int gScore = 0;

            ANode? curNode = null;
            ANode startNode = myMaps[sy, sx];
            ANode endNode = myMaps[ey, ex];
            ANode? targetNode = null;
            // Add StartPos to OpenList
            openList.Add(startNode);

            // Start Loop until there is nowhere to go(meaning there are no nodes in the OpenList)
            while (openList.Count > 0)
            {
                // Get One Node from OpenList
                // It means that the node with the lowest F score(lowest cost) is selected from among the nodes that can go forward.
                curNode = GetNodeFirstNodeInList();
                //Record that the node retrieved above was visited(meaning adding it to the ClosedList)
                closedList.Add(curNode);

                // Determines whether the node pulled out above is the destination, and if it is, the loop ends immediately.
                //if (curNode == endNode)
                if (curNode.X == ex && curNode.Y == ey)
                {
                    // Denote the finded path
                    // Through the loop, the node is recorded while moving along the parent node of the destination to the starting point.
                    do
                    {
                        findedList.AddFirst(curNode);
                    }
                    while ((curNode = curNode.parentNode) != null);
                    
                    break;
                }

                ///////// Begin loop again = Repeat the following routine to neighboring nodes centering on the node taken out above.
                for (int dy = -1; dy <= 1; dy++)
                {
                    for (int dx = -1; dx <= 1; dx++)
                    {
                        int lx = curNode.X + dx;
                        int ly = curNode.Y + dy;

                        // Checks whether it is within the range of the array,
                        // and if it is out of range, Continue to a node in the next week.
                        if (lx < 0 || lx >= map_width || ly < 0 || ly >= map_height) continue;

                        targetNode = myMaps[ly, lx];

                        // Check whether the node is reachable(check obstacles: Check IsBlock of Node)
                        if (targetNode.ISBlock) continue;

                        // Check if it has already been visited(check in ClosedList)
                        if (closedList.Contains(targetNode)) continue;


                        // Calculate G
                        if (Math.Abs(lx) + Math.Abs(ly) == 2)
                            gScore = curNode.G + 14;
                        else
                            gScore = curNode.G + 10;

                        ///////// Check if surrounding nodes are added to OpenList
                        // If not in OpenList
                        // The G score is calculated by including the G Score of the node that is the parent of the current node.
                        // H Score records the distance from the destination(you can count the number of cells in the array)
                        if (!openList.Contains(targetNode))
                        {
                            targetNode.G = gScore;
                            targetNode.H = Math.Abs(ey - targetNode.Y) + Math.Abs(ex - targetNode.X);

                            // Record the parent of the neighboring node as the current node
                            targetNode.parentNode = curNode;
                            // Add neighboring node to OpenList
                            openList.Add(targetNode);
                        }
                        else
                        {
                            // If in OpenList
                            // Calculate G Score based on the current node, compare the existing G Score,
                            // and calculate a new G Score Update if the G score is small.
                            // Here, if updated, the parent is also changed to the current node.
                            if(targetNode.G > gScore)
                            {
                                targetNode.G = gScore;
                                targetNode.parentNode = curNode;
                            }
                        }
                    }
                }
            }
        }

        ANode GetNodeFirstNodeInList()
        {
            ANode? tartget = null;
            // openList를 F score 기준으로 정렬(오름차순) - 이렇게 하면 openList의 0 번째 순위가 제일 높은것
            openList.Sort((a, b) => a.F.CompareTo(b.F));
            tartget = openList[0]; // 우선순위 제일 높은것을 전달해 주기 위해서 참조변수로

            openList.RemoveAt(0); // 맨 앞에 것을 꺼내는 효과를 위해
            return tartget;
        }

        void PrintPath()
        {

        }
    }
}

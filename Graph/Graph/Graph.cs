using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graph
{
    internal class Graph
    {
        // List For nodeList in Graph
        private List<GNode> nodeList;

        // onstructor that runs when a Graph instance is created
        public Graph()
        {
            nodeList = new List<GNode>();
        }

        // add Node in graph
        public GNode AddNode(string name, bool isVisited)
        {
            GNode n = new GNode(name, isVisited);
            nodeList.Add(n);
            return n;
        }

        // make edge between two node
        public void AddEdge(GNode from, GNode to, bool oneway)
        {
            from.AdjacentNode.Add(to);
            if (oneway == false)
            {
                to.AdjacentNode.Add(from);
            }
        }

        // DFS Using Stack
        public void DFSList(GNode start)
        {
            Console.WriteLine("DFS탐색 시작~");
            Stack<GNode> stack = new Stack<GNode>();
            stack.Push(start);
            start.IsVisited = true;
            GNode curNode = null;

            while (stack.Count > 0)
            {
                curNode = stack.Pop();
                // Based on the current node, among neighboring nodes
                // unvisited nodes are put on the stack
                for (int i = 0; i < curNode.AdjacentNode.Count; i++)
                {
                    GNode adjacentNode = curNode.AdjacentNode[i];
                    if (adjacentNode.IsVisited == false)
                    {
                        adjacentNode.IsVisited = true;
                        stack.Push(adjacentNode);
                    }
                }
                Console.WriteLine(curNode.Name);
            }
        }

        // BFS Using Queue
        public void BFSList(GNode start)
        {
            Console.WriteLine("BFS탐색 시작~");

            List<GNode> visitList = new List<GNode>();
            Queue<GNode> queue = new Queue<GNode>();
            queue.Enqueue(start);
            start.IsVisited = true;
            GNode curNode = start;

            while (queue.Count > 0)
            {
                curNode = queue.Dequeue();

                // First, if the adjacent nodes are before visiting
                // they are put in the queue in order.
                for (int i = 0; i < curNode.AdjacentNode.Count; i++)
                {
                    GNode adjacentNode = curNode.AdjacentNode[i];
                    if (adjacentNode.IsVisited == false)
                    {
                        adjacentNode.IsVisited = true;
                        queue.Enqueue(adjacentNode);
                    }
                }

                visitList.Add(curNode);
            }

            for(int i = 0; i < visitList.Count; i++)
            {
                Console.WriteLine(visitList[i].Name);
            }
        }
    }
}



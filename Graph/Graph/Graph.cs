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
        private List<GNode> nodeList;

        public Graph()
        {
            nodeList = new List<GNode>();
        }

        public GNode AddNode(string name, bool isVisited)
        {
            GNode n = new GNode(name, isVisited);
            nodeList.Add(n);
            return n;
        }

        public void AddEdge(GNode from, GNode to, bool oneway)
        {
            from.Neighbors.Add(to);
            if (oneway == false)
            {
                to.Neighbors.Add(from);
            }
        }

        // 스택을 이용한 DFS
        public void DFSList(GNode start)
        {
            Console.WriteLine("DFS탐색 시작~");
            Stack<GNode> stack = new Stack<GNode>();
            stack.Push(start);
            start.IsVisited = true;

            while (stack.Count > 0)
            {
                GNode curNode = stack.Pop();
                for (int i = 0; i < curNode.Neighbors.Count; i++)
                {
                    GNode adjacentNode = curNode.Neighbors[i];
                    if (adjacentNode.IsVisited == false)
                    {
                        adjacentNode.IsVisited = true;
                        stack.Push(adjacentNode);
                    }
                }
                Console.WriteLine(curNode.Name);
            }
        }

        // 큐를 이용한 BFS
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
              
                // 우선 인접 노드들이 방문전이면 큐에 순서대로 넣어준다
                for (int i = 0; i < curNode.Neighbors.Count; i++)
                {
                    GNode adjacentNode = curNode.Neighbors[i];
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



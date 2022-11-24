using System;
using System.Collections.Generic;

namespace GraphTest
{
    class NodeData
    {
        public NodeData(int inNum)
        {
            Num = inNum;
        }

        public int Num;
        public List<int> Neighbors = new List<int>(); // 주변 노드
    }

    internal class Test
    {
        List<NodeData> nodes = new List<NodeData>(); // 그래프 노드 목록

        public void Run()
        {
            string inputLine = Console.ReadLine();
            string[] tokens = inputLine.Split(' ');

            int N = int.Parse(tokens[0]); // 정점의 개수
            int M = int.Parse(tokens[1]); // 간선의 개수
            int V = int.Parse(tokens[2]); // 시작 정점

            // 정점을 생성
            for (int i = 1; i <= N; i++)
            {
                nodes.Add(new NodeData(i));
            }

            // 간선을 입력 (i는 Index)
            int a, b;
            for (int edgeNum = 0; edgeNum < M; edgeNum++)
            {
                inputLine = Console.ReadLine();
                tokens = inputLine.Split(' ');

                // 서로 연결된 정보 표시
                a = int.Parse(tokens[0]) - 1;
                b = int.Parse(tokens[1]) - 1;

                nodes[a].Neighbors.Add(b);
                nodes[b].Neighbors.Add(a);
            }

            // 주변 노드의 우선순위(노드데이터(숫자)가 낮은것) 기준으로 정렬을 먼저 한다.
            for (int i = 0; i < N; i++)
            {
                nodes[i].Neighbors.Sort();
            }

            // DFS 기준 탐색
            traversDFS(V);
            // BFS 기준 탐색
            traversBFS(V);
            Console.ReadLine();
        }

        void traversDFS(int startNode)
        {
            List<int> node4Visit = new List<int>(); // 방문기록용
            Stack<NodeData> node4Path = new Stack<NodeData>(); // 경로 추적용


            NodeData connectedNode = null; // 시작 노드
            NodeData targetNode = nodes[startNode - 1]; // 시작 노드

            node4Visit.Add(startNode - 1); // 방문기록
            Console.Write(targetNode.Num);

            // node와 연결되어 있는 다른 노드들을 탐색
            bool visited = false;
            while (targetNode != null)
            {
                visited = false;
                // targetNode와 연결되어 있는 노드들 탐색
                for (int nIdex = 0; nIdex < targetNode.Neighbors.Count; nIdex++)
                {
                    connectedNode = nodes[targetNode.Neighbors[nIdex]];
                    // 이미 방문한 곳인지 체크해서 방문하지 않은 노드에 대해서 탐색                        
                    if (node4Visit.Contains(targetNode.Neighbors[nIdex]) == false)
                    {
                        // 해당 노트 탐색
                        // 방문 기록                        
                        node4Visit.Add(targetNode.Neighbors[nIdex]);
                        Console.Write(" " + connectedNode.Num);

                        // 경로 추적용
                        node4Path.Push(targetNode);

                        // 다음 방문노드로 변경
                        targetNode = connectedNode;
                        visited = true;
                        break;
                    }
                    // 방문한 곳이면 다음 노드로 이동
                }

                // 자기 주변 노드 중에서 방문할 곳이 없다면 되돌아갈 노드를 찾는다.
                if (visited == false)
                {
                    targetNode = node4Path.Count > 0 ? node4Path.Pop() : null; // 되돌아갈 노드
                }
            }
        }

        void traversBFS(int startNode)
        {
            List<int> node4Visit = new List<int>(); // 방문 기록용
            Queue<NodeData> node4Path = new Queue<NodeData>();

            NodeData targetNode = nodes[startNode - 1]; // 시작 노드

            node4Visit.Add(startNode - 1); // 방문 기록
            Console.Write(targetNode.Num);
            node4Path.Enqueue(targetNode); // 탐색 시작노드를 넣기

            // 연결되어 있는 노드들을 먼저 탐색
            NodeData connectedNode = null;
            while (node4Path.Count > 0)
            {
                targetNode = node4Path.Dequeue();
                //Console.Write(targetNode.Num);

                // 자기 주변 노드를 탐색해 보기
                for (int nIdx = 0; nIdx < targetNode.Neighbors.Count; ++nIdx)
                {
                    // 주변 노드 1개
                    connectedNode = nodes[targetNode.Neighbors[nIdx]];

                    // 이미 방문한 곳인지 체크
                    //int ret = node4Visit.FindIndex(delegate(int data) { return data == targetNode.Neighbors[nIdx]; });
                    //int ret = node4Visit.FindIndex((data) => data == targetNode.Neighbors[nIdx]);
                    if (node4Visit.Contains(targetNode.Neighbors[nIdx]))
                        continue;

                    // 방문할 곳을 추가
                    node4Path.Enqueue(connectedNode);

                    // 방문 기록
                    node4Visit.Add(targetNode.Neighbors[nIdx]);
                    Console.Write(" " + nodes[targetNode.Neighbors[nIdx]].Num);
                }
            }
        }
    }
}


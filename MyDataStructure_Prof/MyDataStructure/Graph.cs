using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyDataStructure
{
	internal class Graph
	{
		LinkedList nodeList = new LinkedList();

		//// 탐색시 이용
		//QueueLL nodeQueue4Path = new QueueLL();     // 방문차례
		//StackLL nodeStack4Path = new StackLL();     // 경로추적
		LinkedList node4Visit = new LinkedList();	// 방문기록용

		public LNode AddNode(GraphNodeData newData)
		{
			return nodeList.InsertTail(newData);
		}

		//
		// oneway : true 방향그래프, false 비방향 그래프
		public void AddEdge(GraphNodeData from, GraphNodeData to, bool oneway = false)
		{
			from.Neighbors.InsertTail(to);
			// 비방향그래프는 양쪽 모두 추가
			if(oneway == false)
				to.Neighbors.InsertTail(from);

		}

		//
		// oneway : true 방향그래프, false 비방향 그래프
		public void AddEdge(LNode from, LNode to, bool oneway = false)
		{
			((GraphNodeData)from.data).Neighbors.InsertTail(to.data);
			// 비방향그래프는 양쪽 모두 추가
			if (oneway == false)
				((GraphNodeData)to.data).Neighbors.InsertTail(from.data);

		}

		// 그래프 정보 출력
		public void Print()
		{
			nodeList.PrintForwardAll();
		}

		/*
		//=============================================================
		//방법1
		//
		// DFS(Depth First Search :깊이 우선 탐색)
		public void TraversalDFS(DelegateTraversalLNode doSomething)
		{
			Console.WriteLine("DFS 탐색 시작 ~~~");
			node4Visit.Clear();     // 방문기록용
			nodeStack4Path.Clear();     // 경로추적

			LNode connectedNode = null; // 연결된 노드목록의 처음노드
			LNode targetNode = nodeList.GetHead(); // 그래프의 처음부터 시작
			// 해당 탐색 -..
			doSomething(targetNode);
			node4Visit.InsertTail(targetNode.data);   // 방문기록

			// node 와 연결되어 있는 노드들을 탐색
			bool isVisited = false;
			while (targetNode != null)
			{
				isVisited = false;
				connectedNode = ((GraphNodeData)targetNode.data).Neighbors.GetHead();
				while (connectedNode != null)
				{
					// 이미 방문한 곳이면 다음으로
					if (node4Visit.Search(connectedNode.data) == null)
					{
						// 해당 탐색 -..
						doSomething(connectedNode);
						node4Visit.InsertTail(connectedNode.data);   // 방문기록	

						nodeStack4Path.Push(targetNode.data);
						targetNode = connectedNode;
						isVisited = true;
						break;
					}
					// 다음 연결된 곳으로
					connectedNode = connectedNode.next;
				}
				if (isVisited == false) // targetNode랑 연결되어있는 노드 중에서 더 이상 방문할 곳이 없을 때
				{
					targetNode = nodeStack4Path.Pop();
				}
			} 
		}
		///*/
		
		////방법2
		////
		//// DFS(Depth First Search :깊이 우선 탐색)
		//public void TraversalDFS(DelegateTraversalLNode doSomething)
		//{
		//	Console.WriteLine("DFS 탐색 시작 ~~~");
		//	node4Visit.Clear();		// 방문기록용
		//	traversalDFS(nodeList.GetHead(), doSomething);
		//}

		//void traversalDFS(LNode node, DelegateTraversalLNode doSomething)
		//{
		//	// 해당 탐색 -..
		//	doSomething(node);
		//	node4Visit.InsertTail(node.data);	// 방문기록

		//	// node 와 연결되어 있는 노드들을 탐색
		//	LNode connectedNode = ((GraphNodeData)node.data).Neighbors.GetHead();
		//	while(connectedNode != null)
		//	{
		//		// 이미 방문한 곳이면 다음으로
		//		if (node4Visit.Search(connectedNode.data) != null)
		//			continue;

		//		// 탐색
		//		traversalDFS(connectedNode, doSomething);

		//		connectedNode = connectedNode.next;
		//	}
		//}
		////=============================================================
		////*/

		
	}

	public class GraphNodeData : INodeData
	{
		// 인접한 노드들
		public LinkedList Neighbors = new LinkedList();

		public virtual int CompareTo(INodeData inData)
		{
			return 0;
		}

		public virtual void Print()
		{
			//Console.WriteLine($"{NumData}");
			Neighbors.PrintForwardAll(delegate (LNode node)
			{
				//Console.WriteLine($"{NumData} - {((GraphNodeData_Int)node.data).NumData}");
			});
		}

		public virtual string OutputString()
		{
			return $"";
		}
	}
}

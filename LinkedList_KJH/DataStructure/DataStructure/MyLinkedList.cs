using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
	public struct NodeData
	{
		public int Num;
		public string Info;
	}

	public class Node
	{
		public NodeData nodeData; // 데이터

		public Node next; // 다음 요소 연결
	}
	internal class MyLinkedList
	{
		Node head = null;
		Node tail = null;
		Node cur = null;
		Node temp = null;

		int Count = 0;

		public void Add(NodeData nodeData)
		{
			// 1) 추가할 데이터를 담고 있는 노드를 새로 생성
			Node newNode = new Node();
			newNode.nodeData = nodeData;

			// 2) 연결리스트의 마지막(tail)에 추가
			// tail 이 null 이면 연결리스트에 데이터가 하나도 없다는 뜻이므로
			// head, tail 을 모두 데이터를 가리키게 한다.
			if (head == null)
				head = newNode;
			else
				tail.next = newNode;

			// tail 이 마지막 노드를 가리키도록 한다.
			// 여기서 새로 추가된 노드가 마지막 노드가 되는 것이다.
			tail = newNode;
			Count++;
		}

		// nodeData의 내용을 newData로 변경
		public void UpdateData(NodeData nodeData, NodeData newData)
		{
			if (head == null)
				return;

			// cur 를 처음 노드(Head)를 가리키게 하고
			cur = head;
            do
            {
				// 찾는 데이터가 맞는지 데이터를 비교한다
				if(cur.nodeData.Num == nodeData.Num)
                {
					cur.nodeData = newData;
					break;
                }
            }
			while(cur != null);
		}

		public bool Delete(NodeData nodeData)
		{
			// 삭제할 데이터가 없다면 종료
			if (head == null)
				return false;

			Node target = null;		// 삭제할 데이터를 담고 있는 노드
			Node prevNode = null;   // cur 노드의 이전 노드

			// 1) 삭제하고자 하는 데이터를 담고 있는 노드를 먼저 찾고
			
			// cur 를 처음 노드(head)를 가리키게 하고
			cur = head;
			do
			{
				// 찾는 데이터가 맞는지 데이터를 비교한다.
				if (cur.nodeData.Num == nodeData.Num)
				{
					target = cur; // 삭제할 노드로 저장

					//2)삭제할 노드의 이전 노드와 삭제할 노드의 다음 노드를 연결해 준다.

					// 삭제할 노드가 head/tail 노드라면
					if (cur == head || cur == tail)
					{
						// 데이터가 1개일 때
						if (head == tail)
							head = tail = null;
						else if (cur == head)
							head = head.next;
						// 삭제할 노드가 tail 노드라면
						else
						{
							tail = prevNode;
							tail.next = null;
						}
					}
					else
					{
						// 삭제할 노드의 이전노드와 다음노드를 연결해 준다.
						prevNode.next = cur.next;
					}
					return true;
				}

				prevNode = cur; // 이전 노드로 저장
				cur = cur.next;
				Count--;
			} while (cur != null);


			return false;
		}


		public void Print()
		{
			// 데이터가 없다면 출력하지 못한다.
			if (head == null)
				Console.WriteLine("데이터가 존재하지 않습니다.");
			else
			{
				// cur 를 처음 노드(head)를 가리키게 하고
				cur = head;

				// 다음 노드가 존재한다면 계속 반복
				do
				{
					Console.WriteLine("데이터 {0}", cur.nodeData.Info);
					cur = cur.next;
				} while (cur != null);
			}
		}



  //      public void Sort()
  //      {
  //          if (head == null)
  //              return;

  //          // cur 를 처음 노드(Head)를 가리키게 하고
  //          cur = head;

  //          do
  //          {
  //              if (Convert.ToInt32(cur.nodeData.Info.Split(' ')[1][0]) > Convert.ToInt32(cur.next.nodeData.Info.Split(' ')[1][0]))
  //              {
		//			Swap(cur.nodeData, cur.next.nodeData);
  //              }
  //              cur = cur.next;
  //          }
  //          while (cur.next != null);
  //      }

		//void Swap(NodeData curData, NodeData nextData)
  //      {
		//	temp = new Node();
		//	temp.nodeData = curData;
		//	curData = nextData;
		//	nextData = temp.nodeData;
		//	temp = null;
		//}
    }
}

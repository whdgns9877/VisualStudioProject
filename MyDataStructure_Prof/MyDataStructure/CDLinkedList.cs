using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyDataStructure
{
	//
	//
	// 원형 양방향 연결 리스트
	//
	internal class CDLinkedList
	{
		DNode head = null;
		DNode tail = null;

		// head 다음에 추가
		public DNode InsertHead(INodeData newData)
		{
			// 새 데이터를 담는 새 노드
			DNode newNode = new DNode();
			newNode.data = newData;

			// 데이터 첫 추가
			if(head == null)
			{
				newNode.prev = newNode; // 원형
				newNode.next = newNode; // 원형
				head = tail = newNode;
			}
			else
			{
				// 새 노드의 연결관계
				newNode.prev = head.prev; // 원형
				newNode.next = head;
				// 새 노드가 추가되었으니 head는 새 노드를 가리켜야 한다.
				head.prev = newNode;
				head = newNode;
				tail.next = newNode;
			}

			return newNode;
		}

		// tail 앞에 추가
		public DNode InsertTail(INodeData newData)
		{
			// 새 데이터를 담는 새 노드
			DNode newNode = new DNode();
			newNode.data = newData;

			// 데이터 첫 추가
			if (tail == null)
			{
				newNode.prev = newNode;
				newNode.next = newNode;
				head = tail = newNode;
			}
			else
			{
				// 새 노드의 연결관계
				newNode.prev = tail;
				newNode.next = tail.next;
				// 새 노드가 추가되었으니 tail은 새 노드를 가리켜야 한다.
				tail.next = newNode;
				tail = newNode;
				head.prev = newNode;
			}
				

			return newNode;
		}

		// 특정 데이터 뒤에 추가
		public DNode InsertAfter(INodeData targetData, INodeData newData)
		{
			DNode targetNode = Search(targetData);
			if (targetNode != null)
				return InsertAfter(targetNode, newData);

			return null;
		}

		// 특정 노드 뒤에 추가
		public DNode InsertAfter(DNode targetNode, INodeData newData)
		{
			// 새 데이터를 담는 새 노드
			DNode newNode = new DNode();
			newNode.data = newData;

			newNode.next = targetNode.next;
			targetNode.next = newNode;
			newNode.prev = targetNode;

			return newNode;
		}

		// 특정 데이터  앞에 추가
		public DNode InsertBefore(INodeData targetData, INodeData newData)
		{
			DNode targetNode = Search(targetData);
			if (targetNode != null)
				return InsertBefore(targetNode, newData);

			return null;
		}

		// 특정 노드 앞에 추가
		public DNode InsertBefore(DNode targetNode, INodeData newData)
		{
			// 새 데이터를 담는 새 노드
			DNode newNode = new DNode();
			newNode.data = newData;

			newNode.prev = targetNode.prev;
			targetNode.prev = newNode;
			newNode.next = targetNode;

			return newNode;
		}

		// head 다음 노드 삭제
		public void DeleteHead()
		{
			// 아무것도 없을 때
			if (head == null) return;

			if(head == tail)
			{
				head = tail = null;
			}
			else
			{
				head.next.prev = tail; // 마지막노드의 이전 노드를 Head 다음으로 (원형)
				tail.next = head.next;
				head = head.next;
			}
		}

		// tail 앞에 제거
		public void DeleteTail()
		{
			// 아무것도 없을 때
			if (tail == null) return;

			// 1개
			if (head == tail)
			{
				head = tail = null;
			}
			else
			{
				tail.prev.next = head; // 맨마지막의 전의 것과 head 연결
				head.prev = tail.prev;
				tail = tail.prev;
			}
		}

		// 특정 데이터 삭제
		public void DeleteNode(INodeData delData)
		{
			DNode targetNode = Search(delData);
			if (targetNode != null)
			{
				targetNode.prev.next = targetNode.next;
				targetNode.next.prev = targetNode.prev;
			}
		}

		// 데이터를 기준으로 노드를 찾기
		public DNode Search(INodeData nodeData)
		{
			DNode tmp = head;		
			do
			{
				if(tmp.data.CompareTo(nodeData) == 0)
					return tmp;

				tmp = tmp.next;
			} while (tmp != head);

			return null;
		}


		// 데이터를 순방향으로 탐색하기
		public void TraversalForward(DelegateTraversalDNode doSomething)
		{
			DNode tmp = head.next;
			while (tmp != tail)
			{
				doSomething(tmp);

				tmp = tmp.next;
			}
		}

		public void TraversalForward(DNode startNode, DelegateTraversalDNode doSomething)
		{
			DNode tmp = startNode.next;
			while (tmp != startNode)
			{
				doSomething(tmp);

				tmp = tmp.next;
			}
		}

		// 데이터를 지정된 노드 순방향으로 탐색하기
		public void TraversalForward(DNode startNode, DNode endNode, DelegateTraversalDNode doSomething)
		{
			DNode tmp = startNode;
			while (tmp != null)
			{
				doSomething(tmp);

				if (tmp == endNode)
					break;
				tmp = tmp.next;
			}
		}

		// 데이터를 역방향으로 탐색하기
		public void TraversalBackward(DelegateTraversalDNode doSomething)
		{
			DNode tmp = tail.prev;
			while (tmp != head)
			{
				doSomething(tmp);
				tmp = tmp.prev;
			}
		}

		// 데이터를 역방향으로 탐색하기
		public void TraversalBackward(DNode startNode, DelegateTraversalDNode doSomething)
		{
			DNode tmp = startNode.prev;
			while (tmp != startNode)
			{
				doSomething(tmp);
				tmp = tmp.prev;
			}
		}

		// 데이터를 지정된 노드 역방향으로 탐색하기
		public void TraversalBackward(DNode startNode, DNode endNode, DelegateTraversalDNode doSomething)
		{
			DNode tmp = startNode;
			while (tmp != null)
			{
				doSomething(tmp);
				if (tmp == endNode)
					break;
				tmp = tmp.prev;
			}
		}

		// 데이터를 순방향으로 출력하기
		public void PrintForward(DNode from, DNode to)
		{
			DNode temp = from;
			do
			{
				temp.data.Print();
			}
			while ((temp = temp.next) != tail);
		}

		// 데이터를 역방향으로 출력하기
		public void PrintBackward(DNode from, DNode to)
		{
			DNode temp = from;
			do
			{
				temp.data.Print();
			}
			while ((temp = temp.prev) != head);
		}

		// 데이터 모두 출력
		public void PrintForwardAll()
		{
			DNode tmp = head;
			while (tmp != tail)
			{
				tmp.data.Print();
				tmp = tmp.next;
			}
			tmp.data.Print(); // 맨마지막 것
		}

		// 데이터 모두 출력
		public void PrintBackwardAll()
		{
			DNode tmp = tail.prev;
			while (tmp != head)
			{
				tmp.data.Print();
				tmp = tmp.prev;
			}
			tmp.data.Print();// 맨처음 것
		}

	}
}

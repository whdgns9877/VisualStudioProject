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
	// 양방향 연결 리스트
	//
	internal class DLinkedList
	{
		DNode head = new DNode();
		DNode tail = new DNode();

		public DLinkedList()
		{
			head.next = tail;
			tail.prev = head;
		}

		// head 다음에 추가
		public DNode InsertHead(INodeData newData)
		{
			// 새 데이터를 담는 새 노드
			DNode newNode = new DNode();
			newNode.data = newData;

			// 새 노드의 연결관계
			newNode.next = head.next;
			newNode.prev = head;
			// 새 노드가 추가되었으니 head는 새 노드를 가리켜야 한다.
			head.next.prev = newNode;
			head.next = newNode;

			return newNode;
		}

		// tail 앞에 추가
		public DNode InsertTail(INodeData newData)
		{
			// 새 데이터를 담는 새 노드
			DNode newNode = new DNode();
			newNode.data = newData;

			// 새 노드의 연결관계
			newNode.prev = tail.prev;
			newNode.next = tail;
			// 새 노드가 추가되었으니 tail은 새 노드를 가리켜야 한다.
			tail.prev.next = newNode;
			tail.prev = newNode;

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
			if (head.next == tail) return;

			DNode delTarget = head.next; // 맨 앞에 있는 노드가 삭제대상
			head.next = delTarget.next;
			delTarget.next.prev = head; // 삭제할 노드의 다음노드의 이전노드가 head가 되도록

			// 참조관계를 모두 끊어주고 메모리 해제가 되기를...
			if(delTarget != null)
			{
				delTarget.prev = null;
				delTarget.next = null;
			}
		}

		// tail 앞에 제거
		public void DeleteTail()
		{
			// 아무것도 없을 때
			if (tail.prev == head) return;

			DNode delTarget = tail.prev; // 맨 뒤에 있는 노드가 삭제대상
			tail.prev = delTarget.prev;
			delTarget.prev.next = tail;// 삭제할 노드의 다음노드의 이전노드가 tail이 되도록

			// 참조관계를 모두 끊어주고 메모리 해제가 되기를...
			delTarget.prev = null;
			delTarget.next = null;
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
			DNode tmp = head.next;
			while (tmp != tail)
			{
				if(tmp.data.CompareTo(nodeData) == 0)
					return tmp;

				tmp = tmp.next;
			}

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
			DNode tmp = startNode;
			while (tmp != tail)
			{
				doSomething(tmp);

				tmp = tmp.next;
			}
		}
		// 데이터를 지정된 노드 순방향으로 탐색하기
		public void TraversalForward(DNode startNode, DNode endNode, DelegateTraversalDNode doSomething)
		{
			DNode tmp = startNode;
			while (tmp != head)
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
			DNode tmp = startNode;
			while (tmp != head)
			{
				doSomething(tmp);
				tmp = tmp.prev;
			}
		}

		
		// 데이터를 지정된 노드 역방향으로 탐색하기
		public void TraversalBackward(DNode startNode, DNode endNode, DelegateTraversalDNode doSomething)
		{
			DNode tmp = startNode;
			while (tmp != head)
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
			DNode tmp = head.next;
			while (tmp != tail)
			{
				tmp.data.Print();
				tmp = tmp.next;
			}
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
		}

	}
}

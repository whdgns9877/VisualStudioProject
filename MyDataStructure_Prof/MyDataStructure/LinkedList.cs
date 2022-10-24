using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDataStructure
{
	
	//
	//
	// 단방향 연결리스트 클래스
	//
	public class LinkedList
	{
		LNode head = null; // 첫번째 노드를 직접 가리키고 있다.
		LNode tail = null; // 마지막 노드를 직접 가리키고 있다.

		public LNode GetHead()
		{
			return head;
		}
		public LNode GetTail()
		{
			return tail;
		}

		public void Clear()
		{
			head = tail = null;
		}

		// head 다음에 추가
		public LNode InsertHead(INodeData newData)
		{
			// 새 데이터를 담는 새 노드
			LNode newNode = new LNode();
			newNode.data = newData;

			InsertHead(newNode);

			return newNode;
		}

		// head 다음에 추가
		// newNode 는 새로 생성해서 넣어줘야 한다.
		public INodeData InsertHead(LNode newNode)
		{
			// 새 노드의 연결관계
			if (head == null)
				head = tail = newNode;
			else
			{
				newNode.next = head;
				head = newNode;
			}

			return newNode.data;
		}


		// tail 앞에 추가
		public LNode InsertTail(INodeData newData)
		{
			// 새 데이터를 담는 새 노드
			LNode newNode = new LNode();
			newNode.data = newData;

			InsertTail(newNode);

			return newNode;
		}

		// tail 앞에 Node 추가
		// newNode 는 새로 생성해서 넣어줘야 한다.
		public INodeData InsertTail(LNode newNode)
		{
			if (tail == null)
				head = tail = newNode;
			else
			{
				// 새 노드의 연결관계
				tail.next = newNode;
				// 새 노드가 추가되었으니 tail은 새 노드를 가리켜야 한다.
				tail = newNode;
			}

			return newNode.data;
		}

		// 특정 데이터 뒤에 추가
		public LNode InsertAfter(INodeData targetData, INodeData newData)
		{
			LNode targetNode = Search(targetData);
			if (targetNode != null)
				return InsertAfter(targetNode, newData);

			return null;
		}

		// 특정 노드 뒤에 추가
		public LNode InsertAfter(LNode targetNode, INodeData newData)
		{
			// 새 데이터를 담는 새 노드
			LNode newNode = new LNode();
			newNode.data = newData;

			newNode.next = targetNode.next;
			targetNode.next = newNode;

			return newNode;
		}

		// head 노드 삭제
		public LNode DeleteHead()
		{
			// 아무것도 없을 때
			if (head == null) return null;

			LNode delTarget = head; // 맨 앞에 있는 노드가 삭제대상

			if (head == tail)
				head = tail = null;			
			else
				head = head.next;// 삭제할 노드의 다음노드의 이전노드가 head가 되도록
					
			
			// 참조관계를 모두 끊어주고 메모리 해제가 되기를...
			delTarget.next = null;
			return delTarget;
		}

		// tail 제거
		public LNode DeleteTail()
		{
			// 아무것도 없을 때
			if (tail == null) return null;

			LNode delTarget = tail;
			if(head == tail)
			{
				head = tail = null;
				return delTarget;
			}

			// tail 바로 전까지 이동
			LNode prev = null;
			LNode tmp = head;
			while (tmp.next != null)
			{
				prev = tmp;
				tmp = tmp.next;
			}
			prev.next = null;
			tail = prev;

			return delTarget;
		}

		// 특정 데이터 삭제
		public LNode DeleteNode(INodeData delData)
		{
			LNode delTarget = null;
			LNode prev = null;
			LNode tmp = head;
			while (tmp != tail)
			{
				// 삭제할 노드 찾고
				if (tmp.data.CompareTo(delData) == 0)
				{
					delTarget = tmp;
					if (prev == null) // 삭제할 노드가 head 라는 뜻
						head = head.next;
					else
						prev.next = tmp.next;
					tmp.next = null;
					// 삭제할 노드가 마지막 노드였다면
					if (prev.next == tail)
						tail = prev;

					return delTarget;
				}

				prev = tmp;
				tmp = tmp.next;
			}

			return null;
		}

		// 검색
		public LNode? Search(INodeData nodeData)
		{
			LNode tmp = head;
			while (tmp != null)
			{
				if (tmp.data.CompareTo(nodeData) == 0)
					return tmp;

				tmp = tmp.next;
			}

			return null;
		}

		// 데이터 모두 출력
		public void PrintForwardAll()
		{
			//Console.WriteLine("## 데이터 전체 출력");
			LNode tmp = head;
			while (tmp != null)
			{
				tmp.data.Print();
				tmp = tmp.next;
			}
		}

		public void PrintForwardAll(DelegateTraversalLNode doSomething)
		{
			LNode tmp = head;
			while (tmp != null)
			{
				doSomething(tmp);

				tmp = tmp.next;
			}
		}

	};
}

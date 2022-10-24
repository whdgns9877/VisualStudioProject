using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDataStructure
{



	//
	//
	// 원형 단방향 연결리스트 클래스
	//
	public class CLinkedList
	{
		LNode tail = null; // 마지막 노드를 직접 가리키고 있다.

		// head 앞에 추가
		public void InsertHead(INodeData newData)
		{
			// 새 데이터를 담는 새 노드
			LNode newNode = new LNode();
			newNode.data = newData;

			if (tail == null)
			{
				newNode.next = newNode;				
				tail = newNode;
			}
			else
			{
				newNode.next = tail.next; // tail.next 가 head
				tail.next = newNode;
			}
		}

		// tail 뒤에 추가
		public void InsertTail(INodeData newData)
		{
			// 새 데이터를 담는 새 노드
			LNode newNode = new LNode();
			newNode.data = newData;

			if (tail == null)
			{
				newNode.next = newNode;
				tail = newNode;
			}
			else
			{
				newNode.next = tail.next; // tail.next 가 head
				tail.next = newNode;
				tail = newNode;
			}
		}

		// 특정 데이터 뒤에 추가
		public void InsertAfter(INodeData targetData, INodeData newData)
		{
			LNode targetNode = Search(targetData);
			if (targetNode != null)
				InsertAfter(targetNode, newData);
		}

		// 특정 노드 뒤에 추가
		public void InsertAfter(LNode targetNode, INodeData newData)
		{
			// 새 데이터를 담는 새 노드
			LNode newNode = new LNode();
			newNode.data = newData;

			newNode.next = targetNode.next;
			targetNode.next = newNode;

			if (targetNode == tail)
				tail = newNode;
		}

		// head 노드 삭제
		public void DeleteHead()
		{
			// 아무것도 없을 때
			if (tail == null) return;

			LNode delTarget = tail.next;
			// 마지막 노드를 삭제하는 경우
			if (delTarget == tail)
				tail = null;
			else
				tail.next = delTarget.next;

			// 참조관계를 모두 끊어주고 메모리 해제가 되기를...
			delTarget.next = null;	
		}

		// tail 제거
		public void DeleteTail()
		{
			// 아무것도 없을 때
			if (tail == null) return;

			// tail 바로 전까지 이동
			LNode tmp = tail.next;
			while (tmp.next != tail)
			{
				tmp = tmp.next;
			}

			if(tail == tmp)
				tail = null;
			else
				tail = tmp;
			
			tmp.next = null;

		}

		// 특정 데이터 삭제
		public void DeleteNode(INodeData delData)
		{
			LNode prev = null;
			LNode tmp = tail.next;
			while (tmp != tail)
			{
				// 삭제할 노드 찾고
				if (tmp.data.CompareTo(delData) == 0)
				{
					if (prev == null) // 삭제할 노드가 head 라는 뜻
						tail.next = tmp.next;
					else
						prev.next = tmp.next;
					tmp.next = null;
					// 삭제할 노드가 마지막 노드였다면
					if (prev.next == tail)
						tail = prev;

					return;
				}

				prev = tmp;
				tmp = tmp.next;
			}
		}

		// 검색
		public LNode Search(INodeData nodeData)
		{
			LNode tmp = tail.next;
			while (tmp != tail)
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
			if(tail == null)
			{
				Console.WriteLine("## 데이터가 존재하지 않습니다.");
				return;
			}
			LNode tmp = tail.next;
			do
			{
				tmp.data.Print();
				tmp = tmp.next;
			} while (tmp != tail.next);
		}


	};
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDataStructure
{
	// 
	// 연결리스트 기반 스택
	internal class StackLL
	{
		LNode head = null; // 스택의 맨 위를 의미한다.


		public void Clear()
		{
			head = null;
		}

		public bool IsEmpty()
		{
			return (head == null);
		}

		//
		// 데이터 추가
		public LNode Push(INodeData newData)
		{
			// 새 데이터를 담는 새 노드
			LNode newNode = new LNode();
			newNode.data = newData;

			newNode.next = head;
			head = newNode;

			return newNode;
		}

		// 
		// 데이터 꺼내오기
		public LNode Pop()
		{
			if(IsEmpty())
			{
				Console.WriteLine("스택이 비어 있습니다.");
				return null;
			}

			LNode targetNode = head;
			head = head.next;

			return targetNode;
		}


		// 데이터 만 받아오기(실제 꺼내지는 것은 아니다.)
		public INodeData Peek()
		{
			if (IsEmpty())
			{
				Console.WriteLine("스택이 비어 있습니다.");
				return null;
			}

			return head.data;
		}
	}
}

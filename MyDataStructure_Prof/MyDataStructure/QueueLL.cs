using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDataStructure
{
	//
	// 연결리스트 기반 큐
	//
	// 큐는 FIFO(First In First Out) - 먼저 추가된 데이터를 제일 먼저 꺼낼 수 있는
	internal class QueueLL
	{
		LNode front = null;	// 맨앞
		LNode rear = null;  // 맨뒤

		public void Clear()
		{
			front = null;
			rear = null;
		}

		public bool IsEmpty()
		{
			return (front == null);
		}

		//
		// 새로 추가하기
		public LNode Enqueue(INodeData newData)
		{
			// 새 데이터를 담는 새 노드
			LNode newNode = new LNode();
			newNode.data = newData;

			if(front == null)
			{
				front = newNode;
				rear = newNode;
			}
			else
			{
				rear.next = newNode;	// 마지막노드에 연결
				rear = newNode;			// 새로 추가한 Node가 마지막 노드
			}

			return newNode;
		}

		//
		// 하나 꺼내기
		// 큐는 FIFO 구조이기 때문에 제일 먼저 들어간 데이터를 제일 먼저 꺼낼 수 있다.
		public LNode Dequeue()
		{
			if (front == null)
			{
				Console.WriteLine("큐에 데이터가 없다.");
				return null;
			}

			LNode target = front;
			front = front.next;

			return target;
		}


		//
		// 데이터 받아오기(실제 꺼내오는 것은 아니다.
		LNode Peek()
		{
			if (front == null)
			{
				Console.WriteLine("큐에 데이터가 없다.");
				return null;
			}

			return front;
		}

	}
}

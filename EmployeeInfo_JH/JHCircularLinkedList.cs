using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeInfo
{
    class Node
    {
        public NodeData nodeData;

        public Node next;
    }

    class NodeData
    {
        public string Lineinfo;
        public int officeNum;
        public string name;
    }

    internal class JHCircularLinkedList
    {
        Node head = null;
        Node tail = null;
        Node cur = null;

        int Count;

        public void Add(NodeData nodeDataForAdd)
        {
            // 추가할 데이터를 담고 있는 노트 생성
            Node addNode = new Node();
            addNode.nodeData = nodeDataForAdd;

            // head가 null이라는것은 현재 리스트에 데이터가
            // 아무것도 없다는 뜻이므로 head, tail이 추가된 데이터를
            // 가리키게 한다
            if (head == null)
                head = addNode;
            else
                tail.next = addNode;

            // 위 상황이 아니라면 이미 추가되어있는 데이터가 있는상태이고
            // 이때 노드의 마지막이 추가된 노드가 되도록
            // tail이 이 추가된 노드를 가리키게한다
            tail = addNode;
            // 그리고 tail의 next를 head로 지정하여 순환구조로 만든다
            tail.next = head;
            Count++;
        }

        // 리스트의 데이터를 출력하는 함수
        public void Show(string name, int num)
        {
            // 데이터가 아무것도 없다면 출력할게 없으므로
            // 안내 문자 출력
            if (head == null)
                Console.WriteLine("리스트가 비어있습니다.");
            else
            {
                // cur 를 입력받은 곳에서부터
                cur = head;

                while (true)
                {
                    if(cur.nodeData.name == name)
                    {
                        break;
                    }
                    else
                    {
                        cur = cur.next;
                    }
                }

                // 처음부터 반복시작
                for(int i = 0; i < num; i++)
                {
                    cur = cur.next;
                }

                Console.WriteLine($"다음 당직자의 정보 : " + cur.nodeData.Lineinfo);
            }
        }

        public void SetInfo()
        {
            // 데이터가 아무것도 없다면 출력할게 없으므로
            // 안내 문자 출력
            if (head == null)
                Console.WriteLine("리스트가 비어있습니다.");
            else
            {
                // cur 를 맨 처음 노드인 head를 가리키게 하여
                cur = head;
                // 처음부터 반복시작
                for (int i = 0; i < Count; i++)
                {
                    cur.nodeData.name = cur.nodeData.Lineinfo.Split(' ', ',')[0];
                    cur.nodeData.officeNum = Convert.ToInt32(cur.nodeData.Lineinfo.Split(' ')[1]);
                    cur = cur.next;
                }
            }
        }
    }
}

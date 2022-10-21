using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookInfo
{
    class Node
    {
        public NodeData nodeData;

        public Node next;
    }

    class NodeData
    {
        public string lineInfo;
        public string cookName;
    }

    internal class JHLinkedList
    {
        Node head = null;
        Node tail = null;
        Node cur = null;

        int listCount = 0;

        // 리스트에 데이터를 추가하는 함수
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
            listCount++;
        }

        // 알파벳 순대로 정렬
        public void Sort()
        {
            // 데이터가 아무것도 없다면 출력할게 없으므로
            // 안내 문자 출력
            if (head == null)
                Console.WriteLine("리스트가 비어있으므로 정렬할 것이 없습니다.");
            else
            {
                for (int i = 0; i < listCount; i++)
                {
                    cur = head;
                    do
                    {
                        // 다음 노드가 가리키는것이 없다면 비교할게 없이 끝까지 왔다는것이므로 멈춤
                        if (cur.next == null)
                            break;
                        // 텍스트 파일 라인을 공백문자로 쪼개 0번인덱스는 요리사 이름이고
                        // 1번 인덱스가 요리이름이기에 이를 cookName이라는 nodeData에 저장한다
                        cur.nodeData.cookName = cur.nodeData.lineInfo.Split(' ')[1];
                        cur.next.nodeData.cookName = cur.next.nodeData.lineInfo.Split(' ')[1];

                        // string의 0번째 인덱스가 가장 앞 알파벳이므로 이를 정수값으로 변환하여 비교하여 더 크다면 뒤로 스왑해준다
                        if (Convert.ToInt32(cur.nodeData.cookName[0]) > Convert.ToInt32(cur.next.nodeData.cookName[0]))
                        {
                            Node tempNode = new Node();
                            tempNode.nodeData = cur.nodeData;
                            cur.nodeData = cur.next.nodeData;
                            cur.next.nodeData = tempNode.nodeData;
                        }
                        cur = cur.next;
                    } while (cur != null); 
                }
            }
        }

        // 리스트의 데이터를 출력하는 함수
        public void Show()
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
                do
                {
                    Console.WriteLine($"ListData : " + cur.nodeData.lineInfo);
                    // 한번 실행후 현재노드를 가리키는 cur를 다음 노드를 가리키게 하여 진행
                    cur = cur.next;
                } while(cur != null); // 현재 가리킬 노드가 없다면 종료
            }
        }
    }
}

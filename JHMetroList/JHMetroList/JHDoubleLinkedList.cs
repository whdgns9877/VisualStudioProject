using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHMetroList
{
    internal class JHDoubleLinkedList
    {
        Node head = null;
        Node tail = null;
        Node cur = null;

        Node startNode = null;
        Node arriveNode = null;

        // 노드 갯수 저장할 변수
        int nodeCount = 0;

        // 리스트에 데이터를 추가하는 함수
        public void Add(NodeData nodeDataForAdd)
        {
            // 추가할 데이터를 담고 있는 노트 생성
            Node addNode = new Node();
            addNode.nodeData = nodeDataForAdd;

            Node prev = new Node();

            // head가 null이라는것은 현재 리스트에 데이터가
            // 아무것도 없다는 뜻이므로 head, tail이 추가된 데이터를
            // 가리키게 한다
            if (head == null)
                head = addNode;
            else
            {
                // head, tail, 중간에서 찾을수 있지만 기본적으로 tail에추가하는 방식으로add
                // 우선 prev라는 노드에 본인이 원래 가리키고있던걸 가리켜놓고
                prev = tail;
                // 그 후에 tail의next를 새로운 노드를 가리키게한다
                tail.next = addNode;
            }

            // 위 상황이 아니라면 이미 추가되어있는 데이터가 있는상태이고
            // 이때 노드의 마지막이 추가된 노드가 되도록
            // tail이 이 추가된 노드를 가리키게한다
            tail = addNode;
            // 그리고 tail.prev라는 노드에 추가전 연결상태를 넣어주어 가리키게한다
            tail.prev = prev;
            nodeCount++;
        }

        public int CheckStationLine(string station)
        {

            if (head == null)
            {
                Console.WriteLine("리스트가 비어있습니다.");
                return 0;
            }
            else
            {
                // cur 를 맨 처음 노드인 head를 가리키게 하여
                cur = head;
                // 처음부터 반복시작

                while (true)
                {
                    if (cur == null)
                    {
                        return 2;
                    }

                    if (cur.nodeData.station == station)
                    {
                        startNode = cur;
                        return 1;
                        //break;
                    }
                    else
                    {
                        // 입력한 station정보가 나올때까지 처음부터 순회
                        cur = cur.next;
                    }
                }
            }
        }

        // 출발역과 도착역 사이의 경로와 최종 정차역 수를 출력해주는 함수
        public void GetRoute(string startStation, string arriveStation)
        {
            int startStationNum = 0;
            int arriveStationNum = 0;

            int stationCount = 0;

            if (head == null)
                Console.WriteLine("리스트가 비어있습니다.");
            else
            {
                // cur 를 맨 처음 노드인 head를 가리키게 하여
                cur = head;
                // 처음부터 반복시작

                while (true)
                {
                    if (cur.nodeData.station == startStation)
                    {
                        startStationNum = cur.nodeData.num;
                        startNode = cur;
                        break;
                    }
                    else
                    {
                        // 입력한 station정보가 나올때까지 처음부터 순회
                        cur = cur.next;
                    }
                }

                cur = head;

                while (true)
                {
                    if (cur.nodeData.station == arriveStation)
                    {
                        arriveStationNum = cur.nodeData.num;
                        arriveNode = cur;
                        break;
                    }
                    else
                    {
                        // 입력한 station정보가 나올때까지 처음부터 순회
                        cur = cur.next;
                    }
                }
            }

            // cur 를 맨 처음 노드인 head를 가리키게 하여
            cur = startNode;

            if (arriveStationNum - startStationNum > 0)
            {

                while (true)
                {
                    if (cur.nodeData.num == arriveStationNum)
                    {
                        Console.WriteLine(cur.nodeData.station + " ");
                        break;
                    }
                    else
                    {
                        Console.Write(cur.nodeData.station + " -> ");
                        stationCount++;
                        cur = cur.next;
                    }
                }
            }
            else
            {
                while (true)
                {
                    if (cur.nodeData.num == arriveStationNum)
                    {
                        Console.WriteLine(cur.nodeData.station + " ");
                        break;
                    }
                    else
                    {
                        Console.Write(cur.nodeData.station + " -> ");
                        stationCount++;
                        cur = cur.prev;
                    }
                }
            }

            Console.WriteLine("정차역은 : " + stationCount);
        }
    }
}

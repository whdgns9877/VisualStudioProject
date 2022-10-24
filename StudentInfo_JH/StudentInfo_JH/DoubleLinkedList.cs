using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfo_JH
{
    internal class DoubleLinkedList
    {
        public DoubleLinkedList nextList;

        Node head = null;
        Node tail = null;
        Node cur = null;

        int dataCount = 0;

        // 리스트에 데이터를 추가하는 함수
        //public void Add(NodeData nodeDataForAdd)
        //{
        //    // 추가할 데이터를 담고 있는 노트 생성
        //    Node addNode = new Node();
        //    addNode.nodeData = nodeDataForAdd;

        //    Node prev = new Node();

        //    // head가 null이라는것은 현재 리스트에 데이터가
        //    // 아무것도 없다는 뜻이므로 head, tail이 추가된 데이터를
        //    // 가리키게 한다
        //    if (head == null)
        //        head = addNode;
        //    else
        //    {
        //        // head, tail, 중간에서 찾을수 있지만 기본적으로 tail에추가하는 방식으로add
        //        // 우선 prev라는 노드에 본인이 원래 가리키고있던걸 가리켜놓고
        //        prev = tail;
        //        // 그 후에 tail의next를 새로운 노드를 가리키게한다
        //        tail.next = addNode;
        //    }

        //    // 위 상황이 아니라면 이미 추가되어있는 데이터가 있는상태이고
        //    // 이때 노드의 마지막이 추가된 노드가 되도록
        //    // tail이 이 추가된 노드를 가리키게한다
        //    tail = addNode;
        //    // 그리고 tail.prev라는 노드에 추가전 연결상태를 넣어주어 가리키게한다
        //    tail.prev = prev;
        //    dataCount++;
        //}
    }
}

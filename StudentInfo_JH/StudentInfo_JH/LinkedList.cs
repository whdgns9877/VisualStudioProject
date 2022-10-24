using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfo_JH
{
    internal class LinkedList
    {
        Node head = null;
        Node tail = null;
        Node cur = null;

        int listCount = 0;

        // 리스트에 데이터를 추가하는 함수
        public void Add(Node newNode)
        {
            // 추가할 데이터를 담고 있는 노트 생성
            Node addNode = newNode;
            //addNode.nodeData = nodeDataForAdd;

            // head가 null이라는것은 현재 리스트에 데이터가
            // 아무것도 없다는 뜻이므로 head, tail이 추가된 데이터를
            // 가리키게 한다
            if (head == null)
                head = addNode;
            else
                tail.nextData = addNode;

            // 위 상황이 아니라면 이미 추가되어있는 데이터가 있는상태이고
            // 이때 노드의 마지막이 추가된 노드가 되도록
            // tail이 이 추가된 노드를 가리키게한다
            tail = addNode;
            listCount++;
        }

        //// 리스트에 데이터를 추가하는 함수
        //public void Add(DoubleLinkedList nextList)
        //{
        //    // 추가할 데이터를 담고 있는 노트 생성
        //    DoubleLinkedList addList = nextList;

        //    // head가 null이라는것은 현재 리스트에 데이터가
        //    // 아무것도 없다는 뜻이므로 head, tail이 추가된 데이터를
        //    // 가리키게 한다
        //    if (head == null)
        //        head = addList;
        //    else
        //        tail.nextList = addList;

        //    // 위 상황이 아니라면 이미 추가되어있는 데이터가 있는상태이고
        //    // 이때 노드의 마지막이 추가된 노드가 되도록
        //    // tail이 이 추가된 노드를 가리키게한다
        //    tail = addList;
        //    listCount++;
        //}
    }
}

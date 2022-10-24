using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDataStructure
{
	// Binary Search Tree
	internal class BST
	{
		BTNode rootNode = null;

		

		// insert 함수는 노드데이터를 binary search tree 규칙에 의해 tree에 삽입합니다.
		// 단, 같은 값을 가지는 node가 있으면 삽입하지 않고 false를 return하고, 
		// 정상적으로 삽입된 경우 true를 return 합니다
		public bool Insert(INodeData inputData)
		{
			if (rootNode == null)
			{
				rootNode = new BTNode();
				rootNode.data = inputData;
				return true;
			}

			return Insert(inputData, rootNode);
		}

		public bool Insert(INodeData inputData, BTNode node)
		{
			int compareResult = inputData.CompareTo(node.data);
			// 단, 같은 값을 가지는 node가 있으면 삽입하지 않고 false를 return하고, 
			if (compareResult == 0)
				return false;

			// 값이 작으면 leftChildNode에 추가
			else if (compareResult < 0)
			{
				if (node.leftChild != null)
				{
					return Insert(inputData, node.leftChild);
				}
				else
				{
					node.leftChild = new BTNode();
					node.leftChild.data = inputData;
				}
			}
			// 값이 크면 rightChildNode에 추가
			else
			{
				if (node.rightChild != null)
				{
					return Insert(inputData, node.rightChild);
				}
				else
				{
					node.rightChild = new BTNode();
					node.rightChild.data = inputData;
				}
			}

			return true;
		}

		// remove 함수는 노드데이터를 해당되는 node를 찾고, 
		// node를 delete 하면서 tree 구조를 정상적으로 수정합니다. 
		// 단, 해당되는 node가 없으면 false를 return하고, 정상적으로 삭제되면 true를 return 합니다.
		public bool Remove(INodeData inputData)
		{
			// cur가 삭제할 노드인거다

			// 해당되는 node를 찾고,
			BTNode cur = rootNode;
			BTNode parent = null;
			while (cur != null)
			{
				int compareResult = cur.data.CompareTo(inputData);
				if (compareResult == 0)
					break;

				parent = cur;

				if(compareResult < 0)
					cur = cur.leftChild;
				else
					cur = cur.rightChild;
			}

			// 해당되는 node 를 찾으면
			if (cur != null)
			{
				// 더 이상 Child노드가 없을 때 (단말노드, Leaf Node)
				if (cur.leftChild == null && cur.rightChild == null)
				{
					if (parent != null)
					{
						if (cur == parent.leftChild)
						{
							parent.leftChild = null;
						}
						else
						{
							parent.rightChild = null;
						}
					}
					else
					{
						rootNode = null;
					}
				}
				// leftChild에 ChildNode가 존재할 때
				else if (cur.leftChild != null && cur.rightChild == null)
				{
					// leftChild 중에서 가장 오른쪽에 있는 ChildNode를 찾는다.
					// targetNode 가 가장 오른쪽에 있는 childNode 가 된다.
					// targetParent 는 targetNode 의 바로 위 부모노드가 된다.
					BTNode targetNode = cur.leftChild;
					BTNode targetParent = cur; 
					while (targetNode.rightChild != null)
					{
						targetParent = targetNode;
						targetNode = targetNode.rightChild;
					}

					// 찾은 Node의 Parent Node와 끊기(바로 위 부모노드와 관계 끊기)
					if (cur == targetParent)
					{
						targetParent.leftChild = null;
					}
					else
					{
						targetParent.rightChild = null;
					}

					// 찾은 Node를 삭제되는 Node 위치로
					targetNode.leftChild = cur.leftChild;
					targetNode.rightChild = cur.rightChild;

					// 삭제하려는 Node의 Parent의 leftChildNode에 찾은 Node를 연결
					if (parent != null)
					{
						parent.leftChild = targetNode;
					}
					else
					{
						rootNode = targetNode;
					}
				}
				// Childe Node 양쪽(오른쪽만) 모두 존재할 때
				else
				{
					//rightChildNode 중에서 가장 왼쪽에 있는 ChildNode를 찾는다.			
					// targetNode 가 가장 왼쪽에 있는 childNode 가 된다.
					// targetParent 는 targetNode 의 바로 위 부모노드가 된다.
					BTNode targetNode = cur.rightChild;
					BTNode targetParent = cur;
					while (targetNode.leftChild != null)
					{
						targetParent = targetNode;
						targetNode = targetNode.leftChild;
					}


					// 찾은 Node의 Parent Node와 끊기
					if (cur == targetParent)
					{
						targetParent.rightChild = null;
					}
					else
					{
						targetParent.leftChild = null;
					}

					// 찾은 Node를 삭제되는 Node 위치로
					targetNode.leftChild = cur.leftChild;
					targetNode.rightChild = cur.rightChild;

					// 삭제하려는 Node의 Parent의 rightChildNode에 찾은 Node를 연결
					if (parent != null)
					{
						parent.rightChild = targetNode;
					}
					else
					{
						rootNode = targetNode;
					}
				}



				// 해당 노드 메모리 해제
				cur.leftChild = cur.rightChild = null;
				return true;
			}

			// 단, 해당되는 node가 없으면 false를 return하고, 
			return false;
		}

		// find 함수는 int 값을 받아 
		// tree 상에서 그 값을 가진 node를 찾아 node pointer를 return 합니다. 
		// 없는 경우에는 null을 return 합니다.
		public BTNode Find(INodeData inputData)
		{
			return Find(inputData, rootNode);
		}

		public BTNode Find(INodeData inputData, BTNode node)
		{
			if (node != null)
			{
				int compareResult = node.data.CompareTo(inputData);
				// tree 상에서 그 값을 가진 node를 찾아 node pointer를 return 합니다. 
				if (compareResult == 0)
				{
					return node;
				}
				// 값이 작으면 leftChildNode에서 find
				else if (compareResult < 0)
				{
					return Find(inputData, node.leftChild);
				}
				// 값이 크면 rightChildNode에서 find
				else
				{
					return Find(inputData, node.rightChild);
				}
			}

			return null;
		}


		// print 함수는 tree를 in-order로 traverse 하면서 node의 data 값을 print (cout) 합니다.
		public void Print()
		{
			inorderTraverse(rootNode);
			Console.WriteLine("");
		}

		void inorderTraverse(BTNode node)
		{
			if (node != null)
			{
				inorderTraverse(node.leftChild);
				Console.Write(node.data.OutputString() + " ");
				inorderTraverse(node.rightChild);
			}
		}

	}
}

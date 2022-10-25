namespace MyDataStructure
{
	public delegate void DelegateTraversalLNode(LNode node);
	public delegate void DelegateTraversalDNode(DNode node);
	public delegate void DelegateTraversalBTNode(BTNode node);

	//
	// 기본 노드 클래스
	public class LNode
	{
		public INodeData data = null;

		public LNode next = null;    // 다음 노드
	};


	//
	// 양방향 노드 클래스
	public class DNode
	{
		public INodeData data = null;

		public DNode prev = null;
		public DNode next = null;
	}


	//
	// Binary Tree 노드 클래스
	public class BTNode
	{
		public INodeData data = null;

		public BTNode leftChild = null;
		public BTNode rightChild = null;
	}

}

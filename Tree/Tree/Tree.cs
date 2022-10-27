using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Tree
{
    internal class Tree
    {
        private List<TNode> nodeList;

        public TNode rootNode = null;

        public Tree()
        {
            nodeList = new List<TNode>();
        }

        public TNode AddNode(int value)
        {
            TNode n = new TNode(value);
            nodeList.Add(n);
            return n;
        }

        public void InsertNode(int value)
        {
            TNode n = new TNode(value);

            if (rootNode == null)
            {
                rootNode = n;
                return;
            }

            CheckMyPos(n, rootNode);
        }

        public void CheckMyPos(TNode addNode, TNode pivotNode)
        {
            if(addNode.Value < pivotNode.Value)
            {
                if(pivotNode.left == null) { pivotNode.left = addNode; return; }
                CheckMyPos(addNode, pivotNode.left);
            }
            else
            {
                if (pivotNode.right == null) { pivotNode.right = addNode; return; }
                CheckMyPos(addNode, pivotNode.right);
            }
        }

        public void PrintPreorder(TNode node)
        {
            if (rootNode == null)
            {
                Console.WriteLine("트리에 노드가 하나도 없습니다.");
                return;
            }
            Console.WriteLine(node.Value);
            if (node.left != null) PrintPreorder(node.left);
            if (node.right != null) PrintPreorder(node.right);
        }

        public void PrintPostorder(TNode node)
        {
            if(node != null)
            {
                PrintPostorder(node.left);
                PrintPostorder(node.right);
                Console.WriteLine(node.Value);
            }
        }

        public void PrintMidorder(TNode node)
        {
            if (node != null)
            {
                PrintMidorder(node.left);
                Console.WriteLine(node.Value);
                PrintMidorder(node.right);
            }
        }

        public TNode Search(int value, TNode pivotNode)
        {
            if (value < pivotNode.Value)
            {
                if(value == pivotNode.left.Value)
                    return null;
                Search(value, pivotNode.left);
            }
            else
            {
                if (value == pivotNode.right.Value)            
                    return null;
                Search(value, pivotNode);
            }

            return pivotNode;
        }

        public void Delete(int value)
        {
            TNode delTarget = rootNode;
            TNode delTargetParnet = null;

            while(delTarget != null)
            {
                if (delTarget.Value == value)
                    break;

                delTargetParnet = delTarget;

                if (value < delTarget.Value)
                    delTarget = delTarget.left;
                else
                    delTarget = delTarget.right;
            }

            // 삭제할 노드가 자식이 없으면
            if(delTarget.left == null && delTarget.right == null)
            {
                if (delTargetParnet != null)
                {
                    if (delTargetParnet.left == delTarget)
                        delTargetParnet.left = null;
                    else
                        delTargetParnet.right = null;
                }
                else
                {
                    rootNode = null;
                }
            }
            // 삭제할 노드가 자식이 있다면
            else
            {
                // 삭제할 노드의 자식이 2개일때
                if(delTarget.left != null && delTarget.right != null)
                {
                    TNode maxValInLeft = delTarget.left;
                    //TNode minValInRight = delTarget.right;
                    // 삭제할 노드의 왼쪽(작은쪽 노드들) 중에 가장 큰 수를 찾는다
                    while(maxValInLeft != null)
                    {
                        if (maxValInLeft.right == null) break;
                        maxValInLeft = maxValInLeft.right;
                    }
                    //// 삭제할 노드의 오른쪽(큰쪽 노드들) 중에 가장 작은 수를 찾는다
                    //while (minValInRight != null)
                    //{
                    //    if (minValInRight.left == null) break;
                    //    minValInRight = minValInRight.left;
                    //}

                    if (delTargetParnet != null)
                    {
                        // 삭제될 노드의 부모의 왼쪽을 가장 큰 노드로 달아주고
                        delTargetParnet.left = maxValInLeft;
                        // 왼쪽에 달린 노드의 오른쪽을 삭제될 노드의 기존오른쪽에 달아준다
                        maxValInLeft.right = delTarget.right;

                        // 삭제노드 기준 왼쪽에서 가장 큰 노드를 달아준 것이므로
                        // 해당 노드 기준으로 왼쪽 과 기존 노드의 관계를 다시 정해준다
                        maxValInLeft.left = delTarget.left;
                        maxValInLeft.left.right = null;
                    }
                    else
                    {
                        rootNode = null;
                    }
                }
                else
                {
                    // 자식이 1개일때
                    if (delTargetParnet != null)
                    {
                        if (delTargetParnet.left == delTarget)
                            delTargetParnet.left = delTarget.left;
                        else
                            delTargetParnet.right = delTarget.right;
                    }
                    else
                    {
                        rootNode = null;
                    }
                }
            }
        }
    }
}

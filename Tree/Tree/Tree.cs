using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Tree
{
    internal class Tree
    {
        public TNode rootNode = null;

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
            if (rootNode == null)
            {
                Console.WriteLine("트리에 노드가 하나도 없습니다.");
                return;
            }
            if (node != null)
            {
                PrintPostorder(node.left);
                PrintPostorder(node.right);
                Console.WriteLine(node.Value);
            }
        }

        public void PrintMidorder(TNode node)
        {
            if (rootNode == null)
            {
                Console.WriteLine("트리에 노드가 하나도 없습니다.");
                return;
            }
            if (node != null)
            {
                PrintMidorder(node.left);
                Console.WriteLine(node.Value);
                PrintMidorder(node.right);
            }
        }

        public TNode Search(int value)
        {
            TNode searchTarget = rootNode;
            Console.Write("찾고자 하는 노드 : ");
            while (searchTarget != null)
            {
                if (searchTarget.Value == value)
                    break;

                if (value < searchTarget.Value)
                {
                    Console.Write(searchTarget.Value + " -> ");
                    searchTarget = searchTarget.left;
                }
                else
                {
                    Console.Write(searchTarget.Value + " -> ");
                    searchTarget = searchTarget.right;
                }
            }
            Console.WriteLine(searchTarget.Value);
            return searchTarget;
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

            // if delTartget has no child
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
            // if delTarget has child
            else
            {
                // if delTarget has two child
                if(delTarget.left != null && delTarget.right != null)
                {
                    TNode maxValInLeft = delTarget.left;
                    //TNode minValInRight = delTarget.right;
                    // Find the largest number among the left (smallest nodes)
                    // of the node to be deleted.
                    while (maxValInLeft != null)
                    {
                        if (maxValInLeft.right == null) break;
                        maxValInLeft = maxValInLeft.right;
                    }
                    //// Find the smallest number on the right (largest nodes) 
                    //// of the node to be deleted
                    //while (minValInRight != null)
                    //{
                    //    if (minValInRight.left == null) break;
                    //    minValInRight = minValInRight.left;
                    //}

                    if (delTargetParnet != null)
                    {
                        // Set the left side of the parent of the node
                        // to be deleted as the largest node.
                        delTargetParnet.left = maxValInLeft;
                        // Attach the right side of the node attached to the left to the
                        // right side of the existing node of the node to be deleted
                        // maxValInLeft.right = delTarget.right;

                        //  Since the largest node is attached to the left of the deleted node,
                        //  the relationship between the left and the existing node is re - established based on the corresponding node.
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
                    //  if delTarget has one child
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

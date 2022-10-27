using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Tree
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Tree tree = new Tree();

            //TNode a = tree.AddNode(1);
            //TNode b = tree.AddNode(2);
            //TNode c = tree.AddNode(3);
            //TNode d = tree.AddNode(4);
            //TNode e = tree.AddNode(5);

            //a.left = b;
            //a.right = c;

            //b.left = d;
            //b.right = e;

            //Console.WriteLine("--------- 전위 ----------");
            //tree.PrintPreorder(a);
            //Console.WriteLine("--------- 후위 ----------");
            //tree.PrintPostorder(a);
            //Console.WriteLine("--------- 중위 ----------");
            //tree.PrintMidorder(a);

            tree.InsertNode(8);
            tree.InsertNode(4);
            tree.InsertNode(9);
            tree.InsertNode(2);
            tree.InsertNode(7);
            tree.InsertNode(10);
            tree.InsertNode(1);
            tree.InsertNode(6);
            tree.InsertNode(5);
            tree.InsertNode(3);
            
            tree.PrintPreorder(tree.rootNode);

            tree.Delete(4);

            tree.PrintPreorder(tree.rootNode);


            //tree.Search(10 ,tree.rootNode);
        }
    }
}

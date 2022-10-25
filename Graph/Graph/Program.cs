using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();

            //GNode a = graph.AddNode("A", false);
            //GNode b = graph.AddNode("B", false);
            //GNode c = graph.AddNode("C", false);
            //GNode d = graph.AddNode("D", false);
            //GNode e = graph.AddNode("E", false);
            //GNode f = graph.AddNode("F", false);

            //graph.AddEdge(a, b, false);
            //graph.AddEdge(a, f, false);
            //graph.AddEdge(b, c, false);
            //graph.AddEdge(f, c, false);
            //graph.AddEdge(c, d, false);
            //graph.AddEdge(c, e, false);
            //graph.AddEdge(d, e, false);

            //graph.DFSList(a);
            
            //------------------------BFS Test
            GNode a = graph.AddNode("A", false);
            GNode b = graph.AddNode("B", false);
            GNode c = graph.AddNode("C", false);
            GNode d = graph.AddNode("D", false);
            GNode e = graph.AddNode("E", false);
            GNode f = graph.AddNode("F", false);
            GNode g = graph.AddNode("G", false);

            graph.AddEdge(b, a, false);
            graph.AddEdge(b, c, false);
            graph.AddEdge(a, f, false);
            graph.AddEdge(f, c, false);
            graph.AddEdge(e, c, false);
            graph.AddEdge(c, d, false);
            graph.AddEdge(d, e, false);
            graph.AddEdge(e, g, false);

            graph.BFSList(b);
        }
    }
}

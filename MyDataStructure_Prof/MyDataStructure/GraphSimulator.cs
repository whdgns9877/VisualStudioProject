using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDataStructure
{
	internal class GraphSimulator
	{
		public void Run()
		{
			/*Graph g = new Graph();
			LNode A = g.AddNode(new GraphNodeData_Int() { NumData = 10 });
			LNode B = g.AddNode(new GraphNodeData_Int() { NumData = 20 });
			LNode C = g.AddNode(new GraphNodeData_Int() { NumData = 30 });
			LNode D = g.AddNode(new GraphNodeData_Int() { NumData = 40 });
			LNode E = g.AddNode(new GraphNodeData_Int() { NumData = 50 });


			g.AddEdge(A, B);
			g.AddEdge(A, D);
			g.AddEdge(B, C);
			g.AddEdge(C, D);
			g.AddEdge(D, E);
			g.AddEdge(E, A);

			g.Print();*/

			Graph g = new Graph();
			LNode A = g.AddNode(new GraphNodeData_Name() { Name = "A" });
			LNode B = g.AddNode(new GraphNodeData_Name() { Name = "B" });
			LNode C = g.AddNode(new GraphNodeData_Name() { Name = "C" });
			LNode D = g.AddNode(new GraphNodeData_Name() { Name = "D" });
			LNode E = g.AddNode(new GraphNodeData_Name() { Name = "E" });
			LNode F = g.AddNode(new GraphNodeData_Name() { Name = "F" });


			g.AddEdge(A, F);
			g.AddEdge(A, B);
			g.AddEdge(F, C);
			g.AddEdge(C, E);
			g.AddEdge(C, D);
			g.AddEdge(B, C);
			g.AddEdge(E, D);

			//g.Print();
			///*
			g.TraversalDFS(delegate (LNode node)
			{
				Console.WriteLine($"방문 : {node.data.OutputString()}");
			});//*/

			/*
			g.TraversalBFS(delegate (LNode node)
			{
				Console.WriteLine($"방문 : {node.data.OutputString()}");
			});//*/
		}
	}

	public class GraphNodeData_Int : GraphNodeData
	{
		public int NumData { get; set; }

		public override int CompareTo(INodeData inData)
		{
			GraphNodeData_Int otherData = (GraphNodeData_Int)inData;
			return NumData.CompareTo(otherData.NumData);
		}

		public override void Print()
		{
			//Console.WriteLine($"{NumData}");
			Neighbors.PrintForwardAll(delegate (LNode node)
			{
				Console.WriteLine($"{NumData} - {((GraphNodeData_Int)node.data).NumData}");
			});
		}

		public override string OutputString()
		{
			return $"{NumData}";
		}
	}

	public class GraphNodeData_Name : GraphNodeData
	{
		public string Name { get; set; }

		public override int CompareTo(INodeData inData)
		{
			GraphNodeData_Name otherData = (GraphNodeData_Name)inData;
			return Name.CompareTo(otherData.Name);
		}

		public override void Print()
		{
			//Console.WriteLine($"{NumData}");
			Neighbors.PrintForwardAll(delegate (LNode node)
			{
				Console.WriteLine($"{Name} - {((GraphNodeData_Name)node.data).Name}");
			});
		}

		public override string OutputString()
		{
			return $"{Name}";
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDataStructure
{
	public interface INodeData
	{
		public int CompareTo(INodeData inData);

		public void Print();

		public string OutputString();
	}
}

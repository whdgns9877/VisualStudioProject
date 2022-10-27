using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Tree
{
    internal class TNode
    {
        public TNode left;
        public TNode right;

        public int Value { get; set; }

        public TNode(int value)
        {
            this.Value = value;
        }
    }
}

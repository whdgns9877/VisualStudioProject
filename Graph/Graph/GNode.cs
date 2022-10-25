using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    internal class GNode
    {
        private List<GNode> neighbors;

        public string Name { get; set; }
        public bool IsVisited { get; set; }

        public GNode(string name, bool isVisited)
        {
            this.Name = name;
            this.IsVisited = isVisited;
        }

        public List<GNode> Neighbors
        {
            get
            {
                neighbors = neighbors ?? new List<GNode>();
                return neighbors;
            }
        }
    }
}

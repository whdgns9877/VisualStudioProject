using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    internal class GNode
    {
        // List for adjacentNode of this Node
        private List<GNode> adjacentNode;

        // Properties
        public string Name { get; set; }
        public bool IsVisited { get; set; }

        // Constructor that runs when a node instance is created
        public GNode(string name, bool isVisited)
        {
            this.Name = name;
            this.IsVisited = isVisited;
        }

        // Property about adjcentNodeList
        public List<GNode> AdjacentNode
        {
            get
            {
                // if adjacentNode is not null adjacentNode = adjacentNode
                // else adjacentNode = new List<GNode>()
                adjacentNode = adjacentNode ?? new List<GNode>();
                return adjacentNode;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trie
{
    public class Node
    {
        public Dictionary<char, Node> children = new Dictionary<char, Node>();
        public bool IsLeaf { get; set; }
    }
}

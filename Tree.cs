using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trie
{
    public class Tree
    {
        private Node root = new Node();

        public void Add(String word)
        {
            if( !String.IsNullOrEmpty(word) ) 
                Add(word.ToLower(), level: 0, node: root);
        }

        private void Add(String word, int level, Node node)
        {
            Node child = null;
            node.children.TryGetValue(word[level],out child);

            if (child == null)
            {
                child = new Node();
                node.children[word[level]] = child;                
            }

            if (level == word.Length - 1)            
                child.IsLeaf = true;
            else            
                Add(word, level + 1, child);            
        }

        public List<String> FindWordsThatStartWith(String prefix)
        {
            List<String> result = new List<String>();

            FindWordsThatStartWith(root, prefix, 0, String.Empty, result);

            return result;
        }

        private void FindWordsThatStartWith(Node node, String prefix,int currentLevel, String currentWord , List<String> lstResult)
        {
            if (currentLevel <= prefix.Length - 1)
            {
                Node child = null;
                node.children.TryGetValue(prefix[currentLevel], out child);
                if (child != null)
                {
                    FindWordsThatStartWith(child, prefix, currentLevel + 1, currentWord + prefix[currentLevel], lstResult);
                }
            }
            else
            {
                var enumerator = node.children.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    String newWord = currentWord + enumerator.Current.Key;

                    if (enumerator.Current.Value.IsLeaf)
                    {
                        lstResult.Add(newWord);
                    }

                    FindWordsThatStartWith(enumerator.Current.Value, prefix, currentLevel, newWord, lstResult);
                }
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trie
{
    public class Tree
    {        
        private Node[] root = new Node[26];

        public int TotalWords { get; set; }

        public void Add(String word)
        {            
            if (!String.IsNullOrEmpty(word))
            {
                int rootIndex = (int)word[0] - 97;

                if (root[rootIndex] == null)
                {
                    root[rootIndex] = new Node();
                }

                Add(word.ToLower(), level: 0, node: root[rootIndex]);
            }
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
            {
                child.IsLeaf = true;
                ++TotalWords;
            }
            else
            {
                Add(word, level + 1, child);
            }
        }

        public List<String> FindWordsThatStartWith(String prefix)
        {
            List<String> result = new List<String>();

            if (!String.IsNullOrEmpty(prefix))
            {
                prefix = prefix.ToLower();

                if ((int)prefix[0] >= 97 && (int)prefix[0] <= 97 + 26)
                {
                    int rootIndex = (int)prefix[0] - 'a';
                    FindWordsThatStartWith(root[rootIndex], prefix, 0, String.Empty, result);
                }

            }

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

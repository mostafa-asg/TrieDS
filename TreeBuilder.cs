using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Trie
{
    public class TreeBuilder
    {
        private static Tree tree = new Tree();
        private static String[] words;

        private static String[] LoadWords()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Trie.words.txt";

            List<String> result = new List<string>();
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                String line = null;
                while ((line = reader.ReadLine()) != null)
                {
                    result.Add(line);
                }
            }

            return result.ToArray();
        }

        public static Tree build()
        {
            words = LoadWords();

            Thread[] branchBuilders = new Thread[ Environment.ProcessorCount ];
            for (int i = 0; i < branchBuilders.Length; i++)
            {
                branchBuilders[i] = new Thread(buildBranch);                
            }

            int step = (int)Math.Round( (float)words.Length / Environment.ProcessorCount );
            for (int i = 0; i < branchBuilders.Length ; i++)
            {
                if (i == branchBuilders.Length - 1)
                {
                    branchBuilders[i].Start(new int[] { i*step , words.Length });
                }
                else
                {
                    int start = i * step;
                    int stop = (i + 1) * step;

                    branchBuilders[i].Start(new int[] { start, stop });
                }
            }

            foreach (var thread in branchBuilders)
            {
                thread.Join();
            }

            return tree;
        }

        private static void buildBranch(object startAndStopIndexes)
        {
            int start = ((int[])startAndStopIndexes)[0];
            int stop = ((int[])startAndStopIndexes)[1];

            for (int i = start; i < stop; i++)
            {
                tree.Add(words[i]);                
            }
        }

    }
}

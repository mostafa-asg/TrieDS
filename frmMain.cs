using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trie
{
    public partial class frmMain : Form
    {
        private Tree trieTree = new Tree();

        public frmMain()
        {
            InitializeComponent();
        }     

        private void txtPrefix_TextChanged(object sender, EventArgs e)
        {

            if (!String.IsNullOrEmpty(txtPrefix.Text))
            {
                List<String> result = trieTree.FindWordsThatStartWith(txtPrefix.Text);

                lbWords.Items.Clear();
                
                lbWords.Items.AddRange(result.ToArray());
            }

        }

        private String[] LoadWords()
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

        private void frmMain_Load(object sender, EventArgs e)
        {
            String[] words = LoadWords();
            
            foreach (var word in words)
            {
                trieTree.Add(word);
            }
        }
            
          
    }
}

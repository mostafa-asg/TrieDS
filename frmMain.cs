﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
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

        private void frmMain_Load(object sender, EventArgs e)
        {             
            String[] words = System.IO.File.ReadAllLines("c:\\words.txt");
            
            foreach (var word in words)
            {
                trieTree.Add(word);
            }
        }
            
          
    }
}
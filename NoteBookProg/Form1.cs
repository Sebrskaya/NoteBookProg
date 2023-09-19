using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;

namespace NoteBookProg
{
    public partial class Form1 : Form
    {
        Editor editor = new Editor();
        public Form1()
        {
            InitializeComponent();
        }

        public void newTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.NewDoc();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.OpenDoc();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void closeTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.CloseDoc();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mainPanel.Controls.Add(editor);
            editor.Dock = DockStyle.Fill;
            editor.NewDoc();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.SaveDoc();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.SaveDocAs();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.Exit();
        }
    }
}

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
using System.Xml.Serialization;

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
            editor.resentList.RecentListAddNotify += AddFileNamesToRecentMenu;
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
            editor.resentList.LoadData();
            AddFileNamesToRecentMenu();
            
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.SaveDoc();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.SaveDocAs();
            AddFileNamesToRecentMenu();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.Exit();
        }
        private void AddFileNamesToRecentMenu()
        {
            recentToolStripMenuItem.DropDownItems.Clear();
            for (int i = 0; i < editor.resentList.Count; i++)
            {
                recentToolStripMenuItem.DropDownItems.Add(editor.resentList[i]).Click += new EventHandler(RecentListItem_Click);
            }
        }

        private void RefreshRecentList()
        {

        }
        private void RecentListItem_Click(object sender, EventArgs e)
        {
            editor.resentList.RecentListAddNotify += AddFileNamesToRecentMenu;//обработчик события 
            var recentListItem = (ToolStripMenuItem)sender;
            int index = 0;

            foreach (ToolStripMenuItem item in recentToolStripMenuItem.DropDownItems)
            {
                if (item.Text.Equals(recentListItem.Text))
                {
                    break;
                }
                index++;
            }

            editor.OpenDocByRecentIndex(index);
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            e.Cancel = !editor.Exit();

        }
    }
}

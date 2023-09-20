using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace NoteBookProg
{
    public class Document : TabPage
    {
        public Document() 
        {
            //создание богатого текст бокс
            RichTextBox richTextBox= new RichTextBox();
            richTextBox.Dock = DockStyle.Fill;
            richTextBox.Parent = this;
            richTextBox.Name = "rtb";
        } 
        public string Path { get; set; }
        public bool HasPath 
        { 
            get 
            {
                if (Path != null)
                return  true; else return false;
            } 
        }
        public string ShortName { get; set; }
        public void Open(string FileName)//FileName - путь до файла
        {
            
            Path= FileName;
            ShortName = Path.Substring(1 + Path.LastIndexOf('\\'));
            this.Text = ShortName;
            Stream stream = new FileStream(FileName,FileMode.Open);
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            string fileText = Encoding.Default.GetString(buffer);//DECODE
            if (this.Controls.ContainsKey("rtb"))
            {
                RichTextBox selectedRtb = (RichTextBox)this.Controls["rtb"];
                selectedRtb.Text = fileText;

            }
            stream.Close();
        }
        public void Save()
        {
            Stream stream = new FileStream(Path, FileMode.Create);
            byte[] buffer = Encoding.Default.GetBytes(reciveTextBox.Text);
            stream.Write(buffer, 0, buffer.Length);
            stream.Close();
            reciveTextBox.Modified = false;


        }
        public void SaveAs(string FileName) 
        {
            Stream stream = new FileStream(FileName, FileMode.Create);
            byte[] buffer = Encoding.Default.GetBytes(reciveTextBox.Text);
            stream.Write(buffer, 0, buffer.Length);
            stream.Close();
            Path = FileName;
            ShortName = Path.Substring(1 + Path.LastIndexOf('\\'));
            this.Text = ShortName;
            reciveTextBox.Modified = false;
        }
        public RichTextBox reciveTextBox
        {
            get
            {
                RichTextBox selectedRtb = (RichTextBox)this.Controls["rtb"];
                return selectedRtb;
            }
        }
        
        public bool Modified
        {
            get
            {
                return reciveTextBox.Modified;
            }
        }
    }
}

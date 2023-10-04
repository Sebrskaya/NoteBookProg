using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace NoteBookProg
{
    internal class Editor : TabControl
    {
        public RecentList resentList = new RecentList();
        private Document SelectedDoc// Выбранная вкладка
        {
            get
            {
                return SelectedTab as Document;
            }
        }
        public void NewDoc()
        {
            Document document= new Document();
            this.TabPages.Add(document);
            document.Text = "Untilted.txt";
            this.SelectedTab = document;
        }
        
        public void OpenDoc()
        {
            OpenFileDialog openFileDialog= new OpenFileDialog();//вызов окошка по выбору файла
            openFileDialog.InitialDirectory = "C:\\Users\\egori\\OneDrive\\Рабочий стол";//дефолт путь
            openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*\"\"";//Чёт не работает
            if (openFileDialog.ShowDialog() == DialogResult.OK)//открытие окна для выбора, ок закрывает с результатом
            {
                NewDoc();
                SelectedDoc.Open(openFileDialog.FileName);
            }
                
        }
        public void SaveDoc()
        {
            if (SelectedDoc.Text.Contains("Untilted.txt"))
                SaveDocAs();
            else
            {
                SelectedDoc.Save();
                
            }
                
            
        }
        public void SaveDocAs() 
        {
            
            SaveFileDialog saveFileDialog= new SaveFileDialog();
            saveFileDialog.Filter = "Text Documents|*.txt";
            saveFileDialog.DefaultExt = "txt";
            if(saveFileDialog.ShowDialog()==DialogResult.OK) 
            {
                SelectedDoc.SaveAs(saveFileDialog.FileName);
                
            }
           

        }
        public void CloseDoc()
        {
            if(SelectedDoc.Text != null & SelectedDoc.Modified){//)
                DialogResult result = MessageBox.Show(
                "Хотите ли сохранить " + SelectedDoc.ShortName + " перед закрытием?",
                "Сообщение",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
                if (result == DialogResult.Yes)
                {
                    SaveDoc();
                    this.TabPages.Remove(SelectedTab);
                }
                    
                if (result == DialogResult.No)
                    this.TabPages.Remove(SelectedTab);
            }
            else 
            {
                this.TabPages.Remove(SelectedTab);
            }
        }
        public void Exit()
        {
            while (this.TabPages.Count > 0) 
            {
                this.SelectTab(TabPages[0]);
                CloseDoc();
            }
                Application.Exit();
        }


        public void OpenDocByRecentIndex(int Index)//открывает файл из листа
        {

        }
        public bool DocOpened(String Path)
        {
            return false;
        }
    }
}

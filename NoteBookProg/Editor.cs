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
        // Выбранная вкладка
        private Document SelectedDoc
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
        //CloseActiveDoc();
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
                "Вы действительно хотите закрыть документ не сохранясь?",
                "Сообщение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
                if (result == DialogResult.Yes)
                    this.TabPages.Remove(SelectedTab);
            }
            else 
            {
                this.TabPages.Remove(SelectedTab);
            }
        }
        public void Exit()
        {
            //if все сохранены, то выход
                Application.Exit();
        }
        //OpenDocByRecentIndex(int Index);
        //bool DocOpened(FName);
        //RecentList resentList { get;}
    }
}

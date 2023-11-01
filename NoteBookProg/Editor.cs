using System;
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
            Document document = new Document();
            this.TabPages.Add(document);
            document.Text = "Untilted.txt";
            this.SelectedTab = document;
        }

        public void OpenDoc()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();//вызов окошка по выбору файла
            openFileDialog.InitialDirectory = "C:\\Users\\egori\\OneDrive\\Рабочий стол";//дефолт путь
            openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*\"\"";//Чёт не работает
            if (openFileDialog.ShowDialog() == DialogResult.OK)//открытие окна для выбора, ок закрывает с результатом
            {
                CreateTab(openFileDialog.FileName);
            }

        }

        public void CreateTab( string FileName) 
        {
            //проверка на налисие документа(пройти по всем открытым вкладкам и сравнивать пути)
            foreach (TabPage tab in this.TabPages) 
            {
                if ((tab as Document).Path is null) continue;
                if (FileName == (tab as Document).Path)
                {
                    this.SelectedTab = tab;
                    return;
                }     
            }
            NewDoc();
            SelectedDoc.Open(FileName);
            resentList.Add(FileName);
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

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Documents|*.txt";
            saveFileDialog.DefaultExt = "txt";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SelectedDoc.SaveAs(saveFileDialog.FileName);
                resentList.Add(saveFileDialog.FileName);
            }


        }
        public void CloseDoc()
        {
            if (SelectedDoc == null)
                return;
            if (SelectedDoc.Text != null & SelectedDoc.Modified)
            {//)
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

                if (result == DialogResult.Cancel)
                    throw new CancelException();

               
            }
            else
            {
                this.TabPages.Remove(SelectedTab);
            }
        }
        public bool Exit()
        {
            try
            {
                while (this.TabPages.Count > 0)
                {
                    this.SelectTab(TabPages[0]);
                    CloseDoc();
                }
                Application.Exit();
                resentList.SaveData();
                return true;
            }
            catch (CancelException) { return false; }
            
        }

        public void OpenDocByRecentIndex(int Index)//открывает файл из листа
        {
            CreateTab(resentList[Index]);
        }
        public bool DocOpened(String Path)
        {
            return false;
        }
    }
}

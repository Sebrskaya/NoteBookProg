using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NoteBookProg
{
    internal class RecentList : List<String>
    {
        public delegate void RecentListAddDelegate();
        public RecentListAddDelegate RecentListAddNotify;
        public new void Add(String Path)
        {
            
                if (this.Count < 5)
                {
                    base.Insert(0, Path);
                    RecentListAddNotify?.Invoke();
                
                }
                else
                {
                    base.RemoveAt(4);
                    base.Insert(0, Path);
                }
            }
            
            
            //макс 5 элементов листе
            //если мы открываем файл и он уже есть в списке, то мы его поднимаем на верх 
            
        public void SaveData()
        {
            //записать элементы листа в файл
            Stream stream = new FileStream("Path", FileMode.Create);
            byte[] buffer = Encoding.Default.GetBytes(reciveTextBox.Text);
            stream.Write(buffer, 0, buffer.Length);
            stream.Close();
            reciveTextBox.Modified = false;
        }
        public void LoadData()
        {
            //считывает данные с файла и запичывает в лист
        }

        public void Shuffle(int index)
        {
            base.Insert(0, base[index]);
            
            for(int i = index + 1; i < 5; i++)
            {
                base[i] = base[i + 1];
            }
            base.RemoveAt(5);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing.Drawing2D;

namespace NoteBookProg
{
    internal class RecentList : List<String>
    {
        public delegate void RecentListAddDelegate();
        public RecentListAddDelegate RecentListAddNotify;
        public new void Add(String Path)
        {
            //если в recentList есть Path то Shuffle
            if (this.Contains(Path)) 
            {
                MoveToTop(this.IndexOf(Path));
                return;
            }

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
            Stream stream = new FileStream("E:\\lol.txt", FileMode.Create);
            for(int i = 0; i < this.Count; i++)
            {
                byte[] buffer = Encoding.Default.GetBytes(base[i]);
                stream.Write(buffer, 0, buffer.Length);
                if (i < this.Count - 1)
                    stream.WriteByte(0x0D);
            }
            stream.Close();
        } 



        public void LoadData()
        {
            //считывает данные с файла и запичывает в лист
            string Line = null;
            Stream stream = new FileStream("E:\\lol.txt", FileMode.Open);
            if (stream.Length != 0)
            {
                byte[] buffer = new byte[stream.Length];
                // Считывание данных
                stream.Read(buffer, 0, buffer.Length);
                // Декодирование
                string fileText = Encoding.Default.GetString(buffer);
                string[] paths = fileText.Split('\r');

                for (int i = 0; i < paths.Length; i++)
                {
                    Line = paths[i];
                    base.Add(Line);
                }
            }
            stream.Close();

        }

        public void MoveToTop(int index)
        {
            if (index < 0 || index >= base.Count)
            {
                // Обработайте ошибку индекса, если это необходимо
                throw new ArgumentOutOfRangeException("index");
            }

            string element = base[index]; // Сохраняем элемент, который будем перемещать
            base.RemoveAt(index);     // Удаляем элемент из исходной позиции
            base.Insert(0, element);  // Вставляем элемент в начало списка
        }
    }
}

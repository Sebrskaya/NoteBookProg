using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBookProg
{
    internal class RecentList : List<String>
    {
        public delegate void RecentListAddDelegate();
        public RecentListAddDelegate RecentListAddNotify;
        public new void Add(String Path)
        {
            RecentListAddNotify?.Invoke();
            //макс 5 элементов листе
            //если мы открываем файл и он уже есть в списке, то мы его поднимаем на верх 

        }
        public void SaveData()
        {
            //записать элементы листа в файл
        }
        public void LoadData()
        {
            //считывает данные с файла и запичывает в лист
        }
    }
}

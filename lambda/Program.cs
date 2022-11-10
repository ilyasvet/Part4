using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lambda
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int count = 0;
            var list = new ObservableCollection<int>();
            list.CollectionChanged += delegate(object sender, NotifyCollectionChangedEventArgs e) //или так, или вообще без аргументов
            {
                count++; //есть доступ к локальным переменным
            };
            list.CollectionChanged += delegate
            {
                count++;
            };
            
            for (int i = 0; i < 5; i++)
            {
                list.Add(i);
            }
            Console.WriteLine(count); //будет 10, так как вызывались 2 метода


  

            Console.WriteLine("\n\n=============Lambda==========\n");
            var list2 = new List<string>();
            list2.Add("hello");
            list2.Add("world");
            var list3 = list2.FindAll((w)=>w.Contains("h"));//w имеет тип string (параметр предиката)
            //FindAll принимает предикат с параметром типа таким же, как у объекта, его вызывающего. То есть string
            //Каждый элемент коллекции передаётся методу, указанному в аргументах
            var list4 = list2.FindAll(IsWriteSpace); //list4 пустой
            foreach (var item in list3)
            {
                Console.WriteLine(item);
            }
        }
        static bool IsWriteSpace(string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }
    }
}

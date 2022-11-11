using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Methods
{
    static class MyExtensions
    {
        // Этот метод позволяет объекту любого типа
        // отобразить сборку, в которой он определен.
        public static void PrintAndBeep(this IEnumerable values)
        {
            foreach (var value in values)
            {
                Console.WriteLine(value.ToString());
            }
            Console.Beep();
        }
        public static void DisplayDefiningAssembly(this object obj)
        {
            Console.WriteLine("{0} lives here: => {1}\n", obj.GetType().Name,
            Assembly.GetAssembly(obj.GetType()).GetName().Name);
        }
        // Этот метод позволяет любому целочисленному значению изменить порядок
        // следования десятичных цифр на обратный. Например, для 56 возвратится 65.
        public static int ReverseDigits(this int i)
        {
            // Транслировать int в string и затем получить все его символы,
            char[] digits = i.ToString().ToCharArray();
            // Изменить порядок следования элементов массива.
            Array.Reverse(digits);
            // Поместить обратно в строку,
            string newDigits = new string(digits);
            // Возвратить модифицированную строку как int.
            return int.Parse(newDigits);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            int i = 24143;
            Console.WriteLine(i.ReverseDigits());
            i.DisplayDefiningAssembly();
            var l = new List<int>();
            l = l.Append(1).ToList();//Append тоже метод расширения, определён в статическом классе Enumerable
            l = Enumerable.Append(l, 12).ToList(); //Можно вызывать и напрямую
            Console.WriteLine(l[1]);
            l.PrintAndBeep();
            //List реализует IEnumerable!
        }
    }
}

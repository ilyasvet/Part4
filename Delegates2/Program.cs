using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates2
{
    internal class Program
    {
        public delegate void MyGenericDelegate<T>(T arg);
        //Обобщённый делегат, возвращающий void
        static void Main(string[] args)
        {
            MyGenericDelegate<string> genericDelegate = StringArg;
            Console.WriteLine("=========irst=========");
            genericDelegate("Hello");
            Console.WriteLine("\nAdd second method\n");
            genericDelegate += StringArg2;
            genericDelegate("Hello2");
            MyGenericDelegate<int> myGeneric = IntArg;
            myGeneric(11312);

            Console.WriteLine("\n\n-------Action-------\n");
            Action<string, ConsoleColor, int> act = DisplayMessage;
            act("hello", ConsoleColor.Red, 5);

            Console.WriteLine("\n\n-------Func-------\n");
            Func<int, int, int> func = Add; //Последний параметр - возвращаемое значение
            Console.WriteLine(func(10,54345));
        }


        static int Add(int x, int y)
        {
            return x + y;
        }
        static void DisplayMessage(string msg, ConsoleColor txtColor, int printCount)
        {
            // Установить цвет текста консоли.
            ConsoleColor previous = Console.ForegroundColor;
            Console.ForegroundColor = txtColor;
            for (int i = 0; i < printCount; i++)
            {
                Console.WriteLine(msg);
            }
            // Восстановить цвет.
            Console.ForegroundColor = previous;
        }

        static void IntArg(int arg)
        {
            Console.WriteLine("This is number " + arg);
        }
        static void StringArg(string arg)
        {
            Console.WriteLine("string arg " + arg);
        }
        static void StringArg2(string arg)
        {
            Console.WriteLine("the second arg " + arg);
        }
    }
}

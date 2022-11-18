using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnonimusTypes
{
    internal class Program
    {
        static void makeAnyCar()
        {
            var car = new { Name = "bmw", speed = 50 };
            Console.WriteLine(car.ToString()); //уже переопределён
            var car2 = car;
            Console.WriteLine(car.Equals(car2));

            Console.WriteLine(car == car2); // cсылки одинаковые
            var car3 = new { Name = "bmw", speed = 50 };

            Console.WriteLine(car.Equals(car3)); //сравнивает значения

            Console.WriteLine(car == car3); // сравнивает ссылки
            var justvar = new
            {
                var = 999,
                car = car2,
            };
            Console.WriteLine(justvar);
        }

        class Point
        {
            public int x;
            public int y;
        }
        static void Main(string[] args)
        {
            makeAnyCar();
            Console.WriteLine("\n\n=====Pointers======\n");
            unsafe
            {
                int i = 10;
                int* iPt = &i;
                Console.WriteLine((int)&iPt); //адрес указателя

                Console.WriteLine((int)iPt); //адрес переменной
                char* p = stackalloc char[256]; //массив будет храниться в стеке, а значит не будет подвергаться
                for (int k = 0; k < 256; k++) // сборке мусора, но очистится при завершении метода.
                    p[k] = (char)k;
                Point p1 = new Point();
                fixed (int* iPtr = &p1.x)
                {
                    //сдесь использовать взаимодействие с членами ссылочных типов чере указатель
                }
                
            }

        }
    }
}

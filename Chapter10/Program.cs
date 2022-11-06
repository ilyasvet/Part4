using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter10
{
    class Car
    {
        // Данные состояния,
        public int CurrentSpeed { get; set; }
        public int MaxSpeed { get; set; } = 100;
        public string PetName { get; set; }
        // Исправен ли автомобиль?
        private bool carlsDead;
        // Конструкторы класса,
        public Car() { }
        public Car(string name, int maxSp, int currSp)
        {
            CurrentSpeed = currSp;
            MaxSpeed = maxSp;
            PetName = name;
        }

        // 1. Определить тип делегата.
        public delegate void CarEngineHandler(string msgForCaller);
        //2. Определить переменную-член этого типа делегата.
        private CarEngineHandler listOfHandlers;
        // 3. Добавить регистрационную функцию для вызывающего кода.
        public void RegisterWithCarEngine(CarEngineHandler methodToCall)
        {
            listOfHandlers += methodToCall;
        }
        //Добавляет методы в делегат 
        public void UnRegisterWithCarEngine(CarEngineHandler methodToCall)
        {
            listOfHandlers -= methodToCall;
        }
        //Теперь можно удалять методы из делегата

        public void Accelerate(int delta)
        {
            if (carlsDead)
            {
                if (listOfHandlers != null)
                {
                    listOfHandlers("Sorry, man, car is dead(");
                }
            }
            else
            {
                CurrentSpeed += delta;
                if (CurrentSpeed > MaxSpeed && listOfHandlers != null)
                {
                    carlsDead = true;
                }
                else if (MaxSpeed - CurrentSpeed <= 20 && listOfHandlers != null)
                {
                    listOfHandlers("Too speedly, man, carefully");
                }   
            }
        }
    }
    internal class Program
    {
        // Этот делегат может указывать на любой метод, который принимает
        // два целочисленных значения и возвращает целочисленное значение,
        public delegate int BinaryOp(int x, int y);
        //Мы создали, тип, который неявно является MulticastDelegate
        int Add(int x, int y)
        {
            return x + y;
        }
        int Sub(int x, int y)
        {
            return x - y;
        }
        static void Main(string[] args)
        {
            Program p = new Program();
            BinaryOp it = new BinaryOp(p.Add);
            it += p.Sub;
            Console.WriteLine(it(10,10)); //Выполнит оба метода, но вернёт последнее значение

            Console.WriteLine(it.GetInvocationList()[0].DynamicInvoke(10, 10));//Выполнит первый метод 
            Console.WriteLine(it(10, 10)); //Выполнит оба метода, но вернёт последнее значение
            Console.WriteLine(it.Target); //null

            Console.WriteLine(it.Method); //Sub
            BinaryOp it2 = BinaryOp.Combine(it, new BinaryOp(p.Add)) as BinaryOp; //BinaryOp является Delegate
            Console.WriteLine(it2.Method);//Add
            Console.WriteLine("\n\n========it.GetInvocationList()=====");
            foreach (var item in it.GetInvocationList())
            {
                Console.WriteLine(item.Method);
            }
            Console.WriteLine("\n\n========it2.GetInvocationList()=====");
            foreach (var item in it2.GetInvocationList())
            {
                Console.WriteLine(item.Method);
            }
            Console.WriteLine("\n\n========it2.Remove(Add)=====");
            it2 -= p.Add; //Удаляет последний Add
            Console.WriteLine("\n\n========it2.GetInvocationList()=====");
            foreach (var item in it2.GetInvocationList())
            {
                Console.WriteLine(item.Method);
            }

            Console.WriteLine("Class ownew method is " + it2.Method.DeclaringType);

            Console.WriteLine("\n\n\n------------------Car---------------------");

            Car car = new Car("new", 100, 0);
            car.RegisterWithCarEngine(MessageEngine); //Ставим функцию, которая будет выполняться
            Car.CarEngineHandler f2 = new Car.CarEngineHandler(MessageEngine2);
            car.RegisterWithCarEngine(f2);//Теперь будет выполняться 2 функции
            //При вызове делегата.
            //Таким образом через делегат мы посылаем функции внутрь класса.
            for (int i = 0; i < 12; i++)
            {
                car.Accelerate(10);
            }
            car.UnRegisterWithCarEngine(f2); //Теперь только 2 сообщение будет
            //Таким образом чере делегат можно немного менять поведение методов класса из вне.
            for (int i = 0; i < 3; i++)
            {
                car.Accelerate(10);
            }
            Console.WriteLine("\n\n\n---------- ----------\n");
        }
        static void MessageEngine(string msg)
        {
            Console.WriteLine("%%%%%%%%%MessageCar%%%%%%%%%%%");
            Console.WriteLine(msg);
            Console.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
        }
        static void MessageEngine2(string msg)
        {
            Console.WriteLine(msg.ToUpper());
        }
    }
}

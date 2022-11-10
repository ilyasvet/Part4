using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Events
{

    class CarEventArgs : EventArgs //тип, содержащий аргументы события
    {
        public readonly string Message;
        public CarEventArgs(string message)
        {
            Message = message;
        }
    }
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
        //public delegate void CarEngineHandler(object sender, CarEventArgs e); //шаблонный обработчик событий
        //2. Определить переменную-член этого типа делегата.
        //public event CarEngineHandler Exploited;
        //public static event CarEngineHandler AboutToBlow;
        public event EventHandler<CarEventArgs> Exploited;
        public static event EventHandler<CarEventArgs> AboutToBlow;

        public void Accelerate(int delta)
        {
            if (carlsDead)
            {
                Exploited?.Invoke(this, new CarEventArgs("Sorry, man, car is dead("));
            }
            else
            {
                CurrentSpeed += delta;
                if (CurrentSpeed > MaxSpeed)
                {
                    carlsDead = true;
                }
                else if (MaxSpeed - CurrentSpeed <= 20)
                {
                    AboutToBlow?.Invoke(this, new CarEventArgs("Too speedly, man, carefully")); //используем null условную операцию вместо if
                }
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Car car = new Car("car", 100, 0);
            car.Exploited += Car_Exploited;
            Car.AboutToBlow += Car_AboutToBlow;
            for (int i = 0; i < 10; i++)
            {
                car.Accelerate(15);
            }
            Car car2 = new Car("car2", 100, 0); // не будет выводить сообщения, так как слушатели события не определены
            for (int i = 0; i < 10; i++) // для этого объекта
            {                               //Тогда мы сделали событие статическим, теперь оно доступно всем объектам
                car2.Accelerate(15);
            }

        }
        private static void Car_AboutToBlow(object sender, CarEventArgs e)
        {
            Console.WriteLine($"Message frow {(sender as Car).PetName} : {e.Message}");
        }

        private static void Car_Exploited(object sender, CarEventArgs e)
        {
            Console.WriteLine($"Message frow {(sender as Car).PetName} : {e.Message}");
        }
    }
}

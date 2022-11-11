using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Expansion
{
    interface IIndexer
    {
        Person this[DateTimeKind kind] { get; set; } //параметры любого типа
        Person this[DateTime time] { get; set; }
        Person this[Person person] { get; set; }
    }
    class Person
    {
        public string Name { get; set; }
        public string Surame { get; set; }
        public int Age { get; set; }
        public Person(string name, string surname, int age)
        {
            Name = name;
            Age = age;
            Surame = surname;
        }
    }
    class ListOfPerson: IIndexer
    {
        private List<Person> list;
        public ListOfPerson(int c)
        {
            list = new List<Person>(c);
        }
        public Person this[int index]
        {
            get { return list[index]; }
            set { list.Insert(index, value); }
        }
        public Person this[string surname]
        {
            get => list.FirstOrDefault(x => x.Surame == surname);
            set 
            {
                list[list.IndexOf(list.FirstOrDefault(x => x.Surame == surname))] = value;
            }
        }
        //не имеет смысла, просто ради проверки возможности
        public Person this[DateTimeKind kind]
        {
            get
            {
                if (kind == DateTimeKind.Utc)
                {
                    return list[0];
                }
                return list[1];
            }
            set => throw new NotImplementedException();
        }
        public Person this[DateTime time]
        {
            get
            {
                if (time == DateTime.MinValue)
                {
                    return list[1];
                }
                return list[2];
            }
            set => throw new NotImplementedException();
        }
        public Person this[Person person]
        {
            get => list.First();
            set => list.Add(person);
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            var MyPeople = new ListOfPerson(10);
            MyPeople[0] = new Person("Kirill", "Kulyai", 113);
            MyPeople[1] = new Person("Vasya", "Pupkin", 112);
            MyPeople[2] = new Person("Petya", "Krilov", 114); //Вставка по индексу нового человека 
            Console.WriteLine(MyPeople["Kulyai"].Name);
            MyPeople["Pupkin"] = new Person("Pep", "Ily", 114); //Заменяем пупкина на нового 
            Console.WriteLine(MyPeople["Ily"].Name);
            Console.WriteLine(MyPeople[DateTimeKind.Utc].Name);

            Console.WriteLine(MyPeople[DateTime.MinValue].Name);
            var person = new Person("vas", "pes", 17);
            MyPeople[person] = null; //На самом деле тут добавляется person в список 
            Console.WriteLine(MyPeople[3].Name);

            Console.WriteLine("\n\n=======DataTable=======\n");
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("Surname");
            dataTable.Columns.Add("Age");
            for (int i = 0; i < 4; i++)
            {
                dataTable.Rows.Add(MyPeople[i].Name, MyPeople[i].Surame, MyPeople[i].Age);
            }
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Console.WriteLine(dataTable.Rows[i][0].ToString() + " " + dataTable.Rows[i][1].ToString());
            }

            Console.WriteLine("\n\n=======Operators=======\n");

        }
    }
}

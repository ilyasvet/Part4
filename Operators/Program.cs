using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operators
{
    class Point
    {
        public double X { get; set; }
        public double Y { get; set; }
        public static Point operator+ (Point p1, Point p2)
        {
            return new Point() { X = p2.X + p1.X, Y = p2.Y + p1.Y };
        }
        public static Point operator- (Point p1, Point p2)
        {
            return new Point() { X = p1.X - p2.X, Y = p1.Y - p2.Y };
        }
        //Операторы + и - возвращают новый объект
        public static Point operator ++(Point p1)
        {
            p1.X++;
            p1.Y++;
            return p1;
        }
        public static Point operator --(Point p1)
        {
            p1.X--;
            p1.Y--;
            return p1;
        }
        public static bool operator ==(Point p1, Point p2)
        {
            return p1.X == p2.X && p1.Y == p2.Y;
        }
        public static bool operator !=(Point p1, Point p2)
        {
            return p1.X != p2.X || p1.Y != p2.Y;
        }
        public static bool operator <(Point p1, Point p2)
        {
            return p1.X + p1.Y < p2.X + p2.Y;
        }
        public static bool operator >(Point p1, Point p2)
        {
            return p1.X + p1.Y > p2.X + p2.Y;
        }
        public override bool Equals(object obj)
        {
            return this == obj as Point;
        }
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
        public override string ToString()
        {
            return $"X = {X}, Y = {Y}";
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Point p1 = new Point() { X = 132, Y = 12 };
            Point p2 = p1 + p1; //Произошно копирование, теперь p1 и p2 это разные объекты
            p2.X = 100;

            Console.WriteLine(p1);
            Console.WriteLine(p2);
            p1++;++p1;
            p2++;++p2;
            Console.WriteLine(p1);
            Console.WriteLine(p2);
            p1--; --p1;
            p2--; --p2;
            Console.WriteLine(p1);
            Console.WriteLine(p2);

            Console.WriteLine($"p1 > p2: {p1>p2}");

            Console.WriteLine($"p1 < p2: {p1 < p2}");
            Console.WriteLine($"p1 == p2: {p1 == p2}");

            Console.WriteLine($"p1 != p2: {p1 != p2}");
            Point p3= new Point();
            p3 += p1;
            p1.X = 1144;
            Console.WriteLine(p3); //p3 совершенно другой объект, так как было копирование.

        }
    }
}

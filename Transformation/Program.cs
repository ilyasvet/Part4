using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transformation;

namespace Transformation
{
    public struct Rectangle
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Rectangle(int w, int h) : this() //необходимо для инициализации поддерживающих полей для автосвойств
        {
            Width = w; Height = h;
        }
        public void Draw()
        {
            for (int i = 0; i < Height; i ++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
        }
        public static implicit operator Rectangle(Square s)
        {
            return new Rectangle(s.Length, s.Length * 2);
        }
        public override string ToString() => $"[Width = {Width}; Height = {Height}]";
}
    public struct Square
    {
        public int Length { get; set; }
        public Square(int i) : this()
        {
            Length = i;
        }
        public void Draw()
        {
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Length; j++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
        }
        public override string ToString() => $"[Length] = {Length}";
        // Rectangle можно явно преобразовывать в Square,
        public static explicit operator Square(Rectangle r)
        {
            Square s = new Square { Length = r.Height };
            return s;
        }
        public static explicit operator Square(int i)
        {
            Square s = new Square(i);
            return s;
        }
        public static explicit operator int(Square s)
        {
            return s.Length;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Rectangle r = new Rectangle(10,2);
            Console.WriteLine(r);
            r.Draw();
            DrawSquare((Square)r);
            DrawSquare((Square)10); //Преобразовываем int в квадрат
            Console.WriteLine((int)(Square)r); //И наоборот

            Square s = new Square(10);
            Rectangle rr = (Rectangle)s; //Явное, но определено только неявное.
            Rectangle rrr = s; // Неявное. 
            Console.WriteLine(rr);
            Console.WriteLine(rrr);
        }
        static void DrawSquare(Square sq)
        {
            Console.WriteLine(sq);
            sq.Draw();
        }
    }
}

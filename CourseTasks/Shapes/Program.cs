using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    class Program
    {
        static void Main(string[] args)
        {
            var figures = new IShape[]
            {
                new Circle(32d),
                new Rectangle(12d, 8d),
                new Square(23d),
                new Triangle(2d, 3d, 1d, 7d, 8d, 4d),
                new Rectangle(1d, 2d),
                new Circle(9d),
                new Square(4d)
            };

            Console.WriteLine("Фигуры");
            Console.WriteLine();

            foreach (var shape in figures)
            {
                WriteInfo(shape);
            }

            Console.ReadKey();
            Console.Clear();

            Array.Sort(figures, new ShapeComparerArea());
            Console.WriteLine("Фигура с максимальной площадью");
            WriteInfo(figures[figures.Length - 1]);

            Array.Sort(figures, new ShapeComparerPerimeter());
            Console.WriteLine("Фигура со вторым по величине периметром");
            WriteInfo(figures[1]);



            Console.ReadKey();
        }

        private static void WriteInfo(IShape shape)
        {
            Console.WriteLine(shape);
            Console.WriteLine("Ширина {0}; Высота {1}; Периметр {2}; Площадь {3}", shape.getWidth(), shape.getHeight(), shape.getPerimeter(), shape.getArea());
            Console.WriteLine();
        }

        
    }
}

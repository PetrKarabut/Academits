using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vectors
{
    class Program
    {
        static void Main(string[] args)
        {
            var vector1 = new Vector(3);
            var array = new[] { 5d, -15d, 6d, 8.56d, 57d };
            var vector2 = new Vector(array);
            var vector3 = new Vector(3, array);
            var vector4 = new Vector(vector3);

            WriteInfo(vector1);
            WriteInfo(vector2);
            WriteInfo(vector3);
            WriteInfo(vector4);

            Console.ReadKey();
            Console.Clear();

            var vector5 = new Vector(new[] { 1d, -8d, 7d });
            var vector6 = new Vector(new[] { 2d, 4d, -12d, 6 });

            WriteInfo(vector5);
            WriteInfo(vector6);

            Console.WriteLine();

            Console.WriteLine("Сумма: {0}", Vector.Sum(vector5, vector6));
            Console.WriteLine("Разность: {0}", Vector.Difference(vector5, vector6));
            Console.WriteLine("Скалярное произведение: {0}", Vector.Dot(vector5, vector6));

            Console.ReadKey();
        }

        private static void WriteInfo(Vector vector)
        {
            Console.WriteLine();
            Console.WriteLine("Вектор: {0}", vector);
            Console.WriteLine("Размерность: {0}", vector.Size);
            Console.WriteLine("Длина: {0}", vector.Magnitude);
            Console.WriteLine("Обратный вектор: {0}", vector.Opposite);
        }
    }
}

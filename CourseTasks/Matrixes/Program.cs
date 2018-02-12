using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vectors;

namespace Matrixes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Проверка конструкторов");
            Console.WriteLine();

            var width = 5;
            var height = 2;
            Console.WriteLine("Пустая матрица размером {0} на {1}", width, height);
            var matrix = new Matrix(width, height);
            Console.WriteLine(matrix);
            Console.WriteLine();

            Console.WriteLine("Задан массив:");
            var array = new[,] { { 3d, 7d, 8d }, { 6d, 9d, 5d } };
            WriteMatrix(array);
            Console.WriteLine();
            Console.WriteLine("Из него получается матрица:");
            matrix = new Matrix(array);
            Console.WriteLine(matrix);
            Console.WriteLine();

            Console.WriteLine("Копия матрицы:");
            var copy = new Matrix(matrix);
            Console.WriteLine(copy);
            Console.WriteLine();

            var vector1 = new Vector(new[] { 3d, 5d, 8d });
            var vector2 = new Vector(new[] { 2d, 1d, 6d });
            Console.WriteLine("Даны два вектора {0} и {1}", vector1, vector2);
            Console.WriteLine("Из них получается матрица:");
            matrix = new Matrix(new[] { vector1, vector2 });
            Console.WriteLine(matrix);



            Console.ReadKey();
        }

        private static void WriteMatrix(double[,] matrix)
        {
            for (var row = 0; row < matrix.GetLength(0); row++)
            {
                for (var column = 0; column < matrix.GetLength(1); column++)
                {
                    Console.Write(matrix[row, column] + "  ");
                }

                Console.WriteLine();
            }
        }
    }
}

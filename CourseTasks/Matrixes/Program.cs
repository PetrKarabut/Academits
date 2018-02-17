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
            var vector2 = new Vector(new[] { 2d, 1d, 6d, 8d });
            Console.WriteLine("Даны два вектора {0} и {1}", vector1, vector2);
            Console.WriteLine("Из них получается матрица:");
            matrix = new Matrix(new[] { vector1, vector2 });
            Console.WriteLine(matrix);

            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Проверка вектор-стобцов и вектор-строк");
            Console.WriteLine();
            array = new[,] { { 1d, 2d, 4d, 1d }, { 2d, 3d, 0d, 9d }, { 0d, 1d, 7d, 6d }, { 2d, 5d, 7d, 8d }, { 6d, 8d, 5d, 1d } };
            matrix = new Matrix(array);
            Console.WriteLine("Задана матрица:");
            WriteMatrix(matrix);

            Console.WriteLine();
            Console.WriteLine("Ширина: {0}, Высота {1}", matrix.Width, matrix.Height);
            Console.WriteLine();

            var newRow = new Vector(new[] { 9d, 8d, 0d, 4d });
            Console.WriteLine("Вторая строка: {0}, заменим ее на {1}, получим:", matrix.GetRow(1), newRow);
            matrix.SetRow(1, newRow);
            WriteMatrix(matrix);

            Console.WriteLine();
            Console.WriteLine("вот третий столбец: {0}", matrix.GetColumn(2));

            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Проверка транспонирования матрицы");
            Console.WriteLine();
            array = new[,] { { 2d, 6d, 4d, 0d }, { 9d, 7d, 0d, 7d }, { 0d, 6d, 8d, 8d }, { 1d, 1d, 7d, 3d }, { 3d, 8d, 3d, 1d } };
            matrix = new Matrix(array);
            Console.WriteLine("Задана матрица:");
            WriteMatrix(matrix);
            Console.WriteLine();
            matrix.Transpose();
            Console.WriteLine("Транспонированая матрица:");
            WriteMatrix(matrix);

            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Проверка умножения матрицы на число и вектор");
            Console.WriteLine();
            array = new[,] { { 2d, 3d, 2d, 0d }, { 4d, 3d, 0d, 2d }, { 0d, 1d, 1d, 4d }, { 1d, 2d, 2d, 3d } };
            matrix = new Matrix(array);
            Console.WriteLine("Задана матрица:");
            WriteMatrix(matrix);
            Console.WriteLine();

            Console.WriteLine("Умножив на 2 получим:");
            matrix.Multiply(2d);
            WriteMatrix(matrix);
            Console.WriteLine();

            var vector = new Vector(new[] { 1d, 0d, 0d, 2d });
            Console.WriteLine("Умножив справа на вектор-столбец {0} получим вектор-столбец {1}", vector, matrix.GetMultiplicationByColumn(vector));

            vector = new Vector(new[] { 3d, 0d, 2d, 1d });
            Console.WriteLine("Умножив слева на вектор-строку {0} получим вектор-строку {1}", vector, matrix.GetMultiplicationByRow(vector));

            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Проверка вычисления определителя");
            Console.WriteLine();
            Console.WriteLine("Задана матрица:");
            array = new[,] { { -1d, 3d, 2d, -3d }, { 4d, -2d, 5d, 1d }, { -5d, 0d, -4d, 0d }, { 9d, 7d, 8d, -7d } };
            matrix = new Matrix(array);
            WriteMatrix(matrix);
            Console.WriteLine();
            Console.WriteLine("Ее определитель равен {0}", matrix.GetDeterminant());

            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Проверка умножения матриц");
            Console.WriteLine();
            Console.WriteLine("Задана матрица 1:");
            var matrix1 = new Matrix(new[,] { { 1d, 3d, 0d }, { 2d, 0d, 1d }, { 0d, 0d, 1d } });
            WriteMatrix(matrix1);

            Console.WriteLine();
            Console.WriteLine("Задана матрица 2:");
            var matrix2 = new Matrix(new[,] { { 2d, 0d, 0d }, { 1d, 2d, 1d }, { 2d, 0d, 1d } });
            WriteMatrix(matrix2);

            Console.WriteLine();
            Console.WriteLine("Результат умножения:");
            WriteMatrix(Matrix.Multiplication(matrix1, matrix2));

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

        private static void WriteMatrix(Matrix matrix)
        {
            for (var i = 0; i < matrix.Height; i++)
            {
                Console.WriteLine(matrix.GetRow(i));
            }
        }
    }
}

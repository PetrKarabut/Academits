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
            Console.WriteLine("ПРОВЕРКА КОНСТРУКТОРОВ");
            Console.WriteLine();

            Console.WriteLine("Пытаемся создать вектор нулевой размерности");

            try
            {
                var wrong = new Vector(0);
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка: " + e.Message);
            }

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

            Console.WriteLine("ПРОВЕРКА СТАТИЧЕСКИХ МЕТОДОВ");
            Console.WriteLine();

            var vector5 = new Vector(new[] { 1d, -8d, 7d });
            var vector6 = new Vector(new[] { 2d, 4d, -12d, 6d });

            Console.WriteLine("Первый вектор: {0}", vector5);
            Console.WriteLine("Второй вектор: {0}", vector6);

            Console.WriteLine();

            Console.WriteLine("Сумма: {0}", Vector.Sum(vector5, vector6));
            Console.WriteLine("Разность: {0}", Vector.Difference(vector5, vector6));
            Console.WriteLine("Скалярное произведение: {0}", Vector.Dot(vector5, vector6));

            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("ПРОВЕРКА НЕСТАТИЧЕСКИХ МЕТОДОВ");
            Console.WriteLine();

            var currentVector = new Vector(new[] { 1d, 3d, -7d });
            Console.WriteLine("Начальный вектор: {0}", currentVector);

            var plusVector = new Vector(new[] { 1d, 5d });
            currentVector.Plus(plusVector);
            Console.WriteLine("После сложения с вектором {0} получится {1}", plusVector, currentVector);

            var minusVector = new Vector(new[] { 6d, 2d, -9d, 7d });
            currentVector.Minus(minusVector);
            Console.WriteLine("После вычитания вектора {0} получится {1}", minusVector, currentVector);

            var x = 2d;
            currentVector.Multiply(x);
            Console.WriteLine("После умножения на {0} получится {1}", x, currentVector);

            Console.ReadKey();
        }

        private static void WriteInfo(Vector vector)
        {
            Console.WriteLine();
            Console.WriteLine("Вектор: {0}", vector);
            Console.WriteLine("Размерность: {0}", vector.Size);
            Console.WriteLine("Длина: {0}", vector.Magnitude);
            Console.WriteLine("Хэш-код: {0}", vector.GetHashCode());
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ranges
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите начало первого интервала");
            var from1 = double.Parse(Console.ReadLine());

            Console.WriteLine("Введите конец первого интервала");
            var to1 = double.Parse(Console.ReadLine());

            Console.WriteLine("Введите начало второго интервала");
            var from2 = double.Parse(Console.ReadLine());

            Console.WriteLine("Введите конец второго интервала");
            var to2 = double.Parse(Console.ReadLine());
            Console.WriteLine();

            var range1 = new Range(from1, to1);
            var range2 = new Range(from2, to2);

            var intersection = range1.GetIntersection(range2);
            if (intersection != null)
            {
                Console.WriteLine("Пересечение: {0}", intersection.Note);
            }
            else
            {
                Console.WriteLine("Интервалы не пересекаются");
            }

            Console.WriteLine("Объединение: {0}", Range.GetNotes(range1.GetAssociation(range2)));

            Console.WriteLine("Разность: {0}", Range.GetNotes(range1.GetDifference(range2)));

            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ArrayListHome
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = "file.txt";

            try
            {
                foreach (var s in ReadFileStrings(path))
                {
                    Console.WriteLine(s);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Не найден файл с имененм {0}", path);
            }

            Console.WriteLine();

            var list = new List<int> { 0, 3, 7, 5, 6, 9, 8, 10, 3, 7, 14, 16, 19, 20, 32, 31 };
            Console.WriteLine(string.Join(" ", list));

            DeleteEven(list);
            Console.WriteLine(string.Join(" ", list));

            list = new List<int> { 1, 5, 2, 1, 3, 5 };

            Console.WriteLine();
            Console.WriteLine(string.Join(" ", GetListWithoutRepeats(list)));
            Console.ReadKey();
        }


        private static List<string> ReadFileStrings(string path)
        {
            var list = new List<string>();

            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    list.Add(reader.ReadLine());
                }
            }

            return list;
        }


        private static void DeleteEven(List<int> numbers)
        {
            for (var i = 0; i < numbers.Count; i++)
            {
                if (numbers[i] % 2 == 0)
                {
                    numbers.Remove(numbers[i]);
                    i--;
                }
            }
        }


        private static List<int> GetListWithoutRepeats(List<int> original)
        {
            var list = new List<int>();

            foreach (var n in original)
            {
                if (!list.Contains(n))
                {
                    list.Add(n);
                }
            }

            return list;
        }
    }
}

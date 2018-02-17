using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lists
{
    class Program
    {
        static void Main(string[] args)
        {
            var size = 7;
            var range = 99;

            var array = new string[size];

            var random = new Random();
            for (var i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(-range, range).ToString();
            }

            var list = new SimplyConnectedList<string>();
            list.InsertInBegining(array[0]);
            for (var i = 1; i < array.Length; i++)
            {
                list.Insert(array[i], i);
            }

            Console.WriteLine("задан односвязный список: {0}", list);
            Console.WriteLine();

            Console.WriteLine("длина списка {0}; первый элемент {1}; второй элемент {2}", list.Count, list.First, list.GetValue(1));
            Console.WriteLine();
            Console.WriteLine("после замены четвертого элемента равного {0} на {1} получим", list.SetValue(3, "N"), "N");
            Console.WriteLine(list);
            Console.WriteLine();
            Console.WriteLine("после удаления пятого элемента равного {0} получим", list.RemoveAt(4));
            Console.WriteLine(list);
            Console.WriteLine();
            Console.WriteLine("после добавления в начало элемента равного {0} получим", "B");
            list.InsertInBegining("B");
            Console.WriteLine(list);
            Console.WriteLine();
            Console.WriteLine("после добавления по номеру 3 элемента равного {0} получим", "I");
            list.Insert("I", 2);
            Console.WriteLine(list);
            Console.WriteLine();
            Console.WriteLine("после удаления элемента равного {0} получим", "I");
            list.Delete("I");
            Console.WriteLine(list);
            Console.WriteLine();
            Console.WriteLine("после удаления первого элемента равного {0} получим", list.DeleteFirst());
            Console.WriteLine(list);
            Console.WriteLine();
            Console.WriteLine("после разворота списка получим");
            list.Reverse();
            Console.WriteLine(list);

            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ArrayLists
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new ListOnArray<string>(20);

            list.Add("A");
            list.Add("B");
            list.Add("C");
            list.Add("A");
            list.Add("D");
            list.Add("F");

            WriteList(list, "задан список");

            list.TrimExcess();
            WriteList(list, "после урезания внутреннего массива");

            list.Remove("A");
            WriteList(list, "после удаления элемента A");

            list.Add("E");
            WriteList(list, "после добавления элемента E");

            list.Add("G");
            WriteList(list, "после добавления элемента G");

            list.RemoveAt(2);
            WriteList(list, "после удаления 3-го элемента");

            list.Capacity = 8;
            WriteList(list, "после установки длины внутреннего массива равной 8");

            list.Insert(1, "P");
            WriteList(list, "после вставления P на второе место");

            Console.WriteLine("индекс элемента F равен {0}", list.IndexOf("F"));
            Console.WriteLine();

            Console.WriteLine("содержится ли элемент H  {0}", list.Contains("H"));
            Console.WriteLine();

            list.Clear();
            WriteList(list, "после очищения списка");


            Console.ReadKey();
        }

        private static void WriteList(ListOnArray<string> list, string title)
        {
            Console.WriteLine(title);
            Console.WriteLine("список: " + string.Join(" ", list) + $"    Count = {list.Count} ; Capacity = {list.Capacity}");
            Console.WriteLine();
        }


    }
}

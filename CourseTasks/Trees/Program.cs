using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trees
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Задано дерево такое же как в презентации лекции про деревья - на первом слайде");
            Console.WriteLine();

            var tree = new Tree<int>(new IntComparer());

            tree.Insert(8);
            tree.Insert(3);
            tree.Insert(10);
            tree.Insert(1);
            tree.Insert(6);
            tree.Insert(14);
            tree.Insert(4);
            tree.Insert(7);
            tree.Insert(13);

            WriteTree(tree);
            Console.WriteLine();
            Console.WriteLine("Количество узлов: {0}",tree.GetCount());

            tree.Remove(3);
            Console.WriteLine();
            Console.WriteLine("Удалим 3 - получим:");
            Console.WriteLine();
            WriteTree(tree);
            Console.WriteLine();
            Console.WriteLine("Количество узлов: {0}", tree.GetCount());

            Console.ReadKey();
        }

        private class IntComparer : IComparer<int>
        {
            public int Compare(int x, int y)
            {
                return x.CompareTo(y);
            }
        }

        private static void WriteTree(Tree<int> tree)
        {
            Console.WriteLine("              в ширину: {0}", string.Join("  ", tree.Broadways()));
            Console.WriteLine(" в глубину с рекурсией: {0}", string.Join("  ", tree.RecursiveScan()));
            Console.WriteLine("в глубину без рекурсии: {0}", string.Join("  ", tree.NotRecursiveScan()));
        }
    }
}

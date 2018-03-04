using System;
using System.Collections;
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

            Console.WriteLine("Задано дерево");
            Console.WriteLine();

            var tree = new Tree<int>();

            tree.Insert(8);
            tree.Insert(3);
            tree.Insert(10);
            tree.Insert(1);
            tree.Insert(6);
            tree.Insert(14);
            tree.Insert(4);
            tree.Insert(7);
            tree.Insert(13);
            tree.Insert(5);

            WriteTree(tree);
            Console.WriteLine();
            Console.WriteLine("Количество узлов: {0}", tree.Count);

            var removing = 3;
            tree.Remove(removing);
            Console.WriteLine();
            Console.WriteLine("Удалим {0} - получим:", removing);
            Console.WriteLine();
            WriteTree(tree);
            Console.WriteLine();
            Console.WriteLine("Количество узлов: {0}", tree.Count);

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

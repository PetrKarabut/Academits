using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    class Program
    {
        static void Main(string[] args)
        {
            var matrix = new int[10, 10];

            Add(matrix, 0, 7);
            Add(matrix, 3, 7);
            Add(matrix, 9, 7);
            Add(matrix, 9, 6);
            Add(matrix, 5, 2);
            Add(matrix, 5, 4);
            Add(matrix, 1, 4);
            Add(matrix, 7, 4);
            Add(matrix, 1, 2);


            var graph = new Graph(matrix);


            foreach (var node in graph.Broadways())
            {
                Console.Write(node + ";");
            }

            Console.WriteLine();

            foreach (var node in graph.Scan())
            {
                Console.Write(node + ";");
            }

            Console.ReadKey();
        }

        private static void Add(int[,] matrix, int i, int j)
        {
            matrix[i, j] = 1;
            matrix[j, i] = 1;
        }
    }
}

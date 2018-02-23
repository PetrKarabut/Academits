using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTables
{
    class Program
    {
        static void Main(string[] args)
        {
            var hashTable = new HashTable<string>(20);

            hashTable.Add("A");
            hashTable.Add("B");
            hashTable.Add("C");
            hashTable.Add("A");
            hashTable.Add("D");
            hashTable.Add("F");
            hashTable.Add(null);

            WriteTable(hashTable);
            Console.WriteLine();


            hashTable.Remove("D");
            hashTable.Remove(null);
            WriteTable(hashTable);

            Console.ReadKey();
        }

        private static void WriteTable(HashTable<string> table)
        {
            foreach (var s in table)
            {
                Console.Write((s != null ? s : "null") + " ");
            }
        }
    }
}

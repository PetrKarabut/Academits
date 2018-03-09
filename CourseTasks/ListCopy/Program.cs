using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lists;

namespace ListCopy
{
    class Program
    {
        static void Main(string[] args)
        {

            var node1 = new CopyOfList.Node("node1");
            var node2 = new CopyOfList.Node("node2");
            var node3 = new CopyOfList.Node("node3");
            var node4 = new CopyOfList.Node("node4");
            var node5 = new CopyOfList.Node("node5");
            var node6 = new CopyOfList.Node("node6");
            var node7 = new CopyOfList.Node("node7");

            node1.Reference = node3;
            node2.Reference = node4;
            node3.Reference = null;
            node4.Reference = node7;
            node5.Reference = node3;
            node6.Reference = node2;
            node7.Reference = node1;

            var list = new SimplyConnectedList<CopyOfList.Node>();

            list.InsertInBegining(node7);
            list.InsertInBegining(node6);
            list.InsertInBegining(node5);
            list.InsertInBegining(node4);
            list.InsertInBegining(node3);
            list.InsertInBegining(node2);
            list.InsertInBegining(node1);

            
            Console.WriteLine(list);

            Console.WriteLine(CopyOfList.Clone(list));

            Console.ReadKey();
        }
    }
}

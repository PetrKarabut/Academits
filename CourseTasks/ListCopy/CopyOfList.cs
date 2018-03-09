using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lists;

namespace ListCopy
{
    class CopyOfList
    {
        public class Node
        {
            public string Name { get; set; }
            public Node Reference { get; set; }

            public override string ToString()
            {
                return Environment.NewLine + Name + " -> " + (Reference != null ? Reference.Name : "null");
            }

            public Node(string name)
            {
                Name = name;
            }
        }

        public static SimplyConnectedList<Node> Clone(SimplyConnectedList<Node> original)
        {
            var array = original.ToArray();

            var newArray = new Node[array.Length];

            for (var i = 0; i < newArray.Length; i++)
            {
                newArray[i] = new Node("Copy " + array[i].Name);
            }

            for (var i = 0; i < newArray.Length; i++)
            {
                var index = Array.IndexOf(array, array[i].Reference);
                if (index >= 0)
                {
                    newArray[i].Reference = newArray[index];
                }
            }


            var list = new SimplyConnectedList<Node>();

            for (var i = array.Length - 1; i >= 0; i--)
            {
                list.InsertInBegining(newArray[i]);
            }

            return list;
        }
    }
}

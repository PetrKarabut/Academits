using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lists;

namespace ListCopy
{
    public class OtherList<T> : SimplyConnectedList<T>
    {
        protected class Node : Unit
        {
            public Node(T value) : base(value)
            {
            }

            public Node Reference { get; set; }

            public override Unit Next
            {
                get
                {
                    return node;
                }

                set
                {
                    node = (Node)value;
                }
            }

            private Node node;
        }
    }
}

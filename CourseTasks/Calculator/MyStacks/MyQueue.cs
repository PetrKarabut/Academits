using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackCalculator.MyStacks
{
    public class MyQueue<T>
    {
        private class Node
        {
            public Node Next { get; set; }
            public T Value { get; set; }
        }

        public int Count { get; private set; }

        public T Last => last.Value;

        private Node first;

        private Node last; 

        public void Enqueue(T item)
        {
            var node = new Node { Value = item };

            if (Count == 0)
            {
                first = node;
                last = node;
                Count = 1;
                return;
            }

            last.Next = node;
            last = node;
            Count++;
           
        }

        public T Dequeue()
        {
            if (Count == 0)
            {
                throw new StackException("Очередь пуста.");
            }

            var item = first.Value;

            if (Count == 1)
            {
                first = null;
                last = null;
                Count = 0;
                return item;
            }

            first = first.Next;
            Count--;
            return item;
        }


    }
}

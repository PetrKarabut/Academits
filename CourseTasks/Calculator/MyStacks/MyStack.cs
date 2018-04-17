using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackCalculator.MyStacks
{
    public class MyStack<T> 
    {
        private class Node
        {
            public Node Next { get; set; }
            public T Value { get; set; }
        }

        public int Count { get; private set; }

        private Node top;

        public void Push(T item)
        {
            var node = new Node { Value = item, Next = top };
            top = node;
            Count++;
        }

        public T Pop()
        {
            if (Count == 0)
            {
                throw new StacksException("Стек пуст.");
            }

            var item = top.Value;
            top = top.Next;
            Count--;
            return item;
        }

        public T Peek()
        {
            if (Count == 0)
            {
                throw new StacksException("Стек пуст.");
            }

            var item = top.Value;
            return item;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lists
{
    class SimplyConnectedList<T>
    {
        private class Unit
        {
            public T Value { get; set; }

            public Unit Next { get; set; }

            public Unit(T value)
            {
                Value = value;
            }
        }

        private Unit head;

        public int Count { get; private set; }

        public T First
        {
            get
            {
                if (head == null)
                {
                    throw new NullReferenceException();
                }

                return head.Value;
            }
        }

        public T GetValue(int index)
        {
            if (head == null || index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException();
            }

            var unit = head;

            for (var i = 0; i < Count; i++)
            {
                if (i == index)
                {
                    return unit.Value;
                }

                unit = unit.Next;
            }

            return unit.Value;
        }

        public T SetValue(int index, T value)
        {
            if (head == null || index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException();
            }

            var unit = head;
            T old;
            for (var i = 0; i < Count; i++)
            {
                if (i == index)
                {
                    old = unit.Value;
                    unit.Value = value;
                    return old;
                }

                unit = unit.Next;
            }

            return unit.Value;
        }

        public T RemoveAt(int index)
        {
            if (head == null || index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException();
            }

            if (index == 0)
            {
                var value = head.Value;
                head = null;
                Count--;
                return value;
            }

            var previous = head;
            for (var i = 0; i < Count; i++)
            {
                if (i == index - 1)
                {
                    break;
                }
                previous = previous.Next;
            }

            if (index == Count - 1)
            {
                var value = previous.Next.Value;
                previous.Next = null;
                Count--;
                return value;
            }

            var deletedValue = previous.Next.Value;
            previous.Next = previous.Next.Next;
            Count--;
            return deletedValue;
        }
       
    }
}

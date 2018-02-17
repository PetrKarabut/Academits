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

        public void InsertInBegining(T value)
        {
            var unit = new Unit(value);
            if (Count > 1)
            {
                unit.Next = head;
            }

            head = unit;
            Count++;
        }

        public void Insert(T value, int index)
        {
            if (index == 0)
            {
                InsertInBegining(value);
                return;
            }

            if (head == null || index < 0 || index > Count)
            {
                throw new IndexOutOfRangeException();
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

            var unit = new Unit(value);
            unit.Next = previous.Next;
            previous.Next = unit;
            Count++;
        }

        public bool Delete(T value)
        {
            if (head == null)
            {
                return false;
            }

            if (head.Value.Equals(value))
            {
                head = head.Next;
                Count--;
                return true;
            }

            var previous = head;
            for (var i = 0; i < Count - 1; i++)
            {
                if (previous.Next.Value.Equals(value))
                {
                    previous.Next = previous.Next.Next;
                    Count--;
                    return true;
                }
                previous = previous.Next;
            }

            return false;
        }

        public T DeleteFirst()
        {
            if (head == null)
            {
                throw new NullReferenceException();
            }
            var value = head.Value;
            head = head.Next;
            Count--;
            return value;
        }

        public void Reverse()
        {
            if (Count < 2)
            {
                return;
            }

            var end = head;
            while (end.Next != null)
            {
                var next1 = end.Next;
                end.Next = next1.Next;
                next1.Next = head;
                head = next1;
            }
        }

        public override string ToString()
        {
            var unit = head;
            var builder = new StringBuilder();
            while (unit != null)
            {
                builder.Append(unit.Value).Append(" ");
                unit = unit.Next;
            }

            return builder.ToString();
        }

    }
}

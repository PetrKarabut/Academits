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

        private Unit GetUnit(int index)
        {
            var unit = head;

            for (var i = 0; i < Count; i++)
            {
                if (i == index)
                {
                    return unit;
                }

                unit = unit.Next;
            }

            return null;
        }

        public T GetValue(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException("Ошибка при получении значения по индексу: индекс вышел за пределы списка");
            }

            if (head == null)
            {
                throw new NullReferenceException("Ошибка при получении значения по индексу: список пуст");
            }

            return GetUnit(index).Value;
           
        }

        public T SetValue(int index, T value)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException("Ошибка при задании значения по индексу: индекс вышел за пределы списка");
            }

            if (head == null)
            {
                throw new NullReferenceException("Ошибка при задании значения по индексу: список пуст");
            }

            var unit = GetUnit(index);
            var old = unit.Value;
            unit.Value = value;
            return old;
        }

        public T RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException("Ошибка при удалении значения по индексу: индекс вышел за пределы списка");
            }

            if (head == null)
            {
                throw new NullReferenceException("Ошибка при удалении значения по индексу: список пуст");
            }

            if (index == 0)
            {
                return DeleteFirst();
            }

            var previous = GetUnit(index - 1);

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
            if (Count > 0)
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

            if (index < 0 || index > Count)
            {
                throw new IndexOutOfRangeException("Ошибка при вставке значения по индексу: индекс вышел за пределы списка");
            }

            var previous = GetUnit(index - 1);

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

            if (AreEqual(head.Value, value))
            {
                head = head.Next;
                Count--;
                return true;
            }

            var previous = head;
            for (var i = 0; i < Count - 1; i++)
            {
                if (AreEqual(previous.Next.Value,value))
                {
                    previous.Next = previous.Next.Next;
                    Count--;
                    return true;
                }
                previous = previous.Next;
            }

            return false;
        }

        private bool AreEqual(T t1, T t2)
        {
            if (t1 == null)
            {
                return (t2 == null);
            }

            if (t2 == null)
            {
                return false;
            }

            return t1.Equals(t2);
        }

        public T DeleteFirst()
        {
            if (head == null)
            {
                throw new NullReferenceException("Ошибка при удалении первого элемента: список пуст");
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

        public SimplyConnectedList<T> Clone()
        {
            var copy = new SimplyConnectedList<T>();

            if (head == null)
            {
                return copy;
            }

            var unit = head;
            var copyUnit = new Unit(unit.Value);
            copy.head = copyUnit;
            copy.Count = 1;
            for (var i = 0; i < Count - 1; i++)
            {
                copyUnit.Next = new Unit(unit.Next.Value);
                copy.Count++;

                unit = unit.Next;
                copyUnit = copyUnit.Next;
            }

            return copy;
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

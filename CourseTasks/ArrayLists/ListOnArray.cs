using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayLists
{
    class ListOnArray<T> : IList<T>
    {
        private T[] array;

        private int capacity;

        public ListOnArray(int capacity)
        {
            this.capacity = capacity;
            array = new T[capacity];
        }
        
        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException("Ошибка: индекс вышел за пределы списка");
                }

                return array[index];
            }

            set
            {
                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException("Ошибка: индекс вышел за пределы списка");
                }

                array[index] = value;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new ListEnumerator<T>(array, Count);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ListEnumerator<T>(array, Count);
        }

        public int IndexOf(T item)
        {
            for (var i = 0; i < Count; i++)
            {
                if (Equals(array[i], item))
                {
                    return i;
                }
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException("Ошибка: индекс вышел за пределы списка");
            }

            if (Count == array.Length)
            {
                IncreaseCapacity();
            }

            for (var i = Count; i > index; i--)
            {
                array[i] = array[i - 1];
            }

            array[index] = item;
            Count++;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException("Ошибка: индекс вышел за пределы списка");
            }

            for (var i = index; i < Count - 1; i++)
            {
                array[i] = array[i + 1];
            }

            Count--;
        }

        public void Add(T item)
        {
            if (Count == array.Length)
            {
                IncreaseCapacity();
            }

            array[Count] = item;
            Count++;
        }

        private void IncreaseCapacity()
        {
            Capacity = 2 * Count;
        }

        public int Capacity
        {
            get
            {
                return capacity;
            }

            set
            {
                if (value < Count)
                {
                    throw new ArgumentException("нельзя задать вместимость меньше длины списка");
                }

                var old = array;
                array = new T[value];
                Array.Copy(old, array, Count);
                capacity = value;
            }
        }

        public void Clear()
        {
            Count = 0;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (arrayIndex < 0 || arrayIndex > array.Length)
            {
                throw new IndexOutOfRangeException("Ошибка: индекс вышел за пределы массива");
            }

            var i = arrayIndex;

            foreach (var t in this.array)
            {
                array[i] = t;
                i++;
            }
        }

        public bool Remove(T item)
        {
            var index = IndexOf(item);

            if (index == -1)
            {
                return false;
            }

            RemoveAt(index);
            return true;
        }

        public void TrimExcess()
        {
            if (Count > 0.9f * array.Length)
            {
                return;
            }

            var old = array;
            array = new T[Count];
            Array.Copy(old, array, Count);
            capacity = Count;
        }
    }
}

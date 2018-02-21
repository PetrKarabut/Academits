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

        private int runningEnumerators;

        private const int defaultCapacity = 40;

        public ListOnArray()
        {
            array = new T[defaultCapacity];
        }
        
        public ListOnArray(int capacity)
        {
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

                OnChange();
                array[index] = value;
            }
        }

        private void OnChange()
        {
            if (runningEnumerators > 0)
            {
                throw new Exception("Ошибка: нельзя изменять коллекцию во время работы перечислителя");
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            runningEnumerators++;

            for (var i = 0; i < Count; i++)
            {
                yield return array[i];
            }

            runningEnumerators--;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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
            if (index < 0 || index > Count)
            {
                throw new IndexOutOfRangeException("Ошибка: индекс вышел за пределы списка");
            }

            OnChange();

            if (index == Count)
            {
                Add(item);
                return;
            }

            if (Count == array.Length)
            {
                IncreaseCapacity();
            }

            Array.Copy(array, index, array, index + 1, Count - index);

            array[index] = item;
            Count++;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException("Ошибка: индекс вышел за пределы списка");
            }

            OnChange();

            Array.Copy(array, index + 1, array, index, Count - index - 1);

            Count--;
        }

        public void Add(T item)
        {
            OnChange();

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
                return array.Length;
            }

            set
            {
                if (value < Count)
                {
                    throw new ArgumentException("нельзя задать вместимость меньше длины списка");
                }

                OnChange();
                var old = array;
                array = new T[value];
                Array.Copy(old, array, Count);
            }
        }

        public void Clear()
        {
            OnChange();
            Count = 0;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (arrayIndex < 0 || arrayIndex >= array.Length)
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

            OnChange();
            RemoveAt(index);
            return true;
        }

        public void TrimExcess()
        {
            if (Count > 0.9f * array.Length)
            {
                return;
            }

            OnChange();
            var old = array;
            array = new T[Count];
            Array.Copy(old, array, Count);
        }
    }
}

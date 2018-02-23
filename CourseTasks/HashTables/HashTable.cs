using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTables
{
    class HashTable<T> : ICollection<T>
    {
        private List<T>[] array;

        private int changesCount = 0;

        private const int defaultSize = 20;

        public HashTable()
        {
            array = new List<T>[defaultSize];
        }

        public HashTable(int size)
        {
            array = new List<T>[size];
        }

        public int Count
        {
            get
            {
                var count = 0;
                foreach (var list in array)
                {
                    if (list != null)
                    {
                        count += list.Count;
                    }
                }

                return count;
            }
        }


        public bool IsReadOnly => false;

        private void OnChange()
        {
            changesCount++;
        }

        private int GetIndex(object o)
        {
            return GetIndex(o.GetHashCode());
        }

        private int GetIndex(int hashCode)
        {
            return Math.Abs(hashCode % array.Length);
        }

        public void Add(T item)
        {
            var index = GetIndex(item);

            if (array[index] == null)
            {
                array[index] = new List<T>();
            }

            OnChange();
            array[index].Add(item);

        }

        public void Clear()
        {
            if (Count == 0)
            {
                return;
            }

            OnChange();

            for (var i = 0; i < array.Length; i++)
            {
                array[i] = null;
            }
        }

        public bool Contains(T item)
        {
            var index = GetIndex(item);
            return array[index] == null ? false : array[index].Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (arrayIndex < 0 || arrayIndex >= array.Length)
            {
                throw new IndexOutOfRangeException("Ошибка: индекс вышел за пределы массива");
            }

            var index = arrayIndex;

            foreach (var list in this.array)
            {
                if (list == null)
                {
                    continue;
                }

                foreach (var item in list)
                {
                    array[index] = item;
                    index++;
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            var changes = changesCount;

            foreach (var list in array)
            {
                if (list == null)
                {
                    continue;
                }

                foreach (var item in list)
                {
                    yield return item;
                    if (changes != changesCount)
                    {
                        throw new InvalidOperationException("Ошибка: коллекция не должна меняться во время работы перечислителя");
                    }
                }
            }
        }

        public bool Remove(T item)
        {
            var index = GetIndex(item);

            if (array[index] == null)
            {
                return false;
            }

            var removed = array[index].Remove(item);

            if (removed)
            {
                OnChange();
                return true;
            }
            else
            {
                return false;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}

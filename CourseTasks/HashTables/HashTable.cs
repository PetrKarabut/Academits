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

        

        public int Count { get; private set; }
       

        public bool IsReadOnly => false;

        private int GetIndex(int hashCode, int length)
        {
            return Math.Abs(hashCode % length);
        }

        public void Add(T item)
        {
            var index = GetIndex(item.GetHashCode(), array.Length);
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}

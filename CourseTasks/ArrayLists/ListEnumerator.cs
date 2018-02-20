using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayLists
{
    class ListEnumerator<T> : IEnumerator<T>
    {
        private T[] array;
        private int index = -1;

        public ListEnumerator(T[] array, int length)
        {
            this.array = new T[length];
            Array.Copy(array, this.array, length);
        }

        public T Current => array[index];
        

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (index >= array.Length - 1)
            {
                return false;
            }
            else
            {
                index++;
            }

            return true;
        }

        public void Reset()
        {
            index = -1;
        }
    }
}

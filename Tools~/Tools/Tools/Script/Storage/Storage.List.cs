using System;
using System.Collections;
using System.Collections.Generic;

namespace AIO
{
    public partial class Storage : IList<IBinData>
    {
        private readonly List<IBinData> Collection;

        /// <inheritdoc />
        public IBinData this[int index]
        {
            get => Collection[index];
            set => Collection[index] = value;
        }

        /// <inheritdoc />
        public int Count => Collection.Count;

        /// <inheritdoc />
        public bool IsReadOnly => false;

        /// <inheritdoc />
        public void Add(IBinData item)
        {
            Collection.Add(item);
        }

        /// <inheritdoc />
        public void Clear()
        {
            Collection.Clear();
        }

        /// <inheritdoc />
        public bool Contains(IBinData item)
        {
            return Collection.Contains(item);
        }

        /// <inheritdoc />
        public void CopyTo(IBinData[] array, int arrayIndex)
        {
            if (arrayIndex < 0 || arrayIndex >= array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), "The value of arrayIndex is out of range.");
            }

            foreach (var item in Collection)
            {
                if (arrayIndex >= array.Length)
                {
                    throw new ArgumentException("The length of array is less than the number of elements in the collection.");
                }

                array[arrayIndex++] = item;
            }
        }

        /// <inheritdoc />
        public IEnumerator<IBinData> GetEnumerator()
        {
            return ((IEnumerable<IBinData>)Collection).GetEnumerator();
        }

        /// <inheritdoc />
        public int IndexOf(IBinData item)
        {
            return Collection.IndexOf(item);
        }

        /// <inheritdoc />
        public void Insert(int index, IBinData item)
        {
            Collection.Insert(index, item);
        }

        /// <inheritdoc />
        public bool Remove(IBinData item)
        {
            return Collection.Remove(item);
        }

        /// <inheritdoc />
        public void RemoveAt(int index)
        {
            Collection.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
using System;
using System.Collections.Generic;

namespace AIO
{
    public partial class BufferByte
    {
        /// <inheritdoc/> 
        public T[] ReadEnumArray<T>(in bool reverse = false) where T : struct, Enum
        {
            return Arrays.GetEnumArray<T>(ref ReadIndex, reverse);
        }

        /// <inheritdoc/> 
        public void WriteEnumArray<T>(in ICollection<T> value, in bool reverse = false) where T : struct, Enum
        {
            WriteLen(value.Count);
            AutomaticExpansion(value.Count * 4);
            foreach (var item in value) Arrays.SetInt32(ref WriteIndex, item.GetHashCode(), reverse);
        }
    }

    public partial interface IWrite
    {
        void WriteEnumArray<T>(in ICollection<T> value, in bool reverse = false) where T : struct, Enum;
    }

    public partial interface IRead
    {
        T[] ReadEnumArray<T>(in bool reverse = false) where T : struct, Enum;
    }
}
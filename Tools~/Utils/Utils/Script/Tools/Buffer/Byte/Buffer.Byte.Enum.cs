using System;
using System.Collections.Generic;

namespace AIO
{
    public partial class BufferByte
    {
        /// <inheritdoc/> 
        public T ReadEnum<T>(in bool reverse = false) where T : struct, Enum
        {
            return (T)Enum.Parse(typeof(T), Arrays.GetInt32(ref ReadIndex, reverse).ToString());
        }

        /// <inheritdoc/> 
        public T[] ReadEnumArray<T>(in bool reverse = false) where T : struct, Enum
        {
            return Arrays.GetEnumArray<T>(ref ReadIndex, reverse);
        }

        /// <inheritdoc/> 
        public void WriteEnum<T>(in T value, in bool reverse = false) where T : struct, Enum
        {
            AutomaticExpansion(4);
            Arrays.SetInt32(ref WriteIndex, value.GetHashCode(), reverse);
        }

        /// <inheritdoc/> 
        public void WriteEnumArray<T>(in ICollection<T> value, in bool reverse = false) where T : struct, Enum
        {
            WriteLen(value.Count);
            AutomaticExpansion(value.Count * 4);
            foreach (var item in value) Arrays.SetInt32(ref WriteIndex, item.GetHashCode(), reverse);
        }
    }
}
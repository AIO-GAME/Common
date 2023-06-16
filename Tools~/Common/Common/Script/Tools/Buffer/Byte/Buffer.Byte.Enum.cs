using System;
using System.Collections.Generic;

namespace AIO
{
    public partial class BufferByte
    {
        /// <inheritdoc/> 
        public T ReadEnum<T>() where T : struct, Enum
        {
            return (T)Enum.Parse(typeof(T), ReadInt32().ToString());
        }

        /// <inheritdoc/> 
        public T[] ReadEnumArray<T>() where T : struct, Enum
        {
            var list = new T[ReadLen()];
            for (var i = 0; i < list.Length; i++) list[i] = (T)Enum.Parse(typeof(T), ReadInt32().ToString());
            return list;
        }

        /// <inheritdoc/> 
        public void WriteEnum<T>(T value) where T : struct, Enum
        {
            WriteInt32(value.GetHashCode());
        }

        /// <inheritdoc/> 
        public void WriteEnumArray<T>(ICollection<T> value) where T : struct, Enum
        {
            WriteLen(value.Count);
            AutomaticExpansion(value.Count * 4);
            foreach (var item in value) WriteInt32(item.GetHashCode());
        }
    }
}
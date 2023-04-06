using System;

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
        public void WriteEnum<T>(in T value, in bool reverse = false) where T : struct, Enum
        {
            AutomaticExpansion(4);
            Arrays.SetInt32(ref WriteIndex, value.GetHashCode(), reverse);
        }
    }

    public partial interface IWrite
    {
        void WriteEnum<T>(in T value, in bool reverse = false) where T : struct, Enum;
    }

    public partial interface IRead
    {
        T ReadEnum<T>(in bool reverse = false) where T : struct, Enum;
    }
}
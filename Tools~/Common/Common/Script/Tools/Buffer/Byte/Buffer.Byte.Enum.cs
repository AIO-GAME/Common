#region

using System;
using System.Collections.Generic;

#endregion

namespace AIO
{
    partial class BufferByte : IWriteEnum, IReadEnum
    {
        /// <inheritdoc/> 
        public T ReadEnum<T>()
        where T : struct, Enum
        {
            return (T)Enum.Parse(typeof(T), ReadInt32().ToString());
        }

        /// <inheritdoc/> 
        public void WriteEnum(Enum value) { WriteInt32(value.GetHashCode()); }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace AIO
{
    partial class BufferByte : IWriteChar, IReadChar
    {
        /// <inheritdoc/> 
        public char ReadChar(bool reverse = false)
        {
            var bytes = Arrays.GetByteArray(ref ReadIndex, reverse);
            return BitConverter.ToChar(bytes, 0);
        }

        /// <inheritdoc/> 
        public void WriteChar(char value, bool reverse = false)
        {
            var bytes = BitConverter.GetBytes(value);
            WriteByteArray(bytes);
        }

        /// <inheritdoc/>
        public void WriteCharArray(ICollection<char> value, bool reverse = false)
        {
            WriteLen(value.Count);
            foreach (var itemBytes in value)
            {
                WriteByteArray(BitConverter.GetBytes(itemBytes), reverse);
            }
        }

        /// <inheritdoc/>
        public char[] ReadCharArray(bool reverse = false)
        {
            var len = ReadLen();
            var chars = new char[len];
            for (var i = 0; i < len; i++)
            {
                var bytes = Arrays.GetByteArray(ref ReadIndex, reverse);
                chars[i] = BitConverter.ToChar(bytes, 0);
            }

            return chars;
        }
    }
}
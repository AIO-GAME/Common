#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    partial class BufferByte : IWriteByte, IReadByte
    {
        /// <inheritdoc/> 
        public byte ReadByte() => Arrays[ReadIndex++];

        /// <inheritdoc/> 
        public void WriteByte(byte value)
        {
            AutomaticExpansion(1);
            Arrays[WriteIndex++] = value;
        }

        /// <inheritdoc/>
        public byte[] ReadByteArray(bool reverse = false)
        {
            var len   = ReadLen();
            var array = new byte[len];
            if (reverse)
            {
                for (var i = 0; i < len; i++)
                {
                    array[i] = Arrays[ReadIndex++];
                }
            }
            else
            {
                for (var i = len - 1; i >= 0; i--)
                {
                    array[i] = Arrays[ReadIndex++];
                }
            }

            return array;
        }

        /// <inheritdoc/> 
        public void WriteByteArray(ICollection<byte> value, bool reverse = false)
        {
            WriteLen(value.Count);
            AutomaticExpansion(value.Count);
            if (reverse)
            {
                var j                                   = WriteIndex;
                foreach (var item in value) Arrays[j++] = item;
            }
            else
            {
                var j                                   = WriteIndex + value.Count - 1;
                foreach (var item in value) Arrays[j--] = item;
            }

            WriteIndex += value.Count;
        }
    }
}
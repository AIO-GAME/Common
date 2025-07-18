#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    partial class BufferByte : IWriteBool, IReadBool
    {
        /// <inheritdoc/> 
        public bool ReadBool() => Arrays[ReadIndex++] != 0;

        /// <inheritdoc/>
        public void WriteBool(bool b)
        {
            AutomaticExpansion(1);
            Arrays[WriteIndex++] = (byte)(b ? 1 : 0);
        }

        /// <inheritdoc/>
        public void WriteBoolArray(ICollection<bool> value, bool reverse = false)
        {
            WriteLen(value.Count);
            AutomaticExpansion(value.Count);
            if (reverse)
            {
                var j                                   = WriteIndex;
                foreach (var item in value) Arrays[j++] = (byte)(item ? 1 : 0);
            }
            else
            {
                var j                                   = WriteIndex + value.Count - 1;
                foreach (var item in value) Arrays[j--] = (byte)(item ? 1 : 0);
            }

            WriteIndex += value.Count;
        }

        /// <inheritdoc/>
        public bool[] ReadBoolArray(bool reverse = false)
        {
            var len   = ReadLen();
            var array = new bool[len];
            if (reverse)
            {
                for (var i = 0; i < len; i++)
                {
                    array[i] = Arrays[ReadIndex++] != 0;
                }
            }
            else
            {
                for (var i = len - 1; i >= 0; i--)
                {
                    array[i] = Arrays[ReadIndex++] != 0;
                }
            }

            return array;
        }
    }
}
using System.Collections.Generic;

namespace AIO
{
    public partial class BufferByte
    {
        /// <inheritdoc/> 
        public bool ReadBool()
        {
            return Arrays[ReadIndex++] != 0;
        }

        /// <inheritdoc/> 
        public bool[] ReadBoolArray(in bool reverse = false)
        {
            return Arrays.GetBoolArray(ref ReadIndex, reverse);
        }

        /// <inheritdoc/> 
        public void WriteBool(in bool b)
        {
            AutomaticExpansion(1);
            Arrays[WriteIndex++] = (byte)(b ? 1 : 0);
        }

        /// <inheritdoc/> 
        public void WriteBoolArray(in ICollection<bool> value, in bool reverse = false)
        {
            WriteLen(value.Count);
            AutomaticExpansion(value.Count);
            Arrays.SetBoolArray(ref WriteIndex, value, reverse);
        }
    }
}
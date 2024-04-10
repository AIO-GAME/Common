#region

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace AIO
{
    public partial class BufferByte
    {
        #region IReadData Members

        /// <inheritdoc/> 
        public char ReadChar(bool reverse = false)
        {
            var bytes = Arrays.GetByteArray(ref ReadIndex, reverse);
            return BitConverter.ToChar(bytes, 0);
        }

        /// <inheritdoc/> 
        public char[] ReadCharArray(bool reverse = false)
        {
            var str = Arrays.GetString(ref ReadIndex, Encoding.UTF8, reverse);
            if (str == null) return null;
            if (str.Length == 0) return Array.Empty<char>();
            return str.ToCharArray();
        }

        #endregion

        #region IWriteData Members

        /// <inheritdoc/> 
        public void WriteChar(char value, bool reverse = false)
        {
            var bytes = BitConverter.GetBytes(value);
            AutomaticExpansion(bytes.Length + 4);
            Arrays.SetByteArray(ref WriteIndex, bytes, reverse);
        }

        /// <inheritdoc/> 
        public void WriteCharArray(ICollection<char> value, bool reverse = false)
        {
            AutomaticExpansion(4);
            if (value == null)
            {
                Arrays.SetLen(ref WriteIndex, -1);
                return;
            }

            if (value.Count <= 0)
            {
                Arrays.SetLen(ref WriteIndex, 0);
                return;
            }

            var str = new StringBuilder();
            foreach (var c in value) str.Append(c);
            var bytes = Encoding.UTF8.GetBytes(str.ToString());
            AutomaticExpansion(bytes.Length);
            Arrays.SetByteArray(ref WriteIndex, bytes, reverse);
        }

        #endregion
    }
}
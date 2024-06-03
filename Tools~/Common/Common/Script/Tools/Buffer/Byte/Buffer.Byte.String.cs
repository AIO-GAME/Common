#region

using System.Text;

#endregion

namespace AIO
{
    public partial class BufferByte
    {
        #region IReadData Members

        /// <inheritdoc/> 
        public string ReadString(Encoding encoding = null, bool reverse = false)
        {
            return Arrays.GetString(ref ReadIndex, encoding, reverse);
        }

        /// <inheritdoc/> 
        public string ReadStringUTF8(bool reverse = false)
        {
            return Arrays.GetString(ref ReadIndex, Encoding.UTF8, reverse);
        }

        /// <inheritdoc/> 
        public string ReadStringASCII(bool reverse = false)
        {
            return Arrays.GetString(ref ReadIndex, Encoding.ASCII, reverse);
        }

        /// <inheritdoc/> 
        public string ReadStringUnicode(bool reverse = false)
        {
            return Arrays.GetString(ref ReadIndex, Encoding.Unicode, reverse);
        }

        #endregion

        #region IWriteData Members

        /// <inheritdoc/> 
        public void WriteString(string value, Encoding encoding = null, bool reverse = false)
        {
            if (value == null)
            {
                WriteLen(-1);
                return;
            }

            if (value.Length <= 0)
            {
                WriteLen(0);
                return;
            }

            WriteByteArray((encoding ?? Encoding.Default).GetBytes(value), reverse);
        }

        /// <inheritdoc/> 
        public void WriteString(StringBuilder value, Encoding encoding = null, bool reverse = false)
        {
            if (value == null)
            {
                WriteLen(-1);
                return;
            }

            if (value.Length <= 0)
            {
                WriteLen(value.Length);
                return;
            }

            WriteByteArray((encoding ?? Encoding.Default).GetBytes(value.ToString()), reverse);
        }

        /// <inheritdoc/> 
        public void WriteStringUTF8(string value, bool reverse = false)
        {
            WriteString(value, Encoding.UTF8, reverse);
        }

        /// <inheritdoc/> 
        public void WriteStringUTF8(StringBuilder value, bool reverse = false)
        {
            WriteString(value, Encoding.UTF8, reverse);
        }

        /// <inheritdoc/> 
        public void WriteStringASCII(string value, bool reverse = false)
        {
            WriteString(value, Encoding.ASCII, reverse);
        }

        /// <inheritdoc/> 
        public void WriteStringASCII(StringBuilder value, bool reverse = false)
        {
            WriteString(value, Encoding.ASCII, reverse);
        }

        /// <inheritdoc/> 
        public void WriteStringUnicode(string value, bool reverse = false)
        {
            WriteString(value, Encoding.Unicode, reverse);
        }

        /// <inheritdoc/> 
        public void WriteStringUnicode(StringBuilder value, bool reverse = false)
        {
            WriteString(value, Encoding.Unicode, reverse);
        }

        #endregion
    }
}
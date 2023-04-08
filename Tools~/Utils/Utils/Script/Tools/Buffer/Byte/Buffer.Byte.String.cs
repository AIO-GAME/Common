using System.Text;

namespace AIO
{
    public partial class BufferByte
    {
        /// <inheritdoc/> 
        public string ReadString(in Encoding encoding = null, in bool reverse = false)
        {
            return Arrays.GetString(ref ReadIndex, encoding, reverse);
        }

        /// <inheritdoc/> 
        public string ReadStringUTF8(in bool reverse = false)
        {
            return Arrays.GetString(ref ReadIndex, Encoding.UTF8, reverse);
        }

        /// <inheritdoc/> 
        public string ReadStringASCII(in bool reverse = false)
        {
            return Arrays.GetString(ref ReadIndex, Encoding.ASCII, reverse);
        }

        /// <inheritdoc/> 
        public string ReadStringUnicode(in bool reverse = false)
        {
            return Arrays.GetString(ref ReadIndex, Encoding.Unicode, reverse);
        }

        /// <inheritdoc/> 
        public void WriteString(in string value, in Encoding encoding = null, in bool reverse = false)
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

            WriteByteArray((encoding ?? Encoding.Default).GetBytes(value), reverse);
        }

        /// <inheritdoc/> 
        public void WriteString(in StringBuilder value, in Encoding encoding = null, in bool reverse = false)
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
        public void WriteStringUTF8(in string value, in bool reverse = false)
        {
            WriteString(value, Encoding.UTF8, reverse);
        }

        /// <inheritdoc/> 
        public void WriteStringUTF8(in StringBuilder value, in bool reverse = false)
        {
            WriteString(value, Encoding.UTF8, reverse);
        }

        /// <inheritdoc/> 
        public void WriteStringASCII(in string value, in bool reverse = false)
        {
            WriteString(value, Encoding.ASCII, reverse);
        }

        /// <inheritdoc/> 
        public void WriteStringASCII(in StringBuilder value, in bool reverse = false)
        {
            WriteString(value, Encoding.ASCII, reverse);
        }

        /// <inheritdoc/> 
        public void WriteStringUnicode(in string value, in bool reverse = false)
        {
            WriteString(value, Encoding.Unicode, reverse);
        }

        /// <inheritdoc/> 
        public void WriteStringUnicode(in StringBuilder value, in bool reverse = false)
        {
            WriteString(value, Encoding.Unicode, reverse);
        }
    }
}
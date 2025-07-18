#region

using System.Text;

#endregion

namespace AIO
{
    partial class BufferByte : IWriteString, IReadString
    {
        /// <inheritdoc cref="IReadString.Encoding" />
        public Encoding Encoding { get; set; } = Encoding.UTF8;

        #region IReadData Members

        /// <inheritdoc/> 
        public string ReadString(bool reverse = false) { return Arrays.GetString(ref ReadIndex, Encoding, reverse); }

        #endregion

        #region IWriteData Members

        /// <inheritdoc/> 
        public void WriteString(string value, bool reverse = false)
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

            WriteByteArray(Encoding.GetBytes(value), reverse);
        }

        /// <inheritdoc/> 
        public void WriteString(StringBuilder value, bool reverse = false)
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

            WriteByteArray(Encoding.GetBytes(value.ToString()), reverse);
        }

        #endregion
    }
}
#region

using System.Text;

#endregion

namespace AIO
{
    public partial class BufferByte
    {
        #region IReadData Members

        /// <inheritdoc/> 
        public T ReadJson<T>(Encoding encoding = null, bool reverse = false)
        {
            return AHelper.Json.Deserialize<T>(ReadString(encoding, reverse));
        }

        /// <inheritdoc/> 
        public T ReadJsonUTF8<T>(bool reverse = false)
        {
            return AHelper.Json.Deserialize<T>(ReadString(Encoding.UTF8, reverse));
        }

        /// <inheritdoc/> 
        public T ReadJsonASCII<T>(bool reverse = false)
        {
            return AHelper.Json.Deserialize<T>(ReadString(Encoding.ASCII, reverse));
        }

        /// <inheritdoc/> 
        public T ReadJsonUnicode<T>(bool reverse = false)
        {
            return AHelper.Json.Deserialize<T>(ReadString(Encoding.Unicode, reverse));
        }

        #endregion

        #region IWriteData Members

        /// <inheritdoc/> 
        public void WriteJson<T>(T value, Encoding encoding = null, bool reverse = false)
        {
            WriteString(AHelper.Json.Serialize(value), encoding, reverse);
        }

        /// <inheritdoc/> 
        public void WriteJsonUTF8<T>(T value, bool reverse = false)
        {
            WriteString(AHelper.Json.Serialize(value), Encoding.UTF8, reverse);
        }

        /// <inheritdoc/> 
        public void WriteJsonASCII<T>(T value, bool reverse = false)
        {
            WriteString(AHelper.Json.Serialize(value), Encoding.ASCII, reverse);
        }

        /// <inheritdoc/> 
        public void WriteJsonUnicode<T>(T value, bool reverse = false)
        {
            WriteString(AHelper.Json.Serialize(value), Encoding.Unicode, reverse);
        }

        #endregion
    }
}
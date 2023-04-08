using System.Text;

using Newtonsoft.Json;

namespace AIO
{
    public partial class BufferByte
    {
        /// <inheritdoc/> 
        public T ReadJson<T>(in JsonSerializerSettings settings = null, in Encoding encoding = null, in bool reverse = false)
        {
            return Utils.Json.Deserialize<T>(ReadString(encoding, reverse), settings);
        }

        /// <inheritdoc/> 
        public T ReadJsonUTF8<T>(in JsonSerializerSettings settings = null, in bool reverse = false)
        {
            return Utils.Json.Deserialize<T>(ReadString(Encoding.UTF8, reverse), settings);
        }

        /// <inheritdoc/> 
        public T ReadJsonASCII<T>(in JsonSerializerSettings settings = null, in bool reverse = false)
        {
            return Utils.Json.Deserialize<T>(ReadString(Encoding.ASCII, reverse), settings);
        }

        /// <inheritdoc/> 
        public T ReadJsonUnicode<T>(in JsonSerializerSettings settings = null, in bool reverse = false)
        {
            return Utils.Json.Deserialize<T>(ReadString(Encoding.Unicode, reverse), settings);
        }

        /// <inheritdoc/> 
        public void WriteJson<T>(in T value, in JsonSerializerSettings settings = null, in Encoding encoding = null, in bool reverse = false)
        {
            WriteString(Utils.Json.Serialize(value, settings), encoding, reverse);
        }

        /// <inheritdoc/> 
        public void WriteJsonUTF8<T>(in T value, in JsonSerializerSettings settings = null, in bool reverse = false)
        {
            WriteString(Utils.Json.Serialize(value, settings), Encoding.UTF8, reverse);
        }

        /// <inheritdoc/> 
        public void WriteJsonASCII<T>(in T value, in JsonSerializerSettings settings = null, in bool reverse = false)
        {
            WriteString(Utils.Json.Serialize(value, settings), Encoding.ASCII, reverse);
        }

        /// <inheritdoc/> 
        public void WriteJsonUnicode<T>(in T value, in JsonSerializerSettings settings = null, in bool reverse = false)
        {
            WriteString(Utils.Json.Serialize(value, settings), Encoding.Unicode, reverse);
        }
    }
}
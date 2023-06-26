using System.Text;
using Newtonsoft.Json;

namespace AIO
{
    public partial class BufferByte
    {
        /// <inheritdoc/> 
        public T ReadJson<T>(JsonSerializerSettings settings = null, Encoding encoding = null, bool reverse = false)
        {
            return UtilsGen.Json.Deserialize<T>(ReadString(encoding, reverse), settings);
        }

        /// <inheritdoc/> 
        public T ReadJsonUTF8<T>(JsonSerializerSettings settings = null, bool reverse = false)
        {
            return UtilsGen.Json.Deserialize<T>(ReadString(Encoding.UTF8, reverse), settings);
        }

        /// <inheritdoc/> 
        public T ReadJsonASCII<T>(JsonSerializerSettings settings = null, bool reverse = false)
        {
            return UtilsGen.Json.Deserialize<T>(ReadString(Encoding.ASCII, reverse), settings);
        }

        /// <inheritdoc/> 
        public T ReadJsonUnicode<T>(JsonSerializerSettings settings = null, bool reverse = false)
        {
            return UtilsGen.Json.Deserialize<T>(ReadString(Encoding.Unicode, reverse), settings);
        }

        /// <inheritdoc/> 
        public void WriteJson<T>(T value, JsonSerializerSettings settings = null, Encoding encoding = null, bool reverse = false)
        {
            WriteString(UtilsGen.Json.Serialize(value, settings), encoding, reverse);
        }

        /// <inheritdoc/> 
        public void WriteJsonUTF8<T>(T value, JsonSerializerSettings settings = null, bool reverse = false)
        {
            WriteString(UtilsGen.Json.Serialize(value, settings), Encoding.UTF8, reverse);
        }

        /// <inheritdoc/> 
        public void WriteJsonASCII<T>(T value, JsonSerializerSettings settings = null, bool reverse = false)
        {
            WriteString(UtilsGen.Json.Serialize(value, settings), Encoding.ASCII, reverse);
        }

        /// <inheritdoc/> 
        public void WriteJsonUnicode<T>(T value, JsonSerializerSettings settings = null, bool reverse = false)
        {
            WriteString(UtilsGen.Json.Serialize(value, settings), Encoding.Unicode, reverse);
        }
    }
}
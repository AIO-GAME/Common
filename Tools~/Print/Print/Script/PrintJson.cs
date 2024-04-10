namespace AIO.Internal
{
    /// <summary>
    /// 
    /// </summary>
    internal static class Json
    {
        /// <summary>
        /// 序列化
        /// </summary>
        public static string Serialize(object data)
        {
            return MiniJSON.Encode(data);
        }

        /// <summary>
        /// 序列化
        /// </summary>
        public static string Serialize<T>(T data)
        {
            return MiniJSON.Encode(data);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        public static object Deserialize(string data)
        {
            return MiniJSON.Decode(data);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        public static T Deserialize<T>(string data)
        where T : class
        {
            return MiniJSON.Decode<T>(data);
        }
    }
}
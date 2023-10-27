/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System.Collections;
using AIO;

public partial class AHelper
{
    /// <summary>
    /// Json 工具类
    /// </summary>
    /// <see>
    ///     <cref>https://www.newtonsoft.com/json/help/html/N_Newtonsoft_Json.htm</cref>
    /// </see>
    public static partial class Json
    {
        #region Serialize

        /// <summary>
        /// 序列化
        /// </summary>
        public static string Serialize(in object data)
        {
            return MiniJSON.Encode(data);
        }

        /// <summary>
        /// 序列化
        /// </summary>
        public static string Serialize<T>(in T data)
        {
            return MiniJSON.Encode(data);
        }

        #endregion

        #region 反序列化

        /// <summary>
        /// 反序列化 HashTable
        /// </summary>
        public static Hashtable ToHashTable(in string data)
        {
            return MiniJSON.Decode<Hashtable>(data);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        public static object Deserialize(in string data)
        {
            return MiniJSON.Decode(data);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        public static T Deserialize<T>(in string data)
        {
            return MiniJSON.Decode<T>(data);
        }

        #endregion
    }
}
#region

using System.Collections.Generic;
using System.Text;

#endregion

namespace AIO
{
    /// <summary>
    /// 读取数据
    /// </summary>
    public interface IReadDataJson : IReadString
    {
        /// <summary>
        /// 读取二进制数据
        /// </summary>
        /// <param name="reverse">是否反转读取</param>
        /// <typeparam name="TV">泛型 KEY</typeparam>
        /// <typeparam name="TK">泛型 VALUE</typeparam>
        /// <returns>值</returns>
        Dictionary<TK, TV> ReadDictionary<TK, TV>(bool reverse = false);

        /// <summary>
        /// 读取二进制数据
        /// </summary>
        /// <param name="reverse">是否反转读取</param>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>值</returns>
        List<T> ReadList<T>(bool reverse = false);

        /// <summary>
        /// 读取二进制数据
        /// </summary>
        /// <param name="reverse">是否反转读取</param>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>值</returns>
        T[] ReadArray<T>(bool reverse = false);

        /// <summary>
        /// 读取二进制数据
        /// </summary>
        /// <param name="reverse">是否反转读取</param>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>值</returns>
        T ReadData<T>(bool reverse = false);

        /// <summary>
        /// 读取二进制数据
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="reverse">是否反转读取</param>
        /// <typeparam name="TV">泛型 KEY</typeparam>
        /// <typeparam name="TK">泛型 VALUE</typeparam>
        /// <returns>值</returns>
        void ReadData<TK, TV>(out IDictionary<TK, TV> value, bool reverse = false);

        /// <summary>
        /// 读取二进制数据
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="reverse">是否反转读取</param>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>值</returns>
        void ReadData<T>(out IEnumerable<T> value, bool reverse = false);

        /// <summary>
        /// 读取二进制数据
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="reverse">是否反转读取</param>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>值</returns>
        void ReadData<T>(out T[] value, bool reverse = false);

        /// <summary>
        /// 读取二进制数据
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="reverse">是否反转读取</param>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>值</returns>
        void ReadData<T>(out T value, bool reverse = false);
    }
}
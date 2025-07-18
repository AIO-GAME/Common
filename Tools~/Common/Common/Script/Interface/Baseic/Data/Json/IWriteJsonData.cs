#region

using System.Collections.Generic;
using System.Text;

#endregion

namespace AIO
{
    /// <summary>
    /// 写入指定数据类型
    /// </summary>
    public interface IWriteJsonData : IWriteString
    {
        /// <summary>
        /// 写入二进制数据
        /// </summary>
        /// <typeparam name="TV">泛型 KEY</typeparam>
        /// <typeparam name="TK">泛型 VALUE</typeparam>
        void WriteData<TK, TV>(IDictionary<TK, TV> values, bool reverse = false);

        /// <summary>
        /// 写入二进制数据
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        void WriteData<T>(IEnumerable<T> values, bool reverse = false);

        /// <summary>
        /// 写入二进制数据
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        void WriteData<T>(T[] values, bool reverse = false);

        /// <summary>
        /// 写入二进制数据
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        void WriteData<T>(T val, bool reverse = false);

        /// <summary>
        /// 写入二进制数据
        /// </summary>
        /// <typeparam name="TV">泛型 KEY</typeparam>
        /// <typeparam name="TK">泛型 VALUE</typeparam>
        void WriteDictionary<TK, TV>(IDictionary<TK, TV> values, bool reverse = false);

        /// <summary>
        /// 写入二进制数据
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        void WriteList<T>(IEnumerable<T> values, bool reverse = false);

        /// <summary>
        /// 写入二进制数据
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        void WriteArray<T>(T[] values, bool reverse = false);
    }
}
using System.Runtime.CompilerServices;
using UnityEngine;

public static partial class UtilsEngine
{
    /// <summary>
    /// 持久化数据
    /// </summary>
    public static partial class Prefs
    {
        /// <summary>
        /// 判断是否存在Key
        /// </summary>
        public static bool HasKey(in string key)
        {
            return PlayerPrefs.HasKey(key);
        }

        /// <summary>
        /// 删除全部
        /// </summary>
        public static void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }

        /// <summary>
        /// 删除指定key
        /// </summary>
        public static void DeleteKey(in string key)
        {
            PlayerPrefs.DeleteKey(key);
        }
    }
}
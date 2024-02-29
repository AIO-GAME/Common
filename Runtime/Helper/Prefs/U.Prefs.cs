using UnityEngine;

namespace AIO
{
    partial class RHelper
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

            /// <summary>
            /// 判断游戏是否第一次初始化
            /// </summary>
            public static bool FirstOpen()
            {
#if UNITY_EDITOR
                var key = string.Concat(Application.dataPath, "FirstInit");
                if (UnityEditor.EditorPrefs.HasKey(key)) return false;
                UnityEditor.EditorPrefs.SetInt(key, 1);
#else
                if (PlayerPrefs.HasKey("FirstInit")) return false;
                PlayerPrefs.SetInt("FirstInit", 1);
                PlayerPrefs.Save();
#endif
                return true;
            }
        }
    }
}
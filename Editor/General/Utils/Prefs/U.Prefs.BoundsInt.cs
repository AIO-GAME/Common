/*|============|*|
|*|Author:     |*| xinan                
|*|Date:       |*| 2023-10-28               
|*|E-Mail:     |*| 1398581458@qq.com     
|*|============|*/

using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    public partial class EHelper
    {
        public partial class Prefs
        {
            private const int BoundsIntHashCode = 499176574;
            private const int BoundsIntSizeXHashCode = -321463856;
            private const int BoundsIntSizeYHashCode = 1138493816;
            private const int BoundsIntSizeZHashCode = 2033332867;

            private const int BoundsIntPositionXHashCode = -640319793;
            private const int BoundsIntPositionYHashCode = -1997043283;
            private const int BoundsIntPositionZHashCode = 1771579092;

            private static BoundsInt CommonLoadBoundsInt(in string key, in BoundsInt def)
            {
                var v = new BoundsInt
                {
                    size = new Vector3Int
                    {
                        x = EditorPrefs.GetInt(string.Concat(key, BoundsIntSizeXHashCode), def.size.x),
                        y = EditorPrefs.GetInt(string.Concat(key, BoundsIntSizeYHashCode), def.size.y),
                        z = EditorPrefs.GetInt(string.Concat(key, BoundsIntSizeZHashCode), def.size.z)
                    },
                    position = new Vector3Int
                    {
                        x = EditorPrefs.GetInt(string.Concat(key, BoundsIntPositionXHashCode), def.position.x),
                        y = EditorPrefs.GetInt(string.Concat(key, BoundsIntPositionYHashCode), def.position.y),
                        z = EditorPrefs.GetInt(string.Concat(key, BoundsIntPositionZHashCode), def.position.z)
                    }
                };
                return v;
            }

            private static void CommonSaveBoundsInt(in string key, in BoundsInt value)
            {
                EditorPrefs.SetInt(string.Concat(key, BoundsIntSizeXHashCode), value.size.x);
                EditorPrefs.SetInt(string.Concat(key, BoundsIntSizeYHashCode), value.size.y);
                EditorPrefs.SetInt(string.Concat(key, BoundsIntSizeZHashCode), value.size.z);

                EditorPrefs.SetInt(string.Concat(key, BoundsIntPositionXHashCode), value.position.x);
                EditorPrefs.SetInt(string.Concat(key, BoundsIntPositionYHashCode), value.position.y);
                EditorPrefs.SetInt(string.Concat(key, BoundsIntPositionZHashCode), value.position.z);
            }

            #region 1

            /// <summary>
            /// 加载 Vector
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="def">默认值</param>
            /// <returns>返回值</returns>
            public static BoundsInt LoadBoundsInt(in string field, in BoundsInt def)
            {
                var key = CombineKey(field, BoundsIntHashCode);
                return CommonLoadBoundsInt(key, def);
            }

            /// <summary>
            /// 保存为 Vector
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            public static void SaveBoundsInt(in string field, in BoundsInt value)
            {
                var key = CombineKey(field, BoundsIntHashCode);
                CommonSaveBoundsInt(key, value);
            }

            #endregion

            #region 2

            /// <summary>
            /// 加载 Vector
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="def">默认值</param>
            /// <typeparam name="TData">泛型类型</typeparam>
            /// <returns>返回值</returns>
            public static BoundsInt LoadBoundsInt<TData>(in string field, in BoundsInt def = default)
            {
                var key = CombineKey<TData>(field, BoundsIntHashCode);
                return CommonLoadBoundsInt(key, def);
            }

            /// <summary>
            /// 保存为 Vector
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            /// <typeparam name="TData">泛型类型</typeparam>
            public static void SaveBoundsInt<TData>(in string field, in BoundsInt value)
            {
                var key = CombineKey<TData>(field, BoundsIntHashCode);
                CommonSaveBoundsInt(key, value);
            }

            #endregion

            #region 3

            /// <summary>
            /// 加载 Vector
            /// </summary>
            /// <param name="data">值类型</param>
            /// <param name="field">字段名称</param>
            /// <param name="def">默认值</param>
            /// <typeparam name="TData">泛型类型</typeparam>
            /// <returns>返回值</returns>
            public static BoundsInt LoadBoundsInt<TData>(in TData data, in string field, in BoundsInt def = default)
            {
                var key = CombineKey<TData>(field, BoundsIntHashCode);
                return CommonLoadBoundsInt(key, def);
            }

            /// <summary>
            /// 保存为 Vector
            /// </summary>
            /// <param name="data">类型</param>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            public static void SaveBoundsInt<TData>(TData data, in string field, in BoundsInt value)
            {
                var key = CombineKey<TData>(field, BoundsIntHashCode);
                CommonSaveBoundsInt(key, value);
            }

            #endregion
        }
    }
}
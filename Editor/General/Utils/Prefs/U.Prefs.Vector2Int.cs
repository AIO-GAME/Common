/*|============|*|
|*|Author:     |*| xinan                
|*|Date:       |*| 2023-10-28               
|*|E-Mail:     |*| 1398581458@qq.com     
|*|============|*/

using System;
using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    public partial class EHelper
    {
        public partial class Prefs
        {
            private const int VectorIntHashCode = -198387193;
            private const int VectorIntXHashCode = -198387193;
            private const int VectorIntYHashCode = -198387193;
            private const int VectorIntZHashCode = -198387193;
            private const int VectorIntWHashCode = -198387193;

            private static Vector2Int CommonLoadVector2Int(in string key, in Vector2Int def)
            {
                var v = new Vector2Int
                {
                    x = EditorPrefs.GetInt(string.Concat(key, VectorIntXHashCode), def.x),
                    y = EditorPrefs.GetInt(string.Concat(key, VectorIntYHashCode), def.y)
                };
                return v;
            }

            private static void CommonSaveVector2Int(in string key, in Vector2Int value)
            {
                EditorPrefs.SetInt(string.Concat(key, VectorIntXHashCode), value.x);
                EditorPrefs.SetInt(string.Concat(key, VectorIntYHashCode), value.y);
            }

            #region 1

            /// <summary>
            /// 加载 Vector
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="def">默认值</param>
            /// <returns>返回值</returns>
            public static Vector2Int LoadVector2Int(in string field, in Vector2Int def)
            {
                var key = string.Concat(field.GetHashCode(), VectorIntHashCode);
                return CommonLoadVector2Int(key, def);
            }

            /// <summary>
            /// 保存为 Vector
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            public static void SaveVector2Int(in string field, in Vector2Int value)
            {
                var key = string.Concat(field.GetHashCode(), VectorIntHashCode);
                CommonSaveVector2Int(key, value);
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
            public static Vector2Int LoadVector2Int<TData>(in string field, in Vector2Int def = default)
            {
                var key = string.Concat(typeof(TData).FullName, field.GetHashCode(), VectorIntHashCode);
                return CommonLoadVector2Int(key, def);
            }

            /// <summary>
            /// 保存为 Vector
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            /// <typeparam name="TData">泛型类型</typeparam>
            public static void SaveVector2Int<TData>(in string field, in Vector2Int value)
            {
                var key = string.Concat(typeof(TData).FullName, field.GetHashCode(), VectorIntHashCode);
                CommonSaveVector2Int(key, value);
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
            public static Vector2Int LoadVector2Int<TData>(in TData data, in string field, in Vector2Int def = default)
            {
                var key = string.Concat(data.GetType().FullName, field.GetHashCode(), VectorIntHashCode);
                return CommonLoadVector2Int(key, def);
            }

            /// <summary>
            /// 保存为 Vector
            /// </summary>
            /// <param name="data">类型</param>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            public static void SaveVector2Int<TData>(TData data, in string field, in Vector2Int value)
            {
                var key = string.Concat(data.GetType().FullName, field.GetHashCode(), VectorIntHashCode);
                CommonSaveVector2Int(key, value);
            }

            #endregion
        }
    }
}
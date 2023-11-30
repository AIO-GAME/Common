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
            private static Vector3Int CommonLoadVector3Int(in string key, in Vector3Int def)
            {
                var v = new Vector3Int
                {
                    x = EditorPrefs.GetInt(string.Concat(key, VectorIntXHashCode), def.x),
                    y = EditorPrefs.GetInt(string.Concat(key, VectorIntYHashCode), def.y),
                    z = EditorPrefs.GetInt(string.Concat(key, VectorIntZHashCode), def.z)
                };
                return v;
            }

            private static void CommonSaveVector3Int(in string key, in Vector3Int value)
            {
                EditorPrefs.SetInt(string.Concat(key, VectorIntXHashCode), value.x);
                EditorPrefs.SetInt(string.Concat(key, VectorIntYHashCode), value.y);
                EditorPrefs.SetInt(string.Concat(key, VectorIntZHashCode), value.z);
            }

            #region 1

            /// <summary>
            /// 加载 Vector
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="def">默认值</param>
            /// <returns>返回值</returns>
            public static Vector3Int LoadVector3Int(in string field, in Vector3Int def)
            {
                var key = CombineKey(field, VectorIntHashCode);
                return CommonLoadVector3Int(key, def);
            }

            /// <summary>
            /// 保存为 Vector
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            public static void SaveVector3Int(in string field, in Vector3Int value)
            {
                var key = CombineKey(field, VectorIntHashCode);
                CommonSaveVector3Int(key, value);
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
            public static Vector3Int LoadVector3Int<TData>(in string field, in Vector3Int def = default)
            {
                var key = CombineKey<TData>(field, VectorIntHashCode);
                return CommonLoadVector3Int(key, def);
            }

            /// <summary>
            /// 保存为 Vector
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            /// <typeparam name="TData">泛型类型</typeparam>
            public static void SaveVector3Int<TData>(in string field, in Vector3Int value)
            {
                var key = CombineKey<TData>(field, VectorIntHashCode);
                CommonSaveVector3Int(key, value);
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
            public static Vector3Int LoadVector3Int<TData>(in TData data, in string field, in Vector3Int def = default)
            {
                var key = CombineKey<TData>(field, VectorIntHashCode);
                return CommonLoadVector3Int(key, def);
            }

            /// <summary>
            /// 保存为 Vector
            /// </summary>
            /// <param name="data">类型</param>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            public static void SaveVector3Int<TData>(TData data, in string field, in Vector3Int value)
            {
                var key = CombineKey<TData>(field, VectorIntHashCode);
                CommonSaveVector3Int(key, value);
            }

            #endregion
        }
    }
}
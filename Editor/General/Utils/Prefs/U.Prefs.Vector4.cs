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
            private static Vector4 CommonLoadVector4(in string key, in Vector4 def)
            {
                var v = new Vector4
                {
                    x = EditorPrefs.GetFloat(string.Concat(key, VectorXHashCode), def.x),
                    y = EditorPrefs.GetFloat(string.Concat(key, VectorYHashCode), def.y),
                    z = EditorPrefs.GetFloat(string.Concat(key, VectorZHashCode), def.z),
                    w = EditorPrefs.GetFloat(string.Concat(key, VectorWHashCode), def.w)
                };
                return v;
            }

            private static void CommonSaveVector4(in string key, in Vector4 value)
            {
                EditorPrefs.SetFloat(string.Concat(key, VectorXHashCode), value.x);
                EditorPrefs.SetFloat(string.Concat(key, VectorYHashCode), value.y);
                EditorPrefs.SetFloat(string.Concat(key, VectorZHashCode), value.z);
                EditorPrefs.SetFloat(string.Concat(key, VectorWHashCode), value.w);
            }

            #region 1

            /// <summary>
            /// 加载 Vector
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="def">默认值</param>
            /// <returns>返回值</returns>
            public static Vector4 LoadVector4(in string field, in Vector4 def)
            {
                var key = string.Concat(field.GetHashCode(), VectorHashCode);
                return CommonLoadVector4(key, def);
            }

            /// <summary>
            /// 保存为 Vector
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            public static void SaveVector4(in string field, in Vector4 value)
            {
                var key = string.Concat(field.GetHashCode(), VectorHashCode);
                CommonSaveVector4(key, value);
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
            public static Vector4 LoadVector4<TData>(in string field, in Vector4 def = default)
            {
                var key = string.Concat(typeof(TData).FullName, field.GetHashCode(), VectorHashCode);
                return CommonLoadVector4(key, def);
            }

            /// <summary>
            /// 保存为 Vector
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            /// <typeparam name="TData">泛型类型</typeparam>
            public static void SaveVector4<TData>(in string field, in Vector4 value)
            {
                var key = string.Concat(typeof(TData).FullName, field.GetHashCode(), VectorHashCode);
                CommonSaveVector4(key, value);
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
            public static Vector4 LoadVector4<TData>(in TData data, in string field, in Vector4 def = default)
            {
                var key = string.Concat(data.GetType().FullName, field.GetHashCode(), VectorHashCode);
                return CommonLoadVector4(key, def);
            }

            /// <summary>
            /// 保存为 Vector
            /// </summary>
            /// <param name="data">类型</param>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            public static void SaveVector4<TData>(TData data, in string field, in Vector4 value)
            {
                var key = string.Concat(data.GetType().FullName, field.GetHashCode(), VectorHashCode);
                CommonSaveVector4(key, value);
            }

            #endregion
        }
    }
}
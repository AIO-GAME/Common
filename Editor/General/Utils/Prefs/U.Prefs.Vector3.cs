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
            private static Vector3 CommonLoadVector3(in string key, in Vector3 def)
            {
                var v = new Vector3
                {
                    x = EditorPrefs.GetFloat(string.Concat(key, VectorXHashCode), def.x),
                    y = EditorPrefs.GetFloat(string.Concat(key, VectorYHashCode), def.y),
                    z = EditorPrefs.GetFloat(string.Concat(key, VectorZHashCode), def.z)
                };
                return v;
            }

            private static void CommonSaveVector3(in string key, in Vector3 value)
            {
                EditorPrefs.SetFloat(string.Concat(key, VectorXHashCode), value.x);
                EditorPrefs.SetFloat(string.Concat(key, VectorYHashCode), value.y);
                EditorPrefs.SetFloat(string.Concat(key, VectorZHashCode), value.z);
            }

            #region 1

            /// <summary>
            /// 加载 Vector
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="def">默认值</param>
            /// <returns>返回值</returns>
            public static Vector3 LoadVector3(in string field, in Vector3 def)
            {
                var key = CombineKey(field, VectorHashCode);
                return CommonLoadVector3(key, def);
            }

            /// <summary>
            /// 保存为 Vector
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            public static void SaveVector3(in string field, in Vector3 value)
            {
                var key = CombineKey(field, VectorHashCode);
                CommonSaveVector3(key, value);
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
            public static Vector3 LoadVector3<TData>(in string field, in Vector3 def = default)
            {
                var key = CombineKey<TData>(field, VectorHashCode);
                return CommonLoadVector3(key, def);
            }

            /// <summary>
            /// 保存为 Vector
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            /// <typeparam name="TData">泛型类型</typeparam>
            public static void SaveVector3<TData>(in string field, in Vector3 value)
            {
                var key = CombineKey<TData>(field, VectorHashCode);
                CommonSaveVector3(key, value);
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
            public static Vector3 LoadVector3<TData>(in TData data, in string field, in Vector3 def = default)
            {
                var key = CombineKey<TData>(field, VectorHashCode);
                return CommonLoadVector3(key, def);
            }

            /// <summary>
            /// 保存为 Vector
            /// </summary>
            /// <param name="data">类型</param>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            public static void SaveVector3<TData>(TData data, in string field, in Vector3 value)
            {
                var key = CombineKey<TData>(field, VectorHashCode);
                CommonSaveVector3(key, value);
            }

            #endregion
        }
    }
}
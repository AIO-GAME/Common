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
            private const int VectorHashCode = -702003490;
            private const int VectorXHashCode = -1068145401;
            private const int VectorYHashCode = -952238499;
            private const int VectorZHashCode = 369083993;
            private const int VectorWHashCode = 1040733742;

            private static Vector2 CommonLoadVector2(in string key, in Vector2 def)
            {
                var v = new Vector2
                {
                    x = EditorPrefs.GetFloat(string.Concat(key, VectorXHashCode), def.x),
                    y = EditorPrefs.GetFloat(string.Concat(key, VectorYHashCode), def.y)
                };
                return v;
            }

            private static void CommonSaveVector2(in string key, in Vector2 value)
            {
                EditorPrefs.SetFloat(string.Concat(key, VectorXHashCode), value.x);
                EditorPrefs.SetFloat(string.Concat(key, VectorYHashCode), value.y);
            }

            #region 1

            /// <summary>
            /// 加载 Vector
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="def">默认值</param>
            /// <returns>返回值</returns>
            public static Vector2 LoadVector2(in string field, in Vector2 def)
            {
                var key = string.Concat(field.GetHashCode(), VectorHashCode);
                return CommonLoadVector2(key, def);
            }

            /// <summary>
            /// 保存为 Vector
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            public static void SaveVector2(in string field, in Vector2 value)
            {
                var key = string.Concat(field.GetHashCode(), VectorHashCode);
                CommonSaveVector2(key, value);
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
            public static Vector2 LoadVector2<TData>(in string field, in Vector2 def = default)
            {
                var key = string.Concat(typeof(TData).FullName, field.GetHashCode(), VectorHashCode);
                return CommonLoadVector2(key, def);
            }

            /// <summary>
            /// 保存为 Vector
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            /// <typeparam name="TData">泛型类型</typeparam>
            public static void SaveVector2<TData>(in string field, in Vector2 value)
            {
                var key = string.Concat(typeof(TData).FullName, field.GetHashCode(), VectorHashCode);
                CommonSaveVector2(key, value);
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
            public static Vector2 LoadVector2<TData>(in TData data, in string field, in Vector2 def = default)
            {
                var key = string.Concat(data.GetType().FullName, field.GetHashCode(), VectorHashCode);
                return CommonLoadVector2(key, def);
            }

            /// <summary>
            /// 保存为 Vector
            /// </summary>
            /// <param name="data">类型</param>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            public static void SaveVector2<TData>(TData data, in string field, in Vector2 value)
            {
                var key = string.Concat(data.GetType().FullName, field.GetHashCode(), VectorHashCode);
                CommonSaveVector2(key, value);
            }

            #endregion
        }
    }
}
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
            private const int QuaternionHashCode = -1869243822;
            private const int QuaternionXHashCode = 2016409937;
            private const int QuaternionYHashCode = 426061899;
            private const int QuaternionZHashCode = 684522924;
            private const int QuaternionWHashCode = 1247521960;
            
            private static Quaternion CommonLoadQuaternion(in string key, in Quaternion def)
            {
                var v = new Quaternion
                {
                    x = EditorPrefs.GetFloat(string.Concat(key, QuaternionXHashCode), def.x),
                    y = EditorPrefs.GetFloat(string.Concat(key, QuaternionYHashCode), def.y),
                    z = EditorPrefs.GetFloat(string.Concat(key, QuaternionZHashCode), def.z),
                    w = EditorPrefs.GetFloat(string.Concat(key, QuaternionWHashCode), def.w)
                };
                return v;
            }

            private static void CommonSaveQuaternion(in string key, in Quaternion value)
            {
                EditorPrefs.SetFloat(string.Concat(key, QuaternionXHashCode), value.x);
                EditorPrefs.SetFloat(string.Concat(key, QuaternionYHashCode), value.y);
                EditorPrefs.SetFloat(string.Concat(key, QuaternionZHashCode), value.z);
                EditorPrefs.SetFloat(string.Concat(key, QuaternionWHashCode), value.w);
            }

            #region 1

            /// <summary>
            /// 加载 Vector
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="def">默认值</param>
            /// <returns>返回值</returns>
            public static Quaternion LoadQuaternion(in string field, in Quaternion def)
            {
                var key = CombineKey(field, QuaternionHashCode);
                return CommonLoadQuaternion(key, def);
            }

            /// <summary>
            /// 保存为 Vector
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            public static void SaveQuaternion(in string field, in Quaternion value)
            {
                var key = CombineKey(field, QuaternionHashCode);
                CommonSaveQuaternion(key, value);
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
            public static Quaternion LoadQuaternion<TData>(in string field, in Quaternion def = default)
            {
                var key = CombineKey<TData>(field, QuaternionHashCode);
                return CommonLoadQuaternion(key, def);
            }

            /// <summary>
            /// 保存为 Vector
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            /// <typeparam name="TData">泛型类型</typeparam>
            public static void SaveQuaternion<TData>(in string field, in Quaternion value)
            {
                var key = CombineKey<TData>(field, QuaternionHashCode);
                CommonSaveQuaternion(key, value);
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
            public static Quaternion LoadQuaternion<TData>(in TData data, in string field, in Quaternion def = default)
            {
                var key = CombineKey<TData>(field, QuaternionHashCode);
                return CommonLoadQuaternion(key, def);
            }

            /// <summary>
            /// 保存为 Vector
            /// </summary>
            /// <param name="data">类型</param>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            public static void SaveQuaternion<TData>(TData data, in string field, in Quaternion value)
            {
                var key = CombineKey<TData>(field, QuaternionHashCode);
                CommonSaveQuaternion(key, value);
            }

            #endregion
        }
    }
}
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
            private const int BoundsHashCode = -198387193;
            private const int BoundsCenterXHashCode = -198387193;
            private const int BoundsCenterYHashCode = -198387193;
            private const int BoundsCenterZHashCode = -198387193;

            private const int BoundsExtentsXHashCode = -198387193;
            private const int BoundsExtentsYHashCode = -198387193;
            private const int BoundsExtentsZHashCode = -198387193;

            private static Bounds CommonLoadBounds(in string key, in Bounds def)
            {
                var v = new Bounds
                {
                    center = new Vector3
                    {
                        x = EditorPrefs.GetFloat(string.Concat(key, BoundsCenterXHashCode), def.center.x),
                        y = EditorPrefs.GetFloat(string.Concat(key, BoundsCenterYHashCode), def.center.y),
                        z = EditorPrefs.GetFloat(string.Concat(key, BoundsCenterZHashCode), def.center.z)
                    },
                    extents = new Vector3
                    {
                        x = EditorPrefs.GetFloat(string.Concat(key, BoundsExtentsXHashCode), def.extents.x),
                        y = EditorPrefs.GetFloat(string.Concat(key, BoundsExtentsYHashCode), def.extents.y),
                        z = EditorPrefs.GetFloat(string.Concat(key, BoundsExtentsZHashCode), def.extents.z)
                    }
                };
                return v;
            }

            private static void CommonSaveBounds(in string key, in Bounds value)
            {
                EditorPrefs.SetFloat(string.Concat(key, BoundsCenterXHashCode), value.center.x);
                EditorPrefs.SetFloat(string.Concat(key, BoundsCenterYHashCode), value.center.y);
                EditorPrefs.SetFloat(string.Concat(key, BoundsCenterZHashCode), value.center.z);

                EditorPrefs.SetFloat(string.Concat(key, BoundsExtentsXHashCode), value.extents.x);
                EditorPrefs.SetFloat(string.Concat(key, BoundsExtentsYHashCode), value.extents.y);
                EditorPrefs.SetFloat(string.Concat(key, BoundsExtentsZHashCode), value.extents.z);
            }

            #region 1

            /// <summary>
            /// 加载 Vector
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="def">默认值</param>
            /// <returns>返回值</returns>
            public static Bounds LoadBounds(in string field, in Bounds def)
            {
                var key = string.Concat(field.GetHashCode(), BoundsHashCode);
                return CommonLoadBounds(key, def);
            }

            /// <summary>
            /// 保存为 Vector
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            public static void SaveBounds(in string field, in Bounds value)
            {
                var key = string.Concat(field.GetHashCode(), BoundsHashCode);
                CommonSaveBounds(key, value);
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
            public static Bounds LoadBounds<TData>(in string field, in Bounds def = default)
            {
                var key = string.Concat(typeof(TData).FullName, field.GetHashCode(), BoundsHashCode);
                return CommonLoadBounds(key, def);
            }

            /// <summary>
            /// 保存为 Vector
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            /// <typeparam name="TData">泛型类型</typeparam>
            public static void SaveBounds<TData>(in string field, in Bounds value)
            {
                var key = string.Concat(typeof(TData).FullName, field.GetHashCode(), BoundsHashCode);
                CommonSaveBounds(key, value);
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
            public static Bounds LoadBounds<TData>(in TData data, in string field, in Bounds def = default)
            {
                var key = string.Concat(data.GetType().FullName, field.GetHashCode(), BoundsHashCode);
                return CommonLoadBounds(key, def);
            }

            /// <summary>
            /// 保存为 Vector
            /// </summary>
            /// <param name="data">类型</param>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            public static void SaveBounds<TData>(TData data, in string field, in Bounds value)
            {
                var key = string.Concat(data.GetType().FullName, field.GetHashCode(), BoundsHashCode);
                CommonSaveBounds(key, value);
            }

            #endregion
        }
    }
}
/*|============|*|
|*|Author:     |*| xinan                
|*|Date:       |*| 2023-10-28               
|*|E-Mail:     |*| 1398581458@qq.com     
|*|============|*/

using System;
using UnityEditor;

namespace AIO.UEditor
{
    public partial class EHelper
    {
        public partial class Prefs
        {
            private const int EnumHashCode = -198387193;

            private static TEnum CommonLoadEnum<TEnum>(in string key, in TEnum def) where TEnum : struct, Enum
            {
                if (!EditorPrefs.HasKey(key)) return def;
                var value = EditorPrefs.GetInt(key, def.GetHashCode());
                return Enum.TryParse<TEnum>(value.ToString(), out var result) ? result : def;
            }

            private static void CommonSaveEnum<TEnum>(in string key, in TEnum value) where TEnum : struct, Enum
            {
                EditorPrefs.SetInt(key, value.GetHashCode());
            }

            #region 1

            /// <summary>
            /// 加载 Enum
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="def">默认值</param>
            /// <returns>返回值</returns>
            public static TEnum LoadEnum<TEnum>(in string field, in TEnum def) where TEnum : struct, Enum
            {
                var key = string.Concat(field.GetHashCode(), EnumHashCode);
                return CommonLoadEnum(key, def);
            }

            /// <summary>
            /// 保存为 Enum
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            public static void SaveEnum<TEnum>(in string field, in TEnum value) where TEnum : struct, Enum
            {
                var key = string.Concat(field.GetHashCode(), EnumHashCode);
                CommonSaveEnum(key, value);
            }

            #endregion

            #region 2

            /// <summary>
            /// 加载 Enum
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="def">默认值</param>
            /// <typeparam name="TData">泛型类型</typeparam>
            /// <typeparam name="TEnum">泛型类型</typeparam>
            /// <returns>返回值</returns>
            public static TEnum LoadEnum<TData, TEnum>(in string field, in TEnum def = default) where TEnum : struct, Enum
            {
                var key = string.Concat(typeof(TData).FullName, field.GetHashCode(), EnumHashCode);
                return CommonLoadEnum(key, def);
            }

            /// <summary>
            /// 保存为 Enum
            /// </summary>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            /// <typeparam name="TData">泛型类型</typeparam>
            /// <typeparam name="TEnum">泛型类型</typeparam>
            public static void SaveEnum<TData, TEnum>(in string field, in TEnum value) where TEnum : struct, Enum
            {
                var key = string.Concat(typeof(TData).FullName, field.GetHashCode(), EnumHashCode);
                CommonSaveEnum(key, value);
            }

            #endregion

            #region 3

            /// <summary>
            /// 加载 Enum
            /// </summary>
            /// <param name="data">值类型</param>
            /// <param name="field">字段名称</param>
            /// <param name="def">默认值</param>
            /// <typeparam name="TData">泛型类型</typeparam>
            /// <typeparam name="TEnum">泛型类型</typeparam>
            /// <returns>返回值</returns>
            public static TEnum LoadEnum<TData, TEnum>(in TData data, in string field, in TEnum def = default) where TEnum : struct, Enum
            {
                var key = string.Concat(data.GetType().FullName, field.GetHashCode(), EnumHashCode);
                return CommonLoadEnum(key, def);
            }

            /// <summary>
            /// 保存为 Enum
            /// </summary>
            /// <param name="data">类型</param>
            /// <param name="field">字段名称</param>
            /// <param name="value">值</param>
            /// <typeparam name="TData">泛型类型</typeparam>
            /// <typeparam name="TEnum">泛型类型</typeparam>
            public static void SaveEnum<TData, TEnum>(TData data, in string field, in TEnum value) where TEnum : struct, Enum
            {
                var key = string.Concat(data.GetType().FullName, field.GetHashCode(), EnumHashCode);
                CommonSaveEnum(key, value);
            }

            #endregion
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;
using AUtils = Utils;

namespace UnityEngine
{
    public static partial class UtilsEngine
    {
        public static partial class Prefs
        {
            #region String

            /// <summary>
            /// 保存为字符串
            /// </summary>
            public static void SaveString(in string key, in string value)
            {
                PlayerPrefs.SetString(string.Concat(key, "_String"), value);
                PlayerPrefs.Save();
            }

            /// <summary>
            /// 保存为字符串
            /// </summary>
            public static void SaveString<T>(in string field, in string value)
            {
                var fullName = typeof(T).FullName;
                if (!string.IsNullOrEmpty(fullName))
                {
                    PlayerPrefs.SetString(string.Concat(fullName.GetHashCode(), field.GetHashCode()), value);
                    PlayerPrefs.Save();
                }
            }

            /// <summary>
            /// 保存为字符串
            /// </summary>
            public static void SaveString<T>(in T clazz, in string field, in string value)
            {
                if (clazz == null) throw new ArgumentNullException(nameof(clazz));
                var fullName = typeof(T).FullName;
                if (!string.IsNullOrEmpty(fullName))
                {
                    PlayerPrefs.SetString(string.Concat(fullName.GetHashCode(), field.GetHashCode()), value);
                    PlayerPrefs.Save();
                }
            }

            #endregion

            #region Boolean

            /// <summary>
            /// 保存为Bool
            /// </summary>
            public static void SaveBoolean(in string key, in bool value)
            {
                PlayerPrefs.SetInt(string.Concat(key, "_Boolean"), value ? 1 : 0);
                PlayerPrefs.Save();
            }

            /// <summary>
            /// 保存为Bool
            /// </summary>
            public static void SaveBoolean<T>(in T clazz, in string field, in bool value)
            {
                if (clazz == null) throw new ArgumentNullException(nameof(clazz));
                var fullName = typeof(T).FullName;
                if (!string.IsNullOrEmpty(fullName))
                {
                    PlayerPrefs.SetInt(string.Concat(fullName.GetHashCode(), field.GetHashCode()), value ? 1 : 0);
                    PlayerPrefs.Save();
                }
            }

            /// <summary>
            /// 保存为Bool
            /// </summary>
            public static void SaveBoolean<T>(in string field, in bool value)
            {
                var fullName = typeof(T).FullName;
                if (!string.IsNullOrEmpty(fullName))
                {
                    PlayerPrefs.SetInt(string.Concat(fullName.GetHashCode(), field.GetHashCode()), value ? 1 : 0);
                    PlayerPrefs.Save();
                }
            }

            #endregion

            #region Int

            /// <summary>
            /// 保存为Int
            /// </summary>
            public static void SaveInt<T>(in T clazz, in string field, in int value)
            {
                var fullName = typeof(T).FullName;
                if (!string.IsNullOrEmpty(fullName))
                {
                    PlayerPrefs.SetInt(string.Concat(fullName.GetHashCode(), field.GetHashCode()), value);
                    PlayerPrefs.Save();
                }
            }

            /// <summary>
            /// 保存为Int
            /// </summary>
            public static void SaveInt<T>(in string field, in int value)
            {
                var fullName = typeof(T).FullName;
                if (!string.IsNullOrEmpty(fullName))
                {
                    PlayerPrefs.SetInt(string.Concat(fullName.GetHashCode(), field.GetHashCode()), value);
                    PlayerPrefs.Save();
                }
            }

            /// <summary>
            /// 保存为Int
            /// </summary>
            public static void SaveInt(in string key, in int value)
            {
                PlayerPrefs.SetInt(string.Concat(key, "_Int"), value);
                PlayerPrefs.Save();
            }

            #endregion

            #region Enum

            /// <summary>
            /// 保存为Enum
            /// </summary>
            public static void SaveEnum(string key, Enum value)
            {
                PlayerPrefs.SetInt(string.Concat(key, "_Enum"), value.GetHashCode());
                PlayerPrefs.Save();
            }

            /// <summary>
            /// 保存为Enum
            /// </summary>
            public static void SaveEnum<T1, T2>(T1 key, in string field, T2 value) where T2 : struct, Enum
            {
                var fullName = key.GetType().FullName;
                if (!string.IsNullOrEmpty(fullName))
                {
                    PlayerPrefs.SetInt(string.Concat(fullName.GetHashCode(), field.GetHashCode()), value.GetHashCode());
                    PlayerPrefs.Save();
                }
            }

            /// <summary>
            /// 保存为Enum
            /// </summary>
            public static void SaveEnum<T1, T2>(T1 key, T2 value) where T2 : struct, Enum
            {
                var fullName = key.GetType().FullName;
                if (!string.IsNullOrEmpty(fullName))
                {
                    PlayerPrefs.SetInt(string.Concat(fullName.GetHashCode()), value.GetHashCode());
                    PlayerPrefs.Save();
                }
            }

            #endregion

            /// <summary>
            /// 保存为Vector
            /// </summary>
            public static void SaveVector2(in string key, in Vector2 value)
            {
                PlayerPrefs.SetFloat(string.Concat(key, "_v2_x"), value.x);
                PlayerPrefs.SetFloat(string.Concat(key, "_v2_y"), value.y);
                PlayerPrefs.Save();
            }

            /// <summary>
            /// 保存为Vector
            /// </summary>
            public static void SaveVector3(in string key, in Vector3 value)
            {
                PlayerPrefs.SetFloat(string.Concat(key, "_v3_x"), value.x);
                PlayerPrefs.SetFloat(string.Concat(key, "_v3_y"), value.y);
                PlayerPrefs.SetFloat(string.Concat(key, "_v3_z"), value.z);
                PlayerPrefs.Save();
            }

            /// <summary>
            /// 保存为Vector
            /// </summary>
            public static void SaveVector4(in string key, in Vector4 value)
            {
                PlayerPrefs.SetFloat(string.Concat(key, "_v4_x"), value.x);
                PlayerPrefs.SetFloat(string.Concat(key, "_v4_y"), value.y);
                PlayerPrefs.SetFloat(string.Concat(key, "_v4_z"), value.z);
                PlayerPrefs.SetFloat(string.Concat(key, "_v4_w"), value.w);
                PlayerPrefs.Save();
            }

            /// <summary>
            /// 保存为Quaternion
            /// </summary>
            public static void SaveQuaternion(in string key, in Quaternion value)
            {
                PlayerPrefs.SetFloat(key + "_Quaternion_x", value.x);
                PlayerPrefs.SetFloat(key + "_Quaternion_y", value.y);
                PlayerPrefs.SetFloat(key + "_Quaternion_z", value.z);
                PlayerPrefs.SetFloat(key + "_Quaternion_w", value.w);
                PlayerPrefs.Save();
            }

            /// <summary>
            /// 保存为Dictionary
            /// </summary>
            public static void SaveDictionary<K, V>(in string key, in IDictionary<K, V> values)
            {
                PlayerPrefs.SetString(string.Concat(key, "_IDictionary"), AUtils.Json.Serialize(values));
                PlayerPrefs.Save();
            }

            /// <summary>
            /// 保存为List
            /// </summary>
            public static void SaveList<V>(in string key, in IList<V> values)
            {
                PlayerPrefs.SetString(string.Concat(key, "_IList"), AUtils.Json.Serialize(values));
                PlayerPrefs.Save();
            }

            /// <summary>
            /// 保存为Collection
            /// </summary>
            public static void SaveCollection<V>(in string key, in ICollection<V> values)
            {
                PlayerPrefs.SetString(string.Concat(key, "_ICollection"), AUtils.Json.Serialize(values));
                PlayerPrefs.Save();
            }

            /// <summary>
            /// 保存为JsonData
            /// </summary>
            public static void SaveJsonData<T>(in string key, in T value)
            {
                PlayerPrefs.SetString(string.Concat(key, "_Json"), AUtils.Json.Serialize(value));
                PlayerPrefs.Save();
            }

            /// <summary>
            /// 保存为Float
            /// </summary>
            public static void SaveFloat(in string key, in float value)
            {
                PlayerPrefs.SetFloat(string.Concat(key, "_Float"), value);
                PlayerPrefs.Save();
            }
        }
    }
}
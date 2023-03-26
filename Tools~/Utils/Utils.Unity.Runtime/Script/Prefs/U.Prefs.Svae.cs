using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using UnityEngine;

public static partial class UtilsEngine
{
	public static partial class Prefs
	{
	
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SaveString(in string key, in string value)
		{
			PlayerPrefs.SetString(string.Concat(key, "_String"), value);
			PlayerPrefs.Save();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SaveString<T>(in T @class, in string field, in string value)
		{
			PlayerPrefs.SetString(string.Concat(@class.GetType().FullName.GetHashCode(), field.GetHashCode()), value);
			PlayerPrefs.Save();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SaveBoolean(in string key, in bool value)
		{
			PlayerPrefs.SetInt(string.Concat(key, "_Boolean"), value ? 1 : 0);
			PlayerPrefs.Save();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SaveBoolean<T>(in T @class, in string field, in bool value)
		{
			PlayerPrefs.SetInt(string.Concat(@class.GetType().FullName.GetHashCode(), field.GetHashCode()), value ? 1 : 0);
			PlayerPrefs.Save();
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SaveInt<T>(in T @class, in string field, in int value)
		{
			PlayerPrefs.SetInt(string.Concat(@class.GetType().FullName.GetHashCode(), field.GetHashCode()), value);
			PlayerPrefs.Save();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SaveInt(in string key, in int value)
		{
			PlayerPrefs.SetInt(string.Concat(key, "_Int"), value);
			PlayerPrefs.Save();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SaveEnum(string key, Enum value)
		{
			PlayerPrefs.SetInt(string.Concat(key, "_Enum"), value.GetHashCode());
			PlayerPrefs.Save();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SaveEnum<T1, T2>(T1 key, in string field, T2 value) where T2 : Enum
		{
			PlayerPrefs.SetInt(string.Concat(key.GetType().FullName.GetHashCode(), field.GetHashCode()), value.GetHashCode());
			PlayerPrefs.Save();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SaveEnum<T1, T2>(T1 key, T2 value) where T2 : Enum
		{
			PlayerPrefs.SetInt(string.Concat(key.GetType().FullName.GetHashCode()), value.GetHashCode());
			PlayerPrefs.Save();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SaveVector2(in string key, in Vector2 value)
		{
			PlayerPrefs.SetFloat(string.Concat(key, "_v2_x"), value.x);
			PlayerPrefs.SetFloat(string.Concat(key, "_v2_y"), value.y);
			PlayerPrefs.Save();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SaveVector3(in string key, in Vector3 value)
		{
			PlayerPrefs.SetFloat(string.Concat(key, "_v3_x"), value.x);
			PlayerPrefs.SetFloat(string.Concat(key, "_v3_y"), value.y);
			PlayerPrefs.SetFloat(string.Concat(key, "_v3_z"), value.z);
			PlayerPrefs.Save();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SaveVector4(in string key, in Vector4 value)
		{
			PlayerPrefs.SetFloat(string.Concat(key, "_v4_x"), value.x);
			PlayerPrefs.SetFloat(string.Concat(key, "_v4_y"), value.y);
			PlayerPrefs.SetFloat(string.Concat(key, "_v4_z"), value.z);
			PlayerPrefs.SetFloat(string.Concat(key, "_v4_w"), value.w);
			PlayerPrefs.Save();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SaveQuaternion(in string key, in Quaternion value)
		{
			PlayerPrefs.SetFloat(key + "_Quaternion_x", value.x);
			PlayerPrefs.SetFloat(key + "_Quaternion_y", value.y);
			PlayerPrefs.SetFloat(key + "_Quaternion_z", value.z);
			PlayerPrefs.SetFloat(key + "_Quaternion_w", value.w);
			PlayerPrefs.Save();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SaveDictionary<K, V>(in string key, in IDictionary<K, V> values)
		{
			PlayerPrefs.SetString(string.Concat(key, "_IDictionary"), Utils.Json.Serialize(values));
			PlayerPrefs.Save();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SaveList<V>(in string key, in IList<V> values)
		{
			PlayerPrefs.SetString(string.Concat(key, "_List"), Utils.Json.Serialize(values));
			PlayerPrefs.Save();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SaveCollection<V>(in string key, in ICollection<V> values)
		{
			PlayerPrefs.SetString(string.Concat(key, "_Collection"), Utils.Json.Serialize(values));
			PlayerPrefs.Save();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SaveJsonData<T>(in string key, in T value)
		{
			PlayerPrefs.SetString(string.Concat(key, "_Json"), Utils.Json.Serialize(value));
			PlayerPrefs.Save();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SaveFloat(in string key, in float value)
		{
			PlayerPrefs.SetFloat(string.Concat(key, "_Float"), value);
			PlayerPrefs.Save();
		}
	}
}
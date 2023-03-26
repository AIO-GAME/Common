using System;
using System.Runtime.CompilerServices;

using UnityEngine;

public static partial class UtilsEngine
{
    public static partial class Prefs
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string LoadString<T>(in T key, in string field)
        {
            return PlayerPrefs.GetString(string.Concat(key.GetType().FullName.GetHashCode(), field.GetHashCode()));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int LoadInt<T>(in T key, in string field)
        {
            return PlayerPrefs.GetInt(string.Concat(key.GetType().FullName.GetHashCode(), field.GetHashCode()));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LoadBool<T>(in T key, in string field)
        {
            return PlayerPrefs.GetInt(string.Concat(key.GetType().FullName.GetHashCode(), field.GetHashCode())) == 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string LoadString(in string key, in string def = null)
        {
            if (PlayerPrefs.HasKey(string.Concat(key, "_String"))) return def;
            return PlayerPrefs.GetString(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int LoadInt(in string key, in int def = 0)
        {
            if (PlayerPrefs.HasKey(string.Concat(key, "_Int"))) return def;
            return PlayerPrefs.GetInt(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T LoadEnum<T>(in string key, in T def = default) where T : Enum
        {
            if (PlayerPrefs.HasKey(string.Concat(key, "_Enum"))) return def;
            return (T)Enum.Parse(typeof(T), PlayerPrefs.GetInt(key).ToString());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T LoadJsonData<T>(in string key, in T def = default)
        {
            if (PlayerPrefs.HasKey(string.Concat(key, "_Json"))) return def;
            return Utils.Json.Deserialize<T>(PlayerPrefs.GetString(key));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float LoadFloat(in string key, in float def = 0)
        {
            if (PlayerPrefs.HasKey(string.Concat(key, "_Float"))) return def;
            return PlayerPrefs.GetFloat(key);
        }
    }
}


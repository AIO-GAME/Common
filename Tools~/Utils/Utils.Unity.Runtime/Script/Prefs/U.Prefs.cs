using System.Runtime.CompilerServices;

using UnityEngine;

public static partial class UtilsEngine
{
	/// <summary>
	/// 持久化数据
	/// </summary>
	public static partial class Prefs
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasKey(in string key)
        {
            return PlayerPrefs.HasKey(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteKey(in string key)
        {
            PlayerPrefs.DeleteKey(key);
        }
    }
}
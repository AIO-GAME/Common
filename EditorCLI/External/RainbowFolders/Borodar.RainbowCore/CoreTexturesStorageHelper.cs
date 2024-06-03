using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIO.RainbowCore
{
    internal static class CoreTexturesStorageHelper<T>
    {
        public static Texture2D GetTexture(
            T type,
            FilterMode filterMode,
            IDictionary<T, string> strings,
            IDictionary<T, Texture2D> textures)
        {
            if (!textures.TryGetValue(type, out var value))
            {
                value = TextureFromString(strings[type], filterMode);
                textures.Add(type, value);
            }

            return value;
        }

        public static Tuple<Texture2D, Texture2D> GetTextures(
            T type,
            FilterMode filterMode,
            IDictionary<T, Tuple<string, string>> strings,
            IDictionary<T, Tuple<Texture2D, Texture2D>> textures)
        {
            if (!textures.TryGetValue(type, out var value))
            {
                var item = TextureFromString(strings[type].Item1, filterMode);
                var item2 = TextureFromString(strings[type].Item2, filterMode);
                value = Tuple.Create(item, item2);
                textures.Add(type, value);
            }

            return value;
        }

        private static Texture2D TextureFromString(
            string value,
            FilterMode filterMode)
        {
            var obj = new Texture2D(1, 1, TextureFormat.ARGB32, false, false)
            {
                filterMode = filterMode,
                wrapMode = TextureWrapMode.Clamp,
                hideFlags = HideFlags.HideAndDontSave
            };
            obj.LoadImage(Convert.FromBase64String(value));
            return obj;
        }
    }
}
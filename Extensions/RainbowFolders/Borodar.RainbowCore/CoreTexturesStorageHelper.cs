using System;
using System.Collections.Generic;

using UnityEngine;

namespace Borodar.RainbowCore
{
    public static class CoreTexturesStorageHelper<T>
    {
        public static Texture2D GetTexture(T type, FilterMode filterMode, Dictionary<T, string> strings, Dictionary<T, Texture2D> textures)
        {
            if (!textures.TryGetValue(type, out var value))
            {
                value = TextureFromString(strings[type], filterMode);
                textures.Add(type, value);
            }
            return value;
        }

        public static Tuple<Texture2D, Texture2D> GetTextures(T type, FilterMode filterMode, Dictionary<T, Tuple<string, string>> strings, Dictionary<T, Tuple<Texture2D, Texture2D>> textures)
        {
            if (!textures.TryGetValue(type, out var value))
            {
                Texture2D item = TextureFromString(strings[type].Item1, filterMode);
                Texture2D item2 = TextureFromString(strings[type].Item2, filterMode);
                value = Tuple.Create(item, item2);
                textures.Add(type, value);
            }
            return value;
        }

        private static Texture2D TextureFromString(string value, FilterMode filterMode)
        {
            Texture2D obj = new Texture2D(0, 0, TextureFormat.ARGB32, mipChain: false, linear: false)
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

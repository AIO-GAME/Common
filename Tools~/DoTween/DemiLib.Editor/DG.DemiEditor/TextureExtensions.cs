using UnityEditor;

using UnityEngine;

namespace DG.DemiEditor
{
    /// <summary>
    /// Texture extensions
    /// </summary>
    public static class TextureExtensions
    {
        /// <summary>
        /// Returns the full Rect of this texture, with options for position and scale
        /// </summary>
        public static Rect GetRect(this Texture2D texture, float x = 0f, float y = 0f, float scale = 1f)
        {
            return new Rect(x, y, (float)texture.width * scale, (float)texture.height * scale);
        }

        /// <summary>
        /// Checks that the texture uses the correct import settings, and applies them if they're incorrect.
        /// </summary>
        public static void SetGUIFormat(this Texture2D texture, FilterMode filterMode = FilterMode.Point, int maxTextureSize = 32, TextureWrapMode wrapMode = TextureWrapMode.Clamp, int quality = 100)
        {
            string assetPath = AssetDatabase.GetAssetPath(texture);
            TextureImporter textureImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            if (textureImporter.textureType != TextureImporterType.GUI
                || textureImporter.npotScale != 0
                || textureImporter.filterMode != filterMode
                || textureImporter.wrapMode != wrapMode
                || textureImporter.maxTextureSize != maxTextureSize
                //|| (textureImporter.textureFormat != 0 && textureImporter.textureFormat != TextureImporterFormat.AutomaticTruecolor)
                || (textureImporter.textureCompression != 0 && textureImporter.textureCompression != TextureImporterCompression.Compressed)
                || textureImporter.compressionQuality != quality)
            {
                textureImporter.textureType = TextureImporterType.GUI;
                textureImporter.npotScale = TextureImporterNPOTScale.None;
                textureImporter.filterMode = filterMode;
                textureImporter.wrapMode = wrapMode;
                textureImporter.maxTextureSize = maxTextureSize;
                //textureImporter.textureFormat = TextureImporterFormat.AutomaticTruecolor;
                textureImporter.textureCompression = TextureImporterCompression.Compressed;
                textureImporter.compressionQuality = quality;
                AssetDatabase.ImportAsset(assetPath);
            }
        }
    }
}

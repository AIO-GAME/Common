/*|============|*|
|*|Author:     |*| xinan
|*|Date:       |*| 2025-10-13
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

using System;
using System.IO;
using UnityEngine;
using UnityEngine.Scripting;
using Object = UnityEngine.Object;

namespace AIO
{
    /// <summary>
    /// 截图工具
    /// </summary>
    [Preserve]
    public static class ScreenshotCapture
    {
        /// <summary>
        /// 将截图保存到桌面
        /// </summary>
        [Preserve]
        public static void Capture()
        {
            string saveDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            int    fileIndex     = 0;
            string path;
            do { path = Path.Combine(saveDirectory, $"Screenshot {++fileIndex}.jpeg"); } while (File.Exists(path));

            Capture(path);
        }

        /// <summary>
        /// 将截图保存到指定路径
        /// </summary>
        /// <param name="path"> 保存路径 </param>
        [Preserve]
        public static void Capture(string path) { Capture(Camera.main, RenderTexture.active, path); }

        /// <summary>
        /// 将截图保存到指定路径
        /// </summary>
        /// <param name="texture"> 纹理 </param>
        /// <param name="path"> 保存路径 </param>
        [Preserve]
        public static void Capture(RenderTexture texture, string path) { Capture(Camera.main, texture, path); }

        /// <summary>
        /// 将截图保存到指定路径
        /// </summary>
        /// <param name="camera"> 摄像机 </param>
        /// <param name="path"> 保存路径 </param>
        [Preserve]
        public static void Capture(Camera camera, string path) { Capture(camera, RenderTexture.active, path); }

        /// <summary>
        /// 将截图保存到指定路径
        /// </summary>
        /// <param name="camera"> 摄像机 </param>
        /// <param name="texture"> 纹理 </param>
        /// <param name="path"> 保存路径 </param>
        [Preserve]
        public static void Capture(Camera camera, RenderTexture texture, string path)
        {
            Texture2D screenshot = null;
            var       temp2      = camera.targetTexture;
            var       renderTex  = RenderTexture.GetTemporary(Screen.width, Screen.height, 24);
            try
            {
                RenderTexture.active = renderTex;
                camera.targetTexture = renderTex;
                camera.Render();
                screenshot = new Texture2D(renderTex.width, renderTex.height, TextureFormat.RGB24, false);
                screenshot.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0, false);
                screenshot.Apply(false, false);

                File.WriteAllBytes(path, screenshot.EncodeToJPG());
            }
            finally
            {
                camera.targetTexture = temp2;
                RenderTexture.active = texture;
                RenderTexture.ReleaseTemporary(renderTex);

                if (screenshot != null)
                    Object.DestroyImmediate(screenshot);
            }
        }
    }
}
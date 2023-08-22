/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-08-15
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

#if SUPPORT_UNITASK

using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace AIO.UEngine
{
    internal partial class YAssetSystem
    {
        /// <summary>
        /// 网上加载图片
        /// </summary>
        /// <param name="url">网址</param>
        /// <param name="cb">回调</param>
        public static async void LoadTextureNet(string url, Action<Texture2D> cb)
        {
            using (var uwr = UnityWebRequestTexture.GetTexture(url))
            {
                await uwr.SendWebRequest();
                if (LoadCheckNet(uwr))
                    cb?.Invoke(DownloadHandlerTexture.GetContent(uwr));
                else
                    cb?.Invoke(null);
            }
        }

        /// <summary>
        /// 网上加载图片
        /// </summary>
        /// <param name="url">网址</param>
        /// <param name="rect">矩形</param>
        /// <param name="pivot">中心点</param>
        /// <param name="cb">回调</param>
        public static async void LoadSpriteNet(string url, Rect rect, Vector2 pivot, Action<Sprite> cb)
        {
            using (var uwr = UnityWebRequestTexture.GetTexture(url))
            {
                await uwr.SendWebRequest();
                if (LoadCheckNet(uwr))
                    cb?.Invoke(Sprite.Create(DownloadHandlerTexture.GetContent(uwr), rect, pivot));
                else
                    cb?.Invoke(null);
            }
        }

        /// <summary>
        /// 网上加载文本
        /// </summary>
        /// <param name="url">网址</param>
        /// <param name="cb">回调</param>
        public static async void LoadStringNet(string url, Action<string> cb)
        {
            using (var uwr = UnityWebRequest.Get(url))
            {
                await uwr.SendWebRequest();
                if (LoadCheckNet(uwr))
                    cb?.Invoke(uwr.downloadHandler.text);
                else
                    cb?.Invoke(null);
            }
        }

        /// <summary>
        /// 网上加载字节
        /// </summary>
        /// <param name="url">网址</param>
        /// <param name="cb">回调</param>
        public static async void LoadSteamNet(string url, Action<byte[]> cb)
        {
            using (var uwr = UnityWebRequest.Get(url))
            {
                await uwr.SendWebRequest();
                if (LoadCheckNet(uwr))
                    cb?.Invoke(uwr.downloadHandler.data);
                else
                    cb?.Invoke(null);
            }
        }

        /// <summary>
        /// 网上加载音频
        /// </summary>
        /// <param name="url">网址</param>
        /// <param name="audioType">音频类型</param>
        /// <param name="cb">回调</param>
        public static async void LoadAudioClipNet(string url, AudioType audioType, Action<AudioClip> cb)
        {
            using (var uwr = UnityWebRequestMultimedia.GetAudioClip(url, audioType))
            {
                await uwr.SendWebRequest();
                if (LoadCheckNet(uwr))
                    cb?.Invoke(DownloadHandlerAudioClip.GetContent(uwr));
                else
                    cb?.Invoke(null);
            }
        }

        /// <summary>
        /// 网上加载AB包
        /// </summary>
        /// <param name="url">网址</param>
        /// <param name="cb">回调</param>
        public static async void LoadAssetBundleNet(string url, Action<AssetBundle> cb)
        {
            using (var uwr = UnityWebRequestAssetBundle.GetAssetBundle(url))
            {
                await uwr.SendWebRequest();
                if (LoadCheckNet(uwr))
                    cb?.Invoke(DownloadHandlerAssetBundle.GetContent(uwr));
                else
                    cb?.Invoke(null);
            }
        }
    }
}

#endif
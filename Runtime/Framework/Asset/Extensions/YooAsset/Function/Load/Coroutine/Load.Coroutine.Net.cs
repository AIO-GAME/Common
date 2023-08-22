/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-08-11
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using System;
using System.Collections;
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
        public static IEnumerator LoadTextureNetCO(string url, Action<Texture2D> cb)
        {
            using (var uwr = UnityWebRequestTexture.GetTexture(url))
            {
                yield return uwr.SendWebRequest();
                if (LoadCheckNet(uwr)) cb.Invoke(DownloadHandlerTexture.GetContent(uwr));
            }
        }

        /// <summary>
        /// 网上加载精灵
        /// </summary>
        /// <param name="url">网址</param>
        /// <param name="rect">矩形</param>
        /// <param name="pivot">中心点</param>
        /// <param name="cb">回调</param>
        public static IEnumerator LoadSpriteNetCO(string url, Rect rect, Vector2 pivot, Action<Sprite> cb)
        {
            using (var uwr = UnityWebRequestTexture.GetTexture(url))
            {
                yield return uwr.SendWebRequest();
                if (LoadCheckNet(uwr))
                {
                    var tempSprite = Sprite.Create(DownloadHandlerTexture.GetContent(uwr), rect, pivot);
                    cb?.Invoke(tempSprite);
                }
            }
        }

        /// <summary>
        /// 网上加载AB
        /// </summary>
        /// <param name="url">网址</param>
        /// <param name="cb">回调</param>
        public static IEnumerator LoadAssetBundleNetCO(string url, Action<AssetBundle> cb)
        {
            using (var uwr = UnityWebRequestAssetBundle.GetAssetBundle(url))
            {
                yield return uwr.SendWebRequest();
                if (LoadCheckNet(uwr)) cb?.Invoke(DownloadHandlerAssetBundle.GetContent(uwr));
            }
        }

        /// <summary>
        /// 网上加载音频
        /// </summary>
        /// <param name="url">网址</param>
        /// <param name="audioType">音频类型</param>
        /// <param name="cb">回调</param>
        public static IEnumerator LoadAudioNetCO(string url, AudioType audioType, Action<AudioClip> cb)
        {
            using (var uwr = UnityWebRequestMultimedia.GetAudioClip(url, audioType))
            {
                yield return uwr.SendWebRequest();
                if (LoadCheckNet(uwr)) cb?.Invoke(DownloadHandlerAudioClip.GetContent(uwr));
            }
        }

        /// <summary>
        /// 网上加载音频
        /// </summary>
        /// <param name="url">网址</param>
        /// <param name="cb">回调</param>
        public static IEnumerator LoadStringNetCO(string url, Action<string> cb)
        {
            using (var uwr = UnityWebRequest.Get(url))
            {
                yield return uwr.SendWebRequest();
                if (LoadCheckNet(uwr)) cb?.Invoke(uwr.downloadHandler.text);
            }
        }

        /// <summary>
        /// 网上加载流数据
        /// </summary>
        /// <param name="url">网址</param>
        /// <param name="cb">回调</param>
        public static IEnumerator LoadSteamNetCO(string url, Action<byte[]> cb)
        {
            using (var uwr = UnityWebRequest.Get(url))
            {
                yield return uwr.SendWebRequest();
                if (LoadCheckNet(uwr)) cb?.Invoke(uwr.downloadHandler.data);
            }
        }
    }
}
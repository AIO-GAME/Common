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
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace AIO
{
    public partial class AssetSystem
    {
        private static bool LoadCheckNet(UnityWebRequest operation)
        {
            switch (operation.result)
            {
                case UnityWebRequest.Result.InProgress:
                    Debug.LogError("请求正在进行中");
                    break;
                case UnityWebRequest.Result.ConnectionError:
                    Debug.LogError("无法连接到服务器");
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("服务器返回响应错误");
                    break;
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("数据处理异常");
                    break;
                default:
                    Debug.LogError("未知错误");
                    break;
                case UnityWebRequest.Result.Success:
                    break;
            }
#if UNITY_2020_3_OR_NEWER
            if (operation.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(operation.error);
                return false;
            }
#else
        if (operation.isHttpError || operation.isNetworkError)
        {
            UnityEngine.Debug.LogError(operation.error);
            return false;
        }
#endif

            if (!operation.isDone)
            {
                Debug.LogError("请求未完成");
                return false;
            }

            return true;
        }

        #region Async

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

        #endregion

        #region CO

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

        #endregion

        #region UniTask

        /// <summary>
        /// 网上加载图片
        /// </summary>
        /// <param name="url">网址</param>
        internal static async UniTask<Texture2D> LoadNetTextureTask(string url)
        {
            using (var uwr = UnityWebRequestTexture.GetTexture(url))
            {
                await uwr.SendWebRequest();
                if (LoadCheckNet(uwr))
                    return DownloadHandlerTexture.GetContent(uwr);
                return null;
            }
        }

        /// <summary>
        /// 网上加载图片
        /// </summary>
        /// <param name="url">网址</param>
        /// <param name="rect">矩形</param>
        /// <param name="pivot">中心点</param>
        public static async UniTask<Sprite> LoadNetSpriteTask(string url, Rect rect, Vector2 pivot)
        {
            using (var uwr = UnityWebRequestTexture.GetTexture(url))
            {
                await uwr.SendWebRequest();
                if (LoadCheckNet(uwr))
                    return Sprite.Create(DownloadHandlerTexture.GetContent(uwr), rect, pivot);
                return null;
            }
        }

        /// <summary>
        /// 网上加载文本
        /// </summary>
        /// <param name="url">网址</param>
        public static async UniTask<string> LoadStringNetTask(string url)
        {
            using (var uwr = UnityWebRequest.Get(url))
            {
                await uwr.SendWebRequest();
                if (LoadCheckNet(uwr)) return uwr.downloadHandler.text;
                return null;
            }
        }

        /// <summary>
        /// 网上加载字节
        /// </summary>
        /// <param name="url">网址</param>
        public static async UniTask<byte[]> LoadSteamNetTask(string url)
        {
            using (var uwr = UnityWebRequest.Get(url))
            {
                await uwr.SendWebRequest();
                if (LoadCheckNet(uwr)) return uwr.downloadHandler.data;
                return null;
            }
        }

        /// <summary>
        /// 网上加载音频
        /// </summary>
        /// <param name="url">网址</param>
        /// <param name="audioType">音频类型</param>
        public static async UniTask<AudioClip> LoadAudioClipNetTask(string url, AudioType audioType)
        {
            using (var uwr = UnityWebRequestMultimedia.GetAudioClip(url, audioType))
            {
                await uwr.SendWebRequest();
                if (LoadCheckNet(uwr))
                    return DownloadHandlerAudioClip.GetContent(uwr);
                return null;
            }
        }

        /// <summary>
        /// 网上加载AB包
        /// </summary>
        /// <param name="url">网址</param>
        public static async UniTask<AssetBundle> LoadAssetBundleNetTask(string url)
        {
            using (var uwr = UnityWebRequestAssetBundle.GetAssetBundle(url))
            {
                await uwr.SendWebRequest();
                if (LoadCheckNet(uwr)) return DownloadHandlerAssetBundle.GetContent(uwr);
                return null;
            }
        }

        #endregion
    }
}

#endif
/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-08-11
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

#if SUPPORT_UNITASK

using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
namespace AIO.UEngine
{
public partial class YAssetSystem
{
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
}
}
#endif
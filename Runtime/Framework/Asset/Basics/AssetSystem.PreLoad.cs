/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-08-22
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using UnityEngine;

namespace AIO
{
    public partial class AssetSystem
    {
        /// <summary>
        /// 预加载资源
        /// </summary>
        /// <param name="location">资源的定位地址</param>
        /// <typeparam name="TObject">资源类型</typeparam>
        public static void PreLoadSubAssets<TObject>(string location) where TObject : Object
        {
            Proxy.LoadSubAssetsTask<TObject>(location);
        }

        /// <summary>
        /// 预加载资源
        /// </summary>
        /// <param name="location">资源的定位地址</param>
        public static void PreLoadSubAssets(string location)
        {
            Proxy.LoadSubAssetsTask(location, typeof(Object));
        }

        /// <summary>
        /// 预加载资源
        /// </summary>
        /// <param name="location">资源的定位地址</param>
        /// <typeparam name="TObject">资源类型</typeparam>
        public static void PreLoadAsset<TObject>(string location) where TObject : Object
        {
            Proxy.LoadAssetTask<TObject>(location);
        }

        /// <summary>
        /// 预加载资源
        /// </summary>
        /// <param name="location">资源的定位地址</param>
        public static void PreLoadAsset(string location)
        {
            Proxy.LoadAssetTask(location, typeof(Object));
        }

        /// <summary>
        /// 预加载资源
        /// </summary>
        /// <param name="location">资源的定位地址</param>
        public static void PreLoadRaw(string location)
        {
            Proxy.LoadRawFileDataTask(location);
        }
    }
}
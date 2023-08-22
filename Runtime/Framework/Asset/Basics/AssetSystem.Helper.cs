/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-08-22
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AIO
{
    public static partial class AssetSystem
    {
        /// <summary>
        /// 释放资源句柄
        /// </summary>
        /// <param name="location">资源地址</param>
        public static void FreeHandle(string location)
        {
            Proxy.FreeHandle(location);
        }

        public static void FreeHandle(IEnumerable<string> locations)
        {
            foreach (var location in locations) Proxy.FreeHandle(location);
        }


        /// <summary>
        /// 平台名称 字符串
        /// </summary>
        /// <returns></returns>
        public static string PlatformNameStr
        {
            get
            {
#if UNITY_EDITOR
                return EditorUserBuildSettings.activeBuildTarget.ToString();
#else
                switch (Application.platform)
                {
                    case RuntimePlatform.WindowsPlayer:
                    case RuntimePlatform.WindowsEditor:
                        return "StandaloneWindows";
                    case RuntimePlatform.OSXPlayer:
                    case RuntimePlatform.OSXEditor:
                        return "StandaloneOSX";
                    case RuntimePlatform.IPhonePlayer:
                        return "iOS";
                    case RuntimePlatform.Android:
                        return "Android";
                    case RuntimePlatform.WebGLPlayer:
                        return "WebGL";
                    default: return Application.platform.ToString();
                }
#endif
            }
        }

        /// <summary>
        /// 平台
        /// </summary>
        public static RuntimePlatform Platform
        {
            get { return Application.platform; }
        }
    }
}
﻿using System.IO;
using UnityEngine;

namespace UnityEditor
{
    public static partial class EditorMenu
    {
        public class AutoIconEditor : Editor
        {
            //必须和存放Icon的文件夹路径一直，否则获得的Texture2D将会为空
            private static readonly string Icon_Path = @"Assets\Arts\Icons\{0}\{1}.png";

            [MenuItem("Tools/Setting/Icon/App/Active Build Target")]
            public static void SetAppIconsActiveBuildTarget()
            {
                SetIcons("APP", EditorUserBuildSettings.selectedBuildTargetGroup);
            }

            [MenuItem("Tools/Setting/Icon/App/Android")]
            public static void SetAppIconsAndroid()
            {
#if UNITY_ANDROID
            SetIcons("APP", BuildTargetGroup.Android);
#endif
            }

            [MenuItem("Tools/Setting/Icon/App/WebGL")]
            public static void SetAppIconsWebGL()
            {
                SetIcons("APP", BuildTargetGroup.WebGL);
            }

            [MenuItem("Tools/Setting/Icon/App/IOS")]
            public static void SetAppIconsiOS()
            {
                SetIcons("APP", BuildTargetGroup.iOS);
            }

            private static void SetIcons(string iconPrefixName, BuildTargetGroup targetGroup)
            {
                var iconSizes = PlayerSettings.GetIconSizesForTargetGroup(targetGroup);
                var texArray = new Texture2D[iconSizes.Length];
                for (var i = 0; i < iconSizes.Length; ++i)
                {
                    var path = Path.Combine(Icon_Path, iconPrefixName, iconSizes[i].ToString());
                    texArray[i] = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
                }

#if UNITY_ANDROID
            //此处为新API
            //kind有3种，分别对应PlayerSettings的Legacy，Round，Adaptive
            if (targetGroup == BuildTargetGroup.Android)
            {
                var kind = UnityEditor.Android.AndroidPlatformIconKind.Round;
                var icons = PlayerSettings.GetPlatformIcons(targetGroup, kind);
                //将转换后获得的Texture2D数组，逐个赋值给icons
                for (int i = 0, length = icons.Length; i < length; ++i) icons[i].SetTexture(texArray[i]);
                PlayerSettings.SetPlatformIcons(targetGroup, kind, icons);
            }
#endif
                AssetDatabase.SaveAssets();
                Debug.LogFormat("Set {0} Icon Complete", iconPrefixName);
            }
        }
    }
}
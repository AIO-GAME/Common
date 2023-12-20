using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    public static partial class MenuItem_Tools
    {
        //必须和存放Icon的文件夹路径一直，否则获得的Texture2D将会为空
        private const string Icon_Path = @"Assets\Arts\Icons\{0}\{1}.png";

        [MenuItem("AIO/Tools/Setting/Icon/App/Active Build Target")]
        public static void SetAppIconsActiveBuildTarget()
        {
            SetIcons("APP", EditorUserBuildSettings.selectedBuildTargetGroup);
        }

        [MenuItem("AIO/Tools/Setting/Icon/App/Android")]
        public static void SetAppIconsAndroid()
        {
#if UNITY_ANDROID
            SetIcons("APP", BuildTargetGroup.Android);
#endif
        }

        [MenuItem("AIO/Tools/Setting/Icon/App/WebGL")]
        public static void SetAppIconsWebGL()
        {
            SetIcons("APP", BuildTargetGroup.WebGL);
        }

        [MenuItem("AIO/Tools/Setting/Icon/App/IOS")]
        public static void SetAppIconiOS()
        {
            SetIcons("APP", BuildTargetGroup.iOS);
        }

        private static void SetIcons(string iconPrefixName, BuildTargetGroup targetGroup)
        {
#if UNITY_2023_1_OR_NEWER
            var nametarget = UnityEditor.Build.NamedBuildTarget.FromBuildTargetGroup(targetGroup);
            var iconSizes = PlayerSettings.GetIconSizes(nametarget, IconKind.Any);
#else
            var iconSizes = PlayerSettings.GetIconSizesForTargetGroup(targetGroup);
#endif
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
#if UNITY_2023_1_OR_NEWER
                var icons =
 PlayerSettings.GetPlatformIcons(UnityEditor.Build.NamedBuildTarget.FromBuildTargetGroup(targetGroup), kind); 
                //将转换后获得的Texture2D数组，逐个赋值给icons
                for (int i = 0, length = icons.Length; i < length; ++i) icons[i].SetTexture(texArray[i]);
                PlayerSettings.SetPlatformIcons(UnityEditor.Build.NamedBuildTarget.FromBuildTargetGroup(targetGroup), kind, icons);
#else
                var icons = PlayerSettings.GetPlatformIcons(targetGroup, kind);
                //将转换后获得的Texture2D数组，逐个赋值给icons
                for (int i = 0, length = icons.Length; i < length; ++i) icons[i].SetTexture(texArray[i]);
                PlayerSettings.SetPlatformIcons(targetGroup, kind, icons);
#endif
            }
#endif
            AssetDatabase.SaveAssets();
            Debug.LogFormat("Set {0} Icon Complete", iconPrefixName);
        }


        [MenuItem("AIO/Tools/Clean AssetBundle Name")]
        public static void Text()
        {
            var dirTempInfo = new DirectoryInfo(Application.dataPath);
            var directoryDIRArray = dirTempInfo.GetDirectories();

            // 遍历本场景目录下所有的目录或者文件
            foreach (var currentDir in directoryDIRArray)
            {
                // 递归调用方法，找到文件，则使用 AssetImporter 类，标记“包名”与 “后缀名”
                JudgeDirOrFileByRecursive(currentDir);
            }

            AssetDatabase.RemoveUnusedAssetBundleNames();
            //强制删除所有AssetBundle名称  
            foreach (var abName in AssetDatabase.GetAllAssetBundleNames())
            {
                Console.WriteLine($"ab : {abName}");
                AssetDatabase.RemoveAssetBundleName(abName, true);
            }

            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 递归判断判断是否是目录或文件
        /// 是文件，修改 Asset Bundle 标记
        /// 是目录，则继续递归
        /// </summary>
        /// <param name="fileSystemInfo">当前文件信息（文件信息与目录信息可以相互转换）</param>
        private static void JudgeDirOrFileByRecursive(FileSystemInfo fileSystemInfo)
        {
            // 调试信息
            //Debug.Log("currentDir.Name = " + fileSystemInfo.Name);
            //Debug.Log("sceneName = " + sceneName);

            // 参数检查
            if (fileSystemInfo.Exists == false)
            {
                Debug.LogError("文件或者目录名称：" + fileSystemInfo + " 不存在，请检查");
                return;
            }

            // 得到当前目录下一级的文件信息集合
            var directoryInfoObj = fileSystemInfo as DirectoryInfo; // 文件信息转为目录信息
            var fileSystemInfoArray = directoryInfoObj?.GetFileSystemInfos();
            if (fileSystemInfoArray is null) return;
            foreach (var fileInfo in fileSystemInfoArray)
            {
                if (fileInfo is FileInfo fileInfoObj) // 文件类型
                {
                    RemoveFileABLabel(fileInfoObj); // 修改此文件的 AssetBundle 标签
                }
                else // 目录类型
                {
                    JudgeDirOrFileByRecursive(fileInfo); // 如果是目录，则递归调用
                }
            }
        }

        /// <summary>
        /// 给文件移除 Asset Bundle 标记
        /// </summary>
        /// <param name="fileInfoObj">文件（文件信息）</param>
        private static void RemoveFileABLabel(FileInfo fileInfoObj)
        {
            // 调试信息
            //Debug.Log("fileInfoObj.Name = " + fileInfoObj.Name);
            //Debug.Log("scenesName = " + scenesName);

            // 参数检查（*.meta 文件不做处理）
            if (fileInfoObj.Extension == ".meta") return;
            if (fileInfoObj.Extension == ".cs") return;

            // 得到 AB 包名称
            var strABName = string.Empty;
            // 获取资源文件的相对路径
            var tmpIndex = fileInfoObj.FullName.IndexOf("Assets");
            var strAssetFilePath = fileInfoObj.FullName.Substring(tmpIndex); // 得到文件相对路径

            // 给资源文件移除 AB 名称
            var tmpImportObj = AssetImporter.GetAtPath(strAssetFilePath);
            if (tmpImportObj != null)
            {
                tmpImportObj.assetBundleName = null;
            }
        }
    }
}
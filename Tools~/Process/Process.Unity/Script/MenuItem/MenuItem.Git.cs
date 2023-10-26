/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-07-24
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace AIO.UEditor
{
    /// <summary>
    /// PackageGen
    /// </summary>
    internal static partial class PackageGen
    {
        private const string CMD_GIT = nameof(PrPlatform.Git);

        /// <summary>
        /// 生成
        /// </summary>
        [InitializeOnLoadMethod]
        [MenuItem("Git/~~~ Generate ~~~", false, 9999)]
        internal static void Generate()
        {
            var dataPath = Application.dataPath.Replace("Assets", "");

            var packageInfos = AssetDatabase.FindAssets("package", new string[] { "Packages" })
                .Select(AssetDatabase.GUIDToAssetPath)
                .Where(x => AssetDatabase.LoadAssetAtPath<TextAsset>(x) != null)
                .Select(PackageInfo.FindForAssetPath)
                .GroupBy(x => x.assetPath)
                .Select(x => x.First())
                .Where(x => File.Exists(Path.Combine(dataPath, x.resolvedPath, ".git")) ||
                            Directory.Exists(Path.Combine(dataPath, x.resolvedPath, ".git")))
                .ToList();
            CreateTemplate(packageInfos);
        }

        /// <summary>
        /// 生成
        /// </summary>
        [MenuItem("Git/~~~ Clean ~~~", false, 9999)]
        internal static void Clean()
        {
            var OutPath = GetOutPath();
            if (!AssetDatabase.DeleteAsset(OutPath))
            {
                if (!Directory.Exists(OutPath)) return;
                Directory.Delete(OutPath, true);
            }

            AssetDatabase.Refresh();

            var RefreshSettings = typeof(AssetDatabase).GetMethod("RefreshSettings",
                BindingFlags.Static | BindingFlags.Public);
            if (RefreshSettings != null) RefreshSettings.Invoke(null, null);

            CompilationPipeline.RequestScriptCompilation();
        }

        private static string GetOutPath()
        {
            return Path.Combine(Application.dataPath, "Editor", "Gen", "Git");
        }

        public static void CreateTemplate(IEnumerable<PackageInfo> infos)
        {
            var ProjectPath = new DirectoryInfo(Application.dataPath).Parent?.FullName
                .Replace('\\', '/')
                .Trim('\\', '/');
            if (string.IsNullOrEmpty(ProjectPath))
            {
                Debug.LogError("Application.dataPath is null");
                return;
            }

            var OutPath = GetOutPath();

            const string fullPathInfo = @"
        internal static string FullPath
        {
            get
            {
                var p = new DirectoryInfo(Application.dataPath).Parent;
                if (p is null) return URL;
                return Path.Combine(p.FullName, URL);
        }
    }
    ";

            const string tipInfo = @"/*|============================================|*|
|*|Author:        |*|Automatic Generation      |*|
|*|Date:          |*|{0}                |*|
|*|=============================================*/";

            const string usingInfo = @"
    using System;
    using UnityEditor;
    using UnityEngine;
    using System.IO;
";
            const string URL = "FullPath";

            var savaDir = new Dictionary<string, string>();
            var str = new StringBuilder();
            int index;

            foreach (var info in infos)
            {
                var classname = string.Concat("Git_", info.name.Replace('-', '_').Replace('.', '_')).ToUpper();

                index = 100;
                str.Clear();
                str.AppendFormat(tipInfo, DateTime.Now.ToString("yyyy-MM-dd")).Append("\r\n\r\n");
                str.AppendFormat("namespace {0}\r\n{1}", typeof(PackageGen).Namespace, "{");
                str.AppendFormat("{0}\r\n", usingInfo);

                str.AppendFormat("    /// <summary>\r\n    /// Git Manager {0}\r\n    /// </summary>\r\n",
                    info.displayName);
                str.AppendFormat("    [InitializeOnLoad]\r\n");
                str.AppendFormat("    internal static partial class {0}\r\n", classname).Append("    {\r\n");
                str.AppendFormat("        internal const string URL = \"{0}\";\r\n",
                    info.resolvedPath.Replace('\\', '/').Replace(ProjectPath, "").Trim('\\', '/'));
                str.AppendFormat("        internal const string DisplayName = \"{0}\";\r\n", info.displayName);
                str.AppendFormat("        internal const string PackageName = \"{0}\";\r\n", info.name);
                str.AppendFormat("{0}\r\n", fullPathInfo);

                {
                    str.AppendLine();
                    str.AppendFormat("        static {0}()\r\n", classname).Append("        {\r\n");
                    str.Append("            Refresh();\r\n");
                    str.Append("        }\r\n");
                }

                {
                    str.AppendLine();
                    str.AppendFormat("        [MenuItem(\"Git/\" + DisplayName + \"/打开 Open\", false, 0)]\r\n");
                    str.AppendFormat("        internal static async void Open()\r\n").Append("        {\r\n");
                    str.AppendFormat(
                        "            Selection.activeObject = AssetDatabase.LoadAssetAtPath<TextAsset>(Path.Combine(URL, \"package.json\"));\r\n");
                    str.AppendFormat(
                            "            await PrPlatform.Open.Path(FullPath);\r\n")
                        .Append("        }\r\n");
                }

                {
                    str.AppendLine();
                    str.AppendFormat("        private static bool HasUpdate = false;\r\n\r\n");
                    str.AppendFormat("        [MenuItem(\"Git/\" + DisplayName + \"/刷新 Refresh\", false, 1)]\r\n");
                    str.AppendFormat("        internal static async void Refresh()\r\n").Append("        {\r\n");
                    str.AppendFormat(
                        "            var ret = await PrGit.Helper.GetBehind(FullPath);\r\n");
                    str.AppendFormat("            HasUpdate = ret > 0;\r\n");
                    str.AppendFormat("            if (ret < 0)\r\n").Append("            {\r\n");
                    str.Append(
                        "                Debug.LogWarning($\"Refresh : 本地Git库版本 提交数超过 远程库版本 : {Math.Abs(ret)}\");\r\n");
                    str.AppendFormat("                return;\r\n").Append("            }\r\n        }\r\n");
                }

                {
                    str.AppendLine();
                    str.AppendFormat("        [MenuItem(\"Git/\" + DisplayName + \"/拉取 Pull\", true)]\r\n");
                    str.AppendFormat("        private static bool GetHasUpdate()\r\n").Append("        {\r\n");
                    str.AppendFormat("            return HasUpdate;\r\n").Append("        }\r\n");
                }

                {
                    str.AppendLine();
                    str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false, {2})]\r\n",
                        nameof(PrPlatform.Git), "添加 Add", ++index);
                    str.AppendFormat("        internal static async void {0}()\r\n{1}",
                        nameof(PrPlatform.Git.Add), "        {\r\n");
                    str.AppendFormat(
                        "            await PrPlatform.Git.{0}(\r\n                {1}, \".\", false\r\n            ).Async();\r\n{2}",
                        nameof(PrPlatform.Git.Add), URL, "        }\r\n");
                }

                {
                    str.AppendLine();
                    str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false, {2})]\r\n",
                        nameof(PrPlatform.Git), "拉取 Pull", ++index);
                    str.AppendFormat("        internal static async void {0}()\r\n{1}",
                        nameof(PrPlatform.Git.Pull), "        {\r\n");
                    str.AppendFormat(
                        "            await PrPlatform.Git.{0}(\r\n                {1}, false\r\n            ).Async();\r\n{2}",
                        nameof(PrPlatform.Git.Pull), URL, "        }\r\n");
                }

                {
                    str.AppendLine();
                    str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false, {2})]\r\n",
                        nameof(PrPlatform.Git), "推送 Push", ++index);
                    str.AppendFormat("        internal static async void {0}()\r\n{1}",
                        nameof(PrPlatform.Git.Push), "        {\r\n");
                    str.AppendFormat(
                        "            await PrPlatform.Git.{0}(\r\n                {1}, false\r\n            ).Async();\r\n{2}",
                        nameof(PrPlatform.Git.Push), $"({URL}, null)", "        }\r\n");
                }

                {
                    str.AppendLine();
                    str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false, {2})]\r\n",
                        nameof(PrPlatform.Git), "提交 Commit", ++index);
                    str.AppendFormat("        internal static async void {0}()\r\n{1}",
                        nameof(PrPlatform.Git.Commit), "        {\r\n");
                    str.AppendFormat(
                        "            await PrPlatform.Git.{0}(\r\n                {1}, false\r\n            ).Async();\r\n{2}",
                        nameof(PrPlatform.Git.Commit), $"({URL},null)", "        }\r\n");
                }

                {
                    str.AppendLine();
                    str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false, {2})]\r\n",
                        nameof(PrPlatform.Git), "上传 Upload", ++index);
                    str.AppendFormat("        internal static async void {0}()\r\n{1}",
                        nameof(PrPlatform.Git.Upload), "        {\r\n");
                    str.AppendFormat(
                        "            await PrPlatform.Git.{0}(\r\n                {1}, false, false, false\r\n            ).Async();\r\n{2}",
                        nameof(PrPlatform.Git.Upload), URL, "        }\r\n");
                }

                {
                    str.AppendLine();
                    str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false, {2})]\r\n",
                        nameof(PrPlatform.Git), "清理 Clean", ++index);
                    str.AppendFormat("        internal static async void {0}()\r\n{1}",
                        nameof(PrPlatform.Git.Clean), "        {\r\n");
                    str.AppendFormat("            await PrPlatform.Git.{0}(\r\n                {1}, \"-fd -x" +
                                     "\", false\r\n            ).Async();\r\n{2}",
                        nameof(PrPlatform.Git.Clean), URL, "        }\r\n");
                }

                {
                    str.AppendLine();
                    str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false)]\r\n",
                        nameof(PrPlatform.Git), "重置 Reset/--Hard 重置 [分支 暂存区 工作区]");
                    str.AppendFormat("        internal static async void {0}()\r\n{1}",
                        nameof(PrPlatform.Git.ResetHard), "        {\r\n");
                    str.AppendFormat(
                        "            await PrPlatform.Git.{0}(\r\n                {1}, false\r\n            ).Async();\r\n{2}",
                        nameof(PrPlatform.Git.ResetHard), URL, "        }\r\n");
                }

                {
                    str.AppendLine();
                    str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false)]\r\n",
                        nameof(PrPlatform.Git), "重置 Reset/--Keep 重置 [索引] 如果提交和HEAD之间的文件与HEAD不同，则重置将中止");
                    str.AppendFormat("        internal static async void {0}()\r\n{1}",
                        nameof(PrPlatform.Git.ResetKeep), "        {\r\n");
                    str.AppendFormat(
                        "            await PrPlatform.Git.{0}(\r\n                {1}, false\r\n            ).Async();\r\n{2}",
                        nameof(PrPlatform.Git.ResetKeep), URL, "        }\r\n");
                }

                {
                    str.AppendLine();
                    str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false)]\r\n",
                        nameof(PrPlatform.Git), "重置 Reset/--Merge 重置 [索引 暂存区] 更改和索引产生 重置将被终止");
                    str.AppendFormat("        internal static async void {0}()\r\n{1}",
                        nameof(PrPlatform.Git.ResetMerge), "        {\r\n");
                    str.AppendFormat(
                        "            await PrPlatform.Git.{0}(\r\n                {1}, false\r\n            ).Async();\r\n{2}",
                        nameof(PrPlatform.Git.ResetMerge), URL, "        }\r\n");
                }

                {
                    str.AppendLine();
                    str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false)]\r\n",
                        nameof(PrPlatform.Git), "重置 Reset/--Mixed 重置 [分支 暂存区]");
                    str.AppendFormat("        internal static async void {0}()\r\n{1}",
                        nameof(PrPlatform.Git.ResetMixed), "        {\r\n");
                    str.AppendFormat(
                        "            await PrPlatform.Git.{0}(\r\n                {1}, false\r\n            ).Async();\r\n{2}",
                        nameof(PrPlatform.Git.ResetMixed), URL, "        }\r\n");
                }

                {
                    str.AppendLine();
                    str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false)]\r\n",
                        nameof(PrPlatform.Git), "重置 Reset/--Soft 重置 [分支]");
                    str.AppendFormat("        internal static async void {0}()\r\n{1}",
                        nameof(PrPlatform.Git.ResetSoft), "        {\r\n");
                    str.AppendFormat(
                        "            await PrPlatform.Git.{0}(\r\n                {1}, false\r\n            ).Async();\r\n{2}",
                        nameof(PrPlatform.Git.ResetSoft), URL, "        }\r\n");
                }

                {
                    str.AppendLine();
                    str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false, {2})]\r\n",
                        nameof(PrPlatform.Git), "设置关联远端库 RemoteSetUrl", ++index);
                    str.AppendFormat("        internal static async void {0}()\r\n{1}",
                        nameof(PrPlatform.Git.RemoteSetUrl), "        {\r\n");
                    str.AppendFormat(
                        "            await PrPlatform.Git.{0}(\r\n                {1}, false\r\n            ).Async();\r\n{2}",
                        nameof(PrPlatform.Git.RemoteSetUrl), URL, "        }\r\n");
                }

                str.Append("    }\r\n}");
                savaDir.Add(string.Concat(info.name, ".cs"), str.ToString());
            }

            var change = false;
            var bakDir = new DirectoryInfo(OutPath);
            if (bakDir.Exists)
            {
                foreach (var file in bakDir.GetFiles("*.cs", SearchOption.TopDirectoryOnly))
                {
                    if (file.Name.Contains(".meta")) continue;
                    if (savaDir.ContainsKey(file.Name))
                    {
                        // 判断文件是否有变化
                        // 如果有变化则删除原来的文件
                        // 如果没有变化则不删除原来的文件
                        var old = File.ReadAllText(file.FullName, Encoding.UTF8);
                        var now = savaDir[file.Name];
                        if (old == now)
                        {
                            savaDir.Remove(file.Name);
                            continue;
                        }
                    }

                    change = true;
                }
            }
            else
            {
                change = true;
                bakDir.Create();
            }

            if (savaDir.Count > 0)
            {
                change = true;
                foreach (var item in savaDir)
                {
                    File.WriteAllText(Path.Combine(OutPath, item.Key), item.Value, Encoding.UTF8);
                }
            }

            if (change)
            {
                AssetDatabase.Refresh();

                var RefreshSettings = typeof(AssetDatabase).GetMethod("RefreshSettings",
                    BindingFlags.Static | BindingFlags.Public);
                if (RefreshSettings != null) RefreshSettings.Invoke(null, null);

                CompilationPipeline.RequestScriptCompilation();
            }
        }
    }
}
/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-07-24
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace AIO.Unity.Editor
{
    /// <summary>
    /// PackageGen
    /// </summary>
    [InitializeOnLoad]
    internal static partial class PackageGen
    {
        private const string CMD_GIT = nameof(PrPlatform.Git);

        static PackageGen()
        {
            Generate();
        }

        /// <summary>
        /// 生成
        /// </summary>
        [MenuItem("Git/~~~Generate~~~", false, 999)]
        internal static void Generate()
        {
            var dataPath = Application.dataPath.Replace("Assets", "");

            var packageInfos = AssetDatabase.FindAssets("package", new string[] { "Packages" })
                .Select(AssetDatabase.GUIDToAssetPath)
                .Where(x => AssetDatabase.LoadAssetAtPath<TextAsset>(x) != null)
                .Select(PackageInfo.FindForAssetPath)
                .GroupBy(x => x.assetPath)
                .Select(x => x.First())
                .Where(x => Directory.Exists(Path.Combine(dataPath, x.resolvedPath, ".git")))
                .ToList();
            CreateTemplate(packageInfos);
        }

        public static void CreateTemplate(IEnumerable<PackageInfo> infos)
        {
            var ProjectPath = Application.dataPath.Replace("Assets", "");
            var OutPath = Path.Combine(
                PackageInfo.FindForAssembly(typeof(PackageGen).Assembly).resolvedPath,
                "Editor/GitModule/AutomaticGeneration"
            );

            const string tips = @"/*|============================================|*|
|*|Author:        |*|Automatic Generation      |*|
|*|Date:          |*|{0}                |*|
|*|=============================================*/";

            const string usings = @"
    using UnityEditor;
    using UnityEngine;
";
            const string URL = "Application.dataPath.Replace(\"Assets\", URL)";

            var savaDir = new Dictionary<string, string>();
            var str = new StringBuilder();
            int index;

            foreach (var info in infos)
            {
                var classname = string.Concat("Git_", info.name.Replace('-', '_').Replace('.', '_')).ToUpper();

                index = 100;
                str.Clear();
                str.AppendFormat(tips, DateTime.Now.ToString("yyyy-MM-dd")).Append("\r\n\r\n");
                str.AppendFormat("namespace {0}\r\n{1}", "AIO.Unity.Editor", "{");
                str.AppendFormat("{0}\r\n", usings);

                str.AppendFormat("    /// <summary>\r\n    /// Git Manager {0}\r\n    /// </summary>\r\n", info.displayName);
                str.AppendFormat("    internal static partial class {0}\r\n", classname).Append("    {\r\n");

                str.AppendFormat("        internal const string URL = \"{0}\";\r\n", info.resolvedPath.Replace('\\', '/').Replace(ProjectPath, ""));
                str.AppendFormat("        internal const string DisplayName = \"{0}\";\r\n", info.displayName);
                str.AppendFormat("        internal const string PackageName = \"{0}\";\r\n", info.name);

                {
                    str.AppendLine();
                    str.AppendFormat("        static {0}()\r\n", classname).Append("        {\r\n");
                    str.Append("        }\r\n");
                }
                
                {
                    str.AppendLine();
                    str.AppendFormat("        [MenuItem(\"Git/\" + DisplayName + \"/打开 Open\", false, 0)]\r\n");
                    str.AppendFormat("        internal static async void Open()\r\n").Append("        {\r\n");
                    str.AppendFormat("            await PrPlatform.Open.Path(Application.dataPath.Replace(\"Assets\", URL));\r\n").Append("        }\r\n");
                }

                {
                    str.AppendLine();
                    str.AppendFormat("        private static bool HasUpdate = false;\r\n\r\n");
                    str.AppendFormat("        [MenuItem(\"Git/\" + DisplayName + \"/刷新 Refresh\", false, 1)]\r\n");
                    str.AppendFormat("        internal static async void Refresh()\r\n").Append("        {\r\n");
                    str.AppendFormat("            var ret = await PrGit.Helper.GetBehind(Application.dataPath.Replace(\"Assets\", URL));\r\n");
                    str.AppendFormat("            HasUpdate = ret > 0;\r\n");
                    str.AppendFormat("            if (ret < 0)").Append("            {\r\n");
                    str.AppendFormat("                Debug.LogError(\"Refresh Error: \" + ret);\r\n");
                    str.AppendFormat("                return;\r\n").Append("            }\r\n        }\r\n");
                }

                {
                    str.AppendLine();
                    str.AppendFormat("        [MenuItem(\"Git/\" + DisplayName + \"/拉取 Pull\", true)]");
                    str.AppendFormat("        private static bool GetHasUpdate()\r\n").Append("        {\r\n");
                    str.AppendFormat("            return HasUpdate;\r\n").Append("        }\r\n");
                }

                {
                    str.AppendLine();
                    str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false, {2})]\r\n",
                        nameof(PrPlatform.Git), "添加 Add", ++index);
                    str.AppendFormat("        internal static async void {0}()\r\n{1}",
                        nameof(PrPlatform.Git.Add), "        {\r\n");
                    str.AppendFormat("            await PrPlatform.Git.{0}(\r\n                {1}, \".\", false\r\n            ).Async();\r\n{2}",
                        nameof(PrPlatform.Git.Add), URL, "        }\r\n");
                }

                {
                    str.AppendLine();
                    str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false, {2})]\r\n",
                        nameof(PrPlatform.Git), "拉取 Pull", ++index);
                    str.AppendFormat("        internal static async void {0}()\r\n{1}",
                        nameof(PrPlatform.Git.Pull), "        {\r\n");
                    str.AppendFormat("            await PrPlatform.Git.{0}(\r\n                {1}, false\r\n            ).Async();\r\n{2}",
                        nameof(PrPlatform.Git.Pull), URL, "        }\r\n");
                }

                {
                    str.AppendLine();
                    str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false, {2})]\r\n",
                        nameof(PrPlatform.Git), "推送 Push", ++index);
                    str.AppendFormat("        internal static async void {0}()\r\n{1}",
                        nameof(PrPlatform.Git.Push), "        {\r\n");
                    str.AppendFormat("            await PrPlatform.Git.{0}(\r\n                {1}, false\r\n            ).Async();\r\n{2}",
                        nameof(PrPlatform.Git.Push), $"({URL}, null)", "        }\r\n");
                }

                {
                    str.AppendLine();
                    str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false, {2})]\r\n",
                        nameof(PrPlatform.Git), "提交 Commit", ++index);
                    str.AppendFormat("        internal static async void {0}()\r\n{1}",
                        nameof(PrPlatform.Git.Commit), "        {\r\n");
                    str.AppendFormat("            await PrPlatform.Git.{0}(\r\n                {1}, false\r\n            ).Async();\r\n{2}",
                        nameof(PrPlatform.Git.Commit), $"({URL},null)", "        }\r\n");
                }

                {
                    str.AppendLine();
                    str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false, {2})]\r\n",
                        nameof(PrPlatform.Git), "上传 Upload", ++index);
                    str.AppendFormat("        internal static async void {0}()\r\n{1}",
                        nameof(PrPlatform.Git.Upload), "        {\r\n");
                    str.AppendFormat("            await PrPlatform.Git.{0}(\r\n                {1}, false, false, false\r\n            ).Async();\r\n{2}",
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
                    str.AppendFormat("            await PrPlatform.Git.{0}(\r\n                {1}, false\r\n            ).Async();\r\n{2}",
                        nameof(PrPlatform.Git.ResetHard), URL, "        }\r\n");
                }

                {
                    str.AppendLine();
                    str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false)]\r\n",
                        nameof(PrPlatform.Git), "重置 Reset/--Keep 重置 [索引] 如果提交和HEAD之间的文件与HEAD不同，则重置将中止");
                    str.AppendFormat("        internal static async void {0}()\r\n{1}",
                        nameof(PrPlatform.Git.ResetKeep), "        {\r\n");
                    str.AppendFormat("            await PrPlatform.Git.{0}(\r\n                {1}, false\r\n            ).Async();\r\n{2}",
                        nameof(PrPlatform.Git.ResetKeep), URL, "        }\r\n");
                }

                {
                    str.AppendLine();
                    str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false)]\r\n",
                        nameof(PrPlatform.Git), "重置 Reset/--Merge 重置 [索引 暂存区] 更改和索引产生 重置将被终止");
                    str.AppendFormat("        internal static async void {0}()\r\n{1}",
                        nameof(PrPlatform.Git.ResetMerge), "        {\r\n");
                    str.AppendFormat("            await PrPlatform.Git.{0}(\r\n                {1}, false\r\n            ).Async();\r\n{2}",
                        nameof(PrPlatform.Git.ResetMerge), URL, "        }\r\n");
                }

                {
                    str.AppendLine();
                    str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false)]\r\n",
                        nameof(PrPlatform.Git), "重置 Reset/--Mixed 重置 [分支 暂存区]");
                    str.AppendFormat("        internal static async void {0}()\r\n{1}",
                        nameof(PrPlatform.Git.ResetMixed), "        {\r\n");
                    str.AppendFormat("            await PrPlatform.Git.{0}(\r\n                {1}, false\r\n            ).Async();\r\n{2}",
                        nameof(PrPlatform.Git.ResetMixed), URL, "        }\r\n");
                }

                {
                    str.AppendLine();
                    str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false)]\r\n",
                        nameof(PrPlatform.Git), "重置 Reset/--Soft 重置 [分支]");
                    str.AppendFormat("        internal static async void {0}()\r\n{1}",
                        nameof(PrPlatform.Git.ResetSoft), "        {\r\n");
                    str.AppendFormat("            await PrPlatform.Git.{0}(\r\n                {1}, false\r\n            ).Async();\r\n{2}",
                        nameof(PrPlatform.Git.ResetSoft), URL, "        }\r\n");
                }

                {
                    str.AppendLine();
                    str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false, {2})]\r\n",
                        nameof(PrPlatform.Git), "设置关联远端库 RemoteSetUrl", ++index);
                    str.AppendFormat("        internal static async void {0}()\r\n{1}",
                        nameof(PrPlatform.Git.RemoteSetUrl), "        {\r\n");
                    str.AppendFormat("            await PrPlatform.Git.{0}(\r\n                {1}, false\r\n            ).Async();\r\n{2}",
                        nameof(PrPlatform.Git.RemoteSetUrl), URL, "        }\r\n");
                }

                str.Append("    }\r\n}");
                savaDir.Add(string.Concat(info.name, ".cs"), str.ToString());
            }

            var change = false;
            var bakdir = new DirectoryInfo(OutPath);
            if (bakdir.Exists)
            {
                foreach (var file in bakdir.GetFiles("*.cs", SearchOption.TopDirectoryOnly))
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
                            savaDir.Remove(file.Name);
                        else
                        {
                            change = true;
                            file.Delete();
                        }
                    }
                    else
                    {
                        change = true;
                        file.Delete();
                    }
                }
            }
            else
            {
                change = true;
                bakdir.Create();
            }

            if (savaDir.Count > 0)
            {
                change = true;
                foreach (var item in savaDir)
                    File.WriteAllText(Path.Combine(OutPath, item.Key), item.Value, Encoding.UTF8);
            }

            if (change)
            {
                AssetDatabase.Refresh();
#if UNITY_2020_1_OR_NEWER
                AssetDatabase.RefreshSettings();
#endif
            }
        }
    }
}
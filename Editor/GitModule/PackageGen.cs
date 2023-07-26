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
    internal static partial class PackageGen
    {
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

        private const string CMD_Git = "Git";
        internal const string CMD_Git_Pull = nameof(PrPlatform.Git.Pull);
        internal const string CMD_Git_Push = nameof(PrPlatform.Git.Push);
        internal const string CMD_Git_Add = nameof(PrPlatform.Git.Add);
        internal const string CMD_Git_Commit = nameof(PrPlatform.Git.Commit);
        internal const string CMD_Git_Upload = nameof(PrPlatform.Git.Upload);
        internal const string CMD_Git_RemoteSetUrl = nameof(PrPlatform.Git.RemoteSetUrl);
        internal const string CMD_Git_Clean = nameof(PrPlatform.Git.Clean);
        internal const string CMD_Git_Clone = nameof(PrPlatform.Git.Clone);
        internal const string CMD_Git_Reset = nameof(PrPlatform.Git.Reset);

        public static void CreateTemplate(IEnumerable<PackageInfo> infos)
        {
            var ProjectPath = Application.dataPath.Replace("Assets", "");
            var OutPath = Path.Combine(ProjectPath, "Packages/com.blz.package/Editor/GitModule/AutomaticGeneration");

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
                index = 1;
                str.Clear();
                str.AppendFormat(tips, DateTime.Now.ToString("yyyy-MM-dd")).Append("\r\n\r\n");
                str.AppendFormat("namespace {0}\r\n{1}", "AIO.Unity.Editor", "{");
                str.AppendFormat("{0}\r\n", usings);

                str.AppendFormat("    /// <summary>\r\n    /// Git Manager {0}\r\n    /// </summary>\r\n", info.displayName);
                str.AppendFormat("    internal static partial class Git_{0}\r\n", info.name.Replace('-', '_').Replace('.', '_').ToUpper()).Append("    {\r\n");

                str.AppendFormat("        internal const string URL = \"{0}\";\r\n", info.resolvedPath.Replace('\\', '/').Replace(ProjectPath, ""));
                str.AppendFormat("        internal const string DisplayName = \"{0}\";\r\n", info.displayName);
                str.AppendFormat("        internal const string PackageName = \"{0}\";\r\n", info.name);

                {
                    str.AppendLine();
                    str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false, {2})]\r\n",
                        CMD_Git, "添加 Add", ++index);
                    str.AppendFormat("        internal static async void {0}()\r\n{1}",
                        nameof(CMD_Git_Add), "        {\r\n");
                    str.AppendFormat("            await PrPlatform.Git.{0}(\r\n                {1}, \".\", false\r\n            ).Async();\r\n{2}",
                        CMD_Git_Add, URL, "        }\r\n");
                }

                {
                    str.AppendLine();
                    str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false, {2})]\r\n",
                        CMD_Git, "拉取 Pull", ++index);
                    str.AppendFormat("        internal static async void {0}()\r\n{1}",
                        nameof(CMD_Git_Pull), "        {\r\n");
                    str.AppendFormat("            await PrPlatform.Git.{0}(\r\n                {1}, false\r\n            ).Async();\r\n{2}",
                        CMD_Git_Pull, URL, "        }\r\n");
                }

                {
                    str.AppendLine();
                    str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false, {2})]\r\n",
                        CMD_Git, "推送 Push", ++index);
                    str.AppendFormat("        internal static async void {0}()\r\n{1}",
                        nameof(CMD_Git_Push), "        {\r\n");
                    str.AppendFormat("            await PrPlatform.Git.{0}(\r\n                {1}, false\r\n            ).Async();\r\n{2}",
                        CMD_Git_Push, $"({URL}, null)", "        }\r\n");
                }

                {
                    str.AppendLine();
                    str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false, {2})]\r\n",
                        CMD_Git, "提交 Commit", ++index);
                    str.AppendFormat("        internal static async void {0}()\r\n{1}",
                        nameof(CMD_Git_Commit), "        {\r\n");
                    str.AppendFormat("            await PrPlatform.Git.{0}(\r\n                {1}, false\r\n            ).Async();\r\n{2}",
                        CMD_Git_Commit, $"({URL},null)", "        }\r\n");
                }

                {
                    str.AppendLine();
                    str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false, {2})]\r\n",
                        CMD_Git, "上传 Upload", ++index);
                    str.AppendFormat("        internal static async void {0}()\r\n{1}",
                        nameof(CMD_Git_Upload), "        {\r\n");
                    str.AppendFormat("            await PrPlatform.Git.{0}(\r\n                {1}, false, false, false\r\n            ).Async();\r\n{2}",
                        CMD_Git_Upload, URL, "        }\r\n");
                }

                {
                    str.AppendLine();
                    str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false, {2})]\r\n",
                        CMD_Git, "清理 Clean", ++index);
                    str.AppendFormat("        internal static async void {0}()\r\n{1}",
                        nameof(CMD_Git_Clean), "        {\r\n");
                    str.AppendFormat("            await PrPlatform.Git.{0}(\r\n                {1}, \"-fd -x" +
                                     "\", false\r\n            ).Async();\r\n{2}",
                        CMD_Git_Clean, URL, "        }\r\n");
                }

                {
                    const string funcname = nameof(PrPlatform.Git.ResetHard);
                    str.AppendLine();
                    str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false)]\r\n",
                        CMD_Git, "重置 Reset/--Hard 重置 [分支 暂存区 工作区]");
                    str.AppendFormat("        internal static async void {0}()\r\n{1}",
                        funcname, "        {\r\n");
                    str.AppendFormat("            await PrPlatform.Git.{0}(\r\n                {1}, false\r\n            ).Async();\r\n{2}",
                        funcname, URL, "        }\r\n");
                }

                {
                    const string funcname = nameof(PrPlatform.Git.ResetKeep);
                    str.AppendLine();
                    str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false)]\r\n",
                        CMD_Git, "重置 Reset/--Keep 重置 [索引] 如果提交和HEAD之间的文件与HEAD不同，则重置将中止");
                    str.AppendFormat("        internal static async void {0}()\r\n{1}",
                        funcname, "        {\r\n");
                    str.AppendFormat("            await PrPlatform.Git.{0}(\r\n                {1}, false\r\n            ).Async();\r\n{2}",
                        funcname, URL, "        }\r\n");
                }

                {
                    const string funcname = nameof(PrPlatform.Git.ResetMerge);
                    str.AppendLine();
                    str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false)]\r\n",
                        CMD_Git, "重置 Reset/--Merge 重置 [索引 暂存区] 更改和索引产生 重置将被终止");
                    str.AppendFormat("        internal static async void {0}()\r\n{1}",
                        funcname, "        {\r\n");
                    str.AppendFormat("            await PrPlatform.Git.{0}(\r\n                {1}, false\r\n            ).Async();\r\n{2}",
                        funcname, URL, "        }\r\n");
                }

                {
                    const string funcname = nameof(PrPlatform.Git.ResetMixed);
                    str.AppendLine();
                    str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false)]\r\n",
                        CMD_Git, "重置 Reset/--Mixed 重置 [分支 暂存区]");
                    str.AppendFormat("        internal static async void {0}()\r\n{1}",
                        funcname, "        {\r\n");
                    str.AppendFormat("            await PrPlatform.Git.{0}(\r\n                {1}, false\r\n            ).Async();\r\n{2}",
                        funcname, URL, "        }\r\n");
                }

                {
                    const string funcname = nameof(PrPlatform.Git.ResetSoft);
                    str.AppendLine();
                    str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false)]\r\n",
                        CMD_Git, "重置 Reset/--Soft 重置 [分支]");
                    str.AppendFormat("        internal static async void {0}()\r\n{1}",
                        funcname, "        {\r\n");
                    str.AppendFormat("            await PrPlatform.Git.{0}(\r\n                {1}, false\r\n            ).Async();\r\n{2}",
                        funcname, URL, "        }\r\n");
                }

                {
                    str.AppendLine();
                    str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false, {2})]\r\n",
                        CMD_Git, "设置关联远端库 RemoteSetUrl", ++index);
                    str.AppendFormat("        internal static async void {0}()\r\n{1}",
                        nameof(CMD_Git_RemoteSetUrl), "        {\r\n");
                    str.AppendFormat("            await PrPlatform.Git.{0}(\r\n                {1}, false\r\n            ).Async();\r\n{2}",
                        CMD_Git_RemoteSetUrl, URL, "        }\r\n");
                }

                str.Append("    }\r\n}");
                savaDir.Add(info.name, str.ToString());
            }

            var bakdir = new DirectoryInfo(OutPath);
            if (bakdir.Exists) Directory.Delete(OutPath, true);
            bakdir.Create();

            foreach (var item in savaDir)
                File.WriteAllText(Path.Combine(OutPath, string.Concat(item.Key, ".cs")), item.Value, Encoding.UTF8);

            AssetDatabase.Refresh();
#if UNITY_2020_1_OR_NEWER
            AssetDatabase.RefreshSettings();
#endif
        }
    }
}
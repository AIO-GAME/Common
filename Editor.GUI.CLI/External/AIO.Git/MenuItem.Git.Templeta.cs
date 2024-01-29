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
    internal static partial class PackageGen
    {
        private const string INFO_FULL_PATH = @"
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

        private const string INFO_TIP = @"/*|============================================|*|
|*|Author:        |*|Automatic Generation      |*|
|*|Date:          |*|{0}                |*|
|*|=============================================*/";

        private const string INFO_USING = @"
    using System;
    using UnityEditor;
    using UnityEngine;
    using System.IO;
";

        private const string INFO_URL = "FullPath";

        private static void FuncStatic(StringBuilder str, string classname)
        {
            // str.AppendLine();
            // str.AppendFormat("        static {0}()\r\n", classname).Append("        {\r\n");
            // str.Append("            Refresh();\r\n");
            // str.Append("        }\r\n");
        }

        private static void FuncOpen(StringBuilder str)
        {
            str.AppendLine();
            str.Append("        [MenuItem(\"Git/\" + DisplayName + \"/打开 Open\", false, 0)]\r\n");
            str.Append("        internal static async void Open()\r\n").Append("        {\r\n");
            str.Append(
                "            Selection.activeObject = AssetDatabase.LoadAssetAtPath<TextAsset>(Path.Combine(URL, \"package.json\"));\r\n");
            str.Append(
                "            await PrPlatform.Open.Path(FullPath);\r\n").Append("        }\r\n");
        }

        private static void FuncRefresh(StringBuilder str)
        {
            str.AppendLine();
            str.AppendFormat("        private static bool HasUpdate = false;\r\n\r\n");
            str.AppendFormat("        [MenuItem(\"Git/\" + DisplayName + \"/刷新 Refresh\", false, 1), IgnoreConsoleJump, AInit(EInitAttrMode.Editor, -1)]\r\n");
            str.AppendFormat("        internal static async void Refresh()\r\n").Append("        {\r\n");
            str.AppendFormat(
                "            var ret = await PrGit.Helper.GetBehind(FullPath);\r\n");
            str.AppendFormat("            HasUpdate = ret > 0;\r\n");
            str.Append("            if (ret < 0) Console.WriteLine($\"Git Refresh : 本地Git库版本 提交数超过 远程库版本 : {Math.Abs(ret)}\");");
            str.Append("\r\n        }\r\n");
        }

        private static void FuncGetHasUpdate(StringBuilder str)
        {
            str.AppendLine();
            str.AppendFormat("        [MenuItem(\"Git/\" + DisplayName + \"/拉取 Pull\", true)]\r\n");
            str.AppendFormat("        private static bool GetHasUpdate()\r\n").Append("        {\r\n");
            str.AppendFormat("            return HasUpdate;\r\n").Append("        }\r\n");
        }

        private static void FuncAdd(StringBuilder str, int index)
        {
            str.AppendLine();
            str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false, {2})]\r\n",
                nameof(PrPlatform.Git), "添加 Add", index);
            str.AppendFormat("        internal static async void {0}()\r\n{1}",
                nameof(PrPlatform.Git.Add), "        {\r\n");
            str.AppendFormat(
                "            await PrPlatform.Git.{0}(\r\n                {1}, \".\", false\r\n            ).Async();\r\n{2}",
                nameof(PrPlatform.Git.Add), INFO_URL, "        }\r\n");
        }

        private static void FuncPull(StringBuilder str, int index)
        {
            str.AppendLine();
            str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false, {2})]\r\n",
                nameof(PrPlatform.Git), "拉取 Pull", index);
            str.AppendFormat("        internal static async void {0}()\r\n{1}",
                nameof(PrPlatform.Git.Pull), "        {\r\n");
            str.AppendFormat(
                "            await PrPlatform.Git.{0}(\r\n                {1}, false\r\n            ).Async();\r\n{2}",
                nameof(PrPlatform.Git.Pull), INFO_URL, "        }\r\n");
        }

        private static void FuncPush(StringBuilder str, int index)
        {
            str.AppendLine();
            str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false, {2})]\r\n",
                nameof(PrPlatform.Git), "推送 Push", index);
            str.AppendFormat("        internal static async void {0}()\r\n{1}",
                nameof(PrPlatform.Git.Push), "        {\r\n");
            str.AppendFormat(
                "            await PrPlatform.Git.{0}(\r\n                {1}, false\r\n            ).Async();\r\n{2}",
                nameof(PrPlatform.Git.Push), $"({INFO_URL}, null)", "        }\r\n");
        }

        private static void FuncCommit(StringBuilder str, int index)
        {
            str.AppendLine();
            str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false, {2})]\r\n",
                nameof(PrPlatform.Git), "提交 Commit", index);
            str.AppendFormat("        internal static async void {0}()\r\n{1}",
                nameof(PrPlatform.Git.Commit), "        {\r\n");
            str.AppendFormat(
                "            await PrPlatform.Git.{0}(\r\n                {1}, false\r\n            ).Async();\r\n{2}",
                nameof(PrPlatform.Git.Commit), $"({INFO_URL},null)", "        }\r\n");
        }

        private static void FuncUpload(StringBuilder str, int index)
        {
            str.AppendLine();
            str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false, {2})]\r\n",
                nameof(PrPlatform.Git), "上传 Upload", index);
            str.AppendFormat("        internal static async void {0}()\r\n{1}",
                nameof(PrPlatform.Git.Upload), "        {\r\n");
            str.AppendFormat(
                "            await PrPlatform.Git.{0}(\r\n                {1}, false, false, false\r\n            ).Async();\r\n{2}",
                nameof(PrPlatform.Git.Upload), INFO_URL, "        }\r\n");
        }

        private static void FuncClean(StringBuilder str, int index)
        {
            str.AppendLine();
            str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false, {2})]\r\n",
                nameof(PrPlatform.Git), "清理 Clean", index);
            str.AppendFormat("        internal static async void {0}()\r\n{1}",
                nameof(PrPlatform.Git.Clean), "        {\r\n");
            str.AppendFormat("            await PrPlatform.Git.{0}(\r\n                {1}, \"-fd -x" +
                             "\", false\r\n            ).Async();\r\n{2}",
                nameof(PrPlatform.Git.Clean), INFO_URL, "        }\r\n");
        }

        private static void FuncResetHard(StringBuilder str, int index)
        {
            str.AppendLine();
            str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false)]\r\n",
                nameof(PrPlatform.Git), "重置 Reset/--Hard 重置 [分支 暂存区 工作区]");
            str.AppendFormat("        internal static async void {0}()\r\n{1}",
                nameof(PrPlatform.Git.ResetHard), "        {\r\n");
            str.AppendFormat(
                "            await PrPlatform.Git.{0}(\r\n                {1}, false\r\n            ).Async();\r\n{2}",
                nameof(PrPlatform.Git.ResetHard), INFO_URL, "        }\r\n");
        }

        private static void FuncResetKeep(StringBuilder str, int index)
        {
            str.AppendLine();
            str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false)]\r\n",
                nameof(PrPlatform.Git), "重置 Reset/--Keep 重置 [索引] 如果提交和HEAD之间的文件与HEAD不同，则重置将中止");
            str.AppendFormat("        internal static async void {0}()\r\n{1}",
                nameof(PrPlatform.Git.ResetKeep), "        {\r\n");
            str.AppendFormat(
                "            await PrPlatform.Git.{0}(\r\n                {1}, false\r\n            ).Async();\r\n{2}",
                nameof(PrPlatform.Git.ResetKeep), INFO_URL, "        }\r\n");
        }

        private static void FuncResetMerge(StringBuilder str, int index)
        {
            str.AppendLine();
            str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false)]\r\n",
                nameof(PrPlatform.Git), "重置 Reset/--Merge 重置 [索引 暂存区] 更改和索引产生 重置将被终止");
            str.AppendFormat("        internal static async void {0}()\r\n{1}",
                nameof(PrPlatform.Git.ResetMerge), "        {\r\n");
            str.AppendFormat(
                "            await PrPlatform.Git.{0}(\r\n                {1}, false\r\n            ).Async();\r\n{2}",
                nameof(PrPlatform.Git.ResetMerge), INFO_URL, "        }\r\n");
        }

        private static void FuncResetMixed(StringBuilder str, int index)
        {
            str.AppendLine();
            str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false)]\r\n",
                nameof(PrPlatform.Git), "重置 Reset/--Mixed 重置 [分支 暂存区]");
            str.AppendFormat("        internal static async void {0}()\r\n{1}",
                nameof(PrPlatform.Git.ResetMixed), "        {\r\n");
            str.AppendFormat(
                "            await PrPlatform.Git.{0}(\r\n                {1}, false\r\n            ).Async();\r\n{2}",
                nameof(PrPlatform.Git.ResetMixed), INFO_URL, "        }\r\n");
        }

        private static void FuncResetSoft(StringBuilder str, int index)
        {
            str.AppendLine();
            str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false)]\r\n",
                nameof(PrPlatform.Git), "重置 Reset/--Soft 重置 [分支]");
            str.AppendFormat("        internal static async void {0}()\r\n{1}",
                nameof(PrPlatform.Git.ResetSoft), "        {\r\n");
            str.AppendFormat(
                "            await PrPlatform.Git.{0}(\r\n                {1}, false\r\n            ).Async();\r\n{2}",
                nameof(PrPlatform.Git.ResetSoft), INFO_URL, "        }\r\n");
        }

        private static void FuncRemoteSetUrl(StringBuilder str, int index)
        {
            str.AppendLine();
            str.AppendFormat("        [MenuItem(\"{0}/\" + DisplayName + \"/{1}\", false, {2})]\r\n",
                nameof(PrPlatform.Git), "设置关联远端库 RemoteSetUrl", ++index);
            str.AppendFormat("        internal static async void {0}()\r\n{1}",
                nameof(PrPlatform.Git.RemoteSetUrl), "        {\r\n");
            str.AppendFormat(
                "            await PrPlatform.Git.{0}(\r\n                {1}, false\r\n            ).Async();\r\n{2}",
                nameof(PrPlatform.Git.RemoteSetUrl), INFO_URL, "        }\r\n");
        }

        public static bool CreateTemplate(IEnumerable<PackageInfo> infos)
        {
            var ProjectPath = new DirectoryInfo(Application.dataPath).Parent?.FullName
                .Replace('\\', '/')
                .Trim('\\', '/');
            if (string.IsNullOrEmpty(ProjectPath))
            {
                Debug.LogError("Application.dataPath is null");
                return false;
            }

            var OutPath = GetOutPath();

            var savaDir = new Dictionary<string, string>();
            var str = new StringBuilder();
            int index;

            foreach (var info in infos)
            {
                var classname = string.Concat("Git_", info.name.Replace('-', '_').Replace('.', '_')).ToUpper();

                index = 100;
                str.Clear();
                str.AppendFormat(INFO_TIP, DateTime.Now.ToString("yyyy-MM-dd")).Append("\r\n\r\n");
                str.AppendFormat("namespace {0}\r\n{1}", typeof(PackageGen).Namespace, "{");
                str.AppendFormat("{0}\r\n", INFO_USING);

                str.AppendFormat("    /// <summary>\r\n    /// Git Manager {0}\r\n    /// </summary>\r\n",
                    info.displayName);
                
                str.AppendFormat("    [ScriptIcon(IconResource = \"Editor/Icon/App/Git/git-tag-style-1\")]\r\n");
                str.AppendFormat("    internal static partial class {0}\r\n", classname).Append("    {\r\n");
                str.AppendFormat("        internal const string URL = \"{0}\";\r\n",
                    info.resolvedPath.Replace('\\', '/').Replace(ProjectPath, "").Trim('\\', '/'));
                str.AppendFormat("        internal const string DisplayName = \"{0}\";\r\n", info.displayName);
                str.AppendFormat("        internal const string PackageName = \"{0}\";\r\n", info.name);
                str.AppendFormat("{0}\r\n", INFO_FULL_PATH);

                FuncStatic(str, classname);
                FuncOpen(str);
                FuncRefresh(str);
                FuncGetHasUpdate(str);
                FuncAdd(str, ++index);
                FuncPull(str, ++index);
                FuncPush(str, ++index);

                FuncCommit(str, ++index);
                FuncUpload(str, ++index);
                FuncClean(str, ++index);
                FuncResetHard(str, ++index);
                FuncResetKeep(str, ++index);
                FuncResetMerge(str, ++index);
                FuncResetMixed(str, ++index);
                FuncResetSoft(str, ++index);
                FuncRemoteSetUrl(str, ++index);

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
                    if (file.Name.StartsWith("GitUnityProject")) continue;
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

            return change;
        }

        public static bool CreateProject()
        {
            var ProjectPath = Directory.GetParent(Application.dataPath)?.FullName
                .Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar)
                .Trim('\\', '/');
            if (string.IsNullOrEmpty(ProjectPath))
            {
                Debug.LogError("Application.dataPath is null");
                return false;
            }

            var git = Path.Combine(ProjectPath, ".git");
            if (!Directory.Exists(git)) return false;

            var OutPath = GetOutPath();
            var classname = "Git_Project".ToUpper();
            var str = new StringBuilder();
            var index = 100;
            str.Clear();
            str.AppendFormat(INFO_TIP, DateTime.Now.ToString("yyyy-MM-dd")).Append("\r\n\r\n");
            str.AppendFormat("namespace {0}\r\n{1}", typeof(PackageGen).Namespace, "{");
            str.AppendFormat("{0}\r\n", INFO_USING);

            str.AppendFormat("    /// <summary>\r\n    /// Git Manager Project\r\n    /// </summary>\r\n");
            str.AppendFormat("    internal static partial class {0}\r\n", classname).Append("    {\r\n");
            str.AppendFormat("        internal const string URL = \"{0}\";\r\n", "./");
            str.AppendFormat("        internal const string DisplayName = \"Project\";\r\n");
            str.AppendFormat("        internal const string PackageName = \"Project\";\r\n");
            str.Append(@"
        internal static string FullPath
        {
            get
            {
                var p = new DirectoryInfo(Application.dataPath).Parent;
                if (p is null) return URL;
                return p.FullName;
            }
        }");

            FuncStatic(str, classname);
            FuncOpen(str);
            FuncRefresh(str);
            FuncGetHasUpdate(str);
            FuncAdd(str, ++index);
            FuncPull(str, ++index);
            FuncPush(str, ++index);

            FuncCommit(str, ++index);
            FuncUpload(str, ++index);
            FuncClean(str, ++index);
            FuncResetHard(str, ++index);
            FuncResetKeep(str, ++index);
            FuncResetMerge(str, ++index);
            FuncResetMixed(str, ++index);
            FuncResetSoft(str, ++index);
            FuncRemoteSetUrl(str, ++index);

            str.Append("    }\r\n}");
            var outfile = Path.Combine(OutPath, "GitUnityProject.cs");
            if (File.Exists(outfile))
            {
                var old = File.ReadAllText(outfile, Encoding.UTF8);
                if (old == str.ToString()) return false;
            }

            if (!Directory.Exists(OutPath)) Directory.CreateDirectory(OutPath);
            File.WriteAllText(outfile, str.ToString(), Encoding.UTF8);
            return true;
        }
    }
}
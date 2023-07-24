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
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace AIO.Unity.Editor
{
    /// <summary>
    /// PackageGen
    /// </summary>
    internal class PackageGen
    {
        /// <summary>
        /// 生成
        /// </summary>
        [MenuItem("Package/~~~测试~~~/生成")]
        internal static void Generate()
        {
            var search = Client.Search("com.blz.package");
            var packageInfos = AssetDatabase.FindAssets("package", new string[] { "Packages" })
                .Select(AssetDatabase.GUIDToAssetPath)
                .Where(x => AssetDatabase.LoadAssetAtPath<TextAsset>(x) != null)
                .Select(PackageInfo.FindForAssetPath)
                .GroupBy(x => x.assetPath)
                .Select(y => y.First())
                .Where(x => Directory.Exists(Path.Combine(x.assetPath, ".git")))
                .ToList();
            CreateTemplate(packageInfos);
        }

        private const string CMD_Git = "Git";
        internal const string CMD_Git_Pull = "Pull";
        internal const string CMD_Git_Push = "Push";
        internal const string CMD_Git_Add = "Add";
        internal const string CMD_Git_Commit = "Commit";
        internal const string CMD_Git_Upload = "Upload";
        internal const string CMD_Git_Remote_Pull = "Remote Pull";
        internal const string CMD_Git_Clean_FDX = "Clean";

        public static void CreateTemplate(IEnumerable<PackageInfo> infos)
        {
            var OutPath = Application.dataPath.Replace("Assets", "Packages/com.blz.package/Editor/GitModule/AutomaticGeneration");

            const string tips = @"/*|============================================|*|
|*|Author:        |*|Automatic Generation      |*|
|*|Date:          |*|{0}                |*|
|*|=============================================*/";

            const string usings = @"
    using UnityEditor;
    using UnityEngine;
";

            var savaDir = new Dictionary<string, string>();
            var str = new StringBuilder();

            foreach (var info in infos)
            {
                str.Clear();
                str.AppendFormat(tips, DateTime.Now.ToString("yyyy-MM-dd")).Append("\r\n\r\n");
                str.AppendFormat("namespace {0}\r\n{1}", "AIO.Unity.Editor", "{");
                str.AppendFormat("{0}\r\n", usings);

                str.AppendFormat("    /// <summary>\r\n    /// Git管理 {0}\r\n    /// </summary>\r\n", info.displayName);
                str.AppendFormat("    internal static partial class Git_{0}\r\n", info.name.Replace('.', '_').ToUpper()).Append("    {\r\n");

                str.AppendFormat("        internal const string URL = \"{0}\";\r\n", info.assetPath);
                str.AppendFormat("        internal const string DisplayName = \"{0}\";\r\n", info.displayName);
                str.AppendFormat("        internal const string PackageName = \"{0}\";\r\n", info.name);

                str.AppendFormat("        [MenuItem(\"{0}/{1}/\" + DisplayName)]\r\n",
                    CMD_Git, CMD_Git_Add);
                str.AppendFormat("        internal static async void {0}()\r\n{1}",
                    nameof(CMD_Git_Add), "        {\r\n");
                str.AppendFormat("            await PrPlatform.Git.{0}(Application.dataPath.Replace(\"Assets\", URL), false).Async();\r\n{1}",
                    CMD_Git_Add, "        }\r\n");

                str.Append("\r\n    }\r\n}");
                savaDir.Add(info.name, str.ToString());
            }

            var bakdir = new DirectoryInfo(OutPath);
            if (bakdir.Exists) Directory.Delete(OutPath, true);
            bakdir.Create();

            foreach (var item in savaDir)
                File.WriteAllText(Path.Combine(OutPath, string.Concat(item.Key, ".cs")), item.Value, Encoding.UTF8);
        }
    }
}
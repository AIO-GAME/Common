/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-06-26
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;
using UGUIStyle = UnityEngine.GUIStyle;

namespace AIO.UEditor
{
    /// <summary>
    /// 皮肤管理类
    /// </summary>
    public static partial class GEStyle
    {
        private static readonly Dictionary<string, UGUIStyle> StylesDic = new Dictionary<string, UGUIStyle>();

        /// <summary>
        /// 获取皮肤
        /// </summary>
        public static UGUIStyle Get(in string name)
        {
            if (!StylesDic.ContainsKey(name)) StylesDic.Add(name, new UGUIStyle(name));
            return StylesDic[name];
        }

        public static void Gen()
        {
            var sb = new StringBuilder();
            var unityVersion = Application.unityVersion.Split('.')[0];
            foreach (var style in GUI.skin.customStyles) sb.AppendLine(style.name);
            AHelper.IO.WriteUTF8(
                PackageInfo.FindForAssembly(typeof(GEStyle).Assembly).resolvedPath +
                $"/Resources/Editor/Graph/Style/{unityVersion}.txt", sb.ToString());
            AssetDatabase.Refresh();
#if UNITY_2020_1_OR_NEWER
            AssetDatabase.RefreshSettings();
#endif
        }

        private static StringBuilder GenContent(IEnumerable<string> styleNames, string version = null)
        {
            var sb = new StringBuilder();
            sb.AppendLine("/*|✩ - - - - - |||");
            sb.AppendLine($"|||✩ Date:     ||| -> {DateTime.Now:yyyy-MM-dd}");
            sb.AppendLine($"|||✩ Document: ||| -> Automatic Generation Unity {version}");
            sb.AppendLine("|||✩ - - - - - |*/");
            sb.AppendLine();
            if (!string.IsNullOrEmpty(version)) sb.AppendLine($"#if UNITY_{version}");
            sb.AppendLine("using UGUIStyle = UnityEngine.GUIStyle;");
            sb.AppendLine();
            sb.AppendLine("namespace AIO.UEditor");
            sb.AppendLine("{");
            sb.AppendLine("    public partial class GEStyle");
            sb.AppendLine("    {");

            foreach (var name in styleNames)
            {
                var key = name.Replace(" ", "").Replace(".", "_");
                sb.AppendLine($"        public static UGUIStyle {key} => Get(\"{name}\");");
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");
            if (!string.IsNullOrEmpty(version)) sb.AppendLine("#endif");
            return sb;
        }

        [MenuItem("AIO/Gen/Common GUIStyle")]
        public static void GenCommon()
        {
            var dict = new Dictionary<string, int>();
            var versionList = new Dictionary<string, IList<string>>();
            var formatPathCS = PackageInfo.FindForAssembly(typeof(GEStyle).Assembly).resolvedPath +
                               "/Editor.GUI.CLI/External/AIO.Graph/GEStyle/U.GUIStyle.{0}.cs";
            var formatPath = PackageInfo.FindForAssembly(typeof(GEStyle).Assembly).resolvedPath +
                             "/Resources/Editor/Graph/Style/{0}.txt";
            var versions = new string[] { "2019", "2020", "2021", "2022", "2023", "2024", "2025" };
            var validVersionNum = 0;
            foreach (var version in versions)
            {
                var path = string.Format(formatPath, version);
                if (!File.Exists(path)) continue;
                CalcNumber(dict, File.ReadAllLines(path), out var list);
                versionList.Add(version, list);
                validVersionNum++;
            }

            var common = new Dictionary<string, int>();
            var sb = new StringBuilder();
            foreach (var item in dict
                         .Where(item => item.Value >= validVersionNum)
                         .Where(item => !common.ContainsKey(item.Key)))
            {
                common.Add(item.Key, item.Value);
                sb.AppendLine(item.Key);
            }

            File.WriteAllText(string.Format(formatPath, "Common"), sb.ToString());
            File.WriteAllText(string.Format(formatPathCS, "Common"), GenContent(common.Keys).ToString());

            foreach (var version in versions)
            {
                if (!versionList.ContainsKey(version)) continue;
                var list = versionList[version];
                for (var i = list.Count - 1; i >= 0; i--)
                {
                    if (common.ContainsKey(list[i]))
                    {
                        list.RemoveAt(i);
                    }
                }

                if (list.Count == 0) continue;
                var content = GenContent(list, version).ToString();
                if (string.IsNullOrEmpty(content)) continue;


                File.WriteAllText(string.Format(formatPathCS, version), content);
            }

            AssetDatabase.Refresh();
#if UNITY_2020_1_OR_NEWER
            AssetDatabase.RefreshSettings();
#endif
            CompilationPipeline.RequestScriptCompilation();
        }

        private static void CalcNumber(
            IDictionary<string, int> dictionary,
            IEnumerable<string> liens,
            out IList<string> list)
        {
            list = new List<string>();
            foreach (var name in liens)
            {
                if (!dictionary.ContainsKey(name)) dictionary.Add(name, 0);
                dictionary[name]++;
                list.Add(name);
            }
        }
    }
}
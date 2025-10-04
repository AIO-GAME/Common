#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

#endregion

namespace AIO.UEditor
{
    [ScriptIcon(IconResource = "Editor/Icon/Color/general")]
    internal static class GWindowGen
    {
        private static string GetOutPath() { return Path.Combine(Application.dataPath, "Editor", "Gen", "GWindow"); }

        [AInit(EInitAttrMode.Editor, ushort.MaxValue - 2)]
        internal static void Generate()
        {
            var dic = new Dictionary<Type, GWindowAttribute>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsAbstract || !type.IsClass || !type.IsSubclassOf(typeof(EditorWindow))) continue;
                var attr = type.GetCustomAttribute<GWindowAttribute>();
                if (attr is null) continue;
                ScriptIcon.SetIcon(attr.FilePath, attr.GetTexture2D());
                if (string.IsNullOrEmpty(attr.Menu)) continue;
                dic.Add(type, attr);
            }

            // var change = CreateProject(dic);
            // if (!change) return;
            // AssetDatabase.Refresh();
            // CompilationPipeline.RequestScriptCompilation();

            foreach (var kvp in dic.Where(kvp => !string.IsNullOrEmpty(kvp.Value.Menu)))
            {
                var kv = kvp;
                GEHelper.AddMenuItem(kv.Value.Menu, kv.Value.Shortcut, false, kv.Value.MenuPriority, () =>
                {
                    EHelper.Window.Open(kv.Key, kv.Value.Title, kv.Value.DockType);
                });
            }
        }

        private const string INFO_TIP = @"/*|================================|*|
|*|      Automatic Generation      |*|
|*|================================|*/";

        private static bool CreateProject(IDictionary<Type, GWindowAttribute> dictionary)
        {
            var outPath   = GetOutPath();
            var classname = "GWindowGen".ToUpper();
            var str       = new ScriptTextBuilder();
            str.WriteHeader(INFO_TIP).WriteLine();
            str.IncNamespace(typeof(GWindowGen).Namespace);
            str.WriteUsing("System");
            str.WriteUsing("UnityEditor");
            str.WriteUsing("AIO");
            str.WriteLine();
            str.AnnotSummary("GWindow Manager");
            str.AnnotDate();
            str.WriteLine("internal static partial class {0}", classname);
            str.IncBlock();

            foreach (var pair in dictionary)
            {
                var args1 = pair.Value.Menu.Trim('\\', '/', ' ');
                var args3 = pair.Key.FullName?.Replace(".", "_");
                str.WriteLine("[MenuItem(\"{0}\", priority = {1})]", args1, pair.Value.MenuPriority).
                    WriteLine("public static void {0}_Open()", args3).
                    IncBlock().
                    WriteLine("EHelper.Window.Open<{0}>({1});", pair.Key.FullName, string.Join(", ", pair.Value.Dock)).
                    DecBlock().
                    WriteLine();
            }

            str.DecBlock();
            str.DecNamespace();

            var outfile = Path.Combine(outPath, "MenuItems.Designer.cs");
            if (File.Exists(outfile))
            {
                var old = File.ReadAllText(outfile, Encoding.UTF8);
                if (old == str.ToString()) return false;
            }

            str.Save(outfile);
            return true;
        }
    }
}
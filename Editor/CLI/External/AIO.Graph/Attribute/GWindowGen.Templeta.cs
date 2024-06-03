#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

#endregion

namespace AIO.UEditor
{
    [ScriptIcon(IconResource = "Editor/Icon/Color/general")]
    internal static partial class GWindowGen
    {
        private const string INFO_TIP = @"/*|================================|*|
|*|      Automatic Generation      |*|
|*|================================|*/";

        private static bool CreateProject(IDictionary<Type, GWindowAttribute> dictionary)
        {
            var OutPath   = GetOutPath();
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
                str.WriteLine("[MenuItem(\"{0}\", priority = {1})]", pair.Value.Menu.Trim('\\', '/', ' '), pair.Value.MenuPriority)
                   .WriteLine("public static void {0}_Open()", pair.Key.FullName?.Replace(".", "_"))
                   .IncBlock()
                   .WriteLine("EHelper.Window.Open<{0}>({1});", pair.Key.FullName, string.Join(", ", pair.Value.Dock))
                   .DecBlock()
                   .WriteLine();
            }

            str.DecBlock();
            str.DecNamespace();

            var outfile = Path.Combine(OutPath, "MenuItems.Designer.cs");
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
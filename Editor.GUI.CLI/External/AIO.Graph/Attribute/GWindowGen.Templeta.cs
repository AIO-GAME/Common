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
        private const string INFO_TIP = @"/*|============================================|*|
|*|Author:        |*|Automatic Generation      |*|
|*|Date:          |*|{0}                |*|
|*|=============================================*/";

        private const string INFO_USING = @"
    using UnityEditor;
    using AIO;
";

        public static bool CreateProject(IDictionary<Type, GWindowAttribute> dictionary)
        {
            var OutPath = GetOutPath();
            var classname = "GWindowGen".ToUpper();
            var str = new StringBuilder();
            str.AppendFormat(INFO_TIP, DateTime.Now.ToString("yyyy-MM-dd")).Append("\r\n\r\n");
            str.AppendFormat("namespace {0}\r\n{1}", typeof(GWindowGen).Namespace, "{");
            str.AppendFormat("{0}\r\n", INFO_USING);

            str.AppendFormat("    /// <summary>\r\n    /// GWindow Manager\r\n    /// </summary>\r\n");
            str.AppendFormat("    [ScriptIcon(IconResource = \"Editor/Icon/Color/general\")]\r\n");
            str.AppendFormat("    internal static partial class {0}\r\n", classname).Append("    {\r\n");

            foreach (var pair in dictionary)
            {
                str.AppendFormat("        [MenuItem(\"{0}\", priority = {1})]\r\n", pair.Value.Menu,
                                 pair.Value.MenuPriority);
                str.AppendFormat("        public static void {0}_Open()\r\n", pair.Key.FullName?.Replace(".", "_")).
                    Append("        {\r\n");
                str.AppendFormat("            EHelper.Window.Open<{0}>();\r\n", pair.Key.FullName).
                    Append("        }\r\n");
            }

            str.Append("    }\r\n}");
            var outfile = Path.Combine(OutPath, "MenuItems.Designer.cs");
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
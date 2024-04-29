#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0109 // 

#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    public partial class GELayoutSingleton
    {
        [FuncParam(Group = "Foldout", IsArray = true)]
        private static List<FunctionChunk> VFoldout()
        {
            var chunks = new List<FunctionChunk>();

            var value = new FunctionParam("Action", "action", "") { Comments         = "回调函数" };
            var foldout = new FunctionParam("bool", "foldout", "foldout") { Comments = "显示的折叠状态" };
            var toggleOnLabelClick = new FunctionParam("bool", "toggleOnLabelClick", "toggleOnLabelClick")
            {
                Comments = "是否在单击标签时切换折叠状态"
            };
            foreach (var item in new[] { "string", "GUIContent" })
            {
                var label = new FunctionParam(item, "label", "label") { Comments = "标签" };
                foreach (var param in new[]
                {
                    new[] { label, foldout },
                    new[] { label, foldout, Style },
                    new[] { label, foldout, toggleOnLabelClick },
                    new[] { label, foldout, toggleOnLabelClick, Style }
                })
                {
                    var chunk = new FunctionChunk
                    {
                        State          = TChunkState.NewStatic,
                        Comments       = "绘制 折叠式箭头",
                        Name           = "VFoldout",
                        Params         = param,
                        ReturnType     = "bool",
                        ReturnComments = "true:呈现子对象,false:隐藏",
                        Attributes     = new List<string> { "ExcludeFromDocs" }
                    };
                    chunk.Content =
                        $"return EditorGUILayout.Foldout({chunk.GetParamValues().Replace("label, foldout", "foldout, label")});";
                    chunks.Add(chunk);
                }

                foreach (var param in new[]
                {
                    new[] { value, label, foldout },
                    new[] { value, label, foldout, Style },
                    new[] { value, label, foldout, toggleOnLabelClick },
                    new[] { value, label, foldout, toggleOnLabelClick, Style }
                })
                {
                    var chunk = new FunctionChunk
                    {
                        State          = TChunkState.NewStatic,
                        Comments       = "绘制 折叠式箭头",
                        Name           = "VFoldout",
                        Params         = param,
                        ReturnType     = "bool",
                        ReturnComments = "true:呈现子对象,false:隐藏",
                        Attributes     = new List<string> { "ExcludeFromDocs" }
                    };
                    chunk.ContentBuilder.AppendLine(
                        $"foldout = EditorGUILayout.Foldout({chunk.GetParamValues().Replace("label, foldout", "foldout, label")});");
                    chunk.ContentBuilder.AppendLine("if (foldout) action?.Invoke();");
                    chunk.ContentBuilder.AppendLine("return foldout;");
                    chunks.Add(chunk);
                }
            }


            return chunks;
        }

        [FuncParam(Group = "Foldout", IsArray = true)]
        private static List<FunctionChunk> EditorGUILayoutFoldoutHeaderGroup()
        {
            var chunks = new List<FunctionChunk>();

            var value = new FunctionParam("Action", "action", "") { Comments                          = "回调函数" };
            var foldout = new FunctionParam("bool", "foldout", "foldout") { Comments                  = "显示的折叠状态" };
            var style = new FunctionParam("GUIStyle", "style", "style") { Comments                    = "显示风格", Default       = "null" };
            var menuAction = new FunctionParam("Action<Rect>", "menuAction", "menuAction") { Comments = "操作菜单", Default       = "null" };
            var menuIcon = new FunctionParam("GUIStyle", "menuIcon", "menuIcon") { Comments           = "菜单ICON显示风格", Default = "null" };

            foreach (var item in new[] { "string", "GUIContent" })
            {
                var label = new FunctionParam(item, "label", "label") { Comments = "标签" };
                {
                    var chunk = new FunctionChunk
                    {
                        State           = TChunkState.NewStatic,
                        Comments        = "绘制 折页排版",
                        Name            = "VFoldoutHeader",
                        Params          = new[] { value, label, foldout, style, menuAction, menuIcon },
                        ReturnType      = "bool",
                        ReturnComments  = "true:呈现子对象,false:隐藏",
                        Attributes      = new List<string> { "ExcludeFromDocs" },
                        MacroDefinition = "UNITY_2019_1_OR_NEWER"
                    };
                    chunk.ContentBuilder.AppendLine(
                        $"foldout = EditorGUILayout.BeginFoldoutHeaderGroup({chunk.GetParamValues().Replace("label, foldout", "foldout, label")});");
                    chunk.ContentBuilder.AppendLine("if (foldout) action?.Invoke();");
                    chunk.ContentBuilder.AppendLine("EditorGUILayout.EndFoldoutHeaderGroup();");
                    chunk.ContentBuilder.AppendLine("return foldout;");
                    chunks.Add(chunk);
                }
                {
                    var chunk = new FunctionChunk
                    {
                        State           = TChunkState.NewStatic,
                        Comments        = "开始绘制 折页排版",
                        Name            = "BeginFoldoutHeader",
                        Params          = new[] { label, foldout, style, menuAction, menuIcon },
                        ReturnType      = "bool",
                        ReturnComments  = "true:呈现子对象,false:隐藏",
                        Attributes      = new List<string> { "ExcludeFromDocs" },
                        MacroDefinition = "UNITY_2019_1_OR_NEWER"
                    };
                    chunk.Content =
                        $"return EditorGUILayout.BeginFoldoutHeaderGroup({chunk.GetParamValues().Replace("label, foldout", "foldout, label")});";
                    chunks.Add(chunk);
                }
            }

            {
                var chunk = new FunctionChunk
                {
                    State           = TChunkState.NewStatic,
                    Comments        = "结束绘制 折页排版",
                    Name            = "EndFoldoutHeader",
                    Params          = new FunctionParam[] { },
                    Attributes      = new List<string> { "ExcludeFromDocs" },
                    MacroDefinition = "UNITY_2019_1_OR_NEWER",
                    Content         = "EditorGUILayout.EndFoldoutHeaderGroup();"
                };
                chunks.Add(chunk);
            }
            return chunks;
        }

        [FuncParam(Group = "Foldout", IsArray = true)]
        private static List<FunctionChunk> VFoldoutHeaderToggle()
        {
            var chunks = new List<FunctionChunk>();

            var value = new FunctionParam("Action", "action", "") { Comments         = "回调函数" };
            var foldout = new FunctionParam("bool", "foldout", "foldout") { Comments = "显示的折叠状态" };
            var style = new FunctionParam("GUIStyle", "style", "style") { Comments   = "显示风格", Default = "null" };

            foreach (var item in new[] { "string", "GUIContent" })
            {
                var label = new FunctionParam(item, "label", "label") { Comments = "标签" };
                {
                    var chunk = new FunctionChunk
                    {
                        State           = TChunkState.NewStatic,
                        Comments        = "绘制 折页排版",
                        Name            = "VFoldoutHeaderGroup",
                        Params          = new[] { value, label, foldout, style },
                        ReturnType      = "bool",
                        ReturnComments  = "true:呈现子对象,false:隐藏",
                        Attributes      = new List<string> { "ExcludeFromDocs" },
                        MacroDefinition = "UNITY_2018_1_OR_NEWER"
                    };
                    chunk.ContentBuilder.AppendLine("#if UNITY_2019_1_OR_NEWER");
                    chunk.ContentBuilder.AppendLine(
                        "foldout = EditorGUILayout.ToggleLeft(label, foldout, style ?? \"FoldoutHeader\", GTOption.WidthExpand(true));");
                    chunk.ContentBuilder.AppendLine("#else");
                    chunk.ContentBuilder.AppendLine(
                        "foldout = EditorGUILayout.ToggleLeft(label, foldout, style ?? \"GUIEditor.BreadcrumbLeft\", GTOption.WidthExpand(true));");
                    chunk.ContentBuilder.AppendLine("#endif");
                    chunk.ContentBuilder.AppendLine("EditorGUILayout.Space();");
                    chunk.ContentBuilder.AppendLine("if (foldout) action?.Invoke();");
                    chunk.ContentBuilder.AppendLine("return foldout;");
                    chunks.Add(chunk);
                }
            }

            return chunks;
        }
    }
}
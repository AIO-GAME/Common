#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0109 // 

using System.Collections.Generic;

namespace AIO
{
    public partial class GELayoutSingleton
    {
        [FuncParam(Group = "Scope Horizontal", IsArray = true)]
        private static List<FunctionChunk> Horizontal()
        {
            var chunks = new List<FunctionChunk>();
            var value = new FunctionParam("Action", "action", "") { Comments = "回调函数" };
            var width_float = new FunctionParam("float", "width", "GUILayout.Width(width)") { Comments = "宽度" };
            var height_float = new FunctionParam("float", "height", "GUILayout.Width(height)") { Comments = "高度" };
            var paramsList = new List<FunctionParam[]>()
            {
                new FunctionParam[] { value, Options, },
                new FunctionParam[] { value, width_float, },
                new FunctionParam[] { value, width_float, height_float },
                new FunctionParam[] { value, Style, Options, },
                new FunctionParam[] { value, Style, width_float, },
                new FunctionParam[] { value, Style, width_float, height_float },
            };
            foreach (var param in paramsList)
            {
                var chunk = new FunctionChunk
                {
                    State = TChunkState.NewStatic,
                    Comments = "绘制 横排视图",
                    Name = "VHorizontal",
                    Params = param,
                    ReturnType = "void",
                };
                chunk.ContentBuilder.AppendLine("if (action == null) return;");
                chunk.ContentBuilder.AppendLine($"EditorGUILayout.BeginHorizontal({chunk.GetParamValues()});");
                chunk.ContentBuilder.AppendLine("action?.Invoke();");
                chunk.ContentBuilder.AppendLine("EditorGUILayout.EndHorizontal();");
                chunks.Add(chunk);
            }

            paramsList = new List<FunctionParam[]>()
            {
                new FunctionParam[] { width_float, },
                new FunctionParam[] { width_float, height_float, },
                new FunctionParam[] { Options, },
                new FunctionParam[] { Style, Options, },
                new FunctionParam[] { Style, width_float, },
                new FunctionParam[] { Style, width_float, height_float, },
            };
            foreach (var param in paramsList)
            {
                var chunk = new FunctionChunk
                {
                    State = TChunkState.NewStatic,
                    Comments = "绘制 横排视图",
                    Name = "BeginHorizontal",
                    Params = param,
                    ReturnType = "void",
                };
                chunk.Content = $"EditorGUILayout.BeginHorizontal({chunk.GetParamValues()});";
                chunks.Add(chunk);
            }

            {
                var chunk = new FunctionChunk
                {
                    State = TChunkState.NewStatic,
                    Comments = "绘制 横排视图",
                    Name = "EndHorizontal",
                    ReturnType = "void",
                    Content = "EditorGUILayout.EndHorizontal();"
                };
                chunks.Add(chunk);
            }
            return chunks;
        }

        [FuncParam(Group = "Scope Vertical", IsArray = true)]
        private static List<FunctionChunk> Vertical()
        {
            var chunks = new List<FunctionChunk>();
            var value = new FunctionParam("Action", "action", "") { Comments = "回调函数" };
            var width_float = new FunctionParam("float", "width", "GUILayout.Width(width)") { Comments = "宽度" };
            var height_float = new FunctionParam("float", "height", "GUILayout.Width(height)") { Comments = "高度" };
            var paramsList = new List<FunctionParam[]>()
            {
                new FunctionParam[] { value, Options, },
                new FunctionParam[] { value, width_float, },
                new FunctionParam[] { value, width_float, height_float },
                new FunctionParam[] { value, Style, Options, },
                new FunctionParam[] { value, Style, width_float, },
                new FunctionParam[] { value, Style, width_float, height_float },
            };
            foreach (var param in paramsList)
            {
                var chunk = new FunctionChunk
                {
                    State = TChunkState.NewStatic,
                    Comments = "绘制 竖排视图",
                    Name = "Vertical",
                    Params = param,
                    ReturnType = "void",
                };
                chunk.ContentBuilder.AppendLine("if (action == null) return;");
                chunk.ContentBuilder.AppendLine($"EditorGUILayout.BeginVertical({chunk.GetParamValues()});");
                chunk.ContentBuilder.AppendLine("action?.Invoke();");
                chunk.ContentBuilder.AppendLine("EditorGUILayout.EndVertical();");
                chunks.Add(chunk);
            }

            paramsList = new List<FunctionParam[]>()
            {
                new FunctionParam[] { width_float, },
                new FunctionParam[] { width_float, height_float, },
                new FunctionParam[] { Options, },
                new FunctionParam[] { Style, Options, },
                new FunctionParam[] { Style, width_float, },
                new FunctionParam[] { Style, width_float, height_float, },
            };
            foreach (var param in paramsList)
            {
                var chunk = new FunctionChunk
                {
                    State = TChunkState.NewStatic,
                    Comments = "绘制 竖排视图",
                    Name = "BeginVertical",
                    Params = param,
                    ReturnType = "void",
                };
                chunk.Content = $"EditorGUILayout.BeginVertical({chunk.GetParamValues()});";
                chunks.Add(chunk);
            }

            {
                var chunk = new FunctionChunk
                {
                    State = TChunkState.NewStatic,
                    Comments = "绘制 竖排视图",
                    Name = "EndVertical",
                    ReturnType = "void",
                    Content = "EditorGUILayout.EndVertical();"
                };
                chunks.Add(chunk);
            }
            return chunks;
        }

        [FuncParam(Group = "Scope ScrollView", IsArray = true)]
        private static List<FunctionChunk> VScrollView()
        {
            var chunks = new List<FunctionChunk>();
            var width_float = new FunctionParam("float", "width", "GUILayout.Width(width)") { Comments = "宽度" };
            var height_float = new FunctionParam("float", "height", "GUILayout.Width(height)") { Comments = "高度" };

            var value = new FunctionParam("Action", "action", "") { Comments = "回调函数" };
            var v2 = new FunctionParam("Vector2", "v2", "v2") { Comments = "视图在X和Y方向上滚动的像素距离" };
            var alwaysShowHorizontal = new FunctionParam("bool", "alwaysShowHorizontal") { Comments = "始终显示水平滚动条" };
            var alwaysShowVertical = new FunctionParam("bool", "alwaysShowVertical") { Comments = "始终显示垂直滚动条" };
            var styles_h = new FunctionParam("GUIStyle", "styles_h") { Comments = "水平滚动条风格" };
            var styles_v = new FunctionParam("GUIStyle", "styles_v") { Comments = "垂直滚动条风格" };
            var styles_b = new FunctionParam("GUIStyle", "styles_b") { Comments = "底板风格" };

            var paramsList = new List<FunctionParam[]>()
            {
                new FunctionParam[] { value, v2, Options, },
                new FunctionParam[] { value, v2, width_float, },
                new FunctionParam[] { value, v2, width_float, height_float },
                new FunctionParam[] { value, v2, Options, },
                new FunctionParam[] { value, v2, width_float, },
                new FunctionParam[] { value, v2, width_float, height_float },
                new FunctionParam[] { value, v2, alwaysShowHorizontal, alwaysShowVertical, Options, },
                new FunctionParam[] { value, v2, styles_h, styles_v, Options, },
                new FunctionParam[]
                    { value, v2, alwaysShowHorizontal, alwaysShowVertical, styles_h, styles_v, styles_b, Options, },
            };
            foreach (var param in paramsList)
            {
                var chunk = new FunctionChunk
                {
                    State = TChunkState.NewStatic,
                    Comments = "绘制 滚动视图",
                    Name = "VScrollView",
                    Params = param,
                    ReturnType = "Vector2",
                };
                chunk.ContentBuilder.AppendLine($"v2 = EditorGUILayout.BeginScrollView({chunk.GetParamValues()});");
                chunk.ContentBuilder.AppendLine("action?.Invoke();");
                chunk.ContentBuilder.AppendLine("EditorGUILayout.EndScrollView();");
                chunk.ContentBuilder.AppendLine("return v2;");
                chunks.Add(chunk);
            }

            paramsList = new List<FunctionParam[]>()
            {
                new FunctionParam[] { v2, Options, },
                new FunctionParam[] { v2, width_float, },
                new FunctionParam[] { v2, width_float, height_float },
                new FunctionParam[] { v2, Options, },
                new FunctionParam[] { v2, width_float, },
                new FunctionParam[] { v2, width_float, height_float },
                new FunctionParam[] { v2, alwaysShowHorizontal, alwaysShowVertical, Options, },
                new FunctionParam[] { v2, styles_h, styles_v, Options, },
                new FunctionParam[]
                    { v2, alwaysShowHorizontal, alwaysShowVertical, styles_h, styles_v, styles_b, Options, },
            };
            foreach (var param in paramsList)
            {
                var chunk = new FunctionChunk
                {
                    State = TChunkState.NewStatic,
                    Comments = "绘制 滚动视图",
                    Name = "BeginScrollView",
                    Params = param,
                    ReturnType = "Vector2",
                };
                chunk.Content = $"return EditorGUILayout.BeginScrollView({chunk.GetParamValues()});";
                chunks.Add(chunk);
            }

            {
                var chunk = new FunctionChunk
                {
                    State = TChunkState.NewStatic,
                    Comments = "绘制 滚动视图",
                    Name = "EndScrollView",
                    ReturnType = "void",
                    Content = "EditorGUILayout.EndScrollView();"
                };
                chunks.Add(chunk);
            }


            return chunks;
        }

        [FuncParam(Group = "Scope Group", IsArray = true)]
        private static List<FunctionChunk> VGroup()
        {
            var chunks = new List<FunctionChunk>();

            var value = new FunctionParam("Action", "action", "") { Comments = "回调函数" };
            var toggle = new FunctionParam("bool", "toggle", "toggle") { Comments = "显示开关" };
            foreach (var item in new string[] { "string", "GUIContent" })
            {
                var label = new FunctionParam(item, "label", "label") { Comments = "标签" };
                var paramsList = new List<FunctionParam[]>
                {
                    new FunctionParam[] { value, label, toggle, },
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State = TChunkState.NewStatic,
                        Comments = "绘制 组视图",
                        Name = "VGroup",
                        Params = param,
                        ReturnType = "bool",
                    };
                    chunk.ContentBuilder.AppendLine(
                        $"toggle = EditorGUILayout.BeginToggleGroup({chunk.GetParamValues()});");
                    chunk.ContentBuilder.AppendLine("if (toggle) action?.Invoke();");
                    chunk.ContentBuilder.AppendLine("EditorGUILayout.EndToggleGroup();");
                    chunk.ContentBuilder.AppendLine("return toggle;");
                    chunks.Add(chunk);
                }

                paramsList = new List<FunctionParam[]>
                {
                    new FunctionParam[] { label, toggle, },
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State = TChunkState.NewStatic,
                        Comments = "开始绘制 组视图",
                        Name = "BeginGroup",
                        Params = param,
                        ReturnType = "bool",
                    };
                    chunk.Content = $"return EditorGUILayout.BeginToggleGroup({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }
            }

            {
                var chunk = new FunctionChunk
                {
                    State = TChunkState.NewStatic,
                    Comments = "结束绘制 组视图",
                    Name = "EndGroup",
                    ReturnType = "void",
                    Content = "EditorGUILayout.EndToggleGroup();"
                };
                chunks.Add(chunk);
            }


            return chunks;
        }

        [FuncParam(Group = "Scope Group", IsArray = true)]
        private static List<FunctionChunk> VGroupDisabled()
        {
            var chunks = new List<FunctionChunk>();

            var value = new FunctionParam("Action", "action", "") { Comments = "回调函数" };
            var toggle = new FunctionParam("bool", "toggle", "toggle") { Comments = "显示开关" };
            {
                var chunk = new FunctionChunk
                {
                    State = TChunkState.NewStatic,
                    Comments = "绘制 禁用组视图",
                    Name = "VGroupDisabled",
                    Params = new FunctionParam[] { value, toggle, },
                };
                chunk.ContentBuilder.AppendLine($"EditorGUI.BeginDisabledGroup({chunk.GetParamValues()});");
                chunk.ContentBuilder.AppendLine("action?.Invoke();");
                chunk.ContentBuilder.AppendLine("EditorGUI.EndDisabledGroup();");
                chunks.Add(chunk);
            }

            {
                var chunk = new FunctionChunk
                {
                    State = TChunkState.NewStatic,
                    Comments = "开始绘制 禁用组视图",
                    Name = "BeginGroupDisabled",
                    Params = new FunctionParam[] { toggle, },
                };
                chunk.Content = $"EditorGUI.BeginDisabledGroup({chunk.GetParamValues()});";
                chunks.Add(chunk);
            }

            {
                var chunk = new FunctionChunk
                {
                    State = TChunkState.NewStatic,
                    Comments = "结束绘制 禁用组视图",
                    Name = "EndGroupDisabled",
                    ReturnType = "void",
                    Content = "EditorGUI.EndDisabledGroup();"
                };
                chunks.Add(chunk);
            }


            return chunks;
        }

        [FuncParam(Group = "Scope Group", IsArray = true)]
        private static List<FunctionChunk> VGroupBuildTargetSelection()
        {
            var chunks = new List<FunctionChunk>();

            var value = new FunctionParam("Action<BuildTargetGroup>", "action", "") { Comments = "回调函数" };
            {
                var chunk = new FunctionChunk
                {
                    State = TChunkState.NewStatic,
                    Comments = "绘制 开始构建目标分组",
                    Name = "VGroupBuildTargetSelection",
                    Params = new FunctionParam[] { value, },
                    MacroDefinition = "UNITY_2019_1_OR_NEWER",
                };
                chunk.ContentBuilder.AppendLine($"var value = EditorGUILayout.BeginBuildTargetSelectionGrouping();");
                chunk.ContentBuilder.AppendLine("action?.Invoke(value);");
                chunk.ContentBuilder.AppendLine("EditorGUILayout.EndBuildTargetSelectionGrouping();");
                chunks.Add(chunk);
            }
            {
                var chunk = new FunctionChunk
                {
                    State = TChunkState.NewStatic,
                    Comments = "绘制 开始构建目标分组",
                    Name = "VGroupBuildTargetSelection",
                    Params = new FunctionParam[] { value, new FunctionParam("BuildTargetGroup", "value") },
                    MacroDefinition = "UNITY_2019_1_OR_NEWER",
                    ReturnType = "BuildTargetGroup",
                };
                chunk.ContentBuilder.AppendLine("value = EditorGUILayout.BeginBuildTargetSelectionGrouping();");
                chunk.ContentBuilder.AppendLine("action?.Invoke(value);");
                chunk.ContentBuilder.AppendLine("EditorGUILayout.EndBuildTargetSelectionGrouping();");
                chunk.ContentBuilder.AppendLine("return value;");
                chunks.Add(chunk);
            }
            {
                var chunk = new FunctionChunk
                {
                    MacroDefinition = "UNITY_2019_1_OR_NEWER",
                    State = TChunkState.NewStatic,
                    Comments = "开始绘制 目标分组视图",
                    Name = "BeginGroupBuildTargetSelection",
                    Params = new FunctionParam[] { },
                    ReturnType = "BuildTargetGroup",
                    Content = $"return EditorGUILayout.BeginBuildTargetSelectionGrouping();"
                };
                chunks.Add(chunk);
            }

            {
                var chunk = new FunctionChunk
                {
                    MacroDefinition = "UNITY_2019_1_OR_NEWER",
                    State = TChunkState.NewStatic,
                    Comments = "结束绘制 目标分组视图",
                    Name = "EndGroupBuildTargetSelection",
                    ReturnType = "void",
                    Content = "EditorGUILayout.EndBuildTargetSelectionGrouping();"
                };
                chunks.Add(chunk);
            }


            return chunks;
        }

        [FuncParam(Group = "Scope Group", IsArray = true)]
        private static List<FunctionChunk> VGroupFade()
        {
            var chunks = new List<FunctionChunk>();

            var value = new FunctionParam("Action", "action", "") { Comments = "回调函数" };
            var alpha = new FunctionParam("float", "alpha", "alpha") { Comments = "介于0到1之间的值，0是隐藏的，1是完全可见的" };
            {
                var chunk = new FunctionChunk
                {
                    State = TChunkState.NewStatic,
                    Comments = "绘制 隐藏显示分组视图",
                    Name = "VGroupFade",
                    Params = new FunctionParam[] { value, alpha },
                };
                chunk.ContentBuilder.AppendLine($"if (action == null) return;");
                chunk.ContentBuilder.AppendLine("if (EditorGUILayout.BeginFadeGroup(alpha)) action?.Invoke();");
                chunk.ContentBuilder.AppendLine("EditorGUILayout.EndFadeGroup();");
                chunks.Add(chunk);
            }
            {
                var chunk = new FunctionChunk
                {
                    State = TChunkState.NewStatic,
                    Comments = "绘制 隐藏显示分组视图",
                    Name = "VGroupFade",
                    Params = new FunctionParam[]
                    {
                        new FunctionParam(value) { Type = "Action<bool>" },
                        new FunctionParam("bool", "show"),
                        alpha
                    },
                    ReturnType = "bool",
                };
                chunk.ContentBuilder.AppendLine("show = EditorGUILayout.BeginFadeGroup(alpha);");
                chunk.ContentBuilder.AppendLine("action?.Invoke(show);");
                chunk.ContentBuilder.AppendLine("EditorGUILayout.EndFadeGroup();");
                chunk.ContentBuilder.AppendLine("return show;");
                chunks.Add(chunk);
            }
            {
                var chunk = new FunctionChunk
                {
                    State = TChunkState.NewStatic,
                    Comments = "开始绘制 隐藏显示分组视图",
                    Name = "BeginGroupFade",
                    Params = new FunctionParam[] { alpha },
                    ReturnType = "bool",
                    Content = $"return EditorGUILayout.BeginFadeGroup(alpha);"
                };
                chunks.Add(chunk);
            }

            {
                var chunk = new FunctionChunk
                {
                    State = TChunkState.NewStatic,
                    Comments = "结束绘制 隐藏显示分组视图",
                    Name = "EndGroupFade",
                    ReturnType = "void",
                    Content = "EditorGUILayout.EndFadeGroup();"
                };
                chunks.Add(chunk);
            }


            return chunks;
        }
    }
}
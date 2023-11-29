#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0109 // 

using System.Collections.Generic;

namespace AIO
{
    public partial class GULayoutSingleton
    {
        [FuncParam(Group = "Scope Horizontal", IsArray = true)]
        private static List<FunctionChunk> Horizontal()
        {
            var chunks = new List<FunctionChunk>();
            var value = new FunctionParam("Action", "action", "") { Comments = "回调函数" };
            var width_float = new FunctionParam("float", "width", "GUILayout.Width(width)") { Comments = "宽度" };
            var height_float = new FunctionParam("float", "height", "GUILayout.Height(height)") { Comments = "高度" };
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
                    State = TChunkState.Static,
                    Comments = "绘制 横排视图",
                    Name = "VHorizontal",
                    Params = param,
                    ReturnType = "void",
                };
                chunk.ContentBuilder.AppendLine($"using (new GUILayout.HorizontalScope({chunk.GetParamValues()})");
                chunk.ContentBuilder.AppendLine("{");
                chunk.ContentBuilder.AppendLine("    action?.Invoke();");
                chunk.ContentBuilder.AppendLine("}");
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
                    State = TChunkState.Static,
                    Comments = "绘制 横排视图",
                    Name = "BeginHorizontal",
                    Params = param,
                    ReturnType = "void",
                };
                chunk.Content = $"GUILayout.BeginHorizontal({chunk.GetParamValues()});";
                chunks.Add(chunk);
            }

            {
                var chunk = new FunctionChunk
                {
                    State = TChunkState.Static,
                    Comments = "绘制 横排视图",
                    Name = "EndHorizontal",
                    ReturnType = "void",
                    Content = "GUILayout.EndHorizontal();"
                };
                chunks.Add(chunk);
            }
            return chunks;
        }

        [FuncParam(Group = "Scope Vertical", IsArray = true)]
        private static List<FunctionChunk> Vertical()
        {
            var chunks = new List<FunctionChunk>();
            var width_float = new FunctionParam("float", "width", "GUILayout.Width(width)") { Comments = "宽度" };
            var height_float = new FunctionParam("float", "height", "GUILayout.Height(height)") { Comments = "高度" };
            var value = new FunctionParam("Action", "action", "") { Comments = "回调函数" };
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
                    State = TChunkState.Static,
                    Comments = "绘制 竖排视图",
                    Name = "Vertical",
                    Params = param,
                    ReturnType = "void",
                };
                chunk.ContentBuilder.AppendLine("if (action == null) return;");
                chunk.ContentBuilder.AppendLine($"using (new GUILayout.VerticalScope({chunk.GetParamValues()}))");
                chunk.ContentBuilder.AppendLine("{");
                chunk.ContentBuilder.AppendLine("    action?.Invoke();");
                chunk.ContentBuilder.AppendLine("}");
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
                    State = TChunkState.Static,
                    Comments = "绘制 竖排视图",
                    Name = "BeginVertical",
                    Params = param,
                    ReturnType = "void",
                };
                chunk.Content = $"GUILayout.BeginVertical({chunk.GetParamValues()});";
                chunks.Add(chunk);
            }

            {
                var chunk = new FunctionChunk
                {
                    State = TChunkState.Static,
                    Comments = "绘制 竖排视图",
                    Name = "EndVertical",
                    ReturnType = "void",
                    Content = "GUILayout.EndVertical();"
                };
                chunks.Add(chunk);
            }
            return chunks;
        }

        [FuncParam(Group = "Scope ScrollView", IsArray = true)]
        private static List<FunctionChunk> ScrollView()
        {
            var chunks = new List<FunctionChunk>();
            var width_float = new FunctionParam("float", "width", "GUILayout.Width(width)") { Comments = "宽度" };
            var height_float = new FunctionParam("float", "height", "GUILayout.Height(height)") { Comments = "高度" };

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
                    State = TChunkState.Static,
                    Comments = "绘制 滚动视图",
                    Name = "VScrollView",
                    Params = param,
                    ReturnType = "Vector2",
                };
                chunk.ContentBuilder.AppendLine("if (action == null) return v2;");
                chunk.ContentBuilder.AppendLine($"v2 = GUILayout.BeginScrollView({chunk.GetParamValues()});");
                chunk.ContentBuilder.AppendLine("action();");
                chunk.ContentBuilder.AppendLine("GUILayout.EndScrollView();");
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
                    State = TChunkState.Static,
                    Comments = "绘制 滚动视图",
                    Name = "BeginScrollView",
                    Params = param,
                    ReturnType = "Vector2",
                };
                chunk.Content = $"return GUILayout.BeginScrollView({chunk.GetParamValues()});";
                chunks.Add(chunk);
            }

            {
                var chunk = new FunctionChunk
                {
                    State = TChunkState.Static,
                    Comments = "绘制 滚动视图",
                    Name = "EndScrollView",
                    ReturnType = "void",
                    Content = "GUILayout.EndScrollView();"
                };
                chunks.Add(chunk);
            }


            return chunks;
        }

        [FuncParam(Group = "Scope Scroll", IsArray = true)]
        private static List<FunctionChunk> Scroll()
        {
            var chunks = new List<FunctionChunk>();
            var width_float = new FunctionParam("float", "width", "GUILayout.Width(width)") { Comments = "宽度" };
            var height_float = new FunctionParam("float", "height", "GUILayout.Height(height)") { Comments = "高度" };

            var value = new FunctionParam("Action", "action", "") { Comments = "回调函数" };
            var v2 = new FunctionParam("Vector2", "v2", "v2") { Comments = "视图在X和Y方向上滚动的像素距离" };

            var viewRect = new FunctionParam("Rect", "viewRect") { Comments = "在滚动视图内部使用的矩形" };
            var rect = new FunctionParam("Rect", "rect") { Comments = "屏幕上用于滚动视图的矩形" };

            var styles_h = new FunctionParam("GUIStyle", "styles_h") { Comments = "水平滚动条风格" };
            var styles_v = new FunctionParam("GUIStyle", "styles_v") { Comments = "垂直滚动条风格" };

            var alwaysShowHorizontal = new FunctionParam("bool", "alwaysShowHorizontal") { Comments = "始终显示水平滚动条" };
            var alwaysShowVertical = new FunctionParam("bool", "alwaysShowVertical") { Comments = "始终显示垂直滚动条" };

            var paramsList = new List<FunctionParam[]>()
            {
                new FunctionParam[] { value, rect, v2, viewRect, },
                new FunctionParam[] { value, rect, v2, viewRect, alwaysShowHorizontal, alwaysShowVertical },
                new FunctionParam[] { value, rect, v2, viewRect, styles_h, styles_v },
                new FunctionParam[]
                    { value, rect, v2, viewRect, alwaysShowHorizontal, alwaysShowVertical, styles_h, styles_v },
            };
            foreach (var param in paramsList)
            {
                var chunk = new FunctionChunk
                {
                    State = TChunkState.Static,
                    Comments = "绘制 滚动视图",
                    Name = "VScroll",
                    Params = param,
                    ReturnType = "Vector2",
                };
                chunk.ContentBuilder.AppendLine("if (action == null) return v2;");
                chunk.ContentBuilder.AppendLine($"v2 = GUI.BeginScrollView({chunk.GetParamValues()});");
                chunk.ContentBuilder.AppendLine("action();");
                chunk.ContentBuilder.AppendLine("GUI.EndScrollView();");
                chunk.ContentBuilder.AppendLine("return v2;");
                chunks.Add(chunk);
            }

            paramsList = new List<FunctionParam[]>()
            {
                new FunctionParam[] { rect, v2, viewRect, },
                new FunctionParam[] { rect, v2, viewRect, alwaysShowHorizontal, alwaysShowVertical },
                new FunctionParam[] { rect, v2, viewRect, styles_h, styles_v },
                new FunctionParam[]
                    { rect, v2, viewRect, alwaysShowHorizontal, alwaysShowVertical, styles_h, styles_v },
            };
            foreach (var param in paramsList)
            {
                var chunk = new FunctionChunk
                {
                    State = TChunkState.Static,
                    Comments = "绘制 滚动视图",
                    Name = "BeginScroll",
                    Params = param,
                    ReturnType = "Vector2",
                };
                chunk.Content = $"return GUI.BeginScrollView({chunk.GetParamValues()});";
                chunks.Add(chunk);
            }

            {
                var chunk = new FunctionChunk
                {
                    State = TChunkState.Static,
                    Comments = "绘制 滚动视图",
                    Name = "EndScroll",
                    ReturnType = "void",
                    Content = "GUI.EndScrollView();"
                };
                chunks.Add(chunk);
            }


            return chunks;
        }

        [FuncParam(Group = "Scope Area", IsArray = true)]
        private static List<FunctionChunk> Area()
        {
            var chunks = new List<FunctionChunk>();
            var value = new FunctionParam("Action", "action", "") { Comments = "回调函数" };
            var rect = new FunctionParam("Rect", "rect", "rect") { Comments = "矩形" };


            foreach (var item in new string[] { "string", "GUIContent", "Texture" })
            {
                var label = new FunctionParam(item, "label", "label") { Comments = "标题" };
                var paramsList = new List<FunctionParam[]>()
                {
                    new FunctionParam[] { value, rect, },
                    new FunctionParam[] { value, rect, label, },
                    new FunctionParam[] { value, rect, label, Style },
                    new FunctionParam[] { value, rect, Style },
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State = TChunkState.Static,
                        Comments = "绘制 区域视图",
                        Name = "VArea",
                        Params = param,
                        ReturnType = "void",
                    };
                    chunk.ContentBuilder.AppendLine("if (action == null) return;");
                    chunk.ContentBuilder.AppendLine($"GUILayout.BeginArea({chunk.GetParamValues()});");
                    chunk.ContentBuilder.AppendLine("action();");
                    chunk.ContentBuilder.AppendLine("GUILayout.EndArea();");
                    chunks.Add(chunk);
                }

                paramsList = new List<FunctionParam[]>()
                {
                    new FunctionParam[] { rect, },
                    new FunctionParam[] { rect, label, },
                    new FunctionParam[] { rect, label, Style },
                    new FunctionParam[] { rect, Style },
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State = TChunkState.Static,
                        Comments = "绘制 区域视图",
                        Name = "BeginArea",
                        Params = param,
                        ReturnType = "void",
                    };
                    chunk.Content = $"GUILayout.BeginArea({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }

                {
                    var chunk = new FunctionChunk
                    {
                        State = TChunkState.Static,
                        Comments = "绘制 区域视图",
                        Name = "EndArea",
                        ReturnType = "void",
                        Content = "GUILayout.EndArea();"
                    };
                    chunks.Add(chunk);
                }
            }

            return chunks;
        }

        [FuncParam(Group = "Scope Clip", IsArray = true)]
        private static List<FunctionChunk> Clip()
        {
            var chunks = new List<FunctionChunk>();
            var rect = new FunctionParam("Rect", "rect", "rect") { Comments = "矩形" };
            var scrollOffset = new FunctionParam("Vector2", "scrollOffset", "scrollOffset")
            {
                Comments = "滚动区域补偿"
            };
            var renderOffset = new FunctionParam("Vector2", "renderOffset", "renderOffset")
            {
                Comments = "显示区域补偿"
            };
            var rectOffset = new FunctionParam("Rect", "rectOffset", "rectOffset.position, rectOffset.size")
            {
                Comments = "区域补偿"
            };

            var value = new FunctionParam("Action", "action", "") { Comments = "回调函数" };
            var resetOffset = new FunctionParam("bool", "resetOffset") { Comments = "重置补偿" };
            var paramsList = new List<FunctionParam[]>()
            {
                new FunctionParam[] { value, rect, },
                new FunctionParam[] { value, rect, scrollOffset, renderOffset, resetOffset, },
                new FunctionParam[] { value, rect, rectOffset, resetOffset, },
            };

            foreach (var param in paramsList)
            {
                var chunk = new FunctionChunk
                {
                    State = TChunkState.Static,
                    Comments = "绘制 裁剪视图",
                    Name = "VClip",
                    Params = param,
                    ReturnType = "void",
                };
                chunk.ContentBuilder.AppendLine("if (action == null) return;");
                chunk.ContentBuilder.AppendLine($"GUI.BeginClip({chunk.GetParamValues()});");
                chunk.ContentBuilder.AppendLine("action();");
                chunk.ContentBuilder.AppendLine("GUI.EndClip();");
                chunks.Add(chunk);
            }

            paramsList = new List<FunctionParam[]>()
            {
                new FunctionParam[] { rect, },
                new FunctionParam[] { rect, scrollOffset, renderOffset, resetOffset, },
                new FunctionParam[] { rect, rectOffset, resetOffset, },
            };
            foreach (var param in paramsList)
            {
                var chunk = new FunctionChunk
                {
                    State = TChunkState.Static,
                    Comments = "开始绘制 裁剪视图",
                    Name = "BeginClip",
                    Params = param,
                    ReturnType = "void",
                };
                chunk.Content = $"GUI.BeginClip({chunk.GetParamValues()});";
                chunks.Add(chunk);
            }

            {
                var chunk = new FunctionChunk
                {
                    State = TChunkState.Static,
                    Comments = "结束绘制 裁剪视图",
                    Name = "EndClip",
                    ReturnType = "void",
                    Content = "GUI.EndClip();"
                };
                chunks.Add(chunk);
            }
            return chunks;
        }

        [FuncParam(Group = "Scope Group", IsArray = true)]
        private static List<FunctionChunk> Group()
        {
            var chunks = new List<FunctionChunk>();
            var value = new FunctionParam("Action", "action", "") { Comments = "回调函数" };
            var rect = new FunctionParam("Rect", "rect", "rect") { Comments = "矩形" };

            foreach (var item in new string[] { "string", "GUIContent", "Texture" })
            {
                var label = new FunctionParam(item, "label", "label") { Comments = "标题" };
                var paramsList = new List<FunctionParam[]>()
                {
                    new FunctionParam[] { value, rect, },
                    new FunctionParam[] { value, rect, label, },
                    new FunctionParam[] { value, rect, label, Style },
                    new FunctionParam[] { value, rect, Style },
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State = TChunkState.Static,
                        Comments = "绘制 组视图",
                        Name = "VGroup",
                        Params = param,
                        ReturnType = "void",
                    };
                    chunk.ContentBuilder.AppendLine("if (action == null) return;");
                    chunk.ContentBuilder.AppendLine($"GUI.BeginGroup({chunk.GetParamValues()});");
                    chunk.ContentBuilder.AppendLine("action();");
                    chunk.ContentBuilder.AppendLine("GUI.EndGroup();");
                    chunks.Add(chunk);
                }

                paramsList = new List<FunctionParam[]>()
                {
                    new FunctionParam[] { rect, },
                    new FunctionParam[] { rect, label, },
                    new FunctionParam[] { rect, label, Style },
                    new FunctionParam[] { rect, Style },
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State = TChunkState.Static,
                        Comments = "绘制 组视图",
                        Name = "BeginGroup",
                        Params = param,
                        ReturnType = "void",
                    };
                    chunk.Content = $"GUI.BeginGroup({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }

                {
                    var chunk = new FunctionChunk
                    {
                        State = TChunkState.Static,
                        Comments = "绘制 组视图",
                        Name = "EndGroup",
                        ReturnType = "void",
                        Content = "GUI.EndGroup();"
                    };
                    chunks.Add(chunk);
                }
            }

            return chunks;
        }
    }
}
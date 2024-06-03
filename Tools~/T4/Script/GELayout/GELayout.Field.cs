#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0109 // 

#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    public partial class GELayoutSingleton
    {
        [FuncParam(Group = "Struct", IsArray = true)]
        private static IEnumerable<FunctionChunk> GetFieldStruct()
        {
            var chunks = new List<FunctionChunk>();
            foreach (var type in new[]
            {
                "Bounds",
                "BoundsInt",
                "RectInt",
                "Rect"
            })
            {
                var value = new FunctionParam(type, "value", "value") { Comments = "值" };
                foreach (var item in new[] { "string", "GUIContent", "Texture" })
                {
                    var label = new FunctionParam(item, "label", "label") { Comments = "标签" };
                    if (item == "Texture") label.Output = "new GUIContent(label)";
                    var paramsList = new[]
                    {
                        new[] { value, Options },
                        new[] { label, value, Options }
                    };
                    foreach (var param in paramsList)
                    {
                        var chunk = new FunctionChunk
                        {
                            State      = TChunkState.NewStatic,
                            Comments   = $"绘制 {type}",
                            Name       = "Field",
                            Params     = param,
                            ReturnType = type
                        };
                        var api = $"EditorGUILayout.{char.ToUpper(type[0])}{type.Substring(1, type.Length - 1)}Field";
                        chunk.Content = $"return {api}({chunk.GetParamValues()});";
                        chunks.Add(chunk);
                    }
                }
            }

            foreach (var type in new[]
            {
                "Vector2Int",
                "Vector3Int",
                "Vector4", "Vector3",
                "Vector2"
            })
            {
                var value = new FunctionParam(type, "value", "value") { Comments = "值" };
                foreach (var item in new[] { "string", "GUIContent", "Texture" })
                {
                    var label = new FunctionParam(item, "label", "label") { Comments = "标签" };
                    if (item == "Texture") label.Output = "new GUIContent(label)";
                    var paramsList = new[]
                    {
                        new[] { label, value, Options }
                    };
                    foreach (var param in paramsList)
                    {
                        var chunk = new FunctionChunk
                        {
                            State      = TChunkState.NewStatic,
                            Comments   = $"绘制 {type}",
                            Name       = "Field",
                            Params     = param,
                            ReturnType = type
                        };
                        var api = $"EditorGUILayout.{char.ToUpper(type[0])}{type.Substring(1, type.Length - 1)}Field";
                        chunk.Content = $"return {api}({chunk.GetParamValues()});";
                        chunks.Add(chunk);
                    }
                }
            }

            return chunks;
        }

        [FuncParam(Group = "Delayed", IsArray = true)]
        private static IEnumerable<FunctionChunk> GetFieldDelayed()
        {
            var chunks = new List<FunctionChunk>();
            foreach (var type in new[]
            {
                "float",
                "int",
                "double",
                "text"
            })
            {
                var value = new FunctionParam(type == "text" ? "string" : type, "value", "value") { Comments = "值" };
                foreach (var item in new[] { "string", "GUIContent", "Texture" })
                {
                    var label = new FunctionParam(item, "label", "label")
                    {
                        Comments = "标签"
                    };
                    if (item == "Texture") label.Output = "new GUIContent(label)";
                    var paramsList = new[]
                    {
                        new[] { value, Options },
                        new[] { label, value, Options },
                        new[] { value, Style, Options },
                        new[] { label, value, Style, Options }
                    };
                    foreach (var param in paramsList)
                    {
                        var chunk = new FunctionChunk
                        {
                            State      = TChunkState.NewStatic,
                            Comments   = $"绘制 {value.Type}",
                            Name       = "FieldDelayed",
                            Params     = param,
                            ReturnType = value.Type
                        };
                        var api =
                            $"EditorGUILayout.Delayed{char.ToUpper(type[0])}{type.Substring(1, type.Length - 1)}Field";
                        chunk.Content = $"return {api}({chunk.GetParamValues()});";
                        chunks.Add(chunk);
                    }
                }
            }

            return chunks;
        }

        [FuncParam(Group = "Number", IsArray = true)]
        private static IEnumerable<FunctionChunk> GetFieldNumber()
        {
            var chunks = new List<FunctionChunk>();
            foreach (var type in new[]
            {
                "float",
                "int",
                "double",
                "long"
            })
            {
                var value = new FunctionParam(type, "value", "value") { Comments = "值" };
                foreach (var item in new[] { "string", "GUIContent", "Texture" })
                {
                    var label = new FunctionParam(item, "label", "label") { Comments = "标签" };
                    if (item == "Texture") label.Output = "new GUIContent(label)";
                    var paramsList = new[]
                    {
                        new[] { value, Options },
                        new[] { value, Style, Options },
                        new[] { label, value, Options },
                        new[] { label, value, Style, Options }
                    };
                    foreach (var param in paramsList)
                    {
                        var chunk = new FunctionChunk
                        {
                            State      = TChunkState.NewStatic,
                            Comments   = $"绘制 {value.Type}",
                            Name       = "Field",
                            Params     = param,
                            ReturnType = value.Type
                        };
                        var api =
                            $"EditorGUILayout.{char.ToUpper(type[0])}{type.Substring(1, type.Length - 1)}Field";
                        chunk.Content = $"return {api}({chunk.GetParamValues()});";
                        chunks.Add(chunk);
                    }
                }
            }

            return chunks;
        }

        [FuncParam(Group = "AnimationCurve", IsArray = true)]
        private static IEnumerable<FunctionChunk> GetAnimationCurve()
        {
            var chunks = new List<FunctionChunk>();
            var value = new FunctionParam("AnimationCurve", "value", "value") { Comments = "值" };
            var color = new FunctionParam("Color", "color", "color") { Comments          = "颜色" };
            var ranges = new FunctionParam("Rect", "ranges", "ranges") { Comments        = "区间值" };
            foreach (var item in new[] { "string", "GUIContent", "Texture" })
            {
                var label = new FunctionParam(item, "label", "label") { Comments = "标签" };
                if (item == "Texture") label.Output = "new GUIContent(label)";
                var paramsList = new[]
                {
                    new[] { value, Options },
                    new[] { label, value, Options },
                    new[] { value, color, ranges, Options },
                    new[] { label, value, color, ranges, Options }
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State      = TChunkState.NewStatic,
                        Comments   = $"绘制 {value.Type}",
                        Name       = "Field",
                        Params     = param,
                        ReturnType = value.Type
                    };
                    chunk.Content = $"return EditorGUILayout.CurveField({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }
            }

            return chunks;
        }

        [FuncParam(Group = "Color", IsArray = true)]
        private static IEnumerable<FunctionChunk> GetColor()
        {
            var chunks = new List<FunctionChunk>();
            var value = new FunctionParam("Color", "value", "value") { Comments                           = "值" };
            var showEyedropper = new FunctionParam("bool", "showEyedropper", "showEyedropper") { Comments = "颜色" };
            var showAlpha = new FunctionParam("bool", "showAlpha", "showAlpha") { Comments                = "区间值" };
            var hdr = new FunctionParam("bool", "hdr", "hdr") { Comments                                  = "区间值" };
            foreach (var item in new[] { "string", "GUIContent", "Texture" })
            {
                var label = new FunctionParam(item, "label", "label") { Comments = "标签" };
                if (item != "GUIContent") label.Output = "new GUIContent(label)";
                var paramsList = new[]
                {
                    new[] { value, Options },
                    new[] { label, value, Options },
                    new[] { label, value, showEyedropper, showAlpha, hdr, Options }
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State      = TChunkState.NewStatic,
                        Comments   = $"绘制 {value.Type}",
                        Name       = "Field",
                        Params     = param,
                        ReturnType = value.Type
                    };
                    chunk.Content = $"return EditorGUILayout.ColorField({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }
            }

            return chunks;
        }

        [FuncParam(Group = "Gradient", IsArray = true)]
        private static IEnumerable<FunctionChunk> GetGradient()
        {
            var chunks = new List<FunctionChunk>();
            var value = new FunctionParam("Gradient", "value", "value") { Comments = "值" };
            foreach (var item in new[] { "string", "GUIContent", "Texture" })
            {
                var label = new FunctionParam(item, "label", "label") { Comments = "标签" };
                if (item != "GUIContent") label.Output = "new GUIContent(label)";
                var paramsList = new[]
                {
                    new[] { value, Options },
                    new[] { label, value, Options }
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State           = TChunkState.NewStatic,
                        Comments        = $"绘制 {value.Type}",
                        Name            = "Field",
                        Params          = param,
                        MacroDefinition = "UNITY_2019_1_OR_NEWER",
                        ReturnType      = value.Type
                    };
                    chunk.Content = $"return EditorGUILayout.GradientField({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }
            }

            return chunks;
        }

        [FuncParam(Group = "Text", IsArray = true)]
        private static IEnumerable<FunctionChunk> GetText()
        {
            var chunks = new List<FunctionChunk>();
            var value = new FunctionParam("string", "value", "value") { Comments = "值" };
            foreach (var item in new[] { "string", "GUIContent", "Texture" })
            {
                var label = new FunctionParam(item, "label", "label") { Comments = "标签" };
                if (item == "Texture") label.Output = "new GUIContent(label)";
                var paramsList = new[]
                {
                    new[] { value, Options },
                    new[] { label, value, Options },
                    new[] { value, Style, Options },
                    new[] { label, value, Style, Options }
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State      = TChunkState.NewStatic,
                        Comments   = $"绘制 {value.Type}",
                        Name       = "Field",
                        Params     = param,
                        ReturnType = value.Type
                    };
                    chunk.Content = $"return EditorGUILayout.TextField({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }
            }

            return chunks;
        }

        [FuncParam(Group = "Object", IsArray = true)]
        private static IEnumerable<FunctionChunk> GetObject()
        {
            var chunks = new List<FunctionChunk>();
            var value = new FunctionParam("T", "value", "value") { Comments = "值" };
            var type = new FunctionParam("Type", "type", "type") { Comments = "类型" };
            var allowSceneObjects = new FunctionParam("bool", "allowSceneObjects", "allowSceneObjects")
            {
                Comments = "是否允许选择场景物体"
            };
            var allowSceneObjects1 = new FunctionParam(allowSceneObjects) { Output = "typeof(T), allowSceneObjects" };
            foreach (var item in new[] { "string", "GUIContent", "Texture" })
            {
                var label = new FunctionParam(item, "label", "label") { Comments = "标签" };
                if (item == "Texture") label.Output = "new GUIContent(label)";
                var paramsList = new[]
                {
                    new[] { value, allowSceneObjects1, Options },
                    new[] { value, type, allowSceneObjects, Options },
                    new[] { label, value, type, allowSceneObjects, Options },
                    new[] { label, value, allowSceneObjects1, Options }
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State      = TChunkState.NewStatic,
                        Comments   = "绘制 Object",
                        Name       = "Field",
                        Generics   = new Dictionary<string, string> { { "T", "UnityEngine.Object" } },
                        Params     = param,
                        ReturnType = value.Type
                    };
                    chunk.Content = $"return (T)EditorGUILayout.ObjectField({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }
            }

            chunks.Add(new FunctionChunk
            {
                State      = TChunkState.NewStatic,
                Comments   = "绘制 Object",
                Name       = "Field",
                Generics   = new Dictionary<string, string> { { "T", "UnityEngine.Object" } },
                Params     = new[] { value, type, Options },
                Content    = "return (T)EditorGUILayout.ObjectField(value, type, true, options);",
                ReturnType = value.Type
            });
            chunks.Add(new FunctionChunk
            {
                State      = TChunkState.NewStatic,
                Comments   = "绘制 Object",
                Name       = "Field",
                Generics   = new Dictionary<string, string> { { "T", "UnityEngine.Object" } },
                Params     = new[] { value, Options },
                Content    = "return (T)EditorGUILayout.ObjectField(value, typeof(T), true, options);",
                ReturnType = value.Type
            });
            return chunks;
        }

        [FuncParam(Group = "Layer", IsArray = true)]
        private static IEnumerable<FunctionChunk> GetLayer()
        {
            var chunks = new List<FunctionChunk>();
            var value = new FunctionParam("int", "value", "value") { Comments = "值" };
            foreach (var item in new[] { "string", "GUIContent", "Texture" })
            {
                var label = new FunctionParam(item, "label", "label") { Comments = "标签" };
                if (item == "Texture") label.Output = "new GUIContent(label)";
                var paramsList = new[]
                {
                    new[] { value, Options },
                    new[] { label, value, Options },
                    new[] { value, Style, Options },
                    new[] { label, value, Style, Options }
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State      = TChunkState.NewStatic,
                        Comments   = "绘制 Layer",
                        Name       = "Layer",
                        Params     = param,
                        ReturnType = value.Type
                    };
                    chunk.Content = $"return EditorGUILayout.LayerField({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }
            }

            return chunks;
        }

        [FuncParam(Group = "Password", IsArray = true)]
        private static IEnumerable<FunctionChunk> GetPassword()
        {
            var chunks = new List<FunctionChunk>();
            var value = new FunctionParam("string", "value", "value") { Comments = "值" };
            foreach (var item in new[] { "string", "GUIContent", "Texture" })
            {
                var label = new FunctionParam(item, "label", "label") { Comments = "标签" };
                if (item == "Texture") label.Output = "new GUIContent(label)";
                var paramsList = new[]
                {
                    new[] { value, Options },
                    new[] { label, value, Options },
                    new[] { value, Style, Options },
                    new[] { label, value, Style, Options }
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State      = TChunkState.NewStatic,
                        Comments   = "绘制 密码文本框",
                        Name       = "Password",
                        Params     = param,
                        ReturnType = value.Type
                    };
                    chunk.Content = $"return EditorGUILayout.PasswordField({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }
            }

            return chunks;
        }
    }
}
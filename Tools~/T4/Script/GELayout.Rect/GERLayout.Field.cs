#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0109 // 

using System.Collections.Generic;

namespace AIO
{
    public partial class GERLayoutSingleton
    {
        private static List<FunctionChunk> Field_Witch_No_Label(bool hasLabel, params string[] types)
        {
            var chunks = new List<FunctionChunk>();
            var rect = new FunctionParam("Rect", "rect", "rect") { Comments = "矩形" };
            var pos = new FunctionParam("Vector2", "pos", "") { Comments = "位置" };
            var size = new FunctionParam("Vector2", "size", "new Rect(pos, size)") { Comments = "大小" };
            var label = new FunctionParam("GTContent", "label", "label") { Comments = "标题" };
            foreach (var type in types)
            {
                var value = new FunctionParam(type, "value", "value") { Comments = "值" };
                var paramList = new List<FunctionParam[]>();
                if (hasLabel) paramList.Add(new FunctionParam[] { rect, value });
                paramList.Add(new FunctionParam[] { rect, label, value });
                if (hasLabel) paramList.Add(new FunctionParam[] { pos, size, value });
                paramList.Add(new FunctionParam[] { pos, size, label, value });
                foreach (var param in paramList)
                {
                    var chunk = new FunctionChunk
                    {
                        State = TChunkState.Static,
                        Comments = $"创建区域 绘制 {type} 字段",
                        Name = "Field",
                        Params = param,
                        ReturnType = type,
                        Attributes = new List<string> { "ExcludeFromDocs" },
                    };
                    var api = $"{char.ToUpper(type[0])}{type.Substring(1, type.Length - 1)}";
                    if (api == "AnimationCurve") api = "Curve";
                    if (api.ToLower() == "string") api = "Text";
                    chunk.Content = $"return EditorGUI.{api}Field({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }
            }

            return chunks;
        }

        [FuncParam(Group = "Vector", IsArray = true)]
        private static List<FunctionChunk> VectorField()
        {
            return Field_Witch_No_Label(false, "Vector2", "Vector2Int", "Vector3", "Vector3Int", "Vector4");
        }

        [FuncParam(Group = "Color", IsArray = true)]
        private static List<FunctionChunk> ColorField()
        {
            return Field_Witch_No_Label(true, "Color");
        }

        [FuncParam(Group = "Bounds", IsArray = true)]
        private static List<FunctionChunk> BoundsField()
        {
            return Field_Witch_No_Label(true, "Bounds", "BoundsInt");
        }

        [FuncParam(Group = "Rect", IsArray = true)]
        private static List<FunctionChunk> RectField()
        {
            return Field_Witch_No_Label(true, "Rect", "RectInt");
        }

        [FuncParam(Group = "AnimationCurve", IsArray = true)]
        private static List<FunctionChunk> AnimationCurveField()
        {
            return Field_Witch_No_Label(true, "AnimationCurve");
        }

        [FuncParam(Group = "Number", IsArray = true)]
        private static List<FunctionChunk> NumberField()
        {
            return Field_Witch_No_Label(true, "double", "float", "int", "long");
        }

        [FuncParam(Group = "Gradient", IsArray = true)]
        private static List<FunctionChunk> GradientField()
        {
            return Field_Witch_No_Label(true, "Gradient");
        }

        [FuncParam(Group = "Text", IsArray = true)]
        private static List<FunctionChunk> TextField()
        {
            return Field_Witch_No_Label(true, "string");
        }
    }
}
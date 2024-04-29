#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0109 // 

#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    public partial class GELayoutSingleton
    {
        [FuncParam(Group = "Selection Grid", IsArray = true)]
        private static List<FunctionChunk> SelectionGrid()
        {
            var chunks = new List<FunctionChunk>();
            var selected = new FunctionParam("int", "selected", "selected")
                { Comments = "The index of the selected button." };

            var xCount = new FunctionParam("int", "xCount", "xCount")
            {
                Comments =
                    "How many elements to fit in the horizontal direction. The elements will be scaled to fit unless the style defines a fixedWidth to use. The height of the control will be determined from the number of elements."
            };

            foreach (var label in new List<FunctionParam>
            {
                new FunctionParam("GUIContent[]", "content", "content"),
                new FunctionParam("IEnumerable<GUIContent>", "content", "GTContent.Temp(content)"),

                new FunctionParam("string[]", "content", "content"),
                new FunctionParam("ICollection<string>", "content", "GTContent.Temp(content)"),

                new FunctionParam("Texture[]", "content", "GTContent.Temp(content)"),
                new FunctionParam("ICollection<Texture>", "content", "GTContent.Temp(content)")
            })
            {
                label.Comments = "An array of text, image and tooltips for the button.";
                var paramsList = new List<FunctionParam[]>
                {
                    new[] { selected, label, xCount, Options },
                    new[] { selected, label, xCount, Style, Options }
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State      = TChunkState.Static,
                        Comments   = "Make a Selection Grid",
                        Name       = "Selection",
                        Params     = param,
                        ReturnType = "int"
                    };
                    chunk.Content = $"return GUILayout.SelectionGrid({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }
            }


            return chunks;
        }
    }
}
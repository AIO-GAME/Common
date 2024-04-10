#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0109 // 

#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    public partial class GULayoutSingleton
    {
        [FuncParam(Group = "Password", IsArray = true)]
        private static List<FunctionChunk> PasswordField()
        {
            var chunks = new List<FunctionChunk>();
            var value = new FunctionParam("string", "password", "password") { Comments    = "文本内容" };
            var mask = new FunctionParam("char", "mask", "mask") { Comments               = "屏蔽密码的字符" };
            var maxLength = new FunctionParam("int", "maxLength", "maxLength") { Comments = "输入字符串最大长度" };
            var rect = new FunctionParam("Rect", "rect", "rect") { Comments               = "绘制区域" };
            var paramsList = new List<FunctionParam[]>
            {
                new[] { value, mask, Options },
                new[] { value, mask, Style, Options },
                new[] { value, mask, maxLength, Options },
                new[] { value, mask, maxLength, Style, Options }
            };
            foreach (var param in paramsList)
            {
                var chunk = new FunctionChunk
                {
                    State      = TChunkState.Static,
                    Comments   = "绘制 密码框",
                    Name       = "Password",
                    Params     = param,
                    ReturnType = "string"
                };
                chunk.Content = $"return GUILayout.PasswordField({chunk.GetParamValues()});";
                chunks.Add(chunk);
            }

            paramsList = new List<FunctionParam[]>
            {
                new[] { rect, value, mask },
                new[] { rect, value, mask, Style },
                new[] { rect, value, mask, maxLength },
                new[] { rect, value, mask, maxLength, Style }
            };
            foreach (var param in paramsList)
            {
                var chunk = new FunctionChunk
                {
                    State      = TChunkState.Static,
                    Comments   = "绘制 密码框",
                    Name       = "Password",
                    Params     = param,
                    ReturnType = "string"
                };
                chunk.Content = $"return GUI.PasswordField({chunk.GetParamValues()});";
                chunks.Add(chunk);
            }

            return chunks;
        }
    }
}
﻿#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0109 // 

#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    public partial class GELayoutSingleton
    {
        [FuncParam(Group = "List", IsArray = true)]
        private static IEnumerable<FunctionChunk> GetFieldList()
        {
            var chunks = new List<FunctionChunk>();
            var array = new FunctionParam("IList<T>", "array", "array") { Comments                     = "列表数据" };
            var label = new FunctionParam("string", "label", "label") { Comments                       = "标签" };
            var foldout = new FunctionParam("bool", "foldout", "foldout") { Comments                   = "是否展开列表" };
            var tips = new FunctionParam("Action", "tips", "tips") { Comments                          = "提示信息回调函数" };
            var tips_null = new FunctionParam("", "", "null") { Comments                               = "提示信息回调函数" };
            var addFunc = new FunctionParam("Func<T>", "addFunc", "addFunc") { Comments                = "添加回调函数" };
            var addFunc_null = new FunctionParam("", "", "() => default") { Comments                   = "添加回调函数" };
            var lbStyle = new FunctionParam("GUIStyle", "labelStyle", "labelStyle") { Comments         = "标签显示风格" };
            var lbStyle_null = new FunctionParam("", "", "new GUIStyle(\"CenteredLabel\")") { Comments = "标签显示风格" };
            var bgStyle = new FunctionParam("GUIStyle", "bgStyle", "bgStyle") { Comments               = "背景显示风格" };
            var bgStyle_null = new FunctionParam("", "", "null") { Comments                            = "背景显示风格" };
            foreach (var showFunc in new[]
            {
                new FunctionParam { Type = "Action<T>", Output = "index => showFunc.Invoke(array[index])" },
                new FunctionParam
                    { Type = "Action<int, T>", Output = "index => showFunc.Invoke(index, array[index])" },
                new FunctionParam
                    { Type = "Func<T, T>", Output = "index => array[index] = showFunc.Invoke(array[index])" },
                new FunctionParam
                {
                    Type   = "Func<int, T, T>",
                    Output = "index => array[index] = showFunc.Invoke(index, array[index])"
                }
            })
            {
                showFunc.Name     = "showFunc";
                showFunc.Comments = "显示回调函数";
                var paramsList = new[]
                {
                    new[] { label, array, tips, showFunc, addFunc, lbStyle, bgStyle },
                    new[] { label, array, tips, showFunc, addFunc, lbStyle, bgStyle_null },
                    new[] { label, array, tips, showFunc, addFunc, lbStyle_null, bgStyle_null },
                    new[] { label, array, tips, showFunc, addFunc_null, lbStyle_null, bgStyle_null },

                    new[] { label, array, tips_null, showFunc, addFunc, lbStyle, bgStyle },
                    new[] { label, array, tips_null, showFunc, addFunc, lbStyle, bgStyle_null },
                    new[] { label, array, tips_null, showFunc, addFunc, lbStyle_null, bgStyle_null },
                    new[]
                        { label, array, tips_null, showFunc, addFunc_null, lbStyle_null, bgStyle_null }
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State    = TChunkState.Static,
                        Comments = "绘制 List 列表",
                        Generics = new Dictionary<string, string> { { "T", "" } },
                        Name     = "List",
                        Params   = param
                    };
                    chunk.Content = $"FieldList({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }

                paramsList = new[]
                {
                    new[] { label, array, foldout, tips, showFunc, addFunc, lbStyle, bgStyle },
                    new[] { label, array, foldout, tips, showFunc, addFunc, lbStyle, bgStyle_null },
                    new[] { label, array, foldout, tips, showFunc, addFunc, lbStyle_null, bgStyle_null },
                    new[]
                        { label, array, foldout, tips, showFunc, addFunc_null, lbStyle_null, bgStyle_null },

                    new[] { label, array, foldout, tips_null, showFunc, addFunc, lbStyle, bgStyle },
                    new[] { label, array, foldout, tips_null, showFunc, addFunc, lbStyle, bgStyle_null },
                    new[]
                        { label, array, foldout, tips_null, showFunc, addFunc, lbStyle_null, bgStyle_null },
                    new[]
                        { label, array, foldout, tips_null, showFunc, addFunc_null, lbStyle_null, bgStyle_null }
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State          = TChunkState.Static,
                        Comments       = "绘制 List 列表",
                        Generics       = new Dictionary<string, string> { { "T", "" } },
                        Name           = "List",
                        Params         = param,
                        ReturnType     = "bool",
                        ReturnComments = "Ture: 打开列表, False: 关闭列表"
                    };
                    chunk.Content = $"return FieldList({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }
            }

            return chunks;
        }
    }
}
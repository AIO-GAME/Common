﻿#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0109 // 

using System;
using System.Collections.Generic;
using System.Reflection;

namespace AIO
{
    public partial class GULayoutSingleton : ClassParam
    {
        private static FunctionParam _Style;

        private static FunctionParam Style
        {
            get
            {
                if (_Style is null)
                {
                    _Style = new FunctionParam("GUIStyle", "style", "style")
                    {
                        Comments = "样式",
                    };
                }

                return _Style;
            }
        }

        private static FunctionParam _Options;

        private static FunctionParam Options
        {
            get
            {
                if (_Options != null) return _Options;
                _Options = new FunctionParam("GUILayoutOption", "options", "options")
                {
                    Comments = "排版格式",
                    IsParams = true
                };
                return _Options;
            }
        }

        public GULayoutSingleton()
        {
            var methods = GetType().GetMethods(BindingFlags.Static | BindingFlags.NonPublic);
            foreach (var method in methods)
            {
                try
                {
                    var attr = method.GetCustomAttribute<FuncParamAttribute>();
                    if (attr is null) continue;
                    if (string.IsNullOrEmpty(attr.Group)) attr.Group = "Default";
                    if (!FunctionGroups.ContainsKey(attr.Group))
                        FunctionGroups.Add(attr.Group, new List<FunctionChunk>());

                    if (attr.IsArray)
                    {
                        var chunks = (IEnumerable<FunctionChunk>)method.Invoke(null, null);
                        FunctionGroups[attr.Group].AddRange(chunks);
                    }
                    else
                    {
                        var chunk = (FunctionChunk)method.Invoke(null, null);
                        FunctionGroups[attr.Group].Add(chunk);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            Comments = "Layout";
            Header.AppendLine("/*|✩ - - - - - |||");
            Header.AppendLine("|||✩ Date:     ||| -> Automatic Generate");
            Header.AppendLine("|||✩ Document: ||| ->");
            Header.AppendLine("|||✩ - - - - - |*/");
            Pragma.Add("warning disable CS1591 // Missing XML comment for publicly visible type or member");
            Using.Add("System");
            Using.Add("System.Collections.Generic");
            Using.Add("System.Linq");
            Using.Add("UnityEngine");
            NameSpace = "AIO";
            Name = "GULayout";
            IsPartial = true;
            State = TChunkState.None;
            Accessibility = "public";
        }
    }
}
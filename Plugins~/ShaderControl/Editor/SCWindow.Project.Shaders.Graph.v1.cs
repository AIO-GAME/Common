using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace Shader.Control.Editor
{
    /// <summary>
    /// Shader Control - (C) Copyright 2016-2020 Ramiro Oliva (Kronnect)
    /// </summary>
    /// 
    public partial class SCWindow
    {
        private const string JSON_NODE_DATA = "JSONnodeData";
        private const string JSON_KEYWORD_SCOPE = "m_KeywordScope";

        [Serializable]
        public struct SerializedKeywordData
        {
            public string typeInfo;
            public string JSONnodeData;
        }

        [Serializable]
        public struct SerializedKeywordProxy
        {
            public string m_Name;
            public string m_DefaultReferenceName;
            public int m_KeywordScope;
            public int m_KeywordDefinition;
        }

        [Serializable]
        public struct ShaderGraphProxy
        {
            public SerializedKeywordData[] m_SerializedKeywords;
        }


        private void ScanShaderGraphV1(SCShader shader, string shaderContents)
        {
            shaderContents = shaderContents.Replace("UnityEditor.ShaderGraph.ShaderKeyword", "ShaderControl.SCWindow.SerializedKeyword");
            var graph = JsonUtility.FromJson<ShaderGraphProxy>(shaderContents);

            var currentPass = new SCShaderPass();
            if (graph.m_SerializedKeywords != null)
            {
                foreach (var skw in graph.m_SerializedKeywords)
                {
                    if (string.IsNullOrEmpty(skw.JSONnodeData)) continue;

                    var kw = JsonUtility.FromJson<SerializedKeywordProxy>(skw.JSONnodeData);

                    var pragmaType = PragmaType.Unknown;
                    switch (kw.m_KeywordDefinition)
                    {
                        case SHADER_GRAPH_KEYWORD_DEFINITION_MULTI_COMPILE when kw.m_KeywordScope == SHADER_GRAPH_KEYWORD_SCOPE_GLOBAL:
                            pragmaType = PragmaType.MultiCompileGlobal;
                            break;
                        case SHADER_GRAPH_KEYWORD_DEFINITION_MULTI_COMPILE when kw.m_KeywordScope == SHADER_GRAPH_KEYWORD_SCOPE_LOCAL:
                            pragmaType = PragmaType.MultiCompileLocal;
                            break;
                        case SHADER_GRAPH_KEYWORD_DEFINITION_SHADER_FEATURE when kw.m_KeywordScope == SHADER_GRAPH_KEYWORD_SCOPE_GLOBAL:
                            pragmaType = PragmaType.FeatureGlobal;
                            break;
                        case SHADER_GRAPH_KEYWORD_DEFINITION_SHADER_FEATURE when kw.m_KeywordScope == SHADER_GRAPH_KEYWORD_SCOPE_LOCAL:
                            pragmaType = PragmaType.FeatureLocal;
                            break;
                    }

                    var keywordLine = new SCKeywordLine();
                    keywordLine.pragmaType = pragmaType;

                    var keyword = new SCKeyword(kw.m_DefaultReferenceName, kw.m_Name);
                    keywordLine.Add(keyword);
                    currentPass.Add(keywordLine);
                }
            }

            shader.Add(currentPass);
            shader.UpdateVariantCount();
        }

        private void ConvertToLocalGraphV1(SCKeyword keyword, SCShader shader)
        {
            var contents = File.ReadAllText(shader.path, Encoding.UTF8);
            var i = contents.IndexOf("m_SerializedKeywords", StringComparison.CurrentCulture);
            if (i < 0) return;
            var j = contents.IndexOf("m_SerializedNodes", StringComparison.CurrentCulture);
            if (j < 0) j = contents.Length - 1;

            var pos = contents.IndexOf(keyword.name, i, StringComparison.CurrentCulture);
            var changed = false;
            if (pos > i && pos < j)
            {
                var dataBlockPos = contents.LastIndexOf(JSON_NODE_DATA, pos, StringComparison.CurrentCulture);
                if (dataBlockPos > 0)
                {
                    var scopePos = contents.IndexOf(JSON_KEYWORD_SCOPE, dataBlockPos, StringComparison.CurrentCulture);
                    if (scopePos > dataBlockPos && scopePos < j)
                    {
                        scopePos += JSON_KEYWORD_SCOPE.Length + 2;
                        var valuePos = contents.IndexOf("1", scopePos, StringComparison.CurrentCulture);
                        var safetyPos = contents.IndexOf("\"", scopePos, StringComparison.CurrentCulture);
                        if (valuePos > scopePos && valuePos < safetyPos && safetyPos > valuePos)
                        {
                            contents = contents.Substring(0, valuePos) + "0" + contents.Substring(valuePos + 1);
                            changed = true;
                        }
                    }
                }
            }

            if (changed)
            {
                MakeBackup(shader);
                File.WriteAllText(shader.path, contents, Encoding.UTF8);
            }
        }
    }
}
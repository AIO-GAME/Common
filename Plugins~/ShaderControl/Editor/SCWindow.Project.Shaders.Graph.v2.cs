using System;
using System.Collections.Generic;
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
        internal const string JSON_NODE_DATA_V2 = "JSONnodeData";
        internal const string JSON_KEYWORD_SCOPE_V2 = "m_KeywordScope";

        [Serializable]
        public struct ShaderGraphChunkDataV2
        {
            public string m_Type;
            public string m_ObjectId;
            public string m_Name;
            public string m_DefaultReferenceName;
            public int m_KeywordScope;
            public int m_KeywordDefinition;
        }


        private List<ShaderGraphChunkDataV2> graphKeywords;

        private void ScanShaderGraphV2(SCShader shader, string shaderContents)
        {
            // Only extract info from first JSON chunk
            ExtractJSONChunks(shaderContents);

            var currentPass = new SCShaderPass();
            foreach (var kw in graphKeywords)
            {
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

                var keyword = new SCKeyword(kw.m_DefaultReferenceName, kw.m_Name, kw.m_ObjectId);
                keywordLine.Add(keyword);
                currentPass.Add(keywordLine);
            }

            shader.Add(currentPass);
            shader.UpdateVariantCount();
        }

        private void ConvertToLocalGraphV2(SCKeyword keyword, SCShader shader)
        {
            var contents = File.ReadAllText(shader.path, Encoding.UTF8);

            var pos = contents.IndexOf("\"m_ObjectId\": \"" + keyword.shaderGraphObjectId, StringComparison.CurrentCulture);
            var changed = false;
            if (pos > 0)
            {
                var scopePos = contents.IndexOf(JSON_KEYWORD_SCOPE, pos, StringComparison.CurrentCulture);
                if (scopePos > pos)
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

            if (changed)
            {
                MakeBackup(shader);
                File.WriteAllText(shader.path, contents, Encoding.UTF8);
            }
        }

        private static readonly char[] jsonClosures = { '{', '}' };

        private void ExtractJSONChunks(string json)
        {
            if (graphKeywords == null)
            {
                graphKeywords = new List<ShaderGraphChunkDataV2>();
            }
            else
            {
                graphKeywords.Clear();
            }

            var count = 0;
            int startIndex = 0, lastIndex = 0;
            do
            {
                var nextClosure = json.IndexOfAny(jsonClosures, lastIndex);
                if (nextClosure < 0) break;
                if (json[nextClosure] == '{') count++;
                else if (json[nextClosure] == '}') count--;
                lastIndex = nextClosure + 1;

                if (count == 0)
                {
                    var jsonChunk = json.Substring(startIndex, lastIndex - startIndex);
                    var chunk = JsonUtility.FromJson<ShaderGraphChunkDataV2>(jsonChunk);
                    if (chunk.m_Type.Equals("UnityEditor.ShaderGraph.ShaderKeyword"))
                    {
                        graphKeywords.Add(chunk);
                    }

                    startIndex = lastIndex;
                }
            } while (true);
        }
    }
}
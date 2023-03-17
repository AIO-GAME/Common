using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Shader.Control.Editor
{
    /// <summary>
    /// Shader Control - (C) Copyright 2016-2020 Ramiro Oliva (Kronnect)
    /// </summary>
    /// 
    public partial class SCWindow
    {
        private void ScanShaderNonGraph(SCShader shader)
        {
            // Reads shader
            var shaderLines = File.ReadAllLines(shader.path);
            var separator = new string[] { " " };
            var currentPass = new SCShaderPass();
            SCShaderPass basePass = null;
            var pragmaControl = 0;
            var pass = -1;
            var blockComment = false;
            var keywordLine = new SCKeywordLine();
            foreach (var t in shaderLines)
            {
                var line = t.Trim();
                if (line.Length == 0) continue;

                var lineCommentIndex = line.IndexOf("//", StringComparison.CurrentCulture);
                var blocCommentIndex = line.IndexOf("/*", StringComparison.CurrentCulture);
                var endCommentIndex = line.IndexOf("*/", StringComparison.CurrentCulture);
                if (blocCommentIndex > 0 && (lineCommentIndex > blocCommentIndex || lineCommentIndex < 0))
                {
                    blockComment = true;
                }

                if (endCommentIndex > blocCommentIndex && (lineCommentIndex > endCommentIndex || lineCommentIndex < 0))
                {
                    blockComment = false;
                }

                if (blockComment)
                    continue;

                var lineUPPER = line.ToUpper();
                if (lineUPPER.Equals("PASS") || lineUPPER.StartsWith("PASS "))
                {
                    if (pass >= 0)
                    {
                        currentPass.pass = pass;
                        if (basePass != null)
                            currentPass.Add(basePass.keywordLines);
                        shader.Add(currentPass);
                    }
                    else if (currentPass.keywordCount > 0)
                    {
                        basePass = currentPass;
                    }

                    currentPass = new SCShaderPass();
                    pass++;
                    continue;
                }

                var j = line.IndexOf(PRAGMA_COMMENT_MARK, StringComparison.CurrentCulture);
                if (j >= 0)
                {
                    pragmaControl = 1;
                }
                else
                {
                    j = line.IndexOf(PRAGMA_DISABLED_MARK, StringComparison.CurrentCulture);
                    if (j >= 0)
                        pragmaControl = 3;
                }

                if (lineCommentIndex == 0 && pragmaControl != 1 && pragmaControl != 3)
                {
                    continue; // do not process lines commented by user
                }

                var pragmaType = PragmaType.Unknown;
                var offset = 0;
                j = line.IndexOf(SCKeywordLine.PRAGMA_MULTICOMPILE_GLOBAL, StringComparison.CurrentCulture);
                if (j >= 0)
                {
                    pragmaType = PragmaType.MultiCompileGlobal;
                    offset = SCKeywordLine.PRAGMA_MULTICOMPILE_GLOBAL.Length;
                }
                else
                {
                    j = line.IndexOf(SCKeywordLine.PRAGMA_FEATURE_GLOBAL, StringComparison.CurrentCulture);
                    if (j >= 0)
                    {
                        pragmaType = PragmaType.FeatureGlobal;
                        offset = SCKeywordLine.PRAGMA_FEATURE_GLOBAL.Length;
                    }
                    else
                    {
                        j = line.IndexOf(SCKeywordLine.PRAGMA_MULTICOMPILE_LOCAL, StringComparison.CurrentCulture);
                        if (j >= 0)
                        {
                            pragmaType = PragmaType.MultiCompileLocal;
                            offset = SCKeywordLine.PRAGMA_MULTICOMPILE_LOCAL.Length;
                        }
                        else
                        {
                            j = line.IndexOf(SCKeywordLine.PRAGMA_FEATURE_LOCAL, StringComparison.CurrentCulture);
                            if (j >= 0)
                            {
                                pragmaType = PragmaType.FeatureLocal;
                                offset = SCKeywordLine.PRAGMA_FEATURE_LOCAL.Length;
                            }
                        }
                    }
                }

                if (j >= 0)
                {
                    if (pragmaControl != 2)
                    {
                        keywordLine = new SCKeywordLine();
                    }

                    keywordLine.pragmaType = pragmaType;
                    // exclude potential comments inside the #pragma line
                    var lastStringPos = line.IndexOf("//", j + offset, StringComparison.CurrentCulture);
                    if (lastStringPos < 0)
                    {
                        lastStringPos = line.Length;
                    }

                    var length = lastStringPos - j - offset;
                    var kk = line.Substring(j + offset, length).Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    // Sanitize keywords
                    for (var i = 0; i < kk.Length; i++)
                    {
                        kk[i] = kk[i].Trim();
                    }

                    // Act on keywords
                    switch (pragmaControl)
                    {
                        case 1: // Edited by Shader Control line
                            shader.editedByShaderControl = true;
                            // Add original keywords to current line
                            foreach (var t1 in kk) keywordLine.Add(shader.GetKeyword(t1));
                            pragmaControl = 2;
                            break;
                        case 2:
                            // check enabled keywords
                            keywordLine.DisableKeywords();
                            foreach (var t1 in kk)
                            {
                                var keyword = keywordLine.GetKeyword(t1);
                                if (keyword != null) keyword.enabled = true;
                            }

                            currentPass.Add(keywordLine);
                            pragmaControl = 0;
                            break;
                        case 3: // disabled by Shader Control line
                            shader.editedByShaderControl = true;
                            // Add original keywords to current line
                            foreach (var t1 in kk)
                            {
                                var keyword = shader.GetKeyword(t1);
                                keyword.enabled = false;
                                keywordLine.Add(keyword);
                            }

                            currentPass.Add(keywordLine);
                            pragmaControl = 0;
                            break;
                        case 0:
                            // Add keywords to current line
                            foreach (var item in kk) keywordLine.Add(shader.GetKeyword(item));
                            currentPass.Add(keywordLine);
                            break;
                    }
                }
            }

            currentPass.pass = Mathf.Max(pass, 0);
            if (basePass != null)
                currentPass.Add(basePass.keywordLines);
            shader.Add(currentPass);
            shader.UpdateVariantCount();
        }


        private void UpdateShaderNonGraph(SCShader shader)
        {
            // Reads and updates shader from disk
            var shaderLines = File.ReadAllLines(shader.path);
            var separator = new string[] { " " };
            var builder = new StringBuilder();
            var pragmaControl = 0;
            shader.editedByShaderControl = false;
            var keywordLine = new SCKeywordLine();
            var blockComment = false;
            foreach (var line in shaderLines)
            {
                var lineCommentIndex = line.IndexOf("//", StringComparison.CurrentCulture);
                var blocCommentIndex = line.IndexOf("/*", StringComparison.CurrentCulture);
                var endCommentIndex = line.IndexOf("*/", StringComparison.CurrentCulture);
                if (blocCommentIndex > 0 && (lineCommentIndex > blocCommentIndex || lineCommentIndex < 0))
                {
                    blockComment = true;
                }

                if (endCommentIndex > blocCommentIndex && (lineCommentIndex > endCommentIndex || lineCommentIndex < 0))
                {
                    blockComment = false;
                }

                var j = -1;
                var pragmaType = PragmaType.Unknown;
                if (!blockComment)
                {
                    j = line.IndexOf(PRAGMA_COMMENT_MARK, StringComparison.CurrentCulture);
                    if (j >= 0)
                    {
                        pragmaControl = 1;
                    }

                    j = line.IndexOf(SCKeywordLine.PRAGMA_MULTICOMPILE_GLOBAL, StringComparison.CurrentCulture);
                    if (j >= 0)
                    {
                        pragmaType = PragmaType.MultiCompileGlobal;
                    }
                    else
                    {
                        j = line.IndexOf(SCKeywordLine.PRAGMA_FEATURE_GLOBAL, StringComparison.CurrentCulture);
                        if (j >= 0)
                        {
                            pragmaType = PragmaType.FeatureGlobal;
                        }
                        else
                        {
                            j = line.IndexOf(SCKeywordLine.PRAGMA_MULTICOMPILE_LOCAL, StringComparison.CurrentCulture);
                            if (j >= 0)
                            {
                                pragmaType = PragmaType.MultiCompileLocal;
                            }
                            else
                            {
                                j = line.IndexOf(SCKeywordLine.PRAGMA_FEATURE_LOCAL, StringComparison.CurrentCulture);
                                if (j >= 0)
                                {
                                    pragmaType = PragmaType.FeatureLocal;
                                }
                            }
                        }
                    }

                    if (pragmaControl != 1 && lineCommentIndex == 0 && line.IndexOf(PRAGMA_DISABLED_MARK, StringComparison.CurrentCulture) < 0)
                    {
                        // do not process a commented line
                        j = -1;
                    }
                }

                if (j >= 0)
                {
                    if (pragmaControl != 2)
                    {
                        keywordLine.Clear();
                    }

                    keywordLine.pragmaType = pragmaType;
                    j = line.IndexOf(' ', j + 20) + 1; // first space after pragma declaration
                    if (j >= line.Length) continue;
                    // exclude potential comments inside the #pragma line
                    var lastStringPos = line.IndexOf("//", j, StringComparison.CurrentCulture);
                    if (lastStringPos < 0)
                    {
                        lastStringPos = line.Length;
                    }

                    var length = lastStringPos - j;
                    var kk = line.Substring(j, length).Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    // Sanitize keywords
                    for (var i = 0; i < kk.Length; i++)
                    {
                        kk[i] = kk[i].Trim();
                    }

                    // Act on keywords
                    switch (pragmaControl)
                    {
                        case 1:
                            // Read original keywords
                            foreach (var t in kk) keywordLine.Add(shader.GetKeyword(t));
                            pragmaControl = 2;
                            break;
                        case 0:
                        case 2:
                            if (pragmaControl == 0)
                            {
                                foreach (var t in kk) keywordLine.Add(shader.GetKeyword(t));
                            }

                            var kCount = keywordLine.keywordCount;
                            var kEnabledCount = keywordLine.keywordsEnabledCount;
                            if (kEnabledCount < kCount)
                            {
                                // write original keywords
                                if (kEnabledCount == 0)
                                {
                                    builder.Append(PRAGMA_DISABLED_MARK);
                                }
                                else
                                {
                                    builder.Append(PRAGMA_COMMENT_MARK);
                                }

                                shader.editedByShaderControl = true;
                                builder.Append(keywordLine.GetPragma());
                                if (keywordLine.hasUnderscoreVariant)
                                    builder.Append(PRAGMA_UNDERSCORE);
                                for (var s = 0; s < kCount; s++)
                                {
                                    builder.Append(keywordLine.keywords[s].name);
                                    if (s < kCount - 1) builder.Append(" ");
                                }

                                builder.AppendLine();
                            }

                            if (kEnabledCount > 0)
                            {
                                // Write actual keywords
                                builder.Append(keywordLine.GetPragma());
                                if (keywordLine.hasUnderscoreVariant)
                                    builder.Append(PRAGMA_UNDERSCORE);
                                for (var s = 0; s < kCount; s++)
                                {
                                    var keyword = keywordLine.keywords[s];
                                    if (keyword.enabled)
                                    {
                                        builder.Append(keyword.name);
                                        if (s < kCount - 1) builder.Append(" ");
                                    }
                                }

                                builder.AppendLine();
                            }

                            pragmaControl = 0;
                            break;
                    }
                }
                else
                {
                    builder.AppendLine(line);
                }
            }

            // Writes modified shader
            File.WriteAllText(shader.path, builder.ToString());
            AssetDatabase.Refresh();
        }

        private void ConvertToLocalNonGraph(SCKeyword keyword, SCShader shader)
        {
            var path = shader.path;
            if (!File.Exists(path)) return;
            var lines = File.ReadAllLines(path);
            var changed = false;
            for (var k = 0; k < lines.Length; k++)
            {
                // Just convert to local shader_features for now since multi_compile global keywords can be nabled using the Shader global API
                if (lines[k].IndexOf(SCKeywordLine.PRAGMA_FEATURE_GLOBAL, StringComparison.CurrentCultureIgnoreCase) >= 0 &&
                    lines[k].IndexOf(keyword.name, StringComparison.CurrentCultureIgnoreCase) >= 0)
                {
                    lines[k] = lines[k].Replace(SCKeywordLine.PRAGMA_FEATURE_GLOBAL, SCKeywordLine.PRAGMA_FEATURE_LOCAL);
                    lines[k] = lines[k].Replace(SCKeywordLine.PRAGMA_FEATURE_GLOBAL.ToUpper(), SCKeywordLine.PRAGMA_FEATURE_LOCAL);
                    changed = true;
                }
            }

            if (changed)
            {
                MakeBackup(shader);
                File.WriteAllLines(path, lines, Encoding.UTF8);
            }
        }
    }
}
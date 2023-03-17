using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shader.Control.Editor
{
    public enum PragmaType
    {
        Unknown,
        MultiCompileGlobal,
        MultiCompileLocal,
        FeatureGlobal,
        FeatureLocal
    }

    public class SCShader
    {
        public string fullName = "";
        public string name = "";
        public string path = "";
        public int GUID;
        public List<SCShaderPass> passes = new List<SCShaderPass>();
        public List<SCKeyword> keywords = new List<SCKeyword>();
        public List<SCMaterial> materials = new List<SCMaterial>();
        public int totalVariantCount, actualBuildVariantCount;
        public int keywordEnabledCount;
        public bool foldout;
        public bool showMaterials;
        public bool pendingChanges;
        public bool editedByShaderControl;
        public bool hasBackup;
        public bool isReadOnly;
        public bool isShaderGraph;
        public int shaderGraphVersion = 1;

        public void Add(SCShaderPass pass)
        {
            passes.Add(pass);
            UpdateKeywords();
        }

        public void AddKeywordsByName(IEnumerable<string> names)
        {
            var changes = false;
            foreach (var kwName in names)
            {
                if (string.IsNullOrEmpty(kwName)) continue;
                // is it already included?
                var kwCount = keywords.Count;
                var repeated = false;
                for (var j = 0; j < kwCount; j++)
                {
                    if (keywords[j].name.Equals(kwName))
                    {
                        repeated = true;
                        break;
                    }
                }

                if (repeated) continue;
                keywords.Add(new SCKeyword(kwName));
                changes = true;
            }

            if (changes)
            {
                keywords.Sort((k1, k2) => string.Compare(k1.name, k2.name, StringComparison.CurrentCulture));
            }
        }

        public void RemoveKeyword(string name)
        {
            for (var k = 0; k < keywords.Count; k++)
            {
                var keyword = keywords[k];
                if (keyword.name.Equals(name))
                {
                    if (keyword.enabled) keywordEnabledCount--;
                    keywords.Remove(keyword);
                    return;
                }
            }
        }

        public void EnableKeywords()
        {
            keywords.ForEach(keyword => keyword.enabled = true);
        }

        public List<string> enabledKeywords
        {
            get
            {
                var kk = new List<string>(keywords.Count);
                keywords.ForEach(kw =>
                {
                    if (kw.enabled) kk.Add(kw.name);
                });
                return kk;
            }
        }

        public bool hasSource
        {
            get { return path.Length > 0; }
        }

        private void UpdateKeywords()
        {
            passes.ForEach(pass =>
            {
                foreach (var line in pass.keywordLines)
                {
                    foreach (var keyword in line.keywords.Where(keyword => !keywords.Contains(keyword)))
                    {
                        keywords.Add(keyword);
                    }
                }
            });
        }

        public void UpdateVariantCount()
        {
            totalVariantCount = 0;
            actualBuildVariantCount = 0;
            passes.ForEach(pass =>
            {
                var matCount = materials.Count;
                var passCount = 1;
                var passBuildCount = 1;
                var passKeywordLinesCount = pass.keywordLines.Count;
                for (var l = 0; l < passKeywordLinesCount; l++)
                {
                    var line = pass.keywordLines[l];
                    var kLineEnabledCount = line.hasUnderscoreVariant ? 1 : 0;
                    var kLineCount = kLineEnabledCount;
                    var lineKeywordsCount = line.keywords.Count;
                    for (var k = 0; k < lineKeywordsCount; k++)
                    {
                        var keyword = line.keywords[k];

                        // if this is a shader feature, first check if there's at least one material using it
                        if (line.pragmaType == PragmaType.FeatureGlobal || line.pragmaType == PragmaType.FeatureLocal)
                        {
                            var materialUsesKeyword = false;
                            for (var m = 0; m < matCount; m++)
                            {
                                if (materials[m].ContainsKeyword(keyword.name))
                                {
                                    materialUsesKeyword = true;
                                    break;
                                }
                            }

                            if (!materialUsesKeyword) continue;
                        }

                        kLineCount++;
                        if (keyword.enabled)
                        {
                            kLineEnabledCount++;
                        }
                    }

                    if (kLineEnabledCount > 0)
                    {
                        passBuildCount *= kLineEnabledCount;
                    }

                    passCount *= kLineCount;
                }

                totalVariantCount += passCount;
                actualBuildVariantCount += passBuildCount;
            });

            keywordEnabledCount = 0;
            var keywordCount = keywords.Count;
            for (var k = 0; k < keywordCount; k++)
            {
                if (keywords[k].enabled)
                    keywordEnabledCount++;
            }
        }

        public SCKeyword GetKeyword(string name)
        {
            var kCount = keywords.Count;
            for (var k = 0; k < kCount; k++)
            {
                var keyword = keywords[k];
                if (keyword.name.Equals(name))
                    return keyword;
            }

            return new SCKeyword(name);
        }

        public static string GetSimpleName(string longName)
        {
            var k = longName.LastIndexOf("/", StringComparison.CurrentCulture);
            if (k >= 0)
            {
                return longName.Substring(k + 1);
            }

            return longName;
        }
    }

    public class SCShaderPass
    {
        public int pass;
        public List<SCKeywordLine> keywordLines = new List<SCKeywordLine>();
        public int keywordCount;

        public void Add(SCKeywordLine keywordLine)
        {
            keywordLines.Add(keywordLine);
            UpdateKeywordCount();
        }

        public void Add(IEnumerable<SCKeywordLine> keywordLines)
        {
            this.keywordLines.AddRange(keywordLines);
            UpdateKeywordCount();
        }

        private void UpdateKeywordCount()
        {
            keywordCount = 0;
            keywordLines.ForEach(obj => keywordCount += obj.keywordCount);
        }

        public void Clear()
        {
            keywordCount = 0;
            keywordLines.Clear();
        }
    }

    public class SCKeywordLine
    {
        public const string PRAGMA_MULTICOMPILE_GLOBAL = "#pragma multi_compile ";
        public const string PRAGMA_MULTICOMPILE_LOCAL = "#pragma multi_compile_local ";
        public const string PRAGMA_FEATURE_GLOBAL = "#pragma shader_feature ";
        public const string PRAGMA_FEATURE_LOCAL = "#pragma shader_feature_local ";


        public List<SCKeyword> keywords = new List<SCKeyword>();
        public bool hasUnderscoreVariant;
        public PragmaType pragmaType;

        public string GetPragma()
        {
            switch (pragmaType)
            {
                case PragmaType.MultiCompileGlobal: return PRAGMA_MULTICOMPILE_GLOBAL;
                case PragmaType.MultiCompileLocal: return PRAGMA_MULTICOMPILE_LOCAL;
                case PragmaType.FeatureGlobal: return PRAGMA_FEATURE_GLOBAL;
                case PragmaType.FeatureLocal: return PRAGMA_FEATURE_LOCAL;
            }

            return "";
        }

        public SCKeyword GetKeyword(string name)
        {
            int kc = keywords.Count;
            for (int k = 0; k < kc; k++)
            {
                SCKeyword keyword = keywords[k];
                if (keyword.name.Equals(name))
                {
                    return keyword;
                }
            }

            return null;
        }

        public void Add(SCKeyword keyword)
        {
            if (GetKeyword(keyword.name) != null)
                return;
            // ignore underscore keywords
            var goodKeyword = keyword.name.Any(t => t != '_');

            keyword.isMultiCompile = pragmaType == PragmaType.MultiCompileGlobal || pragmaType == PragmaType.MultiCompileLocal;
            if (goodKeyword)
            {
                keyword.isGlobal = pragmaType != PragmaType.FeatureLocal && pragmaType != PragmaType.MultiCompileLocal && pragmaType != PragmaType.Unknown;
                keyword.verboseName = keyword.name + " (";
                if (!string.IsNullOrEmpty(keyword.shaderGraphName))
                {
                    keyword.verboseName += keyword.shaderGraphName + ", ";
                }

                keyword.verboseName += keyword.isMultiCompile ? "multi_compile" : "shader_feature";
                keyword.verboseName += keyword.isGlobal ? ", global)" : ", local)";
                keywords.Add(keyword);
            }
            else
            {
                keyword.isUnderscoreKeyword = true;
                hasUnderscoreVariant = true;
            }
        }

        public void DisableKeywords()
        {
            keywords.ForEach(obj => obj.enabled = false);
        }

        public void Clear()
        {
            keywords.Clear();
        }

        public int keywordCount
        {
            get { return keywords.Count; }
        }

        public int keywordsEnabledCount
        {
            get
            {
                int kCount = keywords.Count;
                int enabledCount = 0;
                for (int k = 0; k < kCount; k++)
                {
                    if (keywords[k].enabled)
                        enabledCount++;
                }

                return enabledCount;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int k = 0; k < keywords.Count; k++)
            {
                if (k > 0)
                    sb.Append(" ");
                sb.Append(keywords[k].name);
            }

            return sb.ToString();
        }
    }

    public class SCKeyword
    {
        public readonly string name;
        public string verboseName;
        public string shaderGraphName;
        public string shaderGraphObjectId;
        public bool enabled;
        public bool isUnderscoreKeyword;
        public bool isGlobal = true;
        public bool isMultiCompile = true;
        public bool canBeConvertedToLocal;
        public bool canBeModified;

        public SCKeyword(string name, string shaderGraphName = null, string shaderGraphObjectId = null)
        {
            this.name = name;
            this.verboseName = name;
            this.shaderGraphName = shaderGraphName;
            this.shaderGraphObjectId = shaderGraphObjectId;
            enabled = true;
        }

        public override bool Equals(object obj)
        {
            if (obj is SCKeyword other)
            {
                return name.Equals(other.name);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return name.GetHashCode();
        }

        public override string ToString()
        {
            return name;
        }
    }
}
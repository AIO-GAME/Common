using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shader.Control.Editor
{
    public class SCMaterial
    {
        public Material unityMaterial;
        public string path;
        public string GUID;
        public List<SCKeyword> keywords = new List<SCKeyword>();
        public bool pendingChanges;
        public bool foldout;

        private HashSet<string> keywordSet = new HashSet<string>();

        public SCMaterial(Material material, string path, string GUID)
        {
            unityMaterial = material;
            this.path = path;
            this.GUID = GUID;
        }

        public void SetKeywords(IEnumerable<string> names)
        {
            foreach (var t in names)
            {
                if (!keywordSet.Contains(t))
                {
                    keywordSet.Add(t);
                    keywords.Add(new SCKeyword(t));
                }
            }

            keywords.Sort((k1, k2) => string.Compare(k1.name, k2.name, StringComparison.CurrentCulture));
        }

        public bool ContainsKeyword(string name)
        {
            return keywordSet.Contains(name);
        }

        public void RemoveKeyword(string name)
        {
            for (var k = 0; k < keywords.Count; k++)
            {
                if (keywords[k].name.Equals(name))
                {
                    keywords.RemoveAt(k);
                    return;
                }
            }
        }
    }
}
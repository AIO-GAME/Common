using UnityEngine;
using System.Collections.Generic;

namespace Shader.Control.Editor
{
    public class StringPerm
    {
        public static List<List<string>> GetCombinations(IList<string> items)
        {
            var variants = new List<List<string>>();
            var bitCount = items.Count;
            var mask = (int)Mathf.Pow(2, bitCount);
            for (var k = 1; k < mask; k++)
            {
                var variant = new List<string>();
                var bit = 1;
                for (var j = 0; j < bitCount; j++, bit <<= 1)
                {
                    if ((k & bit) != 0)
                    {
                        variant.Add(items[j]);
                    }
                }

                variants.Add(variant);
            }

            return variants;
        }
    }
}
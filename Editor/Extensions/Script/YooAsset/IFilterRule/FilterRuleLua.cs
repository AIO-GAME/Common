#if SUPPORT_YOOASSET
using System.IO;
using UnityEngine;
using YooAsset.Editor;

namespace AIO.Unity.Editor
{
    [DisplayName("收集Lua代码及其他文件")]
    public class FilterRuleLua : IFilterRule
    {
        public bool IsCollectAsset(FilterRuleData data)
        {
            var ext = Path.GetExtension(data.AssetPath).ToLower();
            switch (ext)
            {
                case ".lua":
                    return true;
                // case ".sproto":
                //     return true;
                // case ".sql":
                //     return true;
                default:
                    return false;
            }
        }
    }
}
#endif
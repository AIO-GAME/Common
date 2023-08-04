#if SUPPORT_YOOASSET
using System.IO;
using UnityEngine;
using YooAsset.Editor;

namespace AIO.UEditor
{
    [DisplayName("收集 Lua代码及其他文件")]
    public class CollectRuleLua : IFilterRule
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

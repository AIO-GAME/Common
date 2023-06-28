#if SUPPORT_YOOASSET
using System.IO;
using YooAsset.Editor;

namespace AIO.Unity.Editor
{
    [DisplayName("收集FBX资源")]
    public class FilterRuleFBX : IFilterRule
    {
        public bool IsCollectAsset(FilterRuleData data)
        {
            return Path.GetExtension(data.AssetPath).ToLower()  == ".fbx";
        }
    }
}
#endif
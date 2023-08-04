#if SUPPORT_YOOASSET
using System.IO;
using YooAsset.Editor;

namespace AIO.UEditor
{
    [DisplayName("收集 FBX资源")]
    public class CollectRuleFBX : IFilterRule
    {
        public bool IsCollectAsset(FilterRuleData data)
        {
            return Path.GetExtension(data.AssetPath).ToLower()  == ".fbx";
        }
    }
}
#endif

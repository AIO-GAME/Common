#if SUPPORT_YOOASSET
using System.IO;
using YooAsset.Editor;

namespace UnityEditor
{
    [DisplayName("收集 Sprite Atlas")]
    public class FilterRuleSpriteAtlas : IFilterRule
    {
        public bool IsCollectAsset(FilterRuleData data)
        {
            return Path.GetExtension(data.AssetPath).ToLower() == ".spriteatlas";
        }
    }
}
#endif
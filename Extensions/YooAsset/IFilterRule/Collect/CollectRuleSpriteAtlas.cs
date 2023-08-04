#if SUPPORT_YOOASSET
using System.IO;
using YooAsset.Editor;

namespace AIO.UEditor
{
    [DisplayName("收集 SpriteAtlas")]
    public class CollectRuleSpriteAtlas : IFilterRule
    {
        public bool IsCollectAsset(FilterRuleData data)
        {
            return Path.GetExtension(data.AssetPath).ToLower() == ".spriteatlas";
        }
    }

}
#endif

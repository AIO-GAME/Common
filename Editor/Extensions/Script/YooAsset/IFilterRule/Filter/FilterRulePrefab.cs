/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-08-02
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

#if SUPPORT_YOOASSET
using System.IO;
using YooAsset.Editor;

namespace AIO.Unity.Editor
{
    [DisplayName("过滤 预制件")]
    public class FilterRulePrefab : IFilterRule
    {
        public bool IsCollectAsset(FilterRuleData data)
        {
            return Path.GetExtension(data.AssetPath).ToLower() != ".prefab";
        }
    }

    [DisplayName("过滤 SpriteAtlas")]
    public class FilterRuleSpriteAtlas : IFilterRule
    {
        public bool IsCollectAsset(FilterRuleData data)
        {
            return Path.GetExtension(data.AssetPath).ToLower() != ".spriteatlas";
        }
    }

    [DisplayName("过滤 SpriteAtlas Prefab")]
    public class FilterRuleSpriteAtlasPrefab : IFilterRule
    {
        public bool IsCollectAsset(FilterRuleData data)
        {
            var Extension = Path.GetExtension(data.AssetPath).ToLower();
            return Extension != ".spriteatlas" &&
                   Extension != ".prefab";
        }
    }
    
    [DisplayName("过滤 Scene Prefab")]
    public class FilterRuleScenePrefab : IFilterRule
    {
        public bool IsCollectAsset(FilterRuleData data)
        {
            var Extension = Path.GetExtension(data.AssetPath).ToLower();
            return Extension != ".unity" &&
                   Extension != ".prefab";
        }
    }

    [DisplayName("过滤 SpriteAtlas Prefab Scene")]
    public class FilterRuleSpriteAtlasPrefabScene : IFilterRule
    {
        public bool IsCollectAsset(FilterRuleData data)
        {
            var Extension = Path.GetExtension(data.AssetPath).ToLower();
            return Extension != ".spriteatlas" &&
                   Extension != ".prefab" &&
                   Extension != ".unity";
        }
    }
}
#endif
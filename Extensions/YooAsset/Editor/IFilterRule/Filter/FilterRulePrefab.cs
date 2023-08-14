/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-08-02
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System.IO;
using YooAsset.Editor;

namespace AIO.UEditor.YooAsset
{
    [DisplayName("过滤 Prefab")]
    public class FilterRulePrefab : IFilterRule
    {
        public bool IsCollectAsset(FilterRuleData data)
        {
            var Extension = Path.GetExtension(data.AssetPath).ToLower();
            return
                Extension != ".prefab";
        }
    }

    [DisplayName("过滤 SpriteAtlas")]
    public class FilterRuleSpriteAtlas : IFilterRule
    {
        public bool IsCollectAsset(FilterRuleData data)
        {
            var Extension = Path.GetExtension(data.AssetPath).ToLower();
            return
                Extension != ".spriteatlas";
        }
    }

    [DisplayName("过滤 SpriteAtlas Prefab")]
    public class FilterRuleSpriteAtlasPrefab : IFilterRule
    {
        public bool IsCollectAsset(FilterRuleData data)
        {
            var Extension = Path.GetExtension(data.AssetPath).ToLower();
            return
                Extension != ".spriteatlas" &&
                Extension != ".prefab";
        }
    }

    [DisplayName("过滤 SpriteAtlas Prefab Material")]
    public class FilterRuleSpriteAtlasPrefabMaterial : IFilterRule
    {
        public bool IsCollectAsset(FilterRuleData data)
        {
            var Extension = Path.GetExtension(data.AssetPath).ToLower();
            return Extension != ".spriteatlas" &&
                   Extension != ".prefab" &&
                   Extension != ".mat";
        }
    }

    [DisplayName("过滤 SpriteAtlas Prefab Material AudioClip")]
    public class FilterRuleSpriteAtlasPrefabMaterialAudioClip : IFilterRule
    {
        public bool IsCollectAsset(FilterRuleData data)
        {
            var rule = new CollectRuleAudioClip();
            if (rule.IsCollectAsset(data)) return false;
            var Extension = Path.GetExtension(data.AssetPath).ToLower();
            return Extension != ".spriteatlas" &&
                   Extension != ".prefab" &&
                   Extension != ".mat";
        }
    }

    [DisplayName("过滤 SpriteAtlas Prefab Material AudioClip Font")]
    public class FilterRuleSpriteAtlasPrefabMaterialAudioClipFont : IFilterRule
    {
        public bool IsCollectAsset(FilterRuleData data)
        {
            IFilterRule rule = new CollectRuleAudioClip();
            if (rule.IsCollectAsset(data)) return false;
            rule = new CollectRuleFont();
            if (rule.IsCollectAsset(data)) return false;
            rule = new CollectRulePrefab();
            if (rule.IsCollectAsset(data)) return false;
            rule = new CollectRuleSpriteAtlas();
            if (rule.IsCollectAsset(data)) return false;
            rule = new CollectRuleMaterial();
            if (rule.IsCollectAsset(data)) return false;
            return true;
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

    [DisplayName("过滤 Scene Prefab Material")]
    public class FilterRuleScenePrefabMaterial : IFilterRule
    {
        public bool IsCollectAsset(FilterRuleData data)
        {
            var Extension = Path.GetExtension(data.AssetPath).ToLower();
            return Extension != ".unity" &&
                   Extension != ".prefab" &&
                   Extension != ".mat";
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
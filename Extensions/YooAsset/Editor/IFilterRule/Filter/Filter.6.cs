﻿/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-08-18
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using System.IO;
using YooAsset.Editor;

namespace AIO.UEditor.YooAsset
{
    [DisplayName("过滤 SpriteAtlas Prefab Material AudioClip Font Asset")]
    public class FilterRuleSpriteAtlasPrefabMaterialAudioClipFontAsset : IFilterRule
    {
        public bool IsCollectAsset(FilterRuleData data)
        {
            if (new CollectAudioClip().IsCollectAsset(data)) return false;
            if (new CollectFont().IsCollectAsset(data)) return false;
            if (new CollectPrefab().IsCollectAsset(data)) return false;
            if (new CollectSpriteAtlas().IsCollectAsset(data)) return false;
            if (new CollectMaterial().IsCollectAsset(data)) return false;
            if (new CollectAsset().IsCollectAsset(data)) return false;
            return true;
        }
    }
}
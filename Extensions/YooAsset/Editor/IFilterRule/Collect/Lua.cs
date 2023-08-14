using System.IO;
using UnityEngine;
using YooAsset.Editor;

namespace AIO.UEditor.YooAsset
{
    [DisplayName("收集 Lua")]
    public class CollectRuleLua : IFilterRule
    {
        public bool IsCollectAsset(FilterRuleData data)
        {
            var Extension = Path.GetExtension(data.AssetPath).ToLower();
            return
                Extension == ".lua";
        }
    }
}
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
}
#endif
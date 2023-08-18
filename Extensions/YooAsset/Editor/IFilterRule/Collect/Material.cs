﻿/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-08-14
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using System.IO;
using YooAsset.Editor;

namespace AIO.UEditor.YooAsset
{
    [DisplayName("收集 Material")]
    public class CollectMaterial : IFilterRule
    {
        public bool IsCollectAsset(FilterRuleData data)
        {
            var Extension = Path.GetExtension(data.AssetPath).ToLower();
            return
                Extension == ".mat";
        }
    }
}
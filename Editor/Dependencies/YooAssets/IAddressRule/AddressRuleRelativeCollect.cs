#if SUPPORT_YOOASSET
using System.IO;
using YooAsset.Editor;

namespace UnityEditor
{
    [DisplayName("Collect + 后缀 = 定位地址")]
    public class AddressRuleRelativeCollectWithSuffix : IAddressRule
    {
        string IAddressRule.GetAssetAddress(AddressRuleData data)
        {
            return data.AssetPath.Replace(data.CollectPath + "/", "");
        }
    }

    [DisplayName("Collect = 定位地址")]
    public class AddressRuleRelativeCollectNoSuffix : IAddressRule
    {
        string IAddressRule.GetAssetAddress(AddressRuleData data)
        {
            var path = data.AssetPath.Replace(string.Concat(data.CollectPath, "/"), "");
            var suffix = Path.GetExtension(path);
            if (string.IsNullOrEmpty(suffix)) return path;
            return path.Replace(Path.GetExtension(path), "");
        }
    }
}
#endif
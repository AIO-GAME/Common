#if SUPPORT_YOOASSET
using System.IO;
using YooAsset.Editor;

namespace AIO.UEditor
{
    [DisplayName("Collect + 后缀 = 定位地址")]
    public class AddressRuleRelativeCollectWithSuffix : IAddressRule
    {
        string IAddressRule.GetAssetAddress(AddressRuleData data)
        {
            return data.AssetPath.Replace(data.CollectPath + "/", "");
        }
    }

    [DisplayName("Collect + 后缀 = 定位地址  (全小写)")]
    public class AddressRuleRelativeCollectWithSuffixToLower : IAddressRule
    {
        string IAddressRule.GetAssetAddress(AddressRuleData data)
        {
            return data.AssetPath.Replace(data.CollectPath + "/", "").ToLower();
        }
    }

    [DisplayName("Collect + 后缀 = 定位地址  (全大写)")]
    public class AddressRuleRelativeCollectWithSuffixToUpper : IAddressRule
    {
        string IAddressRule.GetAssetAddress(AddressRuleData data)
        {
            return data.AssetPath.Replace(data.CollectPath + "/", "").ToUpper();
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

    [DisplayName("Collect = 定位地址 (全小写)")]
    public class AddressRuleRelativeCollectNoSuffixToLower : IAddressRule
    {
        string IAddressRule.GetAssetAddress(AddressRuleData data)
        {
            var path = data.AssetPath.Replace(string.Concat(data.CollectPath, "/"), "");
            var suffix = Path.GetExtension(path);
            if (string.IsNullOrEmpty(suffix)) return path;
            return path.Replace(Path.GetExtension(path), "").ToLower();
        }
    }

    [DisplayName("Collect = 定位地址  (全大写)")]
    public class AddressRuleRelativeCollectNoSuffixToUpper : IAddressRule
    {
        string IAddressRule.GetAssetAddress(AddressRuleData data)
        {
            var path = data.AssetPath.Replace(string.Concat(data.CollectPath, "/"), "");
            var suffix = Path.GetExtension(path);
            if (string.IsNullOrEmpty(suffix)) return path;
            return path.Replace(Path.GetExtension(path), "").ToUpper();
        }
    }

    [DisplayName("Root Collect + 后缀 = 定位地址")]
    public class AddressRuleRelativeRootCollectWithSuffix : IAddressRule
    {
        string IAddressRule.GetAssetAddress(AddressRuleData data)
        {
            return data.AssetPath.Replace(data.CollectPath + "/", Path.GetFileName(data.CollectPath) + "/");
        }
    }

    [DisplayName("Group Collect + 后缀 = 定位地址")]
    public class AddressRuleRelativeGroupCollectWithSuffix : IAddressRule
    {
        string IAddressRule.GetAssetAddress(AddressRuleData data)
        {
            return data.AssetPath.Replace(data.CollectPath + "/", Path.GetFileName(data.GroupName) + "/");
        }
    }

    [DisplayName("Group Collect + 后缀 = 定位地址 (全小写)")]
    public class AddressRuleRelativeGroupCollectWithSuffixToLower : IAddressRule
    {
        string IAddressRule.GetAssetAddress(AddressRuleData data)
        {
            return data.AssetPath.Replace(data.CollectPath + "/", Path.GetFileName(data.GroupName) + "/").ToLower();
        }
    }

    [DisplayName("Group Collect + 后缀 = 定位地址 (全大写)")]
    public class AddressRuleRelativeGroupCollectWithSuffixToUpper : IAddressRule
    {
        string IAddressRule.GetAssetAddress(AddressRuleData data)
        {
            return data.AssetPath.Replace(data.CollectPath + "/", Path.GetFileName(data.GroupName) + "/").ToUpper();
        }
    }

    [DisplayName("UserData Collect + 后缀 = 定位地址")]
    public class AddressRuleRelativeUserDataCollectWithSuffix : IAddressRule
    {
        string IAddressRule.GetAssetAddress(AddressRuleData data)
        {
            return data.AssetPath.Replace(data.CollectPath + "/", Path.GetFileName(data.UserData) + "/");
        }
    }

    [DisplayName("UserData Collect + 后缀 = 定位地址 (全小写)")]
    public class AddressRuleRelativeUserDataCollectWithSuffixToLower : IAddressRule
    {
        string IAddressRule.GetAssetAddress(AddressRuleData data)
        {
            return data.AssetPath.Replace(data.CollectPath + "/", Path.GetFileName(data.UserData) + "/").ToLower();
        }
    }

    [DisplayName("UserData Collect + 后缀 = 定位地址 (全大写)")]
    public class AddressRuleRelativeUserDataCollectWithSuffixToUpper : IAddressRule
    {
        string IAddressRule.GetAssetAddress(AddressRuleData data)
        {
            return data.AssetPath.Replace(data.CollectPath + "/", Path.GetFileName(data.UserData) + "/").ToUpper();
        }
    }


    [DisplayName("UserData Collect = 定位地址")]
    public class AddressRuleRelativeUserDataCollect : IAddressRule
    {
        string IAddressRule.GetAssetAddress(AddressRuleData data)
        {
            var path = data.AssetPath.Replace(data.CollectPath + "/", Path.GetFileName(data.UserData) + "/");
            return path.Replace(Path.GetExtension(path), "");
        }
    }

    [DisplayName("UserData Collect = 定位地址 (全小写)")]
    public class AddressRuleRelativeUserDataCollectToLower : IAddressRule
    {
        string IAddressRule.GetAssetAddress(AddressRuleData data)
        {
            var path = data.AssetPath.Replace(data.CollectPath + "/", Path.GetFileName(data.UserData) + "/").ToLower();
            return path.Replace(Path.GetExtension(path), "");
        }
    }

    [DisplayName("UserData Collect = 定位地址 (全大写)")]
    public class AddressRuleRelativeUserDataCollectToUpper : IAddressRule
    {
        string IAddressRule.GetAssetAddress(AddressRuleData data)
        {
            var path = data.AssetPath.Replace(data.CollectPath + "/", Path.GetFileName(data.UserData) + "/").ToUpper();
            return path.Replace(Path.GetExtension(path), "");
        }
    }
}
#endif

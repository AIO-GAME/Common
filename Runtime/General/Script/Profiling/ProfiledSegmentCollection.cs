namespace AIO
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// 调试判断集合
    /// </summary>
    public class ProfiledSegmentCollection : KeyedCollection<string, ProfiledSegment>
    {
        /// <summary>
        /// 在派生类中实现时，将从指定元素提取键。
        /// </summary>
        protected override string GetKeyForItem(ProfiledSegment item)
        {
            return item.Name;
        }
    }
}

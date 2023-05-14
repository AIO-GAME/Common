namespace AIO
{
    /// <summary>
    /// 通知集合
    /// </summary>
    public interface INotifiedCollectionItem
    {
        /// <summary>
        /// 广播之后添加
        /// </summary>
        void BeforeAdd();

        /// <summary>
        /// 广播之前添加
        /// </summary>
        void AfterAdd();

        /// <summary>
        /// 广播之后移除
        /// </summary>
        void BeforeRemove();

        /// <summary>
        /// 广播之前移除
        /// </summary>
        void AfterRemove();
    }
}

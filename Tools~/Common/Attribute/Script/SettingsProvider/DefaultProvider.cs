namespace AIO
{
    #region

#if !PORTABLE
    using System.Configuration;
#endif

    #endregion

    /// <summary>
    /// 选择提供者
    /// </summary>
#if PORTABLE
    public class DefaultProvider : PortableSettingsProvider
#else
    public class DefaultProvider : LocalFileSettingsProvider
#endif
    { }
}
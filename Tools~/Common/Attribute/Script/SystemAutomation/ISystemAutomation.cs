#if NO_UNITY
using System.Collections;
using System.Threading.Tasks;

/// <summary>
/// 系统自动化
/// </summary>
public interface ISystemAutomation
{
    /// <summary>
    /// 异步 初始化
    /// </summary>
    Task InstallAsync();

    /// <summary>
    /// 异步 卸载
    /// </summary>
    Task UnInstallAsync();

    /// <summary>
    /// 同步 初始化
    /// </summary>
    IEnumerator InstallSync();

    /// <summary>
    /// 同步 卸载
    /// </summary>
    IEnumerator UnInstallSync();
}
#endif
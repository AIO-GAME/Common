using System.Collections;
using System.Threading.Tasks;

/// <summary>
/// DLL自动化处理
/// </summary>
public interface IDllAutomation
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

    /// <summary>
    /// 运行
    /// </summary>
    void Run();
}
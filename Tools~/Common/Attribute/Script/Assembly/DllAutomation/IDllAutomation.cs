using System.Threading.Tasks;
/// <summary>
/// DLL自动化处理
/// </summary>
public interface IDllAutomation
{
    /// <summary>
    /// 初始化
    /// </summary>
    Task Install();

    /// <summary>
    /// 卸载
    /// </summary>
    Task UnInstall();
}
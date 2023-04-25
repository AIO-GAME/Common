using System.Threading.Tasks;

/// <summary>
/// 系统自动化
/// </summary>
public interface ISystemAutomation
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

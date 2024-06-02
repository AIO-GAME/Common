#region

using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

#endregion

// 有关程序集的一般信息由以下
// 控制。更改这些特性值可修改
// 与程序集关联的信息。
[assembly: AssemblyTitle("AIO.Security")]
[assembly: AssemblyProduct("AIO.Security")]
[assembly: AssemblyDefaultAlias("AIO.Security")]
[assembly: AssemblyDescription("安全工具程序集")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyTrademark("")]
// 将 ComVisible 设置为 false 会使此程序集中的类型
//对 COM 组件不可见。如果需要从 COM 访问此程序集中的类型
//请将此类型的 ComVisible 特性设置为 true。
[assembly: ComVisible(false)]

// 如果此项目向 COM 公开，则下列 GUID 用于类型库的 ID
[assembly: Guid("92CC9BE2-7F3E-11A1-50D8-A6A4F35F978A")]

[assembly: AssemblyVersion(AssemblyInfo.Version)]
[assembly: AssemblyFileVersion(AssemblyInfo.FileVersion)]
[assembly: AssemblyCompany(AssemblyInfo.Company)]
[assembly: AssemblyCopyright(AssemblyInfo.Copyright)]
[assembly: AssemblyCulture(AssemblyInfo.Culture)]

[assembly: Editor("SemanticVersion", AssemblyInfo.Version)]
[assembly: Editor("Version4", AssemblyInfo.Version)]
[assembly: Editor("WaveMarketingName", AssemblyInfo.WaveMarketingName)]
[assembly: Editor("WaveMarketingNameCompressed", AssemblyInfo.WaveMarketingName)]

[assembly: AssemblyMetadata("SemanticVersion", AssemblyInfo.Version)]
[assembly: AssemblyMetadata("Version4", AssemblyInfo.Version)]
[assembly: AssemblyMetadata("WaveMarketingName", AssemblyInfo.WaveMarketingName)]
[assembly: AssemblyMetadata("WaveMarketingNameCompressed", AssemblyInfo.WaveMarketingName)]

[module: UnverifiableCode]

/// <summary>
/// 程序集信息
/// </summary>
internal static class AssemblyInfo
{
    /// <summary>
    /// 密钥信息
    /// </summary>
    public const string KEY = ",PublicKey=" + PublicKey + ",PublicKeyToken=" + Token;

    /// <summary>
    /// 公钥
    /// </summary>
    public const string PublicKey =
        "002400000480000094000000060200000024000052534131000400000100010015955a571ef6904bd2bad9d840866399d7985020e6f2f7db9ec57370d5b7b0524fc70e54059bd367789d8c55bd159f4d3766852ea223e215c8bc7454916da83b445e7ea746828372350de16ebe00a41a7159cd27f626ab320b450af0f27cf1d24fc0405b769225e1914ac5da2f82eef67e567607d3c1e0b30725ccb442d55fdb";

    /// <summary>
    /// 公钥Token
    /// </summary>
    public const string Token = "f76e188eeb081931";

    /// <summary>
    /// 版本
    /// </summary>
    public const string Version = "1.0.0.0";

    /// <summary>
    /// 版本
    /// </summary>
    public const string WaveMarketingName = "2024.5.12";

    /// <summary>
    /// 指示编译器使用 Win32 文件版本资源的特定版本号。 Win32 文件版本不需要与程序集的版本号相同。
    /// </summary>
    public const string FileVersion = "1.0.0.0";

    /// <summary>
    /// 密钥位置
    /// </summary>
    internal const string KeyFile = "..\\..\\Resources\\PublicKey\\AIO.snk";

    /// <summary>
    /// 密钥位置
    /// </summary>
    internal const string KeyName = "AIO.snk";

    /// <summary>
    /// 作者
    /// </summary>
    public const string Company = "XINAN";

    /// <summary>
    /// 程序集属性
    /// </summary>
    public const string Copyright = "Copyright ©  2023";

    /// <summary>
    /// 支持的区域性
    /// </summary>
    public const string Culture = "neutral";
}
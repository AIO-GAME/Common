﻿using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// 有关程序集的一般信息由以下
// 控制。更改这些特性值可修改
// 与程序集关联的信息。
[assembly: AssemblyTitle("Ensure")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyProduct("Ensure")]
[assembly: AssemblyTrademark("")]

// 将 ComVisible 设置为 false 会使此程序集中的类型
//对 COM 组件不可见。如果需要从 COM 访问此程序集中的类型
//请将此类型的 ComVisible 特性设置为 true。
[assembly: ComVisible(false)]

// 如果此项目向 COM 公开，则下列 GUID 用于类型库的 ID
[assembly: Guid("3e725c5e-8dd9-4ab3-8042-96effab382b4")]

// 程序集的版本信息由下列四个值组成: 
//
//      主版本
//      次版本
//      生成号
//      修订号
//
//可以指定所有这些值，也可以使用“生成号”和“修订号”的默认值
//通过使用 "*"，如下所示:
// [assembly: AssemblyVersion("1.0.*")]

[assembly: AssemblyVersion(AssemblyInfo.Version)]
[assembly: AssemblyFileVersion(AssemblyInfo.FileVersion)]
[assembly: AssemblyCompany(AssemblyInfo.Company)]
[assembly: AssemblyCopyright(AssemblyInfo.Copyright)]
[assembly: AssemblyCulture(AssemblyInfo.Culture)]

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
    public const string PublicKey = "002400000480000094000000060200000024000052534131000400000100010015955a571ef6904bd2bad9d840866399d7985020e6f2f7db9ec57370d5b7b0524fc70e54059bd367789d8c55bd159f4d3766852ea223e215c8bc7454916da83b445e7ea746828372350de16ebe00a41a7159cd27f626ab320b450af0f27cf1d24fc0405b769225e1914ac5da2f82eef67e567607d3c1e0b30725ccb442d55fdb";

    /// <summary>
    /// 公钥Token
    /// </summary>
    public const string Token = "f76e188eeb081931";

    /// <summary>
    /// 版本
    /// </summary>
    public const string Version = "1.0.0.0";

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
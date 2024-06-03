/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-11-23                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

#endregion

namespace AIO
{
    /// <summary>
    /// 进程构造器 只读
    /// </summary>
    public interface IPrCourseRead : IDisposable
    {
        /// <summary>
        /// ProcessStartInfo类型的属性，表示启动进程所需的信息。
        /// </summary>
        [DebuggerHidden, DebuggerNonUserCode]
        ProcessStartInfo Info { get; }

        /// <summary>
        /// 开启输出日志
        /// </summary>
        [DebuggerHidden, DebuggerNonUserCode]
        bool EnableOutput { get; set; }
    }

    /// <summary>
    /// 进程构造器
    /// </summary>
    public interface IPrCourse : IDisposable
    {
        /// <summary>
        /// 生成执行器
        /// </summary>
        /// <returns>执行器</returns>
        [DebuggerHidden, DebuggerNonUserCode]
        IExecutor Execute();

        /// <summary>
        /// 执行文件路径
        /// </summary>
        /// <param name="target"></param>
        /// <returns>构造器</returns>
        [DebuggerHidden, DebuggerNonUserCode]
        IPrCourse SetFileName(in string target);

        /// <summary>
        /// 是否开启日志输出
        /// </summary>
        /// <returns>构造器</returns>
        [DebuggerHidden, DebuggerNonUserCode]
        IPrCourse SetRedirectOutput(in bool value);

        /// <summary>
        /// 是否开启错误输出
        /// </summary>
        /// <returns>构造器</returns>
        [DebuggerHidden, DebuggerNonUserCode]
        IPrCourse SetRedirectError(in bool value);

        /// <summary>
        /// 设置参数
        /// </summary>
        /// <returns>构造器</returns>
        [DebuggerHidden, DebuggerNonUserCode]
        IPrCourse SetInArgs(in ICollection<string> args);

        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="args"></param>
        /// <returns>构造器</returns>
        [DebuggerHidden, DebuggerNonUserCode]
        IPrCourse SetInArgs(in string args);

        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="args"></param>
        /// <returns>构造器</returns>
        [DebuggerHidden, DebuggerNonUserCode]
        IPrCourse SetInArgs(in StringBuilder args);

        /// <summary>
        /// 设置参数
        /// </summary>
        /// <returns>构造器</returns>
        [DebuggerHidden, DebuggerNonUserCode]
        IPrCourse SetInArgs(in string format, params object[] args);

        /// <summary>
        /// 默认为当前应用程序范围
        /// </summary>
        /// <returns>构造器</returns>
        [DebuggerHidden, DebuggerNonUserCode]
        IPrCourse SetInDomain(in string domain = null);

        /// <summary>
        /// 是否显示错误对话框
        /// </summary>
        /// <returns>构造器</returns>
        [DebuggerHidden, DebuggerNonUserCode]
        IPrCourse SetInErrorDialog(in bool value);

        /// <summary>
        /// 是否加载用户配置文件
        /// </summary>
        /// <returns>构造器</returns>
        [DebuggerHidden, DebuggerNonUserCode]
        IPrCourse SetInLoadUserProfile(in bool value);

        /// <summary>
        /// 设置用户名
        /// </summary>
        /// <returns>构造器</returns>
        [DebuggerHidden, DebuggerNonUserCode]
        IPrCourse SetUserName(in string username);

        /// <summary>
        /// 设置密码
        /// </summary>
        /// <returns>构造器</returns>
        [DebuggerHidden, DebuggerNonUserCode]
        IPrCourse SetInPassword(in string username);

        /// <summary>
        /// 设置执行方式
        /// </summary>
        /// true  : 指执行方式是系统执行 相当于用户双击文件，此时FileName不限于可执行文件(.exe)，例如网址，bat文件均可；
        /// false : 程序创建进程 程序使用CreateProcess来创建进程，对进程的控制更多，但FileName必须是可执行文件(.exe)。
        /// <returns>构造器</returns>
        [DebuggerHidden, DebuggerNonUserCode]
        IPrCourse SetInUseShellExecute(in bool value);

        /// <summary>
        /// 创建新窗口
        /// </summary>
        /// UseShellExecute = true  该值无效
        /// UseShellExecute = false；CreateNoWindow = true 控制台无法显示 无法窗口关闭
        /// <returns>构造器</returns>
        [DebuggerHidden, DebuggerNonUserCode]
        IPrCourse SetInCreateNoWindow(in bool value);

        /// <summary>
        /// 用于设置GUI程序的窗口。对控制台程序无效。
        /// </summary>
        /// <returns>构造器</returns>
        [DebuggerHidden, DebuggerNonUserCode]
        IPrCourse SetInWindow(in ProcessWindowStyle style = ProcessWindowStyle.Normal);

        /// <summary>
        /// Verb控制
        /// </summary>
        /// 文件的右键菜单里，除了“打开”，根据文件的不同还会出现“打印”，“编辑”，“以管理员身份运行”等选项，Verb控制的就是这个选项。
        /// <returns>构造器</returns>
        [DebuggerHidden, DebuggerNonUserCode]
        IPrCourse SetInVers(in EPrVerb verb);

        /// <summary>
        /// Verb控制
        /// </summary>
        /// 文件的右键菜单里，除了“打开”，根据文件的不同还会出现“打印”，“编辑”，“以管理员身份运行”等选项，Verb控制的就是这个选项。
        /// <returns>构造器</returns>
        [DebuggerHidden, DebuggerNonUserCode]
        IPrCourse SetWorkingDir(in string workDir);

        /// <summary>
        /// 设置编码
        /// </summary>
        /// <returns>构造器</returns>
        [DebuggerHidden, DebuggerNonUserCode]
        IPrCourse SetInEncoding(Encoding defEncoding);

        /// <summary>
        /// 输入 开启
        /// </summary>
        /// <returns>构造器</returns>
        [DebuggerHidden, DebuggerNonUserCode]
        IPrCourse SetRedirectInput(bool value);
    }
}
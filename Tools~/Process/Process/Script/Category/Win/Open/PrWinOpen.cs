/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-11-23                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;

namespace AIO
{
    public partial class PrWin
    {
        /// <summary>
        /// 开启命令或操作
        /// </summary>
        public static class Open
        {
            /// <summary>
            /// 打开路径
            /// </summary>
            /// <param name="target">目标路径</param>
            /// <returns>执行器</returns>
            public static IExecutor Path(in string target)
            {
                return Create<PrNative>(target);
            }

            /// <summary>
            /// 打开 windows 控制面板
            /// </summary>
            /// <returns>执行器</returns>
            public static IExecutor ControlPanel()
            {
                return Create("rundll32.exe ", "shell32.dll,Control_RunDLL");
            }

            /// <summary>
            /// 打开 windows 扫描仪和照相机向导
            /// </summary>
            /// <returns>执行器</returns>
            public static IExecutor Wiaacmgr(in string args = null)
            {
                return Create("wiaacmgr", args);
            }

            /// <summary>
            /// 打开 windows 脚本宿主设置
            /// </summary>
            /// <returns>执行器</returns>
            public static IExecutor Wscript(in string args = null)
            {
                return Create("wscript", args);
            }

            /// <summary>
            /// 打开 windows 画图板
            /// </summary>
            /// <returns>执行器</returns>
            public static IExecutor Mspaint()
            {
                return Create("mspaint");
            }

            /// <summary>
            /// 打开 windows 远程桌面连接
            /// </summary>
            /// <returns>执行器</returns>
            public static IExecutor Mstsc()
            {
                return Create("mstsc");
            }

            /// <summary>
            /// 打开 windows 放大镜实用程序
            /// </summary>
            /// <returns>执行器</returns>
            public static IExecutor Magnify()
            {
                return Create("magnify");
            }

            /// <summary>
            /// 打开 windows 控制台
            /// </summary>
            /// <returns>执行器</returns>
            public static IExecutor Mmc()
            {
                return Create("mmc");
            }

            /// <summary>
            /// 打开 windows 同步命令
            /// </summary>
            /// <returns>执行器</returns>
            public static IExecutor Mobsync()
            {
                return Create("mobsync");
            }

            /// <summary>
            /// 打开 windows 系统组件服务
            /// </summary>
            /// <returns>执行器</returns>
            public static IExecutor Dcomcnfg()
            {
                return Create("dcomcnfg");
            }

            /// <summary>
            /// 打开 windows Media Player
            /// </summary>
            /// <returns>执行器</returns>
            public static IExecutor Dvdplay()
            {
                return Create("dvdplay");
            }

            /// <summary>
            /// 打开 windows 记事本
            /// </summary>
            /// <returns>执行器</returns>
            public static IExecutor Notepad()
            {
                return Create("notepad");
            }

            /// <summary>
            /// 打开 windows 网络管理的工具向导
            /// 运行里输入 nslookup 即可打开 nslookup.exe程序 ，也可在cmd窗口输入，
            /// 用法见 https://wenku.baidu.com/view/4c130edca58da0116c174947.html）
            /// </summary>
            /// <returns>执行器</returns>
            public static IExecutor Nslookup(string args = null)
            {
                return Create("nslookup", args);
            }

            /// <summary>
            /// 打开 windows 屏幕“讲述人”
            /// </summary>
            /// <returns>执行器</returns>
            public static IExecutor Narrator()
            {
                return Create("narrator");
            }

            /// <summary>
            /// 打开 windows (TC)命令检查接口
            /// </summary>
            /// <returns>执行器</returns>
            public static IExecutor Netstat(string args = "-an")
            {
                return new PrNative().SetFileName("netstat").SetInArgs(args).Execute();
            }

            /// <summary>
            /// 打开 windows 文件签名验证程序
            /// </summary>
            /// <returns>执行器</returns>
            public static IExecutor Sigverif()
            {
                return Create("sigverif");
            }

            /// <summary>
            /// 打开 windows 录音机
            /// </summary>
            /// <returns>执行器</returns>
            public static IExecutor Sndrec32()
            {
                return Create("sndrec32");
            }

            /// <summary>
            /// 打开 windows 任务管理器
            /// </summary>
            /// <returns>执行器</returns>
            public static IExecutor Taskmgr()
            {
                return Create("taskmgr");
            }

            /// <summary>
            /// 打开 windows 事件查看器
            /// </summary>
            /// <returns>执行器</returns>
            public static IExecutor Eventvwr()
            {
                return Create("eventvwr");
            }

            /// <summary>
            /// 打开 windows 造字程序
            /// </summary>
            /// <returns>执行器</returns>
            public static IExecutor Eudcedit()
            {
                return Create("eudcedit");
            }

            /// <summary>
            /// 打开 windows 资源管理器
            /// </summary>
            /// <returns>执行器</returns>
            public static IExecutor Explorer(string target = null)
            {
                return Create("explorer", target);
            }

            /// <summary>
            /// 打开 windows 对象包装程序
            /// </summary>
            /// <returns>执行器</returns>
            public static IExecutor Perfmon()
            {
                return Create("perfmon");
            }

            /// <summary>
            /// 打开 windows 注册表
            /// </summary>
            /// <returns>执行器</returns>
            public static IExecutor Regedit()
            {
                return Create("regedit.exe");
            }

            /// <summary>
            /// 打开 windows 注册表编辑器
            /// </summary>
            /// <returns>执行器</returns>
            public static IExecutor Regedt32()
            {
                return Create("regedt32");
            }

            /// <summary>
            /// 打开 windows Chkdsk 磁盘检查
            /// </summary>
            /// <returns>执行器</returns>
            public static IExecutor Chkdsk(string args = "/F")
            {
                return new PrNative().SetFileName("chkdsk").SetInArgs(args).Execute();
            }

            /// <summary>
            /// 打开 windows 计算器
            /// </summary>
            /// <returns>执行器</returns>
            public static IExecutor Calc()
            {
                return Create("calc");
            }

            /// <summary>
            /// 打开 windows 字符映射表
            /// </summary>
            /// <returns>执行器</returns>
            public static IExecutor Charmap()
            {
                return Create("charmap");
            }

            /// <summary>
            /// 打开 windows SQL SERVER 客户端网络实用程序
            /// </summary>
            /// <returns>执行器</returns>
            public static IExecutor SQLSERVER()
            {
                return Create("cliconfg");
            }

            /// <summary>
            /// 打开 windows 垃圾整理
            /// </summary>
            /// <returns>执行器</returns>
            public static IExecutor Cleanmgr()
            {
                return Create("cleanmgr");
            }

            /// <summary>
            /// 打开 windows 屏幕键盘
            /// </summary>
            /// <returns>执行器</returns>
            public static IExecutor OSK()
            {
                return Create("osk");
            }

            /// <summary>
            /// 打开 windows ODBC数据源管理器
            /// </summary>
            /// <returns>执行器</returns>
            public static IExecutor Odbcad32()
            {
                return Create("odbcad32");
            }

            /// <summary>
            /// 打开 windows 辅助工具管理器
            /// </summary>
            /// <returns>执行器</returns>
            public static IExecutor Utilman()
            {
                return Create("utilman");
            }

            /// <summary>
            /// 打开 windows 管理体系结构(控制台根节点 WMI控件)
            /// </summary>
            public static IExecutor Wmimgmt()
            {
                return Activator.CreateInstance<PrCmd>().SetInArgs(PrCmd.CMD_ARGS).Execute().Input("wmimgmt.msc");
            }

            /// <summary>
            /// 打开 windows 本地服务设置 　
            /// </summary>
            public static IExecutor Services()
            {
                return Activator.CreateInstance<PrCmd>().SetInArgs(PrCmd.CMD_ARGS).Execute().Input("services.msc");
            }

            /// <summary>
            /// 打开 windows 证书管理实用程序
            /// </summary>
            public static IExecutor Certmgr()
            {
                return Activator.CreateInstance<PrCmd>().SetInArgs(PrCmd.CMD_ARGS).Execute().Input("certmgr");
            }

            /// <summary>
            /// 打开 windows 本机用户和组
            /// </summary>
            public static IExecutor Lusrmgr()
            {
                return Activator.CreateInstance<PrCmd>().SetInArgs(PrCmd.CMD_ARGS).Execute().Input("lusrmgr.msc");
            }

            /// <summary>
            /// 打开 windows 共享文件夹管理器
            /// </summary>
            public static IExecutor Fsmgmt()
            {
                return Activator.CreateInstance<PrCmd>().SetInArgs(PrCmd.CMD_ARGS).Execute().Input("fsmgmt.msc");
            }

            /// <summary>
            /// 打开 windows 设备管理器
            /// </summary>
            public static IExecutor Devmgmt()
            {
                return Activator.CreateInstance<PrCmd>().SetInArgs(PrCmd.CMD_ARGS).Execute().Input("devmgmt.msc");
            }

            /// <summary>
            /// 打开 windows 磁盘管理实用程序
            /// </summary>
            public static IExecutor Diskmgmt()
            {
                return Activator.CreateInstance<PrCmd>().SetInArgs(PrCmd.CMD_ARGS).Execute().Input("diskmgmt.msc");
            }
        }
    }
}
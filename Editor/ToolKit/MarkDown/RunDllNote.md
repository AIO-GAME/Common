/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2020 by XiNan Indie Developer
* All rights reserved.
* FileName:         Editors.Note
* Author:           XiNan
* Version:          0.1
* UnityVersion:     2019.2.18f1
* Date:             2020-04-22
* Time:             15:33:47
* E-Mail:           1398581458@qq.com
* Description:
* History:
* * * * * * * * * * * * * * * * * * * * * * * * */

/*
    命令列:rundll32.exe user.exe,restartwindows
    功能: 系统重启

    命令列:rundll32.exe user.exe,exitwindows
    功能: 关闭系统

    命令列: rundll32.exe shell32.dll,Control_RunDLL
    功能: 显示控制面板

    命令列: rundll32.exe shell32.dll,Control_RunDLL access.cpl,,1
    功能: 显示“控制面板－辅助选项－键盘”选项视窗

    命令列: rundll32.exe shell32.dll,Control_RunDLL access.cpl,,2
    功能: 显示“控制面板－辅助选项－声音”选项视窗

    命令列: rundll32.exe shell32.dll,Control_RunDLL access.cpl,,3
    功能: 显示“控制面板－辅助选项－显示”选项视窗

    命令列: rundll32.exe shell32.dll,Control_RunDLL access.cpl,,4
    功能: 显示“控制面板－辅助选项－滑鼠”选项视窗

    命令列: rundll32.exe shell32.dll,Control_RunDLL access.cpl,,5
    功能: 显示“控制面板－辅助选项－传统”选项视窗

    命令列: rundll32.exe shell32.dll,Control_RunDLL sysdm.cpl @1
    功能: 执行“控制面板－添加新硬体”向导。

    命令列: rundll32.exe shell32.dll,SHHelpShortcuts_RunDLL AddPrinter
    功能: 执行“控制面板－添加新印表机”向导。

    命令列: rundll32.exe shell32.dll,Control_RunDLL appwiz.cpl,,1
    功能: 显示 “控制面板－添加/删除程式” 面板。

    命令列: rundll32.exe shell32.dll,Control_RunDLL appwiz.cpl,,1

    功能: 显示 “控制面板－添加/删除程式－安装/卸载” 面板。

    命令列: rundll32.exe shell32.dll,Control_RunDLL appwiz.cpl,,2
    功能: 显示 “控制面板－添加/删除程式－安装Windows” 面板。

    命令列: rundll32.exe shell32.dll,Control_RunDLL appwiz.cpl,,3
    功能: 显示 “控制面板－添加/删除程式－启动盘” 面板。

    命令列: rundll32.exe syncui.dll,Briefcase_Create
    功能: 在桌面上建立一个新的“我的公文包”。

    命令列: rundll32.exe diskcopy.dll,DiskCopyRunDll
    功能: 显示复制软碟视窗

    命令列: rundll32.exe apwiz.cpl,NewLinkHere ％1
    功能: 显示“建立快捷方式”的对话框，所建立的快捷方式的位置由％1参数决定。

    命令列: rundll32.exe shell32.dll,Control_RunDLL timedate.cpl,,0
    功能: 显示“日期与时间”选项视窗。

    命令列: rundll32.exe shell32.dll,Control_RunDLL timedate.cpl,,1
    功能: 显示“时区”选项视窗。

    命令列: rundll32.exe rnaui.dll,RnaDial [某个拨号连接的名称]
    功能: 显示某个拨号连接的拨号视窗。如果已经拨号连接，则显示目前的连接状态的视窗。

    命令列: rundll32.exe rnaui.dll,RnaWizard
    功能: 显示“新建拨号连接”向导的视窗。

    命令列: rundll32.exe shell32.dll,Control_RunDLL desk.cpl,,0
    功能: 显示“显示属性－背景”选项视窗。

    命令列: rundll32.exe shell32.dll,Control_RunDLL desk.cpl,,1
    功能: 显示“显示属性－萤屏保护”选项视窗。

    命令列: rundll32.exe shell32.dll,Control_RunDLL desk.cpl,,2
    功能: 显示“显示属性－外观”选项视窗。

    命令列: rundll32.exe shell32.dll,Control_RunDLL desk.cpl,,3
    功能: 显示显示“显示属性－属性”选项视窗。

    命令列: rundll32.exe shell32.dll,SHHelpShortcuts_RunDLL FontsFolder
    功能: 显示Windows的“字体”档案夹。

    命令列: rundll32.exe shell32.dll,Control_RunDLL main.cpl @3
    功能: 同样是显示Windows的“字体”档案夹。

    命令列: rundll32.exe shell32.dll,SHFormatDrive
    功能: 显示格式化软碟对话框。

    命令列: rundll32.exe shell32.dll,Control_RunDLL joy.cpl,,0
    功能: 显示“控制面板－游戏控制器－一般”选项视窗。

    命令列: rundll32.exe shell32.dll,Control_RunDLL joy.cpl,,1
    功能: 显示“控制面板－游戏控制器－进阶”选项视窗。

    命令列: rundll32.exe mshtml.dll,PrintHTML (HTML文档)
    功能: 列印HTML文档。

    命令列: rundll32.exe shell32.dll,Control_RunDLL mlcfg32.cpl
    功能: 显示Microsoft Exchange一般选项视窗。

    命令列: rundll32.exe shell32.dll,Control_RunDLL main.cpl @0
    功能: 显示“控制面板－滑鼠” 选项 。

    命令列: rundll32.exe shell32.dll,Control_RunDLL main.cpl @1
    功能: 显示 “控制面板－键盘属性－速度”选项视窗。

    命令列: rundll32.exe shell32.dll,Control_RunDLL main.cpl @1,,1
    功能: 显示 “控制面板－键盘属性－语言”选项视窗。

    命令列: rundll32.exe shell32.dll,Control_RunDLL main.cpl @2
    功能: 显示Windows“印表机”档案夹。

    命令列: rundll32.exe shell32.dll,Control_RunDLL main.cpl @4
    功能: 显示“控制面板－输入法属性－输入法”选项视窗。

    命令列: rundll32.exe shell32.dll,Control_RunDLL modem.cpl,,add
    功能: 执行“添加新调制解调器”向导。

    命令列: rundll32.exe shell32.dll,Control_RunDLL mmsys.cpl,,0
    功能: 显示“控制面板－多媒体属性－音频”属性页。

    命令列: rundll32.exe shell32.dll,Control_RunDLL mmsys.cpl,,1
    功能: 显示“控制面板－多媒体属性－视频”属性页。

    命令列: rundll32.exe shell32.dll,Control_RunDLL mmsys.cpl,,2
    功能: 显示“控制面板－多媒体属性－MIDI”属性页。

    命令列: rundll32.exe shell32.dll,Control_RunDLL mmsys.cpl,,3
    功能: 显示“控制面板－多媒体属性－CD音乐”属性页。

    命令列: rundll32.exe shell32.dll,Control_RunDLL mmsys.cpl,,4
    功能: 显示“控制面板－多媒体属性－设备”属性页。

    命令列: rundll32.exe shell32.dll,Control_RunDLL mmsys.cpl @1
    功能: 显示“控制面板－声音”选项视窗。

    命令列: rundll32.exe shell32.dll,Control_RunDLL netcpl.cpl
    功能: 显示“控制面板－网路”选项视窗。

    命令列: rundll32.exe shell32.dll,Control_RunDLL odbccp32.cpl
    功能: 显示ODBC32资料管理选项视窗。

    命令列: rundll32.exe shell32.dll,OpenAs_RunDLL {drive:/path/filename}
    功能: 显示指定档案(drive:/path/filename)的“打开方式”对话框。

    命令列: rundll32.exe shell32.dll,Control_RunDLL password.cpl
    功能: 显示“控制面板－密码”选项视窗。

    命令列: rundll32.exe shell32.dll,Control_RunDLL powercfg.cpl
    功能: 显示“控制面板－电源管理属性”选项视窗。

    命令列: rundll32.exe shell32.dll,SHHelpShortcuts_RunDLL PrintersFolder
    功能: 显示Windows“印表机”档案夹。(同rundll32.exe shell32.dll,Control_RunDLL main.cpl @2)

    命令列: rundll32.exe shell32.dll,Control_RunDLL intl.cpl,,0
    功能: 显示“控制面板－区域设置属性－区域设置”选项视窗。

    命令列: rundll32.exe shell32.dll,Control_RunDLL intl.cpl,,1
    功能: 显示“控制面板－区域设置属性－数字”选项视窗。

    命令列: rundll32.exe shell32.dll,Control_RunDLL intl.cpl,,2
    功能: 显示“控制面板－区域设置属性－货币”选项视窗。

    命令列: rundll32.exe shell32.dll,Control_RunDLL intl.cpl,,3
    功能: 显示“控制面板－区域设置属性－时间”选项视窗。

    命令列: rundll32.exe shell32.dll,Control_RunDLL intl.cpl,,4
    功能: 显示“控制面板－区域设置属性－日期”选项视窗。

    命令列: rundll32.exe desk.cpl,InstallScreenSaver [萤屏保护档案名]
    功能: 将指定的萤屏保护档案设置为Windows的屏保，并显示萤屏保护属性视窗。

    命令列: rundll32.exe shell32.dll,Control_RunDLL sysdm.cpl,,0
    功能: 显示“控制面板－系统属性－传统”属性视窗。

    命令列: rundll32.exe shell32.dll,Control_RunDLL sysdm.cpl,,1
    功能: 显示“控制面板－系统属性－设备管理器”属性视窗。

    命令列: rundll32.exe shell32.dll,Control_RunDLL sysdm.cpl,,2
    功能: 显示“控制面板－系统属性－硬体配置档案”属性视窗。

    命令列: rundll32.exe shell32.dll,Control_RunDLL sysdm.cpl,,3
    功能: 显示“控制面板－系统属性－性能”属性视窗。

    命令列: rundll32.exe shell32.dll,Control_RunDLL telephon.cpl
    功能: 显示“拨号属性”选项视窗

    命令列: rundll32.exe shell32.dll,Control_RunDLL themes.cpl

    功能: 显示“桌面主题”选项面板

    命令列: rundll32.exe shell32.dll,Control_RunDLL firewall.cpl
    功能: 显示“Windows防火墙"面板

    命令列: rundll32.exe shell32.dll,Control_RunDLL NetSetup.cpl,@0,WNSW
    功能: 显示“无线网络设置"面板

    System.Diagnostics.Process.Start("notepad.exe");        -- 打开记事本

    System.Diagnostics.Process.Start("calc.exe ");                -- 打开计算器

    System.Diagnostics.Process.Start("regedit.exe ");           -- 打开注册表

    System.Diagnostics.Process.Start("mspaint.exe ");        -- 打开画图板

    System.Diagnostics.Process.Start("write.exe ");              -- 打开写字板

    System.Diagnostics.Process.Start("mplayer2.exe ");        --打开播放器

    System.Diagnostics.Process.Start("taskmgr.exe ");          --打开任务管理器

    System.Diagnostics.Process.Start("eventvwr.exe ");          --打开事件查看器

    System.Diagnostics.Process.Start("winmsd.exe ");           --打开系统信息

    System.Diagnostics.Process.Start("winver.exe ");              --打开Windows版本信息

    System.Diagnostics.Process.Start("mailto: "+ address);    -- 发邮件

    System.Diagnostics.Process.Start("shutdown.exe","-r");              -- 关闭并重启计算机

    System.Diagnostics.Process.Start("shutdown.exe","-s -f");          -- 关闭计算机

    System.Diagnostics.Process.Start("shutdown.exe","-s -f 30");     -- 30s后关闭计算机

    System.Diagnostics.Process.Start("shutdown.exe","-l");               --注销计算机

    System.Diagnostics.Process.Start("shutdown.exe","-a");              --撤销关闭计算机

    ApplicationData	目录，它用作当前漫游用户的应用程序特定数据的公共储存库。
    漫游用户在网络上的多台计算机上工作。漫游用户的配置文件保存在网络服务器上，当用户登录到某个系统上时，它会加载到该系统。

    CommonApplicationData	目录，它用作所有用户使用的应用程序特定数据的公共储存库。
    CommonProgramFiles	用于应用程序间共享的组件的目录。
    Cookies	用作 Internet Cookie 的公共储存库的目录。
    Desktop	逻辑桌面，而不是物理文件系统位置。
    DesktopDirectory	用于物理上存储桌面上的文件对象的目录。
    不应将此目录与桌面文件夹本身混淆，后者是虚拟文件夹。

    Favorites	用作用户收藏夹项的公共储存库的目录。
    History	用作 Internet 历史记录项的公共储存库的目录。
    InternetCache	用作 Internet 临时文件的公共储存库的目录。
    LocalApplicationData	目录，它用作当前非漫游用户使用的应用程序特定数据的公共储存库。
    MyComputer	“我的电脑”文件夹。
    注意
    由于没有为“我的电脑”文件夹定义路径，因此 MyComputer 常数将始终生成空字符串 ("")。

    MyDocuments	“我的电脑”文件夹。
    MyMusic	“My Music”文件夹。
    MyPictures	“My Pictures”文件夹。
    Personal	用作文档的公共储存库的目录。
    ProgramFiles	“Program files”目录。
    Programs	包含用户程序组的目录。
    Recent	包含用户最近使用过的文档的目录。
    SendTo	包含“发送”菜单项的目录。
    StartMenu	包含“开始”菜单项的目录。
    Startup	对应于用户的“启动”程序组的目录。
    每当用户登录、启动 Windows NT 或更高版本或启动 Windows 98 时，系统均会启动这些程序。

    System	“System”目录。
    Templates	用作文档模板的公共储存库的目录。
 */

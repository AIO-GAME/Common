/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

#if UNITY_EDITOR_WIN
using Microsoft.Win32;
#endif


namespace AIO.UEditor
{
    public static partial class UtilsEditor
    {
        /// <summary>
        /// windos软件检测
        /// </summary>
        public static class SoftUtils
        {
#if UNITY_EDITOR_WIN

            /// <summary>
            /// 用于判断windows电脑中有无安装指定程序，名字是*.exe
            /// 如果某些程序注册表已被 则检测失败 默认理想情况为 所有程序注册表为完整有效
            /// </summary>
            public static bool IsInstallSoft(string softname)
            {
                if (SoftIsInLocalMachine(softname)) return true;
                if (SoftIsInUninstall(softname)) return true;
                if (SoftIsInUsers(softname)) return true;
                return false;
            }

            /// <summary>
            /// 查找Local 是否有指定程序
            /// </summary>
            private static bool SoftIsInLocalMachine(string softname)
            {
                var regSubKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\", false);
                foreach (var keyName in regSubKey.GetSubKeyNames())
                {
                    if (keyName.Contains(softname)) return true;
                }

                return false;
            }

            /// <summary>
            /// 查找是否有对应卸载工具
            /// </summary>
            private static bool SoftIsInUninstall(string softname)
            {
                try
                {
                    var uninstallNode = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall");
                    foreach (var subKeyName in uninstallNode.GetSubKeyNames())
                    {
                        var subKey = uninstallNode.OpenSubKey(subKeyName);
                        var displayName = subKey.GetValue("DisplayName");
                        if (displayName != null)
                        {
                            if (displayName.ToString().ToLower().Contains(System.IO.Path.GetFileName(softname).ToLower()))
                            {
                                return true;
                            }
                        }
                    }
                }
                catch
                {
                    return false;
                }

                return false;
            }

            /// <summary>
            /// 查找相关应用程序的位置 Users
            /// </summary>
            private static bool SoftIsInUsers(string softname)
            {
                var regKey = Registry.Users;
                foreach (var keyName in regKey.GetSubKeyNames())
                {
                    if (keyName.ToLower().Contains("classes"))
                    {
                        var subKeyName = keyName.Substring(0, keyName.Length - 8);
                        var name = string.Concat(subKeyName, @"\Software\Microsoft\Windows\CurrentVersion\App Paths\");
                        var regSubKey = regKey.OpenSubKey(name, false);
                        if (regSubKey == null) continue;
                        foreach (var subName in regSubKey.GetSubKeyNames())
                            if (subName.ToLower().Contains(softname.ToLower()))
                                return true;
                    }
                }

                return false;
            }

#endif
        }
    }
}

using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace AIO
{
    partial class PrCmd
    {
        /// <summary>
        /// 给文件或目录设置属性命令
        /// </summary>
        public static class Icacls
        {
            /// <param name="username">用户名</param>
            /// <param name="target">目标文件</param>
            /// <param name="args">参数说明:</param>
            /// N - 无访问权限
            /// F - 完全访问权限
            /// M - 修改权限
            /// RX - 读取和执行权限
            /// R - 只读权限
            /// W - 只写权限
            /// D - 删除权限
            /// 在括号中以逗号分隔的特定权限列表:
            /// DE - 删除
            /// RC - 读取控制
            /// WDAC - 写入 DAC
            /// WO - 写入所有者
            /// S - 同步
            /// AS - 访问系统安全性
            /// MA - 允许的最大值
            /// GR - 一般性读取
            /// GW - 一般性写入
            /// GE - 一般性执行
            /// GA - 全为一般性
            /// RD - 读取数据/列出目录
            /// WD - 写入数据/添加文件
            /// AD - 附加数据/添加子目录
            /// REA - 读取扩展属性
            /// WEA - 写入扩展属性
            /// X - 执行/遍历
            /// DC - 删除子项
            /// RA - 读取属性
            /// WA - 写入属性
            /// <returns></returns>
            public static IExecutor Executor(string username, string target, string args = "F")
            {
                if (!File.Exists(target) && !Directory.Exists(target))
                    throw new FileNotFoundException("目标文件或目录不存在", target);
                return Create().Input($"Icacls \"{target.Replace("/", "\\")}\" /grant {username}:{args}");
            }

            /// <param name="username">用户名</param>
            /// <param name="target">目标文件</param>
            /// <param name="args">参数说明:</param>
            /// N - 无访问权限
            /// F - 完全访问权限
            /// M - 修改权限
            /// RX - 读取和执行权限
            /// R - 只读权限
            /// W - 只写权限
            /// D - 删除权限
            /// 在括号中以逗号分隔的特定权限列表:
            /// DE - 删除
            /// RC - 读取控制
            /// WDAC - 写入 DAC
            /// WO - 写入所有者
            /// S - 同步
            /// AS - 访问系统安全性
            /// MA - 允许的最大值
            /// GR - 一般性读取
            /// GW - 一般性写入
            /// GE - 一般性执行
            /// GA - 全为一般性
            /// RD - 读取数据/列出目录
            /// WD - 写入数据/添加文件
            /// AD - 附加数据/添加子目录
            /// REA - 读取扩展属性
            /// WEA - 写入扩展属性
            /// X - 执行/遍历
            /// DC - 删除子项
            /// RA - 读取属性
            /// WA - 写入属性
            /// <returns></returns>
            public static IExecutor Executor(string username, FileInfo target, string args = "F")
            {
                if (!target.Exists) throw new FileNotFoundException("目标文件不存在", target.FullName);
                return Create().Input($"Icacls \"{target.FullName}\" /grant {username}:{args}");
            }

            /// <param name="username">用户名</param>
            /// <param name="target">目标文件</param>
            /// <param name="args">参数说明:</param>
            /// N - 无访问权限
            /// F - 完全访问权限
            /// M - 修改权限
            /// RX - 读取和执行权限
            /// R - 只读权限
            /// W - 只写权限
            /// D - 删除权限
            /// 在括号中以逗号分隔的特定权限列表:
            /// DE - 删除
            /// RC - 读取控制
            /// WDAC - 写入 DAC
            /// WO - 写入所有者
            /// S - 同步
            /// AS - 访问系统安全性
            /// MA - 允许的最大值
            /// GR - 一般性读取
            /// GW - 一般性写入
            /// GE - 一般性执行
            /// GA - 全为一般性
            /// RD - 读取数据/列出目录
            /// WD - 写入数据/添加文件
            /// AD - 附加数据/添加子目录
            /// REA - 读取扩展属性
            /// WEA - 写入扩展属性
            /// X - 执行/遍历
            /// DC - 删除子项
            /// RA - 读取属性
            /// WA - 写入属性
            /// <returns></returns>
            public static IExecutor Executor(string username, IEnumerable<string> target, string args = "F")
            {
                IExecutor current = null;
                IExecutor first = null;
                foreach (var s in target)
                {
                    var info = new FileInfo(s);
                    if (!info.Exists) continue;
                    var executor = Create().Input($"Icacls \"{info.FullName}\" /grant {username}:{args}");
                    if (first == null) first = executor;
                    current?.Link(executor);
                    current = executor;
                }

                return first;
            }

            /// <param name="username">用户名</param>
            /// <param name="target">目标文件</param>
            /// <param name="args">参数说明:</param>
            /// N - 无访问权限
            /// F - 完全访问权限
            /// M - 修改权限
            /// RX - 读取和执行权限
            /// R - 只读权限
            /// W - 只写权限
            /// D - 删除权限
            /// 在括号中以逗号分隔的特定权限列表:
            /// DE - 删除
            /// RC - 读取控制
            /// WDAC - 写入 DAC
            /// WO - 写入所有者
            /// S - 同步
            /// AS - 访问系统安全性
            /// MA - 允许的最大值
            /// GR - 一般性读取
            /// GW - 一般性写入
            /// GE - 一般性执行
            /// GA - 全为一般性
            /// RD - 读取数据/列出目录
            /// WD - 写入数据/添加文件
            /// AD - 附加数据/添加子目录
            /// REA - 读取扩展属性
            /// WEA - 写入扩展属性
            /// X - 执行/遍历
            /// DC - 删除子项
            /// RA - 读取属性
            /// WA - 写入属性
            /// <returns></returns>
            public static IExecutor Executor(string username, IEnumerable<FileInfo> target, string args = "F")
            {
                IExecutor current = null;
                IExecutor first = null;
                foreach (var info in target)
                {
                    if (!info.Exists) continue;
                    var executor = Create().Input($"Icacls \"{info.FullName}\" /grant {username}:{args}");
                    if (first == null) first = executor;
                    current?.Link(executor);
                    current = executor;
                }

                return first;
            }
        }
    }
}
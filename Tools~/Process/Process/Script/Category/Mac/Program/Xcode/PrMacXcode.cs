using System;
using System.IO;
using System.Text;

namespace AIO
{
    /// <summary>
    /// 构建配置
    /// </summary>
    public enum Configuration
    {
        /// <summary>
        ///
        /// </summary>
        Debug,

        /// <summary>
        ///
        /// </summary>
        Release,

        /// <summary>
        ///
        /// </summary>
        ReleaseForProfiling,

        /// <summary>
        ///
        /// </summary>
        ReleaseForRunning,
    }

    public sealed partial class PrMac
    {
        /// <summary>
        /// Xcode 进程命令
        /// </summary>
        public sealed partial class Xcode
        {
            /// <summary>
            /// 
            /// </summary>
            public const string Format_Clean = "clean -project {0} -configuration {1} -alltargets";

            /// <summary>
            /// 
            /// </summary>
            public const string Format_iBTool = "ibtool --generate-strings-file Localizable.strings en.lpoj/Interface.xib";

            /// <summary>
            /// 
            /// </summary>
            public const string Format_ShowBuildSettings = "{0} -target {1} -configuration {2} -showBuildSettings";

            /// <summary>
            /// 
            /// </summary>
            public const string Format_Archive = "archive -project {0} -scheme {1} -sdk {2} {3} -configuration {4} -archivePath {5} -allowProvisioningUpdates {6}";

            /// <summary>
            /// 
            /// </summary>
            public const string Format_ExportArchive = "-exportArchive -archivePath {0} -exportOptionsPlist {1} -exportPath {2} {3}";

            /// <summary>
            /// 
            /// </summary>
            public const string Format_Test = "test -project {0} -scheme {1} -configuration {2} -archivePath {3} -destination {4} -derivedDataPath {5} {6}";

            /// <summary>
            /// 
            /// </summary>
            public const string Format_TestCacheBuilding = "test-without-building -workspace {0} -scheme {1} -destination {2} -only-testing {3}";

            /// <summary>
            /// 清空
            /// </summary>
            public static IExecutor Clean(string project, Configuration configuration)
            {
                if (project is null) throw new ArgumentNullException(nameof(project));
                if (!project.Contains(".xcodeproj")) throw new FileNotFoundException($"[PrMac Xcode] not find file : {project}");
                return Create(CMD_Xcodebuild, string.Format(Format_Clean, project, configuration));
            }

            /// <summary>
            /// 打包
            /// </summary>
            /// <param name="project">项目路径</param>
            /// <param name="scheme">项目名称</param>
            /// <param name="workspace">项目名称.xcworkspace</param>
            /// <param name="archivePath">Archive包存储路径</param>
            /// <param name="configuration">构建配置</param>
            /// <param name="sdkName"></param>
            /// <param name="quiet"></param>
            public static IExecutor Archive(string project, string scheme, string archivePath, string workspace, Configuration configuration, string sdkName = "iphoneos", bool quiet = true)
            {
                if (string.IsNullOrEmpty(project)) throw new ArgumentException($"“{nameof(project)}”不能为 null 或空。", nameof(project));
                if (string.IsNullOrEmpty(scheme)) throw new ArgumentException($"“{nameof(scheme)}”不能为 null 或空。", nameof(scheme));
                if (string.IsNullOrEmpty(archivePath)) throw new ArgumentException($"“{nameof(archivePath)}”不能为 null 或空。", nameof(archivePath));
                if (string.IsNullOrEmpty(sdkName)) throw new ArgumentException($"“{nameof(sdkName)}”不能为 null 或空。", nameof(sdkName));
                if (!project.Contains(".xcodeproj")) throw new Exception($"[PrMac Xcode] not find file : {project}");

                var cmd = string.Format(Format_Archive, project, scheme, sdkName, string.IsNullOrEmpty(workspace) ? "" : workspace, configuration, archivePath, quiet ? "-quiet" : "");
                return Create(CMD_Xcodebuild, cmd);
            }

            /// <summary>
            /// 导出IPA
            /// </summary>
            /// <param name="archivePath">archive生成的产物路径</param>
            /// <param name="exportPath">要导出ipa的路径</param>
            /// <param name="exportOptionsPlist">导出时的配置信息路径</param>
            /// <param name="allowProvisioningUpdates">允许xcodebuild与苹果网站通讯，进行自动签名，证书自动更新，生成。</param>
            public static IExecutor ArchiveExport(string archivePath, string exportPath, string exportOptionsPlist, bool allowProvisioningUpdates = false)
            {
                if (string.IsNullOrEmpty(archivePath)) throw new ArgumentException($"“{nameof(archivePath)}”不能为 null 或空。", nameof(archivePath));
                if (string.IsNullOrEmpty(exportPath)) throw new ArgumentException($"“{nameof(exportPath)}”不能为 null 或空。", nameof(exportPath));
                if (string.IsNullOrEmpty(exportOptionsPlist)) throw new ArgumentException($"“{nameof(exportOptionsPlist)}”不能为 null 或空。", nameof(exportOptionsPlist));
                if (!archivePath.Contains(".xcarchive")) throw new Exception($"[PrMac Xcode] not find file : {archivePath}");

                var cmd = string.Format(Format_ExportArchive, archivePath, exportOptionsPlist, exportPath, allowProvisioningUpdates ? "-allowProvisioningUpdates" : "");
                return Create(CMD_Xcodebuild, cmd);
            }

            /// <summary>
            /// 导出IPA
            /// </summary>
            public static IExecutor Test(string project, string targetName, Configuration buildType, string archivePath, string destination, string derivedDataPath, bool quiet = true)
            {
                var cmd = string.Format(Format_Test, project, targetName, buildType, archivePath, destination, derivedDataPath, quiet ? "-quiet" : "");
                return Create(CMD_Xcodebuild, cmd);
            }

            /// <summary>
            /// UI测试/单元测试
            /// </summary>
            /// 不进行代码编译，利用上次编译的缓存（包括工程编译+测试用例编译），进行重新跑测试。
            public static IExecutor TestCacheBuilding(string workspace, string scheme, string destination, string testing)
            {
                var cmd = string.Format(Format_TestCacheBuilding, workspace, scheme, destination, testing);
                return Create(CMD_Xcodebuild, cmd);
            }

            /// <summary>
            /// 本地化命令
            /// </summary>
            /// <see url="https://www.jianshu.com/p/ecfab7e13aba"/>
            /// 本地化命令，根据指定的C/Object-C源文件生成.strings文件。
            public static IExecutor Genstrings(string folder, string suffix)
            {
                const string format = "genstrings -a {0}*{1}";
                var cmd = string.Format(format, folder, suffix);
                return Create(CMD_Xcodebuild, cmd);
            }

            /// <summary>
            /// 本地化命令
            /// </summary>
            /// 本地化命令，作用于xib文件。
            public static IExecutor iBTool(string folder, string suffix)
            {
                // var cmd = string.Format(format, folder, suffix);
                return Create(CMD_Xcodebuild, Format_iBTool);
            }

            /// <summary>
            /// 查看项目设置
            /// </summary>
            public static IExecutor ShowBuildSettings(string target, Configuration configuration)
            {
                var cmd = string.Format(Format_ShowBuildSettings, CMD_Xcodebuild, target, configuration);
                return Create(CMD_Xcodebuild, cmd);
            }

            /// <summary>
            /// Shell 命令
            /// </summary>
            private static StringBuilder ShellCommand()
            {
                var str = new StringBuilder();
                str.AppendLine("#!/bin/zsh");
                str.AppendLine("PATH=/bin:/sbin:/usr/bin:/usr/sbin:/usr/local/bin:/usr/local/sbin:~/bin");
                str.AppendLine("export PATH");
                str.AppendLine("echo $\"┌───────────────────────────────────┐\"");
                str.AppendLine("echo $\"|Author      :XINAN                 |\"");
                str.AppendLine("echo $\"|E-MAIL      :1398581458@qq.com     |\"");
                str.AppendLine("echo $\"|Description :Automatic Generation  |\"");
                str.AppendLine("echo $\"└───────────────────────────────────┘\"");
                str.AppendLine("echo");
                str.AppendLine("current=$(cd $(dirname $0); pwd)");
                return str.AppendLine();
            }

            /// <summary>
            /// 获取清空指令
            /// </summary>
            /// <param name="project">.xcodeproj文件 路径</param>
            /// <param name="configuration">构建类型</param>
            /// <returns>Command</returns>
            public static string GetCleanShellCommand(string project, string configuration)
            {
                var str = ShellCommand();
                str.Append("cd ..").AppendLine();
                str.Append(CMD_Xcodebuild).Append(' ').Append(string.Format(Format_Clean, project, configuration));
                return str.ToString();
            }

            /// <summary>
            /// 导出IPA
            /// </summary>
            /// <param name="archivePath">archive生成的产物路径</param>
            /// <param name="exportPath">要导出ipa的路径</param>
            /// <param name="exportOptionsPlist">导出时的配置信息路径</param>
            /// <param name="allowProvisioningUpdates"></param>
            public static string GetArchiveExportShellCommand(string archivePath, string exportPath, string exportOptionsPlist, bool allowProvisioningUpdates = false)
            {
                var cmd = string.Format(Format_ExportArchive, archivePath, exportOptionsPlist, exportPath, allowProvisioningUpdates ? "-allowProvisioningUpdates" : "");
                var str = ShellCommand();
                str.Append("cd ..").AppendLine();
                str.Append(CMD_Xcodebuild).Append(' ').Append(cmd);
                return str.ToString();
            }

            /// <summary>
            /// 打包
            /// </summary>
            /// <param name="project">项目路径</param>
            /// <param name="scheme">项目名称</param>
            /// <param name="workspace">项目名称.xcworkspace</param>
            /// <param name="archivePath">Archive包存储路径</param>
            /// <param name="configuration">构建配置</param>
            /// <param name="sdkName"></param>
            /// <param name="quiet"></param>
            public static string GetArchiveShellCommand(string project, string scheme, string archivePath, string workspace, string configuration, string sdkName = "iphoneos", bool quiet = true)
            {
                var cmd = string.Format(Format_Archive, project, scheme, sdkName, string.IsNullOrEmpty(workspace) ? "" : workspace, configuration, archivePath, quiet ? "-quiet" : "");
                var str = ShellCommand();
                str.Append("cd ..\n").Append(CMD_Xcodebuild).Append(' ').Append(cmd);
                return str.ToString();
            }
        }
    }
}
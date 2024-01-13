/*|✩ - - - - - |||
|||✩ Author:   ||| -> xi nan
|||✩ Date:     ||| -> 2023-06-26

|||✩ - - - - - |*/

namespace AIO
{
    /// <summary>
    /// Unity 接收函数
    /// </summary>
    public class UnityArgsCommand : ArgumentCustom
    {
        /// <summary>
        /// 以批处理模式运行 Unity。请始终将此命令与其他命令行参数结合使用，从而确保不会出现弹出窗口且无需任何人为干预。在执行脚本代码期间发生异常时，资源服务器更新失败时，或其他操作失败时，Unity 将立即退出并返回代码 1。
        /// 请注意，在批处理模式下，Unity 会将其日志输出的最小版本发送到控制台。但是，日志文件仍包含完整的日志信息。当 Editor 打开某个项目时，您无法以批处理模式打开相同的项目；一次只能运行一个 Unity 实例。
        /// 要检查是否正在以批处理模式运行 Editor 或独立平台播放器，请使用 Application.isBatchMode 运算符。
        /// 如果在使用 -batchmode 时还没有导入项目，则目标平台为默认平台。要强制选择其他平台，请使用 -buildTarget 选项。
        /// </summary>
        [Argument("-batchmode", EArgLabel.Bool)]
        public bool batchmode;

        /// <summary>
        /// 在其他命令执行完毕后退出 Unity Editor。这可能导致错误消息被隐藏（但是，它们仍会出现在 Editor.log 文件中）。
        /// </summary>
        [Argument("-quit", EArgLabel.Bool)] public bool quit;

        /// <summary>
        /// 阻止 Unity 显示独立平台播放器崩溃时出现的对话框。希望在自动的构建或测试中运行播放器时（此时不希望对话框提示阻碍自动化过程），此参数非常有用。
        /// </summary>
        [Argument("-silent-crashes", EArgLabel.Bool)]
        public bool silentcrashes;

        /// <summary>
        /// 不生成输出日志。通常，Unity 将 output_log.txt 写入到 Log Files 文件夹中，其中会打印 Debug.Log 输出。
        /// </summary>
        [Argument("-nolog", EArgLabel.Bool)] public bool nolog;

        /// <summary>
        /// 在批处理模式下运行此命令时，不会初始化图形设备。这样，在没有 GPU 的机器上可以运行自动化工作流程。
        /// </summary>
        [Argument("-nographics", EArgLabel.Bool)]
        public bool nographics;

        /// <summary>
        /// 关闭立体渲染。
        /// </summary>
        [Argument("-no-stereo-rendering", EArgLabel.Bool)]
        public bool nostereorendering;

        /// <summary>
        /// 对 CPU 性能分析器启用深度性能分析选项。
        /// </summary>
        [Argument("-deepprofiling", EArgLabel.Bool)]
        public bool deepprofiling;

        /// <summary>
        /// Unity 仅启动一个着色器编译器实例，并将其超时强制为一小时。对于调试着色器编译器问题很有用。
        /// </summary>
        [Argument("-diag-debug-shader-compiler", EArgLabel.Bool)]
        public bool diagdebugshadercompiler;

        /// <summary>
        /// 启用代码覆盖率并允许访问 Coverage API。
        /// </summary>
        [Argument("-enableCodeCoverage", EArgLabel.Bool)]
        public bool enableCodeCoverage;

        /// <summary>
        /// 允许详细调试。所有设置都允许选择 None、Script Only 和 Full。（例如，-stackTraceLogType Full）
        /// </summary>
        [Argument("-stackTraceLogType", EArgLabel.String)]
        public string stackTraceLogType;

        /// <summary>
        /// 执行函数
        /// </summary>
        [Argument("-executeMethod", EArgLabel.StringArray)]
        public string[] executeMethod;

        /// <summary>
        /// 执行函数
        /// </summary>
        [Argument("-executeMethodArgs", EArgLabel.StringArray)]
        public string[] executeMethodArgs;

        /// <summary>
        /// 项目路径
        /// </summary>
        [Argument("-projectPath", EArgLabel.String)]
        public string projectPath;

        /// <summary>
        /// 构建 64 位独立平台 Linux 播放器（例如，-buildLinux64Player path/to/your/build）。
        /// </summary>
        [Argument("-buildLinux64Player", EArgLabel.String)]
        public string buildLinux64Player;

        /// <summary>
        /// 构建 64 位独立平台 Mac OSX 播放器（例如，-buildOSXUniversalPlayer path/to/your/build.app）。
        /// </summary>
        [Argument("-buildOSXUniversalPlayer", EArgLabel.String)]
        public string buildOSXUniversalPlayer;

        /// <summary>
        /// 在加载项目之前选择有效的构建目标。可能的选项包括：
        /// Standalone、Win、Win64、OSXUniversal、Linux64、iOS、Android、
        /// WebGL、XboxOne、PS4、WindowsStoreApps、Switch、tvOS。
        /// </summary>
        [Argument("-buildTarget", EArgLabel.String)]
        public string buildTarget;

        /// <summary>
        /// 构建 32 位独立平台 Windows 播放器（例如，-buildWindowsPlayer path/to/your/build.exe）。
        /// </summary>
        [Argument("-buildWindowsPlayer", EArgLabel.String)]
        public string buildWindowsPlayer;

        /// <summary>
        /// 构建 64 位独立平台 Windows 播放器（例如，-buildWindows64Player path/to/your/build.exe）。
        /// </summary>
        [Argument("-buildWindowsPlayer", EArgLabel.String)]
        public string buildWindows64Player;

        /// <summary>
        /// 在指定路径中创建一个空项目。
        /// </summary>
        [Argument("-createProject", EArgLabel.String)]
        public string createProject;

        /// <summary>
        /// 此参数可以设置 Unity Profiler 在启动时将性能分析数据流式传输到 .raw 文件。对播放器和 Editor 都有效。
        /// </summary>
        [Argument("-profiler-log-file", EArgLabel.String)]
        public string profilerlogfile;

        /// <summary>
        /// 在导入纹理或构建项目之前，将默认纹理压缩设置为所需的格式。这样就不必再使用所需的格式导入纹理。可用的格式为 dxt、pvrtc、atc、etc、etc2 和 astc。
        /// </summary>
        [Argument("-setDefaultPlatformTextureFormat", EArgLabel.String)]
        public string setDefaultPlatformTextureFormat;
    }
}

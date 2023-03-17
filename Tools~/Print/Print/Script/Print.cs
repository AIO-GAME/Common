using System.Diagnostics;

namespace AIO
{
    /// <summary>
    /// 打印输出
    /// </summary>
    public  partial class Print
    {
        /// <summary>
        /// 输出开关
        /// </summary>
        public static bool IsNotOut { get; private set; }

        /// <summary>
        /// 类型
        /// </summary>
        public static int CurOutLevel { get; private set; } = Log | Warn | Error | Exception;

        /// <summary>
        /// 
        /// </summary>
        public const int Log = 1;

        /// <summary>
        /// 
        /// </summary>
        public const int Warn = 2;

        /// <summary>
        /// 
        /// </summary>
        public const int Error = 4;

        /// <summary>
        /// 
        /// </summary>
        public const int Exception = 8;

        /// <summary>
        /// 显示日志
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void SetOutLevel(EPrint type)
        {
            CurOutLevel = type.GetHashCode();
        }

        /// <summary>
        /// 显示日志
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void SetOutLevel(int type)
        {
            CurOutLevel = type;
        }

        /// <summary>
        /// 显示日志
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void Show()
        {
            IsNotOut = false;
        }

        /// <summary>
        /// 关闭日志
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void Close()
        {
            IsNotOut = true;
        }

        /// <summary>
        /// 宏定义
        /// </summary>
        public const string MACRO_DEFINITION = "DEBUG";

        /// <summary>
        /// 
        /// </summary>
        public static bool NoStatus(int status)
        {
            if (CurOutLevel < 0) return true;
            return (CurOutLevel & status) != status;
        }
    }

}
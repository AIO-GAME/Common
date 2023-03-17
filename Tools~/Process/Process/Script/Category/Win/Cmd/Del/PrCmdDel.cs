using System.Collections.Generic;

namespace AIO
{
    public partial class PrCmd
    {
        /// <summary>
        /// 删除 不区分文件与目录
        /// </summary>
        public static class Del
        {
            /// <summary>
            /// 执行
            /// </summary>
            public static IExecutor Execute(in string traget, in string command)
            {
                return Create().Input(string.Format("{0} \"{1}\" {2}", CMD_Del, traget.Replace('/', '\\'), command));
            }

            /// <summary>
            /// 删除 只读文件
            /// </summary>
            public static IExecutor ReadOnly(in string traget)
            {
                return Create().Input(string.Format("{0} \"{1}\" /f /q", CMD_Del, traget.Replace('/', '\\')));
            }

            /// <summary>
            /// 删除 子目录下所有文件
            /// </summary>
            public static IExecutor ALL(in string traget)
            {
                return Create().Input(string.Format("{0} \"{1}\" /f /q /s", CMD_Del, traget.Replace('/', '\\')));
            }

            /// <summary>
            /// 删除 子目录下所有文件
            /// </summary>
            public static IExecutor ALL(in ICollection<string> traget)
            {
                return Create().Input(string.Format("{0} \"{1}\" /f /q /s", CMD_Del, string.Join(" ", traget)));
            }

            /// <summary>
            /// 删除 子目录下所有文件
            /// </summary>
            public static IExecutor ALL(params string[] traget)
            {
                return Create().Input(string.Format("{0} \"{1}\" /f /q /s", CMD_Del, string.Join(" ", traget)));
            }

            /// <summary>
            /// 删除 指定属性文件
            /// </summary>
            public static IExecutor Attributes(in string traget, in string attributes)
            {
                return Create().Input(string.Format("{0} \"{1}\" /f /q /a {2}", CMD_Del, traget.Replace('/', '\\'), attributes));
            }
        }
    }
}
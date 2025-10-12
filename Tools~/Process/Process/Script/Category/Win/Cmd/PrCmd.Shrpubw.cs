// ReSharper disable UnusedMember.Global
// ReSharper disable MemberHidesStaticFromOuterClass

namespace AIO
{
    partial class PrCmd
    {
        #region Nested type: Shrpubw

        /// <summary>
        /// windows 共享文件夹
        /// </summary>
        public static class Shrpubw
        {
            /// <summary>
            /// 创建 windows 共享文件夹
            /// </summary>
            public static IExecutor Create(in string target) { return PrCmd.Create().Input($"{CMD_Shrpubw} /s \"{target.Replace('/', '\\')}\""); }

            /// <summary>
            /// 执行
            /// </summary>
            public static IExecutor Execute(in string messages) { return PrCmd.Create().Input(messages); }
        }

        #endregion
    }
}
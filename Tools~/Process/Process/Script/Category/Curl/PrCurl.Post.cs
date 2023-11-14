/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-11-23                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AIO
{
    public partial class PrCurl
    {
        /// <summary>
        /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体
        /// </summary>
        public sealed class Option : IDisposable
        {
            /// <summary>
            /// 用户名和密码
            /// </summary>
            public string userAndPassword { get; set; } = null;

            /// <summary>
            /// 用户代理
            /// </summary>
            public string userAgent { get; set; } = null;

            /// <summary>
            /// 输出路径
            /// </summary>
            public string output { get; set; } = null;

            /// <summary>
            /// 在输出中是否包含协议头
            /// </summary>
            public bool include { get; set; } = false;

            /// <summary>
            /// 显示更详细
            /// </summary>
            public bool verbose { get; set; } = false;

            /// <summary>
            /// 快速失败，HTTP错误没有输出
            /// </summary>
            public bool fail { get; set; } = false;

            /// <summary>
            /// 静默模式
            /// </summary>
            public bool silent { get; set; } = false;

            /// <summary>
            /// 头部信息
            /// </summary>
            public IDictionary<string, string> Header { get; private set; }

            /// <summary>
            /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体
            /// </summary>
            public Option()
            {
                Header = new Dictionary<string, string>();
            }

            /// <summary>
            /// 添加参数信息
            /// </summary>
            /// <param name="str">容器</param>
            internal void Append(StringBuilder str)
            {
                if (Header != null)
                    foreach (var item in Header)
                    {
                        str.AppendFormat(Usage.Header, item.Key, item.Value);
                    }

                if (!string.IsNullOrEmpty(userAndPassword) && userAndPassword.Contains(":"))
                    str.AppendFormat(Usage.User, userAndPassword);
                if (!string.IsNullOrEmpty(userAgent)) str.AppendFormat(Usage.UserAgent, userAgent);
                if (verbose) str.Append(Usage.Verbose);
                if (fail) str.Append(Usage.Fail);
                if (!string.IsNullOrEmpty(output)) str.AppendFormat(Usage.Output, output);
                if (include) str.Append(Usage.Include);
                if (silent) str.Append(Usage.Silent);
            }

            /// <summary>
            /// 添加头部信息
            /// </summary>
            public void AddContentType(string value = "application/json")
            {
                Header.Add("Content-Type", value);
            }

            /// <summary>
            /// 添加头部信息
            /// </summary>
            public void AddCharset(string value = "UTF-8")
            {
                Header.Add("Charset", value);
            }

            /// <summary>
            /// 添加头部信息
            /// </summary>
            public void AddAuthorization(string value = "bearer")
            {
                Header.Add("Authorization", value);
            }

            /// <inheritdoc />
            public void Dispose()
            {
                Header?.Clear();
                Header = null;
            }
        }


        /// <summary>
        /// HTTP POST data
        /// </summary>
        /// <param name="data">上传数据</param>
        /// <param name="remote">远端地址</param>
        /// <param name="option">可选参数</param>
        /// <returns>结果执行器</returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static IExecutor Post(string remote, string data, Option option = default)
        {
            var str = new StringBuilder();
            str.AppendFormat(Usage.Data, data.Replace("\"", "\\\""));
            str.Append(Usage.XPost);
            (option ?? new Option()).Append(str);
            return Create().SetInArgs(str.Append(" ").Append(remote)).Execute();
        }
    }
}
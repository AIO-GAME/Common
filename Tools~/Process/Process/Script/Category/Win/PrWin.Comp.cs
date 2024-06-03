/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-11-23                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

namespace AIO
{
    public partial class PrWin
    {
        #region Nested type: Comp

        /// <summary>
        /// 比较
        /// </summary>
        public static class Comp
        {
            /// <summary>
            /// 比较 以十进制显示差异
            /// </summary>
            /// <param name="source">指定要比较的第一批文件的位置和名称。</param>
            /// <param name="target">指定要比较的第二批文件的位置和名称。</param>
            /// <param name="args">[/D] [/A] [/L] [/N=number] [/C] [/OFF[LINE]] [/M]</param>
            public static IExecutor Execute(string source, string target, string args)
            {
                return Create(CMD_Comp, "\"{0}\" \"{1}\" {2} {3}", source.Replace('/', '\\'), target.Replace('/', '\\'), args);
            }

            /// <summary>
            /// 比较 以十进制显示差异
            /// </summary>
            /// <param name="source">指定要比较的第一批文件的位置和名称。</param>
            /// <param name="target">指定要比较的第二批文件的位置和名称。</param>
            /// <param name="caseASCII">忽略ASCII大小写</param>
            /// <param name="skipOffline">Ture跳过脱机属性文件</param>
            public static IExecutor Decimal(string source, string target, bool caseASCII = false, bool skipOffline = false)
            {
                return Create(CMD_Comp, "\"{0}\" \"{1}\" /D {2} {3} /M",
                              source.Replace('/', '\\'),
                              target.Replace('/', '\\'),
                              caseASCII ? "/C" : "",
                              skipOffline ? "/OFF[LINE]" : ""
                );
            }

            /// <summary>
            /// 比较 以十进制显示差异
            /// </summary>
            /// <param name="source">指定要比较的第一批文件的位置和名称。</param>
            /// <param name="target">指定要比较的第二批文件的位置和名称。</param>
            /// <param name="caseASCII">忽略ASCII大小写</param>
            /// <param name="skipOffline">Ture跳过脱机属性文件</param>
            public static IExecutor DecimalASCII(string source, string target, bool caseASCII = false, bool skipOffline = false)
            {
                return Create(CMD_Comp, "\"{0}\" \"{1}\" /D /A {2} {3} /M",
                              source.Replace('/', '\\'),
                              target.Replace('/', '\\'),
                              caseASCII ? "/C" : "",
                              skipOffline ? "/OFF[LINE]" : ""
                );
            }

            /// <summary>
            /// 比较 以十进制显示差异
            /// </summary>
            /// <param name="source">指定要比较的第一批文件的位置和名称。</param>
            /// <param name="target">指定要比较的第二批文件的位置和名称。</param>
            /// <param name="caseASCII">忽略ASCII大小写</param>
            /// <param name="skipOffline">Ture跳过脱机属性文件</param>
            public static IExecutor DecimalASCIILineNumbers(string source, string target, bool caseASCII = false, bool skipOffline = false)
            {
                return Create(CMD_Comp, "\"{0}\" \"{1}\" /D /A /L {2} {3} /M",
                              source.Replace('/', '\\'),
                              target.Replace('/', '\\'),
                              caseASCII ? "/C" : "",
                              skipOffline ? "/OFF[LINE]" : ""
                );
            }

            /// <summary>
            /// 比较 以ASCII显示差异
            /// </summary>
            /// <param name="source">指定要比较的第一批文件的位置和名称。</param>
            /// <param name="target">指定要比较的第二批文件的位置和名称。</param>
            /// <param name="caseASCII">忽略ASCII大小写</param>
            /// <param name="skipOffline">Ture跳过脱机属性文件</param>
            public static IExecutor ASCII(string source, string target, bool caseASCII = false, bool skipOffline = false)
            {
                return Create(CMD_Comp, "\"{0}\" \"{1}\" /A {2} {3} /M",
                              source.Replace('/', '\\'),
                              target.Replace('/', '\\'),
                              caseASCII ? "/C" : "",
                              skipOffline ? "/OFF[LINE]" : ""
                );
            }

            /// <summary>
            /// 比较 以ASCII显示差异
            /// </summary>
            /// <param name="source">指定要比较的第一批文件的位置和名称。</param>
            /// <param name="target">指定要比较的第二批文件的位置和名称。</param>
            /// <param name="caseASCII">忽略ASCII大小写</param>
            /// <param name="skipOffline">Ture跳过脱机属性文件</param>
            public static IExecutor ASCIILineNumbers(string source, string target, bool caseASCII = false, bool skipOffline = false)
            {
                return Create(CMD_Comp, "\"{0}\" \"{1}\" /A /L {2} {3} /M",
                              source.Replace('/', '\\'),
                              target.Replace('/', '\\'),
                              caseASCII ? "/C" : "",
                              skipOffline ? "/OFF[LINE]" : ""
                );
            }

            /// <summary>
            /// 比较 以行号显示差异
            /// </summary>
            /// <param name="source">指定要比较的第一批文件的位置和名称。</param>
            /// <param name="target">指定要比较的第二批文件的位置和名称。</param>
            /// <param name="caseASCII">忽略ASCII大小写</param>
            /// <param name="skipOffline">Ture跳过脱机属性文件</param>
            public static IExecutor LineNumbers(string source, string target, bool caseASCII = false, bool skipOffline = false)
            {
                return Create(CMD_Comp, "\"{0}\" \"{1}\" /L {2} {3} /M",
                              source.Replace('/', '\\'),
                              target.Replace('/', '\\'),
                              caseASCII ? "/C" : "",
                              skipOffline ? "/OFF[LINE]" : ""
                );
            }

            /// <summary>
            /// 比较指定行数 以行号显示差异
            /// </summary>
            /// <param name="source">指定要比较的第一批文件的位置和名称</param>
            /// <param name="target">指定要比较的第二批文件的位置和名称</param>
            /// <param name="lineIndex">行数</param>
            /// <param name="caseASCII">忽略ASCII大小写</param>
            /// <param name="skipOffline">Ture跳过脱机属性文件</param>
            public static IExecutor SpecifiedLineNumber(string source, string target, int lineIndex, bool caseASCII = true, bool skipOffline = true)
            {
                return Create(CMD_Comp, "\"{0}\" \"{1}\" /L /N={2}{3} {4} /M",
                              source.Replace('/', '\\'),
                              target.Replace('/', '\\'),
                              lineIndex,
                              caseASCII ? "/C" : "",
                              skipOffline ? "/OFF[LINE]" : ""
                );
            }

            /// <summary>
            /// 比较指定行数 以ASCII显示差异
            /// </summary>
            /// <param name="source">指定要比较的第一批文件的位置和名称</param>
            /// <param name="target">指定要比较的第二批文件的位置和名称</param>
            /// <param name="lineIndex">行数</param>
            /// <param name="caseASCII">忽略ASCII大小写</param>
            /// <param name="skipOffline">Ture跳过脱机属性文件</param>
            public static IExecutor SpecifiedLineASCII(string source, string target, int lineIndex, bool caseASCII = true, bool skipOffline = true)
            {
                return Create(CMD_Comp, "\"{0}\" \"{1}\" /A /N={2}{3} {4} /M",
                              source.Replace('/', '\\'),
                              target.Replace('/', '\\'),
                              lineIndex,
                              caseASCII ? "/C" : "",
                              skipOffline ? "/OFF[LINE]" : ""
                );
            }

            /// <summary>
            /// 比较指定行数 以十进制显示差异
            /// </summary>
            /// <param name="source">指定要比较的第一批文件的位置和名称</param>
            /// <param name="target">指定要比较的第二批文件的位置和名称</param>
            /// <param name="lineIndex">行数</param>
            /// <param name="caseASCII">忽略ASCII大小写</param>
            /// <param name="skipOffline">Ture跳过脱机属性文件</param>
            public static IExecutor SpecifiedLineDecimal(string source, string target, int lineIndex, bool caseASCII = true, bool skipOffline = true)
            {
                return Create(CMD_Comp, "\"{0}\" \"{1}\" /A /N={2}{3} {4} /M",
                              source.Replace('/', '\\'),
                              target.Replace('/', '\\'),
                              lineIndex,
                              caseASCII ? "/C" : "",
                              skipOffline ? "/OFF[LINE]" : ""
                );
            }
        }

        #endregion
    }
}
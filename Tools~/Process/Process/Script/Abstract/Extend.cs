/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-11-23                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

#region

using System;
using System.Diagnostics;

#endregion

namespace AIO
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class ResultExtend
    {
        /// <summary>
        /// 输出日志
        /// </summary>
        /// <param name="ret">执行结果</param>
        /// <exception cref="Exception">异常信息</exception>
        [DebuggerHidden, DebuggerNonUserCode]
        public static void Debug(this IResult ret)
        {
            if (!PrCourse.IsLog || ret == null) return;
            if (ret is ResultEmpty)
            {
                Console.WriteLine(ret.StdALL);
                return;
            }

            if (PrCourse.IsCache && !string.IsNullOrEmpty(ret.StdError.ToString()))
                throw new Exception(ret.StdError.ToString());
            Console.WriteLine(ret.StdALL);
        }
    }
}
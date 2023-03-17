/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-11-23                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;

namespace AIO
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class Extend
    {
        /// <summary>
        /// 输出日志
        /// </summary>
        /// <param name="ret">执行结果</param>
        /// <exception cref="Exception">异常信息</exception>
        public static void Debug(this IResult ret)
        {
            if (!PrCourse.IsLog || ret == null) return;
            if (ret is ResultEmpty)
            {
                if (ret.Next != null) Debug(ret.Next);
                return;
            }

            if (PrCourse.IsCache && !string.IsNullOrEmpty(ret.StdError.ToString()))
                throw new Exception(ret.StdError.ToString());
            else Console.WriteLine(ret.StdALL);

            if (ret.Next != null) Debug(ret.Next);
        }
    }
}
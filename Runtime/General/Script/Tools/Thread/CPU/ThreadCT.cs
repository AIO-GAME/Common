/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


using System;
using System.Threading;

namespace AIO.Core.Runtime
{
    /// <summary>
    /// 子线程自定义任务
    /// 模板构造器
    /// </summary>
    public class ThreadCT : ThreadCPU
    {
        public ThreadCT(string name, Action Update = null, Action Execute = null, Action Exception = null) : base(Update, Execute, Exception)
        {
            ThreadAttribute attribute;
            attribute.Name = string.Concat(name, thread.ManagedThreadId);
            attribute.Priority = ThreadPriority.Normal;
            attribute.IsBackground = true;
            ThreadSetting(attribute);
        }

        protected override void ThreadSetting(ThreadAttribute attribute)
        {
            thread.Name = string.Concat(attribute.Name, thread.ManagedThreadId);
            thread.Priority = attribute.Priority;
            thread.IsBackground = attribute.IsBackground;
        }
    }
}
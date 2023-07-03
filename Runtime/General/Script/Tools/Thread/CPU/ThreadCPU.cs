/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


using System;
using System.Diagnostics;
using System.Threading;
using ThreadState = System.Threading.ThreadState;

namespace AIO.Core.Runtime
{
    internal interface IThread
    {
        /// <summary>
        /// 子线程执行逻辑
        /// </summary>
        void TUpdate();

        /// <summary>
        /// 执行完成
        /// </summary>
        void TExecute();

        /// <summary>
        /// 异常错误
        /// </summary>
        void TException();
    }

    /// <summary>
    /// 线程属性
    /// </summary>
    public struct ThreadAttribute
    {
        public string Name;
        public ThreadPriority Priority;
        public bool IsBackground;
    }

    /// <summary>
    /// CPU子线程 任务执行器
    /// </summary>
    public abstract class ThreadCPU : IThread, IDisposable
    {
        /// <summary>
        /// 线程
        /// </summary>
        public Thread thread { get; protected set; }

        /// <summary>
        /// 时间码表
        /// </summary>
        protected Stopwatch timewatch;

        /// <summary>
        /// 线程开启时间 单位毫秒
        /// </summary>
        public ulong StartTime { get; private set; }

        /// <summary>
        /// 线程结束时间
        /// </summary>
        public ulong EndTime { get; private set; }

        /// <summary>
        /// 计算方法
        /// </summary>
        protected Action Update { get; set; }

        /// <summary>
        /// 完成方法
        /// </summary>
        protected Action Execute { get; set; }

        /// <summary>
        /// 异常回调
        /// </summary>
        protected Action Exception { get; set; }

        public ThreadCPU()
        {
            thread = new Thread(tUpdate);
        }

        public ThreadCPU(Action Update = null, Action Execute = null, Action Exception = null)
        {
            thread = new Thread(tUpdate);
            this.Update = Update;
            this.Execute = Execute;
            this.Exception = Exception;
        }

        private void tUpdate()
        {
            try
            {
                start:
                if (thread.IsAlive)
                {
                    //如果为存活 则执行
                    TUpdate();
                    if (Loop == false)
                        TExecute();
                }
                else TException();

                if (Loop)
                {
                    timewatch = Stopwatch.StartNew();
                    while (Loop)
                    {
                        if (timewatch.ElapsedMilliseconds >= IntervalTime)
                        {
                            goto start;
                        }
                    }
                }
                else
                {
                    EndTime = (ulong)DateTime.Now.Ticks;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// 开启线程
        /// </summary>
        public void Start()
        {
            if (!thread.IsAlive)
            {
                thread.Start();
                StartTime = (ulong)DateTime.Now.Ticks;
            }
        }

        /// <summary>
        /// 获取线程存活时间
        /// </summary>
        public ulong GetAliveTime()
        {
            if (thread.ThreadState == ThreadState.Aborted ||
                thread.ThreadState == ThreadState.Stopped)
            {
                return EndTime - StartTime;
            }
            else return (ulong)DateTime.Now.Millisecond - StartTime;
        }

        /// <summary>
        /// 判断当前线程是否存活
        /// </summary>
        public bool IsAlive()
        {
            return thread.IsAlive;
        }

        private bool isAwait;

        /// <summary>
        /// 线程等待 自旋转
        /// </summary>
        public void TAwait()
        {
            isAwait = true;
            lock (this)
            {
                while (isAwait)
                {
                    if (isAwait == false) return;
                }
            }
        }

        /// <summary>
        /// 线程解除自旋
        /// </summary>
        public void TDone()
        {
            isAwait = false;
        }

        /// <summary>
        /// 调用Thread.Abort方法试图强制终止thread线程
        /// 线程设计不建议使用该方法 推荐状态开关
        /// </summary>
        public void Abort()
        {
            thread.Abort();
            while (thread.ThreadState != ThreadState.Aborted)
            {
                //当调用Abort方法后，如果thread线程的状态不为Aborted，线程就一直在这里做循环，直到thread线程的状态变为Aborted为止
                Thread.Sleep(100);
            }

            EndTime = (ulong)DateTime.Now.Ticks;
        }

        /// <summary>
        /// 线程循环执行计算
        /// </summary>
        public bool Loop { get; private set; }

        /// <summary>
        /// 循环计算间隔时间 单位s
        /// </summary>
        public long IntervalTime { get; private set; }

        /// <summary>
        /// 设置为循环执行
        /// </summary>
        /// <param name="time">间隙时间</param>
        public void StartLoop(long time)
        {
            Loop = true;
            IntervalTime = time;
            Start();
        }

        /// <summary>
        /// 结束循环
        /// </summary>
        public void EndLoop()
        {
            Loop = false;
        }

        /// <summary>
        /// 线程设置
        /// </summary>
        protected virtual void ThreadSetting(ThreadAttribute attribute)
        {
        }

        /// <summary>
        /// 子线程执行逻辑
        /// </summary>
        public virtual void TUpdate()
        {
            Update?.Invoke();
        }

        /// <summary>
        /// 执行完成
        /// </summary>
        public virtual void TExecute()
        {
            if (Execute != null) Execute.Invoke();
            else Console.WriteLine($"Execute Succeed Thread Name : {thread.Name}");
        }

        /// <summary>
        /// 异常错误
        /// </summary>
        public virtual void TException()
        {
            if (Exception != null) Exception.Invoke();
            else Console.WriteLine($"Execute Fail Thread Name :{thread.Name}");
        }

        public void Dispose()
        {
            isAwait = Loop = false;
            thread.Abort();
            thread = null;
            Execute = Update = Exception = null;
        }
    }
}
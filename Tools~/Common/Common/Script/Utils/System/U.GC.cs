/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


using System;
using System.Runtime.ConstrainedExecution;
using System.Security;
using SGC = System.GC;

public partial class Utils
{
    /*
     1、只管理内存，非托管资源，如文件句柄，GDI资源，数据库连接等还需要用户去管理。

　　  2、循环引用，网状结构等的实现会变得简单。GC的标志-压缩算法能有效的检测这些关系，并将不再被引用的网状结构整体删除。

　　  3、GC通过从程序的根对象开始遍历来检测一个对象是否可被其他对象访问，而不是用类似于COM中的引用计数方法。

　　  4、GC在一个独立的线程中运行来删除不再被引用的内存。

　　  5、GC每次运行时会压缩托管堆。

　　  6、你必须对非托管资源的释放负责。可以通过在类型中定义Finalizer来保证资源得到释放。

　　  7、对象的Finalizer被执行的时间是在对象不再被引用后的某个不确定的时间。注意并非和C++中一样在对象超出声明周期时立即执行析构函数

　　  8、Finalizer的使用有性能上的代价。需要Finalization的对象不会立即被清除，而需要先执行Finalizer.Finalizer，不是在GC执行的线程被调用。
        GC把每一个需要执行Finalizer的对象放到一个队列中去，然后启动另一个线程来执行所有这些Finalizer，
        而GC线程继续去删除其他待回收的对象。在下一个GC周期，这些执行完Finalizer的对象的内存才会被回收。

　　  9、.NET GC使用"代"(generations)的概念来优化性能。代帮助GC更迅速的识别那些最可能成为垃圾的对象。
        在上次执行完垃圾回收后新创建的对象为第0代对象。经历了一次GC周期的对象为第1代对象。经历了两次或更多的GC周期的对象为第2代对象。
        代的作用是为了区分局部变量和需要在应用程序生存周期中一直存活的对象。
        大部分第0代对象是局部变量。成员变量和全局变量很快变成第1代对象并最终成为第2代对象。

　　  10、GC对不同代的对象执行不同的检查策略以优化性能。每个GC周期都会检查第0代对象。
        大约1/10的GC周期检查第0代和第1代对象。大约1/100的GC周期检查所有的对象。
        重新思考Finalization的代价：需要Finalization的对象可能比不需要Finalization在内存中停留额外9个GC周期。
        如果此时它还没有被Finalize，就变成第2代对象，从而在内存中停留更长时间。
    */
    /// <see>
    ///     <cref>https://docs.microsoft.com/zh-cn/dotnet/api/system.gc?view=net-5.0</cref>
    /// </see>
    /// <!--公共语言运行时中的垃圾回收器支持使用代的对象老化。 代是内存中对象的相对生存期的单位。
    /// 对象的代数或 age 指示对象所属的代。 最近创建的对象是较新的生成的一部分，其生成号比之前在应用程序生命周期中创建的对象的生成号要低。
    /// 最近一代中的对象位于第0代中。 垃圾回收器的这种实现支持三代对象，第0代、第1代和第2代。
    /// 可以检索属性的值 MaxGeneration ，以确定系统支持的最大代数。-->
    public static class GC
    {
        /* 代是内存中对象的相对生存期的单位。 */

        /// <summary>
        /// 获取系统当前支持的最大代数
        /// </summary>
        public static int MaxGeneration => SGC.MaxGeneration;

        /// <summary>
        /// 阻止GC调用Finalize方法
        /// </summary>
        /// <!--因为Finalize方法的调用会牺牲部分性能。如果你的Dispose方法已经对委托管资源作了清理，就没必要让GC再调用对象的Finalize方法-->
        [SecurityCritical]
        public static void SuppressFinalize(object obj)
        {
            SGC.SuppressFinalize(obj);
        }

        #region Collect

        /// <summary>
        /// 强制对所有代进行即时垃圾回收
        /// </summary>
        [SecurityCritical]
        public static void Collect()
        {
            SGC.Collect();
        }

        /// <summary>
        /// 强制对 0 代到指定代进行即时垃圾回收。
        /// </summary>
        /// <param name="generation">代</param>
        [SecurityCritical]
        public static void Collect(int generation)
        {
            SGC.Collect(generation);
        }

        /// <summary>
        /// 强制在 GCCollectionMode 值所指定的时间对 0 代到指定代进行垃圾回收。
        /// </summary>
        /// <param name="generation">代</param>
        /// <param name="gCCollectionMode">GC模式集合</param>
        [SecurityCritical]
        public static void Collect(int generation, GCCollectionMode gCCollectionMode)
        {
            SGC.Collect(generation, gCCollectionMode);
        }

        /// <summary>
        /// 在由 GCCollectionMode 值指定的时间，强制对 0 代到指定代进行垃圾回收，另有数值指定回收是否应该为阻碍性。
        /// </summary>
        /// <param name="generation">代</param>
        /// <param name="gCCollectionMode">GC模式集合</param>
        /// <param name="blocking">阻塞</param>
        [SecurityCritical]
        public static void Collect(int generation, GCCollectionMode gCCollectionMode, bool blocking)
        {
            SGC.Collect(generation, gCCollectionMode, blocking);
        }

        /// <summary>
        /// 在由 GCCollectionMode 值指定的时间，强制对 0 代到指定代进行垃圾回收，另有数值指定回收应该为阻碍性还是压缩性。
        /// </summary>
        /// <param name="generation">代</param>
        /// <param name="gCCollectionMode">GC模式集合</param>
        /// <param name="blocking">阻塞</param>
        /// <param name="compacting">压缩</param>
        [SecurityCritical]
        public static void Collect(int generation, GCCollectionMode gCCollectionMode, bool blocking, bool compacting)
        {
            SGC.Collect(generation, gCCollectionMode, blocking, compacting);
        }

        /// <summary>
        /// 返回已经对对象的指定代进行的垃圾回收次数。
        /// </summary>
        /// <param name="generation">代</param>
        [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static void CollectionCount(int generation)
        {
            SGC.CollectionCount(generation);
        }

        #endregion

        /// <summary>
        /// 取消注册垃圾回收通知。
        /// </summary>
        [SecurityCritical]
        public static void CancelFullGCNotification()
        {
            SGC.CancelFullGCNotification();
        }

        /// <summary>
        /// 通知运行时在安排垃圾回收时应考虑分配大量的非托管内存
        /// </summary>
        /// <param name="bytesAllocated">分配的字节数</param>
        [SecurityCritical]
        public static void AddMemoryPressure(long bytesAllocated)
        {
            SGC.AddMemoryPressure(bytesAllocated);
        }

        /// <summary>
        /// 通知运行时已释放非托管内存，在安排垃圾回收时不需要再考虑它。
        /// </summary>
        /// <param name="bytesAllocated">分配的字节数</param>
        [SecurityCritical]
        public static void RemoveMemoryPressure(long bytesAllocated)
        {
            SGC.RemoveMemoryPressure(bytesAllocated);
        }

        #region 创建 关闭 无CG模式

        /// <summary>
        /// 如果指定数量的内存可用，则在关键路径执行期间尝试禁止垃圾回收。
        /// </summary>
        /// <param name="totalSize">总空间</param>
        /// <returns>是否成功</returns>
        [SecurityCritical]
        public static bool TryStartNoGCRegion(long totalSize)
        {
            return SGC.TryStartNoGCRegion(totalSize);
        }

        /// <summary>
        /// 如果指定数量的内存可用，则在关键路径执行期间尝试禁止垃圾回收；
        /// 并在初始没有足够内存可用的情况下，
        /// 控制垃圾回收器是否进行完整的阻碍性垃圾回收。
        /// </summary>
        /// <param name="totalSize">总空间</param>
        /// <param name="disallowFullBlockingGC">禁止完全阻塞GC</param>
        /// <returns>是否成功</returns>
        [SecurityCritical]
        public static bool TryStartNoGCRegion(long totalSize, bool disallowFullBlockingGC)
        {
            return SGC.TryStartNoGCRegion(totalSize, disallowFullBlockingGC);
        }

        /// <summary>
        /// 如果指定数量的内存可用于大对象堆和小对象堆，则在关键路径执行期间尝试禁止垃圾回收。
        /// </summary>
        /// <param name="totalSize">总空间</param>
        /// <param name="lohSize">大对象堆</param>
        /// <returns>是否成功</returns>
        [SecurityCritical]
        public static bool TryStartNoGCRegion(long totalSize, long lohSize)
        {
            return SGC.TryStartNoGCRegion(totalSize, lohSize);
        }

        /// <summary>
        /// 如果指定数量的内存可用大对象堆和小对象堆，则在关键路径执行期间尝试禁止垃圾回收；
        /// 并在初始没有足够内存可用的情况下，控制垃圾回收器是否进行完整的阻碍性垃圾回收。
        /// </summary>
        /// <param name="totalSize">总空间</param>
        /// <param name="lohSize">大对象堆</param>
        /// <param name="disallowFullBlockingGC">禁止完全阻塞GC</param>
        /// <returns>是否成功</returns>
        [SecurityCritical]
        public static bool TryStartNoGCRegion(long totalSize, long lohSize, bool disallowFullBlockingGC)
        {
            return SGC.TryStartNoGCRegion(totalSize, lohSize, disallowFullBlockingGC);
        }

        /// <summary>
        /// 结束无 SGC 区域延迟模式
        /// </summary>
        [SecurityCritical]
        public static void EndNoGCRegion()
        {
            SGC.EndNoGCRegion();
        }

        #endregion

        /// <summary>
        /// 返回指定对象的当前代数。
        /// </summary>
        /// <param name="obj">数据</param>
        /// <returns>代</returns>
        [SecurityCritical]
        public static int GetGeneration(object obj)
        {
            return SGC.GetGeneration(obj);
        }

        /// <summary>
        /// 返回指定弱引用的目标的当前代数。
        /// </summary>
        /// <param name="obj">数据</param>
        /// <returns>代</returns>
        [SecurityCritical]
        public static int GetGeneration(WeakReference obj)
        {
            return SGC.GetGeneration(obj);
        }

        /// <summary>
        /// 检索当前认为要分配的字节数
        /// </summary>
        /// <param name="forceFullCollection">是否可以等待较短间隔再返回，以便系统回收垃圾和终结对象。</param>
        /// <returns>全部内存大小</returns>
        [SecurityCritical]
        public static long GetTotalMemory(bool forceFullCollection)
        {
            return SGC.GetTotalMemory(forceFullCollection);
        }

        /// <summary>
        /// 引用指定对象，使其从当前例程开始到调用此方法的那一刻为止均不符合进行垃圾回收的条件。
        /// </summary>
        /// <param name="obj">指定对象</param>
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static void KeepAlive(object obj)
        {
            SGC.KeepAlive(obj);
        }

        /// <summary>
        /// 注册完整GC通知
        /// </summary>
        /// <param name="maxGenerationThreshold">马克斯代阈值</param>
        /// <param name="largeObjectHeapThreshold">大对象堆阈值</param>
        [SecurityCritical]
        public static void RegisterForFullGCNotification(int maxGenerationThreshold, int largeObjectHeapThreshold)
        {
            SGC.RegisterForFullGCNotification(maxGenerationThreshold, largeObjectHeapThreshold);
        }

        /// <summary>
        /// 请求系统调用指定对象的终结器，此前已为该对象调用 SuppressFinalize(Object)。
        /// </summary>
        /// <param name="obj">数据</param>
        [SecuritySafeCritical]
        public static void ReRegisterForFinalize(object obj)
        {
            SGC.ReRegisterForFinalize(obj);
        }

        /// <summary>
        /// 返回已注册通知的状态，用于确定公共语言运行时是否即将引发完整、阻碍性垃圾回收。
        /// </summary>
        /// <returns>GC通知状态</returns>
        [SecurityCritical]
        public static GCNotificationStatus WaitForFullGCApproach()
        {
            return SGC.WaitForFullGCApproach();
        }

        /// <summary>
        /// 在指定的超时期限内，返回已注册通知的状态，用于确定公共语言运行时是否即将引发完整、阻碍性垃圾回收。
        /// </summary>
        /// <param name="millisecondsTimeout">毫秒超时时间</param>
        /// <returns>GC通知状态</returns>
        [SecurityCritical]
        public static GCNotificationStatus WaitForFullGCApproach(int millisecondsTimeout)
        {
            return SGC.WaitForFullGCApproach(millisecondsTimeout);
        }

        /// <summary>
        /// 返回已注册通知的状态，用于确定公共语言运行时引发的完整、阻碍性垃圾回收是否已完成。
        /// </summary>
        /// <returns>GC通知状态</returns>
        [SecurityCritical]
        public static GCNotificationStatus WaitForFullGCComplete()
        {
            return SGC.WaitForFullGCComplete();
        }

        /// <summary>
        /// 在指定的超时期限内，返回已注册通知的状态，用于确定公共语言运行时引发的完整、阻碍性垃圾回收是否已完成。
        /// </summary>
        /// <param name="millisecondsTimeout">毫秒超时时间</param>
        /// <returns>GC通知状态</returns>
        [SecurityCritical]
        public static GCNotificationStatus WaitForFullGCComplete(int millisecondsTimeout)
        {
            return SGC.WaitForFullGCComplete(millisecondsTimeout);
        }

        /// <summary>
        /// 挂起当前线程，直到处理终结器队列的线程清空该队列为止。
        /// </summary>
        [SecuritySafeCritical]
        public static void WaitForPendingFinalizers()
        {
            SGC.WaitForPendingFinalizers();
        }

        //#region Net-5.0

        ///// <summary>
        ///// 通知运行时在安排垃圾回收时应考虑分配大量的非托管内存
        ///// </summary>
        ///// <param name="bytesAllocated">分配的字节数</param>
        //[SecurityCritical]
        //public static void AllocateArray(int i, bool b)
        //{
        //    SGC.AllocateArray(i, b);
        //}

        //#endregion
    }
}
/*|============|*|
|*|Author:     |*| xinan
|*|Date:       |*| 2023-06-04
|*|E-Mail:     |*| 1398581458@qq.com
|*|============|*/

using System;
using UnityEngine;
#if UNITY_EDITOR_WIN
using System.Runtime.InteropServices;
#endif

namespace AIO.UEditor
{
#if UNITY_EDITOR_WIN

    /// <summary>
    /// 内存信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MEMORY_INFO
    {
        /// <summary>
        ///
        /// </summary>
        public uint dwLength;

        /// <summary>
        /// 系统内存负载
        /// </summary>
        public uint dwMemoryLoad;

        /// <summary>
        /// 系统内存总量
        /// </summary>
        public ulong dwTotalPhys;

        /// <summary>
        /// 系统可用内存
        /// </summary>
        public ulong dwAvailPhys;

        /// <summary>
        /// 系统总页面文件
        /// </summary>
        public ulong dwTotalPageFile;

        /// <summary>
        /// 系统可用显示文件
        /// </summary>
        public ulong dwAvailPageFile;

        /// <summary>
        /// 系统总虚拟内存
        /// </summary>
        public ulong dwTotalVirtual;

        /// <summary>
        /// 系统可用虚拟内存
        /// </summary>
        public ulong dwAvailVirtual;
    }

    /*
        System.ExecutableAndDlls：系统可执行程序和DLL，是只读的内存，用来执行所有的脚本和DLL引用。不同平台和不同硬件得到的值会不一样，可以通过修改Player Setting的Stripping Level来调节大小。
        Ricky：我试着修改了一下Stripping Level似乎没什么改变，感觉虽占用内存大但不会影响游戏运行。我们暂时忽略它吧(- -)!

        GfxClientDevice：GFX（图形加速\图形加速器\显卡 (GraphicsForce Express)）客户端设备。
        Ricky：虽占用较大内存，但这也是必备项，没办法优化。继续忽略吧(- -)!!

        ManagedHeap.UsedSize：托管堆使用大小。
        Ricky：重点监控对象，不要让它超过20MB，否则可能会有性能问题！

        ShaderLab：Unity自带的着色器语言工具相关资源。
        Ricky：这个东西大家都比较熟悉了，忽略它吧。

        SerializedFile：序列化文件，把显示中的Prefab、Atlas和metadata等资源加载进内存。
        Ricky：重点监控对象，这里就是你要监控的哪些预设在序列化中在内存中占用大小，根据需求进行优化。

        PersistentManager.Remapper：持久化数据重映射管理相关
        Ricky：与持久化数据相关，比如AssetBundle之类的。注意监控相关的文件。

        ManagedHeap.ReservedUnusedSize：托管堆预留不使用内存大小，只由Mono使用。
        Ricky：无法优化。
    */

    /*
        Profiler内存重点关注优化项目:
        1）ManagedHeap.UsedSize: 移动游戏建议不要超过20MB.
        2）SerializedFile: 通过异步加载(LoadFromCache、WWW等)的时候留下的序列化文件,可监视是否被卸载.
        3）WebStream: 通过异步WWW下载的资源文件在内存中的解压版本，比SerializedFile大几倍或几十倍，不过我们现在项目中展示没有。
        4）Texture2D: 重点检查是否有重复资源和超大Memory是否需要压缩等.
        5）AnimationClip: 重点检查是否有重复资源.
        6）Mesh： 重点检查是否有重复资源.
    */

    /*
        缓存组件:
        1）每次GetComponent均会分配一定的GC Allow.
        2）每次Object.name都会分配39B的堆内存.

        使用StringBuilder替代字符串直接连接.

        控制StartCoroutine的次数：
        1）开启一个Coroutine(协程)，至少分配37B的内存.
        2）Coroutine类的实例 -> 21B.
        3）Enumerator -> 16B.

        GarbageCollectAssetsProfile:
        1）引擎在执行UnloadUnusedAssets操作（该操作是比较耗时的,建议在切场景的时候进行）。
        2）尽可能地避免使用Unity内建GUI，避免GUI.Repaint过渡GCAllow.
        3）if(other.tag == a.tag)改为other.CompareTag(a.tag).因为other.tag为产生180B的GC Allow.
        4）少用foreach，因为每次foreach为产生一个enumerator(约16B的内存分配)，尽量改为for.
        5）Lambda表达式，使用不当会产生内存泄漏.

        GC.Collect: 原因：
        1）代码分配内存过量(恶性的)
        2）一定时间间隔由系统调用(良性的).
        占用时间：
        1）与现有Garbage size相关
        2）与剩余内存使用颗粒相关（比如场景物件过多，利用率低的情况下，GC释放后需要做内存重排)

        Overhead:
        1）一般情况为Vsync所致.
        2）通常出现在Android设备上.

        Device.Present:
        1）GPU的presentdevice确实非常耗时，一般出现在使用了非常复杂的shader.
        2）GPU运行的非常快，而由于Vsync的原因，使得它需要等待较长的时间.
        3）同样是Vsync的原因，但其他线程非常耗时，所以导致该等待时间很长，比如：过量AssetBundle加载时容易出现该问题.
        4）Shader.CreateGPUProgram:Shader在runtime阶段（非预加载）会出现卡顿(华为K3V2芯片).
        5）StackTraceUtility.PostprocessStacktrace()和StackTraceUtility.ExtractStackTrace(): 一般是由Debug.Log或类似API造成，游戏发布后需将Debug API进行屏蔽。
     */

#endif

    public static partial class UtilsEditor
    {
        /// <summary>
        /// Profiler Editor
        /// </summary>
        public static class Profiler
        {
            /// <summary>
            /// 获取 Texture 磁盘占用大小
            /// </summary>
            /// <returns>占用空间</returns>
            public static long GetStorageMemoryTexture<T>(T obj) where T : Texture
            {
                var method = UtilsGen.Assembly.GetMethodInfo(
                    "UnityEditor.dll",
                    "UnityEditor.TextureUtil",
                    "GetStorageMemorySize");
                return Convert.ToInt64(method.Invoke(null, new object[] { obj }));
            }

#if UNITY_EDITOR_WIN
            [DllImport("kernel32")]
            private static extern void GlobalMemoryStatus(ref MEMORY_INFO meminfo);

            /// <summary>
            /// 获取内存信息
            /// </summary>
            public static MEMORY_INFO GetMemoryInfo()
            {
                var MemInfo = new MEMORY_INFO();
                GlobalMemoryStatus(ref MemInfo);
                return MemInfo;
            }

            /// <summary>
            /// 获取当前程序可用内存
            /// </summary>
            public static long GetMemoryAvailPhys()
            {
                var MemInfo = new MEMORY_INFO();
                GlobalMemoryStatus(ref MemInfo);
                return Convert.ToInt64(MemInfo.dwAvailPhys.ToString()) / 1024 / 1024;
            }
#endif
        }
    }
}

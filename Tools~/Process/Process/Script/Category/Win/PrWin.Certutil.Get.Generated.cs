using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace AIO
{
    partial class PrWin
    {    
        partial class Certutil
        { 
            #region MD5
      
            /// <summary>
            ///  <see cref="MD5"/> 算法 计算 <see cref="target"/>
            /// </summary>
            /// <param name="target"> 目标文件路径 </param>
            /// <returns> 执行器 </returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static IExecutor MD5(string target) 
                => Create(cmd, "-hashfile \"{0}\" MD5", target.Replace('/', '\\'));

            /// <summary>
            /// 获取 <see cref="target"/> 的 <see cref="MD5"/> 值
            /// </summary>
            /// <param name="target"> 目标文件路径 </param>
            /// <returns> MD5 值 </returns>
            public static string GetMD5(string target)
            {
                var executor = MD5(target);
                var temp     = executor.EnableOutput;
                executor.EnableOutput = false;
                var value = executor.Sync().StdOut.ToString().Split('\n')[1].Trim();
                executor.EnableOutput = temp;
                return value;
            }

            /// <summary>
            /// 获取 <see cref="target"/> 的 <see cref="MD5"/> 值
            /// </summary>
            /// <param name="target"> 目标文件路径 </param>
            /// <returns> MD5 值 </returns>
            public static async Task<string> GetMD5Async(string target)
            {
                var executor = MD5(target);
                var temp     = executor.EnableOutput;
                executor.EnableOutput = false;
                var result = await executor.Async();
                var value  = result.StdOut.ToString().Split('\n')[1].Trim();
                executor.EnableOutput = temp;
                return value;
            }

            #endregion

            #region SHA1
      
            /// <summary>
            ///  <see cref="SHA1"/> 算法 计算 <see cref="target"/>
            /// </summary>
            /// <param name="target"> 目标文件路径 </param>
            /// <returns> 执行器 </returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static IExecutor SHA1(string target) 
                => Create(cmd, "-hashfile \"{0}\" SHA1", target.Replace('/', '\\'));

            /// <summary>
            /// 获取 <see cref="target"/> 的 <see cref="SHA1"/> 值
            /// </summary>
            /// <param name="target"> 目标文件路径 </param>
            /// <returns> SHA1 值 </returns>
            public static string GetSHA1(string target)
            {
                var executor = SHA1(target);
                var temp     = executor.EnableOutput;
                executor.EnableOutput = false;
                var value = executor.Sync().StdOut.ToString().Split('\n')[1].Trim();
                executor.EnableOutput = temp;
                return value;
            }

            /// <summary>
            /// 获取 <see cref="target"/> 的 <see cref="SHA1"/> 值
            /// </summary>
            /// <param name="target"> 目标文件路径 </param>
            /// <returns> SHA1 值 </returns>
            public static async Task<string> GetSHA1Async(string target)
            {
                var executor = SHA1(target);
                var temp     = executor.EnableOutput;
                executor.EnableOutput = false;
                var result = await executor.Async();
                var value  = result.StdOut.ToString().Split('\n')[1].Trim();
                executor.EnableOutput = temp;
                return value;
            }

            #endregion

            #region SHA256
      
            /// <summary>
            ///  <see cref="SHA256"/> 算法 计算 <see cref="target"/>
            /// </summary>
            /// <param name="target"> 目标文件路径 </param>
            /// <returns> 执行器 </returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static IExecutor SHA256(string target) 
                => Create(cmd, "-hashfile \"{0}\" SHA256", target.Replace('/', '\\'));

            /// <summary>
            /// 获取 <see cref="target"/> 的 <see cref="SHA256"/> 值
            /// </summary>
            /// <param name="target"> 目标文件路径 </param>
            /// <returns> SHA256 值 </returns>
            public static string GetSHA256(string target)
            {
                var executor = SHA256(target);
                var temp     = executor.EnableOutput;
                executor.EnableOutput = false;
                var value = executor.Sync().StdOut.ToString().Split('\n')[1].Trim();
                executor.EnableOutput = temp;
                return value;
            }

            /// <summary>
            /// 获取 <see cref="target"/> 的 <see cref="SHA256"/> 值
            /// </summary>
            /// <param name="target"> 目标文件路径 </param>
            /// <returns> SHA256 值 </returns>
            public static async Task<string> GetSHA256Async(string target)
            {
                var executor = SHA256(target);
                var temp     = executor.EnableOutput;
                executor.EnableOutput = false;
                var result = await executor.Async();
                var value  = result.StdOut.ToString().Split('\n')[1].Trim();
                executor.EnableOutput = temp;
                return value;
            }

            #endregion

            #region SHA384
      
            /// <summary>
            ///  <see cref="SHA384"/> 算法 计算 <see cref="target"/>
            /// </summary>
            /// <param name="target"> 目标文件路径 </param>
            /// <returns> 执行器 </returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static IExecutor SHA384(string target) 
                => Create(cmd, "-hashfile \"{0}\" SHA384", target.Replace('/', '\\'));

            /// <summary>
            /// 获取 <see cref="target"/> 的 <see cref="SHA384"/> 值
            /// </summary>
            /// <param name="target"> 目标文件路径 </param>
            /// <returns> SHA384 值 </returns>
            public static string GetSHA384(string target)
            {
                var executor = SHA384(target);
                var temp     = executor.EnableOutput;
                executor.EnableOutput = false;
                var value = executor.Sync().StdOut.ToString().Split('\n')[1].Trim();
                executor.EnableOutput = temp;
                return value;
            }

            /// <summary>
            /// 获取 <see cref="target"/> 的 <see cref="SHA384"/> 值
            /// </summary>
            /// <param name="target"> 目标文件路径 </param>
            /// <returns> SHA384 值 </returns>
            public static async Task<string> GetSHA384Async(string target)
            {
                var executor = SHA384(target);
                var temp     = executor.EnableOutput;
                executor.EnableOutput = false;
                var result = await executor.Async();
                var value  = result.StdOut.ToString().Split('\n')[1].Trim();
                executor.EnableOutput = temp;
                return value;
            }

            #endregion

            #region SHA512
      
            /// <summary>
            ///  <see cref="SHA512"/> 算法 计算 <see cref="target"/>
            /// </summary>
            /// <param name="target"> 目标文件路径 </param>
            /// <returns> 执行器 </returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static IExecutor SHA512(string target) 
                => Create(cmd, "-hashfile \"{0}\" SHA512", target.Replace('/', '\\'));

            /// <summary>
            /// 获取 <see cref="target"/> 的 <see cref="SHA512"/> 值
            /// </summary>
            /// <param name="target"> 目标文件路径 </param>
            /// <returns> SHA512 值 </returns>
            public static string GetSHA512(string target)
            {
                var executor = SHA512(target);
                var temp     = executor.EnableOutput;
                executor.EnableOutput = false;
                var value = executor.Sync().StdOut.ToString().Split('\n')[1].Trim();
                executor.EnableOutput = temp;
                return value;
            }

            /// <summary>
            /// 获取 <see cref="target"/> 的 <see cref="SHA512"/> 值
            /// </summary>
            /// <param name="target"> 目标文件路径 </param>
            /// <returns> SHA512 值 </returns>
            public static async Task<string> GetSHA512Async(string target)
            {
                var executor = SHA512(target);
                var temp     = executor.EnableOutput;
                executor.EnableOutput = false;
                var result = await executor.Async();
                var value  = result.StdOut.ToString().Split('\n')[1].Trim();
                executor.EnableOutput = temp;
                return value;
            }

            #endregion

        }
    }
}
/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using SMarshal = System.Runtime.InteropServices.Marshal;

public partial class AHelper
{
    /// <summary>
    /// SMarshal 类
    /// </summary>
    /// <see>
    ///     <cref>https://msdn.microsoft.com/zh-cn/library/system.runtime.interopservices.marshal(VS.80).aspx</cref>
    /// </see>
    /// <!--提供了一个方法集，这些方法用于分配非托管内存、复制非托管内存块、将托管类型转换为非托管类型 此外还提供了在与非托管代码交互时使用的其他杂项方法-->
    /// <!--备注 SMarshal 类中定义的 static 方法对于处理非托管代码至关重要。此类中定义的大多数方法通常由需要-->
    /// <!--此类型的任何公共静态（Visual Basic 中的 Shared）成员都是线程安全的，但不保证所有实例成员都 是线程安全的-->
    public class Marshal
    {
        /// <summary>
        /// 表示系统上的默认字符大小；Unicode 系统上默认值为 2，ANSI 系统上默认值为 1。 此字段为只读。
        /// </summary>
        public static int SystemDefaultCharSize => SMarshal.SystemDefaultCharSize;

        /// <summary>
        /// 表示用于当前操作系统的双字节字符集 (DBCS) 的最大大小（以字节为单位）。 此字段为只读。
        /// </summary>
        public static int SystemMaxDBCSCharSize => SMarshal.SystemMaxDBCSCharSize;

        /// <summary>
        /// 获取 结构体实例 空间大小
        /// </summary>
        /// <param name="obj">返回对象的非托管大小 以字节为单位</param>
        [SecurityCritical]
        public static int SizeOf<T>(T obj)
        {
            return SMarshal.SizeOf(obj);
        }

        /// <summary>
        /// 获取 结构体实例 空间大小
        /// </summary>
        [SecurityCritical]
        public static int SizeOf<T>()
        {
            return SMarshal.SizeOf<T>();
        }

        /// <summary>
        /// 使用指定的字节数从进程的非托管内存中分配内存。
        /// </summary>
        /// <param name="size">内存中所需的字节数</param>
        /// <returns>一个指向新分配内存的指针 这个内存必须使用Marshal.FreeHGlobal 来释放 </returns>
        public static IntPtr AllocHGlobal(int size)
        {
            return SMarshal.AllocHGlobal(size);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static int AddRef(IntPtr pUnk)
        {
            return SMarshal.AddRef(pUnk);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static IntPtr AllocCoTaskMem(int cb)
        {
            return SMarshal.AllocCoTaskMem(cb);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static bool AreComObjectsAvailableForCleanup()
        {
            return SMarshal.AreComObjectsAvailableForCleanup();
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static object BindToMoniker(string monikerName)
        {
            return SMarshal.BindToMoniker(monikerName);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void ChangeWrapperHandleStrength(object otp, bool fIsWeak)
        {
            SMarshal.ChangeWrapperHandleStrength(otp, fIsWeak);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void CleanupUnusedObjectsInCurrentContext()
        {
            SMarshal.CleanupUnusedObjectsInCurrentContext();
        }

        /// <summary>
        /// 使用指定的字节数从进程的非托管内存中分配内存。
        /// </summary>
        /// <param name="ptr">内存中所需的字节数</param>
        /// <returns>一个指向新分配内存的指针 这个内存必须使用Marshal.FreeHGlobal 来释放 </returns>
        [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        public static IntPtr AllocHGlobal(IntPtr ptr)
        {
            return SMarshal.AllocHGlobal(ptr);
        }

        /// <summary>
        /// 释放内存中指针
        /// </summary>
        /// <param name="hglobal">内存中的指针</param>
        [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static void FreeHGlobal(IntPtr hglobal)
        {
            SMarshal.FreeHGlobal(hglobal);
        }

        /// <summary>
        /// 创建聚合对象
        /// </summary>
        /// <param name="ptr">指针</param>
        /// <param name="obj">数据</param>
        public static void CreateAggregatedObject<T>(IntPtr ptr, T obj)
        {
            SMarshal.CreateAggregatedObject(ptr, obj);
        }

        /// <summary>
        /// 创建类型的包装器
        /// </summary>
        /// <typeparam name="T">结构类型</typeparam>
        /// <param name="obj">数据</param>
        /// <returns>包装数据</returns>
        public static object CreateWrapperOfType<T>(object obj)
        {
            return SMarshal.CreateWrapperOfType(obj, typeof(T));
        }

        /// <summary>
        /// 创建类型的包装器
        /// </summary>
        /// <typeparam name="T">结构类型</typeparam>
        /// <typeparam name="TWrapper">包装器</typeparam>
        /// <param name="obj">数据</param>
        /// <returns>包装器</returns>
        public static TWrapper CreateWrapperOfType<T, TWrapper>(T obj)
        {
            return SMarshal.CreateWrapperOfType<T, TWrapper>(obj);
        }

        #region Clone

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void Copy(float[] source, int startIndex, IntPtr destination, int length)
        {
            SMarshal.Copy(source, startIndex, destination, length);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void Copy(IntPtr[] source, int startIndex, IntPtr destination, int length)
        {
            SMarshal.Copy(source, startIndex, destination, length);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void Copy(IntPtr source, float[] destination, int startIndex, int length)
        {
            SMarshal.Copy(source, destination, startIndex, length);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void Copy(IntPtr source, IntPtr[] destination, int startIndex, int length)
        {
            SMarshal.Copy(source, destination, startIndex, length);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void Copy(IntPtr source, long[] destination, int startIndex, int length)
        {
            SMarshal.Copy(source, destination, startIndex, length);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void Copy(IntPtr source, int[] destination, int startIndex, int length)
        {
            SMarshal.Copy(source, destination, startIndex, length);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void Copy(IntPtr source, double[] destination, int startIndex, int length)
        {
            SMarshal.Copy(source, destination, startIndex, length);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void Copy(IntPtr source, short[] destination, int startIndex, int length)
        {
            SMarshal.Copy(source, destination, startIndex, length);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void Copy(IntPtr source, byte[] destination, int startIndex, int length)
        {
            SMarshal.Copy(source, destination, startIndex, length);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void Copy(long[] source, int startIndex, IntPtr destination, int length)
        {
            SMarshal.Copy(source, startIndex, destination, length);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void Copy(int[] source, int startIndex, IntPtr destination, int length)
        {
            SMarshal.Copy(source, startIndex, destination, length);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void Copy(short[] source, int startIndex, IntPtr destination, int length)
        {
            SMarshal.Copy(source, startIndex, destination, length);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void Copy(double[] source, int startIndex, IntPtr destination, int length)
        {
            SMarshal.Copy(source, startIndex, destination, length);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void Copy(char[] source, int startIndex, IntPtr destination, int length)
        {
            SMarshal.Copy(source, startIndex, destination, length);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void Copy(byte[] source, int startIndex, IntPtr destination, int length)
        {
            SMarshal.Copy(source, startIndex, destination, length);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void Copy(IntPtr source, char[] destination, int startIndex, int length)
        {
            SMarshal.Copy(source, destination, startIndex, length);
        }

        #endregion

        #region Read

#if UNITY_2021_1_OR_NEWER
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [Obsolete("ReadByte(Object, Int32) may be unavailable in future releases.")]
#endif
        /// <summary>
        /// 
        /// </summary>
        [SuppressUnmanagedCodeSecurity, SecurityCritical]
        public static byte ReadByte(object ptr, int ofs)
        {
            return SMarshal.ReadByte(ptr, ofs);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static byte ReadByte(IntPtr ptr)
        {
            return SMarshal.ReadByte(ptr);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static byte ReadByte(IntPtr ptr, int ofs)
        {
            return SMarshal.ReadByte(ptr, ofs);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static short ReadInt16(IntPtr ptr)
        {
            return SMarshal.ReadInt16(ptr);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static short ReadInt16(IntPtr ptr, int ofs)
        {
            return SMarshal.ReadInt16(ptr, ofs);
        }

#if UNITY_2021_1_OR_NEWER
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [Obsolete("ReadInt16(Object, Int32) may be unavailable in future releases.")]
#endif
        /// <summary>
        /// 
        /// </summary>
        [SuppressUnmanagedCodeSecurity, SecurityCritical]
        public static short ReadInt16(object ptr, int ofs)
        {
            return SMarshal.ReadInt16(ptr, ofs);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static int ReadInt32(IntPtr ptr)
        {
            return SMarshal.ReadInt32(ptr);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static int ReadInt32(IntPtr ptr, int ofs)
        {
            return SMarshal.ReadInt32(ptr, ofs);
        }

#if UNITY_2021_1_OR_NEWER
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [Obsolete("ReadInt32(Object, Int32) may be unavailable in future releases.")]
#endif
        /// <summary>
        /// 
        /// </summary>
        [SuppressUnmanagedCodeSecurity, SecurityCritical,
         ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static int ReadInt32(object ptr, int ofs)
        {
            return SMarshal.ReadInt32(ptr, ofs);
        }

#if UNITY_2021_1_OR_NEWER
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [Obsolete("ReadInt64(Object, Int32) may be unavailable in future releases.")]
#endif
        /// <summary>
        /// 
        /// </summary>
        [SuppressUnmanagedCodeSecurity, SecurityCritical,
         ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static long ReadInt64(object ptr, int ofs)
        {
            return SMarshal.ReadInt64(ptr, ofs);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static long ReadInt64(IntPtr ptr, int ofs)
        {
            return SMarshal.ReadInt64(ptr, ofs);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static long ReadInt64(IntPtr ptr)
        {
            return SMarshal.ReadInt64(ptr);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static IntPtr ReadIntPtr(IntPtr ptr)
        {
            return SMarshal.ReadIntPtr(ptr);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static IntPtr ReadIntPtr(IntPtr ptr, int ofs)
        {
            return SMarshal.ReadIntPtr(ptr, ofs);
        }

#if UNITY_2021_1_OR_NEWER
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [Obsolete("ReadIntPtr(Object, Int32) may be unavailable in future releases.")]
#endif
        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static IntPtr ReadIntPtr(object ptr, int ofs)
        {
            return SMarshal.ReadIntPtr(ptr, ofs);
        }

        #endregion

        #region Write

#if UNITY_2021_1_OR_NEWER
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [Obsolete("WriteByte(Object, Int32, Byte) may be unavailable in future releases.")]
#endif
        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical, SuppressUnmanagedCodeSecurity]
        public static void WriteByte(object ptr, int ofs, byte val)
        {
            SMarshal.WriteByte(ptr, ofs, val);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void WriteByte(IntPtr ptr, int ofs, byte val)
        {
            SMarshal.WriteByte(ptr, ofs, val);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void WriteByte(IntPtr ptr, byte val)
        {
            SMarshal.WriteByte(ptr, val);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void WriteInt16(IntPtr ptr, char val)
        {
            SMarshal.WriteInt16(ptr, val);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void WriteInt16(IntPtr ptr, short val)
        {
            SMarshal.WriteInt16(ptr, val);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void WriteInt16(IntPtr ptr, int ofs, char val)
        {
            SMarshal.WriteInt16(ptr, ofs, val);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void WriteInt16(IntPtr ptr, int ofs, short val)
        {
            SMarshal.WriteInt16(ptr, ofs, val);
        }

#if UNITY_2021_1_OR_NEWER
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [Obsolete("WriteInt16(Object, Int32, Char) may be unavailable in future releases.")]
#endif
        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void WriteInt16(object ptr, int ofs, char val)
        {
            SMarshal.WriteInt16(ptr, ofs, val);
        }

#if UNITY_2021_1_OR_NEWER
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [Obsolete("WriteInt16(Object, Int32, Int16) may be unavailable in future releases.")]
#endif
        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical, SuppressUnmanagedCodeSecurity]
        public static void WriteInt16(object ptr, int ofs, short val)
        {
            SMarshal.WriteInt16(ptr, ofs, val);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void WriteInt32(IntPtr ptr, int val)
        {
            SMarshal.WriteInt32(ptr, val);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void WriteInt32(IntPtr ptr, int ofs, int val)
        {
            SMarshal.WriteInt32(ptr, ofs, val);
        }

#if UNITY_2021_1_OR_NEWER
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [Obsolete("WriteInt32(Object, Int32, Int32) may be unavailable in future releases.")]
#endif
        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical, SuppressUnmanagedCodeSecurity]
        public static void WriteInt32(object ptr, int ofs, int val)
        {
            SMarshal.WriteInt32(ptr, ofs, val);
        }

#if UNITY_2021_1_OR_NEWER
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [Obsolete("WriteInt64(Object, Int32, Int64) may be unavailable in future releases.")]
#endif
        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical, SuppressUnmanagedCodeSecurity]
        public static void WriteInt64(object ptr, int ofs, long val)
        {
            SMarshal.WriteInt64(ptr, ofs, val);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void WriteInt64(IntPtr ptr, int ofs, long val)
        {
            SMarshal.WriteInt64(ptr, ofs, val);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void WriteInt64(IntPtr ptr, long val)
        {
            SMarshal.WriteInt64(ptr, val);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void WriteIntPtr(IntPtr ptr, int ofs, IntPtr val)
        {
            SMarshal.WriteIntPtr(ptr, ofs, val);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void WriteIntPtr(IntPtr ptr, IntPtr val)
        {
            SMarshal.WriteIntPtr(ptr, val);
        }

#if UNITY_2021_1_OR_NEWER
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [Obsolete("WriteIntPtr(Object, Int32, IntPtr) may be unavailable in future releases.")]
#endif
        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void WriteIntPtr(object ptr, int ofs, IntPtr val)
        {
            SMarshal.WriteIntPtr(ptr, ofs, val);
        }

        #endregion

        #region Zero

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void ZeroFreeBSTR(IntPtr s)
        {
            SMarshal.ZeroFreeBSTR(s);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void ZeroFreeCoTaskMemAnsi(IntPtr s)
        {
            SMarshal.ZeroFreeCoTaskMemAnsi(s);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void ZeroFreeCoTaskMemUnicode(IntPtr s)
        {
            SMarshal.ZeroFreeCoTaskMemUnicode(s);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void ZeroFreeGlobalAllocAnsi(IntPtr s)
        {
            SMarshal.ZeroFreeGlobalAllocAnsi(s);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void ZeroFreeGlobalAllocUnicode(IntPtr s)
        {
            SMarshal.ZeroFreeGlobalAllocUnicode(s);
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        [ComVisible(true), ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail), SecurityCritical]
        public static void StructureToPtr(object structure, IntPtr ptr, bool fDeleteOld)
        {
            SMarshal.StructureToPtr(structure, ptr, fDeleteOld);
        }

        /// <summary>
        /// 
        /// </summary>
        [ComVisible(true), SecurityCritical]
        public static void DestroyStructure(IntPtr ptr, Type structuretype)
        {
            SMarshal.DestroyStructure(ptr, structuretype);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void DestroyStructure<T>(IntPtr ptr)
        {
            SMarshal.DestroyStructure<T>(ptr);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static int FinalReleaseComObject(object o)
        {
            return SMarshal.FinalReleaseComObject(o);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void FreeBSTR(IntPtr ptr)
        {
            SMarshal.FreeBSTR(ptr);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void FreeCoTaskMem(IntPtr ptr)
        {
            SMarshal.FreeCoTaskMem(ptr);
        }

        /// <summary>
        /// 将数据从托管对象封送到非托管内存块
        /// </summary>
        /// <typeparam name="T">保存要封送的数据的托管对象 此对象必须是格式化类的结构或实例</typeparam>
        /// <param name="obj">实例</param>
        /// <param name="ptr">指向非托管内存块的指针，在调用此方法之前必须分配该内存块</param>
        /// <param name="fDeleteOld">在ptr参数上使用DestroyStructure方法复制数据</param>
        public static void StructureToPtr<T>(T obj, IntPtr ptr, bool fDeleteOld = true)
        {
            SMarshal.StructureToPtr(obj, ptr, fDeleteOld);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void ThrowExceptionForHR(int errorCode)
        {
            SMarshal.ThrowExceptionForHR(errorCode);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void ThrowExceptionForHR(int errorCode, IntPtr errorInfo)
        {
            SMarshal.ThrowExceptionForHR(errorCode, errorInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static IntPtr UnsafeAddrOfPinnedArrayElement(Array arr, int index)
        {
            return SMarshal.UnsafeAddrOfPinnedArrayElement(arr, index);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static IntPtr UnsafeAddrOfPinnedArrayElement<T>(T[] arr, int index)
        {
            return SMarshal.UnsafeAddrOfPinnedArrayElement(arr, index);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static int QueryInterface(IntPtr pUnk, ref System.Guid iid, out IntPtr ppv)
        {
            return SMarshal.QueryInterface(pUnk, ref iid, out ppv);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecuritySafeCritical]
        public static bool IsComObject(object o)
        {
            return SMarshal.IsComObject(o);
        }

        /// <summary>
        /// 
        /// </summary>
        public static IntPtr OffsetOf(Type t, string fieldName)
        {
            return SMarshal.OffsetOf(t, fieldName);
        }

        /// <summary>
        /// 
        /// </summary>
        public static IntPtr OffsetOf<T>(string fieldName)
        {
            return SMarshal.OffsetOf<T>(fieldName);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void Prelink(MethodInfo m)
        {
            SMarshal.Prelink(m);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void PrelinkAll(Type c)
        {
            SMarshal.PrelinkAll(c);
        }

        #region String

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static IntPtr StringToBSTR(string s)
        {
            return SMarshal.StringToBSTR(s);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static IntPtr StringToCoTaskMemAnsi(string s)
        {
            return SMarshal.StringToCoTaskMemAnsi(s);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static IntPtr StringToCoTaskMemAuto(string s)
        {
            return SMarshal.StringToCoTaskMemAuto(s);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static IntPtr StringToCoTaskMemUni(string s)
        {
            return SMarshal.StringToCoTaskMemUni(s);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static IntPtr StringToHGlobalAnsi(string s)
        {
            return SMarshal.StringToHGlobalAnsi(s);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static IntPtr StringToHGlobalAuto(string s)
        {
            return SMarshal.StringToHGlobalAuto(s);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static IntPtr StringToHGlobalUni(string s)
        {
            return SMarshal.StringToHGlobalUni(s);
        }

        #endregion

        #region Secure String

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static IntPtr SecureStringToBSTR(SecureString s)
        {
            return SMarshal.SecureStringToBSTR(s);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static IntPtr SecureStringToCoTaskMemAnsi(SecureString s)
        {
            return SMarshal.SecureStringToCoTaskMemAnsi(s);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static IntPtr SecureStringToCoTaskMemUnicode(SecureString s)
        {
            return SMarshal.SecureStringToCoTaskMemUnicode(s);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static IntPtr SecureStringToGlobalAllocAnsi(SecureString s)
        {
            return SMarshal.SecureStringToGlobalAllocAnsi(s);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static IntPtr SecureStringToGlobalAllocUnicode(SecureString s)
        {
            return SMarshal.SecureStringToGlobalAllocUnicode(s);
        }

        #endregion

        #region Re Alloc

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static IntPtr ReAllocCoTaskMem(IntPtr pv, int cb)
        {
            return SMarshal.ReAllocCoTaskMem(pv, cb);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static IntPtr ReAllocHGlobal(IntPtr pv, IntPtr cb)
        {
            return SMarshal.ReAllocHGlobal(pv, cb);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static int Release(IntPtr pUnk)
        {
            return SMarshal.Release(pUnk);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static int ReleaseComObject(object o)
        {
            return SMarshal.ReleaseComObject(o);
        }

        #endregion

        #region PtrToString

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static string PtrToStringAnsi(IntPtr ptr, int len)
        {
            return SMarshal.PtrToStringAnsi(ptr, len);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static string PtrToStringAnsi(IntPtr ptr)
        {
            return SMarshal.PtrToStringAnsi(ptr);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static string PtrToStringAuto(IntPtr ptr)
        {
            return SMarshal.PtrToStringAuto(ptr);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static string PtrToStringAuto(IntPtr ptr, int len)
        {
            return SMarshal.PtrToStringAuto(ptr, len);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static string PtrToStringBSTR(IntPtr ptr)
        {
            return SMarshal.PtrToStringBSTR(ptr);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static string PtrToStringUni(IntPtr ptr)
        {
            return SMarshal.PtrToStringUni(ptr);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static string PtrToStringUni(IntPtr ptr, int len)
        {
            return SMarshal.PtrToStringUni(ptr, len);
        }

        /// <summary>
        /// 
        /// </summary>
        [ComVisible(true), SecurityCritical]
        public static void PtrToStructure(IntPtr ptr, object structure)
        {
            SMarshal.PtrToStructure(ptr, structure);
        }

        /// <summary>
        /// 
        /// </summary>
        [ComVisible(true), SecurityCritical]
        public static object PtrToStructure(IntPtr ptr, Type structureType)
        {
            return SMarshal.PtrToStructure(ptr, structureType);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static T PtrToStructure<T>(IntPtr ptr)
        {
            return SMarshal.PtrToStructure<T>(ptr);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void PtrToStructure<T>(IntPtr ptr, T structure)
        {
            SMarshal.PtrToStructure(ptr, structure);
        }

        #endregion

        #region Get

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static IntPtr GetComInterfaceForObject(object o, Type T, CustomQueryInterfaceMode mode)
        {
            return SMarshal.GetComInterfaceForObject(o, T, mode);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static IntPtr GetComInterfaceForObject<T, TInterface>(T o)
        {
            return SMarshal.GetComInterfaceForObject<T, TInterface>(o);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static IntPtr GetComInterfaceForObject(object o, Type T)
        {
            return SMarshal.GetComInterfaceForObject(o, T);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static Delegate GetDelegateForFunctionPointer(IntPtr ptr, Type t)
        {
            return SMarshal.GetDelegateForFunctionPointer(ptr, t);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static TDelegate GetDelegateForFunctionPointer<TDelegate>(IntPtr ptr)
        {
            return SMarshal.GetDelegateForFunctionPointer<TDelegate>(ptr);
        }

#if UNITY_2021_1_OR_NEWER
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [Obsolete("GetExceptionCode() may be unavailable in future releases.")]
#endif
        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static int GetExceptionCode()
        {
            return SMarshal.GetExceptionCode();
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static Exception GetExceptionForHR(int errorCode)
        {
            return SMarshal.GetExceptionForHR(errorCode);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static Exception GetExceptionForHR(int errorCode, IntPtr errorInfo)
        {
            return SMarshal.GetExceptionForHR(errorCode, errorInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static IntPtr GetFunctionPointerForDelegate(Delegate d)
        {
            return SMarshal.GetFunctionPointerForDelegate(d);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static IntPtr GetFunctionPointerForDelegate<TDelegate>(TDelegate d)
        {
            return SMarshal.GetFunctionPointerForDelegate(d);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static int GetHRForException(Exception e)
        {
            return SMarshal.GetHRForException(e);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static int GetHRForLastWin32Error()
        {
            return SMarshal.GetHRForLastWin32Error();
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static IntPtr GetIUnknownForObject(object o)
        {
            return SMarshal.GetIUnknownForObject(o);
        }

        /// <summary>
        /// 
        /// </summary>
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), SecurityCritical]
        public static int GetLastWin32Error()
        {
            return SMarshal.GetLastWin32Error();
        }

#if UNITY_2021_1_OR_NEWER
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [Obsolete("GetNativeVariantForObject(Object, IntPtr) may be unavailable in future releases.")]
#endif
        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void GetNativeVariantForObject(object obj, IntPtr pDstNativeVariant)
        {
            SMarshal.GetNativeVariantForObject(obj, pDstNativeVariant);
        }

#if UNITY_2021_1_OR_NEWER
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [Obsolete("GetNativeVariantForObject<T>(T, IntPtr) may be unavailable in future releases.")]
#endif
        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void GetNativeVariantForObject<T>(T obj, IntPtr pDstNativeVariant)
        {
            SMarshal.GetNativeVariantForObject(obj, pDstNativeVariant);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static object GetObjectForIUnknown(IntPtr pUnk)
        {
            return SMarshal.GetObjectForIUnknown(pUnk);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static object GetObjectForNativeVariant(IntPtr pSrcNativeVariant)
        {
            return SMarshal.GetObjectForIUnknown(pSrcNativeVariant);
        }

#if UNITY_2021_1_OR_NEWER
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [Obsolete("GetObjectForNativeVariant<T>(IntPtr) may be unavailable in future releases.")]
#endif
        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static T GetObjectForNativeVariant<T>(IntPtr pSrcNativeVariant)
        {
            return SMarshal.GetObjectForNativeVariant<T>(pSrcNativeVariant);
        }

#if UNITY_2021_1_OR_NEWER
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [Obsolete("GetObjectsForNativeVariants(IntPtr, Int32) may be unavailable in future releases.")]
#endif

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static object[] GetObjectsForNativeVariants(IntPtr aSrcNativeVariant, int cVars)
        {
            return SMarshal.GetObjectsForNativeVariants(aSrcNativeVariant, cVars);
        }

#if UNITY_2021_1_OR_NEWER
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [Obsolete("GetObjectsForNativeVariants<T>(IntPtr, Int32) may be unavailable in future releases.")]
#endif
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aSrcNativeVariant"></param>
        /// <param name="cVars"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [SecurityCritical]
        public static T[] GetObjectsForNativeVariants<T>(IntPtr aSrcNativeVariant, int cVars)
        {
            return SMarshal.GetObjectsForNativeVariants<T>(aSrcNativeVariant, cVars);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [SecurityCritical]
        public static int GetStartComSlot(Type t)
        {
            return SMarshal.GetStartComSlot(t);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clsid"></param>
        /// <returns></returns>
        [SecuritySafeCritical]
        public static Type GetTypeFromCLSID(System.Guid clsid)
        {
            return SMarshal.GetTypeFromCLSID(clsid);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static string GetTypeInfoName(ITypeInfo typeInfo)
        {
            return SMarshal.GetTypeInfoName(typeInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static object GetUniqueObjectForIUnknown(IntPtr unknown)
        {
            return SMarshal.GetUniqueObjectForIUnknown(unknown);
        }

        #endregion
    }
}
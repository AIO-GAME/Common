/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;

public partial class Utils
{
    /// <summary>
    /// Marshal 类
    /// </summary>
    /// <see>
    ///     <cref>https://msdn.microsoft.com/zh-cn/library/system.runtime.interopservices.marshal(VS.80).aspx</cref>
    /// </see>
    /// <!--提供了一个方法集，这些方法用于分配非托管内存、复制非托管内存块、将托管类型转换为非托管类型 此外还提供了在与非托管代码交互时使用的其他杂项方法-->
    /// <!--备注 Marshal 类中定义的 static 方法对于处理非托管代码至关重要。此类中定义的大多数方法通常由需要-->
    /// <!--此类型的任何公共静态（Visual Basic 中的 Shared）成员都是线程安全的，但不保证所有实例成员都 是线程安全的-->
    public static class MarshalEx
    {
        /// <summary>
        /// 表示系统上的默认字符大小；Unicode 系统上默认值为 2，ANSI 系统上默认值为 1。 此字段为只读。
        /// </summary>
        public static int SystemDefaultCharSize => Marshal.SystemDefaultCharSize;

        /// <summary>
        /// 表示用于当前操作系统的双字节字符集 (DBCS) 的最大大小（以字节为单位）。 此字段为只读。
        /// </summary>
        public static int SystemMaxDBCSCharSize => Marshal.SystemMaxDBCSCharSize;

        /// <summary>
        /// 获取 结构体实例 空间大小
        /// </summary>
        /// <param name="obj">返回对象的非托管大小 以字节为单位</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SecurityCritical]
        public static int SizeOf<T>(T obj)
        {
            return Marshal.SizeOf(obj);
        }

        /// <summary>
        /// 获取 结构体实例 空间大小
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SecurityCritical]
        public static int SizeOf<T>()
        {
            return Marshal.SizeOf<T>();
        }

        /// <summary>
        /// 使用指定的字节数从进程的非托管内存中分配内存。
        /// </summary>
        /// <param name="size">内存中所需的字节数</param>
        /// <returns>一个指向新分配内存的指针 这个内存必须使用Marshal.FreeHGlobal 来释放 </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntPtr AllocHGlobal(int size)
        {
            return Marshal.AllocHGlobal(size);
        }

        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SecurityCritical]
        public static int AddRef(IntPtr pUnk)
        {
            return Marshal.AddRef(pUnk);
        }

        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SecurityCritical]
        public static IntPtr AllocCoTaskMem(int cb)
        {
            return Marshal.AllocCoTaskMem(cb);
        }

        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SecurityCritical]
        public static bool AreComObjectsAvailableForCleanup()
        {
            return Marshal.AreComObjectsAvailableForCleanup();
        }

        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SecurityCritical]
        public static object BindToMoniker(string monikerName)
        {
            return Marshal.BindToMoniker(monikerName);
        }

        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SecurityCritical]
        public static void ChangeWrapperHandleStrength(object otp, bool fIsWeak)
        {
            Marshal.ChangeWrapperHandleStrength(otp, fIsWeak);
        }

        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SecurityCritical]
        public static void CleanupUnusedObjectsInCurrentContext()
        {
            Marshal.CleanupUnusedObjectsInCurrentContext();
        }

        /// <summary>
        /// 使用指定的字节数从进程的非托管内存中分配内存。
        /// </summary>
        /// <param name="ptr">内存中所需的字节数</param>
        /// <returns>一个指向新分配内存的指针 这个内存必须使用Marshal.FreeHGlobal 来释放 </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        public static IntPtr AllocHGlobal(IntPtr ptr)
        {
            return Marshal.AllocHGlobal(ptr);
        }

        /// <summary>
        /// 释放内存中指针
        /// </summary>
        /// <param name="hglobal">内存中的指针</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static void FreeHGlobal(IntPtr hglobal)
        {
            Marshal.FreeHGlobal(hglobal);
        }

        /// <summary>
        /// 创建聚合对象
        /// </summary>
        /// <param name="ptr">指针</param>
        /// <param name="obj">数据</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateAggregatedObject<T>(IntPtr ptr, T obj)
        {
            Marshal.CreateAggregatedObject(ptr, obj);
        }

        /// <summary>
        /// 创建类型的包装器
        /// </summary>
        /// <typeparam name="T">结构类型</typeparam>
        /// <param name="obj">数据</param>
        /// <returns>包装数据</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object CreateWrapperOfType<T>(object obj)
        {
            return Marshal.CreateWrapperOfType(obj, typeof(T));
        }

        /// <summary>
        /// 创建类型的包装器
        /// </summary>
        /// <typeparam name="T">结构类型</typeparam>
        /// <typeparam name="TWrapper">包装器</typeparam>
        /// <param name="obj">数据</param>
        /// <returns>包装器</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TWrapper CreateWrapperOfType<T, TWrapper>(T obj)
        {
            return Marshal.CreateWrapperOfType<T, TWrapper>(obj);
        }

        #region Clone

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void Copy(float[] source, int startIndex, IntPtr destination, int length)
        {
            Marshal.Copy(source, startIndex, destination, length);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void Copy(IntPtr[] source, int startIndex, IntPtr destination, int length)
        {
            Marshal.Copy(source, startIndex, destination, length);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void Copy(IntPtr source, float[] destination, int startIndex, int length)
        {
            Marshal.Copy(source, destination, startIndex, length);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void Copy(IntPtr source, IntPtr[] destination, int startIndex, int length)
        {
            Marshal.Copy(source, destination, startIndex, length);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void Copy(IntPtr source, long[] destination, int startIndex, int length)
        {
            Marshal.Copy(source, destination, startIndex, length);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void Copy(IntPtr source, int[] destination, int startIndex, int length)
        {
            Marshal.Copy(source, destination, startIndex, length);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void Copy(IntPtr source, double[] destination, int startIndex, int length)
        {
            Marshal.Copy(source, destination, startIndex, length);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void Copy(IntPtr source, short[] destination, int startIndex, int length)
        {
            Marshal.Copy(source, destination, startIndex, length);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void Copy(IntPtr source, byte[] destination, int startIndex, int length)
        {
            Marshal.Copy(source, destination, startIndex, length);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void Copy(long[] source, int startIndex, IntPtr destination, int length)
        {
            Marshal.Copy(source, startIndex, destination, length);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void Copy(int[] source, int startIndex, IntPtr destination, int length)
        {
            Marshal.Copy(source, startIndex, destination, length);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void Copy(short[] source, int startIndex, IntPtr destination, int length)
        {
            Marshal.Copy(source, startIndex, destination, length);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void Copy(double[] source, int startIndex, IntPtr destination, int length)
        {
            Marshal.Copy(source, startIndex, destination, length);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void Copy(char[] source, int startIndex, IntPtr destination, int length)
        {
            Marshal.Copy(source, startIndex, destination, length);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void Copy(byte[] source, int startIndex, IntPtr destination, int length)
        {
            Marshal.Copy(source, startIndex, destination, length);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void Copy(IntPtr source, char[] destination, int startIndex, int length)
        {
            Marshal.Copy(source, destination, startIndex, length);
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
            return Marshal.ReadByte(ptr, ofs);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static byte ReadByte(IntPtr ptr)
        {
            return Marshal.ReadByte(ptr);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static byte ReadByte(IntPtr ptr, int ofs)
        {
            return Marshal.ReadByte(ptr, ofs);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static short ReadInt16(IntPtr ptr)
        {
            return Marshal.ReadInt16(ptr);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static short ReadInt16(IntPtr ptr, int ofs)
        {
            return Marshal.ReadInt16(ptr, ofs);
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
            return Marshal.ReadInt16(ptr, ofs);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static int ReadInt32(IntPtr ptr)
        {
            return Marshal.ReadInt32(ptr);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static int ReadInt32(IntPtr ptr, int ofs)
        {
            return Marshal.ReadInt32(ptr, ofs);
        }

#if UNITY_2021_1_OR_NEWER
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [Obsolete("ReadInt32(Object, Int32) may be unavailable in future releases.")]
#endif
        /// <summary>
        /// 
        /// </summary>
        [SuppressUnmanagedCodeSecurity, SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static int ReadInt32(object ptr, int ofs)
        {
            return Marshal.ReadInt32(ptr, ofs);
        }

#if UNITY_2021_1_OR_NEWER
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [Obsolete("ReadInt64(Object, Int32) may be unavailable in future releases.")]
#endif
        /// <summary>
        /// 
        /// </summary>
        [SuppressUnmanagedCodeSecurity, SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static long ReadInt64(object ptr, int ofs)
        {
            return Marshal.ReadInt64(ptr, ofs);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static long ReadInt64(IntPtr ptr, int ofs)
        {
            return Marshal.ReadInt64(ptr, ofs);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static long ReadInt64(IntPtr ptr)
        {
            return Marshal.ReadInt64(ptr);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static IntPtr ReadIntPtr(IntPtr ptr)
        {
            return Marshal.ReadIntPtr(ptr);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static IntPtr ReadIntPtr(IntPtr ptr, int ofs)
        {
            return Marshal.ReadIntPtr(ptr, ofs);
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
            return Marshal.ReadIntPtr(ptr, ofs);
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
            Marshal.WriteByte(ptr, ofs, val);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void WriteByte(IntPtr ptr, int ofs, byte val)
        {
            Marshal.WriteByte(ptr, ofs, val);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void WriteByte(IntPtr ptr, byte val)
        {
            Marshal.WriteByte(ptr, val);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void WriteInt16(IntPtr ptr, char val)
        {
            Marshal.WriteInt16(ptr, val);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void WriteInt16(IntPtr ptr, short val)
        {
            Marshal.WriteInt16(ptr, val);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void WriteInt16(IntPtr ptr, int ofs, char val)
        {
            Marshal.WriteInt16(ptr, ofs, val);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void WriteInt16(IntPtr ptr, int ofs, short val)
        {
            Marshal.WriteInt16(ptr, ofs, val);
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
            Marshal.WriteInt16(ptr, ofs, val);
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
            Marshal.WriteInt16(ptr, ofs, val);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void WriteInt32(IntPtr ptr, int val)
        {
            Marshal.WriteInt32(ptr, val);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void WriteInt32(IntPtr ptr, int ofs, int val)
        {
            Marshal.WriteInt32(ptr, ofs, val);
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
            Marshal.WriteInt32(ptr, ofs, val);
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
            Marshal.WriteInt64(ptr, ofs, val);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void WriteInt64(IntPtr ptr, int ofs, long val)
        {
            Marshal.WriteInt64(ptr, ofs, val);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void WriteInt64(IntPtr ptr, long val)
        {
            Marshal.WriteInt64(ptr, val);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void WriteIntPtr(IntPtr ptr, int ofs, IntPtr val)
        {
            Marshal.WriteIntPtr(ptr, ofs, val);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void WriteIntPtr(IntPtr ptr, IntPtr val)
        {
            Marshal.WriteIntPtr(ptr, val);
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
            Marshal.WriteIntPtr(ptr, ofs, val);
        }

        #endregion

        #region Zero

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void ZeroFreeBSTR(IntPtr s)
        {
            Marshal.ZeroFreeBSTR(s);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void ZeroFreeCoTaskMemAnsi(IntPtr s)
        {
            Marshal.ZeroFreeCoTaskMemAnsi(s);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void ZeroFreeCoTaskMemUnicode(IntPtr s)
        {
            Marshal.ZeroFreeCoTaskMemUnicode(s);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void ZeroFreeGlobalAllocAnsi(IntPtr s)
        {
            Marshal.ZeroFreeGlobalAllocAnsi(s);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void ZeroFreeGlobalAllocUnicode(IntPtr s)
        {
            Marshal.ZeroFreeGlobalAllocUnicode(s);
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ComVisible(true), ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail), SecurityCritical]
        public static void StructureToPtr(object structure, IntPtr ptr, bool fDeleteOld)
        {
            Marshal.StructureToPtr(structure, ptr, fDeleteOld);
        }

        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ComVisible(true), SecurityCritical]
        public static void DestroyStructure(IntPtr ptr, Type structuretype)
        {
            Marshal.DestroyStructure(ptr, structuretype);
        }

        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SecurityCritical]
        public static void DestroyStructure<T>(IntPtr ptr)
        {
            Marshal.DestroyStructure<T>(ptr);
        }

        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SecurityCritical]
        public static int FinalReleaseComObject(object o)
        {
            return Marshal.FinalReleaseComObject(o);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FreeBSTR(IntPtr ptr)
        {
            Marshal.FreeBSTR(ptr);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FreeCoTaskMem(IntPtr ptr)
        {
            Marshal.FreeCoTaskMem(ptr);
        }

        /// <summary>
        /// 将数据从托管对象封送到非托管内存块
        /// </summary>
        /// <typeparam name="T">保存要封送的数据的托管对象 此对象必须是格式化类的结构或实例</typeparam>
        /// <param name="obj">实例</param>
        /// <param name="ptr">指向非托管内存块的指针，在调用此方法之前必须分配该内存块</param>
        /// <param name="fDeleteOld">在ptr参数上使用DestroyStructure方法复制数据</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StructureToPtr<T>(T obj, IntPtr ptr, bool fDeleteOld = true)
        {
            Marshal.StructureToPtr(obj, ptr, fDeleteOld);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowExceptionForHR(int errorCode)
        {
            Marshal.ThrowExceptionForHR(errorCode);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowExceptionForHR(int errorCode, IntPtr errorInfo)
        {
            Marshal.ThrowExceptionForHR(errorCode, errorInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntPtr UnsafeAddrOfPinnedArrayElement(Array arr, int index)
        {
            return Marshal.UnsafeAddrOfPinnedArrayElement(arr, index);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntPtr UnsafeAddrOfPinnedArrayElement<T>(T[] arr, int index)
        {
            return Marshal.UnsafeAddrOfPinnedArrayElement(arr, index);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int QueryInterface(IntPtr pUnk, ref System.Guid iid, out IntPtr ppv)
        {
            return Marshal.QueryInterface(pUnk, ref iid, out ppv);
        }

        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SecuritySafeCritical]
        public static bool IsComObject(object o)
        {
            return Marshal.IsComObject(o);
        }

        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntPtr OffsetOf(Type t, string fieldName)
        {
            return Marshal.OffsetOf(t, fieldName);
        }

        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntPtr OffsetOf<T>(string fieldName)
        {
            return Marshal.OffsetOf<T>(fieldName);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Prelink(MethodInfo m)
        {
            Marshal.Prelink(m);
        }

        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SecurityCritical]
        public static void PrelinkAll(Type c)
        {
            Marshal.PrelinkAll(c);
        }

        #region String

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static IntPtr StringToBSTR(string s)
        {
            return Marshal.StringToBSTR(s);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static IntPtr StringToCoTaskMemAnsi(string s)
        {
            return Marshal.StringToCoTaskMemAnsi(s);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static IntPtr StringToCoTaskMemAuto(string s)
        {
            return Marshal.StringToCoTaskMemAuto(s);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static IntPtr StringToCoTaskMemUni(string s)
        {
            return Marshal.StringToCoTaskMemUni(s);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static IntPtr StringToHGlobalAnsi(string s)
        {
            return Marshal.StringToHGlobalAnsi(s);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static IntPtr StringToHGlobalAuto(string s)
        {
            return Marshal.StringToHGlobalAuto(s);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static IntPtr StringToHGlobalUni(string s)
        {
            return Marshal.StringToHGlobalUni(s);
        }

        #endregion

        #region Secure String

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static IntPtr SecureStringToBSTR(SecureString s)
        {
            return Marshal.SecureStringToBSTR(s);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static IntPtr SecureStringToCoTaskMemAnsi(SecureString s)
        {
            return Marshal.SecureStringToCoTaskMemAnsi(s);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static IntPtr SecureStringToCoTaskMemUnicode(SecureString s)
        {
            return Marshal.SecureStringToCoTaskMemUnicode(s);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static IntPtr SecureStringToGlobalAllocAnsi(SecureString s)
        {
            return Marshal.SecureStringToGlobalAllocAnsi(s);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static IntPtr SecureStringToGlobalAllocUnicode(SecureString s)
        {
            return Marshal.SecureStringToGlobalAllocUnicode(s);
        }

        #endregion

        #region Re Alloc

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static IntPtr ReAllocCoTaskMem(IntPtr pv, int cb)
        {
            return Marshal.ReAllocCoTaskMem(pv, cb);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static IntPtr ReAllocHGlobal(IntPtr pv, IntPtr cb)
        {
            return Marshal.ReAllocHGlobal(pv, cb);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static int Release(IntPtr pUnk)
        {
            return Marshal.Release(pUnk);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static int ReleaseComObject(object o)
        {
            return Marshal.ReleaseComObject(o);
        }

        #endregion

        #region PtrToString

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static string PtrToStringAnsi(IntPtr ptr, int len)
        {
            return Marshal.PtrToStringAnsi(ptr, len);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static string PtrToStringAnsi(IntPtr ptr)
        {
            return Marshal.PtrToStringAnsi(ptr);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static string PtrToStringAuto(IntPtr ptr)
        {
            return Marshal.PtrToStringAuto(ptr);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static string PtrToStringAuto(IntPtr ptr, int len)
        {
            return Marshal.PtrToStringAuto(ptr, len);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static string PtrToStringBSTR(IntPtr ptr)
        {
            return Marshal.PtrToStringBSTR(ptr);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static string PtrToStringUni(IntPtr ptr)
        {
            return Marshal.PtrToStringUni(ptr);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static string PtrToStringUni(IntPtr ptr, int len)
        {
            return Marshal.PtrToStringUni(ptr, len);
        }

        /// <summary>
        /// 
        /// </summary>
        [ComVisible(true), SecurityCritical]
        public static void PtrToStructure(IntPtr ptr, object structure)
        {
            Marshal.PtrToStructure(ptr, structure);
        }

        /// <summary>
        /// 
        /// </summary>
        [ComVisible(true), SecurityCritical]
        public static object PtrToStructure(IntPtr ptr, Type structureType)
        {
            return Marshal.PtrToStructure(ptr, structureType);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static T PtrToStructure<T>(IntPtr ptr)
        {
            return Marshal.PtrToStructure<T>(ptr);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        public static void PtrToStructure<T>(IntPtr ptr, T structure)
        {
            Marshal.PtrToStructure(ptr, structure);
        }

        #endregion

        #region Get

        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SecurityCritical]
        public static IntPtr GetComInterfaceForObject(object o, Type T, CustomQueryInterfaceMode mode)
        {
            return Marshal.GetComInterfaceForObject(o, T, mode);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntPtr GetComInterfaceForObject<T, TInterface>(T o)
        {
            return Marshal.GetComInterfaceForObject<T, TInterface>(o);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntPtr GetComInterfaceForObject(object o, Type T)
        {
            return Marshal.GetComInterfaceForObject(o, T);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Delegate GetDelegateForFunctionPointer(IntPtr ptr, Type t)
        {
            return Marshal.GetDelegateForFunctionPointer(ptr, t);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TDelegate GetDelegateForFunctionPointer<TDelegate>(IntPtr ptr)
        {
            return Marshal.GetDelegateForFunctionPointer<TDelegate>(ptr);
        }

#if UNITY_2021_1_OR_NEWER
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [Obsolete("GetExceptionCode() may be unavailable in future releases.")]
#endif
        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetExceptionCode()
        {
            return Marshal.GetExceptionCode();
        }

        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SecurityCritical]
        public static Exception GetExceptionForHR(int errorCode)
        {
            return Marshal.GetExceptionForHR(errorCode);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Exception GetExceptionForHR(int errorCode, IntPtr errorInfo)
        {
            return Marshal.GetExceptionForHR(errorCode, errorInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntPtr GetFunctionPointerForDelegate(Delegate d)
        {
            return Marshal.GetFunctionPointerForDelegate(d);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntPtr GetFunctionPointerForDelegate<TDelegate>(TDelegate d)
        {
            return Marshal.GetFunctionPointerForDelegate(d);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetHRForException(Exception e)
        {
            return Marshal.GetHRForException(e);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetHRForLastWin32Error()
        {
            return Marshal.GetHRForLastWin32Error();
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntPtr GetIUnknownForObject(object o)
        {
            return Marshal.GetIUnknownForObject(o);
        }

        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), SecurityCritical]
        public static int GetLastWin32Error()
        {
            return Marshal.GetLastWin32Error();
        }

#if UNITY_2021_1_OR_NEWER
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [Obsolete("GetNativeVariantForObject(Object, IntPtr) may be unavailable in future releases.")]
#endif
        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SecurityCritical]
        public static void GetNativeVariantForObject(object obj, IntPtr pDstNativeVariant)
        {
            Marshal.GetNativeVariantForObject(obj, pDstNativeVariant);
        }

#if UNITY_2021_1_OR_NEWER
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [Obsolete("GetNativeVariantForObject<T>(T, IntPtr) may be unavailable in future releases.")]
#endif
        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SecurityCritical]
        public static void GetNativeVariantForObject<T>(T obj, IntPtr pDstNativeVariant)
        {
            Marshal.GetNativeVariantForObject(obj, pDstNativeVariant);
        }

        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SecurityCritical]
        public static object GetObjectForIUnknown(IntPtr pUnk)
        {
            return Marshal.GetObjectForIUnknown(pUnk);
        }

        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SecurityCritical]
        public static object GetObjectForNativeVariant(IntPtr pSrcNativeVariant)
        {
            return Marshal.GetObjectForIUnknown(pSrcNativeVariant);
        }

#if UNITY_2021_1_OR_NEWER
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [Obsolete("GetObjectForNativeVariant<T>(IntPtr) may be unavailable in future releases.")]
#endif
        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetObjectForNativeVariant<T>(IntPtr pSrcNativeVariant)
        {
            return Marshal.GetObjectForNativeVariant<T>(pSrcNativeVariant);
        }

#if UNITY_2021_1_OR_NEWER
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [Obsolete("GetObjectsForNativeVariants(IntPtr, Int32) may be unavailable in future releases.")]
#endif

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object[] GetObjectsForNativeVariants(IntPtr aSrcNativeVariant, int cVars)
        {
            return Marshal.GetObjectsForNativeVariants(aSrcNativeVariant, cVars);
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SecurityCritical]
        public static T[] GetObjectsForNativeVariants<T>(IntPtr aSrcNativeVariant, int cVars)
        {
            return Marshal.GetObjectsForNativeVariants<T>(aSrcNativeVariant, cVars);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [SecurityCritical]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetStartComSlot(Type t)
        {
            return Marshal.GetStartComSlot(t);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clsid"></param>
        /// <returns></returns>
        [SecuritySafeCritical]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type GetTypeFromCLSID(System.Guid clsid)
        {
            return Marshal.GetTypeFromCLSID(clsid);
        }

        /// <summary>
        /// 
        /// </summary>
        [SecurityCritical]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetTypeInfoName(ITypeInfo typeInfo)
        {
            return Marshal.GetTypeInfoName(typeInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SecurityCritical]
        public static object GetUniqueObjectForIUnknown(IntPtr unknown)
        {
            return Marshal.GetUniqueObjectForIUnknown(unknown);
        }

        #endregion
    }
}
﻿#region

using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

#endregion

namespace AIO
{
    partial class ExtendIEnumerable
    {
        /// <summary>
        /// 转化为字符串 UTF8格式
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToConvertStringUTF8(this IEnumerable<byte> bytes)
        {
            return Encoding.UTF8.GetString(bytes.ToArray());
        }

        /// <summary>
        /// 获取指定字节数组转化为字符串 UTF8格式
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToConvertStringUTF8(this IEnumerable<byte> bytes, in int offset, in int count)
        {
            return Encoding.UTF8.GetString(bytes.ToArray(), offset, count);
        }

        /// <summary>
        /// 字节数组 转小写16进制字符
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToConvertHex16x(this IEnumerable<byte> bytes)
        {
            var sb = new StringBuilder();
            foreach (var b in bytes) sb.Append(b.ToString("x2"));
            return sb.ToString();
        }

        /// <summary>
        /// 字节数组 转大写16进制字符
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToConvertHex16X(this IEnumerable<byte> bytes)
        {
            var sb = new StringBuilder();
            foreach (var b in bytes) sb.Append(b.ToString("X2"));
            return sb.ToString();
        }
    }
}
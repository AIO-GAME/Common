﻿using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace AIO
{
    public partial class ExtendISpan
    {
        /// <summary>
        /// 转化为字符串
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToConverString(this byte[] bytes)
        {
            return Encoding.Default.GetString(bytes);
        }

        /// <summary>
        /// 获取指定字节数组转化为字符串
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToConverString(this byte[] bytes, in int offset, in int count)
        {
            return Encoding.Default.GetString(bytes, offset, count);
        }

        /// <summary>
        /// 转化为二进制文本
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToHexString(this byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", "");
        }

        /// <summary>
        /// 转化为字符串 UTF8格式
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToConverStringUTF8(this byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
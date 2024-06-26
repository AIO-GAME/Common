﻿#region

using System;
using System.Security.Cryptography;
using System.Text;

#endregion

namespace AIO
{
    public partial class AHelper
    {
        #region Nested type: Encrypt

        /// <summary>
        /// Encrypt 加密
        /// </summary>
        public class Encrypt
        {
            private Encrypt() { }

            /// <summary>
            /// HmacSHA256 单向散列函数
            /// </summary>
            /// <param name="secret">签名</param>
            /// <param name="signKey">签名字符串</param>
            /// <param name="encoding">编码</param>
            /// <returns>签名</returns>
            public static string HmacSHA256ToBase64(string secret, string signKey, Encoding encoding = null)
            {
                encoding ??= Encoding.UTF8;
                using var mac = new HMACSHA256(encoding.GetBytes(signKey));
                var hash = mac.ComputeHash(encoding.GetBytes(secret));
                return Convert.ToBase64String(hash);
            }

            /// <summary>
            /// HmacSHA256 单向散列函数
            /// </summary>
            /// <param name="secret">签名</param>
            /// <param name="signKey">签名字符串</param>
            /// <param name="encoding">编码</param>
            /// <returns>签名</returns>
            public static byte[] HmacSHA256(string secret, string signKey, Encoding encoding = null)
            {
                encoding ??= Encoding.UTF8;
                var keyByte = encoding.GetBytes(secret);
                var messageBytes = encoding.GetBytes(signKey);
                using var mac = new HMACSHA256(keyByte);
                return mac.ComputeHash(messageBytes);
            }
        }

        #endregion
    }
}
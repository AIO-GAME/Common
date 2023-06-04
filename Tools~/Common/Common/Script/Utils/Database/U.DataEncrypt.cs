using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public partial class Utils
{
    /// <summary>
    /// 数据加密解密
    /// </summary>
    public static class DataEncrypt
    {
        internal static readonly byte[] RGBKey =
        {
            (byte)20,
            (byte)'b',
            (byte)'l',
            (byte)'a',
            (byte)'z',
            (byte)'e',
            (byte)141,
            (byte)'j',
            (byte)'o',
            (byte)'y',
            (byte)21,
            (byte)'p',
            (byte)'r',
            (byte)'0',
            (byte)'j',
            (byte)'6',
        };

        internal static readonly byte[] RGBIV =
        {
            (byte)131,
            (byte)'L',
            (byte)'L',
            (byte)'M',
            (byte)167,
            (byte)'s',
            (byte)'t',
            (byte)'e',
            (byte)'a',
            (byte)'l',
            (byte)'d',
            (byte)'r',
            (byte)'e',
            (byte)'a',
            (byte)'m',
            (byte)201,
        };

        #region Encrypt

        private static byte[] Crypt(byte[] data, ICryptoTransform cryptor)
        {
            using (var m = new MemoryStream())
            {
                using (var c = new CryptoStream(m, cryptor, CryptoStreamMode.Write))
                {
                    c.Write(data, 0, data.Length);
                }

                return m.ToArray();
            }
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data">数据源</param>
        /// <param name="rgbKey">对称算法的密钥</param>
        /// <param name="rgbIV">对称算法的初始化向量</param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] data, byte[] rgbKey, byte[] rgbIV)
        {
            using (var algorithm = Aes.Create())
            {
                using (var encryptor = algorithm.CreateEncryptor(rgbKey, rgbIV))
                {
                    return Crypt(data, encryptor);
                }
            }
        }

        /// <summary>
        /// 加密
        /// </summary>
        public static byte[] Encrypt(byte[] data)
        {
            return Encrypt(data, RGBKey, RGBIV);
        }

        /// <summary>
        /// 加密
        /// </summary>
        public static byte[] Encrypt(string data)
        {
            return Encrypt(Encoding.UTF8.GetBytes(data), RGBKey, RGBIV);
        }

        /// <summary>
        /// 加密
        /// </summary>
        public static string EncryptToStr(string data)
        {
            return Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(data), RGBKey, RGBIV));
        }

        #endregion

        #region Decrypt

        /// <summary>
        /// 解码 
        /// </summary>
        /// <param name="data">数据源</param>
        /// <param name="rgbKey">对称算法的密钥</param>
        /// <param name="rgbIV">对称算法的初始化向量</param>
        public static byte[] Decrypt(byte[] data, byte[] rgbKey, byte[] rgbIV)
        {
            using (var algorithm = Aes.Create())
            {
                using (var decryptor = algorithm.CreateDecryptor(rgbKey, rgbIV))
                {
                    return Crypt(data, decryptor);
                }
            }
        }

        /// <summary>
        /// 解码 
        /// </summary>
        public static byte[] Decrypt(byte[] data)
        {
            return Decrypt(data, RGBKey, RGBIV);
        }

        /// <summary>
        /// 解码 
        /// </summary>
        public static string DecryptToStr(byte[] data)
        {
            return Encoding.UTF8.GetString(Decrypt(data, RGBKey, RGBIV));
        }

        /// <summary>
        /// 解码 
        /// </summary>
        public static string Decrypt(string data)
        {
            return Encoding.UTF8.GetString(Decrypt(Convert.FromBase64String(data), RGBKey, RGBIV));
        }

        #endregion
    }
}
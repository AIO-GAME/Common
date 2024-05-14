using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AIO.Security
{
    public static class DESCEncryption
    {
        /// <summary> 
        /// 加密字符串
        /// 加密密钥必须为8位
        /// </summary> 
        /// <param name="strText">被加密的字符串</param> 
        /// <param name="key">8位长度密钥</param> 
        /// <returns>加密后的数据</returns> 
        public static string DesEncrypt(this string strText, string key)
        {
            if (key.Length < 8) throw new Exception("密钥长度无效，密钥必须是8位！");

            var       ret            = new StringBuilder();
            using var des            = DES.Create();
            var       inputByteArray = Encoding.Default.GetBytes(strText);
            des.Key = Encoding.ASCII.GetBytes(key.Substring(0, 8));
            des.IV  = Encoding.ASCII.GetBytes(key.Substring(0, 8));
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            foreach (var b in ms.ToArray()) ret.AppendFormat("{0:X2}", b);
            return ret.ToString();
        }

        /// <summary> 
        /// 加密字符串
        /// 加密密钥必须为8位
        /// </summary> 
        /// <param name="strText">被加密的字符串</param>
        /// <param name="desKey"></param>
        /// <param name="desIV"></param>
        /// <returns>加密后的数据</returns> 
        public static string DesEncrypt(this string strText, byte[] desKey, byte[] desIV)
        {
            var       ret            = new StringBuilder();
            using var des            = DES.Create();
            var       inputByteArray = Encoding.Default.GetBytes(strText);
            des.Key = desKey;
            des.IV  = desIV;
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            foreach (var b in ms.ToArray()) ret.AppendFormat($"{b:X2}");
            return ret.ToString();
        }

        /// <summary>
        /// DES加密文件
        /// </summary>
        /// <param name="fin">文件输入流</param>
        /// <param name="outFilePath">文件输出路径</param>
        /// <param name="key">加密密钥</param>
        public static void DesEncrypt(this FileStream fin, string outFilePath, string key)
        {
            byte[] iv =
            {
                0x12,
                0x34,
                0x56,
                0x78,
                0x90,
                0xAB,
                0xCD,
                0xEF
            };
            var       byKey      = Encoding.UTF8.GetBytes(key.Substring(0, 8));
            using var fileStream = new FileStream(outFilePath, FileMode.OpenOrCreate, FileAccess.Write);
            fileStream.SetLength(0);
            var       bin         = new byte[100];
            long      lengthTotal = 0;
            var       length      = fin.Length;
            using var des         = DES.Create();
            var       encStream   = new CryptoStream(fileStream, des.CreateEncryptor(byKey, iv), CryptoStreamMode.Write);
            while (lengthTotal < length)
            {
                var len = fin.Read(bin, 0, 100);
                encStream.Write(bin, 0, len);
                lengthTotal += len;
            }
        }

        /// <summary>
        /// DES加密文件
        /// </summary>
        /// <param name="fin">文件输入流</param>
        /// <param name="outFilePath">文件输出路径</param>
        /// <param name="desKey"></param>
        /// <param name="desIV"></param>
        public static void DesEncrypt(this FileStream fin, string outFilePath, byte[] desKey, byte[] desIV)
        {
            using var fileStream = new FileStream(outFilePath, FileMode.OpenOrCreate, FileAccess.Write);
            fileStream.SetLength(0);
            var       bin         = new byte[100];
            long      lengthTotal = 0;
            var       length      = fin.Length;
            using var des         = DES.Create();
            var       encStream   = new CryptoStream(fileStream, des.CreateEncryptor(desKey, desIV), CryptoStreamMode.Write);
            while (lengthTotal < length)
            {
                var len = fin.Read(bin, 0, 100);
                encStream.Write(bin, 0, len);
                lengthTotal += len;
            }
        }

        /// <summary>
        /// DES解密文件
        /// </summary>
        /// <param name="fin">输入文件流</param>
        /// <param name="outFilePath">文件输出路径</param>
        /// <param name="sDecrKey">解密密钥</param>
        public static void DesDecrypt(this FileStream fin, string outFilePath, string sDecrKey)
        {
            byte[] iv =
            {
                0x12,
                0x34,
                0x56,
                0x78,
                0x90,
                0xAB,
                0xCD,
                0xEF
            };
            var       byKey      = Encoding.UTF8.GetBytes(sDecrKey.Substring(0, 8));
            using var fileStream = new FileStream(outFilePath, FileMode.OpenOrCreate, FileAccess.Write);
            fileStream.SetLength(0);
            var       bin         = new byte[100];
            long      lengthTotal = 0;
            var       length      = fin.Length;
            using var des         = DES.Create();
            var       encStream   = new CryptoStream(fileStream, des.CreateDecryptor(byKey, iv), CryptoStreamMode.Write);
            while (lengthTotal < length)
            {
                var len = fin.Read(bin, 0, 100);
                encStream.Write(bin, 0, len);
                lengthTotal += len;
            }
        }

        /// <summary>
        /// DES解密文件
        /// </summary>
        /// <param name="fin">输入文件流</param>
        /// <param name="outFilePath">文件输出路径</param>
        /// <param name="desKey"></param>
        /// <param name="desIV"></param>
        public static void DesDecrypt(this FileStream fin, string outFilePath, byte[] desKey, byte[] desIV)
        {
            using var fileStream = new FileStream(outFilePath, FileMode.OpenOrCreate, FileAccess.Write);
            fileStream.SetLength(0);
            var       bin         = new byte[100];
            long      lengthTotal = 0;
            var       length      = fin.Length;
            using var des         = DES.Create();
            var       encStream   = new CryptoStream(fileStream, des.CreateDecryptor(desKey, desIV), CryptoStreamMode.Write);
            while (lengthTotal < length)
            {
                var len = fin.Read(bin, 0, 100);
                encStream.Write(bin, 0, len);
                lengthTotal += len;
            }
        }

        /// <summary>
        ///     DES解密算法
        ///     密钥为8位
        /// </summary>
        /// <param name="pToDecrypt">需要解密的字符串</param>
        /// <param name="sKey">密钥</param>
        /// <returns>解密后的数据</returns>
        public static string DesDecrypt(this string pToDecrypt, string sKey)
        {
            if (sKey.Length < 8) throw new Exception("密钥长度无效，密钥必须是8位！");

            using var ms             = new MemoryStream();
            using var des            = DES.Create();
            var       inputByteArray = new byte[pToDecrypt.Length / 2];
            for (var x = 0; x < pToDecrypt.Length / 2; x++)
            {
                var i = Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }

            des.Key = Encoding.ASCII.GetBytes(sKey.Substring(0, 8));
            des.IV  = Encoding.ASCII.GetBytes(sKey.Substring(0, 8));
            using var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }

        /// <summary>
        ///     DES解密算法
        ///     密钥为8位
        /// </summary>
        /// <param name="pToDecrypt">需要解密的字符串</param>
        /// <param name="desKey"></param>
        /// <param name="desIV"></param>
        /// <returns>解密后的数据</returns>
        public static string DesDecrypt(this string pToDecrypt, byte[] desKey, byte[] desIV)
        {
            using var ms             = new MemoryStream();
            using var des            = DES.Create();
            var       inputByteArray = new byte[pToDecrypt.Length / 2];
            for (var x = 0; x < pToDecrypt.Length / 2; x++)
            {
                var i = Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }

            des.Key = desKey;
            des.IV  = desIV;
            using var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }
    }
}
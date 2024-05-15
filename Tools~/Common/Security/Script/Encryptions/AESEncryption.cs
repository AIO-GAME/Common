using System;
using System.Buffers;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace AIO.Security
{
    /// <summary>
    /// AES加密
    /// </summary>
    public static class AESEncryption
    {
        private static readonly byte[] Keys =
        {
            0x41, 0x72, 0x65, 0x79, 0x6F, 0x75, 0x6D, 0x79, 0x53, 0x6E, 0x6F, 0x77, 0x6D, 0x61, 0x6E, 0x3F
        };

        /// <summary>
        /// 生成符合AES加密规则的密钥
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerateAesKey(int length)
        {
            using var aes = Aes.Create();
            aes.KeySize   = length;
            aes.BlockSize = 128;
            aes.GenerateKey();
            return Convert.ToBase64String(aes.Key);
        }

        /// <summary>
        /// 对称加密算法AES(块式加密算法)
        /// </summary>
        /// <param name="encryptString">待加密字符串</param>
        /// <param name="encryptKey">加密密钥，须半角字符</param>
        /// <param name="mode">加密模式</param>
        /// <returns>加密结果字符串</returns>
        public static string AESEncrypt(this string encryptString, string encryptKey, CipherMode mode = CipherMode.CBC)
        {
            encryptKey = GetSubString(encryptKey, 32, "");
            encryptKey = encryptKey.PadRight(32, ' ');
            using var aes = Aes.Create();
            aes.Key  = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 32));
            aes.IV   = Keys;
            aes.Mode = mode;
            using var rijndaelEncrypt = aes.CreateEncryptor();
            var       inputData       = Encoding.UTF8.GetBytes(encryptString);
            var       encryptedData   = rijndaelEncrypt.TransformFinalBlock(inputData, 0, inputData.Length);
            return Convert.ToBase64String(encryptedData);
        }

        /// <summary>
        /// 对称加密算法AES加密(块式加密算法)
        /// </summary>
        /// <param name="encryptString">待加密字符串</param>
        /// <param name="options">加密选项</param>
        /// <returns>加密结果字符串</returns>
        public static string AESEncrypt(this string encryptString, Aes options)
        {
            using var rijndaelEncrypt = options.CreateEncryptor();
            var       inputData       = Encoding.UTF8.GetBytes(encryptString);
            var       encryptedData   = rijndaelEncrypt.TransformFinalBlock(inputData, 0, inputData.Length);
            return Convert.ToBase64String(encryptedData);
        }

        /// <summary>
        /// 对称加密算法AES加密(块式加密算法)
        /// </summary>
        /// <param name="encryptString">待加密字符串</param>
        /// <param name="encryptKey">加密密钥，须半角字符</param>
        /// <param name="mode">加密模式</param>
        /// <returns>加密结果字符串</returns>
        public static string AESEncrypt(this string encryptString, byte[] encryptKey, CipherMode mode = CipherMode.CBC)
        {
            using var aes = Aes.Create();
            aes.Key  = encryptKey;
            aes.IV   = Keys;
            aes.Mode = mode;
            using var rijndaelEncrypt = aes.CreateEncryptor();
            var       inputData       = Encoding.UTF8.GetBytes(encryptString);
            var       encryptedData   = rijndaelEncrypt.TransformFinalBlock(inputData, 0, inputData.Length);
            return Convert.ToBase64String(encryptedData);
        }

        /// <summary>
        /// 对称加密算法AES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,和加密密钥相同</param>
        /// <param name="mode">加密模式</param>
        /// <returns>解密成功返回解密后的字符串,失败返回空</returns>
        public static string AESDecrypt(this string decryptString, string decryptKey, CipherMode mode = CipherMode.CBC)
        {
            try
            {
                decryptKey = GetSubString(decryptKey, 32, "");
                decryptKey = decryptKey.PadRight(32, ' ');
                using var aes = Aes.Create();
                aes.Key  = Encoding.UTF8.GetBytes(decryptKey);
                aes.IV   = Keys;
                aes.Mode = mode;
                using var rijndaelDecrypt = aes.CreateDecryptor();
                var       inputData       = Convert.FromBase64String(decryptString);
                var       decryptedData   = rijndaelDecrypt.TransformFinalBlock(inputData, 0, inputData.Length);
                return Encoding.UTF8.GetString(decryptedData);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 对称加密算法AES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="options">加密选项</param>
        /// <returns>解密成功返回解密后的字符串,失败返回空</returns>
        public static string AESDecrypt(this string decryptString, Aes options)
        {
            try
            {
                using var rijndaelDecrypt = options.CreateDecryptor();
                var       inputData       = Convert.FromBase64String(decryptString);
                var       decryptedData   = rijndaelDecrypt.TransformFinalBlock(inputData, 0, inputData.Length);
                return Encoding.UTF8.GetString(decryptedData);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 对称加密算法AES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,和加密密钥相同</param>
        /// <param name="mode">加密模式</param>
        /// <returns>解密成功返回解密后的字符串,失败返回空</returns>
        public static string AESDecrypt(this string decryptString, byte[] decryptKey, CipherMode mode = CipherMode.CBC)
        {
            try
            {
                using var aes = Aes.Create();
                aes.Key  = decryptKey;
                aes.IV   = Keys;
                aes.Mode = mode;
                using var rijndaelDecrypt = aes.CreateDecryptor();
                var       inputData       = Convert.FromBase64String(decryptString);
                var       decryptedData   = rijndaelDecrypt.TransformFinalBlock(inputData, 0, inputData.Length);
                return Encoding.UTF8.GetString(decryptedData);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 按字节长度(按字节,一个汉字为2个字节)取得某字符串的一部分
        /// </summary>
        /// <param name="sourceString">源字符串</param>
        /// <param name="length">所取字符串字节长度</param>
        /// <param name="tailString">附加字符串(当字符串不够长时，尾部所添加的字符串，一般为"...")</param>
        /// <returns>某字符串的一部分</returns>
        private static string GetSubString(this string sourceString, int length, string tailString)
        {
            return GetSubString(sourceString, 0, length, tailString);
        }

        /// <summary>
        /// 按字节长度(按字节,一个汉字为2个字节)取得某字符串的一部分
        /// </summary>
        /// <param name="sourceString">源字符串</param>
        /// <param name="startIndex">索引位置，以0开始</param>
        /// <param name="length">所取字符串字节长度</param>
        /// <param name="tailString">附加字符串(当字符串不够长时，尾部所添加的字符串，一般为"...")</param>
        /// <returns>某字符串的一部分</returns>
        private static string GetSubString(this string sourceString, int startIndex, int length, string tailString)
        {
            //当是日文或韩文时(注:中文的范围:\u4e00 - \u9fa5, 日文在\u0800 - \u4e00, 韩文为\xAC00-\xD7A3)
            if (Regex.IsMatch(sourceString, "[\u0800-\u4e00]+") || Regex.IsMatch(sourceString, "[\xAC00-\xD7A3]+"))
            {
                //当截取的起始位置超出字段串长度时
                if (startIndex >= sourceString.Length)
                {
                    return string.Empty;
                }

                return sourceString.Substring(startIndex,
                                              length + startIndex > sourceString.Length ? (sourceString.Length - startIndex) : length);
            }

            //中文字符，如"中国人民abcd123"
            if (length <= 0)
            {
                return string.Empty;
            }

            var bytesSource = Encoding.Default.GetBytes(sourceString);

            //当字符串长度大于起始位置
            if (bytesSource.Length <= startIndex) return string.Empty;
            var endIndex = bytesSource.Length;

            //当要截取的长度在字符串的有效长度范围内
            if (bytesSource.Length > (startIndex + length))
            {
                endIndex = length + startIndex;
            }
            else
            {
                //当不在有效范围内时,只取到字符串的结尾
                length     = bytesSource.Length - startIndex;
                tailString = "";
            }

            var anResultFlag = new int[length];
            var nFlag        = 0;
            //字节大于127为双字节字符
            for (var i = startIndex; i < endIndex; i++)
            {
                if (bytesSource[i] > 127)
                {
                    nFlag++;
                    if (nFlag == 3)
                    {
                        nFlag = 1;
                    }
                }
                else
                {
                    nFlag = 0;
                }

                anResultFlag[i] = nFlag;
            }

            //最后一个字节为双字节字符的一半
            if ((bytesSource[endIndex - 1] > 127) && (anResultFlag[length - 1] == 1))
            {
                length++;
            }

            var bsResult = new byte[length];
            Array.Copy(bytesSource, startIndex, bsResult, 0, length);
            return string.Concat(Encoding.Default.GetString(bsResult), tailString);
        }

        /// <summary>
        /// 加密文件流
        /// </summary>
        /// <param name="fs">需要加密的文件流</param>
        /// <param name="decryptKey">加密密钥</param>
        /// <param name="mode">加密模式</param>
        /// <returns>加密流</returns>
        public static CryptoStream AESEncryptStream(this FileStream fs,
                                                    string          decryptKey,
                                                    CipherMode      mode = CipherMode.CBC)
        {
            decryptKey = GetSubString(decryptKey, 32, "");
            decryptKey = decryptKey.PadRight(32, ' ');
            using var aes = Aes.Create();
            aes.Key  = Encoding.UTF8.GetBytes(decryptKey);
            aes.IV   = Keys;
            aes.Mode = mode;
            using var encryptor = aes.CreateEncryptor();
            return new CryptoStream(fs, encryptor, CryptoStreamMode.Write);
        }

        /// <summary>
        /// 加密文件流
        /// </summary>
        /// <param name="fs">需要加密的文件流</param>
        /// <param name="decryptKey">加密密钥</param>
        /// <param name="mode">加密模式</param>
        /// <returns>加密流</returns>
        public static CryptoStream AESEncryptStream(this FileStream fs,
                                                    byte[]          decryptKey,
                                                    CipherMode      mode = CipherMode.CBC)
        {
            using var aes = Aes.Create();
            aes.Key  = decryptKey;
            aes.IV   = Keys;
            aes.Mode = mode;
            using var encryptor = aes.CreateEncryptor();
            return new CryptoStream(fs, encryptor, CryptoStreamMode.Write);
        }

        /// <summary>
        /// 解密文件流
        /// </summary>
        /// <param name="fs">需要解密的文件流</param>
        /// <param name="decryptKey">解密密钥</param>
        /// <param name="mode">加密模式</param>
        /// <returns>加密流</returns>
        public static CryptoStream AESDecryptStream(this FileStream fs,
                                                    string          decryptKey,
                                                    CipherMode      mode = CipherMode.CBC)
        {
            decryptKey = GetSubString(decryptKey, 32, "");
            decryptKey = decryptKey.PadRight(32, ' ');
            using var aes = Aes.Create();
            aes.Key  = Encoding.UTF8.GetBytes(decryptKey);
            aes.IV   = Keys;
            aes.Mode = mode;
            using var decryptor = aes.CreateDecryptor();
            return new CryptoStream(fs, decryptor, CryptoStreamMode.Read);
        }

        /// <summary>
        /// 解密文件流
        /// </summary>
        /// <param name="fs">需要解密的文件流</param>
        /// <param name="decryptKey">解密密钥</param>
        /// <param name="mode">加密模式</param>
        /// <returns>加密流</returns>
        public static CryptoStream AESDecryptStream(this FileStream fs,
                                                    byte[]          decryptKey,
                                                    CipherMode      mode = CipherMode.CBC)
        {
            using var aes = Aes.Create();
            aes.Key  = decryptKey;
            aes.IV   = Keys;
            aes.Mode = mode;
            using var transform = aes.CreateDecryptor();
            return new CryptoStream(fs, transform, CryptoStreamMode.Read);
        }

        /// <summary>
        /// 对指定文件AES加密
        /// </summary>
        /// <param name="input">源文件流</param>
        /// <param name="key">加密密钥</param>
        /// <param name="mode">加密模式</param>
        /// <param name="outputPath">输出文件路径</param>
        public static void AESEncryptFile(this FileStream input,
                                          string          outputPath,
                                          string          key,
                                          CipherMode      mode = CipherMode.CBC)
        {
            using var fileStream    = new FileStream(outputPath, FileMode.Create);
            using var encryptStream = AESEncryptStream(fileStream, key, mode);
            var       bytes         = new byte[input.Length];
            _ = input.Read(bytes, 0, bytes.Length);
            encryptStream.Write(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// 对指定的文件AES解密
        /// </summary>
        /// <param name="input">源文件流</param>
        /// <param name="key">解密密钥</param>
        /// <param name="mode">加密模式</param>
        /// <param name="outputPath">输出文件路径</param>
        public static void AESDecryptFile(this FileStream input,
                                          string          outputPath,
                                          string          key,
                                          CipherMode      mode = CipherMode.CBC)
        {
            using var fileStream    = new FileStream(outputPath, FileMode.Create);
            using var decryptStream = AESDecryptStream(input, key, mode);
            var       bytes         = ArrayPool<byte>.Shared.Rent(1024);
            while (true)
            {
                var count = decryptStream.Read(bytes, 0, bytes.Length);
                fileStream.Write(bytes, 0, count);
                if (count < bytes.Length) break;
            }

            ArrayPool<byte>.Shared.Return(bytes);
        }
    }
}
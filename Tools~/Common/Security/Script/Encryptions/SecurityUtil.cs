using System;

namespace AIO.Security
{
    internal class SecurityUtil
    {
        static SecurityUtil()
        {
            AESKey = RandomString(8);
            DesKey = RandomString(8);
        }

        /// <summary>
        /// AED加密密钥
        /// </summary>
        internal static string AESKey { get; set; }

        /// <summary>
        /// Des加密密钥
        /// </summary>
        internal static string DesKey { get; set; }

        /// <summary>
        /// 随机偏移量值
        /// </summary>
        internal static int RandomRange = 60527;

        #region Random

        private static readonly Random _random = new Random();

        /// <summary>
        /// 随机整数
        /// </summary>
        /// <returns> <see cref="int"/> </returns>
        public static int RandomInt()
        {
            return _random.Next(-RandomRange, RandomRange);
        }

        /// <summary>
        /// 随机无符号整数
        /// </summary>
        /// <returns> <see cref="uint"/> </returns>
        public static uint RandomUInt()
        {
            return (uint)_random.Next(0, RandomRange);
        }

        /// <summary>
        /// 随机短整数
        /// </summary>
        /// <returns> <see cref="ushort"/> </returns>
        public static ushort RandomUShort()
        {
            return (ushort)_random.Next(0, 100);
        }

        private const string _randomChars = "ABCDEFGHIJKLMNOPQRSTUWVXYZ0123456789abcdefghijklmnopqrstuvwxyz";

        /// <summary>
        /// 随机字符方法一 遍历返回
        /// </summary>
        /// <param name="length">返回随机的字符串个数</param>
        /// <returns></returns>
        private static string RandomString(int length)
        {
            var str = string.Empty;
            for (var i = 0; i < length; i++)
            {
                str += _randomChars[_random.Next(_randomChars.Length)];
            }

            return str;
        }

        #endregion

        /// <summary>
        /// 内存被修改事件
        /// </summary>
        public static event Action OnMemoryModify;

        internal static void OnDetected()
        {
            OnMemoryModify?.Invoke();
        }
    }
}
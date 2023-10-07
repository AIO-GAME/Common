/*|============|*|
|*|Author:     |*| xinan
|*|Date:       |*| 2023-10-07
|*|E-Mail:     |*| 1398581458@qq.com
|*|============|*/

namespace AIO
{
    /// <summary>
    /// Net 数据加密解密
    /// </summary>
    public interface INetCryptography
    {
        /// <summary>
        /// 加密
        /// </summary>
        byte[] Encrypt(byte[] data);

        /// <summary>
        /// 解密
        /// </summary>
        byte[] Decrypt(byte[] data);
    }
}

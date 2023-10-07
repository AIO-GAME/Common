/*|============|*|
|*|Author:     |*| xinan                
|*|Date:       |*| 2023-10-07               
|*|E-Mail:     |*| 1398581458@qq.com     
|*|============|*/

namespace AIO
{
    /// <summary>
    /// 请求对象序列化
    /// </summary>
    public interface INetSerialization
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="protocol">协议号</param>
        /// <param name="data">元数据</param>
        /// <returns>数据</returns>
        byte[] Serialize<T>(int protocol, T data);
        
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="protocol">协议号</param>
        /// <param name="data">元数据</param>
        /// <returns>对象</returns>
        T Deserialize<T>(int protocol, byte[] data);
    }
}
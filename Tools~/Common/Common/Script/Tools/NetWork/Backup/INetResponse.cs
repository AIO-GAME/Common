// /*|============|*|
// |*|Author:     |*| xinan
// |*|Date:       |*| 2023-10-07
// |*|E-Mail:     |*| 1398581458@qq.com
// |*|============|*/
//
// using System.IO;
// using System.Threading.Tasks;
//
// namespace AIO
// {
//     /// <summary>
//     /// Net 响应
//     /// </summary>
//     public interface INetResponse
//     {
//         /// <summary>
//         /// 协议号
//         /// </summary>
//         int Protocol { get; }
//
//         /// <summary>
//         /// 响应码
//         /// </summary>
//         long ResponseCode { get; }
//
//         /// <summary>
//         /// 响应体
//         /// </summary>
//         object Body { get; }
//
//         /// <summary>
//         /// 请求
//         /// </summary>
//         INetRequest Request { get; }
//
//         /// <summary>
//         /// 获取数据
//         /// </summary>
//         Task<T> GetAsync<T>(T defaultValue = default);
//
//         /// <summary>
//         /// 获取字符串
//         /// </summary>
//         Task<string> GetStringAsync();
//
//         /// <summary>
//         /// 获取流数据
//         /// </summary>
//         Task<Stream> GetStreamAsync();
//
//         /// <summary>
//         /// 获取字节数组
//         /// </summary>
//         Task<byte[]> GetBytesAsync();
//     }
// }
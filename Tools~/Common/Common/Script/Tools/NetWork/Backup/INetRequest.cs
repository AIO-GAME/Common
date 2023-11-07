// /*|============|*|
// |*|Author:     |*| xinan
// |*|Date:       |*| 2023-10-07
// |*|E-Mail:     |*| 1398581458@qq.com
// |*|============|*/
//
// using System;
// using System.IO;
// using System.Threading.Tasks;
//
// namespace AIO
// {
//     /// <summary>
//     /// Net 请求
//     /// </summary>
//     public interface INetRequest
//     {
//         /// <summary>
//         /// 协议号
//         /// </summary>
//         int Protocol { get; }
//
//         /// <summary>
//         /// 原始数据
//         /// </summary>
//         object Data { get; }
//
//         /// <summary> 
//         /// 发送
//         /// </summary>
//         Task<INetResponse> SendAsync();
//
//         /// <summary>
//         /// 发送
//         /// </summary>
//         void Send(Action<INetResponse> callback);
//     }
// }
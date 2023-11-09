// /*|============|*|
// |*|Author:     |*| xinan                
// |*|Date:       |*| 2023-10-07               
// |*|E-Mail:     |*| 1398581458@qq.com     
// |*|============|*/
//
// using System;
// using System.Collections.Generic;
// using System.Net;
// using System.Net.Http;
// using System.Threading.Tasks;
//
// namespace AIO
// {
//     /// <summary>
//     /// HTTP Method
//     /// </summary>
//     public enum HTTPMethod
//     {
//         /// <summary>
//         /// GET
//         /// </summary>
//         GET,
//
//         /// <summary>
//         /// POST
//         /// </summary>
//         POST,
//
//         /// <summary>
//         /// PUT
//         /// </summary>
//         PUT,
//
//         /// <summary>
//         /// DELETE
//         /// </summary>
//         DELETE,
//
//         /// <summary>
//         /// HEAD
//         /// </summary>
//         HEAD,
//
//         /// <summary>
//         /// OPTIONS
//         /// </summary>
//         OPTIONS,
//
//         /// <summary>
//         /// TRACE
//         /// </summary>
//         TRACE,
//
//         /// <summary>
//         /// CONNECT
//         /// </summary>
//         CONNECT,
//
//         /// <summary>
//         /// PATCH
//         /// </summary>
//         PATCH
//     }
//
//     /// <summary>
//     /// Net Http Request
//     /// </summary>
//     public class HTTPRequest : IHTTPRequest
//     {
//         /// <summary>
//         /// 构造函数
//         /// </summary>
//         public HTTPRequest()
//         {
//             Headers = new Dictionary<string, string>();
//         }
//
//         /// <summary>
//         /// 协议号
//         /// </summary>
//         public int Protocol { get; set; }
//
//         /// <summary>
//         /// 原始数据
//         /// </summary>
//         public object Data { get; set; }
//
//         /// <summary>
//         /// 请求地址
//         /// </summary>
//         public string Url { get; set; }
//
//         /// <summary>
//         /// 请求方法
//         /// </summary>
//         public HTTPMethod Method { get; set; }
//
//         #region Headers
//
//         /// <summary>
//         /// 请求Header
//         /// </summary>
//         public IDictionary<string, string> Headers { get; }
//
//         /// <summary>
//         /// UserAgent
//         /// </summary>
//         public string UserAgent
//         {
//             get
//             {
//                 if (Headers.TryGetValue("User-Agent", out var value)) return value;
//                 return string.Empty;
//             }
//             set => Headers["User-Agent"] = value;
//         }
//
//         /// <summary>
//         /// 请求体
//         /// </summary>
//         public string ContentType
//         {
//             get
//             {
//                 if (Headers.TryGetValue("ContentType", out var value)) return value;
//                 return string.Empty;
//             }
//             set => Headers["ContentType"] = value;
//         }
//
//
//         /// <summary>
//         /// Cookie
//         /// </summary>
//         public string Cookie
//         {
//             get
//             {
//                 if (Headers.TryGetValue("Cookie", out var value)) return value;
//                 return string.Empty;
//             }
//             set => Headers["Cookie"] = value;
//         }
//
//         #endregion
//
//         /// <summary>
//         /// 发送
//         /// </summary>
//         public async Task<INetResponse> SendAsync()
//         {
//             HttpContent content = null;
//             switch (Method)
//             {
//                 case HTTPMethod.GET:
//                     content = await GetAsync();
//                     break;
//                 case HTTPMethod.POST:
//                     content = await PostAsync(Data);
//                     break;
//                 default:
//                     throw new Exception($"HTTP 请求方法错误: {Method}");
//             }
//
//             var response = new HTTPResponse
//             {
//                 Protocol = Protocol,
//                 Body = content,
//                 ResponseCode = (int)HttpStatusCode.OK,
//                 Request = this
//             };
//             return response;
//         }
//
//         /// <summary>
//         /// 发送
//         /// </summary>
//         public async void Send(Action<INetResponse> cb)
//         {
//             cb?.Invoke(await SendAsync());
//         }
//
//         /// <summary>
//         /// GET
//         /// </summary>
//         protected async Task<HttpContent> GetAsync()
//         {
//             var client = new HttpClient();
//             if (Headers != null)
//                 foreach (var header in Headers)
//                     client.DefaultRequestHeaders.Add(header.Key, header.Value);
//
//             var request = await client.GetAsync(Url);
//             request.EnsureSuccessStatusCode();
//             return request.Content;
//         }
//
//         /// <summary>
//         /// GET
//         /// </summary>
//         protected async void Get(Action<HttpContent> cb)
//         {
//             cb.Invoke(await GetAsync());
//         }
//
//         /// <summary>
//         /// Post
//         /// </summary>
//         protected async Task<HttpContent> PostAsync<T>(T data)
//         {
//             if (data is null) throw new ArgumentNullException($"HTTP 请求数据不能为空");
//
//             var client = new HttpClient();
//             if (Headers != null)
//                 foreach (var header in Headers)
//                     client.DefaultRequestHeaders.Add(header.Key, header.Value);
//
//             var request = await client.PostAsync(Url, new StringContent(AHelper.Json.Serialize(data)));
//             request.EnsureSuccessStatusCode();
//             return request.Content;
//         }
//
//         /// <summary>
//         /// Post
//         /// </summary>
//         protected async void Post<T>(T data, Action<HttpContent> cb)
//         {
//             cb.Invoke(await PostAsync(data));
//         }
//     }
// }
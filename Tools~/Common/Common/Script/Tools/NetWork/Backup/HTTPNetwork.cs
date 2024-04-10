// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.IO.Compression;
// using System.Net.Http;
// using System.Threading.Tasks;
// using AIO.Net;
// using HttpClient = System.Net.Http.HttpClient;
//
// namespace AIO
// {
//     /// <summary>
//     /// HTTP
//     /// </summary>
//     public class HTTPNetwork : Network
//     {
//         /// <summary>
//         /// 请求队列
//         /// </summary>
//         private Queue<IHTTPRequest> requestQueue;
//
//         /// <summary>
//         /// 无需遮罩的请求队列
//         /// </summary>
//         private Queue<IHTTPRequest> waitingNoLoadingReqs;
//
//         /// <summary>
//         /// 重复请求队列
//         /// </summary>
//         private Queue<IHTTPRequest> waitingRepeatReqs;
//
//         /// <summary>
//         /// 响应
//         /// </summary>
//         private INetResponse response;
//
//         /// <summary>
//         /// 令牌
//         /// </summary>
//         public string Token { get; private set; }
//
//         /// <summary>
//         /// 构造函数
//         /// </summary>
//         public HTTPNetwork()
//         {
//             requestQueue = Pool.Queue<IHTTPRequest>();
//             waitingNoLoadingReqs = Pool.Queue<IHTTPRequest>();
//             waitingRepeatReqs = Pool.Queue<IHTTPRequest>();
//         }
//
//         /// <summary>
//         /// 连接
//         /// </summary>
//         protected override void OnConnect()
//         {
//         }
//
//         /// <summary>
//         /// 关闭连接
//         /// </summary>
//         protected override void OnDisconnect()
//         {
//             requestQueue.Clear();
//             waitingRepeatReqs.Clear();
//             waitingNoLoadingReqs.Clear();
//         }
//
//         /// <summary>
//         /// 执行与释放或重置非托管资源关联的应用程序定义的任务
//         /// </summary>
//         protected override void OnDispose()
//         {
//             requestQueue.Free();
//             waitingRepeatReqs.Free();
//             waitingNoLoadingReqs.Free();
//         }
//
//         protected virtual void POST()
//         {
//         }
//
//         protected virtual void GET()
//         {
//         }
//
//         private void DecryptTread(INetRequest httpReq, byte[] tmpData)
//         {
//             byte[] rawBody = tmpData;
//             LogTimeStamp($"解密【前】长度：{rawBody.Length}字节", httpReq.Protocol);
//             if (Encrypt)
//             {
//                 try
//                 {
//                     tmpData = Cryptography.Decrypt(tmpData);
//
//                     LogTimeStamp($"解密【后】长度：{tmpData.Length}字节", httpReq.Protocol);
//                     using (var inputStream = new MemoryStream(tmpData))
//                     {
//                         using (var gz = new GZipStream(inputStream, CompressionMode.Decompress, true))
//                         {
//                             using (var outStream = new MemoryStream())
//                             {
//                                 gz.CopyTo(outStream);
//                                 rawBody = outStream.ToArray();
//                             }
//                         }
//                     }
//
//                     LogTimeStamp($"解压后长度：{rawBody.Length}字节", httpReq.Protocol);
//                     //对象编码
//                     var body = ReqSerialization.Deserialize<object>(httpReq.Protocol, rawBody);
//                     decryptMutex.WaitOne();
//                     // response.Body = body;
//                     decryptMutex.ReleaseMutex();
//                 }
//                 catch (Exception e)
//                 {
//                     Error?.Invoke($"HTTP Decrypt处理错误, 协议号{httpReq.Protocol}");
//                     Exception?.Invoke(e);
//                 }
//             }
//         }
//
//         /// <summary>
//         /// POST 完成 
//         /// </summary>
//         public Action<IHTTPRequest> POSTFinish;
//
//         /// <summary>
//         /// 发送
//         /// </summary>
//         /// <param name="request"></param>
//         public void Send(INetRequest request)
//         {
//             Guard.OnSend(request);
//             OnSend();
//         }
//
//         private int GetQueueCount()
//         {
//             return requestQueue.Count + waitingRepeatReqs.Count + waitingNoLoadingReqs.Count;
//         }
//
//         private IHTTPRequest GetRequest()
//         {
//             if (requestQueue.Count > 0) return requestQueue.Dequeue();
//             if (waitingNoLoadingReqs.Count > 0) return waitingNoLoadingReqs.Dequeue();
//             if (waitingRepeatReqs.Count > 0) return waitingRepeatReqs.Dequeue();
//             return null;
//         }
//
//         /// <summary>
//         /// 
//         /// </summary>
//         /// <exception cref="Exception"></exception>
//         public async Task AsyncSend()
//         {
//             while (GetQueueCount() > 0)
//             {
//                 var httpReq = GetRequest();
//                 if (httpReq == null) break;
//                 RefreshEncryptTime();
//                 byte[] sendData = null;
//                 try
//                 {
//                     sendData = ReqSerialization.Serialize(httpReq.Protocol, httpReq);
//                     LogTimeStamp("发起显示遮罩及序列化", httpReq.Protocol);
//                 }
//                 catch (Exception e)
//                 {
//                     Error($"HTTP 对象编码错误.");
//                     Exception(e);
//                     continue;
//                 }
//
//                 if (Encrypt)
//                 {
//                     try
//                     {
//                         sendData = Cryptography.Encrypt(sendData);
//                     }
//                     catch (Exception e)
//                     {
//                         Error($"HTTP 对象加密错误");
//                         Exception(e);
//                         continue;
//                     }
//                 }
//
//                 LogTimeStamp("加密", httpReq.Protocol);
//
//                 HttpClient client = null;
//                 for (var i = 0; i < MaxRetryCount; i++)
//                 {
//                     client = new HttpClient();
//                     await client.GetStreamAsync(httpReq.Url);
//
//                     client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");
//
//                     if (httpReq.Headers != null)
//                     {
//                         foreach (var header in httpReq.Headers)
//                             client.DefaultRequestHeaders.Add(header.Key, header.Value);
//                     }
//
//                     if (sendData == null || sendData.Length == 0)
//                     {
//                         throw new Exception("POST 模式下不允许空表单");
//                     }
//
//                     HttpContent content = new ByteArrayContent(sendData);
//                     content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
//                     var message = await client.PostAsync(httpReq.Url, content);
//                     if (message.IsSuccessStatusCode)
//                     {
//                     }
//                     else
//                     {
//                         Error(
//                             $"网络请求失败，准备第{i + 2}次重试！\nprotocal:{httpReq.Protocol}|error:{message.StatusCode}|responseCode:{message.StatusCode}");
//                     }
//
//                     //set content type default
//                 }
//             }
//         }
//
//         /// <summary>
//         /// 
//         /// </summary>
//         protected virtual void OnSend()
//         {
//         }
//     }
// }


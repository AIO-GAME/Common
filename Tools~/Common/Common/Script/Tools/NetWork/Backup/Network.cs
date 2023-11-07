// /*|============|*|
// |*|Author:     |*| xinan
// |*|Date:       |*| 2023-10-07
// |*|E-Mail:     |*| 1398581458@qq.com
// |*|============|*/
//
// using System;
// using System.Threading;
//
// namespace AIO
// {
//     /// <summary>
//     /// Net work
//     /// </summary>
//     public abstract class Network : INetwork, IDisposable
//     {
//         /// <summary>
//         /// 加密解密
//         /// /// </summary>
//         public INetCryptography Cryptography { get; protected set; }
//
//         /// <summary>
//         /// 序列化
//         /// </summary>
//         public INetSerialization ReqSerialization { get; protected set; }
//
//         /// <summary>
//         /// 流程控制
//         /// </summary>
//         public INetGuard Guard { get; protected set; }
//
//         /// <summary>
//         /// 主机地址
//         /// </summary>
//         public string Host { get; private set; }
//
//         /// <summary>
//         /// 是否加密
//         /// </summary>
//         public bool Encrypt { get; protected set; }
//
//         /// <summary>
//         /// 端口号
//         /// </summary>
//         public int Port { get; private set; }
//
//         /// <summary>
//         /// 是否使用SSL
//         /// </summary>
//         public bool SSL { get; private set; }
//
//         /// <summary>
//         /// 是否已连接
//         /// </summary>
//         public bool IsConnected { get; private set; }
//
//         /// <summary>
//         /// 最大重试次数
//         /// </summary>
//         public int MaxRetryCount { get; set; }
//
//         /// <summary>
//         /// 解密时间
//         /// </summary>
//         protected static long EncryptTime { get; private set; }
//
//         /// <summary>
//         /// 互斥锁
//         /// </summary>
//         protected Mutex decryptMutex { get; private set; }
//
//         /// <summary>
//         /// Uri构造器
//         /// </summary>
//         protected UriBuilder UriBuilder { get; private set; }
//
//         /// <summary>
//         /// 构造函数
//         /// </summary>
//         public Network()
//         {
//             decryptMutex = new Mutex();
//             RefreshEncryptTime();
//         }
//
//         /// <summary>
//         /// 连接
//         /// </summary>
//         protected abstract void OnConnect();
//
//         /// <summary>
//         /// 关闭连接
//         /// </summary>
//         protected abstract void OnDisconnect();
//
//         /// <summary>
//         /// 执行与释放或重置非托管资源关联的应用程序定义的任务
//         /// </summary>
//         protected virtual void OnDispose()
//         {
//         }
//
//         /// <summary>
//         /// 连接
//         /// </summary>
//         /// <param name="host">地址</param>
//         /// <param name="port">端口</param>
//         /// <param name="ssl">是否使用SSL</param>
//         public void Connect(string host, int port, bool ssl)
//         {
//             Host = host;
//             Port = port;
//             SSL = ssl;
//             UriBuilder = new UriBuilder(Host);
//             if (Port > 0) UriBuilder.Port = Port;
//             UriBuilder.Scheme = SSL ? "https" : "http";
//             OnConnect();
//             IsConnected = true;
//         }
//
//         /// <summary>
//         /// 关闭连接
//         /// </summary>
//         public void Disconnect()
//         {
//             IsConnected = false;
//             OnDisconnect();
//         }
//
//         /// <summary>
//         /// 执行与释放或重置非托管资源关联的应用程序定义的任务
//         /// </summary>
//         public void Dispose()
//         {
//             OnDispose();
//         }
//
//         /// <summary>确定指定的对象是否等于当前对象。</summary>
//         /// <param name="obj">要与当前对象进行比较的对象。</param>
//         /// <returns>
//         ///   如果指定的对象等于当前对象，则为 <see langword="true" />，否则为 <see langword="false" />。
//         /// </returns>
//         public sealed override bool Equals(object obj)
//         {
//             return false;
//         }
//
//         /// <summary>返回表示当前对象的字符串。</summary>
//         /// <returns>表示当前对象的字符串。</returns>
//         public sealed override string ToString()
//         {
//             return base.ToString();
//         }
//
//         /// <summary>作为默认哈希函数。</summary>
//         /// <returns>当前对象的哈希代码。</returns>
//         public sealed override int GetHashCode()
//         {
//             return 0;
//         }
//
//         /// <summary>
//         /// 日志
//         /// </summary>
//         public static Action<string> Log { get; private set; }
//
//         /// <summary>
//         /// 错误
//         /// </summary>
//         public static Action<string> Error { get; private set; }
//
//         /// <summary>
//         /// 异常
//         /// </summary>
//         public static Action<Exception> Exception { get; private set; }
//
//         /// <summary>
//         /// 警告
//         /// </summary>
//         public static Action<string> Warning { get; private set; }
//
//
//         /// <summary>
//         /// 是否打印网络详细日志
//         /// </summary>
//         protected static bool PrintNetworkDetailLog { get; private set; }
//
//         /// <summary>
//         /// 打开网络详细日志
//         /// </summary>
//         /// <param name="log"></param>
//         /// <param name="error"></param>
//         /// <param name="exception"></param>
//         /// <param name="warning"></param>
//         public static void OpenDetailLog(Action<string> log, Action<string> error, Action<Exception> exception, Action<string> warning)
//         {
//             Log = log;
//             Error = error;
//             Exception = exception;
//             Warning = warning;
//             PrintNetworkDetailLog = true;
//         }
//
//         /// <summary>
//         /// 白名单请求协议号
//         /// </summary>
//         public static Func<int, bool> FilterBlock;
//
//         /// <summary>
//         /// 日志时间戳
//         /// </summary>
//         protected static void LogTimeStamp(string title, int protocol, int length)
//         {
//             if (!PrintNetworkDetailLog) return;
//             if (FilterBlock == null) return;
//             if (FilterBlock.Invoke(protocol)) return;
//
//             Log?.Invoke($"【{title}，耗时】：{CurrentTimeStamp() - EncryptTime}ms，协议号：{protocol} ContentLength：{length}");
//             RefreshEncryptTime();
//         }
//
//         /// <summary>
//         /// 日志时间戳
//         /// </summary>
//         protected static void LogTimeStamp(string title, int protocol)
//         {
//             if (!PrintNetworkDetailLog) return;
//             if (FilterBlock == null) return;
//             if (FilterBlock.Invoke(protocol)) return;
//
//             Log?.Invoke($"【{title}，耗时】：{CurrentTimeStamp() - EncryptTime}ms，协议号：{protocol}");
//             RefreshEncryptTime();
//         }
//
//         /// <summary>
//         /// 获取当前时间戳
//         /// </summary>
//         /// <param name="isMinseconds">Ture:毫秒 False:秒</param>
//         /// <returns>时间磋</returns>
//         protected static long CurrentTimeStamp(bool isMinseconds = true)
//         {
//             var ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
//             var times = Convert.ToInt64(isMinseconds ? ts.TotalMilliseconds : ts.TotalSeconds);
//             return times;
//         }
//
//         /// <summary>
//         /// 刷新加密时间
//         /// </summary>
//         public static void RefreshEncryptTime()
//         {
//             EncryptTime = CurrentTimeStamp();
//         }
//     }
// }
using System;
using System.IO;
using System.Threading.Tasks;

public class Program
{
    public static DateTime TimestampStart = DateTime.UtcNow;
    public static DateTime TimestampStop = DateTime.UtcNow;
    public static long TotalErrors;
    public static long TotalBytes;
    public static long TotalMessages;

    static async void Test()
    {
        var progress = new AProgressEvent();
        progress.OnProgress += Console.WriteLine;
        progress.OnError += Console.WriteLine;
        progress.OnComplete += () => Console.WriteLine("Download done!");
        progress.OnBegin += () => Console.WriteLine("Download begin!");
        progress.OnPause += () => Console.WriteLine("Download pause!");
        progress.OnResume += () => Console.WriteLine("Download resume!");
        progress.OnCancel += () => Console.WriteLine("Download cancel!");

        const string serverIp = @"ftpshare-hot.ingcreations.com";
        const string user = "ftpshare-hot";
        const string pass = "ingcreations2023";
        // using var handle = AHandle.FTP.Create(serverIp, user, pass, "Bundles");
        // await handle.InitAsync();


        // await handle.UploadDirAsync(@"E:\Project\AIO\20190440f1\Bundles", progress);
        // await handle.UploadFileAsync(@"E:\WWW\G101\Version\StandaloneWindows64.json", "StandaloneWindows64.json",
        //     progress);
        // Console.WriteLine("Upload done!");
        //
        // Console.WriteLine();

        var handle = AHelper.Net.HTTP.Download(
            new[]
            {
                "https://ftpshare-hot.ingcreations.com/Bundles/android-ndk-r16b-windows-x86_64.zip",
                "https://ftpshare-hot.ingcreations.com/Bundles/Version/StandaloneWindows64.json"
            },
            @"E:\WWW\Test");

        handle.Event = progress;
        handle.Begin();
        Task.Factory.StartNew(async () =>
        {
            while (handle.State != ProgressState.Finish && handle.State != ProgressState.Fail)
            {
                await Task.Delay(1000);
                handle.Pause();
                await Task.Delay(1000);
                handle.Resume();
            }

            handle.Dispose();
        });

        await handle.WaitAsync();
    }

    public static void P(string name)
    {
        Console.WriteLine("_{0} {1}", name, $"_{name}".GetHashCode());
    }

    static void Main(string[] args)
    {
        Test();
        // bool help = false;
        // string address = "127.0.0.1";
        // int port = 8080;
        // int clients = 100;
        // int messages = 1;
        // int seconds = 10;
        //
        // var options = new OptionSet()
        // {
        //     { "h|?|help", v => help = v != null },
        //     { "a|address=", v => address = v },
        //     { "p|port=", v => port = int.Parse(v) },
        //     { "c|clients=", v => clients = int.Parse(v) },
        //     { "m|messages=", v => messages = int.Parse(v) },
        //     { "z|seconds=", v => seconds = int.Parse(v) }
        // };
        //
        // try
        // {
        //     options.Parse(args);
        // }
        // catch (OptionException e)
        // {
        //     Console.Write("Command line error: ");
        //     Console.WriteLine(e.Message);
        //     Console.WriteLine("Try `--help' to get usage information.");
        //     return;
        // }
        //
        // if (help)
        // {
        //     Console.WriteLine("Usage:");
        //     options.WriteOptionDescriptions(Console.Out);
        //     return;
        // }
        //
        // Console.WriteLine($"Server address: {address}");
        // Console.WriteLine($"Server port: {port}");
        // Console.WriteLine($"Working clients: {clients}");
        // Console.WriteLine($"Working messages: {messages}");
        // Console.WriteLine($"Seconds to benchmarking: {seconds}");
        //
        // Console.WriteLine();
        //
        // // Create HTTP clients
        // var httpClients = new List<HttpTraceClient>();
        // for (int i = 0; i < clients; i++)
        // {
        //     var client = new HttpTraceClient(address, port, messages);
        //     // client.OptionNoDelay = true;
        //     httpClients.Add(client);
        // }
        //
        // TimestampStart = DateTime.UtcNow;
        //
        // // Connect clients
        // Console.Write("Clients connecting...");
        // foreach (var client in httpClients)
        //     client.ConnectAsync();
        // Console.WriteLine("Done!");
        // foreach (var client in httpClients)
        //     while (!client.IsConnected)
        //         Thread.Yield();
        // Console.WriteLine("All clients connected!");
        //
        // // Wait for benchmarking
        // Console.Write("Benchmarking...");
        // Thread.Sleep(seconds * 1000);
        // Console.WriteLine("Done!");
        //
        // // Disconnect clients
        // Console.Write("Clients disconnecting...");
        // foreach (var client in httpClients)
        //     client.DisconnectAsync();
        // Console.WriteLine("Done!");
        // foreach (var client in httpClients)
        //     while (client.IsConnected)
        //         Thread.Yield();
        // Console.WriteLine("All clients disconnected!");
        //
        // Console.WriteLine();
        //
        // Console.WriteLine($"Errors: {TotalErrors}");
        //
        // Console.WriteLine();
        // Console.WriteLine($"Total time: {(TimestampStop - TimestampStart).TotalMilliseconds.ToConverseTimePeriod()}");
        // Console.WriteLine($"Total data: {TotalBytes.ToConverseStringFileSize()}");
        // Console.WriteLine($"Total messages: {TotalMessages}");
        // Console.WriteLine(
        //     $"Data throughput: {((long)(TotalBytes / (TimestampStop - TimestampStart).TotalSeconds)).ToConverseStringFileSize()}/s");
        // if (TotalMessages > 0)
        // {
        //     Console.WriteLine(
        //         $"Message latency: {((TimestampStop - TimestampStart).TotalMilliseconds / TotalMessages).ToConverseTimePeriod()}");
        //     Console.WriteLine(
        //         $"Message throughput: {(long)(TotalMessages / (TimestampStop - TimestampStart).TotalSeconds)} msg/s");
        // }

        // var remote =
        //     "https://oapi.dingtalk.com/robot/send?access_token=ef2a15e5f980819007e3933b6ce0d701dfc772cfff6b8f40918a1d14294e6084";
        // var data = "{\"msgtype\":\"text\",\"text\":{\"content\":\"text\"}}";
        // var msg = AHelper.Net.HTTP.Post(remote, data);


        Console.Read();
    }
}
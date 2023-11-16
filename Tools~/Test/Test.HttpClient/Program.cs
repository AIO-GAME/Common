using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Threading;
using NDesk.Options;
using AIO;

public class Program
{
    public static DateTime TimestampStart = DateTime.UtcNow;
    public static DateTime TimestampStop = DateTime.UtcNow;
    public static long TotalErrors;
    public static long TotalBytes;
    public static long TotalMessages;

    static void Main(string[] args)
    {
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
        
        var remote = @"http://127.0.0.1/G101/Version/StandaloneWindows64.json";
        var msg = AHelper.Net.HTTP.Get(remote);
        Console.WriteLine(string.IsNullOrEmpty(msg));
        Console.WriteLine(msg);
        Console.Read();
    }
}
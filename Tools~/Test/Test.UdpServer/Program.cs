using System;
using System.Net;
using AIO;
using AIO.Net;

public partial class Program
{
    public static void Main(string[] args)
    {
        // UDP server port
        int port = 3333;
        if (args.Length > 0)
            port = int.Parse(args[0]);

        Console.WriteLine($"UDP server port: {port}");

        Console.WriteLine();

        // Create a new UDP echo server
        var server = new EchoServer(IPAddress.Any, port);

        // Start the server
        Console.Write("Server starting...");
        server.Start();
        Console.WriteLine("Done!");

        Console.WriteLine("Press Enter to stop the server or '!' to restart the server...");

        // Perform text input
        for (;;)
        {
            var line = Console.ReadLine();
            if (string.IsNullOrEmpty(line))
                break;

            // Restart the server
            if (line == "!")
            {
                Console.Write("Server restarting...");
                server.Restart();
                Console.WriteLine("Done!");
            }
            else server.MulticastAsync(line);
        }

        // Stop the server
        Console.Write("Server stopping...");
        server.Stop();
        Console.WriteLine("Done!");
    }
}
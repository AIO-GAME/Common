using System;
using System.Net;
using NDesk.Options;

public partial class Program
{
    static void Main(string[] args)
    {
        bool help = false;
        int port = 8080;

        var options = new OptionSet()
        {
            { "h|?|help", v => help = v != null },
            { "p|port=", v => port = int.Parse(v) }
        };

        try
        {
            options.Parse(args);
        }
        catch (OptionException e)
        {
            Console.Write("Command line error: ");
            Console.WriteLine(e.Message);
            Console.WriteLine("Try `--help' to get usage information.");
            return;
        }

        if (help)
        {
            Console.WriteLine("Usage:");
            options.WriteOptionDescriptions(Console.Out);
            return;
        }

        Console.WriteLine($"Server port: {port}");

        Console.WriteLine();

        // Create a new HTTP server
        var server = new HttpTraceServer(IPAddress.Any, port);
        // server.OptionNoDelay = true;
        server.Option.ReuseAddress = true;

        // Start the server
        Console.Write("Server starting...");
        server.Start();
        Console.WriteLine("Done!");

        Console.WriteLine("Press Enter to stop the server or '!' to restart the server...");

        // Perform text input
        for (;;)
        {
            string line = Console.ReadLine();
            if (string.IsNullOrEmpty(line))
                break;

            // Restart the server
            if (line == "!")
            {
                Console.Write("Server restarting...");
                server.Restart();
                Console.WriteLine("Done!");
            }
        }

        // Stop the server
        Console.Write("Server stopping...");
        server.Stop();
        Console.WriteLine("Done!");
    }
}
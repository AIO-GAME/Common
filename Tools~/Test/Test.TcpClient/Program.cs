using System;

public partial class Program
{
    private static void Main(string[] args)
    {
        // TCP server address
        var address = "127.0.0.1";
        if (args.Length > 0)
            address = args[0];

        // TCP server port
        int port = 1111;
        if (args.Length > 1)
            port = int.Parse(args[1]);

        Console.WriteLine($"TCP server address: {address}");
        Console.WriteLine($"TCP server port: {port}");

        Console.WriteLine();

        // Create a new TCP chat client
        var client = new ChatClient(address, port);

        // Connect the client
        Console.Write("Client connecting...");
        client.ConnectAsync();
        Console.WriteLine("Done!");

        Console.WriteLine("Press Enter to stop the client or '!' to reconnect the client...");

        // Perform text input
        for (;;)
        {
            var line = Console.ReadLine();
            if (string.IsNullOrEmpty(line))
                break;

            // Disconnect the client
            if (line == "!")
            {
                Console.Write("Client disconnecting...");
                client.DisconnectAsync();
                Console.WriteLine("Done!");
                continue;
            }

            // Send the entered text to the chat server
            client.SendAsync(line);
        }

        // Disconnect the client
        Console.Write("Client disconnecting...");
        client.DisconnectAndStop();
        Console.WriteLine("Done!");
    }
}
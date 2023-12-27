using System;
using System.Collections.Generic;
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
        progress.OnComplete += Console.WriteLine;
        progress.OnBegin += () => Console.WriteLine("Download begin!");
        progress.OnPause += () => Console.WriteLine("Download pause!");
        progress.OnResume += () => Console.WriteLine("Download resume!");
        progress.OnCancel += () => Console.WriteLine("Download cancel!");
    }

    public static void P(string name)
    {
        Console.WriteLine("_{0} {1}", name, $"_{name}".GetHashCode());
    }

    static async void AAA()
    {
    }

    static void Main(string[] args)
    {
        Test();


        Console.Read();
    }
}
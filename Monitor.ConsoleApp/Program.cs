using System;
using System.Threading;

namespace Monitor.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var apiHost = new NancyApiHost();
            apiHost.Start();
            Console.Out.WriteLine("App started.");
            while (true)
            {
                Thread.Sleep(100000);
            }
        }
    }
}

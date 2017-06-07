using System;
using System.Threading;
using Monitor.Config;

namespace Monitor.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var apiHost = new NancyApiHost();
            string configPath = args.Length > 1 ? args[1] : ConfigurationLoader.DefaultConfigFilePath;
            apiHost.Start(configPath);
            Console.Out.WriteLine("App started.");
            while (true)
            {
                Thread.Sleep(100000);
            }
        }
    }
}

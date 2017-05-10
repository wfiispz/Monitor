using System;

namespace Monitor.Logging
{
    internal class Logger : ILogger
    {
        public void LogDebug(string message)
        {
            Console.Out.WriteLine($"{DateTime.Now} [DEBUG] {message}");
        }

        public void LogInfo(string message)
        {
            Console.Out.WriteLine($"{DateTime.Now} [INFO]  {message}");
        }

        public void LogError(string message, Exception exception)
        {
            Console.Out.WriteLine($"{DateTime.Now} [ERROR] {message}{Environment.NewLine}{exception}");
        }
    }
}

using System;

namespace Monitor.Logging
{
    internal interface ILogger
    {
        void LogDebug(string message);
        void LogInfo(string message);
        void LogError(string message, Exception exception);
    }
}
using System;

namespace Banking.Api.Logging
{
    public interface ILogger
    {
        void Log(string message, LogLevel level);
        void Log(Exception exception);
        void Log(Exception exception, string message);
    }
}
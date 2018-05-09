using System;
using log4net;
using Seterlund.CodeGuard;

namespace Banking.Api.Logging
{
    public class Log4NetLogger : ILogger
    {
        public Log4NetLogger(ILog logger)
        {
            Guard.That(() => logger).IsNotNull();

            _log4Net = logger;

            _isDebugEnabled = _log4Net.IsDebugEnabled;
            _isInfoEnabled = _log4Net.IsInfoEnabled;
            _isWarnEnabled = _log4Net.IsWarnEnabled;
            _isErrorEnabled = _log4Net.IsErrorEnabled;
        }

        private readonly bool _isDebugEnabled;
        private readonly bool _isErrorEnabled;
        private readonly bool _isInfoEnabled;
        private readonly bool _isWarnEnabled;
        private readonly ILog _log4Net;

        public void Log(string message, LogLevel level)
        {
            Log(level, message);
        }

        public void Log(Exception exception)
        {
            Log(LogLevel.Exception, exception.Message, exception);
        }

        public void Log(Exception exception, string message)
        {
            Log(LogLevel.Exception, message, exception);
        }

        private void Log(LogLevel level, string message, Exception exception)
        {
            try
            {
                switch (level)
                {
                    case LogLevel.Info:
                        if (_isInfoEnabled)
                        {
                            _log4Net.Info(message, exception);
                        }
                        break;
                    case LogLevel.Warn:
                        if (_isWarnEnabled)
                        {
                            _log4Net.Warn(message, exception);
                        }
                        break;
                    case LogLevel.Exception:
                        if (_isErrorEnabled)
                        {
                            _log4Net.Error(message, exception);
                        }
                        break;
                    case LogLevel.Debug:
                        if (_isDebugEnabled)
                        {
                            _log4Net.Debug(message, exception);
                        }
                        break;
                }
            }
            catch
            {
            }
        }

        private void Log(LogLevel level, string message)
        {
            try
            {
                switch (level)
                {
                    case LogLevel.Info:
                        if (_isInfoEnabled)
                        {
                            _log4Net.Info(message);
                        }
                        break;
                    case LogLevel.Warn:
                        if (_isWarnEnabled)
                        {
                            _log4Net.Warn(message);
                        }
                        break;
                    case LogLevel.Exception:
                        if (_isErrorEnabled)
                        {
                            _log4Net.Error(message);
                        }
                        break;
                    case LogLevel.Debug:
                        if (_isDebugEnabled)
                        {
                            _log4Net.Debug(message);
                        }
                        break;
                }
            }
            catch
            {
            }
        }
    }
}
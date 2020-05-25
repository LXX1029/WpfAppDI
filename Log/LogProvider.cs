using System;
using System.IO;
using log4net.Config;
using Microsoft.Extensions.Logging;

namespace WpfAppDI.Log
{
    public class LogProvider : ILoggerProvider
    {
        static LogProvider()
        {
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log", "Log4netConfig.xml");
            XmlConfigurator.ConfigureAndWatch(new FileInfo(file));
        }


        public Microsoft.Extensions.Logging.ILogger CreateLogger(string categoryName)
        {
            var log = log4net.LogManager.GetLogger(categoryName);
            return new Log4netLogger(categoryName, log);
        }

        public void Dispose()
        {

        }
    }
    public class Log4netLogger : Microsoft.Extensions.Logging.ILogger
    {

        private string _category { get; set; }
        private log4net.ILog _log = null;
        public Log4netLogger(string category, log4net.ILog log)
        {
            this._category = category;
            this._log = log;
        }



        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            try
            {
                var str = formatter(state, exception);
                switch (logLevel)
                {
                    case LogLevel.Trace:
                    case LogLevel.Debug:
                        _log.Debug(str);
                        break;

                    case LogLevel.Information:
                        _log.Info(str);
                        break;
                    case LogLevel.Warning:
                        _log.Warn(str);
                        break;
                    case LogLevel.Error:
                        _log.Error(str);
                        break;
                    case LogLevel.Critical:
                        _log.Fatal(str);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }


}

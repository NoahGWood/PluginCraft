using Serilog;
using Serilog.Sinks;

namespace PluginCraft.Core
{
    public static class Logger
    {
        private static readonly ILogger CoreLog = new LoggerConfiguration()
            .MinimumLevel.Error()
            .WriteTo.File("logs/core.debug", rollingInterval: RollingInterval.Day)
            .CreateLogger();
        private static readonly ILogger AppDebugLog = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File("logs/" + App.Name + ".debug", rollingInterval: RollingInterval.Day)
            .CreateLogger();
        public static void Error(Exception ex, string message)
        {
            if(App.EnableLogs)
                AppDebugLog.Error(ex, message);
        }
        public static void Debug(string message)
        {
            if (App.EnableLogs)
                AppDebugLog.Debug(message);
        }
        public static void CoreExcept(Exception ex, string message)
        {
            if (App.EnableLogs)
                CoreLog.Error(ex, message);
        }
        public static void CoreDebug(string message)
        {
            if (App.EnableLogs)
            {
                CoreLog.Debug(message);
                Console.WriteLine(message);
            }
        }
    }
}

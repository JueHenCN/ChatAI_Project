using Serilog;
using System;
using System.Threading.Tasks;

namespace ChatAI_Connect.LogManage
{
    /// <summary>
    /// 日志处理类
    /// </summary>
    public class LogService : ILogService
    {
        private readonly ILogger _logger;

        /// <summary>
        /// 日志回调委托
        /// </summary>
        public event Action<LogInfo> LogInfoGenerated;

        public LogService()
        {
            _logger = new LoggerConfiguration() // 创建日志配置
                .WriteTo.Console() // 写入控制台
                .WriteTo.Async(a => a.File("logs\\log.log", // 写入文件
                    rollingInterval: RollingInterval.Day, // 按天滚动
                    fileSizeLimitBytes: 10_000_000, // 限制单个日志文件大小 10 MB
                    rollOnFileSizeLimit: true, // 滚动到新文件
                    retainedFileCountLimit: null, // 保留文件数
                    shared: true)) // 线程共享文件
                .CreateLogger();
        }

        public void Debug(string message, string source = "Debug")
        {
            _logger.Debug($"[Debug] {source} {message}");
            LogAndNotify(new LogInfo(LogLevel.Debug, source, message));
        }

        public void Info(string message, string source = "Info")
        {
            _logger.Information($"[Info] {source} {message}");
            LogAndNotify(new LogInfo(LogLevel.Info, source, message));
        }

        public void Error(string message, string source = "Error")
        {
            _logger.Error($"[Error] {source} {message}");
            LogAndNotify(new LogInfo(LogLevel.Error, source, message));
        }

        public void Error(Exception exception, string source = "Error")
        {
            _logger.Error(exception, $"[Error] {source} {exception.Message}");
            LogAndNotify(new LogInfo(LogLevel.Error, source, exception.Message));
        }

        public void Fatal(string message, string source = "Fatal")
        {
            _logger.Fatal($"[Fatal] {source} {message}");
            LogAndNotify(new LogInfo(LogLevel.Fatal, source, message));
        }

        public void Fatal(Exception exception, string source = "Fatal")
        {
            _logger.Fatal(exception, $"[Fatal] {source} {exception.Message}");
            LogAndNotify(new LogInfo(LogLevel.Fatal, source, exception.Message));
        }

        /// <summary>
        /// 触发日志委托事件
        /// </summary>
        /// <param name="logInfo">日志信息</param>
        private void LogAndNotify(LogInfo logInfo)
        {
            Task.Run(() => LogInfoGenerated?.Invoke(logInfo));
        }
    }

    public static class LogHandler
    {
        private static readonly ILogService _logService = new LogService();

        // 声明一个事件
        public static event Action<LogInfo> LogInfoGenerated
        {
            add => (_logService as LogService).LogInfoGenerated += value;
            remove => (_logService as LogService).LogInfoGenerated -= value;
        }

        /// <summary>
        /// 记录调试信息
        /// </summary>
        /// <param name="message">调试信息</param>
        /// <param name="source">信息来源，默认为"Debug"</param>
        public static void Debug(string message, string source = "Debug")
        {
            Task.Run(() => { _logService.Debug(message, source); });
        }

        /// <summary>
        /// 记录普通信息
        /// </summary>
        /// <param name="message">普通信息</param>
        /// <param name="source">信息来源，默认为"Info"</param>
        public static void Info(string message, string source = "Info")
        {
            Task.Run(() => { _logService.Info(message, source); });
        }

        /// <summary>
        /// 记录错误信息
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="source">信息来源，默认为"Error"</param>
        public static void Error(string message, string source = "Error")
        {
            Task.Run(() => { _logService.Error(message, source); });
        }

        /// <summary>
        /// 记录错误信息
        /// </summary>
        /// <param name="exception">错误信息</param>
        /// <param name="source">信息来源，默认为"Error"</param>
        public static void Error(Exception exception, string source = "Error")
        {
            Task.Run(() => { _logService.Error(exception, source); });
        }

        /// <summary>
        /// 记录警告信息
        /// </summary>
        /// <param name="message">警告信息</param>
        /// <param name="source">信息来源，默认为"Fatal"</param>
        public static void Fatal(string message, string source = "Fatal")
        {
            Task.Run(() => { _logService.Fatal(message, source); });
        }

        /// <summary>
        /// 记录警告信息
        /// </summary>
        /// <param name="exception">警告信息</param>
        /// <param name="source">信息来源，默认为"Fatal"</param>
        public static void Fatal(Exception exception, string source = "Fatal")
        {
            Task.Run(() => { _logService.Fatal(exception, source); });
        }
    }


}

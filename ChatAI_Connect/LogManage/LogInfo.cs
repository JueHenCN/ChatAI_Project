using System;

namespace ChatAI_Connect.LogManage
{

    /// <summary>
    /// 日志级别
    /// </summary>
    public enum LogLevel
    {
        Debug,
        Info,
        Error,
        Fatal
    }

    /// <summary>
    /// 日志信息
    /// </summary>
    public class LogInfo
    {
        /// <summary>
        /// 日志时间
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// 日志级别
        /// </summary>
        public LogLevel LogLevel { get; set; }

        /// <summary>
        /// 日志源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 日志消息
        /// </summary>
        public string Message { get; set; }

        public LogInfo(LogLevel logLevel, string source, string message)
        {
            Time = DateTime.Now;
            LogLevel = logLevel;
            Source = source;
            Message = message;
        }
    }
}
using System;

namespace ChatAI_Connect.LogManage
{

    /// <summary>
    /// 日志服务接口
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// 记录调试信息
        /// </summary>
        /// <param name="message">调试信息</param>
        /// <param name="source">信息来源，默认为"Debug"</param>
        void Debug(string message, string source = "Debug");

        /// <summary>
        /// 记录普通信息
        /// </summary>
        /// <param name="message">普通信息</param>
        /// <param name="source">信息来源，默认为"Info"</param>
        void Info(string message, string source = "Info");

        /// <summary>
        /// 记录错误信息
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="source">信息来源，默认为"Error"</param>
        void Error(string message, string source = "Error");

        /// <summary>
        /// 记录错误信息
        /// </summary>
        /// <param name="exception">错误信息</param>
        /// <param name="source">信息来源，默认为"Error"</param>
        void Error(Exception exception, string source = "Error");

        /// <summary>
        /// 记录警告信息
        /// </summary>
        /// <param name="message">警告信息</param>
        /// <param name="source">信息来源，默认为"Fatal"</param>
        void Fatal(string message, string source = "Fatal");

        /// <summary>
        /// 记录警告信息
        /// </summary>
        /// <param name="exception">警告信息</param>
        /// <param name="source">信息来源，默认为"Fatal"</param>
        void Fatal(Exception exception, string source = "Fatal");
    }


}

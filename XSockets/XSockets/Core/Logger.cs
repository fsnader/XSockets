using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSockets.Core
{
    public static class Logger
    {
        /// <summary>
        /// Saves the XSockets Log
        /// </summary>
        /// <param name="status"></param>
        /// <param name="message"></param>
        public static void Log(LogStatusEnum status, string message)
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();

            switch (status)
            {
                case LogStatusEnum.Info:
                    logger.Info(message);
                    break;
                case LogStatusEnum.Warning:
                    logger.Warn(message);
                    break;
                case LogStatusEnum.Error:
                    logger.Error(message);
                    break;
                case LogStatusEnum.Debug:
                    logger.Debug(message);
                    break;
            }

            //Console.WriteLine(String.Format("[{0}] {1}: {2}",
            //    DateTime.Now, status, message));
        }

        /// <summary>
        /// Saves the XSockets Log
        /// </summary>
        /// <param name="status"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Log(LogStatusEnum status, string message, Exception exception)
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();

            switch (status)
            {
                case LogStatusEnum.Info:
                    logger.Info(exception, message);
                    break;
                case LogStatusEnum.Warning:
                    logger.Warn(exception, message);
                    break;
                case LogStatusEnum.Error:
                    logger.Error(exception, message);
                    break;
                case LogStatusEnum.Debug:
                    logger.Debug(exception, message);
                    break;
            }

            //Console.WriteLine(String.Format("[{0}] {1}: {2}",
            //    DateTime.Now, status, message));
        }
    }
    /// <summary>
    /// Enum to indicate the log status
    /// </summary>
    public enum LogStatusEnum
    {
        Debug,
        Info,
        Warning,
        Error
    }
}

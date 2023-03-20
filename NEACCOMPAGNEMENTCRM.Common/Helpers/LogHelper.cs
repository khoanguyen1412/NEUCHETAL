//-----------------------------------------------------------------------
// <copyright file="LogHelper.cs" company="ELCA">
//     Copyright (c) ELCA Informatique SA Microsoft Corporation. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NEACCOMPAGNEMENTCRM.Common
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Security;
    using System.Web.Services.Protocols;
    using log4net;
    using log4net.Appender;
    using log4net.Config;
    using log4net.Layout;
    using log4net.Repository.Hierarchy;
    using Microsoft.Win32;

    /// <summary>
    /// Class used for logging
    /// </summary>
    public sealed class LogHelper
    {
        #region Members

        /// <summary>
        /// The logger used for logging messages
        /// </summary>
        private static ILog s_defaultLog = LogManager.GetLogger(typeof(LogHelper));

        /// <summary>
        /// The information log
        /// </summary>
        private static ILog s_informationLog = LogManager.GetLogger(LogType.Information);

        /// <summary>
        /// The alert log
        /// </summary>
        private static ILog s_alertLog = LogManager.GetLogger(LogType.Alert);

        /// <summary>
        /// The functional log
        /// </summary>
        private static ILog s_functionalLog = LogManager.GetLogger(LogType.Functional);

        /// <summary>
        /// The technical log
        /// </summary>
        private static ILog s_technicalLog = LogManager.GetLogger(LogType.Technical);

        /// <summary>
        /// The is technical error
        /// </summary>
        private static bool s_isTechnicalError = false;

        /// <summary>
        /// The is functional error
        /// </summary>
        private static bool s_isFunctionalError = false;

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether this instance is technical error.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is technical error; otherwise, <c>false</c>.
        /// </value>
        public static bool IsTechnicalError
        { get { return s_isTechnicalError; } }

        /// <summary>
        /// Gets a value indicating whether this instance is functional error.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is functional error; otherwise, <c>false</c>.
        /// </value>
        public static bool IsFunctionalError
        { get { return s_isFunctionalError; } }

        #endregion

        #region Constructors

        /// <summary>
        /// Prevents a default instance of the LogHelper class from being created.
        /// </summary>
        private LogHelper()
        {
        }

        #endregion

        #region Init LogHelper

        /// <summary>
        /// Configures the logger.
        /// </summary>
        /// <param name="dictFileName">Name of the dictionary file.</param>
        /// <param name="logFilePath">The log file path.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        public static void InitLogHelper(Dictionary<string, string> dictFileName, string logFilePath, bool isActive)
        {
            try
            {
                if (isActive)
                {
                    string log4NetFilePath = Registry.GetValue(
                        Constants.Log4NetRegistryKeyName,
                        Constants.Log4NetRegistryValueName,
                        Constants.Log4NetConfigDefaultFilePath) as string;

                    if (!string.IsNullOrEmpty(log4NetFilePath))
                    {
                        FileInfo log4NetConfigFileInfo = new FileInfo(log4NetFilePath);
                        if (log4NetConfigFileInfo.Exists)
                        {
                            XmlConfigurator.Configure(log4NetConfigFileInfo);
                        }

                        return;
                    }

                    CreateLog(dictFileName, logFilePath);
                    BasicConfigurator.Configure();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Configures the default logger.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="logFilePath">The log file path.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        public static void InitLogHelper(string fileName, string logFilePath, bool isActive)
        {
            try
            {
                if (isActive)
                {
                    string logPath = string.Format("{0}{1}.txt", logFilePath, fileName);

                    //string log4NetFilePath = Registry.GetValue(
                    //Constants.Log4NetRegistryKeyName,
                    //Constants.Log4NetRegistryValueName,
                    //Constants.Log4NetConfigDefaultFilePath) as string;

                    //if (!string.IsNullOrEmpty(log4NetFilePath))
                    //{
                    //    FileInfo log4NetConfigFileInfo = new FileInfo(log4NetFilePath);
                    //    if (log4NetConfigFileInfo.Exists)
                    //    {
                    //        XmlConfigurator.Configure(log4NetConfigFileInfo);
                    //    }

                    //    return;
                    //}

                    AddAppenderToLog(s_defaultLog, CreateAppender(logPath, Constants.ConfigParameter_DefaultLogName));
                    BasicConfigurator.Configure();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Logs the information.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void LogInfo(object message)
        {
            s_informationLog.Info(message);
        }

        /// <summary>
        /// Logs the alert.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void LogAlert(object message)
        {
            s_isFunctionalError = true;
            s_alertLog.Error(message);
        }

        /// <summary>
        /// Logs the functional.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void LogFunctional(object message)
        {
            s_isFunctionalError = true;
            s_functionalLog.Error(message);
        }

        /// <summary>
        /// Logs the technical.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void LogTechnical(object message)
        {
            s_isTechnicalError = true;
            s_technicalLog.Error(message);
        }

        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public static void LogException(object message, Exception exception)
        {
            s_defaultLog.Error(message, exception);
        }

        /// <summary>
        /// Logs the information.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void LogInformation(object message)
        {
            s_defaultLog.Info(message);
        }

        /// <summary>
        /// Logs formatted message.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        public static void LogInfoFormat(string format, params object[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == null)
                {
                    args[i] = string.Empty;
                }
            }

            s_defaultLog.InfoFormat(format, args);
        }

        /// <summary>
        /// Logs the warning.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void LogWarning(object message)
        {
            s_defaultLog.Warn(message);
        }

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void LogError(object message)
        {
            s_defaultLog.Error(message);
        }

        /// <summary>
        /// Logs the error with exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public static void LogErrorWithException(object message, Exception exception)
        {
            s_defaultLog.Error(message, exception);
        }

        /// <summary>
        /// Builds the SOAP exception message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <returns>The built Soap Exception message</returns>
        public static string BuildSoapExceptionMessage(string message, SoapException exception)
        {
            string builtMessage = string.Empty;
            if (message != null && exception != null)
            {
                builtMessage = string.Format(Constants.SoapExceptionFormat, message, exception.Detail.InnerText);
            }

            return builtMessage;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Create Log
        /// </summary>
        /// <param name="dicFileName">The list name's log file.</param>
        /// <param name="logFilePath">The log file path.</param>
        private static void CreateLog(Dictionary<string, string> dicFileName, string logFilePath)
        {
            foreach (var item in dicFileName)
            {
                string logPath = string.Format("{0}{1}_{2}.txt", logFilePath, item.Value, DateTime.Now.ToString("yyMMdd"));

                switch (item.Key)
                {
                    case LogType.Information:
                        AddAppenderToLog(s_informationLog, CreateAppender(logPath, item.Key));
                        break;
                    case LogType.Alert:
                        AddAppenderToLog(s_alertLog, CreateAppender(logPath, item.Key));
                        break;
                    case LogType.Functional:
                        AddAppenderToLog(s_functionalLog, CreateAppender(logPath, item.Key));
                        break;
                    case LogType.Technical:
                        AddAppenderToLog(s_technicalLog, CreateAppender(logPath, item.Key));
                        break;
                }
            }
        }

        /// <summary>
        /// Create Appender
        /// </summary>
        /// <param name="logPath">The log file path.</param>
        /// <param name="name">The name's log.</param>
        /// <returns>Appender</returns>
        private static IAppender CreateAppender(string logPath, string name)
        {
            FileAppender appender = new FileAppender();
            appender.Name = name;
            appender.File = logPath;
            appender.AppendToFile = true;
            appender.Layout = new PatternLayout(Constants.DefaultLogPatternLayout);
            appender.ActivateOptions();
            return appender;
        }

        /// <summary>
        /// Add Appender to Log
        /// </summary>
        /// <param name="log">The log.</param>
        /// <param name="appender">The appender.</param>
        private static void AddAppenderToLog(ILog log, IAppender appender)
        {
            Logger logger = (Logger)log.Logger;
            logger.AddAppender(appender);
        }

        #endregion
    }

    /// <summary>
    /// The class defines Log type.
    /// </summary>
    public class LogType
    {
        /// <summary>
        /// The information
        /// </summary>
        public const string Information = "Information";

        /// <summary>
        /// The alert
        /// </summary>
        public const string Alert = "Alert";

        /// <summary>
        /// The functional
        /// </summary>
        public const string Functional = "Functional";

        /// <summary>
        /// The technical
        /// </summary>
        public const string Technical = "Technical";
    }
}
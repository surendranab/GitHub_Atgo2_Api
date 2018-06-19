using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Atgo2.Api.CrossCuttingLayer.Logging.Interfaces;
using Atgo2.Api.CrossCuttingLayer.Logging.Model;
using Atgo2.Api.Entity;
using NLog;

namespace Atgo2.Api.CrossCuttingLayer.Logging
{
    public sealed class ServiceLogger : IServiceLogger
    {
        private readonly AppSettings _appsettings;
        private readonly Logger _logger;
        public ServiceLogger(AppSettings appsettings)
        {
            _appsettings = appsettings;
            _logger = LogManager.GetCurrentClassLogger();
        }
        
        public void Error(Exception exception, [Localizable(false)] string message, params object[] args)
        {
            _logger.Error(exception, message, args);
        }

        public void Error([Localizable(false)] string message)
        {
            _logger.Error(message);
        }

        public void Error([Localizable(false)] string message, [Localizable(false)] string argument)
        {
            _logger.Error(message, argument);
        }
        
        public void Info([Localizable(false)] string message)
        {
            _logger.Info(message);
        }
        /// <summary>
        /// Log the error or info for the method executions 
        /// </summary>
        /// <param name="logInformation"></param>
        /// <param name="memberName"></param>
        /// <param name="sourceFilePath"></param>
        /// <param name="sourceLineNumber"></param>
        /// <returns></returns>
        public void Log(LogInformation logInformation, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (_appsettings.settings.IsLoggingEnabled)
            {
                SetNLogContext(logInformation, memberName, sourceFilePath, sourceLineNumber);
                if (logInformation.Exception != null)
                    _logger.Error(logInformation.Exception, logInformation.Message);
                else
                    _logger.Info(logInformation.Message);
                //Now clear the context
                GlobalDiagnosticsContext.Clear();
            }
        }

        private void SetNLogContext(LogInformation logInformation, string memberName, string sourceFilePath,
            int sourceLineNumber)
        {
            #region Set the values in the NLog context object

            GlobalDiagnosticsContext.Set("userId", logInformation.UserId);
            GlobalDiagnosticsContext.Set("module", logInformation.Module);
            GlobalDiagnosticsContext.Set("logData", logInformation.Data);
            GlobalDiagnosticsContext.Set("method", memberName);
            GlobalDiagnosticsContext.Set("fileName", sourceFilePath);
            GlobalDiagnosticsContext.Set("lineNumber", sourceLineNumber.ToString());

            #endregion
        }
    }
}

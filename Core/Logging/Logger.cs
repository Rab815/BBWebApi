using System;
using System.Linq;
using System.Reflection;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Repository.Hierarchy;

namespace Core.Logging
{
    public class Logger : ILog 
    {
        public enum EntryType
        {
            Info,
            Warning,
            Error
        }

        #region Private fields

        private readonly ILog _log;

        #endregion

        #region public fields
        public bool ErrorsOccured { get; private set; }
        public string LogFileName { get; private set; }

        public void CloseLog()
        {
            var rootAppender = ((Hierarchy)LogManager.GetRepository()).Root.Appenders.OfType<RollingFileAppender>().FirstOrDefault();
            var fileAppender = rootAppender as FileAppender;
            if (fileAppender != null) fileAppender.Close();
        }

        #endregion@

        #region constructors
        public Logger()
        {
            // Load the configuration from the config file
            XmlConfigurator.Configure();

            // set the log file name when constructed, when appender is closed in CloseLog method the File Property of 
            // the appender is cleared
            var rootAppender = ((Hierarchy)LogManager.GetRepository()).Root.Appenders.OfType<RollingFileAppender>().FirstOrDefault();
            this.LogFileName = rootAppender != null ? rootAppender.File : string.Empty;
            _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }

        public Logger(string name) 
        {
            _log = LogManager.GetLogger(name);
        }

        public Logger(Type type)
        {
            _log = LogManager.GetLogger(type);
        }
        #endregion

        #region Implementation of ILoggerWrapper

        ILogger ILoggerWrapper.Logger
        {
            get { return _log.Logger; }
        }

        #endregion

        #region Implementation of ILog

        #region Debug

        public void Debug(object message)
        {
            _log.Debug(message);
        }

        public void Debug(object message, Exception exception)
        {
            _log.Debug(message, exception);
        }

        public void DebugFormat(string format, object arg0)
        {
            DebugFormat(format, new[] { arg0 });
        }

        public void DebugFormat(string format, params object[] args)
        {
            var message = string.Format(format, args);
            Debug(message);
        }

        public void DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            var message = string.Format(provider, format, args);
            Debug(message);
        }

        #endregion

        #region Info

        public void Info(object message)
        {
            _log.Info(message);
        }

        public void Info(object message, Exception exception)
        {
            _log.Info(message, exception);
        }

        public void InfoFormat(string format, object arg0)
        {
            InfoFormat(format, new[] { arg0 });
        }

        public void InfoFormat(string format, params object[] args)
        {
            var message = string.Format(format, args);
            Info(message);
        }

        public void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            var message = string.Format(provider, format, args);
            Info(message);
        }

        #endregion

        #region Warn

        public void Warn(object message)
        {
            _log.Warn(message);
        }

        public void Warn(object message, Exception exception)
        {
            _log.Warn(message, exception);
        }

        public void WarnFormat(string format, object arg0)
        {
            WarnFormat(format, new[] { arg0 });
        }

        public void WarnFormat(string format, params object[] args)
        {
            var message = string.Format(format, args);
            Warn(message);
        }

        public void WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            var message = string.Format(provider, format, args);
            Warn(message);
        }

        #endregion

        #region Error

        public void Error(object message)
        {
            ErrorsOccured = true;
            _log.Error(message);
        }

        public void Error(object message, Exception exception)
        {
            ErrorsOccured = true;
            _log.Error(message, exception);
        }

        public void ErrorFormat(string format, object arg0)
        {
            ErrorsOccured = true;
            ErrorFormat(format, new[] { arg0 });
        }

        public void ErrorFormat(string format, params object[] args)
        {
            ErrorsOccured = true;
            var message = string.Format(format, args);
            Error(message);
        }

        public void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            ErrorsOccured = true;
            var message = string.Format(provider, format, args);
            Error(message);
        }

        #endregion

        #region Fatal

        public void Fatal(object message)
        {
            _log.Fatal(message);
        }

        public void Fatal(object message, Exception exception)
        {
            _log.Fatal(message, exception);
        }

        public void FatalFormat(string format, object arg0)
        {
            FatalFormat(format, new[] { arg0 });
        }

        public void FatalFormat(string format, params object[] args)
        {
            var message = string.Format(format, args);
            Fatal(message);
        }

        public void FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            var message = string.Format(provider, format, args);
            Fatal(message);
        }

        #endregion

        #endregion

        #region Properties
        public bool IsDebugEnabled
        {
            get { return _log.IsDebugEnabled; }
        }

        public bool IsInfoEnabled
        {
            get { return _log.IsInfoEnabled; }
        }

        public bool IsWarnEnabled
        {
            get { return _log.IsWarnEnabled; }
        }

        public bool IsErrorEnabled
        {
            get { return _log.IsErrorEnabled; }
        }

        public bool IsFatalEnabled
        {
            get { return _log.IsFatalEnabled; }
        }
        #endregion

        #region Not Implemented Methods
        public void DebugFormat(string format, object arg0, object arg1, object arg2)
        {
            throw new NotImplementedException();
        }

        public void DebugFormat(string format, object arg0, object arg1)
        {
            throw new NotImplementedException();
        }

        public void ErrorFormat(string format, object arg0, object arg1, object arg2)
        {
            throw new NotImplementedException();
        }

        public void ErrorFormat(string format, object arg0, object arg1)
        {
            throw new NotImplementedException();
        }

        public void FatalFormat(string format, object arg0, object arg1, object arg2)
        {
            throw new NotImplementedException();
        }

        public void FatalFormat(string format, object arg0, object arg1)
        {
            throw new NotImplementedException();
        }

        public void InfoFormat(string format, object arg0, object arg1, object arg2)
        {
            throw new NotImplementedException();
        }

        public void InfoFormat(string format, object arg0, object arg1)
        {
            throw new NotImplementedException();
        }

        public void WarnFormat(string format, object arg0, object arg1, object arg2)
        {
            throw new NotImplementedException();
        }

        public void WarnFormat(string format, object arg0, object arg1)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

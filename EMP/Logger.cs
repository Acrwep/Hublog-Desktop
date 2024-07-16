using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.IO;

namespace EMP
{
  public  class Logger
    {
      private static ILog log = null;

      static Logger()
      {
          /**
           * Read Configuration from app.config
           */
          log4net.Config.XmlConfigurator.Configure();
          log = LogManager.GetLogger("EMP");
      }

      /**
       * Log Debug Messages
       */
      public static void LogDebug(string msg)
      {
          log.Debug(msg);
      }

      /**
       * Log Info Messages
       */
      public static void LogInfo(string msg)
      {
          log.Info(msg);
      }

      /**
       * Log Warnings
       */
      public static void LogWarning(string msg)
      {
          log.Warn(msg);
      }

      /**
       * Log Error Messages
       */
      public static void LogError(string msg)
      {
          log.Error(msg);
      }

      /**
       * Log Exception Messages
       */
      public static void LogError(Exception ex)
      {
          log.Error(ex.Message, ex);
      }

      /**
       * Log Exception Messages
       */
      public static void LogError(string msg, Exception ex)
      {
          log.Error(msg, ex);
      }

      /**
       * Returns the error log file name
       */
      public static string GetLogFilePath(string logName, string typeName)
      {
          string fileName = null;
          log4net.Repository.ILoggerRepository RootRep;
          RootRep = log4net.LogManager.GetRepository();
          foreach (log4net.Appender.IAppender iApp in RootRep.GetAppenders())
          {
              if (iApp is log4net.Appender.FileAppender)
              {
                  log4net.Appender.FileAppender fApp = (log4net.Appender.FileAppender)iApp;
                  fileName = fApp.File.ToString();
                  if (iApp.Name.Contains(logName) && iApp.Name.Contains(typeName))
                      break;
              }
          }
          return fileName;
      }


    }
}


using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Libary.Logger
{
    public class Logger
    {
        private static readonly Lazy<ILog> LazyConnection;

        static Logger()
        {
            if (LazyConnection == null || !LazyConnection.IsValueCreated)
            {
                LazyConnection = new Lazy<ILog>(() => LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType));
            }
        }

        public static ILog LogClient => LazyConnection.Value;

        public static void InfoTime(string info)
        {
            string message = string.Format("TimeUTC: {0}, Class: '{1}', Method: '{2}', Message: {3}",
               DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss.fff"),
               GetClassAndMethodName().ClassName,
               GetClassAndMethodName().MethodName,
               info);

            LogClient.Info(message);
        }

        public static void InfoTimeAsync(string info)
        {
            string message = string.Format("TimeUTC: {0}, Class: '{1}', Method: '{2}', Message: {3}",
               DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss.fff"),
               GetClassAndMethodName().ClassName,
               GetClassAndMethodName().MethodName,
               info);

            Task.Factory.StartNew(() =>
            {
                LogClient.Info(message);
            });
        }

        public static void Info(string info)
        {
            LogClient.Info(info);
        }

        public static void InfoAsync(string info)
        {
            Task.Factory.StartNew(() =>
            {
                LogClient.Info(info);
            });
        }

        public static void Error(string error)
        {
            string errMessage = string.Format("TimeUTC: {0}, Class: '{1}', Method: '{2}', Message: {3}",
                DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss.fff"),
                GetClassAndMethodName().ClassName,
                GetClassAndMethodName().MethodName,
                error);

            Task.Factory.StartNew(() =>
            {
                LogClient.Error(errMessage);
            });
        }

        public static void ErrorException(Exception exception)
        {
            string errMessage = string.Format("TimeUTC: {0}, Class: '{1}', Method: '{2}', Message: {3}, StackTrace: {4}",
                DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss.fff"),
                GetClassAndMethodName().ClassName,
                GetClassAndMethodName().MethodName,
                exception.Message.ToString(),
                exception.StackTrace.ToString());

            Task.Factory.StartNew(() =>
            {
                LogClient.Error(errMessage);
            });
        }

        /// <summary>
        /// Gets the current class and method name.
        /// Equivalent of C/C++ macro __FUNCTION__
        /// </summary>
        /// <remarks>
        /// See:<br/>
        /// <list type="bullet">
        /// <item><a href="http://stackoverflow.com/questions/259536/c-version-of-function-macro">C# version of __FUNCTION__ macro</a></item>
        /// <item><a href="http://discuss.fogcreek.com/dotnetquestions/default.asp?cmd=show&amp;ixPost=6163">printing the calling method name : C#</a></item>
        /// </list>
        /// <br/>
        /// This method is public because of the unit tests.
        /// </remarks>
        /// <returns>The name of the last - 2 function called.</returns>
        public static ClassMethodName GetClassAndMethodName()
        {
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();
            string className = stackTrace.GetFrame(2).GetMethod().ReflectedType.FullName;
            string methodName = stackTrace.GetFrame(2).GetMethod().Name;
            methodName = methodName.Replace(".ctor", "ctor");

            return new ClassMethodName
            {
                ClassName = className,
                MethodName = methodName
            };
        }
    }

    public class ClassMethodName
    {
        public string ClassName { get; set; }
        public string MethodName { get; set; }
    }
}

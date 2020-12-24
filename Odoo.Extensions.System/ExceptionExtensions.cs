using System;
using System.Linq;
using System.Reflection;

namespace System
{
    /// <summary>
    /// 异常扩展工具
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// 获取异常详细，针对<see cref="ReflectionTypeLoadException"/>，<see cref="AggregateException"/>进行处理
        /// </summary>
        /// <param name="exc"></param>
        /// <returns></returns>
        public static string GetDetails(this Exception exc)
        {
            var exception = exc.GetBaseException();

            if (exception is ReflectionTypeLoadException)
            {
                var typeLoadException = exception as ReflectionTypeLoadException;
                return typeLoadException.Message + ":" + typeLoadException.LoaderExceptions.Select(p => p.Message).Distinct().Join(",") + "\r\n" + typeLoadException.StackTrace;
            }

            if (exception is AggregateException)
            {
                var aggregateException = exception as AggregateException;
                return aggregateException.Message + ":" + aggregateException.InnerExceptions.Select(p => p.Message).Distinct().Join(",") + "\r\n" + aggregateException.StackTrace;
            }
            return exc.ToString();
        }
    }
}

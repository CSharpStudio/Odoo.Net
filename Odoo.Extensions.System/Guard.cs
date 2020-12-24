using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System
{
    /// <summary>
    /// 检查
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// 参数不能为null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static T NotNull<T>(this T value, string parameterName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }
        /// <summary>
        /// 字符串参数不能为null或者<see cref="string.Empty"/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        public static string NotNullOrEmpty(this string value, string parameterName)
        {
            if (value.IsNullOrEmpty())
            {
                throw new ArgumentException("参数[{0}]不能为空".FormatArgs(parameterName), parameterName);
            }

            return value;
        }
        /// <summary>
        /// 集合参数不能为null或者<see cref="ICollection.Count"/>为0
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        public static ICollection<T> NotNullOrEmpty<T>(this ICollection<T> value, string parameterName)
        {
            if (value.IsNullOrEmpty())
            {
                throw new ArgumentException("参数[{0}]不能为空".FormatArgs(parameterName), parameterName);
            }

            return value;
        }
        /// <summary>
        /// 字符串参数不能为null或者<see cref="string.Empty"/>或者空格
        /// </summary>
        /// <param name="value"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        public static string NotNullOrWhiteSpace(this string value, string parameterName)
        {
            if (value.IsNullOrWhiteSpace())
            {
                throw new ArgumentException("参数[{0}]不能为空".FormatArgs(parameterName), parameterName);
            }

            return value;
        }
        /// <summary>
        /// 断言字符串参数不能为null或者<see cref="string.Empty"/>或者空格
        /// </summary>
        /// <param name="value"></param>
        /// <param name="message"></param>
        /// <exception cref="AssertException"></exception>
        /// <returns></returns>
        public static string AssertNotNullOrWhiteSpace(this string value, string message)
        {
            if (value.IsNullOrWhiteSpace())
                throw new AssertException("值为空:" + message);
            return value;
        }
        /// <summary>
        /// 断言变量不为空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="message">错误信息</param>
        /// <exception cref="AssertException">value is null</exception>
        /// <returns></returns>
        public static ICollection<T> AssertNotNullOrEmpty<T>(this ICollection<T> value, string message)
        {
            if (value.IsNullOrEmpty())
                throw new AssertException("值为空:" + message);
            return value;
        }

        /// <summary>
        /// 断言变量不为空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">要判断的值</param>
        /// <param name="message">错误信息</param>
        /// <exception cref="AssertException">value is null</exception>
        /// <returns></returns>
        public static T AssertNotNull<T>(this T value, string message)
        {
            if (value == null)
                throw new AssertException("值为空:" + message);
            return value;
        }

        /// <summary>
        /// 断言变量不为空, AssertNotNull简写
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">要判断的值</param>
        /// <param name="message">错误信息</param>
        /// <returns></returns>
        public static T ANN<T>(this T value, string message)
        {
            return AssertNotNull(value, message);
        }
    }

    /// <summary>
    /// 断言异常
    /// </summary>
    [Serializable]
    public class AssertException : ApplicationException
    {
        /// <summary>
        /// 构造<see cref="AssertException"/>实例
        /// </summary>
        public AssertException() { }

        /// <summary>
        /// 使用指定的信息构造<see cref="AppException"/>实例
        /// </summary>
        /// <param name="message">异常信息</param>
        public AssertException(string message) : base(message) { }

        /// <summary>
        /// 使用指定的信息和相关的异常构造<see cref="AppException"/>实例
        /// </summary>
        /// <param name="message">异常信息</param>
        /// <param name="innerException">相关异常</param>
        public AssertException(string message, Exception innerException) : base(message, innerException) { }
        /// <summary>
        /// Initializes a new instance with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected AssertException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}

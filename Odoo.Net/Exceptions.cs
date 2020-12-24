using System;
using System.Runtime.Serialization;

namespace Odoo.Net
{
    /// <summary>
    /// 领域模型异常
    /// </summary>
    [Serializable]
    public class DomainException : ApplicationException
    {
        /// <summary>
        /// 构建<see cref="DomainException"/>
        /// </summary>
        public DomainException() { }
        /// <summary>
        /// 构建<see cref="DomainException"/>
        /// </summary>
        /// <param name="message"></param>
        public DomainException(string message) : base(message) { }
        /// <summary>
        /// 构建<see cref="DomainException"/>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public DomainException(string message, Exception innerException) : base(message, innerException) { }
        /// <summary>
        /// 构建<see cref="DomainException"/>
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected DomainException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    /// <summary>
    /// 领域模型访问异常
    /// </summary>
    [Serializable]
    public class DomainAccessException : ApplicationException
    {
        /// <summary>
        /// 构建<see cref="DomainAccessException"/>
        /// </summary>
        public DomainAccessException() { }
        /// <summary>
        /// 构建<see cref="DomainAccessException"/>
        /// </summary>
        /// <param name="message"></param>
        public DomainAccessException(string message) : base(message) { }
        /// <summary>
        /// 构建<see cref="DomainAccessException"/>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public DomainAccessException(string message, Exception innerException) : base(message, innerException) { }
        /// <summary>
        /// 构建<see cref="DomainAccessException"/>
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected DomainAccessException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    /// <summary>
    /// 找不到方法异常
    /// </summary>
    [Serializable]
    public class MethodNotFoundException : ApplicationException
    {
        /// <summary>
        /// 构建<see cref="MethodNotFoundException"/>
        /// </summary>
        public MethodNotFoundException() { }
        /// <summary>
        /// 构建<see cref="MethodNotFoundException"/>
        /// </summary>
        /// <param name="message"></param>
        public MethodNotFoundException(string message) : base(message) { }
        /// <summary>
        /// 构建<see cref="MethodNotFoundException"/>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public MethodNotFoundException(string message, Exception innerException) : base(message, innerException) { }
        /// <summary>
        /// 构建<see cref="MethodNotFoundException"/>
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected MethodNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    /// <summary>
    /// 找不到方法异常
    /// </summary>
    [Serializable]
    public class MethodAmbiguityException : ApplicationException
    {
        /// <summary>
        /// 构建<see cref="MethodAmbiguityException"/>
        /// </summary>
        public MethodAmbiguityException() { }
        /// <summary>
        /// 构建<see cref="MethodAmbiguityException"/>
        /// </summary>
        /// <param name="message"></param>
        public MethodAmbiguityException(string message) : base(message) { }
        /// <summary>
        /// 构建<see cref="MethodAmbiguityException"/>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public MethodAmbiguityException(string message, Exception innerException) : base(message, innerException) { }
        /// <summary>
        /// 构建<see cref="MethodAmbiguityException"/>
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected MethodAmbiguityException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}

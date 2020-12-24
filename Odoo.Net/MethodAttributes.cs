using System;

namespace Odoo.Net
{
    /// <summary>
    /// 字段变更
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class OnChangeAttribute : Attribute
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string[] Fields { get; set; }

        /// <summary>
        /// 构建<see cref="OnChangeAttribute"/>
        /// </summary>
        public OnChangeAttribute(params string[] fields) => Fields = fields ?? throw new ArgumentNullException(nameof(fields));
    }
    /// <summary>
    /// API方法声明
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class MethodAttribute : Attribute
    {
        /// <summary>
        /// 方法名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 构建<see cref="MethodAttribute"/>
        /// </summary>
        /// <param name="name"></param>
        public MethodAttribute(string name = null)
        {
            Name = name;
        }
    }
}

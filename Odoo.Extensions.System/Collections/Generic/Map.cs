namespace System.Collections.Generic
{
    /// <summary>
    /// 键值对字典，使用不区分大小写的字符串为键
    /// </summary>
    public class Map : Dict<string, object>
    {
        /// <summary>
        /// 构建<see cref="Map"/>, 键不区分大小写
        /// </summary>
        public Map() : base(StringComparer.OrdinalIgnoreCase) { }
        /// <summary>
        /// 使用指定值构建<see cref="Map"/>
        /// </summary>
        /// <param name="values">键值</param>
        public Map(IDictionary<string, object> values) : base(values, StringComparer.OrdinalIgnoreCase) { }
        /// <summary>
        /// 使用指定值构建<see cref="Map"/>
        /// </summary>
        /// <param name="values">键值</param>
        public Map(int capacity) : base(capacity, StringComparer.OrdinalIgnoreCase) { }
    }
}

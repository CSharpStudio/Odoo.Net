using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Odoo.Net.Core
{
    public abstract class MergableAttribute : Attribute
    {
        readonly Dictionary<string, object> _values = new Dictionary<string, object>();
        /// <summary>
        /// 获取值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        protected T Get<T>([CallerMemberName] string property = null)
        {
            if (_values.TryGetValue(property, out object value))
                return value.ConvertTo<T>();
            return default;
        }
        /// <summary>
        /// 获取值，提供默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="defaultValue"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        protected T Get<T>(T defaultValue, [CallerMemberName] string property = null)
        {
            if (_values.TryGetValue(property, out object value))
                return value.ConvertTo<T>();
            return defaultValue;
        }
        /// <summary>
        /// 获取值，提供默认值提供者
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="defaultValueProvider"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        protected T Get<T>(Func<T> defaultValueProvider, [CallerMemberName] string property = null)
        {
            if (_values.TryGetValue(property, out object value))
                return value.ConvertTo<T>();
            return defaultValueProvider();
        }
        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="property"></param>
        protected void Set(object value, [CallerMemberName] string property = null)
        {
            _values[property] = value;
        }
        /// <summary>
        /// 如果value是null，移除键
        /// </summary>
        /// <param name="value"></param>
        /// <param name="property"></param>
        protected void NSet(object value, [CallerMemberName] string property = null)
        {
            if (value == null)
                _values.Remove(property);
            else
                _values[property] = value;
        }

        /// <summary>
        /// 是否能合并
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public virtual bool CanMerge(MergableAttribute target)
        {
            return GetType() == target.GetType();
        }

        /// <summary>
        /// 合并
        /// </summary>
        /// <param name="target"></param>
        public virtual void MergeTo(MergableAttribute target)
        {
            foreach (var kv in _values)
                target._values[kv.Key] = kv.Value;
        }
    }
}

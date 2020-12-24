using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;

namespace Odoo.Net.Core
{
    /// <summary>
    /// 方法元数据
    /// </summary>
    [DebuggerDisplay("Method:{Name} Model={Model}")]
    public class Method
    {
        Model _instance;
        /// <summary>
        /// 实例对象
        /// </summary>
        public Model ObjectInstance { get => _instance ??= CreateInstance(); }
        /// <summary>
        /// 模型名称
        /// </summary>
        public MetaModel Model { get; set; }
        /// <summary>
        /// 方法
        /// </summary>
        public MethodInfo MethodInfo { get; set; }
        /// <summary>
        /// 方法名称
        /// </summary>
        public string Name { get; set; }

        public object Invoke(object[] args)
        {
            return MethodInfo.Invoke(ObjectInstance, args);
        }

        static ConcurrentDictionary<Type, Model> _instances = new ConcurrentDictionary<Type, Model>();
        Model CreateInstance()
        {
            if (!_instances.TryGetValue(MethodInfo.ReflectedType, out Model instance))
            {
                instance = MetaModel.Create(MethodInfo.ReflectedType);
                instance.Meta = Model;
                _instances.TryAdd(MethodInfo.ReflectedType, instance);
            }
            return instance;
        }
    }
}

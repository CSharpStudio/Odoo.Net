using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Odoo.Net.Core
{
    /// <summary>
    /// 元模型
    /// </summary>
    [DebuggerDisplay("Model:{Name} Inherit={Inherit} Fields={Fields.Count} Methods={_allMethods.Count}")]
    public partial class MetaModel
    {
        static readonly ProxyGenerator _proxyGenerator = new ProxyGenerator();

        Dictionary<string, Field> _nameFields;

        readonly Dictionary<string, List<Method>> _allMethods = new Dictionary<string, List<Method>>(StringComparer.OrdinalIgnoreCase);

        readonly Dictionary<string, Method> _keyMethods = new Dictionary<string, Method>(StringComparer.OrdinalIgnoreCase);

        readonly Dictionary<string, Method> _onchangeMethods = new Dictionary<string, Method>(StringComparer.OrdinalIgnoreCase);

        public static Model Create(Type modelType)
        {
            return (Model)_proxyGenerator.CreateClassProxy(modelType, ModelInterceptor.Interceptor);
        }
        public static T Create<T>() where T : Self
        {
            return _proxyGenerator.CreateClassProxy<T>(ModelInterceptor.Interceptor);
        }

        /// <summary>
        /// 模型名称
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// 继承的模型基类名称（name）
        /// </summary>
        public string Inherit { get; set; }
        /// <summary>
        /// 抽象
        /// </summary>
        public bool Abstract { get; set; } = false;

        ModelAttribute attr;
        public ModelAttribute Attr => attr ??= Attributes.OfType<ModelAttribute>().FirstOrDefault() ?? new ModelAttribute { Name = Name, Inherit = Inherit, IsAbstract = Abstract };

        public Registry Container { get; internal set; }
        /// <summary>
        /// 字段集合
        /// </summary>
        public IReadOnlyCollection<Field> Fields { get; private set; }
        /// <summary>
        /// 特性集合
        /// </summary>
        public IReadOnlyCollection<Attribute> Attributes { get; private set; }

        /// <summary>
        /// 公开的方法
        /// </summary>
        public IEnumerable<Method> Methods => _keyMethods.Values;

        /// <summary>
        /// 构建<see cref="MetaModel"/>
        /// </summary>
        /// <param name="name">模型名称</param>
        internal MetaModel([DisallowNull] string name)
        {
            Name = name.NotNullOrEmpty(nameof(name));
            Fields = Array.Empty<Field>();
            Attributes = Array.Empty<Attribute>();
        }

        /// <summary>
        /// 构建<see cref="MetaModel"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="container"></param>
        public MetaModel([DisallowNull] string name, [DisallowNull] Registry container) : this(name)
        {
            Container = container.NotNull(nameof(container));
        }

        /// <summary>
        /// 构造模型描述器
        /// </summary>
        /// <param name="name">模型名称</param>
        /// <param name="fields">字段集合</param>
        /// <param name="attributes">特性集合</param>
        /// <exception cref="ArgumentException">字段名称重复</exception>
        public MetaModel([DisallowNull] string name, [DisallowNull] IEnumerable<Field> fields, IEnumerable<Attribute> attributes = null)
        {
            name.NotNullOrEmpty(nameof(name));
            Name = name;
            Fields = fields.NotNull(nameof(fields)).ToArray();
            Attributes = attributes?.ToArray() ?? new Attribute[0];
            try
            {
                _nameFields = fields.ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase);
            }
            catch (ArgumentException exc)
            {
                var fieldNames = fields.GroupBy(p => p.Name, StringComparer.OrdinalIgnoreCase).Where(p => p.Count() > 1).Select(p => p.Key).Join(",");
                throw new DomainException("模型[{0}]字段[{1}]重复".FormatArgs(name, fieldNames), exc);
            }
        }
        /// <summary>
        /// Create a recordset instance.
        /// </summary>
        /// <param name="env">an environment</param>
        /// <param name="ids">a tuple of record ids</param>
        /// <param name="prefetchIds"></param>
        /// <returns></returns>
        public Self Browse(Environment env, Ids ids, Ids prefetchIds)
        {
            var m = new Self(this);
            m.Env = env;
            m.Ids = ids;
            m.PrefetchIds = prefetchIds;
            return m;
        }

        internal void SetAttributes(IEnumerable<Attribute> attributes)
        {
            Attributes = attributes?.ToArray() ?? new Attribute[0];
        }

        internal void SetFields(Dictionary<string, Field> fields)
        {
            Fields = fields.Values;
            _nameFields = fields;
        }

        /// <summary>
        /// 查找字段，不区分大小写，找不到时返回 null
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <exception cref="ArgumentException">fieldName为空</exception>
        /// <returns>返回指定名称的<see cref="Field"/></returns>
        public Field FindField(string fieldName)
        {
            fieldName.NotNullOrEmpty(nameof(fieldName));
            Field field = null;
            _nameFields?.TryGetValue(fieldName, out field);
            return field;
        }

        public Field GetField(string fieldName)
        {
            return FindField(fieldName) ?? throw new MissingFieldException($"模型[{Name}]找不到字段[{fieldName}]");
        }

        /// <summary>
        /// 继承父类的方法
        /// </summary>
        /// <param name="parent"></param>
        public void AddParent(MetaModel parent)
        {
            foreach (var m in parent._allMethods)
            {
                if (!_allMethods.TryGetValue(m.Key, out List<Method> methods))
                {
                    methods = new List<Method>();
                    _allMethods[m.Key] = methods;
                }
                methods.AddRange(m.Value.Select(p => new Method { Model = this, MethodInfo = p.MethodInfo, Name = p.Name }));
            }
            foreach (var m in parent._keyMethods)
                _keyMethods[m.Key] = new Method { Model = this, MethodInfo = m.Value.MethodInfo, Name = m.Value.Name };
            foreach (var m in parent._onchangeMethods)
                _onchangeMethods[m.Key] = new Method { Model = this, MethodInfo = m.Value.MethodInfo, Name = m.Value.Name };
        }

        /// <summary>
        /// 注册方法
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="method"></param>
        public void Register(MethodInfo method)
        {
            if (method == null)
                throw new ArgumentNullException(nameof(method));
            var attr = method.GetCustomAttribute<MethodAttribute>();
            var descriptor = new Method { Model = this, MethodInfo = method, Name = attr?.Name ?? method.Name };
            if (method.IsPublic)
            {
                var key = GetMethodKey(method);
                _keyMethods[key] = descriptor;
            }
            if (!_allMethods.TryGetValue(method.Name, out List<Method> methods))
            {
                methods = new List<Method>();
                _allMethods[method.Name] = methods;
            }
            var onchanged = method.GetCustomAttribute<OnChangeAttribute>();
            if (onchanged != null)
            {
                var args = descriptor.MethodInfo.GetParameters();
                if (args.Length != 2 || args[0].ParameterType != typeof(Self) || args[1].ParameterType != typeof(string))
                    throw new NotSupportedException($"模型API[{Name}]OnChange方法[{descriptor.MethodInfo.Name}]参数必须是两个:参数一类型为Model,参数二类型为String");
                foreach (var field in onchanged.Fields.Where(p => !string.IsNullOrEmpty(p)))
                    _onchangeMethods[field.ToLower()] = descriptor;
            }
            methods.Add(descriptor);
        }

        /// <summary>
        /// 尝试执行public方法,方法名不区分大小写
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="args"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TryInvoke(string methodName, object[] args, out object result)
        {
            _allMethods.TryGetValue(methodName, out List<Method> methods);
            if (methods.IsNotEmpty())
            {
                foreach (var method in methods)
                {
                    var parameterInfo = method.MethodInfo.GetParameters();
                    if (IsParameterMatch(parameterInfo, args, out object[] invokeArgs))
                    {
                        result = method.Invoke(invokeArgs);
                        return true;
                    }
                }
            }
            result = null;
            return false;
        }

        /// <summary>
        /// 获取方法,方法名不区分大小写
        /// </summary>
        /// <param name="method"></param>
        /// <param name="parameters"></param>
        /// <exception cref="MethodNotFoundException">找不到方法</exception>
        /// <returns></returns>
        public Method GetMethod(string method, Type[] parameterTypes)
        {
            var meta = FindMethod(method, parameterTypes);
            return meta ?? throw new MethodNotFoundException($"找不到模型[{Name}]API方法[{method}({string.Join(",", parameterTypes.Select(p => p.Name))})]");
        }

        /// <summary>
        /// 查找方法,方法名不区分大小写
        /// </summary>
        /// <param name="method"></param>
        /// <param name="parameterTypes"></param>
        /// <returns></returns>
        public Method FindMethod(string method, Type[] parameterTypes)
        {
            if (string.IsNullOrEmpty(method))
                throw new ArgumentNullException(nameof(method));
            if (parameterTypes == null)
                throw new ArgumentNullException(nameof(parameterTypes));
            if (_allMethods.TryGetValue(method, out List<Method> methods))
            {
                foreach (var m in methods)
                {
                    var para = m.MethodInfo.GetParameters();
                    if (!IsParameterMatch(para, parameterTypes))
                        continue;
                    return m;
                }
            }
            return null;
        }

        /// <summary>
        /// 查找标记<see cref="OnChangeAttribute"/>的方法,字段名不区分大小写
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public Method FindOnChange(string field)
        {
            field.NotNullOrEmpty(nameof(field));
            _onchangeMethods.TryGetValue(field, out Method method);
            return method;
        }

        /// <summary>
        /// 查的重写的方法描述器
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public Method FindOverridMethod(MethodInfo method)
        {
            if (method == null)
                throw new ArgumentNullException(nameof(method));
            if (!_allMethods.TryGetValue(method.Name, out List<Method> methods))
                return null;
            var parameters = method.GetParameters();
            for (int i = methods.Count - 1; i > 0; i--)
            {
                var m = methods[i];
                var para = m.MethodInfo.GetParameters();
                if (IsParameterMatch(parameters, para))
                {
                    if (m.MethodInfo.DeclaringType == method.DeclaringType)
                        return null;
                    return m;
                }
            }
            return null;
        }

        /// <summary>
        /// 查找父方法描述器
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public Method FindParentMethod(MethodInfo method)
        {
            if (method == null)
                throw new ArgumentNullException(nameof(method));
            if (!_allMethods.TryGetValue(method.Name, out List<Method> methods))
                return null;
            var parameters = method.GetParameters();
            for (int i = 0; i < methods.Count; i++)
            {
                var m = methods[i];
                if (m.MethodInfo == method)
                    return null;
                var para = m.MethodInfo.GetParameters();
                if (!IsParameterMatch(parameters, para))
                    continue;
                return m;
            }
            return null;
        }

        bool IsParameterMatch(ParameterInfo[] source, ParameterInfo[] target)
        {
            if (source.Length != target.Length)
                return false;
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i].ParameterType != target[i].ParameterType)
                {
                    return false;
                }
            }
            return true;
        }

        bool IsParameterMatch(ParameterInfo[] parameters, Type[] types)
        {
            if (parameters.Length != types.Length)
                return false;
            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i].ParameterType != types[i])
                {
                    return false;
                }
            }
            return true;
        }

        bool IsParameterMatch(ParameterInfo[] parameterInfo, object[] args, out object[] invokeArgs)
        {
            invokeArgs = new object[parameterInfo.Length];
            if (args.Length > parameterInfo.Length)
                return false;
            for (int i = 0; i < parameterInfo.Length; i++)
            {
                var p = parameterInfo[i];
                if (i < args.Length)
                {
                    if (!args[i].TryConventTo(p.ParameterType, out object value))
                        return false;
                    invokeArgs[i] = value;
                }
                else if (p.IsOptional)
                    invokeArgs[i] = p.DefaultValue;
                else
                    return false;
            }
            return true;
        }

        string GetMethodKey(MethodInfo method)
        {
            return $"{method.Name}({string.Join(",", method.GetParameters().Select(p => p.ParameterType.Name + (p.IsOptional ? "*" : "")))})";
        }
    }
}

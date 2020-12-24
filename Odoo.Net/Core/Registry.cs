using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Odoo.Net.Core
{
    /// <summary>
    /// 模型注册表，用于管理模型的元数据，注册模型，创建模型实例
    /// </summary>
    [DebuggerDisplay("ModelContainer:Count={_models.Count}")]
    public class Registry
    {
        readonly Dictionary<string, MetaModel> _models = new Dictionary<string, MetaModel>(StringComparer.OrdinalIgnoreCase);
        static readonly object _lockRegisterObj = new object();

        /// <summary>
        /// 所有已注册的元模型
        /// </summary>

        public IEnumerable<MetaModel> Models { get => _models.Values; }

        /// <summary>
        /// 构建<see cref="Registry"/>
        /// </summary>
        public Registry()
        {
            Register(typeof(Model));
        }

        /// <summary>
        /// 获取元模型
        /// </summary>
        /// <param name="modelName">模型名称</param>
        /// <exception cref="KeyNotFoundException">modelName不存在</exception>
        /// <returns></returns>
        public MetaModel GetMetaModel(string modelName)
        {
            modelName.NotNullOrEmpty(nameof(modelName));
            if (_models.TryGetValue(modelName, out MetaModel model))
                return model;
            throw new KeyNotFoundException("模型[{0}]未注册".FormatArgs(modelName));
        }

        /// <summary>
        /// 索引器获取元模型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public MetaModel this[string model] => GetMetaModel(model);

        /// <summary>
        /// 重置清空模型结构
        /// </summary>
        public void Reset(string modelName = null)
        {
            if (modelName.IsNullOrEmpty())
                _models.Clear();
            else
                _models.Remove(modelName);
        }

        /// <summary>
        /// 注册实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Register<T>() where T : Model
        {
            Register(typeof(T));
        }

        /// <summary>
        /// 注册实体
        /// </summary>
        /// <param name="entityType"></param>
        public void Register(Type entityType)
        {
            entityType.NotNull(nameof(entityType));
            if (!typeof(Model).IsAssignableFrom(entityType))
                throw new ArgumentException($"参数entityType[{entityType.GetQualifiedName()}]不是Entity的子类");
            lock (_lockRegisterObj)
            {
                var attribute = entityType.GetCustomAttribute<ModelAttribute>();
                if (attribute.Inherit.IsNotEmpty() && attribute.Inherits.IsNotEmpty())
                    throw new ArgumentException($"模型[{entityType.GetQualifiedName()}]不能同时指定Inherit和Inherits");
                var modelName = attribute?.Name ?? attribute?.Inherit ?? entityType.Name;
                var inherit = attribute?.Inherit ?? "base";
                var inherits = new List<string>();
                if (attribute.Inherits.IsNotEmpty())
                    inherits.AddRange(attribute.Inherits);
                else
                    inherits.Add(inherit);

                //模型 Name==Inherit 表示的是扩展，是可以重新注册的
                if (modelName != inherit && _models.ContainsKey(modelName))
                    throw new ArgumentException("已经存在模型名称：{0} 不允许重复注册".FormatArgs(modelName));

                if (!_models.TryGetValue(modelName, out MetaModel descriptor))
                {
                    descriptor = new MetaModel(modelName, this);
                    _models[modelName] = descriptor;
                }

                descriptor.Abstract = attribute?.IsAbstract ?? entityType.IsAbstract;

                var attributes = new List<Attribute>();
                var fields = new Dictionary<string, Field>(StringComparer.OrdinalIgnoreCase);
                foreach (var i in inherits)
                {
                    if (!_models.TryGetValue(i, out MetaModel parent))
                        throw new DomainException($"模型[{entityType.GetQualifiedName()}]继承的模型[{i}]未注册");
                    if (descriptor != parent)
                        descriptor.AddParent(parent);
                    attributes.AddRange(parent.Attributes);
                    foreach (var field in parent.Fields)
                        fields.TryAdd(field.Name, field);
                }

                attributes = Override(attributes, entityType.GetCustomAttributes());
                descriptor.SetAttributes(attributes);

                foreach (var f in entityType.GetFields(BindingFlags.Public | BindingFlags.Static))
                {
                    if (typeof(Field).IsAssignableFrom(f.FieldType))
                    {
                        var field = (Field)f.GetValue(null);
                        field.Name = f.Name;
                        fields.Add(field.Name, field);
                    }
                }
                if (descriptor.Attr.LogAccess)
                {
                    //if (!fields.ContainsKey("CreateUid"))
                    //    fields["CreateUid"] = new Many2OneField("CreateUid", typeof(RefId), typeof(Model), new[] { new Many2OneAttribute("res.users") });
                    //if (!fields.ContainsKey("CreateDate"))
                    //    fields["CreateDate"] = new DateTimeField("CreateDate", typeof(RefId), typeof(Model), new[] { new DateTimeAttribute() });
                    //if (!fields.ContainsKey("WriteUid"))
                    //    fields["WriteUid"] = new Many2OneField("WriteUid", typeof(RefId), typeof(Model), new[] { new Many2OneAttribute("res.users") });
                    //if (!fields.ContainsKey("WriteDate"))
                    //    fields["WriteDate"] = new DateTimeField("WriteDate", typeof(RefId), typeof(Model), new[] { new DateTimeAttribute() });
                }
                descriptor.SetFields(fields);

                var methods = entityType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic)
                    .Where(m => !m.IsGenericMethod && !m.IsAbstract && !m.IsSpecialName && (m.IsPublic || m.IsVirtual) && m.DeclaringType == entityType);
                foreach (var method in methods)
                {
                    descriptor.Register(method);
                }
            }
        }

        List<Attribute> Override(IEnumerable<Attribute> bases, IEnumerable<Attribute> overrides)
        {
            var result = new List<Attribute>();
            result.AddRange(bases);
            foreach (var over in overrides)
            {
                if (over is MergableAttribute mergeable)
                {
                    var exists = bases.OfType<MergableAttribute>().Where(p => mergeable.CanMerge(p));
                    if (exists.Any())
                    {
                        foreach (var e in exists)//找到可以合并的就合并，不能合并的就加入
                            mergeable.MergeTo(e);
                        continue;
                    }
                }
                result.Add(over);
            }
            return result;
        }

        /// <summary>
        /// 模型是否已注册
        /// </summary>
        /// <param name="modelName">模型名称</param>
        /// <returns></returns>
        public bool IsRegister(string modelName)
        {
            return _models.ContainsKey(modelName.NotNull(nameof(modelName)));
        }
    }
}

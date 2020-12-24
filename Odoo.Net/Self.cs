using Odoo.Net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Odoo.Net
{
    public class Self : IEnumerable<Self>
    {
        MetaModel _meta;
        Ids _ids;
        Ids _prefetchIds;
        Map _context;

        public MetaModel Meta { get => _meta ?? throw new ArgumentException("MetaModel未设置"); set => _meta = value; }

        static AsyncLocal<Core.Environment> _env = new AsyncLocal<Core.Environment>();
        public Core.Environment Env { get => _env.Value ?? throw new ArgumentException("Env未设置"); set => _env.Value = value; }
        /// <summary>
        /// id集合
        /// </summary>
        public Ids Ids { get => _ids ??= new Ids(); set => _ids = value; }
        /// <summary>
        /// 预加载id集合
        /// </summary>
        public Ids PrefetchIds { get => _prefetchIds ??= new Ids(); set => _prefetchIds = value; }
        /// <summary>
        /// 上下文
        /// </summary>
        public Map Context { get => _context ??= new Map(); set => _context = value; }

        /// <summary>
        /// 构建<see cref="Self"/>
        /// </summary>
        /// <param name="meta">元模型</param>
        internal Self(MetaModel meta)
        {
            _meta = meta;
        }

        /// <summary>
        /// Returns a recordset for the ids provided as parameter in the current environment
        /// Can take no ids, a single id or an iterable of ids.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public virtual Self Browse(Ids ids = null)
        {
            return Meta.Browse(Env, ids, ids);
        }

        public virtual object Call(string method, params object[] args)
        {
            var param = new List<object>();
            if (args.Length == 0 || args[0] is not Self)
                param.Add(this);
            param.AddRange(args);
            if (!Meta.TryInvoke(method, param.ToArray(), out object result))
                throw new MethodNotFoundException($"模型[{Meta.Name}]找不到方法:{method}({param.Select(p => p?.GetType().Name ?? "null").Join(",")})");
            return result;
        }

        /// <summary>
        /// 获取字段值
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <returns></returns>
        public object Get(string fieldName)
        {
            var field = Meta.GetField(fieldName);
            return Get(field);
        }
        /// <summary>
        /// 获取字段值
        /// </summary>
        /// <param name="field">字段描述器</param>
        /// <returns></returns>
        public object Get(Field field)
        {
            return field.Get(this);
        }

        /// <summary>
        /// 设置字段值
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">值</param>
        public void Set(string fieldName, object value)
        {
            var field = Meta.GetField(fieldName);
            Set(field, value);
        }

        /// <summary>
        /// 设置字段值
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        public void Set(Field field, object value)
        {
            field.Set(this, value);
        }

        IEnumerator<Self> IEnumerable<Self>.GetEnumerator()
        {
            if (_ids != null)
                foreach (var id in Ids)
                    yield return Browse(id);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            if (_ids != null)
                foreach (var id in Ids)
                    yield return Browse(id);
        }
    }
}

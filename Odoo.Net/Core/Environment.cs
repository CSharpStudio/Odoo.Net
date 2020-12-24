using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Odoo.Net.Core
{
    /// <summary>
    /// 环境
    /// </summary>
    public class Environment : DisposableBase
    {
        static AsyncLocal<Environment> Local = new AsyncLocal<Environment>();

        /// <summary>
        /// 当前
        /// </summary>
        public static Environment Current => Local.Value;

        /// <summary>
        /// 获取或创建<see cref="Environment"/>
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static Environment GetOrCreate(IServiceProvider serviceProvider)
        {
            if (Local.Value == null)
            {
                var env = new Environment(serviceProvider);
                Local.Value = env;
            }
            return Local.Value;
        }
        /// <summary>
        /// 清空
        /// </summary>
        public static void Clear()
        {
            Local.Value?.Dispose();
            Local.Value = null;
        }
        /// <summary>
        /// 服务提供者
        /// </summary>
        public IServiceProvider ServiceProvider { get; }
        /// <summary>
        /// 上下文
        /// </summary>
        public Map Context { get; }
        Dictionary<string, Self> _typeCache;
        internal Dictionary<string, Self> TypeCache => _typeCache ??= new Dictionary<string, Self>();
        /// <summary>
        /// 注册器
        /// </summary>
        public Registry Registry { get; }
        /// <summary>
        /// 租户
        /// </summary>
        public string Tenant { get; set; }
        /// <summary>
        /// 文化
        /// </summary>
        public string Culture
        {
            get => Context.GetString("culture");
            set => Context["culture"] = value;
        }
        public Self Company { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string Uid { get; set; }
        /// <summary>
        /// 是否超级用户
        /// </summary>
        public bool SuperUser { get; set; }
        //Database cursor;
        ///// <summary>
        ///// 游标，用于访问数据库
        ///// </summary>
        //public Database Cursor => cursor ??= ServiceProvider.GetRequiredService<Database>();
        Cache cache;
        /// <summary>
        /// 缓存模型的值
        /// </summary>
        public Cache Cache => cache ??= new Cache();
        /// <summary>
        /// 构建<see cref="Environment"/>
        /// </summary>
        /// <param name="serviceProvider"></param>
        public Environment(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider.NotNull(nameof(serviceProvider));
            Registry = ServiceProvider.GetRequiredService<Registry>();
            Context = new Map();
        }
        /// <summary>
        /// 获取元模型
        /// </summary>
        /// <param name="model"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public Self this[string model] => Registry[model].Browse(this, null, null);
    }
}

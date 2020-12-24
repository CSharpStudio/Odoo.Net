using Odoo.Net.Base;
using Odoo.Net.Core;
using System;

namespace Odoo.Net
{
    public static class Extensions
    {
        public static Registry RegisterBase(this Registry registry)
        {
            registry.Register<IrDefault>();
            return registry;
        }

        /// <summary>
        /// 检查集合是null或<see cref="Ids.Count"/>为0
        /// </summary>
        public static bool IsNullOrEmpty(this Ids source)
        {
            return source == null || source.Count <= 0;
        }

        /// <summary>
        /// 检查是null或空
        /// </summary>
        public static bool IsNullOrEmpty(this RefId source)
        {
            return source == null || source.Id.IsNullOrEmpty();
        }

        /// <summary>
        /// 检查集合不是null且<see cref="Ids.Count"/>大于0
        /// </summary>
        public static bool IsNotEmpty(this Ids source)
        {
            return source != null && source.Count > 0;
        }

        /// <summary>
        /// 检查不是null且不为空
        /// </summary>
        public static bool IsNotEmpty(this RefId source)
        {
            return source != null && source.Id.IsNotEmpty();
        }
    }
}

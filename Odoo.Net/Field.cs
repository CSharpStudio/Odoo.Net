using Odoo.Net.Core;
using Odoo.Net.Data;
using System;
using System.Collections.Generic;

namespace Odoo.Net
{
    public abstract class Field : MetaField
    {        /// <summary>
             /// 字段名称
             /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 模块
        /// </summary>
        public string Module { get => Get<string>(); set => Set(value); }
        /// <summary>
        /// 标题：the label of the field seen by users (string); if not
        /// set, the ORM takes the field name in the class (capitalized).
        /// </summary>
        public string Caption { get => Get<string>(); set => Set(value); }
        /// <summary>
        /// 帮助信息显示：the tooltip of the field seen by users (string)
        /// </summary>
        public string Help { get => Get<string>(); set => Set(value); }
        /// <summary>
        /// 是否只读：whether the field is readonly (boolean, by default ``False``)
        /// </summary>
        public bool Readonly { get => Get<bool>(false); set => Set(value); }
        /// <summary>
        /// 是否必填：whether the value of the field is required (boolean, by default ``False``)
        /// </summary>
        public bool Required { get => Get<bool>(false); set => Set(value); }
        /// <summary>
        /// 是否索引：whether the field is indexed in database. Note: no effect
        /// on non-stored and virtual fields. (boolean, by default ``False``)
        /// </summary>
        public bool Index { get => Get<bool>(false); set => Set(value); }
        /// <summary>
        /// 默认值：the default value for the field; this is either a static
        /// value, or a function taking a recordset and returning a value; use
        /// ``default=null`` to discard default values for the field
        /// </summary>
        public object DefaultValue { get => Get<object>(); set => Set(value); }
        /// <summary>
        /// 默认值委托
        /// </summary>
        public Func<Self, object> Default { get => Get<Func<Self, object>>(); set => Set(value); }
        /// <summary>
        /// 权限组：comma-separated list of group xml ids (string); this
        /// restricts the field access to the users of the given groups only
        /// </summary>
        public string Groups { get => Get<string>(); set => Set(value); }
        /// <summary>
        /// 复制模型时是否复制字段:whether the field value should be copied when the record
        ///    is duplicated(default: ``True`` for normal fields, ``False`` for
        ///    ``one2many`` and computed fields, including property fields and
        ///    related fields)
        /// </summary>
        public virtual bool Copy { get => Get<bool>(true); set => Set(value); }
        /// <summary>
        /// 计算方法：name of a method that computes the field
        /// </summary>
        public Action Compute { get => Get<Action>(); set => Set(value); }
        /// <summary>
        /// 计算逆方法：name of a method that inverses the field (optional)
        /// </summary>
        public Action Inverse { get => Get<Action>(); set => Set(value); }
        /// <summary>
        /// 查询方法：name of a method that implement search on the field (optional)
        /// </summary>
        public Action Search { get => Get<Action>(); set => Set(value); }
        /// <summary>
        /// 是否持久化：whether the field is stored in database (boolean, by
        /// default ``False`` on computed fields)
        /// </summary>
        public virtual bool Store { get => Get<bool>(true); set => Set(value); }
        /// <summary>
        /// 超级管理员重新计算：whether the field should be recomputed as superuser
        /// to bypass access rights(boolean, by default ``True``)
        /// </summary>
        public virtual bool ComputeSudo { get => Get<bool>(true); set => Set(value); }
        /// <summary>
        /// 分组操作：operator for aggregating values
        /// </summary>
        public virtual string GroupOperator { get => Get<string>(); set => Set(value); }
        /// <summary>
        /// 是否加载：whether the field is prefetched
        /// </summary>
        public virtual bool Prefetch { get => Get<bool>(true); set => Set(value); }

        public virtual string Related { get => Get<string>(); set => Set(value); }
        /// <summary>
        /// 不可见
        /// </summary>
        public virtual bool Invisible { get => Get<bool>(); set => Set(value); }
        /// <summary>
        /// whether the field is inherited (_inherits)
        /// </summary>
        public virtual bool Inherited { get => Get<bool>(); set => Set(value); }

        /// <summary>
        /// 数据类型
        /// </summary>
        public virtual DataType DataType { get; } = DataType.None;
        public virtual int Length { get; }
        public virtual int DecimalDigits { get; }

        string[] _depends;
        public string[] Depends { get => _depends ??= Array.Empty<string>(); set => _depends = value; }
        Set<string> _dependsContext;
        public Set<string> DependsContext => _dependsContext ??= new Set<string>();



        protected internal virtual void Set(Self records, object value)
        {
            records.Env.Cache.Set(records, this, value);
        }

        protected internal virtual object Get(Self records)
        {
            return records.Env.Cache.Get(records, this, null);
        }
    }
}

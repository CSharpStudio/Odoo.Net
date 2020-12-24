using Odoo.Net.Core;

namespace Odoo.Net
{
    /// <summary>
    /// 模型声明
    /// </summary>
    public class ModelAttribute : MergableAttribute
    {
        /// <summary>
        /// 模型名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string Table { get; set; }
        /// <summary>
        /// 继承的模型名称，当Inherit等于Name时表示扩展
        /// </summary>
        public string Inherit { get; set; }
        /// <summary>
        /// 继承的多个控制器名称
        /// </summary>
        public string[] Inherits { get; set; }
        /// <summary>
        /// 是否抽象
        /// </summary>
        public bool IsAbstract { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get => Get<string>(); set => Set(value); }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get => Get<string>("Name"); set => Set(value); }
        /// <summary>
        /// 翻译
        /// </summary>
        public bool Transient { get => Get<bool>(); set => Set(value); }
        /// <summary>
        /// 信息
        /// </summary>
        public string Info { get => Get<string>(); set => Set(value); }
        /// <summary>
        /// 排序
        /// </summary>
        public string Order { get => Get<string>(); set => Set(value); }
        /// <summary>
        /// 记录修改人修改时间
        /// </summary>
        public bool LogAccess { get => Get<bool>(true); set => Set(value); }
        /// <summary>
        /// 构建<see cref="ModelAttribute"/>
        /// </summary>
        public ModelAttribute() { }

        /// <summary>
        /// 构建<see cref="ModelAttribute"/>
        /// </summary>
        /// <param name="name">模型名称</param>
        public ModelAttribute(string name)
        {
            Name = name;
        }
    }
}

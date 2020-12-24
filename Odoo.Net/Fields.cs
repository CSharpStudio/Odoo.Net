using Odoo.Net.Data;
using System;
using System.Collections.Generic;

namespace Odoo.Net
{
    public static class Fields
    {
        public static BooleanField Boolean(string caption = null, string help = null, bool? required = null, bool? readOnly = null, bool? index = null, Func<Self, object> defaultProvider = null, object defaultValue = null, string groups = null, Action compute = null, Action inverse = null, Action search = null, bool? store = null, bool? computeSudo = null, string groupOperator = null, bool? prefetch = null, string related = null, bool? invisible = null)
        {
            return new BooleanField().Init(caption, help, required, readOnly, index, defaultProvider, defaultValue, groups, compute, inverse, search, store, computeSudo, groupOperator, prefetch, related, invisible);
        }
        public static IntegerField Integer(string caption = null, string help = null, bool? required = null, bool? readOnly = null, bool? index = null, Func<Self, object> defaultProvider = null, object defaultValue = null, string groups = null, Action compute = null, Action inverse = null, Action search = null, bool? store = null, bool? computeSudo = null, string groupOperator = null, bool? prefetch = null, string related = null, bool? invisible = null)
        {
            return new IntegerField().Init(caption, help, required, readOnly, index, defaultProvider, defaultValue, groups, compute, inverse, search, store, computeSudo, groupOperator, prefetch, related, invisible);
        }
        public static FloatField Float(string caption = null, (int total, int precision)? digits = null, string help = null, bool? required = null, bool? readOnly = null, bool? index = null, Func<Self, object> defaultProvider = null, object defaultValue = null, string groups = null, Action compute = null, Action inverse = null, Action search = null, bool? store = null, bool? computeSudo = null, string groupOperator = null, bool? prefetch = null, string related = null, bool? invisible = null)
        {
            return new FloatField().Init(caption, help, required, readOnly, index, defaultProvider, defaultValue, groups, compute, inverse, search, store, computeSudo, groupOperator, prefetch, related, invisible)
                .SetIf(digits, nameof(FloatField.Digits));
        }
        public static MonetaryField Monetary(string caption = null, string currencyField = null, string help = null, bool? required = null, bool? readOnly = null, bool? index = null, Func<Self, object> defaultProvider = null, object defaultValue = null, string groups = null, Action compute = null, Action inverse = null, Action search = null, bool? store = null, bool? computeSudo = null, string groupOperator = null, bool? prefetch = null, string related = null, bool? invisible = null)
        {
            return new MonetaryField().Init(caption, help, required, readOnly, index, defaultProvider, defaultValue, groups, compute, inverse, search, store, computeSudo, groupOperator, prefetch, related, invisible)
                .SetIf(currencyField, nameof(MonetaryField.CurrencyField));
        }
        public static CharField Char(string caption = null, string help = null, bool? required = null, bool? readOnly = null, bool? index = null, Func<Self, object> defaultProvider = null, object defaultValue = null, string groups = null, Action compute = null, Action inverse = null, Action search = null, bool? store = null, bool? computeSudo = null, string groupOperator = null, bool? prefetch = null, string related = null, bool? invisible = null, int? size = null, bool? trim = null, bool? translate = null)
        {
            return new CharField().Init(caption, help, required, readOnly, index, defaultProvider, defaultValue, groups, compute, inverse, search, store, computeSudo, groupOperator, prefetch, related, invisible)
                .SetIf(size, nameof(CharField.Size))
                .SetIf(trim, nameof(CharField.Trim))
                .SetIf(translate, nameof(CharField.Translate));
        }
        public static TextField Text(string caption = null, string help = null, bool? required = null, bool? readOnly = null, bool? index = null, Func<Self, object> defaultProvider = null, object defaultValue = null, string groups = null, Action compute = null, Action inverse = null, Action search = null, bool? store = null, bool? computeSudo = null, string groupOperator = null, bool? prefetch = null, string related = null, bool? invisible = null, bool? translate = null)
        {
            return new TextField().Init(caption, help, required, readOnly, index, defaultProvider, defaultValue, groups, compute, inverse, search, store, computeSudo, groupOperator, prefetch, related, invisible)
                .SetIf(translate, nameof(TextField.Translate));
        }
        public static HtmlField Html(string caption = null, string help = null, bool? required = null, bool? readOnly = null, bool? index = null, Func<Self, object> defaultProvider = null, object defaultValue = null, string groups = null, Action compute = null, Action inverse = null, Action search = null, bool? store = null, bool? computeSudo = null, string groupOperator = null, bool? prefetch = null, string related = null, bool? invisible = null, bool? sanitize = null, bool? sanitizeTags = null, bool? sanitizeAttributes = null, bool? sanitizeStyle = null, bool? stripStyle = null, bool? stripClasses = null)
        {
            return new HtmlField().Init(caption, help, required, readOnly, index, defaultProvider, defaultValue, groups, compute, inverse, search, store, computeSudo, groupOperator, prefetch, related, invisible)
                .SetIf(sanitize, nameof(HtmlField.Sanitize))
                .SetIf(sanitizeTags, nameof(HtmlField.SanitizeTags))
                .SetIf(sanitizeAttributes, nameof(HtmlField.SanitizeAttributes))
                .SetIf(sanitizeStyle, nameof(HtmlField.SanitizeStyle))
                .SetIf(stripStyle, nameof(HtmlField.StripStyle))
                .SetIf(stripClasses, nameof(HtmlField.StripClasses));
        }
        public static DateTimeField DateTime(string caption = null, string help = null, bool? required = null, bool? readOnly = null, bool? index = null, Func<Self, object> defaultProvider = null, object defaultValue = null, string groups = null, Action compute = null, Action inverse = null, Action search = null, bool? store = null, bool? computeSudo = null, string groupOperator = null, bool? prefetch = null, string related = null, bool? invisible = null)
        {
            return new DateTimeField().Init(caption, help, required, readOnly, index, defaultProvider, defaultValue, groups, compute, inverse, search, store, computeSudo, groupOperator, prefetch, related, invisible);
        }
        public static DateField Date(string caption = null, string help = null, bool? required = null, bool? readOnly = null, bool? index = null, Func<Self, object> defaultProvider = null, object defaultValue = null, string groups = null, Action compute = null, Action inverse = null, Action search = null, bool? store = null, bool? computeSudo = null, string groupOperator = null, bool? prefetch = null, string related = null, bool? invisible = null)
        {
            return new DateField().Init(caption, help, required, readOnly, index, defaultProvider, defaultValue, groups, compute, inverse, search, store, computeSudo, groupOperator, prefetch, related, invisible);
        }
        public static BinaryField Binary(string caption = null, string help = null, bool? required = null, bool? readOnly = null, bool? index = null, Func<Self, object> defaultProvider = null, object defaultValue = null, string groups = null, Action compute = null, Action inverse = null, Action search = null, bool? store = null, bool? computeSudo = null, string groupOperator = null, bool? prefetch = null, string related = null, bool? invisible = null, bool? attachemet = null)
        {
            return new BinaryField().Init(caption, help, required, readOnly, index, defaultProvider, defaultValue, groups, compute, inverse, search, store, computeSudo, groupOperator, prefetch, related, invisible)
                .SetIf(attachemet, nameof(BinaryField.Attachment));
        }
        public static ImageField Image(string caption = null, string help = null, bool? required = null, bool? readOnly = null, bool? index = null, Func<Self, object> defaultProvider = null, object defaultValue = null, string groups = null, Action compute = null, Action inverse = null, Action search = null, bool? store = null, bool? computeSudo = null, string groupOperator = null, bool? prefetch = null, string related = null, bool? invisible = null, bool? attachemet = null, int? maxWidth = null, int? maxHeight = null)
        {
            return new ImageField().Init(caption, help, required, readOnly, index, defaultProvider, defaultValue, groups, compute, inverse, search, store, computeSudo, groupOperator, prefetch, related, invisible)
                .SetIf(attachemet, nameof(ImageField.Attachment))
                .SetIf(maxWidth, nameof(ImageField.MaxWidth))
                .SetIf(maxHeight, nameof(ImageField.MaxHeight));
        }
        public static SelectionField Selection(IEnumerable<(string key, string value)> selection = null, string caption = null, string help = null, bool? required = null, bool? readOnly = null, bool? index = null, Func<Self, object> defaultProvider = null, object defaultValue = null, string groups = null, Action compute = null, Action inverse = null, Action search = null, bool? store = null, bool? computeSudo = null, string groupOperator = null, bool? prefetch = null, string related = null, bool? invisible = null, bool? validate = null)
        {
            return new SelectionField().Init(caption, help, required, readOnly, index, defaultProvider, defaultValue, groups, compute, inverse, search, store, computeSudo, groupOperator, prefetch, related, invisible)
                .SetIf(selection, nameof(SelectionField.Selection))
                .SetIf(validate, nameof(SelectionField.Validate));
        }
        public static ReferenceField Reference(IEnumerable<(string key, string value)> selection = null, string caption = null, string help = null, bool? required = null, bool? readOnly = null, bool? index = null, Func<Self, object> defaultProvider = null, object defaultValue = null, string groups = null, Action compute = null, Action inverse = null, Action search = null, bool? store = null, bool? computeSudo = null, string groupOperator = null, bool? prefetch = null, string related = null, bool? invisible = null, bool? validate = null, string selectionMethod = null)
        {
            return new ReferenceField().Init(caption, help, required, readOnly, index, defaultProvider, defaultValue, groups, compute, inverse, search, store, computeSudo, groupOperator, prefetch, related, invisible)
                .SetIf(selection, nameof(ReferenceField.Selection))
                .SetIf(validate, nameof(ReferenceField.Validate))
                .SetIf(selectionMethod, nameof(ReferenceField.SelectionMethod));
        }
        public static Many2OneField Many2One(string comodel = null, string caption = null, string help = null, bool? required = null, bool? readOnly = null, bool? index = null, Func<Self, object> defaultProvider = null, object defaultValue = null, string groups = null, Action compute = null, Action inverse = null, Action search = null, bool? store = null, bool? computeSudo = null, string groupOperator = null, bool? prefetch = null, string related = null, bool? invisible = null, bool? autoJoin = null, Ondelete? ondelete = null)
        {
            return new Many2OneField().Init(caption, help, required, readOnly, index, defaultProvider, defaultValue, groups, compute, inverse, search, store, computeSudo, groupOperator, prefetch, related, invisible)
                .SetIf(comodel, nameof(Many2OneField.Comodel))
                .SetIf(autoJoin, nameof(Many2OneField.AutoJoin))
                .SetIf(ondelete, nameof(Many2OneField.Ondelete));
        }
        public static Many2OneReferenceField Many2OneReference(string caption = null, string help = null, bool? required = null, bool? readOnly = null, bool? index = null, Func<Self, object> defaultProvider = null, object defaultValue = null, string groups = null, Action compute = null, Action inverse = null, Action search = null, bool? store = null, bool? computeSudo = null, string groupOperator = null, bool? prefetch = null, string related = null, bool? invisible = null, string modelField = null)
        {
            return new Many2OneReferenceField().Init(caption, help, required, readOnly, index, defaultProvider, defaultValue, groups, compute, inverse, search, store, computeSudo, groupOperator, prefetch, related, invisible)
                .SetIf(modelField, nameof(Many2OneReferenceField.ModelField));
        }
        public static One2ManyField One2Many(string comodel = null, string inverseName = null, string caption = null, string help = null, bool? required = null, bool? readOnly = null, bool? index = null, Func<Self, object> defaultProvider = null, object defaultValue = null, string groups = null, Action compute = null, Action inverse = null, Action search = null, bool? store = null, bool? computeSudo = null, string groupOperator = null, bool? prefetch = null, string related = null, bool? invisible = null, bool? autoJoin = null, int? limit = null, bool? copy = null)
        {
            return new One2ManyField().Init(caption, help, required, readOnly, index, defaultProvider, defaultValue, groups, compute, inverse, search, store, computeSudo, groupOperator, prefetch, related, invisible)
                .SetIf(comodel, nameof(One2ManyField.Comodel))
                .SetIf(inverseName, nameof(One2ManyField.InverseName))
                .SetIf(autoJoin, nameof(One2ManyField.AutoJoin))
                .SetIf(limit, nameof(One2ManyField.Limit))
                .SetIf(copy, nameof(One2ManyField.Copy));
        }
        public static Many2ManyField Many2Many(string comodel = null, string relation = null, string column1 = null, string column2 = null, string caption = null, string help = null, bool? required = null, bool? readOnly = null, bool? index = null, Func<Self, object> defaultProvider = null, object defaultValue = null, string groups = null, Action compute = null, Action inverse = null, Action search = null, bool? store = null, bool? computeSudo = null, string groupOperator = null, bool? prefetch = null, string related = null, bool? invisible = null, bool? autoJoin = null, int? limit = null, Ondelete? ondelete = null)
        {
            return new Many2ManyField().Init(caption, help, required, readOnly, index, defaultProvider, defaultValue, groups, compute, inverse, search, store, computeSudo, groupOperator, prefetch, related, invisible)
                .SetIf(comodel, nameof(Many2ManyField.Comodel))
                .SetIf(relation, nameof(Many2ManyField.Relation))
                .SetIf(column1, nameof(Many2ManyField.Column1))
                .SetIf(column2, nameof(Many2ManyField.Column2))
                .SetIf(autoJoin, nameof(Many2ManyField.AutoJoin))
                .SetIf(ondelete, nameof(Many2ManyField.Ondelete));
        }

        static T Init<T>(this T field, string caption = null, string help = null, bool? required = null, bool? readOnly = null, bool? index = null, Func<Self, object> defaultProvider = null, object defaultValue = null, string groups = null, Action compute = null, Action inverse = null, Action search = null, bool? store = null, bool? computeSudo = null, string groupOperator = null, bool? prefetch = null, string related = null, bool? invisible = null) where T : Field
        {
            field.SetIf(caption, nameof(field.Caption));
            field.SetIf(help, nameof(field.Help));
            field.SetIf(required, nameof(field.Required));
            field.SetIf(readOnly, nameof(field.Readonly));
            field.SetIf(index, nameof(field.Index));
            field.SetIf(defaultProvider, nameof(field.Default));
            field.SetIf(defaultValue, nameof(field.DefaultValue));
            field.SetIf(groups, nameof(field.Groups));
            field.SetIf(compute, nameof(field.Compute));
            field.SetIf(inverse, nameof(field.Inverse));
            field.SetIf(search, nameof(field.Search));
            field.SetIf(store, nameof(field.Store));
            field.SetIf(computeSudo, nameof(field.ComputeSudo));
            field.SetIf(groupOperator, nameof(field.GroupOperator));
            field.SetIf(prefetch, nameof(field.Prefetch));
            field.SetIf(related, nameof(field.Related));
            field.SetIf(invisible, nameof(field.Invisible));
            return field;
        }
        static T SetIf<T>(this T field, object value, string property) where T : Field
        {
            if (value != null)
                field.Set(value, property);
            return field;
        }
    }

    public class BooleanField : Field
    {
        public override DataType DataType => DataType.Boolean;
    }
    public class IntegerField : Field
    {
        public override DataType DataType => DataType.Int32;
        public override string GroupOperator { get => Get<string>("sum"); set => Set(value); }
    }
    public class FloatField : Field
    {
        public override DataType DataType => DataType.Double;
        public (int? total, int? precision) Digits { get => Get<(int? total, int? precision)>(); set => Set(value); }
        public override int DecimalDigits => Digits.precision ?? 0;
        public override int Length => Digits.total ?? 16;
    }
    public class MonetaryField : Field
    {
        public override DataType DataType => DataType.Double;

        public override string GroupOperator { get => Get<string>("sum"); set => Set(value); }

        public string CurrencyField { get => Get<string>(); set => Set(value); }
    }
    public abstract class StringField : Field
    {
        public bool Translate { get => Get<bool>(); set => Set(value); }
    }
    public class CharField : StringField
    {
        public override DataType DataType => DataType.String;
        public int Size { get => Get<int>(); set => Set(value); }
        public bool Trim { get => Get<bool>(true); set => Set(value); }
        public override int Length => Size;
    }
    public class TextField : StringField
    {
        public override DataType DataType => DataType.Text;
    }
    public class HtmlField : StringField
    {
        public override DataType DataType => DataType.Text;
        /// <summary>
        /// 无注入处理：whether value must be sanitized
        /// </summary>
        public bool Sanitize { get => Get<bool>(true); set => Set(value); }
        /// <summary>
        /// 是否处理标签：whether to sanitize tags (only a white list of attributes is accepted)
        /// </summary>
        public bool SanitizeTags { get => Get<bool>(true); set => Set(value); }
        /// <summary>
        /// 是否处理特性：whether to sanitize attributes (only a white list of attributes is accepted)
        /// </summary>
        public bool SanitizeAttributes { get => Get<bool>(true); set => Set(value); }
        /// <summary>
        /// 是否处理样式：whether to sanitize style attributes
        /// </summary>
        public bool SanitizeStyle { get => Get<bool>(false); set => Set(value); }
        /// <summary>
        /// 是否去除样式：whether to strip style attributes (removed and therefore not sanitized)
        /// </summary>
        public bool StripStyle { get => Get<bool>(false); set => Set(value); }
        /// <summary>
        /// 是否去除类选择器：whether to strip classes attributes
        /// </summary>
        public bool StripClasses { get => Get<bool>(false); set => Set(value); }
    }

    public class DateTimeField : Field
    {
        public override DataType DataType => DataType.DateTime;
    }
    public class DateField : Field
    {
        public override DataType DataType => DataType.Date;
    }
    public class BinaryField : Field
    {
        public override bool Prefetch { get => Get<bool>(false); set => Set(value); }
        /// <summary>
        /// 是否存为附件：whether value is stored in attachment
        /// </summary>
        public bool Attachment { get => Get<bool>(true); set => Set(value); }
    }
    public class ImageField : BinaryField
    {
        /// <summary>
        /// 最大宽
        /// </summary>
        public int MaxWidth { get => Get<int>(); set => Set(value); }
        /// <summary>
        /// 最大高
        /// </summary>
        public int MaxHeight { get => Get<int>(); set => Set(value); }
    }
    public class SelectionField : Field
    {
        public IEnumerable<(string key, string value)> Selection { get => Get<IEnumerable<(string key, string value)>>(); set => Set(value); }

        /// <summary>
        /// whether validating upon write
        /// </summary>
        public bool Validate { get => Get<bool>(true); set => Set(value); }
    }
    public class ReferenceField : SelectionField
    {
        /// <summary>
        /// 选择方法
        /// </summary>
        public string SelectionMethod { get => Get<string>(); set => Set(value); }
    }
    public abstract class RelationalField : Field
    {
        /// <summary>
        /// domain for searching values
        /// </summary>
        public virtual string Domain { get => Get<string>(); set => Set(value); }
        /// <summary>
        /// 是否验证公司
        /// </summary>
        public virtual bool CheckCompany { get => Get<bool>(); set => Set(value); }
    }
    public class Many2OneField : RelationalField
    {
        /// <summary>
        /// 目标模型名称：name of the target model (string)
        /// </summary>
        public string Comodel { get => Get<string>(); set => Set(value); }
        /// <summary>
        /// 删除处理：what to do when the referred record is deleted;
        /// </summary>
        public Ondelete Ondelete { get => Get<Ondelete>(); set => Set(value); }
        /// <summary>
        /// whether joins are generated upon search
        /// </summary>
        public bool AutoJoin { get => Get<bool>(); set => Set(value); }
        /// <summary>
        /// whether self implements delegation
        /// </summary>
        public bool Delegate { get => Get<bool>(); set => Set(value); }
    }
    public class Many2OneReferenceField : Field
    {
        public string ModelField { get => Get<string>(); set => Set(value); }
    }
    /// <summary>
    /// 删除处理方式
    /// </summary>
    public enum Ondelete
    {
        /// <summary>
        /// 设为null
        /// </summary>
        SetNull,
        /// <summary>
        /// 限制
        /// </summary>
        Restrict,
        /// <summary>
        /// 级联
        /// </summary>
        Cascade
    }
    public class RelationalMutil : RelationalField
    {

    }
    public class One2ManyField : RelationalMutil
    {
        /// <summary>
        /// 目标模型名称：name of the target model (string)
        /// </summary>
        public string Comodel { get => Get<string>(); set => Set(value); }
        /// <summary>
        /// 逆关联名称：name of the inverse ``Many2one`` field in
        /// ``comodel_name`` (string)
        /// </summary>
        public string InverseName { get => Get<string>(); set => Set(value); }
        /// <summary>
        /// 限制数量：optional limit to use upon read (integer)
        /// </summary>
        public int? Limit { get => Get<int?>(); set => Set(value); }
        /// <summary>
        /// whether joins are generated upon search
        /// </summary>
        public bool AutoJoin { get => Get<bool>(); set => Set(value); }
        /// <summary>
        /// o2m are not copied by default
        /// </summary>
        public override bool Copy { get => Get<bool>(false); set => Set(value); }
    }
    public class Many2ManyField : RelationalMutil
    {
        /// <summary>
        /// 目标模型名称
        /// </summary>
        public string Comodel { get => Get<string>(); set => Set(value); }
        /// <summary>
        /// 限制数量：optional limit to use upon read (integer)
        /// </summary>
        public int? Limit { get => Get<int?>(); set => Set(value); }
        /// <summary>
        /// 关系表：optional name of the table that stores the relation in
        /// the database(string)
        /// </summary>
        public string Relation { get => Get<string>(); set => Set(value); }
        /// <summary>
        /// 列1：optional name of the column referring to "these" records
        /// in the table ``relation`` (string)
        /// </summary>
        public string Column1 { get => Get<string>(); set => Set(value); }
        /// <summary>
        /// 列2：optional name of the column referring to "those" records
        /// in the table ``relation`` (string)
        /// </summary>
        public string Column2 { get => Get<string>(); set => Set(value); }
        /// <summary>
        /// whether joins are generated upon search
        /// </summary>
        public bool AutoJoin { get => Get<bool>(); set => Set(value); }
        /// <summary>
        /// 删除处理：what to do when the referred record is deleted;不支持SetNull
        /// </summary>
        public Ondelete Ondelete { get => Get<Ondelete>(); set { if (value == Ondelete.SetNull) throw new NotSupportedException("多对多关联删除不支持SetNull"); Set(value); } }
    }
    public class IdField : Field
    {
        public IdField(string name, Type type, Type declaringType)
        {
            Name = name;
        }
        public override DataType DataType => DataType.String;
        public override int Length => 16;
    }
}

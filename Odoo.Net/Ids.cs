using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace Odoo.Net
{
    /// <summary>
    /// 只读的id集合
    /// </summary>
    [TypeConverter(typeof(IdsConverter))]
    public class Ids : IReadOnlyList<string>
    {
        readonly List<string> values;
        public Ids()
        {
            values = new List<string>();
        }
        public Ids(IEnumerable<string> collection)
        {
            values = new List<string>(collection);
        }

        public string this[int index]
        {
            get
            {
                if (index < 0 || index >= values.Count)
#pragma warning disable S112 // General exceptions should never be thrown
                    throw new IndexOutOfRangeException($"Ids[{values.Count - 1}], 索引[{index}]超出范围");
#pragma warning restore S112 // General exceptions should never be thrown
                return values[index];
            }
        }

        public int Count => values.Count;

        public IEnumerator<string> GetEnumerator()
        {
            return values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return values.GetEnumerator();
        }

        public static implicit operator Ids(string id)
        {
            return new Ids(new[] { id });
        }

        public static implicit operator Ids(string[] ids)
        {
            return new Ids(ids);
        }

        public static implicit operator Ids(List<string> ids)
        {
            return new Ids(ids);
        }
    }

    class IdsConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || typeof(IEnumerable<string>).IsAssignableFrom(sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string str)
                return new Ids(new[] { str });
            if (value is IEnumerable<string> en)
                return new Ids(en);
            return base.ConvertFrom(context, culture, value);
        }
    }
}

using System.ComponentModel;
using System.Globalization;

namespace System.Collections.Generic
{
    /// <summary>
    /// 列表集合, 使用<see cref="List{T}"/>实现。<br/>
    /// 1.支持单值隐式转换:<code>Set&lt;string&gt; set = "str"</code>
    /// 2.支持列表隐式转换:<code>Set&lt;string&gt; set = new List&lt;string&gt;{"abc"}</code>
    /// 3.支持数组隐式转换:<code>Set&lt;string&gt; set = new []{"abc"}</code>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [TypeConverter(typeof(MapConverter))]
    public class Set<T> : IList<T>
    {
        readonly List<T> _set;
        /// <summary>
        /// 构建<see cref="Set{T}"/>
        /// </summary>
        public Set()
        {
            _set = new List<T>();
        }
        /// <summary>
        /// 构建<see cref="Set{T}"/>
        /// </summary>
        /// <param name="collection">集合</param>
        public Set(IEnumerable<T> collection)
        {
            _set = new List<T>(collection);
        }
        /// <summary>
        /// 构建<see cref="Set{T}"/>
        /// </summary>
        /// <param name="one"></param>
        public Set(T one)
        {
            _set = new List<T> { one };
        }
        /// <summary>
        /// 单值转为<see cref="Set{T}"/>
        /// </summary>
        /// <param name="one"></param>
        public static implicit operator Set<T>(T one)
        {
            return new Set<T>(one);
        }
        /// <summary>
        /// 列表转为<see cref="Set{T}"/>
        /// </summary>
        /// <param name="list"></param>
        public static implicit operator Set<T>(List<T> list)
        {
            return new Set<T>(list);
        }
        /// <summary>
        /// 数组转为<see cref="Set{T}"/>
        /// </summary>
        /// <param name="list"></param>
        public static implicit operator Set<T>(T[] list)
        {
            return new Set<T>(list);
        }

        public T this[int index] { get => _set[index]; set => _set[index] = value; }
        public int Count => _set.Count;
        public bool IsReadOnly => false;
        public void Add(T item) => _set.Add(item);
        public void AddRange(IEnumerable<T> items) => _set.AddRange(items);
        public void Clear() => _set.Clear();
        public bool Contains(T item) => _set.Contains(item);
        public void CopyTo(T[] array, int arrayIndex) => _set.CopyTo(array, arrayIndex);
        public IEnumerator<T> GetEnumerator() => _set.GetEnumerator();
        public int IndexOf(T item) => _set.IndexOf(item);
        public void Insert(int index, T item) => _set.Insert(index, item);
        public bool Remove(T item) => _set.Remove(item);
        public void RemoveAt(int index) => _set.RemoveAt(index);
        IEnumerator IEnumerable.GetEnumerator() => _set.GetEnumerator();
    }

    class MapConverter : TypeConverter
    {
        readonly Type targetType;
        public MapConverter(Type type)
        {
            targetType = type;
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            var innerType = targetType.GetGenericArguments()[0];
            if (innerType.IsAssignableFrom(sourceType))
                return true;
            if (sourceType.IsArray)
            {
                var elementType = sourceType.GetElementType();
                if (innerType.IsAssignableFrom(elementType))
                    return true;
            }
            foreach (Type @interface in sourceType.GetInterfaces())
            {
                if (@interface.IsGenericType && @interface.GetGenericTypeDefinition() == typeof(ICollection<>))
                {
                    var genType = @interface.GetGenericArguments()[0];
                    if (innerType.IsAssignableFrom(genType))
                        return true;
                }
            }
            return false;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return Activator.CreateInstance(targetType, value);
        }
    }
}

using System.Collections.Generic;
using System.Linq;

namespace System
{
    /// <summary>
    /// 集合扩展方法
    /// </summary>
    public static class IEnumerableExtension
    {
        /// <summary>
        /// 检查集合是null或<see cref="ICollection{T}.Count"/>为0
        /// </summary>
        public static bool IsNullOrEmpty<T>(this ICollection<T> source)
        {
            return source == null || source.Count <= 0;
        }

        /// <summary>
        /// 检查集合不是null且<see cref="ICollection{T}.Count"/>大于0
        /// </summary>
        public static bool IsNotEmpty<T>(this ICollection<T> source)
        {
            return source != null && source.Count > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="collection"></param>
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> collection)
        {
            foreach (var item in collection)
                list.Add(item);
        }

        /// <summary>
        /// 转换为一个只读的集合。
        /// </summary>
        /// <typeparam name="T">集合项类型参数</typeparam>
        /// <param name="orignalCollections">原集合</param>
        /// <returns></returns>
        public static IList<T> AsReadOnly<T>(this IList<T> orignalCollections)
        {
            if (orignalCollections == null) throw new ArgumentNullException("orignalCollections");

            return new System.Collections.ObjectModel.ReadOnlyCollection<T>(orignalCollections);
        }

        /// <summary>
        /// 循环集合每个项目执行指定动作
        /// </summary>
        /// <typeparam name="T">集合项类型参数</typeparam>
        /// <param name="e">集合</param>
        /// <param name="action">每项执行的处理动作</param>
        public static void ForEach<T>(this IEnumerable<T> e, Action<T> action)
        {
            foreach (var i in e)
                action(i);
        }

        /// <summary>
        /// 过滤集合的重复项目
        /// </summary>
        /// <typeparam name="T">集合项目类型参数</typeparam>
        /// <param name="source">集合</param>
        /// <param name="comparer">比较器</param>
        /// <returns></returns>
        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> source, Func<T, T, bool> comparer)
        {
            return source.Distinct(new DistinctComparer<T>(comparer));
        }

        /// <summary>
        /// 使用指定的keySelector从<see cref="IEnumerable{TSource}"/>创建<see cref="Dictionary{TKey, TSource}"/>.
        /// 当主键重复时，可以指定抛出异常，或者取最后一个键的值
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="throwOnKeyDuplicate"></param>
        /// <returns></returns>
        public static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, bool throwOnKeyDuplicate)
        {
            try
            {
                return source.ToDictionary(keySelector);
            }
            catch (ArgumentException exc)
            {
                if (throwOnKeyDuplicate)
                {
                    var keys = source.GroupBy(keySelector).Where(p => p.Count() > 1).Select(p => p.Key.ToString()).Join(",");
                    throw new ArgumentException($"ToDictionary存在重复的Key[{keys}]", exc);
                }
                var result = new Dictionary<TKey, TSource>();
                foreach (var s in source)
                    result[keySelector(s)] = s;
                return result;
            }
        }
        /// <summary>
        /// 使用指定的keySelector和elementSelector从<see cref="IEnumerable{TSource}"/>创建<see cref="Dictionary{TKey, TElement}"/>.
        /// 当主键重复时，可以指定抛出异常，或者取最后一个键的值
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="elementSelector"></param>
        /// <param name="throwOnKeyDuplicate"></param>
        /// <returns></returns>
        public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, bool throwOnKeyDuplicate)
        {
            try
            {
                return source.ToDictionary(keySelector, elementSelector);
            }
            catch (ArgumentException exc)
            {
                if (throwOnKeyDuplicate)
                {
                    var keys = source.GroupBy(keySelector).Where(p => p.Count() > 1).Select(p => p.Key.ToString()).Join(",");
                    throw new ArgumentException($"ToDictionary存在重复的Key[{keys}]", exc);
                }
                var result = new Dictionary<TKey, TElement>();
                foreach (var s in source)
                    result[keySelector(s)] = elementSelector(s);
                return result;
            }
        }

        /// <summary>
        /// <see cref="IDictionary{TKey, TValue}"/>获取值或者默认值
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="keyValues"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TValue GetValueOr<TKey, TValue>(this IDictionary<TKey, TValue> keyValues, TKey key, TValue defaultValue = default)
        {
            keyValues.NotNull(nameof(keyValues));
            if (keyValues.TryGetValue(key, out TValue result))
                return result;
            return defaultValue;
        }

        /// <summary>
        ///  <see cref="IDictionary{object, object}"/>获取值或者默认值
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="keyValues"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TValue GetValueOr<TValue>(this IDictionary<object, object> keyValues, object key, TValue defaultValue = default)
        {
            keyValues.NotNull(nameof(keyValues));
            if (keyValues.TryGetValue(key, out object result))
                return result.ConvertTo<TValue>(defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// <see cref="IDictionary{TKey, TValue}"/>获取值或者默认值
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="keyValues"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TValue GetValueOr<TKey, TValue>(this IDictionary<TKey, TValue> keyValues, TKey key, Func<TValue> defaultValue = null)
        {
            keyValues.NotNull(nameof(keyValues));
            if (keyValues.TryGetValue(key, out TValue result))
                return result;
            if (defaultValue != null)
                return defaultValue();
            return default;
        }

        /// <summary>
        /// 把values更新到字典
        /// </summary>
        /// <param name="values"></param>
        public static void Update<TKey, TValue>(this IDictionary<TKey, TValue> source, IDictionary<TKey, TValue> values)
        {
            foreach (var kv in values)
                source[kv.Key] = kv.Value;
        }

        public static T Get<T>(this Map ctx, string key, T @default = default)
        {
            return ctx[key].ConvertTo<T>(@default);
        }
        public static string GetString(this Map ctx, string key, string @default = null)
        {
            return ctx[key].ConvertTo(@default);
        }
        public static int GetInt32(this Map ctx, string key, int @default = 0)
        {
            return ctx[key].ConvertTo(@default);
        }
        public static bool GetBool(this Map ctx, string key, bool @default = false)
        {
            return ctx[key].ConvertTo(@default);
        }

    }

    class DistinctComparer<T> : IEqualityComparer<T>
    {
        readonly Func<T, T, bool> comparer;

        public DistinctComparer(Func<T, T, bool> comparer)
        {
            this.comparer = comparer;
        }

        public bool Equals(T x, T y)
        {
            return comparer(x, y);
        }

        public int GetHashCode(T obj)
        {
            return 0;
        }
    }
}

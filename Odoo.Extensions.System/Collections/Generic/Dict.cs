namespace System.Collections.Generic
{
    /// <summary>
    /// 字典，内部使用<see cref="Dictionary{TKey,TValue}"/>实现。
    /// 索引器使用不存在的key时返回默认值，而不会抛<see cref="KeyNotFoundException"/>
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    [Serializable]
    public class Dict<TKey, TValue> : IDictionary<TKey, TValue>
    {
        readonly Dictionary<TKey, TValue> _dict;
        /// <summary>
        /// Initializes a new instance of the <see cref="Dict{TKey,TValue}"/> class
        /// that is empty, has the default initial capacity, and uses the default equality
        /// comparer for the key type.
        /// </summary>
        public Dict() { _dict = new Dictionary<TKey, TValue>(); }
        /// <summary>
        /// Initializes a new instance of the <see cref="Dict{TKey,TValue}"/> class
        /// that is empty, has the default initial capacity, and uses the specified <see cref="IEqualityComparer{TKey}"/>.
        /// </summary>
        /// <param name="comparer">The <see cref="IEqualityComparer{TKey}"/> implementation to use when
        /// comparing keys, or null to use the default <see cref="IEqualityComparer{TKey}"/> for the type of the key.
        /// </param>
        public Dict(IEqualityComparer<TKey> comparer) { _dict = new Dictionary<TKey, TValue>(comparer); }
        /// <summary>
        /// Initializes a new instance of the <see cref="Dict{TKey,TValue}"/> class
        /// that is empty, has the specified initial capacity, and uses the default equality
        /// comparer for the key type.
        /// </summary>
        /// <param name="capacity">The initial number of elements that the <see cref="Dict{TKey,TValue}"/> can contain.</param>
        /// <exception cref="ArgumentOutOfRangeException">capacity is less than 0.</exception>
        public Dict(int capacity) { _dict = new Dictionary<TKey, TValue>(capacity); }
        /// <summary>
        /// Initializes a new instance of the <see cref="Dict{TKey,TValue}"/> class
        /// that is empty, has the specified initial capacity, uses the specified <see cref="IEqualityComparer{TKey}"/>.
        /// </summary>
        /// <param name="capacity">The initial number of elements that the <see cref="Dict{TKey,TValue}"/> can contain.</param>
        /// <exception cref="ArgumentOutOfRangeException">capacity is less than 0.</exception>
        public Dict(int capacity, IEqualityComparer<TKey> comparer) { _dict = new Dictionary<TKey, TValue>(capacity, comparer); }
        /// <summary>
        /// Initializes a new instance of the <see cref="Dict{TKey,TValue}"/> class
        /// that contains elements copied from the specified <see cref="IDictionary{TKey,TValue}"/>
        /// and uses the default equality comparer for the key type.
        /// </summary>
        /// <param name="dictionary">The <see cref="IDictionary{TKey,TValue}"/> whose elements are copied to the new <see cref="Dict{TKey,TValue}"/>.</param>
        /// <exception cref="ArgumentNullException">dictionary is null.</exception>
        /// <exception cref="ArgumentException">dictionary contains one or more duplicate keys.</exception>
        public Dict(IDictionary<TKey, TValue> dictionary) { _dict = new Dictionary<TKey, TValue>(dictionary); }
        /// <summary>
        /// Initializes a new instance of the <see cref="Dict{TKey,TValue}"/> class
        /// that contains elements copied from the specified <see cref="IDictionary{TKey,TValue}"/>
        /// and uses the specified <see cref="IEqualityComparer{TKey}"/>.
        /// </summary>
        /// <param name="dictionary">The <see cref="IDictionary{TKey,TValue}"/> whose elements are copied to the new <see cref="Dict{TKey,TValue}"/>.</param>
        /// <param name="comparer">The <see cref="IEqualityComparer{TKey}"/> implementation to use when
        /// comparing keys, or null to use the default <see cref="EqualityComparer{TKey}"/> for the type of the key.
        /// </param>
        /// <exception cref="ArgumentNullException">dictionary is null.</exception>
        /// <exception cref="ArgumentException">dictionary contains one or more duplicate keys.</exception>
        public Dict(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer) { _dict = new Dictionary<TKey, TValue>(dictionary, comparer); }
        /// <summary>
        /// Initializes a new instance of the <see cref="Dict{TKey,TValue}"/> class
        /// </summary>
        /// <param name="collection"></param>
        public Dict(IEnumerable<KeyValuePair<TKey, TValue>> collection) { _dict = new Dictionary<TKey, TValue>(collection); }
        /// <summary>
        /// Initializes a new instance of the <see cref="Dict{TKey,TValue}"/> class
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="comparer"></param>
        public Dict(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer) { _dict = new Dictionary<TKey, TValue>(collection, comparer); }

        public virtual TValue this[TKey key]
        {
            get
            {
                _dict.TryGetValue(key, out TValue value);
                return value;
            }
            set { _dict[key] = value; }
        }

        public ICollection<TKey> Keys => _dict.Keys;
        public ICollection<TValue> Values => _dict.Values;
        public int Count => _dict.Count;
        public void Add(TKey key, TValue value) => _dict.Add(key, value);
        public void Clear() => _dict.Clear();
        public bool ContainsKey(TKey key) => _dict.ContainsKey(key);
        public bool Remove(TKey key) => _dict.Remove(key);
        public bool TryGetValue(TKey key, out TValue value) => _dict.TryGetValue(key, out value);

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => false;
        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) => ((IDictionary<TKey, TValue>)_dict).Add(item);
        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) => ((IDictionary<TKey, TValue>)_dict).Contains(item);
        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => ((IDictionary<TKey, TValue>)_dict).CopyTo(array, arrayIndex);
        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() => _dict.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _dict.GetEnumerator();
        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) => ((IDictionary<TKey, TValue>)_dict).Remove(item);
    }
}

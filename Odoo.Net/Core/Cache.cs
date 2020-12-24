using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odoo.Net.Core
{
    /// <summary>
    /// 缓存
    /// </summary>
    public class Cache
    {
        readonly Dict<Field, Dict<string, object>> _data;
        public Cache() { _data = new Dict<Field, Dict<string, object>>(); }
        public Cache(int capacity) { _data = new Dict<Field, Dict<string, object>>(capacity); }

        public Dict<string, object> this[Field key, Self record]
        {
            get
            {
                if (!_data.TryGetValue(key, out Dict<string, object> value))
                {
                    value = new Dict<string, object>();
                    _data.Add(key, value);
                }
                return value;
            }
            set => _data[key] = value;
        }

        public object Get(Self record, Field field, object @default)
        {
            //if (_data.TryGetValue(field, out Dict<string, object> values) && values.TryGetValue(record.Ids[0], out object value))
            //{
            //    if (field.DependsContext.IsNullOrEmpty())
            //    {
            //        var dict = value as Dict<object, object>;
            //        if (dict != null && dict.TryGetValue(field.CacheKey(record.Env), out object v))
            //            return v;
            //        return @default;
            //    }
            //    return value;
            //}
            return @default;
        }

        public void Set(Self record, Field field, object value)
        {
            //record.NotNull(nameof(record));
            //field.NotNull(nameof(field));
            //if (record.Ids.IsNullOrEmpty())
            //    throw new ArgumentException($"模型[{record.Meta.Name}]Ids为空，不能设置值");
            //var id = record.Ids[0];
            //if (field.DependsContext.IsNotEmpty())
            //{
            //    var key = field.CacheKey(record.Env);
            //    var fieldCache = this[field];
            //    if (!(fieldCache.TryGetValue(id, out object obj) && obj is Dict<object, object> dict))
            //    {
            //        dict = new Dict<object, object>();
            //        fieldCache[id] = dict;
            //    }
            //    dict[key] = value;
            //}
            //else
            //    this[field][id] = value;
        }

        public void Update(Self records, Field field, Set<object> values)
        {
            //records.NotNull(nameof(records));
            //field.NotNull(nameof(field));
            //if (records.Ids.IsNullOrEmpty())
            //    throw new ArgumentException($"模型[{records.Meta.Name}]Ids为空，不能设置值");
            //if (field.DependsContext.IsNotEmpty())
            //{
            //    var key = field.CacheKey(records.Env);
            //    var fieldCache = this[field];
            //    foreach ((var recordId, var value) in records.Ids.Zip(values))
            //    {
            //        if (!(fieldCache.TryGetValue(recordId, out object obj) && obj is Dict<object, object> dict))
            //        {
            //            dict = new Dict<object, object>();
            //            fieldCache[recordId] = dict;
            //        }
            //        dict[key] = value;
            //    }
            //}
            //else
            //    this[field].Update(records.Ids.Zip(values).ToDictionary(p => p.First, p => p.Second));
        }

        public void Remove(Self record, Field field)
        {
            //record.NotNull(nameof(record));
            //field.NotNull(nameof(field));
            //if (record.Ids.IsNullOrEmpty())
            //    throw new ArgumentException($"模型[{record.Meta.Name}]Ids为空，不能设置值");
            //if (_data.TryGetValue(field, out Dict<string, object> values))
            //    values.Remove(record.Ids[0]);
        }

        public IEnumerable<object> GetValues(Self records, Field field)
        {
            //var fieldCache = this[field];
            //var key = field.DependsContext.IsNotEmpty() ? field.CacheKey(records.Env) : null;
            //foreach (var recordId in records.Ids)
            //{
            //    if (key != null)
            //    {
            //        if (fieldCache.TryGetValue(recordId, out object obj) && obj is Dict<object, object> dict && dict.TryGetValue(key, out object val))
            //            yield return val;
            //    }
            //    else if (fieldCache.TryGetValue(recordId, out object val))
            //        yield return val;
            //}
            throw new NotImplementedException();
        }
    }
}

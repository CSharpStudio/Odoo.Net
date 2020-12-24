using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Xml.Linq;

namespace System
{
    /// <summary>
    /// 常用扩展
    /// </summary>
    public static class SystemExtension
    {
        /// <summary>
        /// 把对象转换为指定类型，可转换<see cref="JObject"/>为指定类型
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="obj">原对象</param>
        /// <returns>转换成目标类型的对象</returns>
        public static T ConvertTo<T>(this object obj)
        {
            if (obj is T result)
                return result;
            return (T)ConvertTo(obj, typeof(T));
        }

        /// <summary>
        /// 把对象转为指定的类型，可转换<see cref="JObject"/>为指定类型
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="obj"></param>
        /// <param name="defaultValue">转换失败时的默认值</param>
        /// <param name="ignoreException">转换失败时是否忽略异常，默认为true</param>
        /// <returns>转换成目标类型的对象</returns>
        public static T ConvertTo<T>(this object obj, T defaultValue)
        {
            if (obj is T r)
                return r;
            if (TryConventTo(obj, typeof(T), out object result))
                return (T)result;
            return defaultValue;
        }

        /// <summary>
        /// 转换为指定类型，可转换<see cref="JObject"/>为指定类型
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="targetType">目标类型</param>
        /// <returns>转换成目标类型的对象</returns>
        public static object ConvertTo(this object obj, Type targetType)
        {
            if (TryConventTo(obj, targetType, out object result))
                return result;

            throw new InvalidOperationException("[{0}]不能转换成[{1}]".FormatArgs(obj.GetType().Name, targetType.Name));
        }

        /// <summary>
        /// 尝试转换为指定类型
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="targetType"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryConventTo(this object obj, Type targetType, out object result)
        {
            if (obj == null)
            {
                result = targetType.GetDefault();
                return targetType.IsNullable();
            }

            if (obj is Text.Json.JsonElement el)
            {
                result = el.ToObject(targetType);
                return true;
            }

            if (obj is JToken jToken)
            {
                var type = targetType;
                if (jToken is JObject jObject && jObject.TryGetValue("$type", StringComparison.OrdinalIgnoreCase, out JToken jtype))
                {
                    var declareType = Type.GetType((jtype as JValue).Value?.ToString());
                    if (declareType != null)
                        type = declareType;
                }
                result = JsonSerializer.Create(JsonConvert.DefaultSettings?.Invoke()).Deserialize(jToken.CreateReader(), type);
                return true;
            }

            if (obj is DBNull)
            {
                result = targetType.GetDefault();
                return true;
            }

            var sourceType = obj.GetType();
            if (targetType.IsAssignableFrom(sourceType))
            {
                result = obj;
                return true;
            }

            if (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(Nullable<>))
                targetType = Nullable.GetUnderlyingType(targetType);

            if (targetType.IsEnum)
            {
                result = Enum.Parse(targetType, obj.ToString(), true);
                return true;
            }

            if (targetType == typeof(bool))//对bool特殊处理
            {
                if ("1".Equals(obj))
                {
                    result = true;
                    return true;
                }
                if ("0".Equals(obj))
                {
                    result = false;
                    return true;
                }
            }
            //处理数字类型。（空字符串转换为数字 0）
            if ((targetType.IsPrimitive || targetType == typeof(decimal)) && obj is string str && str.IsNullOrEmpty())
            {
                result = 0;
                return true;
            }

            if (typeof(IConvertible).IsAssignableFrom(sourceType) &&
                typeof(IConvertible).IsAssignableFrom(targetType))
            {
                result = Convert.ChangeType(obj, targetType);
                return true;
            }

            var converter = ComponentModel.TypeDescriptor.GetConverter(obj);
            if (converter != null && converter.CanConvertTo(targetType))
            {
                result = converter.ConvertTo(obj, targetType);
                return true;
            }

            converter = ComponentModel.TypeDescriptor.GetConverter(targetType);
            if (converter != null && converter.CanConvertFrom(sourceType))
            {
                result = converter.ConvertFrom(obj);
                return true;
            }

            try
            {
                result = Convert.ChangeType(obj, targetType);
                return true;
            }
            catch (Exception exc)
            {
                Diagnostics.Trace.WriteLine($"Convert.ChangeType({targetType.Name}) 失败:" + exc.Message);
            }

            result = null;
            return false;
        }

        /// <summary>
        /// 返回输出SQL字符串，把命令的参数也格式化输出
        /// </summary>
        /// <param name="cmd">Db命令</param>
        /// <returns></returns>
        public static string ToTraceString(this IDbCommand cmd)
        {
            var content = cmd.CommandText;

            if (cmd.Parameters.Count > 0)
            {
                var pValues = cmd.Parameters.OfType<DbParameter>().Select(p =>
                {
                    var value = p.Value;
                    if (value is string)
                    {
                        value = "'{0}'".FormatArgs(value);
                    }
                    return p.ParameterName + "->" + value;
                });
                content += Environment.NewLine + pValues.Join(",");
            }
            return content;
        }

        /// <summary>
        /// 获取值，如果不存在指定键，返回T的默认值，调用<see cref="SystemExtension.ConvertTo{T}(object, T, bool)"/>
        /// 实现类型转换，可转换<see cref="JObject"/>为指定类型
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="hd">字典</param>
        /// <param name="key">键</param>
        /// <param name="defaultValue">字典找不到值，或者值类型转换失败时返回的默认值</param>
        /// <returns></returns>
        public static T GetValue<T>(this HybridDictionary hd, object key, T defaultValue = default)
        {
            if (hd.Contains(key))
                return hd[key].ConvertTo<T>(defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// 是否包含在指定值中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="thisValue"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool IsIn<T>(this T thisValue, params T[] values)
        {
            return values.Contains(thisValue);
        }

        /// <summary>
        /// 获取元素值
        /// </summary>
        /// <param name="element">元素</param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetElementValue(this XElement element, string name)
        {
            return element?.Element(name).Value;
        }

        /// <summary>
        /// 压缩为16位字符，有可能重复，但是概率非常低
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static string To16Chars(this Guid guid)
        {
            long i = 1;
            guid.ToByteArray().ForEach(b => i *= ((int)b + 1));
            return "{0:x}".FormatArgs(i - DateTime.Now.Ticks).PadRight(16, '=');
        }
    }
}
using System;

namespace Odoo.Net
{
    public static class ModelExtensions
    {
        public static T Get<T>(this Self model, Field field)
        {
            return model.Get(field).ConvertTo<T>();
        }
        public static T Get<T>(this Self model, string field)
        {
            return model.Get(field).ConvertTo<T>();
        }
        public static string GetString(this Self model, Field field)
        {
            return model.Get(field).ConvertTo<string>();
        }
        public static string GetString(this Self model, string field)
        {
            return model.Get(field).ConvertTo<string>();
        }
        public static int GetInt32(this Self model, Field field)
        {
            return model.Get(field).ConvertTo<int>();
        }
        public static int GetInt32(this Self model, string field)
        {
            return model.Get(field).ConvertTo<int>();
        }
        public static double GetDouble(this Self model, Field field)
        {
            return model.Get(field).ConvertTo<double>();
        }
        public static double GetDouble(this Self model, string field)
        {
            return model.Get(field).ConvertTo<double>();
        }
        public static DateTime GetDateTime(this Self model, Field field)
        {
            return model.Get(field).ConvertTo<DateTime>();
        }
        public static DateTime GetDateTime(this Self model, string field)
        {
            return model.Get(field).ConvertTo<DateTime>();
        }
        public static T Call<T>(this Self model, string method, params object[] args)
        {
            return model.Call(method, args).ConvertTo<T>();
        }
    }
}

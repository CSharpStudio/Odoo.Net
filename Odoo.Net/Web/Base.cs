using System;
using System.Collections.Generic;

namespace Odoo.Net.Web
{
    [Model(Inherit = "base")]
    public class Base : Model
    {
        public virtual object web_search_read(Self self, string domain, string[] fields, int offset, int limit, string sort)
        {
            List<Map> records = self.Call<List<Map>>("search_read", domain, fields, offset, limit, sort);
            if (records.IsNullOrEmpty())
                return new { Length = 0, Records = Array.Empty<Map>() };
            int length;
            if (limit > 0 && records.Count == limit)
                length = self.Call<int>("search_count", domain);
            else
                length = records.Count + offset;
            return new { Length = length, Records = records };
        }
    }
}

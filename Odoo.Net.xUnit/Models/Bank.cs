using System;
using System.Collections.Generic;

namespace Odoo.Net.xUnit.Models
{
    [Model("res.bank", Description = "Bank", Order = "Name")]
    public class Bank : Model
    {
        public static Field name = Fields.Char(required: true);
        public static Field street = Fields.Char();
        public static Field street2 = Fields.Char();
        public static Field zip = Fields.Char();
        public static Field city = Fields.Char();
        public static Field state = Fields.Many2One();
        public static Field country = Fields.Many2One();
        public static Field email = Fields.Char();
        public static Field phone = Fields.Char();
        public static Field active = Fields.Boolean(defaultValue: false);
        public static Field bic = Fields.Char();

        public IList<(string id, string name)> name_get(Self self)
        {
            var result = new List<(string id, string name)>();
            foreach (var recored in self)
            {
                var b = self.GetString(bic);
                if (b.IsNotEmpty())
                    result.Add((recored.Ids[0], self.GetString(name) + " - " + bic));
                else
                    result.Add((recored.Ids[0], self.GetString(name)));
            }
            return result;
        }
    }
}

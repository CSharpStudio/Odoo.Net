using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Odoo.Net.Core;
using Odoo.Net.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Odoo.Net
{
    [Model("base", IsAbstract = true)]
    public class Model
    {
        MetaModel _meta;
        public MetaModel Meta { get => _meta ?? throw new ArgumentException("MetaModel未设置"); set => _meta = value; }

        public static Field id = new IdField("id", typeof(string), typeof(Model));

        protected Self _create(Self self, Set<Map> dataList)
        {
            var otherFields = new List<string>();
            var ids = new List<string>();
            foreach (var data in dataList)
            {
                var stored = (Map)data["stored"];
                if (!stored.ContainsKey("Id"))
                    stored["Id"] = Guid.NewGuid().To16Chars();
                if (self.Meta.Attr.LogAccess)
                {
                    if (!stored.ContainsKey("CreateUid"))
                        stored["CreateUid"] = self.Env.Uid;
                    if (!stored.ContainsKey("CreateDate"))
                        stored["CreateDate"] = DbNow.Now;
                    if (!stored.ContainsKey("WriteUid"))
                        stored["WriteUid"] = self.Env.Uid;
                    if (!stored.ContainsKey("WriteDate"))
                        stored["WriteDate"] = DbNow.Now;
                }
                //DoInsert(table, stored, self.Env.Cursor, otherFields);
                ids.Add(stored["Id"].ToString());
            }

            var records = self.Browse(ids);
            foreach ((var data, var record) in dataList.Zip(records))
            {
                data["record"] = record;
                foreach (var field in self.Meta.Fields)
                {

                }
            }
            return self;
        }

        public virtual Self create(Self self, Set<Map> valsList)
        {
            if (valsList.IsNullOrEmpty())
                return self.Browse();
            self = self.Browse();
            check_access_rights(self, "create");

            var dataList = new Set<Map>();
            var inversedFields = new List<Field>();
            foreach (var map in valsList)
            {
                var vals = _add_missing_default_values(self, map);
                var data = new Map();
                Map stored = new Map(), inversed = new Map(), inherited = new Map(), @protected = new Map();
                data["stored"] = stored;
                data["inversed"] = inversed;
                data["inherited"] = inherited;
                data["protected"] = @protected;
                foreach ((var key, var val) in vals)
                {
                    var field = self.Meta.FindField(key);
                    if (field == null)
                    {
                        var logger = self.Env.ServiceProvider.GetRequiredService<ILogger<Self>>();
                        logger.LogWarning($"{Meta.Name}.create() with unknown fields:{key}");
                        continue;
                    }
                    ////if (field.CompanyDependent)
                    ////{

                    ////}
                    if (field.Store)
                        stored[key] = val;
                    if (field.Inherited)
                    {
                        ////var m = inherited[field.RelatedField.ModelName];
                        ////m[key] = val;
                    }
                    else if (field.Inverse != null)
                    {
                        inversed[key] = val;
                        inversedFields.Add(field);
                    }

                    ////if(field.Compute != null && !field.Readonly)
                    ////    @protected.Update(self.FieldComputed)
                }
                dataList.Add(data);
            }
            //# create or update parent records:未实现

            var records = _create(self, dataList);

            ////if (self.CheckCompanyAuto)
            ////    records.CheckCompany();

            return records;
        }

        /// <summary>
        /// Verifies that the operation given by ``operation`` is allowed for
        /// the current user according to the access rights.
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="raiseException"></param>
        /// <returns></returns>
        public virtual bool check_access_rights(Self self, string operation, bool raiseException = true)
        {
            ///return Env["ir.model.access"].Check(Meta.Name, operation, raiseException);
            return true;
        }

        /// <summary>
        /// Check the user access rights on the given fields. This raises Access
        /// Denied if the user does not have the rights.Otherwise it returns the
        /// fields(as is if the fields is not falsy, or the readable/writable
        /// fields if fields is falsy).
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public virtual IEnumerable<Field> check_field_access_rights(Self self, string operation, string[] fields)
        {
            if (self.Env.SuperUser)
                return fields.IsNotEmpty() ? fields.Select(Meta.GetField) : Meta.Fields;

            bool ValidField(Self self, Field field)
            {
                if (field != null /*&& field.Groups.IsNotEmpty()*/)
                    return user_has_groups(self, field);
                return true;
            }
            if (fields.IsNullOrEmpty())
                return Meta.Fields.Where(field => ValidField(self, field));
            var invalidFields = fields.Where(field => !ValidField(self, Meta.FindField(field))).ToArray();
            if (invalidFields.IsNotEmpty())
            {
                var logger = self.Env.ServiceProvider.GetRequiredService<ILogger<Self>>();
                logger.LogInformation($"Access Denied by ACLs for operation:{operation},uid:{self.Env.Uid},model:{Meta.Name},fields:{invalidFields.Join(",")}");
                throw new DomainAccessException("You do not have enough rights to access the fields:{0} on {1}({2}).\r\nPlease contact your system administrator.\r\n(Operation:{3})".FormatArgs(invalidFields.Join(","), self.Meta.Attr.Description, Meta.Name, operation));
            }
            return fields.Select(Meta.GetField);
        }
        public virtual bool user_has_groups(Self self, object group)
        {
            return true;
        }

        protected Map _add_missing_default_values(Self self, Map values)
        {
            var logAccessFields = new string[] { "CreateUid", "CreateDate", "WriteUid", "WriteDate" };
            var missingDefaults = Meta.Fields.Where(p => !(self.Meta.Attr.LogAccess && logAccessFields.Contains(p.Name)) || !values.ContainsKey(p.Name)).Select(p => p.Name).ToArray();

            if (missingDefaults.IsNullOrEmpty())
                return values;

            var defaults = default_get(self, missingDefaults);
            //for name, value in defaults.items():
            //if self._fields[name].type == 'many2many' and value and isinstance(value[0], int):
            //    # convert a list of ids into a list of commands
            //    defaults[name] = [(6, 0, value)]
            //elif self._fields[name].type == 'one2many' and value and isinstance(value[0], dict):
            //    # convert a list of dicts into a list of commands
            //    defaults[name] = [(0, 0, x) for x in value]
            defaults.Update(values);
            return defaults;
        }

        public virtual Map default_get(Self self, string[] fieldsList)
        {
            //# trigger view init hook
            //self.view_init(fields_list)

            var defaults = new Map();
            ///var parentFields = new Map();
            //var irDefaultModel = self.Env["ir.default"];
            Map irDefaults = new Map();// irDefaultModel.Call("get_model_defaults", self.Meta.Name).ConvertTo<Map>();

            foreach (var name in fieldsList)
            {
                //1. look up context
                var key = "default_" + name;
                if (self.Context.ContainsKey(key))
                {
                    defaults[name] = self.Context[key];
                    continue;
                }
                //2. look up ir.default
                if (irDefaults.ContainsKey(name))
                {
                    defaults[name] = irDefaults[name];
                    continue;
                }
                var field = Meta.FindField(name);
                //3. look up field.default
                if (field?.Default != null)
                {
                    defaults[name] = field.Default(self);
                    continue;
                }
                //4. delegate to parent model
                if (field?.Inherited == true)
                {
                    //field = field.related_field
                    //parent_fields[field.model_name].append(field.name)
                }
            }

            //# add default values for inherited fields
            //for model, names in parent_fields.items():
            //    defaults.update(self.env[model].default_get(names))

            return defaults;
        }

        /// <summary>
        /// Private implementation of search() method, allowing specifying the uid to use for the access right check.
        /// This is useful for example when filling in the selection list for a drop-down and avoiding access rights errors,
        /// by specifying ``access_rights_uid=1`` to bypass access rights check, but not ir.rules!
        /// This is ok at the security level because this method is private and not callable through XML-RPC.
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        protected Ids _search(Self self, string domain, int offset, int limit, string sort)
        {
            List<string> ids = new List<string>();
            for (int i = 0; i < 10; i++)
                ids.Add("id" + i);
            return ids;
        }

        /// <summary>
        /// Searches for records based on the domain
        /// </summary>
        /// <param name="domain">A search domain</param>
        /// <param name="offset">number of results to ignore (default: none)</param>
        /// <param name="limit">maximum number of records to return (default: all)</param>
        /// <param name="sort">sort string</param>
        /// <returns>at most ``limit`` records matching the search criteria</returns>
        /// <exception cref="DomainAccessException">if user tries to bypass access rules for read on the requested object.</exception>
        public virtual Self search(Self self, string domain, int offset, int limit, string sort)
        {
            var ids = _search(self, domain, offset, limit, sort);
            return self.Browse(ids);
        }

        public virtual int search_count(string domain)
        {
            return 5;
        }

        /// <summary>
        /// Performs a ``search()`` followed by a ``read()``.
        /// </summary>
        /// <param name="domain">Search domain, see ``args`` parameter in ``search()``. Defaults to an empty domain that will match all records.</param>
        /// <param name="fields">List of fields to read, see ``fields`` parameter in ``read()``. Defaults to all fields.</param>
        /// <param name="offset">Number of records to skip, see ``offset`` parameter in ``search()``. Defaults to 0.</param>
        /// <param name="limit">Maximum number of records to return, see ``limit`` parameter in ``search()``. Defaults to no limit.</param>
        /// <param name="sort">Columns to sort result, see ``order`` parameter in ``search()``. Defaults to no sort.</param>
        /// <returns>List of dictionaries containing the asked fields.</returns>
        public virtual List<Map> search_read(Self self, string domain, string[] fields, int offset, int limit, string sort)
        {
            var records = search(self, domain, offset, limit, sort);
            if (records.Ids.IsNullOrEmpty())
                return new List<Map>();

            if (fields.IsNotEmpty() && fields.Length == 1 && "Id".CIEquals(fields[0]))
                return records.Ids.Select(id => new Map { { "Id", id } }).ToList();

            var result = read(records, fields);
            return result;
        }

        /// <summary>
        /// read([fields])
        /// Reads the requested fields for the records in ``self``, low-level/RPC
        /// method.In Python code, prefer :meth:`~.browse`.
        /// </summary>
        /// <param name="fields">list of field names to return (default is all fields)</param>
        /// <returns>a list of dictionaries mapping field names to their values,
        /// with one dictionary per record</returns>
        /// <exception cref="DomainAccessException">if user has no read rights on some of the given records</exception>
        public virtual List<Map> read(Self self, string[] fields)
        {
            var result = new List<Map>();
            var metaFields = check_field_access_rights(self, "read", fields);
            var storedFields = new List<Field>();
            foreach (var field in metaFields)
            {
                if (field.Store)
                    storedFields.Add(field);
                else if (field.Compute != null)
                {
                    foreach (var dotname in field.Depends)
                    {
                        var f = Meta.GetField(dotname.Split('.')[0]);
                        if (f.Prefetch && (f.Groups.IsNullOrEmpty() || user_has_groups(self, f.Groups)))
                            storedFields.Add(f);
                    }
                }
            }

            _read(self, storedFields.ToArray());

            foreach (var m in self)
            {
                var map = new Map();
                foreach (var field in metaFields)
                {
                    map[field.Name] = m.Get(field);
                }
                result.Add(map);
            }
            return result;
        }

        protected void _read(Self self, IList<Field> fields)
        {
            if (self.Ids.IsNullOrEmpty())
                return;
            check_access_rights(self, "read");
            //
        }

        /// <summary>
        /// Returns a textual representation for the records in ``self``.
        /// By default this is the value of the ``display_name`` field.
        /// </summary>
        /// <returns>list of pairs ``(id, text_repr)`` for each records</returns>
        public virtual IList<(string Id, string Name)> NameGet(Self self)
        {
            var list = new List<(string Id, string Name)>();
            var name = self.Meta.Attr.DisplayName ?? "name";
            var field = Meta.FindField(name);
            if (field != null)
            {
                foreach (var record in self)
                    list.Add((Id: record.Ids[0], Name: record.Get(field)?.ToString()));
            }
            else
            {
                var model = Meta.Name;
                foreach (var record in self)
                    list.Add((Id: record.Ids[0], Name: model + record.Ids[0]));
            }
            return list;
        }
    }
}

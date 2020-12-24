using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odoo.Net
{
    /// <summary>
    /// 关联ID
    /// </summary>
    public class RefId
    {
        public string Id { get; set; }

        public Self Model { get; set; }

        public RefId(string id)
        {
            Id = id;
        }
        public RefId(string id, Self model)
        {
            Id = id;
            Model = model;
        }
        public override bool Equals(object obj)
        {
            if (obj is RefId other)
                return Model?.Meta == other.Model?.Meta && Id == other.Id;
            return false;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        public static implicit operator RefId(string id)
        {
            return new RefId(id);
        }
        public static implicit operator RefId(Self model)
        {
            return new RefId(model.Ids[0], model);
        }
        public static implicit operator string(RefId id)
        {
            return id.Id;
        }
        public override string ToString()
        {
            return Id;
        }
    }
}

using System;
using System.Collections.Generic;
using FluentNHibernate.Mapping;

namespace Monitor.Database
{
    public class Resource
    {
        public virtual int Id { get; protected set; }
        public virtual Guid Guid { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual IList<Sensor> Sensors {get; set; }

        public Resource()
        {
            Sensors = new List<Sensor>();
        }

        public class ResourceMap : ClassMap<Resource>
        {
            public ResourceMap()
            {
                Id(x => x.Id);
                Map(x => x.Guid).Not.Nullable().Unique();
                Map(x => x.Name).Nullable();
                Map(x => x.Description).Nullable();
                HasMany(x => x.Sensors).Cascade.All().Inverse();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using FluentNHibernate.Mapping;

namespace Monitor.Database
{
    public class Sensor
    {
        public virtual int Id { get; protected set; }
        public virtual Guid Guid { get; set; }
        public virtual string Metric { get; set; }
        public virtual string Unit { get; set; }
        public virtual bool Complex { get; set; }
        public virtual Resource Resource { get; set; }
        public virtual IList<Measurement> Measurements { get; set; }
        // TODO complexMetric field?

        public class SensorMap : ClassMap<Sensor>
        {
            public SensorMap()
            {
                Id(x => x.Id);
                Map(x => x.Guid).Not.Nullable().Unique();
                Map(x => x.Metric).Not.Nullable();
                Map(x => x.Unit).Not.Nullable();
                Map(x => x.Complex).Not.Nullable();

                References(x => x.Resource)
                    .Cascade.All();
                HasMany(x => x.Measurements).Cascade.All().Inverse();
            }
        }
    }
}
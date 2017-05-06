using System;
using FluentNHibernate.Mapping;

namespace Monitor.Database
{
    public class Measurement
    {
        public virtual int Id { get; protected set; }
        public virtual float Value { get; set; }
        public virtual DateTime Timestamp { get; set; }
        public virtual Sensor Sensor { get; set; }

        public class MeasurementMap : ClassMap<Measurement>
        {
            public MeasurementMap()
            {
                Id(x => x.Id);
                Map(x => x.Value);
                Map(x => x.Timestamp);
                References(x => x.Sensor);
            }
        }
    }
}
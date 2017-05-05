using System;
using FluentNHibernate.Mapping;

namespace Monitor.Database
{
    public class ComplexMetric
    {
        public virtual int Id { get; protected set; }
        public virtual Sensor Sensor { get; set; }
        public virtual int Frequency { get; set; }
        public virtual int WindowSize { get; set; }
        public virtual DateTime TimeStart { get; set; }


        public class ComplexMetricMap : ClassMap<ComplexMetric>
        {
            public ComplexMetricMap()
            {
                Id(x => x.Id);
                References(x => x.Sensor).Cascade.All();
                Map(x => x.Frequency);
                Map(x => x.WindowSize);
                Map(x => x.TimeStart);
            }
        }
    }
}
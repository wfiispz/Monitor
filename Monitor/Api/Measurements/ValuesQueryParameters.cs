using System;

namespace Monitor.Api.Measurements
{
    public class ValuesQueryParameters
    {
        public Guid Id { get; set; }
        public DateTime TimeFrom { get; set; }
        public DateTime TimeTo { get; set; }

        public ValuesQueryParameters()
        {
            TimeTo = DateTime.Now;
            TimeFrom = TimeTo - TimeSpan.FromMinutes(3);
        }
    }
}
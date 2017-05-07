using System;

namespace Monitor.Api.Measurements
{
    public class ValuesQueryParameters
    {
        public Guid Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public ValuesQueryParameters()
        {
            To = DateTime.Now;
            From = To - TimeSpan.FromMinutes(5);
        }
    }
}
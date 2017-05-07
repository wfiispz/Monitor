namespace Monitor.Modules.Measurements.Query
{
    public class Sensor
    {
        public string Host { get; set; }
        public string Metric { get; set; }
        public string Unit { get; set; }
        public bool Complex { get; set; }
//        public float MaxValue { get; set; }
        public string Values { get; set; }
    }
}
namespace Monitor.Config
{
    public class Configuration
    {
        public string DatabaseFilepath { get; set; }
        public string UrlBasePath {get;set;}
        public string SensorUDPIp { get; set; }
        public int SensorUDPPort { get; set; }
        public bool DebugMode { get; set; }
    }
}
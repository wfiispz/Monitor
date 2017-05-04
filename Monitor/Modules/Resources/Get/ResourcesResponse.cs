using Monitor.Sensors;

namespace Monitor.Modules.Resources.Get
{
    public class ResourcesResponse
    {
        public Resource[] Resources { get; set; }
        public PageDetails Page { get; set; }
    }
}
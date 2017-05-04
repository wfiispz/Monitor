using System;
using Monitor.CommandBus;

namespace Monitor.Sensors
{
    public class GetSensors:IQuery<ResourcesQueryResult>
    {
        public string Name { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
        public int TotalCount { get; set; }

        public ResourcesQueryResult Query()
        {
            throw new NotImplementedException();
        }
    }
}
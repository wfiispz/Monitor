using System.Collections.Generic;

namespace Monitor.Sensors
{
    public class ResourcesQueryResult
    {
        public ResourcesQueryResult(IEnumerable<SensorDetails> sensors, int pageSize, int page, int totalCount)
        {
            Sensors = sensors;
            PageSize = pageSize;
            Page = page;
            TotalCount = totalCount;
        }

        public IEnumerable<SensorDetails> Sensors { get; private set; }
        public int PageSize { get; private set; }
        public int Page { get; private set; }
        public int TotalCount { get; private set; }
    }
}
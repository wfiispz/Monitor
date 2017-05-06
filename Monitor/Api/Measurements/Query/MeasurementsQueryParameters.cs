namespace Monitor.Api.Measurements.Query
{
    public class MeasurementsQueryParameters
    {
        public int PageSize { get; set; }
        public int Page { get; set; }

        public MeasurementsQueryParameters()
        {
            PageSize = 100;
            Page = 1;
        }
    }
}
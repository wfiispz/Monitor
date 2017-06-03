namespace Monitor.Api.Measurements.Query
{
    internal interface IComplexMetricValuesRetriever
    {
        bool TryGet(ValuesQueryParameters parameters, out ValuesResponse values);
    }
}
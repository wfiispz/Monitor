using NHibernate;

namespace Monitor.Api.Measurements.Query
{
    internal interface ISimpleMetricValuesRetriever
    {
        ValuesResponse GetSimpleMetric(ValuesQueryParameters parameters, ISession session);
    }
}
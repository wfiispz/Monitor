using System;

namespace Monitor.Api.Measurements.Query
{
    internal interface IMeasurementsQuery
    {
        MeasurementsResponse All(MeasurementsQueryParameters queryParameters);
        Sensor GetById(Guid id);
        ValuesResponse GetValues(ValuesQueryParameters parameters);
    }
}
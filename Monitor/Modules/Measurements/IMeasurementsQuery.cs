using System;

namespace Monitor.Modules.Measurements
{
    internal interface IMeasurementsQuery
    {
        MeasurementsResponse All(MeasurementsQueryParameters queryParameters);
        Sensor GetById(Guid id);
        ValuesResponse GetValues(ValuesQueryParameters parameters);
    }
}
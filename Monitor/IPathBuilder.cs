using System;

namespace Monitor
{
    internal interface IPathBuilder
    {
        string CreateForSensor(Guid guid);
        string CreateForResource(Guid guid);
        string CreateForValues(Guid guid);
    }
}
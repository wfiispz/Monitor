using System;

namespace Monitor
{
    internal interface IPathBuilder
    {
        string CreateForSensor(Guid guid);
    }
}
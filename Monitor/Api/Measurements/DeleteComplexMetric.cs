using System;
using Monitor.CommandBus;

namespace Monitor.Api.Measurements
{
    public class DeleteComplexMetric:ICommand
    {
        public Guid Id { get; set; }
    }
}
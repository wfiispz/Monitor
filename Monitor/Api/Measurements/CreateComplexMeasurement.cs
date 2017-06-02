using System;
using Monitor.CommandBus;

namespace Monitor.Api.Measurements
{
    public class CreateComplexMeasurement:ICommand
    {
        public Guid BaseMeasurement { get; set; }
        public string Description { get; set; }
        public int Frequency { get; set; }
        public int Windowsize { get; set; }
    }
}
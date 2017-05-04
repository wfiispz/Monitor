using System;
using Monitor.CommandBus;

namespace Monitor.Sensors
{
    public class DeleteSensor : ICommand
    {
        public Guid Id { get; set; }
    }
}
using System;
using Monitor.CommandBus;

namespace Monitor.Modules.Measurements
{
    internal class DeleteValues:ICommand
    {
        private Guid Id { get; set; }
    }
}
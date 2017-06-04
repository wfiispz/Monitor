using System;
using Monitor.CommandBus;

namespace Monitor.Api.Measurements
{
    internal class DeleteValues:ICommand
    {
        public Guid Id { get; set; }
    }
}
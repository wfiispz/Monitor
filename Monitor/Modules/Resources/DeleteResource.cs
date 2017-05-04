using System;
using Monitor.CommandBus;

namespace Monitor.Modules.Resources
{
    public class DeleteResource:ICommand
    {
        public Guid Id { get; set; }
    }
}
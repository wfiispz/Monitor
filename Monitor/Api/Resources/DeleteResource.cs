using System;
using Monitor.CommandBus;

namespace Monitor.Api.Resources
{
    public class DeleteResource:ICommand
    {
        public Guid Id { get; set; }
    }
}
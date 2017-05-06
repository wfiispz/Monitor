using System;
using Monitor.CommandBus;

namespace Monitor.Api.Measurements
{
    [CommandHandler(typeof(DeleteValues))]
    internal class DeleteValuesHandler : IHandleCommand<DeleteValues>
    {
        public object Handle(DeleteValues command)
        {
            throw new NotImplementedException();
        }
    }
}
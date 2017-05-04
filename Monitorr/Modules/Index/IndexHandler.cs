using Monitor.CommandBus;
using Nancy;

namespace Monitor.Modules.Index
{
    [CommandHandler(typeof(IndexCommand))]
    internal class IndexHandler : IHandleCommand<IndexCommand>
    {
        public Response Handle(IndexCommand command)
        {
            return "Command processed. Value: " + command.Value;
        }
    }
}
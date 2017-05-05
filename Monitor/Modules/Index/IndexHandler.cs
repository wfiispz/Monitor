using Monitor.CommandBus;

namespace Monitor.Modules.Index
{
    [CommandHandler(typeof(IndexCommand))]
    internal class IndexHandler : IHandleCommand<IndexCommand>
    {
        public object Handle(IndexCommand command)
        {
            return "Command processed. Value: " + command.Value;
        }
    }
}
using Monitor.CommandBus;

namespace Monitor.Api.Index
{
    internal class IndexCommand:ICommand
    {
        public IndexCommand(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}
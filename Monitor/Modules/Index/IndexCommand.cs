using Monitor.CommandBus;

namespace Monitor.Modules.Index
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
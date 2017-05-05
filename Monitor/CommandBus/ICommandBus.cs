namespace Monitor.CommandBus
{
    public interface ICommandBus
    {
        object Handle<TCommand>(TCommand command) where TCommand:ICommand;
    }
}
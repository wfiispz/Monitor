using Nancy;

namespace Monitor.CommandBus
{
    public interface ICommandBus
    {
        Response Handle<TCommand>(TCommand command) where TCommand:ICommand;
    }
}
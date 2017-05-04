using Nancy;

namespace Monitor.CommandBus
{
    interface IHandleCommand
    {
    }

    interface IHandleCommand<T>:IHandleCommand where T:ICommand
    {
        Response Handle(T command);
    }
}

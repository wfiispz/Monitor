namespace Monitor.CommandBus
{
    interface IHandleCommand
    {
    }

    interface IHandleCommand<T>:IHandleCommand where T:ICommand
    {
        object Handle(T command);
    }
}

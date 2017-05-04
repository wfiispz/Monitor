namespace Monitor.CommandBus
{
    public interface IQuery<TResult>
    {
        TResult Query();
    }
}
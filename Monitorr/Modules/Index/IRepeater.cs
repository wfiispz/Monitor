namespace Monitor.Modules.Index
{
    public interface IRepeater
    {
        string Repeat(string value);
        string FromDb(string value);
    }
}
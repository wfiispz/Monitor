using Simple.Data;

namespace Monitor.Persistence
{
    public class DbAdapter
    {
        public DbAdapter()
        {
            SimpleData = Database.OpenNamedConnection("ProductionDB");
        }

        public dynamic SimpleData { get; }
    }
}
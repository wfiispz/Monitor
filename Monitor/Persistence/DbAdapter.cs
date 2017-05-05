using Simple.Data;

namespace Monitor.Persistence
{
    public class DbAdapter : IDbAdapter
    {
        public DbAdapter()
        {
            SimpleData = Database.OpenNamedConnection("ProductionDB");
        }

        public dynamic SimpleData { get; }
    }
}
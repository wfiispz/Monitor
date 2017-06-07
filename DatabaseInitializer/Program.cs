using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monitor;
using Monitor.Config;
using Monitor.Database;

namespace DatabaseInitializer
{
    class Program
    {
        static void Main(string[] args)
        {
            string configPath = args.Length >1 ? args[1] : ConfigurationLoader.DefaultConfigFilePath;
            var configuration = new ConfigurationLoader(new JsonDeserializer(), configPath).Load();
            var sessionFactory = new SessionFactoryProvider().Create(true, configuration.DatabaseFilepath);
            var session = sessionFactory.OpenSession();
            Console.Out.WriteLine("Database schema exported");
        }

        
    }
}

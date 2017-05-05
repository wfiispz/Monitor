using System;
using System.IO;
using Monitor.Persistence;
using Moq;
using NUnit.Framework;
using Simple.Data;
using Simple.Data.RawSql;

namespace Monitor.UnitTests.Resources
{
    public class TestDatabase : IDisposable
    {
        private string _dbFileName;

        public TestDatabase()
        {
            // TODO try to configure in memory db properly
            _dbFileName = $"{TestContext.CurrentContext.TestDirectory}\\test{Guid.NewGuid()}.db";
//            if(File.Exists(_dbFileName))
//                File.Delete(_dbFileName);
            _dbFileName = ":memory:";
            Database db = Database.OpenConnection($"Data Source={_dbFileName};Version=3;New=True;");
            db.Execute(File.ReadAllText(TestContext.CurrentContext.TestDirectory + "/DBSchema.sql"));
            var dbMock = new Mock<IDbAdapter>();
            dbMock.Setup(x => x.SimpleData).Returns(db);
            Db = dbMock.Object;
        }

        public IDbAdapter Db { get; }

        public void Dispose()
        {
            File.Delete(_dbFileName);
        }
    }
}
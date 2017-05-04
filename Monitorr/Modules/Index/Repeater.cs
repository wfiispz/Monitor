using Simple.Data;

namespace Monitor.Modules.Index
{
    internal class Repeater : IRepeater
    {
        public string Repeat(string value)
        {
            return value;
        }

        public string FromDb(string value)
        {
            var db = Database.Open();
            var result = db.DummyData.FindById(11);
            return result.String;
        }
    }
}
﻿
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
            var db = Database.OpenNamedConnection("ProductionDB");
            var result = db.DummyData.FindById(int.Parse(value));
            return result.String;
        }
    }
}
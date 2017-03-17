using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace Monitor.Modules
{
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = parameters => "Its working!!!";
        }
    }
}
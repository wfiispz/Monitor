using System.Collections.Generic;
using Nancy.Security;

namespace Monitor.Api.Auth
{
    internal class UserIdentity : IUserIdentity
    {
        public string UserName => "Authenticated";
        public IEnumerable<string> Claims => new[] {"Access"};
    }
}
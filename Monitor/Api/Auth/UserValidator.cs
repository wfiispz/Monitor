using Monitor.Config;
using Nancy.Authentication.Basic;
using Nancy.Security;

namespace Monitor.Api.Auth
{
    internal class UserValidator : IUserValidator
    {
        private readonly Configuration _configuration;

        public UserValidator(Configuration configuration)
        {
            _configuration = configuration;
        }

        public IUserIdentity Validate(string username, string password)
        {
            if (_configuration.Username == username && _configuration.Password == password)
                return new UserIdentity();

            return null;
        }
    }
}
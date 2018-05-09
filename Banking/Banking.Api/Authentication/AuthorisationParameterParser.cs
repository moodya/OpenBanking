using System;
using System.Linq;
using System.Text;

namespace Banking.Api.Authentication
{
    public class AuthorisationParameterParser : IAuthorisationParameterParser
    {
        public LoginDetails Parse(string authorizationParameter, char separator = '|')
        {
            if (string.IsNullOrWhiteSpace(authorizationParameter))
            {
                return null;
            }

            var fromBase64String = Convert.FromBase64String(authorizationParameter);
            var authString = Encoding.ASCII.GetString(fromBase64String).Split(separator);

            if (!authString.Any() || authString.Length != 2)
            {
                return null;
            }

            var username = authString[0];
            var password = authString[1];

            return new LoginDetails(username, password);
        }
    }
}
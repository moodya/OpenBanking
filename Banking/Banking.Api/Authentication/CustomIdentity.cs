using System.Security.Principal;
using Seterlund.CodeGuard;

namespace Banking.Api.Authentication
{
    public class CustomIdentity : IIdentity
    {
        public CustomIdentity(string name, string authenticationType, bool isAuthenticated)
        {
            Guard.That(() => name).IsNotNull();
            Guard.That(() => authenticationType).IsNotNull();
            
            Name = name;
            AuthenticationType = authenticationType;
            IsAuthenticated = isAuthenticated;
        }

        public string Name { get; }
        public string AuthenticationType { get; }
        public bool IsAuthenticated { get; }
    }
}
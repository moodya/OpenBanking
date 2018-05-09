using System.Security.Principal;
using Seterlund.CodeGuard;

namespace Banking.Api.Authentication
{
    public class CustomPrincipal : IPrincipal
    {
        public CustomPrincipal(IIdentity identity)
        {
            Guard.That(() => identity).IsNotNull();
            
            Identity = identity;
        }

        public IIdentity Identity { get; }

        public bool IsInRole(string role)
        {
            return true;
        }
    }
}
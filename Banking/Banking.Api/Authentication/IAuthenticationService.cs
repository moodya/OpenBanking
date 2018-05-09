using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace Banking.Api.Authentication
{
    public interface IAuthenticationService
    {
        Task<IPrincipal> AuthenticateAsync(string userName, string password, CancellationToken cancellationToken);
    }
}
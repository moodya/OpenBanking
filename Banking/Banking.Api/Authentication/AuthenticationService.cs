using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Banking.Model.Repositories;
using Seterlund.CodeGuard;

namespace Banking.Api.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IUserRepository userRepository)
        {
            Guard.That(() => userRepository).IsNotNull();

            _userRepository = userRepository;
        }

        public async Task<IPrincipal> AuthenticateAsync(string userName, string password, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return null;
            }

            var user = await _userRepository.GetSingleAsync(x => x.Username == userName && x.Password == password);

            if (user == null)
            {
                return null;
            }

            return (IPrincipal)new CustomPrincipal(new CustomIdentity(user.Name, string.Empty, true));
        }
    }
}
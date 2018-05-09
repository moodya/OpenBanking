using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using Seterlund.CodeGuard;

namespace Banking.Api.Authentication
{
    public class BasicAuthenticationFilter : ActionFilterAttribute, IAuthenticationFilter
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IAuthorisationParameterParser _authorisationParameterParser;

        public BasicAuthenticationFilter(IAuthenticationService authenticationService, IAuthorisationParameterParser authorisationParameterParser)
        {
            Guard.That(() => authenticationService).IsNotNull();
            Guard.That(() => authorisationParameterParser).IsNotNull();
            
            _authenticationService = authenticationService;
            _authorisationParameterParser = authorisationParameterParser;
        }
        
        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            HttpRequestMessage request = context.Request;
            AuthenticationHeaderValue authorization = request.Headers.Authorization;

            if (authorization == null)
            {
                return;
            }

            if (authorization.Scheme != "Basic")
            {
                return;
            }

            if (string.IsNullOrEmpty(authorization.Parameter))
            {
                context.ErrorResult = new AuthenticationFailureResult("Missing credentials", request);
                return;
            }

            var loginDetails = _authorisationParameterParser.Parse(authorization.Parameter, '|');

            if (loginDetails == null)
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid credentials", request);
                return;
            }
            
            var principal = await _authenticationService.AuthenticateAsync(loginDetails.Username, loginDetails.Password, cancellationToken);

            if (principal == null)
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid username or password", request);
                return;
            }

            context.Principal = principal;
        }
        
        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}
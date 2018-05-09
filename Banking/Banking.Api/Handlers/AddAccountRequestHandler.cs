using System.Threading.Tasks;
using Banking.Api.Requests;
using Banking.Api.Services;
using Banking.Api.Validation;
using MediatR;
using Seterlund.CodeGuard;

namespace Banking.Api.Handlers
{
    public class AddAccountRequestHandler : IAsyncRequestHandler<AddAccountRequest, Banking.Contract.Account>
    {
        private readonly IUserService _userService;
        private readonly IAddAccountRequestValidator _requestValidator;

        public AddAccountRequestHandler(IUserService userService, IAddAccountRequestValidator requestValidator)
        {
            Guard.That(() => userService).IsNotNull();
            Guard.That(() => requestValidator).IsNotNull();

            _userService = userService;
            _requestValidator = requestValidator;
        }

        public Task<Banking.Contract.Account> Handle(AddAccountRequest message)
        {
            _requestValidator.Validate(message).ThrowIfNotValid();
            return _userService.AddAsync(message?.Account);
        }
    }
}
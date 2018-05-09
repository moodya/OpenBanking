using System.Collections.Generic;
using System.Threading.Tasks;
using Banking.Api.Requests;
using Banking.Api.Services;
using Banking.Contract;
using MediatR;
using Seterlund.CodeGuard;

namespace Banking.Api.Handlers
{
    public class UserRequestHandler : IAsyncRequestHandler<UserRequest, IEnumerable<User>>
    {
        private readonly IUserService _userService;

        public UserRequestHandler(IUserService userService)
        {
            Guard.That(() => userService).IsNotNull();

            _userService = userService;
        }
        public async Task<IEnumerable<Banking.Contract.User>> Handle(UserRequest message)
        {
            return await _userService.GetAsync();
        }
    }
}
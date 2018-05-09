using System.Collections.Generic;
using Banking.Contract;
using MediatR;

namespace Banking.Api.Requests
{
    public class UserRequest : IAsyncRequest<IEnumerable<User>>
    {
    }
}
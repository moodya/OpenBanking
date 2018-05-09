using MediatR;
using Seterlund.CodeGuard;

namespace Banking.Api.Requests
{
    public class AddAccountRequest : IAsyncRequest<Contract.Account>
    {
        public AddAccountRequest(Contract.Account account)
        {
            Guard.That(() => account).IsNotNull();

            Account = account;
        }

        public Contract.Account Account { get; }
    }
}
using System.Linq;
using System.Threading.Tasks;
using Banking.Api.Requests;
using Banking.Contract;
using MediatR;
using Seterlund.CodeGuard;

namespace Banking.Api.Handlers
{
    public class SortedAccountTransactionsRequestHandler :
        IAsyncRequestHandler<AccountTransactionsRequest, AccountTransactions>
    {
        private readonly IAsyncRequestHandler<AccountTransactionsRequest, AccountTransactions> _decoratedHandler;

        public SortedAccountTransactionsRequestHandler(
            IAsyncRequestHandler<AccountTransactionsRequest, AccountTransactions> decoratedHandler)
        {
            Guard.That(() => decoratedHandler).IsNotNull();

            _decoratedHandler = decoratedHandler;
        }

        public async Task<AccountTransactions> Handle(AccountTransactionsRequest message)
        {
            var accountTransactions = await _decoratedHandler.Handle(message);
            accountTransactions.Transactions = accountTransactions.Transactions.OrderBy(x => x.Type);
            return accountTransactions;
        }
    }
}
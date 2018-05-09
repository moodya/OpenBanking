using System.Threading.Tasks;
using Banking.Api.Requests;
using Banking.Api.Services;
using Banking.Api.Validation;
using Banking.Contract;
using MediatR;
using Seterlund.CodeGuard;

namespace Banking.Api.Handlers
{
    public class AccountTransactionsRequestHandler : IAsyncRequestHandler<AccountTransactionsRequest, AccountTransactions>
    {
        private readonly ITransactionService _transactionService;
        private readonly IAccountTransactionsRequestValidator _requestValidator;

        public AccountTransactionsRequestHandler(ITransactionService transactionService, IAccountTransactionsRequestValidator requestValidator)
        {
            Guard.That(() => transactionService).IsNotNull();
            Guard.That(() => requestValidator).IsNotNull();

            _transactionService = transactionService;
            _requestValidator = requestValidator;
        }

        public Task<AccountTransactions> Handle(AccountTransactionsRequest message)
        {
            _requestValidator.Validate(message).ThrowIfNotValid();
            return _transactionService.GetAccountTransactionsAsync(message.BankName, message.AccountNumber);
        }
    }
}
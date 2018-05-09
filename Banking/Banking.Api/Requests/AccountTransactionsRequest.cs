using Banking.Contract;
using MediatR;
using Seterlund.CodeGuard;

namespace Banking.Api.Requests
{
    public class AccountTransactionsRequest : IAsyncRequest<AccountTransactions>
    {
        public AccountTransactionsRequest(string bankName, string accountNumber)
        {
            Guard.That(() => bankName).IsNotNull();
            Guard.That(() => accountNumber).IsNotNull();
            
            BankName = bankName;
            AccountNumber = accountNumber;
        }

        public string BankName { get; }
        public string AccountNumber { get; }
    }
}
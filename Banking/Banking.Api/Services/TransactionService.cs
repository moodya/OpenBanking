using System.Threading.Tasks;
using Banking.Api.Clients;
using Banking.Contract;
using Banking.Model.Repositories;
using Seterlund.CodeGuard;

namespace Banking.Api.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IBankRepository _bankRepository;
        private readonly IAccountTransactionsHttpClientFactory _accountTransactionsHttpClientFactory;

        public TransactionService(
            IAccountRepository accountRepository,
            IBankRepository bankRepository,
            IAccountTransactionsHttpClientFactory accountTransactionsHttpClientFactory)
        {
            Guard.That(() => accountRepository).IsNotNull();
            Guard.That(() => bankRepository).IsNotNull();
            Guard.That(() => accountTransactionsHttpClientFactory).IsNotNull();

            _accountRepository = accountRepository;
            _bankRepository = bankRepository;
            _accountTransactionsHttpClientFactory = accountTransactionsHttpClientFactory;
        }

        public async Task<AccountTransactions> GetAccountTransactionsAsync(string bankName, string accountNumber)
        {
            var modelBank = await _bankRepository.GetSingleAsync(x => x.Name == bankName);
            var modelAccount = await _accountRepository.GetSingleAsync(x => x.Bank.Name == bankName && x.Number == accountNumber);
            var client = _accountTransactionsHttpClientFactory.Create(modelBank);
            return await client.GetAsync(modelAccount.Number);
        }
    }
}
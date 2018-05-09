using System.Threading.Tasks;
using Banking.Contract;

namespace Banking.Api.Services
{
    public interface ITransactionService
    {
        Task<AccountTransactions> GetAccountTransactionsAsync(string bankName, string accountNumber);
    }
}
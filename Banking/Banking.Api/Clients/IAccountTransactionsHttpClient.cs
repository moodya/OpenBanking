using System.Threading.Tasks;
using Banking.Contract;

namespace Banking.Api.Clients
{
    public interface IAccountTransactionsHttpClient
    {
        Task<AccountTransactions> GetAsync(string accountNumber);
    }
}
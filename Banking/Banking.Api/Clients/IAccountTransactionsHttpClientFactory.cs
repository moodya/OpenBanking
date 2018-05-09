using Banking.Model.Entities;

namespace Banking.Api.Clients
{
    public interface IAccountTransactionsHttpClientFactory
    {
        IAccountTransactionsHttpClient Create(Bank bank);
    }
}
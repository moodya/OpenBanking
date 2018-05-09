using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Banking.Model.Entities;
using Seterlund.CodeGuard;

namespace Banking.Api.Clients
{
    public class AccountTransactionsHttpClientFactory : IAccountTransactionsHttpClientFactory
    {
        private readonly IMapper _mapper;

        public AccountTransactionsHttpClientFactory(IMapper mapper)
        {
            Guard.That(() => mapper).IsNotNull();

            _mapper = mapper;
        }

        public IAccountTransactionsHttpClient Create(Bank bank)
        {
            var accountTransactionsHttpClient = CreateAccountTransactionsHttpClientInstance(bank.ClientName, _mapper, bank.ClientBaseAddress);

            if (accountTransactionsHttpClient == null)
            {
                throw new Exception("Cant find the client related to that bank!");
            }

            return accountTransactionsHttpClient;
        }

        private static IAccountTransactionsHttpClient CreateAccountTransactionsHttpClientInstance(string clientName, IMapper mapper, string clientBaseAddress)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var type = assembly.GetTypes().First(t => t.Name == clientName);
            return (IAccountTransactionsHttpClient)Activator.CreateInstance(type, mapper, clientBaseAddress);
        }
    }
}